using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using WebApplication1.App_Start;

namespace WebApplication1.Controllers
{
    public partial class MoreController : Controller
    {
        // GET: More
       [AppStartAuthorized ("VIP", "admin", "Super-Admin")]
        public virtual ActionResult Index()
        {
            return View();
        }
    }
}