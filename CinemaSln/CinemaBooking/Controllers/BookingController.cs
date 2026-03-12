using Microsoft.AspNetCore.Mvc;
using CinemaBooking.Models;
using CinemaBooking.Infrastructure;

namespace CinemaBooking.Controllers
{
    public class BookingController : Controller
    {
        private ICinemaRepository repository;
        private const string CartSessionKey = "BookingCart";

        public BookingController(ICinemaRepository repo)
        {
            repository = repo;
        }

        public IActionResult Index()
        {
            var cart = HttpContext.Session.GetJson<BookingCart>(CartSessionKey)
                       ?? new BookingCart();
            return View(cart);
        }

        [HttpPost]
        public IActionResult AddSeat(long movieId, int row, int seat)
        {
            var movie = repository.Movies.FirstOrDefault(m => m.MovieID == movieId);
            if (movie == null) return NotFound();

            var cart = HttpContext.Session.GetJson<BookingCart>(CartSessionKey)
                       ?? new BookingCart();

            string seatLabel = $"Ряд {row}, Місце {seat}";

            bool exists = cart.Items.Any(i => i.MovieID == movieId && i.Seat == seatLabel);
            if (!exists)
            {
                cart.Items.Add(new CartItem
                {
                    MovieID = movie.MovieID ?? 0,
                    Title = movie.Title,
                    Seat = seatLabel,
                    TicketPrice = movie.TicketPrice
                });
            }

            HttpContext.Session.SetJson(CartSessionKey, cart);
            return RedirectToAction("Index");
        }

        [HttpPost]
        public IActionResult RemoveSeat(long movieId, string seat)
        {
            var cart = HttpContext.Session.GetJson<BookingCart>(CartSessionKey)
                       ?? new BookingCart();

            cart.Items.RemoveAll(i => i.MovieID == movieId && i.Seat == seat);
            HttpContext.Session.SetJson(CartSessionKey, cart);

            return RedirectToAction("Index");
        }

        [HttpPost]
        public IActionResult Clear()
        {
            HttpContext.Session.Remove(CartSessionKey);
            return RedirectToAction("Index");
        }
    }
}
