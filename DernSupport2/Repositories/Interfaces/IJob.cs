using DernSupport2.Models.DTOs;
using Microsoft.AspNetCore.Mvc;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace DernSupport2.Repositories.Interfaces
{
    public interface IJob
    {
        Task<ActionResult<IEnumerable<JobDTO>>> GetJobs();
        Task<ActionResult<JobDTO>> GetJob(int id);
        Task<IActionResult> PutJob(int id, JobDTO jobDto);
        Task<ActionResult<JobDTO>> PostJob(JobDTO jobDto);
        Task<IActionResult> DeleteJob(int id);
        bool JobExists(int id);
    }
}
