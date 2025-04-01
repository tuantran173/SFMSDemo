using AutoMapper;
using SFMSSolution.Application.DataTransferObjects.Event.Request;
using SFMSSolution.Application.DataTransferObjects.Event;
using SFMSSolution.Domain.Entities;
using SFMSSolution.Infrastructure.Implements.UnitOfWorks;
using SFMSSolution.Response;
using SFMSSolution.Application.Extensions;

namespace SFMSSolution.Application.Services.Events
{
    public class EventService : IEventService
    {
        private readonly IUnitOfWork _unitOfWork;
        private readonly IMapper _mapper;

        public EventService(IUnitOfWork unitOfWork, IMapper mapper)
        {
            _unitOfWork = unitOfWork;
            _mapper = mapper;
        }

        public async Task<EventDto?> GetEventByIdAsync(Guid id)
        {
            var ev = await _unitOfWork.EventRepository.GetByIdAsync(id);
            return ev == null ? null : _mapper.Map<EventDto>(ev);
        }

        public async Task<(IEnumerable<EventDto> Events, int TotalCount)> GetAllEventsAsync(string? title, int pageNumber, int pageSize)
        {
            var allEvents = await _unitOfWork.EventRepository.GetAllAsync();

            if (!string.IsNullOrWhiteSpace(title))
            {
                var normalizedInput = title.Trim().ToLower().RemoveDiacritics();
                allEvents = allEvents.Where(e =>
                    e.Title != null &&
                    e.Title.Trim().ToLower().RemoveDiacritics().Contains(normalizedInput));
            }

            var totalCount = allEvents.Count();

            var pagedEvents = allEvents
                .Skip((pageNumber - 1) * pageSize)
                .Take(pageSize)
                .ToList();

            var mappedEvents = _mapper.Map<IEnumerable<EventDto>>(pagedEvents);
            return (mappedEvents, totalCount);
        }

        public async Task<(IEnumerable<EventDto> Events, int TotalCount)> GetEventsByOwnerAsync(Guid ownerId, string? title, int pageNumber, int pageSize)
        {
            var events = await _unitOfWork.EventRepository.GetAllAsync();

            var filteredEvents = events.Where(e => e.OwnerId == ownerId);

            if (!string.IsNullOrWhiteSpace(title))
            {
                var normalizedTitle = title.Trim().ToLower().RemoveDiacritics();
                filteredEvents = filteredEvents.Where(e =>
                    e.Title != null &&
                    e.Title.Trim().ToLower().RemoveDiacritics().Contains(normalizedTitle));
            }

            var totalCount = filteredEvents.Count();

            var pagedEvents = filteredEvents
                .Skip((pageNumber - 1) * pageSize)
                .Take(pageSize)
                .ToList();

            var mappedEvents = _mapper.Map<IEnumerable<EventDto>>(pagedEvents);
            return (mappedEvents, totalCount);
        }

        public async Task<ApiResponse<string>> CreateEventAsync(EventCreateRequestDto request, Guid ownerId)
        {
            var eventEntity = _mapper.Map<Event>(request);
            eventEntity.OwnerId = ownerId;

            await _unitOfWork.EventRepository.AddAsync(eventEntity);
            await _unitOfWork.CompleteAsync();
            return new ApiResponse<string>(string.Empty, "Event created successfully.");
        }

        public async Task<ApiResponse<string>> UpdateEventAsync(EventUpdateRequestDto request)
        {
            var existingEvent = await _unitOfWork.EventRepository.GetByIdAsync(request.Id);
            if (existingEvent == null)
                return new ApiResponse<string>("Event not found.");

            _mapper.Map(request, existingEvent);
            await _unitOfWork.EventRepository.UpdateAsync(existingEvent);
            await _unitOfWork.CompleteAsync();
            return new ApiResponse<string>(string.Empty, "Event updated successfully.");
        }

        public async Task<ApiResponse<string>> DeleteEventAsync(Guid id)
        {
            var existingEvent = await _unitOfWork.EventRepository.GetByIdAsync(id);
            if (existingEvent == null)
                return new ApiResponse<string>("Event not found.");

            await _unitOfWork.EventRepository.DeleteAsync(id);
            await _unitOfWork.CompleteAsync();
            return new ApiResponse<string>(string.Empty, "Event deleted successfully.");
        }
    }
}
