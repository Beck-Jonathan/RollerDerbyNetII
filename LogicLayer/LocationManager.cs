using DataAccessInterfaces;
using DataAccessLayer;
using DataObjects;
using System;
using System.Collections.Generic;
using System.Runtime.Remoting.Messaging;

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
        //this function will add a location
        public bool AddLocation(Location location)
        {
            bool result = false;
            try
            {
                result = (1 == _locationAccessor.AddLocation(location));
            }
            catch (Exception ex)
            {
                throw new ApplicationException("create failed", ex);
            }
            return result;
        }

        //this function will mark a locaiton as inactive
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
        //this function will grab all locations
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

        //this function will select a location by its PK
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

        //this function will will grab all locaitons associated witha  league. Not imploemented
        public List<LocationVM> SelectLocationsByLeague(League league)
        {
            throw new NotImplementedException();
        }

        //this function will update a location
        public int updateLocation(Location oldLocation, Location newLocation)
        {
            int result = 0;

            try
            {
                result = _locationAccessor.updateLocation(oldLocation, newLocation);
            }
            catch (Exception ex)
            {

                throw new ApplicationException("Location not found, update failed", ex);
            }
            return result;
        }
        public List<String> getLeaguesForDropDown() { 
        List<String> result = new List<String>();
            try
            {
               result= _locationAccessor.getLeaguesForDropDown();
            }
            catch (Exception ex)
            {

                throw new ApplicationException("unable to find leagues", ex);
         
            }
            return result;
        }
        
    }
}

