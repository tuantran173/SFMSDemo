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
    public class FacilityRepository : GenericRepository<Facility>, IFacilityRepository
    {
        public FacilityRepository(SFMSDbContext context) : base(context) { }

        public async Task<Facility?> GetFacilityByIdAsync(Guid id)
        {
            return await GetByIdAsync(id);
        }

        public async Task<IEnumerable<Facility>> GetAllFacilitiesAsync()
        {
            return await GetAllAsync();
        }
    }
}
