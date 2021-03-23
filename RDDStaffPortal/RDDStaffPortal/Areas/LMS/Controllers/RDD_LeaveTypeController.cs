using RDDStaffPortal.DAL.Admin;
using RDDStaffPortal.DAL.LMS;
using RDDStaffPortal.DAL.DataModels.LMS;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using System.Data;


namespace RDDStaffPortal.Areas.LMS.Controllers
{
    [Authorize]
    public class RDD_LeaveTypeController : Controller
    {
        RDD_LeaveType_Db_Operation rDD_LeaveType_TemplatesDb = new RDD_LeaveType_Db_Operation();
        // GET: LMS/RDD_LeaveType
        public ActionResult Index()
        {
            RDD_LeaveType rDD_LeaveType = new RDD_LeaveType();
            rDD_LeaveType.CountryList = rDD_LeaveType_TemplatesDb.GetCountryList();
            return View(rDD_LeaveType);

        }
        [Route("SaveLeaveType")]
        public ActionResult SaveLeaveType(RDD_LeaveType rDD_LeaveType)
        {
            rDD_LeaveType.CreatedBy = User.Identity.Name;
            rDD_LeaveType.CreatedOn = DateTime.Now;
            if (rDD_LeaveType.Editflag == true)
            {
                rDD_LeaveType.LastUpdatedBy = User.Identity.Name;
                rDD_LeaveType.LastUpdatedOn = System.DateTime.Now;
            }
            return Json(rDD_LeaveType_TemplatesDb.Save(rDD_LeaveType), JsonRequestBehavior.AllowGet);
        }
        [Route("DeleteLeaveType")]
        public ActionResult DeleteFlag(int LeaveTypeId)
        {
            return Json(new { data = rDD_LeaveType_TemplatesDb.DeleteFlag(LeaveTypeId) }, JsonRequestBehavior.AllowGet);
        }
        public ActionResult GetLeaveDetails()
        {
            List<RDD_LeaveType> rDD_LeaveTypes = new List<RDD_LeaveType>();
            rDD_LeaveTypes = rDD_LeaveType_TemplatesDb.GetALLDATA();
            return Json(rDD_LeaveTypes, JsonRequestBehavior.AllowGet);
        }
    }
}