using System.Collections.Generic;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using DernSupport2.Repositories.Interfaces;
using DernSupport2.Models.DTOs;

namespace DernSupport2.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class SupportRequestsController : ControllerBase
    {
        private readonly ISupportRequest _supportRequestService;

        public SupportRequestsController(ISupportRequest supportRequestService)
        {
            _supportRequestService = supportRequestService;
        }

        [HttpGet]
        public async Task<ActionResult<IEnumerable<SupportRequestDTO>>> GetSupportRequests()
        {
            return await _supportRequestService.GetSupportRequests();
        }

        [HttpGet("{id}")]
        public async Task<ActionResult<SupportRequestDTO>> GetSupportRequest(int id)
        {
            return await _supportRequestService.GetSupportRequest(id);
        }

        [HttpPut("{id}")]
        public async Task<IActionResult> PutSupportRequest(int id, SupportRequestDTO supportRequestDto)
        {
            return await _supportRequestService.PutSupportRequest(id, supportRequestDto);
        }

        [HttpPost]
        public async Task<ActionResult<SupportRequestDTO>> PostSupportRequest(SupportRequestDTO supportRequestDto)
        {
            return await _supportRequestService.PostSupportRequest(supportRequestDto);
        }

        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteSupportRequest(int id)
        {
            return await _supportRequestService.DeleteSupportRequest(id);
        }
    }
}
