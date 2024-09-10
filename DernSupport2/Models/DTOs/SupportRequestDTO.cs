using DernSupport2.Models;

namespace DernSupport2.Models.DTOs
{
    public class SupportRequestDTO
    {
        public int SupportRequestId { get; set; }
        public string UserId { get; set; } // Changed from CustomerId to UserId
        public string IssueDescription { get; set; }
        public SupportStatus Status { get; set; }
        public DateTime RequestedDate { get; set; }
        public DateTime? ScheduledDate { get; set; }
        public decimal EstimatedCost { get; set; }
        public string Priority { get; set; } // Example: Low, Medium, High
    }
}
