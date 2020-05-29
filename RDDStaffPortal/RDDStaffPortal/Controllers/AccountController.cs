using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Net;
using System.Net.Http;
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
                   Session["LoginName"] = moduleDbOp.GetProfilimg(login.UserName);
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
            return RedirectToAction("/Login", "Account");
        }

        [ChildActionOnly]
        public ActionResult GetMenuTreeMenu()
        {
            //if (User.Identity.Name == "")
            //{
            //    return RedirectToAction("/Login", "Account");

            //}
            return PartialView(moduleDbOp.GetModuleList2(User.Identity.Name,"U"));

        }



        [HttpGet]
        [ChildActionOnly]
        public ActionResult GetFirtsDashBoard()
        {
           
            return PartialView(moduleDbOp.GetFirstDashBoards(User.Identity.Name));
        }

        [ChildActionOnly]
        public ActionResult GetDashBoardView()
        {
        //    if (User.Identity.Name == "")
        //    {
        //        return RedirectToAction("/Login", "Account");

        //    }
            return PartialView(moduleDbOp.GetDashBoarMain(User.Identity.Name,"U"));
        }


    }
}