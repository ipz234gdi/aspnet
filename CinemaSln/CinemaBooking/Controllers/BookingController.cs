using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using CinemaBooking.Data;
using CinemaBooking.Data.Models;
using CinemaBooking.Infrastructure;

namespace CinemaBooking.Controllers
{
    [Authorize]
    public class BookingController : Controller
    {
        private ICinemaRepository repository;

        public BookingController(ICinemaRepository repo)
        {
            repository = repo;
        }

        private string GetCartKey() => $"BookingCart_{User.Identity?.Name}";

        public IActionResult Index()
        {
            var cart = HttpContext.Session.GetJson<BookingCart>(GetCartKey())
                       ?? new BookingCart();
            return View(cart);
        }

        [HttpPost]
        public IActionResult AddSeat(long movieId, int row, int seat)
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
            }

            HttpContext.Session.SetJson(GetCartKey(), cart);
            return RedirectToAction("Index");
        }

        [HttpPost]
        public IActionResult RemoveSeat(long movieId, string seat)
        {
            var cart = HttpContext.Session.GetJson<BookingCart>(GetCartKey())
                       ?? new BookingCart();

            cart.Items.RemoveAll(i => i.MovieID == movieId && i.Seat == seat);
            HttpContext.Session.SetJson(GetCartKey(), cart);

            return RedirectToAction("Index");
        }

        [HttpPost]
        public IActionResult Clear()
        {
            HttpContext.Session.Remove(GetCartKey());
            return RedirectToAction("Index");
        }
    }
}
