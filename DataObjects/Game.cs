using System;
using System.ComponentModel.DataAnnotations;



namespace DataObjects
{
    public class Game
    {
        [Required(ErrorMessage = "Please enter [GameID] ")]
        [Display(Name = "Game ID")]
        public int GameID { set; get; }
        [Required(ErrorMessage = "Please enter [LocationID] ")]
        [StringLength(100)]
        [Display(Name = "Location ID")]
        public string LocationID { set; get; }
        [Required(ErrorMessage = "Please enter [Date] ")]
        [Display(Name = "[Date]")]
        public DateTime Date { set; get; }

    }
    public class GameVM : Game
    {
        public Location _location { get; set; }
    }

}
