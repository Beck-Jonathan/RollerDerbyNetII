using System;

namespace DataObjects
{
    public class Receipts
    {
        public int ReceiptID { set; get; }
        public int InvoiceID { set; get; }
        public DateTime RecipttDate { set; get; }
        public float ReceiptAmount { set; get; }

    }
}
