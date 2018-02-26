using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using Microsoft.AspNet.SignalR;
using VendeeCrop.Models;

namespace VendeeCrop.SignalR
{
    public class CommunicationR : Hub
    {
        //public void Hello()
        //{
        //    Clients.All.hello();
        //}
        private ApplicationDbContext db = new ApplicationDbContext();
        public void Send(string type, string fromId, string toId, string message)
        {
            MessageModel msgModel = new MessageModel();
            msgModel.FromUserId = int.Parse(fromId);
            msgModel.ToUserID = int.Parse(toId);
            msgModel.Message = message;

            db.MessageModels.Add(msgModel);

            UserModel userModel = db.UserModels.Find(int.Parse(fromId));

            NotificationModel notiModel = new NotificationModel();
            notiModel.Message = "You have a message from " + userModel.FullName;
            notiModel.MessageType = type;
            notiModel.ToId = int.Parse(toId);
            notiModel.TypeTo = "User";

            db.NotificationModels.Add(notiModel);

            db.SaveChanges();


            if (type == "Message")
            {
                UserMessages noti = new UserMessages();
                noti.IsUnread = true;
                noti.UserId = int.Parse(toId);
                noti.MessageId = msgModel.MessageId;
                db.UserMessages.Add(noti);
                db.SaveChanges();
            }

            Clients.All.addNewMessageToPage(type ,fromId, toId, message);
        }
    }
}