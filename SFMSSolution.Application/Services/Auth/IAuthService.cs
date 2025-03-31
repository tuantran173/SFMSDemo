using SFMSSolution.Application.DataTransferObjects.Auth.Request;
using SFMSSolution.Application.DataTransferObjects.Auth;
using System;
using System.Threading.Tasks;
using SFMSSolution.Response;

namespace SFMSSolution.Application.Services.Auth
{
    public interface IAuthService
    {
        // Đăng ký người dùng mới
        Task<ApiResponse<string>> RegisterAsync(RegisterRequestDto request); // Trả về ApiResponse để chứa thông báo

        Task<ApiResponse<string>> SendOtpAsync(string email);
        Task<ApiResponse<bool>> VerifyOtpAsync(string email, string otp);
        Task<ApiResponse<string>> ResetPasswordAsync(string email, string otp, string newPassword);
        // Đăng xuất người dùng
        Task<bool> LogoutAsync(Guid userId);
    }
}
