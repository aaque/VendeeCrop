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
        public ActionResult Index(int? id)
        {
            PostDetailsViewModel vm = new PostDetailsViewModel();
            vm.CropPostId = id;
            if (id == null)
            {
                vm.CropPost = db.CropPostModels.First();
                var cropPostDetailModels = db.CropPostDetailModels.Include(c => c.Crop).Include(c => c.CropPost);
                //ICollection<PostImageModel> cropPostImages = new ICollection<PostImageModel>();
                vm.CropPostDetails = (ICollection<CropPostDetailModel>)cropPostDetailModels.ToList();
                //vm.PostImages = cropPostImages;
                //return View(cropPostDetailModels.ToList());
                return View(vm);
            }
            else
            {
                vm.CropPost = db.CropPostModels.Find(id);
                var cropPostDetailModels = db.CropPostDetailModels.Where(c => c.CropPostId == id).Include(c => c.Crop).Include(c => c.CropPost);
                ICollection<CropPostImageModel> cropPostImages = db.CropPostImageModels.Where(p => p.CropPostId == id).Include(c => c.CropPost).ToList();
                vm.PostImages = cropPostImages;
                vm.CropPostDetails = (ICollection<CropPostDetailModel>)cropPostDetailModels.ToList();
                return View(vm);
            }
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
        public ActionResult Create(int? id)
        {
            ViewBag.CropId = new SelectList(db.CropModels, "Id", "Name");
            if (id == null)
            {
                Session["CropPostId"] = new SelectList(db.CropPostModels, "CropPostId", "Title");
            }
            else
            {
                Session["CropPostId"] = id;
            }
            return View();
        }

        // POST: CropPostDetail/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "CropPostDetailId,Details,CropPostId,CropId,Price,Status,Created")] CropPostDetailModel cropPostDetailModel)
        {
            if (ModelState.IsValid)
            {
                if (Session["CropPostId"] != null)
                {
                    cropPostDetailModel.CropPostId = (int)Session["CropPostId"];
                }
                db.CropPostDetailModels.Add(cropPostDetailModel);
                db.SaveChanges();
                return RedirectToAction("Index", new {id = (int)Session["CropPostId"] });
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
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
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
