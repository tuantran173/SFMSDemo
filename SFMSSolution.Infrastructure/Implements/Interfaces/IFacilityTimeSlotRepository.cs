using SFMS.Infrastructure.Repositories;
using SFMSSolution.Domain.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;

namespace SFMSSolution.Infrastructure.Implements.Interfaces
{
    public interface IFacilityTimeSlotRepository : IGenericRepository<FacilityTimeSlot>
    {
        Task<FacilityTimeSlot?> FindFirstOrDefaultAsync(
            Expression<Func<FacilityTimeSlot, bool>> predicate,
            params Expression<Func<FacilityTimeSlot, object>>[] includes);

        Task<List<FacilityTimeSlot>> GetByFacilityIdAsync(Guid facilityId);


    }
}
