using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Web;

namespace VendeeCrop.Models
{
    public class MessagesViewModel
    {
        public int FromUserId { get; set; }

        [ForeignKey(nameof(FromUserId))]
        public virtual UserModel FromUser { get; set; }

        public string LatestMessage { get; set; }
        public bool IsUnread { get; set; }
        public DateTime MessageDateTime { get; set; }

        public MessagesViewModel()
        {
            FromUser = new UserModel();
            IsUnread = false;
        }
    }
}