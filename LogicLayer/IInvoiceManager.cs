using DataObjects;
using System;
using System.Collections.Generic;

namespace LogicLayer
{
    public interface IInvoiceManager
    {
        int addInvoice(int InvoiceID, string SkaterID, float InvoiceAmount, DateTime IssueDate);
        Invoice getInvoiceByPrimaryKey(string InvoiceID);
        List<Invoice> getAllInvoice();
        int editInvoice(int oldInvoiceID, string oldSkaterID, float oldInvoiceAmount, DateTime oldIssueDate, int newInvoiceID, string newSkaterID, float newInvoiceAmount, DateTime newIssueDate);
        int purgeInvoice(string InvoiceID);
    }
}
