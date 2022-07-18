using ColoShopEcommerce.WebApp.Models;
using ColoShopEcommerce.WebApp.Models.EF;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using ColoShopEcommerce.WebApp.Models.Common;

namespace ColoShopEcommerce.WebApp.Areas.Admin.Controllers
{
    public class CategoryController : Controller
    {
        private ApplicationDbContext _dbContext = new ApplicationDbContext();
        // GET: Admin/Category
        public ActionResult Index()
        {
            var items = _dbContext.Categories.ToList();
            return View(items);
        }

        [HttpGet]
        public ActionResult Add()
        {
            return View();
        }
        [HttpPost]
        public ActionResult Add(string txtTitle, string txtDescription,int txtPosition,string txtSeoTitle,string txtSeoDescription,string txtSeoKeyword)
        {
            Category category = new Category();
            if (ModelState.IsValid)
            {              
                category.Title = txtTitle;
                category.Description = txtDescription;
                category.Position = txtPosition;
                category.SeoTitle = txtSeoTitle;
                category.SeoDescription = txtSeoDescription;
                category.SeoKeywords = txtSeoKeyword;
                category.Alias = Models.Common.Filter.FilterChar(txtTitle);
                category.CreatedDate = DateTime.Now;
                category.ModifiedDate = DateTime.Now;
                _dbContext.Categories.Add(category);
                _dbContext.SaveChanges();
                return RedirectToAction("Index");
            }                    
            return View(category);
        }

        [HttpGet]
        public ActionResult Edit(int Id)
        {
            var _id = _dbContext.Categories.Find(Id);
           return View(_id);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit(Category category)
        {
            if (ModelState.IsValid)
            {
                _dbContext.Categories.Attach(category);
                category.CreatedDate = DateTime.Now;
                category.ModifiedDate = DateTime.Now;
                category.Alias = Models.Common.Filter.FilterChar(category.Title);
                _dbContext.Entry(category).Property(x=>x.Title).IsModified = true;
                _dbContext.Entry(category).Property(x=>x.Description).IsModified = true;
                _dbContext.Entry(category).Property(x=>x.Alias).IsModified = true;
                _dbContext.Entry(category).Property(x=>x.SeoDescription).IsModified = true;
                _dbContext.Entry(category).Property(x=>x.SeoKeywords).IsModified = true;
                _dbContext.Entry(category).Property(x=>x.SeoTitle).IsModified = true;
                _dbContext.Entry(category).Property(x=>x.Position).IsModified = true;
                _dbContext.Entry(category).Property(x=>x.ModifiedDate).IsModified = true;
                _dbContext.Entry(category).Property(x=>x.ModifiedBy).IsModified = true;
                _dbContext.SaveChanges();
                return RedirectToAction("Index");
            }
            return View(category);
        }

    }
}