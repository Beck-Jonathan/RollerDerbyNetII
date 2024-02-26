using DataAccessFakes;
using DataObjects;
using LogicLayer;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace UnitTestProject1
{
    [TestClass]
    public  class GearApplicaitonManagerTests
    {
        GearApplicationManager _GAManager = null;
       
        [TestInitialize]
        public void testSetup()
        {
          _GAManager   = new GearApplicationManager(new GearApplicationFakes());
           
        }

        [TestMethod]
        public void checkfunctions() {
           
            //_GAManager.editGearApplication();
            
            //_GAManager.getGearApplicationByPrimaryKey();
            
        
        }
        [TestMethod]
        public void TestGetAllApplicationsGetsAllApplications() {
            int actual = 0;
            int expected = 3;

            actual = _GAManager.getAllGearApplication().Count();

            Assert.AreEqual(expected, actual);
        
        
        }

        [TestMethod]
        public void TestAddApplicationAddsAnApplication() {
            int acutal = 0;
            int expected = 4;

            GearApplication gearApplication = new GearApplication();
            _GAManager.addGearApplication(gearApplication);
            acutal = _GAManager.getAllGearApplication().Count();

            Assert.AreEqual(expected, acutal);

        }
        [TestMethod]

        public void TestEditGearApplicationEditsAGearApplication() {
            int oldPK = 100;
            string actualstatus = "";
            String oldStatus = "Pending";
            string newStatus = "accepted";
            GearApplication ga = new GearApplication();
            ga.ApplicationID = oldPK;
            ga.ApplicationStatus = oldStatus;
            GearApplication ga2 = new GearApplication();
            ga2.ApplicationID = oldPK;
            ga2.ApplicationStatus = newStatus;
            int result = _GAManager.editGearApplication(ga, ga2);
            actualstatus = _GAManager.getGearApplicationByPrimaryKey(oldPK).ApplicationStatus;
            Assert.AreEqual(newStatus,actualstatus);




        }
        [TestMethod]
        [ExpectedException(typeof(ApplicationException))]
        public void TestEditGearApplicationFailsWithBadData() {
            int oldPK = 104;
            String oldStatus = "Pending";
            string newStatus = "accepted";
            GearApplication ga = new GearApplication();
            ga.ApplicationID = oldPK;
            ga.ApplicationStatus = oldStatus;
            GearApplication ga2 = new GearApplication();
            ga2.ApplicationID = oldPK;
            ga2.ApplicationStatus = newStatus;
            int result = _GAManager.editGearApplication(ga, ga2);
            
        
        }
        [TestMethod]
        public void TestSelectByPrimaryKeyReturnsTheCorrectApplication() {
            int PrimaryKey = 100;
            string actual = "";
            string expected = "malfoy";
            actual = _GAManager.getGearApplicationByPrimaryKey(PrimaryKey).SkaterID;
            Assert.AreEqual(actual, expected);
        
        
        }
        [TestMethod]
        [ExpectedException(typeof(ApplicationException))]
        public void TestSelectByPrimaryKeyFailsWithBadData() {
            _GAManager.getGearApplicationByPrimaryKey(40);

        }


    }

}
