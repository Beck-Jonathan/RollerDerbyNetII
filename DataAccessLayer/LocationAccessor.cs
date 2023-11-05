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
        public int AddLocation(string LocationID, string LeagueID, string ContactPhone, string City, string State, string Zip)
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
            cmd.Parameters.Add("@ContactPhone", SqlDbType.NVarChar, 11);
            cmd.Parameters.Add("@City", SqlDbType.NVarChar, 100);
            cmd.Parameters.Add("@State", SqlDbType.NVarChar, 5);
            cmd.Parameters.Add("@Zip", SqlDbType.NVarChar, 5);

            //We need to set the parameter values
            cmd.Parameters["@LocationID"].Value = LocationID;
            cmd.Parameters["@LeagueID"].Value = LeagueID;
            cmd.Parameters["@ContactPhone"].Value = ContactPhone;
            cmd.Parameters["@City"].Value = City;
            cmd.Parameters["@State"].Value = State;
            cmd.Parameters["@Zip"].Value = Zip;
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
                        output.LocationId = reader.GetString(0);
                        output.LeagueID = reader.GetString(1);
                        output.ContactPhone = reader.GetString(2);
                        output.City = reader.GetString(3);
                        output.State = reader.GetString(4);
                        output.ZipCode = reader.GetString(5);

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
                        _Location.LocationId = reader.GetString(0);
                        _Location.LeagueID = reader.GetString(1);
                        _Location.ContactPhone = reader.GetString(2);
                        _Location.City = reader.GetString(3);
                        _Location.State = reader.GetString(4);
                        _Location.ZipCode = reader.GetString(5);
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
        public int updateLocation(string oldLocationID, string oldLeagueID, string oldContactPhone, string oldCity, string oldState, string oldZip, string newLeagueID, string newContactPhone, string newCity, string newState, string newZip)
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
            cmd.Parameters.Add("@newLocationID", SqlDbType.NVarChar, 100);
            cmd.Parameters.Add("@oldLeagueID", SqlDbType.NVarChar, 100);

            cmd.Parameters.Add("@oldContactPhone", SqlDbType.NVarChar, 11);
            cmd.Parameters.Add("@newContactPhone", SqlDbType.NVarChar, 11);
            cmd.Parameters.Add("@oldCity", SqlDbType.NVarChar, 100);
            cmd.Parameters.Add("@newCity", SqlDbType.NVarChar, 100);
            cmd.Parameters.Add("@oldState", SqlDbType.NVarChar, 5);
            cmd.Parameters.Add("@newState", SqlDbType.NVarChar, 5);
            cmd.Parameters.Add("@oldZip", SqlDbType.NVarChar, 5);
            cmd.Parameters.Add("@newZip", SqlDbType.NVarChar, 5);

            //We need to set the parameter values
            cmd.Parameters["@oldLocationID"].Value = oldLocationID;

            cmd.Parameters["@oldLeagueID"].Value = oldLeagueID;
            cmd.Parameters["@newLeagueID"].Value = newLeagueID;
            cmd.Parameters["@oldContactPhone"].Value = oldContactPhone;
            cmd.Parameters["@newContactPhone"].Value = newContactPhone;
            cmd.Parameters["@oldCity"].Value = oldCity;
            cmd.Parameters["@newCity"].Value = newCity;
            cmd.Parameters["@oldState"].Value = oldState;
            cmd.Parameters["@newState"].Value = newState;
            cmd.Parameters["@oldZip"].Value = oldZip;
            cmd.Parameters["@newZip"].Value = newZip;
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
    }
}