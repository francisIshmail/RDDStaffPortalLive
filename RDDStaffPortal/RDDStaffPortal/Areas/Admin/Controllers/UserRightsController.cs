using RDDStaffPortal.DAL.DataModels;
using RDDStaffPortal.DAL.InitialSetup;
using System.Collections.Generic;
using System.Linq;
using System.Web.Mvc;
using static RDDStaffPortal.DAL.Global;

namespace RDDStaffPortal.Areas.Admin.Controllers
{
    [Authorize]
    public class UserRightsController : Controller
    {
        // GET: Admin/UserRights

        RDD_User_RightsDBOperation Rdduser = new RDD_User_RightsDBOperation();
        ModulesDbOperation moduleDbOp = new ModulesDbOperation();
        public ActionResult Index()
        {                           
            return View();
        }

        public ActionResult UserDashWidget()
        {
            return View();
        }
        [Route("SaveUserRights")]
        public ActionResult SaveUserRights(RDD_User_Rights Users)
        {
            Users.CreatedBy = User.Identity.Name;
            return Json(new { SaveFlag = Rdduser.save1(Users) }, JsonRequestBehavior.AllowGet);
        }

        public ActionResult AddUserRights()
        {
            return PartialView(Rdduser.GetNew());
        }
        [Route("GetUserList")]
        [MyOutputCache(VaryByParam = "none", VaryByCustom = "LoggedUserName")]
        public ActionResult GetUserList()
        {
            return Json(Rdduser.GetUserList(),JsonRequestBehavior.AllowGet);
        }

        [Route("GetUserListAuto")]
        public ActionResult GetUserListAuto(string Prefix)
        { 
            return Json(Rdduser.GetUserListAuto(Prefix), JsonRequestBehavior.AllowGet);
        }

        [Route("GetUserRightsList")]
        public ActionResult GerUserRightList(string UserId)
        {
            return Json(Rdduser.GetUserRightsList(UserId), JsonRequestBehavior.AllowGet);
        }
       

        [ChildActionOnly]
        public ActionResult GetMenuTree()
        {
            return PartialView(moduleDbOp.GetModuleList2("Admin", "A"));

        }
        [ChildActionOnly]
        public ActionResult GetWidget()
        {
            return PartialView(moduleDbOp.GetDashBoarMain("Admin","A"));
        }
        [ChildActionOnly]
        public ActionResult GetUserWidget()
        {
            return PartialView(Rdduser.GetUserWidget(User.Identity.Name));
        }

        [Route("SaveUserDash")]
        public ActionResult SaveUserDash(RDD_DashBoard_Main UsersWidget)
        {
            UsersWidget.UserId = User.Identity.Name;
            return Json(new { SaveFlag = Rdduser.Save2(UsersWidget) }, JsonRequestBehavior.AllowGet);
        }
    }
}