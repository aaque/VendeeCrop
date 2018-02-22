using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace VendeeCrop.Models
{
    public class NewMessageViewModel
    {
        public virtual UserModel ToUserModel { get; set; }
        public virtual UserModel FromUserModel { get; set; }

        public virtual IEnumerable<MessageModel> Messages { get; set; }
    }
}