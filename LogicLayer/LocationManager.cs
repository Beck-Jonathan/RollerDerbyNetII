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
    }
}

