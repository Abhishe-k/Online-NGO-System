using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;
namespace Trial3.Models
{
    public class Items
    {
        [Key]
        public int ItemsId { get; set; }
        [Required(ErrorMessage ="Please enter item suitable for donation")]
        public string ItemsName { get; set; }
        public virtual ICollection<UserDonation> UserDonation { get; set; }

    }
}