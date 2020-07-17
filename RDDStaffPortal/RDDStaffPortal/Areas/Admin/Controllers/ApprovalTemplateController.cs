using RDDStaffPortal.DAL.Admin;
using RDDStaffPortal.DAL.DataModels.Admin;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace RDDStaffPortal.Areas.Admin.Controllers
{
    public class ApprovalTemplateController : Controller
    {
        // GET: Admin/ApprovalTemplate

        RDD_Approval_TemplatesDBOperation rDD_Approval = new RDD_Approval_TemplatesDBOperation();
        public ActionResult Index()
        {
            return View();
        }

        [Route("ADDAPPROVAL")]
        public ActionResult ADDApprovalTemplate(int TEMPId = -1)
        {
            RDD_Approval_Templates RDD_Approval = new RDD_Approval_Templates();

            RDD_Approval.EditFlag = false;
            RDD_Approval.SaveFlag = false;
            RDD_Approval.CreatedOn = DateTime.Now;
            RDD_Approval.CreatedBy = User.Identity.Name;
            RDD_Approval = rDD_Approval.GetDropList(User.Identity.Name);
            if (TEMPId != -1)
            {
                RDD_Approval = rDD_Approval.GetData(User.Identity.Name, TEMPId, rDD_Approval.GetDropList(User.Identity.Name));
                RDD_Approval.EditFlag = true;
            }
            return PartialView("~/Areas/Admin/Views/ApprovalTemplate/ADDApprovalTemplate.cshtml", RDD_Approval);
        }
        [Route("GETAPPROVAL")]
        public ActionResult GetDataApproval(int pagesize, int pageno, string psearch)
        {
            List<RDD_Approval_Templates> rDD_Approval_s = new List<RDD_Approval_Templates>();
            rDD_Approval_s = rDD_Approval.GetALLDATA(User.Identity.Name, pagesize, pageno, psearch);
            return Json(new { data = rDD_Approval_s }, JsonRequestBehavior.AllowGet);
        }
        [Route ("SAVEAPPROVAL")]
        public ActionResult SaveApproval(RDD_Approval_Templates RDD_Approval)
        {            
            RDD_Approval.CreatedBy = User.Identity.Name;
            RDD_Approval.CreatedOn = DateTime.Now;
            if (RDD_Approval.EditFlag == true)
            {
                RDD_Approval.LastUpdatedBy = User.Identity.Name;
                RDD_Approval.LastUpdatedOn = DateTime.Now;
            }
            return Json(rDD_Approval.Save1(RDD_Approval), JsonRequestBehavior.AllowGet);
        }
    }
}