using System.ComponentModel.DataAnnotations;

namespace DataObjects
{
    public class Workout
    {
        [Display(Name = "Workout ID")]
        [Required(ErrorMessage = "Please select the workout id")]
        public int WorkoutID { set; get; }
        [Display(Name = "Event ID")]
        [Required(ErrorMessage = "Please select the event")]
        public int EventID { set; get; }
        [Display(Name = "Workout Name")]
        [StringLength(50)]
        [Required(ErrorMessage = "Please supply a workout name")]
        public string WorkoutName { set; get; }

    }
    public class WorkoutVM : Workout
    {
        public Event _event{ get; set; }
    }

}
