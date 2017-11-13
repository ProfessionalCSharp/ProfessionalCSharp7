using Microsoft.AspNetCore.Mvc;
using MVCSampleApp.Models;
using System.Collections.Generic;

namespace MVCSampleApp.Controllers
{
    public class ViewsDemoController : Controller
    {
        private EventsAndMenusContext _context;
        public ViewsDemoController(EventsAndMenusContext context)
        {
            _context = context;
        }

        public IActionResult Index()
        {
            return View();
        }
        public IActionResult PassingData()
        {
            ViewBag.MyData = "Hello from the controller";
            return View();
        }

        private IEnumerable<Menu> GetSampleData() =>
            new List<Menu>
            {
                new Menu
                {
                    Id=1,
                    Text="Schweinsbraten mit Knödel und Sauerkraut",
                    Price=6.9,
                    Category="Main"
                },
                new Menu
                {
                    Id=2,
                    Text="Erdäpfelgulasch mit Tofu und Gebäck",
                    Price=6.9,
                    Category="Vegetarian"
                },
                new Menu
                {
                    Id=3,
                    Text="Tiroler Bauerngröst'l mit Spiegelei und Krautsalat",
                    Price=6.9,
                    Category="Main"
                }
            };

        public IActionResult PassingAModel() =>
            View(GetSampleData());

        public IActionResult LayoutSample() => 
            View();

        public IActionResult LayoutUsingSections() =>
            View();

        public IActionResult UseAPartialView1() => View(_context);

        public IActionResult UseAPartialView2() => View();
        public IActionResult ShowEvents()
        {
            ViewBag.EventsTitle = "Live Events";
            return PartialView(_context.Events);
        }

        public IActionResult UseViewComponent1() => View();
        public IActionResult UseViewComponent2() => View();
        public IActionResult InjectServiceInView() => View();
    }
}