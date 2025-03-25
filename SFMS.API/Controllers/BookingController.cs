using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.SignalR;
using SFMSSolution.API.Hubs;
using SFMSSolution.Application.DataTransferObjects.Booking.Request;
using SFMSSolution.Application.Services.Bookings;

namespace SFMSSolution.API.Controllers
{
    [Authorize(Roles = "Owner")]
    [ApiController]
    [Route("api/[controller]")]
    public class BookingController : ControllerBase
    {
        private readonly IBookingService _bookingService;
        private readonly IHubContext<BookingHub> _hubContext;

        public BookingController(IBookingService bookingService, IHubContext<BookingHub> hubContext)
        {
            _bookingService = bookingService;
            _hubContext = hubContext;
        }

        [HttpGet("{id:Guid}")]
        public async Task<IActionResult> GetBooking(Guid id)
        {
            var booking = await _bookingService.GetBookingAsync(id);
            if (booking == null)
                return NotFound("Booking not found.");

            return Ok(booking);
        }

        [HttpGet("all")]
        public async Task<IActionResult> GetAllBookings([FromQuery] int pageNumber = 1, [FromQuery] int pageSize = 10)
        {
            var (bookings, totalCount) = await _bookingService.GetAllBookingsAsync(pageNumber, pageSize);
            return Ok(new { TotalCount = totalCount, Bookings = bookings });
        }

        [HttpPost]
        public async Task<IActionResult> CreateBooking([FromBody] BookingCreateRequestDto request)
        {
            var result = await _bookingService.CreateBookingAsync(request);
            if (!result)
                return BadRequest("Failed to create booking.");

            // Thông báo tạo booking mới qua SignalR
            await _hubContext.Clients.All.SendAsync("BookingCreated", $"A new booking has been created by user {request.UserId}.");
            return Ok("Booking created successfully.");
        }

        [HttpPut("{id:Guid}")]
        public async Task<IActionResult> UpdateBooking(Guid id, [FromBody] BookingUpdateRequestDto request)
        {
            var result = await _bookingService.UpdateBookingAsync(id, request);
            if (!result)
                return NotFound("Booking not found or update failed.");

            // Thông báo cập nhật booking qua SignalR
            await _hubContext.Clients.All.SendAsync("BookingUpdated", $"Booking {id} has been updated.");
            return Ok("Booking updated successfully.");
        }

        [HttpDelete("{id:Guid}")]
        public async Task<IActionResult> DeleteBooking(Guid id)
        {
            var result = await _bookingService.DeleteBookingAsync(id);
            if (!result)
                return NotFound("Booking not found or delete failed.");

            // Thông báo xóa booking qua SignalR
            await _hubContext.Clients.All.SendAsync("BookingDeleted", $"Booking {id} has been deleted.");
            return Ok("Booking deleted successfully.");
        }
    }
}
