using DernSupport2.Data;
using DernSupport2.Models.DTOs;
using DernSupport2.Models;
using DernSupport2.Repositories.Interfaces;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace DernSupport2.Repositories.Services
{
    public class FeedbackService : IFeedback
    {
        private readonly ApplicationDbContext _context;

        public FeedbackService(ApplicationDbContext context)
        {
            _context = context;
        }

        public bool FeedbackExists(int id)
        {
            return (_context.Feedbacks?.Any(e => e.FeedbackId == id)).GetValueOrDefault();
        }

        public async Task<IActionResult> DeleteFeedback(int id)
        {
            var feedback = await _context.Feedbacks.FindAsync(id);
            if (feedback == null)
            {
                return new NotFoundResult();
            }
            _context.Feedbacks.Remove(feedback);
            await _context.SaveChangesAsync();
            return new OkObjectResult("Deleted");
        }

        public async Task<ActionResult<FeedbackDTO>> GetFeedback(int id)
        {
            var feedback = await _context.Feedbacks.FindAsync(id);
            if (feedback == null)
            {
                return new NotFoundResult();
            }
            var feedbackDto = new FeedbackDTO
            {
                FeedbackId = feedback.FeedbackId,
                UserId = feedback.UserId, // Updated to UserId
                SupportRequestId = feedback.SupportRequestId,
                Rating = feedback.Rating,
                Comments = feedback.Comments,
                SubmittedDate = feedback.SubmittedDate
            };
            return new OkObjectResult(feedbackDto);
        }

        public async Task<ActionResult<IEnumerable<FeedbackDTO>>> GetFeedbacks()
        {
            var feedbacks = await _context.Feedbacks
                .Select(f => new FeedbackDTO
                {
                    FeedbackId = f.FeedbackId,
                    UserId = f.UserId, // Updated to UserId
                    SupportRequestId = f.SupportRequestId,
                    Rating = f.Rating,
                    Comments = f.Comments,
                    SubmittedDate = f.SubmittedDate
                }).ToListAsync();
            return new OkObjectResult(feedbacks);
        }

        public async Task<ActionResult<FeedbackDTO>> PostFeedback(FeedbackDTO feedbackDto)
        {
            var feedback = new Feedback
            {
                UserId = feedbackDto.UserId, // Updated to UserId
                SupportRequestId = feedbackDto.SupportRequestId,
                Rating = feedbackDto.Rating,
                Comments = feedbackDto.Comments,
                SubmittedDate = feedbackDto.SubmittedDate
            };

            await _context.Feedbacks.AddAsync(feedback);
            await _context.SaveChangesAsync();

            feedbackDto.FeedbackId = feedback.FeedbackId;
            return new OkObjectResult(feedbackDto);
        }

        public async Task<IActionResult> PutFeedback(int id, FeedbackDTO feedbackDto)
        {
            var existingFeedback = await _context.Feedbacks.FindAsync(id);
            if (existingFeedback == null)
            {
                return new NotFoundResult();
            }

            existingFeedback.Rating = feedbackDto.Rating;
            existingFeedback.Comments = feedbackDto.Comments;
            existingFeedback.SubmittedDate = feedbackDto.SubmittedDate;

            _context.Feedbacks.Update(existingFeedback);
            await _context.SaveChangesAsync();

            return new OkObjectResult(feedbackDto);
        }
    }
}
