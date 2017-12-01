using Microsoft.AspNetCore.Mvc;
using MVCSampleApp.Models;
using System;

namespace MVCSampleApp.Controllers
{
    public class ResultController : Controller
    {
        public IActionResult ContentDemo() =>
            Content("Hello World", "text/plain");

        public IActionResult JsonDemo()
        {
            var m = new Menu
            {
                Id = 3,
                Text = "Grilled sausage with sauerkraut and potatoes",
                Price = 12.90,
                Date = new DateTime(2018, 3, 31),
                Category = "Main"
            };
            return Json(m);
        }

        public IActionResult RedirectDemo() => Redirect("https://www.cninnovation.com");

        public IActionResult RedirectRouteDemo() =>
            RedirectToRoute(new { controller = "Home", action = "Hello" });

        public IActionResult FileDemo() =>
            File("~/images/Matthias.jpg", "image/jpeg");
    }
}
