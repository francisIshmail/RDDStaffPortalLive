using RDDStaffPortal.DAL.InitialSetup;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace RDDStaffPortal.Areas.Reports.Controllers
{

    [Authorize]
    public class DownloadReportsController : Controller
    {
        // GET: Reports/DownloadReports

       RDD_Reports_DBOperation _ReptOp = new RDD_Reports_DBOperation();
        public ActionResult Index()
        {
            return View();
        }




        [ChildActionOnly]
        public ActionResult GetReport()
        {
            return PartialView(_ReptOp.GetReportList(User.Identity.Name));
        }
    }
}