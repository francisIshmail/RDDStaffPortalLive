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
    [Authorize]
    public class DepartmentsController : Controller
    {
        DepartmentsDbOperation DepartDbOp = new DepartmentsDbOperation();

        // MenusDbOperation menuDbOp = new MenusDbOperation();

        // GET: Admin/Departments
        public ActionResult Index()
        {
            return View();
        }



        public JsonResult AddeptName(Departments Dept)
        {
            string result = string.Empty;
            try
            {
                RDD_Departments rdd_department = new RDD_Departments();

                rdd_department.DeptId = Dept.DeptId;
                rdd_department.DeptName = Dept.DeptName;
                rdd_department.IsActive = Dept.IsActive;
                rdd_department.CreatedBy = User.Identity.Name;
                result = DepartDbOp.Save(rdd_department);

            }
            catch (Exception ex)
            {
                result = "Error occured :" + ex.Message;
            }
            return Json(result, JsonRequestBehavior.AllowGet);
        }
        public JsonResult DeleteItem(int DeptId)
        {
            string result = string.Empty;
            try
            {
           
                result = DepartDbOp.Delete(DeptId);

            }
            catch (Exception ex)
            {
                result = "Error occured :" + ex.Message;
            }
            return Json(result, JsonRequestBehavior.AllowGet);
        }





        public JsonResult GetDeptList()
        {
            return Json(DepartDbOp.GetDeptList(), JsonRequestBehavior.AllowGet);
        }

        public ActionResult BindModules()
        {
            List<RDD_Departments> modules = new List<RDD_Departments>();
            modules = DepartDbOp.GetDeptList();
            return Json(modules, JsonRequestBehavior.AllowGet);

        }

    }
  
}

