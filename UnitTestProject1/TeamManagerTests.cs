using DataAccessFakes;
using DataObjects;
using LogicLayer;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using System;

namespace UnitTestProject1
{
    [TestClass]
    public class TeamManagerTests
    {
        ITeamManager _teamManager = null;
        [TestInitialize]
        public void testSetup()
        {
            _teamManager = new TeamManager(new TeamAccessorFakes());
        }

        [TestMethod]
        public void TestSelectTeamByIdReturnscorrectteam()
        {
            //arrange
            string actualResult = "";
            string ExpectedResult = "Chicago";
            string input = "Blackhawks";

            //act
            actualResult = _teamManager.getTeamByPrimaryKey(input).City;


            //assert
            Assert.AreEqual(actualResult, ExpectedResult);
        }
        [TestMethod]
        public void TestInsertTeamAddsATeam()
        {
            //arrange
            bool Expected = true;
            bool Actual = false;
            Team team = new Team();

            //act
            Actual = _teamManager.addTeam(team);

            //assert
            Assert.AreEqual(Expected, Actual);
        }
        [TestMethod]
        public void TestSelectAllTeamsReturnsAllTeams()
        {
            //arrange
            int actual = 0;
            int expected = 3;

            //act
            actual = _teamManager.getAllTeam().Count;

            //assert
            Assert.AreEqual(actual, expected);
        }
        [TestMethod]
        public void TestUpdateTeamUpdatesATeam()
        {
            //arrange
            string actual = "";
            string expected = "Moline";

            TeamVM team1 = new TeamVM();
            team1.TeamId = "Blackhawks";
            team1.LeagueID = "WFTDA";
            team1.MonthlyDue = 40;
            team1.City = "Chicago";
            team1.State = "IL";
            team1.Zip = "55214";
            Team newTeam = new Team();
            newTeam.State = "IL";
            newTeam.MonthlyDue = 50;
            newTeam.LeagueID = "WTFDA";
            newTeam.City = "Moline";
            newTeam.Zip = "55214";
            newTeam.TeamId = "Blackhawks";



            //act
            _teamManager.editTeam(team1, newTeam);
            actual = _teamManager.getTeamByPrimaryKey("Blackhawks").City;

            //assert
            Assert.AreEqual(actual, expected);
        }
        [TestMethod]
        [ExpectedException(typeof(ApplicationException))]
        public void TestUpdateTeamFailsWithBadData()
        {
            //arrange

            Team oldTeam = new Team();
            oldTeam.State = "IL";
            oldTeam.MonthlyDue = 40;
            oldTeam.LeagueID = "WTFDA";
            oldTeam.City = "Chicago";
            oldTeam.Zip = "55214";
            oldTeam.TeamId = "Blueberries";
            Team newTeam = new Team();
            newTeam.State = "IL";
            newTeam.MonthlyDue = 50;
            newTeam.LeagueID = "WTFDA";
            newTeam.City = "Moline";
            newTeam.Zip = "55214";



            //act
            _teamManager.editTeam(oldTeam, newTeam);
            //slack

            //assert nothing to do

        }
        [TestMethod]
        public void test6()
        {
            //arrange

            //act

            //assert
        }
        [TestMethod]
        public void test7()
        {
            //arrange

            //act

            //assert
        }
    }
}
