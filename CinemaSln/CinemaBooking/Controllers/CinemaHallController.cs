using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using CinemaBooking.Data;
using CinemaBooking.Data.Models;

namespace CinemaBooking.Controllers
{
    [Authorize]
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

        [Authorize(Roles = "Admin")]
        public IActionResult Create()
        {
            return View();
        }

        [HttpPost]
        [Authorize(Roles = "Admin")]
        public IActionResult Create(CinemaHall hall)
        {
            hall.Capacity = hall.Rows * hall.SeatsPerRow;
            ModelState.ClearValidationState("Capacity");
            if (TryValidateModel(hall))
            {
                repository.CreateCinemaHall(hall);
                return RedirectToAction("Index");
            }
            return View(hall);
        }

        [Authorize(Roles = "Admin")]
        public IActionResult Edit(long id)
        {
            var hall = repository.CinemaHalls.FirstOrDefault(h => h.CinemaHallID == id);
            if (hall == null) return NotFound();
            return View(hall);
        }

        [HttpPost]
        [Authorize(Roles = "Admin")]
        public IActionResult Edit(CinemaHall hall)
        {
            hall.Capacity = hall.Rows * hall.SeatsPerRow;
            ModelState.ClearValidationState("Capacity");
            if (TryValidateModel(hall))
            {
                var existing = repository.CinemaHalls.FirstOrDefault(h => h.CinemaHallID == hall.CinemaHallID);
                if (existing == null) return NotFound();
                existing.Name = hall.Name;
                existing.Rows = hall.Rows;
                existing.SeatsPerRow = hall.SeatsPerRow;
                existing.Capacity = hall.Capacity;
                existing.Location = hall.Location;
                repository.SaveCinemaHall(existing);
                return RedirectToAction("Index");
            }
            return View(hall);
        }

        [Authorize(Roles = "Admin")]
        public IActionResult Delete(long id)
        {
            var hall = repository.CinemaHalls.FirstOrDefault(h => h.CinemaHallID == id);
            if (hall == null) return NotFound();
            return View(hall);
        }

        [HttpPost]
        [Authorize(Roles = "Admin")]
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
