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
    public class roomsController : Controller
    {
        private HotelEntities db = new HotelEntities();

        public ActionResult Index()
        {
            AutoUpdateRoomStatuses();
            return View(db.rooms.ToList());
        }

        public ActionResult StaffIndex()
        {
            AutoUpdateRoomStatuses();
            return View(db.rooms.ToList());
        }

        private void AutoUpdateRoomStatuses()
        {
            var expiredBookings = db.bookings
                .Where(b => b.check_out <= DateTime.Now && b.status == "Confirmed")
                .ToList();

            foreach (var booking in expiredBookings)
            {
                var room = db.rooms.Find(booking.room_id);

                bool hasUpcoming = db.bookings.Any(b =>
                    b.room_id == room.id &&
                    b.check_in > DateTime.Now &&
                    b.status == "Confirmed");

                if (!hasUpcoming && room.status != "Available")
                {
                    room.status = "Available";
                    db.Entry(room).State = EntityState.Modified;
                }

                booking.status = "Completed";
                db.Entry(booking).State = EntityState.Modified;
            }

            db.SaveChanges();
        }


        public ActionResult Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            room room = db.rooms.Find(id);
            if (room == null)
            {
                return HttpNotFound();
            }
            return View(room);
        }

        public ActionResult Create()
        {
            return View();
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "id,number,type,price,description")] room room)
        {
            bool roomNumberExists = db.rooms.Any(r => r.number == room.number);

            if (roomNumberExists)
            {
                ModelState.AddModelError("number", "Room number already exists. Please choose a different number.");
            }

            if (ModelState.IsValid)
            {
                room.status = "Available";
                db.rooms.Add(room);
                db.SaveChanges();
                return RedirectToAction("Index");
            }

            return View(room);
        }

        public ActionResult Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            room room = db.rooms.Find(id);
            if (room == null)
            {
                return HttpNotFound();
            }
            return View(room);
        }


        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "id,number,type,status,price,description")] room room)
        {
            if (ModelState.IsValid)
            {
                db.Entry(room).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            return View(room);
        }

        public ActionResult Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            room room = db.rooms.Find(id);
            if (room == null)
            {
                return HttpNotFound();
            }
            return View(room);
        }

        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            room room = db.rooms.Find(id);
            db.rooms.Remove(room);
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
