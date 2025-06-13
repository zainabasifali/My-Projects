using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.Linq;
using System.Net;
using System.Web;
using System.Web.Mvc;
using first.Models;

namespace first.Controllers
{
    public class attendancesController : Controller
    {
        private HotelEntities db = new HotelEntities();

        public ActionResult AttendanceByDate(DateTime? date)
        {
            DateTime selectedDate = date ?? DateTime.Today;

            var housekeeperUserIds = db.users
                .Where(u => u.role == "Housekeeper")
                .Select(u => u.id)
                .ToList();

            // Fetch attendance records for that date & housekeepers
            var attendances = db.attendances
                .Where(a => a.date == selectedDate && housekeeperUserIds.Contains(a.user_id))
                .Include(a => a.user)
                .Include(a => a.user1)
                .ToList();

            ViewBag.SelectedDate = selectedDate;

            return View(attendances);
        }


        // GET: attendances
        public ActionResult Index()
        {
            DateTime today = DateTime.Today;

            var housekeeperUserIds = db.users
                .Where(u => u.role == "Housekeeper")
                .Select(u => u.id)
                .ToList();

            var markedUserIds = db.attendances
                 .Where(a => a.date == today)
                 .Select(a => a.user_id)
                 .ToList();

            var unmarkedUserIds = housekeeperUserIds.Except(markedUserIds).ToList();

            foreach (var userId in unmarkedUserIds)
            {
                db.attendances.Add(new attendance
                {
                    user_id = userId,
                    date = today,
                    status = "absent",
                    marked_by = null
                });
            }

            db.SaveChanges();

            var attendances = db.attendances
                .Where(a => a.date == today && housekeeperUserIds.Contains(a.user_id))
                .Include(a => a.user)
                .Include(a => a.user1)
                .ToList();

            return View(attendances);
        }

        public ActionResult MyAttendance()
        {
            int currentUserId = Convert.ToInt32(Session["userID"]);

            var myAttendanceRecords = db.attendances
                .Where(a => a.user_id == currentUserId)
                .Include(a => a.user)
                .Include(a => a.user1) // optional: if you need the name of the person who marked it
                .OrderByDescending(a => a.date)
                .ToList();

            return View(myAttendanceRecords);
        }

        public ActionResult StaffIndex()
        {
            int currentUserId = Convert.ToInt32(Session["userID"]);
            DateTime today = DateTime.Today;

            var existingToday = db.attendances.FirstOrDefault(a => a.user_id == currentUserId && a.date == today);

            if (existingToday == null)
            {
                var newAttendance = new attendance
                {
                    user_id = currentUserId,
                    date = today,
                    status = "absent",
                    marked_by = null
                };
                db.attendances.Add(newAttendance);
                db.SaveChanges();
                existingToday = newAttendance;
            }

            ViewBag.CanMarkAttendance = existingToday.status == "absent" && DateTime.Now.TimeOfDay < new TimeSpan(23, 0, 0) && existingToday.marked_by == null;

            var attendances = db.attendances
                .Where(a => a.user_id == currentUserId && DbFunctions.TruncateTime(a.date) == today)
                .Include(a => a.user)
                .Include(a => a.user1)
                .OrderByDescending(a => a.date)
                .Select(a => new AttendanceViewModel
                {
                    date = a.date,
                    status = a.status,
                    userName = a.user1 != null ? a.user1.name : "N/A",
                    markedByName = a.user != null ? a.user.name : "N/A"
                })
                .ToList();


            return View(attendances);
        }

        [HttpPost]

        public ActionResult MarkAllAbsent()
        {
            DateTime today = DateTime.Today;
            int adminUserId = Convert.ToInt32(Session["userID"]);

            var unmarkedAttendances = db.attendances
                .Where(a => a.date == today && a.marked_by == null)
                .ToList();

            foreach (var attendance in unmarkedAttendances)
            {
                attendance.status = "absent";
                attendance.marked_by = adminUserId;  
            }

            db.SaveChanges();

            TempData["Message"] = $"{unmarkedAttendances.Count} employees marked as absent.";

            return RedirectToAction("Index");
        }

        [HttpPost]

        public ActionResult MarkOnLeave(int id)
        {
            var attendance = db.attendances.FirstOrDefault(a => a.id == id);
            int adminUserId = Convert.ToInt32(Session["userID"]);
            if (attendance != null)
            {
                attendance.status = "on_leave";
                attendance.marked_by = adminUserId;
                db.SaveChanges();
            }

            return RedirectToAction("Index");
        }




        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult MarkPresent()
        {
            int currentUserId = Convert.ToInt32(Session["userID"]);
            DateTime today = DateTime.Today;

            var attendance = db.attendances.FirstOrDefault(a => a.user_id == currentUserId && a.date == today);

            if (attendance != null && attendance.status == "absent" && DateTime.Now.TimeOfDay < new TimeSpan(23, 0, 0))
            {
                attendance.status = "present";
                attendance.marked_by = currentUserId; // Marked by self
                db.SaveChanges();
            }

            return RedirectToAction("StaffIndex");
        }


        // GET: attendances/Details/5
        public ActionResult Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            attendance attendance = db.attendances.Find(id);
            if (attendance == null)
            {
                return HttpNotFound();
            }
            return View(attendance);
        }

        public ActionResult Create()
        {
            ViewBag.marked_by = new SelectList(db.users, "id", "name");
            ViewBag.user_id = new SelectList(db.users, "id", "name");
            return View();
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "id,user_id,date,status,marked_by")] attendance attendance)
        {
            if (ModelState.IsValid)
            {
                db.attendances.Add(attendance);
                db.SaveChanges();
                return RedirectToAction("Index");
            }

            ViewBag.marked_by = new SelectList(db.users, "id", "name", attendance.marked_by);
            ViewBag.user_id = new SelectList(db.users, "id", "name", attendance.user_id);
            return View(attendance);
        }

        // GET: attendances/Edit/5
        public ActionResult Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            attendance attendance = db.attendances.Find(id);
            if (attendance == null)
            {
                return HttpNotFound();
            }
            ViewBag.marked_by = new SelectList(db.users, "id", "name", attendance.marked_by);
            ViewBag.user_id = new SelectList(db.users, "id", "name", attendance.user_id);
            return View(attendance);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "id,user_id,date,status,marked_by")] attendance attendance)
        {
            if (ModelState.IsValid)
            {
                db.Entry(attendance).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            ViewBag.marked_by = new SelectList(db.users, "id", "name", attendance.marked_by);
            ViewBag.user_id = new SelectList(db.users, "id", "name", attendance.user_id);
            return View(attendance);
        }

        // GET: attendances/Delete/5
        public ActionResult Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            attendance attendance = db.attendances.Find(id);
            if (attendance == null)
            {
                return HttpNotFound();
            }
            return View(attendance);
        }

        // POST: attendances/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            attendance attendance = db.attendances.Find(id);
            db.attendances.Remove(attendance);
            db.SaveChanges();
            return RedirectToAction("Index");
        }

        protected override void Dispose(bool disposing)
        {
            if (disposing)
            {
                db.Dispose();
            }
            base.Dispose(disposing);
        }
    }
}
