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
    public class FacilityPriceRepository : GenericRepository<FacilityPrice>, IFacilityPriceRepository
    {
        public FacilityPriceRepository(SFMSDbContext context) : base(context) { }

        public async Task<List<FacilityPrice>> GetByFacilityTimeSlotIdAsync(Guid facilityTimeSlotId)
        {
            return await _context.FacilityPrices
                .Where(fp => fp.FacilityTimeSlotId == facilityTimeSlotId)
                .ToListAsync();
        }
    }
}
