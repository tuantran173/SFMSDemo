using Microsoft.EntityFrameworkCore;
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
    public class AdminRepository : GenericRepository<User>, IAdminRepository
    {
        public AdminRepository(SFMSDbContext context) : base(context) { }

        public async Task<(List<User> Users, int TotalCount)> GetAllUsersWithRolesAsync(int pageNumber, int pageSize)
        {
            var query = _dbSet.Include(u => u.Role);

            var totalCount = await query.CountAsync();
            var users = await query
                .Skip((pageNumber - 1) * pageSize)
                .Take(pageSize)
                .ToListAsync();

            return (users, totalCount);
        }

        public async Task<User?> GetUserByIdWithRolesAsync(Guid userId)
        {
            return await _dbSet
                .Include(u => u.Role)
                .FirstOrDefaultAsync(u => u.Id == userId);
        }

        public async Task ChangeUserRoleAsync(Guid userId, Guid newRoleId)
        {
            var user = await _dbSet.FindAsync(userId) ?? throw new ArgumentException("User not found.");
            user.RoleId = newRoleId;

            _dbSet.Update(user); // Không gọi SaveChangesAsync() ở đây
        }

    }
}
