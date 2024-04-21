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
        SkaterManager skaterManager;

        // GET: Skater
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

                throw ex;
            }

            return View(skaters);
        }

        // GET: Skater/Details/5
        public ActionResult Details(String id)
        {
            SkaterVM passed;
            skaterManager = new SkaterManager();
            try
            {
                passed = skaterManager.GetSkaterVMByDerbyName("BloodSoak");
            }
            catch (Exception ex)
            {

                throw ex;
            }

            return View(passed);
        }

        // GET: Skater/Create
        [Authorize(Roles = "Coach,Team_admin,League_Admin")]
        public ActionResult Create()
        {

            return View();
        }

        // POST: Skater/Create
        [HttpPost]
        [Authorize(Roles = "Coach,Team_admin,League_Admin")]
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
        [Authorize(Roles = "Coach,Team_admin,League_Admin")]
        public ActionResult Edit(string id)
        {
            SkaterVM skater;
            SkaterManager manager = new SkaterManager();
            try
            {
                skater = manager.GetSkaterVMByDerbyName(id);
                Session.Add("oldSkater", skater);
            }
            catch (Exception ex)
            {

                throw ex;
            }
            return View(skater);
        }

        // POST: Skater/Edit/5
        [HttpPost]
        [Authorize(Roles = "Coach,Team_admin,League_Admin")]
        public ActionResult Edit(string id, Skater newSkater)
        {
            SkaterManager sm = new SkaterManager();
            try
            {
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

        // GET: Skater/Delete/5
        public ActionResult Delete(string id)
        {
            return View();
        }

        // POST: Skater/Delete/5
        [HttpPost]
        [Authorize(Roles = "Coach,Team_admin,League_Admin")]
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
    }
}
