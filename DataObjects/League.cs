using System.ComponentModel.DataAnnotations;

namespace DataObjects
{
    public class League
    {
        [Display(Name = "LeagueID")]
        [Required(ErrorMessage = "Please enter LeagueID ")]
        [StringLength(100)]
        public string LeagueID { set; get; }
        [Display(Name = "Region")]
        [Required(ErrorMessage = "Please enter Region ")]
        [StringLength(100)]
        public string Region { set; get; }
        [Display(Name = "Gender")]
        [Required(ErrorMessage = "Please enter Gender ")]
        [StringLength(100)]
        public string Gender { set; get; }

    }
}
