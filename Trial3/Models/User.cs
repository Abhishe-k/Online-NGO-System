using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;
namespace Trial3.Models
{
    public class User
    {
        [Key]
        public int UserId { get; set; }
        [Required]
        [Display(Name = "Your Full Name")]
        public string FullName { get; set; }

        [DataType(DataType.Date)]
        [Display(Name = "Birth Date")]
        [DisplayFormat(DataFormatString = "{0:dd/MM/yyyy}", ApplyFormatInEditMode = true)]
        public DateTime DateOfBirth { get; set; }

        [Required]
        [Display(Name = " Resedential Address")]
        public string Address { get; set; }

        [Required]
        public string State { get; set; }

        [Required]
        public string City { get; set; }

        [Required]
        [RegularExpression(@"^\d{6}(-\d{4})?$", ErrorMessage = "Please Enter Valid Postal Code.")]
        public string Pincode { get; set; }

        [Display(Name = "Your Mobile No.")]
        [Required]
        [DataType(DataType.PhoneNumber)]

        [RegularExpression(@"^([0]|\+91[\-\s]?)?[789]\d{9}$", ErrorMessage = "Entered Mobile No is not valid.")]
        public string MobileNo { get; set; }

        [Required]
        [Display(Name = "Your EmailId")]
        [DataType(DataType.EmailAddress)]
        [EmailAddress(ErrorMessage = "Invalid Email Address.")]
        public string EmailId { get; set; }

        public ICollection<EventUser> EventUser { get; set; }
        public ICollection<UserDonation> UserDonation { get; set; }
    }
}