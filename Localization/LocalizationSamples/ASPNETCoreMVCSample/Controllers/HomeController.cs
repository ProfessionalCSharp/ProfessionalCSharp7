using ASPNETCoreMVCSample.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Localization;
using System.Diagnostics;

namespace ASPNETCoreMVCSample.Controllers
{
    public class HomeController : Controller
    {
        private readonly IStringLocalizer<HomeController> _localizer;
        public HomeController(IStringLocalizer<HomeController> localizer)
        {
            _localizer = localizer;
        }
        public IActionResult Index()
        {
            return View();
        }

        public IActionResult Hello()
        {
            ViewBag.Message1 = _localizer.GetString("Message1");
            return View();
        }

        public IActionResult Book()
        {
            var b = new Book
            {
                Booktitle = "Professional C# 7 and .NET Core 2",
                Publisher = "Wrox Press"
            };
            return View(b);
        }

        public IActionResult About()
        {
            ViewData["Message"] = "Your application description page.";

            return View();
        }

        public IActionResult Contact()
        {
            ViewData["Message"] = "Your contact page.";

            return View();
        }

        public IActionResult Error()
        {
            return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
        }
    }
}
