using SFMS.Infrastructure.Repositories;
using SFMSSolution.Domain.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SFMSSolution.Infrastructure.Implements.Interfaces
{
    public interface IAuthRepository : IGenericRepository<User>
    {
        // ✅ Lấy User theo ID
        Task<User?> GetUserByIdAsync(Guid userId);

        // ✅ Lấy User theo email (đã chuyển về lowercase)
        Task<User?> GetUserByEmailAsync(string email);

        // ✅ Lấy User theo refresh token (sử dụng cho việc làm mới token)
        Task<User?> GetUserByRefreshTokenAsync(string refreshToken);

        // ✅ Đăng ký User mới (Không cần trả về bool, không gọi SaveChangesAsync)
        Task RegisterUserAsync(User user);

        // ✅ Cập nhật thông tin User (Không cần trả về bool, không gọi SaveChangesAsync)
        Task UpdateUserAsync(User user);
    }
}
