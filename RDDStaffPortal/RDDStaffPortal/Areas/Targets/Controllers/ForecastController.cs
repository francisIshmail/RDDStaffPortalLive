using RDDStaffPortal.Areas.Targets.Models;
using RDDStaffPortal.DAL.DataModels;
using RDDStaffPortal.DAL.Targets;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace RDDStaffPortal.Areas.Targets.Controllers
{
    public class ForecastController : Controller
    {
        // GET: Targets/Forecast
        CountryTargetDbOperations CountryDbOp = new CountryTargetDbOperations();
        SalesPersonDBOperation sales = new SalesPersonDBOperation();
        ForecastDBOperation forecast = new ForecastDBOperation();
        // GET: Targets/SalesPersonTarget
        public ActionResult Index()
        {
            string LoginUserName= User.Identity.Name;
         
            ViewData["userid"] = LoginUserName;

            List<RDD_CountryTarget> data = new List<RDD_CountryTarget>();

           // data = GetSalesPersonCountry(LoginUserName);
           // ViewBag.List = data;
            return View();
        }
       

        public List<RDD_CountryTarget> GetSalesPersonCountry(string id)
        {
            System.Web.Script.Serialization.JavaScriptSerializer objSerializer = new System.Web.Script.Serialization.JavaScriptSerializer();
            // string id1= objSerializer.Deserialize<string>(id);
            List<RDD_CountryTarget> Country = new List<RDD_CountryTarget>();

            Country = sales.GetCountryList(id);
            return Country;
        }


        public ActionResult GetForecastBU(string fdata)
        {
            System.Web.Script.Serialization.JavaScriptSerializer objSerializer = new System.Web.Script.Serialization.JavaScriptSerializer();
            List<RDD_ForecastBU> BU = new List<RDD_ForecastBU>();
            ForecastBU datum = objSerializer.Deserialize<ForecastBU>(fdata);
            BU = forecast.GetBUList(datum.year,datum.Month,datum.salesperson,datum.country);
            //data= moduleDbOp.GetModulesList3(Levels);
            ViewBag.BUList = BU;
            return Json(BU, JsonRequestBehavior.AllowGet);
            // return View();
        }
        // GetCopyCountrywiseBU
       
        public JsonResult SaveCountryBU(List<RDD_ForecastTarget> rjdata)
        {

            System.Web.Script.Serialization.JavaScriptSerializer objSerializer = new System.Web.Script.Serialization.JavaScriptSerializer();
            // List<RDD_CountryBU> BU = new List<RDD_CountryBU>();
            //  List<CountryTarget> datum = objSerializer.Deserialize<List<CountryTarget>>(fdata);
            string result = string.Empty;
            string userid = User.Identity.Name;
            try
            {

                result = forecast.SaveBU(rjdata,userid);
            }
            catch (Exception ex)
            {
                result = "Error occured :" + ex.Message;
            }
            return Json(result, JsonRequestBehavior.AllowGet);
        }
    }
}