namespace DernSupport2.Models.DTOs
{
    public class FeedbackDTO
    {
        public int FeedbackId { get; set; }
        public string UserId { get; set; } // Changed from CustomerId to UserId
        public int SupportRequestId { get; set; }
        public int Rating { get; set; }
        public string Comments { get; set; }
        public DateTime SubmittedDate { get; set; }
    }
}
