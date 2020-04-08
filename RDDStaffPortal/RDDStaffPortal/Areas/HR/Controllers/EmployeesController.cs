using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace RDDStaffPortal.Areas.HR.Controllers
{
    public class EmployeesController : Controller
    {
        // GET: HR/Employees
        public ActionResult Index()
        {
            return View();
        }
    }
}