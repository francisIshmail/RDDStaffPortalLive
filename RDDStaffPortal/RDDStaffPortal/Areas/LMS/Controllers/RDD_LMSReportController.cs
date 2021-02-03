using RDDStaffPortal.DAL.Admin;
using RDDStaffPortal.DAL.LMS;
using RDDStaffPortal.DAL.DataModels.LMS;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using System.Data;
using System.Data.SqlClient;
using RDDStaffPortal.DAL;
using Newtonsoft.Json;

namespace RDDStaffPortal.Areas.LMS.Controllers
{
    [Authorize]
    public class RDD_LMSReportController : Controller
    {
        RDD_LMSReport_Db_Operation rDD_LMSReport_TemplatesDb = new RDD_LMSReport_Db_Operation();
        // GET: LMS/RDD_LMSReport
        public ActionResult Index()
        {
            RDD_LMSReport rDD_LMSReport = new RDD_LMSReport();
            rDD_LMSReport.CountryList = rDD_LMSReport_TemplatesDb.GetCountryList();
            //rDD_LMSReport.DepartmentList = rDD_LMSReport_TemplatesDb.GetDepartmentByCountry();
            return View(rDD_LMSReport);
        }
        public ActionResult GetCountryWiseDepartment(string Cid)
        {
            ContentResult retVal = null;
            DataSet DS;
            try
            {
                DS = rDD_LMSReport_TemplatesDb.DepartmentByCountry(Cid);

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
        public ActionResult GetDeptWiseEmployee(int dept, string cid)

        {
            ContentResult retVal = null;
            DataSet DS;
            try
            {
                DS = rDD_LMSReport_TemplatesDb.EmployeeByDept(dept, cid);

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
        public ActionResult ShowEmployeeReport(int Empid)
        {
            List<RDD_LMSReport> rDD_LMSReports = new List<RDD_LMSReport>();
            rDD_LMSReports = rDD_LMSReport_TemplatesDb.GetAllData(Empid);
            return Json(rDD_LMSReports, JsonRequestBehavior.AllowGet);
        }
    }
}