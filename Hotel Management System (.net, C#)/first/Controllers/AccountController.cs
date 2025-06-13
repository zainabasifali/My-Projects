using first.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace first.Controllers
{
    public class AccountController : Controller
    {
        private HotelEntities db = new HotelEntities();

        public ActionResult Register()
        {
            return View();
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Register([Bind(Include = "id,name,email,password")] user user)
        {
            bool emailExists = db.users.Any(u => u.email == user.email);

            if (emailExists)
            {
                ModelState.AddModelError("email", "Email is already registered. Please use a different email.");
            }
            if (ModelState.IsValid)
            {
                user.created_at = DateTime.Now;
                user.role = "Guest";
                db.users.Add(user);
                db.SaveChanges();

                return RedirectToAction("Login");
            }

            return View(user);
        }

        public ActionResult Login()
        {
            return View();
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Login(string email, string password)
        {
            if (string.IsNullOrWhiteSpace(email) || string.IsNullOrWhiteSpace(password))
            {
                ViewBag.Error = "Please fill in all the fields.";
                return View();
            }
            var user = db.users.FirstOrDefault(u => u.email == email && u.password == password);
            if (user != null)
            {

                Session["UserId"] = user.id;
                Session["Username"] = user.name;
                Session["UserRole"] = user.role;
                Session["LoginTime"] = DateTime.Now.ToString("HH:mm");
                System.Diagnostics.Debug.WriteLine("Logged in Role: " + user.role);
                switch (user.role)
                {
                    case "Admin":
                        return RedirectToAction("AdminIndex", "home");
                    case "Guest":
                        return RedirectToAction("GuestIndex", "home");
                    case "Housekeeper":
                        return RedirectToAction("StaffIndex", "home");
                }
            }
            else
            {
                ViewBag.Error = "Invalid login credentials.";
            }
            return View();
        }
        public ActionResult Logout()
        {
            Session.Clear();
            Session.Abandon(); 

            return RedirectToAction("Login", "Account");
        }



    }
}