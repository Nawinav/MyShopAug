using Microsoft.Owin;
using Owin;

[assembly: OwinStartupAttribute(typeof(MyShopAug.UI.Startup))]
namespace MyShopAug.UI
{
    public partial class Startup
    {
        public void Configuration(IAppBuilder app)
        {
            ConfigureAuth(app);
        }
    }
}
