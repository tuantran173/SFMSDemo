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

        public async Task<List<User>> GetAllUsersWithRolesAsync()
        {
            return (await GetAllWithIncludesAsync(u => u.Role)).ToList();
        }

        public async Task<User?> GetUserByIdWithRolesAsync(Guid userId)
        {
            return await GetByIdWithIncludesAsync(userId, u => u.Role);
        }

        public async Task ChangeUserRoleAsync(Guid userId, Guid newRoleId)
        {
            var user = await GetByIdWithIncludesAsync(userId, u => u.Role);
            if (user == null) return;

            user.RoleId = newRoleId; // Cập nhật RoleId trực tiếp

            // Không gọi SaveChangesAsync() ở đây. Việc lưu sẽ được quản lý bởi UnitOfWork.
        }
    }
}
