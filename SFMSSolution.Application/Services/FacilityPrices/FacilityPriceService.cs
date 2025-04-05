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

        //public async Task<ApiResponse<string>> CreatePriceAsync(FacilityPriceCreateRequestDto request)
        //{
        //    var facility = await _unitOfWork.FacilityRepository.GetByIdAsync(request.FacilityId);
        //    if (facility == null)
        //        return new ApiResponse<string>("Facility not found.");

        //    if (request.StartDate > request.EndDate)
        //        return new ApiResponse<string>("StartDate cannot be after EndDate.");

        //    var slotDuration = TimeSpan.FromMinutes(90);
        //    var openTime = new TimeSpan(5, 0, 0);
        //    var closeTime = new TimeSpan(23, 0, 0);

        //    var timeSlots = new List<FacilityTimeSlot>();
        //    var prices = new List<FacilityPrice>();

        //    for (var time = openTime; time + slotDuration <= closeTime; time += slotDuration)
        //    {
        //        var endTime = time + slotDuration;

        //        var newSlot = new FacilityTimeSlot
        //        {
        //            Id = Guid.NewGuid(),
        //            FacilityId = request.FacilityId,
        //            StartTime = time,
        //            EndTime = endTime,
        //            StartDate = request.StartDate,
        //            EndDate = request.EndDate,
        //            Status = SlotStatus.Available,
        //            IsWeekend = false,
        //            CreatedDate = DateTime.UtcNow
        //        };

        //        var finalPrice = request.BasePrice * (decimal)request.Coefficient;

        //        var price = new FacilityPrice
        //        {
        //            Id = Guid.NewGuid(),
        //            FacilityId = request.FacilityId,
        //            FacilityTimeSlotId = newSlot.Id,
        //            BasePrice = request.BasePrice,
        //            Coefficient = request.Coefficient,
        //            FinalPrice = finalPrice,
        //            CreatedDate = DateTime.UtcNow
        //        };

        //        timeSlots.Add(newSlot);
        //        prices.Add(price);
        //    }

        //    await _unitOfWork.FacilityTimeSlotRepository.AddRangeAsync(timeSlots);
        //    await _unitOfWork.FacilityPriceRepository.AddRangeAsync(prices);
        //    await _unitOfWork.CompleteAsync();

        //    return new ApiResponse<string>(true,"Created 12 slots and assigned prices successfully.");
        //}

        public async Task<ApiResponse<string>> CreatePriceAsync(FacilityPriceCreateRequestDto request)
        {
            var facility = await _unitOfWork.FacilityRepository.GetByIdAsync(request.FacilityId);
            if (facility == null)
                return new ApiResponse<string>("Facility not found.");

            if (request.StartDate > request.EndDate)
                return new ApiResponse<string>("StartDate cannot be after EndDate.");

            if (request.StartTime >= request.EndTime)
                return new ApiResponse<string>("StartTime must be before EndTime.");

            var slotDuration = TimeSpan.FromMinutes(90);
            var openTime = new TimeSpan(5, 0, 0);
            var closeTime = new TimeSpan(23, 0, 0);

            var timeSlots = new List<FacilityTimeSlot>();
            var prices = new List<FacilityPrice>();

            for (var time = openTime; time + slotDuration <= closeTime; time += slotDuration)
            {
                var endTime = time + slotDuration;

                // ✅ Chỉ tạo slot nằm trong khoảng yêu cầu
                if (time < request.StartTime || endTime > request.EndTime)
                    continue;

                var newSlot = new FacilityTimeSlot
                {
                    Id = Guid.NewGuid(),
                    FacilityId = request.FacilityId,
                    StartTime = time,
                    EndTime = endTime,
                    StartDate = request.StartDate,
                    EndDate = request.EndDate,
                    Status = SlotStatus.Available,
                    IsWeekend = false,
                    CreatedDate = DateTime.UtcNow
                };

                var finalPrice = request.BasePrice * (decimal)request.Coefficient;

                var price = new FacilityPrice
                {
                    Id = Guid.NewGuid(),
                    FacilityId = request.FacilityId,
                    FacilityTimeSlotId = newSlot.Id,
                    BasePrice = request.BasePrice,
                    Coefficient = request.Coefficient,
                    FinalPrice = finalPrice,
                    CreatedDate = DateTime.UtcNow
                };

                timeSlots.Add(newSlot);
                prices.Add(price);
            }

            if (timeSlots.Count == 0)
                return new ApiResponse<string>("No valid time slots were created based on the time range provided.");

            await _unitOfWork.FacilityTimeSlotRepository.AddRangeAsync(timeSlots);
            await _unitOfWork.FacilityPriceRepository.AddRangeAsync(prices);
            await _unitOfWork.CompleteAsync();

            return new ApiResponse<string>(true, $"Created {timeSlots.Count} slots and assigned prices successfully.");
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
                FacilityId = entity.FacilityId,
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
