using Microsoft.AspNetCore.Mvc;

namespace CinemaBooking.Controllers
{
    public class HomeController : Controller
    {
        // Повертає базове представлення
        public IActionResult Index() => View();
    }
}