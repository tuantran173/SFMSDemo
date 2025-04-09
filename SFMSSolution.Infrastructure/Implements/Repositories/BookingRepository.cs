using Microsoft.EntityFrameworkCore;
using SFMS.Infrastructure.Repositories;
using SFMSSolution.Domain.Entities;
using SFMSSolution.Domain.Enums;
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
            return await _context.Bookings
                .Include(b => b.Facility)
                .Include(b => b.User)
                .Include(b => b.FacilityTimeSlot)
                .FirstOrDefaultAsync(b => b.Id == id);
        }

        public async Task<(IEnumerable<Booking> Bookings, int TotalCount)> GetAllBookingsWithDetailsAsync(string? name, int pageNumber, int pageSize)
        {
            var query = _context.Bookings
                .Include(b => b.Facility)
                .Include(b => b.User)
                .Include(b => b.FacilityTimeSlot)
                .AsQueryable();

            if (!string.IsNullOrWhiteSpace(name))
            {
                query = query.Where(b => b.Facility.Name.Contains(name, StringComparison.OrdinalIgnoreCase));
            }

            var totalCount = await query.CountAsync();

            var bookings = await query
                .OrderByDescending(b => b.CreatedDate)
                .Skip((pageNumber - 1) * pageSize)
                .Take(pageSize)
                .ToListAsync();

            return (bookings, totalCount);
        }

        public async Task<List<Booking>> GetBookingsByUserAsync(Guid userId)
        {
            return await _context.Bookings
                .Include(b => b.Facility)
                .Include(b => b.FacilityTimeSlot)
                .Where(b => b.UserId == userId)
                .ToListAsync();
        }

        public async Task<List<Booking>> GetBookingsByFacilityAsync(Guid facilityId, DateTime fromDate, DateTime toDate)
        {
            return await _context.Bookings
                .Include(b => b.FacilityTimeSlot)
                .Include(b => b.User)
                .Where(b =>
                    b.FacilityId == facilityId &&
                    b.BookingDate >= fromDate.Date &&
                    b.BookingDate <= toDate.Date)
                .ToListAsync();
        }

        public async Task<bool> IsTimeSlotBooked(Guid facilityTimeSlotId, DateTime date, TimeSpan startTime, TimeSpan endTime)
        {
            return await _context.Bookings.AnyAsync(b =>
                b.FacilityTimeSlotId == facilityTimeSlotId &&
                b.BookingDate.Date == date.Date &&
                b.StartTime == startTime &&
                b.EndTime == endTime);
        }


        public async Task<Booking?> GetBookingBySlotAndDateAsync(Guid slotId, DateTime date)
        {
            return await _context.Bookings
                .Include(b => b.FacilityTimeSlot)
                .FirstOrDefaultAsync(b => b.FacilityTimeSlotId == slotId && b.BookingDate.Date == date.Date);
        }

        public async Task<Booking?> GetBookingBySlotAndDateAndTimeAsync(Guid slotId, DateTime date, TimeSpan startTime, TimeSpan endTime)
        {
            return await _context.Bookings
                .FirstOrDefaultAsync(b =>
                    b.FacilityTimeSlotId == slotId &&
                    b.BookingDate.Date == date.Date &&
                    b.StartTime == startTime &&
                    b.EndTime == endTime);
        }

    }
}

