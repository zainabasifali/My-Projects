using first.Models;
using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace first.Controllers
{
    public class HomeController : Controller
    {
        private HotelEntities db = new HotelEntities();

        // GET: bookings
        public ActionResult BookingStats()
        {
            var allMonths = Enumerable.Range(1, 12)
                .Select(m => new { Month = m, Name = CultureInfo.CurrentCulture.DateTimeFormat.GetMonthName(m) })
                .ToList();

            var confirmedBookings = db.bookings
                .Where(b => b.status == "confirmed")
                .GroupBy(b => b.check_in.Month)
                .Select(g => new { Month = g.Key, Count = g.Count() })
                .ToList();

            var bookingStats = allMonths.Select(m => new
            {
                Month = m.Name,
                Count = confirmedBookings.FirstOrDefault(b => b.Month == m.Month)?.Count ?? 0
            }).ToList();

            return Json(bookingStats, JsonRequestBehavior.AllowGet);
        }


        public ActionResult AdminIndex()

        {
            if (Session["UserId"] == null || Session["UserRole"]?.ToString() != "Admin")
            {
                return RedirectToAction("Login", "Account");
            }
            var pendingCount = db.bookings.Count(b => b.status == "pending");
            ViewBag.PendingBookingCount = pendingCount;

            var cancelledCount = db.bookings.Count(b => b.status == "cancelled");
            ViewBag.CancelledBookingCount = cancelledCount;

            var lowStockItems = db.inventories
            .Where(i => i.quantity < 10)
            .ToList();

            ViewBag.LowStockInventory = lowStockItems;

            var totalRooms = db.rooms.Count();
            var bookedRooms = db.bookings
                                .Where(b => b.status == "confirmed")
                                .Select(b => b.room_id)
                                .Distinct()
                                .Count();

            ViewBag.TotalRooms = totalRooms;
            ViewBag.BookedRooms = bookedRooms;

            return View();
        }

        public ActionResult GuestIndex()
        {
            if (Session["UserId"] == null || Session["UserRole"]?.ToString() != "Guest")
            {
                return RedirectToAction("Login", "Account");
            }

            int userId = Convert.ToInt32(Session["UserId"]);

            var bookings = db.bookings.Where(b => b.guest_id == userId).ToList();
            ViewBag.PendingBookings = bookings.Count(b => b.status == "Pending");
            ViewBag.CancelledBookings = bookings.Count(b => b.status == "Cancelled");
            ViewBag.ConfirmedBookings = bookings.Count(b => b.status == "Confirmed");

            var bookingIds = bookings
                .Where(b => b.status == "Confirmed")
                .Select(b => b.id)
                .ToList();

            ViewBag.UnpaidBills = db.bills.Count(bill => bookingIds.Contains(bill.booking_id) && bill.status == "unpaid");
            ViewBag.TotalBills = db.bills.Count(bill => bookingIds.Contains(bill.booking_id));

            var guest = db.users.FirstOrDefault(g => g.id == userId);
            ViewBag.Guest = guest;

            return View();
        }



        public ActionResult StaffIndex()
        {
            if (Session["UserId"] == null || Session["UserRole"]?.ToString() != "Housekeeper")
            {
                return RedirectToAction("Login", "Account");
            }
            int userId = Convert.ToInt32(Session["UserId"]);
            DateTime today = DateTime.Today;

            int totalAllowedLeaves = 3;
            int leavesTaken = db.attendances
                .Count(a => a.user_id == userId && a.status == "on_leave");
            int leavesLeft = totalAllowedLeaves - leavesTaken;

            int pendingDutiesCount = db.HouseKeeping_duties
                .Count(h => h.assigned_to == userId && h.status == "Pending");

            bool attendanceMarked = db.attendances
                .Any(a => a.user_id == userId && a.date == today && a.marked_by != null);

            ViewBag.LeavesLeft = leavesLeft < 0 ? 0 : leavesLeft;
            ViewBag.PendingDutiesCount = pendingDutiesCount;
            ViewBag.AttendanceMarked = attendanceMarked;

            return View();
        }


    }
}