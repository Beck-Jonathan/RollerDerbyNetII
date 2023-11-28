using DataAccessFakes;
using DataObjects;
using LogicLayer;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using System;

namespace UnitTestProject1
{
    [TestClass]
    public class SkaterTeamApplicationTests
    {
        ISkaterteamApplicationManager _applicationManager = null;
        [TestInitialize]
        public void testSetup()
        {
            _applicationManager = new SkaterteamApplicationManager(new SkaterTeamApplicationFakes());
        }

        [TestMethod]
        public void TestSelectApplicationByIDReturnsCorrectrecord()
        {
            //arrange
            String expectedTeam = "East Hawks";
            String actualTeam = "";

            //act
            actualTeam = _applicationManager.getSkaterteamApplicationByPrimaryKey(100).TeamName;

            //assert
            Assert.AreEqual(expectedTeam, actualTeam);

        }
        [TestMethod]
        public void TestAddApplicationAddsAnApplication()
        {
            //arrange
            int expected = 4;
            int actual = 0;

            //act
            _applicationManager.addSkaterteamApplication(new SkaterTeamApplication());

            actual = _applicationManager.getAllSkaterteamApplication().Count;
            //assert
            Assert.AreEqual(expected, actual);


        }

        [TestMethod]
        public void TestSelectAllApplicationsGrabsAllApplications()
        {
            //arrage
            int actual = 0;
            int expected = 3;

            //act
            actual = _applicationManager.getAllSkaterteamApplication().Count;

            //assert
            Assert.AreEqual(expected, actual);



        }

        [TestMethod]
        public void TestDeleteApplicationDeletesAnApplication()
        {
            bool actual = true;
            bool expected = false;

            _applicationManager.purgeSkaterteamApplication(100);
            actual = _applicationManager.getSkaterteamApplicationByPrimaryKey(100).isActive;


            Assert.AreEqual(actual, expected);


        }

        [TestMethod]
        public void TestUpdateApplicationUpdatesAnApplication()
        {
            //arrage
            int applicationID = 100;
            string oldskaterID = "Alice";
            string oldTeam = "East Hawks";
            bool oldActive = true;
            string newSkaterID = "Patty";
            string newTeam = "South Hawks";
            bool newActive = false;
            SkaterTeamApplication oldApp = new SkaterTeamApplication();
            SkaterTeamApplication newApp = new SkaterTeamApplication();
            oldApp.ApplicationID = applicationID;
            oldApp.skaterID = oldskaterID;
            oldApp.TeamName = oldTeam;
            oldApp.isActive = oldActive;
            newApp.ApplicationID = applicationID;
            newApp.skaterID = newSkaterID;
            newApp.TeamName = newTeam;
            newApp.isActive = newActive;


            ;



            //act
            _applicationManager.editSkaterteamApplication(oldApp, newApp);
            oldApp = _applicationManager.getSkaterteamApplicationByPrimaryKey(100);


            //assert
            Assert.AreEqual(oldApp.TeamName, newApp.TeamName);

            Assert.AreEqual(oldApp.skaterID, newApp.skaterID);
            Assert.AreEqual(oldApp.ApplicationID, newApp.ApplicationID);





        }
        [TestMethod]
        [ExpectedException(typeof(ApplicationException))]
        public void TestEditApplicaitonFailsWithBadData()
        {


            //arrage

            SkaterTeamApplication a = new SkaterTeamApplication();
            a.ApplicationID = 107;
            a.skaterID = "Malice";
            a.TeamName = "East Hawks";
            a.isActive = true;

            SkaterTeamApplication b = new SkaterTeamApplication();
            b.ApplicationID = 100;
            b.skaterID = "Balice";
            b.TeamName = "Central Hawks";
            b.isActive = true;

            //act
            _applicationManager.editSkaterteamApplication(a, b);

            //assert - nothing to do

        }

        [TestMethod]
        [ExpectedException(typeof(ApplicationException))]
        public void TestGetApplicaitonByPKFailsWithBadData()
        {


            //arrage
            int applicationID = 105;

            //act
            _applicationManager.getSkaterteamApplicationByPrimaryKey(applicationID);

            //assert - nothing to do

        }

        [TestMethod]
        [ExpectedException(typeof(ApplicationException))]
        public void TestPurgeApplicaitonFailsWithBadData()
        {


            //arrage
            int applicationID = 105;

            //act
            _applicationManager.purgeSkaterteamApplication(applicationID);

            //assert - nothing to do

        }
    }
}
