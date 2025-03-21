using AutoMapper;
using SFMSSolution.Application.DataTransferObjects.FacilityPrice;
using SFMSSolution.Application.DataTransferObjects.FacilityPrice.Request;
using SFMSSolution.Domain.Entities;
using SFMSSolution.Infrastructure.Implements.UnitOfWorks;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SFMSSolution.Application.Services.FacilityPrices
{
    public class FacilityPriceService: IFacilityPriceService
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
            // Kiểm tra dữ liệu truyền vào có hợp lệ không
            if (!TimeSpan.TryParse(request.StartTime, out var parsedStartTime) ||
                !TimeSpan.TryParse(request.EndTime, out var parsedEndTime))
            {
                throw new ArgumentException("Invalid time format. Use HH:mm:ss or HH:mm.");
            }

            // Tìm giá cơ bản theo CategoryId
            var basePriceEntity = await _unitOfWork.PriceRepository.GetByCategoryIdAsync(request.CategoryId);
            if (basePriceEntity == null) return false;

            // Tính toán giá cuối cùng
            var finalPrice = basePriceEntity.BasePrice * request.Coefficient;

            // Tìm xem đã tồn tại FacilityPrice với khung giờ và FacilityTimeSlotId này chưa
            var existingFacilityPrice = await _unitOfWork.FacilityPriceRepository
                .FindAsync(fp => fp.FacilityTimeSlotId == request.FacilityTimeSlotId
                              && fp.StartTime == parsedStartTime
                              && fp.EndTime == parsedEndTime);

            var facilityPrice = existingFacilityPrice.FirstOrDefault();

            if (facilityPrice == null)
            {
                // Tạo mới FacilityPrice
                facilityPrice = _mapper.Map<FacilityPrice>(request);
                facilityPrice.FinalPrice = finalPrice;
                facilityPrice.StartTime = parsedStartTime;
                facilityPrice.EndTime = parsedEndTime;

                await _unitOfWork.FacilityPriceRepository.AddAsync(facilityPrice);
            }
            else
            {
                // Cập nhật FacilityPrice nếu đã tồn tại
                facilityPrice.Coefficient = request.Coefficient;
                facilityPrice.FinalPrice = finalPrice;
                facilityPrice.StartTime = parsedStartTime;
                facilityPrice.EndTime = parsedEndTime;

                await _unitOfWork.FacilityPriceRepository.UpdateAsync(facilityPrice);
            }

            // Lưu thay đổi vào cơ sở dữ liệu
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
