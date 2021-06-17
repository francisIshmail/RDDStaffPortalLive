using System.Web.Mvc;

namespace RDDStaffPortal.Areas.SORCodeGenerator
{
    public class SORCodeGeneratorAreaRegistration : AreaRegistration 
    {
        public override string AreaName 
        {
            get 
            {
                return "SORCodeGenerator";
            }
        }

        public override void RegisterArea(AreaRegistrationContext context) 
        {
            context.MapRoute(
                "SORCodeGenerator_default",
                "SORCodeGenerator/{controller}/{action}/{id}",
                new { action = "Index", id = UrlParameter.Optional }
            );
        }
    }
}