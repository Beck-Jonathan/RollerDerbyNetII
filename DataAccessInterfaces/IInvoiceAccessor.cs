using DataObjects;
using System;
using System.Collections.Generic;

namespace DataAccessInterfaces
{
    public interface IInvoiceAccessor
    {
        int insertInvoice(int InvoiceID, string SkaterID, float InvoiceAmount, DateTime IssueDate);
        Invoice selectInvoiceByPrimaryKey(int InvoiceID);
        List<Invoice> selectAllInvoice();
        int updateInvoice(int oldInvoiceID, string oldSkaterID, float oldInvoiceAmount, DateTime oldIssueDate, string newSkaterID, float newInvoiceAmount, DateTime newIssueDate);
        int deleteInvoice(string InvoiceID);
    }

}
