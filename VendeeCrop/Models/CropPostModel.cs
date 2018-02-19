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

        [Display(Name = "Farmer")]
        public int FarmerModelId { get; set; }

        [ForeignKey("FarmerModelId")]
        [Display(Name = "Farmer")]
        public virtual FarmerModel Farmer { get; set; }

        public virtual ICollection<CropPostDetailModel> CropPostDetails { get; set; }

        public virtual ICollection<PostImageModel> PostImages { get; set; }
    }
}