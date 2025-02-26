using SFMSSolution.Application.DataTransferObjects.Facility.Request;
using SFMSSolution.Application.DataTransferObjects.Facility;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SFMSSolution.Application.Services.Facilities
{
    public interface IFacilityService
    {
        Task<FacilityDto> GetFacilityAsync(Guid id);
        Task<IEnumerable<FacilityDto>> GetAllFacilitiesAsync();
        Task<bool> CreateFacilityAsync(FacilityCreateRequestDto request);
        Task<bool> UpdateFacilityAsync(Guid id, FacilityUpdateRequestDto request);
        Task<bool> DeleteFacilityAsync(Guid id);
    }
}
