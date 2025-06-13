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
    public class bookingsController : Controller
    {
        private HotelEntities db = new HotelEntities();

        // GET: bookings
        public ActionResult AdminIndex()
        {
            var bookings = db.bookings.Include(b => b.user).Include(b => b.room);
            return View(bookings.ToList());
        }
        public ActionResult GuestIndex()
        {
            int? currentGuestId = Session["UserId"] as int?;
            var bookings = db.bookings.Include(b => b.user).Include(b => b.room).Where(b=>b.guest_id == currentGuestId);
            return View(bookings.ToList());
        }

        public ActionResult StaffIndex()
        {
            var bookings = db.bookings.Include(b => b.user).Include(b => b.room);
            return View(bookings.ToList());
        }

        // GET: bookings/Details/5
        public ActionResult Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            booking booking = db.bookings.Find(id);
            if (booking == null)
            {
                return HttpNotFound();
            }
            return View(booking);
        }

        // GET: bookings/Create
        public ActionResult Create()
        {
            ViewBag.guest_id = new SelectList(db.users, "id", "name");
            ViewBag.room_id = new SelectList(db.rooms, "id", "number");
            return View();
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "id,guest_id,room_id,check_in,check_out")] booking booking)
        {
            var userRole = Session["UserRole"].ToString();
            if (booking.check_out <= booking.check_in)
            {
                ModelState.AddModelError("check_out", "Check-out must be after check-in.");
            }

            if (booking.check_in < DateTime.Today)
            {
                ModelState.AddModelError("check_in", "Check-in date cannot be in the past.");
            }
            var overlappingBooking = db.bookings
                         .Where(b => b.room_id == booking.room_id && b.id != booking.id && b.status == "Confirmed" &&
                             ((booking.check_in >= b.check_in && booking.check_in < b.check_out)
                           || (booking.check_out > b.check_in && booking.check_out <= b.check_out)
                           || (booking.check_in <= b.check_in && booking.check_out >= b.check_out)))
                         .FirstOrDefault();

            if (overlappingBooking != null)
            {
                ModelState.AddModelError("room_id", "This room is already booked during the selected dates.");
            }

            if (ModelState.IsValid)
            {
                if ( userRole== "Guest")
                {
                    booking.status = "Pending";
                }
                else
                {
                    booking.status = "Confirmed";
                    var room = db.rooms.Find(booking.room_id);
                    if (room != null)
                    {
                        room.status = "Booked";
                        db.Entry(room).State = EntityState.Modified;
                    }
                }
   
                db.bookings.Add(booking);
                db.SaveChanges();
                return userRole == "Admin" ? RedirectToAction("AdminIndex") : RedirectToAction("GuestIndex");
            }


            ViewBag.guest_id = new SelectList(db.users, "id", "name", booking.guest_id);
            ViewBag.room_id = new SelectList(db.rooms, "id", "number", booking.room_id);
            return View(booking);
        }

        // GET: bookings/Edit/5
        public ActionResult Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            booking booking = db.bookings.Find(id);
            if (booking == null)
            {
                return HttpNotFound();
            }
            ViewBag.guest_id = new SelectList(db.users, "id", "name", booking.guest_id);
            ViewBag.room_id = new SelectList(db.rooms, "id", "number", booking.room_id);
            return View(booking);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "id,guest_id,room_id,check_in,check_out")] booking booking)
        {
            var userRole = Session["UserRole"]?.ToString();

            if (booking.check_out <= booking.check_in)
            {
                ModelState.AddModelError("check_out", "Check-out must be after check-in.");
            }

            if (booking.check_in < DateTime.Today)
            {
                ModelState.AddModelError("check_in", "Check-in date cannot be in the past.");
            }

            // Only check against CONFIRMED overlapping bookings
            var overlappingBooking = db.bookings
                .Where(b => b.room_id == booking.room_id && b.id != booking.id && b.status == "Confirmed" &&
                    ((booking.check_in >= b.check_in && booking.check_in < b.check_out)
                  || (booking.check_out > b.check_in && booking.check_out <= b.check_out)
                  || (booking.check_in <= b.check_in && booking.check_out >= b.check_out)))
                .FirstOrDefault();

            if (overlappingBooking != null)
            {
                ModelState.AddModelError("room_id", "This room is already booked during the selected dates.");
            }

            if (ModelState.IsValid)
            {
                var originalBooking = db.bookings.AsNoTracking().FirstOrDefault(b => b.id == booking.id);
                int previousRoomId = originalBooking?.room_id ?? 0;
                string originalStatus = originalBooking?.status;

                if (userRole == "Guest")
                {
                    booking.status = "Pending";
                }
                else
                {
                    booking.status = "Confirmed";

                    var newRoom = db.rooms.Find(booking.room_id);
                    if (newRoom != null)
                    {
                        newRoom.status = "Booked";
                        db.Entry(newRoom).State = EntityState.Modified;
                    }

                    // Free the previous room if changed AND the original booking was confirmed
                    if (booking.room_id != previousRoomId && originalStatus == "Confirmed")
                    {
                        var oldRoom = db.rooms.Find(previousRoomId);
                        if (oldRoom != null)
                        {
                            // Check for any OTHER confirmed bookings in the future
                            bool hasOtherConfirmedBookings = db.bookings.Any(b =>
                                b.room_id == previousRoomId &&
                                b.id != booking.id &&
                                b.status == "Confirmed" &&
                                b.check_out > DateTime.Now);

                            if (!hasOtherConfirmedBookings)
                            {
                                oldRoom.status = "Available";
                                db.Entry(oldRoom).State = EntityState.Modified;
                            }
                        }
                    }
                }

                db.Entry(booking).State = EntityState.Modified;
                db.SaveChanges();

                return userRole == "Admin" ? RedirectToAction("AdminIndex") : RedirectToAction("GuestIndex");
            }

            ViewBag.guest_id = new SelectList(db.users, "id", "name", booking.guest_id);
            ViewBag.room_id = new SelectList(db.rooms, "id", "number", booking.room_id);
            return View(booking);
        }


        public ActionResult ConfirmBooking(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }

            var booking = db.bookings.Find(id);
            if (booking == null)
            {
                return HttpNotFound();
            }

            if (booking.status == "Pending")
            {
                booking.status = "Confirmed";
                db.Entry(booking).State = EntityState.Modified; 
            }

            var room = db.rooms.Find(booking.room_id);
            if (room != null)
            {
                room.status = "Booked";
                db.Entry(room).State = EntityState.Modified;
            }

            db.SaveChanges();

            return RedirectToAction("AdminIndex");
        }


        public ActionResult DeleteConfirmed(int id)
        {
            var userRole = Session["UserRole"]?.ToString();

            var booking = db.bookings.Find(id);

            if (booking != null)
            {
                int roomId = booking.room_id;

                booking.status = "Cancelled";
                db.Entry(booking).State = EntityState.Modified;
                db.SaveChanges();

                // Check for other CONFIRMED future bookings for the same room
                bool hasOtherConfirmedBookings = db.bookings.Any(b =>
                    b.room_id == roomId &&
                    b.id != id &&
                    b.status == "Confirmed" &&
                    b.check_out > DateTime.Now);

                // If no other confirmed future bookings exist, free the room
                if (!hasOtherConfirmedBookings)
                {
                    var room = db.rooms.Find(roomId);
                    if (room != null)
                    {
                        room.status = "Available";
                        db.Entry(room).State = EntityState.Modified;
                        db.SaveChanges();
                    }
                }
            }

            return userRole == "Admin" ? RedirectToAction("AdminIndex") : RedirectToAction("GuestIndex");
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
