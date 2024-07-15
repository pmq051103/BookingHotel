using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using BookingHotel.Data;
using BookingHotel.Models;

namespace BookingHotel.Controllers
{
    public class MenusController : Controller
    {
        private readonly HotelContext _context;

        public MenusController(HotelContext context)
        {
            _context = context;
        }

        // GET: Menus
        public async Task<IActionResult> Index(string searchString, int pageIndex = 1)
        {
            ViewData["CurrentFilter"] = searchString;

            var menus = from m in _context.Menus
                        select m;

            if (!String.IsNullOrEmpty(searchString))
            {
                menus = menus.Where(s => s.dishName.Contains(searchString));
            }

            int pageSize = 6;
            var paginatedList = await PaginatedList<Menu>.CreateAsync(menus.AsNoTracking(), pageIndex, pageSize);

            return View(paginatedList);
        }

        // GET: Menus/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null || _context.Menus == null)
            {
                return NotFound();
            }

            var menu = await _context.Menus
                .FirstOrDefaultAsync(m => m.ID == id);
            if (menu == null)
            {
                return NotFound();
            }

            return View(menu);
        }

        // GET: Menus/Create
        public IActionResult Create()
        {
            return View();
        }

        // POST: Menus/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("ID,dishName,dishDescription,dishPrice,dishImage")] Menu menu, IFormFile dishImage) 
        {
            if (ModelState.IsValid)
            {
                if (dishImage != null && dishImage.Length > 0)
                {
                    var fileName = Path.GetFileName(dishImage.FileName);
                    var filePath = Path.Combine(Directory.GetCurrentDirectory(), "wwwroot/images/menu", fileName);

                    using (var fileStream = new FileStream(filePath, FileMode.Create))
                    {
                        await dishImage.CopyToAsync(fileStream);
                    }
                    menu.dishImage = "/images/Menu/" + fileName;
                }

                if (menu != null) 
                {
                    _context.Add(menu);
                    await _context.SaveChangesAsync();
                }

                return RedirectToAction(nameof(Index));
            }
            return View(menu);
        }
        // GET: Menus/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null || _context.Menus == null)
            {
                return NotFound();
            }

            var menu = await _context.Menus.FindAsync(id);
            if (menu == null)
            {
                return NotFound();
            }
            return View(menu);
        }

        // POST: Menus/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("ID,dishName,dishDescription,dishPrice")] Menu menu, IFormFile dishImage)
        {
            if (id != menu.ID)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    if (dishImage != null && dishImage.Length > 0)
                    {
                        var fileName = Path.GetFileName(dishImage.FileName);
                        var directoryPath = Path.Combine(Directory.GetCurrentDirectory(), "wwwroot/images/Menu");

                        if (!Directory.Exists(directoryPath))
                        {
                            Directory.CreateDirectory(directoryPath);
                        }

                        var filePath = Path.Combine(directoryPath, fileName);

                        using (var fileStream = new FileStream(filePath, FileMode.Create))
                        {
                            await dishImage.CopyToAsync(fileStream);
                        }

                        menu.dishImage = "/images/Menu/" + fileName;
                    }

                    _context.Update(menu);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!MenuExists(menu.ID))
                    {
                        return NotFound();
                    }
                    else
                    {
                        throw;
                    }
                }
                return RedirectToAction(nameof(Index));
            }
            return View(menu);
        }


        // GET: Menus/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null || _context.Menus == null)
            {
                return NotFound();
            }

            var menu = await _context.Menus
                .FirstOrDefaultAsync(m => m.ID == id);
            if (menu == null)
            {
                return NotFound();
            }

            return View(menu);
        }

        // POST: Menus/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            if (_context.Menus == null)
            {
                return Problem("Entity set 'HotelContext.Menus'  is null.");
            }
            var menu = await _context.Menus.FindAsync(id);
            if (menu != null)
            {
                _context.Menus.Remove(menu);
            }

            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool MenuExists(int id)
        {
            return _context.Menus.Any(e => e.ID == id);
        }
    }
}
