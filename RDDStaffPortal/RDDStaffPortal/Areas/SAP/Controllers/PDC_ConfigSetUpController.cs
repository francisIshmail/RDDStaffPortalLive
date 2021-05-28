using Newtonsoft.Json;
using RDDStaffPortal.DAL.DataModels.SAP;
using RDDStaffPortal.DAL.SAP;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace RDDStaffPortal.Areas.SAP.Controllers
{
    public class PDC_ConfigSetUpController : Controller
    {
        // GET: SAP/PDC_ConfigSetUp
        RDD_PDC_ConfidSetUpDbOperation rDD_PDCSetUp_DbOperation = new RDD_PDC_ConfidSetUpDbOperation();
        public ActionResult Index()
        {
            return View();
        }
        public ActionResult GetUserList()
        {
            ContentResult retVal = null;
            DataSet ds;
            try
            {
                ds = rDD_PDCSetUp_DbOperation.GetUserList();
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
        public ActionResult GetPDCSetUpInfo()
        {
            ContentResult retVal = null;
            DataSet ds;
            try
            {
                ds = rDD_PDCSetUp_DbOperation.GetPDCSetUpInfo();
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
        

        [Route("SavePDCConfig")]
        public ActionResult SavePdcSetupDetails(RDD_PDCConfigSetUp rDD_PDC)
        {
            if (rDD_PDC.Editflag == true)
            {
                rDD_PDC.LastUpdatedBy = User.Identity.Name;
            }
            else
            {
                rDD_PDC.CreatedBy = User.Identity.Name;
            }    
            
            

            return Json(rDD_PDCSetUp_DbOperation.SavePDC(rDD_PDC), JsonRequestBehavior.AllowGet);
        }
        [Route("SaveBounceReason")]
        public ActionResult SaveBounceReasonDetails(RDD_PDCConfigSetUp rDD_PDC)
        {
            if (rDD_PDC.Editflag == true)
            {
                rDD_PDC.LastUpdatedBy = User.Identity.Name;
            }
            else
            {
                rDD_PDC.CreatedBy = User.Identity.Name;
            }
            return Json(rDD_PDCSetUp_DbOperation.SaveReason(rDD_PDC), JsonRequestBehavior.AllowGet);
        }
    }
}