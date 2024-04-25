using DataAccessInterfaces;
using DataObjects;
using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;

namespace DataAccessLayer
{
    /******************
create the Accessor for the Invoice table
 Created By Jonathan Beck 4/20/2024
***************/
    public class InvoiceAccessor : IInvoiceAccessor
    {

        public int addInvoice(Invoice _invoice)
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
            cmd.Parameters.Add("@SkaterID", SqlDbType.NVarChar, 50);
            cmd.Parameters.Add("@InvoiceAmount", SqlDbType.Money);
            cmd.Parameters.Add("@IssueDate", SqlDbType.DateTime);
            

            //We need to set the parameter values
            cmd.Parameters["@SkaterID"].Value = _invoice.SkaterID;
            cmd.Parameters["@InvoiceAmount"].Value = _invoice.InvoiceAmount;
            cmd.Parameters["@IssueDate"].Value = _invoice.IssueDate;
            
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
                    if (reader.Read())
                    {
                        output.InvoiceID = reader.GetInt32(reader.GetOrdinal("Invoice_InvoiceID"));
                        output.SkaterID = reader.GetString(reader.GetOrdinal("Invoice_SkaterID"));
                        output.InvoiceAmount = (decimal)reader.GetSqlMoney(reader.GetOrdinal("Invoice_InvoiceAmount"));
                        output.IssueDate = reader.GetDateTime(reader.GetOrdinal("Invoice_IssueDate"));
                        output.is_active = reader.GetBoolean(reader.GetOrdinal("Invoice_is_active"));

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
                    while (reader.Read())
                    {
                        var _Invoice = new Invoice();
                        _Invoice.InvoiceID = reader.GetInt32(0);
                        _Invoice.SkaterID = reader.GetString(1);
                        _Invoice.InvoiceAmount = (decimal)reader.GetSqlMoney(2);
                        _Invoice.IssueDate = reader.GetDateTime(3);
                        _Invoice.is_active = reader.GetBoolean(4);
                        output.Add(_Invoice);
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

        public List<Invoice> selectInvoiceBySkater(string SkaterID) {

            List<Invoice> output = new List<Invoice>();
            // start with a connection object
            var conn = SqlConnectionProvider.GetConnection();
            // set the command text
            var commandText = "sp_retreive_by_skater_Invoice";
            // create the command object
            var cmd = new SqlCommand(commandText, conn);
            // set the command type
            cmd.CommandType = CommandType.StoredProcedure;
            // we need to add parameters to the command
            cmd.Parameters.Add("@SkaterID", SqlDbType.NVarChar,50);


            //We need to set the parameter values
            cmd.Parameters["@SkaterID"].Value = SkaterID;
            try
            {
                //open the connection 
                conn.Open();  //execute the command and capture result
                var reader = cmd.ExecuteReader();
                //process the results
                if (reader.HasRows)
                    while (reader.Read())
                    {
                        var _Invoice = new Invoice();
                        _Invoice.InvoiceID = reader.GetInt32(0);
                        _Invoice.SkaterID = reader.GetString(1);
                        _Invoice.InvoiceAmount = (decimal)reader.GetSqlMoney(2);
                        _Invoice.IssueDate = reader.GetDateTime(3);
                        _Invoice.is_active = reader.GetBoolean(4);
                        output.Add(_Invoice);
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
        public int updateInvoice(Invoice _oldInvoice, Invoice _newInvoice)
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
            cmd.Parameters.Add("@oldInvoiceAmount", SqlDbType.Money);
            cmd.Parameters.Add("@newInvoiceAmount", SqlDbType.Money);
            cmd.Parameters.Add("@oldIssueDate", SqlDbType.DateTime);
            cmd.Parameters.Add("@newIssueDate", SqlDbType.DateTime);
            cmd.Parameters.Add("@oldis_active", SqlDbType.Bit);
            cmd.Parameters.Add("@newis_active", SqlDbType.Bit);

            //We need to set the parameter values
            cmd.Parameters["@oldInvoiceID"].Value = _oldInvoice.InvoiceID;
            cmd.Parameters["@oldSkaterID"].Value = _oldInvoice.SkaterID;
            cmd.Parameters["@newSkaterID"].Value = _newInvoice.SkaterID;
            cmd.Parameters["@oldInvoiceAmount"].Value = _oldInvoice.InvoiceAmount;
            cmd.Parameters["@newInvoiceAmount"].Value = _newInvoice.InvoiceAmount;
            cmd.Parameters["@oldIssueDate"].Value = _oldInvoice.IssueDate;
            cmd.Parameters["@newIssueDate"].Value = _newInvoice.IssueDate;
            cmd.Parameters["@oldis_active"].Value = _oldInvoice.is_active;
            cmd.Parameters["@newis_active"].Value = _newInvoice.is_active;
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
        

        public int payInvoice(Invoice _invoice)
        {
            int rows = 0;
            // start with a connection object
            var conn = SqlConnectionProvider.GetConnection();
            // set the command text
            var commandText = "sp_pay_Invoice";
            // create the command object
            var cmd = new SqlCommand(commandText, conn);
            // set the command type
            cmd.CommandType = CommandType.StoredProcedure;
            // we need to add parameters to the command
            cmd.Parameters.Add("@InvoiceID", SqlDbType.Int);
            

            //We need to set the parameter values
            cmd.Parameters["@InvoiceID"].Value = _invoice.InvoiceID;
            
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
       

        public int refundInvoice(Invoice _invoice)
        {
            int rows = 0;
            // start with a connection object
            var conn = SqlConnectionProvider.GetConnection();
            // set the command text
            var commandText = "sp_undelete_Invoice";
            // create the command object
            var cmd = new SqlCommand(commandText, conn);
            // set the command type
            cmd.CommandType = CommandType.StoredProcedure;
            // we need to add parameters to the command
            cmd.Parameters.Add("@InvoiceID", SqlDbType.Int);
            

            //We need to set the parameter values
            cmd.Parameters["@InvoiceID"].Value = _invoice.InvoiceID;
            
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
        /// <summary>///A method that ________________
        ///_____________.
        ///</ summary >
        ///< param name = "a" > 
        ///____________
        ///</ param >
        ///< returns >
        ///< see cref = "int" > int </ see >: ____________.
        ///</ returns >
        ///< remarks >
        ///Parameters:
        ///< br />
        ///< see cref = "int" > int </ see > a: _______________///< br />< br />
        ///Exceptions:
        ///< br /> 
        ///< see cref = "ArgumentOutOfRangeException" > ArgumentOutOfRangeException </ see >:____________.
        ///< br />< br /> 
        ///CONTRIBUTOR: Jonathan Beck 
        ///< br /> 
        ///CREATED: 20/4/2024
        ///< br />< br />
        public List<String> selectDistinctSkaterForDropDown()
        {
            List<String> output = new List<String>();
            // start with a connection object
            var conn = SqlConnectionProvider.GetConnection();
            // set the command text
            var commandText = "sp_select_distinct_and_active_Skater_for_dropdown";
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
                    while (reader.Read())
                    {
                        String _Skater = reader.GetString(0);
                        output.Add(_Skater);
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
    }
}


