using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using CinemaBooking.Data;
using CinemaBooking.Data.Models;

namespace CinemaBooking.Api.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    [Authorize]
    public class CinemaHallsController : ControllerBase
    {
        private ICinemaRepository repository;

        public CinemaHallsController(ICinemaRepository repo)
        {
            repository = repo;
        }

        [HttpGet]
        [AllowAnonymous]
        public IActionResult GetAll()
        {
            return Ok(repository.CinemaHalls.ToList());
        }

        [HttpGet("{id}")]
        [AllowAnonymous]
        public IActionResult Get(long id)
        {
            var hall = repository.CinemaHalls.FirstOrDefault(h => h.CinemaHallID == id);
            if (hall == null) return NotFound();
            hall.Movies = repository.Movies.Where(m => m.CinemaHallID == id).ToList();
            return Ok(hall);
        }

        [HttpPost]
        [Authorize(Roles = "Admin")]
        public IActionResult Create([FromBody] CinemaHall hall)
        {
            if (!ModelState.IsValid) return BadRequest(ModelState);
            repository.CreateCinemaHall(hall);
            return CreatedAtAction(nameof(Get), new { id = hall.CinemaHallID }, hall);
        }

        [HttpPut("{id}")]
        [Authorize(Roles = "Admin")]
        public IActionResult Update(long id, [FromBody] CinemaHall hall)
        {
            var existing = repository.CinemaHalls.FirstOrDefault(h => h.CinemaHallID == id);
            if (existing == null) return NotFound();
            existing.Name = hall.Name;
            existing.Capacity = hall.Capacity;
            existing.Location = hall.Location;
            repository.SaveCinemaHall(existing);
            return Ok(existing);
        }

        [HttpDelete("{id}")]
        [Authorize(Roles = "Admin")]
        public IActionResult Delete(long id)
        {
            var hall = repository.CinemaHalls.FirstOrDefault(h => h.CinemaHallID == id);
            if (hall == null) return NotFound();
            repository.DeleteCinemaHall(hall);
            return NoContent();
        }
    }
}
