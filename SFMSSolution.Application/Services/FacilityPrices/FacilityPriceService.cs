using AutoMapper;
using SFMSSolution.Application.DataTransferObjects.FacilityPrice.Request;
using SFMSSolution.Application.DataTransferObjects.FacilityPrice;
using SFMSSolution.Application.Services.FacilityPrices;
using SFMSSolution.Domain.Entities;
using SFMSSolution.Infrastructure.Implements.UnitOfWorks;
using SFMSSolution.Response;
using SFMSSolution.Domain.Enums;

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

            // 1. Tạo slot duy nhất
            var newSlot = new FacilityTimeSlot
            {
                Id = Guid.NewGuid(),
                FacilityId = request.FacilityId,
                StartTime = request.StartTime,  // Ví dụ: 05:00
                EndTime = request.EndTime,      // Ví dụ: 14:00
                StartDate = request.StartDate,
                EndDate = request.EndDate,
                Status = SlotStatus.Available,
                CreatedDate = DateTime.UtcNow
            };

            await _unitOfWork.FacilityTimeSlotRepository.AddAsync(newSlot);

            // 2. Tạo giá cho slot đó
            var finalPrice = request.BasePrice * (decimal)request.Coefficient;

            var facilityPrice = new FacilityPrice
            {
                Id = Guid.NewGuid(),
                FacilityId = request.FacilityId,
                FacilityTimeSlotId = newSlot.Id,
                ImageUrl = request.PriceImageUrl,
                BasePrice = request.BasePrice,
                Coefficient = request.Coefficient,
                FinalPrice = finalPrice,
                CreatedDate = DateTime.UtcNow
            };

            await _unitOfWork.FacilityPriceRepository.AddAsync(facilityPrice);
            await _unitOfWork.CompleteAsync();

            return new ApiResponse<string>(true, "Created a time slot and assigned price successfully.");
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
            entity.ImageUrl = request.PriceImageUrl;
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

        public async Task<(IEnumerable<FacilityPriceDto> Prices, int TotalCount)> GetByOwnerAsync(Guid ownerId, string? facilityName, int pageNumber, int pageSize)
        {
            var (prices, total) = await _unitOfWork.FacilityPriceRepository.GetAllWithTimeSlotAndFacilityAsync(pageNumber, pageSize);

            // Lọc theo ownerId
            var filtered = prices.Where(p => p.Facility != null && p.Facility.OwnerId == ownerId);

            // Lọc theo tên sân nếu có
            if (!string.IsNullOrWhiteSpace(facilityName))
            {
                filtered = filtered.Where(p => p.Facility.Name.Contains(facilityName, StringComparison.OrdinalIgnoreCase));
            }

            var dtos = filtered.Select(p => new FacilityPriceDto
            {
                Id = p.Id,
                FacilityId = p.FacilityId,
                ImageUrl = p.Facility?.ImageUrl,
                FacilityName = p.Facility?.Name,
                StartTime = p.FacilityTimeSlot.StartTime,
                EndTime = p.FacilityTimeSlot.EndTime,
                StartDate = p.FacilityTimeSlot.StartDate,
                EndDate = p.FacilityTimeSlot.EndDate,
                PriceImageUrl = p.ImageUrl,
                BasePrice = p.BasePrice,
                Coefficient = p.Coefficient,
                FinalPrice = p.FinalPrice
            });

            return (dtos, dtos.Count()); // count sau khi filter
        }

        public async Task<FacilityPriceDto?> GetByIdAsync(Guid id)
        {
            var entity = await _unitOfWork.FacilityPriceRepository.GetByIdWithTimeSlotAndFacilityAsync(id);
            if (entity == null) return null;

            return new FacilityPriceDto
            {
                Id = entity.Id,
                FacilityId = entity.FacilityId,
                ImageUrl = entity.Facility?.ImageUrl,
                FacilityName = entity.Facility?.Name,
                StartTime = entity.FacilityTimeSlot.StartTime,
                EndTime = entity.FacilityTimeSlot.EndTime,
                StartDate = entity.FacilityTimeSlot.StartDate,
                EndDate = entity.FacilityTimeSlot.EndDate,
                PriceImageUrl = entity.ImageUrl,
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
