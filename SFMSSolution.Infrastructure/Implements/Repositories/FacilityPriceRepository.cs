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
                .Include(fp => fp.FacilityTimeSlot)
                .ToListAsync();
        }

        public async Task<(IEnumerable<FacilityPrice> Prices, int TotalCount)> GetAllWithTimeSlotAsync(int pageNumber, int pageSize)
        {
            var query = _context.FacilityPrices
                .Include(fp => fp.FacilityTimeSlot)
                .AsQueryable();

            var totalCount = await query.CountAsync();

            var items = await query
                .OrderByDescending(fp => fp.CreatedDate)
                .Skip((pageNumber - 1) * pageSize)
                .Take(pageSize)
                .ToListAsync();

            return (items, totalCount);
        }

        public async Task<FacilityPrice?> GetByIdWithTimeSlotAsync(Guid id)
        {
            return await _context.FacilityPrices
                .Include(fp => fp.FacilityTimeSlot)
                .FirstOrDefaultAsync(fp => fp.Id == id);
        }

        public async Task<(IEnumerable<FacilityPrice> Prices, int TotalCount)> GetAllWithTimeSlotAndFacilityAsync(int pageNumber, int pageSize)
        {
            var query = _context.FacilityPrices
                .Include(fp => fp.FacilityTimeSlot)
                .Include(fp => fp.Facility)
                .AsQueryable();

            var totalCount = await query.CountAsync();
            var items = await query
                .OrderByDescending(fp => fp.CreatedDate)
                .Skip((pageNumber - 1) * pageSize)
                .Take(pageSize)
                .ToListAsync();

            return (items, totalCount);
        }

        public async Task<FacilityPrice?> GetByIdWithTimeSlotAndFacilityAsync(Guid id)
        {
            return await _context.FacilityPrices
                .Include(fp => fp.FacilityTimeSlot)
                .Include(fp => fp.Facility)
                .FirstOrDefaultAsync(fp => fp.Id == id);
        }
        public async Task<FacilityPrice?> GetByTimeSlotIdAsync(Guid slotId)
        {
            return await _context.FacilityPrices
                .FirstOrDefaultAsync(p => p.FacilityTimeSlotId == slotId);
        }
    }
}
