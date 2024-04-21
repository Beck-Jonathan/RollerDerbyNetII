using DataObjects;
using System;
using System.Collections.Generic;

namespace LogicLayer
{
    public interface IEventManager
    {
        bool AddEvent(Event _Event);
        Event getEventByPrimaryKey(int EventID);
        List<Event> getAllEvent();
        List<Event> getAllEventByType(String Type);
        int updateEvent(Event _oldEvent, Event _newEvent);
        int purgeEvent(Event _event);
        
        List<String> getDistinctLocationForDropDown();
    }

}
