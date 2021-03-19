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
    public class CategoryAssignToEmployeeController : Controller
    {
        RDD_CategoryAssignToEmployee_DbOperation rDD_AssignCategory_TemplateDb = new RDD_CategoryAssignToEmployee_DbOperation();
        // GET: PerformanceEvaluation/CategoryAssignToEmployee
        public ActionResult Index()
        {
            return View();
        }

        public ActionResult GetCatEmpDetails()
        {
            ContentResult retVal = null;
            DataSet ds;
            try
            {
                ds = rDD_AssignCategory_TemplateDb.GetCatEmpDetails();
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
                
        public ActionResult GetUserListAuto(string Prefix)
        {
            return Json(rDD_AssignCategory_TemplateDb.GetUserListAuto(Prefix), JsonRequestBehavior.AllowGet);
        }

        public ActionResult GetCategoryList()
        {
            ContentResult retVal = null;
            DataSet ds;
            try
            {
                ds = rDD_AssignCategory_TemplateDb.GetCategoryList();
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

        public ActionResult SaveAssignCategoryDetails(RDD_CategoryAssignToEmployee rDD_AssignCategory)
        {
            rDD_AssignCategory.CreatedBy = User.Identity.Name;
            if (rDD_AssignCategory.EditFlag == true)
            {
                rDD_AssignCategory.LastUpdatedBy = User.Identity.Name;
                rDD_AssignCategory.LastUpdatedOn = System.DateTime.Now;
            }
            return Json(rDD_AssignCategory_TemplateDb.SaveAssignCategoryDetails(rDD_AssignCategory), JsonRequestBehavior.AllowGet);
        }

        public ActionResult DeleteAssignCategory(int EmployeeId)
        {
            return Json(rDD_AssignCategory_TemplateDb.DeleteAssignCategory(EmployeeId), JsonRequestBehavior.AllowGet);
        }
    }
}