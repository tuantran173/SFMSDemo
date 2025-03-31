using AutoMapper;
using SFMSSolution.Application.DataTransferObjects.Facility.Request;
using SFMSSolution.Application.DataTransferObjects.Facility;
using SFMSSolution.Application.Services.Facilities;
using SFMSSolution.Domain.Entities;
using SFMSSolution.Infrastructure.Implements.UnitOfWorks;

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
        var allFacilities = await _unitOfWork.FacilityRepository.GetFacilitiesWithDetailsAsync();

        var ownerFacilities = allFacilities
            .Where(f => f.OwnerId == ownerId)
            .ToList();
        if (!string.IsNullOrWhiteSpace(name))
        {
            allFacilities = allFacilities.Where(f => f.Name.Contains(name, StringComparison.OrdinalIgnoreCase));
        }
        var totalCount = ownerFacilities.Count;

        var pagedFacilities = ownerFacilities
            .Skip((pageNumber - 1) * pageSize)
            .Take(pageSize);

        return (_mapper.Map<IEnumerable<FacilityDto>>(pagedFacilities), totalCount);
    }

    // Filter facilities by name (for users)
    public async Task<(IEnumerable<FacilityDto> Facilities, int TotalCount)> GetAllFacilitiesAsync(string? name, int pageNumber, int pageSize)
    {
        var facilities = await _unitOfWork.FacilityRepository.GetFacilitiesWithDetailsAsync();

        if (!string.IsNullOrWhiteSpace(name))
        {
            facilities = facilities.Where(f => f.Name.Contains(name, StringComparison.OrdinalIgnoreCase));
        }

        var totalCount = facilities.Count();

        var filteredFacilities = facilities
            .Skip((pageNumber - 1) * pageSize)
            .Take(pageSize)
            .ToList();

        return (_mapper.Map<IEnumerable<FacilityDto>>(filteredFacilities), totalCount);
    }

    public async Task<(IEnumerable<FacilityDto> Facilities, int TotalCount)> FilterFacilitiesAsync(
    string? name, int pageNumber, int pageSize)
    {
        var facilities = await _unitOfWork.FacilityRepository.GetFacilitiesWithDetailsAsync();

        if (!string.IsNullOrWhiteSpace(name))
        {
            facilities = facilities.Where(f => f.Name.Contains(name, StringComparison.OrdinalIgnoreCase));
        }

        var totalCount = facilities.Count();

        var filteredFacilities = facilities
            .Skip((pageNumber - 1) * pageSize)
            .Take(pageSize)
            .ToList();

        return (_mapper.Map<IEnumerable<FacilityDto>>(filteredFacilities), totalCount);
    }


    public async Task<bool> CreateFacilityAsync(FacilityCreateRequestDto request)
    {
        var facility = _mapper.Map<Facility>(request);

        // Gán thêm OwnerId nếu cần, ví dụ từ token (nếu bạn truyền từ controller)
        // facility.OwnerId = currentUserId;

        await _unitOfWork.FacilityRepository.AddAsync(facility);
        await _unitOfWork.CompleteAsync();
        return true;
    }

    public async Task<bool> UpdateFacilityAsync(FacilityUpdateRequestDto request)
    {
        var facility = await _unitOfWork.FacilityRepository.GetFacilityByIdAsync(request.Id);
        if (facility == null)
            return false;

        facility.Name = request.Name;
        facility.Description = request.Description;
        facility.Address = request.Address;
        facility.ImageUrl = request.ImageUrl;
        facility.FacilityType = request.FacilityType;
        facility.UpdatedDate = DateTime.UtcNow;

        await _unitOfWork.FacilityRepository.UpdateAsync(facility);
        await _unitOfWork.CompleteAsync();
        return true;
    }

    public async Task<bool> DeleteFacilityAsync(Guid id)
    {
        await _unitOfWork.FacilityRepository.DeleteAsync(id);
        await _unitOfWork.CompleteAsync();
        return true;
    }
}
