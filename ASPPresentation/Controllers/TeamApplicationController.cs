using DataObjects;
using LogicLayer;
using System;
using System.Collections.Generic;
using System.Web.Mvc;

namespace ASPPresentation.Controllers
{
    [Authorize(Roles = "Team_Admin,League_Admin")]
    public class TeamApplicationController : Controller
    {

        TeamApplicationManager teamApplicationManager = new TeamApplicationManager();
        // GET: TeamApplication
        public ActionResult Index()
        {
            List<TeamApplication> applications = new List<TeamApplication>();
            try
            {
                applications = teamApplicationManager.getAllTeamApplication();
            }
            catch (Exception ex)
            {

                throw ex;
            }
            return View(applications);
        }

        // GET: TeamApplication/Details/5
        public ActionResult Details(int id)
        {
            TeamApplication application;
            try
            {
                application = teamApplicationManager.getTeamApplicationByPrimaryKey(id);
            }
            catch (Exception ex)
            {

                throw ex;
            }
            return View(application);
        }

        // GET: TeamApplication/Create
        public ActionResult Create()
        {
            return View();
        }

        // POST: TeamApplication/Create
        [HttpPost]
        public ActionResult Create(TeamApplication collection)
        {
            try
            {
                teamApplicationManager.addTeamApplication(collection);

                return RedirectToAction("Index");
            }
            catch
            {
                return View();
            }
        }

        // GET: TeamApplication/Edit/5
        public ActionResult Edit(int id)
        {
            TeamApplication application;
            try
            {
                application = teamApplicationManager.getTeamApplicationByPrimaryKey(id);
                Session["application"] = application;
            }
            catch (Exception ex)
            {

                throw ex;
            }
            return View(application);
        }

        // POST: TeamApplication/Edit/5
        [HttpPost]
        public ActionResult Edit(int id, TeamApplication newapplication)
        {
            try
            {
                TeamApplication oldTeamApplication = (TeamApplication)Session["application"];
                teamApplicationManager.editTeamApplication(oldTeamApplication, newapplication);
                return RedirectToAction("Index");
            }
            catch
            {
                return View();
            }
        }

        // GET: TeamApplication/Delete/5
        public ActionResult Delete(int id)
        {
            TeamApplication application;
            try
            {
                application = teamApplicationManager.getTeamApplicationByPrimaryKey(id);
                Session["deleteApplication"] = application;
            }
            catch (Exception)
            {

                throw;
            }
            return View(application);
        }

        // POST: TeamApplication/Delete/5
        [HttpPost]
        public ActionResult Delete(int id, FormCollection collection)
        {
            try
            {
                TeamApplication oldApplication = (TeamApplication)Session["deleteApplication"];
                TeamApplication newApplication = new TeamApplication();
                newApplication.ApplicationTime = oldApplication.ApplicationTime;
                newApplication.ApplicationStatus = oldApplication.ApplicationStatus;
                newApplication.TeamApplicationID = oldApplication.TeamApplicationID;
                newApplication.SkaterID = oldApplication.SkaterID;
                newApplication.TeamName = oldApplication.TeamName;
                newApplication.ApplicationStatus = "Rejected";
                teamApplicationManager.editTeamApplication(oldApplication, newApplication);

                return RedirectToAction("Index");
            }
            catch
            {
                return View();
            }
        }
    }
}
