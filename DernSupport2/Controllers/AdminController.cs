using DernSupport2.Models;
using DernSupport2.Models.DTOs;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using System.Threading.Tasks;

namespace DernSupport2.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    [Authorize(Roles = "Admin")]
    public class AdminController : ControllerBase
    {
        private readonly UserManager<ApplicationUser> _userManager;
        private readonly RoleManager<IdentityRole> _roleManager;

        public AdminController(UserManager<ApplicationUser> userManager, RoleManager<IdentityRole> roleManager)
        {
            _userManager = userManager;
            _roleManager = roleManager;
        }

        // POST: api/Admin/CreateUser
        [HttpPost("CreateUser")]
        public async Task<IActionResult> CreateUser([FromBody] AdminUserCreationDTO adminUserDto)
        {
            if (adminUserDto == null)
                return BadRequest("Invalid user data.");

            var user = new ApplicationUser
            {
                UserName = adminUserDto.Username,
                Email = adminUserDto.Email,
                FirstName = adminUserDto.FirstName,
                LastName = adminUserDto.LastName,
                EmailConfirmed = true,
                Role = adminUserDto.Role
            };

            var result = await _userManager.CreateAsync(user, adminUserDto.Password);

            if (!result.Succeeded)
                return BadRequest(result.Errors);

            if (!await _roleManager.RoleExistsAsync(adminUserDto.Role))
                return BadRequest("Specified role does not exist.");

            var roleResult = await _userManager.AddToRoleAsync(user, adminUserDto.Role);
            if (!roleResult.Succeeded)
                return BadRequest("Failed to assign role.");

            return Ok("User created successfully.");
        }
    }
}
