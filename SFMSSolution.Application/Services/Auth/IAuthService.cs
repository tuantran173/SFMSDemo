using SFMSSolution.Application.DataTransferObjects.Auth.Request;
using SFMSSolution.Application.DataTransferObjects.Auth;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using SFMSSolution.Response;

namespace SFMSSolution.Application.Services.Auth
{
    public interface IAuthService
    {// Xác thực người dùng (đăng nhập)
        Task<AuthResponseDto?> AuthenticateAsync(AuthRequestDto request);

        // Đăng ký người dùng mới
        Task<bool> RegisterAsync(RegisterRequestDto request);

        // Làm mới JWT token dựa trên refresh token
        Task<ApiResponse<AuthResponseDto>> RefreshTokenAsync(string refreshToken);

        // Quên mật khẩu: sinh token reset mật khẩu và gửi email reset
        Task<ApiResponse<string>> ForgotPasswordAsync(string email);
    }
}
