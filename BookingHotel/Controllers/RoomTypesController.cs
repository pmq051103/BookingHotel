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
    public class RoomTypesController : Controller
    {
        private readonly HotelContext _context;

        public RoomTypesController(HotelContext context)
        {
            _context = context;
        }

        // GET: RoomTypes
        public async Task<IActionResult> Index(string searchString, string sortOrder, string currentFilter, int? pageNumber)
        {
            ViewData["CurrentSort"] = sortOrder;
            ViewData["IDSortParm"] = sortOrder == "id" ? "id_desc" : "id";
            ViewData["NameSortParm"] = sortOrder == "name" ? "name_desc" : "name";
            ViewData["roomLeftSortParm"] = sortOrder == "rl" ? "rl_desc" : "rl";
            ViewData["PriceSortParm"] = sortOrder == "price" ? "price_desc" : "price";

            if (searchString != null)
            {
                pageNumber = 1;
            }
            else
            {
                searchString = currentFilter;
            }

            ViewData["CurrentFilter"] = searchString;

            var roomTypes = _context.RoomTypes
                .Include(r => r.RoomTypeDetail)
                .AsNoTracking();

            if (!String.IsNullOrEmpty(searchString))
            {
                roomTypes = roomTypes.Where(r => r.roomTypeName.Contains(searchString));
            }

            switch (sortOrder)
            {
                case "id":
                    roomTypes = roomTypes.OrderBy(r => r.roomTypeID);
                    break;
                case "id_desc":
                    roomTypes = roomTypes.OrderByDescending(r => r.roomTypeID);
                    break;
                case "name":
                    roomTypes = roomTypes.OrderBy(r => r.roomTypeName);
                    break;
                case "name_desc":
                    roomTypes = roomTypes.OrderByDescending(r => r.roomTypeName);
                    break;
                case "price":
                    roomTypes = roomTypes.OrderBy(r => r.price);
                    break;
                case "price_desc":
                    roomTypes = roomTypes.OrderByDescending(r => r.price);
                    break;
                case "rl":
                    roomTypes = roomTypes.OrderBy(r => r.roomLeft);
                    break;
                case "rl_desc":
                    roomTypes = roomTypes.OrderByDescending(r => r.roomLeft);
                    break;
                default:
                    roomTypes = roomTypes.OrderBy(r => r.roomTypeID);
                    break;
            }

            int pageSize = 6;
            return View(await PaginatedList<RoomType>.CreateAsync(roomTypes.AsNoTracking(), pageNumber ?? 1, pageSize));
        }

        // GET: RoomTypes/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null || _context.RoomTypes == null)
            {
                return NotFound();
            }

            var roomType = await _context.RoomTypes
                .FirstOrDefaultAsync(m => m.roomTypeID == id);
            if (roomType == null)
            {
                return NotFound();
            }

            return View(roomType);
        }

        // GET: RoomTypes/Create
        public IActionResult Create()
        {
            return View();
        }

        // POST: RoomTypes/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("roomTypeID,roomTypeName,roomLeft,price")] RoomType roomType)
        {
            if (ModelState.IsValid)
            {
                var existingRoomType = await _context.RoomTypes
                    .FirstOrDefaultAsync(r => r.roomTypeName == roomType.roomTypeName);

                if (existingRoomType != null)
                {
                    TempData["ErrorMessage"] = "A room type with this name already exists.";
                    return View(roomType);
                }
                _context.Add(roomType);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            return View(roomType);
        }

        // GET: RoomTypes/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null || _context.RoomTypes == null)
            {
                return NotFound();
            }

            var roomType = await _context.RoomTypes.FindAsync(id);
            if (roomType == null)
            {
                return NotFound();
            }
            return View(roomType);
        }

        // POST: RoomTypes/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("roomTypeID,roomTypeName,roomLeft,price")] RoomType roomType)
        {
            if (id != roomType.roomTypeID)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(roomType);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!RoomTypeExists(roomType.roomTypeID))
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
            return View(roomType);
        }

        // GET: RoomTypes/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null || _context.RoomTypes == null)
            {
                return NotFound();
            }

            var roomType = await _context.RoomTypes
                .FirstOrDefaultAsync(m => m.roomTypeID == id);
            if (roomType == null)
            {
                return NotFound();
            }

            return View(roomType);
        }

        // POST: RoomTypes/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            if (_context.RoomTypes == null)
            {
                return Problem("Entity set 'HotelContext.RoomTypes'  is null.");
            }
            var roomType = await _context.RoomTypes.FindAsync(id);
            if (roomType != null)
            {
                _context.RoomTypes.Remove(roomType);
            }
            
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool RoomTypeExists(int id)
        {
          return _context.RoomTypes.Any(e => e.roomTypeID == id);
        }
    }
}
