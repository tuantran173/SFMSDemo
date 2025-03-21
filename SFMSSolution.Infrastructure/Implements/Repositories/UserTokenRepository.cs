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
    public class UserTokenRepository : IUserTokenRepository
    {
        private readonly SFMSDbContext _context;

        public UserTokenRepository(SFMSDbContext context)
        {
            _context = context;
        }

        public async Task<UserToken?> GetTokenAsync(Guid userId, string tokenType)
        {
            return await _context.UserTokens
                .FirstOrDefaultAsync(t => t.UserId == userId && t.TokenType == tokenType);
        }

        public async Task<UserToken?> GetTokenAsyncByValue(string tokenValue, string tokenType)
        {
            return await _context.UserTokens
                .FirstOrDefaultAsync(t => t.Token == tokenValue && t.TokenType == tokenType);
        }

        public async Task<List<UserToken>> GetTokensByUserIdAndTypeAsync(Guid userId, string tokenType)
        {
            return await _context.UserTokens
                .Where(t => t.UserId == userId && t.TokenType == tokenType)
                .ToListAsync();
        }

        public async Task<bool> AddAsync(UserToken token)
        {
            _context.UserTokens.Add(token);
            return await _context.SaveChangesAsync() > 0;
        }

        public async Task<bool> DeleteAsync(UserToken token)
        {
            _context.UserTokens.Remove(token);
            return await _context.SaveChangesAsync() > 0;
        }
    }
}
