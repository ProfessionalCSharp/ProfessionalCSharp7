using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using ASPNETCoreMVCSecurity.Models;
using Microsoft.AspNetCore.Html;
using System.Net.Http.Headers;

namespace ASPNETCoreMVCSecurity.Controllers
{
    public class HomeController : Controller
    {
        public string Echo(string x) => x;

        public IActionResult EchoUnencoded(string x) => Content(x, "text/html");

        public IActionResult EchoWithView(string x)
        {
            ViewBag.SampleData = x;
            return View();
        }

        public IActionResult Index()
        {
            return View();
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

        public IActionResult EditBook() => View();

        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult EditBook(Book book) => View("EditBookResult", book);
    }
}
