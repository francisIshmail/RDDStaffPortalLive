using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using RDDStaffPortal.DAL.Admin;
using RDDStaffPortal.DAL.DataModels.Admin;
using RDDStaffPortal.DAL.SAP;
using System.Data.SqlClient;
using DataTable = System.Data.DataTable;
using System.Data;
using Newtonsoft.Json;
using RDDStaffPortal.DAL;

namespace RDDStaffPortal.Areas.Admin.Controllers
{
    public class ApprovalStatusReportController : Controller
    {
        // GET: Admin/ApprovalStatusReport
        RDD_Approval_TemplatesDBOperation RDD_Approval_TemplatesDBOperation = new RDD_Approval_TemplatesDBOperation();
        public ActionResult Index()
        {
            Db.constr = System.Configuration.ConfigurationManager.ConnectionStrings["tejSAP"].ConnectionString;

            DataSet DS = Db.myGetDS("EXEC RDD_DOC_Get_Originator_List ");
            List<RDD_APPROVAL_DOC> ddlOriginatorList = new List<RDD_APPROVAL_DOC>();
            if (DS.Tables.Count > 0)
            {
                for (int i = 0; i < DS.Tables[0].Rows.Count; i++)
                {
                    RDD_APPROVAL_DOC OriginatorList = new RDD_APPROVAL_DOC();
                    OriginatorList.ORIGINATORCode = DS.Tables[0].Rows[i]["Originator"].ToString();
                    OriginatorList.ORIGINATOR = DS.Tables[0].Rows[i]["OriginatorName"].ToString();
                    ddlOriginatorList.Add(OriginatorList);

                }
            }
            ViewBag.ddlOriginatorList = new SelectList(ddlOriginatorList, "ORIGINATORCode", "ORIGINATOR");

            return View();
        }

        [Route("Get_ApprovalDoc_List")]
        public ActionResult Get_ApprovalDoc_List(string DBName, string UserName, int pagesize, int pageno, string psearch)
        {
            List<RDD_APPROVAL_DOC> _RDD_APPROVAL_DOC = new List<RDD_APPROVAL_DOC>();
            _RDD_APPROVAL_DOC = RDD_Approval_TemplatesDBOperation.Get_ApprovalDoc_List(DBName, UserName, pagesize, pageno, psearch);
            return Json(new { data = _RDD_APPROVAL_DOC }, JsonRequestBehavior.AllowGet);
        }

        [Route("Get_Doc_ApproverList")]
        public ActionResult Get_Doc_ApproverList(string ObjectType, string DocKey, string LoginUser)
        {
            ContentResult retVal = null;
            DataSet DS;
            try
            {
                DS = RDD_Approval_TemplatesDBOperation.Get_Doc_ApproverList(ObjectType, DocKey, LoginUser);

                if (DS.Tables.Count > 0)
                {
                    retVal = Content(JsonConvert.SerializeObject(DS), "application/json");
                }
                return retVal;

            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        [Route("Get_Doc_ApproverAction")]
        public ActionResult Get_Doc_ApproverAction(string ID,string Template_ID,string ObjectType, string DocKey, string Approver, string Action,string Remark,DateTime ApprovalDate)
        {
            ContentResult retVal = null;
            DataSet DS;
            try
            {
                DS = RDD_Approval_TemplatesDBOperation.Get_Doc_ApproverAction(ID,Template_ID,ObjectType, DocKey, Approver, Action,Remark,ApprovalDate);

                if (DS.Tables.Count > 0)
                {
                    retVal = Content(JsonConvert.SerializeObject(DS), "application/json");
                }
                return retVal;

            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
    }
}