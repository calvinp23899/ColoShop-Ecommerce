using ColoShopEcommerce.WebApp.Models;
using Microsoft.AspNet.Identity;
using Microsoft.AspNet.Identity.EntityFramework;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace ColoShopEcommerce.WebApp.Areas.Admin.Controllers
{
    public class RoleController : Controller
    {
        #region private property
        private ApplicationDbContext _dbContext = new ApplicationDbContext();
        #endregion

        // GET: Admin/Admin
        public ActionResult Index()
        {
            var items = _dbContext.Roles.ToList();
            return View(items);
        }

        #region Add method
        [HttpGet]
        public ActionResult Create()
        {
            return View();
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create(IdentityRole model)
        {
            if (ModelState.IsValid)
            {
                var roleManager = new RoleManager<IdentityRole>(new RoleStore<IdentityRole>(_dbContext));
                roleManager.Create(model);
                return RedirectToAction("Index");
            }
            return View(model);
        }
        #endregion

        #region Edit method
        [HttpGet]
        public ActionResult Edit(string id)
        {
            var item = _dbContext.Roles.Find(id);
            return View(item);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit(IdentityRole model)
        {
            if (ModelState.IsValid)
            {
                var roleManager = new RoleManager<IdentityRole>(new RoleStore<IdentityRole>(_dbContext));
                roleManager.Update(model);
                return RedirectToAction("Index");
            }
            return View(model);
        }
        #endregion

        #region Delete method
        [HttpPost]
        public ActionResult Delete(string id)
        {
            var item = _dbContext.Roles.Find(id);
            if (item != null)
            {
                var roleManager = new RoleManager<IdentityRole>(new RoleStore<IdentityRole>(_dbContext));
                roleManager.Delete(item);
                return Json(new { success = true });
            }
            return Json(new { success = false });
        }
        #endregion
    }
}