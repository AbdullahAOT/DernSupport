namespace DernSupport2.Models
{
    public class Feedback
    {
        public int FeedbackId { get; set; }
        public string UserId { get; set; } // Foreign key to ApplicationUser
        public ApplicationUser User { get; set; } // Navigation property to ApplicationUser
        public int SupportRequestId { get; set; } // Foreign key to SupportRequest
        public SupportRequest SupportRequest { get; set; } // Navigation property
        public int Rating { get; set; }
        public string Comments { get; set; }
        public DateTime SubmittedDate { get; set; }
    }
}
