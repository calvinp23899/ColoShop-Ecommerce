using ColoShopEcommerce.WebApp.Models;
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
    }
}