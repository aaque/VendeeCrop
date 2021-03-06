﻿using System;
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
    public class CropPostController : Controller
    {
        private ApplicationDbContext db = new ApplicationDbContext();

        // GET: CropPost
        public ActionResult Index()
        {
            var UserModel = (UserModel)Session["UserModel"];
            var cropPostModels = db.CropPostModels.Where(c=> c.UserModelId == UserModel.Id).Include(c => c.User);
            return View(cropPostModels.ToList());
        }

        // GET: CropPost/Details/5
        public ActionResult Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            CropPostModel cropPostModel = db.CropPostModels.Find(id);
            if (cropPostModel == null)
            {
                return HttpNotFound();
            }
            return View(cropPostModel);
        }

        // GET: CropPost/Create
        public ActionResult Create()
        {
            ViewBag.UserModelId = new SelectList(db.UserModels, "Id", "PhoneNumber");
            return View();
        }

        // POST: CropPost/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "CropPostId,Title,Description,UserModelId,Type")] CropPostModel cropPostModel)
        {
            if (ModelState.IsValid)
            {
                UserModel user = (UserModel)Session["UserModel"];
                cropPostModel.UserModelId = user.Id;
                if (user.Type == "Farmer")
                {
                    cropPostModel.Type = "Crop";
                }
                db.CropPostModels.Add(cropPostModel);
                db.SaveChanges();
                return RedirectToAction("Index");
            }

            ViewBag.UserModelId = new SelectList(db.UserModels, "Id", "PhoneNumber", cropPostModel.UserModelId);
            return View(cropPostModel);
        }

        // GET: CropPost/Edit/5
        public ActionResult Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            CropPostModel cropPostModel = db.CropPostModels.Find(id);
            if (cropPostModel == null)
            {
                return HttpNotFound();
            }
            ViewBag.UserModelId = new SelectList(db.UserModels, "Id", "PhoneNumber", cropPostModel.UserModelId);
            return View(cropPostModel);
        }

        // POST: CropPost/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "CropPostId,Title,Description,UserModelId,Type")] CropPostModel cropPostModel)
        {
            if (ModelState.IsValid)
            {
                db.Entry(cropPostModel).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            ViewBag.UserModelId = new SelectList(db.UserModels, "Id", "PhoneNumber", cropPostModel.UserModelId);
            return View(cropPostModel);
        }

        // GET: CropPost/Delete/5
        public ActionResult Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            CropPostModel cropPostModel = db.CropPostModels.Find(id);
            if (cropPostModel == null)
            {
                return HttpNotFound();
            }
            return View(cropPostModel);
        }

        // POST: CropPost/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            CropPostModel cropPostModel = db.CropPostModels.Find(id);
            db.CropPostModels.Remove(cropPostModel);
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
