using DataAccessInterfaces;
using DataObjects;
using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;

namespace DataAccessLayer
{

    public class Skater_practice_LineAccessor : ISkaterpracticeLineAccessor
    {
        public int insertSkater_practice_Line(string SkaterID, int PracticeID)
        {
            int rows = 0;
            // start with a connection object
            var conn = SqlConnectionProvider.GetConnection();
            // set the command text
            var commandText = "sp_insert_Skater_practice_Line";
            // create the command object
            var cmd = new SqlCommand(commandText, conn);
            // set the command type
            cmd.CommandType = CommandType.StoredProcedure;
            // we need to add parameters to the command
            cmd.Parameters.Add("@SkaterID", SqlDbType.NVarChar, 50);
            cmd.Parameters.Add("@PracticeID", SqlDbType.Int);

            //We need to set the parameter values
            cmd.Parameters["@SkaterID"].Value = SkaterID;
            cmd.Parameters["@PracticeID"].Value = PracticeID;
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
        public Skater_practice_Line selectSkater_practice_LineByPrimaryKey(string SkaterID, int PracticeID)
        {
            Skater_practice_Line output = new Skater_practice_Line();
            // start with a connection object
            var conn = SqlConnectionProvider.GetConnection();
            // set the command text
            var commandText = "sp_retreive_by_pk_Skater_practice_Line";
            // create the command object
            var cmd = new SqlCommand(commandText, conn);
            // set the command type
            cmd.CommandType = CommandType.StoredProcedure;
            // we need to add parameters to the command
            cmd.Parameters.Add("@SkaterID", SqlDbType.NVarChar, 50);
            cmd.Parameters.Add("@PracticeID", SqlDbType.Int);

            //We need to set the parameter values
            cmd.Parameters["@SkaterID"].Value = SkaterID;
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
                        output.SkaterID = reader.GetString(0);
                        output.PracticeID = reader.GetInt32(1);

                    }
                }
                else
                {
                    throw new ArgumentException("Skater_practice_Line not found");
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
        public List<Skater_practice_Line> selectAllSkater_practice_Line()
        {
            List<Skater_practice_Line> output = new List<Skater_practice_Line>();
            // start with a connection object
            var conn = SqlConnectionProvider.GetConnection();
            // set the command text
            var commandText = "sp_retreive_by_all_Skater_practice_Line";
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
                        var _Skater_practice_Line = new Skater_practice_Line();
                        _Skater_practice_Line.SkaterID = reader.GetString(0);
                        _Skater_practice_Line.PracticeID = reader.GetInt32(1);
                        output.Add(_Skater_practice_Line);
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
        public int updateSkater_practice_Line(string oldSkaterID, int oldPracticeID, string newSkaterID, int newPracticeID)
        {
            int rows = 0;
            // start with a connection object
            var conn = SqlConnectionProvider.GetConnection();
            // set the command text
            var commandText = "sp_update_Skater_practice_Line";
            // create the command object
            var cmd = new SqlCommand(commandText, conn);
            // set the command type
            cmd.CommandType = CommandType.StoredProcedure;
            // we need to add parameters to the command
            cmd.Parameters.Add("@oldSkaterID", SqlDbType.NVarChar, 50);
            cmd.Parameters.Add("@newSkaterID", SqlDbType.NVarChar, 50);
            cmd.Parameters.Add("@oldPracticeID", SqlDbType.Int);
            cmd.Parameters.Add("@newPracticeID", SqlDbType.Int);

            //We need to set the parameter values
            cmd.Parameters["@oldSkaterID"].Value = oldSkaterID;
            cmd.Parameters["@newSkaterID"].Value = newSkaterID;
            cmd.Parameters["@oldPracticeID"].Value = oldPracticeID;
            cmd.Parameters["@newPracticeID"].Value = newPracticeID;
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
        public int deleteSkater_practice_Line(string Skater_practice_LineID)
        {
            int rows = 0;
            // start with a connection object
            var conn = SqlConnectionProvider.GetConnection();
            // set the command text
            var commandText = "sp_delete_Skater_practice_Line";
            // create the command object
            var cmd = new SqlCommand(commandText, conn);
            // set the command type
            cmd.CommandType = CommandType.StoredProcedure;
            // we need to add parameters to the command
            cmd.Parameters["@Skater_practice_LineID"].Value = Skater_practice_LineID;
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
