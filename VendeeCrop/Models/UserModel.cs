using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace VendeeCrop.Models
{
    public class UserModel
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
        

        [StringLength(50)]
        public string Type { get; set; }

        [Required]
        [DataType(DataType.Password)]
        public string Password { get; set; }

        [Compare("Password", ErrorMessage = "Confirm password doesn't match, Type again !")]
        [DataType(DataType.Password)]
        [Display(Name = "Confirm Password")]
        public string ConfirmPassword { get; set; }
        
        [StringLength(255)]
        [Display(Name = "Store Name")]
        public string StoreName { get; set; }

        [Required]
        [StringLength(255)]
        public string Address { get; set; }

        public string ImagePath { get; set; }

        public bool IsActive { get; set; }
        public bool IsApproved { get; set; }

        public ICollection<CropPostModel> CropPosts { get; set; }

        public UserModel()
        {
            IsActive = false;
            IsApproved = false;
        }
    }
}