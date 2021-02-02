using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using RDDStaffPortal.DAL.Incentive;
using RDDStaffPortal.DAL.DataModels.Incentive;
using System.Data;
using Newtonsoft.Json;
using System.Data.SqlClient;
using RDDStaffPortal.DAL;

namespace RDDStaffPortal.Areas.Incentive.Controllers
{
    public class Incentive_TandCController : Controller
    {
        RDD_IncentiveTNC_DbOperation ItncDbOp = new RDD_IncentiveTNC_DbOperation();
        // GET: Incentive/Incentive_TandC
        public ActionResult Index()
        {
            return View();
        }
        [Route("ADDTNC")]
        public ActionResult ADDIncentiveTNC(int TEMPId = -1)
        {
            RDD_IncentiveTNC RDD_Itnc = new RDD_IncentiveTNC();
            RDD_Itnc.SaveFlag = false;
            RDD_Itnc.CreatedOn = DateTime.Now;
            RDD_Itnc.CreatedBy = User.Identity.Name;

            if (TEMPId != -1)
            {
                RDD_Itnc = ItncDbOp.GetData(User.Identity.Name, TEMPId, ItncDbOp.GetDropList(User.Identity.Name, "E"));
                RDD_Itnc.EditFlag = true;
            }
            else
            {
                RDD_Itnc.EditFlag = false;
                RDD_Itnc = ItncDbOp.GetDropList(User.Identity.Name, "N");
            }
            return PartialView("~/Areas/Incentive/Views/Incentive_TandC/ADDIncentiveTNC.cshtml", RDD_Itnc);
        }
        public ActionResult SaveIncentiveTNC(RDD_IncentiveTNC RDD_IncentiveTNC)
        {
            if (RDD_IncentiveTNC.EditFlag == true)
            {
                RDD_IncentiveTNC.LastUpdatedBy = User.Identity.Name;
                RDD_IncentiveTNC.LastUpdatedOn = DateTime.Now;
            }
            else
            {
                RDD_IncentiveTNC.CreatedBy = User.Identity.Name;
                //RDD_Incentive.LastUpdatedBy = User.Identity.Name;
            }
            return Json(ItncDbOp.Save1(RDD_IncentiveTNC), JsonRequestBehavior.AllowGet);
        }
        [Route("GETTNC")]
        public ActionResult GetDataKPI(int pagesize, int pageno, string psearch)
        {
            List<RDD_IncentiveTNC> rDD_IncentiveTnc_s = new List<RDD_IncentiveTNC>();
            rDD_IncentiveTnc_s = ItncDbOp.GetALLDATA(User.Identity.Name, pagesize, pageno, psearch);
            return Json(new { data = rDD_IncentiveTnc_s }, JsonRequestBehavior.AllowGet);
        }
    }
}