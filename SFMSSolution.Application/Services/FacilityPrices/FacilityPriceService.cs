using AutoMapper;
using SFMSSolution.Application.DataTransferObjects.FacilityPrice.Request;
using SFMSSolution.Application.DataTransferObjects.FacilityPrice;
using SFMSSolution.Application.Services.FacilityPrices;
using SFMSSolution.Domain.Entities;
using SFMSSolution.Infrastructure.Implements.UnitOfWorks;

namespace SFMSSolution.Application.Services
{
    public class FacilityPriceService : IFacilityPriceService
    {
        private readonly IUnitOfWork _unitOfWork;
        private readonly IMapper _mapper;

        public FacilityPriceService(IUnitOfWork unitOfWork, IMapper mapper)
        {
            _unitOfWork = unitOfWork;
            _mapper = mapper;
        }

        public async Task<bool> AddOrUpdatePriceAsync(FacilityPriceCreateRequestDto request)
        {
            // Lấy TimeSlot kèm Facility
            var timeSlot = await _unitOfWork.FacilityTimeSlotRepository
                .FindFirstOrDefaultAsync(ts => ts.Id == request.FacilityTimeSlotId, include => include.Facility);

            if (timeSlot == null || timeSlot.Facility == null)
                return false;

            var facilityType = timeSlot.Facility.FacilityType;

            // Lấy giá cơ bản theo FacilityType
            var basePriceEntity = await _unitOfWork.PriceRepository.GetByFacilityTypeAsync(facilityType);
            if (basePriceEntity == null)
                return false;

            var finalPrice = basePriceEntity.BasePrice * request.Coefficient;

            // Kiểm tra xem FacilityPrice đã tồn tại
            var existingFacilityPrice = await _unitOfWork.FacilityPriceRepository
                .FindAsync(fp => fp.FacilityTimeSlotId == request.FacilityTimeSlotId);

            var facilityPrice = existingFacilityPrice.FirstOrDefault();

            if (facilityPrice == null)
            {
                facilityPrice = _mapper.Map<FacilityPrice>(request);
                facilityPrice.FinalPrice = finalPrice;

                await _unitOfWork.FacilityPriceRepository.AddAsync(facilityPrice);
            }
            else
            {
                facilityPrice.Coefficient = request.Coefficient;
                facilityPrice.FinalPrice = finalPrice;

                await _unitOfWork.FacilityPriceRepository.UpdateAsync(facilityPrice);
            }

            await _unitOfWork.CompleteAsync();
            return true;
        }

        public async Task<List<FacilityPriceDto>> GetPricesByTimeSlotAsync(Guid facilityTimeSlotId)
        {
            var facilityPrices = await _unitOfWork.FacilityPriceRepository
                .GetByFacilityTimeSlotIdAsync(facilityTimeSlotId);

            return _mapper.Map<List<FacilityPriceDto>>(facilityPrices);
        }
    }
}
