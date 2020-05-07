using Microsoft.Owin;
using Owin;

[assembly: OwinStartup(typeof(LayeredWebDemo.Web.Startup))]
namespace LayeredWebDemo.Web
{
    public class Startup
    {
        public void Configuration(IAppBuilder app)
        {
            BLL.Config.Startup.ConfigureAuth(app);
        }
    }
}
