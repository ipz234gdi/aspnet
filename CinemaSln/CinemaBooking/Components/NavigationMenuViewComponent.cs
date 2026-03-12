using Microsoft.AspNetCore.Mvc;
using CinemaBooking.Models;

namespace CinemaBooking.Components
{
    public class NavigationMenuViewComponent : ViewComponent
    {
        private ICinemaRepository repository;

        public NavigationMenuViewComponent(ICinemaRepository repo)
        {
            repository = repo;
        }

        public IViewComponentResult Invoke()
        {
            ViewBag.SelectedGenre = RouteData?.Values["genre"]
                ?? HttpContext.Request.Query["genre"].FirstOrDefault();

            return View(repository.Movies
                .Select(m => m.Genre)
                .Distinct()
                .OrderBy(g => g));
        }
    }
}
