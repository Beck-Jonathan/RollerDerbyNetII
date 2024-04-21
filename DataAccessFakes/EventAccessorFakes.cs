using DataAccessInterfaces;
using DataObjects;
using System;
using System.Collections.Generic;
using System.Diagnostics.Eventing.Reader;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Controls.Primitives;

namespace DataAccessFakes
{
    public class EventAccessorFakes : IEventAccessor
    {
        List<Event> _fakeEvents;   
        public EventAccessorFakes() { 
        
            _fakeEvents = new List<Event>();
            _fakeEvents.Add(new Event{ 
            EventID = 1,
            EventType="Practice"
            });
            _fakeEvents.Add(new Event {
                EventType = "Practice"
            });
            _fakeEvents.Add(new Event());
        }
        public int addEvent(Event _Event)
        {
            int start = _fakeEvents.Count;
            _fakeEvents.Add(_Event);
            int end = _fakeEvents.Count;
            if (end-start== 0)
            {
                throw new ApplicationException("Could not add event");
            }
            return end-start;
        }

        public int deleteEvent(Event _Event)
        {
            int result = 0;
            foreach (Event e in _fakeEvents) { 
            if (e.EventID == _Event.EventID)
                {
                    _fakeEvents.Remove(e);
                    result++;
                }
            }
            if (result == 0)
            {
                throw new ApplicationException("Could not add event");
            }

            return result;
        }

        public List<Event> selectAllEvent()
        {
            if (_fakeEvents.Count == 0) {
                throw new ApplicationException("Could not load events");
            }
            return _fakeEvents;
        }

        public List<Event> selectAllEventByType(String type)
        {
            List<Event> results = new List<Event>();
            foreach (Event e in _fakeEvents)
            {
                if (e.EventType == type)
                {
                    results.Add(e);
                }
            }
            if (results.Count == 0) {
                throw new ApplicationException("Could not load events");
            }
            return results;
        }

       

        public List<string> selectDistinctLocationForDropDown()
        {
            throw new NotImplementedException();
        }

        public Event selectEventByPrimaryKey(int EventID)
        {
            
            Event result = new Event();
            foreach (Event e in _fakeEvents)
            {
                if (e.EventID == EventID)
                {
                    result = e;
                }
            }
            if (result.EventID == 0) {
                throw new ApplicationException("Could not find event");
            }
            return result;
        }
    

        

        public int updateEvent(Event _oldEvent, Event _newEvent)
        {
            int result = 0;
            foreach (Event e in _fakeEvents) {
                if (e.EventID == _oldEvent.EventID) { 
                e.EventType=_newEvent.EventType;
                    e.DateTime=_newEvent.DateTime;
                    e.LocationID=_newEvent.LocationID;
                    e.EventType= _newEvent.EventType;
                    result = 1;
                    break;
                }
            }
            if (result == 0) 
                
            {
                    throw new ApplicationException("Coult not update event.");    
            }
            return result;
        }
    }
}
