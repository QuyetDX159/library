using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using library.Models.Entities;
using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Authentication.Cookies;
using Microsoft.AspNetCore.Http;
using System.Security.Claims;

namespace library.Controllers
{
    public class UsersController : Controller
    {
        private readonly LibraryContext _context;

        public UsersController(LibraryContext context)
        {
            _context = context;
        }

        public IActionResult Login()
        {
            return View();
        }
        

        // GET: Users
        public async Task<IActionResult> Admin()
        {
            // Kiểm tra xem người dùng đã đăng nhập hay chưa
            if (HttpContext.Session == null || HttpContext.Session.GetInt32("IdRole") != 1)
            {
                TempData["Message"] = "Bạn cần đăng nhập tài khoản admin trước mới có thể vào trang này";
                TempData["MessageType"] = "danger";
                

                return RedirectToAction("Login");
            }
            return _context.Users != null ?
                        View(await _context.Users.ToListAsync()) :
                        Problem("Entity set 'LibraryContext.Users'  is null.");
        }
        public async Task<IActionResult> Home()
        {
            return _context.Users != null ?
                        View(await _context.Users.ToListAsync()) :
                        Problem("Entity set 'LibraryContext.Users'  is null.");
        }

        // GET: Users/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null || _context.Users == null)
            {
                return NotFound();
            }

            var user = await _context.Users
                .FirstOrDefaultAsync(m => m.IdU == id);
            if (user == null)
            {
                return NotFound();
            }

            return View(user);
        }

        // GET: Users/Create
        public IActionResult Create()
        {
            return View();
        }
        public IActionResult Register()
        {
            return View();
        }

        [HttpPost]
        public async Task<IActionResult> Login(string username, string password, string fullname)
        {
            var user = await _context.Users
                .FirstOrDefaultAsync(u => u.Username == username && u.Password == password);


            if (user == null)
            {
                ViewData["ValidateMessage"] = "tài khoản không tồn tại";
                return View();
            }
            if ((int)user.IdRole == 1)
            {
                // Lưu thông tin người dùng vào session
                HttpContext.Session.SetString("Username", user.Username);
                HttpContext.Session.SetInt32("IdRole", (int)user.IdRole);


                return RedirectToAction("Admin");
            }
            else if ((int)user.IdRole == 2)
            {
                // Lưu thông tin người dùng vào session
                HttpContext.Session.SetString("Username", user.Username);
                HttpContext.Session.SetInt32("IdRole", (int)user.IdRole);

                return RedirectToAction("Home");
            }
            else if ((int)user.IdRole == 3)
            {
                ModelState.AddModelError("", "chưa có loại quyền này");
                return View();
            }
            

            return View();
        }

        public async Task<IActionResult> Logout()
        {
            // Xóa thông tin người dùng khỏi session
            HttpContext.Session.Remove("Username");
            HttpContext.Session.Remove("IdRole");

            // Đăng xuất khỏi hệ thống
            await HttpContext.SignOutAsync(CookieAuthenticationDefaults.AuthenticationScheme);

            return RedirectToAction("Login");
        }
        

        // POST: Users/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("IdU,Username,Password,Email,Fullname,Phone,Address,IdRole")] User user)
        {
            if (ModelState.IsValid)
            {
                _context.Add(user);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Admin));
            }
            return View(user);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Register([Bind("IdU,Username,Password,ConfirmPassword,Email,Fullname,Phone,Address,IdRole")] User user)
        {
            if (ModelState.IsValid)
            {
                // Check if username already exists
                if (await _context.Users.AnyAsync(u => u.Username == user.Username))
                {
                    ModelState.AddModelError("Username", "Username already exists");
                    return View(user);
                }

                // Check if email already exists
                if (await _context.Users.AnyAsync(u => u.Email == user.Email))
                {
                    ModelState.AddModelError("Email", "Email already exists");
                    return View(user);
                }

                // Add new user to database
                _context.Add(user);
                await _context.SaveChangesAsync();

                return RedirectToAction(nameof(Login));
            }
            return View(user);
        }

        // GET: Users/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null || _context.Users == null)
            {
                return NotFound();
            }

            var user = await _context.Users.FindAsync(id);
            if (user == null)
            {
                return NotFound();
            }
            return View(user);
        }

        // POST: Users/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("IdU,Username,Password,Email,Fullname,Phone,Address,IdRole")] User user)
        {
            if (id != user.IdU)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(user);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!UserExists(user.IdU))
                    {
                        return NotFound();
                    }
                    else
                    {
                        throw;
                    }
                }
                return RedirectToAction(nameof(Admin));
            }
            return View(user);
        }

        // GET: Users/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null || _context.Users == null)
            {
                return NotFound();
            }

            var user = await _context.Users
                .FirstOrDefaultAsync(m => m.IdU == id);
            if (user == null)
            {
                return NotFound();
            }

            return View(user);
        }

        // POST: Users/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            if (_context.Users == null)
            {
                return Problem("Entity set 'LibraryContext.Users'  is null.");
            }
            var user = await _context.Users.FindAsync(id);
            if (user != null)
            {
                _context.Users.Remove(user);
            }

            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Admin));
        }

        private bool UserExists(int id)
        {
            return (_context.Users?.Any(e => e.IdU == id)).GetValueOrDefault();
        }
    }
}
