using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace VendeeCrop.Models
{
    public class FarmerModel
    {
        [Key]
        public int Id { get; set; }

        [Required]
        [DataType(DataType.PhoneNumber)]
        [Display(Name = "Phone Number")]
        public string PhoneNumber { get; set; }

        [Required]
        [StringLength(50)]
        [Display(Name = "First Name")]
        public string FirstName { get; set; }

        [Required]
        [StringLength(50)]
        [Display(Name = "Last Name")]
        public string LastName { get; set; }

        public string FullName { get { return FirstName + ' ' + LastName; } }


        [Required]
        [DataType(DataType.Password)]
        public string Password { get; set; }

        [Compare("Password", ErrorMessage = "Confirm password doesn't match, Type again !")]
        [DataType(DataType.Password)]
        [Display(Name = "Confirm Password")]
        public string ConfirmPassword { get; set; }

        [Required]
        [StringLength(255)]
        public string Address { get; set; }


        public bool IsActive { get; set; }
        public bool IsApproved { get; set; }

        public virtual ICollection<CropPostModel> CropPosts { get; set; }

        public FarmerModel()
        {
            IsActive = false;
            IsApproved = false;
        }

    }
}