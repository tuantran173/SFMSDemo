using Microsoft.AspNetCore.Mvc;
using SFMSSolution.Application.DataTransferObjects.Auth.Request;
using SFMSSolution.Application.Services.Auth;

namespace SFMSSolution.API.Controllers
{
    [ApiController]
    [Route("api/auth")]
    public class AuthController : ControllerBase
    {
        private readonly IAuthService _authService;

        public AuthController(IAuthService authService)
        {
            _authService = authService;
        }

        [HttpPost("login")]
        public async Task<IActionResult> Login([FromBody] AuthRequestDto request)
        {
            var response = await _authService.AuthenticateAsync(request);
            if (response == null)
            {
                // Đăng nhập thất bại
                return Unauthorized(new
                {
                    message = "Invalid email or password",
                    result = false
                });
            }

            // Đăng nhập thành công
            return Ok(new
            {
                message = "Login successful",
                result = true,
                data = response
            });
        }

        [HttpPost("logout")]
        public async Task<IActionResult> Logout([FromQuery] Guid userId)
        {
            var result = await _authService.LogoutAsync(userId);
            if (!result)
                return BadRequest(new { message = "Failed to logout user." });

            return Ok(new { message = "Logout successful." });
        }

        [HttpPost("register")]
        public async Task<IActionResult> Register([FromBody] RegisterRequestDto request)
        {
            var response = await _authService.RegisterAsync(request);
            if (!response.Success)
            {
                return BadRequest(new { message = response.Message });
            }

            return Ok(new { message = "Registration successful" });
        }


        [HttpPost("forgot-password")]
        public async Task<IActionResult> ForgotPassword([FromBody] ForgotPasswordRequestDto request)
        {
            var result = await _authService.ForgotPasswordAsync(request.Email);
            if (!result.Success)
                return BadRequest(new { message = result.Message });
            return Ok(new { message = result.Message });
        }

        [HttpPost("reset-password")]
        public async Task<IActionResult> ResetPasswordWithOTP([FromBody] ResetPasswordRequestDto request)
        {
            var result = await _authService.ResetPasswordWithOTPAsync(request.Email, request.OTP, request.NewPassword);

            if (!result.Success)
                return BadRequest(new { message = result.Message });

            return Ok(new { message = result.Message });
        }
    }
}
