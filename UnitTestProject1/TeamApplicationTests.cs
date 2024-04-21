using DataAccessFakes;
using DataObjects;
using LogicLayer;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using System;
using System.Linq;

namespace UnitTestProject1
{
    [TestClass]
    public class TeamApplicationTests
    {
        ITeamApplicationManager _applicationmanager = null;
        [TestInitialize]
        public void testSetup()
        {
            _applicationmanager = new TeamApplicationManager(new TeamApplicationFakes());
        }
        [TestMethod]
        public void TestAddApplicaitonAddsApplication()
        {
            //arrange
            int actual = 0;
            int expected = 4;

            //act
            _applicationmanager.addTeamApplication(new DataObjects.TeamApplication());
            actual = _applicationmanager.getAllTeamApplication().Count();

            //assert
            Assert.AreEqual(expected, actual);
        }

        [TestMethod]
        public void TestGetAllApplicaitonsGetsAllApplicaitons()
        {
            int actual = 0;
            int expected = 3;

            actual = _applicationmanager.getAllTeamApplication().Count;
            Assert.AreEqual(expected, actual);


        }
        [TestMethod]
        public void TestselectTeamApplicationByPrimaryKey()
        {
            string actual = "";
            string expected = "malFoy";

            actual = _applicationmanager.getTeamApplicationByPrimaryKey(100).SkaterID;

            Assert.AreEqual(actual, expected);
        }

        [TestMethod]
        [ExpectedException(typeof(ApplicationException))]
        public void TestSelectTeamApplicationFailsWithBadKey()
        {
            string actual = "";
            string expected = "malfoy";
            actual = actual = _applicationmanager.getTeamApplicationByPrimaryKey(105).SkaterID;

        }
        [TestMethod]
        public void TestupdateTeamApplicationUpdatesAnApplication()
        {
            string actual = "";
            string expected = "Approved";
            TeamApplication teamApplication = new TeamApplication();
            TeamApplication newteamApplication = new TeamApplication();
            teamApplication.TeamApplicationID = 100;
            teamApplication.ApplicationStatus = "pending";
            newteamApplication.TeamApplicationID = 100;
            newteamApplication.ApplicationStatus = "Approved";
            _applicationmanager.editTeamApplication(teamApplication, newteamApplication);
            actual = _applicationmanager.getTeamApplicationByPrimaryKey(100).ApplicationStatus;
            Assert.AreEqual(actual, expected);


        }

        [TestMethod]
        [ExpectedException(typeof(ApplicationException))]
        public void TestupdateTeamApplicationFailsWithBadData()
        {
            string actual = "";
            string expected = "Approved";
            TeamApplication teamApplication = new TeamApplication();
            TeamApplication newteamApplication = new TeamApplication();
            teamApplication.TeamApplicationID = 104;
            teamApplication.ApplicationStatus = "pending";
            newteamApplication.TeamApplicationID = 100;
            newteamApplication.ApplicationStatus = "Approved";
            _applicationmanager.editTeamApplication(teamApplication, newteamApplication);


        }



    }
}

