using DataAccessInterfaces;
using DataObjects;
using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;

namespace DataAccessLayer
{
    public class LeagueAccessor : ILeagueAccessor
    {
        public int addLeague(string LeagueID, string Region, string Gender)
        {
            int rows = 0;
            // start with a connection object
            var conn = SqlConnectionProvider.GetConnection();
            // set the command text
            var commandText = "sp_insert_League";
            // create the command object
            var cmd = new SqlCommand(commandText, conn);
            // set the command type
            cmd.CommandType = CommandType.StoredProcedure;
            // we need to add parameters to the command
            cmd.Parameters.Add("@LeagueID", SqlDbType.NVarChar, 100);
            cmd.Parameters.Add("@Region", SqlDbType.NVarChar, 100);
            cmd.Parameters.Add("@Gender", SqlDbType.NVarChar, 100);

            //We need to set the parameter values
            cmd.Parameters["@LeagueID"].Value = LeagueID;
            cmd.Parameters["@Region"].Value = Region;
            cmd.Parameters["@Gender"].Value = Gender;
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
        public League selectLeagueByPrimaryKey(string LeagueID)
        {
            League output = new League();
            // start with a connection object
            var conn = SqlConnectionProvider.GetConnection();
            // set the command text
            var commandText = "sp_retreive_by_pk_League";
            // create the command object
            var cmd = new SqlCommand(commandText, conn);
            // set the command type
            cmd.CommandType = CommandType.StoredProcedure;
            // we need to add parameters to the command
            cmd.Parameters.Add("@LeagueID", SqlDbType.NVarChar, 100);

            //We need to set the parameter values
            cmd.Parameters["@LeagueID"].Value = LeagueID;
            try
            {
                //open the connection 
                conn.Open();  //execute the command and capture result
                var reader = cmd.ExecuteReader();
                //process the results
                if (reader.HasRows)
                    if (reader.Read())
                    {
                        output.LeagueID = reader.GetString(0);
                        output.Region = reader.GetString(1);
                        output.Gender = reader.GetString(2);

                    }
                    else
                    {
                        throw new ArgumentException("League not found");
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
        public List<League> selectAllLeague()
        {
            List<League> output = new List<League>();
            // start with a connection object
            var conn = SqlConnectionProvider.GetConnection();
            // set the command text
            var commandText = "sp_retreive_by_all_League";
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
                        var _League = new League();
                        _League.LeagueID = reader.GetString(0);
                        _League.Region = reader.GetString(1);
                        _League.Gender = reader.GetString(2);
                        output.Add(_League);
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
        public int updateLeague(string oldLeagueID, string oldRegion, string oldGender, string newRegion, string newGender)
        {
            int rows = 0;
            // start with a connection object
            var conn = SqlConnectionProvider.GetConnection();
            // set the command text
            var commandText = "sp_update_League";
            // create the command object
            var cmd = new SqlCommand(commandText, conn);
            // set the command type
            cmd.CommandType = CommandType.StoredProcedure;
            // we need to add parameters to the command
            cmd.Parameters.Add("@oldLeagueID", SqlDbType.NVarChar, 100);

            cmd.Parameters.Add("@oldRegion", SqlDbType.NVarChar, 100);
            cmd.Parameters.Add("@newRegion", SqlDbType.NVarChar, 100);
            cmd.Parameters.Add("@oldGender", SqlDbType.NVarChar, 100);
            cmd.Parameters.Add("@newGender", SqlDbType.NVarChar, 100);

            //We need to set the parameter values
            cmd.Parameters["@oldLeagueID"].Value = oldLeagueID;

            cmd.Parameters["@oldRegion"].Value = oldRegion;
            cmd.Parameters["@newRegion"].Value = newRegion;
            cmd.Parameters["@oldGender"].Value = oldGender;
            cmd.Parameters["@newGender"].Value = newGender;
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
        public int deleteLeague(League league)
        {
            int rows = 0;
            // start with a connection object
            var conn = SqlConnectionProvider.GetConnection();
            // set the command text
            var commandText = "sp_delete_League";
            // create the command object
            var cmd = new SqlCommand(commandText, conn);
            // set the command type
            cmd.CommandType = CommandType.StoredProcedure;
            // we need to add parameters to the command
            cmd.Parameters.Add("@LeagueID", SqlDbType.NVarChar, 100);
            cmd.Parameters["@LeagueID"].Value = league.LeagueID;
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