using ColoShopEcommerce.WebApp.Constant;
using ColoShopEcommerce.WebApp.Models;
using Microsoft.AspNet.Identity;
using Microsoft.AspNet.Identity.Owin;
using Microsoft.Owin.Security;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.Web;
using System.Web.Mvc;

namespace ColoShopEcommerce.WebApp.Areas.Admin.Controllers
{
    public class LoginAdminController : Controller
    {

        #region private property
        private ApplicationDbContext _dbContext = new ApplicationDbContext();
        private ApplicationSignInManager _signInManager;
        private ApplicationUserManager _userManager;
        #endregion
        // GET: Admin/LoginAdmin
        public ActionResult Index()
        {
            return View();
        }

        public LoginAdminController()
        {
        }

        public LoginAdminController(ApplicationDbContext dbContext, ApplicationSignInManager signInManager, ApplicationUserManager userManager)
        {
            _dbContext = dbContext;
            _signInManager = signInManager;
            _userManager = userManager;
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

        #region Login
        [HttpPost]
        public async Task<ActionResult> Login(string emailAdmin, string passAdmin, string returnUrl)
        {
            if (ModelState.IsValid)
            {
                var user = await UserManager.FindAsync(emailAdmin, passAdmin);
                bool checkRole = await UserManager.IsInRoleAsync(user.Id, "Admin");
                if (user != null)
                {
                    if (checkRole)
                    {
                        await SignInManager.SignInAsync(user, isPersistent: false, rememberBrowser: false);
                        var userSession = new UserLogin();
                        userSession.Email = user.Email;
                        userSession.ID = user.Id;
                        Session.Add(CommonConstant.USER_SESSION, userSession);
                        return RedirectToLoginAdmin(returnUrl);
                    }
                    else
                    {
                        ModelState.AddModelError("", "Email is wrong.");
                    }
                }
                else
                {
                    ModelState.AddModelError("", "Invalid password.");
                }

            }
            return RedirectToAction("LoginAdmin", "Index");

        }

        #endregion

        public ActionResult Logout()
        {
            Session.Clear();
            AuthenticationManager.SignOut(DefaultAuthenticationTypes.ApplicationCookie);
            return RedirectToAction("Index", "LoginAdmin");
        }

        #region private methods
        private IAuthenticationManager AuthenticationManager
        {
            get
            {
                return HttpContext.GetOwinContext().Authentication;
            }
        }
        private ActionResult RedirectToLoginAdmin(string returnUrl)
        {
            if (Url.IsLocalUrl(returnUrl))
            {
                return Redirect(returnUrl);
            }
            return RedirectToAction("Index", "Dashboard");
        }
        #endregion
    }
}