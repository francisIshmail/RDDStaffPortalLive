using RDDStaffPortal.DAL.Admin;
using RDDStaffPortal.DAL.LMS;
using RDDStaffPortal.DAL.DataModels.LMS;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace RDDStaffPortal.Areas.LMS.Controllers
{
    [Authorize]
    public class RDD_LeaveRequestController : Controller
    {
        RDD_LeaveRequest_Db_Operation rDD_LeaveRequest_TemplatesDb = new RDD_LeaveRequest_Db_Operation();
        // GET: LMS/RDD_LeaveRequest
        public ActionResult Index()
        {
            return View();
        }
        public ActionResult GetLeaveRequest_List()
        {

            List<RDD_LeaveRequest> modules = new List<RDD_LeaveRequest>();
            modules = rDD_LeaveRequest_TemplatesDb.GetList();
            return PartialView(modules);
        }
        [Route("SaveLeaveRequest")]
        public ActionResult SaveHolidays(RDD_LeaveRequest rDD_LeaveRequest)
        {
            rDD_LeaveRequest.CreatedBy = User.Identity.Name;
            rDD_LeaveRequest.CreatedOn = DateTime.Now;
            if (rDD_LeaveRequest.Editflag == true)
            {
                rDD_LeaveRequest.LastUpdatedBy = User.Identity.Name;
                rDD_LeaveRequest.LastUpdatedOn = System.DateTime.Now;
            }
            return Json(new { data = rDD_LeaveRequest_TemplatesDb.Save(rDD_LeaveRequest) }, JsonRequestBehavior.AllowGet);
        }
        [Route("GetLeaveRequest")]
        public ActionResult GetData(string LeaveRequestId)
        {
            return Json(new { data = rDD_LeaveRequest_TemplatesDb.GetData(LeaveRequestId) }, JsonRequestBehavior.AllowGet);
        }

    }
}