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
    public class billsController : Controller
    {
        private HotelEntities db = new HotelEntities();

        public ActionResult AdminIndex()
        {
            CancelOldUnpaidBookings();
            var bills = db.bills.Include(b => b.booking);
            return View(bills.ToList());
        }

        public ActionResult GuestIndex()
        {
            CancelOldUnpaidBookings();

            int userId = Convert.ToInt32(Session["UserId"]);

            var unpaidBills = (from b in db.bills
                               join bk in db.bookings on b.booking_id equals bk.id
                               where bk.guest_id == userId && b.status == "unpaid" && bk.status != "Cancelled"
                               select b)
                              .Include(b => b.booking)
                              .ToList();

            return View(unpaidBills);
        }

        private void CancelOldUnpaidBookings()
        {
            var twoDaysAgo = DateTime.Now.AddDays(-2);

            var unpaidOldBills = db.bills
                .Where(b => b.status == "unpaid" && b.generated_at <= twoDaysAgo)
                .Include(b => b.booking)
                .ToList();

            foreach (var bill in unpaidOldBills)
            {
                var booking = bill.booking;
                if (booking != null && booking.status != "Cancelled")
                {
                    booking.status = "Cancelled";
                    db.Entry(booking).State = EntityState.Modified;

                    var room = db.rooms.Find(booking.room_id);
                    if (room != null)
                    {
                        bool hasOtherActiveBookings = db.bookings.Any(bk =>
                            bk.room_id == room.id &&
                            bk.id != booking.id &&
                            bk.status == "Confirmed" &&      
                            bk.check_out > DateTime.Now);

                        if (!hasOtherActiveBookings && room.status != "Available")
                        {
                            room.status = "Available";
                            db.Entry(room).State = EntityState.Modified;
                        }
                    }
                }
            }
            db.SaveChanges();
        }


        public ActionResult Details(int id) 
        {
            var billDetails = (from b in db.bills
                               join bk in db.bookings on b.booking_id equals bk.id
                               join u in db.users on bk.guest_id equals u.id
                               join r in db.rooms on bk.room_id equals r.id
                               where b.id == id
                               select new BillView
                               {
                                   id = b.id,
                                   booking_id = b.booking_id,
                                   name = u.name,
                                   room_charges = b.room_charges,
                                   other_charges = b.other_charges,
                                   total_amount = b.total_amount,
                                   generated_at = b.generated_at
                               }).FirstOrDefault();

            if (billDetails == null)
            {
                return HttpNotFound();
            }

            return View(billDetails);
        }


        public ActionResult Create(int id) 
        {
            var booking = db.bookings.Include(b => b.room).Include(b => b.user).FirstOrDefault(b => b.id == id);
            if (booking == null)
                return HttpNotFound();

            if (db.bills.Any(b => b.booking_id == id))
            {
                var existingBill = db.bills.First(b => b.booking_id == id);
                return RedirectToAction("Details", new { id = existingBill.id });
            }

            decimal days = (decimal)(booking.check_out - booking.check_in).TotalDays;
            decimal roomCharges = booking.room.price * days;
            decimal otherCharges = roomCharges * 0.15m; 
            decimal total = roomCharges + otherCharges;

            var bill = new bill
            {
                booking_id = booking.id,
                room_charges = roomCharges,
                other_charges = otherCharges,
                total_amount = total,
                generated_at = DateTime.Now,
                status = "unpaid"
            };

            db.bills.Add(bill);
            db.SaveChanges();

            return RedirectToAction("Details", new { id = bill.id });
        }

        public ActionResult PayBill(int id)
        {
            var bill = db.bills.Find(id);
            if (bill == null)
            {
                return HttpNotFound();
            }

            bill.status = "paid";
            db.SaveChanges();

            return RedirectToAction("GuestIndex"); 
        }
        public ActionResult Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            bill bill = db.bills.Find(id);
            if (bill == null)
            {
                return HttpNotFound();
            }
            ViewBag.booking_id = new SelectList(db.bookings, "id", "status", bill.booking_id);
            return View(bill);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "id,booking_id,room_charges,other_charges,total_amount,generated_at")] bill bill)
        {
            if (ModelState.IsValid)
            {
                db.Entry(bill).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("AdminIndex");
            }
            ViewBag.booking_id = new SelectList(db.bookings, "id", "status", bill.booking_id);
            return View(bill);
        }

        public ActionResult Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            bill bill = db.bills.Find(id);
            if (bill == null)
            {
                return HttpNotFound();
            }
            return View(bill);
        }


        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            bill bill = db.bills.Find(id);
            db.bills.Remove(bill);
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
