using SFMSSolution.Application.DataTransferObjects.Booking.Request;
using SFMSSolution.Application.DataTransferObjects.Booking;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SFMSSolution.Application.Services.Bookings
{
    public interface IBookingService
    {
        Task<BookingDto> GetBookingAsync(Guid id);
        Task<IEnumerable<BookingDto>> GetAllBookingsAsync();
        Task<bool> CreateBookingAsync(BookingCreateRequestDto request);
        Task<bool> UpdateBookingAsync(Guid id, BookingUpdateRequestDto request);
        Task<bool> DeleteBookingAsync(Guid id);
    }
}
