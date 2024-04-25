using DataObjects;
using System;
using System.Collections.Generic;
using System.Net;

namespace LogicLayer
{
    public interface IInvoiceManager
    {
        bool addInvoice(Invoice _Invoice);
        Invoice getInvoiceByPrimaryKey(int InvoiceID);
        List<Invoice> getAllInvoice();
        List<Invoice> getInvoiceBySkater(string SkaterID);
        int editInvoice(Invoice _oldInvoice, Invoice _newInvoice);
        int payInvoice(Invoice _invoice);
        int refundInvoice(Invoice _invoice);
        List<String> getDistinctSkaterForDropDown();

        int addMultipleInvoices(List<Invoice> invoices);
        
        }
    }

