using RDDStaffPortal.Infrastructure;
using System.Web;
using System.Web.Mvc;

namespace RDDStaffPortal
{
    public class FilterConfig
    {
        public static void RegisterGlobalFilters(GlobalFilterCollection filters)
        {
            filters.Add(new HandleErrorAttribute());
            filters.Add(new AuthorizeAttribute() { Roles = "Admin,HR,Funnel,Marketing,Reports,SAP,Targets" });
          //filters.Add(new System.Web.Mvc.AuthorizeAttribute() );

        }
    }
}
