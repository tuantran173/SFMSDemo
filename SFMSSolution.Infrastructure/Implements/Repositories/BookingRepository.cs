using Microsoft.EntityFrameworkCore;
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
    public class BookingRepository : IBookingRepository
    {
        private readonly SFMSDbContext _context;

        public BookingRepository(SFMSDbContext context)
        {
            _context = context;
        }

        public async Task<Booking> GetByIdAsync(Guid id)
        {
            return await _context.Bookings
                .Include(b => b.Facility)
                .Include(b => b.User)
                .FirstOrDefaultAsync(b => b.Id == id);
        }

        public async Task<IEnumerable<Booking>> GetAllAsync()
        {
            return await _context.Bookings
                .Include(b => b.Facility)
                .Include(b => b.User)
                .ToListAsync();
        }

        public async Task<bool> AddAsync(Booking booking)
        {
            _context.Bookings.Add(booking);
            return await _context.SaveChangesAsync() > 0;
        }

        public async Task<bool> UpdateAsync(Booking booking)
        {
            _context.Bookings.Update(booking);
            return await _context.SaveChangesAsync() > 0;
        }

        public async Task<bool> DeleteAsync(Guid id)
        {
            var booking = await _context.Bookings.FirstOrDefaultAsync(b => b.Id == id);
            if (booking == null)
                return false;
            _context.Bookings.Remove(booking);
            return await _context.SaveChangesAsync() > 0;
        }
    }
}
