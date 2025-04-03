using AutoMapper;
using SFMSSolution.Application.DataTransferObjects.Booking;
using SFMSSolution.Application.DataTransferObjects.Booking.Request;
using SFMSSolution.Domain.Entities;
using SFMSSolution.Domain.Enums;
using SFMSSolution.Infrastructure.Implements.Interfaces;
using SFMSSolution.Infrastructure.Implements.UnitOfWorks;
using SFMSSolution.Response;

namespace SFMSSolution.Application.Services.Bookings
{
    public class BookingService : IBookingService
    {
        private readonly IUnitOfWork _unitOfWork;
        private readonly IMapper _mapper;

        public BookingService(IUnitOfWork unitOfWork, IMapper mapper)
        {
            _unitOfWork = unitOfWork;
            _mapper = mapper;
        }

        public async Task<BookingDto> GetBookingAsync(Guid id)
        {
            var booking = await _unitOfWork.BookingRepository.GetBookingByIdWithDetailsAsync(id);
            return _mapper.Map<BookingDto>(booking);
        }

        public async Task<(IEnumerable<BookingDto> Bookings, int TotalCount)> GetAllBookingsAsync(string? name, int pageNumber, int pageSize)
        {
            var (bookings, totalCount) = await _unitOfWork.BookingRepository.GetAllBookingsWithDetailsAsync(name, pageNumber, pageSize);
            var bookingDtos = _mapper.Map<IEnumerable<BookingDto>>(bookings);
            return (bookingDtos, totalCount);
        }

        public async Task<IEnumerable<BookingDto>> GetBookingsByUserAsync(Guid userId)
        {
            var bookings = await _unitOfWork.BookingRepository.GetBookingsByUserAsync(userId);
            return _mapper.Map<IEnumerable<BookingDto>>(bookings);
        }

        public async Task<bool> CreateBookingAsync(BookingCreateRequestDto request)
        {
            var isBooked = await _unitOfWork.BookingRepository.IsTimeSlotBooked(request.FacilityTimeSlotId, request.BookingDate);
            if (isBooked)
                throw new Exception("Time slot is already booked.");

            var booking = _mapper.Map<Booking>(request);
            booking.Status = BookingStatus.Pending;

            await _unitOfWork.BookingRepository.AddAsync(booking);
            await _unitOfWork.CompleteAsync();

            return true;
        }

        public async Task<bool> UpdateBookingAsync(Guid id, BookingUpdateRequestDto request)
        {
            var booking = await _unitOfWork.BookingRepository.GetByIdAsync(id);
            if (booking == null)
                return false;

            booking.BookingDate = request.BookingDate;
            booking.Note = request.Note;
            booking.FacilityTimeSlotId = request.FacilityTimeSlotId;

            if (!string.IsNullOrWhiteSpace(request.Status) &&
                Enum.TryParse<BookingStatus>(request.Status, true, out var parsedStatus))
            {
                booking.Status = parsedStatus;
            }

            booking.UpdatedDate = DateTime.UtcNow;

            await _unitOfWork.BookingRepository.UpdateAsync(booking);
            await _unitOfWork.CompleteAsync();

            return true;
        }

        public async Task<bool> DeleteBookingAsync(Guid id)
        {
            await _unitOfWork.BookingRepository.DeleteAsync(id);
            await _unitOfWork.CompleteAsync();
            return true;
        }

        public async Task<bool> UpdateBookingStatusAsync(Guid bookingId, BookingStatusUpdateRequestDto request)
        {
            var booking = await _unitOfWork.BookingRepository.GetByIdAsync(bookingId);
            if (booking == null) return false;

            if (!Enum.TryParse<BookingStatus>(request.Status, true, out var status))
                throw new Exception("Invalid status value.");

            booking.Status = status;
            booking.UpdatedDate = DateTime.UtcNow;

            await _unitOfWork.BookingRepository.UpdateAsync(booking);
            await _unitOfWork.CompleteAsync();

            return true;
        }

        public async Task<IEnumerable<BookingDto>> GetBookingHistoryForUserAsync(Guid userId)
        {
            var now = DateTime.UtcNow;
            var bookings = await _unitOfWork.BookingRepository.GetBookingsByUserAsync(userId);

            var history = bookings
                .Where(b => b.BookingDate.Date < now.Date ||
                       (b.BookingDate.Date == now.Date && b.FacilityTimeSlot.EndTime < now.TimeOfDay))
                .ToList();

            return _mapper.Map<IEnumerable<BookingDto>>(history);
        }

        public async Task<ApiResponse<FacilityBookingCalendarDto>> GetFacilityCalendarAsync(Guid facilityId)
        {
            var facility = await _unitOfWork.FacilityRepository.GetFacilityByIdAsync(facilityId);
            if (facility == null)
                return new ApiResponse<FacilityBookingCalendarDto>("Facility not found.");

            var startHour = new TimeSpan(5, 0, 0);
            var endHour = new TimeSpan(23, 30, 0);
            var slotDuration = TimeSpan.FromMinutes(90);
            var today = DateTime.Today;
            var futureDays = 14;

            var timeSlots = await _unitOfWork.FacilityTimeSlotRepository.GetByFacilityIdAsync(facilityId);
            var bookings = await _unitOfWork.BookingRepository.GetBookingsByFacilityAsync(facilityId, DateTime.UtcNow);

            var calendarItems = new List<FacilityBookingSlotDto>();

            for (int day = 0; day < futureDays; day++)
            {
                var currentDate = today.AddDays(day);
                for (var time = startHour; time < endHour; time += slotDuration)
                {
                    var slotEnd = time + slotDuration;
                    var slot = timeSlots.FirstOrDefault(s =>
                        s.StartDate <= currentDate && s.EndDate >= currentDate &&
                        Math.Abs((s.StartTime - time).TotalMinutes) < 1 &&
                        Math.Abs((s.EndTime - slotEnd).TotalMinutes) < 1);

                    var booking = bookings.FirstOrDefault(b => b.FacilityTimeSlotId == slot?.Id && b.BookingDate.Date == currentDate);

                    var status = booking != null
                        ? SlotStatus.Booked
                        : slot != null ? SlotStatus.Available : SlotStatus.Closed;

                    calendarItems.Add(new FacilityBookingSlotDto
                    {
                        SlotId = slot?.Id ?? Guid.Empty,
                        StartTime = time,
                        EndTime = slotEnd,
                        StartDate = currentDate,
                        EndDate = currentDate,
                        Status = status
                    });
                }
            }

            var result = new FacilityBookingCalendarDto
            {
                FacilityId = facility.Id,
                Name = facility.Name,
                Address = facility.Address,
                ImageUrl = facility.ImageUrl,
                Calendar = calendarItems
            };

            return new ApiResponse<FacilityBookingCalendarDto>(result);
        }

        public async Task<ApiResponse<FacilityBookingSlotDto>> GetCalendarSlotDetailAsync(Guid slotId, DateTime date)
        {
            var slot = await _unitOfWork.FacilityTimeSlotRepository.GetByIdAsync(slotId);
            if (slot == null)
                return new ApiResponse<FacilityBookingSlotDto>("Slot not found.");

            var booking = await _unitOfWork.BookingRepository.GetBookingBySlotAndDateAsync(slotId, date);
            var price = await _unitOfWork.FacilityPriceRepository.GetByTimeSlotIdAsync(slotId);

            var status = booking != null ? SlotStatus.Booked : SlotStatus.Available;

            var dto = new FacilityBookingSlotDto
            {
                SlotId = slot.Id,
                StartTime = slot.StartTime,
                EndTime = slot.EndTime,
                StartDate = date,
                EndDate = date,
                Status = status,
                Note = booking?.Note ?? string.Empty,
                FinalPrice = price?.FinalPrice ?? 0,
            };

            return new ApiResponse<FacilityBookingSlotDto>(dto);
        }

        public async Task<ApiResponse<string>> UpdateCalendarSlotStatusAsync(Guid slotId, SlotStatus newStatus)
        {
            var slot = await _unitOfWork.FacilityTimeSlotRepository.GetByIdAsync(slotId);
            if (slot == null)
                return new ApiResponse<string>("Slot not found.");

            slot.Status = newStatus;

            await _unitOfWork.FacilityTimeSlotRepository.UpdateAsync(slot);
            await _unitOfWork.CompleteAsync();

            return new ApiResponse<string>($"Slot status updated to {newStatus}.");
        }
    }
}
