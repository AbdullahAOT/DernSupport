using System.Collections.Generic;

namespace DernSupport2.Models
{
    public class SupportRequest
    {
        public int SupportRequestId { get; set; }
        public string UserId { get; set; } // Foreign key to ApplicationUser
        public ApplicationUser User { get; set; } // Navigation property to ApplicationUser
        public string IssueDescription { get; set; }
        public SupportStatus Status { get; set; } // Enum for tracking status
        public DateTime RequestedDate { get; set; }
        public DateTime? ScheduledDate { get; set; }
        public decimal EstimatedCost { get; set; }
        public string Priority { get; set; } // Example: Low, Medium, High
        public List<Job> Jobs { get; set; } // Navigation property to Jobs
        public List<Feedback> Feedbacks { get; set; } // Navigation property to Feedbacks
    }

    public enum SupportStatus
    {
        Submitted,
        Scheduled,
        InProgress,
        Completed,
        Cancelled
    }
}
