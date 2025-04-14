using SFMSSolution.Application.DataTransferObjects.Booking;
using SFMSSolution.Application.DataTransferObjects.Booking.Request;
using SFMSSolution.Domain.Enums;
using SFMSSolution.Response;

namespace SFMSSolution.Application.Services.Bookings
{
    public interface IBookingService
    {
        Task<ApiResponse<BookingDto>> GetBookingDetailAsync(Guid bookingId);

        Task<(IEnumerable<BookingDto> Bookings, int TotalCount)> GetAllBookingsAsync(string? name, int pageNumber, int pageSize);

        Task<ApiResponse<string>> CreateBookingAsync(BookingCreateRequestDto request, Guid userId);
        //Task<ApiResponse<string>> ConfirmBookingAsync(Guid bookingId);
        Task<bool> UpdateBookingAsync(Guid id, BookingUpdateRequestDto request);

        Task<bool> DeleteBookingAsync(Guid id);
        Task<IEnumerable<BookingDto>> GetBookingsByUserAsync(Guid userId);
        Task<bool> UpdateBookingStatusAsync(Guid bookingId, BookingStatusUpdateRequestDto request);
        Task<ApiResponse<FacilityBookingCalendarDto>> GetFacilityCalendarAsync(Guid facilityId, Guid? userId = null);
        Task<ApiResponse<SlotDetailDto>> GetCalendarSlotDetailAsync(Guid slotId, DateTime date, TimeSpan startTime, TimeSpan endTime);
        Task<ApiResponse<FacilityBookingCalendarDto>> GetCalendarForCustomerAsync(Guid facilityId, Guid? userId);
        Task<ApiResponse<FacilityBookingCalendarDto>> GetCalendarForGuestAsync(Guid facilityId);
        Task<ApiResponse<string>> UpdateSlotStatusByOwnerAsync(Guid slotId, DateTime date, TimeSpan startTime, TimeSpan endTime, SlotStatus status);
        Task<ApiResponse<string>> CancelBookingByCustomerAsync(Guid bookingId, Guid userId);
        }
}
