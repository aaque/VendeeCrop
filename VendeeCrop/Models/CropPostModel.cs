using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
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

        [Display(Name = "User")]
        public int UserModelId { get; set; }

        //event or crop
        public string Type { get; set; }

        [ForeignKey(nameof(UserModelId))]
        [Display(Name = "User")]
        public virtual UserModel User { get; set; }

        public virtual ICollection<CropPostDetailModel> CropPostDetails { get; set; }

        public virtual ICollection<CropPostImageModel> PostImages { get; set; }
    }
}