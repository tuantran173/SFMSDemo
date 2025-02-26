using AutoMapper;
using SFMSSolution.Application.DataTransferObjects.Facility.Request;
using SFMSSolution.Application.DataTransferObjects.Facility;
using SFMSSolution.Domain.Entities;
using SFMSSolution.Infrastructure.Implements.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using SFMSSolution.Infrastructure.Implements.UnitOfWorks;

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

        public async Task<IEnumerable<FacilityDto>> GetAllFacilitiesAsync()
        {
            var facilities = await _unitOfWork.FacilityRepository.GetAllAsync();
            return _mapper.Map<IEnumerable<FacilityDto>>(facilities);
        }

        public async Task<bool> CreateFacilityAsync(FacilityCreateRequestDto request)
        {
            var facility = _mapper.Map<Facility>(request);
            facility.Status = "Active"; // Đặt trạng thái mặc định là Active
            var result = await _unitOfWork.FacilityRepository.AddAsync(facility);
            if (result)
            {
                await _unitOfWork.CompleteAsync();
            }
            return result;
        }

        public async Task<bool> UpdateFacilityAsync(Guid id, FacilityUpdateRequestDto request)
        {
            var facility = await _unitOfWork.FacilityRepository.GetByIdAsync(id);
            if (facility == null)
                return false;

            facility.Name = request.Name;
            facility.Description = request.Description;
            facility.Location = request.Location;
            facility.Capacity = request.Capacity;
            facility.Images = request.Images;
            if (!string.IsNullOrEmpty(request.Status))
            {
                facility.Status = request.Status;
            }
            facility.UpdatedDate = DateTime.UtcNow;
            var result = await _unitOfWork.FacilityRepository.UpdateAsync(facility);
            if (result)
            {
                await _unitOfWork.CompleteAsync();
            }
            return result;
        }

        public async Task<bool> DeleteFacilityAsync(Guid id)
        {
            var result = await _unitOfWork.FacilityRepository.DeleteAsync(id);
            if (result)
            {
                await _unitOfWork.CompleteAsync();
            }
            return result;
        }
    }
}
