using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace VendeeCrop.Models
{
    public class CropPostImageModel
    {
        [Key]
        public int ImageId { get; set; }


        public string ImageName { get; set; }
        public string Image { get; set; }

        public int CropPostId { get; set; }
        public virtual CropPostModel CropPost { get; set; }

        [HiddenInput(DisplayValue = false)]

        [DataType(DataType.DateTime)]
        public DateTime Created { get; set; }

        public CropPostImageModel()
        {
            Created = DateTime.Now;
        }
    }
}