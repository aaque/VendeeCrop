using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Web;

namespace VendeeCrop.Models
{
    public class CropPostDetailModel
    {
        [Key]
        public int CropPostDetailId { get; set; }

        [Display(Name = "Crop Post")]
        public int CropPostId { get; set; }

        [ForeignKey("CropPostId")]
        public virtual CropPostModel CropPost { get; set; }

        [Display(Name = "Crop Type")]
        public int CropTypeId { get; set; }

        [ForeignKey("CropTypeId")]
        public virtual CropTypeModel CropType { get; set; }


    }
}