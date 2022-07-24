using ColoShopEcommerce.WebApp.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace ColoShopEcommerce.WebApp.Controllers
{
    public class ShoppingCartController : Controller
    {
        private ApplicationDbContext _dbContext = new ApplicationDbContext();
        // GET: ShoppingCart
        public ActionResult Index()
        {
            return View();
        }

        [HttpPost]
        public ActionResult AddToCart(int id, int quantity)
        {
            var code = new { Success = false,msg = "", code = -1, Count = 0 };
            var checkProduct = _dbContext.Products.FirstOrDefault(p => p.Id == id);
            if (checkProduct != null)
            {
                Cart cart = (Cart)Session["Cart"];
                if(cart == null)
                {
                    cart = new Cart();
                }
                CartItem item = new CartItem
                {
                    ProductId = checkProduct.Id,
                    ProductName = checkProduct.Title,
                    Image = checkProduct.Image,
                    Quantity = quantity

                };
                if (checkProduct.PriceSale > 0)
                {
                    item.Price = (decimal)checkProduct.PriceSale;
                }
                else
                {
                    item.Price = checkProduct.Price;
                }
                item.TotalPrice = item.Quantity * item.Price;
                cart.AddToCart(item, quantity);
                Session["Cart"] = cart;
                code = new { Success = true, msg = "Thêm Sản Phẩm Thành Công", code = 1 , Count = cart.CartItems.Count};
            }
            return Json(code);
        }
    }
}