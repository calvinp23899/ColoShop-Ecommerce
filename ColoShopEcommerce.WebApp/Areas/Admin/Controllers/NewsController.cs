using ColoShopEcommerce.WebApp.Models;
using ColoShopEcommerce.WebApp.Models.EF;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace ColoShopEcommerce.WebApp.Areas.Admin.Controllers
{
    public class NewsController : Controller
    {
        private ApplicationDbContext _dbContext = new ApplicationDbContext();
        // GET: Admin/News
        public ActionResult Index()
        {
            var items = _dbContext.News.OrderByDescending(x=>x.CreatedDate).ToList();
            return View(items);
        }

        [HttpGet]
        public ActionResult Add()
        {
            return View();
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Add(News news, HttpPostedFileBase imageNews)
        {
            if (ModelState.IsValid)
            {
                if(imageNews != null)
                {
                    string path = Server.MapPath("~/UploadImageNews");
                    if (!Directory.Exists(path)) Directory.CreateDirectory(path);
                    string imagesName = imageNews.FileName;
                    imageNews.SaveAs(path + "\\" + imagesName);
                    news.Image = imagesName;
                }
                news.CreatedDate = DateTime.Now;
                news.ModifiedDate = DateTime.Now;
                news.CategoryId = 3;
                news.Alias = Models.Common.Filter.FilterChar(news.Title);
                _dbContext.News.Add(news);
                _dbContext.SaveChanges();
                return RedirectToAction("Index");
            }
            return View(news);
        }
    }
}