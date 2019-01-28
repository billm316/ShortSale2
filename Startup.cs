using Microsoft.Owin;
using Owin;

[assembly: OwinStartupAttribute(typeof(ShortSale2.Startup))]
namespace ShortSale2
{
    public partial class Startup
    {
        public void Configuration(IAppBuilder app)
        {
            ConfigureAuth(app);
        }
    }
}
