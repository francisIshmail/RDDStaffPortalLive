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
    public class PDCEntryController : Controller
    {
        // GET: SAP/PDCEntry
        RDD_PDCEntryDbOperation rDD_PDCEntry_DbOperation = new RDD_PDCEntryDbOperation();
        public ActionResult Index()
        {
            return View();
        }
        public ActionResult GetPartialView()
        {
            return PartialView("~/Areas/SAP/Views/PDCEntry/PDCEntryPartial.cshtml");
        }
        public ActionResult GetDatabaseList()
        {
            ContentResult retVal = null;
            DataSet ds;
            try
            {
                ds = rDD_PDCEntry_DbOperation.GetDatabaseList();
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
        public ActionResult GetCustomerNameList(string DbName)
        {
            ContentResult retVal = null;
            DataSet ds;
            try
            {
                ds = rDD_PDCEntry_DbOperation.GetCustomerNameList(DbName);
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
        public ActionResult GetBankNameList(string DbName)
        {
            ContentResult retVal = null;
            DataSet ds;
            try
            {
                ds = rDD_PDCEntry_DbOperation.GetBankNameList(DbName);
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
        public ActionResult GetEmpName()
        {
            ContentResult retVal = null;
            DataSet ds;
            try
            {
                ds = rDD_PDCEntry_DbOperation.GetEmpName(User.Identity.Name);
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
    }
}