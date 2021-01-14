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
    public class RDD_LeaveAdjustmentController : Controller
    {
        RDD_LeaveAdjustment_Db_Operation rDD_LeaveAdjustment_TemplatesDb = new RDD_LeaveAdjustment_Db_Operation();
        // GET: LMS/RDD_LeaveLedger
        public ActionResult Index()
        {
            RDD_LeaveAdjustment rDD_LeaveAdjustment = new RDD_LeaveAdjustment();
            rDD_LeaveAdjustment.CountryList = rDD_LeaveAdjustment_TemplatesDb.GetCountryList();
            rDD_LeaveAdjustment.DepartmentList = rDD_LeaveAdjustment_TemplatesDb.GetDeptList();
            rDD_LeaveAdjustment.EmployeeList = rDD_LeaveAdjustment_TemplatesDb.GetEmployeeList();
            rDD_LeaveAdjustment.LeaveTypeList = rDD_LeaveAdjustment_TemplatesDb.GetLeaveTypeList();
            return View(rDD_LeaveAdjustment);
        }
        [Route("SaveLeaveAdjustment")]
        public ActionResult SaveLeaveAdjustment(RDD_LeaveAdjustment rDD_LeaveAdjustment)
        {
            rDD_LeaveAdjustment.CreatedBy = User.Identity.Name;
            rDD_LeaveAdjustment.CreatedOn = DateTime.Now;
            if (rDD_LeaveAdjustment.Editflag == true)
            {
                rDD_LeaveAdjustment.LastUpdatedBy = User.Identity.Name;
                rDD_LeaveAdjustment.LastUpdatedOn = System.DateTime.Now;
            }
            return Json(rDD_LeaveAdjustment_TemplatesDb.Save(rDD_LeaveAdjustment), JsonRequestBehavior.AllowGet);
        }
        public ActionResult GetLeaveAdjustment(int Empid, string Countrycode, int Deptid)
        {
            List<RDD_LeaveAdjustment> rDD_LeaveAdjustments = new List<RDD_LeaveAdjustment>();
            rDD_LeaveAdjustments = rDD_LeaveAdjustment_TemplatesDb.GetAllData(Empid, Countrycode, Deptid);
            return Json(rDD_LeaveAdjustments, JsonRequestBehavior.AllowGet);
        }
        public ActionResult ShowLeaveAdjustmentList()
        {
            List<RDD_LeaveAdjustment> rDD_LeaveAdjustments = new List<RDD_LeaveAdjustment>();
            rDD_LeaveAdjustments = rDD_LeaveAdjustment_TemplatesDb.ShowAllData();
            return Json(rDD_LeaveAdjustments, JsonRequestBehavior.AllowGet);
            
        }
       
    }
}