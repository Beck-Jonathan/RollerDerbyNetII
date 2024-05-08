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
        ILocationManager locationManager = new LocationManager();
        // GET: Location
        [AllowAnonymous]
        public ActionResult Index()
        {
            List<LocationVM> locations = locationManager.SelectAllLocations();
            return View(locations);
        }

        [AllowAnonymous]
        // GET: Location/Details/5
        public ActionResult Details(String id)
        {
            dropDowns();
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
            dropDowns();
            return View();
        }

        // POST: Location/Create
        [HttpPost]
        public ActionResult Create(Location _location)
        {
           
            bool tempresult = ModelState.IsValid;
            dropDowns();
            LocationVM location = new LocationVM()
            {
                LocationID = _location.LocationID,
                LeagueID = _location.LeagueID,
                City = _location.City,
                State = _location.State,
                Zip = _location.Zip,
                ContactPhone = _location.ContactPhone

            };
            if (ModelState.IsValid)
            {
                try
                {
                    bool result = locationManager.AddLocation(location);
                    if (result)
                    {
                        return RedirectToAction("Index");
                    }
                    else { return View(_location); }
                }
                catch
                {
                    return View(_location);
                }
                
            }
            return View(_location);
        }

        // GET: Location/Edit/5
        public ActionResult Edit(String id)
        {
            dropDowns();
            Location edit = new Location();
            try
            {
                edit = locationManager.SelectLocationByLocationID(id);
                Session["edit"] = edit;
            }
            catch (Exception ex)
            {
                ViewBag.Error = ex.Message;
                return View("Error");
            }



            return View(edit);
        }

        // POST: Location/Edit/5
        [HttpPost]
        public ActionResult Edit(string id, Location collection)
        {
            dropDowns();
            Location old = (Location)Session["edit"];
            old.LocationID = id;
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
            dropDowns();
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
            dropDowns();
            Location edit = (Location)Session["delete"];
            try
            {
                locationManager.deleteLocation(id);

                return RedirectToAction("Index");
            }
            catch (Exception ex)
            {
                return View();
            }
        }
        public void dropDowns() {
           List<string> Leagues = locationManager.getLeaguesForDropDown();
            ViewBag.LeagueList = Leagues;
        
        }
    }
}
