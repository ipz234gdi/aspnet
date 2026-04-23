using Microsoft.AspNetCore.SignalR;

namespace CinemaBooking.Hubs
{
    public class BookingHub : Hub
    {
        public async Task JoinMovieGroup(string movieId)
        {
            await Groups.AddToGroupAsync(Context.ConnectionId, $"Movie_{movieId}");
        }

        public async Task LeaveMovieGroup(string movieId)
        {
            await Groups.RemoveFromGroupAsync(Context.ConnectionId, $"Movie_{movieId}");
        }
    }
}
