using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;

namespace Trial3.Models
{
    public class UserDonation
    {
        public int UserDonationId { get; set; }
        [Required]
        [Display(Name = "User")]
        public int UserId { get; set; }
        public virtual User User { get; set; }
        [Required]
        [Display(Name ="Items")]
        public int ItemsId { get; set; }
        public virtual Items Items { get; set; }

        [Required(ErrorMessage = "Please enter quantity")]
        public int Quantity { get; set; }
        public string Description { get; set; }
    }
}