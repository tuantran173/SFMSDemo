using SFMSSolution.Domain.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SFMSSolution.Infrastructure.Implements.Interfaces
{
    public interface IAuthRepository
    {
        // Lấy user theo email (đã chuyển về lowercase)
        Task<User?> GetUserByEmailAsync(string email);

        // Lấy user theo refresh token (sử dụng cho việc làm mới token)
        Task<User?> GetUserByRefreshTokenAsync(string refreshToken);

        // Đăng ký user mới
        Task<bool> RegisterUserAsync(User user);

        // Cập nhật thông tin user (ví dụ: khi thay đổi token hoặc mật khẩu)
        Task<bool> UpdateUserAsync(User user);
    }
}
