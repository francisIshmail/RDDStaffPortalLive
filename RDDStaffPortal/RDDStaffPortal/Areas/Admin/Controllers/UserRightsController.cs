using RDDStaffPortal.DAL.DataModels;
using RDDStaffPortal.DAL.InitialSetup;
using System.Collections.Generic;
using System.Web.Mvc;

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
        public ActionResult GetUserList()
        {
            return Json(Rdduser.GetUserList(),JsonRequestBehavior.AllowGet);
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
    }
}