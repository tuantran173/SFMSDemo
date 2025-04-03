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
            var facility = await _unitOfWork.FacilityRepository.GetByIdAsync(request.FacilityId);
            if (facility == null)
                return new ApiResponse<string>("Facility not found.");

            if (request.StartDate > request.EndDate)
                return new ApiResponse<string>("StartDate cannot be after EndDate.");

            if (request.StartTime >= request.EndTime)
                return new ApiResponse<string>("StartTime must be before EndTime.");

            var existingTimeSlots = await _unitOfWork.FacilityTimeSlotRepository.GetByFacilityIdAsync(request.FacilityId);
            bool hasOverlap = existingTimeSlots.Any(slot =>
                slot.StartDate <= request.EndDate &&
                slot.EndDate >= request.StartDate &&
                slot.StartTime < request.EndTime &&
                slot.EndTime > request.StartTime
            );
            if (hasOverlap)
                return new ApiResponse<string>("Time slot overlaps with existing slot for this facility.");

            bool isExactDuplicate = existingTimeSlots.Any(slot =>
                slot.StartDate == request.StartDate &&
                slot.EndDate == request.EndDate &&
                slot.StartTime == request.StartTime &&
                slot.EndTime == request.EndTime
            );

            if (isExactDuplicate)
                return new ApiResponse<string>("A time slot with the exact same time already exists.");


            var newTimeSlot = new FacilityTimeSlot
            {
                Id = Guid.NewGuid(),
                FacilityId = request.FacilityId,
                StartTime = request.StartTime,
                EndTime = request.EndTime,
                StartDate = request.StartDate,
                EndDate = request.EndDate,
                IsWeekend = false,
                CreatedDate = DateTime.UtcNow
            };

            await _unitOfWork.FacilityTimeSlotRepository.AddAsync(newTimeSlot);

            var finalPrice = request.BasePrice * request.Coefficient;

            var facilityPrice = new FacilityPrice
            {
                Id = Guid.NewGuid(),
                FacilityId = request.FacilityId,
                FacilityTimeSlotId = newTimeSlot.Id,
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

            var existingTimeSlots = await _unitOfWork.FacilityTimeSlotRepository.GetByFacilityIdAsync(entity.FacilityId);
            bool hasOverlap = existingTimeSlots.Any(slot =>
                slot.Id != entity.FacilityTimeSlotId &&
                slot.StartDate <= request.EndDate &&
                slot.EndDate >= request.StartDate &&
                slot.StartTime < request.EndTime &&
                slot.EndTime > request.StartTime
            );
            if (hasOverlap)
                return new ApiResponse<string>("Time slot overlaps with existing slot for this facility.");

            entity.BasePrice = request.BasePrice;
            entity.Coefficient = request.Coefficient;
            entity.FinalPrice = request.BasePrice * request.Coefficient;
            entity.UpdatedDate = DateTime.UtcNow;

            entity.FacilityTimeSlot.StartDate = request.StartDate;
            entity.FacilityTimeSlot.EndDate = request.EndDate;
            entity.FacilityTimeSlot.StartTime = request.StartTime;
            entity.FacilityTimeSlot.EndTime = request.EndTime;
            entity.FacilityTimeSlot.UpdatedDate = DateTime.UtcNow;

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
