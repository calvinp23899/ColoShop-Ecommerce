using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using System.Web.Routing;

namespace ColoShopEcommerce.WebApp
{
    public class RouteConfig
    {
        public static void RegisterRoutes(RouteCollection routes)
        {
            routes.IgnoreRoute("{resource}.axd/{*pathInfo}");
            routes.MapRoute(
                name: "CategoryProduct",
                url: "danh-muc-san-pham/{alias}-{id}",
                defaults: new { controller = "Product", action = "ProductCategory", id = UrlParameter.Optional }
            );
            routes.MapRoute(
                name: "ShoppingCart",
                url: "gio-hang",
                defaults: new { controller = "ShoppingCart", action = "Index", alias = UrlParameter.Optional }
            );
            routes.MapRoute(
                name: "CheckOut",
                url: "thanh-toan",
                defaults: new { controller = "CheckOut", action = "Index", alias = UrlParameter.Optional }
            );
            routes.MapRoute(
                name: "DetailProduct",
                url: "chi-tiet/{alias}-{id}",
                defaults: new { controller = "Product", action = "ProductDetail", alias = UrlParameter.Optional }
            );
            routes.MapRoute(
                name: "Product",
                url: "san-pham",
                defaults: new { controller = "Product", action = "Index", alias = UrlParameter.Optional }
            );
            routes.MapRoute(
                name: "Home",
                url: "trang-chu",
                defaults: new { controller = "Home", action = "Index", alias = UrlParameter.Optional }
            );
            routes.MapRoute(
                name: "Register",
                url: "dang-ky-tai-khoan",
                defaults: new { controller = "Account", action = "Register", alias = UrlParameter.Optional }
            );
            routes.MapRoute(
                name: "Login",
                url: "dang-nhap-tai-khoan",
                defaults: new { controller = "Account", action = "Login", alias = UrlParameter.Optional }
            );
            routes.MapRoute(
                name: "Default",
                url: "{controller}/{action}/{id}",
                defaults: new { controller = "Home", action = "Index", id = UrlParameter.Optional },new[] { "ColoShopEcommerce.WebApp.Controllers" }
            );
        }
    }
}
