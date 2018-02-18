using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace VendeeCrop.Models
{
    public class CropPostModel
    {
        [Key]
        public int CropPostId { get; set; }

        [Required]
        public string Title { get; set; }

        public string Description { get; set; }

        public virtual ICollection<CropPostDetailModel> CropPostDetails { get; set; }
    }
}