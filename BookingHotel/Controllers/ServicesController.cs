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
    public class ServicesController : Controller
    {
        private readonly HotelContext _context;

        public ServicesController(HotelContext context)
        {
            _context = context;
        }

        // GET: Services
        public async Task<IActionResult> Index(string searchString, string sortOrder, string currentFilter, int? pageNumber)
        {
            ViewData["CurrentSort"] = sortOrder;
            ViewData["IDSortParm"] = sortOrder == "id" ? "id_desc" : "id";
            ViewData["rtSortParm"] = sortOrder == "rt" ? "rt_desc" : "rt";



            if (searchString != null)
            {
                pageNumber = 1;
            }
            else
            {
                searchString = currentFilter;
            }

            ViewData["CurrentFilter"] = searchString;

            var services = _context.Services
                .Include(r => r.RoomTypeDetail)
                    .ThenInclude(r => r.RoomType)
                .AsNoTracking();

            if (!String.IsNullOrEmpty(searchString))
            {
                services = services.Where(r => r.RoomTypeDetail.RoomType.roomTypeName.Contains(searchString));
            }

            switch (sortOrder)
            {
                case "id":
                    services = services.OrderBy(r => r.serviceID);
                    break;
                case "id_desc":
                    services = services.OrderByDescending(r => r.serviceID);
                    break;
                case "rt":
                    services = services.OrderBy(r => r.RoomTypeDetail.RoomType.roomTypeName);
                    break;
                case "rt_desc":
                    services = services.OrderByDescending(r => r.RoomTypeDetail.RoomType.roomTypeName);
                    break;
                default:
                    services = services.OrderBy(r => r.serviceID);
                    break;
            }

            int pageSize = 6;
            return View(await PaginatedList<Service>.CreateAsync(services.AsNoTracking(), pageNumber ?? 1, pageSize));
        }

        // GET: Services/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null || _context.Services == null)
            {
                return NotFound();
            }

            var service = await _context.Services
                .Include(s => s.RoomTypeDetail)
                .FirstOrDefaultAsync(m => m.serviceID == id);
            if (service == null)
            {
                return NotFound();
            }

            return View(service);
        }

        // GET: Services/Create
        public IActionResult Create()
        {
            ViewData["roomTypeDetailID"] = new SelectList(_context.RoomTypes, "roomTypeID", "roomTypeName");
            return View();
        }

        // POST: Services/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("serviceID,roomHighlights,bedAndBath,servicesAndAmenities,roomTypeDetailID")] Service service)
        {
            if (ModelState.IsValid)
            {
                _context.Add(service);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            ViewData["roomTypeDetailID"] = new SelectList(_context.RoomTypes, "roomTypeID", "roomTypeName");
            return View(service);
        }

        // GET: Services/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null || _context.Services == null)
            {
                return NotFound();
            }

            var service = await _context.Services.FindAsync(id);
            if (service == null)
            {
                return NotFound();
            }
            ViewData["roomTypeDetailID"] = new SelectList(_context.RoomTypes, "roomTypeID", "roomTypeName");
            return View(service);
        }

        // POST: Services/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("serviceID,roomHighlights,bedAndBath,servicesAndAmenities,roomTypeDetailID")] Service service)
        {
            if (id != service.serviceID)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(service);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!ServiceExists(service.serviceID))
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
            ViewData["roomTypeDetailID"] = new SelectList(_context.RoomTypeDetails, "roomTypeDetailID", "roomTypeDetailID", service.roomTypeDetailID);
            return View(service);
        }

        // GET: Services/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null || _context.Services == null)
            {
                return NotFound();
            }

            var service = await _context.Services
                .Include(s => s.RoomTypeDetail)
                .FirstOrDefaultAsync(m => m.serviceID == id);
            if (service == null)
            {
                return NotFound();
            }

            return View(service);
        }

        // POST: Services/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            if (_context.Services == null)
            {
                return Problem("Entity set 'HotelContext.Services'  is null.");
            }
            var service = await _context.Services.FindAsync(id);
            if (service != null)
            {
                _context.Services.Remove(service);
            }
            
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool ServiceExists(int id)
        {
          return _context.Services.Any(e => e.serviceID == id);
        }
    }
}
