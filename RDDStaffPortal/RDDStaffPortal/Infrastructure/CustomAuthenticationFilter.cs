using System;
using System.Web;
using System.Web.Mvc;
using System.Web.Mvc.Filters;
using System.Web.Routing;



namespace RDDStaffPortal.Infrastructure
{
    public class CustomAuthenticationFilter : ActionFilterAttribute, IAuthorizationFilter
    {
        public void OnAuthorization(AuthorizationContext filterContext)
        
        
        {
            
            
            if (filterContext.ActionDescriptor.IsDefined(typeof(AllowAnonymousAttribute), true)
                || filterContext.ActionDescriptor.ControllerDescriptor.IsDefined(typeof(AllowAnonymousAttribute), true))
            {
                // Don't check for authorization as AllowAnonymous filter is applied to the action or controller
                return;
            }

            // Check for authorization
            if (HttpContext.Current.Session["LoginName"] == null)
            {
               

               
                // filterContext.Result = new RedirectResult("~/Account/Login");
                // filterContext.Result = new RedirectToRouteResult(new RouteValueDictionary(new { area,controller,action }));

                new RedirectToRouteResult(new RouteValueDictionary
                {
                     { "Area" , "" },
                     { "controller", "Account" },
                     { "action", "Login" }
                       
                });
                //filterContext.Result = new RedirectToRouteResult(new RouteValueDictionary
                //{
                //     { "action", "Login" },
                //    { "controller", "Account" },                    
                //        { "Area" , string.Empty }
                //});
            }
        }
    


    //public void OnActionExecuting(AuthenticationContext filterContext)
    //{
    //    if (string.IsNullOrEmpty(Convert.ToString(filterContext.HttpContext.Session["LoginName"])))
    //    {
    //        filterContext.Result = new HttpUnauthorizedResult();
    //    }
    //}
    //public void OnResultExecuting(AuthenticationChallengeContext filterContext)
    //{
    //    if (filterContext.Result == null || filterContext.Result is HttpUnauthorizedResult)
    //    {
    //        //Redirecting the user to the Login View of Account Controller  
    //        filterContext.Result = new RedirectToRouteResult(
    //        new RouteValueDictionary
    //        {
    //             { "controller", "Account" },
    //             { "action", "Login" }
    //        });
    //    }
    //}
}

}