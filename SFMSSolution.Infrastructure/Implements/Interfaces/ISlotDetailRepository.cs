﻿using SFMS.Infrastructure.Repositories;
using SFMSSolution.Domain.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SFMSSolution.Infrastructure.Implements.Interfaces
{
    public interface ISlotDetailRepository : IGenericRepository<SlotDetail>
    {
        Task<SlotDetail?> GetBySlotAndTimeAsync(Guid slotId, DateTime date, TimeSpan startTime, TimeSpan endTime);
    }

}
