using iTextSharp.text.pdf.qrcode;
using RDDStaffPortal.DAL;
using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace RDDStaffPortal.Infrastructure
{
    public class MyCustomHandleErrorAttribute : HandleErrorAttribute
    {

        CommonFunction Com = new CommonFunction();
        public override void OnException(ExceptionContext filterContext)
        {
            Exception ex = filterContext.Exception;

            // Log Exception ex in database

            // Notify  admin team
            //,,,,

            var controllerName = (string)filterContext.RouteData.Values["controller"];
            var actionName = (string)filterContext.RouteData.Values["action"];
            var area = (string)filterContext.RouteData.Values["Area"];
            var model = new HandleErrorInfo(filterContext.Exception, controllerName, actionName);

            filterContext.ExceptionHandled = true;
            SqlParameter[] parm ={ new SqlParameter("@p_userid",filterContext.HttpContext.User.Identity.Name),
                        new SqlParameter("@p_AreaName",area),
                        new SqlParameter("@p_ControllerName",model.ControllerName),
                        new SqlParameter("@p_ActionName",model.ActionName),
                        new SqlParameter("@p_ErrorMsg",ex.Message)
                         };

            Com.ExecuteNonQuery("Rdd_errorlog_Insert",parm);
          

            filterContext.Result = new ViewResult()
            {
                ViewName = "CustomErrorView"
            };
        }
    }
}