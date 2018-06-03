using BLL.Infrastructure;
using DAL;
using Ninject;
using Ninject.Modules;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Http;
using System.Web.Mvc;
using System.Web.Optimization;
using System.Web.Routing;
using WebApp.Models;
using WebApp.Util;
using System.Data.Entity;

namespace WebApp
{
    public class WebApiApplication : System.Web.HttpApplication
    {
        protected void Application_Start()
        {
            AreaRegistration.RegisterAllAreas();
            GlobalConfiguration.Configure(WebApiConfig.Register);
            FilterConfig.RegisterGlobalFilters(GlobalFilters.Filters);
            RouteConfig.RegisterRoutes(RouteTable.Routes);
            BundleConfig.RegisterBundles(BundleTable.Bundles);

            //Ninject module binding
            //NinjectModule ninjectModule = new NinjectSetting();
            //NinjectModule vacancyModule = new VacancyModule();
            //NinjectModule resumeModule = new ResumeModule();
            //NinjectModule serviceModule = new ServiceModule("DefaultConnection");
            //var kernel = new StandardKernel(ninjectModule, serviceModule);
            //var ninjectResolver = new NinjectDependencyResolver(kernel);
            //DependencyResolver.SetResolver(ninjectResolver); // MVC
            //GlobalConfiguration.Configuration.DependencyResolver = ninjectResolver; // Web API
        }
    }
}
