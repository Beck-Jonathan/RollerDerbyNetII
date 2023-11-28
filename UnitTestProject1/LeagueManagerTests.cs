using DataAccessFakes;
using DataObjects;
using LogicLayer;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using System;

namespace UnitTestProject1
{
    [TestClass]
    public class LeagueManagerTests
    {
        ILeagueManager _leagueManager = null;
        [TestInitialize]
        public void testSetup()
        {
            _leagueManager = new LeagueManager(new LeagueAccessorFakes());
        }
        [TestMethod]
        public void TestSelectLeagueByLeagueIDReturnsCorrectLeague()
        {
            //arrange
            string expectedResult = "Midwest";
            string actualResult = "";
            string testInput = "Fake1";



            //act
            League resultLeague = _leagueManager.getLeagueByPrimaryKey("Fake1");
            actualResult = resultLeague.Region;

            //assert
            Assert.AreEqual(expectedResult, actualResult);

        }
        [TestMethod]

        public void TestCreateLeagueProperlyAddsaLague()
        {
            //arrange
            string resultID = "";
            string resultRegion = "";
            string resultGender = "";
            string inputId = "test4";
            string inputRegion = "Ohio";
            string inputGender = "mixed";
            League newLeague = new League();
            newLeague.Region = inputRegion;
            newLeague.Gender = inputGender;
            newLeague.LeagueID = inputId;

            //act
            _leagueManager.createLeague(newLeague);
            resultID = _leagueManager.getLeagueByPrimaryKey(inputId).LeagueID;
            resultRegion = _leagueManager.getLeagueByPrimaryKey(inputId).Region;
            resultGender = _leagueManager.getLeagueByPrimaryKey(inputId).Gender;


            //assert
            Assert.AreEqual(resultID, inputId);
            Assert.AreEqual(resultGender, inputGender);
            Assert.AreEqual(inputRegion, resultRegion);

        }
        [TestMethod]

        public void TestSelectAllLeaguesReturnsAllLeagues()
        {
            //arrange
            int returncount = 0;
            int expectedcount = 3;


            //act
            returncount = _leagueManager.getAllLeague().Count;

            //assert
            Assert.AreEqual(returncount, expectedcount);

        }
        [TestMethod]

        public void TestUpdateLeagueUpdatesALeague()
        {
            //arrange
            string inputoldId = "Fake1";
            string inputoldRegion = "Midwest";
            string inputOldGender = "Female";
            League oldLeague = new League();
            oldLeague.Gender = inputOldGender;
            oldLeague.Region = inputoldRegion;
            oldLeague.LeagueID = inputoldId;
            string inputNewID = inputoldId;
            string inputNewRegion = "Texas";
            string inputNewGender = "Mixed";
            League newLeague = new League();
            newLeague.Gender = inputNewGender;
            newLeague.Region = inputNewRegion;
            newLeague.LeagueID = inputNewID;

            string resultID = "";
            string resultRegion = "";
            string resultGender = "";

            string ExpectedID = "Fake1";
            string ExpectedRegion = "Texas";
            string ExpectedGender = "Mixed";

            //act
            _leagueManager.updateLeague(oldLeague, newLeague);



            resultID = _leagueManager.getLeagueByPrimaryKey(inputNewID).LeagueID;
            resultRegion = _leagueManager.getLeagueByPrimaryKey(inputNewID).Region;
            resultGender = _leagueManager.getLeagueByPrimaryKey(inputNewID).Gender;


            //assert
            Assert.AreEqual(ExpectedID, resultID);
            Assert.AreEqual(ExpectedGender, resultGender);
            Assert.AreEqual(ExpectedRegion, resultRegion);

        }
        [TestMethod]

        public void TestDeleteLeagueRemovesALeagueFromTheList()
        {
            //arrange
            int expectedcount = 2;
            int acutalCount = 0;

            String LeagueToDelete = "Fake1";
            League league = new League();
            league.LeagueID = LeagueToDelete;


            //act
            _leagueManager.deleteLeague(league);
            acutalCount = _leagueManager.getAllLeague().Count;


            //assert
            Assert.AreEqual(expectedcount, acutalCount);

        }
        [TestMethod]
        [ExpectedException(typeof(ApplicationException))]

        public void TestUpdateFailsWithBadData()
        {
            //arrange
            string inputoldId = "Fake4";
            string inputoldRegion = "Midwest";
            string inputOldGender = "Female";
            League oldLeague = new League();
            oldLeague.Gender = inputOldGender;
            oldLeague.Region = inputoldRegion;
            oldLeague.LeagueID = inputoldId;
            string inputNewID = inputoldId;
            string inputNewRegion = "Texas";
            string inputNewGender = "Mixed";
            League newLeague = new League();
            newLeague.Gender = inputNewGender;
            newLeague.Region = inputNewRegion;
            newLeague.LeagueID = inputNewID;



            //act
            _leagueManager.updateLeague(oldLeague, newLeague);

            //assert - nothing to do

        }
        [TestMethod]

        public void TestDeleteLocationFailsWithBadData()
        {
            //arrange
            int expectedcount = 3;
            int acutalCount = 0;
            String LeagueToDelete = "Fake4";
            League league = new League();
            league.LeagueID = LeagueToDelete;


            //act
            _leagueManager.deleteLeague(league);
            acutalCount = _leagueManager.getAllLeague().Count;


            //assert
            Assert.AreEqual(expectedcount, acutalCount);

        }

        [TestMethod]
        [ExpectedException(typeof(ApplicationException))]
        public void TestRetreiveByIdFailsWithBadData()
        {
            //arrange
            string LeagueID = "WFTDA";


            //act
            League league = _leagueManager.getLeagueByPrimaryKey(LeagueID);


            //assert

        }


    }
}
