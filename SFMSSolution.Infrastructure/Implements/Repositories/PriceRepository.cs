using Microsoft.EntityFrameworkCore;
using SFMS.Infrastructure.Repositories;
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
    public class PriceRepository : GenericRepository<Price>, IPriceRepository
    {
        public PriceRepository(SFMSDbContext context) : base(context) { }

        public async Task<Price?> GetByCategoryIdAsync(Guid categoryId)
        {
            return await _context.Prices.FirstOrDefaultAsync(p => p.CategoryId == categoryId);
        }
    }
}
