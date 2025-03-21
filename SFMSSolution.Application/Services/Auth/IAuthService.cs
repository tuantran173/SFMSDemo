using SFMSSolution.Application.DataTransferObjects.Auth.Request;
using SFMSSolution.Application.DataTransferObjects.Auth;
using System;
using System.Threading.Tasks;
using SFMSSolution.Response;

namespace SFMSSolution.Application.Services.Auth
{
    public interface IAuthService
    {
        // Xác thực người dùng (Đăng nhập)
        Task<AuthResponseDto?> AuthenticateAsync(AuthRequestDto request);

        // Đăng ký người dùng mới
        Task<ApiResponse<string>> RegisterAsync(RegisterRequestDto request); // Trả về ApiResponse để chứa thông báo

        // Làm mới JWT token dựa trên refresh token
        Task<ApiResponse<AuthResponseDto>> RefreshTokenAsync(string refreshToken);

        // Quên mật khẩu: sinh token reset mật khẩu và gửi email reset
        Task<ApiResponse<string>> ForgotPasswordAsync(string email);

        // Đặt lại mật khẩu bằng OTP
        Task<ApiResponse<string>> ResetPasswordWithOTPAsync(string email, string otp, string newPassword);

        // Đăng xuất người dùng
        Task<bool> LogoutAsync(Guid userId);
    }
}
