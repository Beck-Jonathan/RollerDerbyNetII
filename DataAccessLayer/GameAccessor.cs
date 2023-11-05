using DataAccessInterfaces;
using DataObjects;
using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;

namespace DataAccessLayer
{

    public class GameAccessor : IGameAccessor
    {
        public int insertGame(int GameID, string LocationID, DateTime Date)
        {
            int rows = 0;
            // start with a connection object
            var conn = SqlConnectionProvider.GetConnection();
            // set the command text
            var commandText = "sp_insert_Game";
            // create the command object
            var cmd = new SqlCommand(commandText, conn);
            // set the command type
            cmd.CommandType = CommandType.StoredProcedure;
            // we need to add parameters to the command
            cmd.Parameters.Add("@GameID", SqlDbType.Int);
            cmd.Parameters.Add("@LocationID", SqlDbType.NVarChar, 100);
            cmd.Parameters.Add("@Date", SqlDbType.DateTime);

            //We need to set the parameter values
            cmd.Parameters["@GameID"].Value = GameID;
            cmd.Parameters["@LocationID"].Value = LocationID;
            cmd.Parameters["@Date"].Value = Date;
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
        public Game selectGameByPrimaryKey(int GameID)
        {
            Game output = new Game();
            // start with a connection object
            var conn = SqlConnectionProvider.GetConnection();
            // set the command text
            var commandText = "sp_retreive_by_pk_Game";
            // create the command object
            var cmd = new SqlCommand(commandText, conn);
            // set the command type
            cmd.CommandType = CommandType.StoredProcedure;
            // we need to add parameters to the command
            cmd.Parameters.Add("@GameID", SqlDbType.Int);

            //We need to set the parameter values
            cmd.Parameters["@GameID"].Value = GameID;
            try
            {
                //open the connection 
                conn.Open();  //execute the command and capture result
                var reader = cmd.ExecuteReader();
                //process the results
                if (reader.HasRows)
                    while (reader.Read())
                    {
                        output.GameID = reader.GetInt32(0);
                        output.LocationID = reader.GetString(1);
                        output.Date = reader.GetDateTime(2);

                    }
                else
                {
                    throw new ArgumentException("Game not found");
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
        public List<Game> selectAllGame()
        {
            List<Game> output = new List<Game>();
            // start with a connection object
            var conn = SqlConnectionProvider.GetConnection();
            // set the command text
            var commandText = "sp_retreive_by_all_Game";
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
                        var _Game = new Game();
                        _Game.GameID = reader.GetInt32(0);
                        _Game.LocationID = reader.GetString(1);
                        _Game.Date = reader.GetDateTime(2);
                        output.Add(_Game);
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
        public int updateGame(int oldGameID, string oldLocationID, DateTime oldDate, string newLocationID, DateTime newDate)
        {
            int rows = 0;
            // start with a connection object
            var conn = SqlConnectionProvider.GetConnection();
            // set the command text
            var commandText = "sp_update_Game";
            // create the command object
            var cmd = new SqlCommand(commandText, conn);
            // set the command type
            cmd.CommandType = CommandType.StoredProcedure;
            // we need to add parameters to the command
            cmd.Parameters.Add("@oldGameID", SqlDbType.Int);

            cmd.Parameters.Add("@oldLocationID", SqlDbType.NVarChar, 100);
            cmd.Parameters.Add("@newLocationID", SqlDbType.NVarChar, 100);
            cmd.Parameters.Add("@oldDate", SqlDbType.DateTime);
            cmd.Parameters.Add("@newDate", SqlDbType.DateTime);

            //We need to set the parameter values
            cmd.Parameters["@oldGameID"].Value = oldGameID;

            cmd.Parameters["@oldLocationID"].Value = oldLocationID;
            cmd.Parameters["@newLocationID"].Value = newLocationID;
            cmd.Parameters["@oldDate"].Value = oldDate;
            cmd.Parameters["@newDate"].Value = newDate;
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
        public int deleteGame(string GameID)
        {
            int rows = 0;
            // start with a connection object
            var conn = SqlConnectionProvider.GetConnection();
            // set the command text
            var commandText = "sp_delete_Game";
            // create the command object
            var cmd = new SqlCommand(commandText, conn);
            // set the command type
            cmd.CommandType = CommandType.StoredProcedure;
            // we need to add parameters to the command
            cmd.Parameters["@GameID"].Value = GameID;
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
