using System.ComponentModel.DataAnnotations;

namespace DataObjects
{
    public class Location
    {
        [Display(Name = "Location")]
        [Required(ErrorMessage = "Please enter a locaiton id ")]
        [StringLength(100)]
        public string LocationID { set; get; }
        [Display(Name = "League")]
        [Required(ErrorMessage = "Please select a league ")]
        [StringLength(100)]
        public string LeagueID { set; get; }
        [Display(Name = "Contact Phone (ex (123) 123-1234)")]
        [Required(ErrorMessage = "Please enter the phone number (ex (123) 123-1234)")]
        [StringLength(14)]
        public string ContactPhone { set; get; }
        [Display(Name = "City")]
        [Required(ErrorMessage = "Please enter City ")]
        [StringLength(100)]
        public string City { set; get; }
        [Display(Name = "State")]
        [Required(ErrorMessage = "Please enter State ")]
        [StringLength(50)]
        public string State { set; get; }
        [Display(Name = "Zip")]
        [Required(ErrorMessage = "Please enter Zip ")]
        [StringLength(5)]
        public string Zip { set; get; }
    }
    public class LocationVM : Location
    {
        public League league { set; get; }
    }
}
