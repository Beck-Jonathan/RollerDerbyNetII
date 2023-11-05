using DataObjects;
using System;
using System.Collections.Generic;

namespace LogicLayer
{
    public interface IReceiptManager
    {
        int addReceipts(int ReceiptID, int InvoiceID, DateTime RecipttDate, float ReceiptAmount);
        Receipts getReceiptsByPrimaryKey(string ReceiptsID);
        List<Receipts> getAllReceipts();
        int editReceipts(int oldReceiptID, int oldInvoiceID, DateTime oldRecipttDate, float oldReceiptAmount, int newReceiptID, int newInvoiceID, DateTime newRecipttDate, float newReceiptAmount);
        int purgeReceipts(string ReceiptsID);
    }
}
