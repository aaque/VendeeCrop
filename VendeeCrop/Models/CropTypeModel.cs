using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace VendeeCrop.Models
{
    public class CropTypeModel
    {
        [Key]
        public int CropTypeId { get; set; }

        [Required]
        public string Type { get; set; }
    }
}