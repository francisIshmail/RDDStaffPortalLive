using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using System.Data.SqlClient;
using RDDStaffPortal.DAL;
using RDDStaffPortal.DAL.PerformanceEvaluation;
using RDDStaffPortal.DAL.DataModels.PerformanceEvaluation;
using Newtonsoft.Json;
using System.IO;

namespace RDDStaffPortal.Areas.PerformanceEvaluation.Controllers
{
    public class EmployeeRatingController : Controller
    {
        RDD_Employeerating_DbOperation rDD_EmpRating_TemplateDb = new RDD_Employeerating_DbOperation();
        CommonFunction cf = new CommonFunction();
        // GET: PerformanceEvaluation/EmployeeRating
        public ActionResult Index()
        {
            return View();
        }
        public ActionResult GetEmployeeDetails()
        {
            ContentResult retVal = null;
            DataSet ds;
            string LoginName = User.Identity.Name;
            try
            {
                ds = rDD_EmpRating_TemplateDb.GetEmployeeDetails(LoginName);
                if (ds.Tables.Count > 0)
                {
                    retVal = Content(JsonConvert.SerializeObject(ds), "application/json");
                }
                return retVal;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
        public ActionResult GetCategoryList()
        {
            ContentResult retVal = null;
            DataSet ds;
            string LoginName = User.Identity.Name;
            try
            {
                ds = rDD_EmpRating_TemplateDb.GetCategoryList(LoginName);
                if (ds.Tables.Count > 0)
                {
                    retVal = Content(JsonConvert.SerializeObject(ds), "application/json");
                }
                return retVal;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
        public ActionResult GetQuestionList(int CategoryId,string Qperiod)
        {
            ContentResult retVal = null;
            DataSet ds;
            string LoginName = User.Identity.Name;
            try
            {
                ds = rDD_EmpRating_TemplateDb.GetQuestionList(CategoryId,LoginName,Qperiod);
                if (ds.Tables.Count > 0)
                {
                    retVal = Content(JsonConvert.SerializeObject(ds), "application/json");
                }
                return retVal;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
        public ActionResult SaveEmployeeRating(RDD_EmployeeRating rDD_EmpAppraisal)
        {
            rDD_EmpAppraisal.Emp_SubmittedBy = User.Identity.Name;
            if (rDD_EmpAppraisal.EditFlag == true)
            {
                //rDD_EmpAppraisal.E = User.Identity.Name;
                rDD_EmpAppraisal.Emp_LastUpdatedOn = System.DateTime.Now;
            }
            return Json(rDD_EmpRating_TemplateDb.SaveEmployeeRating(rDD_EmpAppraisal), JsonRequestBehavior.AllowGet);
        }
        [Route("FinalSaveEmployeeRating")]
        public ActionResult FinalSaveEmployeeRating(int EmployeeId,int Year,string Period)
        {
            //ContentResult retVal = null;
            var t = rDD_EmpRating_TemplateDb.FinalSaveEmployeeRating(EmployeeId, Year, Period);
            if (t[0].Id != -1)
            {
                string MailResponse = "";
                try
                {
                    DataSet ds = new DataSet();
                    ds = rDD_EmpRating_TemplateDb.GetMailDetails(EmployeeId, Period, Year);
                    string ToMail = ds.Tables[1].Rows[0]["ToMail"].ToString();
                    string cc = ds.Tables[2].Rows[0]["CC"].ToString();
                    string MailBody = ds.Tables[3].Rows[0]["Body"].ToString();
                    string Subject = ds.Tables[0].Rows[0]["MailSubject"].ToString();
                    MailResponse = SendMail.Send(ToMail, cc, Subject, MailBody, true);
                }
                catch (Exception ex)
                {
                    MailResponse = ex.Message;                  
                }                
            }
            else
            {
                t[0].Outtf = false;
            }
            return Json(t, JsonRequestBehavior.AllowGet);
        }

        public ActionResult GetDetailsByYearAndPeriod(string Qperiod)
        {
            ContentResult retVal = null;
            DataSet ds;            
            try
            {
                string Loginname = User.Identity.Name;
                ds = rDD_EmpRating_TemplateDb.GetDetailsByYearAndPeriod(Qperiod,Loginname);
                if (ds.Tables.Count > 0)
                {
                    retVal = Content(JsonConvert.SerializeObject(ds), "application/json");
                }
                return retVal;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public ActionResult ViewEmployeerating()
        {
            return View();
        }

        public ActionResult ViewEmployeeRatingData(string UrlId)
        {
            ContentResult retVal = null;
            DataSet ds;
            try
            {
                string Loginname = User.Identity.Name;
                ds = rDD_EmpRating_TemplateDb.GetDetailsByClickUrl(UrlId);
                if (ds.Tables.Count > 0)
                {
                    retVal = Content(JsonConvert.SerializeObject(ds), "application/json");
                }
                return retVal;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public ActionResult GetCategoryWiseDetailsOnClickUrl(int CategoryId, string UrlId)
        {
            ContentResult retVal = null;
            DataSet ds;
            string LoginName = User.Identity.Name;
            try
            {
                ds = rDD_EmpRating_TemplateDb.GetCategoryWiseDetailsOnClickUrl(CategoryId, UrlId);
                if (ds.Tables.Count > 0)
                {
                    retVal = Content(JsonConvert.SerializeObject(ds), "application/json");
                }
                return retVal;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
    }
}