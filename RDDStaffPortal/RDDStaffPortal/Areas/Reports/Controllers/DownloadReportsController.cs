using RDDStaffPortal.DAL.InitialSetup;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Web;
using System.Web.Mvc;
using System.Web.Routing;

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

        public ActionResult Download(string parentPartId,string Date1)
        {

           

               string FileVirtualPath = null;
                if (parentPartId != null)
                {

                try
                {
                    FileVirtualPath = @"" + parentPartId + "";
                    byte[] fileBytes = System.IO.File.ReadAllBytes(FileVirtualPath);


                    if (fileBytes != null || fileBytes.Length != 0)
                    {
                        return File(FileVirtualPath, "application/force-download", Path.GetFileName(FileVirtualPath));
                    }
                    else
                    {
                        return RedirectToAction("Index", new RouteValueDictionary(new { controller = "DownloadReports", action = "Index", Date1 = Date1 })); 
                    }
                }
                catch (Exception)
                {

                    return RedirectToAction("Index", new RouteValueDictionary(new { controller = "DownloadReports", action = "Index", Date1 = Date1 }));
                }
                    

            }
            else
            {
                return RedirectToAction("Index", new RouteValueDictionary(new { controller = "DownloadReports", action = "Index", Date1 = Date1 }));
            }
               
           
           

        }


        [ChildActionOnly]
        public ActionResult GetReport()
        {
            return PartialView(_ReptOp.GetReportList(User.Identity.Name));
        }
    }
}