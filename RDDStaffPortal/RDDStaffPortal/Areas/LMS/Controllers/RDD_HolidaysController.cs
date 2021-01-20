using RDDStaffPortal.DAL.Admin;
using RDDStaffPortal.DAL.LMS;
using RDDStaffPortal.DAL.DataModels.LMS;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using System.Data;

namespace RDDStaffPortal.Areas.LMS.Controllers
{
    [Authorize]
    public class RDD_HolidaysController : Controller
    {
        RDD_Holidays_Db_Operation rDD_Holidays_TemplatesDb = new RDD_Holidays_Db_Operation();
        // GET: LMS/RDD_Holidays
        public ActionResult Index()
        {

            RDD_Holidays rDD_Holidays = new RDD_Holidays();
            rDD_Holidays.CountryList = rDD_Holidays_TemplatesDb.GetCountryList(User.Identity.Name);
            
            DataSet ds = rDD_Holidays_TemplatesDb.GetHRRole(User.Identity.Name);
            if (ds.Tables.Count > 0)
            {


                rDD_Holidays.HR = Convert.ToBoolean(ds.Tables[0].Rows[0]["IsHRUser"].ToString());

                                
            }
            return View(rDD_Holidays);
        }
        //[Route("GetHolidays_List")]
        public ActionResult GetHolidays_List()
        {

            List<RDD_Holidays> modules = new List<RDD_Holidays>();
            modules = rDD_Holidays_TemplatesDb.GetList();
            return PartialView(modules);
        }
        [Route("SaveHolidays")]
        public ActionResult SaveHolidays(RDD_Holidays rDD_holiday)
        {
            rDD_holiday.CreatedBy = User.Identity.Name;
            rDD_holiday.CreatedOn = DateTime.Now;
            if (rDD_holiday.Editflag == true)
            {
                rDD_holiday.LastUpdatedBy = User.Identity.Name;
                rDD_holiday.LastUpdatedOn = System.DateTime.Now;
            }
            return Json(rDD_Holidays_TemplatesDb.Save(rDD_holiday), JsonRequestBehavior.AllowGet);
        }
        [Route("GetHolidays")]
        public ActionResult GetData(string HolidayId)
        {
            return Json(new { data = rDD_Holidays_TemplatesDb.GetData(HolidayId) }, JsonRequestBehavior.AllowGet);
        }

        [Route("DeleteHolidays")]
        public ActionResult DeleteFlag(string HolidayId)
        {
            return Json(new { data = rDD_Holidays_TemplatesDb.DeleteFlag(HolidayId) }, JsonRequestBehavior.AllowGet);
        }
        public ActionResult GetHoliday()
        {
            List<RDD_Holidays> rDD_Holiday_s = new List<RDD_Holidays>();
            rDD_Holiday_s = rDD_Holidays_TemplatesDb.GetALLDATA();
            return Json(rDD_Holiday_s, JsonRequestBehavior.AllowGet);
        }
        //public ActionResult GetCountryDetails_list()
        //{
        //    List<SelectListItem> modules = new List<SelectListItem>();
        //    modules = rDD_Holidays_TemplatesDb.GetCountryList();
        //    return Json(modules, JsonRequestBehavior.AllowGet);

        //}
    }
}