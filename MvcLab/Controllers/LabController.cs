using Microsoft.AspNetCore.Mvc;

namespace MvcLab.Controllers
{
    public class LabController : Controller
    {
        // [cite_start]
        [Route("info")]
        public IActionResult Info()
        {
            // [cite_start]
            ViewBag.LabNumber = "1";
            ViewBag.Topic = "Вступ до ASP.NET Core";
            ViewBag.Purpose = "ознайомитися з основними принципами роботи .NET, навчитися налаштовувати середовище розробки та встановлювати необхідні компоненти, набути навичок створення рішень та проектів різних типів, набути навичок обробки запитів з використанням middleware.";
            ViewBag.StudentName = "Денис Грушевицький";

            return View();
        }
    }
}