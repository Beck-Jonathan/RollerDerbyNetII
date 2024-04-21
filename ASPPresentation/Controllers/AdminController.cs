using ASPPresentation.Models;
using Microsoft.AspNet.Identity;
using Microsoft.AspNet.Identity.Owin;
using System;
using System.Data;
using System.Linq;
using System.Net;
using System.Web;
using System.Web.Mvc;

namespace ASPPresentation.Controllers
{
    public class AdminController : Controller
    {
        //private ApplicationDbContext db = new ApplicationDbContext();
        private ApplicationUserManager userManager;
        // GET: Admin
        public ActionResult Index()
        {

            //return View(db.ApplicationUsers.ToList());
            userManager = HttpContext.GetOwinContext().GetUserManager<ApplicationUserManager>();
            return View(userManager.Users.OrderBy(n => n.FamilyName).ToList());
        }

        //GET: Admin/Details/5
        public ActionResult Details(string id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            //ApplicationUser applicationUser = db.ApplicationUsers.Find(id);
            userManager = HttpContext.GetOwinContext().GetUserManager<ApplicationUserManager>();
            ApplicationUser applicationUser = userManager.FindById(id);
            if (applicationUser == null)
            {
                return HttpNotFound();
            }
            var sktMgr = new LogicLayer.SkaterManager();
            var allRoles = sktMgr.RetreiveSkaterroles();
            var roles = userManager.GetRoles(id);
            var noRoles = allRoles.Except(roles);

            ViewBag.Roles = roles;
            ViewBag.noRoles = noRoles;
            //return View();
            return View(applicationUser);
        }

        public ActionResult RemoveRole(string id, string role)
        {
            var userManager = HttpContext.GetOwinContext().GetUserManager<ApplicationUserManager>();
            var user = userManager.Users.First(u => u.Id == id);
            if (role == "Administrator")
            {
                var adminUsers = userManager.Users.ToList()
                        .Where(u => userManager.IsInRole(u.Id, "Administrator"))
                        .ToList().Count();
                if (adminUsers < 2)
                {
                    ViewBag.Error = "Cannot remove last Administrator.";
                    return RedirectToAction("Details", "Admin", new { id = user.Id });
                }
            }
            userManager.RemoveFromRole(id, role);

            if (user.SkaterID != "")
            {
                try
                {
                    var usrMgr = new LogicLayer.Skater_Role_LineManager();
                    usrMgr.deleteSkater_Role_Line(role, user.SkaterID);
                }
                catch (Exception)
                {

                    //nothing to do
                }

            }





            return RedirectToAction("Details", "Admin", new { id = user.Id });
        }

        public ActionResult AddRole(string id, string role)
        {
            var userManager = HttpContext.GetOwinContext().GetUserManager<ApplicationUserManager>();
            var user = userManager.Users.First(u => u.Id == id);
            userManager.AddToRole(id, role);

            if (user.SkaterID != "")
            {
                try
                {
                    var usrmgr = new LogicLayer.Skater_Role_LineManager();
                    usrmgr.addSkater_Role_Line(role, user.SkaterID);
                }
                catch (Exception)
                {

                    //nothing to do
                }
            }
            return RedirectToAction("Details", "Admin", new { id = user.Id });
        }


    }



}
