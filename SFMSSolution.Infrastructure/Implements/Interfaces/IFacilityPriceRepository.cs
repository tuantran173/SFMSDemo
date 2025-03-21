using SFMS.Infrastructure.Repositories;
using SFMSSolution.Domain.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SFMSSolution.Infrastructure.Implements.Interfaces
{
    public interface IFacilityPriceRepository : IGenericRepository<FacilityPrice>
    {
        Task<List<FacilityPrice>> GetByFacilityTimeSlotIdAsync(Guid facilityTimeSlotId);
    }
}
