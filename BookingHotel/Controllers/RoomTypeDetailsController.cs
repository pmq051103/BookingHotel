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
    public class RoomTypeDetailsController : Controller
    {
        private readonly HotelContext _context;

        public RoomTypeDetailsController(HotelContext context)
        {
            _context = context;
        }

        // GET: RoomTypeDetails
        public async Task<IActionResult> Index(string searchString, string sortOrder, string currentFilter, int? pageNumber)
        {
            ViewData["CurrentSort"] = sortOrder;
            ViewData["IDSortParm"] = sortOrder == "id" ? "id_desc" : "id";
            ViewData["OccupancySortParm"] = sortOrder == "occ" ? "occ_desc" : "occ";
            ViewData["ViewSortParm"] = sortOrder == "view" ? "view_desc" : "view";
            ViewData["SizeSortParm"] = sortOrder == "size" ? "size_desc" : "size";
            ViewData["BedSortParm"] = sortOrder == "bed" ? "bed_desc" : "bed";

            if (searchString != null)
            {
                pageNumber = 1;
            }
            else
            {
                searchString = currentFilter;
            }

            ViewData["CurrentFilter"] = searchString;

            var roomTypeDetails = _context.RoomTypeDetails
                .Include(r => r.RoomType)
                .AsNoTracking();

            if (!String.IsNullOrEmpty(searchString))
            {
                roomTypeDetails = roomTypeDetails.Where(r => r.RoomType.roomTypeName.Contains(searchString));
            }

            switch (sortOrder)
            {
                case "id":
                    roomTypeDetails = roomTypeDetails.OrderBy(r => r.roomTypeDetailID);
                    break;
                case "id_desc":
                    roomTypeDetails = roomTypeDetails.OrderByDescending(r => r.roomTypeDetailID);
                    break;
                case "occ":
                    roomTypeDetails = roomTypeDetails.OrderBy(r => r.maxPeople);
                    break;
                case "occ_desc":
                    roomTypeDetails = roomTypeDetails.OrderByDescending(r => r.maxPeople);
                    break;
                case "view":
                    roomTypeDetails = roomTypeDetails.OrderBy(r => r.view);
                    break;
                case "view_desc":
                    roomTypeDetails = roomTypeDetails.OrderByDescending(r => r.view);
                    break;
                case "size":
                    roomTypeDetails = roomTypeDetails.OrderBy(r => r.size);
                    break;
                case "size_desc":
                    roomTypeDetails = roomTypeDetails.OrderByDescending(r => r.size);
                    break;
                case "bed":
                    roomTypeDetails = roomTypeDetails.OrderBy(r => r.bed);
                    break;
                case "bed_desc":
                    roomTypeDetails = roomTypeDetails.OrderByDescending(r => r.bed);
                    break;
                default:
                    roomTypeDetails = roomTypeDetails.OrderBy(r => r.roomTypeDetailID);
                    break;
            }

            int pageSize = 6;
            return View(await PaginatedList<RoomTypeDetail>.CreateAsync(roomTypeDetails.AsNoTracking(), pageNumber ?? 1, pageSize)); ;
        }

        // GET: RoomTypeDetails/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null || _context.RoomTypeDetails == null)
            {
                return NotFound();
            }

            var roomTypeDetail = await _context.RoomTypeDetails
                .Include(r => r.RoomType)
                .FirstOrDefaultAsync(m => m.roomTypeDetailID == id);
            if (roomTypeDetail == null)
            {
                return NotFound();
            }

            return View(roomTypeDetail);
        }

        // GET: RoomTypeDetails/Create
        public IActionResult Create()
        {
            ViewData["roomTypeID"] = new SelectList(_context.RoomTypes, "roomTypeID", "roomTypeName");
            return View();
        }

        // POST: RoomTypeDetails/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("roomTypeDetailID,roomTypeID,description,maxPeople,view,size,bed")] RoomTypeDetail roomTypeDetail)
        {
            if (ModelState.IsValid)
            {
                var existingRoomTypeDetail = await _context.RoomTypeDetails
                    .FirstOrDefaultAsync(r => r.roomTypeID == roomTypeDetail.roomTypeID);

                if (existingRoomTypeDetail != null)
                {
                    TempData["ErrorMessage"] = "A room type detail with this type already exists.";
                    ViewData["roomTypeID"] = new SelectList(_context.RoomTypes, "roomTypeID", "roomTypeName");
                    return View(roomTypeDetail);
                }
                _context.Add(roomTypeDetail);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            return View(roomTypeDetail);
        }

        // GET: RoomTypeDetails/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null || _context.RoomTypeDetails == null)
            {
                return NotFound();
            }

            var roomTypeDetail = await _context.RoomTypeDetails.FindAsync(id);
            if (roomTypeDetail == null)
            {
                return NotFound();
            }
            ViewData["roomTypeID"] = new SelectList(_context.RoomTypes, "roomTypeID", "roomTypeName");
            return View(roomTypeDetail);
        }

        // POST: RoomTypeDetails/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("roomTypeDetailID,roomTypeID,description,maxPeople,view,size,bed")] RoomTypeDetail roomTypeDetail)
        {
            if (id != roomTypeDetail.roomTypeDetailID)
            {
                return NotFound();
            }
            var existingRoom = await _context.Rooms
                .FirstOrDefaultAsync(r => r.roomTypeID == roomTypeDetail.roomTypeID);

            if (existingRoom != null)
            {
                TempData["ErrorMessage"] = "A room type with this name already exists detail.";
                ViewData["roomTypeID"] = new SelectList(_context.RoomTypes, "roomTypeID", "roomTypeName");
                return View(roomTypeDetail);
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(roomTypeDetail);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!RoomTypeDetailExists(roomTypeDetail.roomTypeDetailID))
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
            ViewData["roomTypeID"] = new SelectList(_context.RoomTypes, "roomTypeID", "roomTypeName");
            return View(roomTypeDetail);
        }

        // GET: RoomTypeDetails/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null || _context.RoomTypeDetails == null)
            {
                return NotFound();
            }

            var roomTypeDetail = await _context.RoomTypeDetails
                .Include(r => r.RoomType)
                .FirstOrDefaultAsync(m => m.roomTypeDetailID == id);
            if (roomTypeDetail == null)
            {
                return NotFound();
            }

            return View(roomTypeDetail);
        }

        // POST: RoomTypeDetails/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            if (_context.RoomTypeDetails == null)
            {
                return Problem("Entity set 'HotelContext.RoomTypeDetails'  is null.");
            }
            var roomTypeDetail = await _context.RoomTypeDetails.FindAsync(id);
            if (roomTypeDetail != null)
            {
                _context.RoomTypeDetails.Remove(roomTypeDetail);
            }
            
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool RoomTypeDetailExists(int id)
        {
          return _context.RoomTypeDetails.Any(e => e.roomTypeDetailID == id);
        }
    }
}
