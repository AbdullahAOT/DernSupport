using DernSupport2.Models.DTOs;
using Microsoft.AspNetCore.Mvc;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace DernSupport2.Repositories.Interfaces
{
    public interface IFeedback
    {
        Task<ActionResult<IEnumerable<FeedbackDTO>>> GetFeedbacks();
        Task<ActionResult<FeedbackDTO>> GetFeedback(int id);
        Task<IActionResult> PutFeedback(int id, FeedbackDTO feedbackDto);
        Task<ActionResult<FeedbackDTO>> PostFeedback(FeedbackDTO feedbackDto);
        Task<IActionResult> DeleteFeedback(int id);
        bool FeedbackExists(int id);
    }
}
