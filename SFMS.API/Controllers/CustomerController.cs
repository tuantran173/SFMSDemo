using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using SFMSSolution.Application.DataTransferObjects.Booking.Request;
using SFMSSolution.Application.Services.Bookings;
using SFMSSolution.Application.Services.Facilities;
using System.Security.Claims;

namespace SFMSSolution.API.Controllers
{
    [Authorize(Roles = "Customer")]
    [ApiController]
    [Route("api/customer")]
    public class CustomerController : ControllerBase
    {
        private readonly IBookingService _bookingService;
        private readonly IFacilityService _facilityService;

        public CustomerController(IBookingService bookingService, IFacilityService facilityService)
        {
            _bookingService = bookingService;
            _facilityService = facilityService;
        }

        private Guid GetUserId()
        {
            var userIdClaim = User.FindFirst(ClaimTypes.NameIdentifier)?.Value;
            if (Guid.TryParse(userIdClaim, out var userId))
                return userId;

            throw new UnauthorizedAccessException("User ID is invalid.");
        }

        [AllowAnonymous]
        [HttpGet("facilities")]
        public async Task<IActionResult> GetAllFacilities([FromQuery] int pageNumber = 1, [FromQuery] int pageSize = 10)
        {
            var (facilities, totalCount) = await _facilityService.GetAllFacilitiesAsync(pageNumber, pageSize);
            return Ok(new { TotalCount = totalCount, Facilities = facilities });
        }

        [AllowAnonymous]
        [HttpGet("facilities/{id:Guid}")]
        public async Task<IActionResult> GetFacility(Guid id)
        {
            var facility = await _facilityService.GetFacilityAsync(id);
            if (facility == null)
                return NotFound(new { message = "Facility not found." });

            return Ok(facility);
        }

        [AllowAnonymous]
        [HttpGet("filter")]
        public async Task<IActionResult> FilterFacilities(
            [FromQuery] Guid? categoryId,
            [FromQuery] string? location,
            [FromQuery] string? startTime,
            [FromQuery] string? endTime,
            [FromQuery] int pageNumber = 1,
            [FromQuery] int pageSize = 10)
        {
            TimeSpan? parsedStartTime = null;
            TimeSpan? parsedEndTime = null;

            if (!string.IsNullOrWhiteSpace(startTime) && TimeSpan.TryParse(startTime, out var sTime))
                parsedStartTime = sTime;

            if (!string.IsNullOrWhiteSpace(endTime) && TimeSpan.TryParse(endTime, out var eTime))
                parsedEndTime = eTime;

            var (facilities, totalCount) = await _facilityService.FilterFacilitiesAsync(
                categoryId, location, parsedStartTime, parsedEndTime, pageNumber, pageSize);

            return Ok(new { TotalCount = totalCount, Facilities = facilities });
        }

        [HttpGet("all")]
        public async Task<IActionResult> GetAllBookings([FromQuery] int pageNumber = 1, [FromQuery] int pageSize = 10)
        {
            var (bookings, totalCount) = await _bookingService.GetAllBookingsAsync(pageNumber, pageSize);
            return Ok(new { TotalCount = totalCount, Bookings = bookings });
        }

        [HttpPost("bookings")]
        public async Task<IActionResult> CreateBooking([FromBody] BookingCreateRequestDto request)
        {
            var userId = GetUserId();
            request.UserId = userId;

            var result = await _bookingService.CreateBookingAsync(request);
            if (!result)
                return BadRequest(new { message = "Failed to create booking." });

            return Ok(new { message = "Booking created successfully." });
        }

        [HttpPut("bookings/{id:Guid}")]
        public async Task<IActionResult> UpdateBooking(Guid id, [FromBody] BookingUpdateRequestDto request)
        {
            var result = await _bookingService.UpdateBookingAsync(id, request);
            if (!result)
                return NotFound(new { message = "Booking not found or update failed." });

            return Ok(new { message = "Booking updated successfully." });
        }

        [HttpDelete("bookings/{id:Guid}")]
        public async Task<IActionResult> DeleteBooking(Guid id)
        {
            var result = await _bookingService.DeleteBookingAsync(id);
            if (!result)
                return NotFound(new { message = "Booking not found or delete failed." });

            return Ok(new { message = "Booking deleted successfully." });
        }
    }
}
