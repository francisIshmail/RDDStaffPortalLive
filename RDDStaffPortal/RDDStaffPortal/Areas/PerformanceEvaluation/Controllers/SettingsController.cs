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
    public class SettingsController : Controller
    {
        RDD_Settings_DbOperation rDD_Settings_TemplateDb = new RDD_Settings_DbOperation();
        // GET: PerformanceEvaluation/Settings
        public ActionResult Index()
        {
            return View();
        }

        public ActionResult GetSettingsDetails()
        {
            ContentResult retVal = null;
            DataSet ds;
            try
            {
                ds = rDD_Settings_TemplateDb.GetSettingsDetails();
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

        public ActionResult SaveAppraisalSettings(RDD_Settings rDD_Setting)
        {
            rDD_Setting.CreatedBy = User.Identity.Name;
            if (rDD_Setting.EditFlag == true)
            {
                rDD_Setting.LastUpdatedBy = User.Identity.Name;
                rDD_Setting.LastUpdatedOn = System.DateTime.Now;
            }
            return Json(rDD_Settings_TemplateDb.SaveAppraisalSettings(rDD_Setting), JsonRequestBehavior.AllowGet);
        }
    }
}