using DocumentFormat.OpenXml.Office2010.ExcelAc;
using RDDStaffPortal.Infrastructure;
using RDDStaffPortal.WebServices;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace RDDStaffPortal
{
    public class FilterConfig
    {
        public static void RegisterGlobalFilters(GlobalFilterCollection filters)
        {

          AccountService accountService = new AccountService();



          
            List<string> Roles = new List<string>();
            Roles = accountService.GetRoles().ToList();
            var str = "";
            foreach (var roleKey in Roles)
            {
                str =  roleKey.ToString()+","+str;
            }
            filters.Add(new HandleErrorAttribute());
          
            filters.Add(new AuthorizeAttribute() { Roles =str });//"Admin,HR,Funnel,Marketing,Reports,SAP,Targets" 
            //filters.Add(new System.Web.Mvc.AuthorizeAttribute() );

        }
    }
}
