using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using BusinessLayer;
using System.Web.Mvc;
using System.Web.Optimization;
using System.Web.Routing;
//using System.Data.Entity; //!!                    //removed, lab32
//using WebApplication1.DataAccessLayer; //!!       //removed, lab32

namespace WebApplication1
{
    public class MvcApplication : System.Web.HttpApplication
    {
        protected void Application_Start()
        {
            //Database.SetInitializer(new DropCreateDatabaseIfModelChanges<SalesERPDAL>()); //!!!!!!!!!!!!!!!!
            AreaRegistration.RegisterAllAreas();
            FilterConfig.RegisterGlobalFilters(GlobalFilters.Filters);
            RouteConfig.RegisterRoutes(RouteTable.Routes);
            BundleConfig.RegisterBundles(BundleTable.Bundles);
            BusinessSettings.SetBusiness();
        }
    }
}
