using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Web;

namespace VendeeCrop.Models
{
    public class UserMessages
    {

        [Key]
        public int UserMessageId { get; set; }

        public int MessageId { get; set; }

        [ForeignKey(nameof(MessageId))]
        public MessageModel Message { get; set; }
        

        public int UserId { get; set; }

        [ForeignKey(nameof(UserId))]
        public UserModel User { get; set; }

        public bool IsUnread { get; set; }

        public DateTime Created { get; set; }

        public UserMessages()
        {
            Created = DateTime.Now;
        }

    }
}