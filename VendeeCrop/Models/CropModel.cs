﻿using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Web;

namespace VendeeCrop.Models
{
    public class CropModel
    {
        [Key]
        public int Id { get; set; }

        [Required]
        [StringLength(255)]
        public string Name { get; set; }

        [Display(Name = "Crop Type")]
        public int CropTypeId { get; set; }

        [ForeignKey("CropTypeId")]
        public virtual CropTypeModel CropType { get; set; }
    }
}