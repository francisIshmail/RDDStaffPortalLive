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
//using DocumentFormat.OpenXml.Drawing;
using System.IO;

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

        public ActionResult Download()
        {
            string fullPath = Path.Combine(Server.MapPath("~/excelFileUpload/PerformanceEvaluation"), "Employee Performance Appraisal Question Format.xlsx");
            return File(fullPath, "application/vnd.ms-excel", "Employee Performance Appraisal Question Format.xlsx");            
        }
    }
}