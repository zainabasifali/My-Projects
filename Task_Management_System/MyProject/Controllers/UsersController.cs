using Microsoft.AspNetCore.Mvc;
using MyProject.Models;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Authentication.Cookies;
using System.Security.Claims;
using System.Linq;

namespace MyProject.Controllers
{
    public class UsersController : Controller
    {
        private readonly TaskMsContext _context;

        public UsersController(TaskMsContext context)
        {
            _context = context;
        }

        // GET: Users/Register
        public IActionResult Register()
        {
            return View();
        }

        // POST: Users/Register
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Register(Register model)
        {
            if (ModelState.IsValid)
            {
                if (model.Password == model.ConfirmPassword)
                {
                    // Check if the email already exists in the database
                    if (_context.Users.Any(u => u.Email == model.Email))
                    {
                        ModelState.AddModelError("Email", "Email already exists.");
                        return View(model);
                    }

                    // Add your logic to save the user registration details to the database
                    _context.Add(model);
                    await _context.SaveChangesAsync();
                    return RedirectToAction("Login", "Users");
                }
                else
                {
                    ModelState.AddModelError("ConfirmPassword", "Passwords do not match.");
                }
            }
            return View(model);
        }

        // GET: Users/Login
        public IActionResult Login()
        {
            return View();
        }

        // POST: Users/Login
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Login(LoginViewModel model)
        {
            if (ModelState.IsValid)
            {
                // Add your logic to authenticate the user
                var user = await _context.Users.FirstOrDefaultAsync(u => u.Email == model.Email && u.Password == model.Password);
                if (user != null)
                {
                    // Create the identity and sign in
                    var claims = new List<Claim>
                    {
                        new Claim(ClaimTypes.NameIdentifier, user.Id.ToString()),
                        new Claim(ClaimTypes.Name, user.Email)
                    };

                    var claimsIdentity = new ClaimsIdentity(claims, CookieAuthenticationDefaults.AuthenticationScheme);
                    await HttpContext.SignInAsync(CookieAuthenticationDefaults.AuthenticationScheme, new ClaimsPrincipal(claimsIdentity));

                    // Redirect to a dashboard or home page upon successful login
                    return RedirectToAction("Index", "Home");
                }
                else
                {
                    ModelState.AddModelError("Password", "Invalid username or password.");
                }
            }

            return View(model);
        }

        // POST: Users/Logout
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Logout()
        {
            await HttpContext.SignOutAsync(CookieAuthenticationDefaults.AuthenticationScheme);
            return RedirectToAction(nameof(Login));
        }
    }
}
