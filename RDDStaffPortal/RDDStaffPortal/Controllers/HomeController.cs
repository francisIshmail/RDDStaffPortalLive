using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using RDDStaffPortal.Areas.Admin.Models;

namespace RDDStaffPortal.Controllers
{
    public class HomeController : Controller
    {
        /// <summary>
        /// This is test change in HomeController to check push from VS into GitHub
        /// Successfully pushed changes from VS to GitHub & Now checking from GitHib to VS
        /// </summary>
        /// <returns></returns>
        public ActionResult Index()
        {
            return View();
        }

        public ActionResult About()
        {
            ViewBag.Message = "Your application description page.";

            return View();
        }

        public ActionResult Contact()
        {
            ViewBag.Message = "Your contact page.";

            return View();
        }


    }
}
