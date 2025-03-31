using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using SFMSSolution.Application.DataTransferObjects.Admin.Request;
using SFMSSolution.Application.DataTransferObjects.User.Request;
using SFMSSolution.Application.Services.Admin;
using SFMSSolution.Domain.Enums;

namespace SFMSSolution.API.Controllers
{
    [Authorize(Policy = "Admin")]
    [ApiController]
    [Route("api/user")]
    public class UserController : ControllerBase
    {
        private readonly IUserService _adminService;

        public UserController(IUserService adminService)
        {
            _adminService = adminService;
        }

        [Authorize]
        [HttpGet("get-user-by-id/{userId:Guid}")]
        public async Task<IActionResult> GetUserById(Guid userId)
        {
            var user = await _adminService.GetUserByIdAsync(userId);
            if (user == null)
                return NotFound(new { message = "User not found" });

            return Ok(user);
        }

        [HttpGet("get-all-users")]
        public async Task<IActionResult> GetAllUsers(
        [FromQuery] int pageNumber = 1,
        [FromQuery] int pageSize = 10,
        [FromQuery] string? fullName = null,
        [FromQuery] string? email = null,
        [FromQuery] string? phone = null,
        [FromQuery] EntityStatus? status = null,
        [FromQuery] string? role = null)
        {
            var (users, totalCount) = await _adminService.GetAllUsersAsync(
                pageNumber, pageSize, fullName, email, phone, status, role);

            return Ok(new { TotalCount = totalCount, Users = users });
        }


        //[HttpPut("update-user/{userId:Guid}")]
        //public async Task<IActionResult> UpdateUser(Guid userId, [FromBody] UpdateUserRequestDto request)
        //{
        //    var result = await _adminService.UpdateUserAsync(userId, request);
        //    if (!result)
        //        return NotFound(new { message = "User not found or update failed" });

        //    return Ok(new { message = "User updated successfully" });
        //}

        [HttpPost("update-account/{userId:Guid}")]
        public async Task<IActionResult> ChangeUserRole([FromRoute] Guid userId, [FromQuery] string role)
        {
            var request = new ChangeUserRoleRequestDto
            {
                UserId = userId,
                Role = role
            };

            var result = await _adminService.UpdateAccountAsync(request);
            if (!result)
                return NotFound(new { message = "User not found or role update failed" });

            return Ok(new { message = "User role updated successfully" });
        }

        [Authorize]
        [HttpGet("UserInfo/{userId}")]
        public async Task<IActionResult> GetUserProfile(Guid userId)
        {
            var profile = await _adminService.GetUserProfileAsync(userId);
            if (profile == null)
                return NotFound(new { message = "User not found" });

            return Ok(profile);
        }

        [HttpPost("disable-account/{userId:Guid}")]
        public async Task<IActionResult> DisableUser(Guid userId)
        {
            var result = await _adminService.DisableUserAsync(userId);
            if (!result)
                return NotFound(new { message = "User not found or disable failed" });

            return Ok(new { message = "User disabled successfully" });
        }

        [HttpPut("active-account/{userId:Guid}")]
        public async Task<IActionResult> ActivateUser(Guid userId)
        {
            var result = await _adminService.ActivateUserAsync(userId);
            if (!result)
                return NotFound(new { message = "User not found or activation failed" });

            return Ok(new { message = "User activated successfully" });
        }

        [Authorize]
        [HttpPut("change-password")]
        public async Task<IActionResult> ChangePassword([FromBody] ChangePasswordRequestDto request)
        {
            try
            {
                var result = await _adminService.ChangePasswordAsync(request);
                if (!result)
                    return NotFound(new { message = "User not found or password change failed" });

                return Ok(new { message = "Password changed successfully" });
            }
            catch (Exception ex)
            {
                return BadRequest(new { message = ex.Message });
            }
        }

        [Authorize]
        [HttpPut("change-email")]
        public async Task<IActionResult> ChangeEmail([FromBody] ChangeEmailRequestDto request)
        {
            var result = await _adminService.ChangeEmailAsync(request);
            if (!result)
                return BadRequest(new { message = "Failed to change email" });

            return Ok(new { message = "Email updated successfully" });
        }

        [Authorize]
        [HttpPost("check-password")]
        public async Task<IActionResult> CheckPassword([FromBody] CheckPasswordRequestDto request)
        {
            var result = await _adminService.CheckPasswordAsync(request);
            return Ok(new { isMatch = result });
        }
    }
}
