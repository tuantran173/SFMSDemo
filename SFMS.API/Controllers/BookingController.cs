using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using SFMSSolution.Application.DataTransferObjects.Booking;
using SFMSSolution.Application.DataTransferObjects.Booking.Request;
using SFMSSolution.Application.Services.Bookings;
using SFMSSolution.Response;

namespace SFMSSolution.API.Controllers
{
    [ApiController]
    [Route("api/booking")]
    public class BookingController : ControllerBase
    {
        private readonly IBookingService _bookingService;

        public BookingController(IBookingService bookingService)
        {
            _bookingService = bookingService;
        }

        [HttpGet("{id:Guid}/get-booking-by-id")]
        [Authorize]
        public async Task<IActionResult> GetBooking(Guid id)
        {
            var booking = await _bookingService.GetBookingDetailAsync(id);
            if (booking == null) return NotFound("Booking not found.");
            return Ok(booking);
        }
        [HttpGet("user/{userId}")]
        [Authorize]
        public async Task<IActionResult> GetBookingsByUser(Guid userId)
        {
            var result = await _bookingService.GetBookingsByUserAsync(userId);
            return Ok(new ApiResponse<IEnumerable<BookingDto>>(result));
        }
        [HttpGet("get-all-bookings")]
        [Authorize]
        public async Task<IActionResult> GetAllBookings(
            [FromQuery] string? name,
            [FromQuery] int pageNumber = 1,
            [FromQuery] int pageSize = 10)
        {
            var (bookings, totalCount) = await _bookingService.GetAllBookingsAsync(name, pageNumber, pageSize);

            return Ok(new
            {
                Data = bookings,
                TotalCount = totalCount,
                CurrentPage = pageNumber,
                PageSize = pageSize
            });
        }

        [HttpPost("create")]
        [Authorize]
        public async Task<IActionResult> CreateBooking([FromBody] BookingCreateRequestDto request)
        {
            // Lấy userId từ token
            var userIdClaim = User.Claims.FirstOrDefault(c => c.Type == "sub" || c.Type == "id");
            Guid userId = Guid.Empty;

            if (userIdClaim != null && Guid.TryParse(userIdClaim.Value, out var parsedId))
            {
                userId = parsedId;
            }

            var result = await _bookingService.CreateBookingAsync(request, userId);

            if (!result.Success)
                return BadRequest(result);

            return Ok(result);
        }

        [HttpPut("update-booking")]
        [Authorize]
        public async Task<IActionResult> UpdateBooking([FromBody] BookingUpdateRequestDto request)
        {
            var result = await _bookingService.UpdateBookingAsync(request.Id, request);
            return result ? Ok("Booking updated successfully.") : NotFound("Booking not found or update failed.");
        }

        [Authorize]
        [HttpPost("cancel-booking")]
        public async Task<IActionResult> CancelBookingBySlot([FromBody] CancelBookingRequestDto request)
        {
            var userIdClaim = User.Claims.FirstOrDefault(c => c.Type == "sub" || c.Type == "id");
            if (userIdClaim == null || !Guid.TryParse(userIdClaim.Value, out var userId))
            {
                return Unauthorized("User is not authenticated.");
            }

            var result = await _bookingService.CancelBookingBySlotAsync(request, userId);
            if (!result.Success)
                return BadRequest(result);

            return Ok(result);
        }

        [HttpDelete("delete-booking/{id:Guid}")]
        [Authorize]
        public async Task<IActionResult> DeleteBooking(Guid id)
        {
            var result = await _bookingService.DeleteBookingAsync(id);
            return result ? Ok("Booking deleted successfully.") : NotFound("Booking not found or deletion failed.");
        }

        [HttpGet("my-bookings")]
        [Authorize]
        public async Task<IActionResult> GetMyBookings()
        {
            var userId = User.FindFirst("sub")?.Value;
            if (string.IsNullOrEmpty(userId)) return Unauthorized();

            var bookings = await _bookingService.GetBookingsByUserAsync(Guid.Parse(userId));
            return Ok(bookings);
        }

        [HttpPut("update-status-by-owner")]
        [Authorize(Policy = "Owner")]
        public async Task<IActionResult> UpdateStatus([FromBody] BookingStatusUpdateRequestDto request)
        {
            try
            {
                var success = await _bookingService.UpdateBookingStatusAsync(request.BookingId, request);
                return success ? Ok("Status updated.") : NotFound("Booking not found.");
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }

    }
}
