using DataObjects;
using System;
using System.Collections.Generic;

namespace DataAccessInterfaces
{
    public interface IInvoiceAccessor
    {
        int addInvoice(Invoice _Invoice);
        Invoice selectInvoiceByPrimaryKey(int InvoiceID);
        List<Invoice> selectAllInvoice();
        List<Invoice> selectInvoiceBySkater(string SktaerID);
        int updateInvoice(Invoice _oldInvoice , Invoice _newInvoice);
        int payInvoice(Invoice _Invoice);
        int refundInvoice(Invoice _Invoice);
        List<String> selectDistinctSkaterForDropDown();
        
    }

}
