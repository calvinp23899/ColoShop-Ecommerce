using ColoShopEcommerce.WebApp.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace ColoShopEcommerce.WebApp.Controllers
{
    public class ProductController : Controller
    {
        private ApplicationDbContext _dbContext = new ApplicationDbContext();
        // GET: CategoryProduct
        public ActionResult Index(int? id)
        {
            var items = _dbContext.Products.ToList();
            if(id != null)
            {
                items = items.Where(x=>x.ProductCategoryID == id).ToList();
            }
            return View(items);
        }

        public ActionResult ProductDetail(int? id)
        {
            var item = _dbContext.Products.Find(id);
            return View(item);
        }
        public ActionResult ProductCategory(string alias,int? id)
        {
            var items = _dbContext.Products.ToList();
            if (id != null)
            {
                items = items.Where(x => x.ProductCategoryID == id).ToList();
            }
            ViewBag.CateId = id;
            return View(items);
        }

        public ActionResult ProductByCateId(int cateId)
        {
            var items = _dbContext.Products.Where(x=>x.ProductCategoryID == cateId).ToList().Take(6);
            return PartialView("_ProductByCateId", items);
        }

        public ActionResult ProductByCateNewArrival()
        {
            var items = _dbContext.Products.Where(x=>x.IsActive).Take(12).ToList();
            return PartialView("_ProductByCateNewArrival", items);
        }

        public ActionResult HomeBestSeller()
        {
            var items = _dbContext.Products.Where(x=>x.IsActive && x.IsHot).Take(12).ToList();
            return PartialView("_HomeBestSeller", items);
        }


    }
}