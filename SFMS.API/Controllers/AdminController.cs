using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using SFMSSolution.Application.DataTransferObjects.Admin.Request;
using SFMSSolution.Application.Services.Admin;

namespace SFMSSolution.API.Controllers
{
    [Authorize(Roles = "Admin")]
    [ApiController]
    [Route("api/admin")]
    public class AdminController : ControllerBase
    {
        private readonly IAdminService _adminUserService;

        public AdminController(IAdminService adminUserService)
        {
            _adminUserService = adminUserService;
        }

        [HttpGet("{userId}")]
        public async Task<IActionResult> GetUserById(Guid userId)
        {
            var user = await _adminUserService.GetUserByIdAsync(userId);
            if (user == null)
            {
                return NotFound(new { message = "User not found" });
            }
            return Ok(user);
        }

        [HttpGet]
        public async Task<IActionResult> GetAllUsers()
        {
            var users = await _adminUserService.GetAllUsersAsync();
            return Ok(users);
        }

        [HttpPut("{userId}")]
        public async Task<IActionResult> UpdateUser(Guid userId, [FromBody] UpdateUserRequestDto request)
        {
            var result = await _adminUserService.UpdateUserAsync(userId, request);
            return result ? Ok(new { message = "User updated successfully" }) : NotFound();
        }

        [HttpPut("change-role")]
        public async Task<IActionResult> ChangeUserRole([FromBody] ChangeUserRoleRequestDto request)
        {
            var result = await _adminUserService.ChangeUserRoleAsync(request);
            return result ? Ok(new { message = "User role updated successfully" }) : NotFound();
        }
    }
}
