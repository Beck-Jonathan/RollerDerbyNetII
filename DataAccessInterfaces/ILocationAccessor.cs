using DataObjects;
using System;
using System.Collections.Generic;

namespace DataAccessInterfaces
{
    public interface ILocationAccessor
    {
        List<LocationVM> SelectAllLocations();
        List<LocationVM> SelectLocationsByLeague(League league);
        LocationVM SelectLocationByLocationID(String LocaitonID);

        int AddLocation(Location location);
        int updateLocation(Location oldLocation, Location newLocation);
        int deleteLocation(string LocationID);


    }
}
