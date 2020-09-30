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
    public class DSR_ModeOfCallController : Controller
    {
        // GET: DailyReports/RDD_DSR_ModeOfCall
        RDD_DSR_ModeOfCall_Db_Operation rDD_DSR_ModeOfCallDB = new RDD_DSR_ModeOfCall_Db_Operation();
        public ActionResult Index()
        {
            return View();
        }

        public ActionResult GetDSR_ModeOfCallList()
        {

            List<RDD_DSR_ModeOfCall> modules = new List<RDD_DSR_ModeOfCall>();
            modules = rDD_DSR_ModeOfCallDB.GetList();
            return PartialView(modules);
        }
        [Route("SaveDSR_ModeOfCall")]
        public ActionResult SaveDSR_NextAction(RDD_DSR_ModeOfCall rDD_ModeOfCall)
        {
            rDD_ModeOfCall.CreatedBy = User.Identity.Name;
            rDD_ModeOfCall.CreatedOn = DateTime.Now;
            if (rDD_ModeOfCall.Editflag == true)
            {
                rDD_ModeOfCall.LastUpdatedBy = User.Identity.Name;
                rDD_ModeOfCall.LastUpdatedOn = System.DateTime.Now;
            }
            return Json(new { data = rDD_DSR_ModeOfCallDB.Save(rDD_ModeOfCall) }, JsonRequestBehavior.AllowGet);
        }

        [Route("GetDSR_ModeOfCall")]
        public ActionResult GetData(string ids)
        {
            return Json(new { data = rDD_DSR_ModeOfCallDB.GetData(ids) }, JsonRequestBehavior.AllowGet);
        }

        [Route("DeleteDSR_ModeOfCall")]
        public ActionResult DeleteFlag(string ids)
        {
            return Json(new { data = rDD_DSR_ModeOfCallDB.DeleteFlag(ids) }, JsonRequestBehavior.AllowGet);
        }
    }
}