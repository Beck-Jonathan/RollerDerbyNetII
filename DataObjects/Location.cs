using System.ComponentModel.DataAnnotations;

namespace DataObjects
{
    public class Location
    {
        [Display(Name = "LocationID")]
        [Required(ErrorMessage = "Please enter a locaiton id ")]
        [StringLength(100)]
        public string LocationID { set; get; }
        [Display(Name = "LeagueID")]
        [Required(ErrorMessage = "Please select a league ")]
        [StringLength(100)]
        public string LeagueID { set; get; }
        [Display(Name = "ContactPhone")]
        [Required(ErrorMessage = "Please enter Contact Phone e.g.(123)123-1234 ")]
        [StringLength(11)]
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
