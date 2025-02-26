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
    public class FacilityRepository : IFacilityRepository
    {
        private readonly SFMSDbContext _context;

        public FacilityRepository(SFMSDbContext context)
        {
            _context = context;
        }

        public async Task<Facility> GetByIdAsync(Guid id)
        {
            return await _context.Facilities.FirstOrDefaultAsync(f => f.Id == id);
        }

        public async Task<IEnumerable<Facility>> GetAllAsync()
        {
            return await _context.Facilities.ToListAsync();
        }

        public async Task<bool> AddAsync(Facility facility)
        {
            _context.Facilities.Add(facility);
            return await _context.SaveChangesAsync() > 0;
        }

        public async Task<bool> UpdateAsync(Facility facility)
        {
            _context.Facilities.Update(facility);
            return await _context.SaveChangesAsync() > 0;
        }

        public async Task<bool> DeleteAsync(Guid id)
        {
            var facility = await _context.Facilities.FirstOrDefaultAsync(f => f.Id == id);
            if (facility == null)
                return false;
            _context.Facilities.Remove(facility);
            return await _context.SaveChangesAsync() > 0;
        }
    }
}
