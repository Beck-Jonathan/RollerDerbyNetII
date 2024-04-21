using DataAccessFakes;
using LogicLayer;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using DataObjects;

namespace UnitTestProject1
{
    [TestClass]
    public class EventManagerTests
    {
        IEventManager _eventManager = null;

        [TestInitialize]
        public void testSetup()
        {
            _eventManager = new EventManager(new EventAccessorFakes());
        }
        [TestMethod]
        public void TestCreateEventCreatesAnEvent() {
            
            //arrange
            int actual = 0;
            int expected = 4;

            //act
            _eventManager.AddEvent(new Event());
            actual=_eventManager.getAllEvent().Count();

            //assert
            Assert.AreEqual(expected, actual);
        }

        [TestMethod]
        public void TestRetreiveAllEventsCanRetreiveAllEvents()
        {
            //arrange
            int actual = 0;
            int expected = 3;

            //act
            actual= _eventManager.getAllEvent().Count;

            //assert
            Assert.AreEqual(actual, expected);
        }
        [TestMethod]
        [ExpectedException(typeof(ApplicationException))]
        public void TestRetreiveAllEventsCanFail()
        {
            List <Event> _events = _eventManager.getAllEvent();
            foreach (Event e in _events)
            {
                _events.Remove(e);
            }
            List<Event> emptyevents = _eventManager.getAllEvent();
        }
        [TestMethod]
        public void TestRetreiveOneEventRetreivesAppropraiteEvent()
        {
            int actual = 0;
            int expected=1;
            actual = _eventManager.getEventByPrimaryKey(1).EventID;
            Assert.AreEqual(actual, expected);
        }
        [TestMethod]
        [ExpectedException(typeof(ApplicationException))]
        public void TestRetreiveOneEventCanFail() {
            Event nothing = _eventManager.getEventByPrimaryKey(10);
        }
        [TestMethod]
        public void TestRetreiveByTypeRetreivesAppropraiteEvents()
        {
            int actual = 0;
            int expect = 2;
            actual = _eventManager.getAllEventByType("Practice").Count;
            Assert.AreEqual(actual, expect);
        }
        [TestMethod]
        [ExpectedException(typeof(ApplicationException))]
        public void TestRetreiveByTypeCanFail() {
            List<Event> _events = _eventManager.getAllEvent();
            foreach (Event e in _events)
            {
                _events.Remove(e);
            }
            int noResults = _eventManager.getAllEventByType("Practive").Count;
        }

        [TestMethod]
        public void TestUpdateEventUpdatesAnEvent()
        {
            String expected = "TestRecord";
            String actual = "";
            Event old = new Event();
            old.EventID = 1;
            Event newEvent = new Event();
            newEvent.EventID = 1;
            newEvent.EventType = "TestRecord";
            _eventManager.updateEvent(old, newEvent);
            actual = _eventManager.getEventByPrimaryKey(1).EventType;
            Assert.AreEqual(expected, actual);

        }
        [TestMethod]
        [ExpectedException(typeof(ApplicationException))]
        public void TestUpdateEventCanFail() {
            Event old = new Event();
            Event newEvent  = new Event();
            _eventManager.updateEvent(old, newEvent);
        }
    }
}
