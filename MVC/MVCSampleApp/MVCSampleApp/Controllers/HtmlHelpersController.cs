using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using MVCSampleApp.Extensions;
using MVCSampleApp.Models;

namespace MVCSampleApp.Controllers
{
    public class HtmlHelpersController : Controller
    {
        public IActionResult Index()
        {
            return View();
        }

        public IActionResult SimpleHelper() => View();

        public IActionResult HelperWithMenu() => View(GetSampleMenu());
        private Menu GetSampleMenu() =>
          new Menu
          {
              Id = 1,
              Text = "Schweinsbraten mit Knödel und Sauerkraut",
              Price = 6.9,
              Date = new DateTime(2017, 11, 14),
              Category = "Main"
          };

        public IActionResult HtmlAttributes() => View();

        public IActionResult HelperList()
        {
            var cars = new Dictionary<int, string>();
            cars.Add(1, "Red Bull Racing");
            cars.Add(2, "McLaren");
            cars.Add(3, "Mercedes");
            cars.Add(4, "Ferrari");
            return View(cars.ToSelectListItems(4));
        }

        public IActionResult StronglyTypedMenu() => View(GetSampleMenu());

        public IActionResult EditorExtensions() => View(GetSampleMenu());

        public IActionResult Display() => View(GetSampleMenu());
    }
}