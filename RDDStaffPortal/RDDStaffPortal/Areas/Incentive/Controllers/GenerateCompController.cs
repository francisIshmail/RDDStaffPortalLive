using Newtonsoft.Json;
using RDDStaffPortal.DAL.DataModels.Incentive;
using RDDStaffPortal.DAL.Incentive;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace RDDStaffPortal.Areas.Incentive.Controllers
{
    public class GenerateCompController : Controller
    {
        RDD_GenerateCompensation_DbOperation GenCompDbOp = new RDD_GenerateCompensation_DbOperation();
        // GET: Incentive/GenerateComp
        public ActionResult Index()
        {            
            return View();            
        }
        [Route("ADDGENCOMPPLAN")]
        public ActionResult ADDGenerateCompensationPlan(int TEMPId = -1)
        {
            RDD_GenerateComp RDD_GenCompPlan = new RDD_GenerateComp();
            RDD_GenCompPlan.SaveFlag = false;
            RDD_GenCompPlan.CreatedOn = DateTime.Now;
            RDD_GenCompPlan.CreatedBy = User.Identity.Name;

            if (TEMPId != -1)
            {
                RDD_GenCompPlan = GenCompDbOp.GetData(User.Identity.Name, TEMPId, GenCompDbOp.GetDropList(User.Identity.Name, "E"));
                RDD_GenCompPlan.EditFlag = true;
            }
            else
            {
                RDD_GenCompPlan.EditFlag = false;
                RDD_GenCompPlan = GenCompDbOp.GetDropList(User.Identity.Name, "N");
            }
            return PartialView("~/Areas/Incentive/Views/GenerateComp/ADDGenerateCompensationPlan.cshtml", RDD_GenCompPlan);
        }
        //[Route("GETDESIGNATION")]
        public ActionResult GetDesignationDetails(int Empide)
        {
            DesignationLists DesigList = new DesignationLists();
            DesigList = GenCompDbOp.DesignationDetails(Empide);
            return Json(DesigList, JsonRequestBehavior.AllowGet);
        }
        [Route("GETKPITNC")]
        public ActionResult GetKpiTnc(int DesigId, string Period, int Years)
        {
            ContentResult retVal = null;
            DataSet ds;
            try
            {
                ds = GenCompDbOp.GetKpiTncs(DesigId, Period, Years);
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
        [Route("GETDEDUCTAMOUNT")]
        public ActionResult GetDeductAmount(int EmpId, string Period, int Years)
        {
            ContentResult retVal = null;
            DataSet ds;
            try
            {
                ds = GenCompDbOp.GetDeductAmount(EmpId, Period, Years);
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
        [Route("GETINCENTIVESLAB")]
        public ActionResult GetIncentiveSlab(int Empid, string Period, int Years)
        {
            ContentResult retVal = null;
            DataSet ds;
            try
            {
                ds = GenCompDbOp.GetIncentiveSlab(Empid, Period, Years);
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
        public ActionResult GetCompensationAmountDetails(int Empide, string Period, int Year)
        {
            ContentResult retVal = null;
            DataSet ds;
            try
            {
                ds = GenCompDbOp.GetCompAmount(Empide, Period, Year);
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

        [Route("GETBUCOMPDETAILS")]
        public ActionResult GetBuCompdetail(int EmployeeId, string Period, int Years)
        {
            ContentResult retVal = null;
            DataSet ds;
            try
            {
                ds = GenCompDbOp.GetBuCompdetails(EmployeeId, Period, Years);
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

        public ActionResult SaveCompensationCalculation(RDD_GenerateComp RDD_GenComp)
        {
            if (RDD_GenComp.EditFlag == true)
            {
                RDD_GenComp.LastUpdatedBy = User.Identity.Name;
                RDD_GenComp.LastUpdatedOn = DateTime.Now;
            }
            else
            {
                RDD_GenComp.CreatedBy = User.Identity.Name;
                //RDD_CompPlan.LastUpdatedBy = User.Identity.Name;
            }
            return Json(GenCompDbOp.Save1(RDD_GenComp), JsonRequestBehavior.AllowGet);
        }

        [Route("GETCOMPCAL")]
        public ActionResult GetDataCompCal(int pagesize, int pageno, string psearch)
        {
            List<RDD_GenerateComp> rDD_GenComp_s = new List<RDD_GenerateComp>();
            rDD_GenComp_s = GenCompDbOp.GetALLDATA(User.Identity.Name, pagesize, pageno, psearch);
            return Json(new { data = rDD_GenComp_s }, JsonRequestBehavior.AllowGet);
        }
    }
}