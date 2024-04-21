using DataAccessInterfaces;
using System;
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

        public int removeSkater_Role_Line(string RoleID, string SkaterID)
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
    }
}
