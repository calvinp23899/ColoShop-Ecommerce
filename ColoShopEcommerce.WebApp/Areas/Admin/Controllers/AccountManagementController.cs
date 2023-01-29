using ColoShopEcommerce.WebApp.Constant;
using ColoShopEcommerce.WebApp.Models;
using ColoShopEcommerce.WebApp.Models.ViewModel;
using Microsoft.AspNet.Identity;
using Microsoft.AspNet.Identity.EntityFramework;
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
    public class AccountManagementController : Controller
    {
        private ApplicationDbContext _dbContext = new ApplicationDbContext();
        private readonly UserManager<ApplicationUser> _userManager;
        private readonly RoleManager<IdentityRole> _roleManager;



        // GET: Admin/Account
        public ActionResult Index()
        {
            var usersWithRoles = (from user in _dbContext.Users
                                  select new
                                  {
                                      UserId = user.Id,
                                      Username = user.UserName,
                                      Email = user.Email,
                                      RoleNames = (from userRole in user.Roles
                                                   join role in _dbContext.Roles on userRole.RoleId
                                                   equals role.Id
                                                   select role.Name).ToList()
                                  }).ToList().Select(p => new UserRolesViewModel()
                                  {
                                      UserId = p.UserId,
                                      Username = p.Username,
                                      Email = p.Email,
                                      Role = string.Join(", ", p.RoleNames)
                                  });
            return View(usersWithRoles);
        }

        //Get Method
        [HttpGet]
        public ActionResult Manage(string userId)
        {
            ViewBag.userId = userId;
            var user = _dbContext.Users.Find(userId);
            if (user == null)
            {
                ViewBag.ErrorMessage = $"User with Id = {userId} cannot be found";
                return View("NotFound");
            }
            ViewBag.UserName = user.UserName;
            //var model = new List<ManageUserRolesVM>();
            //foreach (var role in _roleManager.Roles)
            //{
            //    var userRolesViewModel = new ManageUserRolesVM
            //    {
            //        RoleId = role.Id,
            //        RoleName = role.Name
            //    };
            //    var checkRole = user.Roles;
            //    if (checkRole == "Admin")
            //    {
            //        userRolesViewModel.Selected = true;
            //    }
            //    else
            //    {
            //        userRolesViewModel.Selected = false;
            //    }
            //    model.Add(userRolesViewModel);
            //}
            return View(user);
        }

        [HttpPost]
        public ActionResult Manage(List<ManageUserRolesVM> model, string userId)
        {

            return RedirectToAction("Index");
        }




    }
}