using System;
using System.ComponentModel.DataAnnotations;

namespace DataObjects
{
    public class TeamApplication
    {
        [Display(Name = "TeamApplicationID")]
        [Required(ErrorMessage = "Please enter TeamApplicationID ")]
        public int TeamApplicationID { set; get; }
        [Display(Name = "SkaterID")]
        [Required(ErrorMessage = "Please enter SkaterID ")]
        [StringLength(50)]
        public string SkaterID { set; get; }
        [Display(Name = "TeamName")]
        [Required(ErrorMessage = "Please enter TeamName ")]
        [StringLength(50)]
        public string TeamName { set; get; }
        [Display(Name = "ApplicationTime")]
        [Required(ErrorMessage = "Please enter ApplicationTime ")]
        public DateTime ApplicationTime { set; get; }
        [Display(Name = "ApplicationStatus")]
        [Required(ErrorMessage = "Please enter ApplicationStatus ")]
        [StringLength(50)]
        public string ApplicationStatus { set; get; }

    }
    public class TeamApplicationVM : TeamApplication
    {
        public Skater _skater { get; set; }
        public Team _team { get; set; }
    }
}
