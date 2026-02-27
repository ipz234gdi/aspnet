using Microsoft.AspNetCore.Mvc;
using CinemaBooking.Models;

namespace CinemaBooking.Controllers
{
    public class HomeController : Controller
    {
        private ICinemaRepository repository;

        public HomeController(ICinemaRepository repo)
        {
            repository = repo;
        }

        public IActionResult Index() => View(repository.Movies);
    }
}