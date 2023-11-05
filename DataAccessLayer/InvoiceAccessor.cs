using DataAccessInterfaces;
using DataObjects;
using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;

namespace DataAccessLayer
{
    public class InvoiceAccessor : IInvoiceAccessor
    {
        public int insertInvoice(int InvoiceID, string SkaterID, float InvoiceAmount, DateTime IssueDate)
        {
            int rows = 0;
            // start with a connection object
            var conn = SqlConnectionProvider.GetConnection();
            // set the command text
            var commandText = "sp_insert_Invoice";
            // create the command object
            var cmd = new SqlCommand(commandText, conn);
            // set the command type
            cmd.CommandType = CommandType.StoredProcedure;
            // we need to add parameters to the command
            cmd.Parameters.Add("@InvoiceID", SqlDbType.Int);
            cmd.Parameters.Add("@SkaterID", SqlDbType.NVarChar, 50);
            cmd.Parameters.Add("@InvoiceAmount", SqlDbType.Float);
            cmd.Parameters.Add("@IssueDate", SqlDbType.DateTime);

            //We need to set the parameter values
            cmd.Parameters["@InvoiceID"].Value = InvoiceID;
            cmd.Parameters["@SkaterID"].Value = SkaterID;
            cmd.Parameters["@InvoiceAmount"].Value = InvoiceAmount;
            cmd.Parameters["@IssueDate"].Value = IssueDate;
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
        public Invoice selectInvoiceByPrimaryKey(int InvoiceID)
        {
            Invoice output = new Invoice();
            // start with a connection object
            var conn = SqlConnectionProvider.GetConnection();
            // set the command text
            var commandText = "sp_retreive_by_pk_Invoice";
            // create the command object
            var cmd = new SqlCommand(commandText, conn);
            // set the command type
            cmd.CommandType = CommandType.StoredProcedure;
            // we need to add parameters to the command
            cmd.Parameters.Add("@InvoiceID", SqlDbType.Int);

            //We need to set the parameter values
            cmd.Parameters["@InvoiceID"].Value = InvoiceID;
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
                        output.InvoiceID = reader.GetInt32(0);
                        output.SkaterID = reader.GetString(1);
                        output.InvoiceAmount = reader.GetFloat(2);
                        output.IssueDate = reader.GetDateTime(3);

                    }
                }
                else
                {
                    throw new ArgumentException("Invoice not found");
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
        public List<Invoice> selectAllInvoice()
        {
            List<Invoice> output = new List<Invoice>();
            // start with a connection object
            var conn = SqlConnectionProvider.GetConnection();
            // set the command text
            var commandText = "sp_retreive_by_all_Invoice";
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
                        var _Invoice = new Invoice();
                        _Invoice.InvoiceID = reader.GetInt32(0);
                        _Invoice.SkaterID = reader.GetString(1);
                        _Invoice.InvoiceAmount = reader.GetFloat(2);
                        _Invoice.IssueDate = reader.GetDateTime(3);
                        output.Add(_Invoice);
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
        public int updateInvoice(int oldInvoiceID, string oldSkaterID, float oldInvoiceAmount, DateTime oldIssueDate, string newSkaterID, float newInvoiceAmount, DateTime newIssueDate)
        {
            int rows = 0;
            // start with a connection object
            var conn = SqlConnectionProvider.GetConnection();
            // set the command text
            var commandText = "sp_update_Invoice";
            // create the command object
            var cmd = new SqlCommand(commandText, conn);
            // set the command type
            cmd.CommandType = CommandType.StoredProcedure;
            // we need to add parameters to the command
            cmd.Parameters.Add("@oldInvoiceID", SqlDbType.Int);

            cmd.Parameters.Add("@oldSkaterID", SqlDbType.NVarChar, 50);
            cmd.Parameters.Add("@newSkaterID", SqlDbType.NVarChar, 50);
            cmd.Parameters.Add("@oldInvoiceAmount", SqlDbType.Float);
            cmd.Parameters.Add("@newInvoiceAmount", SqlDbType.Float);
            cmd.Parameters.Add("@oldIssueDate", SqlDbType.DateTime);
            cmd.Parameters.Add("@newIssueDate", SqlDbType.DateTime);

            //We need to set the parameter values
            cmd.Parameters["@oldInvoiceID"].Value = oldInvoiceID;

            cmd.Parameters["@oldSkaterID"].Value = oldSkaterID;
            cmd.Parameters["@newSkaterID"].Value = newSkaterID;
            cmd.Parameters["@oldInvoiceAmount"].Value = oldInvoiceAmount;
            cmd.Parameters["@newInvoiceAmount"].Value = newInvoiceAmount;
            cmd.Parameters["@oldIssueDate"].Value = oldIssueDate;
            cmd.Parameters["@newIssueDate"].Value = newIssueDate;
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
        public int deleteInvoice(string InvoiceID)
        {
            int rows = 0;
            // start with a connection object
            var conn = SqlConnectionProvider.GetConnection();
            // set the command text
            var commandText = "sp_delete_Invoice";
            // create the command object
            var cmd = new SqlCommand(commandText, conn);
            // set the command type
            cmd.CommandType = CommandType.StoredProcedure;
            // we need to add parameters to the command
            cmd.Parameters["@InvoiceID"].Value = InvoiceID;
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
