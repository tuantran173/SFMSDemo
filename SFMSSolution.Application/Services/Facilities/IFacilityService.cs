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

        Task<(IEnumerable<FacilityDto> Facilities, int TotalCount)> GetAllFacilitiesAsync(int pageNumber, int pageSize);

        Task<(IEnumerable<FacilityDto> Facilities, int TotalCount)> SearchFacilitiesByNameAsync(string name, int pageNumber, int pageSize);

        Task<(IEnumerable<FacilityDto> Facilities, int TotalCount)> FilterFacilitiesAsync(
            Guid? categoryId, string? location, TimeSpan? startTime, TimeSpan? endTime, int pageNumber, int pageSize);

        Task<bool> CreateFacilityAsync(FacilityCreateRequestDto request);

        Task<bool> UpdateFacilityAsync(Guid id, FacilityUpdateRequestDto request);

        Task<bool> DeleteFacilityAsync(Guid id);
    }
}
