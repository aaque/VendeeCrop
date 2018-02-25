using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.Linq;
using System.Net;
using System.Reflection;
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
                .Select(s => s.OrderBy(c => c.Created)
                .FirstOrDefault());

            var FromMeMessages = db.MessageModels
                .Where(m => m.FromUserId == UserModel.Id)
                .Include(e => e.FromUser)
                .GroupBy(g => g.FromUserId)
                .Select(s => s.OrderBy(c => c.Created)
                .FirstOrDefault());

            ICollection<MessagesViewModel> mm = new List<MessagesViewModel>();
            

            foreach (var itm in FromMeMessages) // from ako
            {
                bool hasValue = false;

                if (mm.Where(d => d.FromUserId == itm.ToUserID).Count() > 0)
                {
                    hasValue = true;
                }
                        
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

            }

            foreach (var itm in MyMessages) // to ako
            {
                bool hasValue = false;

                foreach (var eee in mm.ToList())
                {
                    if (mm.Where(d => d.FromUserId == itm.FromUserId).Count() > 0)
                    {
                        hasValue = true;

                    }
                    if (!hasValue)
                    {
                        UserModel uu = db.UserModels.Find((int)itm.FromUserId);
                        MessagesViewModel mvm = new MessagesViewModel();
                        mvm.FromUserId = (int)itm.FromUserId;
                        mvm.MessageDateTime = itm.Created;
                        mvm.LatestMessage = itm.Message;
                        mvm.FromUser = uu;
                        mm.Add(mvm);
                    }

                }

            }

        Requery:
            DataTable dtt = ToDataTable(mm.ToList());
            foreach (var fin in mm.ToList())
            {
                
                MessageModel fromfin = db.MessageModels.Where(ee => (ee.FromUserId == fin.FromUserId && ee.ToUserID == UserModel.Id)).OrderByDescending(r => r.MessageId).FirstOrDefault();
                MessageModel tofin = db.MessageModels.Where(ee => (ee.FromUserId == UserModel.Id && ee.ToUserID == fin.FromUserId)).OrderByDescending(r => r.MessageId).FirstOrDefault();
                if (fromfin != null && tofin != null)//naay mga sulod
                {
                    if (fromfin.MessageId > tofin.MessageId)
                    {
                        if(fin.MessageDateTime != fromfin.Created)
                        {
                            fin.LatestMessage = fromfin.Message;
                            fin.MessageDateTime = fromfin.Created;
                            fin.IsUnread = db.UserMessages.Where(u => u.Message.FromUserId == fin.FromUserId && u.IsUnread == true && u.UserId == UserModel.Id).Count() > 0;
                            goto Requery;
                        }
                        
                    }
                    else if (fromfin.MessageId < tofin.MessageId)
                    {
                        if(fin.MessageDateTime != tofin.Created)
                        {
                            fin.LatestMessage = tofin.Message;
                            fin.MessageDateTime = tofin.Created;
                            fin.IsUnread = db.UserMessages.Where(u => u.Message.FromUserId == fin.FromUserId && u.IsUnread == true && u.UserId == UserModel.Id).Count() > 0;
                            goto Requery;
                        }
                        
                    }
                }
                else if (fromfin != null && tofin == null)//fromfin lng naay sulod
                {

                    if(fin.MessageDateTime != fromfin.Created)
                    {
                        fin.LatestMessage = fromfin.Message;
                        fin.MessageDateTime = fromfin.Created;
                        fin.IsUnread = db.UserMessages.Where(u => u.Message.FromUserId == fin.FromUserId && u.IsUnread == true && u.UserId == UserModel.Id).Count() > 0;
                        goto Requery;
                    }
                }
                else if (fromfin == null && tofin != null)//fromfin lng naay sulod
                {
                    if(fin.MessageDateTime != tofin.Created)
                    {
                        fin.LatestMessage = tofin.Message;
                        fin.MessageDateTime = tofin.Created;
                        fin.IsUnread = db.UserMessages.Where(u => u.Message.FromUserId == fin.FromUserId && u.IsUnread == true && u.UserId == UserModel.Id).Count() > 0;
                        goto Requery;
                    }
                }
            }
            return View(mm);
        }

        public DataTable ToDataTable<T>(List<T> items)
        {
            DataTable dataTable = new DataTable(typeof(T).Name);
            //Get all the properties by using reflection   
            PropertyInfo[] Props = typeof(T).GetProperties(BindingFlags.Public | BindingFlags.Instance);
            foreach (PropertyInfo prop in Props)
            {
                //Setting column names as Property names  
                dataTable.Columns.Add(prop.Name);
            }
            foreach (T item in items)
            {
                var values = new object[Props.Length];
                for (int i = 0; i < Props.Length; i++)
                {

                    values[i] = Props[i].GetValue(item, null);
                }
                dataTable.Rows.Add(values);
            }

            return dataTable;
        }

        public string SaveRealTime(string type, string from, string to, string value)
        {
            if (type == "Message")
            {
                UserMessages noti = new UserMessages();
                noti.IsUnread = true;
                noti.UserId = int.Parse(to);
                db.UserMessages.Add(noti);
                db.SaveChanges();
            }else if(type == "Notification")
            {

            }
            return "";
        }

        public int GetMessagesNotificationCount()
        {
            UserModel me = (UserModel)Session["UserModel"];
            return db.UserMessages.Where(u => u.UserId == me.Id && u.IsUnread == true).Count();
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
            var updateMessagesUnread = db.UserMessages.Where(e => e.IsUnread == true && e.UserId == currentUser.Id && e.Message.ToUserID == currentUser.Id).ToList();
            updateMessagesUnread.ForEach(a => a.IsUnread = false);
            db.SaveChanges();
            
            messageVM.FromUserModel = currentUser;
            messageVM.ToUserModel = db.UserModels.Find(id);
            var msg = db.MessageModels.Where(mm => (mm.FromUserId == currentUser.Id && mm.ToUserID == id) || (mm.FromUserId == id && mm.ToUserID == currentUser.Id)).Take(40);
            messageVM.Messages = msg.OrderBy(d=>d.Created).ToList();
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