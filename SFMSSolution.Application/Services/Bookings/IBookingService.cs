using SFMSSolution.Application.DataTransferObjects.Booking;
using SFMSSolution.Application.DataTransferObjects.Booking.Request;
using SFMSSolution.Domain.Enums;
using SFMSSolution.Response;

namespace SFMSSolution.Application.Services.Bookings
{
    public interface IBookingService
    {
        Task<BookingDto> GetBookingAsync(Guid id);

        Task<(IEnumerable<BookingDto> Bookings, int TotalCount)> GetAllBookingsAsync(string? name, int pageNumber, int pageSize);

        Task<ApiResponse<string>> CreateBookingAsync(BookingCreateRequestDto request, Guid userId);

        Task<bool> UpdateBookingAsync(Guid id, BookingUpdateRequestDto request);

        Task<bool> DeleteBookingAsync(Guid id);
        Task<IEnumerable<BookingDto>> GetBookingsByUserAsync(Guid userId);
        Task<bool> UpdateBookingStatusAsync(Guid bookingId, BookingStatusUpdateRequestDto request);
        Task<IEnumerable<BookingDto>> GetBookingHistoryForUserAsync(Guid userId);
        Task<ApiResponse<FacilityBookingCalendarDto>> GetFacilityCalendarAsync(Guid facilityId, Guid? userId = null);
        Task<ApiResponse<SlotDetailDto>> GetCalendarSlotDetailAsync(Guid slotId, DateTime date, TimeSpan startTime, TimeSpan endTime);
        Task<ApiResponse<string>> UpdateCalendarSlotDetailAsync(UpdateSlotDetailRequestDto request);
        Task<ApiResponse<FacilityBookingCalendarDto>> GetCalendarForCustomerAsync(Guid facilityId, Guid? userId);
        Task<ApiResponse<FacilityBookingCalendarDto>> GetCalendarForGuestAsync(Guid facilityId);

        }
}
