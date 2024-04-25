using DataAccessInterfaces;
using DataAccessLayer;
using DataObjects;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LogicLayer
{
    /******************
Create the Logic Layer Manager for the Invoice table
 Created By Jonathan Beck 4/20/2024
***************/
    public class InvoiceManager : IInvoiceManager
    {
        private IInvoiceAccessor _invoiceAccessor = null;
        //default constuctor uses the database
        public InvoiceManager()
        {
            _invoiceAccessor = new InvoiceAccessor();
        }
        //the optional constuctor can accept any data provider
        public InvoiceManager(IInvoiceAccessor invoiceAccessor)
        {
            _invoiceAccessor = invoiceAccessor;
        }
        /******************
        Create the Logic Layer Delete method for the Invoice table
         Created By Jonathan Beck 4/20/2024
        ***************/

        public bool addInvoice(Invoice _Invoice)
        {
            bool result = false; ;
            try
            {
                result = (1 == _invoiceAccessor.addInvoice(_Invoice));
                if (!result) {
                    throw new ApplicationException("Invoice Not Added");
                }
            }
            catch (Exception ex)
            {
                throw new ApplicationException("Invoice not added", ex); ;
            }
            return result;
        }

        /******************
        Create the Logic Layer Undelete method for the Invoice table
         Created By Jonathan Beck 4/20/2024
        ***************/
        public int payInvoice(Invoice invoice)
        {
            int result = 0;
            try
            {
                result = _invoiceAccessor.payInvoice(invoice);
                if (result == 0)
                {
                    throw new ApplicationException("Unable to Mark Invoice as paid.");
                }
            }
            catch (Exception ex)
            {
                throw ex;
            }
            return result;
        }
        /******************
        Create the Logic Layer Undelete method for the Invoice table
         Created By Jonathan Beck 4/20/2024
        ***************/
        public int refundInvoice(Invoice invoice)
        {
            int result = 0;
            try
            {
                result = _invoiceAccessor.refundInvoice(invoice);
                if (result == 0)
                {
                    throw new ApplicationException("Unable to refund Invoice");
                }
            }
            catch (Exception ex)
            {
                throw ex;
            }
            return result;
        }
        /******************
        Create the Logic Layer Retreive by Primary Key method for theInvoice table
         Created By Jonathan Beck 4/20/2024
        ***************/
        public Invoice getInvoiceByPrimaryKey(int InvoiceID)
        {
            Invoice result = null;
            try
            {
                result = _invoiceAccessor.selectInvoiceByPrimaryKey(InvoiceID);
                if (result.InvoiceID == 0)
                {
                    throw new ApplicationException("Unable to retreive Invoice");
                }
            }
            catch (Exception ex)
            {
                throw new ApplicationException("Unable to retreive Invoice",ex); ;
            }
            return result;
        }
        /******************
        Create the Logic Layer Retreive all method for the the Invoice table
         Created By Jonathan Beck 4/20/2024
        ***************/
        public List<Invoice> getAllInvoice()
        {
            List<Invoice> result = new List<Invoice>();
            try
            {
                result = _invoiceAccessor.selectAllInvoice();
                if (result.Count == 0)
                {
                    throw new ApplicationException("Unable to retreive Invoices");
                }
            }
            catch (Exception ex)
            {
                throw new ApplicationException("Unable To Retrive Invoices",ex);
            }
            return result;
        }

        public List<Invoice> getInvoiceBySkater(string SkaterID)
        {
            List<Invoice> result = new List<Invoice>();
            try
            {
                result = _invoiceAccessor.selectInvoiceBySkater(SkaterID);
                if (result.Count == 0)
                {
                    throw new ApplicationException("Unable to retreive Invoices");
                }
            }
            catch (Exception ex)
            {
                throw new ApplicationException("Unable To Retrive Invoices", ex);
            }
            return result;
        }
        /******************
        Create the Logic Layer Update method for the Invoice table
         Created By Jonathan Beck 4/20/2024
        ***************/
        public int editInvoice(Invoice oldInvoice, Invoice newInvoice)
        {
            int result = 0;
            try
            {
                result = _invoiceAccessor.updateInvoice(oldInvoice, newInvoice);
                if (result == 0)
                {
                    throw new ApplicationException("Unable to update Invoice");
                }
            }
            catch (Exception ex)
            {
                throw ex;
            }
            return result;
        }

        public List<string> getDistinctSkaterForDropDown()
        {
            List<String> results = new List<string> ();
            try
            {
                results = _invoiceAccessor.selectDistinctSkaterForDropDown();
            }
            catch (Exception ex)
            {

                throw new ApplicationException("unable to retreive skaters",ex);
            }


            return results;
        }
        /******************
        Create the Logic Layeradd multiple invoices method for the Invoice table
         Created By Jonathan Beck 4/20/2024
        ***************/
        public int addMultipleInvoices(List<Invoice> invoices)
        {
            int result = 0;
            foreach (Invoice i in invoices)
            {
                if (addInvoice(i)) result++;
            }
            return result;

        }
    }


}
