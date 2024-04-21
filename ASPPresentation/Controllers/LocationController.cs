using DataObjects;
using LogicLayer;
using System;
using System.Collections.Generic;
using System.Web.Mvc;

namespace ASPPresentation.Controllers
{
    [Authorize(Roles = "Fan,Coach,Team_Admin,League_Admin")]
    public class LocationController : Controller
    {
        LocationManager locationManager = new LocationManager();
        // GET: Location
        public ActionResult Index()
        {
            List<LocationVM> locations = locationManager.SelectAllLocations();
            return View(locations);
        }

        // GET: Location/Details/5
        public ActionResult Details(String id)
        {
            LocationVM location = new LocationVM();
            try
            {
                location = locationManager.SelectLocationByLocationID(id);
            }
            catch (Exception)
            {

                throw;
            }

            return View(location);
        }

        // GET: Location/Create
        public ActionResult Create()
        {
            return View();
        }

        // POST: Location/Create
        [HttpPost]
        public ActionResult Create(Location _location)
        {
            try
            {
                // TODO: Add insert logic here

                return RedirectToAction("Index");
            }
            catch
            {
                return View();
            }
        }

        // GET: Location/Edit/5
        public ActionResult Edit(String id)
        {
            Location edit = new Location();
            try
            {
                edit = locationManager.SelectLocationByLocationID(id);
                Session["edit"] = edit;
            }
            catch (Exception)
            {

                throw;
            }



            return View(edit);
        }

        // POST: Location/Edit/5
        [HttpPost]
        public ActionResult Edit(int id, Location collection)
        {
            Location old = (Location)Session["edit"];
            try
            {
                locationManager.updateLocation(old, collection);

                return RedirectToAction("Index");
            }
            catch
            {
                return View();
            }
        }

        // GET: Location/Delete/5
        [Authorize(Roles = "League_Admin")]
        public ActionResult Delete(String id)
        {
            Location edit = new Location();
            try
            {
                edit = locationManager.SelectLocationByLocationID(id);
                Session["delete"] = edit;
            }
            catch (Exception)
            {

                throw;
            }



            return View(edit);
        }

        // POST: Location/Delete/5
        [HttpPost]
        [Authorize(Roles = "League_Admin")]
        public ActionResult Delete(String id, FormCollection collection)
        {
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
    }
}
