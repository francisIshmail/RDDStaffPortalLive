using Newtonsoft.Json;
using RDDStaffPortal.DAL.LMS;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace RDDStaffPortal.Areas.LMS.Controllers
{
    public class RDD_AnnualLeaveCalenderController : Controller
    {
        // GET: LMS/RDD_AnnualLeaveCalender
        RDD_LMS_Annual_Leave_Calendra_Db_Operation _LMS_planner = new RDD_LMS_Annual_Leave_Calendra_Db_Operation();
        public ActionResult Index()
        {
            return View();
        }

        [Route("GetAnnualLeaveCalendra")]
        public ActionResult LMS_AnnualLeaveClendraGet( int pagesize,int pageno, string psearch,string fromdate,string todate, string country, int deptid, int empid)
        {
            ContentResult retVal = null;
            DataSet DS;
            try
            {
                DS = _LMS_planner.Get_LMS_Leave_Planner(User.Identity.Name,pageno,psearch, pagesize,Convert.ToDateTime(fromdate),Convert.ToDateTime(todate),country,deptid,empid);

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
        [Route("GetAnnualLeaveCalendra_Dropwdown_Fill")]
        public ActionResult LMS_AnnualLeaveClenar_Dropwdown_Fill(string ptype, string CountryCode, int deptid)
        {
            ContentResult retVal = null;
            DataSet DS;
            try
            {
                DS = _LMS_planner.Get_LMS_Leave_Planner_DropDown_Fill(User.Identity.Name,ptype,CountryCode,deptid);

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