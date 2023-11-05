using DataAccessFakes;
using DataObjects;
using LogicLayer;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using System;

namespace UnitTestProject1
{
    [TestClass]
    public class LocationManagerTests
    {
        LocationManager _locationmanager = null;
        ILeagueManager _leagueManager = null;
        [TestInitialize]
        public void testSetup()
        {
            _locationmanager = new LocationManager(new LocationAccessorFakes());
            _leagueManager = new LeagueManager(new LeagueAccessorFakes());
        }

        [TestMethod]
        public void TestSelectByLocationIDREturnsCorrectLocation()
        {
            //arrage
            string LocationID = "x";
            Location location1 = new Location();
            location1.LocationId = "Imon";
            location1.LeagueID = "WFTDA";
            location1.ContactPhone = "111-123-2134";
            location1.City = "Cedar Rapids";
            location1.State = "Iowa";
            location1.ZipCode = "52405";
            Location actual = null;
            //act
            actual = _locationmanager.SelectLocationByLocationID(location1.LocationId);

            //assert
            Assert.AreEqual(location1.LocationId, actual.LocationId);
            Assert.AreEqual(location1.LeagueID, actual.LeagueID);
            Assert.AreEqual(location1.ContactPhone, actual.ContactPhone);
            Assert.AreEqual(location1.City, actual.City);
            Assert.AreEqual(location1.State, actual.State);
            Assert.AreEqual(location1.ZipCode, actual.ZipCode);

        }
        [TestMethod]
        [ExpectedException(typeof(ApplicationException))]
        public void TestSelectByLocationIDRFailsWithBadData()
        {
            //arrange
            string LocationID = "x";
            //act
            _locationmanager.SelectLocationByLocationID(LocationID);
            //assert - nothing to do


        }

        [TestMethod]
        public void TestReturnAllLocaitonsReturnsAllLocaitons()
        {
            //arrange
            int actualsize = 0;
            int expectedsize = 3;

            //act
            actualsize = _locationmanager.SelectAllLocations().Count;

            //assert
            Assert.AreEqual(actualsize, expectedsize);

        }
        [TestMethod]
        public void TestAddLocaitonAddsALocaiton()
        {

            //arrange
            int acutalsize = 0;
            int expectedsize = 4;

            string LocationId = "United Center";
            string LeagueID = "WFTDA";
            string ContactPhone = "311 842 2242";
            string City = "Chicago";
            string State = "IL";
            string ZipCode = "45521";

            //act
            _locationmanager.AddLocation(LocationId, LeagueID, ContactPhone, City, State, ZipCode);
            // assert
            Assert.AreEqual(expectedsize, _locationmanager.SelectAllLocations().Count);

        }

        [TestMethod]
        public void TestDeleteALeagueRemovesALeagueFromTheList()
        {
            int expectedcount = 2;
            int acutalCount = 0;
            String LocationToDelete = "Imon";


            //act
            _locationmanager.deleteLocation(LocationToDelete);
            acutalCount = _locationmanager.SelectAllLocations().Count;


            //assert
            Assert.AreEqual(expectedcount, acutalCount);


        }

        [TestMethod]

        public void TestDeleteLocationFailsWithBadData()
        {
            //arrange
            int expectedcount = 3;
            int acutalCount = 0;
            String LocationToDelete = "Fake4";


            //act
            _locationmanager.deleteLocation(LocationToDelete);
            acutalCount = _locationmanager.SelectAllLocations().Count;


            //assert
            Assert.AreEqual(expectedcount, acutalCount);

        }



    }
}
