using DataAccessInterfaces;
using DataObjects;
using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;

namespace DataAccessLayer
{
    public class ReceiptsAccessor : IReceiptAccessor
    {
        public int insertReceipt(int ReceiptID, int InvoiceID, DateTime RecipttDate, float ReceiptAmount)
        {
            int rows = 0;
            // start with a connection object
            var conn = SqlConnectionProvider.GetConnection();
            // set the command text
            var commandText = "sp_insert_Receipts";
            // create the command object
            var cmd = new SqlCommand(commandText, conn);
            // set the command type
            cmd.CommandType = CommandType.StoredProcedure;
            // we need to add parameters to the command
            cmd.Parameters.Add("@ReceiptID", SqlDbType.Int);
            cmd.Parameters.Add("@InvoiceID", SqlDbType.Int);
            cmd.Parameters.Add("@RecipttDate", SqlDbType.DateTime);
            cmd.Parameters.Add("@ReceiptAmount", SqlDbType.Float);

            //We need to set the parameter values
            cmd.Parameters["@ReceiptID"].Value = ReceiptID;
            cmd.Parameters["@InvoiceID"].Value = InvoiceID;
            cmd.Parameters["@RecipttDate"].Value = RecipttDate;
            cmd.Parameters["@ReceiptAmount"].Value = ReceiptAmount;
            try
            {
                //open the connection 
                conn.Open();  //execute the command and capture result
                rows = cmd.ExecuteNonQuery();
            }

            catch (Exception ex)
            {
                throw ex;
            }
            finally
            {
                conn.Close();
            }
            return rows;
        }
        public Receipts selectReceiptsByPrimaryKey(int ReceiptID)
        {
            Receipts output = new Receipts();
            // start with a connection object
            var conn = SqlConnectionProvider.GetConnection();
            // set the command text
            var commandText = "sp_retreive_by_pk_Receipts";
            // create the command object
            var cmd = new SqlCommand(commandText, conn);
            // set the command type
            cmd.CommandType = CommandType.StoredProcedure;
            // we need to add parameters to the command
            cmd.Parameters.Add("@ReceiptID", SqlDbType.Int);

            //We need to set the parameter values
            cmd.Parameters["@ReceiptID"].Value = ReceiptID;
            try
            {
                //open the connection 
                conn.Open();  //execute the command and capture result
                var reader = cmd.ExecuteReader();
                //process the results
                if (reader.HasRows)
                {
                    while (reader.Read())
                    {
                        output.ReceiptID = reader.GetInt32(0);
                        output.InvoiceID = reader.GetInt32(1);
                        output.RecipttDate = reader.GetDateTime(2);
                        output.ReceiptAmount = reader.GetFloat(3);

                    }
                }
                else
                {
                    throw new ArgumentException("Receipts not found");
                }

            }
            catch (Exception ex)
            {
                throw ex;
            }
            finally
            {
                conn.Close();
            }
            return output;
        }
        public List<Receipts> selectAllReceipts()
        {
            List<Receipts> output = new List<Receipts>();
            // start with a connection object
            var conn = SqlConnectionProvider.GetConnection();
            // set the command text
            var commandText = "sp_retreive_by_all_Receipts";
            // create the command object
            var cmd = new SqlCommand(commandText, conn);
            // set the command type
            cmd.CommandType = CommandType.StoredProcedure;
            // There are no parameters to set or add
            try
            {
                //open the connection 
                conn.Open();  //execute the command and capture result
                var reader = cmd.ExecuteReader();
                //process the results
                if (reader.HasRows)
                {
                    while (reader.Read())
                    {
                        var _Receipts = new Receipts();
                        _Receipts.ReceiptID = reader.GetInt32(0);
                        _Receipts.InvoiceID = reader.GetInt32(1);
                        _Receipts.RecipttDate = reader.GetDateTime(2);
                        _Receipts.ReceiptAmount = reader.GetFloat(3);
                        output.Add(_Receipts);
                    }
                }
            }
            catch (Exception ex)
            {
                throw ex;
            }
            finally
            {
                conn.Close();
            }
            return output;
        }
        public int updateReceipts(int oldReceiptID, int oldInvoiceID, DateTime oldRecipttDate, float oldReceiptAmount, int newInvoiceID, DateTime newRecipttDate, float newReceiptAmount)
        {
            int rows = 0;
            // start with a connection object
            var conn = SqlConnectionProvider.GetConnection();
            // set the command text
            var commandText = "sp_update_Receipts";
            // create the command object
            var cmd = new SqlCommand(commandText, conn);
            // set the command type
            cmd.CommandType = CommandType.StoredProcedure;
            // we need to add parameters to the command
            cmd.Parameters.Add("@oldReceiptID", SqlDbType.Int);

            cmd.Parameters.Add("@oldInvoiceID", SqlDbType.Int);
            cmd.Parameters.Add("@newInvoiceID", SqlDbType.Int);
            cmd.Parameters.Add("@oldRecipttDate", SqlDbType.DateTime);
            cmd.Parameters.Add("@newRecipttDate", SqlDbType.DateTime);
            cmd.Parameters.Add("@oldReceiptAmount", SqlDbType.Float);
            cmd.Parameters.Add("@newReceiptAmount", SqlDbType.Float);

            //We need to set the parameter values
            cmd.Parameters["@oldReceiptID"].Value = oldReceiptID;

            cmd.Parameters["@oldInvoiceID"].Value = oldInvoiceID;
            cmd.Parameters["@newInvoiceID"].Value = newInvoiceID;
            cmd.Parameters["@oldRecipttDate"].Value = oldRecipttDate;
            cmd.Parameters["@newRecipttDate"].Value = newRecipttDate;
            cmd.Parameters["@oldReceiptAmount"].Value = oldReceiptAmount;
            cmd.Parameters["@newReceiptAmount"].Value = newReceiptAmount;
            try
            {
                //open the connection 
                conn.Open();  //execute the command and capture result
                rows = cmd.ExecuteNonQuery();
                if (rows == 0)
                {
                    //treat failed update as exception 
                    throw new ArgumentException("invalid values, update failed");
                }
            }
            catch (Exception ex)
            {
                throw ex;
            }
            finally
            {
                conn.Close();
            }
            return rows;
        }
        public int deleteReceipts(string ReceiptsID)
        {
            int rows = 0;
            // start with a connection object
            var conn = SqlConnectionProvider.GetConnection();
            // set the command text
            var commandText = "sp_delete_Receipts";
            // create the command object
            var cmd = new SqlCommand(commandText, conn);
            // set the command type
            cmd.CommandType = CommandType.StoredProcedure;
            // we need to add parameters to the command
            cmd.Parameters["@ReceiptsID"].Value = ReceiptsID;
            try
            {
                conn.Open();
                rows = cmd.ExecuteNonQuery(); if (rows == 0)
                {
                    //treat failed delete as exepction
                    throw new ArgumentException("Invalid Primary Key");
                }
            }
            catch (Exception ex)
            {
                throw ex;
            }
            finally
            {
                conn.Close();
            }
            return rows;
        }
    }
}
