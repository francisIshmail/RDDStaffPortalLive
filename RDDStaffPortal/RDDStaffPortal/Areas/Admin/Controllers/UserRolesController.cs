using RDDStaffPortal.DAL.InitialSetup;
using RDDStaffPortal.WebServices;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web.Mvc;

namespace RDDStaffPortal.Areas.Admin.Controllers
{
    [Authorize]
    public class UserRolesController : Controller
    {
        RDD_User_RightsDBOperation Rdduser = new RDD_User_RightsDBOperation();
        AccountService accountService = new AccountService();
        [Authorize]
        // GET: Admin/UserRoles
        public ActionResult Index()
        {

            ViewBag.UserRoles = accountService.GetRoles().ToList();
            ViewBag.Users = Rdduser.GetUserList();

            return View();
        }


        public JsonResult GetRoles()
        {
            return Json(accountService.GetRoles(), JsonRequestBehavior.AllowGet);
        }

        public JsonResult CreateRole(string RoleName)
        {
            MembershipResponse membershipResponse = new MembershipResponse();
            try
            {
                membershipResponse = accountService.CreateRole(RoleName);
            }
            catch (Exception ex)
            {
                membershipResponse.Success = false;
                membershipResponse.Message = "Error occured : " + ex.Message;
            }
            return Json(membershipResponse, JsonRequestBehavior.AllowGet);
        }

        public JsonResult DeleteRole(string RoleName)
        {
            MembershipResponse membershipResponse = new MembershipResponse();
            try
            {
                membershipResponse = accountService.DeleteRole(RoleName);
            }
            catch (Exception ex)
            {
                membershipResponse.Success = false;
                membershipResponse.Message = "Error occured : " + ex.Message;
            }
            return Json(membershipResponse, JsonRequestBehavior.AllowGet);
        }

        public JsonResult GetRolesForUser(string Username)
        {
            return Json(accountService.GetRolesForUser(Username), JsonRequestBehavior.AllowGet);
        }


        public JsonResult RemoveUserFromRole(string Username, string Role)
        {
            return Json(accountService.RemoveUserFromRole(Username, Role), JsonRequestBehavior.AllowGet);
        }

        public JsonResult AddUserToRole(string Username, List<string> Role)
        {
            return Json(accountService.AddUserToRole(Username, Role), JsonRequestBehavior.AllowGet);
        }

    }
}