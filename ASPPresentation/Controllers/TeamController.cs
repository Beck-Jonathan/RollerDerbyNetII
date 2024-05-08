using DataObjects;
using LogicLayer;
using System;
using System.Collections.Generic;
using System.Web.Mvc;


namespace ASPPresentation.Controllers
{
    [Authorize(Roles = "League_Admin")]
    public class TeamController : Controller
    {
        ITeamManager tm;
        // GET: Team
        [AllowAnonymous]
        public ActionResult Index()
        {
            tm = new TeamManager();
            List<Team> teams = new List<Team>();
            teams = tm.getAllTeam();

            return View(teams);
        }

        [AllowAnonymous]
        // GET: Team/Details/5
        public ActionResult Details(string id)
        {
            tm = new TeamManager();
            Team team;
            try
            {
                 team = tm.getTeamByPrimaryKey(id);
            }
            catch (Exception ex)
            {

                throw;
            }
           
            return View(team);
        }

        // GET: Team/Create
        [Authorize(Roles = "League_Admin")]
        public ActionResult Create()
        {
            dropdowns();
            return View();
        }

        // POST: Team/Create
        [HttpPost]
        [Authorize(Roles = "League_Admin")]
        public ActionResult Create(Team team)
        {
            dropdowns();
            try
            {
                if (ModelState.IsValid)
                {
                    tm = new TeamManager();
                    bool result = tm.addTeam(team);
                    if (!result)
                    {
                        RedirectToAction("Create");
                    }
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

        // GET: Team/Edit/5
        [Authorize(Roles = "League_Admin")]
        public ActionResult Edit(string id)
        {
            dropdowns();
            Team team = null;
            try
            {
                tm = new TeamManager();
                team = tm.getTeamByPrimaryKey(id);
            }
            catch (Exception ex)
            {
                ViewBag.ErrorMessage = ex.Message;
                ViewBag.InnerErrorMessage = ex.InnerException.Message;
                return View("Error");

            }
            return View(team);
        }

        // POST: Team/Edit/5
        [HttpPost]
        [Authorize(Roles = "League_Admin")]
        public ActionResult Edit(string id, Team team)
        {
            dropdowns();
            try
            {
                tm = new TeamManager();
                Team old = tm.getTeamByPrimaryKey(id);
                Team updated = team;
                int result = tm.editTeam(old, updated);


                return RedirectToAction("Index");
            }
            catch (Exception ex)
            {
                ViewBag.ErrorMessage = ex.Message;
                ViewBag.InnerErrorMessage = ex.InnerException.Message;
                return View("Error");

            }
        }

        // GET: Team/Delete/5
        [Authorize(Roles = "League_Admin")]
        public ActionResult Delete(int id)
        {
            dropdowns();
            return View();
        }

        // POST: Team/Delete/5
        [HttpPost]
        [Authorize(Roles = "League_Admin")]
        public ActionResult Delete(int id, FormCollection collection)
        {
            dropdowns();
            try
            {
                // TODO: Add delete logic here

                return RedirectToAction("Index");
            }
            catch (Exception ex)
            {
                ViewBag.ErrorMessage = ex.Message;
                ViewBag.InnerErrorMessage = ex.InnerException.Message;
                return View("Error");

            }
        }

        public void dropdowns() {
            LocationManager locationManager = new LocationManager();
            List<string> Leagues = locationManager.getLeaguesForDropDown();
            ViewBag.LeagueList = Leagues;

        }
    }
}
