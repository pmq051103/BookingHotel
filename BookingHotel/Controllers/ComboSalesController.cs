using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using BookingHotel.Data;
using BookingHotel.Models;
using System.Web;
using System.IO;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Hosting;
using static System.Collections.Specialized.BitVector32;
using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.Hosting.Internal;
using BookingHotel.Controllers;

namespace BookingHotel.Controllers
{
    public class ComboSalesController : Controller
    {

        private readonly HotelContext _context;
        IWebHostEnvironment hostEnvironment;
        public ComboSalesController(HotelContext context, IWebHostEnvironment hc)
        {
            _context = context;
            hostEnvironment = hc;
        }
        // GET: ComboSales
        public async Task<IActionResult> Index(string CombosaleGenre, string SearchString, int? page)
        {
            if (_context.ComboSales == null)
            {
                return Problem("No data found");
            }

            IQueryable<string> genreQuery = from m in _context.ComboSales
                                            orderby m.genre
                                            select m.genre;

            var movies = from m in _context.ComboSales
                         select m;

            if (!string.IsNullOrEmpty(SearchString))
            {
                movies = movies.Where(s => s.title.Contains(SearchString));
            }

            if (!string.IsNullOrEmpty(CombosaleGenre))
            {
                movies = movies.Where(x => x.genre == CombosaleGenre);
            }

            var genres = new SelectList(await genreQuery.Distinct().ToListAsync());
            var pageSize = 5;
            var paginatedList = await PaginatedList<ComboSale>.CreateAsync(movies.AsNoTracking(), page ?? 1, pageSize);

            var movieGenreVM = new COMBOGenreViewModel
            {
                Genres = genres,
                ComboSales = paginatedList,
                CombosaleGenre = CombosaleGenre,
                SearchString = SearchString
            };

            return View(movieGenreVM);
        }


        // GET: ComboSales/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null || _context.ComboSales == null)
            {
                return NotFound();
            }

            var comboSale = await _context.ComboSales
                .FirstOrDefaultAsync(m => m.comboSaleID == id);
            if (comboSale == null)
            {
                return NotFound();
            }

            return View(comboSale);
        }

        // GET: ComboSales/Create
        public IActionResult Create()
        {
            ViewBag.GenreList = new List<string> { "Friendly Service", "Get Breakfast", "Transfer Services", "Suits & SPA", "Cozy Rooms" };
            return View();
        }



        // POST: ComboSales/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("comboSaleID,title,genre,ShortContent,content,image,price")] ComboSale comboSale, IFormFile upimage)
        {
            if (ModelState.IsValid)
            {
                if (upimage != null && upimage.Length > 0)
                {
                    var uploadsFolder = Path.Combine(hostEnvironment.WebRootPath, "hinhanhsale");
                    if (!Directory.Exists(uploadsFolder))
                    {
                        Directory.CreateDirectory(uploadsFolder);
                    }

                    var uniqueFileName = Guid.NewGuid().ToString() + "_" + Path.GetFileName(upimage.FileName);
                    var filePath = Path.Combine(uploadsFolder, uniqueFileName);
                    using (var fileStream = new FileStream(filePath, FileMode.Create))
                    {
                        await upimage.CopyToAsync(fileStream);
                    }
                    comboSale.image = uniqueFileName;
                }


                _context.Add(comboSale);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            ViewBag.GenreList = new List<string> { "Friendly Service", "Get Breakfast", "Transfer Services", "Suits & SPA", "Cozy Rooms" };
            return View(comboSale);
        }



        // GET: ComboSales/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null || _context.ComboSales == null)
            {
                return NotFound();
            }

            var comboSale = await _context.ComboSales.FindAsync(id);
            if (comboSale == null)
            {
                return NotFound();
            }
            ViewBag.GenreList = new List<string> { "Friendly Service", "Get Breakfast", "Transfer Services", "Suits & SPA", "Cozy Rooms" };
            return View(comboSale);
        }

        // POST: ComboSales/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("comboSaleID,title,genre,ShortContent,content,image,price")] ComboSale comboSale, IFormFile upimage)
        {
            if (id != comboSale.comboSaleID)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    // Check if a new image has been uploaded
                    if (upimage != null && upimage.Length > 0)
                    {
                        var uploadsFolder = Path.Combine(hostEnvironment.WebRootPath, "hinhanhsale");
                        if (!Directory.Exists(uploadsFolder))
                        {
                            Directory.CreateDirectory(uploadsFolder);
                        }

                        // Generate a unique file name
                        var uniqueFileName = Guid.NewGuid().ToString() + "_" + Path.GetFileName(upimage.FileName);
                        var filePath = Path.Combine(uploadsFolder, uniqueFileName);

                        // Save the new image
                        using (var fileStream = new FileStream(filePath, FileMode.Create))
                        {
                            await upimage.CopyToAsync(fileStream);
                        }

                        // Update the image path in the model
                        comboSale.image = uniqueFileName;
                    }

                    // Update the ComboSale object
                    _context.Update(comboSale);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!ComboSaleExists(comboSale.comboSaleID))
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
            ViewBag.GenreList = new List<string> { "Friendly Service", "Get Breakfast", "Transfer Services", "Suits & SPA", "Cozy Rooms" };
            return View(comboSale);
        }

        // GET: ComboSales/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null || _context.ComboSales == null)
            {
                return NotFound();
            }

            var comboSale = await _context.ComboSales
                .FirstOrDefaultAsync(m => m.comboSaleID == id);
            if (comboSale == null)
            {
                return NotFound();
            }

            return View(comboSale);
        }

        // POST: ComboSales/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            if (_context.ComboSales == null)
            {
                return Problem("Entity set 'HotelContext.ComboSales'  is null.");
            }
            var comboSale = await _context.ComboSales.FindAsync(id);
            if (comboSale != null)
            {
                _context.ComboSales.Remove(comboSale);
            }

            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool ComboSaleExists(int id)
        {
            return _context.ComboSales.Any(e => e.comboSaleID == id);
        }
    }
}