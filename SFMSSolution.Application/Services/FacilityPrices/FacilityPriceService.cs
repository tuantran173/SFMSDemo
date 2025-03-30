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
            // Tìm giá cơ bản theo CategoryId
            var basePriceEntity = await _unitOfWork.PriceRepository.GetByCategoryIdAsync(request.CategoryId);
            if (basePriceEntity == null) return false;

            // Tính toán giá cuối cùng
            var finalPrice = basePriceEntity.BasePrice * request.Coefficient;

            // Kiểm tra xem FacilityPrice đã tồn tại cho FacilityTimeSlotId hay chưa
            var existingFacilityPrice = await _unitOfWork.FacilityPriceRepository
                .FindAsync(fp => fp.FacilityTimeSlotId == request.FacilityTimeSlotId);

            var facilityPrice = existingFacilityPrice.FirstOrDefault();

            if (facilityPrice == null)
            {
                // Tạo mới
                facilityPrice = _mapper.Map<FacilityPrice>(request);
                facilityPrice.FinalPrice = finalPrice;

                await _unitOfWork.FacilityPriceRepository.AddAsync(facilityPrice);
            }
            else
            {
                // Cập nhật
                facilityPrice.Coefficient = request.Coefficient;
                facilityPrice.FinalPrice = finalPrice;

                await _unitOfWork.FacilityPriceRepository.UpdateAsync(facilityPrice);
            }

            await _unitOfWork.CompleteAsync();
            return true;
        }

        public async Task<List<FacilityPriceDto>> GetPricesByTimeSlotAsync(Guid facilityTimeSlotId)
        {
            var facilityPrices = await _unitOfWork.FacilityPriceRepository.GetByFacilityTimeSlotIdAsync(facilityTimeSlotId);
            return _mapper.Map<List<FacilityPriceDto>>(facilityPrices);
        }
    }
}