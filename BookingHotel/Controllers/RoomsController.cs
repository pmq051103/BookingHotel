using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using BookingHotel.Data;
using BookingHotel.Models;
using System.Diagnostics;
using BookingHotel.Models.RoomViewModels;

namespace BookingHotel.Controllers
{
    public class RoomsController : Controller
    {
        private readonly HotelContext _context;

        public RoomsController(HotelContext context)
        {
            _context = context;
        }

        // GET: Rooms
        public async Task<IActionResult> Index(string searchString, string sortOrder, string currentFilter, int? pageNumber)
        {
            ViewData["CurrentSort"] = sortOrder;
            ViewData["IDSortParm"] = sortOrder == "id" ? "id_desc" : "id";
            ViewData["NameSortParm"] = sortOrder == "name" ? "name_desc" : "name";
            ViewData["RoomTypeSortParm"] = sortOrder == "rt" ? "rt_desc" : "rt";
            ViewData["StatusSortParm"] = sortOrder == "status" ? "status_desc" : "status";

            if (searchString != null)
            {
                pageNumber = 1;
            }
            else
            {
                searchString = currentFilter;
            }

            ViewData["CurrentFilter"] = searchString;

            var rooms = _context.Rooms
           .Include(r => r.RoomType)
           .AsNoTracking();

            if (!String.IsNullOrEmpty(searchString))
            {
                rooms = rooms.Where(r => r.roomName.Contains(searchString));
            }

            switch (sortOrder)
            {
                case "id":
                    rooms = rooms.OrderBy(r => r.roomTypeID);
                    break;
                case "id_desc":
                    rooms = rooms.OrderByDescending(r => r.roomTypeID);
                    break;
                case "name":
                    rooms = rooms.OrderBy(r => r.roomName);
                    break;
                case "name_desc":
                    rooms = rooms.OrderByDescending(r => r.roomName);
                    break;
                case "rt":
                    rooms = rooms.OrderBy(r => r.roomTypeID);
                    break;
                case "rt_desc":
                    rooms = rooms.OrderByDescending(r => r.roomTypeID);
                    break;
                case "status":
                    rooms = rooms.OrderBy(r => r.status);
                    break;
                case "status_desc":
                    rooms = rooms.OrderByDescending(r => r.status);
                    break;
                default:
                    rooms = rooms.OrderByDescending(r => r.roomID);
                    break;
            }

            int pageSize = 6;
            return View(await PaginatedList<Room>.CreateAsync(rooms.AsNoTracking(), pageNumber ?? 1, pageSize));

            //return View(await rooms.ToListAsync());
        }

        //GET: Rooms/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null || _context.Rooms == null)
            {
                return NotFound();
            }

            var rooms = _context.Rooms.AsNoTracking().FirstOrDefault(r => r.roomID == id);
            ViewBag.r = rooms.roomName;

            var room = await _context.Enrollments
         .Include(e => e.Room)
         .Include(e => e.Account)
             .ThenInclude(e => e.Requests)
         .Where(e => e.roomID == id)
         .OrderByDescending(e => e.dateOfReceipt) 
         .FirstOrDefaultAsync();

            return View(room);
        }

        // GET: Rooms/Create
        public IActionResult Create()
        {
            ViewData["roomTypeID"] = new SelectList(_context.RoomTypes, "roomTypeID", "roomTypeName");
            return View();
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("roomID,roomName,roomTypeID,guest,status")] Room room)
        {
            if (!ModelState.IsValid)
            {
                return View(room);
            }
            if(room.roomName == null)
            {
                TempData["ErrorMessage"] = "A room with this name must be have.";
                ViewData["roomTypeID"] = new SelectList(_context.RoomTypes, "roomTypeID", "roomTypeName");
                return View(room);
            }
                var existingRoom = await _context.Rooms
                    .FirstOrDefaultAsync(r => r.roomName == room.roomName);

                if (existingRoom != null)
                {
                    TempData["ErrorMessage"] = "A room with this name already exists.";
                    ViewData["roomTypeID"] = new SelectList(_context.RoomTypes, "roomTypeID", "roomTypeName");
                    return View(room);
                }


                room.status = "Empty";
                _context.Add(room);

                var roomType = await _context.RoomTypes.FindAsync(room.roomTypeID);
                if (roomType != null)
                {
                    roomType.roomLeft += 1;
                    _context.Update(roomType);
                }

                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));

        }

        // GET: Rooms/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {

            if (id == null || _context.Rooms == null)
            {
                return NotFound();
            }
            var room = await _context.Rooms.FindAsync(id);
            if (room == null)
            {
                return NotFound();
            }
            ViewData["roomTypeID"] = new SelectList(_context.RoomTypes, "roomTypeID", "roomTypeName");


            return View(room);
        }

        // POST: Rooms/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("roomID,roomName,roomTypeID,guest,status")] Room room)
        {
            if (id != room.roomID)
            {
                return NotFound();
            }
            if (room.roomName == null)
            {
                TempData["ErrorMessage"] = "A room with this name must be have.";
                ViewData["roomTypeID"] = new SelectList(_context.RoomTypes, "roomTypeID", "roomTypeName");
                return View(room);
            }
            var existingRoom = await _context.Rooms
                .FirstOrDefaultAsync(r => r.roomName == room.roomName);

            if (existingRoom != null)
            {
                TempData["ErrorMessage"] = "A room with this name already exists.";
                ViewData["roomTypeID"] = new SelectList(_context.RoomTypes, "roomTypeID", "roomTypeName");
                return View(room);
            }
            if (ModelState.IsValid)
            {
                try
                {
                    var originalRoom = await _context.Rooms.AsNoTracking().FirstOrDefaultAsync(r => r.roomID == id);
                    if (originalRoom == null)
                    {
                        return NotFound();
                    }

                    if (originalRoom.roomTypeID != room.roomTypeID)
                    {
                        // Decrement roomLeft of the original room type
                        var oldRoomType = await _context.RoomTypes.FindAsync(originalRoom.roomTypeID);
                        if (oldRoomType != null)
                        {
                            oldRoomType.roomLeft -= 1;
                            _context.Update(oldRoomType);
                        }

               
                        var newRoomType = await _context.RoomTypes.FindAsync(room.roomTypeID);
                        if (newRoomType != null)
                        {
                            newRoomType.roomLeft += 1;
                            _context.Update(newRoomType);
                        }
                    }
                    _context.Update(room);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!RoomExists(room.roomID))
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
            ViewBag.RoomTypes = new SelectList(_context.RoomTypes, "roomTypeID", "roomTypeName", room.roomTypeID);
            return View(room);
        }

        // GET: Rooms/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null || _context.Rooms == null)
            {
                return NotFound();
            }

            var room = await _context.Rooms
                .FirstOrDefaultAsync(m => m.roomID == id);
            if (room == null)
            {
                return NotFound();
            }

            return View(room);
        }

        // POST: Rooms/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            if (_context.Rooms == null)
            {
                return Problem("Entity set 'HotelContext.Rooms'  is null.");
            }
            var room = await _context.Rooms.FindAsync(id);
            if (room != null)
            {
                _context.Rooms.Remove(room);
            }

            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool RoomExists(int id)
        {
            return _context.Rooms.Any(e => e.roomID == id);
        }
        public async Task<IActionResult> CheckOut(int id)
        {
            var room = await _context.Rooms.FindAsync(id);
            if (room == null)
            {
                return NotFound();
            }

            if (room.status == "Occupied")
            {
                room.status = "Empty";
                _context.Update(room);
                await _context.SaveChangesAsync();
            }

            return RedirectToAction(nameof(Index));
        }
    }
}