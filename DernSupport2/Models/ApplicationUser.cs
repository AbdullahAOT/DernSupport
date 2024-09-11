using Microsoft.AspNetCore.Identity;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace DernSupport2.Models
{
    public class ApplicationUser : IdentityUser
    {
        [Required]
        [MaxLength(50)]
        public string FirstName { get; set; }

        [Required]
        [MaxLength(50)]
        public string LastName { get; set; }

        // Store the primary role of the user in the database
        [Required]
        [MaxLength(20)]
        public string Role { get; set; } // Admin, Customer, etc.

        // Optional: Define navigation properties if needed
        public List<SupportRequest> SupportRequests { get; set; }
        public List<Feedback> Feedbacks { get; set; }
    }
}
