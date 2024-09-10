using Microsoft.AspNetCore.Identity;

namespace DernSupport2.Models
{
    public class ApplicationUser : IdentityUser
    {
        public string FirstName { get; set; }
        public string LastName { get; set; }

        // Optional: Define navigation properties if needed
        public List<SupportRequest> SupportRequests { get; set; }
        public List<Feedback> Feedbacks { get; set; }
    }
}
