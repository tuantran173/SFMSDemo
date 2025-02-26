using Microsoft.EntityFrameworkCore;
using SFMSSolution.Domain.Entities;
using SFMSSolution.Infrastructure.Database.AppDbContext;
using SFMSSolution.Infrastructure.Implements.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SFMSSolution.Infrastructure.Implements.Repositories
{
    public class RoleRepository : IRoleRepository
    {
        private readonly SFMSDbContext _context;

        public RoleRepository(SFMSDbContext context)
        {
            _context = context;
        }

        public async Task<Role?> GetRoleByIdAsync(Guid id)
        {
            return await _context.Roles.FirstOrDefaultAsync(r => r.Id == id);
        }

        public async Task<Role?> GetRoleByCodeAsync(string roleCode)
        {
            return await _context.Roles.FirstOrDefaultAsync(r => r.RoleCode.ToLower() == roleCode.ToLower());
        }
    }
}
