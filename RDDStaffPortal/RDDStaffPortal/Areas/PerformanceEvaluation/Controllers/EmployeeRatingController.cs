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
        public ActionResult GetQuestionList(int CategoryId)
        {
            ContentResult retVal = null;
            DataSet ds;
            string LoginName = User.Identity.Name;
            try
            {
                ds = rDD_EmpRating_TemplateDb.GetQuestionList(CategoryId);
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
    }
}