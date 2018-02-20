using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace VendeeCrop.Models
{
    public class CropPriceModel
    {
        [Key]
        public int Id { get; set; }

        [Display(Name = "Crop")]
        public int CropId { get; set; }

        [ForeignKey("CropId")]
        public virtual CropModel Crop { get; set; }

        [DataType(DataType.Currency)]
        public decimal Price { get; set; }

        [HiddenInput(DisplayValue = false)]
        public DateTime Created { get; set; }

        public CropPriceModel()
        {
            Created = DateTime.Now;
        }

    }
}