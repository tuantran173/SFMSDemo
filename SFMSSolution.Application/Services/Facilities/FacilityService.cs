using AutoMapper;
using SFMSSolution.Application.DataTransferObjects.Facility.Request;
using SFMSSolution.Application.DataTransferObjects.Facility;
using SFMSSolution.Application.Services.Facilities;
using SFMSSolution.Domain.Entities;
using SFMSSolution.Infrastructure.Implements.UnitOfWorks;
using SFMSSolution.Response;
using SFMSSolution.Application.Extensions;

public class FacilityService : IFacilityService
{
    private readonly IUnitOfWork _unitOfWork;
    private readonly IMapper _mapper;

    public FacilityService(IUnitOfWork unitOfWork, IMapper mapper)
    {
        _unitOfWork = unitOfWork;
        _mapper = mapper;
    }

    public async Task<FacilityDto> GetFacilityAsync(Guid id)
    {
        var facility = await _unitOfWork.FacilityRepository.GetFacilityByIdAsync(id); // Sử dụng method có include
        return _mapper.Map<FacilityDto>(facility);
    }

    // Get all facilities by owner (for facility owner)
    public async Task<(IEnumerable<FacilityDto> Facilities, int TotalCount)> GetFacilitiesByOwnerAsync(Guid ownerId, string? name, int pageNumber, int pageSize)
    {
        var facilities = await _unitOfWork.FacilityRepository.GetFacilitiesWithDetailsAsync();

        var filteredFacilities = facilities.Where(f => f.OwnerId == ownerId);

        if (!string.IsNullOrWhiteSpace(name))
        {
            var normalizedInput = name.Trim().ToLower().RemoveDiacritics();
            filteredFacilities = filteredFacilities.Where(f =>
                f.Name != null &&
                f.Name.Trim().ToLower().RemoveDiacritics().Contains(normalizedInput));
        }

        var totalCount = filteredFacilities.Count();

        var pagedFacilities = filteredFacilities
            .Skip((pageNumber - 1) * pageSize)
            .Take(pageSize)
            .ToList();

        return (_mapper.Map<IEnumerable<FacilityDto>>(pagedFacilities), totalCount);
    }

    public async Task<(IEnumerable<FacilityDto> Facilities, int TotalCount)> GetAllFacilitiesAsync(string? name, int pageNumber, int pageSize)
    {
        var facilities = await _unitOfWork.FacilityRepository.GetFacilitiesWithDetailsAsync();

        if (!string.IsNullOrWhiteSpace(name))
        {
            var normalizedInput = name.Trim().ToLower().RemoveDiacritics();
            facilities = facilities.Where(f =>
                f.Name != null &&
                f.Name.Trim().ToLower().RemoveDiacritics().Contains(normalizedInput));
        }

        var totalCount = facilities.Count();

        var filteredFacilities = facilities
            .Skip((pageNumber - 1) * pageSize)
            .Take(pageSize)
            .ToList();

        return (_mapper.Map<IEnumerable<FacilityDto>>(filteredFacilities), totalCount);
    }


    public async Task<ApiResponse<string>> CreateFacilityAsync(FacilityCreateRequestDto request, Guid ownerId)
    {
        var facility = _mapper.Map<Facility>(request);
        facility.OwnerId = ownerId;

        await _unitOfWork.FacilityRepository.AddAsync(facility);
        await _unitOfWork.CompleteAsync();

        return new ApiResponse<string>(string.Empty, "Facility created successfully.");
    }

    public async Task<ApiResponse<string>> UpdateFacilityAsync(FacilityUpdateRequestDto request)
    {
        var facility = await _unitOfWork.FacilityRepository.GetFacilityByIdAsync(request.Id);
        if (facility == null)
            return new ApiResponse<string>("Facility not found.");

        facility.Name = request.Name;
        facility.Description = request.Description;
        facility.Address = request.Address;
        facility.ImageUrl = request.ImageUrl;
        facility.FacilityType = request.FacilityType;
        facility.UpdatedDate = DateTime.UtcNow;

        await _unitOfWork.FacilityRepository.UpdateAsync(facility);
        await _unitOfWork.CompleteAsync();

        return new ApiResponse<string>(string.Empty, "Facility updated successfully.");
    }

    public async Task<ApiResponse<string>> DeleteFacilityAsync(Guid id)
    {
        var facility = await _unitOfWork.FacilityRepository.GetByIdAsync(id);
        if (facility == null)
            return new ApiResponse<string>("Facility not found.");

        await _unitOfWork.FacilityRepository.DeleteAsync(id);
        await _unitOfWork.CompleteAsync();

        return new ApiResponse<string>(string.Empty, "Facility deleted successfully.");
    }
}
