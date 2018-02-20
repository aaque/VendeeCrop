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

        [ForeignKey("UserModelId")]
        [Display(Name = "User")]
        public virtual UserModel User { get; set; }

        public virtual ICollection<CropPostDetailModel> CropPostDetails { get; set; }

        public virtual ICollection<PostImageModel> PostImages { get; set; }
    }
}