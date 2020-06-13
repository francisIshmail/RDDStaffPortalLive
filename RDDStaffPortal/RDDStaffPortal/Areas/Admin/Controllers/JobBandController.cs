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
    public class JobBandController : Controller
    {

        JobBandDbOperations JobBandDbOp = new JobBandDbOperations();

        
        public ActionResult Index()
        {
            return View();
        }

        public JsonResult AddJobBand(JobBand dataJobBand)
        {
            string result = string.Empty;
            try
            {
                RDD_JobBand rdd_jobband = new RDD_JobBand();


                rdd_jobband.JobBandId = dataJobBand.JobBandId;
                rdd_jobband.JobBandName = dataJobBand.JobBandName;
                rdd_jobband.IsActive = dataJobBand.IsActive;
                rdd_jobband.CreatedBy = User.Identity.Name;
                result = JobBandDbOp.Save(rdd_jobband);

            }
            catch (Exception ex)
            {
                result = "Error occured :" + ex.Message;
            }
            return Json(result, JsonRequestBehavior.AllowGet);
        }
        public JsonResult DeleteItem(int JobBandId)
        {
            string result = string.Empty;
            try
            {

                result = JobBandDbOp.Delete(JobBandId);

            }
            catch (Exception ex)
            {
                result = "Error occured :" + ex.Message;
            }
            return Json(new { DeleteFlag = result }, JsonRequestBehavior.AllowGet);
        }

        public JsonResult GetJobBandList()
        {
            return Json(JobBandDbOp.GetJobBandList(), JsonRequestBehavior.AllowGet);
        }

        //public ActionResult BindModules()
        //{
        //    List<RDD_JobGrade> modules = new List<RDD_JobGrade>();
        //    modules = JobBandDbOp.GetJobBandList();
        //    return Json(modules, JsonRequestBehavior.AllowGet);

        //}

    }
}