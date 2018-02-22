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
            if (Session["UserModel"] == null && !Request.IsAuthenticated)
            {
                return RedirectToAction("LoginIndex");
            }
            var cropPostModels = db.CropPostModels.Include(c => c.User);
            return View(cropPostModels.ToList());
        }

        public ActionResult Messages()
        {
            if (Session["UserModel"] == null && !Request.IsAuthenticated)
            {
                return RedirectToAction("LoginIndex");
            }
            var UserModel = (UserModel)Session["UserModel"];
            var MyMessages = db.MessageModels.Where(m => m.ToUserID == UserModel.Id).Include(e => e.FromUser).GroupBy(mm => mm.FromUserId);
            return View(MyMessages.ToList());
        }

        public  ActionResult MessageNew(int id)
        {
            
            NewMessageViewModel messageVM = new NewMessageViewModel();
            
            UserModel currentUser = (UserModel)Session["UserModel"];
            messageVM.FromUserModel = currentUser;
            messageVM.ToUserModel = db.UserModels.Find(id);
            var msg = db.MessageModels.Where(mm => (mm.FromUserId == currentUser.Id && mm.ToUserID == id) || (mm.FromUserId == id && mm.ToUserID == currentUser.Id));
            messageVM.Messages = msg.ToList();
            return View(messageVM);
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