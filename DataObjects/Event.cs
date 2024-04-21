using System;
using System.ComponentModel.DataAnnotations;

namespace DataObjects
{
    public class Event
    {
        [Display(Name = "EventID")]
        [Required(ErrorMessage = "Please enter Event ID ")]
        public int EventID { set; get; }
        [Display(Name = "LocationID")]
        [Required(ErrorMessage = "Please pick a location ")]
        [StringLength(100)]
        public string LocationID { set; get; }
        [Display(Name = "EventType")]
        [Required(ErrorMessage = "Please select an event type")]
        [StringLength(100)]
        public string EventType { set; get; }
        [Display(Name = "DateTime")]
        [Required(ErrorMessage = "Please enter the date for this event ")]
        public DateTime DateTime { set; get; }

    }
    public class EventVM : Event
    {
        public Location _location { get; set; }
    }

}
