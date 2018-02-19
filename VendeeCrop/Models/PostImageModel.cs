using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Web;

namespace VendeeCrop.Models
{
    public class PostImageModel
    {
        [Key]
        public string PostImageId { get; set; }

        [Display(Name = "Crop Post")]
        public int CropPostId { get; set; }

        [ForeignKey("CropPostId")]
        public virtual CropPostModel CropPost { get; set; }

        public string ImageName { get; set; }

        public byte[] Image { get; set; }


    }
}