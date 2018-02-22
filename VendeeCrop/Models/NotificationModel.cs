using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace VendeeCrop.Models
{
    public class NotificationModel
    {
        [Key]
        public int NotificationId { get; set; }

        //ALL/USER
        public string TypeTo { get; set; }

        public int ToId { get; set; }

        public string MessageType { get; set; }

        public string Message { get; set; }

        [DataType(DataType.DateTime)]
        public DateTime Created { get; set; }

        public NotificationModel()
        {
            Created = DateTime.Now;
        }

    }
}