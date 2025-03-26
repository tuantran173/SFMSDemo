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

        [AllowAnonymous]
        [HttpGet("get-event-by-id/{id:Guid}")]
        public async Task<IActionResult> GetEvent(Guid id)
        {
            var ev = await _eventService.GetEventByIdAsync(id);
            if (ev == null)
                return NotFound(new { message = "Event not found." });

            return Ok(ev);
        }

        [AllowAnonymous]
        [HttpGet("get-all-events")]
        public async Task<IActionResult> GetAllEvents([FromQuery] int pageNumber = 1, [FromQuery] int pageSize = 10)
        {
            var (events, totalCount) = await _eventService.GetAllEventsAsync(pageNumber, pageSize);
            return Ok(new { TotalCount = totalCount, Events = events });
        }

        [HttpPost("create-event")]
        public async Task<IActionResult> CreateEvent([FromBody] EventCreateRequestDto request)
        {
            if (!ModelState.IsValid)
                return BadRequest(ModelState);

            var result = await _eventService.CreateEventAsync(request);
            if (!result)
                return BadRequest(new { message = "Failed to create event." });

            return Ok(new { message = "Event created successfully." });
        }

        [HttpPut("update-event/{id:Guid}")]
        public async Task<IActionResult> UpdateEvent(Guid id, [FromBody] EventUpdateRequestDto request)
        {
            if (!ModelState.IsValid)
                return BadRequest(ModelState);

            var result = await _eventService.UpdateEventAsync(id, request);
            if (!result)
                return BadRequest(new { message = "Failed to update event." });

            return Ok(new { message = "Event updated successfully." });
        }

        [HttpDelete("delete/event{id:Guid}")]
        public async Task<IActionResult> DeleteEvent(Guid id)
        {
            var result = await _eventService.DeleteEventAsync(id);
            if (!result)
                return BadRequest(new { message = "Failed to delete event." });

            return Ok(new { message = "Event deleted successfully." });
        }

        [AllowAnonymous]
        [HttpGet("search-event")]
        public async Task<IActionResult> SearchEvents(
            [FromQuery] string title,
            [FromQuery] int pageNumber = 1,
            [FromQuery] int pageSize = 10)
        {
            var (events, totalCount) = await _eventService.SearchEventsByTitleAsync(title, pageNumber, pageSize);
            return Ok(new { TotalCount = totalCount, Events = events });
        }
    }
}
