using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using DataObjects;
using LogicLayer;

namespace ASPPresentation.Controllers
{
    public class EventController : Controller
    {
        EventManager _eventManager;
        // GET: Event
        public ActionResult Index()
        {
            int mode = 0;
            List<Event> allEvents = new List<Event>();
            _eventManager = new EventManager();

            if (User.IsInRole("Coach"))
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
            return View();
        }

        // POST: Event/Edit/5
        [HttpPost]
        public ActionResult Edit(int id, FormCollection collection)
        {
            fillDropDowns();
            try
            {
                // TODO: Add update logic here

                return RedirectToAction("Index");
            }
            catch
            {
                return View();
            }
        }

        // GET: Event/Delete/5
        public ActionResult Delete(int id)
        {
            fillDropDowns();
            return View();
        }

        // POST: Event/Delete/5
        [HttpPost]
        public ActionResult Delete(int id, FormCollection collection)
        {
            fillDropDowns();
            try
            {
                // TODO: Add delete logic here

                return RedirectToAction("Index");
            }
            catch
            {
                return View();
            }
        }

        private void fillDropDowns()
        {_eventManager = new EventManager();
            try
            {
                ViewBag.LocatonList = _eventManager.getDistinctLocationForDropDown();
            }
            catch (Exception)
            {

                ViewBag.LocationList = new List<string>();
            }
        }
    }

}