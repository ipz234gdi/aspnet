using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using CinemaBooking.Models;

namespace CinemaBooking.Controllers
{
    public class MovieController : Controller
    {
        private ICinemaRepository repository;

        public MovieController(ICinemaRepository repo)
        {
            repository = repo;
        }

        public IActionResult Index()
        {
            return View(repository.Movies.ToList());
        }

        public IActionResult Details(long id)
        {
            var movie = repository.Movies.FirstOrDefault(m => m.MovieID == id);
            if (movie == null) return NotFound();
            var hall = repository.CinemaHalls.FirstOrDefault(h => h.CinemaHallID == movie.CinemaHallID);
            movie.CinemaHall = hall;
            return View(movie);
        }

        public IActionResult Create()
        {
            ViewBag.CinemaHalls = new SelectList(repository.CinemaHalls.ToList(), "CinemaHallID", "Name");
            return View();
        }

        [HttpPost]
        public IActionResult Create(Movie movie)
        {
            if (ModelState.IsValid)
            {
                repository.CreateMovie(movie);
                return RedirectToAction("Index");
            }
            ViewBag.CinemaHalls = new SelectList(repository.CinemaHalls.ToList(), "CinemaHallID", "Name");
            return View(movie);
        }

        public IActionResult Edit(long id)
        {
            var movie = repository.Movies.FirstOrDefault(m => m.MovieID == id);
            if (movie == null) return NotFound();
            ViewBag.CinemaHalls = new SelectList(repository.CinemaHalls.ToList(), "CinemaHallID", "Name", movie.CinemaHallID);
            return View(movie);
        }

        [HttpPost]
        public IActionResult Edit(Movie movie)
        {
            if (ModelState.IsValid)
            {
                var existing = repository.Movies.FirstOrDefault(m => m.MovieID == movie.MovieID);
                if (existing == null) return NotFound();
                existing.Title = movie.Title;
                existing.Description = movie.Description;
                existing.Genre = movie.Genre;
                existing.TicketPrice = movie.TicketPrice;
                existing.CinemaHallID = movie.CinemaHallID;
                repository.SaveMovie(existing);
                return RedirectToAction("Index");
            }
            ViewBag.CinemaHalls = new SelectList(repository.CinemaHalls.ToList(), "CinemaHallID", "Name", movie.CinemaHallID);
            return View(movie);
        }

        public IActionResult Delete(long id)
        {
            var movie = repository.Movies.FirstOrDefault(m => m.MovieID == id);
            if (movie == null) return NotFound();
            var hall = repository.CinemaHalls.FirstOrDefault(h => h.CinemaHallID == movie.CinemaHallID);
            movie.CinemaHall = hall;
            return View(movie);
        }

        [HttpPost]
        public IActionResult DeleteConfirmed(long movieID)
        {
            var movie = repository.Movies.FirstOrDefault(m => m.MovieID == movieID);
            if (movie != null)
            {
                repository.DeleteMovie(movie);
            }
            return RedirectToAction("Index");
        }
    }
}
