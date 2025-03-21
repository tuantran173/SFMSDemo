using SFMSSolution.Application.DataTransferObjects.Event;
using SFMSSolution.Application.DataTransferObjects.Event.Request;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SFMSSolution.Application.Services.Events
{
    public interface IEventService
    {
        Task<EventDto?> GetEventByIdAsync(Guid id);
        Task<List<EventDto>> GetAllEventsAsync();
        Task<bool> CreateEventAsync(EventCreateRequestDto request);
        Task<bool> UpdateEventAsync(Guid id, EventUpdateRequestDto request);
        Task<bool> DeleteEventAsync(Guid id);
        Task<List<EventDto>> SearchEventsByTitleAsync(string title);
    }
}
