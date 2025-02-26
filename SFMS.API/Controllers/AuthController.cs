using Microsoft.AspNetCore.Mvc;
using SFMSSolution.Application.DataTransferObjects.Auth;
using SFMSSolution.Application.DataTransferObjects.Auth.Request;
using SFMSSolution.Application.Services.Auth;
using System.Threading.Tasks;

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


        [HttpPost("register")]
        public async Task<IActionResult> Register([FromBody] RegisterRequestDto request)
        {
            var success = await _authService.RegisterAsync(request);
            if (!success)
                return BadRequest(new { message = "User already exists" });

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
    }
}
