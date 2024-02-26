using DataAccessInterfaces;
using DataObjects;
using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DataAccessLayer
{
    public class GearApplicationAccessor : IGearApplicationAccessor
    {
        public int addGearApplication(GearApplication _gearapplication)
        {
            int rows = 0;
            // start with a connection object
            var conn = SqlConnectionProvider.GetConnection();
            // set the command text
            var commandText = "sp_insertGearApplication";
            // create the command object
            var cmd = new SqlCommand(commandText, conn);
            // set the command type
            cmd.CommandType = CommandType.StoredProcedure;
            // we need to add parameters to the command
            
            cmd.Parameters.Add("@SkaterID", SqlDbType.NVarChar, 50);
            cmd.Parameters.Add("@GearRequestID", SqlDbType.Int, 50);
            

            //We need to set the parameter values
            
            cmd.Parameters["@SkaterID"].Value = _gearapplication.SkaterID;
            cmd.Parameters["@GearRequestID"].Value = _gearapplication.GearReuestID;
            
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


        public List<GearApplication> selectAllGearApplication()
        {
            List<GearApplication> output = new List<GearApplication>();
            // start with a connection object
            var conn = SqlConnectionProvider.GetConnection();
            // set the command text
            var commandText = "sp_retreive_by_all_GearApplication";
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
                        var _GearApplication = new GearApplication();
                        _GearApplication.ApplicationID = reader.GetInt32(0);
                        _GearApplication.SkaterID = reader.GetString(1);
                        _GearApplication.GearReuestID = reader.GetInt32(2);
                        _GearApplication.ApplicationTime = reader.GetDateTime(3);
                        _GearApplication.ApplicationStatus = reader.GetString(4);
                        output.Add(_GearApplication);
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

        public GearApplication selectGearApplicationByPrimaryKey(int ApplicationID)
        {
            GearApplication output = new GearApplication();
            // start with a connection object
            var conn = SqlConnectionProvider.GetConnection();
            // set the command text
            var commandText = "sp_retreive_by_pk_GearApplication";
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
                        output.SkaterID = reader.GetString(1);
                        output.GearReuestID = reader.GetInt32(2);
                        output.ApplicationTime = reader.GetDateTime(3);
                        output.ApplicationStatus = reader.GetString(4);

                    }
                    else
                    {
                        throw new ArgumentException("GearApplication not found");
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

        public GearApplication selectGearApplicationByPrimaryKey(string GearApplicationID)
        {
            throw new NotImplementedException();
        }

        public int updateGearApplication(GearApplication _oldGearApplication, GearApplication _newGearApplication)
        {
            int rows = 0;
            // start with a connection object
            var conn = SqlConnectionProvider.GetConnection();
            // set the command text
            var commandText = "sp_update_GearApplication";
            // create the command object
            var cmd = new SqlCommand(commandText, conn);
            // set the command type
            cmd.CommandType = CommandType.StoredProcedure;
            // we need to add parameters to the command
            cmd.Parameters.Add("@oldApplicationID", SqlDbType.Int);
            cmd.Parameters.Add("@oldSkaterID", SqlDbType.NVarChar, 50);
            
            cmd.Parameters.Add("@oldGearRequestID", SqlDbType.Int, 50);
            
            cmd.Parameters.Add("@oldApplicationTime", SqlDbType.DateTime);
            
            cmd.Parameters.Add("@oldApplicationStatus", SqlDbType.NVarChar, 50);
            cmd.Parameters.Add("@newApplicationStatus", SqlDbType.NVarChar, 50);

            //We need to set the parameter values
            cmd.Parameters["@oldApplicationID"].Value = _oldGearApplication.ApplicationID;
            cmd.Parameters["@oldSkaterID"].Value = _oldGearApplication.SkaterID;
            
            cmd.Parameters["@oldGearRequestID"].Value = _oldGearApplication.GearReuestID;
            
            cmd.Parameters["@oldApplicationTime"].Value = _oldGearApplication.ApplicationTime;
            
            cmd.Parameters["@oldApplicationStatus"].Value = _oldGearApplication.ApplicationStatus;
            cmd.Parameters["@newApplicationStatus"].Value = _newGearApplication.ApplicationStatus;
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
