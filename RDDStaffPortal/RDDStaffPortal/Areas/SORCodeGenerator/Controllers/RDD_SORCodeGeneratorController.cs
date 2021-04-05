using RDDStaffPortal.DAL.SORCodeGenerator;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using Newtonsoft.Json;
using System.Threading;
using RDDStaffPortal.DAL.DataModels.SORCodeGenerator;

namespace RDDStaffPortal.Areas.SORCodeGenerator.Controllers
{
    public class RDD_SORCodeGeneratorController : Controller
    {
        // GET: SORCodeGenerator/RDD_SORCodeGenerator
        RDD_SORCodeGenerator_Db_Operation rDD_SorCodeGenerator_TemplatesDb = new RDD_SORCodeGenerator_Db_Operation();
        public ActionResult Index()
        {
            return View();
        }
        public ActionResult GetDatabaseList()
        {
            List<SelectListItem> rDD_DatabaseList = new List<SelectListItem>();
            rDD_DatabaseList = rDD_SorCodeGenerator_TemplatesDb.GetDatabaseList();
            return Json(rDD_DatabaseList, JsonRequestBehavior.AllowGet);
        }
        public ActionResult GetRequestedByList()
        {
            ContentResult retVal = null;
            DataSet ds;
            try
            {
                ds = rDD_SorCodeGenerator_TemplatesDb.GetRequestedByList();
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
        public ActionResult SaveSORDetails(RDD_SORCodeGenerator rDD_SORCodeGenerator)
        {
            rDD_SORCodeGenerator.CreatedBy = User.Identity.Name;
           
            return Json(rDD_SorCodeGenerator_TemplatesDb.SaveSOR(rDD_SORCodeGenerator), JsonRequestBehavior.AllowGet);
        }
        public ActionResult DraftSORNumVallidation(string DraftNo, string DBName)
        {
            ContentResult retVal = null;
            DataSet ds;            
            try
            {
                ds = rDD_SorCodeGenerator_TemplatesDb.DraftSORNumVallidation(DraftNo, DBName);
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
        public ActionResult GetSORApprovalCodeList()
        {
            ContentResult retVal = null;
            DataSet ds;
            try
            {
                ds = rDD_SorCodeGenerator_TemplatesDb.GetSORApprovaLCodeList();
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
        public ActionResult GetCACM(string DraftNo, string DBName)
        {
            ContentResult retVal = null;
            DataSet ds;
            try
            {
                ds = rDD_SorCodeGenerator_TemplatesDb.GetCACM(DraftNo, DBName);
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
        public ActionResult GenerateOTP(string Project, string DraftSORDocEntry)
        {
            ContentResult retVal = null;
            string Code = string.Empty;
            try
            {
                Random _random = new Random();
                Code = Project.Trim().ToUpper() + "-" + DraftSORDocEntry + '-' + _random.Next(50, 999989).ToString("D8");
                retVal = Content(JsonConvert.SerializeObject(Code),"application/json");
                Thread.Sleep(700);
               
            }
            catch (Exception ex)
            {
                throw ex;
            }
            return retVal;
        }

    }
}