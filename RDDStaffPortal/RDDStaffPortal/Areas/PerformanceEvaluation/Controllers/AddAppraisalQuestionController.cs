using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using System.Data;
using System.Data.SqlClient;
using RDDStaffPortal.DAL;
using RDDStaffPortal.DAL.PerformanceEvaluation;
using RDDStaffPortal.DAL.DataModels.PerformanceEvaluation;
using Newtonsoft.Json;
using System.IO;
using OfficeOpenXml;

namespace RDDStaffPortal.Areas.PerformanceEvaluation.Controllers
{
    public class AddAppraisalQuestionController : Controller
    {
        RDD_AddAppraisalQuestion_DbOperation rDD_AppraisalQuestion_TemplateDb = new RDD_AddAppraisalQuestion_DbOperation();
        // GET: PerformanceEvaluation/AddAppraisalQuestion
        public ActionResult Index()
        {
            return View();
        }

        public ActionResult GetQuestionDetails()
        {
            ContentResult retVal = null;
            DataSet ds;            
            try
            {
                ds = rDD_AppraisalQuestion_TemplateDb.GetQuestionDetails();
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

        public ActionResult GetCategorywiseQuestionList(int CategoryId, string Qperiod)
        {
            ContentResult retVal = null;
            DataSet ds;
            string LoginName = User.Identity.Name;
            try
            {
                ds = rDD_AppraisalQuestion_TemplateDb.GetCategorywiseQuestionList(CategoryId, Qperiod);
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

        public ActionResult GetPeriodCategoryList()
        {
            ContentResult retVal = null;
            DataSet ds;
            try
            {
                ds = rDD_AppraisalQuestion_TemplateDb.GetPeriodCategoryList();
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

        public ActionResult GetPreviousPeriodQuestion(string PrevPeriod,string CategoryId)
        {
            ContentResult retVal = null;
            DataSet ds;
            try
            {
                ds = rDD_AppraisalQuestion_TemplateDb.GetPreviousPeriodQuestion(PrevPeriod, CategoryId);
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

        public ActionResult GetQuestionByPeriodYear(string Qperiodyear)
        {
            ContentResult retVal = null;
            DataSet ds;
            try
            {
                ds = rDD_AppraisalQuestion_TemplateDb.GetQuestionByPeriodYear(Qperiodyear);
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

        public ActionResult Download()
        {
            string fullPath = Path.Combine(Server.MapPath("~/excelFileUpload/PerformanceEvaluation"), "Employee Performance Appraisal Question Format.xlsx");
            return File(fullPath, "application/vnd.ms-excel", "Employee Performance Appraisal Question Format.xlsx");            
        }

        public ActionResult GetExcelData()
        {
            CommonFunction Com = new CommonFunction();
            try
            {
                string Msg = "";
                if (Request.Files.Count > 0)
                {
                    List<RDD_AddAppraisalQuestion_ExcelData> docs = new List<RDD_AddAppraisalQuestion_ExcelData>();
                    HttpFileCollectionBase Files = Request.Files;
                    for (int i = 0; i < Files.Count; i++)
                    {
                        HttpPostedFileBase FileDetails = Files[i];
                        DataTable dt = new DataTable();
                        ExcelPackage excelpack = new ExcelPackage(FileDetails.InputStream);
                        dt = Com.ExcelToDataTable(excelpack);
                        dt = Com.RemoveBlankRow(dt);
                        dt = Com.ChangeColumnDataType(dt);
                        dt = Com.SettiingDataTableHeaderAsList(dt, docs);
                        docs = Com.ConvertDataTableToClassObjectList<RDD_AddAppraisalQuestion_ExcelData>(dt);
                    }
                    return Json(docs, JsonRequestBehavior.AllowGet);
                }
                else
                {
                    Msg = "Error";
                }
                return Json(Msg, JsonRequestBehavior.AllowGet);
            }
            catch (Exception ex)
            {
                return Json(Convert.ToString(ex.Message), JsonRequestBehavior.AllowGet);
            }
        }

        public ActionResult SaveAppraisalQuestion(RDD_AddAppraisalQuestion rDD_Question)
        {
            rDD_Question.CreatedBy = User.Identity.Name;
            if (rDD_Question.EditFlag == true)
            {
                rDD_Question.LastUpdatedBy = User.Identity.Name;
                rDD_Question.LastUpdatedOn = System.DateTime.Now;
            }
            return Json(rDD_AppraisalQuestion_TemplateDb.SaveAssignCategoryDetails(rDD_Question), JsonRequestBehavior.AllowGet);
        }

        public ActionResult UpdateAppraisalQuestion(string Qid,string Question)
        {            
            return Json(rDD_AppraisalQuestion_TemplateDb.UpdateAssignCategoryDetails(Qid,Question), JsonRequestBehavior.AllowGet);
        }

        public ActionResult DeleteAppraisalQuestion(int Qid)
        {
            return Json(rDD_AppraisalQuestion_TemplateDb.DeleteAppraisalQuestion(Qid), JsonRequestBehavior.AllowGet);
        }
        
        public ActionResult LaunchAppraisal(string Qperiod,DateTime AppraisalEndDate)
        {
            var t = rDD_AppraisalQuestion_TemplateDb.LaunchAppraisal(Qperiod, AppraisalEndDate);
            if (t[0].Id != -1)
            {
                string Mailresponse = "";
                try
                {
                    DataSet ds = new DataSet();
                    ds = rDD_AppraisalQuestion_TemplateDb.GetMailDetails(Qperiod);
                    string ToMail = ds.Tables[0].Rows[0]["EmployeeMail"].ToString();
                    string MailSubject = ds.Tables[1].Rows[0]["MailSubject"].ToString();
                    string MailBody = ds.Tables[2].Rows[0]["Body"].ToString();
                    Mailresponse = SendMail.Send(ToMail, "", MailSubject, MailBody, true);
                }
                catch (Exception ex)
                {
                    Mailresponse = ex.Message;
                    t[0].Outtf = false;
                    t[0].Responsemsg = Mailresponse;
                }
            }
            else
            {
                t[0].Outtf = false;
            }
            return Json(t, JsonRequestBehavior.AllowGet);
        }

        public ActionResult CloseAppraisal(string Qperiod)
        {
            var t = rDD_AppraisalQuestion_TemplateDb.CloseAppraisal(Qperiod);
            if (t[0].Id != -1)
            {
                string Mailresponse = "";
                try
                {
                    DataSet ds = new DataSet();
                    ds = rDD_AppraisalQuestion_TemplateDb.GetMailDetailsCloseAppraisal(Qperiod);
                    string ToMail = ds.Tables[0].Rows[0]["EmployeeMail"].ToString();
                    string MailSubject = ds.Tables[1].Rows[0]["MailSubject"].ToString();
                    string MailBody = ds.Tables[2].Rows[0]["Body"].ToString();
                    Mailresponse = SendMail.Send(ToMail, "", MailSubject, MailBody, true);
                }
                catch (Exception ex)
                {
                    Mailresponse = ex.Message;
                }
            }
            else
            {
                t[0].Outtf = false;
            }
            return Json(t, JsonRequestBehavior.AllowGet);
        }
    }
}