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
    [Authorize]
    public class IncentiveSlabController : Controller
    {
        RDD_IncentiveSlab_DbOperation IslabDbOp = new RDD_IncentiveSlab_DbOperation();
        // GET: Incentive/IncentiveSlab
        public ActionResult Index()
        {
            return View();
        }
        //public ActionResult GetPartial()
        //{
        //    return View("~/Areas/Incentive/Views/IncentiveSlab/PartialView_IncentiveSlab.cshtml");
        //}
        public ActionResult GetYearList()
        {            
            ContentResult retVal = null;
            DataSet DS;
            try
            {
                DS = IslabDbOp.FillDropdown();

                if (DS.Tables.Count > 0)
                {
                    retVal = Content(JsonConvert.SerializeObject(DS), "application/json");
                }
                return retVal;

            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
        public ActionResult SaveIncentiveSlab(RDD_IncentiveSlab RDD_Incentive)
        {
            if (RDD_Incentive.EditFlag == true)
            {
                RDD_Incentive.LastUpdatedBy = User.Identity.Name;
                RDD_Incentive.LastUpdatedOn = DateTime.Now;
            }
            else
            {
                RDD_Incentive.CreatedBy = User.Identity.Name;
                //RDD_Incentive.LastUpdatedBy = User.Identity.Name;
            }
            return Json(IslabDbOp.Save1(RDD_Incentive), JsonRequestBehavior.AllowGet);
        }
        [Route("GETSLAB")]
        public ActionResult GetDataSlab(int pagesize, int pageno, string psearch)
        {
            List<RDD_IncentiveSlab> rDD_IncentiveSlab_s = new List<RDD_IncentiveSlab>();
            rDD_IncentiveSlab_s = IslabDbOp.GetALLDATA(User.Identity.Name, pagesize, pageno, psearch);
            return Json(new { data = rDD_IncentiveSlab_s }, JsonRequestBehavior.AllowGet);
        }
        [Route("ADDSLAB")]
        public ActionResult ADDIncentiveSlab(int TEMPId = -1)
        {
            RDD_IncentiveSlab RDD_Islab = new RDD_IncentiveSlab();
            RDD_Islab.SaveFlag = false;
            RDD_Islab.CreatedOn = DateTime.Now;
            RDD_Islab.CreatedBy = User.Identity.Name;

            if (TEMPId != -1)
            {
                RDD_Islab = IslabDbOp.GetData(User.Identity.Name, TEMPId, IslabDbOp.GetDropList(User.Identity.Name, "E"));
                RDD_Islab.EditFlag = true;
            }
            else
            {
                RDD_Islab.EditFlag = false;
                RDD_Islab = IslabDbOp.GetDropList(User.Identity.Name, "N");
            }
            return PartialView("~/Areas/Incentive/Views/IncentiveSlab/ADDIncentiveSlab.cshtml", RDD_Islab);
        }
    }
}