using Microsoft.EntityFrameworkCore;
using SFMS.Infrastructure.Repositories;
using SFMSSolution.Domain.Entities;
using SFMSSolution.Infrastructure.Database.AppDbContext;
using SFMSSolution.Infrastructure.Implements.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;

namespace SFMSSolution.Infrastructure.Implements.Repositories
{
    public class FacilityTimeSlotRepository : GenericRepository<FacilityTimeSlot>, IFacilityTimeSlotRepository
    {
        public FacilityTimeSlotRepository(SFMSDbContext context) : base(context)
        {
        }

        public async Task<FacilityTimeSlot?> FindFirstOrDefaultAsync(
            Expression<Func<FacilityTimeSlot, bool>> predicate,
            params Expression<Func<FacilityTimeSlot, object>>[] includes)
        {
            IQueryable<FacilityTimeSlot> query = _dbSet;

            foreach (var include in includes)
            {
                query = query.Include(include);
            }

            return await query.FirstOrDefaultAsync(predicate);
        }
    }
}
