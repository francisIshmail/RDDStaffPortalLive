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
    public class ManagerRatingController : Controller
    {
        RDD_Managerrating_DbOperation rDD_MngRating_TemplateDb = new RDD_Managerrating_DbOperation();
        // GET: PerformanceEvaluation/ManagerRating
        public ActionResult Index()
        {
            return View();
        }
        public ActionResult GetDetailsForManager()
        {
            ContentResult retVal = null;
            DataSet ds;
            string LoginName = User.Identity.Name;
            try
            {
                ds = rDD_MngRating_TemplateDb.GetDetailsForManager(LoginName);
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

        public ActionResult GetDetailsForManagerByChangePeriods(string Qperiod)
        {
            ContentResult retVal = null;
            DataSet ds;
            string LoginName = User.Identity.Name;
            try
            {
                ds = rDD_MngRating_TemplateDb.GetDetailsForManagerByChangePeriods(LoginName, Qperiod);
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

        public ActionResult GetEmployeeRating(int EmpId,string Qperiods)
        {
            ContentResult retVal = null;
            DataSet ds;            
            try
            {
                ds = rDD_MngRating_TemplateDb.GetEmployeeRating(EmpId,Qperiods);
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

        public ActionResult GetPartial()
        {
            return PartialView("~/Areas/PerformanceEvaluation/Views/ManagerRating/ManagerRatingPartial.cshtml");
        }

        public ActionResult GetQuestionList(int CategoryId, string Qperiod, int EmpId)
        {
            ContentResult retVal = null;
            DataSet ds;
            string LoginName = User.Identity.Name;
            try
            {
                ds = rDD_MngRating_TemplateDb.GetQuestionList(CategoryId, Qperiod, EmpId);
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

        public ActionResult SaveManagerRating(RDD_EmployeeRating rDD_MngAppraisal)
        {
            rDD_MngAppraisal.Emp_SubmittedBy = User.Identity.Name;
            if (rDD_MngAppraisal.EditFlag == true)
            {
                //rDD_EmpAppraisal.E = User.Identity.Name;
                rDD_MngAppraisal.Emp_LastUpdatedOn = System.DateTime.Now;
            }
            return Json(rDD_MngRating_TemplateDb.SaveManagerRating(rDD_MngAppraisal), JsonRequestBehavior.AllowGet);
        }
        [Route("FinalSaveManagerRating")]
        public ActionResult FinalSaveEmployeeRating(int EmployeeId, int Year, string Period)
        {
            //ContentResult retVal = null;
            var t = rDD_MngRating_TemplateDb.FinalSaveManagerRating(EmployeeId, Year, Period);
            if (t[0].Id != -1)
            {
                string MailResponse = "";
                try
                {
                    DataSet ds = new DataSet();
                    ds = rDD_MngRating_TemplateDb.GetMailDetails(EmployeeId, Period, Year);
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

        public ActionResult GetManagerRatingViewOnClickUrl(string UrlId)
        {
            ContentResult retVal = null;
            DataSet ds;
            string LoginName = User.Identity.Name;
            try
            {
                ds = rDD_MngRating_TemplateDb.GetManagerRatingViewOnClickUrl(UrlId);
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