using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using CinemaBooking.Data;
using CinemaBooking.Data.Models;

namespace CinemaBooking.Controllers
{
    [Authorize]
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

        [Authorize(Roles = "Admin")]
        public IActionResult Create()
        {
            ViewBag.CinemaHalls = new SelectList(repository.CinemaHalls.ToList(), "CinemaHallID", "Name");
            return View();
        }

        [HttpPost]
        [Authorize(Roles = "Admin")]
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

        [Authorize(Roles = "Admin")]
        public IActionResult Edit(long id)
        {
            var movie = repository.Movies.FirstOrDefault(m => m.MovieID == id);
            if (movie == null) return NotFound();
            ViewBag.CinemaHalls = new SelectList(repository.CinemaHalls.ToList(), "CinemaHallID", "Name", movie.CinemaHallID);
            return View(movie);
        }

        [HttpPost]
        [Authorize(Roles = "Admin")]
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
                existing.ImageUrl = movie.ImageUrl;
                repository.SaveMovie(existing);
                return RedirectToAction("Index");
            }
            ViewBag.CinemaHalls = new SelectList(repository.CinemaHalls.ToList(), "CinemaHallID", "Name", movie.CinemaHallID);
            return View(movie);
        }

        [Authorize(Roles = "Admin")]
        public IActionResult Delete(long id)
        {
            var movie = repository.Movies.FirstOrDefault(m => m.MovieID == id);
            if (movie == null) return NotFound();
            var hall = repository.CinemaHalls.FirstOrDefault(h => h.CinemaHallID == movie.CinemaHallID);
            movie.CinemaHall = hall;
            return View(movie);
        }

        [HttpPost]
        [Authorize(Roles = "Admin")]
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
