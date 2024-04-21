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
        }
        public int addInvoice(Invoice _Invoice)
        {
            int start = invoices.Count;
            invoices.Add(_Invoice);
            int end = invoices.Count;
            return end - start;

        }

        public int deleteInvoice(Invoice _Invoice)
        {
            foreach (Invoice i in invoices) {
                if (i.InvoiceID == _Invoice.InvoiceID) {
                    invoices.Remove(i);
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

        public int undeleteInvoice(Invoice _Invoice)
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
    }
    
}
