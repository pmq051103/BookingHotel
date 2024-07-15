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
    public class AccountsUserController : Controller
    {
        private readonly HotelContext _context;

        public AccountsUserController(HotelContext context)
        {
            _context = context;
        }

        // GET: AccountsUser
        public async Task<IActionResult> Index(string searchString, int? page)
        {
            ViewData["CurrentFilter"] = searchString;

            IQueryable<Account> accounts = _context.Accounts;

            if (!string.IsNullOrEmpty(searchString))
            {
                accounts = accounts.Where(a => a.username.Contains(searchString)
                                               || a.name.Contains(searchString)
                                               || a.phoneNumber.Contains(searchString));
            }

            int pageSize = 6;
            return View(await PaginatedList<Account>.CreateAsync(accounts.AsNoTracking(), page ?? 1, pageSize));
        }


        // GET: AccountsUser/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null || _context.Accounts == null)
            {
                return NotFound();
            }

            var account = await _context.Accounts
                .FirstOrDefaultAsync(m => m.accountID == id);
            if (account == null)
            {
                return NotFound();
            }

            return View(account);
        }

        // GET: AccountsUser/Create
        public IActionResult Create()
        {
            return View();
        }

        // POST: AccountsUser/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("accountID,username,password,name,phoneNumber,role")] Account account)
        {
            if (ModelState.IsValid)
            {
                bool usernameExists = await _context.Accounts.AnyAsync(a => a.username == account.username);
                bool phoneNumberExists = await _context.Accounts.AnyAsync(a => a.phoneNumber == account.phoneNumber);

                if (usernameExists || phoneNumberExists)
                {
                    if (usernameExists)
                    {
                         TempData["ErrorMessage"]="Username already exists."; 
                    }

                    if (phoneNumberExists)
                    {
                        TempData["ErrorMessage"] = "The phone number is already in use.";
                    }

                    return View(account); 
                }
                else
                {
                    _context.Add(account);
                    await _context.SaveChangesAsync();
                    return RedirectToAction(nameof(Index));
                }
            }

            return View(account); // Trả về view với các lỗi validate khác nếu có
        }

        // GET: AccountsUser/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null || _context.Accounts == null)
            {
                return NotFound();
            }

            var account = await _context.Accounts.FindAsync(id);
            if (account == null)
            {
                return NotFound();
            }
            return View(account);
        }

        // POST: AccountsUser/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("accountID,username,password,name,phoneNumber,role")] Account account)
        {
            if (id != account.accountID)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    var existingAccount = await _context.Accounts
                    .FirstOrDefaultAsync(a => a.phoneNumber == account.phoneNumber && a.accountID != account.accountID);
                    if (existingAccount != null)
                    {
                        TempData["ErrorMessage"] = "The phone number is already in use.";
                        return View(account);
                    }
                    _context.Update(account);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!AccountExists(account.accountID))
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
            return View(account);
        }

        // GET: AccountsUser/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null || _context.Accounts == null)
            {
                return NotFound();
            }

            var account = await _context.Accounts
                .FirstOrDefaultAsync(m => m.accountID == id);
            if (account == null)
            {
                return NotFound();
            }

            return View(account);
        }

        // POST: AccountsUser/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            if (_context.Accounts == null)
            {
                return Problem("Entity set 'HotelContext.Accounts'  is null.");
            }
            var account = await _context.Accounts.FindAsync(id);
            if (account != null)
            {
                _context.Accounts.Remove(account);
            }
            
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool AccountExists(int id)
        {
          return _context.Accounts.Any(e => e.accountID == id);
        }
    }
}
