using DataAccessFakes;
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

            //act
            Actual = _teamManager.addTeam("a", "b", 50,"c", "d", "e");

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



            //act
            _teamManager.editTeam("Blackhawks", "WFTDA",40 ,"Chicago", "IL", "55214", "Blackhawks", "WFTDA",50 ,"Moline", "IL", "55214");
            actual = _teamManager.getTeamByPrimaryKey("Blackhawks").City;

            //assert
            Assert.AreEqual(actual, expected);
        }
        [TestMethod]
        [ExpectedException(typeof(ApplicationException))]
        public void TestUpdateTeamFailsWithBadData()
        {
            //arrange

            string actual = "";
            string expected = "Moline";



            //act
            _teamManager.editTeam("Blackhawks", "xx",45 ,"xx", "IL", "55214", "Blackhawks" ,"WFTDA",50, "Moline", "IL", "55214");
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
