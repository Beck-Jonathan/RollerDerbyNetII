using DataAccessInterfaces;
using DataObjects;
using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;

namespace DataAccessLayer
{
    public class TeamApplicationAccessor : ITeamApplicationAccessor
    {
        public int addTeamApplication(TeamApplication _teamapplication)
        {
            int rows = 0;
            // start with a connection object
            var conn = SqlConnectionProvider.GetConnection();
            // set the command text
            var commandText = "sp_insertTeamApplication";
            // create the command object
            var cmd = new SqlCommand(commandText, conn);
            // set the command type
            cmd.CommandType = CommandType.StoredProcedure;
            // we need to add parameters to the command

            cmd.Parameters.Add("@SkaterID", SqlDbType.NVarChar, 50);
            cmd.Parameters.Add("@TeamID", SqlDbType.NVarChar, 50);



            //We need to set the parameter values

            cmd.Parameters["@SkaterID"].Value = _teamapplication.SkaterID;
            cmd.Parameters["@TeamID"].Value = _teamapplication.TeamName;


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

        public List<TeamApplication> selectAllTeamApplication()
        {
            List<TeamApplication> output = new List<TeamApplication>();
            // start with a connection object
            var conn = SqlConnectionProvider.GetConnection();
            // set the command text
            var commandText = "sp_retreive_by_all_TeamApplication";
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
                        var _TeamApplication = new TeamApplication();
                        _TeamApplication.TeamApplicationID = reader.GetInt32(0);
                        _TeamApplication.SkaterID = reader.GetString(1);
                        _TeamApplication.TeamName = reader.GetString(2);
                        _TeamApplication.ApplicationTime = reader.GetDateTime(3);
                        _TeamApplication.ApplicationStatus = reader.GetString(4);
                        output.Add(_TeamApplication);
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

        public TeamApplication selectTeamApplicationByPrimaryKey(int TeamApplicationID)
        {

            TeamApplication output = new TeamApplication();
            // start with a connection object
            var conn = SqlConnectionProvider.GetConnection();
            // set the command text
            var commandText = "sp_retreive_by_pk_TeamApplication";
            // create the command object
            var cmd = new SqlCommand(commandText, conn);
            // set the command type
            cmd.CommandType = CommandType.StoredProcedure;
            // we need to add parameters to the command
            cmd.Parameters.Add("@TeamApplicationID", SqlDbType.Int);

            //We need to set the parameter values
            cmd.Parameters["@TeamApplicationID"].Value = TeamApplicationID;
            try
            {
                //open the connection 
                conn.Open();  //execute the command and capture result
                var reader = cmd.ExecuteReader();
                //process the results
                if (reader.HasRows)
                    if (reader.Read())
                    {
                        output.TeamApplicationID = reader.GetInt32(0);
                        output.SkaterID = reader.GetString(1);
                        output.TeamName = reader.GetString(2);
                        output.ApplicationTime = reader.GetDateTime(3);
                        output.ApplicationStatus = reader.GetString(4);

                    }
                    else
                    {
                        throw new ArgumentException("TeamApplication not found");
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


        public int updateTeamApplication(TeamApplication _oldTeamApplication, TeamApplication _newTeamApplication)
        {
            int rows = 0;
            // start with a connection object
            var conn = SqlConnectionProvider.GetConnection();
            // set the command text
            var commandText = "sp_update_TeamApplication";
            // create the command object
            var cmd = new SqlCommand(commandText, conn);
            // set the command type
            cmd.CommandType = CommandType.StoredProcedure;
            // we need to add parameters to the command
            cmd.Parameters.Add("@oldTeamApplicationID", SqlDbType.Int);
            cmd.Parameters.Add("@oldSkaterID", SqlDbType.NVarChar, 50);

            cmd.Parameters.Add("@oldTeamID", SqlDbType.NVarChar, 50);

            cmd.Parameters.Add("@oldApplicationTime", SqlDbType.DateTime);

            cmd.Parameters.Add("@oldApplicationStatus", SqlDbType.NVarChar, 50);
            cmd.Parameters.Add("@newApplicationStatus", SqlDbType.NVarChar, 50);

            //We need to set the parameter values
            cmd.Parameters["@oldTeamApplicationID"].Value = _oldTeamApplication.TeamApplicationID;
            cmd.Parameters["@oldSkaterID"].Value = _oldTeamApplication.SkaterID;

            cmd.Parameters["@oldTeamID"].Value = _oldTeamApplication.TeamName;

            cmd.Parameters["@oldApplicationTime"].Value = _oldTeamApplication.ApplicationTime;

            cmd.Parameters["@oldApplicationStatus"].Value = _oldTeamApplication.ApplicationStatus;
            cmd.Parameters["@newApplicationStatus"].Value = _newTeamApplication.ApplicationStatus;
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
    }
}
