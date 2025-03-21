using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using SFMSSolution.Application.DataTransferObjects.Event.Request;
using SFMSSolution.Application.Services.Events;

namespace SFMSSolution.API.Controllers
{
    [Authorize(Roles = "Owner")]
    [ApiController]
    [Route("api/[controller]")]
    public class EventController : ControllerBase
    {
        private readonly IEventService _eventService;

        public EventController(IEventService eventService)
        {
            _eventService = eventService;
        }

        [HttpGet("{id:int}")]
        public async Task<IActionResult> GetEvent(Guid id)
        {
            var ev = await _eventService.GetEventByIdAsync(id);
            if (ev == null)
                return NotFound();
            return Ok(ev);
        }

        [AllowAnonymous]
        [HttpGet]
        public async Task<IActionResult> GetAllEvents()
        {
            var events = await _eventService.GetAllEventsAsync();
            return Ok(events);
        }

        [HttpPost]
        public async Task<IActionResult> CreateEvent([FromBody] EventCreateRequestDto request)
        {
            if (!ModelState.IsValid)
                return BadRequest(ModelState);

            var result = await _eventService.CreateEventAsync(request);
            if (!result)
                return BadRequest(new { message = "Failed to create event." });
            return Ok(new { message = "Event created successfully." });
        }

        [HttpPut("{id:int}")]
        public async Task<IActionResult> UpdateEvent(Guid id, [FromBody] EventUpdateRequestDto request)
        {
            if (!ModelState.IsValid)
                return BadRequest(ModelState);

            var result = await _eventService.UpdateEventAsync(id, request);
            if (!result)
                return BadRequest(new { message = "Failed to update event." });
            return Ok(new { message = "Event updated successfully." });
        }

        [AllowAnonymous]
        [HttpDelete("{id:int}")]
        public async Task<IActionResult> DeleteEvent(Guid id)
        {
            var result = await _eventService.DeleteEventAsync(id);
            if (!result)
                return BadRequest(new { message = "Failed to delete event." });
            return Ok(new { message = "Event deleted successfully." });
        }

        [HttpGet("search/{title}")]
        public async Task<IActionResult> SearchEvents(string title)
        {
            var events = await _eventService.SearchEventsByTitleAsync(title);
            return Ok(events);
        }
    }
}
