using DernSupport2.Data;
using DernSupport2.Models.DTOs;
using DernSupport2.Models;
using DernSupport2.Repositories.Interfaces;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace DernSupport2.Repositories.Services
{
    public class SupportRequestService : ISupportRequest
    {
        private readonly ApplicationDbContext _context;

        public SupportRequestService(ApplicationDbContext context)
        {
            _context = context;
        }

        public bool SupportRequestExists(int id)
        {
            return (_context.SupportRequests?.Any(e => e.SupportRequestId == id)).GetValueOrDefault();
        }

        public async Task<IActionResult> DeleteSupportRequest(int id)
        {
            var supportRequest = await _context.SupportRequests.FindAsync(id);
            if (supportRequest == null)
            {
                return new NotFoundResult();
            }
            _context.SupportRequests.Remove(supportRequest);
            await _context.SaveChangesAsync();
            return new OkObjectResult("Deleted");
        }

        public async Task<ActionResult<SupportRequestDTO>> GetSupportRequest(int id)
        {
            var supportRequest = await _context.SupportRequests.FindAsync(id);
            if (supportRequest == null)
            {
                return new NotFoundResult();
            }
            var supportRequestDto = new SupportRequestDTO
            {
                SupportRequestId = supportRequest.SupportRequestId,
                UserId = supportRequest.UserId, // Updated to UserId
                IssueDescription = supportRequest.IssueDescription,
                Status = supportRequest.Status,
                RequestedDate = supportRequest.RequestedDate,
                ScheduledDate = supportRequest.ScheduledDate,
                EstimatedCost = supportRequest.EstimatedCost,
                Priority = supportRequest.Priority
            };
            return new OkObjectResult(supportRequestDto);
        }

        public async Task<ActionResult<IEnumerable<SupportRequestDTO>>> GetSupportRequests()
        {
            var supportRequests = await _context.SupportRequests
                .Select(sr => new SupportRequestDTO
                {
                    SupportRequestId = sr.SupportRequestId,
                    UserId = sr.UserId, // Updated to UserId
                    IssueDescription = sr.IssueDescription,
                    Status = sr.Status,
                    RequestedDate = sr.RequestedDate,
                    ScheduledDate = sr.ScheduledDate,
                    EstimatedCost = sr.EstimatedCost,
                    Priority = sr.Priority
                }).ToListAsync();
            return new OkObjectResult(supportRequests);
        }

        public async Task<ActionResult<SupportRequestDTO>> PostSupportRequest(SupportRequestDTO supportRequestDto)
        {
            var supportRequest = new SupportRequest
            {
                UserId = supportRequestDto.UserId, // Updated to UserId
                IssueDescription = supportRequestDto.IssueDescription,
                Status = supportRequestDto.Status,
                RequestedDate = supportRequestDto.RequestedDate,
                ScheduledDate = supportRequestDto.ScheduledDate,
                EstimatedCost = supportRequestDto.EstimatedCost,
                Priority = supportRequestDto.Priority
            };

            await _context.SupportRequests.AddAsync(supportRequest);
            await _context.SaveChangesAsync();

            supportRequestDto.SupportRequestId = supportRequest.SupportRequestId;
            return new OkObjectResult(supportRequestDto);
        }

        public async Task<IActionResult> PutSupportRequest(int id, SupportRequestDTO supportRequestDto)
        {
            var existingSupportRequest = await _context.SupportRequests.FindAsync(id);
            if (existingSupportRequest == null)
            {
                return new NotFoundResult();
            }

            existingSupportRequest.IssueDescription = supportRequestDto.IssueDescription;
            existingSupportRequest.Status = supportRequestDto.Status;
            existingSupportRequest.RequestedDate = supportRequestDto.RequestedDate;
            existingSupportRequest.ScheduledDate = supportRequestDto.ScheduledDate;
            existingSupportRequest.EstimatedCost = supportRequestDto.EstimatedCost;
            existingSupportRequest.Priority = supportRequestDto.Priority;

            _context.SupportRequests.Update(existingSupportRequest);
            await _context.SaveChangesAsync();

            return new OkObjectResult(supportRequestDto);
        }
    }
}
