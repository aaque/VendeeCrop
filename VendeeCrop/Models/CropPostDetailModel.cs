using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace VendeeCrop.Models
{
    public class CropPostDetailModel
    {
        [Key]
        public int CropPostDetailId { get; set; }

        public string Details { get; set; }
        

        [Display(Name = "Crop Post")]
        public int CropPostId { get; set; }

        [ForeignKey("CropPostId")]
        public virtual CropPostModel CropPost { get; set; }

        [Display(Name = "Crop")]
        public int CropId { get; set; }

        [Display(Name = "Crop")]
        [ForeignKey("CropId")]
        public virtual CropModel Crop { get; set; }

        public string Status { get; set; }

        [DataType(DataType.DateTime)]
        [HiddenInput(DisplayValue = false)]
        public DateTime Created { get; set; }

        public CropPostDetailModel()
        {
            Created = DateTime.Now;
            Status = "Pending";
        }

    }
}