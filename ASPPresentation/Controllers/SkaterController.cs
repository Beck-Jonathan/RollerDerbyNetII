using DataObjects;
using LogicLayer;
using System;
using System.Collections.Generic;
using System.Web.Mvc;

namespace ASPPresentation.Controllers
{
    [Authorize]
    public class SkaterController : Controller
    {
        ISkaterManager skaterManager;

        // GET: Skater
        [AllowAnonymous]
        public ActionResult Index()
        {
            skaterManager = new SkaterManager();
            List<SkaterVM> skaters = new List<SkaterVM>();
            try
            {
                skaters = skaterManager.getAllSkater();
            }
            catch (Exception ex)
            {

                ViewBag.Error = ex.Message;
                return View("Error");
            }

            return View(skaters);
        }

        [Authorize(Roles = "Skater,Team_admin,League_Admin")]
        // GET: Skater/Details/5
        public ActionResult Details(String id)
        {
            SkaterVM passed;
            skaterManager = new SkaterManager();
            try
            {
                passed = skaterManager.GetSkaterVMByDerbyName(id);
            }
            catch (Exception ex)
            {
                ViewBag.Error = ex.Message;
                return View("Error");
            }

            return View(passed);
        }

        // GET: Skater/Create
        [Authorize(Roles = "Administrator,Team_admin,League_Admin")]
        public ActionResult Create()
        {
            try
            {
                fillDropDowns();
            }
            catch (Exception ex)
            {

                ViewBag.Error = ex.Message;
                return View("Error");
            }

            return View();
        }

        // POST: Skater/Create
        [HttpPost]
        [Authorize(Roles = "Administrator,Team_admin,League_Admin")]
        public ActionResult Create(SkaterVM skater)

        {
            skater.passwordhash = "jfdkswljafkd";
            ICollection<String> keys = ModelState.Keys;
            ModelState.Remove("passwordHash");
            //ModelState["passwordHash"].Errors = 0;
            if (ModelState.IsValid)
            {
                try
                {
                    skaterManager = new SkaterManager();


                    skaterManager.addSkater(skater);


                    // TODO: Add insert logic here

                    return RedirectToAction("Index");
                }
                catch
                {
                    return View();
                }
            }
            return Create();
        }

        // GET: Skater/Edit/5
        [Authorize(Roles = "Administrator,Team_admin,League_Admin")]
        public ActionResult Edit(string id)
        {
            SkaterVM skater;
            SkaterManager manager = new SkaterManager();
            try
            {
                fillDropDowns();
                skater = manager.GetSkaterVMByDerbyName(id);
                Session.Add("oldSkater", skater);
            }
            catch (Exception ex)
            {

                ViewBag.Error = ex.Message;
                return View("Error");
            }
            return View(skater);
        }

        // POST: Skater/Edit/5
        [HttpPost]
        [Authorize(Roles = "Administrator,Team_admin,League_Admin")]
        public ActionResult Edit(string id, Skater newSkater)
        {
            SkaterManager sm = new SkaterManager();
            try
            {
                fillDropDowns();
                Skater oldSkater = (Skater)Session["oldSkater"];
                if (oldSkater != null)
                {
                    sm.editSkater(oldSkater, newSkater);
                }

                return RedirectToAction("Index");
            }
            catch
            {
                return View();
            }
        }
        [Authorize(Roles = "Administrator,League_Admin")]
        // GET: Skater/Delete/5
        public ActionResult Delete(string id)
        {
            return View();
        }

        // POST: Skater/Delete/5
        [HttpPost]
        [Authorize(Roles = "Administrator,League_Admin")]
        public ActionResult Delete(string id, SkaterVM deleteSkater)
        {
            SkaterManager sm = new SkaterManager();
            try
            {
                sm.purgeSkater(deleteSkater);

                return RedirectToAction("Index");
            }
            catch
            {
                return View();
            }
        }

        private void fillDropDowns() {
            skaterManager = new SkaterManager();
            try
            {
                ViewBag.Teams = skaterManager.SelectDistinctTeamsForDropDown();
            }
            catch (Exception ex)
            {

                throw ex;
            }
        
        }
    }
}
