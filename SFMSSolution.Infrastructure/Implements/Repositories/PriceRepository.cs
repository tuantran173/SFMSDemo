using Microsoft.EntityFrameworkCore;
using SFMS.Infrastructure.Repositories;
using SFMSSolution.Domain.Entities;
using SFMSSolution.Infrastructure.Database.AppDbContext;
using SFMSSolution.Infrastructure.Implements.Interfaces;
using System;
using System.Threading.Tasks;

namespace SFMSSolution.Infrastructure.Implements.Repositories
{
    public class PriceRepository : GenericRepository<Price>, IPriceRepository
    {
        public PriceRepository(SFMSDbContext context) : base(context) { }

        public async Task<Price?> GetByFacilityTypeAsync(string facilityType)
        {
            return await _context.Prices
                .FirstOrDefaultAsync(p => p.FacilityType == facilityType);
        }
    }
}
