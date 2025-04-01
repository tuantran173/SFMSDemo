using SFMSSolution.Application.DataTransferObjects.Event;
using SFMSSolution.Application.DataTransferObjects.Event.Request;
using SFMSSolution.Response;
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
        Task<ApiResponse<string>> CreateEventAsync(EventCreateRequestDto request, Guid ownerId);
        Task<ApiResponse<string>> UpdateEventAsync(EventUpdateRequestDto request);
        Task<ApiResponse<string>> DeleteEventAsync(Guid id);
    }
}
