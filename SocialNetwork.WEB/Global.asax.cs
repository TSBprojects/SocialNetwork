using SocialNetwork.BLL.Infrastructure;
using SocialNetwork.DAL.EntityFramework;
using SocialNetwork.WEB.App_Start;
using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using System.Web.Optimization;
using System.Web.Routing;

namespace SocialNetwork.WEB
{
    public class MvcApplication : System.Web.HttpApplication
    {
        protected void Application_Start()
        {
            AreaRegistration.RegisterAllAreas();
            RouteConfig.RegisterRoutes(RouteTable.Routes);
            BundleConfig.RegisterBundles(BundleTable.Bundles);
            DependencyResolver.SetResolver(new NinjectDependencyResolver("MyDatabase"));
            AutoMapperBLLConfig.RegisterMappings();
            AutoMapperWEBConfig.RegisterMappings();
        }
    }
}
