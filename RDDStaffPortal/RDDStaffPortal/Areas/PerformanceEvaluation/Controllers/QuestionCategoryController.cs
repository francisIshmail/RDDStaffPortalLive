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

namespace RDDStaffPortal.Areas.PerformanceEvaluation.Controllers
{
    public class QuestionCategoryController : Controller
    {
        RDD_QuestionCategory_DbOperation rDD_QuestionCategory_TemplateDb = new RDD_QuestionCategory_DbOperation();
        // GET: PerformanceEvaluation/QuestionCategory
        public ActionResult Index()
        {
            return View();
        }
        public ActionResult GetDepartmentList()
        {
            ContentResult retVal = null;
            DataSet ds;
            try
            {
                ds = rDD_QuestionCategory_TemplateDb.GetDepartmentList();
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

        public ActionResult SaveCategoryDetails(RDD_QuestionCategory rDD_Category)
        {
            rDD_Category.CreatedBy = User.Identity.Name;
            if (rDD_Category.EditFlag == true)
            {
                rDD_Category.LastUpdatedBy = User.Identity.Name;
                rDD_Category.LastUpdatedOn = System.DateTime.Now;
            }
            return Json(rDD_QuestionCategory_TemplateDb.SaveCategory(rDD_Category), JsonRequestBehavior.AllowGet);
        }

        public ActionResult DeleteCategory(int Categoryid)
        {            
            return Json(rDD_QuestionCategory_TemplateDb.DeleteCategory(Categoryid), JsonRequestBehavior.AllowGet);
        }

        public ActionResult GetCategoryDetails()
        {
            ContentResult retVal = null;
            DataSet ds;
            try
            {
                ds = rDD_QuestionCategory_TemplateDb.GetCategoryDetails();
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