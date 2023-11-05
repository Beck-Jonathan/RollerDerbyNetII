using DataObjects;
using System;
using System.Collections.Generic;

namespace DataAccessInterfaces
{
    public interface IReceiptAccessor
    {
        int insertReceipt(int ReceiptID, int InvoiceID, DateTime RecipttDate, float ReceiptAmount);
        Receipts selectReceiptsByPrimaryKey(int ReceiptsID);
        List<Receipts> selectAllReceipts();
        int updateReceipts(int oldReceiptID, int oldInvoiceID, DateTime oldRecipttDate, float oldReceiptAmount, int newInvoiceID, DateTime newRecipttDate, float newReceiptAmount);
        int deleteReceipts(string ReceiptsID);
    }
}
