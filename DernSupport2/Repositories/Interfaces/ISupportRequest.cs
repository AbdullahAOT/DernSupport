using DernSupport2.Models.DTOs;
using Microsoft.AspNetCore.Mvc;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace DernSupport2.Repositories.Interfaces
{
    public interface ISupportRequest
    {
        Task<ActionResult<IEnumerable<SupportRequestDTO>>> GetSupportRequests();
        Task<ActionResult<SupportRequestDTO>> GetSupportRequest(int id);
        Task<IActionResult> PutSupportRequest(int id, SupportRequestDTO supportRequestDto);
        Task<ActionResult<SupportRequestDTO>> PostSupportRequest(SupportRequestDTO supportRequestDto);
        Task<IActionResult> DeleteSupportRequest(int id);
        bool SupportRequestExists(int id);
    }
}
