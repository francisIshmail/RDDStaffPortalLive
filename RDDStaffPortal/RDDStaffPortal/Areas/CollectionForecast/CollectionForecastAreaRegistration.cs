using System.Web.Mvc;

namespace RDDStaffPortal.Areas.CollectionForecast
{
    public class CollectionForecastAreaRegistration : AreaRegistration 
    {
        public override string AreaName 
        {
            get 
            {
                return "CollectionForecast";
            }
        }

        public override void RegisterArea(AreaRegistrationContext context) 
        {
            context.MapRoute(
                "CollectionForecast_default",
                "CollectionForecast/{controller}/{action}/{id}",
                new { action = "Index", id = UrlParameter.Optional }
            );
        }
    }
}