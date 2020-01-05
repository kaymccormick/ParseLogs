using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using System.Web.Optimization;
using System.Web.Routing;
using System.Web.Security;
using System.Web.SessionState;
using System.Web.Http;

namespace WebApp
{
    public class Global : HttpApplication
    {
        private static readonly NLog.Logger Logger = NLog.LogManager.GetCurrentClassLogger();

        void Application_Start(object sender, EventArgs e)
        {
            // Code that runs on application startup
            Logger.Info("Web Application Starting up");
            AreaRegistration.RegisterAllAreas();
            GlobalConfiguration.Configure(WebApiConfig.Register);
            RouteConfig.RegisterRoutes(RouteTable.Routes);
            BundleConfig.RegisterBundles(BundleTable.Bundles);
        }
    }
}