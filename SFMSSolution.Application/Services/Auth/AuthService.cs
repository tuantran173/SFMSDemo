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
using SFMSSolution.Application.Services.Email;

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
            var user = await _unitOfWork.AuthRepository.GetUserByEmailAsync(request.Email.ToLower());
            if (user == null || !BCrypt.Net.BCrypt.Verify(request.Password, user.PasswordHash))
                return null;

            // Kiểm tra trạng thái
            if (user.Status != EntityStatus.Active)
                throw new Exception("Account has been deactivated.");

            if (user.UserRoles == null)
                throw new Exception("User does not have an assigned role.");

            var response = _mapper.Map<AuthResponseDto>(user);
            response.Token = GenerateJwtToken(user);

            return response;
        }

        public async Task<bool> RegisterAsync(RegisterRequestDto request)
        {
            if (!string.Equals(request.Password, request.ConfirmPassword))
                return false;

            var existingUser = await _unitOfWork.AuthRepository.GetUserByEmailAsync(request.Email.ToLower());
            if (existingUser != null)
                return false;

            // Thay vì _context.Roles, bạn có thể dùng _unitOfWork.RoleRepository
            // hoặc UnitOfWork cho phép _unitOfWork.Context.Roles
            // Ở đây giả sử có IRoleRepository
            var customerRole = await _unitOfWork.RoleRepository.GetRoleByCodeAsync("CUS");
            if (customerRole == null)
                throw new Exception("Role 'Customer' does not exist in the database.");

            var newUser = _mapper.Map<User>(request);
            newUser.Email = newUser.Email.ToLower();
            newUser.PasswordHash = HashPassword(request.Password);
            newUser.UserRoles.Add(new UserRole
            {
                UserId = newUser.Id,
                RoleId = customerRole.Id
            });

            // Thay vì authRepository.RegisterUserAsync(newUser),
            // ta gọi method add user (ví dụ: AddUserAsync)
            var result = await _unitOfWork.AuthRepository.RegisterUserAsync(newUser);
            if (result)
            {
                // Commit transaction
                await _unitOfWork.CompleteAsync();
                return true;
            }
            return false;
        }

        public async Task<ApiResponse<AuthResponseDto>> RefreshTokenAsync(string refreshToken)
        {
            var user = await _unitOfWork.AuthRepository.GetUserByRefreshTokenAsync(refreshToken);
            if (user == null)
                return new ApiResponse<AuthResponseDto>("Invalid refresh token.");

            if (user.RefreshTokenExpiry == null || user.RefreshTokenExpiry < DateTime.UtcNow)
                return new ApiResponse<AuthResponseDto>("Refresh token has expired.");

            var newJwtToken = GenerateJwtToken(user);
            var newRefreshToken = GenerateRefreshToken();

            user.RefreshToken = newRefreshToken;
            user.RefreshTokenExpiry = DateTime.UtcNow.AddDays(7);
            var updateResult = await _unitOfWork.AuthRepository.UpdateUserAsync(user);
            if (updateResult)
            {
                await _unitOfWork.CompleteAsync();
            }

            var responseDto = _mapper.Map<AuthResponseDto>(user);
            responseDto.Token = newJwtToken;
            responseDto.RefreshToken = newRefreshToken;

            return new ApiResponse<AuthResponseDto>(responseDto, "Token refreshed successfully.");
        }

        public async Task<ApiResponse<string>> ForgotPasswordAsync(string email)
        {
            // 1. Tìm user theo email
            var user = await _unitOfWork.AuthRepository.GetUserByEmailAsync(email.ToLower());

            // 2. Để bảo mật, nếu không tìm thấy user, trả về thông báo giống nhau
            if (user == null)
                return new ApiResponse<string>(string.Empty, "If an account with that email exists, a reset link has been sent.");

            // 3. Sinh token reset mật khẩu và thiết lập thời gian hết hạn (ví dụ: 1 giờ)
            var token = Guid.NewGuid().ToString();
            user.ResetPasswordToken = token;
            user.ResetPasswordTokenExpiry = DateTime.UtcNow.AddHours(1);

            // 4. Cập nhật user với thông tin token (lưu vào DB)
            var updateResult = await _unitOfWork.AuthRepository.UpdateUserAsync(user);
            if (!updateResult)
                return new ApiResponse<string>("An error occurred while processing your request.");

            // 5. Tạo link reset mật khẩu, lấy URL frontend từ cấu hình
            var frontendUrl = _configuration["Frontend:BaseUrl"]; // Ví dụ: "https://yourdomain.com"
            var resetLink = $"{frontendUrl}/reset-password?token={token}&email={email}";

            // 6. Soạn email reset mật khẩu
            var subject = "Reset Your Password";
            var body = $"<p>Hello {user.FullName},</p>" +
                       $"<p>You requested a password reset. Please click the link below to reset your password:</p>" +
                       $"<p><a href='{resetLink}'>Reset Password</a></p>" +
                       "<p>If you did not request a password reset, please ignore this email.</p>";

            // 7. Gửi email sử dụng IEmailService (MailKit)
            var emailSent = await _emailService.SendEmailAsync(email, subject, body);
            if (!emailSent)
                return new ApiResponse<string>("Failed to send password reset email.");

            // 8. Trả về thông báo thành công (giống nhau để bảo mật)
            return new ApiResponse<string>(string.Empty, "If an account with that email exists, a reset link has been sent.");
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
                new Claim(ClaimTypes.Role, user.UserRoles.FirstOrDefault()?.Role.Name ?? "User")
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
