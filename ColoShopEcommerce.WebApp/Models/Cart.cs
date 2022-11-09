using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace ColoShopEcommerce.WebApp.Models
{
    public class Cart
    {
        public List<CartItem> CartItems {get; set;}
        public Cart()
        {
            this.CartItems = new List<CartItem>();
        }

        public void AddToCart(CartItem item,int Quantity)
        {
            var check = CartItems.FirstOrDefault(x => x.ProductId == item.ProductId);
            if(check != null)
            {
                check.Quantity += Quantity;
                check.TotalPrice = check.Price * check.Quantity;
            }
            else
            {
                CartItems.Add(item);
            }
        }

        public void Remove(int id)
        {
            var checkExist = CartItems.SingleOrDefault(x=>x.ProductId == id);
            if (checkExist != null)
            {
                CartItems.Remove(checkExist);
            }

        }

        public decimal UpdateQuantityCart(int id, int Quantity)
        {
            var checkExist = CartItems.SingleOrDefault(x => x.ProductId == id);
            if (checkExist != null)
            {
                checkExist.Quantity = Quantity;
                checkExist.TotalPrice = checkExist.Price * checkExist.Quantity;
            }
            return checkExist.TotalPrice;
        }

        public decimal GetTotalPrice()
        {
            return CartItems.Sum(x => x.TotalPrice);
        }

        public int GetTotalQuantity()
        {
            return CartItems.Sum(x => x.Quantity);
        }

        public void ClearCart()
        {
            CartItems.Clear();
        }
    }

    public class CartItem
    {
        public int ProductId { get; set; }
        public string ProductName { get; set; }
        public string Image { get; set; }
        public int Quantity { get; set; }
        public decimal Price { get; set; }
        public decimal TotalPrice { get; set; }
    }
}