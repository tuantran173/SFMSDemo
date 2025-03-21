using AutoMapper;
using SFMSSolution.Application.DataTransferObjects.Auth;
using SFMSSolution.Domain.Entities;
using System;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;
using System.Threading.Tasks;
using Microsoft.Extensions.Configuration;
using Microsoft.IdentityModel.Tokens;
using Microsoft.EntityFrameworkCore;
using SFMSSolution.Application.DataTransferObjects.Auth.Request;
using SFMSSolution.Response;
using SFMSSolution.Domain.Enums;
using SFMSSolution.Infrastructure.Implements.UnitOfWorks;
using SFMSSolution.Application.ExternalService.Email;

namespace SFMSSolution.Application.Services.Auth
{
    public class AuthService : IAuthService
    {
        private readonly IUnitOfWork _unitOfWork;
        private readonly IConfiguration _configuration;
        private readonly IMapper _mapper;
        private readonly IEmailService _emailService;

        public AuthService(
            IUnitOfWork unitOfWork,
            IConfiguration configuration,
            IMapper mapper,
            IEmailService emailService)
        {
            _unitOfWork = unitOfWork;
            _configuration = configuration;
            _mapper = mapper;
            _emailService = emailService;
        }

        public async Task<AuthResponseDto?> AuthenticateAsync(AuthRequestDto request)
        {
            // Lấy User theo email
            var user = await _unitOfWork.AuthRepository.GetUserByEmailAsync(request.Email.ToLower());
            if (user == null || !BCrypt.Net.BCrypt.Verify(request.Password, user.PasswordHash))
                return null;

            // Kiểm tra trạng thái của tài khoản
            if (user.Status != EntityStatus.Active)
                throw new Exception("Account has been deactivated.");

            // Kiểm tra xem người dùng có được gán Role không
            if (user.Role == null)
                throw new Exception("User does not have an assigned role.");

            // Ánh xạ User sang AuthResponseDto
            var response = _mapper.Map<AuthResponseDto>(user);
            response.Token = GenerateJwtToken(user); // Tạo JWT Token cho người dùng

            return response;
        }


        public async Task<ApiResponse<string>> RegisterAsync(RegisterRequestDto request)
        {
            if (!string.Equals(request.Password, request.ConfirmPassword))
                return new ApiResponse<string>("Password and Confirm Password do not match.");

            var existingUser = await _unitOfWork.AuthRepository.GetUserByEmailAsync(request.Email.ToLower());
            if (existingUser != null)
                return new ApiResponse<string>("User with this email already exists.");

            var customerRole = await _unitOfWork.RoleRepository.GetRoleByCodeAsync("CUS");
            if (customerRole == null)
                return new ApiResponse<string>("Role 'Customer' does not exist in the database.");

            var newUser = _mapper.Map<User>(request);
            newUser.Email = newUser.Email.ToLower();
            newUser.PasswordHash = HashPassword(request.Password);
            newUser.RoleId = customerRole.Id;

            await _unitOfWork.AuthRepository.RegisterUserAsync(newUser);
            var saveResult = await _unitOfWork.CompleteAsync();

            if (saveResult > 0)
            {
                // Gửi email thông báo đăng ký thành công
                var subject = "Registration Successful";
                var body = $@"
                    <p>Thân gửi {newUser.FullName},</p>
                    <p>Bạn đã đăng ký tài khoản thành công trên nền tảng của chúng tôi. 
                       Bây giờ bạn có thể đăng nhập bằng email đã đăng ký.</p>
                    <p>Nếu bạn gặp bất kỳ sự cố nào, vui lòng liên hệ với nhóm hỗ trợ của chúng tôi.
                       </p>
                   
                    <p>Trân trọng,<br>Sport Facility Management System Team</p>";

                var emailSent = await _emailService.SendEmailAsync(newUser.Email, subject, body);

                if (!emailSent)
                    return new ApiResponse<string>("Registration successful, but failed to send confirmation email.");

                return new ApiResponse<string>(string.Empty, "Registration successful. A confirmation email has been sent.");
            }
            else
            {
                return new ApiResponse<string>("Failed to save user to the database.");
            }
        }


        public async Task<ApiResponse<AuthResponseDto>> RefreshTokenAsync(string refreshToken)
        {
            var userToken = await _unitOfWork.UserTokenRepository.GetTokenAsyncByValue(refreshToken, "Refresh");
            if (userToken == null)
                return new ApiResponse<AuthResponseDto>("Invalid refresh token.");

            if (userToken.Expiry < DateTime.UtcNow)
                return new ApiResponse<AuthResponseDto>("Refresh token has expired.");

            var user = await _unitOfWork.AuthRepository.GetUserByIdAsync(userToken.UserId);
            if (user == null)
                return new ApiResponse<AuthResponseDto>("User not found.");

            var newJwtToken = GenerateJwtToken(user);
            var newRefreshToken = GenerateRefreshToken();

            // Xóa token cũ và tạo token mới
            await _unitOfWork.UserTokenRepository.DeleteAsync(userToken);
            var newUserToken = new UserToken
            {
                UserId = user.Id,
                Token = newRefreshToken,
                Expiry = DateTime.UtcNow.AddDays(7),
                TokenType = "Refresh"
            };

            var addResult = await _unitOfWork.UserTokenRepository.AddAsync(newUserToken);
            if (addResult)
                await _unitOfWork.CompleteAsync();

            var responseDto = _mapper.Map<AuthResponseDto>(user);
            responseDto.Token = newJwtToken;
            responseDto.RefreshToken = newRefreshToken;

            return new ApiResponse<AuthResponseDto>(responseDto, "Token refreshed successfully.");
        }

        public async Task<bool> LogoutAsync(Guid userId)
        {
            var tokens = await _unitOfWork.UserTokenRepository.GetTokensByUserIdAndTypeAsync(userId, "Refresh");
            if (tokens != null && tokens.Count != 0)
            {
                foreach (var token in tokens)
                {
                    await _unitOfWork.UserTokenRepository.DeleteAsync(token);
                }
                await _unitOfWork.CompleteAsync();
            }
            return true;
        }

        // Forgot Password: gửi OTP
        public async Task<ApiResponse<string>> ForgotPasswordAsync(string email)
        {
            var user = await _unitOfWork.AuthRepository.GetUserByEmailAsync(email.ToLower());
            if (user == null)
                return new ApiResponse<string>(string.Empty, "If an account with that email exists, an OTP has been sent.");

            // Xóa OTP cũ nếu có
            var existingOtp = await _unitOfWork.UserTokenRepository.GetTokenAsync(user.Id, "ResetPasswordOTP");
            if (existingOtp != null)
                await _unitOfWork.UserTokenRepository.DeleteAsync(existingOtp);

            // Sinh OTP 6 chữ số
            var otp = new Random().Next(100000, 999999).ToString();
            var otpExpiry = DateTime.UtcNow.AddMinutes(3);

            var otpToken = new UserToken
            {
                UserId = user.Id,
                Token = otp,
                Expiry = otpExpiry,
                TokenType = "ResetPasswordOTP"
            };

            var addResult = await _unitOfWork.UserTokenRepository.AddAsync(otpToken);
            if (!addResult)
                return new ApiResponse<string>("An error occurred while processing your request.");

            await _unitOfWork.CompleteAsync();

            var subject = "Mã OTP để đặt lại mật khẩu của bạn";
            var body = $"<p>Xin chào {user.FullName},</p>" +
                       $"<p>Mã OTP để đặt lại mật khẩu là: <strong>{otp}</strong></p>" +
                       $"<p>Mã OTP sẽ có hiệu lực trong vòng 3 phút.</p>" +
                       "<p>Nếu bạn không yêu cầu đặt lại mật khẩu, vui lòng bỏ qua mail này</p>";

            var emailSent = await _emailService.SendEmailAsync(email, subject, body);
            if (!emailSent)
                return new ApiResponse<string>("Failed to send OTP email.");

            return new ApiResponse<string>(string.Empty, "If an account with that email exists, an OTP has been sent.");
        }

        // Reset password with OTP
        public async Task<ApiResponse<string>> ResetPasswordWithOTPAsync(string email, string otp, string newPassword)
        {
            var user = await _unitOfWork.AuthRepository.GetUserByEmailAsync(email.ToLower());
            if (user == null)
                return new ApiResponse<string>("Invalid email.");

            var otpToken = await _unitOfWork.UserTokenRepository.GetTokenAsync(user.Id, "ResetPasswordOTP");
            if (otpToken == null || otpToken.Token != otp || otpToken.Expiry < DateTime.UtcNow)
                return new ApiResponse<string>("Invalid or expired OTP.");

            // Mã OTP hợp lệ, tiến hành đặt lại mật khẩu
            user.PasswordHash = HashPassword(newPassword);

            // Xóa mã OTP sau khi sử dụng
            await _unitOfWork.UserTokenRepository.DeleteAsync(otpToken);

            // Cập nhật thông tin người dùng
            await _unitOfWork.AuthRepository.UpdateUserAsync(user);
            await _unitOfWork.CompleteAsync();

            return new ApiResponse<string>(string.Empty, "Password reset successfully.");
        }

        #region Private Helpers

        private string GenerateJwtToken(User user)
        {
            var secretKey = _configuration["JwtSettings:Secret"];
            if (string.IsNullOrEmpty(secretKey))
                throw new Exception("JWT Secret is missing from configuration.");

            var key = Encoding.UTF8.GetBytes(secretKey);
            var tokenHandler = new JwtSecurityTokenHandler();

            var claims = new[]
            {
        new Claim(ClaimTypes.NameIdentifier, user.Id.ToString()),
        new Claim(ClaimTypes.Name, user.FullName ?? string.Empty),
        new Claim(ClaimTypes.Email, user.Email),
        new Claim(ClaimTypes.Role, user.Role != null ? user.Role.Name : "User"),  // Lấy trực tiếp Role.Name
        new Claim("RoleId", user.RoleId.ToString())  // Thêm RoleId vào token
    };

            var tokenDescriptor = new SecurityTokenDescriptor
            {
                Subject = new ClaimsIdentity(claims),
                Expires = DateTime.UtcNow.AddHours(12),
                SigningCredentials = new SigningCredentials(new SymmetricSecurityKey(key), SecurityAlgorithms.HmacSha256)
            };

            var token = tokenHandler.CreateToken(tokenDescriptor);
            return tokenHandler.WriteToken(token);
        }

        private string GenerateRefreshToken()
        {
            return Guid.NewGuid().ToString("N") + Guid.NewGuid().ToString("N");
        }

        private static string HashPassword(string password)
        {
            return BCrypt.Net.BCrypt.HashPassword(password);
        }

        #endregion
    }
}
