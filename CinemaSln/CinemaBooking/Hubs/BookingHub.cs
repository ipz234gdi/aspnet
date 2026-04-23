using Microsoft.AspNetCore.SignalR;

namespace CinemaBooking.Hubs
{
    public class BookingHub : Hub
    {
        public async Task SeatBooked(long movieId, int row, int seat)
        {
            await Clients.Others.SendAsync("SeatUpdated", movieId, row, seat, true);
        }

        public async Task SeatReleased(long movieId, int row, int seat)
        {
            await Clients.Others.SendAsync("SeatUpdated", movieId, row, seat, false);
        }
    }
}
