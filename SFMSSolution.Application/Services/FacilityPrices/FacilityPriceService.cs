using AutoMapper;
using SFMSSolution.Application.DataTransferObjects.FacilityPrice;
using SFMSSolution.Application.DataTransferObjects.FacilityPrice.Request;
using SFMSSolution.Application.Services.FacilityPrices;
using SFMSSolution.Domain.Entities;
using SFMSSolution.Infrastructure.Implements.UnitOfWorks;
using SFMSSolution.Response;

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

        public async Task<ApiResponse<string>> CreatePriceAsync(FacilityPriceCreateRequestDto request)
        {
            // Kiểm tra slot tồn tại
            var slot = await _unitOfWork.FacilityTimeSlotRepository.GetByIdAsync(request.FacilityTimeSlotId);
            if (slot == null)
                return new ApiResponse<string>("Time slot not found.");

            // Kiểm tra đã có giá cho slot này chưa
            var existing = await _unitOfWork.FacilityPriceRepository.GetByTimeSlotIdAsync(request.FacilityTimeSlotId);
            if (existing != null)
                return new ApiResponse<string>("Price already exists for this time slot.");

            var finalPrice = request.BasePrice * request.Coefficient;

            var facilityPrice = new FacilityPrice
            {
                Id = Guid.NewGuid(),
                FacilityId = slot.FacilityId,
                FacilityTimeSlotId = slot.Id,
                BasePrice = request.BasePrice,
                Coefficient = request.Coefficient,
                FinalPrice = finalPrice,
                CreatedDate = DateTime.UtcNow
            };

            await _unitOfWork.FacilityPriceRepository.AddAsync(facilityPrice);
            await _unitOfWork.CompleteAsync();

            return new ApiResponse<string>(facilityPrice.Id.ToString(), "Facility price created successfully.");
        }


        public async Task<ApiResponse<string>> UpdatePriceAsync(FacilityPriceUpdateRequestDto request)
        {
            if (request.StartDate > request.EndDate)
                return new ApiResponse<string>("StartDate cannot be after EndDate.");

            if (request.StartTime >= request.EndTime)
                return new ApiResponse<string>("StartTime must be before EndTime.");

            var entity = await _unitOfWork.FacilityPriceRepository.GetByIdWithTimeSlotAsync(request.Id);
            if (entity == null)
                return new ApiResponse<string>("Facility price not found.");

            var timeSlot = entity.FacilityTimeSlot;

            // Nếu thời gian thực sự thay đổi thì mới kiểm tra trùng
            bool isTimeChanged =
                timeSlot.StartDate != request.StartDate ||
                timeSlot.EndDate != request.EndDate ||
                timeSlot.StartTime != request.StartTime ||
                timeSlot.EndTime != request.EndTime;

            if (isTimeChanged)
            {
                var existingTimeSlots = await _unitOfWork.FacilityTimeSlotRepository.GetByFacilityIdAsync(entity.FacilityId);
                bool hasOverlap = existingTimeSlots.Any(slot =>
                    slot.Id != timeSlot.Id &&
                    slot.StartDate <= request.EndDate &&
                    slot.EndDate >= request.StartDate &&
                    slot.StartTime < request.EndTime &&
                    slot.EndTime > request.StartTime
                );
                if (hasOverlap)
                    return new ApiResponse<string>("Time slot overlaps with existing slot for this facility.");
            }

            // Update giá
            entity.BasePrice = request.BasePrice;
            entity.Coefficient = request.Coefficient;
            entity.FinalPrice = request.BasePrice * request.Coefficient;
            entity.UpdatedDate = DateTime.UtcNow;

            //// Update thời gian nếu có thay đổi
            //timeSlot.StartDate = request.StartDate;
            //timeSlot.EndDate = request.EndDate;
            //timeSlot.StartTime = request.StartTime;
            //timeSlot.EndTime = request.EndTime;
            //timeSlot.UpdatedDate = DateTime.UtcNow;

            await _unitOfWork.FacilityPriceRepository.UpdateAsync(entity);
            await _unitOfWork.CompleteAsync();

            return new ApiResponse<string>("Facility price updated successfully.");
        }


        public async Task<(IEnumerable<FacilityPriceDto> Prices, int TotalCount)> GetAllAsync(string? facilityName, int pageNumber, int pageSize)
        {
            var (prices, total) = await _unitOfWork.FacilityPriceRepository.GetAllWithTimeSlotAndFacilityAsync(pageNumber, pageSize);

            if (!string.IsNullOrWhiteSpace(facilityName))
            {
                prices = prices.Where(p => p.Facility != null &&
                    p.Facility.Name.Contains(facilityName, StringComparison.OrdinalIgnoreCase));
            }

            var dtos = prices.Select(p => new FacilityPriceDto
            {
                Id = p.Id,
                ImageUrl = p.Facility?.ImageUrl,
                FacilityName = p.Facility?.Name,
                StartTime = p.FacilityTimeSlot.StartTime,
                EndTime = p.FacilityTimeSlot.EndTime,
                StartDate = p.FacilityTimeSlot.StartDate,
                EndDate = p.FacilityTimeSlot.EndDate,
                BasePrice = p.BasePrice,
                Coefficient = p.Coefficient,
                FinalPrice = p.FinalPrice
            });

            return (dtos, total);
        }

        public async Task<FacilityPriceDto?> GetByIdAsync(Guid id)
        {
            var entity = await _unitOfWork.FacilityPriceRepository.GetByIdWithTimeSlotAndFacilityAsync(id);
            if (entity == null) return null;

            return new FacilityPriceDto
            {
                Id = entity.Id,
                ImageUrl = entity.Facility?.ImageUrl,
                FacilityName = entity.Facility?.Name,
                StartTime = entity.FacilityTimeSlot.StartTime,
                EndTime = entity.FacilityTimeSlot.EndTime,
                StartDate = entity.FacilityTimeSlot.StartDate,
                EndDate = entity.FacilityTimeSlot.EndDate,
                BasePrice = entity.BasePrice,
                Coefficient = entity.Coefficient,
                FinalPrice = entity.FinalPrice
            };
        }

        public async Task<ApiResponse<string>> DeleteAsync(Guid id)
        {
            await _unitOfWork.FacilityPriceRepository.DeleteAsync(id);
            await _unitOfWork.CompleteAsync();
            return new ApiResponse<string>("Facility price deleted successfully.");
        }
    }
}
