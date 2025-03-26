using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using Polly;
using SFMS.Infrastructure.Repositories;
using SFMSSolution.Domain.Entities;
using SFMSSolution.Infrastructure.Database.AppDbContext;
using SFMSSolution.Infrastructure.Implements.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace SFMSSolution.Infrastructure.Implements.Repositories
{
    public class UserRepository : GenericRepository<User>, IUserRepository
    {
        private readonly UserManager<User> _userManager;

        public UserRepository(SFMSDbContext context, UserManager<User> userManager) : base(context)
        {
            _userManager = userManager;
        }

        public async Task<(List<User> Users, int TotalCount)> GetAllUsersWithRolesAsync(int pageNumber, int pageSize)
        {
            var query = _dbSet.AsQueryable();
            var totalCount = await query.CountAsync();

            var users = await query
                .Skip((pageNumber - 1) * pageSize)
                .Take(pageSize)
                .ToListAsync();

            // Lấy vai trò cho từng user nếu cần
            foreach (var user in users)
            {
                var roles = await _userManager.GetRolesAsync(user);
                // Nếu cần gắn roles vào một property tạm, bạn có thể thêm logic ở đây
            }

            return (users, totalCount);
        }

        public async Task<User?> GetUserByIdWithRolesAsync(Guid userId)
        {
            var user = await _userManager.FindByIdAsync(userId.ToString());
            if (user != null)
            {
                // Lấy vai trò nếu cần
                var roles = await _userManager.GetRolesAsync(user);
                // Nếu cần gắn roles vào một property tạm, bạn có thể thêm logic ở đây
            }
            return user;
        }

        public async Task ChangeUserRoleAsync(Guid userId, Guid newRoleId)
        {
            var user = await _userManager.FindByIdAsync(userId.ToString());
            if (user == null)
                throw new ArgumentException("User not found.");

            // Lấy tên vai trò từ newRoleId
            var role = await _context.Roles.FirstOrDefaultAsync(r => r.Id == newRoleId);
            if (role == null)
                throw new ArgumentException("Role not found.");

            // Xóa tất cả vai trò hiện tại của user
            var currentRoles = await _userManager.GetRolesAsync(user);
            await _userManager.RemoveFromRolesAsync(user, currentRoles);

            // Gán vai trò mới
            await _userManager.AddToRoleAsync(user, role.Name);
        }
    }
}