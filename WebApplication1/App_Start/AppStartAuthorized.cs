using Microsoft.AspNet.Identity.EntityFramework;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using System.Web.Routing;
using WebApplication1.Models;

namespace WebApplication1.App_Start
{
    public class AppStartAuthorized : AuthorizeAttribute
    {
        private readonly string[] allowedroles;

        public AppStartAuthorized(params string[] roles)
        {
            this.allowedroles = roles;
        }
        protected override bool AuthorizeCore(HttpContextBase httpContext)
        {
            bool authorize = false;
            var userName = Convert.ToString(httpContext.Session["UserName"]);
            if (!string.IsNullOrEmpty(userName))
                using (var context = new ApplicationDbContext())
                {
                    //var userRole = (from u in context.Users
                    //                join r in context.Roles on u.RoleId equals r.Id
                    //                where u.UserId == userId
                    //                select new
                    //                {
                    //                    r.Name
                    //                }).FirstOrDefault();
                    ApplicationUser au = context.Users.First(u => u.UserName == userName);
                    foreach (IdentityUserRole role in au.Roles)
                    {
                        string name = role.RoleId;
                        string RoleName = context.Roles.First(r => r.Id == name).Name;
                        foreach (var allowedRole in allowedroles)
                        {
                            if (String.Compare(allowedRole, RoleName)==0) return true;
                        }
                    }
                }

            return authorize;
        }

        protected override void HandleUnauthorizedRequest(AuthorizationContext filterContext)
        {
            filterContext.Result = new RedirectToRouteResult(
               new RouteValueDictionary
               {
                    { "controller", "Home" },
                    { "action", "unauthorized" },
               });
        }

    }
}