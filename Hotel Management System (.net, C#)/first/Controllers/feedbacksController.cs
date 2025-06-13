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
    public class feedbacksController : Controller
    {
        private HotelEntities db = new HotelEntities();

        // GET: feedbacks
        public ActionResult Index()
        {
            var feedbacks = db.feedbacks.Include(f => f.booking);
            return View(feedbacks.ToList());
        }

        public ActionResult GuestIndex()
        {
            int userId = Convert.ToInt32(Session["UserID"]);

            var feedbacks = db.feedbacks
                              .Include(f => f.booking)
                              .Where(f => f.booking.guest_id == userId)
                              .ToList();

            return View(feedbacks);
        }


        public ActionResult PendingFeedbacks()
        {
            int userId = Convert.ToInt32(Session["UserID"]);
            var today = DateTime.Today;

            var bookings = db.bookings
                .Where(b => b.guest_id == userId &&
                            b.status == "Confirmed" 
                            //&& b.check_out < today
                            && !db.feedbacks.Any(f => f.booking_id == b.id))
                .ToList();

            return View(bookings);

        }

        // GET: feedbacks/Details/5
        public ActionResult Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            feedback feedback = db.feedbacks.Find(id);
            if (feedback == null)
            {
                return HttpNotFound();
            }
            return View(feedback);
        }

        // GET: feedbacks/Create
        public ActionResult Create(int bookingId)
        {
            int userId = Convert.ToInt32(Session["UserID"]);
            var today = DateTime.Today;

            var booking = db.bookings.FirstOrDefault(b => b.id == bookingId &&
                                                          b.guest_id == userId &&
                                                          b.status == "Confirmed" 
                                                          //&& b.check_out < today
                                                          );

            if (booking == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.Forbidden);
            }

            // Check if feedback already exists
            bool feedbackExists = db.feedbacks.Any(f => f.booking_id == bookingId);
            if (feedbackExists)
            {
                TempData["Message"] = "Feedback already submitted for this booking.";
                return RedirectToAction("GuestIndex");
            }

            var feedback = new feedback
            {
                booking_id = bookingId
            };

            return View(feedback);
        }




        // POST: feedbacks/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create(feedback feedback)
        {

            int userId = Convert.ToInt32(Session["UserID"]);
            var today = DateTime.Today;

            // Validate booking ownership and status again for security
            var booking = db.bookings.FirstOrDefault(b => b.id == feedback.booking_id &&
                                                          b.guest_id == userId &&
                                                          b.status == "Confirmed"
                                                        //&& b.check_out < today
                                                          );

            if (booking == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.Forbidden);
            }

            if (db.feedbacks.Any(f => f.booking_id == feedback.booking_id))
            {
                TempData["Message"] = "Feedback already submitted for this booking.";
                return RedirectToAction("GuestIndex");
            }

            if (ModelState.IsValid)
            {
                feedback.created_at = DateTime.Now;
                db.feedbacks.Add(feedback);
                db.SaveChanges();
                TempData["Success"] = "Thank you for your feedback!";
                return RedirectToAction("GuestIndex");
            }

            return View(feedback);
        }

        // GET: feedbacks/Edit/5
        public ActionResult Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            feedback feedback = db.feedbacks.Find(id);
            if (feedback == null)
            {
                return HttpNotFound();
            }
            ViewBag.booking_id = new SelectList(db.bookings, "id", "status", feedback.booking_id);
            return View(feedback);
        }

        // POST: feedbacks/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "id,message,created_at,booking_id,rating")] feedback feedback)
        {
            if (ModelState.IsValid)
            {
                db.Entry(feedback).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("GuestIndex");
            }
            ViewBag.booking_id = new SelectList(db.bookings, "id", "status", feedback.booking_id);
            return View(feedback);
        }

        // GET: feedbacks/Delete/5
        public ActionResult Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            feedback feedback = db.feedbacks.Find(id);
            if (feedback == null)
            {
                return HttpNotFound();
            }
            return View(feedback);
        }

        // POST: feedbacks/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            feedback feedback = db.feedbacks.Find(id);
            db.feedbacks.Remove(feedback);
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
