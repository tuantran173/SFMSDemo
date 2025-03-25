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
        private readonly IAdminService _adminService;

        public AdminController(IAdminService adminService)
        {
            _adminService = adminService;
        }

        [HttpGet("get-user/{userId:Guid}")]
        public async Task<IActionResult> GetUserById(Guid userId)
        {
            var user = await _adminService.GetUserByIdAsync(userId);
            if (user == null)
                return NotFound(new { message = "User not found" });

            return Ok(user);
        }

        [HttpGet("get-all-user")]
        public async Task<IActionResult> GetAllUsers([FromQuery] int pageNumber = 1, [FromQuery] int pageSize = 10)
        {
            var (users, totalCount) = await _adminService.GetAllUsersAsync(pageNumber, pageSize);
            return Ok(new { TotalCount = totalCount, Users = users });
        }

        [HttpPut("{userId:Guid}")]
        public async Task<IActionResult> UpdateUser(Guid userId, [FromBody] UpdateUserRequestDto request)
        {
            var result = await _adminService.UpdateUserAsync(userId, request);
            if (!result)
                return NotFound(new { message = "User not found or update failed" });

            return Ok(new { message = "User updated successfully" });
        }

        [HttpPut("change-role")]
        public async Task<IActionResult> ChangeUserRole([FromBody] ChangeUserRoleRequestDto request)
        {
            var result = await _adminService.ChangeUserRoleAsync(request);
            if (!result)
                return NotFound(new { message = "User not found or change role failed" });

            return Ok(new { message = "User role updated successfully" });
        }

        [HttpDelete("{userId:Guid}")]
        public async Task<IActionResult> DeleteUser(Guid userId)
        {
            var result = await _adminService.DeleteUserAsync(userId);
            if (!result)
                return NotFound(new { message = "User not found or deletion failed" });

            return Ok(new { message = "User deleted successfully" });
        }
    }
}
