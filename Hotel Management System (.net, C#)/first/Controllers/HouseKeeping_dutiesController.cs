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
    public class HouseKeeping_dutiesController : Controller
    {
        private HotelEntities db = new HotelEntities();

        // GET: HouseKeeping_duties
        public ActionResult Index()
        {
            var houseKeeping_duties = db.HouseKeeping_duties.Include(h => h.user).Include(h => h.room);
            return View(houseKeeping_duties.ToList());
        }

        public ActionResult StaffIndex()
        {
            int userId = Convert.ToInt32(Session["UserID"]); 
            var houseKeeping_duties = db.HouseKeeping_duties
                                        .Include(h => h.user)
                                        .Include(h => h.room)
                                        .Where(h => h.assigned_to == userId);

            return View(houseKeeping_duties.ToList());
        }

        // POST: HouseKeeping_duties/MarkAsDone/5
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult MarkAsDoneConfirmed(int id)
        {
            var duty = db.HouseKeeping_duties.Find(id);
            if (duty == null)
            {
                return HttpNotFound();
            }

            int currentUserId = Convert.ToInt32(Session["UserID"]);
            if (duty.assigned_to != currentUserId)
            {
                return new HttpStatusCodeResult(HttpStatusCode.Forbidden);
            }

            duty.status = "done";
            db.Entry(duty).State = EntityState.Modified;
            db.SaveChanges();

            return RedirectToAction("StaffIndex");
        }


        // GET: HouseKeeping_duties/Details/5
        public ActionResult Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            HouseKeeping_duties houseKeeping_duties = db.HouseKeeping_duties.Find(id);
            if (houseKeeping_duties == null)
            {
                return HttpNotFound();
            }
            return View(houseKeeping_duties);
        }

        // GET: HouseKeeping_duties/Create
        public ActionResult Create()
        {
            ViewBag.assigned_to = new SelectList(db.users.Where(u => u.role == "Housekeeper"), "id", "name");
            ViewBag.room_id = new SelectList(db.rooms, "id", "number");

            var newDuty = new HouseKeeping_duties
            {
                status = "pending",
                issue_reported = "none"
            };

            return View(newDuty);
        }

        // POST: HouseKeeping_duties/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "id,room_id,assigned_to,status,issue_reported,date")] HouseKeeping_duties houseKeeping_duties)
        {
            if (ModelState.IsValid)
            {
                db.HouseKeeping_duties.Add(houseKeeping_duties);
                db.SaveChanges();
                return RedirectToAction("Index");
            }

            ViewBag.assigned_to = new SelectList(db.users, "id", "name", houseKeeping_duties.assigned_to);
            ViewBag.room_id = new SelectList(db.rooms, "id", "number", houseKeeping_duties.room_id);
            return View(houseKeeping_duties);
        }

        // GET: HouseKeeping_duties/ReportIssue/5
        public ActionResult ReportIssue(int id)
        {
            var duty = db.HouseKeeping_duties.Find(id);
            if (duty == null)
            {
                return HttpNotFound();
            }

            // Ensure only assigned user can report issue
            int currentUserId = Convert.ToInt32(Session["UserID"]);
            if (duty.assigned_to != currentUserId)
            {
                return new HttpStatusCodeResult(HttpStatusCode.Forbidden);
            }

            return View(duty);
        }

        // POST: HouseKeeping_duties/ReportIssue/5
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult ReportIssue(int id, string issueText)
        {
            var duty = db.HouseKeeping_duties.Find(id);
            if (duty == null)
            {
                return HttpNotFound();
            }

            int currentUserId = Convert.ToInt32(Session["UserID"]);
            if (duty.assigned_to != currentUserId)
            {
                return new HttpStatusCodeResult(HttpStatusCode.Forbidden);
            }

            duty.issue_reported = issueText;
            db.Entry(duty).State = EntityState.Modified;
            db.SaveChanges();

            return RedirectToAction("StaffIndex");
        }


        // GET: HouseKeeping_duties/Edit/5
        public ActionResult Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            HouseKeeping_duties houseKeeping_duties = db.HouseKeeping_duties.Find(id);
            if (houseKeeping_duties == null)
            {
                return HttpNotFound();
            }
            ViewBag.assigned_to = new SelectList(
                db.users.Where(u => u.role == "Housekeeper"),
                "id",
                "name",
                houseKeeping_duties.assigned_to
);
            ViewBag.room_id = new SelectList(db.rooms, "id", "number", houseKeeping_duties.room_id);
            return View(houseKeeping_duties);
        }

        // POST: HouseKeeping_duties/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "id,room_id,assigned_to,status,issue_reported,date")] HouseKeeping_duties houseKeeping_duties)
        {
            if (ModelState.IsValid)
            {
                db.Entry(houseKeeping_duties).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            ViewBag.assigned_to = new SelectList(db.users.Where(u => u.role == "Housekeeper"), "id", "name");
            ViewBag.room_id = new SelectList(db.rooms, "id", "number", houseKeeping_duties.room_id);
            return View(houseKeeping_duties);
        }

        // GET: HouseKeeping_duties/Delete/5
        public ActionResult Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            HouseKeeping_duties houseKeeping_duties = db.HouseKeeping_duties.Find(id);
            if (houseKeeping_duties == null)
            {
                return HttpNotFound();
            }
            return View(houseKeeping_duties);
        }

        // POST: HouseKeeping_duties/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            HouseKeeping_duties houseKeeping_duties = db.HouseKeeping_duties.Find(id);
            db.HouseKeeping_duties.Remove(houseKeeping_duties);
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
