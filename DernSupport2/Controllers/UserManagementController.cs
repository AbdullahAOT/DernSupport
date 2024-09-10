using DernSupport2.Models;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace DernSupport2.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    [Authorize(Roles = "Admin")]
    public class UserManagementController : ControllerBase
    {
        private readonly UserManager<ApplicationUser> _userManager;

        public UserManagementController(UserManager<ApplicationUser> userManager)
        {
            _userManager = userManager;
        }

        // GET: api/UserManagement
        [HttpGet]
        public async Task<ActionResult<IEnumerable<ApplicationUser>>> GetUsers()
        {
            var users = _userManager.Users.ToList();
            return Ok(users);
        }

        // GET: api/UserManagement/{id}
        [HttpGet("{id}")]
        public async Task<ActionResult<ApplicationUser>> GetUserById(string id)
        {
            var user = await _userManager.FindByIdAsync(id);
            if (user == null)
            {
                return NotFound("User not found.");
            }
            return Ok(user);
        }

        // POST: api/UserManagement
        [HttpPost]
        public async Task<ActionResult<ApplicationUser>> CreateUser(ApplicationUser user, string password)
        {
            var result = await _userManager.CreateAsync(user, password);
            if (!result.Succeeded)
            {
                return BadRequest(result.Errors);
            }
            return Ok(user);
        }

        // PUT: api/UserManagement/{id}
        [HttpPut("{id}")]
        public async Task<IActionResult> UpdateUser(string id, ApplicationUser updatedUser)
        {
            var user = await _userManager.FindByIdAsync(id);
            if (user == null)
            {
                return NotFound("User not found.");
            }

            // Update user properties
            user.UserName = updatedUser.UserName;
            user.Email = updatedUser.Email;
            user.FirstName = updatedUser.FirstName;
            user.LastName = updatedUser.LastName;

            var result = await _userManager.UpdateAsync(user);
            if (!result.Succeeded)
            {
                return BadRequest(result.Errors);
            }

            return Ok(user);
        }

        // DELETE: api/UserManagement/{id}
        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteUser(string id)
        {
            var user = await _userManager.FindByIdAsync(id);
            if (user == null)
            {
                return NotFound("User not found.");
            }

            var result = await _userManager.DeleteAsync(user);
            if (!result.Succeeded)
            {
                return BadRequest(result.Errors);
            }

            return Ok("User deleted successfully.");
        }
    }
}