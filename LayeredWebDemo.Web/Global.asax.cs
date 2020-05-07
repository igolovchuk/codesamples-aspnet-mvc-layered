using LayeredWebDemo.BLL.Config;
using System.Configuration;
using System.Data.SqlClient;
using System.Web.Mvc;
using System.Web.Optimization;
using System.Web.Routing;


namespace LayeredWebDemo.Web
{
    public class MvcApplication : System.Web.HttpApplication
    {
        protected void Application_Start()
        {
            AreaRegistration.RegisterAllAreas();
            FilterConfig.RegisterGlobalFilters(GlobalFilters.Filters);
            RouteConfig.RegisterRoutes(RouteTable.Routes);
            BundleConfig.RegisterBundles(BundleTable.Bundles);
            //Auto Mapper
            AutoMapperConfig.RegisterMappings();
            //SQL Dependency
            SqlDependency.Start(ConfigurationManager.ConnectionStrings["DefaultConnection"].ConnectionString);
            //GlobalConfiguration.Configuration.Formatters.JsonFormatter.SerializerSettings.ReferenceLoopHandling = Newtonsoft.Json.ReferenceLoopHandling.Ignore;
            //GlobalConfiguration.Configuration.Formatters.Remove(GlobalConfiguration.Configuration.Formatters.XmlFormatter);           
        }

        protected void Application_End()
        {
            SqlDependency.Stop(ConfigurationManager.ConnectionStrings["DefaultConnection"].ConnectionString);
        }
    }
}
