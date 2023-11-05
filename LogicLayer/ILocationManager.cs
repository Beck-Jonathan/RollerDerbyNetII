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

        bool AddLocation(string LocaitonID, string LeagueID, string ContactPhone, string City, string State, string ZipCode);
        int updateLocation(string oldLocaitonID, string oldLeagueID, string oldContactPhone, string oldCity, string oldState, string oldZipCode,
                            string newLocaitonID, string newLeagueID, string newContactPhone, string newCity, string newState, string newZipCode);
        int deleteLocation(string LocationID);
    }
}

