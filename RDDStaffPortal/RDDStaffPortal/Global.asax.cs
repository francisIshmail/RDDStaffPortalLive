using RDDStaffPortal.Infrastructure;
using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data.SqlClient;
using System.Globalization;
using System.Linq;
using System.Threading;
using System.Web;
using System.Web.Mvc;
using System.Web.Optimization;
using System.Web.Routing;

namespace RDDStaffPortal
{
    public class MvcApplication : System.Web.HttpApplication
    {
        string conStr = ConfigurationManager.ConnectionStrings["tejSAP"].ConnectionString;
        protected void Application_Start()
        {
            AreaRegistration.RegisterAllAreas();            
            FilterConfig.RegisterGlobalFilters(GlobalFilters.Filters);
            RouteConfig.RegisterRoutes(RouteTable.Routes);
            BundleConfig.RegisterBundles(BundleTable.Bundles);
            GlobalFilters.Filters.Add(new CustomAuthenticationFilter());
            GlobalFilters.Filters.Add(new MyCustomHandleErrorAttribute());
            //START SQL DEPENDENCY
            SqlDependency.Start(conStr);

        }
        protected void Session_Start(object sender, EventArgs e)
        {
            string username = Context.User.Identity.Name;
            NotificationComponent notiCom = new NotificationComponent();
            var currentDateTime = DateTime.Now;
            HttpContext.Current.Session["LastTimeNotified"] = currentDateTime;
            notiCom.RegisterNotification(currentDateTime,username);
        }
        protected void Application_BeginRequest(Object sender, EventArgs e)                           
       {
            CultureInfo cInfo = new CultureInfo("en-IN");
            cInfo.DateTimeFormat.ShortDatePattern = "yyyy/MM/dd";
            cInfo.DateTimeFormat.DateSeparator = "-";
            Thread.CurrentThread.CurrentCulture = cInfo;
            Thread.CurrentThread.CurrentUICulture = cInfo;
        }

        public override string GetVaryByCustomString(HttpContext context, string custom)
        {
            if (custom == "LoggedUserName")
            {
                if (context.Request.IsAuthenticated)
                {
                    return context.User.Identity.Name;
                }
                return null;
            }
            return base.GetVaryByCustomString(context, custom);
        }
        protected void Application_End()
        {
            //STOP SQL DEPENDENCY
            SqlDependency.Stop(conStr);
        }

    }
}
