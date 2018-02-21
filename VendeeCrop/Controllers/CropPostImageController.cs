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
    public class CropPostImageController : Controller
    {
        private ApplicationDbContext db = new ApplicationDbContext();

        // GET: CropPostImage
        public ActionResult Index()
        {
            var cropPostImageModels = db.CropPostImageModels.Include(c => c.CropPost);
            return View(cropPostImageModels.ToList());
        }

        // GET: CropPostImage/Details/5
        public ActionResult Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            CropPostImageModel cropPostImageModel = db.CropPostImageModels.Find(id);
            if (cropPostImageModel == null)
            {
                return HttpNotFound();
            }
            return View(cropPostImageModel);
        }

        // GET: CropPostImage/Create
        public ActionResult Create(int id)
        {
            Session["CropPostId"] = id;
            ViewBag.CropPostId = new SelectList(db.CropPostModels, "CropPostId", "Title");
            return View();
        }

        // POST: CropPostImage/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "ImageId,ImageName,Image,CropPostId,Created")] CropPostImageModel cropPostImageModel, HttpPostedFileBase file)
        {
            if (ModelState.IsValid)
            {
                if (file != null)
                {
                    string filename = Guid.NewGuid().ToString("N") + file.FileName;
                    file.SaveAs(HttpContext.Server.MapPath("~/Images/")
                                                          + filename);
                    cropPostImageModel.ImageName = file.FileName;
                    cropPostImageModel.Image = filename;

                    cropPostImageModel.CropPostId = (int)Session["CropPostId"];
                    db.CropPostImageModels.Add(cropPostImageModel);
                    db.SaveChanges();
                    return RedirectToAction("Index","CropPostDetail",new { id = cropPostImageModel.CropPostId});
                }
            }

            ViewBag.CropPostId = new SelectList(db.CropPostModels, "CropPostId", "Title", cropPostImageModel.CropPostId);
            return View(cropPostImageModel);
        }

        // GET: CropPostImage/Edit/5
        public ActionResult Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            CropPostImageModel cropPostImageModel = db.CropPostImageModels.Find(id);
            if (cropPostImageModel == null)
            {
                return HttpNotFound();
            }
            ViewBag.CropPostId = new SelectList(db.CropPostModels, "CropPostId", "Title", cropPostImageModel.CropPostId);
            return View(cropPostImageModel);
        }

        // POST: CropPostImage/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "ImageId,ImageName,Image,CropPostId,Created")] CropPostImageModel cropPostImageModel)
        {
            if (ModelState.IsValid)
            {
                db.Entry(cropPostImageModel).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            ViewBag.CropPostId = new SelectList(db.CropPostModels, "CropPostId", "Title", cropPostImageModel.CropPostId);
            return View(cropPostImageModel);
        }

        // GET: CropPostImage/Delete/5
        public ActionResult Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            CropPostImageModel cropPostImageModel = db.CropPostImageModels.Find(id);
            if (cropPostImageModel == null)
            {
                return HttpNotFound();
            }
            return View(cropPostImageModel);
        }

        // POST: CropPostImage/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            CropPostImageModel cropPostImageModel = db.CropPostImageModels.Find(id);
            db.CropPostImageModels.Remove(cropPostImageModel);
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
