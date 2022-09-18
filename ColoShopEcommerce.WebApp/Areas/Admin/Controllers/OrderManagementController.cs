using ColoShopEcommerce.WebApp.Models;
using ColoShopEcommerce.WebApp.Models.EF;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace ColoShopEcommerce.WebApp.Areas.Admin.Controllers
{
    public class OrderManagementController : Controller
    {
        #region private property
        private ApplicationDbContext _dbContext = new ApplicationDbContext();
        #endregion

        // GET: Admin/OrderManagement
        public ActionResult Index()
        {
            var items = _dbContext.Orders.OrderByDescending(x=>x.CreatedDate).ToList();
            return View(items);
        }

        public ActionResult ViewDetail(int id)
        {
            var model = _dbContext.Orders.Where(x=>x.Id == id).FirstOrDefault();
            return View(model);
        }

        public ActionResult Partial_Product(int id)
        {
            var model = _dbContext.OrderDetails.Where(x=>x.OrderId == id).ToList();
            return PartialView(model);
        }

        [HttpGet]
        public ActionResult UpdateOrder(int id)
        {
            var model = _dbContext.Orders.Where(x => x.Id == id).FirstOrDefault();
            return View(model);
        }
        [HttpPost]
        public ActionResult UpdateOrder(Order model)
        {
            if (ModelState.IsValid)
            {
                model.ModifiedDate = DateTime.Now;
                _dbContext.Entry(model).State = System.Data.Entity.EntityState.Modified;
                _dbContext.SaveChanges();
                return RedirectToAction("Index");
            }
            return View(model);
        }

        [HttpPost]
        public ActionResult Delete(int id)
        {
            var item = _dbContext.Orders.Find(id);
            if (item != null)
            {
                var deleteItem = _dbContext.Orders.Attach(item);
                _dbContext.Orders.Remove(item);
                _dbContext.SaveChanges();
                return Json(new { success = true });
            }
            return Json(new { success = false });
        }
    }
}