using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using SFMSSolution.Application.DataTransferObjects.Booking.Request;
using SFMSSolution.Application.Services.Bookings;

namespace SFMSSolution.API.Controllers
{
    [Authorize(Roles ="Owner")]
    [ApiController]
    [Route("api/[controller]")]
    public class BookingController : ControllerBase
    {
        private readonly IBookingService _bookingService;

        public BookingController(IBookingService bookingService)
        {
            _bookingService = bookingService;
        }

        [HttpGet("{id:Guid}")]
        public async Task<IActionResult> GetBooking(Guid id)
        {
            var booking = await _bookingService.GetBookingAsync(id);
            if (booking == null)
                return NotFound("Booking not found.");
            return Ok(booking);
        }

        [AllowAnonymous]
        [HttpGet]
        public async Task<IActionResult> GetAllBookings()
        {
            var bookings = await _bookingService.GetAllBookingsAsync();
            return Ok(bookings);
        }

        [HttpPost]
        public async Task<IActionResult> CreateBooking([FromBody] BookingCreateRequestDto request)
        {
            var result = await _bookingService.CreateBookingAsync(request);
            if (!result)
                return BadRequest("Failed to create booking.");
            return Ok("Booking created successfully.");
        }

        [HttpPut("{id:Guid}")]
        public async Task<IActionResult> UpdateBooking(Guid id, [FromBody] BookingUpdateRequestDto request)
        {
            var result = await _bookingService.UpdateBookingAsync(id, request);
            if (!result)
                return NotFound("Booking not found or update failed.");
            return Ok("Booking updated successfully.");
        }

        [HttpDelete("{id:Guid}")]
        public async Task<IActionResult> DeleteBooking(Guid id)
        {
            var result = await _bookingService.DeleteBookingAsync(id);
            if (!result)
                return NotFound("Booking not found or delete failed.");
            return Ok("Booking deleted successfully.");
        }
    }
}
