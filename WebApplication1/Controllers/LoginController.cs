using Microsoft.AspNet.Identity.Owin;
using System.Linq;
using System.Threading.Tasks;
using System.Web;
using System.Web.Mvc;
using WebApplication1.Models;
using ClassLibrary1;

namespace WebApplication1.Controllers
{
    public partial class LoginController : Controller
    {
        private readonly PatrickDBEntities db = new PatrickDBEntities();
        public virtual ActionResult Login()
        {
            return View();
        }

        [HttpPost]
        public virtual ActionResult Login(LoginModel login)
        {
            if (!ModelState.IsValid) { return View(); }
            else
            {
                var check = db.tblUserAccounts.Where(u => u.username == login.username && u.password == login.password).FirstOrDefault();
                if (check != null)
                {
                    Session["username"] = login.username;
                    return RedirectToAction("programe", "home");
                }
                ViewBag.error = "Invalid Username or Password";
                return View();
            }
        }

        public virtual ActionResult index()
        {
            return View();
        }

        private ApplicationSignInManager _signInManager;
        private ApplicationUserManager _userManager;
        private ApplicationRoleManager _roleManager;

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

        [HttpPost]
        public virtual async Task<ActionResult> Index(ASPLoginModel login)
        {
            if (ModelState.IsValid)
            {
                var user = await UserManager.FindAsync(login.userName, login.password);
                if (user != null)
                {
                    await SignInManager.SignInAsync(user, isPersistent: false, rememberBrowser: false);
                    Session["UserName"] = login.userName;
                    return RedirectToAction("index", "home");
                }
                else
                    return View();
            }
            else
                return View();
        }
    }
}