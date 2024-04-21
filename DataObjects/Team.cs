using System;
using System.ComponentModel.DataAnnotations;

namespace DataObjects
{
    public class Team
    {
        [Display(Name = "TeamId")]
        [Required(ErrorMessage = "Please enter TeamId ")]
        [StringLength(50)]
        public string TeamId { set; get; }
        [Display(Name = "LeagueID")]
        [Required(ErrorMessage = "Please enter LeagueID ")]
        [StringLength(100)]
        public string LeagueID { set; get; }
        [Display(Name = "MonthlyDue")]
        [Required(ErrorMessage = "Please enter MonthlyDue ")]
        public Decimal MonthlyDue { set; get; }
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
        [Display(Name = "isActive")]
        [Required(ErrorMessage = "Please enter isActive ")]
        public bool isActive { set; get; }

    }
    public class TeamVM : Team
    {
        public League _league { get; set; }
    }

}
