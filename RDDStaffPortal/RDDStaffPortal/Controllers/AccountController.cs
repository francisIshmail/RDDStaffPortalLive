using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using RDDStaffPortal.DAL.InitialSetup;
using RDDStaffPortal.Models;
using RDDStaffPortal.WebServices;

namespace RDDStaffPortal.Controllers
{
    public class AccountController : Controller
    {
        // GET: Account
        ModulesDbOperation moduleDbOp = new ModulesDbOperation();
        public ActionResult Index()
        {
            return View();
        }
        
        public ActionResult Login()
        {
            return View();
        }

        [HttpPost]
        public ActionResult Login(Login login, string ReturnUrl)
        {
            if (ModelState.IsValid)
            {
                AccountService accountService = new AccountService();
                var response = accountService.Login(login.UserName, login.UserPassword);
                if (response.Success)
                {
                    //Services.Dashboard dashboardService = new Services.Dashboard();
                    //List<MonthWiseRevenueAndGP> MonthwiseRevGPList = new List<MonthWiseRevenueAndGP>();
                    //string LoggedInUser = User.Identity.Name;
                    //if (string.IsNullOrEmpty(LoggedInUser))
                    //{
                    //    LoggedInUser = login.UserName;
                    //}
                    //MonthwiseRevGPList = dashboardService.GetMonthWiseRevenueAndGP(LoggedInUser);
                    //Session["MonthwiseRevGPList"] = MonthwiseRevGPList;

                    return RedirectToAction("Index", "Dashboard");
                }
                else
                {
                    ModelState.AddModelError("", "Invalid UserName and Password");
                }

            }
            return View();
        }

        public ActionResult SignOut()
        {
            // Rename AcountLogin to LoginService in Live
            AccountService accountService = new AccountService();
            bool response = accountService.SignOut();
            if (response)
            {
                Session.Abandon();
                Session.Clear();
            }
            // FormsAuthentication.SignOut();
            return RedirectToAction("Login", "Account");
        }

        [ChildActionOnly]
        public ActionResult GetMenuTree()



        {
            return PartialView(moduleDbOp.GetModuleList2());

        }


    }
}