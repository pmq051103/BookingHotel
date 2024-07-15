﻿using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using BookingHotel.Data;
using BookingHotel.Models;

namespace BookingHotel.Controllers
{
    public class FriendlyServiceCBSController : Controller
    {
        private readonly HotelContext _context;
        public FriendlyServiceCBSController(HotelContext context)
        {
            _context = context;
        }
        public async Task<IActionResult> FriendlyServiceCBSDetail(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }
            // Truy vấn cơ sở dữ liệu để lấy ComboSale với ID tương ứng
            var comboSale = await _context.ComboSales
                .FirstOrDefaultAsync(m => m.comboSaleID == id);

            if (comboSale == null)
            {
                return NotFound();
            }

            // Trả về view với dữ liệu của ComboSale được truy vấn
            return View(comboSale);

        }



            public async Task<IActionResult> Index()
        {
            var friendlyServiceCombos = await _context.ComboSales
                .Where(x => x.genre == "Friendly Service")
                .ToListAsync();

            return View(friendlyServiceCombos);
        }

    }
}