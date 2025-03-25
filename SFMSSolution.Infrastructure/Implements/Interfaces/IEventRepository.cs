using SFMS.Infrastructure.Repositories;
using SFMSSolution.Domain.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SFMSSolution.Infrastructure.Implements.Interfaces
{
    public interface IEventRepository : IGenericRepository<Event>
    {
        Task<Event?> GetEventByIdAsync(Guid id);
        Task<List<Event>> SearchEventsByTitleAsync(string title, int pageNumber = 1, int pageSize = 10);
        Task<int> CountEventsByTitleAsync(string title);
    }
}
