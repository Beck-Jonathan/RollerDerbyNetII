using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using ASPPresentation.Migrations;
using DataObjects;
using LogicLayer;

namespace ASPPresentation.Controllers
{
    public class EventController : Controller
    {
        IEventManager _eventManager;
        // GET: Event
        public ActionResult Index()
        {
            int mode = 0;
            List<Event> allEvents = new List<Event>();
            _eventManager = new EventManager();

            if (User.IsInRole("Skater") || User.IsInRole("Team_Admin")|| User.IsInRole("League_Admin"))
            {
                mode = 1;
            }
            try
            {

                if (mode == 1)
                {
                    allEvents = _eventManager.getAllEvent();
                }
                else
                {
                    allEvents = _eventManager.getAllEventByType("Game");
                }
            }
            catch (Exception ex)
            {
                ViewBag.ErrorMessage = ex.Message;
                ViewBag.InnerErrorMessage = ex.InnerException.Message;
                return View("Error");

            }
            return View(allEvents);
        }

        // GET: Event/Details/5
        public ActionResult Details(int id)
        {
            fillDropDowns();
            _eventManager = new EventManager();
            Event _event = new Event();
            try
            {
                _event = _eventManager.getEventByPrimaryKey(id);
            }
            catch (Exception ex)
            {
                ViewBag.ErrorMessage = ex.Message;
                ViewBag.InnerErrorMessage = ex.InnerException.Message;
                return View("Error");

            }
            return View(_event);
        }

        // GET: Event/Create
        public ActionResult Create()
        {
            fillDropDowns();
            return View();
        }

        // POST: Event/Create
        [HttpPost]
        public ActionResult Create(Event _event)
        {
            fillDropDowns();
            _eventManager = new EventManager();
            if (ModelState.IsValid)
            {
                try
                {
                    bool result = _eventManager.AddEvent(_event);
                    if (result)
                    {
                        return RedirectToAction("Index");
                    }
                }
                catch
                {
                    return View(_event);
                }
            }
            return View(_event);
        }

        // GET: Event/Edit/5
        public ActionResult Edit(int id)
        {
            fillDropDowns();
            _eventManager = new EventManager();
            Event _event = null;
            try
            {
                _event = _eventManager.getEventByPrimaryKey(id);
                Session.Add("oldEvent", _event);
            }
            catch (Exception ex)
            {

                ViewBag.ErrorMessage = ex.Message;
                ViewBag.InnerErrorMessage = ex.InnerException.Message;
                return View("Error");
            }
            return View(_event);
        }

        // POST: Event/Edit/5
        [HttpPost]
        public ActionResult Edit(int id, Event _newevent)
        {
            fillDropDowns();
            _eventManager = new EventManager();
            Event _old = (Event)Session["oldEvent"];
            _newevent.EventID = id;
            try
            {
                if (_old != null &&ModelState.IsValid)
                {
                    _eventManager.updateEvent(_old, _newevent);
                }

                return RedirectToAction("Index");
            }
            catch (Exception ex)
            {
                ViewBag.ErrorMessage = ex.Message;
                ViewBag.InnerErrorMessage = ex.InnerException.Message;
                return View("Error");
            }

        }

        // GET: Event/Delete/5
        public ActionResult Delete(int id)
        {
            fillDropDowns();
            _eventManager = new EventManager();
            Event _event = null;
            
            
                try
                {
                   _event= _eventManager.getEventByPrimaryKey(id);
                Session.Add("oldEvent", _event);
                if (_event == null) 
                {
                    throw new ApplicationException("Event not found");

                }
                    
                }
                catch (Exception ex)
                {
                    ViewBag.ErrorMessage = ex.Message;
                    ViewBag.InnerErrorMessage = ex.InnerException.Message;
                    return View("Error");
                }
            
            return View(_event);
        }

        // POST: Event/Delete/5
        [HttpPost]
        public ActionResult Delete(int id, Event _event)
        {
            fillDropDowns();
            _eventManager = new EventManager();
            _event = (Event)Session["oldEvent"];
            _event.EventID = id;
            
                try
                {
                    _eventManager.purgeEvent(_event);

                    return RedirectToAction("Index");
                }
                catch (Exception ex)
                {
                    ViewBag.ErrorMessage = ex.Message;
                    ViewBag.InnerErrorMessage = ex.InnerException.Message;
                    return View("Error");
                }
            
            return View(_event);
        }

        private void fillDropDowns()
        {_eventManager = new EventManager();
            try
            {
                ViewBag.LocatonList = _eventManager.getDistinctLocationForDropDown();
                ViewBag.TypeList = new List<String> {"Game","Practice","Mixer" };
            }
            catch (Exception)
            {

                ViewBag.LocationList = new List<string>();
            }
        }
    }

}