using Microsoft.AspNetCore.Mvc;
using CinemaBooking.Models;
using CinemaBooking.Models.ViewModels;
using System.Linq;

namespace CinemaBooking.Controllers
{
    public class HomeController : Controller
    {
        private ICinemaRepository repository;
        
        public int PageSize = 2; 

        public HomeController(ICinemaRepository repo)
        {
            repository = repo;
        }

        public ViewResult Index(int moviePage = 1)
        {
            var viewModel = new MoviesListViewModel
            {
                Movies = repository.Movies
                    .OrderBy(m => m.MovieID)
                    .Skip((moviePage - 1) * PageSize)
                    .Take(PageSize),
                
                PagingInfo = new PagingInfo
                {
                    CurrentPage = moviePage,
                    ItemsPerPage = PageSize,
                    TotalItems = repository.Movies.Count()
                }
            };

            return View(viewModel);
        }
    }
}