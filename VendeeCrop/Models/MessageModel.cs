using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Web;

namespace VendeeCrop.Models
{
    public class MessageModel
    {
        [Key]
        public int MessageId { get; set; }

        public int? FromUserId { get; set; }

        [ForeignKey(nameof(FromUserId))]
        public virtual UserModel FromUser { get; set; }

        public int ToUserID { get; set; }

        //[ForeignKey(nameof(ToUserID))]
        //public virtual UserModel ToUser { get; set; }

        public string Message { get; set; }

        public DateTime Created { get; set; }
        
        public MessageModel()
        {
            Created = DateTime.Now;
        }
    }
}