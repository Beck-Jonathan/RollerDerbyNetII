using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace DataObjects
{
    public class Skater
    {
        [Display(Name = "Derby Name")]
        [Required(ErrorMessage = "Please enter your derby name ")]
        [StringLength(50)]
        public string SkaterID { set; get; }
        [Display(Name = "Associated Team")]
        [Required(ErrorMessage = "Please select a team ")]
        [StringLength(50)]
        public string TeamID { set; get; }
        [Display(Name = "First Name")]
        [Required(ErrorMessage = "Please enter youro first name ")]
        [StringLength(50)]
        public string GivenName { set; get; }
        [Display(Name = "Last Name")]
        [Required(ErrorMessage = "Please enter your family name ")]
        [StringLength(50)]
        public string FamilyName { set; get; }
        [Display(Name = "Phone (xxx) xxx-xxxx")]
        [Required(ErrorMessage = "Please enter your phone number (ex (123) 123-1234")]
        [StringLength(14)]
        public string Phone { set; get; }
        [Display(Name = "e-mail")]
        [Required(ErrorMessage = "Please enter your email ")]
        [StringLength(250)]
        public string Email { set; get; }
        [Display(Name = "passwordhash")]
        [Required(ErrorMessage = "Please enter your passwordhash ")]
        [StringLength(256)]
        public string passwordhash { set; get; }
        [Display(Name = "active")]
        [Required(ErrorMessage = "Please enter select this check box ")]
        public bool Active { set; get; }

    }
    public class SkaterVM : Skater
    {
        public Team _team { get; set; }
        public List<string> Roles { get; set; }
    }

}
