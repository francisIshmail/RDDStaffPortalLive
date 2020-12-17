using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using RDDStaffPortal.Areas.HR.Models;
using RDDStaffPortal.DAL.HR;
using RDDStaffPortal.DAL.DataModels;
using System.Data;
using RDDStaffPortal.DAL;
using System.IO;
using System.Web.Helpers;
using System.Net;

namespace RDDStaffPortal.Areas.HR.Controllers
{
    public class BUCountriesController : Controller
    {
        BUCountriesDbOperations BUDbOp = new BUCountriesDbOperations();
        Common cm = new Common();
        // GET: HR/BUCountries
        //public ActionResult GetEmpList()
        //{ 
        //}

        [Route("GetEmpList")]
        public ActionResult GetEmpList(string Prefix)
        {

            Db.constr = System.Configuration.ConfigurationManager.ConnectionStrings["tejSAP"].ConnectionString;
            DataSet DS = Db.myGetDS("exec RDD_GetBUListEmp'" + Prefix+"'");
            List<Rdd_comonDrop> EmpList = new List<Rdd_comonDrop>();
            try
            {
                if (DS.Tables.Count > 0)
                {
                    for (int i = 0; i < DS.Tables[0].Rows.Count; i++)
                    {
                        Rdd_comonDrop EmpLst = new Rdd_comonDrop();
                        EmpLst.Code = DS.Tables[0].Rows[i]["EmployeeId"].ToString();
                        EmpLst.CodeName = DS.Tables[0].Rows[i]["Empname"].ToString();
                        EmpLst.imagepath = Convert.ToBase64String((byte[])DS.Tables[0].Rows[i]["ImagePath"]);
                        EmpList.Add(EmpLst);

                    }
                }
            }
            catch (Exception ex)
            {

                EmpList = null;
            }
           
            return Json(EmpList, JsonRequestBehavior.AllowGet);
           
        }
        public ActionResult Index() 
        {
            Db.constr = System.Configuration.ConfigurationManager.ConnectionStrings["tejSAP"].ConnectionString;
            DataSet DS = Db.myGetDS("exec RDD_GetBUList");
            List<RDD_EmployeeRegistration> BUItmList = new List<RDD_EmployeeRegistration>();
            List<RDD_EmployeeRegistration> CountryList = new List<RDD_EmployeeRegistration>();
            try
            {
              
                if (DS.Tables.Count > 0)
                {
                    for (int i = 0; i < DS.Tables[0].Rows.Count; i++)
                    {
                        RDD_EmployeeRegistration BUItmLst = new RDD_EmployeeRegistration();
                        BUItmLst.ItmsGrpNam = DS.Tables[0].Rows[i]["ItmsGrpNam"].ToString();
                        BUItmLst.ItmsGrpCod = Convert.ToInt32(DS.Tables[0].Rows[i]["ItmsGrpCod"].ToString());

                        BUItmList.Add(BUItmLst);

                    }
                }

            }
            catch (Exception ex)
            {

                BUItmList = null;
            }
            try
            {
                if (DS.Tables.Count > 0)
                {
                    for (int i = 0; i < DS.Tables[1].Rows.Count; i++)
                    {
                        RDD_EmployeeRegistration CountryLst = new RDD_EmployeeRegistration();
                        CountryLst.CountryCode = DS.Tables[1].Rows[i]["CountryCode"].ToString();
                        CountryList.Add(CountryLst);

                    }
                }
            }
            catch (Exception ex)
            {

                CountryList = null;
            }
           

            
           
          



          

           // ViewBag.EmpList = new SelectList(EmpList, "EmployeeId", "EmpName");
            ViewBag.BUItmList = BUItmList;
            ViewBag.CountryList = CountryList;

           

            return View();
        }



        public JsonResult AddBUcountries(RDD_BUCountries BUCoun)
        {
            string result = string.Empty;
            try
            {

                BUCoun.CreatedBy = User.Identity.Name;

            result = BUDbOp.Save(BUCoun);

        }
            catch (Exception ex)
            {
                result = "Error occured :" + ex.Message;
            }
            return Json(result, JsonRequestBehavior.AllowGet);
       }

        public JsonResult GetBUByEmpId(int empid)
        {

            Db.constr = System.Configuration.ConfigurationManager.ConnectionStrings["tejSAP"].ConnectionString;
           
            DataSet DS = Db.myGetDS("exec RDD_BUMapping_GetData " + empid + "");
            List<RDD_BUCountries> BUItmList = new List<RDD_BUCountries>();
            if (DS.Tables.Count > 0)
            {
                for (int i = 0; i < DS.Tables[0].Rows.Count; i++)
                {
                    RDD_BUCountries BUItmLst = new RDD_BUCountries();
                    BUItmLst.CId = Convert.ToInt32(DS.Tables[0].Rows[i]["Id"].ToString());
                    BUItmLst.ItmsGrpNam = DS.Tables[0].Rows[i]["ItmsGrpNam"].ToString();
                    BUItmLst.CountryCodeName = DS.Tables[0].Rows[i]["CountryCode"].ToString();
                    BUItmLst.BU = Convert.ToInt32(DS.Tables[0].Rows[i]["BU"].ToString());

                    BUItmList.Add(BUItmLst);

                }
            }
         
           return Json(BUItmList, JsonRequestBehavior.AllowGet);
        }

        public JsonResult DeleteRecord(int buId)
        {
            string result = string.Empty;
            try
            {
              
                result = BUDbOp.Delete(buId, User.Identity.Name);
            }
            catch (Exception ex)
            {
                result = "Error occured :" + ex.Message;
            }
            return Json(new { DeleteFlag = result }, JsonRequestBehavior.AllowGet);

        }




        public ActionResult gethistory(int empid, string tblname)
        {

            return Json(cm.GetChangeLog(empid, tblname), JsonRequestBehavior.AllowGet);
        }
    }
}