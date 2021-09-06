using Microsoft.AspNet.Identity;
using Microsoft.AspNet.Identity.EntityFramework;
using Microsoft.AspNet.Identity.Owin;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.Web;
using System.Web.Mvc;
using WebApplication1.Models;

namespace WebApplication1.Controllers
{
    /// <summary>
    /// Controller for manipulating roles and role assigment
    /// </summary>
    public class RolesController : Controller
    {

        private ApplicationDbContext _context;
        private ApplicationUserManager _userManager;

        public ApplicationDbContext ApplicationDbContext
        {
            get
            {
                return _context ?? HttpContext.GetOwinContext().Get<ApplicationDbContext>();
            }
            private set { _context = value; }
        }

        public ApplicationUserManager UserManager
        {
            get
            {
                return _userManager ?? HttpContext.GetOwinContext().GetUserManager<ApplicationUserManager>();
            }
            private set
            {
                _userManager = value;
            }
        }

        /// <summary>
        /// Action for Index view
        /// </summary>
        /// <returns></returns>
        public ActionResult Index()
        {
            var roles = ApplicationDbContext.Roles.ToList();
            return View(roles);
        }

        //
        // POST: /Roles/Create
        /// <summary>
        /// Action for create view
        /// </summary>
        /// <param name="collection">The collection.</param>
        /// <returns></returns>
        [HttpPost]
        public ActionResult Create(FormCollection collection)
        {
            try
            {
                ApplicationDbContext.Roles.Add(new IdentityRole()
                {
                    Name = collection["RoleName"]
                });
                ApplicationDbContext.SaveChanges();
                ViewBag.ResultMessage = "Role created successfully !";
                return RedirectToAction("Index");
            }
            catch (Exception ex)
            {
                return View();
            }
        }

        [HttpGet]
        public ActionResult Create()
        {
            return View();
        }

        /// <summary>
        /// Action for delete view.
        /// </summary>
        /// <param name="roleName">Name of the role.</param>
        /// <returns></returns>
        public ActionResult Delete(string roleName)
        {
            var thisRole = ApplicationDbContext.Roles.FirstOrDefault(r => r.Name.Equals(roleName, StringComparison.CurrentCultureIgnoreCase));
            ApplicationDbContext.Roles.Remove(thisRole);
            ApplicationDbContext.SaveChanges();
            return RedirectToAction("Index");
        }

        /// <summary>
        /// Action for Edit View.
        /// </summary>
        /// <param name="roleName">Name of the role.</param>
        /// <returns></returns>
        public ActionResult Edit(string roleName)
        {
            var thisRole = ApplicationDbContext.Roles.FirstOrDefault(r => r.Name.Equals(roleName, StringComparison.CurrentCultureIgnoreCase));

            return View(thisRole);
        }

        //
        // POST: /Roles/Edit/5
        /// <summary>
        /// Post Action for edit view.
        /// </summary>
        /// <param name="role">The role.</param>
        /// <returns></returns>
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit(IdentityRole role)
        {
            try
            {
                ApplicationDbContext.Entry(role).State = System.Data.Entity.EntityState.Modified;
                ApplicationDbContext.SaveChanges();

                return RedirectToAction("Index");
            }
            catch
            {
                return View();
            }
        }

        /// <summary>
        /// Action for ManageUserRoles view.
        /// </summary>
        /// <returns></returns>
        public ActionResult ManageUserRoles()
        {
            // prepopulat roles for the view dropdown
            var list = ApplicationDbContext.Roles.OrderBy(r => r.Name).ToList().Select(rr => new SelectListItem { Value = rr.Name.ToString(), Text = rr.Name }).ToList();
            ViewBag.Roles = list;
            return View();
        }

        /// <summary>
        /// Action for posting role to add to user.
        /// </summary>
        /// <param name="UserName">Name of the user.</param>
        /// <param name="RoleName">Name of the role.</param>
        /// <returns></returns>
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult RoleAddToUser(string UserName, string RoleName)
        {
            var user = ApplicationDbContext.Users.FirstOrDefault(u => u.UserName.Equals(UserName, StringComparison.CurrentCultureIgnoreCase));
            UserManager.AddToRole(user.Id, RoleName);
            ViewBag.ResultMessage = "Role created successfully !";
            // prepopulat roles for the view dropdown
            var list = ApplicationDbContext.Roles.OrderBy(r => r.Name).ToList().Select(rr => new SelectListItem { Value = rr.Name.ToString(), Text = rr.Name }).ToList();
            ViewBag.Roles = list;

            return View("ManageUserRoles");
        }

        /// <summary>
        /// Action Gets the roles for the user.
        /// </summary>
        /// <param name="UserName">Name of the user.</param>
        /// <returns></returns>
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult GetRoles(string UserName)
        {
            if (!string.IsNullOrWhiteSpace(UserName))
            {
                var user = ApplicationDbContext.Users.FirstOrDefault(u => u.UserName.Equals(UserName, StringComparison.CurrentCultureIgnoreCase));
                ViewBag.RolesForThisUser = UserManager.GetRoles(user.Id);

                // prepopulat roles for the view dropdown
                var list = ApplicationDbContext.Roles.OrderBy(r => r.Name).ToList().Select(rr => new SelectListItem { Value = rr.Name.ToString(), Text = rr.Name }).ToList();
                ViewBag.Roles = list;
            }

            return View("ManageUserRoles");
        }

        /// <summary>
        /// Action Deletes the role for user.
        /// </summary>
        /// <param name="UserName">Name of the user.</param>
        /// <param name="RoleName">Name of the role.</param>
        /// <returns></returns>
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteRoleForUser(string UserName, string RoleName)
        {
            var user = ApplicationDbContext.Users.FirstOrDefault(u => u.UserName.Equals(UserName, StringComparison.CurrentCultureIgnoreCase));

            if (UserManager.IsInRole(user.Id, RoleName))
            {
                UserManager.RemoveFromRole(user.Id, RoleName);
                ViewBag.ResultMessage = "Role removed from this user successfully !";
            }
            else
            {
                ViewBag.ResultMessage = "This user doesn't belong to selected role.";
            }
            // prepopulat roles for the view dropdown
            var list = ApplicationDbContext.Roles.OrderBy(r => r.Name).ToList().Select(rr => new SelectListItem { Value = rr.Name.ToString(), Text = rr.Name }).ToList();
            ViewBag.Roles = list;

            return View("ManageUserRoles");
        }
    }
}