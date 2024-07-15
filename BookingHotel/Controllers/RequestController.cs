using Microsoft.AspNetCore.Mvc;
using System.Linq;
using BookingHotel.Models;
using BookingHotel.Data;
using Microsoft.EntityFrameworkCore;
using OfficeOpenXml;
namespace BookingHotel.Controllers
{
    public class RequestController : Controller
    {
        private readonly HotelContext _context;

        public RequestController(HotelContext context)
        {
            _context = context;
        }

        public async Task<IActionResult> Index(string searchString, string sortOrder, string statusFilter, string currentFilter, int? pageNumber)
        {
            ViewData["CurrentSort"] = sortOrder;
            ViewData["DateSort"] = String.IsNullOrEmpty(sortOrder) ? "dateCheckIn_desc" : "";
            ViewData["RoomTypeSort"] = sortOrder == "RoomType" ? "RoomType_desc" : "RoomType";
            if (searchString != null)
            {
                pageNumber = 1;
            }
            else
            {
                searchString = currentFilter;
            }

            ViewData["CurrentFilter"] = searchString;
            ViewData["StatusFilter"] = statusFilter;
            IQueryable<Request> requests = _context.Requests.Include(r => r.Account).Include(a => a.RoomType);

            if (!String.IsNullOrEmpty(searchString))
            {
                requests = requests.Where(s => s.Account.phoneNumber.Contains(searchString));
            }
            if (!String.IsNullOrEmpty(statusFilter))
            {
                requests = requests.Where(s => s.status == statusFilter);
            }

            switch (sortOrder)
            {
                case "dateCheckIn_desc":
                    requests = requests.OrderByDescending(r => r.dateCheckIn);
                    break;
                case "RoomType":
                    requests = requests.OrderBy(r => r.RoomType.roomTypeName);
                    break;
                case "RoomType_desc":
                    requests = requests.OrderByDescending(r => r.RoomType.roomTypeName);
                    break;
                default:
                    requests = requests.OrderBy(r => r.dateCheckIn);
                    break;
            }

            int pageSize = 6;
            var paginatedRequests = await PaginatedList<Request>.CreateAsync(requests.AsNoTracking(), pageNumber ?? 1, pageSize);
            return View(paginatedRequests);
        }

        // Action method để hiển thị chi tiết yêu cầu
        public IActionResult Details(int id)
        {
            var request = _context.Requests
                .Include(r => r.Account)
                .ThenInclude(a => a.Enrollment)
                .ThenInclude(e => e.Room)
                .Include(r => r.RoomType)
                .FirstOrDefault(r => r.requestID == id);

            if (request == null)
            {
                return NotFound();
            }

            string RoomName = null;
            if (request.status == "Apply" && request.Account.Enrollment != null)
            {
                var approvedEnrollment = request.Account.Enrollment
                                               .FirstOrDefault(e => e.RequestID == id);

                if (approvedEnrollment != null)
                {
                    RoomName = approvedEnrollment.Room?.roomName;
                }
            }

            ViewBag.ApprovedRoomName = RoomName;
            return View(request);
        }

        public IActionResult Approve(int id)
        {
            // Lấy thông tin request và danh sách phòng thỏa mãn điều kiện
            var request = _context.Requests.Include(r => r.RoomType).FirstOrDefault(r => r.requestID == id);
            if (request == null)
            {
                return NotFound();
            }

            var availableRooms = _context.Rooms
                .Where(r => r.roomTypeID == request.roomTypeID && r.status == "Empty")
                .ToList();

            var viewModel = new ViewModels.ApproveViewModel
            {
                Request = request,
                AvailableRooms = availableRooms
            };

            return View(viewModel);
        }


        [HttpPost]
        public IActionResult ConfirmApprove(int requestId, int roomId)
        {
            // Lấy request và room dựa trên ID
            var request = _context.Requests.FirstOrDefault(r => r.requestID == requestId);
            var room = _context.Rooms.FirstOrDefault(r => r.roomID == roomId);
            if (request == null)
            {
                return RedirectToAction(nameof(Index));
            }
            if (room == null)
            {
                TempData["ErrorMessage"] = "Please select a room";
                return RedirectToAction(nameof(Approve), new { id = requestId });
            }

            if (room.status == "Occupied")
            {
                TempData["ErrorMessage"] = "The selected room has already been occupied. Please select a different room.";
                return RedirectToAction(nameof(Approve), new { id = requestId });
            }

            // Tạo và lưu Enrollment 
            var enrollment = new Enrollment
            {
                accountID = request.accountID,
                roomID = roomId,
                dateOfReceipt = DateTime.Now,
                RequestID = requestId,
            };

            _context.Enrollments.Add(enrollment);

            request.status = "Apply";
            room.status = "Occupied";

            _context.SaveChanges();
            TempData["SuccessMessage"] = "The request has been approved successfully.";

            return RedirectToAction(nameof(Approve), new { id = requestId });
        }


        public IActionResult Cancel(int id)
        {
            var request = _context.Requests.FirstOrDefault(r => r.requestID == id);
            if (request == null)
            {
                return NotFound();
            }
            var roomType = _context.RoomTypes.FirstOrDefault(rt => rt.roomTypeID == request.roomTypeID);
            if (roomType != null)
            {
                roomType.roomLeft += 1;
            }


            request.status = "Cancelled";
            _context.SaveChanges();
            TempData["CancelMessage"] = "The request has been Cancelled successfully.";
            return RedirectToAction(nameof(Index));
        }
        public async Task<IActionResult> ExportToExcel(string searchString, string statusFilter)
        {
            IQueryable<Request> requests = _context.Requests
                .Include(r => r.Account)
                .Include(a => a.RoomType);

            if (!String.IsNullOrEmpty(searchString))
            {
                requests = requests.Where(s => s.Account.phoneNumber.Contains(searchString));
            }
            if (!String.IsNullOrEmpty(statusFilter))
            {
                requests = requests.Where(s => s.status == statusFilter);
            }

            var requestList = await requests.ToListAsync();

            //EPPlus
            ExcelPackage.LicenseContext = LicenseContext.NonCommercial;

            using (var package = new ExcelPackage())
            {
                var worksheet = package.Workbook.Worksheets.Add("Requests");
                worksheet.Cells[1, 1].Value = "DateCheckIn";
                worksheet.Cells[1, 2].Value = "DateCheckOut";
                worksheet.Cells[1, 3].Value = "GuestCount";
                worksheet.Cells[1, 4].Value = "RoomType";
                worksheet.Cells[1, 5].Value = "Status";
                worksheet.Cells[1, 6].Value = "PhoneNumber";

                for (int i = 0; i < requestList.Count; i++)
                {
                    var request = requestList[i];
                    worksheet.Cells[i + 2, 1].Value = request.dateCheckIn;
                    worksheet.Cells[i + 2, 2].Value = request.dateCheckOut;
                    worksheet.Cells[i + 2, 3].Value = request.guestCount;
                    worksheet.Cells[i + 2, 4].Value = request.RoomType.roomTypeName;
                    worksheet.Cells[i + 2, 5].Value = request.status;
                    worksheet.Cells[i + 2, 6].Value = request.Account.phoneNumber;

                    worksheet.Cells[i + 2, 1].Style.Numberformat.Format = "dd-mm-yyyy hh:mm:ss";
                    worksheet.Cells[i + 2, 2].Style.Numberformat.Format = "dd-mm-yyyy hh:mm:ss";
                }

                worksheet.Cells[worksheet.Dimension.Address].AutoFitColumns();
                var stream = new MemoryStream(package.GetAsByteArray());
                return File(stream, "application/vnd.openxmlformats-officedocument.spreadsheetml.sheet", "Requests.xlsx");
            }
        }
    }
}
