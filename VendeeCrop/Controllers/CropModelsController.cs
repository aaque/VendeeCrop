using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.Linq;
using System.Net;
using System.Web;
using System.Web.Mvc;
using VendeeCrop.Models;

namespace VendeeCrop.Controllers
{
    public class CropModelsController : Controller
    {
        private ApplicationDbContext db = new ApplicationDbContext();

        // GET: CropModels
        public ActionResult Index()
        {
            var cropModels = db.CropModels.Include(c => c.CropType);
            return View(cropModels.ToList());
        }

        // GET: CropModels/Details/5
        public ActionResult Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            CropModel cropModel = db.CropModels.Find(id);
            if (cropModel == null)
            {
                return HttpNotFound();
            }
            return View(cropModel);
        }

        // GET: CropModels/Create
        public ActionResult Create()
        {
            ViewBag.CropTypeId = new SelectList(db.CropTypeModels, "CropTypeId", "Type");
            return View();
        }

        // POST: CropModels/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "Id,Name,CropTypeId")] CropModel cropModel)
        {
            if (ModelState.IsValid)
            {
                db.CropModels.Add(cropModel);
                db.SaveChanges();
                return RedirectToAction("Index");
            }

            ViewBag.CropTypeId = new SelectList(db.CropTypeModels, "CropTypeId", "Type", cropModel.CropTypeId);
            return View(cropModel);
        }

        // GET: CropModels/Edit/5
        public ActionResult Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            CropModel cropModel = db.CropModels.Find(id);
            if (cropModel == null)
            {
                return HttpNotFound();
            }
            ViewBag.CropTypeId = new SelectList(db.CropTypeModels, "CropTypeId", "Type", cropModel.CropTypeId);
            return View(cropModel);
        }

        // POST: CropModels/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "Id,Name,CropTypeId")] CropModel cropModel)
        {
            if (ModelState.IsValid)
            {
                db.Entry(cropModel).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            ViewBag.CropTypeId = new SelectList(db.CropTypeModels, "CropTypeId", "Type", cropModel.CropTypeId);
            return View(cropModel);
        }

        // GET: CropModels/Delete/5
        public ActionResult Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            CropModel cropModel = db.CropModels.Find(id);
            if (cropModel == null)
            {
                return HttpNotFound();
            }
            return View(cropModel);
        }

        // POST: CropModels/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            CropModel cropModel = db.CropModels.Find(id);
            db.CropModels.Remove(cropModel);
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
