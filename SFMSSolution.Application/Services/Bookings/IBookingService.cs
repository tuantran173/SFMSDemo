using SFMSSolution.Application.DataTransferObjects.Booking.Request;
using SFMSSolution.Application.DataTransferObjects.Booking;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using SFMSSolution.Response;

namespace SFMSSolution.Application.Services.Bookings
{
    public interface IBookingService
    {
        Task<BookingDto> GetBookingAsync(Guid id);

        Task<(IEnumerable<BookingDto> Bookings, int TotalCount)> GetAllBookingsAsync(string? name, int pageNumber, int pageSize);

        Task<bool> CreateBookingAsync(BookingCreateRequestDto request);

        Task<bool> UpdateBookingAsync(Guid id, BookingUpdateRequestDto request);

        Task<bool> DeleteBookingAsync(Guid id);
        Task<IEnumerable<BookingDto>> GetBookingsByUserAsync(Guid userId);
        Task<bool> UpdateBookingStatusAsync(Guid bookingId, BookingStatusUpdateRequestDto request);
        Task<IEnumerable<BookingDto>> GetBookingHistoryForUserAsync(Guid userId);
        Task<ApiResponse<FacilityBookingCalendarDto>> GetFacilityCalendarAsync(Guid facilityId);
        Task<ApiResponse<FacilityBookingSlotDto>> GetCalendarSlotDetailAsync(Guid slotId, DateTime date, TimeSpan startTime, TimeSpan endTime);
        Task<ApiResponse<string>> UpdateCalendarSlotAsync(Guid slotId, DateTime newStartDate, DateTime newEndDate);
        Task<ApiResponse<string>> GenerateDefaultSlotsAsync(Guid facilityId, DateTime fromDate, DateTime toDate);
    }
}
