using Microsoft.EntityFrameworkCore;
using SFMS.Infrastructure.Repositories;
using SFMSSolution.Domain.Entities;
using SFMSSolution.Infrastructure.Database.AppDbContext;
using SFMSSolution.Infrastructure.Implements.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SFMSSolution.Infrastructure.Implements.Repositories
{
    public class BookingRepository : GenericRepository<Booking>, IBookingRepository
    {
        public BookingRepository(SFMSDbContext context) : base(context) { }

        public async Task<Booking?> GetBookingByIdWithDetailsAsync(Guid id)
        {
            return await _dbSet
                .Include(b => b.Facility)
                .Include(b => b.User)
                .FirstOrDefaultAsync(b => b.Id == id);
        }

        public async Task<(IEnumerable<Booking> Bookings, int TotalCount)> GetAllBookingsWithDetailsAsync(int pageNumber, int pageSize)
        {
            var query = _dbSet
                .Include(b => b.Facility)
                .Include(b => b.User);

            var totalCount = await query.CountAsync();

            var bookings = await query
                .Skip((pageNumber - 1) * pageSize)
                .Take(pageSize)
                .ToListAsync();

            return (bookings, totalCount);
        }
    }
}
