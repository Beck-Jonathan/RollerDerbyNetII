using System;
using System.ComponentModel.DataAnnotations;

namespace DataObjects
{
    public class Invoice
    {
        [Display(Name = "InvoiceID")]
        [Required(ErrorMessage = "Please enter the InvoiceID ")]
        public int InvoiceID { set; get; }
        [Display(Name = "SkaterID")]
        [Required(ErrorMessage = "Please enter the Skater assigned to this invoice ")]
        [StringLength(50)]
        public string SkaterID { set; get; }
        [Display(Name = "InvoiceAmount")]
        [Required(ErrorMessage = "Please enter Invoice Amount ")]
        public decimal InvoiceAmount { set; get; }
        [Display(Name = "IssueDate")]
        [Required(ErrorMessage = "Please select the issue date")]
        public DateTime IssueDate { set; get; }
        [Display(Name = "is_active")]
        [Required(ErrorMessage = "Please select this box ")]
        public bool is_active { set; get; }

    }
    public class InvoiceVM : Invoice
    {
        public Skater _skater { get; set; }
    }

}
