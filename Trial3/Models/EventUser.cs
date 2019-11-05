using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;

namespace Trial3.Models
{
    public class EventUser
    {
        public int EventUserId { get; set; }
        [Required]
        [Display(Name ="User")]
        public int UserId { get; set; }
        public virtual User User { get; set; }
        [Required]
        [Display(Name ="Event")]
        public int EventId { get; set; }
        [Required]
        [Display(Name ="No of Participants")]
        [Range(1,4)]
        public int Quantity { get; set; }
        public virtual Event Event { get; set; }
     }
}