using Microsoft.EntityFrameworkCore;
using SFMSSolution.Domain.Entities;
using SFMSSolution.Infrastructure.Database.AppDbContext;
using SFMSSolution.Infrastructure.Implements.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace SFMSSolution.Infrastructure.Implements.Repositories
{
    public class AdminRepository : IAdminRepository
    {
        private readonly SFMSDbContext _context;

        public AdminRepository(SFMSDbContext context)
        {
            _context = context;
        }

        public async Task<List<User>> GetAllUsersAsync()
        {
            // Nạp UserRoles và Role tương ứng cho mỗi user
            return await _context.Users
                .Include(u => u.UserRoles)
                    .ThenInclude(ur => ur.Role)
                .ToListAsync();
        }

        public async Task<User?> GetUserByIdAsync(Guid userId)
        {
            return await _context.Users
                .Include(u => u.UserRoles)
                    .ThenInclude(ur => ur.Role)
                .FirstOrDefaultAsync(u => u.Id == userId);
        }

        public async Task<bool> UpdateUserAsync(User user)
        {
            // Cập nhật toàn bộ entity user
            _context.Users.Update(user);
            return await _context.SaveChangesAsync() > 0;
        }

        public async Task<bool> ChangeUserRoleAsync(Guid userId, Guid newRoleId)
        {
            // Lấy user bao gồm các UserRoles
            var user = await _context.Users
                .Include(u => u.UserRoles)
                .FirstOrDefaultAsync(u => u.Id == userId);

            if (user == null)
                return false;

            // Xóa hết các vai trò hiện tại (tùy vào nghiệp vụ, bạn có thể điều chỉnh)
            user.UserRoles.Clear();

            // Thêm vai trò mới
            user.UserRoles.Add(new UserRole
            {
                UserId = userId,
                RoleId = newRoleId
            });

            _context.Users.Update(user);
            return await _context.SaveChangesAsync() > 0;
        }

        public async Task<bool> DeleteUserAsync(Guid userId)
        {
            var user = await _context.Users.FindAsync(userId);
            if (user == null) return false;

            _context.Users.Remove(user);
            return await _context.SaveChangesAsync() > 0;
        }
    }
}
