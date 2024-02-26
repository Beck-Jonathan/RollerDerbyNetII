using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DataObjects
{
    public class Invoice
    {
        public int InvoiceID { set; get; }
        public string SkaterID { set; get; }
        public float InvoiceAmount { set; get; }
        public DateTime IssueDate { set; get; }

    }
}
