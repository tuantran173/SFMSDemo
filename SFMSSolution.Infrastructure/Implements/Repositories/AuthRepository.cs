using Microsoft.EntityFrameworkCore;
using SFMS.Infrastructure.Repositories;
using SFMSSolution.Domain.Entities;
using SFMSSolution.Infrastructure.Database.AppDbContext;
using SFMSSolution.Infrastructure.Implements.Interfaces;

namespace SFMSSolution.Infrastructure.Implements.Repositories
{
    public class AuthRepository : GenericRepository<User>, IAuthRepository
    {
        public AuthRepository(SFMSDbContext context) : base(context) { }

        public async Task<User?> GetUserByIdAsync(Guid userId)
        {
            return await GetByIdWithIncludesAsync(userId, u => u.Role, u => u.UserTokens);
        }

        public async Task<User?> GetUserByEmailAsync(string email)
        {
            return await _context.Users
                .Include(u => u.Role)
                .Include(u => u.UserTokens)
                .FirstOrDefaultAsync(u => u.Email.ToLower() == email.ToLower());
        }

        public async Task<User?> GetUserByRefreshTokenAsync(string refreshToken)
        {
            return await _context.Users
                .Include(u => u.Role)
                .Include(u => u.UserTokens)
                .FirstOrDefaultAsync(u => u.UserTokens.Any(t => t.Token == refreshToken && t.TokenType == "Refresh"));
        }

        public async Task RegisterUserAsync(User user)
        {
            await AddAsync(user);  // Không gọi SaveChangesAsync ở đây
        }

        public async Task UpdateUserAsync(User user)
        {
            var existingUser = await GetByIdWithIncludesAsync(user.Id, u => u.Role, u => u.UserTokens);
            if (existingUser == null)
                throw new Exception("User not found.");

            _context.Entry(existingUser).CurrentValues.SetValues(user);

            // Không gọi SaveChangesAsync ở đây
        }
    }
}
