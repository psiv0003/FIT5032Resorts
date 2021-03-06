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
using Newtonsoft.Json;
using static EFolio_Take10.Models.ChartModel;

namespace EFolio_Take10.Controllers
{
    public class Bookings1Controller : Controller
    {
        private ResortEntities db = new ResortEntities();

        // GET: Bookings1
        public ActionResult Index()
        {
            var userId = User.Identity.GetUserId();
            var booking = db.Bookings.Where(s => s.GuestID == userId).ToList();

         //   var bookings = db.Bookings.Include(b => b.AspNetUser).Include(b => b.Room);
            return View(booking);
        }

        // GET: Bookings1
        public ActionResult Admin()
        {
           

            //   var bookings = db.Bookings.Include(b => b.AspNetUser).Include(b => b.Room);
            return View(db.Bookings);
        }

        public ActionResult Charts()
        {


           

            var bookings = db.Bookings;
            List<string> roomNamesArray = new List<string>();
            foreach (Booking book in bookings) {
                
                var Room = book.Room.Name;
                roomNamesArray.Add(Room);
            }

            var res = db.Bookings
                .GroupBy(l => (l.Room.Resort.Name))
                .Select(g => new
                {
                    Name = g.Key,
                    Count = g.Select(l => (l.Room.Resort.Name)).Distinct().Count()
                }
                );


            List<DataPoint> dataPoints = new List<DataPoint>();

            foreach (var v in res) {
                string name = v.Name;
                dataPoints.Add(new DataPoint(name, v.Count));
            }
           

            ViewBag.DataPoints = JsonConvert.SerializeObject(dataPoints);
            return View(db.Bookings);
        }

        // GET: Bookings1
        public ActionResult Ratings()
        {


            //   var bookings = db.Bookings.Include(b => b.AspNetUser).Include(b => b.Room);
            return View(db.Bookings);
        }
        // GET: Bookings1/Details/5
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

        // GET: Bookings1/Create
        public ActionResult Create()
        {
            ViewBag.GuestID = new SelectList(db.AspNetUsers, "Id", "Email");
            ViewBag.RoomID = new SelectList(db.Rooms, "Id", "Name");
            return View();
        }

        // POST: Bookings1/Create
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
        public ActionResult BookRoom([Bind(Include = "Id,RoomID,,NoOfAdults,NoOfChildren,TotalCharge,Comment")] Booking booking, String datepicker, String checkout, int id)
        {
            if (ModelState.IsValid)
            {
                DateTime date = DateTime.ParseExact(datepicker, "dd/mm/yyyy", null);
                DateTime date1 = DateTime.ParseExact(checkout, "dd/mm/yyyy", null);


                booking.CheckInDate = date;
                booking.CheckOutDate = date1;
                DateTime localDate = DateTime.Now;

                booking.BookingDateTime = localDate;
                booking.GuestID = User.Identity.GetUserId();
                booking.RoomID = id;
                int roomId = booking.RoomID;
                Room room = db.Rooms.Find(roomId);
                String diffDates = (date1 - date).TotalDays.ToString();

                if (int.Parse(diffDates) == 0)
                {
                    //booked for one dat
                    booking.TotalCharge = room.Price;
                }
                else if (int.Parse(diffDates) >= 1) {
                    decimal days = decimal.Parse(diffDates) + 1;
                    booking.TotalCharge = days* room.Price;

                }


                db.Bookings.Add(booking);
                db.SaveChanges();
                return RedirectToAction("Index");
            }


            //var result = db.Rooms.ToList();
            //List<Room> roomList = new List<Room>() ;
            //foreach (Room room in result) {
            //    if (room.ImgName.Equals("Active")) {
            //        roomList.Add(room);
            //    }
            //}
            //if (result != null)
            //{
                
            //    ViewBag.mySkills = result.Select(N => new SelectListItem { Text = N.Name, Value = N.Id.ToString() });
            //}
            //ViewBag.GuestID = new SelectList(db.AspNetUsers, "Id", "Email", booking.GuestID);
          //  ViewBag.RoomID = new SelectList(roomList, "Id", "Name", booking.RoomID);

            return View(booking);
        }

        // GET: Bookings1/Edit/5
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

        // POST: Bookings1/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "Id,BookingDateTime,RoomID,GuestID,CheckInDate,CheckOutDate,NoOfAdults,NoOfChildren,TotalCharge,Rating,Comment")] Booking booking, int rating, String comment)
        {
            if (ModelState.IsValid)
            {
                booking.Rating = rating;
                booking.Comment = comment;
                db.Entry(booking).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            //ViewBag.GuestID = new SelectList(db.AspNetUsers, "Id", "Email", booking.GuestID);
            //ViewBag.RoomID = new SelectList(db.Rooms, "Id", "Name", booking.RoomID);
            return View(booking);
        }

        // GET: Bookings1/Edit/5
        public ActionResult Status(int? id)
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
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Status([Bind(Include = "Id,BookingDateTime,RoomID,GuestID,CheckInDate,CheckOutDate,NoOfAdults,NoOfChildren,TotalCharge,Rating,Comment, status")] Booking booking)
        {
            if (ModelState.IsValid)
            {
               
                db.Entry(booking).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Admin");
            }
            //ViewBag.GuestID = new SelectList(db.AspNetUsers, "Id", "Email", booking.GuestID);
            //ViewBag.RoomID = new SelectList(db.Rooms, "Id", "Name", booking.RoomID);
            return View(booking);
        }
        // GET: Bookings1/Delete/5
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

        // POST: Bookings1/Delete/5
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
