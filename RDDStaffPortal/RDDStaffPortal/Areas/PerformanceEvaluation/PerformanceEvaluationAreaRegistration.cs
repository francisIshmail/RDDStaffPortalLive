using System.Web.Mvc;

namespace RDDStaffPortal.Areas.PerformanceEvaluation
{
    public class PerformanceEvaluationAreaRegistration : AreaRegistration 
    {
        public override string AreaName 
        {
            get 
            {
                return "PerformanceEvaluation";
            }
        }

        public override void RegisterArea(AreaRegistrationContext context) 
        {
            context.MapRoute(
                "PerformanceEvaluation_default",
                "PerformanceEvaluation/{controller}/{action}/{id}",
                new { action = "Index", id = UrlParameter.Optional }
            );
        }
    }
}