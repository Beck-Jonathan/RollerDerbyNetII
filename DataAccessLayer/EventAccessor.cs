using DataAccessInterfaces;
using DataObjects;
using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;

namespace DataAccessLayer
{
    /******************
create the Accessor for the Event table
 Created By Jonathan Beck 4/20/2024
***************/
    public class EventAccessor : IEventAccessor
    {

        public int addEvent(Event _event)
        {
            int rows = 0;
            // start with a connection object
            var conn = SqlConnectionProvider.GetConnection();
            // set the command text
            var commandText = "sp_insertEvent";
            // create the command object
            var cmd = new SqlCommand(commandText, conn);
            // set the command type
            cmd.CommandType = CommandType.StoredProcedure;
            // we need to add parameters to the command
            cmd.Parameters.Add("@LocationID", SqlDbType.NVarChar, 100);
            cmd.Parameters.Add("@EventType", SqlDbType.NVarChar, 100);
            cmd.Parameters.Add("@DateTime", SqlDbType.DateTime);

            //We need to set the parameter values
            cmd.Parameters["@LocationID"].Value = _event.LocationID;
            cmd.Parameters["@EventType"].Value = _event.EventType;
            cmd.Parameters["@DateTime"].Value = _event.DateTime;
            try
            {
                //open the connection 
                conn.Open();  //execute the command and capture result
                rows = cmd.ExecuteNonQuery();
            }

            catch (Exception ex)
            {
                throw new ApplicationException("Unable to create Event",ex);
            }
            finally
            {
                conn.Close();
            }
            return rows;
        }

        public Event selectEventByPrimaryKey(int EventID)
        {
            Event output = new Event();
            // start with a connection object
            var conn = SqlConnectionProvider.GetConnection();
            // set the command text
            var commandText = "sp_retreive_by_pk_Event";
            // create the command object
            var cmd = new SqlCommand(commandText, conn);
            // set the command type
            cmd.CommandType = CommandType.StoredProcedure;
            // we need to add parameters to the command
            cmd.Parameters.Add("@EventID", SqlDbType.Int);

            //We need to set the parameter values
            cmd.Parameters["@EventID"].Value = EventID;
            try
            {
                //open the connection 
                conn.Open();  //execute the command and capture result
                var reader = cmd.ExecuteReader();
                //process the results
                if (reader.HasRows)
                    if (reader.Read())
                    {
                        output.EventID = reader.GetInt32(reader.GetOrdinal("Event_EventID"));
                        output.LocationID = reader.GetString(reader.GetOrdinal("Event_LocationID"));
                        output.EventType = reader.GetString(reader.GetOrdinal("Event_EventType"));
                        output.DateTime = reader.GetDateTime(reader.GetOrdinal("Event_DateTime"));

                    }
                    else
                    {
                        throw new ArgumentException("Event not found");
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

        public List<Event> selectAllEvent()
        {
            List<Event> output = new List<Event>();
            // start with a connection object
            var conn = SqlConnectionProvider.GetConnection();
            // set the command text
            var commandText = "sp_retreive_by_active_Event";
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
                        var _Event = new Event();
                        _Event.EventID = reader.GetInt32(0);
                        _Event.LocationID = reader.GetString(1);
                        _Event.EventType = reader.GetString(2);
                        _Event.DateTime = reader.GetDateTime(3);
                        output.Add(_Event);
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

        public int updateEvent(Event _oldEvent, Event _newEvent)
        {
            int rows = 0;
            // start with a connection object
            var conn = SqlConnectionProvider.GetConnection();
            // set the command text
            var commandText = "sp_update_Event";
            // create the command object
            var cmd = new SqlCommand(commandText, conn);
            // set the command type
            cmd.CommandType = CommandType.StoredProcedure;
            // we need to add parameters to the command
            cmd.Parameters.Add("@oldEventID", SqlDbType.Int);
            cmd.Parameters.Add("@newEventID", SqlDbType.Int);
            cmd.Parameters.Add("@oldLocationID", SqlDbType.NVarChar, 100);
            cmd.Parameters.Add("@newLocationID", SqlDbType.NVarChar, 100);
            cmd.Parameters.Add("@oldEventType", SqlDbType.NVarChar, 100);
            cmd.Parameters.Add("@newEventType", SqlDbType.NVarChar, 100);
            cmd.Parameters.Add("@oldDateTime", SqlDbType.DateTime);
            cmd.Parameters.Add("@newDateTime", SqlDbType.DateTime);

            //We need to set the parameter values
            cmd.Parameters["@oldEventID"].Value = _oldEvent.EventID;
            cmd.Parameters["@newEventID"].Value = _oldEvent.EventID;
            cmd.Parameters["@oldLocationID"].Value = _oldEvent.LocationID;
            cmd.Parameters["@newLocationID"].Value = _newEvent.LocationID;
            cmd.Parameters["@oldEventType"].Value = _oldEvent.EventType;
            cmd.Parameters["@newEventType"].Value = _newEvent.EventType;
            cmd.Parameters["@oldDateTime"].Value = _oldEvent.DateTime;
            cmd.Parameters["@newDateTime"].Value = _newEvent.DateTime;
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
        /// <summary>///A method that ________________
        ///_____________.
        ///</ summary >
        ///< param name = "a" > 
        ///____________
        ///</ param >
        ///< returns >
        ///< see cref = "int" > int </ see >: ____________.
        ///</ returns >
        ///< remarks >
        ///Parameters:
        ///< br />
        ///< see cref = "int" > int </ see > a: _______________///< br />< br />
        ///Exceptions:
        ///< br /> 
        ///< see cref = "ArgumentOutOfRangeException" > ArgumentOutOfRangeException </ see >:____________.
        ///< br />< br /> 
        ///CONTRIBUTOR: Jonathan Beck 
        ///< br /> 
        ///CREATED: 20/4/2024
        ///< br />< br />

        public int deleteEvent(Event _event)
        {
            int rows = 0;
            // start with a connection object
            var conn = SqlConnectionProvider.GetConnection();
            // set the command text
            var commandText = "sp_delete_Event";
            // create the command object
            var cmd = new SqlCommand(commandText, conn);
            // set the command type
            cmd.CommandType = CommandType.StoredProcedure;
            // we need to add parameters to the command
            cmd.Parameters.Add("@EventID", SqlDbType.Int);
            cmd.Parameters.Add("@LocationID", SqlDbType.NVarChar, 100);
            cmd.Parameters.Add("@EventType", SqlDbType.NVarChar, 100);
            cmd.Parameters.Add("@DateTime", SqlDbType.DateTime);

            //We need to set the parameter values
            cmd.Parameters["@EventID"].Value = _event.EventID;
            cmd.Parameters["@LocationID"].Value = _event.LocationID;
            cmd.Parameters["@EventType"].Value = _event.EventType;
            cmd.Parameters["@DateTime"].Value = _event.DateTime;
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
        

        
        /// <summary>///A method that ________________
        ///_____________.
        ///</ summary >
        ///< param name = "a" > 
        ///____________
        ///</ param >
        ///< returns >
        ///< see cref = "int" > int </ see >: ____________.
        ///</ returns >
        ///< remarks >
        ///Parameters:
        ///< br />
        ///< see cref = "int" > int </ see > a: _______________///< br />< br />
        ///Exceptions:
        ///< br /> 
        ///< see cref = "ArgumentOutOfRangeException" > ArgumentOutOfRangeException </ see >:____________.
        ///< br />< br /> 
        ///CONTRIBUTOR: Jonathan Beck 
        ///< br /> 
        ///CREATED: 20/4/2024
        ///< br />< br />
        public List<String> selectDistinctLocationForDropDown()
        {
            List<String> output = new List<String>();
            // start with a connection object
            var conn = SqlConnectionProvider.GetConnection();
            // set the command text
            var commandText = "sp_select_distinct_and_active_Locationfor_dropdown";
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
                        String _Location = reader.GetString(0);
                        output.Add(_Location);
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

        public List<Event> selectAllEventByType(String type)
        {
            List<Event> output = new List<Event>();
            // start with a connection object
            var conn = SqlConnectionProvider.GetConnection();
            // set the command text
            var commandText = "sp_retreive_by_type_Event";
            // create the command object
            var cmd = new SqlCommand(commandText, conn);
            // set the command type
            cmd.CommandType = CommandType.StoredProcedure;

            //add the parameters
            cmd.Parameters.Add("@EventType", SqlDbType.NVarChar,100);

            //We need to set the parameter values
            cmd.Parameters["@EventType"].Value = type;
            try
            {
                //open the connection 
                conn.Open();  //execute the command and capture result
                var reader = cmd.ExecuteReader();
                //process the results
                if (reader.HasRows)
                    while (reader.Read())
                    {
                        var _Event = new Event();
                        _Event.EventID = reader.GetInt32(0);
                        _Event.LocationID = reader.GetString(1);
                        _Event.EventType = reader.GetString(2);
                        _Event.DateTime = reader.GetDateTime(3);
                        output.Add(_Event);
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
