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
    public class BuyerModelsController : Controller
    {
        private ApplicationDbContext db = new ApplicationDbContext();

        public ActionResult Login()
        {
            if (Session["UserModel"] != null)
            {
                return RedirectToAction("Index", "Home");
            }
            return View();
        }

        public ActionResult Logout()
        {
            Session["UserModel"] = null;
            Session["ErrorLogin"] = "";
            return RedirectToAction("Login");
        }

        [HttpPost]
        public ActionResult LoginNow(string txtPhoneNumber, string txtPassword)
        {
            string errorMessage = "";
            //BuyerModel buyer = db.BuyerModels.Where(b => b.PhoneNumber == txtPhoneNumber && b.Password == txtPassword).SingleOrDefault();
            UserModel user = db.UserModels.Where(b => b.PhoneNumber == txtPhoneNumber && b.Password == txtPassword && b.Type == "Buyer").SingleOrDefault();
            if (user != null)
            {
                Session["ErrorLogin"] = "";
                Session["UserModel"] = user;
                return RedirectToAction("Index", "Home");
            }
            else
            {
                errorMessage = "Invalid credentials";

            }
            Session["UserModel"] = null;
            Session["ErrorLogin"] = errorMessage;
            return RedirectToAction("Login");
        }

        // GET: BuyerModels
        public ActionResult Index()
        {
            return View(db.BuyerModels.ToList());
        }

        // GET: BuyerModels/Details/5
        public ActionResult Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            BuyerModel buyerModel = db.BuyerModels.Find(id);
            if (buyerModel == null)
            {
                return HttpNotFound();
            }
            return View(buyerModel);
        }

        // GET: BuyerModels/Create
        public ActionResult Create()
        {
            return View();
        }

        // POST: BuyerModels/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "Id,PhoneNumber,ImagePath,FirstName,LastName,Password,ConfirmPassword,StoreName,BusinessAddress,IsActive,IsApproved")] BuyerModel buyerModel, HttpPostedFileBase file)
        {
            UserModel user = new UserModel();
            if (ModelState.IsValid)
            {
                if (file != null)
                {
                    string filename = Guid.NewGuid().ToString("N") + file.FileName;
                    file.SaveAs(HttpContext.Server.MapPath("~/Images/")
                                                          + filename);
                    buyerModel.ImagePath = filename;
                }
                

                user.PhoneNumber = buyerModel.PhoneNumber;
                user.FirstName = buyerModel.FirstName;
                user.LastName = buyerModel.LastName;
                user.Type = "Buyer";
                user.Password = buyerModel.Password;
                user.ConfirmPassword = buyerModel.ConfirmPassword;
                user.StoreName = buyerModel.StoreName;
                user.Address = buyerModel.BusinessAddress;
                user.ImagePath = buyerModel.ImagePath;

                //db.BuyerModels.Add(buyerModel);
                db.UserModels.Add(user);
                db.SaveChanges();
                //return RedirectToAction("Index");
            }
            Session["UserModel"] = user;
            return RedirectToAction("Login");
            //return View(buyerModel);
        }

        // GET: BuyerModels/Edit/5
        public ActionResult Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            BuyerModel buyerModel = db.BuyerModels.Find(id);
            if (buyerModel == null)
            {
                return HttpNotFound();
            }
            return View(buyerModel);
        }

        // POST: BuyerModels/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "Id,PhoneNumber,ImagePath,FirstName,LastName,Password,ConfirmPassword,StoreName,BusinessAddress,IsActive,IsApproved")] BuyerModel buyerModel, HttpPostedFileBase file)
        {
            if (ModelState.IsValid)
            {
                if (file != null)
                {
                    string filename = Guid.NewGuid().ToString("N") + file.FileName;
                    file.SaveAs(HttpContext.Server.MapPath("~/Images/")
                                                          + filename);
                    buyerModel.ImagePath = filename;
                }
                db.Entry(buyerModel).State = EntityState.Modified;
                db.SaveChanges();
                //return RedirectToAction("Index");
                return RedirectToAction("Index", "Home");
            }
            return View(buyerModel);
        }

        // GET: BuyerModels/Delete/5
        public ActionResult Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            BuyerModel buyerModel = db.BuyerModels.Find(id);
            if (buyerModel == null)
            {
                return HttpNotFound();
            }
            return View(buyerModel);
        }

        // POST: BuyerModels/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            BuyerModel buyerModel = db.BuyerModels.Find(id);
            db.BuyerModels.Remove(buyerModel);
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
