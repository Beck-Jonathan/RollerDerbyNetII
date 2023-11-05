using DataAccessFakes;
using DataObjects;
using LogicLayer;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using System;

namespace UnitTestProject1
{
    [TestClass]
    public class SkaterManagerTests
    {
        ISkaterManager _skaterManager = null;

        [TestInitialize]
        public void testSetup()
        {
            _skaterManager = new SkaterManager(new SkaterAccessorFake());
        }

        [TestMethod]
        public void TestHashSha256ReturnsACorrectHashValue()
        {
            // in TDD (test-driven development, the test comes first)
            // we use the red-green-refactor workflow
            // we write the test method with the A-A-A framework

            // Arrange - set up the test condition

            string testString = "newuser";
            string actualHash = "";
            string expectedHash = "9c9064c59f1ffa2e174ee754d2979be80dd30db552ec03e7e327e9b1a4bd594e";

            // Act - invoke the method being tested, and capture results
            actualHash = _skaterManager.hashSHA256(testString);

            // Assert
            Assert.AreEqual(expectedHash, actualHash);
        }
        [TestMethod]
        public void TestAuthenticateSkaterPassesWithCorrectDerbyNameAndPassword()
        {

            //arrange
            string email = "tess@company.com";
            string password = "newuser";
            bool expectedresult = true;
            bool acutalResult = false;




            //act
            acutalResult = _skaterManager.AuthenticateSkater(email, password);




            //assert
            Assert.AreEqual(expectedresult, acutalResult);

        }

        [TestMethod]
        public void TestAuthenticateSkaterFailsWithBadDerbyNameAndPassword()
        {

            //arrange
            string email = "tess@company.com";
            string password = "Louse";
            bool expectedresult = false;
            bool acutalResult = true;




            //act
            acutalResult = _skaterManager.AuthenticateSkater(email, password);



            //assert
            Assert.AreEqual(expectedresult, acutalResult);

        }

        [TestMethod]
        public void TestGetSkaterVMbyDerbyNamereturnsCorrectSkater()
        {
            //arrange
            string derbyname = "High";
            string expectedName = "Jess";
            string actualSkaterName = "";


            //act
            Skater skater = _skaterManager.GetSkaterVMByDerbyName(derbyname);
            actualSkaterName = skater.GivenName;

            //assert
            Assert.AreEqual(expectedName, actualSkaterName);


        }
        [TestMethod]
        [ExpectedException(typeof(ApplicationException))]
        public void TestGetskaterbyDerbyNamelfialswithbadDerbyName()
        {
            //arrage
            string derbyName = "Smitty";//bad derby name
            string expctedSkaterID = "1";
            string actualSkaterID = "0";

            //act
            Skater skater = _skaterManager.GetSkaterVMByDerbyName(derbyName);
            actualSkaterID = skater.SkaterID;

            //assert-nothing to do

        }
        [TestMethod]
        public void TestGetRolesBySkaterIDReturnsCorrectRoles()
        {
            //arrage
            string DerbyName = "Hit";
            int expectedRoleCount = 2;
            int actualRoleCount = 0;

            //act
            actualRoleCount = _skaterManager.getRolesBySkaterDerbyName(DerbyName).Count;

            //assert
            Assert.AreEqual(expectedRoleCount, actualRoleCount);


        }
        [TestMethod]
        public void TestResetPasswordWorksCorrectly()
        {

            //arrage
            string derbyName = "Hit";
            string oldPW = "newuser";
            string newPW = "password";
            bool expectedResult = true;
            bool actualResult = false;

            //act
            actualResult = _skaterManager.resetPassword(derbyName, oldPW, newPW);


            //assert
            Assert.AreEqual(expectedResult, actualResult);

        }
    }
}
