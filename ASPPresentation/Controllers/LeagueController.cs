using DataObjects;
using LogicLayer;
using System;
using System.Collections.Generic;
using System.Web.Mvc;

namespace ASPPresentation.Controllers
{
    [Authorize(Roles = "League_Admin")]
    public class LeagueController : Controller
    {
        LeagueManager lm = new LeagueManager();
        // GET: League
        [AllowAnonymous]
        public ActionResult Index()
        {

            List<League> leagues = new List<League>();
            try
            {
                leagues = lm.getAllLeague();
            }
            catch (Exception ex)
            {

                throw;
            }

            return View(leagues);
        }

        // GET: League/Details/5
        public ActionResult Details(string id)
        {
            League league = new League();
            try
            {
                league = lm.getLeagueByPrimaryKey(id);
            }
            catch (Exception)
            {

                throw;
            }
            return View(league);
        }

        // GET: League/Create
        public ActionResult Create()
        {
            return View();
        }

        // POST: League/Create
        [HttpPost]
        public ActionResult Create(League league)
        {
            if (ModelState.IsValid)
            {
                try
                {
                    lm.createLeague(league);

                    return RedirectToAction("Index");
                }
                catch
                {
                    return View();
                }


            }
            return RedirectToAction("Index");


        }

        // GET: League/Edit/5
        public ActionResult Edit(string id)

        {
            League league;
            try
            {
                league = lm.getLeagueByPrimaryKey(id);
                Session.Add("oldLeague", league);
            }
            catch (Exception ex)
            {

                return RedirectToAction("Index");
            }

            return View(league);
        }

        // POST: League/Edit/5
        [HttpPost]
        public ActionResult Edit(String id, League newLeague)
        {
            League oldLeague = (League)Session["oldLeague"];
            if (ModelState.IsValid)
            {
                try
                {

                    lm.updateLeague(oldLeague, newLeague);

                    return RedirectToAction("Index");
                }
                catch
                {
                    return View();
                }
            }
            else
            {
                return View();
            }

        }

        // GET: League/Delete/5
        public ActionResult Delete(string id)
        {
            League league;
            try
            {
                league = lm.getLeagueByPrimaryKey(id);
                Session.Add("deleteLeague", league);
            }
            catch (Exception ex)
            {

                return RedirectToAction("Index");
            }

            return View(league);
        }

        // POST: League/Delete/5
        [HttpPost]
        public ActionResult Delete(string id, League league)
        {
            int x = 0;
            try
            {
                League _league = (League)Session["deleteLeague"];
                lm.deleteLeague(_league);

                return RedirectToAction("Index");
            }
            catch
            {
                return View();
            }
        }
    }
}
