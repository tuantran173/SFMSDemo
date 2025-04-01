using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using SFMSSolution.Application.DataTransferObjects.Event.Request;
using SFMSSolution.Application.Services.Events;
using System.Security.Claims;

namespace SFMSSolution.API.Controllers
{
    [ApiController]
    [Route("api/event")]
    public class EventController : ControllerBase
    {
        private readonly IEventService _eventService;

        public EventController(IEventService eventService)
        {
            _eventService = eventService;
        }

        [AllowAnonymous]
        [HttpGet("get-event-by-id/{id:Guid}")]
        public async Task<IActionResult> GetById(Guid id)
        {
            var result = await _eventService.GetEventByIdAsync(id);
            return result == null ? NotFound("Event not found.") : Ok(result);
        }

        [AllowAnonymous]
        [HttpGet("get-all-events")]
        public async Task<IActionResult> GetAll([FromQuery] string? title, [FromQuery] int pageNumber = 1, [FromQuery] int pageSize = 10)
        {
            var (events, total) = await _eventService.GetAllEventsAsync(title, pageNumber, pageSize);
            return Ok(new { Data = events, TotalCount = total, CurrentPage = pageNumber, PageSize = pageSize });
        }

        [Authorize(Policy = "Owner")]
        [HttpGet("get-events-by-owner")]
        public async Task<IActionResult> GetByOwner([FromQuery] string? title, [FromQuery] int pageNumber = 1, [FromQuery] int pageSize = 10)
        {
            var ownerIdStr = User.FindFirstValue("sub");
            if (!Guid.TryParse(ownerIdStr, out var ownerId))
                return Unauthorized("Invalid token");

            var (events, total) = await _eventService.GetEventsByOwnerAsync(ownerId, title, pageNumber, pageSize);
            return Ok(new { Data = events, TotalCount = total, CurrentPage = pageNumber, PageSize = pageSize });
        }

        [Authorize(Policy = "Owner")]
        [HttpPost("create-event")]
        public async Task<IActionResult> CreateEvent([FromBody] EventCreateRequestDto request)
        {
            var ownerIdStr = User.FindFirstValue("sub");
            if (!Guid.TryParse(ownerIdStr, out var ownerId))
                return Unauthorized("Invalid owner ID.");

            var result = await _eventService.CreateEventAsync(request, ownerId);
            return result.Success ? Ok(new { Message = result.Message }) : BadRequest(result.Message);
        }

        [Authorize(Policy = "Owner")]
        [HttpPut("update-event")]
        public async Task<IActionResult> Update([FromBody] EventUpdateRequestDto request)
        {
            var result = await _eventService.UpdateEventAsync(request);
            return result.Success ? Ok(new { Message = result.Message }) : NotFound(result.Message);
        }

        [Authorize(Policy = "Owner")]
        [HttpDelete("delete-event/{id:Guid}")]
        public async Task<IActionResult> Delete(Guid id)
        {
            var result = await _eventService.DeleteEventAsync(id);
            return result.Success ? Ok(new { Message = result.Message }) : NotFound(result.Message);
        }
    }
}
