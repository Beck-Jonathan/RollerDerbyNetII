using DataAccessInterfaces;
using DataObjects;
using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;

namespace DataAccessLayer
{

    public class Game_Team_LineAccessor : IGame_Team_LineAccessor
    {
        public int insertGame_Team_Line(int GameID, string TeamId, int Points)
        {
            int rows = 0;
            // start with a connection object
            var conn = SqlConnectionProvider.GetConnection();
            // set the command text
            var commandText = "sp_insert_Game_Team_Line";
            // create the command object
            var cmd = new SqlCommand(commandText, conn);
            // set the command type
            cmd.CommandType = CommandType.StoredProcedure;
            // we need to add parameters to the command
            cmd.Parameters.Add("@GameID", SqlDbType.Int);
            cmd.Parameters.Add("@TeamId", SqlDbType.NVarChar, 50);
            cmd.Parameters.Add("@Points", SqlDbType.Int);

            //We need to set the parameter values
            cmd.Parameters["@GameID"].Value = GameID;
            cmd.Parameters["@TeamId"].Value = TeamId;
            cmd.Parameters["@Points"].Value = Points;
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
        public Game_Team_Line selectGame_Team_LineByPrimaryKey(int GameID, string TeamId)
        {
            Game_Team_Line output = new Game_Team_Line();
            // start with a connection object
            var conn = SqlConnectionProvider.GetConnection();
            // set the command text
            var commandText = "sp_retreive_by_pk_Game_Team_Line";
            // create the command object
            var cmd = new SqlCommand(commandText, conn);
            // set the command type
            cmd.CommandType = CommandType.StoredProcedure;
            // we need to add parameters to the command
            cmd.Parameters.Add("@GameID", SqlDbType.Int);
            cmd.Parameters.Add("@TeamId", SqlDbType.NVarChar, 50);

            //We need to set the parameter values
            cmd.Parameters["@GameID"].Value = GameID;
            cmd.Parameters["@TeamId"].Value = TeamId;
            try
            {
                //open the connection 
                conn.Open();  //execute the command and capture result
                var reader = cmd.ExecuteReader();
                //process the results
                if (reader.HasRows)
                    if (reader.Read())
                    {
                        output.GameID = reader.GetInt32(0);
                        output.TeamId = reader.GetString(1);
                        output.Points = reader.GetInt32(2);

                    }
                    else
                    {
                        throw new ArgumentException("Game_Team_Line not found");
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
        public List<Game_Team_Line> selectAllGame_Team_Line()
        {
            List<Game_Team_Line> output = new List<Game_Team_Line>();
            // start with a connection object
            var conn = SqlConnectionProvider.GetConnection();
            // set the command text
            var commandText = "sp_retreive_by_all_Game_Team_Line";
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
                        var _Game_Team_Line = new Game_Team_Line();
                        _Game_Team_Line.GameID = reader.GetInt32(0);
                        _Game_Team_Line.TeamId = reader.GetString(1);
                        _Game_Team_Line.Points = reader.GetInt32(2);
                        output.Add(_Game_Team_Line);
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
        public int updateGame_Team_Line(int oldGameID, string oldTeamId, int oldPoints, int newPoints)
        {
            int rows = 0;
            // start with a connection object
            var conn = SqlConnectionProvider.GetConnection();
            // set the command text
            var commandText = "sp_update_Game_Team_Line";
            // create the command object
            var cmd = new SqlCommand(commandText, conn);
            // set the command type
            cmd.CommandType = CommandType.StoredProcedure;
            // we need to add parameters to the command
            cmd.Parameters.Add("@oldGameID", SqlDbType.Int);

            cmd.Parameters.Add("@oldTeamId", SqlDbType.NVarChar, 50);

            cmd.Parameters.Add("@oldPoints", SqlDbType.Int);
            cmd.Parameters.Add("@newPoints", SqlDbType.Int);

            //We need to set the parameter values
            cmd.Parameters["@oldGameID"].Value = oldGameID;

            cmd.Parameters["@oldTeamId"].Value = oldTeamId;

            cmd.Parameters["@oldPoints"].Value = oldPoints;
            cmd.Parameters["@newPoints"].Value = newPoints;
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
        public int deleteGame_Team_Line(string Game_Team_LineID)
        {
            int rows = 0;
            // start with a connection object
            var conn = SqlConnectionProvider.GetConnection();
            // set the command text
            var commandText = "sp_delete_Game_Team_Line";
            // create the command object
            var cmd = new SqlCommand(commandText, conn);
            // set the command type
            cmd.CommandType = CommandType.StoredProcedure;
            // we need to add parameters to the command
            cmd.Parameters["@Game_Team_LineID"].Value = Game_Team_LineID;
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
