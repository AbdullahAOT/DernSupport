using DernSupport2.Data;
using DernSupport2.Models.DTOs;
using DernSupport2.Models;
using DernSupport2.Repositories.Interfaces;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace DernSupport2.Repositories.Services
{
    public class JobService : IJob
    {
        private readonly ApplicationDbContext _context;

        public JobService(ApplicationDbContext context)
        {
            _context = context;
        }

        public bool JobExists(int id)
        {
            return (_context.Jobs?.Any(e => e.JobId == id)).GetValueOrDefault();
        }

        public async Task<IActionResult> DeleteJob(int id)
        {
            var job = await _context.Jobs.FindAsync(id);
            if (job == null)
            {
                return new NotFoundResult();
            }
            _context.Jobs.Remove(job);
            await _context.SaveChangesAsync();
            return new OkObjectResult("Deleted");
        }

        public async Task<ActionResult<JobDTO>> GetJob(int id)
        {
            var job = await _context.Jobs.FindAsync(id);
            if (job == null)
            {
                return new NotFoundResult();
            }
            var jobDto = new JobDTO
            {
                JobId = job.JobId,
                SupportRequestId = job.SupportRequestId,
                ScheduledDate = job.ScheduledDate,
                TechnicianAssigned = job.TechnicianAssigned,
                JobPriority = job.JobPriority
            };
            return new OkObjectResult(jobDto);
        }

        public async Task<ActionResult<IEnumerable<JobDTO>>> GetJobs()
        {
            var jobs = await _context.Jobs
                .Select(j => new JobDTO
                {
                    JobId = j.JobId,
                    SupportRequestId = j.SupportRequestId,
                    ScheduledDate = j.ScheduledDate,
                    TechnicianAssigned = j.TechnicianAssigned,
                    JobPriority = j.JobPriority
                }).ToListAsync();
            return new OkObjectResult(jobs);
        }

        public async Task<ActionResult<JobDTO>> PostJob(JobDTO jobDto)
        {
            var job = new Job
            {
                SupportRequestId = jobDto.SupportRequestId,
                ScheduledDate = jobDto.ScheduledDate,
                TechnicianAssigned = jobDto.TechnicianAssigned,
                JobPriority = jobDto.JobPriority
            };

            await _context.Jobs.AddAsync(job);
            await _context.SaveChangesAsync();

            jobDto.JobId = job.JobId;
            return new OkObjectResult(jobDto);
        }

        public async Task<IActionResult> PutJob(int id, JobDTO jobDto)
        {
            var existingJob = await _context.Jobs.FindAsync(id);
            if (existingJob == null)
            {
                return new NotFoundResult();
            }

            existingJob.ScheduledDate = jobDto.ScheduledDate;
            existingJob.TechnicianAssigned = jobDto.TechnicianAssigned;
            existingJob.JobPriority = jobDto.JobPriority;

            _context.Jobs.Update(existingJob);
            await _context.SaveChangesAsync();

            return new OkObjectResult(jobDto);
        }
    }
}
