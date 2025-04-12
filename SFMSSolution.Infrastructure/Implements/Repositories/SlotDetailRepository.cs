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
    public class SlotDetailRepository : GenericRepository<SlotDetail>, ISlotDetailRepository
    {
        private readonly SFMSDbContext _context;

        public SlotDetailRepository(SFMSDbContext context) : base(context)
        {
            _context = context;
        }

        public async Task<SlotDetail?> GetBySlotAndTimeAsync(Guid slotId, DateTime date, TimeSpan startTime, TimeSpan endTime)
        {
            return await _context.SlotDetails
                .FirstOrDefaultAsync(x =>
                    x.SlotId == slotId &&
                    x.Date.Date == date.Date &&
                    x.StartTime == startTime &&
                    x.EndTime == endTime);
        }

    }
}
