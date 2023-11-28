using DataObjects;
using System;
using System.Collections.Generic;

namespace LogicLayer
{
    public interface ILocationManager
    {
        List<LocationVM> SelectAllLocations();
        List<LocationVM> SelectLocationsByLeague(League league);
        LocationVM SelectLocationByLocationID(String LocaitonID);

        bool AddLocation(Location location);
        int updateLocation(Location oldLocation, Location newLocaiton);
        int deleteLocation(string LocationID);
    }
}

