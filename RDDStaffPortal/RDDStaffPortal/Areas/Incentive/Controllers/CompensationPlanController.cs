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
    public class CompensationPlanController : Controller
    {
        RDD_CompensationPlan_DbOperation CompPlanDbOp = new RDD_CompensationPlan_DbOperation();
        // GET: Incentive/CompensationPlan
        public ActionResult Index()
        {
            return View();
        }
        //public ActionResult GetYearList()
        //{
        //    RDD_CompensationPlan RDD_CompPlan = new RDD_CompensationPlan();
        //    RDD_CompPlan.CreatedBy = User.Identity.Name;
        //    //string Users= User.Identity.Name;
        //    RDD_CompPlan = CompPlanDbOp.GetDropList(User.Identity.Name);
        //    return Json(RDD_CompPlan, JsonRequestBehavior.AllowGet);
        //}
        [Route("ADDCOMPPLAN")]
        public ActionResult ADDCompensationPlan(int TEMPId = -1)
        {
            RDD_CompensationPlan RDD_CompPlan = new RDD_CompensationPlan();
            RDD_CompPlan.SaveFlag = false;
            RDD_CompPlan.CreatedOn = DateTime.Now;
            RDD_CompPlan.CreatedBy = User.Identity.Name;

            if (TEMPId != -1)
            {
                //RDD_CompPlan = CompPlanDbOp.GetData(User.Identity.Name, TEMPId, CompPlanDbOp.GetDropList(User.Identity.Name, "E"));
                RDD_CompPlan.EditFlag = true;
            }
            else
            {
                RDD_CompPlan.EditFlag = false;
                RDD_CompPlan = CompPlanDbOp.GetDropList(User.Identity.Name, "N");
            }
            return PartialView("~/Areas/Incentive/Views/CompensationPlan/ADDCompensationPlan.cshtml", RDD_CompPlan);
        }
    }
}