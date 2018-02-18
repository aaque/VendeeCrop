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
    public class FarmerModelsController : Controller
    {
        private ApplicationDbContext db = new ApplicationDbContext();

        
        public ActionResult Login()
        {
            if (Session["FarmerModel"] != null)
            {
                return RedirectToAction("Index", "Home");
            }
            return View();
        }

        public ActionResult Logout()
        {
            Session["FarmerModel"] = null;
            Session["ErrorLogin"] = "";
            return RedirectToAction("Login");
        }

        [HttpPost]
        public ActionResult LoginNow(string txtPhoneNumber, string txtPassword)
        {
            string errorMessage = "";
            FarmerModel farmer = db.FarmerModels.Where(b => b.PhoneNumber == txtPhoneNumber && b.Password == txtPassword).SingleOrDefault();
            if (farmer != null)
            {
                Session["ErrorLogin"] = "";
                Session["FarmerModel"] = farmer;
                return RedirectToAction("Index", "Home");
            }
            else
            {
                errorMessage = "Invalid credentials";

            }
            Session["FarmerModel"] = null;
            Session["ErrorLogin"] = errorMessage;
            return RedirectToAction("Login");
        }

        // GET: FarmerModels
        public ActionResult Index()
        {
            return View(db.FarmerModels.ToList());
        }

        // GET: FarmerModels/Details/5
        public ActionResult Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            FarmerModel farmerModel = db.FarmerModels.Find(id);
            if (farmerModel == null)
            {
                return HttpNotFound();
            }
            return View(farmerModel);
        }

        // GET: FarmerModels/Create
        public ActionResult Create()
        {
            return View();
        }

        // POST: FarmerModels/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "Id,PhoneNumber,FirstName,LastName,Password,ConfirmPassword,Address,IsActive,IsApproved")] FarmerModel farmerModel)
        {
            if (ModelState.IsValid)
            {
                db.FarmerModels.Add(farmerModel);
                db.SaveChanges();
                //return RedirectToAction("Index");
            }

            Session["FarmerModel"] = farmerModel;
            return RedirectToAction("Login");
            //return View(farmerModel);
        }

        // GET: FarmerModels/Edit/5
        public ActionResult Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            FarmerModel farmerModel = db.FarmerModels.Find(id);
            if (farmerModel == null)
            {
                return HttpNotFound();
            }
            return View(farmerModel);
        }

        // POST: FarmerModels/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "Id,PhoneNumber,FirstName,LastName,Password,ConfirmPassword,Address,IsActive,IsApproved")] FarmerModel farmerModel)
        {
            if (ModelState.IsValid)
            {
                db.Entry(farmerModel).State = EntityState.Modified;
                db.SaveChanges();
                //return RedirectToAction("Index");
                return RedirectToAction("Index", "Home");
            }
            return View(farmerModel);
        }

        // GET: FarmerModels/Delete/5
        public ActionResult Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            FarmerModel farmerModel = db.FarmerModels.Find(id);
            if (farmerModel == null)
            {
                return HttpNotFound();
            }
            return View(farmerModel);
        }

        // POST: FarmerModels/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            FarmerModel farmerModel = db.FarmerModels.Find(id);
            db.FarmerModels.Remove(farmerModel);
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
