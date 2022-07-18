using Microsoft.Owin;
using Owin;

[assembly: OwinStartupAttribute(typeof(ColoShopEcommerce.WebApp.Startup))]
namespace ColoShopEcommerce.WebApp
{
    public partial class Startup
    {
        public void Configuration(IAppBuilder app)
        {
            ConfigureAuth(app);
        }
    }
}
