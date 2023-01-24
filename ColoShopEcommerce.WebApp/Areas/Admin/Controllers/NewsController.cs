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
    public class NewsController : BaseController
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

        [HttpGet]
        public ActionResult Edit(int id)
        {
            var ID = _dbContext.News.Find(id);
            Session["imgPath"] = ID.Image;
            return View(ID);
        }

        [HttpPost]
        public ActionResult Edit(News news, HttpPostedFileBase imageNews)
        {
            
            if (ModelState.IsValid) 
            {
                if (imageNews != null)
                {
                    string path = Server.MapPath("~/UploadImageNews");
                    if (!Directory.Exists(path)) Directory.CreateDirectory(path); 
                    string imageName = imageNews.FileName;
                    imageNews.SaveAs(path + "\\" + imageName);
                    news.Image = imageName;

                    string oldImgPath = path + "\\" + Session["imgPath"].ToString();
                    if (System.IO.File.Exists(oldImgPath))
                    {
                        System.IO.File.Delete(oldImgPath);
                    }
                }
                else
                {
                    news.Image = Session["imgPath"].ToString();
                }               
                news.ModifiedDate = DateTime.Now;
                news.Alias = Models.Common.Filter.FilterChar(news.Title);
                _dbContext.Entry(news).State = System.Data.Entity.EntityState.Modified;
                _dbContext.SaveChanges();
                return RedirectToAction("Index");
            }
            return View(news);
        }
        [HttpPost]
        public ActionResult Delete(int id)
        {
            var item = _dbContext.News.Find(id);
            if (item != null)
            {
                var deleteItem = _dbContext.News.Attach(item);
                _dbContext.News.Remove(item);
                _dbContext.SaveChanges();
                return Json(new { success = true });
            }
            return Json(new { success = false });
        }
    }
}