using RDDStaffPortal.DAL.SORCodeGenerator;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using Newtonsoft.Json;
using System.Threading;
using RDDStaffPortal.DAL.DataModels.SAP;
using RDDStaffPortal.DAL.SAP;

namespace RDDStaffPortal.Areas.SAP.Controllers
{
    public class RDD_TempCreditLimitController : Controller
    {
        RDD_CreditLimit_DbOperation rDD_CreditLimit_DbOperation = new RDD_CreditLimit_DbOperation();

        // GET: SAP/RDD_TempCreditLimit
        public ActionResult Index()
        {
            return View();
        }
        public ActionResult GetUserList(string Country)
        {
            ContentResult retVal = null;
            DataSet ds;
            try
            {
                ds = rDD_CreditLimit_DbOperation.GetUserList( Country);
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
        public ActionResult GetCountryList()
        {
            ContentResult retVal = null;
            DataSet ds;
            try
            {
                ds = rDD_CreditLimit_DbOperation.GetCountryList();
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
        public ActionResult GetCreditLimitDetails()
        {
            ContentResult retVal = null;
            DataSet ds;
            try
            {
                ds = rDD_CreditLimit_DbOperation.GetCreditLimitDetails();
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
        [Route("SaveTempCL")]
        public ActionResult SaveTempCreditLimitDetails(RDD_CreditLimit rDD_TempCreditLimit)
        {
            rDD_TempCreditLimit.CreatedBy = User.Identity.Name;
            rDD_TempCreditLimit.LastUpdatedBy = User.Identity.Name;

            return Json(rDD_CreditLimit_DbOperation.SaveCreditLimit(rDD_TempCreditLimit), JsonRequestBehavior.AllowGet);
        }
    }
   
}