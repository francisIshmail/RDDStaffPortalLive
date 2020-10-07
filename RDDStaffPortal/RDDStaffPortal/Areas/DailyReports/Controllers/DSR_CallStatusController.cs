using RDDStaffPortal.DAL.DailyReports;
using RDDStaffPortal.DAL.DataModels.DailyReports;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace RDDStaffPortal.Areas.DailyReports.Controllers
{
    [Authorize]
    public class DSR_CallStatusController : Controller
    {
        // GET: DailyReports/DSR_CallStatus
        RDD_DSR_CallStatus_Db_Operation rDD_DSR_CallStatusDB = new RDD_DSR_CallStatus_Db_Operation();
        public ActionResult Index()
        {
            return View();
        }

        public ActionResult GetDSR_CallStatusList()
        {

            List<RDD_DSR_CallStatus> modules = new List<RDD_DSR_CallStatus>();
            modules = rDD_DSR_CallStatusDB.GetList();
            return PartialView(modules);
        }
        [Route("SaveDSR_CallStatus")]
        public ActionResult SaveDSR_CallStatus(RDD_DSR_CallStatus rDD_CallStatus)
        {
            rDD_CallStatus.CreatedBy = User.Identity.Name;
            rDD_CallStatus.CreatedOn = DateTime.Now;
            if (rDD_CallStatus.Editflag == true)
            {
                rDD_CallStatus.LastUpdatedBy = User.Identity.Name;
                rDD_CallStatus.LastUpdatedOn = System.DateTime.Now;
            }
            return Json(new { data = rDD_DSR_CallStatusDB.Save(rDD_CallStatus) }, JsonRequestBehavior.AllowGet);
        }

        [Route("GetDSR_CallStatus")]
        public ActionResult GetData(string ids)
        {
            return Json(new { data = rDD_DSR_CallStatusDB.GetData(ids) }, JsonRequestBehavior.AllowGet);
        }

        [Route("DeleteDSR_CallStatus")]
        public ActionResult DeleteFlag(string ids)
        {
            return Json(new { data = rDD_DSR_CallStatusDB.DeleteFlag(ids) }, JsonRequestBehavior.AllowGet);
        }
    }
}