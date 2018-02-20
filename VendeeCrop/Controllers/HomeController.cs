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
    public class HomeController : Controller
    {
        private ApplicationDbContext db = new ApplicationDbContext();
        public ActionResult Admin()
        {
            return RedirectToAction("Login","Account");
        }

        public ActionResult LoginIndex()
        {
            Session["ErrorLogin"] = "";
            return View();
        }

        public ActionResult Index()
        {
            if (Session["BuyerModel"] == null && Session["FarmerModel"] == null && !Request.IsAuthenticated)
            {
                return RedirectToAction("LoginIndex");
            }
            var cropPostModels = db.CropPostModels.Include(c => c.User);
            return View(cropPostModels.ToList());
        }

        public ActionResult About()
        {
            ViewBag.Message = "Your application description page.";

            return View();
        }

        public ActionResult Contact()
        {
            ViewBag.Message = "Your contact page.";

            return View();
        }
    }
}