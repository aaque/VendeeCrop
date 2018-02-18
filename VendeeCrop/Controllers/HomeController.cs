using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace VendeeCrop.Controllers
{
    public class HomeController : Controller
    {
        public ActionResult LoginIndex()
        {
            Session["ErrorLogin"] = "";
            return View();
        }

        public ActionResult Index()
        {
            if (Session["BuyerModel"] == null && Session["FarmerModel"] == null)
            {
                return RedirectToAction("LoginIndex");
            }

                return View();
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