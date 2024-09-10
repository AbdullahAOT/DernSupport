namespace DernSupport2.Models.DTOs
{
    public class JobDTO
    {
        public int JobId { get; set; }
        public int SupportRequestId { get; set; }
        public DateTime ScheduledDate { get; set; }
        public string TechnicianAssigned { get; set; }
        public string JobPriority { get; set; }
    }
}
