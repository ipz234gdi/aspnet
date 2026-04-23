using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using CinemaBooking.Data;
using CinemaBooking.Data.Models;

namespace CinemaBooking.Api.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    [Authorize]
    public class MoviesController : ControllerBase
    {
        private ICinemaRepository repository;

        public MoviesController(ICinemaRepository repo)
        {
            repository = repo;
        }

        [HttpGet]
        [AllowAnonymous]
        public IActionResult GetAll()
        {
            return Ok(repository.Movies.ToList());
        }

        [HttpGet("{id}")]
        [AllowAnonymous]
        public IActionResult Get(long id)
        {
            var movie = repository.Movies.FirstOrDefault(m => m.MovieID == id);
            if (movie == null) return NotFound();
            return Ok(movie);
        }

        [HttpPost]
        [Authorize(Roles = "Admin")]
        public IActionResult Create([FromBody] Movie movie)
        {
            if (!ModelState.IsValid) return BadRequest(ModelState);
            repository.CreateMovie(movie);
            return CreatedAtAction(nameof(Get), new { id = movie.MovieID }, movie);
        }

        [HttpPut("{id}")]
        [Authorize(Roles = "Admin")]
        public IActionResult Update(long id, [FromBody] Movie movie)
        {
            var existing = repository.Movies.FirstOrDefault(m => m.MovieID == id);
            if (existing == null) return NotFound();
            existing.Title = movie.Title;
            existing.Description = movie.Description;
            existing.Genre = movie.Genre;
            existing.TicketPrice = movie.TicketPrice;
            existing.CinemaHallID = movie.CinemaHallID;
            existing.ImageUrl = movie.ImageUrl;
            repository.SaveMovie(existing);
            return Ok(existing);
        }

        [HttpDelete("{id}")]
        [Authorize(Roles = "Admin")]
        public IActionResult Delete(long id)
        {
            var movie = repository.Movies.FirstOrDefault(m => m.MovieID == id);
            if (movie == null) return NotFound();
            repository.DeleteMovie(movie);
            return NoContent();
        }
    }
}
