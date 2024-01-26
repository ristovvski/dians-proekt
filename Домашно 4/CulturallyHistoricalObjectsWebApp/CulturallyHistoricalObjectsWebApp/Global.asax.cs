using CulturallyHistoricalObjectsWebApp.Data;
using CulturallyHistoricalObjectsWebApp.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using System.Web.Optimization;
using System.Web.Routing;

namespace CulturallyHistoricalObjectsWebApp
{
    public class MvcApplication : System.Web.HttpApplication
    {
        protected void Application_Start()
        {
            AreaRegistration.RegisterAllAreas();
            FilterConfig.RegisterGlobalFilters(GlobalFilters.Filters);
            RouteConfig.RegisterRoutes(RouteTable.Routes);
            BundleConfig.RegisterBundles(BundleTable.Bundles);
            AppDomain.CurrentDomain.SetData("DataDirectory", @"C:\Users\vanes\source\repos\dians-proekt4\Домашно 4\CulturallyHistoricalObjectsWebApp\App_Data");
        }
    }
}
