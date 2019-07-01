using Microsoft.Owin;
using Owin;

[assembly: OwinStartupAttribute(typeof(Wiser.MVC.Startup))]
namespace Wiser.MVC
{
    public partial class Startup
    {
        public void Configuration(IAppBuilder app)
        {
            ConfigureAuth(app);
        }
    }
}
