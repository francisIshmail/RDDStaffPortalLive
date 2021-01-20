using RDDStaffPortal.DAL.Admin;
using RDDStaffPortal.DAL.LMS;
using RDDStaffPortal.DAL.DataModels.LMS;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using System.Data;
using System.Data.SqlClient;
using RDDStaffPortal.DAL;
using Newtonsoft.Json;

namespace RDDStaffPortal.Areas.LMS.Controllers
{
    [Authorize]
    public class RDD_LeaveAdjustmentController : Controller
    {
        RDD_LeaveAdjustment_Db_Operation rDD_LeaveAdjustment_TemplatesDb = new RDD_LeaveAdjustment_Db_Operation();
        CommonFunction Com = new CommonFunction();
        // GET: LMS/RDD_LeaveLedger
        public ActionResult Index()
        {
            RDD_LeaveAdjustment rDD_LeaveAdjustment = new RDD_LeaveAdjustment();
            rDD_LeaveAdjustment.CountryList = rDD_LeaveAdjustment_TemplatesDb.GetCountryList();
            rDD_LeaveAdjustment.DepartmentList = rDD_LeaveAdjustment_TemplatesDb.GetDeptList();
            rDD_LeaveAdjustment.EmployeeList = rDD_LeaveAdjustment_TemplatesDb.GetEmployeeList();
            //rDD_LeaveAdjustment.LeaveTypeList = rDD_LeaveAdjustment_TemplatesDb.GetLeaveTypeList();
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
        //[Route("GetLeaveAdjustment")]
        public ActionResult GetLeaveAdjustment(int Empid)
        {
            List<RDD_LeaveAdjustment> rDD_LeaveAdjustments = new List<RDD_LeaveAdjustment>();
            rDD_LeaveAdjustments = rDD_LeaveAdjustment_TemplatesDb.GetAllData(Empid);
            return Json(rDD_LeaveAdjustments, JsonRequestBehavior.AllowGet);
        }
        public ActionResult ShowLeaveAdjustmentList()
        {
            List<RDD_LeaveAdjustment> rDD_LeaveAdjustments = new List<RDD_LeaveAdjustment>();
            rDD_LeaveAdjustments = rDD_LeaveAdjustment_TemplatesDb.ShowAllData();
            return Json(rDD_LeaveAdjustments, JsonRequestBehavior.AllowGet);

        }

        public ActionResult SendAdjustmentEmail(int EmpId, string EmpName, decimal NoOfDays, string Description, string LeaveName, string AddDeduct)        {            try            {                DataSet ds1 = new DataSet();                SqlParameter[] prm1 =               {                   new SqlParameter("@LoginUserId",EmpId)                };                ds1 = Com.ExecuteDataSet("RDD_GetManagerDetails", CommandType.StoredProcedure, prm1);                var Email1 = ds1.Tables[0].Rows[0]["Email"].ToString();                var Email2 = "";                var HrMail = "hr@reddotdistribution.com";                if (ds1.Tables[1].Rows.Count > 0)                {                    Email2 = ds1.Tables[1].Rows[0]["Email"].ToString();                }                var Email3 = ds1.Tables[2].Rows[0]["Email"].ToString();                var Tomail = Email3;                var cc = Email1 + "," + Email2 + "," + HrMail;
                string Subject = "Your Leave Adjustment Request";                var Html = "Dear " + EmpName + ",<br/><br/>";
                if (AddDeduct == "true")
                {
                    Html = Html + "Your" + " " + "adjustment of" + " " + NoOfDays + " " + "days is added to your applied" + " " + LeaveName + " " + "leave" + ". <br/>Description: " + Description + "<br/><br/>";
                }
                else
                {
                    Html = Html + "Your" + " " + "adjustment of" + " " + NoOfDays + " " + "days has been deducted from your applied" + " " + LeaveName + " " + "leave" + ". <br/>Description: " + Description + "<br/><br/>";
                }

                Html = Html + "Best Regards, <br/> Red Dot Distribution";                Tomail = "avijeet@reddotdistribution.com";                cc = "pratim.d@reddotdistribution.com" + "," + "mainak@reddotdistribution.com";                SendMail.Send(Tomail, cc, Subject, Html, true);            }            catch (Exception ex)            {                throw ex;            }            return RedirectToAction("Index", "RDD_LeaveAdjustment");        }
      
        public ActionResult GetCountryWiseLeaveType(int EmployeeId)
        {
            ContentResult retVal = null;            DataSet DS;            try            {                DS = rDD_LeaveAdjustment_TemplatesDb.CountryLeaveType(EmployeeId);                if (DS.Tables.Count > 0)                {                    retVal = Content(JsonConvert.SerializeObject(DS), "application/json");                }                return retVal;            }            catch (Exception ex)            {                throw ex;            }
        }
    }
}