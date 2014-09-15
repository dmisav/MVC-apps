using System.Web.Mvc;
using System.Web.Optimization;
using System.Web.Routing;
using TestProjectSavchenko.App_Start;

namespace TestProjectSavchenko
{
    public class MvcApplication : System.Web.HttpApplication
    {
        protected void Application_Start()
        {
            AreaRegistration.RegisterAllAreas();
            RouteConfig.RegisterRoutes(RouteTable.Routes);
            BundleConfig.RegisterBundles(BundleTable.Bundles);
        }
    }
}