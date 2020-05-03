using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using RDDStaffPortal.Areas.Admin.Models;
using RDDStaffPortal.DAL.InitialSetup;
using RDDStaffPortal.DAL.DataModels;

namespace RDDStaffPortal.Areas.Admin.Controllers
{
    public class EmploymentStatusController : Controller
    {
        EmpStatusDbOperations StatusDbOp = new EmpStatusDbOperations();
     

        // MenusDbOperation menuDbOp = new MenusDbOperation();

        // GET: Admin/Departments
        public ActionResult Index()
        {
            return View();
        }



        public JsonResult AddStatusName(EmploymentStatus Status)
        {
            string result = string.Empty;
            try
            {
                RDD_EmploymentStatus rdd_EmpStatus = new RDD_EmploymentStatus();

                rdd_EmpStatus.StatusId = Status.StatusId;
                rdd_EmpStatus.StatusName = Status.StatusName;
                rdd_EmpStatus.IsActive = Status.IsActive;
                rdd_EmpStatus.CreatedBy = User.Identity.Name;
                result = StatusDbOp.Save(rdd_EmpStatus);

            }
            catch (Exception ex)
            {
                result = "Error occured :" + ex.Message;
            }
            return Json(result, JsonRequestBehavior.AllowGet);
        }
        public JsonResult DeleteItem(int StatusId)
        {
            string result = string.Empty;
            try
            {

                result = StatusDbOp.Delete(StatusId);

            }
            catch (Exception ex)
            {
                result = "Error occured :" + ex.Message;
            }
            return Json(result, JsonRequestBehavior.AllowGet);
        }





        public JsonResult GetStatusList()
        {
            return Json(StatusDbOp.GetStatusList(), JsonRequestBehavior.AllowGet);
        }

        public ActionResult BindModules()
        {
            List<RDD_EmploymentStatus> modules = new List<RDD_EmploymentStatus>();
            modules = StatusDbOp.GetStatusList();
            return Json(modules, JsonRequestBehavior.AllowGet);

        }

    }

}

