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
        Task<(IEnumerable<Booking> Bookings, int TotalCount)> GetAllBookingsWithDetailsAsync(int pageNumber, int pageSize);
    }
}