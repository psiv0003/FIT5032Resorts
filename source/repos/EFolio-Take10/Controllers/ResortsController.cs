using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.Linq;
using System.Net;
using System.Web;
using System.Web.Mvc;
using EFolio_Take10.Models;
using System.IO;

namespace EFolio_Take10.Controllers
{
    public class ResortsController : Controller
    {
        private ResortEntities db = new ResortEntities();

        // GET: Resorts
        public ActionResult Index()
        {
            return View(db.Resorts.ToList());
        }

        //get resorts for admin index
        public ActionResult AdminIndex()
        {
            return View(db.Resorts.ToList());
        }

        // GET: Resorts/Details/5
        public ActionResult Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Resort resort = db.Resorts.Find(id);
            if (resort == null)
            {
                return HttpNotFound();
            }
            return View(resort);
        }

        // GET: Resorts/Create
        public ActionResult Create()
        {
            return View();
        }

        // POST: Resorts/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "Id,Name,Description,Address,ImgURL,Rating,ResortLat,ResortLong")] Resort resort, HttpPostedFileBase
postedFile)
        {
            ModelState.Clear();
            var myUniqueFileName = string.Format(@"{0}", Guid.NewGuid());
            resort.ImgURL = myUniqueFileName;
            TryValidateModel(resort);

            if (ModelState.IsValid)
            {
                string serverPath = Server.MapPath("~/Upload/");
                string fileExtension = Path.GetExtension(postedFile.FileName);
                string filePath = resort.ImgURL + fileExtension;
                resort.ImgURL = filePath;
                postedFile.SaveAs(serverPath + resort.ImgURL);

                db.Resorts.Add(resort);
                db.SaveChanges();
                return RedirectToAction("Index");
            }

            return View(resort);
        }

        // GET: Resorts/Edit/5
        public ActionResult Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Resort resort = db.Resorts.Find(id);
            if (resort == null)
            {
                return HttpNotFound();
            }
            return View(resort);
        }

        // POST: Resorts/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "Id,Name,Description,Address,ImgURL,Rating,ResortLat,ResortLong")] Resort resort)
        {
            if (ModelState.IsValid)
            {
                db.Entry(resort).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            return View(resort);
        }

        // GET: Resorts/Delete/5
        public ActionResult Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Resort resort = db.Resorts.Find(id);
            if (resort == null)
            {
                return HttpNotFound();
            }
            return View(resort);
        }

        // POST: Resorts/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            Resort resort = db.Resorts.Find(id);
            db.Resorts.Remove(resort);
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
