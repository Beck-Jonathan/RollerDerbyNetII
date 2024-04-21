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
            location1.LocationID = "Imon";
            location1.LeagueID = "WFTDA";
            location1.ContactPhone = "111-123-2134";
            location1.City = "Cedar Rapids";
            location1.State = "Iowa";
            location1.Zip = "52405";
            LocationVM location2 = new LocationVM();
            location2.LocationID = "Carver Hall";
            location2.LeagueID = "WFTDA";
            location2.ContactPhone = "452-455-6631";
            location2.City = "Ames";
            location2.State = "Iowa";
            location2.Zip = "50014";
            LocationVM location3 = new LocationVM();
            location3.LocationID = "United Center";
            location3.LeagueID = "MFTDA";
            location3.ContactPhone = "845-122-4523";
            location3.City = "Chicago";
            location3.State = "Illinois";
            location3.Zip = "382-412-4532";

            fakelocations.Add(location1);
            fakelocations.Add(location2);
            fakelocations.Add(location3);


        }
        public int AddLocation(Location location)
        {
            int initialsize = fakelocations.Count;
            LocationVM locationVM = new LocationVM();
            locationVM.LocationID = location.LocationID;

            fakelocations.Add(locationVM);
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
                    if (fakelocations[i].LocationID == LocationID)
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
                    if (_location.LocationID == LocationID)
                    {
                        result.LocationID = _location.LocationID;
                        result.LeagueID = _location.LeagueID;
                        result.City = _location.City;
                        result.State = _location.State;
                        result.ContactPhone = _location.ContactPhone;
                        result.Zip = _location.Zip;


                    }


                }
                if (result.LocationID == null) { throw new ApplicationException(); }
            }
            catch (ApplicationException ex) { throw ex; }



            return result;
        }

        public List<LocationVM> SelectLocationsByLeague(League league)
        {
            throw new NotImplementedException();
        }

        public int updateLocation(Location oldLocaiton, Location newLocation)
        {
            throw new NotImplementedException();
        }
    }
}
