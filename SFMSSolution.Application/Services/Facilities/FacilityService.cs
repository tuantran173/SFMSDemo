using AutoMapper;
using SFMSSolution.Application.DataTransferObjects.Facility.Request;
using SFMSSolution.Application.DataTransferObjects.Facility;
using SFMSSolution.Domain.Entities;
using SFMSSolution.Infrastructure.Implements.Interfaces;
using SFMSSolution.Infrastructure.Implements.UnitOfWorks;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace SFMSSolution.Application.Services.Facilities
{
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
            var facility = await _unitOfWork.FacilityRepository.GetByIdAsync(id);
            return _mapper.Map<FacilityDto>(facility);
        }

        public async Task<(IEnumerable<FacilityDto> Facilities, int TotalCount)> GetAllFacilitiesAsync(int pageNumber, int pageSize)
        {
            var facilities = await _unitOfWork.FacilityRepository.GetAllAsync(pageNumber, pageSize);
            var totalCount = await _unitOfWork.FacilityRepository.CountAsync();
            return (_mapper.Map<IEnumerable<FacilityDto>>(facilities), totalCount);
        }

        public async Task<(IEnumerable<FacilityDto> Facilities, int TotalCount)> SearchFacilitiesByNameAsync(string name, int pageNumber, int pageSize)
        {
            var facilities = await _unitOfWork.FacilityRepository.GetByNameAsync(name, pageNumber, pageSize);
            var totalCount = await _unitOfWork.FacilityRepository.CountByNameAsync(name);
            return (_mapper.Map<IEnumerable<FacilityDto>>(facilities), totalCount);
        }

        public async Task<(IEnumerable<FacilityDto> Facilities, int TotalCount)> FilterFacilitiesAsync(
            Guid? categoryId, string? location, TimeSpan? startTime, TimeSpan? endTime, int pageNumber, int pageSize)
        {
            var facilities = await _unitOfWork.FacilityRepository.GetFacilitiesWithDetailsAsync();

            if (categoryId.HasValue)
                facilities = facilities.Where(f => f.CategoryId == categoryId.Value);

            if (!string.IsNullOrWhiteSpace(location))
                facilities = facilities.Where(f => f.Address.Contains(location, StringComparison.OrdinalIgnoreCase));

            if (startTime.HasValue && endTime.HasValue)
            {
                facilities = facilities.Where(f => f.FacilityTimeSlots.Any(ts =>
                    ts.StartTime <= startTime.Value && ts.EndTime >= endTime.Value));
            }

            var filteredFacilities = facilities
                .Skip((pageNumber - 1) * pageSize)
                .Take(pageSize)
                .ToList();

            var totalCount = facilities.Count();
            return (_mapper.Map<IEnumerable<FacilityDto>>(filteredFacilities), totalCount);
        }

        public async Task<bool> CreateFacilityAsync(FacilityCreateRequestDto request)
        {
            var facility = _mapper.Map<Facility>(request);
            await _unitOfWork.FacilityRepository.AddAsync(facility);
            await _unitOfWork.CompleteAsync();
            return true;
        }

        public async Task<bool> UpdateFacilityAsync(Guid id, FacilityUpdateRequestDto request)
        {
            var facility = await _unitOfWork.FacilityRepository.GetByIdAsync(id);
            if (facility == null)
                return false;

            facility.Name = request.Name;
            facility.Description = request.Description;
            facility.Address = request.Address;
            facility.Capacity = request.Capacity;
            facility.ImageUrl = request.ImageUrl;
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
}
