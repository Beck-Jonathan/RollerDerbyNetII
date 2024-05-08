using DataAccessInterfaces;
using DataObjects;
using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;

namespace DataAccessLayer
{
    public class LocationAccessor : ILocationAccessor
    {
        public int AddLocation(Location location)
        {
            int rows = 0;
            // start with a connection object
            var conn = SqlConnectionProvider.GetConnection();
            // set the command text
            var commandText = "sp_insert_Location";
            // create the command object
            var cmd = new SqlCommand(commandText, conn);
            // set the command type
            cmd.CommandType = CommandType.StoredProcedure;
            // we need to add parameters to the command
            cmd.Parameters.Add("@LocationID", SqlDbType.NVarChar, 100);
            cmd.Parameters.Add("@LeagueID", SqlDbType.NVarChar, 100);
            cmd.Parameters.Add("@ContactPhone", SqlDbType.NVarChar, 13);
            cmd.Parameters.Add("@City", SqlDbType.NVarChar, 100);
            cmd.Parameters.Add("@State", SqlDbType.NVarChar, 50);
            cmd.Parameters.Add("@Zip", SqlDbType.NVarChar, 5);

            //We need to set the parameter values
            cmd.Parameters["@LocationID"].Value = location.LocationID;
            cmd.Parameters["@LeagueID"].Value = location.LeagueID;
            cmd.Parameters["@ContactPhone"].Value = location.ContactPhone;
            cmd.Parameters["@City"].Value = location.City;
            cmd.Parameters["@State"].Value = location.State;
            cmd.Parameters["@Zip"].Value = location.Zip;
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
        public LocationVM SelectLocationByLocationID(string LocationID)
        {
            LocationVM output = new LocationVM();
            // start with a connection object
            var conn = SqlConnectionProvider.GetConnection();
            // set the command text
            var commandText = "sp_retreive_by_pk_Location";
            // create the command object
            var cmd = new SqlCommand(commandText, conn);
            // set the command type
            cmd.CommandType = CommandType.StoredProcedure;
            // we need to add parameters to the command
            cmd.Parameters.Add("@LocationID", SqlDbType.NVarChar, 100);

            //We need to set the parameter values
            cmd.Parameters["@LocationID"].Value = LocationID;
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
                        output.LocationID = reader.GetString(0);
                        output.LeagueID = reader.GetString(1);
                        output.ContactPhone = reader.GetString(2);
                        output.City = reader.GetString(3);
                        output.State = reader.GetString(4);
                        output.Zip = reader.GetString(5);

                    }
                }
                else
                {
                    throw new ArgumentException("Location not found");
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
        public List<LocationVM> SelectAllLocations()
        {
            List<LocationVM> output = new List<LocationVM>();
            // start with a connection object
            var conn = SqlConnectionProvider.GetConnection();
            // set the command text
            var commandText = "sp_retreive_by_all_Location";
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
                        var _Location = new LocationVM();
                        _Location.LocationID = reader.GetString(0);
                        _Location.LeagueID = reader.GetString(1);
                        _Location.ContactPhone = reader.GetString(2);
                        _Location.City = reader.GetString(3);
                        _Location.State = reader.GetString(4);
                        _Location.Zip = reader.GetString(5);
                        output.Add(_Location);
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
        public int updateLocation(Location oldLocation, Location newLocation)
        {
            int rows = 0;
            // start with a connection object
            var conn = SqlConnectionProvider.GetConnection();
            // set the command text
            var commandText = "sp_update_Location";
            // create the command object
            var cmd = new SqlCommand(commandText, conn);
            // set the command type
            cmd.CommandType = CommandType.StoredProcedure;
            // we need to add parameters to the command
            cmd.Parameters.Add("@oldLocationID", SqlDbType.NVarChar, 100);


            cmd.Parameters.Add("@oldLeagueID", SqlDbType.NVarChar, 100);
            cmd.Parameters.Add("@newLeagueID", SqlDbType.NVarChar, 100);

            cmd.Parameters.Add("@oldContactPhone", SqlDbType.NVarChar, 14);
            cmd.Parameters.Add("@newContactPhone", SqlDbType.NVarChar, 14);
            cmd.Parameters.Add("@oldCity", SqlDbType.NVarChar, 100);
            cmd.Parameters.Add("@newCity", SqlDbType.NVarChar, 100);
            cmd.Parameters.Add("@oldState", SqlDbType.NVarChar, 50);
            cmd.Parameters.Add("@newState", SqlDbType.NVarChar, 50);
            cmd.Parameters.Add("@oldZip", SqlDbType.NVarChar, 5);
            cmd.Parameters.Add("@newZip", SqlDbType.NVarChar, 5);

            //We need to set the parameter values
            cmd.Parameters["@oldLocationID"].Value = oldLocation.LocationID;


            cmd.Parameters["@oldLeagueID"].Value = oldLocation.LeagueID;
            cmd.Parameters["@newLeagueID"].Value = newLocation.LeagueID;
            cmd.Parameters["@oldContactPhone"].Value = oldLocation.ContactPhone;
            cmd.Parameters["@newContactPhone"].Value = newLocation.ContactPhone;
            cmd.Parameters["@oldCity"].Value = oldLocation.City;
            cmd.Parameters["@newCity"].Value = newLocation.City;
            cmd.Parameters["@oldState"].Value = oldLocation.State;
            cmd.Parameters["@newState"].Value = newLocation.State;
            cmd.Parameters["@oldZip"].Value = oldLocation.Zip;
            cmd.Parameters["@newZip"].Value = newLocation.Zip;
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
        public int deleteLocation(string LocationID)
        {
            int rows = 0;
            // start with a connection object
            var conn = SqlConnectionProvider.GetConnection();
            // set the command text
            var commandText = "sp_delete_Location";
            // create the command object
            var cmd = new SqlCommand(commandText, conn);
            // set the command type
            cmd.CommandType = CommandType.StoredProcedure;
            // we need to add parameters to the command
            cmd.Parameters.Add("@LocationID", SqlDbType.NVarChar, 100);
            cmd.Parameters["@LocationID"].Value = LocationID;
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
        public List<LocationVM> SelectLocationsByLeague(League league)
        {
            throw new NotImplementedException();
        }
        public List<String> getLeaguesForDropDown()
        {
            List<String> output = new List<String>();
            // start with a connection object
            var conn = SqlConnectionProvider.GetConnection();
            // set the command text
            var commandText = "sp_select_distinct_and_active_League_for_dropdown";
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
                        String _League = reader.GetString(0);
                        output.Add(_League);
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