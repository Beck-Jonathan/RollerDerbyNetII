using DataAccessInterfaces;
using DataObjects;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DataAccessFakes
{
    public class InvoiceAccessorFakes : IInvoiceAccessor
    {
        List<Invoice> invoices;

            public InvoiceAccessorFakes() {
            invoices = new List<Invoice>();
            Invoice invoice1 = new Invoice();
            invoice1.InvoiceID = 1;
            invoice1.SkaterID = "Zelda";
            invoice1.InvoiceAmount = 20;
            invoice1.is_active = true;
            invoices.Add(invoice1);
            Invoice invoice2 = new Invoice();
            invoice2.InvoiceID = 2;
            invoice2.SkaterID = "Link";
            invoice2.InvoiceAmount = 25;
            invoice2.is_active = true;
            invoices.Add(invoice2);
            Invoice invoice3 = new Invoice();
            invoice3.InvoiceID = 3;
            invoice3.SkaterID = "Gannon";
            invoice3.InvoiceAmount = 30;
            invoice3.is_active = false;
            invoices.Add(invoice3);
            Invoice invoice10 = new Invoice();
            invoice10.InvoiceID = 10;
            invoice10.SkaterID = "Gannon";
            invoice10.InvoiceAmount = 31;
            invoice10.is_active = false;
            invoices.Add(invoice10);
        }
        public int addInvoice(Invoice _Invoice)
        {
            int start = invoices.Count;
            foreach (Invoice i in invoices) {
                if (i.InvoiceID == _Invoice.InvoiceID) {
                    return 0;
                    
                }
            }
            invoices.Add(_Invoice);
            int end = invoices.Count;
            return end - start;

        }

        public int payInvoice(Invoice _Invoice)
        {
            foreach (Invoice i in invoices) {
                if (i.InvoiceID == _Invoice.InvoiceID) {
                    i.is_active = false;
                    return 1;
                } 
            }
            return 0;
        }

        public List<Invoice> selectAllInvoice()
        {
            return invoices;
        }

        public List<string> selectDistinctSkaterForDropDown()
        {
            List<String> skaterNames = new List<string>();
            skaterNames.Add("Fizzle");
            skaterNames.Add("Swizzle");
            skaterNames.Add("Zelda");

            return skaterNames;
        }

        public Invoice selectInvoiceByPrimaryKey(int InvoiceID)
        {
            Invoice result = new Invoice();
            foreach (Invoice invoice in invoices)
            {
                if (invoice.InvoiceID== InvoiceID)
                {
                    result = invoice;
                    break;
                }
            }
            return result;
        }

        public int refundInvoice(Invoice _Invoice)
        {
            int result = 0;
            foreach (Invoice invoice in invoices)
            {
                if (invoice.InvoiceID == _Invoice.InvoiceID)
                {
                    invoice.is_active = true;
                    result = 1;
                    break;
                }
            }

            return result;
        }

        public int updateInvoice(Invoice _oldInvoice, Invoice _newInvoice)
        {
            int result = 0;
            foreach (Invoice invoice in invoices)
            {
                if (invoice.InvoiceID == _oldInvoice.InvoiceID)
                {
                    invoice.SkaterID = _newInvoice.SkaterID;
                    invoice.IssueDate = _newInvoice.IssueDate;
                    invoice.InvoiceAmount = _newInvoice.InvoiceAmount;
                    result = 1;
                    break;
                }
            }

            return result;
        }

        public List<Invoice> selectInvoiceBySkater(string SkaterID)
        {
            List<Invoice> results = new List<Invoice>();
            foreach (Invoice invoice in invoices)
            {
                if (invoice.SkaterID .Equals( SkaterID))
                {
                    results.Add(invoice);
                   
                }
            }
            return results;
        }
    }
    
}
