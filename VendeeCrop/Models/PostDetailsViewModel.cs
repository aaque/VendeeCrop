using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace VendeeCrop.Models
{
    public class PostDetailsViewModel
    {
        public int ?CropPostId { get; set; }

        public virtual CropPostModel CropPost { get; set; }

        public virtual ICollection<CropPostDetailModel> CropPostDetails { get; set; }

        public virtual ICollection<CropPostImageModel> PostImages { get; set; }
    }
}