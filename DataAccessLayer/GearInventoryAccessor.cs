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
    public class GearInventoryAccessor : IGearInventoryAccessor
    {
        public List<GearInventory> selectAllGearInventory()
        {
            List<GearInventory> output = new List<GearInventory>();
            // start with a connection object
            var conn = SqlConnectionProvider.GetConnection();
            // set the command text
            var commandText = "sp_retreive_by_all_GearInventory";
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
                        var _GearInventory = new GearInventory();
                        _GearInventory.GearPart = reader.GetString(0);
                        _GearInventory.GearSize = reader.GetString(1);
                        _GearInventory.GearCount = reader.GetInt32(2);
                        output.Add(_GearInventory);
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

        public int updateGearInventory(GearInventory _oldGearInventory, GearInventory _newGearInventory)
        {
            int rows = 0;
            // start with a connection object
            var conn = SqlConnectionProvider.GetConnection();
            // set the command text
            var commandText = "sp_update_GearInventory";
            // create the command object
            var cmd = new SqlCommand(commandText, conn);
            // set the command type
            cmd.CommandType = CommandType.StoredProcedure;
            // we need to add parameters to the command
            cmd.Parameters.Add("@oldGearPart", SqlDbType.NVarChar, 50);
            cmd.Parameters.Add("@oldGearSize", SqlDbType.NVarChar, 50);
            cmd.Parameters.Add("@oldGearCount", SqlDbType.Int);
            cmd.Parameters.Add("@newGearCount", SqlDbType.Int);

            //We need to set the parameter values
            cmd.Parameters["@oldGearPart"].Value = _oldGearInventory.GearPart;
            cmd.Parameters["@oldGearSize"].Value = _oldGearInventory.GearSize;
            cmd.Parameters["@oldGearCount"].Value = _oldGearInventory.GearCount;
            cmd.Parameters["@newGearCount"].Value = _newGearInventory.GearCount;
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
