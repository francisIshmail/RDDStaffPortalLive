using RDDStaffPortal.Areas.Targets.Models;
using RDDStaffPortal.DAL.DataModels;
using RDDStaffPortal.DAL.Targets;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using RDDStaffPortal.DAL.Targets;

namespace RDDStaffPortal.Areas.Targets.Controllers
{
    public class CountryTargetController : Controller
    {
        CountryTargetDbOperations CountryDbOp = new CountryTargetDbOperations();
        
        // GET: Targets/CountryTarget
        public ActionResult Index()
        {
            List<RDD_CountryTarget> data = new List<RDD_CountryTarget>();
            data = CountryDbOp.GetCountryList();
            //data= moduleDbOp.GetModulesList3(Levels);
            ViewBag.List = data;
            return View();
        }
        
        public ActionResult GetCountrywiseBU(string fdata)
        {
            System.Web.Script.Serialization.JavaScriptSerializer objSerializer = new System.Web.Script.Serialization.JavaScriptSerializer();
            List<RDD_CountryBU> BU = new List<RDD_CountryBU>();
           CountrySubmitdata datum = objSerializer.Deserialize<CountrySubmitdata>(fdata);
           BU = CountryDbOp.GetBUList(datum.Ccode,datum.frommonth,datum.Qvalue,datum.QYear,datum.type,datum.year);
            //data= moduleDbOp.GetModulesList3(Levels);
            ViewBag.BUList = BU;
            return Json(BU, JsonRequestBehavior.AllowGet);
           // return View();
        }


        public JsonResult SaveCountryBU(List<CountryTarget> rjdata)
        {

            System.Web.Script.Serialization.JavaScriptSerializer objSerializer = new System.Web.Script.Serialization.JavaScriptSerializer();
           // List<RDD_CountryBU> BU = new List<RDD_CountryBU>();
          //  List<CountryTarget> datum = objSerializer.Deserialize<List<CountryTarget>>(fdata);
            string result = string.Empty;
            string userid = User.Identity.Name;
            try
            {

                result = CountryDbOp.SaveBU(rjdata,userid);
            }
            catch (Exception ex)
            {
                result = "Error occured :" + ex.Message;
            }
            return Json(result, JsonRequestBehavior.AllowGet);
        }
    }
}