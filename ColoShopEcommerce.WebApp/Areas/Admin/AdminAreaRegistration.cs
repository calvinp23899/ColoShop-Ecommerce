using System.Web.Mvc;

namespace ColoShopEcommerce.WebApp.Areas.Admin
{
    public class AdminAreaRegistration : AreaRegistration 
    {
        public override string AreaName 
        {
            get 
            {
                return "Admin";
            }
        }

        public override void RegisterArea(AreaRegistrationContext context) 
        {
            context.MapRoute(
                "Admin_default",
                "Admin/{controller}/{action}/{id}",
                new { action = "Index", id = UrlParameter.Optional },new[] { "ColoShopEcommerce.WebApp.Areas.Admin.Controllers" }
            );

            context.MapRoute(
                name: "PrintOrder",
                url: "in-hoa-don-bill",
                defaults: new { controller = "OrderManagement",action = "PrintInvoice", id = UrlParameter.Optional }, new[] { "ColoShopEcommerce.WebApp.Areas.Admin.Controllers" }
            );

        }
    }
}