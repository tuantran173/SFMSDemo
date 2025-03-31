using Microsoft.AspNetCore.Identity;
using Microsoft.Extensions.Configuration;
using OpenIddict.Abstractions;
using SFMSSolution.Application.DataTransferObjects.Auth;
using SFMSSolution.Domain.Entities;
using SFMSSolution.Response;
using SFMSSolution.Application.DataTransferObjects.Auth.Request;
using SFMSSolution.Application.ExternalService.Email;
using SFMSSolution.Domain.Enums;
using System;
using System.Linq;
using System.Threading.Tasks;

namespace SFMSSolution.Application.Services.Auth
{
    public class AuthService : IAuthService
    {
        private readonly UserManager<User> _userManager;
        private readonly SignInManager<User> _signInManager;
        private readonly IConfiguration _configuration;
        private readonly IEmailService _emailService;
        private readonly IOpenIddictTokenManager _tokenManager;

        public AuthService(
            UserManager<User> userManager,
            SignInManager<User> signInManager,
            IConfiguration configuration,
            IEmailService emailService,
            IOpenIddictTokenManager tokenManager)
        {
            _userManager = userManager;
            _signInManager = signInManager;
            _configuration = configuration;
            _emailService = emailService;
            _tokenManager = tokenManager;
        }

        public async Task<AuthResponseDto?> AuthenticateAsync(AuthRequestDto request)
        {
            var user = await _userManager.FindByEmailAsync(request.Email.ToLower());
            if (user == null)
                return null;

            var result = await _signInManager.CheckPasswordSignInAsync(user, request.Password, lockoutOnFailure: false);
            if (!result.Succeeded)
                return null;

            if (user.Status != EntityStatus.Active)
                throw new Exception("Account has been deactivated.");

            var roles = await _userManager.GetRolesAsync(user);
            if (roles == null || !roles.Any())
                throw new Exception("User does not have an assigned role.");

            // Token sẽ được tạo bởi OpenIddict qua endpoint /connect/token, không cần tạo thủ công ở đây
            var response = new AuthResponseDto
            {
                Id = user.Id,
                Email = user.Email,
                FullName = user.FullName,
                Role = roles.FirstOrDefault()
            };

            return response;
        }

        public async Task<ApiResponse<string>> RegisterAsync(RegisterRequestDto request)
        {
            if (!string.Equals(request.Password, request.ConfirmPassword))
                return new ApiResponse<string>("Password and Confirm Password do not match.");


            var email = request.Email.ToLower();
            var username = request.UserName;
            var phone = request.Phone;

            // Check trùng email
            var existingEmailUser = await _userManager.FindByEmailAsync(email);
            if (existingEmailUser != null)
                return new ApiResponse<string>("User with this email already exists.");

            // Check trùng username
            var existingUserNameUser = await _userManager.FindByNameAsync(username);
            if (existingUserNameUser != null)
                return new ApiResponse<string>("User with this username already exists.");

            // Check trùng số điện thoại
            var existingPhoneUser = _userManager.Users.FirstOrDefault(u => u.PhoneNumber == phone);
            if (existingPhoneUser != null)
                return new ApiResponse<string>("User with this phone number already exists.");


            var newUser = new User
            {
                UserName = request.UserName,
                Email = request.Email.ToLower(),
                Phone = request.Phone,
                FullName = request.FullName,
                Status = EntityStatus.Active
            };

            var result = await _userManager.CreateAsync(newUser, request.Password);
            if (!result.Succeeded)
                return new ApiResponse<string>("Failed to register user: " + string.Join(", ", result.Errors.Select(e => e.Description)));

            // Gán vai trò mặc định "CUS"
            var roleResult = await _userManager.AddToRoleAsync(newUser, Role.Customer.ToString());
            if (!roleResult.Succeeded)
                return new ApiResponse<string>("Failed to assign role: " + string.Join(", ", roleResult.Errors.Select(e => e.Description)));

            // Gửi email xác nhận
            var subject = "Registration Successful";
            var body = $@"
                <p>Thân gửi {newUser.FullName},</p>
                <p>Bạn đã đăng ký tài khoản thành công trên nền tảng của chúng tôi.</p>
                <p>Trân trọng,<br>Sport Facility Management System Team</p>";

            var emailSent = await _emailService.SendEmailAsync(newUser.Email, subject, body);
            if (!emailSent)
                return new ApiResponse<string>("Registration successful, but failed to send confirmation email.");

            return new ApiResponse<string>(string.Empty, "Registration successful. A confirmation email has been sent.");
        }



        public async Task<bool> LogoutAsync(Guid userId)
        {
            var user = await _userManager.FindByIdAsync(userId.ToString());
            if (user != null)
            {
                // Sử dụng await foreach để duyệt qua IAsyncEnumerable
                await foreach (var token in _tokenManager.FindBySubjectAsync(userId.ToString()))
                {
                    await _tokenManager.TryRevokeAsync(token);
                }
            }
            return true;
        }

        public async Task<ApiResponse<string>> SendOtpAsync(string email)
        {
            var user = await _userManager.FindByEmailAsync(email.ToLower());
            if (user == null)
                return new ApiResponse<string>(string.Empty, "If an account with that email exists, an OTP has been sent.");

            var otp = await _userManager.GeneratePasswordResetTokenAsync(user);

            var subject = "Mã OTP để đặt lại mật khẩu của bạn";
            var body = $"<p>Xin chào {user.FullName},</p>" +
                       $"<p>Mã OTP để đặt lại mật khẩu là: <strong>{otp}</strong></p>" +
                       $"<p>Mã OTP sẽ có hiệu lực trong vòng 3 phút.</p>";

            var emailSent = await _emailService.SendEmailAsync(email, subject, body);
            if (!emailSent)
                return new ApiResponse<string>("Failed to send OTP email.");

            return new ApiResponse<string>(string.Empty, "If an account with that email exists, an OTP has been sent.");
        }

        public async Task<ApiResponse<bool>> VerifyOtpAsync(string email, string otp)
        {
            var user = await _userManager.FindByEmailAsync(email.ToLower());
            if (user == null) return new ApiResponse<bool>("Invalid email.");

            // Chỉ kiểm tra tính hợp lệ, không đổi mật khẩu
            var result = await _userManager.VerifyUserTokenAsync(
                user, _userManager.Options.Tokens.PasswordResetTokenProvider,
                "ResetPassword", otp);

            return result
                ? new ApiResponse<bool>(true, "OTP is valid.")
                : new ApiResponse<bool>("OTP is invalid or expired.");
        }

        public async Task<ApiResponse<string>> ResetPasswordAsync(string email, string password, string confirmPassword)
        {
            var user = await _userManager.FindByEmailAsync(email.ToLower());
            if (user == null)
                return new ApiResponse<string>("Invalid email.");

            if (password != confirmPassword)
                return new ApiResponse<string>("Password and confirmation do not match.");

            // Tạo lại token mới vì securityStamp đã được làm mới ở SendOtpAsync
            var otp = await _userManager.GeneratePasswordResetTokenAsync(user);

            var result = await _userManager.ResetPasswordAsync(user, otp, password);
            if (!result.Succeeded)
                return new ApiResponse<string>("Reset failed: " + string.Join(", ", result.Errors.Select(e => e.Description)));

            return new ApiResponse<string>(string.Empty, "Password has been reset successfully.");
        }
    }
}