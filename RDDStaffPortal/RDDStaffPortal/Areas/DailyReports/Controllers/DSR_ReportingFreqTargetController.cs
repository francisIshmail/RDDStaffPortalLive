using Newtonsoft.Json;
using RDDStaffPortal.DAL.DailyReports;
using RDDStaffPortal.DAL.DataModels.DailyReports;
using System;
using System.Data;
using System.Web.Mvc;
using System.Web.Routing;

namespace RDDStaffPortal.Areas.DailyReports.Controllers
{
    [Authorize]
    public class DSR_ReportingFreqTargetController : Controller
    {

        RDD_DSR_ReportingFreqTarget_Db_Operation rDD_DSR_Db = new RDD_DSR_ReportingFreqTarget_Db_Operation();
        // GET: DailyReports/DSR_ReportingFreqTarget
        public ActionResult Index()
        {
            RDD_DSR_ReportingFreqTarget rDD_DSR = new RDD_DSR_ReportingFreqTarget();
            rDD_DSR = rDD_DSR_Db.GetDropList(User.Identity.Name);
            return View(rDD_DSR);
        }
        [Route("GetRDD_DSR_FREQ_List")]
        public ActionResult GetRDD_DSR_FREQ_List(string CountryCode)
        {
            ContentResult retVal = null;
            try
            {
                DataSet DS = rDD_DSR_Db.GetReportingTarget(User.Identity.Name, CountryCode);
                if (DS.Tables.Count > 0)
                    retVal = Content(JsonConvert.SerializeObject(DS), "application/json");
                else
                    retVal = null;
            }
            catch (Exception)
            {
                retVal = null;
            }

            return retVal;


        }

        [Route("SaveRDD_DSR_FREQ")]
        public ActionResult SaveRDD_DSR_FREQ(RDD_DSR_ReportingFreqTarget rDD_DSR)
        {

            return Json(new { data = rDD_DSR_Db.Save(rDD_DSR) }, JsonRequestBehavior.AllowGet);

        }

        // [Route("ScoreCard")]
        public ActionResult DSR_Report()
        {
            RDD_DSR_ReportingFreqTarget rDD_DSR = new RDD_DSR_ReportingFreqTarget();
            rDD_DSR = rDD_DSR_Db.GetDropList(User.Identity.Name);
            return View(rDD_DSR);
        }
        [Route("GetRDD_Person")]
        public ActionResult GetPerson(string Country)
        {
            ContentResult retVal = null;
            try
            {
                DataSet DS = rDD_DSR_Db.GetPerson(User.Identity.Name, Country);
                if (DS.Tables.Count > 0)
                    retVal = Content(JsonConvert.SerializeObject(DS), "application/json");
                else
                    retVal = null;
            }
            catch (Exception)
            {
                retVal = null;
            }

            return retVal;

        }
        [Route("GetRDD_PersonData")]
        public ActionResult GetPersonData(string Person, int month, int year)
        {
            ContentResult retVal = null;
            try
            {
                DataSet DS = rDD_DSR_Db.GetPersonData(Person, year, month);
                if (DS.Tables.Count > 0)
                    retVal = Content(JsonConvert.SerializeObject(DS), "application/json");
                else
                    retVal = null;
            }
            catch (Exception)
            {
                retVal = null;
            }

            return retVal;

        }
    }
}