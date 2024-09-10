using System;

namespace DernSupport2.Models
{
    public class Job
    {
        public int JobId { get; set; }
        public int SupportRequestId { get; set; } // Foreign key to SupportRequest
        public SupportRequest SupportRequest { get; set; } // Navigation property
        public DateTime ScheduledDate { get; set; }
        public string TechnicianAssigned { get; set; }
        public string JobPriority { get; set; } // Example: Low, Medium, High
    }
}
