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
    public class EventRepository : GenericRepository<Event>, IEventRepository
    {
        public EventRepository(SFMSDbContext context) : base(context) { }

        public async Task<Event?> GetEventByIdAsync(Guid id)
        {
            return await GetByIdAsync(id);
        }

        public async Task<List<Event>> SearchEventsByTitleAsync(string title)
        {
            var lowerTitle = title.ToLower();
            return await _context.Events
                .Where(e => e.Title.ToLower().Contains(lowerTitle))
                .ToListAsync();
        }
    }
}
