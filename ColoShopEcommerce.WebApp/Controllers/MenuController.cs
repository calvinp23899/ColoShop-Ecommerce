using ColoShopEcommerce.WebApp.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace ColoShopEcommerce.WebApp.Controllers
{
    public class MenuController : Controller
    {
        private ApplicationDbContext _dbContext = new ApplicationDbContext();
        // GET: Menu
        public ActionResult Index()
        {
            return View();
        }
        public ActionResult MenuTop()
        {
            var items = _dbContext.Categories.OrderBy(x => x.Position).ToList();
            return PartialView("_MenuTop", items);
        }

        public ActionResult MenuProductCategory(int? id)
        {
            if(id != null)
            {
                ViewBag.CateId = id;
            }
            var items = _dbContext.ProductCategories.ToList();
            return PartialView("_MenuProductCategory", items);
        }

        public ActionResult MenuNewArrival()
        {
            var items = _dbContext.ProductCategories.ToList().Take(3);
            return PartialView("_MenuNewArrival", items);
        }
    }
}