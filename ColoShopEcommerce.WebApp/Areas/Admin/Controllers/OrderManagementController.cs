using ColoShopEcommerce.WebApp.Models;
using ColoShopEcommerce.WebApp.Models.Common;
using ColoShopEcommerce.WebApp.Models.EF;
using ColoShopEcommerce.WebApp.Models.ReportModel;
using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace ColoShopEcommerce.WebApp.Areas.Admin.Controllers
{
    public class OrderManagementController : BaseController
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

        [HttpPost]
        public void PrintInvoice(int id)
        {
            var model = _dbContext.Orders.Where(x => x.Id == id).FirstOrDefault();
            ReportParams objReportParams = new ReportParams();
            var data = GetInvoiceInfo(id);
            objReportParams.DataSource = data.Tables[0];
            //Folder Reports
            objReportParams.ReportTitle = "Invoice Info Report"; // Any Title
            objReportParams.RptFileName = "OrderReport.rdlc"; //File name Rdlc
            objReportParams.ReportType = "OrderReport"; // Any Type name
            objReportParams.DataSetName = "dsOrderReport"; // FileName DataSet
            objReportParams.IsHasParams = true;
            //Set param value
            objReportParams.Code = model.Code;
            objReportParams.CreatedDate = model.CreatedDate.ToString("dd-MM-yyyy");
            objReportParams.TotalAmount = Common.FormatCurrency(model.TotalAmount,0) + "VND";
            this.HttpContext.Session["ReportParam"] = objReportParams;

        }

        public DataSet GetInvoiceInfo(int id)
        {
            string conStr = ConfigurationManager.ConnectionStrings["DefaultConnection"].ConnectionString;
            DataSet ds = new DataSet();
            string sql = string.Format(@"SELECT	P.Title,
                                                OD.Price,
                                                OD.Quantity,
                                                (OD.Price * OD.Quantity) AS Total
                                        FROM dbo.[Order] as O
                                        join dbo.OrderDetail as OD on O.Id = OD.OrderId
                                        join dbo.Product as P on OD.ProductId = p.Id
                                        where O.Id = {0}", id);
            SqlConnection con = new SqlConnection(conStr);
            SqlCommand cmd = new SqlCommand(sql, con);
            SqlDataAdapter dt = new SqlDataAdapter(cmd);
            dt.Fill(ds);
            return ds;
        }
    }
}