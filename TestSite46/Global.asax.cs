using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Hosting;
using System.Web.Mvc;
using System.Web.Routing;
using KalikoCMS.ServiceLocator;
using KalikoCMS.UI;
using Serilog;

namespace TestSite46
{
    public class MvcApplication : System.Web.HttpApplication
    {
        protected void Application_Start()
        {
            HostingEnvironment.RegisterVirtualPathProvider(new EmbeddedVirtualPathProvider());
            SimpleInjectorProvider.InitializeContainer();

            var log = new LoggerConfiguration()
                .WriteTo.RollingFile(Server.MapPath("~/App_Data/log-{Date}.txt"))
                .CreateLogger();
            //log.Information("Serilog init");
            Log.Logger = log;

            AreaRegistration.RegisterAllAreas();
            RouteConfig.RegisterRoutes(RouteTable.Routes);
        }
    }
}
