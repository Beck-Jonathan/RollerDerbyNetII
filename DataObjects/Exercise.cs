using System.ComponentModel.DataAnnotations;

namespace DataObjects
{
    public class Exercise
    {
        [Display(Name = "ExerciseID")]
        [Required(ErrorMessage = "Please enter an ExerciseID ")]
        [StringLength(50)]
        public string ExerciseID { set; get; }
        [Display(Name = "Exercise_count")]
        [Required(ErrorMessage = "Please enter a rep count ")]
        public int Exercise_count { set; get; }
        [Display(Name = "Exercise_units")]
        [StringLength(50)]
        [Required(ErrorMessage = "Please enter an rep units ")]
        public string Exercise_units { set; get; }

    }


}
