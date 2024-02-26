using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using DataAccessFakes;
using LogicLayer;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace UnitTestProject1
{
    [TestClass]
    public class GearRequestManagerTests
    {
        IGearRequestManager _reqManager = null;
        [TestInitialize]
        public void testSetup()
        {
            _reqManager = new GearRequestManager(new GearRequestFakes());
        }
        [TestMethod]
        public void TestAddGearRequestAddsAGearRequest() {
            int actual = 0;
            int expected = 1;
            actual = _reqManager.addGearRequest(new DataObjects.GearRequest());
            Assert.AreEqual(expected, actual);
        
        
        }
        [TestMethod]
        public void TestSelectGearRequestByKeyGetsCorrectRequest() {
            string actual = "";
            string expected =  "Large";
            actual = _reqManager.getGearRequestByPrimaryKey(100).WristGuardSize;
            Assert.AreEqual(expected, actual);
        
        
        }
        [TestMethod]
        [ExpectedException(typeof(ApplicationException))]
        public void TestSelectGearRequestFailsWithBadKey() {
            _reqManager.getGearRequestByPrimaryKey(104);
        
        }
    }
}
