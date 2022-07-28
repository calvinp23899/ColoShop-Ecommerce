using ColoShopEcommerce.WebApp.Models;
using ColoShopEcommerce.WebApp.Models.Common;
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
            var code = new{Success = false};
            Cart cart = (Cart)Session["Cart"];
            if(cart != null)
            {
                return View(cart.CartItems);
            }
            return View(code);
        }

        public ActionResult ShowCount()
        {
            Cart cart = (Cart)Session["Cart"];
            if(cart != null)
            {
                return Json(new {Count = cart.CartItems.Count}, JsonRequestBehavior.AllowGet);
            }
            return Json(new { Count = 0 }, JsonRequestBehavior.AllowGet);
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
        [HttpPost]
        public ActionResult UpdateCartItem(int id, int quantityUpdate)
        {
            var code = new { Success = false, newTotalPrice = 0, subTotalPrice = 0 };
            Cart cart = (Cart)Session["Cart"];
            if(cart != null)
            {
                var totalPrice = cart.UpdateQuantityCart(id, quantityUpdate);
                var _subtotalPrice = cart.GetTotalPrice();
                return Json(new { Success = true, newTotalPrice = Common.FormatCurrency(totalPrice,0) + "VND", subTotalPrice =Common.FormatCurrency(_subtotalPrice,0) + "VND"  });
            }
            return Json(code);
        }

        [HttpPost]
        public ActionResult DeleteCartItem(int id)
        {
            var code = new {Success = false, msg ="", code = -1, Count = 0};
            Cart cart = (Cart)Session["Cart"];
            if(cart != null)
            {
                var checkProduct = cart.CartItems.SingleOrDefault(x => x.ProductId == id);
                if (checkProduct != null)
                {
                    cart.Remove(id);
                    code = new { Success = true, msg = "", code = 1, Count = cart.CartItems.Count };
                }
            }
            return Json(code);
        }
    }
}