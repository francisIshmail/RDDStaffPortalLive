using RDDStaffPortal.DAL.Admin;
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
    public class DSR_NextActionController : Controller
    {
        // GET: DailyReports/DSR_NextAction

        RDD_DSR_NextAction_Db_Operation rDD_Approval_TemplatesDB = new RDD_DSR_NextAction_Db_Operation();
        public ActionResult Index()
        {
            return View();
        }
        
        public ActionResult GetDSR_NextList()
        {

            List<RDD_DSR_NextAction> modules = new List<RDD_DSR_NextAction>();
            modules = rDD_Approval_TemplatesDB.GetList();
            return PartialView(modules);
        }
        [Route("SaveDSR_NextAction")]
        public ActionResult SaveDSR_NextAction(RDD_DSR_NextAction rDD_DSR_Next)
        {
            rDD_DSR_Next.CreatedBy = User.Identity.Name;
            rDD_DSR_Next.CreatedOn = DateTime.Now;
            if (rDD_DSR_Next.Editflag == true)
            {
                rDD_DSR_Next.LastUpdatedBy = User.Identity.Name;
                rDD_DSR_Next.LastUpdatedOn = System.DateTime.Now;
            }
            return Json(new { data =rDD_Approval_TemplatesDB.Save(rDD_DSR_Next) }, JsonRequestBehavior.AllowGet);
        }

        [Route("GetDSR_NextAction")]
        public ActionResult GetData(string ids)
        {          
            return Json(new { data = rDD_Approval_TemplatesDB.GetData(ids)}, JsonRequestBehavior.AllowGet);
        }

        [Route("DeleteDSR_NextAction")]
        public ActionResult DeleteFlag(string ids)
        {
            return Json(new { data = rDD_Approval_TemplatesDB.DeleteFlag(ids) }, JsonRequestBehavior.AllowGet);
        }

    }
}