using DataAccessInterfaces;
using DataObjects;
using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;

namespace DataAccessLayer
{
    public class SkaterteamApplicationAccessor : ISkaterTeamApplicationAccessor
    {
        public int addSkaterTeamApplication(int ApplicationID, string SkaterID, string TeamName, string ApplicationStatus)
        {
            int rows = 0;
            // start with a connection object
            var conn = SqlConnectionProvider.GetConnection();
            // set the command text
            var commandText = "sp_insert_SkaterteamApplication";
            // create the command object
            var cmd = new SqlCommand(commandText, conn);
            // set the command type
            cmd.CommandType = CommandType.StoredProcedure;
            // we need to add parameters to the command
            cmd.Parameters.Add("@ApplicationID", SqlDbType.Int);
            cmd.Parameters.Add("@SkaterID", SqlDbType.NVarChar, 50);
            cmd.Parameters.Add("@TeamName", SqlDbType.NVarChar, 50);
            cmd.Parameters.Add("@ApplicationStatus", SqlDbType.NVarChar, 50);

            //We need to set the parameter values
            cmd.Parameters["@ApplicationID"].Value = ApplicationID;
            cmd.Parameters["@SkaterID"].Value = SkaterID;
            cmd.Parameters["@TeamName"].Value = TeamName;
            cmd.Parameters["@ApplicationStatus"].Value = ApplicationStatus;
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
        public SkaterTeamApplication selectSkaterteamApplicationByPrimaryKey(int ApplicationID)
        {
            SkaterTeamApplication output = new SkaterTeamApplication();

            // start with a connection object
            var conn = SqlConnectionProvider.GetConnection();
            // set the command text
            var commandText = "sp_retreive_by_pk_SkaterteamApplication";
            // create the command object
            var cmd = new SqlCommand(commandText, conn);
            // set the command type
            cmd.CommandType = CommandType.StoredProcedure;
            // we need to add parameters to the command
            cmd.Parameters.Add("@ApplicationID", SqlDbType.Int);

            //We need to set the parameter values
            cmd.Parameters["@ApplicationID"].Value = ApplicationID;
            try
            {
                //open the connection 
                conn.Open();  //execute the command and capture result
                var reader = cmd.ExecuteReader();
                //process the results
                if (reader.HasRows)
                    if (reader.Read())
                    {

                        output.ApplicationID = reader.GetInt32(0);
                        output.skaterID = reader.GetString(1);
                        output.TeamName = reader.GetString(2);
                        output.ApplicationStatus = reader.GetString(3);

                    }
                    else
                    {
                        throw new ArgumentException("SkaterteamApplication not found");
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
        public List<SkaterTeamApplication> selectAllSkaterteamApplication()
        {
            List<SkaterTeamApplication> output = new List<SkaterTeamApplication>();
            // start with a connection object
            var conn = SqlConnectionProvider.GetConnection();
            // set the command text
            var commandText = "sp_retreive_by_all_SkaterteamApplication";
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
                        var _SkaterteamApplication = new SkaterTeamApplication();
                        _SkaterteamApplication.ApplicationID = reader.GetInt32(0);
                        _SkaterteamApplication.skaterID = reader.GetString(1);
                        _SkaterteamApplication.TeamName = reader.GetString(2);
                        _SkaterteamApplication.ApplicationStatus = reader.GetString(3);
                        output.Add(_SkaterteamApplication);
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
        public int updateSkaterteamApplication(int oldApplicationID, string oldSkaterID, string oldTeamName, string oldApplicationStatus, string newSkaterID, string newTeamName, string newApplicationStatus)
        {
            int rows = 0;
            // start with a connection object
            var conn = SqlConnectionProvider.GetConnection();
            // set the command text
            var commandText = "sp_update_SkaterteamApplication";
            // create the command object
            var cmd = new SqlCommand(commandText, conn);
            // set the command type
            cmd.CommandType = CommandType.StoredProcedure;
            // we need to add parameters to the command
            cmd.Parameters.Add("@oldApplicationID", SqlDbType.Int);
            cmd.Parameters.Add("@oldSkaterID", SqlDbType.NVarChar, 50);
            cmd.Parameters.Add("@newSkaterID", SqlDbType.NVarChar, 50);
            cmd.Parameters.Add("@oldTeamName", SqlDbType.NVarChar, 50);
            cmd.Parameters.Add("@newTeamName", SqlDbType.NVarChar, 50);
            cmd.Parameters.Add("@oldApplicationStatus", SqlDbType.NVarChar, 50);
            cmd.Parameters.Add("@newApplicationStatus", SqlDbType.NVarChar, 50);

            //We need to set the parameter values
            cmd.Parameters["@oldApplicationID"].Value = oldApplicationID;
            cmd.Parameters["@oldSkaterID"].Value = oldSkaterID;
            cmd.Parameters["@newSkaterID"].Value = newSkaterID;
            cmd.Parameters["@oldTeamName"].Value = oldTeamName;
            cmd.Parameters["@newTeamName"].Value = newTeamName;
            cmd.Parameters["@oldApplicationStatus"].Value = oldApplicationStatus;
            cmd.Parameters["@newApplicationStatus"].Value = newApplicationStatus;
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
        public int deleteSkaterteamApplication(int SkaterteamApplicationID)
        {
            int rows = 0;
            // start with a connection object
            var conn = SqlConnectionProvider.GetConnection();
            // set the command text
            var commandText = "sp_delete_SkaterteamApplication";
            // create the command object
            var cmd = new SqlCommand(commandText, conn);
            // set the command type
            cmd.CommandType = CommandType.StoredProcedure;
            // we need to add parameters to the command
            cmd.Parameters.Add("@SkaterteamApplicationID", SqlDbType.Int); cmd.Parameters["@SkaterteamApplicationID"].Value = SkaterteamApplicationID;
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

        public List<SkaterTeamApplication> selectSkaterTeamApplicationBySkaterID(Skater skater)
        {
            List<SkaterTeamApplication> output = new List<SkaterTeamApplication>();

            // start with a connection object
            var conn = SqlConnectionProvider.GetConnection();
            // set the command text
            var commandText = "sp_retreive_TeamApplication_by_SkaterID";
            // create the command object
            var cmd = new SqlCommand(commandText, conn);
            // set the command type
            cmd.CommandType = CommandType.StoredProcedure;
            // we need to add parameters to the command
            cmd.Parameters.Add("@SkaterID", SqlDbType.NVarChar, 50);

            //We need to set the parameter values
            cmd.Parameters["@SkaterID"].Value = skater.SkaterID;
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
                        var _SkaterteamApplication = new SkaterTeamApplication();
                        _SkaterteamApplication.ApplicationID = reader.GetInt32(0);
                        _SkaterteamApplication.skaterID = reader.GetString(1);
                        _SkaterteamApplication.TeamName = reader.GetString(2);
                        _SkaterteamApplication.ApplicationStatus = reader.GetString(3);
                        output.Add(_SkaterteamApplication);
                    }
                }
                else
                {
                    throw new ArgumentException("SkaterteamApplication not found");
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

