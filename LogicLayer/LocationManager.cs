using DataAccessInterfaces;
using DataAccessLayer;
using DataObjects;
using System;
using System.Collections.Generic;

namespace LogicLayer
{
    public class LocationManager : ILocationManager
    {
        private ILocationAccessor _locationAccessor = null;

        //defailt constructor will use database
        public LocationManager()
        {
            _locationAccessor = new LocationAccessor();
        }

        //the optional contructor can accept any data providero
        public LocationManager(ILocationAccessor locationAccessor)
        {
            _locationAccessor = locationAccessor;

        }
        public bool AddLocation(string LocaitonID, string LeagueID, string ContactPhone, string City, string State, string ZipCode)
        {
            bool result = false;
            try
            {
                result = (1 == _locationAccessor.AddLocation(LocaitonID, LeagueID, ContactPhone, City, State, ZipCode));
            }
            catch (Exception ex)
            {
                throw new ApplicationException("create failed", ex);
            }
            return result;
        }

        public int deleteLocation(string LocationID)
        {
            int result = 0;
            try
            {
                result = _locationAccessor.deleteLocation(LocationID);
            }
            catch (Exception ex)
            {

                throw new ApplicationException("delete failed", ex);
            }


            return result;
        }

        public List<LocationVM> SelectAllLocations()
        {
            List<LocationVM> results = null;
            try
            {
                results = _locationAccessor.SelectAllLocations();
            }
            catch (Exception ex)
            {

                throw new ApplicationException("Locations not found", ex);
            }
            return results;
        }

        public LocationVM SelectLocationByLocationID(string LocaitonID)
        {
            LocationVM location = null;
            try
            {
                location = _locationAccessor.SelectLocationByLocationID(LocaitonID);
            }
            catch (Exception ex)
            {

                throw new ApplicationException("location not found", ex);
            }
            return location;
        }

        public List<LocationVM> SelectLocationsByLeague(League league)
        {
            throw new NotImplementedException();
        }

        public int updateLocation(string oldLocaitonID, string oldLeagueID, string oldContactPhone, string oldCity, string oldState, string oldZipCode, string newLocaitonID, string newLeagueID, string newContactPhone, string newCity, string newState, string newZipCode)
        {
            int result = 0;

            try
            {
                result = _locationAccessor.updateLocation(oldLocaitonID, oldLeagueID, oldContactPhone, oldCity, oldState, oldZipCode, newLeagueID, newContactPhone, newCity, newState, newZipCode);
            }
            catch (Exception ex)
            {

                throw new ApplicationException("Location not found, update failed", ex);
            }
            return result;
        }
    }
}

