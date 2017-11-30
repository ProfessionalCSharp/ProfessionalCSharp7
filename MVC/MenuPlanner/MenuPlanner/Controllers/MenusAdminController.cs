using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using MenuPlanner.Models;
using MenuPlanner.Services;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;

// For more information on enabling MVC for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

namespace MenuPlanner.Controllers
{
    public class MenusAdminController : Controller
    {
        private readonly IMenuCardsService _service;
        public MenusAdminController(IMenuCardsService service) => _service = service;

        public async Task<IActionResult> Index() =>
            View(await _service.GetMenusAsync());

        public async Task<IActionResult> Details(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            Menu menu = await _service.GetMenuByIdAsync(id.Value);
            if (menu == null)
            {
                return NotFound();
            }

            return View(menu);
        }

        public async Task<IActionResult> Create()
        {
            IEnumerable<MenuCard> cards = await _service.GetMenuCardsAsync();
            ViewBag.MenuCardId = new SelectList(cards, "Id", "Name");
            return View();
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("Id,Text,Price,Active,Order,Type,Day,MenuCardId")] Menu menu)
        {
            if (ModelState.IsValid)
            {
                await _service.AddMenuAsync(menu);
                return RedirectToAction(nameof(Index));
            }
            IEnumerable<MenuCard> cards = await _service.GetMenuCardsAsync();
            ViewBag.MenuCardId = new SelectList(cards, "Id", "Name", menu.MenuCardId);
            return View(menu);
        }

        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            Menu menu = await _service.GetMenuByIdAsync(id.Value);
            if (menu == null)
            {
                return NotFound();
            }
            IEnumerable<MenuCard> cards = await _service.GetMenuCardsAsync();
            ViewBag.MenuCardId = new SelectList(cards, "Id", "Name", menu.MenuCardId);
            return View(menu);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("Id,Text,Price,Active,Order,Type,Day,MenuCardId")] Menu menu)
        {
            if (id != menu.Id)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                await _service.UpdateMenuAsync(menu);
                return RedirectToAction(nameof(Index));
            }
            IEnumerable<MenuCard> cards = await _service.GetMenuCardsAsync();
            ViewBag.MenuCardId = new SelectList(cards, "Id", "Name", menu.MenuCardId);
            return View(menu);
        }

        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            Menu menu = await _service.GetMenuByIdAsync(id.Value);
            if (menu == null)
            {
                return NotFound();
            }

            return View(menu);
        }

        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            Menu menu = await _service.GetMenuByIdAsync(id);
            await _service.DeleteMenuAsync(id);
            return RedirectToAction(nameof(Index));
        }
    }
}
