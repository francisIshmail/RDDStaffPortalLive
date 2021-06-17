using System.Web.Mvc;

namespace RDDStaffPortal.Areas.PerformanceAppraisal
{
    public class PerformanceAppraisalAreaRegistration : AreaRegistration 
    {
        public override string AreaName 
        {
            get 
            {
                return "PerformanceAppraisal";
            }
        }

        public override void RegisterArea(AreaRegistrationContext context) 
        {
            context.MapRoute(
                "PerformanceAppraisal_default",
                "PerformanceAppraisal/{controller}/{action}/{id}",
                new { action = "Index", id = UrlParameter.Optional }
            );
        }
    }
}