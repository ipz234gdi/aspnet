using Microsoft.AspNetCore.Mvc;
using CinemaBooking.Models;

namespace CinemaBooking.Controllers
{
    public class CinemaHallController : Controller
    {
        private ICinemaRepository repository;

        public CinemaHallController(ICinemaRepository repo)
        {
            repository = repo;
        }

        public IActionResult Index()
        {
            return View(repository.CinemaHalls.ToList());
        }

        public IActionResult Details(long id)
        {
            var hall = repository.CinemaHalls.FirstOrDefault(h => h.CinemaHallID == id);
            if (hall == null) return NotFound();
            hall.Movies = repository.Movies.Where(m => m.CinemaHallID == id).ToList();
            return View(hall);
        }

        public IActionResult Create()
        {
            return View();
        }

        [HttpPost]
        public IActionResult Create(CinemaHall hall)
        {
            if (ModelState.IsValid)
            {
                repository.CreateCinemaHall(hall);
                return RedirectToAction("Index");
            }
            return View(hall);
        }

        public IActionResult Edit(long id)
        {
            var hall = repository.CinemaHalls.FirstOrDefault(h => h.CinemaHallID == id);
            if (hall == null) return NotFound();
            return View(hall);
        }

        [HttpPost]
        public IActionResult Edit(CinemaHall hall)
        {
            if (ModelState.IsValid)
            {
                var existing = repository.CinemaHalls.FirstOrDefault(h => h.CinemaHallID == hall.CinemaHallID);
                if (existing == null) return NotFound();
                existing.Name = hall.Name;
                existing.Capacity = hall.Capacity;
                existing.Location = hall.Location;
                repository.SaveCinemaHall(existing);
                return RedirectToAction("Index");
            }
            return View(hall);
        }

        public IActionResult Delete(long id)
        {
            var hall = repository.CinemaHalls.FirstOrDefault(h => h.CinemaHallID == id);
            if (hall == null) return NotFound();
            return View(hall);
        }

        [HttpPost]
        public IActionResult DeleteConfirmed(long cinemaHallID)
        {
            var hall = repository.CinemaHalls.FirstOrDefault(h => h.CinemaHallID == cinemaHallID);
            if (hall != null)
            {
                repository.DeleteCinemaHall(hall);
            }
            return RedirectToAction("Index");
        }
    }
}
