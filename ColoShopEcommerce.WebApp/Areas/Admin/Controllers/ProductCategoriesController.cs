using ColoShopEcommerce.WebApp.Models;
using ColoShopEcommerce.WebApp.Models.EF;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace ColoShopEcommerce.WebApp.Areas.Admin.Controllers
{
    public class ProductCategoriesController : Controller
    {
        private ApplicationDbContext _dbContext = new ApplicationDbContext();
        // GET: Admin/ProductCategory
        public ActionResult Index()
        {
            var items = _dbContext.ProductCategories.ToList();
            return View(items);
        }

        [HttpGet]
        public ActionResult Add()
        {
            return View();
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Add(ProductCategory productcategory)
        {
            if (ModelState.IsValid)
            {
                productcategory.CreatedDate = DateTime.Now;
                productcategory.ModifiedDate = DateTime.Now;
                productcategory.Alias = Models.Common.Filter.FilterChar(productcategory.Title);
                _dbContext.ProductCategories.Add(productcategory);
                _dbContext.SaveChanges();
                return RedirectToAction("Index");
            }
            return View(productcategory);
        }

        [HttpGet]
        public ActionResult Edit(int id)
        {
            var item = _dbContext.ProductCategories.Find(id);
            return View(item);
        }

        [HttpPost]
        public ActionResult Edit(ProductCategory model)
        {
            if (ModelState.IsValid)
            {
                _dbContext.ProductCategories.Attach(model);
                model.ModifiedDate = DateTime.Now;
                model.Alias = Models.Common.Filter.FilterChar(model.Title);
                _dbContext.Entry(model).State = System.Data.Entity.EntityState.Modified;
                _dbContext.SaveChanges();
                return RedirectToAction("Index");
            }
            return View(model);
        }

        [HttpPost]
        public ActionResult Delete(int id)
        {
            var item = _dbContext.ProductCategories.Find(id);
            if (item != null)
            {
                var deleteItem = _dbContext.ProductCategories.Attach(item);
                _dbContext.ProductCategories.Remove(item);
                _dbContext.SaveChanges();
                return Json(new { success = true });
            }
            return Json(new { success = false });
        }
    }
}