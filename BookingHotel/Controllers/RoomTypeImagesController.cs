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
    public class RoomTypeImagesController : Controller
    {
        private readonly HotelContext _context;

        public RoomTypeImagesController(HotelContext context)
        {
            _context = context;
        }

        // GET: RoomTypeImages
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

            var roomTypeImages = _context.roomTypeImages
                .Include(r => r.RoomTypeDetail)
                    .ThenInclude(r => r.RoomType)
                .AsNoTracking();

            if (!String.IsNullOrEmpty(searchString))
            {
                roomTypeImages = roomTypeImages.Where(r => r.imagePath.Contains(searchString));
            }

            switch (sortOrder)
            {
                case "id":
                    roomTypeImages = roomTypeImages.OrderBy(r => r.ID);
                    break;
                case "id_desc":
                    roomTypeImages = roomTypeImages.OrderByDescending(r => r.ID);
                    break;
                case "rt":
                    roomTypeImages = roomTypeImages.OrderBy(r => r.RoomTypeDetail.RoomType.roomTypeName);
                    break;
                case "rt_desc":
                    roomTypeImages = roomTypeImages.OrderByDescending(r => r.RoomTypeDetail.RoomType.roomTypeName);
                    break;
                default:
                    roomTypeImages = roomTypeImages.OrderBy(r => r.ID);
                    break;
            }

            int pageSize = 6;
            return View(await PaginatedList<RoomTypeImage>.CreateAsync(roomTypeImages.AsNoTracking(), pageNumber ?? 1, pageSize));
        }

        // GET: RoomTypeImages/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null || _context.roomTypeImages == null)
            {
                return NotFound();
            }

            var roomTypeImage = await _context.roomTypeImages
                .Include(r => r.RoomTypeDetail)
                    .ThenInclude(r => r.RoomType)
                .FirstOrDefaultAsync(m => m.ID == id);
            if (roomTypeImage == null)
            {
                return NotFound();
            }

            return View(roomTypeImage);
        }

        // GET: RoomTypeImages/Create
        public IActionResult Create()
        {
            ViewData["roomTypeDetailID"] = new SelectList(_context.RoomTypes, "roomTypeID", "roomTypeName");
            return View();
        }

        // POST: RoomTypeImages/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("ID,roomTypeDetailID,imagePath")] RoomTypeImage roomTypeImage, IFormFile Image)
        {
            if (ModelState.IsValid)
            {
                if (Image != null && Image.Length > 0) // Sửa tên biến file input
                {
                    var fileName = Path.GetFileName(Image.FileName);
                    var filePath = Path.Combine(Directory.GetCurrentDirectory(), "wwwroot/img/roomtypeimages", fileName);

                    using (var fileStream = new FileStream(filePath, FileMode.Create))
                    {
                        await Image.CopyToAsync(fileStream);
                    }
                    roomTypeImage.imagePath = "/img/roomtypeimages/" + fileName;
                }

                // Lưu đối tượng Menu với đường dẫn ảnh đã cập nhật
                if (roomTypeImage != null) // Kiểm tra null
                {
                    _context.Add(roomTypeImage);
                    await _context.SaveChangesAsync();
                }

                return RedirectToAction(nameof(Index));
            }
            ViewData["roomTypeDetailID"] = new SelectList(_context.RoomTypeDetails, "roomTypeDetailID", "roomTypeDetailID", roomTypeImage.roomTypeDetailID);
            return View(roomTypeImage);
        }

        // GET: RoomTypeImages/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null || _context.roomTypeImages == null)
            {
                return NotFound();
            }

            var roomTypeImage = await _context.roomTypeImages.FindAsync(id);
            if (roomTypeImage == null)
            {
                return NotFound();
            }
            ViewData["roomTypeDetailID"] = new SelectList(_context.RoomTypes, "roomTypeID", "roomTypeName");
            return View(roomTypeImage);
        }

        // POST: RoomTypeImages/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("ID,roomTypeDetailID,imagePath")] RoomTypeImage roomTypeImage, IFormFile Image)
        {
            if (id != roomTypeImage.ID)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                if (Image != null && Image.Length > 0) // Sửa tên biến file input
                {
                    var fileName = Path.GetFileName(Image.FileName);
                    var filePath = Path.Combine(Directory.GetCurrentDirectory(), "wwwroot/img/roomtypeimages", fileName);

                    using (var fileStream = new FileStream(filePath, FileMode.Create))
                    {
                        await Image.CopyToAsync(fileStream);
                    }
                    roomTypeImage.imagePath = "/img/roomtypeimages/" + fileName;
                }

                try
                {
                    _context.Update(roomTypeImage);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!RoomTypeImageExists(roomTypeImage.ID))
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
            ViewData["roomTypeDetailID"] = new SelectList(_context.RoomTypeDetails, "roomTypeDetailID", "roomTypeDetailID", roomTypeImage.roomTypeDetailID);
            return View(roomTypeImage);
        }

        // GET: RoomTypeImages/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null || _context.roomTypeImages == null)
            {
                return NotFound();
            }

            var roomTypeImage = await _context.roomTypeImages
                .Include(r => r.RoomTypeDetail)
                .FirstOrDefaultAsync(m => m.ID == id);
            if (roomTypeImage == null)
            {
                return NotFound();
            }

            return View(roomTypeImage);
        }

        // POST: RoomTypeImages/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            if (_context.roomTypeImages == null)
            {
                return Problem("Entity set 'HotelContext.roomTypeImages'  is null.");
            }
            var roomTypeImage = await _context.roomTypeImages.FindAsync(id);
            if (roomTypeImage != null)
            {
                _context.roomTypeImages.Remove(roomTypeImage);
            }
            
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool RoomTypeImageExists(int id)
        {
          return _context.roomTypeImages.Any(e => e.ID == id);
        }
    }
}
