using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
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
    }
}