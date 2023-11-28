using DataAccessInterfaces;
using DataObjects;
using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;

namespace DataAccessLayer
{

    public class TeamAccessor : ITeamAccessor
    {
        public int insertTeam(Team team)
        {
            int rows = 0;
            // start with a connection object
            var conn = SqlConnectionProvider.GetConnection();
            // set the command text
            var commandText = "sp_insert_Team";
            // create the command object
            var cmd = new SqlCommand(commandText, conn);
            // set the command type
            cmd.CommandType = CommandType.StoredProcedure;
            // we need to add parameters to the command
            cmd.Parameters.Add("@TeamId", SqlDbType.NVarChar, 50);
            cmd.Parameters.Add("@LeagueID", SqlDbType.NVarChar, 100);
            cmd.Parameters.Add("@MonthlyDue", SqlDbType.Money);

            cmd.Parameters.Add("@City", SqlDbType.NVarChar, 100);
            cmd.Parameters.Add("@State", SqlDbType.NVarChar, 50);
            cmd.Parameters.Add("@Zip", SqlDbType.NVarChar, 5);

            //We need to set the parameter values
            cmd.Parameters["@TeamId"].Value = team.TeamId;
            cmd.Parameters["@LeagueID"].Value = team.LeagueID;
            cmd.Parameters["@MonthlyDue"].Value = team.MonthlyDue;
            cmd.Parameters["@City"].Value = team.City;
            cmd.Parameters["@State"].Value = team.State;
            cmd.Parameters["@Zip"].Value = team.Zip;
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
        public Team selectTeamByPrimaryKey(string TeamId)
        {
            Team output = new Team();
            // start with a connection object
            var conn = SqlConnectionProvider.GetConnection();
            // set the command text
            var commandText = "sp_retreive_by_pk_Team";
            // create the command object
            var cmd = new SqlCommand(commandText, conn);
            // set the command type
            cmd.CommandType = CommandType.StoredProcedure;
            // we need to add parameters to the command
            cmd.Parameters.Add("@TeamId", SqlDbType.NVarChar, 50);

            //We need to set the parameter values
            cmd.Parameters["@TeamId"].Value = TeamId;
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
                        output.TeamId = reader.GetString(0);
                        output.LeagueID = reader.GetString(1);
                        output.MonthlyDue = reader.GetDecimal(2);
                        output.City = reader.GetString(3);
                        output.State = reader.GetString(4);
                        output.Zip = reader.GetString(5);

                    }
                }
                else
                {
                    throw new ArgumentException("Team not found");
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
        public List<Team> selectAllTeam()
        {
            List<Team> output = new List<Team>();
            // start with a connection object
            var conn = SqlConnectionProvider.GetConnection();
            // set the command text
            var commandText = "sp_retreive_by_all_Team";
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
                        var _Team = new Team();
                        _Team.TeamId = reader.GetString(0);
                        _Team.LeagueID = reader.GetString(1);
                        _Team.MonthlyDue = reader.GetDecimal(2);
                        _Team.City = reader.GetString(3);
                        _Team.State = reader.GetString(4);

                        _Team.Zip = reader.GetString(5);
                        output.Add(_Team);
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
        public int updateTeam(Team oldTeam, Team newTeam)
        {
            int rows = 0;
            // start with a connection object
            var conn = SqlConnectionProvider.GetConnection();
            // set the command text
            var commandText = "sp_update_Team";
            // create the command object
            var cmd = new SqlCommand(commandText, conn);
            // set the command type
            cmd.CommandType = CommandType.StoredProcedure;
            // we need to add parameters to the command
            cmd.Parameters.Add("@oldTeamId", SqlDbType.NVarChar, 50);

            cmd.Parameters.Add("@oldLeagueID", SqlDbType.NVarChar, 100);
            cmd.Parameters.Add("@newLeagueID", SqlDbType.NVarChar, 100);
            cmd.Parameters.Add("@oldMonthlyDue", SqlDbType.Money);
            cmd.Parameters.Add("@newMonthlyDue", SqlDbType.Money);
            cmd.Parameters.Add("@oldCity", SqlDbType.NVarChar, 100);
            cmd.Parameters.Add("@newCity", SqlDbType.NVarChar, 100);
            cmd.Parameters.Add("@oldState", SqlDbType.NVarChar, 50);
            cmd.Parameters.Add("@newState", SqlDbType.NVarChar, 50);
            cmd.Parameters.Add("@oldZip", SqlDbType.NVarChar, 5);
            cmd.Parameters.Add("@newZip", SqlDbType.NVarChar, 5);

            //We need to set the parameter values
            cmd.Parameters["@oldTeamId"].Value = oldTeam.TeamId;

            cmd.Parameters["@oldLeagueID"].Value = oldTeam.LeagueID;
            cmd.Parameters["@newLeagueID"].Value = newTeam.LeagueID;
            cmd.Parameters["@oldMonthlyDue"].Value = oldTeam.MonthlyDue;
            cmd.Parameters["@newMonthlyDue"].Value = newTeam.MonthlyDue;
            cmd.Parameters["@oldCity"].Value = oldTeam.City;
            cmd.Parameters["@newCity"].Value = newTeam.City;
            cmd.Parameters["@oldState"].Value = oldTeam.State;
            cmd.Parameters["@newState"].Value = newTeam.State;
            cmd.Parameters["@oldZip"].Value = oldTeam.Zip;
            cmd.Parameters["@newZip"].Value = newTeam.Zip;
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
        public int deleteTeam(string TeamID)
        {
            int rows = 0;
            // start with a connection object
            var conn = SqlConnectionProvider.GetConnection();
            // set the command text
            var commandText = "sp_delete_Team";
            // create the command object
            var cmd = new SqlCommand(commandText, conn);
            // set the command type
            cmd.CommandType = CommandType.StoredProcedure;
            // we need to add parameters to the command
            cmd.Parameters.Add("@TeamId", SqlDbType.NVarChar, 50);
            cmd.Parameters["@TeamID"].Value = TeamID;
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
