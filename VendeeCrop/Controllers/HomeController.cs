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
            if (Session["UserModel"] == null && !Request.IsAuthenticated)
            {
                return RedirectToAction("LoginIndex");
            }
            var cropPostModels = db.CropPostModels.Include(c => c.User).OrderByDescending(p=>p.Created);
            return View(cropPostModels.ToList());
        }

        public ActionResult Messages()
        {
            if (Session["UserModel"] == null && !Request.IsAuthenticated)
            {
                return RedirectToAction("LoginIndex");
            }
            var UserModel = (UserModel)Session["UserModel"];
            var MyMessages = db.MessageModels
                .Where(m => m.ToUserID == UserModel.Id)
                .Include(e => e.FromUser)
                .GroupBy(g => g.FromUserId)
                .Select(s => s.OrderByDescending(c => c.Created)
                .FirstOrDefault());

            var FromMeMessages = db.MessageModels
                .Where(m => m.FromUserId == UserModel.Id)
                .Include(e => e.FromUser)
                .GroupBy(g => g.FromUserId)
                .Select(s => s.OrderByDescending(c => c.Created)
                .FirstOrDefault());

            ICollection<MessagesViewModel> mm = new List<MessagesViewModel>();

            
            foreach (var itm in FromMeMessages)
            {
                bool hasValue = false;
                bool hasChanges = false;
                int repeatingId = 0;

                foreach (var eee in mm)
                {
                    if (eee.FromUserId == itm.ToUserID)
                    {
                        hasValue = true;
                        repeatingId = (int)itm.ToUserID;
                        if(eee.MessageDateTime != itm.Created)
                        {
                            hasChanges = true;
                        }
                    }
                }
                if(hasValue)
                {
                    foreach (var occ in mm)
                    {
                        
                        if (!hasValue)
                        {
                            UserModel uu = db.UserModels.Find((int)itm.ToUserID);
                            MessagesViewModel mvm = new MessagesViewModel();
                            mvm.FromUserId = (int)itm.ToUserID;
                            mvm.MessageDateTime = itm.Created;
                            mvm.LatestMessage = itm.Message;
                            mvm.FromUser = uu;
                            mm.Add(mvm);
                        }
                        else
                        {
                            if(hasChanges)
                            {
                                occ.LatestMessage = itm.Message;
                                occ.MessageDateTime = itm.Created;
                            }
                        }
                    }
                }
                else
                {
                    UserModel uu = db.UserModels.Find((int)itm.ToUserID);
                    MessagesViewModel mvm = new MessagesViewModel();
                    mvm.FromUserId = (int)itm.ToUserID;
                    mvm.MessageDateTime = itm.Created;
                    mvm.LatestMessage = itm.Message;
                    mvm.FromUser = uu;
                    mm.Add(mvm);
                }
                
            }

            foreach (var itm in MyMessages)
            {
                bool hasValue = false;
                bool hasChanges = false;
                int repeatingId = 0;

                foreach (var eee in mm)
                {
                    if (eee.FromUserId == itm.FromUserId)
                    {
                        hasValue = true;
                        repeatingId = (int)itm.FromUserId;
                        if (eee.MessageDateTime != itm.Created)
                        {
                            hasChanges = true;
                        }
                    }
                }
                if (hasValue)
                {
                    foreach (var occ in mm)
                    {

                        if (!hasValue)
                        {
                            UserModel uu = db.UserModels.Find((int)itm.ToUserID);
                            MessagesViewModel mvm = new MessagesViewModel();
                            mvm.FromUserId = (int)itm.ToUserID;
                            mvm.MessageDateTime = itm.Created;
                            mvm.LatestMessage = itm.Message;
                            mvm.FromUser = uu;
                            mm.Add(mvm);
                        }
                        else
                        {
                            if (hasChanges)
                            {
                                occ.LatestMessage = itm.Message;
                                occ.MessageDateTime = itm.Created;
                            }
                        }
                    }
                }
                else
                {
                    UserModel uu = db.UserModels.Find((int)itm.ToUserID);
                    MessagesViewModel mvm = new MessagesViewModel();
                    mvm.FromUserId = (int)itm.ToUserID;
                    mvm.MessageDateTime = itm.Created;
                    mvm.LatestMessage = itm.Message;
                    mvm.FromUser = uu;
                    mm.Add(mvm);
                }

            }
            

            return View(mm);
        }

        public  ActionResult MessageNew(int? id)
        {
            if(id == null)
            {
                return RedirectToAction("LoginIndex");
            }
            if (Session["UserModel"] == null && !Request.IsAuthenticated)
            {
                return RedirectToAction("LoginIndex");
            }
            NewMessageViewModel messageVM = new NewMessageViewModel();
            
            UserModel currentUser = (UserModel)Session["UserModel"];
            messageVM.FromUserModel = currentUser;
            messageVM.ToUserModel = db.UserModels.Find(id);
            var msg = db.MessageModels.Where(mm => (mm.FromUserId == currentUser.Id && mm.ToUserID == id) || (mm.FromUserId == id && mm.ToUserID == currentUser.Id)).Take(40);
            messageVM.Messages = msg.OrderByDescending(d=>d.Created).ToList();
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