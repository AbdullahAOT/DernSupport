namespace DernSupport2.Models.DTOs
{
    public class AdminUserUpdateDTO
    {
        public string Id { get; set; } // User ID to identify the user to be updated
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public string Email { get; set; }
        public string Role { get; set; }
    }
}
