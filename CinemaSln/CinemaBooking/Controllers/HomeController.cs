using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using CinemaBooking.Data;
using CinemaBooking.Data.Models;
using CinemaBooking.Data.ViewModels;

namespace CinemaBooking.Controllers
{
    [Authorize]
    public class HomeController : Controller
    {
        private ICinemaRepository repository;

        public int PageSize = 2;

        public HomeController(ICinemaRepository repo)
        {
            repository = repo;
        }

        public ViewResult Index(string? genre, int moviePage = 1)
        {
            IQueryable<Movie> movies = repository.Movies;

            if (!string.IsNullOrEmpty(genre))
            {
                movies = movies.Where(m => m.Genre == genre);
            }

            var viewModel = new MoviesListViewModel
            {
                Movies = movies
                    .OrderBy(m => m.MovieID)
                    .Skip((moviePage - 1) * PageSize)
                    .Take(PageSize),

                PagingInfo = new PagingInfo
                {
                    CurrentPage = moviePage,
                    ItemsPerPage = PageSize,
                    TotalItems = movies.Count()
                },

                CurrentGenre = genre
            };

            return View(viewModel);
        }
    }
}