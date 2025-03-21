using AutoMapper;
using SFMSSolution.Application.DataTransferObjects.Event.Request;
using SFMSSolution.Application.DataTransferObjects.Event;
using SFMSSolution.Domain.Entities;
using SFMSSolution.Infrastructure.Implements.UnitOfWorks;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

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

        public async Task<List<EventDto>> GetAllEventsAsync()
        {
            var events = await _unitOfWork.EventRepository.GetAllAsync();
            return _mapper.Map<List<EventDto>>(events);
        }

        public async Task<bool> CreateEventAsync(EventCreateRequestDto request)
        {
            var ev = _mapper.Map<Event>(request);

            // Thêm event vào DB (không gọi SaveChangesAsync tại đây)
            await _unitOfWork.EventRepository.AddAsync(ev);

            // Gọi SaveChangesAsync từ UnitOfWork
            await _unitOfWork.CompleteAsync();

            return true;
        }

        public async Task<bool> UpdateEventAsync(Guid id, EventUpdateRequestDto request)
        {
            var existingEvent = await _unitOfWork.EventRepository.GetByIdAsync(id);
            if (existingEvent == null)
                return false;

            _mapper.Map(request, existingEvent);

            // Cập nhật event (không gọi SaveChangesAsync tại đây)
            await _unitOfWork.EventRepository.UpdateAsync(existingEvent);

            // Gọi SaveChangesAsync từ UnitOfWork
            await _unitOfWork.CompleteAsync();

            return true;
        }

        public async Task<bool> DeleteEventAsync(Guid id)
        {
            // Xóa event (không gọi SaveChangesAsync tại đây)
            await _unitOfWork.EventRepository.DeleteAsync(id);

            // Gọi SaveChangesAsync từ UnitOfWork
            await _unitOfWork.CompleteAsync();

            return true;
        }

        public async Task<List<EventDto>> SearchEventsByTitleAsync(string title)
        {
            var events = await _unitOfWork.EventRepository.SearchEventsByTitleAsync(title);
            return _mapper.Map<List<EventDto>>(events);
        }
    }
}
