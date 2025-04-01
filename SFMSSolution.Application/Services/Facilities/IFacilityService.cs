using SFMSSolution.Application.DataTransferObjects.Facility.Request;
using SFMSSolution.Application.DataTransferObjects.Facility;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using SFMSSolution.Response;

namespace SFMSSolution.Application.Services.Facilities
{
    public interface IFacilityService
    {
        Task<FacilityDto> GetFacilityAsync(Guid id);

        Task<(IEnumerable<FacilityDto> Facilities, int TotalCount)> GetFacilitiesByOwnerAsync(Guid ownerId, string? name, int pageNumber, int pageSize);

        Task<(IEnumerable<FacilityDto> Facilities, int TotalCount)> GetAllFacilitiesAsync(string? name, int pageNumber, int pageSize);

        Task<ApiResponse<string>> CreateFacilityAsync(FacilityCreateRequestDto request, Guid ownerId);

        Task<ApiResponse<string>> UpdateFacilityAsync(FacilityUpdateRequestDto request);

        Task<ApiResponse<string>> DeleteFacilityAsync(Guid id);
    }
}
