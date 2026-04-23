using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.SignalR;
using CinemaBooking.Data;
using CinemaBooking.Data.Models;
using CinemaBooking.Hubs;
using CinemaBooking.Infrastructure;

namespace CinemaBooking.Controllers
{
    [Authorize]
    public class BookingController : Controller
    {
        private ICinemaRepository repository;
        private IHubContext<BookingHub> hubContext;

        public BookingController(ICinemaRepository repo, IHubContext<BookingHub> hub)
        {
            repository = repo;
            hubContext = hub;
        }

        private string GetCartKey() => $"BookingCart_{User.Identity?.Name}";

        public IActionResult Index()
        {
            var cart = HttpContext.Session.GetJson<BookingCart>(GetCartKey())
                       ?? new BookingCart();
            return View(cart);
        }

        public IActionResult SeatMap(long movieId)
        {
            var movie = repository.Movies.FirstOrDefault(m => m.MovieID == movieId);
            if (movie == null) return NotFound();

            var hall = repository.CinemaHalls.FirstOrDefault(h => h.CinemaHallID == movie.CinemaHallID);

            ViewBag.Movie = movie;
            ViewBag.HallName = hall?.Name ?? "Невідомо";
            ViewBag.Rows = 5;
            ViewBag.SeatsPerRow = 10;

            var cart = HttpContext.Session.GetJson<BookingCart>(GetCartKey())
                       ?? new BookingCart();
            ViewBag.MyBookedSeats = cart.Items
                .Where(i => i.MovieID == movieId)
                .Select(i => i.Seat)
                .ToList();

            return View();
        }

        [HttpPost]
        public async Task<IActionResult> AddSeat(long movieId, int row, int seat)
        {
            var movie = repository.Movies.FirstOrDefault(m => m.MovieID == movieId);
            if (movie == null) return NotFound();
            var hall = repository.CinemaHalls.FirstOrDefault(h => h.CinemaHallID == movie.CinemaHallID);
            string hallName = hall?.Name ?? "Невідомо";

            var cart = HttpContext.Session.GetJson<BookingCart>(GetCartKey())
                       ?? new BookingCart();

            string seatLabel = $"Ряд {row}, Місце {seat}";

            bool exists = cart.Items.Any(i => i.MovieID == movieId && i.Seat == seatLabel);
            if (!exists)
            {
                cart.Items.Add(new CartItem
                {
                    MovieID = movie.MovieID ?? 0,
                    Title = movie.Title,
                    CinemaHallName = hallName,
                    Seat = seatLabel,
                    TicketPrice = movie.TicketPrice
                });

                await hubContext.Clients.All.SendAsync("SeatUpdated", movieId, row, seat, true);
            }

            HttpContext.Session.SetJson(GetCartKey(), cart);

            if (Request.Headers["X-Requested-With"] == "XMLHttpRequest")
            {
                return Json(new { success = true });
            }

            return RedirectToAction("SeatMap", new { movieId });
        }

        [HttpPost]
        public async Task<IActionResult> RemoveSeat(long movieId, string seat)
        {
            var cart = HttpContext.Session.GetJson<BookingCart>(GetCartKey())
                       ?? new BookingCart();

            var item = cart.Items.FirstOrDefault(i => i.MovieID == movieId && i.Seat == seat);
            if (item != null)
            {
                cart.Items.Remove(item);

                var parts = seat.Replace("Ряд ", "").Replace("Місце ", "").Split(", ");
                if (parts.Length == 2 && int.TryParse(parts[0], out int row) && int.TryParse(parts[1], out int seatNum))
                {
                    await hubContext.Clients.All.SendAsync("SeatUpdated", movieId, row, seatNum, false);
                }
            }

            HttpContext.Session.SetJson(GetCartKey(), cart);

            return RedirectToAction("Index");
        }

        [HttpPost]
        public async Task<IActionResult> Clear()
        {
            var cart = HttpContext.Session.GetJson<BookingCart>(GetCartKey())
                       ?? new BookingCart();

            foreach (var item in cart.Items)
            {
                var parts = item.Seat.Replace("Ряд ", "").Replace("Місце ", "").Split(", ");
                if (parts.Length == 2 && int.TryParse(parts[0], out int row) && int.TryParse(parts[1], out int seatNum))
                {
                    await hubContext.Clients.All.SendAsync("SeatUpdated", item.MovieID, row, seatNum, false);
                }
            }

            HttpContext.Session.Remove(GetCartKey());
            return RedirectToAction("Index");
        }
    }
}
