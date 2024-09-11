using DernSupport2.Models;
using DernSupport2.Models.DTOs;
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
    public class UsersController : ControllerBase
    {
        private readonly UserManager<ApplicationUser> _userManager;
        private readonly RoleManager<IdentityRole> _roleManager;

        public UsersController(UserManager<ApplicationUser> userManager, RoleManager<IdentityRole> roleManager)
        {
            _userManager = userManager;
            _roleManager = roleManager;
        }

        // GET: api/Users
        [HttpGet]
        //[Authorize(Roles = "Admin")]
        public ActionResult<IEnumerable<UserDTO>> GetUsers()
        {
            var users = _userManager.Users.Select(user => new UserDTO
            {
                Id = user.Id,
                FirstName = user.FirstName,
                LastName = user.LastName,
                Email = user.Email,
                Role = user.Role
            }).ToList();

            return Ok(users);
        }

        // GET: api/Users/5
        [HttpGet("{id}")]
        //[Authorize(Roles = "Admin")]
        public async Task<ActionResult<UserDTO>> GetUser(string id)
        {
            var user = await _userManager.FindByIdAsync(id);
            if (user == null)
            {
                return NotFound();
            }

            var userDto = new UserDTO
            {
                Id = user.Id,
                FirstName = user.FirstName,
                LastName = user.LastName,
                Email = user.Email,
                Role = user.Role
            };

            return Ok(userDto);
        }

        // PUT: api/Users/5
        [HttpPut("{id}")]
        //[Authorize(Roles = "Admin")]
        public async Task<IActionResult> PutUser(string id, UserDTO userDto)
        {
            if (id != userDto.Id)
            {
                return BadRequest("User ID does not match.");
            }

            var existingUser = await _userManager.FindByIdAsync(id);
            if (existingUser == null)
            {
                return NotFound("User not found.");
            }

            // Update properties
            existingUser.FirstName = userDto.FirstName;
            existingUser.LastName = userDto.LastName;
            existingUser.Email = userDto.Email;
            existingUser.UserName = userDto.Email; // Assuming username is the same as email
            existingUser.Role = userDto.Role;

            var result = await _userManager.UpdateAsync(existingUser);
            if (!result.Succeeded)
            {
                return BadRequest(result.Errors);
            }

            return NoContent();
        }

        // POST: api/Users
        [HttpPost]
        //[Authorize(Roles = "Admin")]
        public async Task<ActionResult<UserDTO>> PostUser(UserDTO userDto)
        {
            var user = new ApplicationUser
            {
                FirstName = userDto.FirstName,
                LastName = userDto.LastName,
                Email = userDto.Email,
                UserName = userDto.Email, // Assuming username is the same as email
                Role = userDto.Role
            };

            var result = await _userManager.CreateAsync(user, "DefaultPassword123!"); // Use a secure password in production
            if (!result.Succeeded)
            {
                return BadRequest(result.Errors);
            }

            // Optionally assign a role to the user
            if (!string.IsNullOrEmpty(userDto.Role))
            {
                await _userManager.AddToRoleAsync(user, userDto.Role);
            }

            userDto.Id = user.Id; // Set the Id of the DTO to the new user's Id

            return CreatedAtAction("GetUser", new { id = user.Id }, userDto);
        }

        // DELETE: api/Users/5
        [HttpDelete("{id}")]
        //[Authorize(Roles = "Admin")]
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

            return NoContent();
        }

        // PUT: api/Users/5/role
        [HttpPut("{id}/role")]
        [Authorize(Roles = "Admin")]
        public async Task<IActionResult> UpdateUserRole(string id, [FromBody] UserDTO userDto)
        {
            if (id != userDto.Id)
            {
                return BadRequest("User ID mismatch.");
            }

            var existingUser = await _userManager.FindByIdAsync(id);
            if (existingUser == null)
            {
                return NotFound("User not found.");
            }

            // Ensure the role exists
            if (!await _roleManager.RoleExistsAsync(userDto.Role))
            {
                return BadRequest($"Role '{userDto.Role}' does not exist.");
            }

            // Fetch current roles
            var currentRoles = await _userManager.GetRolesAsync(existingUser);

            // Remove the user from all current roles
            var removeResult = await _userManager.RemoveFromRolesAsync(existingUser, currentRoles);
            if (!removeResult.Succeeded)
            {
                return BadRequest("Failed to remove current roles.");
            }

            // Add the user to the new role
            var addResult = await _userManager.AddToRoleAsync(existingUser, userDto.Role);
            if (!addResult.Succeeded)
            {
                return BadRequest(addResult.Errors);
            }

            // Verify and log the role update in AspNetUserRoles
            var updatedRoles = await _userManager.GetRolesAsync(existingUser);
            if (!updatedRoles.Contains(userDto.Role))
            {
                // Log or throw an error if role was not updated
                return BadRequest("Failed to update the role in AspNetUserRoles.");
            }

            // Optional: Update the security stamp to force revalidation
            await _userManager.UpdateSecurityStampAsync(existingUser);

            return NoContent();
        }

    }
}
