using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using System.Data;
using System.Data.SqlClient;
using RDDStaffPortal.DAL;
using Newtonsoft.Json;
using System.Text;
using System.IO;
using RDDStaffPortal.DAL.LMS;
using RDDStaffPortal.DAL.DataModels.LMS;

namespace RDDStaffPortal.Areas.LMS.Controllers
{
    public class RDD_HRLMSReportController : Controller
    {
        RDD_HRLMSReport_Db_Operation rDD_HRLMSReport_TemplatesDb = new RDD_HRLMSReport_Db_Operation();
        // GET: LMS/RDD_HRLMSReport
        public ActionResult Index()
        {          
            
            return View();
        }       
        public ActionResult GetDropdownList()
        {
            ContentResult retVal = null;
            DataSet ds;
            try
            {
                ds = rDD_HRLMSReport_TemplatesDb.GetDropdownList();
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
        public ActionResult ShowEmployeeReport(string Empid, string Cid,string DeptId,string LeaveTypeId)
        {
            ContentResult retVal = null;
            DataSet DS;
            try
            {
                string Empide;
                string Cide;
                string Deptide;
                string LeaveTypeIde;
                if (Empid == "-1")
                    Empide = "";
                else
                    Empide = Empid;
                if (Cid == "-1")
                    Cide = "";
                else
                    Cide = Cid;
                if (DeptId == "-1")
                    Deptide = "";
                else
                    Deptide = DeptId;
                if (LeaveTypeId == "-1")
                    LeaveTypeIde = "";
                else
                    LeaveTypeIde = LeaveTypeId;
                DS = rDD_HRLMSReport_TemplatesDb.GetAllData(Empide, Cide, Deptide, LeaveTypeIde);

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
        public ActionResult GetCountryWiseLeaveType(string Cid)
        {
            ContentResult retVal = null;
            DataSet DS;
            try
            {
                DS = rDD_HRLMSReport_TemplatesDb.LeaveTypeByCountry(Cid);

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
    }
}