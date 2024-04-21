using DataObjects;
using System;
using System.Collections.Generic;

namespace DataAccessInterfaces
{
    /******************
Create the I Accessor for the Event table
 Created By Jonathan Beck 4/20/2024
***************/
    public interface IEventAccessor
    {
        int addEvent(Event _Event);
        Event selectEventByPrimaryKey(int EventID);
        List<Event> selectAllEvent();
        List<Event> selectAllEventByType(String type);
        int updateEvent(Event _oldEvent, Event _newEvent);
        int deleteEvent(Event _Event);
        
        List<String> selectDistinctLocationForDropDown();
    }


}
