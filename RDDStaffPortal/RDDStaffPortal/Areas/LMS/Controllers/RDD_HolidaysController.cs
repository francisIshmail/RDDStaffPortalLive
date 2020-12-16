using RDDStaffPortal.DAL.Admin;
using RDDStaffPortal.DAL.LMS;
using RDDStaffPortal.DAL.DataModels.LMS;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace RDDStaffPortal.Areas.LMS.Controllers
{
    [Authorize]
    public class RDD_HolidaysController : Controller
    {
        RDD_Holidays_Db_Operation rDD_Holidays_TemplatesDb = new RDD_Holidays_Db_Operation();
        // GET: LMS/RDD_Holidays
        public ActionResult Index()
        {
            return View();
        }
        public ActionResult GetHolidays_List()
        {

            List<RDD_Holidays> modules = new List<RDD_Holidays>();
            modules = rDD_Holidays_TemplatesDb.GetList();
            return PartialView(modules);
        }
        [Route("SaveHolidays")]
        public ActionResult SaveDSR_NextAction(RDD_Holidays rDD_holiday)
        {
            rDD_holiday.CreatedBy = User.Identity.Name;
            rDD_holiday.CreatedOn = DateTime.Now;
            if (rDD_holiday.Editflag == true)
            {
                rDD_holiday.LastUpdatedBy = User.Identity.Name;
                rDD_holiday.LastUpdatedOn = System.DateTime.Now;
            }
            return Json(new { data = rDD_Holidays_TemplatesDb.Save(rDD_holiday) }, JsonRequestBehavior.AllowGet);
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
    }
}