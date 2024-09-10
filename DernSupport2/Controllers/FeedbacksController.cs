using System.Collections.Generic;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using DernSupport2.Repositories.Interfaces;
using DernSupport2.Models.DTOs;

namespace DernSupport2.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class FeedbacksController : ControllerBase
    {
        private readonly IFeedback _feedbackService;

        public FeedbacksController(IFeedback feedbackService)
        {
            _feedbackService = feedbackService;
        }

        [HttpGet]
        public async Task<ActionResult<IEnumerable<FeedbackDTO>>> GetFeedbacks()
        {
            return await _feedbackService.GetFeedbacks();
        }

        [HttpGet("{id}")]
        public async Task<ActionResult<FeedbackDTO>> GetFeedback(int id)
        {
            return await _feedbackService.GetFeedback(id);
        }

        [HttpPut("{id}")]
        public async Task<IActionResult> PutFeedback(int id, FeedbackDTO feedbackDto)
        {
            return await _feedbackService.PutFeedback(id, feedbackDto);
        }

        [HttpPost]
        public async Task<ActionResult<FeedbackDTO>> PostFeedback(FeedbackDTO feedbackDto)
        {
            return await _feedbackService.PostFeedback(feedbackDto);
        }

        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteFeedback(int id)
        {
            return await _feedbackService.DeleteFeedback(id);
        }
    }
}
