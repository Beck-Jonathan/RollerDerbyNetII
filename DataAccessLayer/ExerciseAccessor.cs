using DataAccessInterfaces;
using DataObjects;
using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;

namespace DataAccessLayer
{
    public class ExerciseAccessor : IExerciseAccessor
    {
        public int insertExercise(string ExerciseID, int Exercise_count, string Exercise_units)
        {
            int rows = 0;
            // start with a connection object
            var conn = SqlConnectionProvider.GetConnection();
            // set the command text
            var commandText = "sp_insert_Exercise";
            // create the command object
            var cmd = new SqlCommand(commandText, conn);
            // set the command type
            cmd.CommandType = CommandType.StoredProcedure;
            // we need to add parameters to the command
            cmd.Parameters.Add("@ExerciseID", SqlDbType.NVarChar, 50);
            cmd.Parameters.Add("@Exercise_count", SqlDbType.Int);
            cmd.Parameters.Add("@Exercise_units", SqlDbType.NVarChar, 50);

            //We need to set the parameter values
            cmd.Parameters["@ExerciseID"].Value = ExerciseID;
            cmd.Parameters["@Exercise_count"].Value = Exercise_count;
            cmd.Parameters["@Exercise_units"].Value = Exercise_units;
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
        public Exercise selectExerciseByPrimaryKey(string ExerciseID)
        {
            Exercise output = new Exercise();
            // start with a connection object
            var conn = SqlConnectionProvider.GetConnection();
            // set the command text
            var commandText = "sp_retreive_by_pk_Exercise";
            // create the command object
            var cmd = new SqlCommand(commandText, conn);
            // set the command type
            cmd.CommandType = CommandType.StoredProcedure;
            // we need to add parameters to the command
            cmd.Parameters.Add("@ExerciseID", SqlDbType.NVarChar, 50);

            //We need to set the parameter values
            cmd.Parameters["@ExerciseID"].Value = ExerciseID;
            try
            {
                //open the connection 
                conn.Open();  //execute the command and capture result
                var reader = cmd.ExecuteReader();
                //process the results
                if (reader.HasRows)
                    if (reader.Read())
                    {
                        output.ExerciseID = reader.GetString(0);
                        output.Exercise_count = reader.GetInt32(1);
                        output.Exercise_units = reader.GetString(2);

                    }
                    else
                    {
                        throw new ArgumentException("Exercise not found");
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
        public List<Exercise> selectAllExercise()
        {
            List<Exercise> output = new List<Exercise>();
            // start with a connection object
            var conn = SqlConnectionProvider.GetConnection();
            // set the command text
            var commandText = "sp_retreive_by_all_Exercise";
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
                        var _Exercise = new Exercise();
                        _Exercise.ExerciseID = reader.GetString(0);
                        _Exercise.Exercise_count = reader.GetInt32(1);
                        _Exercise.Exercise_units = reader.GetString(2);
                        output.Add(_Exercise);
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
        public int updateExercise(string oldExerciseID, int oldExercise_count, string oldExercise_units, int newExercise_count, string newExercise_units)
        {
            int rows = 0;
            // start with a connection object
            var conn = SqlConnectionProvider.GetConnection();
            // set the command text
            var commandText = "sp_update_Exercise";
            // create the command object
            var cmd = new SqlCommand(commandText, conn);
            // set the command type
            cmd.CommandType = CommandType.StoredProcedure;
            // we need to add parameters to the command
            cmd.Parameters.Add("@oldExerciseID", SqlDbType.NVarChar, 50);

            cmd.Parameters.Add("@oldExercise_count", SqlDbType.Int);
            cmd.Parameters.Add("@newExercise_count", SqlDbType.Int);
            cmd.Parameters.Add("@oldExercise_units", SqlDbType.NVarChar, 50);
            cmd.Parameters.Add("@newExercise_units", SqlDbType.NVarChar, 50);

            //We need to set the parameter values
            cmd.Parameters["@oldExerciseID"].Value = oldExerciseID;

            cmd.Parameters["@oldExercise_count"].Value = oldExercise_count;
            cmd.Parameters["@newExercise_count"].Value = newExercise_count;
            cmd.Parameters["@oldExercise_units"].Value = oldExercise_units;
            cmd.Parameters["@newExercise_units"].Value = newExercise_units;
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
        public int deleteExercise(string ExerciseID)
        {
            int rows = 0;
            // start with a connection object
            var conn = SqlConnectionProvider.GetConnection();
            // set the command text
            var commandText = "sp_delete_Exercise";
            // create the command object
            var cmd = new SqlCommand(commandText, conn);
            // set the command type
            cmd.CommandType = CommandType.StoredProcedure;
            // we need to add parameters to the command
            cmd.Parameters["@ExerciseID"].Value = ExerciseID;
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
