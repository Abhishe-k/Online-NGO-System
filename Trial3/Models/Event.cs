using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace Trial3.Models
{
    public class Event
    {
        [Key]
        public int EventId { get; set; }
        public string EventTitle { get; set; }
        public string Venue { get; set; }
        public string Description { get; set; }
        public string About { get; set; }
        public int AllowedQuantity { get; set; }
        public string City { get; set; }

        [Required]
        [DataType(DataType.Date)]
        [DisplayFormat(DataFormatString = "{0:dd/MM/yyyy}", ApplyFormatInEditMode = true)]
        public DateTime EventDate { get; set; }

        [Required]
        [DisplayFormat(ApplyFormatInEditMode = true, DataFormatString = "{0:hh:mm tt}")]
        [DataType(DataType.Time)]
        public DateTime? StartTime { get; set; }

        [DisplayFormat(ApplyFormatInEditMode = true, DataFormatString = "{0:hh:mm tt}")]
        [DataType(DataType.Time)]
        public DateTime? EndTime { get; set; }
         
        
        public ICollection<EventUser> EventUser { get; set; }

    }
}