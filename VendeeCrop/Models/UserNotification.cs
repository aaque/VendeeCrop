using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Web;

namespace VendeeCrop.Models
{
    public class UserNotification
    {
        [Key]
        public int UserNotificationId { get; set; }

        public int NotificationId { get; set; }

        [ForeignKey(nameof(NotificationId))]
        public virtual NotificationModel Notification { get; set; }

        public int UserId { get; set; }

        [ForeignKey(nameof(UserId))]
        public virtual UserModel User { get; set; }

        public bool IsUnread { get; set; }

        public DateTime Created { get; set; }

        public UserNotification()
        {
            Created = DateTime.Now;
        }

    }
}