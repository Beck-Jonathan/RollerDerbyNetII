using DataAccessInterfaces;
using DataObjects;
using System;
using System.Data;
using System.Data.SqlClient;

namespace DataAccessLayer
{
    public class GearRequestAccessor : IGearRequestAccessor
    {
        public int addGearRequest(GearRequest _gearrequest)
        {
            int rows = 0;
            // start with a connection object
            var conn = SqlConnectionProvider.GetConnection();
            // set the command text
            var commandText = "sp_insertGearRequest";
            // create the command object
            var cmd = new SqlCommand(commandText, conn);
            // set the command type
            cmd.CommandType = CommandType.StoredProcedure;
            // we need to add parameters to the command
            
            cmd.Parameters.Add("@HelmSize", SqlDbType.NVarChar, 25);
            cmd.Parameters.Add("@WristGuardSize", SqlDbType.NVarChar, 25);
            cmd.Parameters.Add("@ElbowPadSize", SqlDbType.NVarChar, 25);
            cmd.Parameters.Add("@KneePadSize", SqlDbType.NVarChar, 25);
            cmd.Parameters.Add("@SkateSize", SqlDbType.NVarChar, 25);

            //We need to set the parameter values
            
            cmd.Parameters["@HelmSize"].Value = _gearrequest.HelmSize;
            cmd.Parameters["@WristGuardSize"].Value = _gearrequest.WristGuardSize;
            cmd.Parameters["@ElbowPadSize"].Value = _gearrequest.ElbowPadSize;
            cmd.Parameters["@KneePadSize"].Value = _gearrequest.KneePadSize;
            cmd.Parameters["@SkateSize"].Value = _gearrequest.SkateSize;
            try
            {
                //open the connection 
                conn.Open();  //execute the command and capture result
                rows = Convert.ToInt32(cmd.ExecuteScalar());
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


        public GearRequest selectGearRequestByPrimaryKey(int GearRequestID)
        {
            GearRequest output = new GearRequest();
            // start with a connection object
            var conn = SqlConnectionProvider.GetConnection();
            // set the command text
            var commandText = "sp_retreive_by_pk_GearRequest";
            // create the command object
            var cmd = new SqlCommand(commandText, conn);
            // set the command type
            cmd.CommandType = CommandType.StoredProcedure;
            // we need to add parameters to the command
            cmd.Parameters.Add("@GearRequestID", SqlDbType.Int);

            //We need to set the parameter values
            cmd.Parameters["@GearRequestID"].Value = GearRequestID;
            try
            {
                //open the connection 
                conn.Open();  //execute the command and capture result
                var reader = cmd.ExecuteReader();
                //process the results
                if (reader.HasRows)
                    if (reader.Read())
                    {
                        output.GearRequestID = reader.GetInt32(0);
                        output.HelmSize = reader.GetString(1);
                        output.WristGuardSize = reader.GetString(2);
                        output.ElbowPadSize = reader.GetString(3);
                        output.KneePadSize = reader.GetString(4);
                        output.SkateSize = reader.GetString(5);

                    }
                    else
                    {
                        throw new ArgumentException("GearRequest not found");
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
