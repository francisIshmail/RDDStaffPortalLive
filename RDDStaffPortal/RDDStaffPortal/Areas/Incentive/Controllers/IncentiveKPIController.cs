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
    public class IncentiveKPIController : Controller
    {
        RDD_IncentiveKPI_DbOperation IkpiDbOp = new RDD_IncentiveKPI_DbOperation();
        CommonFunction cf = new CommonFunction();
        // GET: Incentive/IncentiveKPI
        public ActionResult Index()
        {
            return View();
        }
        public ActionResult GetPartial()
        {
            return PartialView("~/Areas/Incentive/Views/IncentiveKPI/ADDIncentiveKPI.cshtml");
        }
        public ActionResult SaveKPIparameter(string _KpiParam)
        {
            return Json(IkpiDbOp.SaveKPIparameter(_KpiParam), JsonRequestBehavior.AllowGet);
        }
        public ActionResult GetDesignationList()
        {
            //return Json(IkpiDbOp.GetDesignationList(), JsonRequestBehavior.AllowGet);
            ContentResult retVal = null;
            DataSet DS;
            try
            {
                DS = IkpiDbOp.FillDropdown();

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
        public ActionResult SaveIncentiveKPI(RDD_IncentiveKPI RDD_Incentive)
        {
            if (RDD_Incentive.EditFlag == true)
            {
                RDD_Incentive.LastUpdatedBy = User.Identity.Name;
                RDD_Incentive.LastUpdatedOn = DateTime.Now;
            }
            else
            {
                RDD_Incentive.CreatedBy = User.Identity.Name;
                //RDD_Incentive.CreatedOn = DateTime.Now.ToString();
            }
            return Json(IkpiDbOp.Save1(RDD_Incentive), JsonRequestBehavior.AllowGet);
        }
        public ActionResult GetKPIdetail()
        {
            ContentResult retVal = null;
            DataSet ds;
            try
            {
                ds = IkpiDbOp.GetKPIdetails();
                if (ds.Tables.Count > 0)
                {
                    retVal = Content(JsonConvert.SerializeObject(ds), "application/json");
                }
                return retVal;
            }
            catch(Exception ex)
            {
                throw ex;
            }
        }
        public ActionResult DeleteKPIdetail(string _KPIParamId)
        {
            return Json(IkpiDbOp.DeleteKPIparameter(_KPIParamId), JsonRequestBehavior.AllowGet);
        }
        [Route("GETKPI")]
        public ActionResult GetDataKPI(int pagesize, int pageno, string psearch)
        {
            List<RDD_IncentiveKPI> rDD_IncentiveKpi_s = new List<RDD_IncentiveKPI>();
            rDD_IncentiveKpi_s = IkpiDbOp.GetALLDATA(User.Identity.Name, pagesize, pageno, psearch);
            return Json(new { data = rDD_IncentiveKpi_s }, JsonRequestBehavior.AllowGet);
        }
        [Route("ADDKPI")]
        public ActionResult ADDIncentiveKPI(int TEMPId = -1)
        {
            RDD_IncentiveKPI RDD_Ikpi = new RDD_IncentiveKPI();
            RDD_Ikpi.SaveFlag = false;
            RDD_Ikpi.CreatedOn = DateTime.Now;
            RDD_Ikpi.CreatedBy = User.Identity.Name;

            if (TEMPId != -1)
            {
                RDD_Ikpi = IkpiDbOp.GetData(User.Identity.Name, TEMPId, IkpiDbOp.GetDropList(User.Identity.Name, "E"));
                RDD_Ikpi.EditFlag = true;
            }
            else
            {
                RDD_Ikpi.EditFlag = false;
                RDD_Ikpi = IkpiDbOp.GetDropList(User.Identity.Name, "N");
            }
            return PartialView("~/Areas/Incentive/Views/IncentiveKPI/ADDIncentiveKPI.cshtml", RDD_Ikpi);
        }
    }
}