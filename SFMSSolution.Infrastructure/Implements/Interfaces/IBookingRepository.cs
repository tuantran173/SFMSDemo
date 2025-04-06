using SFMS.Infrastructure.Repositories;
using SFMSSolution.Domain.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SFMSSolution.Infrastructure.Implements.Interfaces
{
    public interface IBookingRepository : IGenericRepository<Booking>
    {
        Task<Booking?> GetBookingByIdWithDetailsAsync(Guid id);

        Task<(IEnumerable<Booking> Bookings, int TotalCount)> GetAllBookingsWithDetailsAsync(string? name, int pageNumber, int pageSize);

        Task<List<Booking>> GetBookingsByUserAsync(Guid userId);

        Task<List<Booking>> GetBookingsByFacilityAsync(Guid facilityId, DateTime date);

        Task<bool> IsTimeSlotBooked(Guid facilityTimeSlotId, DateTime date, TimeSpan startTime, TimeSpan endTime);
        Task<Booking?> GetBookingBySlotAndDateAsync(Guid slotId, DateTime date);
        Task<Booking?> GetBookingBySlotAndDateAndTimeAsync(Guid slotId, DateTime date, TimeSpan startTime, TimeSpan endTime);

    }
}