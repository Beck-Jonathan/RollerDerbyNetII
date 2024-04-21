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
        // GET: Team
        [AllowAnonymous]
        public ActionResult Index()
        {
            TeamManager tm = new TeamManager();
            List<Team> teams = new List<Team>();
            teams = tm.getAllTeam();

            return View(teams);
        }

        [AllowAnonymous]
        // GET: Team/Details/5
        public ActionResult Details(string id)
        {
            TeamManager tm = new TeamManager();
            Team team = tm.getTeamByPrimaryKey(id);
            return View(team);
        }

        // GET: Team/Create
        [Authorize(Roles = "League_Admin")]
        public ActionResult Create()
        {
            return View();
        }

        // POST: Team/Create
        [HttpPost]
        [Authorize(Roles = "League_Admin")]
        public ActionResult Create(Team team)
        {
            try
            {
                if (ModelState.IsValid)
                {
                    TeamManager tm = new TeamManager();
                    bool result = tm.addTeam(team);
                    if (!result)
                    {
                        RedirectToAction("Create");
                    }
                }

                return RedirectToAction("Index");
            }
            catch
            {
                return View();
            }
        }

        // GET: Team/Edit/5
        [Authorize(Roles = "League_Admin")]
        public ActionResult Edit(string id)
        {
            Team team = null;
            try
            {
                TeamManager tm = new TeamManager();
                team = tm.getTeamByPrimaryKey(id);
            }
            catch (Exception)
            {

                throw;
            }
            return View(team);
        }

        // POST: Team/Edit/5
        [HttpPost]
        [Authorize(Roles = "League_Admin")]
        public ActionResult Edit(string id, Team team)
        {
            try
            {
                TeamManager tm = new TeamManager();
                Team old = tm.getTeamByPrimaryKey(id);
                Team updated = team;
                int result = tm.editTeam(old, updated);


                return RedirectToAction("Index");
            }
            catch
            {
                return View();
            }
        }

        // GET: Team/Delete/5
        [Authorize(Roles = "League_Admin")]
        public ActionResult Delete(int id)
        {
            return View();
        }

        // POST: Team/Delete/5
        [HttpPost]
        [Authorize(Roles = "League_Admin")]
        public ActionResult Delete(int id, FormCollection collection)
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
