using DataAccessInterfaces;
using DataAccessLayer;
using DataObjects;
using System;
using System.Collections.Generic;
using System.Net.Http.Headers;

namespace LogicLayer
{
    /******************
    Create the Logic Layer Manager for the Event table
     Created By Jonathan Beck 4/20/2024
    ***************/
    public class EventManager : IEventManager
    {
        private IEventAccessor _eventAccessor = null;
        //default constuctor uses the database
        public EventManager()
        {
            _eventAccessor = new EventAccessor();
        }
        //the optional constuctor can accept any data provider
        public EventManager(IEventAccessor eventAccessor)
        {
            _eventAccessor = eventAccessor;
        }
        /******************
        Create the Logic Layer Delete method for the Event table
         Created By Jonathan Beck 4/20/2024
        ***************/

        public bool AddEvent(Event _Event)
        {
            bool result = false; 

            try
            {
                result = (1 == _eventAccessor.addEvent(_Event));
            }
            catch (Exception ex)
            {
                throw new ApplicationException("Event not added" + ex.InnerException.Message, ex); ;
            }
            return result;
        }

        /******************
        Create the Logic Layer Undelete method for the Event table
         Created By Jonathan Beck 4/20/2024
        ***************/
        public int purgeEvent(Event _event)
        {
            int result = 0;
            try
            {
                result = _eventAccessor.deleteEvent(_event);

                if (result == 0)
                {
                    throw new ApplicationException("Unable to Delete Event");
                }
            }
            catch (Exception ex)
            {
                throw ex;
            }
            return result;
        }
        
        /******************
        Create the Logic Layer Retreive by Primary Key method for theEvent table
         Created By Jonathan Beck 4/20/2024
        ***************/
        public Event getEventByPrimaryKey(int EventID)
        {
            Event result = null;
            try
            {
                result = _eventAccessor.selectEventByPrimaryKey(EventID);
                if (result == null)
                {
                    throw new ApplicationException("Unable to retreive Event");
                }
            }
            catch (Exception ex)
            {
                throw ex;
            }
            return result;
        }
        /******************
        Create the Logic Layer Retreive all method for the the Event table
         Created By Jonathan Beck 4/20/2024
        ***************/
        public List<Event> getAllEvent()
        {
            List<Event> result = new List<Event>();
            try
            {
                result = _eventAccessor.selectAllEvent();
                if (result.Count == 0)
                {
                    throw new ApplicationException("Unable to retreive Events");
                }
            }
            catch (Exception ex)
            {
                throw ex;
            }
            return result;
        }
        /******************
        Create the Logic Layer Update method for the Event table
         Created By Jonathan Beck 4/20/2024
        ***************/
        public int updateEvent(Event oldEvent, Event newEvent)
        {
            int result = 0;
            try
            {
                result = _eventAccessor.updateEvent(oldEvent, newEvent);
                if (result == 0)
                {
                    throw new ApplicationException("Unable to update Event");
                }
            }
            catch (Exception ex)
            {
                throw ex;
            }
            return result;
        }

        public List<Event> getAllEventByType(string Type)
        {
           List<Event> results = new List<Event> ();
            try
            {
               results= _eventAccessor.selectAllEventByType(Type);
                if (results.Count == 0) {
                    throw new ApplicationException("No events of this type");
                }
            }
            catch (Exception ex)
            {

                throw new ApplicationException("Unable to find events", ex);
            }
            return results;
        }

        public List<string> getDistinctLocationForDropDown()
        {
            List<String> locations = new List<string> ();
            try
            {
                locations = _eventAccessor.selectDistinctLocationForDropDown();
            }
            catch (Exception ex)
            {

                throw new ApplicationException("Unable to retreive locations",ex);
            }
            return locations;
        }
    }


}
