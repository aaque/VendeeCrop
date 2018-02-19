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
    public class CropTypeController : Controller
    {
        private ApplicationDbContext db = new ApplicationDbContext();

        // GET: CropType
        public ActionResult Index()
        {
            return View(db.CropTypeModels.ToList());
        }

        // GET: CropType/Details/5
        public ActionResult Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            CropTypeModel cropTypeModel = db.CropTypeModels.Find(id);
            if (cropTypeModel == null)
            {
                return HttpNotFound();
            }
            return View(cropTypeModel);
        }

        // GET: CropType/Create
        public ActionResult Create()
        {
            return View();
        }

        // POST: CropType/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "CropTypeId,Type")] CropTypeModel cropTypeModel)
        {
            if (ModelState.IsValid)
            {
                db.CropTypeModels.Add(cropTypeModel);
                db.SaveChanges();
                return RedirectToAction("Index");
            }

            return View(cropTypeModel);
        }

        // GET: CropType/Edit/5
        public ActionResult Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            CropTypeModel cropTypeModel = db.CropTypeModels.Find(id);
            if (cropTypeModel == null)
            {
                return HttpNotFound();
            }
            return View(cropTypeModel);
        }

        // POST: CropType/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "CropTypeId,Type")] CropTypeModel cropTypeModel)
        {
            if (ModelState.IsValid)
            {
                db.Entry(cropTypeModel).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            return View(cropTypeModel);
        }

        // GET: CropType/Delete/5
        public ActionResult Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            CropTypeModel cropTypeModel = db.CropTypeModels.Find(id);
            if (cropTypeModel == null)
            {
                return HttpNotFound();
            }
            return View(cropTypeModel);
        }

        // POST: CropType/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            CropTypeModel cropTypeModel = db.CropTypeModels.Find(id);
            db.CropTypeModels.Remove(cropTypeModel);
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
