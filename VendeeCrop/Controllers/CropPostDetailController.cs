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
    public class CropPostDetailController : Controller
    {
        private ApplicationDbContext db = new ApplicationDbContext();

        // GET: CropPostDetail
        public ActionResult Index()
        {
            var cropPostDetailModels = db.CropPostDetailModels.Include(c => c.Crop).Include(c => c.CropPost);
            return View(cropPostDetailModels.ToList());
        }

        // GET: CropPostDetail/Details/5
        public ActionResult Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            CropPostDetailModel cropPostDetailModel = db.CropPostDetailModels.Find(id);
            if (cropPostDetailModel == null)
            {
                return HttpNotFound();
            }
            return View(cropPostDetailModel);
        }

        // GET: CropPostDetail/Create
        public ActionResult Create()
        {
            ViewBag.CropId = new SelectList(db.CropModels, "Id", "Name");
            ViewBag.CropPostId = new SelectList(db.CropPostModels, "CropPostId", "Title");
            return View();
        }

        // POST: CropPostDetail/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "CropPostDetailId,Details,CropPostId,CropId,Price,Status,Created")] CropPostDetailModel cropPostDetailModel)
        {
            if (ModelState.IsValid)
            {
                db.CropPostDetailModels.Add(cropPostDetailModel);
                db.SaveChanges();
                return RedirectToAction("Index");
            }

            ViewBag.CropId = new SelectList(db.CropModels, "Id", "Name", cropPostDetailModel.CropId);
            ViewBag.CropPostId = new SelectList(db.CropPostModels, "CropPostId", "Title", cropPostDetailModel.CropPostId);
            return View(cropPostDetailModel);
        }

        // GET: CropPostDetail/Edit/5
        public ActionResult Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            CropPostDetailModel cropPostDetailModel = db.CropPostDetailModels.Find(id);
            if (cropPostDetailModel == null)
            {
                return HttpNotFound();
            }
            ViewBag.CropId = new SelectList(db.CropModels, "Id", "Name", cropPostDetailModel.CropId);
            ViewBag.CropPostId = new SelectList(db.CropPostModels, "CropPostId", "Title", cropPostDetailModel.CropPostId);
            return View(cropPostDetailModel);
        }

        // POST: CropPostDetail/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "CropPostDetailId,Details,CropPostId,CropId,Price,Status,Created")] CropPostDetailModel cropPostDetailModel)
        {
            if (ModelState.IsValid)
            {
                db.Entry(cropPostDetailModel).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            ViewBag.CropId = new SelectList(db.CropModels, "Id", "Name", cropPostDetailModel.CropId);
            ViewBag.CropPostId = new SelectList(db.CropPostModels, "CropPostId", "Title", cropPostDetailModel.CropPostId);
            return View(cropPostDetailModel);
        }

        // GET: CropPostDetail/Delete/5
        public ActionResult Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            CropPostDetailModel cropPostDetailModel = db.CropPostDetailModels.Find(id);
            if (cropPostDetailModel == null)
            {
                return HttpNotFound();
            }
            return View(cropPostDetailModel);
        }

        // POST: CropPostDetail/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            CropPostDetailModel cropPostDetailModel = db.CropPostDetailModels.Find(id);
            db.CropPostDetailModels.Remove(cropPostDetailModel);
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
