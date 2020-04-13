using RDDStaffPortal.DAL.DataModels;
using RDDStaffPortal.DAL.InitialSetup;
using System.Web.Mvc;

namespace RDDStaffPortal.Areas.Admin.Controllers
{
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
            return Json(Rdduser.save1(Users), JsonRequestBehavior.AllowGet);
        }

        public ActionResult AddUserRights()
        {
            return PartialView(Rdduser.GetNew());
        }
        //[ChildActionOnly]
        //public ActionResult GetMenuTree()



        //{
        //    return PartialView(moduleDbOp.GetModuleList2());

        //}
    }
}