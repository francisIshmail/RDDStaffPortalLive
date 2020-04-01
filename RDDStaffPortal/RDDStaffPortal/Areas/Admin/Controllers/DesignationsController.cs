using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using RDDStaffPortal.Areas.Admin.Models;
using RDDStaffPortal.DAL.DataModels;
using RDDStaffPortal.DAL.InitialSetup;

namespace RDDStaffPortal.Areas.Admin.Controllers
{      
    
    public class DesignationsController : Controller
    {
     
        DesignationDbOperation DesigDbOp = new DesignationDbOperation();
        // GET: Admin/Designations
        public ActionResult Index()
        {
            return View();
        }

          
        public JsonResult AdddesigName(Designations Desig)
        {
            string result = string.Empty;
            try
            {
                RDD_Designation rdd_designation = new RDD_Designation();

                rdd_designation.DesigId = Desig.DesigId;
                rdd_designation.DesigName = Desig.DesigName;
                rdd_designation.IsActive = Desig.IsActive;
                rdd_designation.CreatedBy = User.Identity.Name;
                result = DesigDbOp.Save(rdd_designation);

            }
            catch (Exception ex)
            {
                result = "Error occured :" + ex.Message;
            }
            return Json(result, JsonRequestBehavior.AllowGet);
        }
        public JsonResult DeleteItem(int DesigId)
        {
            string result = string.Empty;
            try
            {

                result = DesigDbOp.Delete(DesigId);

            }
            catch (Exception ex)
            {
                result = "Error occured :" + ex.Message;
            }
            return Json(result, JsonRequestBehavior.AllowGet);
        }

        public JsonResult GetDesigList()
        {
            return Json(DesigDbOp.GetDesigList(), JsonRequestBehavior.AllowGet);
        }

        public ActionResult BindModules()
        {
            List<RDD_Designation> modules = new List<RDD_Designation>();
            modules = DesigDbOp.GetDesigList();
            return Json(modules, JsonRequestBehavior.AllowGet);

        }

    }

}

