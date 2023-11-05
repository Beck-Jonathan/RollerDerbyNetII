using DataAccessInterfaces;
using DataObjects;
using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;

namespace DataAccessLayer
{
    public class PracticeAccessor : IPracticeAccessor
    {
        public int insertPractice(int PracticeID, string LocationID, DateTime DateTime)
        {
            int rows = 0;
            // start with a connection object
            var conn = SqlConnectionProvider.GetConnection();
            // set the command text
            var commandText = "sp_insert_Practice";
            // create the command object
            var cmd = new SqlCommand(commandText, conn);
            // set the command type
            cmd.CommandType = CommandType.StoredProcedure;
            // we need to add parameters to the command
            cmd.Parameters.Add("@PracticeID", SqlDbType.Int);
            cmd.Parameters.Add("@LocationID", SqlDbType.NVarChar, 100);
            cmd.Parameters.Add("@DateTime", SqlDbType.DateTime);

            //We need to set the parameter values
            cmd.Parameters["@PracticeID"].Value = PracticeID;
            cmd.Parameters["@LocationID"].Value = LocationID;
            cmd.Parameters["@DateTime"].Value = DateTime;
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
        public Practice selectPracticeByPrimaryKey(int PracticeID)
        {
            Practice output = new Practice();
            // start with a connection object
            var conn = SqlConnectionProvider.GetConnection();
            // set the command text
            var commandText = "sp_retreive_by_pk_Practice";
            // create the command object
            var cmd = new SqlCommand(commandText, conn);
            // set the command type
            cmd.CommandType = CommandType.StoredProcedure;
            // we need to add parameters to the command
            cmd.Parameters.Add("@PracticeID", SqlDbType.Int);

            //We need to set the parameter values
            cmd.Parameters["@PracticeID"].Value = PracticeID;
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
                        output.PracticeID = reader.GetInt32(0);
                        output.LocationID = reader.GetString(1);
                        output.DateTime = reader.GetDateTime(2);

                    }
                }
                else
                {
                    throw new ArgumentException("Practice not found");
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
        public List<Practice> selectAllPractice()
        {
            List<Practice> output = new List<Practice>();
            // start with a connection object
            var conn = SqlConnectionProvider.GetConnection();
            // set the command text
            var commandText = "sp_retreive_by_all_Practice";
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
                        var _Practice = new Practice();
                        _Practice.PracticeID = reader.GetInt32(0);
                        _Practice.LocationID = reader.GetString(1);
                        _Practice.DateTime = reader.GetDateTime(2);
                        output.Add(_Practice);
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
        public int updatePractice(int oldPracticeID, string oldLocationID, DateTime oldDateTime, string newLocationID, DateTime newDateTime)
        {
            int rows = 0;
            // start with a connection object
            var conn = SqlConnectionProvider.GetConnection();
            // set the command text
            var commandText = "sp_update_Practice";
            // create the command object
            var cmd = new SqlCommand(commandText, conn);
            // set the command type
            cmd.CommandType = CommandType.StoredProcedure;
            // we need to add parameters to the command
            cmd.Parameters.Add("@oldPracticeID", SqlDbType.Int);

            cmd.Parameters.Add("@oldLocationID", SqlDbType.NVarChar, 100);
            cmd.Parameters.Add("@newLocationID", SqlDbType.NVarChar, 100);
            cmd.Parameters.Add("@oldDateTime", SqlDbType.DateTime);
            cmd.Parameters.Add("@newDateTime", SqlDbType.DateTime);

            //We need to set the parameter values
            cmd.Parameters["@oldPracticeID"].Value = oldPracticeID;

            cmd.Parameters["@oldLocationID"].Value = oldLocationID;
            cmd.Parameters["@newLocationID"].Value = newLocationID;
            cmd.Parameters["@oldDateTime"].Value = oldDateTime;
            cmd.Parameters["@newDateTime"].Value = newDateTime;
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
        public int deletePractice(string PracticeID)
        {
            int rows = 0;
            // start with a connection object
            var conn = SqlConnectionProvider.GetConnection();
            // set the command text
            var commandText = "sp_delete_Practice";
            // create the command object
            var cmd = new SqlCommand(commandText, conn);
            // set the command type
            cmd.CommandType = CommandType.StoredProcedure;
            // we need to add parameters to the command
            cmd.Parameters["@PracticeID"].Value = PracticeID;
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
