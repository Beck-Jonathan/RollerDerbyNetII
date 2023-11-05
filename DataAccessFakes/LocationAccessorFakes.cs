using DataAccessInterfaces;
using DataObjects;
using System;
using System.Collections.Generic;

namespace DataAccessFakes
{
    public class LocationAccessorFakes : ILocationAccessor
    {

        private List<LocationVM> fakelocations = new List<LocationVM>();

        public LocationAccessorFakes()
        {

            LocationVM location1 = new LocationVM();
            location1.LocationId = "Imon";
            location1.LeagueID = "WFTDA";
            location1.ContactPhone = "111-123-2134";
            location1.City = "Cedar Rapids";
            location1.State = "Iowa";
            location1.ZipCode = "52405";
            LocationVM location2 = new LocationVM();
            location2.LocationId = "Carver Hall";
            location2.LeagueID = "WFTDA";
            location2.ContactPhone = "452-455-6631";
            location2.City = "Ames";
            location2.State = "Iowa";
            location2.ZipCode = "50014";
            LocationVM location3 = new LocationVM();
            location3.LocationId = "United Center";
            location3.LeagueID = "MFTDA";
            location3.ContactPhone = "845-122-4523";
            location3.City = "Chicago";
            location3.State = "Illinois";
            location3.ZipCode = "382-412-4532";

            fakelocations.Add(location1);
            fakelocations.Add(location2);
            fakelocations.Add(location3);


        }
        public int AddLocation(string LocationID, string LeagueID, string ContactPhone, string City, string State, string ZipCode)
        {
            int initialsize = fakelocations.Count;
            LocationVM _location = new LocationVM();
            _location.LocationId = LocationID;
            _location.LeagueID = LeagueID;
            _location.ContactPhone = ContactPhone;
            _location.City = City;
            _location.State = State;
            _location.ZipCode = ZipCode;
            fakelocations.Add(_location);
            int newsize = fakelocations.Count;
            return newsize - initialsize;


        }

        public int deleteLocation(string LocationID)
        {
            {
                int result = 0;
                int IndexToDelete = fakelocations.Count + 1;
                for (int i = 0; i < fakelocations.Count; i++)
                {
                    if (fakelocations[i].LocationId == LocationID)
                    {
                        IndexToDelete = i;
                        result = 1;
                        break;
                    }
                }
                if (result == 0) { return result; }
                if (result == 1)
                {
                    fakelocations.RemoveAt(IndexToDelete);




                    return result;
                }
                else { return result; }
            }
        }

        public List<LocationVM> SelectAllLocations()
        {
            return fakelocations;
        }

        public LocationVM SelectLocationByLocationID(string LocationID)
        {
            LocationVM result = new LocationVM();
            try
            {
                foreach (Location _location in fakelocations)
                {
                    if (_location.LocationId == LocationID)
                    {
                        result.LocationId = _location.LocationId;
                        result.LeagueID = _location.LeagueID;
                        result.City = _location.City;
                        result.State = _location.State;
                        result.ContactPhone = _location.ContactPhone;
                        result.ZipCode = _location.ZipCode;


                    }


                }
                if (result.LocationId == null) { throw new ApplicationException(); }
            }
            catch (ApplicationException ex) { throw ex; }



            return result;
        }

        public List<LocationVM> SelectLocationsByLeague(League league)
        {
            throw new NotImplementedException();
        }

        public int updateLocation(string oldLocaitonID, string oldLeagueID, string oldContactPhone, string oldCity, string oldState, string oldZipCode, string newLeagueID, string newContactPhone, string newCity, string newState, string newZipCode)
        {
            throw new NotImplementedException();
        }
    }
}
