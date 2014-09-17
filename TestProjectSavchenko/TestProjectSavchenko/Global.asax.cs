using Castle.Windsor;
using Castle.Windsor.Installer;
using System.Web.Mvc;
using System.Web.Optimization;
using System.Web.Routing;
using TestProjectSavchenko.App_Start;
using TestProjectSavchenko.Infrastructure;

namespace TestProjectSavchenko
{
    public class MvcApplication : System.Web.HttpApplication
    {
        private static IWindsorContainer container;

        private static void RegisterIOC()
        {
            container = new WindsorContainer().Install(FromAssembly.This());
            var controllerFactory = new WindsorControllerFactory(container.Kernel);
            ControllerBuilder.Current.SetControllerFactory(controllerFactory);
        }

        protected void Application_Start()
        {
            AreaRegistration.RegisterAllAreas();
            RouteConfig.RegisterRoutes(RouteTable.Routes);
            BundleConfig.RegisterBundles(BundleTable.Bundles);
            RegisterIOC();
        }

        protected void Application_End()
        {
            container.Dispose();
        }
    }
}