using ColoShopEcommerce.WebApp.Models;
using ColoShopEcommerce.WebApp.Models.Common;
using ColoShopEcommerce.WebApp.Models.EF;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace ColoShopEcommerce.WebApp.Controllers
{
    public class CheckOutController : Controller
    {
        private ApplicationDbContext _dbContext = new ApplicationDbContext();
        // GET: CheckOut
        public ActionResult Index()
        {
            Cart cart = (Cart)Session["Cart"];
            ViewBag.subTotalPrice = cart.GetTotalPrice();
            if (cart != null)
            {
                return View(cart.CartItems);
            }
            return View();
        }

        [HttpGet]
        public ActionResult Checkout()
        {
            return View();
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult CheckOut(Order order)
        {
            Random code = new Random();
            Cart cart = (Cart)Session["Cart"];
            if (ModelState.IsValid)
            {
                order.Code = "DH" + code.Next(0,9) + code.Next(0, 9) + code.Next(0, 9) + code.Next(0, 9);
                order.CreatedBy = order.CustomerName;
                order.CreatedDate = DateTime.Now;
                order.ModifiedDate = DateTime.Now;
                order.TotalAmount = cart.GetTotalPrice();
                order.Quantity = cart.GetTotalQuantity();
                order.IsDelivery = false;
                order.IsPaid = false;
                cart.CartItems.ForEach(x => order.OrderDetails.Add(new OrderDetail
                {
                    ProductId = x.ProductId,
                    Quantity = x.Quantity,
                    Price = x.Price,
                }));
                _dbContext.Orders.Add(order);
                _dbContext.SaveChanges();
                // Send Mail
                var strProduct = "";
                var price = decimal.Zero;
                var totapPrice = decimal.Zero;
                foreach(var item in cart.CartItems)
                {
                    strProduct += "<tr>";
                    strProduct += "<td>"+item.ProductName+"</td>";
                    strProduct += "<td>"+item.Quantity+"</td>";
                    strProduct += "<td>"+ Common.FormatCurrency(item.TotalPrice,0)+"</td>";
                    strProduct += "</tr>";
                    price += item.Price * item.Quantity;
                }
                totapPrice = price;
                string contentCustomer = System.IO.File.ReadAllText(Server.MapPath("~/Content/Templates/send2.html"));
                contentCustomer = contentCustomer.Replace("{{Bill}}", order.Code);
                contentCustomer = contentCustomer.Replace("{{Product}}", strProduct);
                contentCustomer = contentCustomer.Replace("{{CustomerName}}", order.CustomerName);
                contentCustomer = contentCustomer.Replace("{{Phone}}", order.Phone);
                contentCustomer = contentCustomer.Replace("{{Email}}", order.Email);
                contentCustomer = contentCustomer.Replace("{{Address}}", order.Email);
                contentCustomer = contentCustomer.Replace("{{Price}}", Common.FormatCurrency(price,0));
                contentCustomer = contentCustomer.Replace("{{TotalPrice}}", Common.FormatCurrency(totapPrice,0));
                Common.SendEmail("ShopOnline", "Don Hang #" + order.Code, contentCustomer.ToString(), order.Email);
                cart.ClearCart();   
                return View();
            }
            return View("Index");
            
        }
    }
}