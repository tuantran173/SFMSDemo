using Microsoft.EntityFrameworkCore;
using SFMSSolution.Domain.Entities;
using SFMSSolution.Infrastructure.Database.AppDbContext;
using SFMSSolution.Infrastructure.Implements.Interfaces;
using System;
using System.Threading.Tasks;

namespace SFMSSolution.Infrastructure.Implements.Repositories
{
    public class AuthRepository : IAuthRepository
    {
        private readonly SFMSDbContext _context;

        public AuthRepository(SFMSDbContext context)
        {
            _context = context;
        }

        public async Task<User?> GetUserByEmailAsync(string email)
        {
            return await _context.Users
                .Include(u => u.UserRoles)
                    .ThenInclude(ur => ur.Role)
                .FirstOrDefaultAsync(u => u.Email.ToLower() == email.ToLower());
        }

        public async Task<User?> GetUserByRefreshTokenAsync(string refreshToken)
        {
            return await _context.Users
                .Include(u => u.UserRoles)
                    .ThenInclude(ur => ur.Role)
                .FirstOrDefaultAsync(u => u.RefreshToken == refreshToken);
        }

        public async Task<bool> RegisterUserAsync(User user)
        {
            _context.Users.Add(user);
            return await _context.SaveChangesAsync() > 0;
        }

        public async Task<bool> UpdateUserAsync(User user)
        {
            // Lấy user hiện tại từ DB kèm các UserRoles
            var existingUser = await _context.Users
                .Include(u => u.UserRoles)
                .FirstOrDefaultAsync(u => u.Id == user.Id);
            if (existingUser == null)
            {
                return false;
            }

            // Cập nhật các thuộc tính scalar từ 'user' sang 'existingUser'
            _context.Entry(existingUser).CurrentValues.SetValues(user);

            // Nếu có thay đổi đối với các navigation property (UserRoles),
            // bạn cần xử lý riêng (ví dụ: xóa, thêm mới). Ở đây, ta chỉ cập nhật scalar values.

            return await _context.SaveChangesAsync() > 0;
        }
    }
}
