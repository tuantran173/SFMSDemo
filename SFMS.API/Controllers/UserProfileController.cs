using CloudinaryDotNet;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using SFMSSolution.Application.DataTransferObjects.User.Request;
using SFMSSolution.Application.ExternalService.CDN;
using SFMSSolution.Application.Services.UserProfile;
using System.Security.Claims;

namespace SFMSSolution.API.Controllers
{
    [Authorize]
    [ApiController]
    [Route("api/user/profile")]
    public class UserProfileController : ControllerBase
    {
        private readonly IUserProfileService _userProfileService;
        private readonly ICloudinaryService _cloudinaryService;
        public UserProfileController(IUserProfileService userProfileService, ICloudinaryService cloudinaryService)
        {
            _userProfileService = userProfileService;
            _cloudinaryService = cloudinaryService;
        }

        private Guid GetUserId() => Guid.Parse(User.FindFirstValue(ClaimTypes.NameIdentifier) ?? throw new UnauthorizedAccessException());

        [HttpGet]
        public async Task<IActionResult> GetProfile()
        {
            var userId = GetUserId();
            var profile = await _userProfileService.GetUserProfileAsync(userId);
            return profile != null ? Ok(profile) : NotFound();
        }

        [HttpPut]
        public async Task<IActionResult> UpdateProfile([FromBody] UpdateProfileRequestDto request)
        {
            var userId = GetUserId();
            var result = await _userProfileService.UpdateUserProfileAsync(userId, request);
            return result ? Ok(new { message = "Profile updated successfully" }) : NotFound();
        }

        [HttpPut("change-password")]
        public async Task<IActionResult> ChangePassword([FromBody] ChangePasswordRequestDto request)
        {
            var userId = GetUserId();
            var result = await _userProfileService.ChangePasswordAsync(userId, request);
            return result ? Ok(new { message = "Password changed successfully" }) : BadRequest(new { message = "Current password is incorrect" });
        }

        [HttpPost("upload-avatar")]
        public async Task<IActionResult> UploadAvatar([FromForm] IFormFile file)
        {
            var result = await _cloudinaryService.UploadImageAsync(file);
            if (result == null)
                return BadRequest(new { message = "Failed to upload image." });

            return Ok(new { imageUrl = result });
        }
    }
}


