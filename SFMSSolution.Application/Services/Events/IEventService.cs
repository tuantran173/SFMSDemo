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
        Task<(IEnumerable<EventDto> Events, int TotalCount)> GetAllEventsAsync(string? title, int pageNumber, int pageSize);
        Task<(IEnumerable<EventDto> Events, int TotalCount)> GetEventsByOwnerAsync(Guid ownerId, string? title, int pageNumber, int pageSize);
        Task<bool> CreateEventAsync(EventCreateRequestDto request, Guid ownerId);
        Task<bool> UpdateEventAsync(EventUpdateRequestDto request);
        Task<bool> DeleteEventAsync(Guid id);
    }
}
