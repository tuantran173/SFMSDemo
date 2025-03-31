using AutoMapper;
using SFMSSolution.Application.DataTransferObjects.Event.Request;
using SFMSSolution.Application.DataTransferObjects.Event;
using SFMSSolution.Domain.Entities;
using SFMSSolution.Infrastructure.Implements.UnitOfWorks;

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
                allEvents = allEvents.Where(e => e.Title.Contains(title, StringComparison.OrdinalIgnoreCase));
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
            var allEvents = await _unitOfWork.EventRepository.GetAllAsync();

            var ownerEvents = allEvents.Where(e => e.OwnerId == ownerId);

            if (!string.IsNullOrWhiteSpace(title))
            {
                ownerEvents = ownerEvents.Where(e => e.Title.Contains(title, StringComparison.OrdinalIgnoreCase));
            }

            var totalCount = ownerEvents.Count();

            var pagedEvents = ownerEvents
                .Skip((pageNumber - 1) * pageSize)
                .Take(pageSize)
                .ToList();

            var mappedEvents = _mapper.Map<IEnumerable<EventDto>>(pagedEvents);
            return (mappedEvents, totalCount);
        }

        public async Task<bool> CreateEventAsync(EventCreateRequestDto request)
        {
            var ev = _mapper.Map<Event>(request);

            await _unitOfWork.EventRepository.AddAsync(ev);
            await _unitOfWork.CompleteAsync();

            return true;
        }

        public async Task<bool> UpdateEventAsync(EventUpdateRequestDto request)
        {
            var existingEvent = await _unitOfWork.EventRepository.GetByIdAsync(request.Id);
            if (existingEvent == null)
                return false;

            _mapper.Map(request, existingEvent);
            await _unitOfWork.EventRepository.UpdateAsync(existingEvent);
            await _unitOfWork.CompleteAsync();

            return true;
        }

        public async Task<bool> DeleteEventAsync(Guid id)
        {
            await _unitOfWork.EventRepository.DeleteAsync(id);
            await _unitOfWork.CompleteAsync();
            return true;
        }
    }
}
