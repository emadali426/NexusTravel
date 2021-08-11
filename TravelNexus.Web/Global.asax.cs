using System;
using System.Collections.Generic;
using System.Data.Entity.Migrations;
using System.Linq;
using System.Runtime.ExceptionServices;
using System.Web;
using System.Web.Mvc;
using System.Web.Optimization;
using System.Web.Routing;
using TravelNexus.Web.Models;
using TravelNexus.Web.Controllers;
using System.Net;
using SMTP2Go.Service.Models;

namespace TravelNexus.Web
{
    public class MvcApplication : System.Web.HttpApplication
    {
        [HandleProcessCorruptedStateExceptions]
        protected void Application_Start()
        {
            AreaRegistration.RegisterAllAreas();
            FilterConfig.RegisterGlobalFilters(GlobalFilters.Filters);
            RouteConfig.RegisterRoutes(RouteTable.Routes);
            BundleConfig.RegisterBundles(BundleTable.Bundles);

            new APIsKeysConfig();
            new SMTP2GoConfig();
            new CountryVATsConfig();
            new CurrenciesConfig();
            new FrontEndManage();
            new CitiesName();
        }
        private void Application_BeginRequest(Object source, EventArgs e)
        {

            ApplicationDbContext db = ApplicationDbContext.Create();
            //TransactionsDbContext tdb = TransactionsDbContext.Create();
            Fawaterk.Service.FawaterksDbContext fdb = Fawaterk.Service.FawaterksDbContext.Create();
        }

        protected void Application_Error(object sender, EventArgs e)
        {
            //Exception lastErrorInfo = Server.GetLastError();
            //Exception errorInfo = null;


            //if (lastErrorInfo != null)
            //{
            //    errorInfo = lastErrorInfo.GetBaseException();
            //    var error = errorInfo as HttpException;
            //    if (error != null)
            //    {
            //        Server.ClearError();
            //        Response.Redirect("~/HttpErrors/NotFound");
            //    }
            //}

        }
    }
}
