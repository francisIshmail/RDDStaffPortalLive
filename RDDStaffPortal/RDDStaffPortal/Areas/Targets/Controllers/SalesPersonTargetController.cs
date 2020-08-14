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
    public class SalesPersonTargetController : Controller
    {
        CountryTargetDbOperations CountryDbOp = new CountryTargetDbOperations();
        SalesPersonDBOperation sales = new SalesPersonDBOperation();
        // GET: Targets/SalesPersonTarget
        public ActionResult Index()
        {
          
            return View();
        }
        public ActionResult GetSalesPerson(string ftype )
        {
            System.Web.Script.Serialization.JavaScriptSerializer objSerializer = new System.Web.Script.Serialization.JavaScriptSerializer();
            List<RDD_salesperson> BU = new List<RDD_salesperson>();
            string unm = "";
            if(ftype=="F")
            {
                unm = User.Identity.Name;
            }
            BU = sales.GetSalesPersonList(unm,ftype);
            //data= moduleDbOp.GetModulesList3(Levels);
           // ViewBag.BUList = BU;
            return Json(BU, JsonRequestBehavior.AllowGet);
            // return View();
        }

        public JsonResult GetSalesPersonCountry(string id)
        {
            System.Web.Script.Serialization.JavaScriptSerializer objSerializer = new System.Web.Script.Serialization.JavaScriptSerializer();
           // string id1= objSerializer.Deserialize<string>(id);
            List<RDD_CountryTarget> Country = new List<RDD_CountryTarget>();

            Country = sales.GetCountryList(id);
            //data= moduleDbOp.GetModulesList3(Levels);
           // ViewBag.BUList = Country;
           // ViewBag.List = Country;
            return Json(Country, JsonRequestBehavior.AllowGet);
            // return View();
        }


        public ActionResult GetCountrywiseBU(string fdata)
        {
            System.Web.Script.Serialization.JavaScriptSerializer objSerializer = new System.Web.Script.Serialization.JavaScriptSerializer();
            List<RDD_CountryBU> BU = new List<RDD_CountryBU>();
            Salespersonsubmitdata datum = objSerializer.Deserialize<Salespersonsubmitdata>(fdata);
            BU = sales.GetBUList(datum.Ccode, datum.frommonth, datum.Qvalue, datum.QYear, datum.type, datum.year,datum.salesperson);
            //data= moduleDbOp.GetModulesList3(Levels);
            ViewBag.BUList = BU;
            return Json(BU, JsonRequestBehavior.AllowGet);
            // return View();
        }
       // GetCopyCountrywiseBU
 public ActionResult GetCopyCountrywiseBU(string fdata)
        {
            System.Web.Script.Serialization.JavaScriptSerializer objSerializer = new System.Web.Script.Serialization.JavaScriptSerializer();
            List<RDD_CountryBU> BU = new List<RDD_CountryBU>();
            CopyfromSalesperson datum = objSerializer.Deserialize<CopyfromSalesperson>(fdata);
            BU = sales.GetCopyBUList(datum.Type,datum.fromyear,datum.frommonth,datum.fromQuarter,datum.fromsalesperson,
                        datum.fromcountry,datum.copyyear,datum.copymonth,datum.copyQyear,datum.copyQuarter ,datum.copysalesperson ,datum.copycountry);
            //string Type, string fromyear, string frommonth, string fromQuarter, string fromsalesperson,
            //string fromcountry, string copyyear,string copymonth, string copyQyear, string copyQuarter, string copysalesperson, string copycountry)


            //data= moduleDbOp.GetModulesList3(Levels);
            ViewBag.BUList = BU;
            return Json(BU, JsonRequestBehavior.AllowGet);
            // return View();
        }
        public JsonResult SaveCountryBU(List<RDD_SalesPersonTarget> rjdata)
        {
            string usernm = User.Identity.Name;
            System.Web.Script.Serialization.JavaScriptSerializer objSerializer = new System.Web.Script.Serialization.JavaScriptSerializer();
            // List<RDD_CountryBU> BU = new List<RDD_CountryBU>();
            //  List<CountryTarget> datum = objSerializer.Deserialize<List<CountryTarget>>(fdata);
            string result = string.Empty;
            try
            {

                result = sales.SaveBU(rjdata,usernm);
              //  TempData["msg"] = "Record Save Successfully";
            }
            catch (Exception ex)
            {
                result = "Error occured :" + ex.Message;
            }
          //  return RedirectToAction("Index", "SalesPersonTarget", new { area = "Targets" });
            return Json(result, JsonRequestBehavior.AllowGet);
        }
    }
}