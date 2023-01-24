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
    public class ProductsController : BaseController
    {
        private ApplicationDbContext _dbContext = new ApplicationDbContext();
        // GET: Admin/Products
        public ActionResult Index()
        {
            var items = _dbContext.Products.ToList(); 
            return View(items);
        }

        [HttpGet]
        public ActionResult Add()
        {
            ViewBag.ProductCategory = new SelectList(_dbContext.ProductCategories.ToList(), "Id", "Title");
            return View();
        }
        [HttpPost]
        public ActionResult Add(Product model, HttpPostedFileBase imageProducts)
        {
            if (ModelState.IsValid)
            {
                if(imageProducts != null)
                {
                    string path = Server.MapPath("~/UploadImageProducts");
                    if (!Directory.Exists(path)) Directory.CreateDirectory(path);
                    string imagesName = imageProducts.FileName;
                    imageProducts.SaveAs(path + "\\" + imagesName);
                    model.Image = imagesName;
                }
                model.Alias = Models.Common.Filter.FilterChar(model.Title);
                //model.ProductCategoryID = 1;
                model.CreatedDate = DateTime.Now;
                model.ModifiedDate = DateTime.Now;
                _dbContext.Products.Add(model);
                _dbContext.SaveChanges();
                return RedirectToAction("Index");
            }
            ViewBag.ProductCategory = new SelectList(_dbContext.ProductCategories.ToList(),"Id","Title");
            return View(model);
        }

        [HttpGet]
        public ActionResult Edit(int id)
        {
            var item = _dbContext.Products.Find(id);
            ViewBag.ProductCategory = new SelectList(_dbContext.ProductCategories.ToList(), "Id", "Title");
            Session["imgPath"] = item.Image;
            return View(item);
        }

        [HttpPost]
        public ActionResult Edit(Product model, HttpPostedFileBase imageProducts, IEnumerable<HttpPostedFileBase> imageProductsFile)
        {
            if (ModelState.IsValid)
            {
                if (imageProducts != null)
                {
                    string path = Server.MapPath("~/UploadImageProducts");
                    if (!Directory.Exists(path)) Directory.CreateDirectory(path);
                    string imageName = imageProducts.FileName;
                    imageProducts.SaveAs(path + "\\" + imageName);
                    model.Image = imageName;

                    string oldImgPath = path + "\\" + Session["imgPath"].ToString();
                    if (System.IO.File.Exists(oldImgPath))
                    {
                        System.IO.File.Delete(oldImgPath);
                    }
                }
                else
                {
                    model.Image = Session["imgPath"].ToString();
                }            
                string pathFile = Server.MapPath("~/UploadProductFile");
                var items = _dbContext.FILES.Where(b => b.ProductId == model.Id).Count();
                if (items <= 0)
                {                   
                    if (!Directory.Exists(pathFile)) Directory.CreateDirectory(pathFile);
                    foreach (var file in imageProductsFile)
                    {
                        if (file != null && file.ContentLength > 0)
                        {
                            var fileName = Path.GetFileName(file.FileName);
                            var pathCombine = Path.Combine(Server.MapPath("~/UploadProductFile"), fileName);
                            file.SaveAs(pathCombine);

                            FILE f = new FILE();
                            f.FileName = fileName;
                            f.Path = "\\UploadProductFile\\" + fileName;
                            f.ProductId = model.Id;

                            _dbContext.FILES.Add(f);
                            _dbContext.SaveChanges();
                        }
                    }
                }
                else
                {
                    var _item = _dbContext.FILES.Where(b => b.ProductId == model.Id).AsEnumerable();
                    foreach (var file in imageProductsFile)
                    {
                        if (file != null && file.ContentLength > 0)
                        {
                            foreach (var detail in _item)
                            {
                                _dbContext.FILES.Remove(detail);
                                if (System.IO.File.Exists(pathFile + "\\" + detail.FileName))
                                {
                                    System.IO.File.Delete(pathFile + "\\" + detail.FileName);
                                }
                            }
                        }
                    }
                    foreach (var file in imageProductsFile)
                    {
                        if (file != null && file.ContentLength > 0)
                        {
                            var fileName = Path.GetFileName(file.FileName);
                            var pathCombine = Path.Combine(Server.MapPath("~/UploadProductFile"), fileName);
                            file.SaveAs(pathCombine);

                            FILE f = new FILE();
                            f.FileName = fileName;
                            f.Path = "\\UploadProductFile\\" + fileName;
                            f.ProductId = model.Id;

                            _dbContext.FILES.Add(f);
                            _dbContext.SaveChanges();
                        }
                    }
                }

                model.ModifiedDate= DateTime.Now;
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
            var item = _dbContext.Products.Find(id);
            if (item != null)
            {
                var deleteItem = _dbContext.Products.Attach(item);
                _dbContext.Products.Remove(item);
                _dbContext.SaveChanges();
                return Json(new { success = true });
            }
            return Json(new { success = false });
        }
    }
}