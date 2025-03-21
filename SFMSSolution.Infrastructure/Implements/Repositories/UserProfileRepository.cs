using Microsoft.EntityFrameworkCore;
using SFMSSolution.Domain.Entities;
using SFMSSolution.Infrastructure.Database.AppDbContext;
using SFMSSolution.Infrastructure.Implements.Interfaces;
using System;
using System.Threading.Tasks;

namespace SFMSSolution.Infrastructure.Implements.Repositories
{
    public class UserProfileRepository : IUserProfileRepository
    {
        private readonly SFMSDbContext _context;

        public UserProfileRepository(SFMSDbContext context)
        {
            _context = context;
        }

        public async Task<User?> GetUserByIdAsync(Guid userId)
        {
            return await _context.Users
                .Include(u => u.Role) // Bao gồm Role thay vì UserRoles
                .FirstOrDefaultAsync(u => u.Id == userId);
        }

        public async Task<bool> UpdateUserAsync(User user)
        {
            _context.Users.Update(user);
            return await _context.SaveChangesAsync() > 0;
        }
    }
}
