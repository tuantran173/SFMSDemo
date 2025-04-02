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
    public class FacilityRepository : GenericRepository<Facility>, IFacilityRepository
    {
        public FacilityRepository(SFMSDbContext context) : base(context) { }

        public async Task<Facility?> GetFacilityByIdAsync(Guid id)
        {
            return await GetByIdWithIncludesAsync(id, f => f.FacilityTimeSlots, f => f.Owner);
        }

        public async Task<IEnumerable<Facility>> GetAllFacilitiesAsync(int? pageNumber = null, int? pageSize = null)
        {
            return await GetAllAsync(pageNumber, pageSize);
        }

        public async Task<IEnumerable<Facility>> GetFacilitiesWithDetailsAsync(int? pageNumber = null, int? pageSize = null)
        {
            return await GetAllWithIncludesAsync(pageNumber, pageSize, f => f.FacilityTimeSlots, f => f.Owner);
        }

        public async Task<IEnumerable<Facility>> GetFacilitiesByNameAsync(string name, int? pageNumber = null, int? pageSize = null)
        {
            return await GetByNameAsync(name, pageNumber, pageSize);
        }

    }
}
