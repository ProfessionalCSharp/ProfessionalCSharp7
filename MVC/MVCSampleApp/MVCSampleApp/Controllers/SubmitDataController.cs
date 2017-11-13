using Microsoft.AspNetCore.Mvc;
using MVCSampleApp.Models;
using System;
using System.Threading.Tasks;

namespace MVCSampleApp.Controllers
{
    public class SubmitDataController : Controller
    {
        public IActionResult Index() => View();
        public IActionResult CreateMenu() => View();
        [HttpPost]
        public IActionResult CreateMenu(int id, string text, double price, DateTime date, string category)
        {
            var m = new Menu { Id = id, Text = text, Price = price, Date = date, Category = category };
            ViewBag.Info = $"menu created: {m.Text}, Price: {m.Price}, date: {m.Date.ToShortDateString()}, category: {m.Category}";
            return View("Index");
        }

        public IActionResult CreateMenu2() => View();
        [HttpPost]
        public IActionResult CreateMenu2(Menu menu)
        {
            ViewBag.Info = $"menu created: {menu.Text}, Price: {menu.Price}, date: {menu.Date.ToShortDateString()}, category: {menu.Category}";
            return View("Index");
        }

        public IActionResult CreateMenu3() => View();
        [HttpPost]
        public async Task<IActionResult> CreateMenu3Result()
        {
            var m = new Menu();
            bool updated = await TryUpdateModelAsync<Menu>(m);
            if (updated)
            {
                ViewBag.Info = $"menu created: {m.Text}, Price: {m.Price}, date: {m.Date.ToShortDateString()}, category: {m.Category}";
                return View("Index");
            }
            else
            {
                return View("Error");
            }
        }

        public IActionResult CreateMenu4() => View();
        [HttpPost]
        public IActionResult CreateMenu4(Menu menu)
        {
            if (ModelState.IsValid)
            {
                ViewBag.Info =
                  $"menu created: {menu.Text}, Price: {menu.Price}, date: {menu.Date.ToShortDateString()}, category: {menu.Category}";
            }
            else
            {
                ViewBag.Info = "not valid";
            }
            return View("Index");
        }
    }
}