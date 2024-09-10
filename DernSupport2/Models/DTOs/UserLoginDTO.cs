using System.ComponentModel.DataAnnotations;

namespace DernSupport2.Models.DTOs
{
    public class UserLoginDTO
    {
        [Required(ErrorMessage = "Username is required.")]
        public string Username { get; set; }

        [Required(ErrorMessage = "Password is required.")]
        [DataType(DataType.Password)]
        public string Password { get; set; }
    }
}
