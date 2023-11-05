using DataAccessInterfaces;
using DataObjects;
using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;

namespace DataAccessLayer
{

    public class Skater_Role_LineAccessor : ISkater_Role_LineAccessor
    {
        public int insertSkater_Role_Line(string RoleID, string SkaterID)
        {
            int rows = 0;
            // start with a connection object
            var conn = SqlConnectionProvider.GetConnection();
            // set the command text
            var commandText = "sp_insert_Skater_Role_Line";
            // create the command object
            var cmd = new SqlCommand(commandText, conn);
            // set the command type
            cmd.CommandType = CommandType.StoredProcedure;
            // we need to add parameters to the command
            cmd.Parameters.Add("@RoleID", SqlDbType.NVarChar, 50);
            cmd.Parameters.Add("@SkaterID", SqlDbType.NVarChar, 50);

            //We need to set the parameter values
            cmd.Parameters["@RoleID"].Value = RoleID;
            cmd.Parameters["@SkaterID"].Value = SkaterID;
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
        public Skater_Role_Line selectSkater_Role_LineByPrimaryKey(string RoleID, string SkaterID)
        {
            Skater_Role_Line output = new Skater_Role_Line();
            // start with a connection object
            var conn = SqlConnectionProvider.GetConnection();
            // set the command text
            var commandText = "sp_retreive_by_pk_Skater_Role_Line";
            // create the command object
            var cmd = new SqlCommand(commandText, conn);
            // set the command type
            cmd.CommandType = CommandType.StoredProcedure;
            // we need to add parameters to the command
            cmd.Parameters.Add("@RoleID", SqlDbType.NVarChar, 50);
            cmd.Parameters.Add("@SkaterID", SqlDbType.NVarChar, 50);

            //We need to set the parameter values
            cmd.Parameters["@RoleID"].Value = RoleID;
            cmd.Parameters["@SkaterID"].Value = SkaterID;
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
                        output.RoleID = reader.GetString(0);
                        output.SkaterID = reader.GetString(1);

                    }
                }
                else
                {
                    throw new ArgumentException("Skater_Role_Line not found");
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
        public List<Skater_Role_Line> selectAllSkater_Role_Line()
        {
            List<Skater_Role_Line> output = new List<Skater_Role_Line>();
            // start with a connection object
            var conn = SqlConnectionProvider.GetConnection();
            // set the command text
            var commandText = "sp_retreive_by_all_Skater_Role_Line";
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
                        var _Skater_Role_Line = new Skater_Role_Line();
                        _Skater_Role_Line.RoleID = reader.GetString(0);
                        _Skater_Role_Line.SkaterID = reader.GetString(1);
                        output.Add(_Skater_Role_Line);
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
        public int updateSkater_Role_Line(string oldRoleID, string oldSkaterID, string newRoleID, string newSkaterID)
        {
            int rows = 0;
            // start with a connection object
            var conn = SqlConnectionProvider.GetConnection();
            // set the command text
            var commandText = "sp_update_Skater_Role_Line";
            // create the command object
            var cmd = new SqlCommand(commandText, conn);
            // set the command type
            cmd.CommandType = CommandType.StoredProcedure;
            // we need to add parameters to the command
            cmd.Parameters.Add("@oldRoleID", SqlDbType.NVarChar, 50);
            cmd.Parameters.Add("@newRoleID", SqlDbType.NVarChar, 50);
            cmd.Parameters.Add("@oldSkaterID", SqlDbType.NVarChar, 50);
            cmd.Parameters.Add("@newSkaterID", SqlDbType.NVarChar, 50);

            //We need to set the parameter values
            cmd.Parameters["@oldRoleID"].Value = oldRoleID;
            cmd.Parameters["@newRoleID"].Value = newRoleID;
            cmd.Parameters["@oldSkaterID"].Value = oldSkaterID;
            cmd.Parameters["@newSkaterID"].Value = newSkaterID;
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
        public int deleteSkater_Role_Line(string Skater_Role_LineID)
        {
            int rows = 0;
            // start with a connection object
            var conn = SqlConnectionProvider.GetConnection();
            // set the command text
            var commandText = "sp_delete_Skater_Role_Line";
            // create the command object
            var cmd = new SqlCommand(commandText, conn);
            // set the command type
            cmd.CommandType = CommandType.StoredProcedure;
            // we need to add parameters to the command
            cmd.Parameters["@Skater_Role_LineID"].Value = Skater_Role_LineID;
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
