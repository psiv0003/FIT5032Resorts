﻿using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.Linq;
using System.Net;
using System.Web;
using System.Web.Mvc;
using EFolio_Take10.Models;
using Microsoft.AspNet.Identity;

namespace EFolio_Take10.Controllers
{
    public class BookingsController : Controller
    {
        private ResortEntities db = new ResortEntities();

        // GET: Bookings
        public ActionResult Index()
        {
            var bookings = db.Bookings.Include(b => b.AspNetUser).Include(b => b.Room);
            return View(bookings.ToList());
        }

        // GET: Bookings/Details/5
        public ActionResult Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Booking booking = db.Bookings.Find(id);
            if (booking == null)
            {
                return HttpNotFound();
            }
            return View(booking);
        }

        // GET: Bookings/Create
        public ActionResult Create()
        {
            ViewBag.GuestID = new SelectList(db.AspNetUsers, "Id", "Email");
            ViewBag.RoomID = new SelectList(db.Rooms, "Id", "Name");
            return View();
        }
        // GET: Bookings/Create
        public ActionResult BookRoom(int id)
        {
            ViewBag.GuestID = new SelectList(db.AspNetUsers, "Id", "Email");
            ViewBag.RoomID = id;
            return View();
        }

        //Create Customer Booking Request
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult BookRoom([Bind(Include = "Id,RoomID,CheckInDate,CheckOutDate,NoOfAdults,NoOfChildren,TotalCharge,Comment")] Booking booking, int id)
        {
            if (ModelState.IsValid)
            {
                db.Bookings.Add(booking);
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            ViewBag.GuestID = User.Identity.GetUserId();
            DateTime localDate = DateTime.Now;
            ViewBag.BookingDateTime = localDate;
            //ViewBag.GuestID = new SelectList(db.AspNetUsers, "Id", "Email", booking.GuestID);
            ViewBag.RoomID = new SelectList(db.Rooms, "Id", "Name", booking.RoomID);
            return View(booking);
        }


        // POST: Bookings/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "Id,BookingDateTime,RoomID,GuestID,CheckInDate,CheckOutDate,NoOfAdults,NoOfChildren,TotalCharge,Rating,Comment")] Booking booking)
        {
            if (ModelState.IsValid)
            {
                db.Bookings.Add(booking);
                db.SaveChanges();
                return RedirectToAction("Index");
            }

            ViewBag.GuestID = new SelectList(db.AspNetUsers, "Id", "Email", booking.GuestID);
            ViewBag.RoomID = new SelectList(db.Rooms, "Id", "Name", booking.RoomID);
            return View(booking);
        }

        // GET: Bookings/Edit/5
        public ActionResult Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Booking booking = db.Bookings.Find(id);
            if (booking == null)
            {
                return HttpNotFound();
            }
            ViewBag.GuestID = new SelectList(db.AspNetUsers, "Id", "Email", booking.GuestID);
            ViewBag.RoomID = new SelectList(db.Rooms, "Id", "Name", booking.RoomID);
            return View(booking);
        }

        // POST: Bookings/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "Id,BookingDateTime,RoomID,GuestID,CheckInDate,CheckOutDate,NoOfAdults,NoOfChildren,TotalCharge,Rating,Comment")] Booking booking)
        {
            if (ModelState.IsValid)
            {
                db.Entry(booking).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            ViewBag.GuestID = new SelectList(db.AspNetUsers, "Id", "Email", booking.GuestID);
            ViewBag.RoomID = new SelectList(db.Rooms, "Id", "Name", booking.RoomID);
            return View(booking);
        }

        // GET: Bookings/Delete/5
        public ActionResult Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Booking booking = db.Bookings.Find(id);
            if (booking == null)
            {
                return HttpNotFound();
            }
            return View(booking);
        }

        // POST: Bookings/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            Booking booking = db.Bookings.Find(id);
            db.Bookings.Remove(booking);
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
