using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using SFMSSolution.Application.DataTransferObjects.Booking.Request;
using SFMSSolution.Application.Services.Bookings;

namespace SFMSSolution.API.Controllers
{
    [ApiController]
    [Route("api/booking")]
    public class BookingCalendarController : ControllerBase
    {
        private readonly IBookingService _bookingService;

        public BookingCalendarController(IBookingService bookingService)
        {
            _bookingService = bookingService;
        }

        [HttpGet("calendar/{facilityId:Guid}")]
        [Authorize(Policy = "Owner")]
        public async Task<IActionResult> GetCalendar(Guid facilityId)
        {
            var result = await _bookingService.GetFacilityCalendarAsync(facilityId);
            if (!result.Success)
                return NotFound(result.Message);
            return Ok(result.Data);
        }

        [HttpGet("calendar/customer/{facilityId}")]
        [Authorize]
        public async Task<IActionResult> GetCalendarForCustomer(Guid facilityId)
        {
            var userIdClaim = User.Claims.FirstOrDefault(c => c.Type == "sub" || c.Type == "id");
            Guid? userId = null;
            if (userIdClaim != null && Guid.TryParse(userIdClaim.Value, out var parsedId))
            {
                userId = parsedId;
            }

            var result = await _bookingService.GetCalendarForCustomerAsync(facilityId, userId);
            return Ok(result);
        }

        [HttpGet("calendar/guest")]
        public async Task<IActionResult> GetGuestCalendar(Guid facilityId)
        {
            var result = await _bookingService.GetCalendarForGuestAsync(facilityId);
            return Ok(result);
        }

        [HttpGet("calendar/slot-detail")]
        [Authorize]
        public async Task<IActionResult> GetCalendarSlotDetail(Guid slotId, DateTime date, TimeSpan startTime, TimeSpan endTime)
        {
            var result = await _bookingService.GetCalendarSlotDetailAsync(slotId, date, startTime, endTime);
            if (!result.Success)
                return BadRequest(result);
            return Ok(result);
        }

        [HttpPut("calendar/update-slot-detail")]
        [Authorize(Policy = "Owner")]
        public async Task<IActionResult> UpdateSlotDetail([FromBody] UpdateSlotDetailRequestDto request)
        {
            var result = await _bookingService.UpdateCalendarSlotDetailAsync(request);
            if (!result.Success)
                return BadRequest(result);
            return Ok(result);
        }
    }
}
