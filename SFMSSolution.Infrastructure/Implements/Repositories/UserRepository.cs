using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using SFMS.Infrastructure.Repositories;
using SFMSSolution.Domain.Entities;
using SFMSSolution.Domain.Enums;
using SFMSSolution.Infrastructure.Database.AppDbContext;
using SFMSSolution.Infrastructure.Implements.Interfaces;

namespace SFMSSolution.Infrastructure.Implements.Repositories
{
    public class UserRepository : GenericRepository<User>, IUserRepository
    {
        private readonly UserManager<User> _userManager;

        public UserRepository(SFMSDbContext context, UserManager<User> userManager) : base(context)
        {
            _userManager = userManager;
        }

        public async Task<(List<User> Users, int TotalCount)> GetAllUsersWithRolesAsync(
    int pageNumber,
    int pageSize,
    string? fullName = null,
    string? email = null,
    string? phone = null,
    EntityStatus? status = null,
    string? role = null)
        {
            var query = _context.Users.AsQueryable();

            if (!string.IsNullOrEmpty(fullName))
                query = query.Where(u => u.FullName!.ToLower().Contains(fullName.ToLower()));

            if (!string.IsNullOrEmpty(email))
                query = query.Where(u => u.Email!.ToLower().Contains(email.ToLower()));

            if (!string.IsNullOrEmpty(phone))
                query = query.Where(u => u.PhoneNumber!.Contains(phone));

            if (status.HasValue)
                query = query.Where(u => u.Status == status.Value);

            if (!string.IsNullOrEmpty(role))
            {
                var userIdsWithRole = await (from ur in _context.UserRoles
                                             join r in _context.Roles on ur.RoleId equals r.Id
                                             where r.Name == role
                                             select ur.UserId).ToListAsync();

                query = query.Where(u => userIdsWithRole.Contains(u.Id));
            }

            var adminRoleId = await _context.Roles
            .Where(r => r.Name == "Admin")
            .Select(r => r.Id)
            .FirstOrDefaultAsync();

            var adminUserIds = await _context.UserRoles
                .Where(ur => ur.RoleId == adminRoleId)
                .Select(ur => ur.UserId)
                .ToListAsync();

            query = query.Where(u => !adminUserIds.Contains(u.Id));

            var totalCount = await query.CountAsync();

            var users = await query
                .Skip((pageNumber - 1) * pageSize)
                .Take(pageSize)
                .ToListAsync();

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