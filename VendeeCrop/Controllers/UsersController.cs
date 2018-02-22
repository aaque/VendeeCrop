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
    public class UsersController : Controller
    {
        private ApplicationDbContext db = new ApplicationDbContext();

        public ActionResult Login()
        {
            if (Session["UserModel"] != null)
            {
                return RedirectToAction("LoginIndex", "Home");
            }
            return View();
        }

        public ActionResult Logout()
        {
            Session["UserModel"] = null;
            Session["ErrorLogin"] = "";
            return RedirectToAction("LoginIndex", "Home");
        }

        // GET: Users
        public ActionResult Index()
        {
            return View(db.UserModels.ToList());
        }

        // GET: Users/Details/5
        public ActionResult Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            UserModel userModel = db.UserModels.Find(id);
            if (userModel == null)
            {
                return HttpNotFound();
            }
            return View(userModel);
        }

        // GET: Users/Create
        public ActionResult Create()
        {
            return View();
        }

        // POST: Users/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "Id,PhoneNumber,FirstName,LastName,Type,Password,ConfirmPassword,StoreName,Address,ImagePath,IsActive,IsApproved")] UserModel userModel)
        {
            if (ModelState.IsValid)
            {
                db.UserModels.Add(userModel);
                db.SaveChanges();
                return RedirectToAction("Index");
            }

            return View(userModel);
        }

        // GET: Users/Edit/5
        public ActionResult Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            UserModel userModel = db.UserModels.Find(id);
            if (userModel == null)
            {
                return HttpNotFound();
            }
            return View(userModel);
        }

        // POST: Users/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "Id,PhoneNumber,FirstName,LastName,Type,Password,ConfirmPassword,StoreName,Address,ImagePath,IsActive,IsApproved")] UserModel userModel, HttpPostedFileBase file)
        {
            if (ModelState.IsValid)
            {
                var old_User = db.UserModels.Find(userModel.Id);
                if (file != null)
                {
                    string filename = Guid.NewGuid().ToString("N") + file.FileName;
                    file.SaveAs(HttpContext.Server.MapPath("~/Images/")
                                                          + filename);
                    userModel.ImagePath = filename;
                }
                else
                {
                    userModel.ImagePath = old_User.ImagePath;
                }

                if (!Request.IsAuthenticated)
                {
                    userModel.IsActive = old_User.IsActive;
                    userModel.IsApproved = old_User.IsApproved;
                }

                userModel.Type = old_User.Type;

                db.Entry(userModel).State = EntityState.Modified;
                db.SaveChanges();
                return Redirect(Request.UrlReferrer.ToString());
            }
            return View(userModel);
        }

        // GET: Users/Delete/5
        public ActionResult Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            UserModel userModel = db.UserModels.Find(id);
            if (userModel == null)
            {
                return HttpNotFound();
            }
            return View(userModel);
        }

        // POST: Users/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            UserModel userModel = db.UserModels.Find(id);
            db.UserModels.Remove(userModel);
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
