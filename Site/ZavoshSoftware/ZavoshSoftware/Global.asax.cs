using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Http;
using System.Web.Mvc;
using System.Web.Optimization;
using System.Web.Routing;

namespace ZavoshSoftware
{
    public class MvcApplication : System.Web.HttpApplication
    {
        protected void Application_Start()
        {
            AreaRegistration.RegisterAllAreas();
            RouteConfig.RegisterRoutes(RouteTable.Routes);

            BundleConfig.RegisterBundles(BundleTable.Bundles);//for register bundle
            GlobalConfiguration.Configure(WebApiConfig.Register);
        }


        //protected void Application_BeginRequest()
        //{
        //    var persianCulture = new PersianCulture();
        //    Thread.CurrentThread.CurrentCulture = persianCulture;
        //    Thread.CurrentThread.CurrentUICulture = persianCulture;
        //}
    }
}
