using SFMSSolution.Domain.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SFMSSolution.Infrastructure.Implements.Interfaces
{
    public interface IFacilityRepository
    {
        Task<Facility> GetByIdAsync(Guid id);
        Task<IEnumerable<Facility>> GetAllAsync();
        Task<bool> AddAsync(Facility facility);
        Task<bool> UpdateAsync(Facility facility);
        Task<bool> DeleteAsync(Guid id);
    }
}
