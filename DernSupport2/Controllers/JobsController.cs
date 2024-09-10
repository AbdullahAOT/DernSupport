using System.Collections.Generic;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using DernSupport2.Repositories.Interfaces;
using DernSupport2.Models.DTOs;

namespace DernSupport2.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class JobsController : ControllerBase
    {
        private readonly IJob _jobService;

        public JobsController(IJob jobService)
        {
            _jobService = jobService;
        }

        [HttpGet]
        public async Task<ActionResult<IEnumerable<JobDTO>>> GetJobs()
        {
            return await _jobService.GetJobs();
        }

        [HttpGet("{id}")]
        public async Task<ActionResult<JobDTO>> GetJob(int id)
        {
            return await _jobService.GetJob(id);
        }

        [HttpPut("{id}")]
        public async Task<IActionResult> PutJob(int id, JobDTO jobDto)
        {
            return await _jobService.PutJob(id, jobDto);
        }

        [HttpPost]
        public async Task<ActionResult<JobDTO>> PostJob(JobDTO jobDto)
        {
            return await _jobService.PostJob(jobDto);
        }

        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteJob(int id)
        {
            return await _jobService.DeleteJob(id);
        }
    }
}
