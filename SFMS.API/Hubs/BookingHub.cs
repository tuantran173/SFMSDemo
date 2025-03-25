using Microsoft.AspNetCore.SignalR;

namespace SFMSSolution.API.Hubs
{
    public class BookingHub : Hub
    {
        public async Task SendBookingUpdate(string message)
        {
            await Clients.All.SendAsync("ReceiveBookingUpdate", message);
        }

        public async Task NotifyBookingCreated(string bookingId)
        {
            await Clients.All.SendAsync("BookingCreated", bookingId);
        }

        public async Task NotifyBookingUpdated(string bookingId)
        {
            await Clients.All.SendAsync("BookingUpdated", bookingId);
        }

        public async Task NotifyBookingDeleted(string bookingId)
        {
            await Clients.All.SendAsync("BookingDeleted", bookingId);
        }

    }
}
