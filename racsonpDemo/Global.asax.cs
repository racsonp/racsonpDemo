﻿using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Web;
using System.Web.Http;
using System.Web.Mvc;
using System.Web.Optimization;
using System.Web.Routing;
using racsonpDemo.DataAcceses;

namespace racsonpDemo
{
    public class MvcApplication : System.Web.HttpApplication
    {
        protected void Application_Start()
        {
            AreaRegistration.RegisterAllAreas();
            GlobalConfiguration.Configure(WebApiConfig.Register);
            FilterConfig.RegisterGlobalFilters(GlobalFilters.Filters);
            RouteConfig.RegisterRoutes(RouteTable.Routes);
            BundleConfig.RegisterBundles(BundleTable.Bundles);

          //  GlobalConfiguration.Configure(WebApiConfig.Register);


            //ESTO SE COMENTA CUADO SE VA APP HARBOR
            //var dataBaseContext = new DataBaseContext();
            //Database.SetInitializer(new DataBaseInitializer());
            //dataBaseContext.Database.Initialize(true);


         


        }
    }
}
