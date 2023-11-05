using DataAccessInterfaces;
using DataObjects;
using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;

namespace DataAccessLayer
{

    public class exercise_practice_lineAccessor : Iexercise_practice_lineAccessor
    {
        public int insertexercise_practice_line(string ExerciseID, int PracticeID, int Count)
        {
            int rows = 0;
            // start with a connection object
            var conn = SqlConnectionProvider.GetConnection();
            // set the command text
            var commandText = "sp_insert_exercise_practice_line";
            // create the command object
            var cmd = new SqlCommand(commandText, conn);
            // set the command type
            cmd.CommandType = CommandType.StoredProcedure;
            // we need to add parameters to the command
            cmd.Parameters.Add("@ExerciseID", SqlDbType.NVarChar, 50);
            cmd.Parameters.Add("@PracticeID", SqlDbType.Int);
            cmd.Parameters.Add("@Count", SqlDbType.Int);

            //We need to set the parameter values
            cmd.Parameters["@ExerciseID"].Value = ExerciseID;
            cmd.Parameters["@PracticeID"].Value = PracticeID;
            cmd.Parameters["@Count"].Value = Count;
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
        public exercise_practice_line selectexercise_practice_lineByPrimaryKey(string ExerciseID, int PracticeID)
        {
            exercise_practice_line output = new exercise_practice_line();
            // start with a connection object
            var conn = SqlConnectionProvider.GetConnection();
            // set the command text
            var commandText = "sp_retreive_by_pk_exercise_practice_line";
            // create the command object
            var cmd = new SqlCommand(commandText, conn);
            // set the command type
            cmd.CommandType = CommandType.StoredProcedure;
            // we need to add parameters to the command
            cmd.Parameters.Add("@ExerciseID", SqlDbType.NVarChar, 50);
            cmd.Parameters.Add("@PracticeID", SqlDbType.Int);

            //We need to set the parameter values
            cmd.Parameters["@ExerciseID"].Value = ExerciseID;
            cmd.Parameters["@PracticeID"].Value = PracticeID;
            try
            {
                //open the connection 
                conn.Open();  //execute the command and capture result
                var reader = cmd.ExecuteReader();
                //process the results
                if (reader.HasRows)
                {
                    if (reader.Read())
                    {
                        output.ExerciseID = reader.GetString(0);
                        output.PracticeID = reader.GetInt32(1);
                        output.Count = reader.GetInt32(2);

                    }
                    else
                    {
                        throw new ArgumentException("exercise_practice_line not found");
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
        public List<exercise_practice_line> selectAllexercise_practice_line()
        {
            List<exercise_practice_line> output = new List<exercise_practice_line>();
            // start with a connection object
            var conn = SqlConnectionProvider.GetConnection();
            // set the command text
            var commandText = "sp_retreive_by_all_exercise_practice_line";
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
                    if (reader.Read())
                    {
                        var _exercise_practice_line = new exercise_practice_line();
                        _exercise_practice_line.ExerciseID = reader.GetString(0);
                        _exercise_practice_line.PracticeID = reader.GetInt32(1);
                        _exercise_practice_line.Count = reader.GetInt32(2);
                        output.Add(_exercise_practice_line);
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
        public int updateexercise_practice_line(string oldExerciseID, int oldPracticeID, int oldCount, int newCount)
        {
            int rows = 0;
            // start with a connection object
            var conn = SqlConnectionProvider.GetConnection();
            // set the command text
            var commandText = "sp_update_exercise_practice_line";
            // create the command object
            var cmd = new SqlCommand(commandText, conn);
            // set the command type
            cmd.CommandType = CommandType.StoredProcedure;
            // we need to add parameters to the command
            cmd.Parameters.Add("@oldExerciseID", SqlDbType.NVarChar, 50);
            cmd.Parameters.Add("@oldPracticeID", SqlDbType.Int);

            cmd.Parameters.Add("@oldCount", SqlDbType.Int);
            cmd.Parameters.Add("@newCount", SqlDbType.Int);

            //We need to set the parameter values
            cmd.Parameters["@oldExerciseID"].Value = oldExerciseID;

            cmd.Parameters["@oldPracticeID"].Value = oldPracticeID;

            cmd.Parameters["@oldCount"].Value = oldCount;
            cmd.Parameters["@newCount"].Value = newCount;
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
        public int deleteexercise_practice_line(string exercise_practice_lineID)
        {
            int rows = 0;
            // start with a connection object
            var conn = SqlConnectionProvider.GetConnection();
            // set the command text
            var commandText = "sp_delete_exercise_practice_line";
            // create the command object
            var cmd = new SqlCommand(commandText, conn);
            // set the command type
            cmd.CommandType = CommandType.StoredProcedure;
            // we need to add parameters to the command
            cmd.Parameters["@exercise_practice_lineID"].Value = exercise_practice_lineID;
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
