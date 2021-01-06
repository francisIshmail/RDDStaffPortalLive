using RDDStaffPortal.Infrastructure;
using System.Web;
using System.Web.Mvc;
using RDDStaffPortal.WebServices;
using System.Collections.Generic;
using System.Linq;
namespace RDDStaffPortal
{
    public class FilterConfig
    {
        public static void RegisterGlobalFilters(GlobalFilterCollection filters)
        {
            AccountService accountService = new AccountService();

            List<string> Roles = new List<string>();            Roles = accountService.GetRoles().ToList();            var str = "";            foreach (var roleKey in Roles)            {                str = roleKey.ToString() + "," + str;            }            filters.Add(new HandleErrorAttribute());

            filters.Add(new AuthorizeAttribute() { Roles = str });//"Admin,HR,Funnel,Marketing,Reports,SAP,Targets"             //filters.Add(new System.Web.Mvc.AuthorizeAttribute() );
        }
    }
}
