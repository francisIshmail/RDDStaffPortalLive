using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using RDDStaffPortal.Areas.Admin.Models;
using RDDStaffPortal.DAL.InitialSetup;
using RDDStaffPortal.DAL.DataModels;

namespace RDDStaffPortal.Areas.Admin.Controllers
{
    public class JobGradeController : Controller
    {
        JobGradeDbOperations JobGradeDbOp = new JobGradeDbOperations();

        // MenusDbOperation menuDbOp = new MenusDbOperation();

        // GET: Admin/Departments
        public ActionResult Index()
        {
            return View();
        }

        public JsonResult AddJobGrade(JobGrade dataJobGrade)
        {
            string result = string.Empty;
            try
            {
                RDD_JobGrade rdd_jobgrade = new RDD_JobGrade();


                rdd_jobgrade.JobGradeId = dataJobGrade.JobGradeId;
                rdd_jobgrade.JobGradeName = dataJobGrade.JobGradeName;
                rdd_jobgrade.IsActive = dataJobGrade.IsActive;
                rdd_jobgrade.CreatedBy = User.Identity.Name;
                result = JobGradeDbOp.Save(rdd_jobgrade);

            }
            catch (Exception ex)
            {
                result = "Error occured :" + ex.Message;
            }
            return Json(result, JsonRequestBehavior.AllowGet);
        }
        public JsonResult DeleteItem(int JobGradeId)
        {
            string result = string.Empty;
            try
            {

                result = JobGradeDbOp.Delete(JobGradeId);

            }
            catch (Exception ex)
            {
                result = "Error occured :" + ex.Message;
            }
           // return Json(result, JsonRequestBehavior.AllowGet);
            return Json(new { DeleteFlag = result }, JsonRequestBehavior.AllowGet);
        }

        public JsonResult GetJobGradeList()
        {
            return Json(JobGradeDbOp.GetJobGradeList(), JsonRequestBehavior.AllowGet);
        }

        public ActionResult BindModules()
        {
            List<RDD_JobGrade> modules = new List<RDD_JobGrade>();
            modules = JobGradeDbOp.GetJobGradeList();
            return Json(modules, JsonRequestBehavior.AllowGet);

        }

    }
}
