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

        public ActionResult GetEmployeeRating(int EmpId)
        {
            ContentResult retVal = null;
            DataSet ds;            
            try
            {
                ds = rDD_MngRating_TemplateDb.GetEmployeeRating(EmpId);
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
    }
}