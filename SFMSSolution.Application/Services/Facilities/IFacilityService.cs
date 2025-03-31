using SFMSSolution.Application.DataTransferObjects.Facility.Request;
using SFMSSolution.Application.DataTransferObjects.Facility;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace SFMSSolution.Application.Services.Facilities
{
    public interface IFacilityService
    {
        Task<FacilityDto> GetFacilityAsync(Guid id);

        Task<(IEnumerable<FacilityDto> Facilities, int TotalCount)> GetFacilitiesByOwnerAsync(Guid ownerId, string? name, int pageNumber, int pageSize);

        Task<(IEnumerable<FacilityDto> Facilities, int TotalCount)> GetAllFacilitiesAsync(string? name, int pageNumber, int pageSize);

        Task<bool> CreateFacilityAsync(FacilityCreateRequestDto request);

        Task<bool> UpdateFacilityAsync(FacilityUpdateRequestDto request);

        Task<bool> DeleteFacilityAsync(Guid id);
    }
}
