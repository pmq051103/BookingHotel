using BookingHotel.Data;
using BookingHotel.Models;
using Microsoft.AspNetCore.Mvc;
using System.Linq;
using Microsoft.AspNetCore.Http;
using System.Threading.Tasks;
using System.Security.Claims;
using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Authentication.Cookies;
using static BookingHotel.ViewModels.AccountViewModels;
using System.Security.Principal;
using Microsoft.EntityFrameworkCore;
using BookingHotel.ViewModels;
using Microsoft.CodeAnalysis.Scripting;

namespace BookingHotel.Controllers
{
    public class AccountController : Controller
    {
        private readonly HotelContext _context;
        private readonly IHttpContextAccessor _httpContextAccessor;



        public AccountController(HotelContext context, IHttpContextAccessor httpContextAccessor)
        {
            _context = context;
            _httpContextAccessor = httpContextAccessor;
        }
        /* =============ĐĂNG KÍ============= */
        [HttpGet]
        public IActionResult Register()
        {
            return View();
        }

        [HttpPost]
        public IActionResult Register([Bind("Username,Password,ConfirmPassword,Name,PhoneNumber")] AccountViewModels model)
        {
            if (!ModelState.IsValid)
            {
                return View(model);
            }

            var existingAccount = _context.Accounts.FirstOrDefault(a => a.username == model.Username || a.phoneNumber == model.PhoneNumber);

            if (existingAccount != null)
            {
                if (existingAccount.username == model.Username)
                {
                    TempData["ErrorMessage"]= "Username already exists.";
                }
                if (existingAccount.phoneNumber == model.PhoneNumber)
                {
                    TempData["ErrorMessage"] = "The phone number is already in use.";
                }

                return View(model);
            }

            if (model.Password != model.ConfirmPassword)
            {
                TempData["ErrorMessage"] = "Password and Confirm Password do not match.";
                return View(model);
            }

            var account = new Account
            {
                username = model.Username,
                password = model.Password, 
                name = model.Name,
                phoneNumber = model.PhoneNumber,
                role = 0
            };

            _context.Accounts.Add(account);
            _context.SaveChanges();
			TempData["SuccessMessage"] = "Register successfully, log in now";
			return View(model);
        }
        /* End đăng kí */

        /* =============ĐĂNG NHẬP============= */
        public IActionResult Login()
        {
            ClaimsPrincipal claimsPrincipal = HttpContext.User;
            if (claimsPrincipal.Identity.IsAuthenticated)
            {
                return RedirectToAction("Index", "Home");
            }
            return View();
        }


        [HttpPost]
        public async Task<IActionResult> Login(LoginViewModel model)
        {
            if (!ModelState.IsValid)
            {
                return View(model);
            }

            var account = _context.Accounts.FirstOrDefault(a => a.username == model.Username && a.password == model.Password);
            if (account != null)
            {
                var role = account.role.ToString();
                List<Claim> claims = new List<Claim>()
                {
                        new Claim(ClaimTypes.Name, model.Username),
                        new Claim(ClaimTypes.Role, role),
                };
                ClaimsIdentity identity = new ClaimsIdentity(claims, CookieAuthenticationDefaults.AuthenticationScheme);

                await _httpContextAccessor.HttpContext.SignInAsync(CookieAuthenticationDefaults.AuthenticationScheme, new ClaimsPrincipal(identity));

                return RedirectToAction("Index", "Home");
            }
            else
            {
                TempData["ErrorMessage"] = "Username or password is incorrect";
            }
            return View(model);
        }


        /* End đăng nhập*/

        /* =============ĐĂNG XUẤT============= */
        public async Task<IActionResult> LogOut()
        {
            await HttpContext.SignOutAsync(CookieAuthenticationDefaults.AuthenticationScheme);
            return RedirectToAction("Index", "Home");
        }

        /* End đăng xuất */


        /* =============Thông tin và lịch sử đặt phòng============= */
        [HttpGet]
        public IActionResult UserProfile()
        {

            string username = HttpContext.User.Identity.Name;

            var account = _context.Accounts.FirstOrDefault(a => a.username == username);

            if (account == null)
            {
                return RedirectToAction("Index", "Home");
            }

            var requests = _context.Requests
            .Where(r => r.accountID == account.accountID)
            .Include(r => r.RoomType)
            .OrderByDescending(r => r.dateCheckIn)
            .ToList();
            var model = new AccountViewModels
            {
                Username = username,
                Name = account.name,
                PhoneNumber = account.phoneNumber,
                Requests = requests
            };
            return View(model);
        }
        /* End thông tin và lịch sử mua hàng */


        /* =============ĐỔI MẬT KHẨU============= */
        public async Task<IActionResult> ChangePassword(string username)
        {
            username = HttpContext.User.Identity.Name;
            var account = await _context.Accounts.FirstOrDefaultAsync(a => a.username == username);

            if (account == null)
            {
                return RedirectToAction("Index", "Home");
            }

            var model = new ChangePasswordViewModel
            {
                Username = account.username,
            };

            return View(model);
        }
        [HttpPost]
        public async Task<IActionResult> ChangePassword([Bind("Username,PasswordCurrent,Password,ConfirmPassword")] ChangePasswordViewModel model)
        {
            string username = HttpContext.User.Identity.Name;

            var account = await _context.Accounts.FirstOrDefaultAsync(a => a.username == username);
            if (account == null)
            {
                return RedirectToAction("Index", "Home");
            }


            if (model.PasswordCurrent != account.password)
            {
                TempData["ErrorMessage"] = "Incorrect current password.";
                return View(model);
            }


            if (model.Password != model.ConfirmPassword)
            {
                TempData["ErrorMessage"] = "New password and confirmation do not match.";
                return View(model);
            }


            account.password = model.Password;
            TempData["SuccessMessage"] = "Update password successfully.";

            _context.Update(account);
            await _context.SaveChangesAsync();

            return View(model);
        }
        /* End Đổi mật khẩu */

    }

}

