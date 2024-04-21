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
        int updateInvoice(Invoice _oldInvoice , Invoice _newInvoice);
        int deleteInvoice(Invoice _Invoice);
        int undeleteInvoice(Invoice _Invoice);
        List<String> selectDistinctSkaterForDropDown();
    }

}
