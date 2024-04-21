using DataObjects;
using System;
using System.Collections.Generic;

namespace LogicLayer
{
    public interface IInvoiceManager
    {
        bool addInvoice(Invoice _Invoice);
        Invoice getInvoiceByPrimaryKey(int InvoiceID);
        List<Invoice> getAllInvoice();
        int editInvoice(Invoice _oldInvoice, Invoice _newInvoice);
        int purgeInvoice(Invoice _invoice);
        int unpurgeInvoice(Invoice _invoice);
        List<String> getDistinctSkaterForDropDown();
    }
}
