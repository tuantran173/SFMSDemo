using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using OpenIddict.Abstractions;
using SFMSSolution.Application.DataTransferObjects.Auth;
using SFMSSolution.Application.DataTransferObjects.Auth.Request;
using SFMSSolution.Application.ExternalService.Email;
using SFMSSolution.Application.ExternalService.OTP;
using SFMSSolution.Domain.Entities;
using SFMSSolution.Domain.Enums;
using SFMSSolution.Response;

namespace SFMSSolution.Application.Services.Auth
{
    public class AuthService : IAuthService
    {
        private readonly UserManager<User> _userManager;
        private readonly SignInManager<User> _signInManager;
        private readonly IEmailService _emailService;
        private readonly IOpenIddictTokenManager _tokenManager;
        private readonly IOTPService _otpService;

        public AuthService(
            UserManager<User> userManager,
            SignInManager<User> signInManager,
            IConfiguration configuration,
            IEmailService emailService,
            IOpenIddictTokenManager tokenManager, IOTPService otpService)
        {
            _userManager = userManager;
            _signInManager = signInManager;
            _emailService = emailService;
            _tokenManager = tokenManager;
            _otpService = otpService;
        }

        public async Task<AuthResponseDto?> AuthenticateAsync(AuthRequestDto request)
        {
            var user = await _userManager.FindByNameAsync(request.UserName);
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
            var subject = "Đăng ký tài khoản thành công!";
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
            var trimmed = email.Trim();

            var user = await _userManager.Users
                .FirstOrDefaultAsync(u => u.Email.ToLower() == trimmed.ToLower());

            if (user == null)
            {
                // Không tiết lộ tài khoản có tồn tại hay không
                return new ApiResponse<string>(string.Empty, "If an account with that email exists, an OTP has been sent.");
            }

            if (user.Status != EntityStatus.Active)
            {
                return new ApiResponse<string>("This account has been deactivated.");
            }

            var otp = _otpService.GenerateOTP();
            _otpService.SaveOTP(user.Email, otp);

            // Soạn email
            var subject = "Mã OTP để đặt lại mật khẩu của bạn";
            var body = $@"
                <p>Xin chào {user.FullName},</p>
                <p>Mã OTP để đặt lại mật khẩu là: <strong>{otp}</strong></p>
                <p>Mã OTP có hiệu lực trong vòng 3 phút.</p>
                <p>Nếu bạn không yêu cầu đặt lại mật khẩu, vui lòng bỏ qua email này.</p>
                <p>Trân trọng,<br>Sport Facility Management System Team</p>";

            // Gửi email
            var emailSent = await _emailService.SendEmailAsync(user.Email, subject, body);
            if (!emailSent)
            {
                return new ApiResponse<string>("Failed to send OTP email. Please try again later.");
            }

            return new ApiResponse<string>(string.Empty, "If an account with that email exists, an OTP has been sent.");
        }

        public async Task<ApiResponse<bool>> VerifyOtpAsync(string email, string otp)
        {
            if (string.IsNullOrWhiteSpace(email) || string.IsNullOrWhiteSpace(otp))
                return new ApiResponse<bool>("Email and OTP are required.");

            var trimmedEmail = email.Trim().ToLower();

            var user = await _userManager.Users
                .FirstOrDefaultAsync(u => u.Email.ToLower() == trimmedEmail);

            if (user == null)
                return new ApiResponse<bool>("Invalid email.");

            var isValid = _otpService.VerifyOTP(user.Email, otp);

            return isValid
                ? new ApiResponse<bool>(true, "OTP is valid.")
                : new ApiResponse<bool>("OTP is invalid or expired.");
        }

        public async Task<ApiResponse<string>> ResetPasswordAsync(string email, string password, string confirmPassword)
        {
            if (password != confirmPassword)
                return new ApiResponse<string>("Password and confirmation do not match.");

            var user = await _userManager.Users
                .FirstOrDefaultAsync(u => u.Email.ToLower() == email.Trim().ToLower());

            if (user == null)
                return new ApiResponse<string>("Invalid email.");

            var hasPassword = await _userManager.HasPasswordAsync(user);
            if (hasPassword)
            {
                var removeResult = await _userManager.RemovePasswordAsync(user);
                if (!removeResult.Succeeded)
                    return new ApiResponse<string>("Failed to remove old password.");
            }

            var addResult = await _userManager.AddPasswordAsync(user, password);
            if (!addResult.Succeeded)
                return new ApiResponse<string>("Failed to reset password: " + string.Join(", ", addResult.Errors.Select(e => e.Description)));

            return new ApiResponse<string>(string.Empty, "Password has been reset successfully.");
        }
    }
}