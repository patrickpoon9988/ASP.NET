using Microsoft.AspNet.Identity;
using Microsoft.AspNet.Identity.Owin;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.Web;
using System.Web.Mvc;
using WebApplication1.Models;
using WebApplication1;
using Microsoft.Owin.Security;


namespace WebApplication1.Controllers
{
    public partial class AccountController : Controller
    {
        private ApplicationSignInManager _signInManager;
        private ApplicationUserManager _userManager;
        private ApplicationRoleManager _roleManager;

        public AccountController()
        {
        }

        public AccountController(ApplicationUserManager userManager, ApplicationSignInManager signInManager, ApplicationRoleManager roleManager)
        {
            UserManager = userManager;
            SignInManager = signInManager;
            RoleManager = roleManager;
        }

        public ApplicationSignInManager SignInManager
        {
            get
            {
                return _signInManager ?? HttpContext.GetOwinContext().Get<ApplicationSignInManager>();
            }
            private set
            {
                _signInManager = value;
            }
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

        public ApplicationRoleManager RoleManager
        {
            get
            {
                return _roleManager ?? HttpContext.GetOwinContext().Get<ApplicationRoleManager>();
            }
            private set
            {
                _roleManager = value;
            }
        }

        [AllowAnonymous]
        public virtual ActionResult Register()
        {
            var roleList = RoleManager.Roles.Select(x => x.Name).ToList();
            ViewBag.roleList = roleList;
            return View();
        }

        //
        // POST: /Account/Register
        [HttpPost]
        [AllowAnonymous]
        [ValidateAntiForgeryToken]
        public virtual async Task<ActionResult> Register(RegisterViewModel model)
        {
            if (ModelState.IsValid)
            {
                var user = new ApplicationUser { UserName = model.Email, Email = model.Email };
                var result = await UserManager.CreateAsync(user, model.Password);
                if (result.Succeeded)
                {
                    await SignInManager.SignInAsync(user, isPersistent: false, rememberBrowser: false);
                    //await RoleManager.CreateAsync(new Microsoft.AspNet.Identity.EntityFramework.IdentityRole(model.roles));
                    UserManager.AddToRole(user.Id, model.SelectOption);

                    Session["UserName"] = user.UserName;

                    return RedirectToAction("Index", "Home");
                }
                AddErrors(result);
            }
            return View();
        }
        private void AddErrors(IdentityResult result)
        {
            foreach (var error in result.Errors)
            {
                ModelState.AddModelError("", error);
            }
        }

        public virtual ActionResult LogOff()
        {
            AuthenticationManager.SignOut(DefaultAuthenticationTypes.ApplicationCookie, DefaultAuthenticationTypes.ExternalCookie);
            Session.Abandon();
            return RedirectToAction("Index", "Home");
        }

        private IAuthenticationManager AuthenticationManager
        {
            get { return HttpContext.GetOwinContext().Authentication; }
        }

        public virtual ActionResult PasswordReset()
        {
            return View();
        }

        [HttpPost]
        public virtual async Task<ActionResult> PasswordReset(PasswordResetModel reset)
        {
            if (ModelState.IsValid)
            {
                var userName = reset.Account;
                var userID = UserManager.Users.Where(x => x.UserName == userName).First().Id;
                if (userName != null)
                {
                    string resetToken = await UserManager.GeneratePasswordResetTokenAsync(userID);
                    var result = await UserManager.ResetPasswordAsync(userID, resetToken, reset.Password);
                    return RedirectToAction("index", "home");
                }
                return View();
            }
            return View();
        }
    }
}