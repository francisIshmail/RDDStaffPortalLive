using RDDStaffPortal.DAL.Admin;
using RDDStaffPortal.DAL.LMS;
using RDDStaffPortal.DAL.DataModels.LMS;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using System.IO;
using System.Data;
using Newtonsoft.Json;
using System.Data.SqlClient;
using RDDStaffPortal.DAL;

namespace RDDStaffPortal.Areas.LMS.Controllers
{
    public class RDD_LeaveApprovalController : Controller
    {
        // GET: LMS/RDD_LeaveApproval
        CommonFunction Com = new CommonFunction();
        RDD_LeaveApproval_Db_Operation rDD_LeaveApproval_TemplatesDb = new RDD_LeaveApproval_Db_Operation();
        public ActionResult Index()
        {
            RDD_LeaveApproval rDD_LeaveApprove = new RDD_LeaveApproval();
            rDD_LeaveApprove.EmployeeLists = rDD_LeaveApproval_TemplatesDb.GetEmployee();           
            rDD_LeaveApprove.EmployeeId = rDD_LeaveApproval_TemplatesDb.GetEmployeeIdByLoginName(User.Identity.Name);
            rDD_LeaveApprove.LeaveTypeList = rDD_LeaveApproval_TemplatesDb.GetLeaveTypeName(rDD_LeaveApprove.EmployeeId);
            rDD_LeaveApprove.WeeklyOffDays = rDD_LeaveApproval_TemplatesDb.GetWeeklyOffDay(rDD_LeaveApprove.EmployeeId);
            return View(rDD_LeaveApprove);
        }
       
        public ActionResult ShowLeaveApprovalDetail()
        {
            ContentResult retVal = null;
            DataSet ds;
            string Username = User.Identity.Name;
            try
            {
                ds = rDD_LeaveApproval_TemplatesDb.GetApprovalList(Username);
                if (ds.Tables.Count > 0)
                {
                    retVal = Content(JsonConvert.SerializeObject(ds), "application/json");
                }
                return retVal;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
        public ActionResult ShowLeaveRequestDetail(int Loginuserid)
        {            
            ContentResult retVal = null;
            DataSet ds;            
            try
            {
                ds = rDD_LeaveApproval_TemplatesDb.GetLeaveRequestList(Loginuserid);
                if (ds.Tables.Count > 0)
                {
                    retVal = Content(JsonConvert.SerializeObject(ds), "application/json");
                }
                return retVal;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
        [Route("LEAVELEDGERENTRY")]
        public ActionResult LeaveLedgerEntry(int LeaveRequestId,int EmployeeId,int LeaveTypeId,string NoOfDays, string ButtonFlag)
        {
            RDD_LeaveApproval rDD_LeaveApprove = new RDD_LeaveApproval();
            var CreatedBy = User.Identity.Name;
            var CreatedOn = DateTime.Now.ToString();
            return Json(rDD_LeaveApproval_TemplatesDb.LeaveLedgerEntry(LeaveRequestId, EmployeeId, LeaveTypeId, NoOfDays,CreatedBy,CreatedOn, ButtonFlag), JsonRequestBehavior.AllowGet);
        }
        [Route("LEAVEAPPROVE")]
        public ActionResult ApprovalRequest(int LeaveRequestId,string ApproverRemarks)
        {
            return Json(rDD_LeaveApproval_TemplatesDb.UpdateAcceptStatus(LeaveRequestId, ApproverRemarks), JsonRequestBehavior.AllowGet);
        }
        [Route("LEAVEDECLINE")]
        public ActionResult DeclineRequest(int LeaveRequestId, string ApproverRemarks)
        {
            return Json(rDD_LeaveApproval_TemplatesDb.UpdateRejectStatus(LeaveRequestId, ApproverRemarks), JsonRequestBehavior.AllowGet);
        }
        [Route("LEAVEDETAILS")]
        public ActionResult GetApprovalData(int LeaveRequestId)
        {

            return Content(JsonConvert.SerializeObject(rDD_LeaveApproval_TemplatesDb.GetLeaveDetails(LeaveRequestId)), "application/json");
        }
        [Route("DELETEREQUEST")]
        public ActionResult DeleteRequest(int LeaveRequestId)
        {

            return Json(rDD_LeaveApproval_TemplatesDb.DeleteLeaveRequest(LeaveRequestId), JsonRequestBehavior.AllowGet);
        }
        [Route("GetLeaveBalance")]
        public ActionResult GetLeaveBalance(int EmployeeId, int LeaveTypeId)
        {
            // return Json(new { data = rDD_LeaveRequest_TemplatesDb.GetLeaveBalance(EmployeeId,LeaveTypeId) }, JsonRequestBehavior.AllowGet);
            ContentResult retVal = null;
            DataSet DS;
            try
            {
                DS = rDD_LeaveApproval_TemplatesDb.GetLeaveBalance(EmployeeId, LeaveTypeId);

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
        [Route("GETHOLIDAY")]
        public ActionResult GetHolidayCountryWise(int EmployeeId)
        {
            ContentResult retVal = null;
            DataSet ds;
            try
            {
                ds = rDD_LeaveApproval_TemplatesDb.GetHolidayCountryWise(EmployeeId);
                if (ds.Tables.Count > 0)
                {
                    retVal = Content(JsonConvert.SerializeObject(ds), "application/json");
                }
                return retVal;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
        [Route("GetAnnualLeaveBalances")]
        public ActionResult GetAnnualLeaveBalance(int EmployeeId)
        {
            // return Json(new { data = rDD_LeaveRequest_TemplatesDb.GetLeaveBalance(EmployeeId,LeaveTypeId) }, JsonRequestBehavior.AllowGet);
            ContentResult retVal = null;
            DataSet DS;
            try
            {
                DS = rDD_LeaveApproval_TemplatesDb.GetAnnualLeaveBalance(EmployeeId);

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

        public JsonResult UploadDoc(string EmployeeId)
        {
            string fname = "";
            string strMappath = "";
            string _imgname = string.Empty;
            string _imgname1 = string.Empty;
            if (Request.Files.Count > 0)
            {
                try
                {
                    string str = EmployeeId + "_" + User.Identity.Name;
                    //strMappath = "~/excelFileUpload/" + "PV/" + User.Identity.Name + "/" + type + "/";
                    strMappath = "~/LMSFileUpload/" + "LRQ/" + str + "/";

                    if (!Directory.Exists(strMappath))
                    {
                        Directory.CreateDirectory(System.IO.Path.Combine(Server.MapPath(strMappath)));
                    }


                    //  Get all files from Request object  
                    System.Web.HttpFileCollectionBase files = Request.Files;
                    var _ext = "";
                    var fileName = "";
                    for (int i = 0; i < files.Count; i++)
                    {
                        HttpPostedFileBase file = files[i];
                        // Checking for Internet Explorer  
                        if (Request.Browser.Browser.ToUpper() == "IE" || Request.Browser.Browser.ToUpper() == "INTERNETEXPLORER" || Request.Browser.Browser.ToUpper() == "GOOGLE CHROME")
                        {
                            string[] testfiles = file.FileName.Split(new char[] { '\\' });
                            fname = testfiles[testfiles.Length - 1];
                        }
                        else
                        {

                            fname = file.FileName;
                            fileName = Path.GetFileNameWithoutExtension(file.FileName);
                            _ext = System.IO.Path.GetExtension(fname).ToUpper();
                            if ((_ext != ".JPG" && _ext != ".PNG" && _ext != ".GIF" && _ext != ".PDF"))
                            {
                                return Json("Error occurred. Error details: Only Image Or Pdf", JsonRequestBehavior.AllowGet);
                            }

                        }

                        // Get the complete folder path and store the file inside it.
                        _imgname1 = fileName + "" + System.DateTime.Now.ToString("ddMMyyyyHHMMss") + "" + _ext;
                        _imgname = System.IO.Path.Combine(Server.MapPath(strMappath), _imgname1);
                        file.SaveAs(_imgname);
                    }
                    // Returns message that successfully uploaded  
                    return Json(strMappath.ToString().Replace("~", "") + _imgname1, JsonRequestBehavior.AllowGet);
                }
                catch (Exception ex)
                {
                    return Json("Error occurred. Error details: " + ex.Message);
                }
            }
            else
            {
                return Json(strMappath.ToString().Replace("~", "") + _imgname1, JsonRequestBehavior.AllowGet);
            }

        }
        public ActionResult SendApprovedEmail(int loginUserId, string Fromdate,string Todate ,string EmployeeName,string LeaveName ,string Comments,int Backup1Id,int Backup2Id)
        {
            try
            {
                DataSet ds1 = new DataSet();
                DataSet ds2 = new DataSet();
                DataSet ds3 = new DataSet();
                SqlParameter[] prm1 =
               {
                   new SqlParameter("@LoginUserId",loginUserId)
                };
                SqlParameter[] prm2 =
              {
                   new SqlParameter("@LoginUserId",Backup1Id)
                };
                SqlParameter[] prm3 =
              {
                   new SqlParameter("@LoginUserId",Backup2Id)
                };
                ds1 = Com.ExecuteDataSet("RDD_GetManagerDetails", CommandType.StoredProcedure, prm1);
                ds2 = Com.ExecuteDataSet("RDD_GetManagerDetails", CommandType.StoredProcedure, prm2);
                ds3 = Com.ExecuteDataSet("RDD_GetManagerDetails", CommandType.StoredProcedure, prm3);
                var Email1 = ds1.Tables[0].Rows[0]["Email"].ToString();
                var Email2 = "";
                var HrMail = System.Configuration.ConfigurationManager.AppSettings["hrEmail"].ToString();
                if (ds1.Tables[1].Rows.Count > 0)
                {
                    Email2 = ds1.Tables[1].Rows[0]["Email"].ToString();
                }
                var Email3 = ds1.Tables[2].Rows[0]["Email"].ToString();
                var backup1Mail= ds2.Tables[2].Rows[0]["Email"].ToString();
                var backup2Mail = ds3.Tables[2].Rows[0]["Email"].ToString();
                var Tomail = Email3;
                var cc = Email1 + "," + Email2+","+HrMail+","+ backup1Mail+","+ backup2Mail; 
                string Subject = "Your Leave Request Is Approved";
                var Html = "Dear " + EmployeeName + ",<br/><br/>";
                if (String.Format("{0:ddd, MMM d, yyyy}", Fromdate) == String.Format("{0:ddd, MMM d, yyyy}", Todate))
                {
                    Html = Html + "Your" + " " + LeaveName + " " + "leave application on" + " " + String.Format("{0:ddd, MMM d, yyyy}", Fromdate) + " " + "  is approved by your manager.<br/><br/>";
                }
                else
                {
                    Html = Html + "Your" + " " + LeaveName + " " + "application from" + " " + String.Format("{0:ddd, MMM d, yyyy}", Fromdate) + " " + "to" + " " + String.Format("{0:ddd, MMM d, yyyy}", Todate) + "  is approved by your manager.<br/><br/>";

                }
                Html = Html + "Remarks:" + Comments + ",<br/><br/>";
                Html = Html + "Best Regards, <br/> Red Dot Distribution";
                Tomail = "mainak@reddotdistribution.com";
                cc = "pratim.d@reddotdistribution.com"+","+"mainakg81@gmail.com";
                SendMail.Send(Tomail, cc, Subject, Html, true);

            }
            catch (Exception ex)
            {
                throw ex;
            }
            return RedirectToAction("Index", "RDD_LeaveApproval");
        }
        public ActionResult SendRejectedEmail(int loginUserId, string Fromdate, string Todate, string EmployeeName, string LeaveName, string Comments, int Backup1Id, int Backup2Id)
        {
            try
            {

                DataSet ds1 = new DataSet();
                DataSet ds2 = new DataSet();
                DataSet ds3 = new DataSet();
                SqlParameter[] prm1 =
               {
                   new SqlParameter("@LoginUserId",loginUserId)
                };
                SqlParameter[] prm2 =
              {
                   new SqlParameter("@LoginUserId",Backup1Id)
                };
                SqlParameter[] prm3 =
              {
                   new SqlParameter("@LoginUserId",Backup2Id)
                };
                ds1 = Com.ExecuteDataSet("RDD_GetManagerDetails", CommandType.StoredProcedure, prm1);
                ds2 = Com.ExecuteDataSet("RDD_GetManagerDetails", CommandType.StoredProcedure, prm2);
                ds3 = Com.ExecuteDataSet("RDD_GetManagerDetails", CommandType.StoredProcedure, prm3);

                var Email1 = ds1.Tables[0].Rows[0]["Email"].ToString();
                var Email2 = "";
                var HrMail = System.Configuration.ConfigurationManager.AppSettings["hrEmail"].ToString();
                if (ds1.Tables[1].Rows.Count > 0)
                {
                    Email2 = ds1.Tables[1].Rows[0]["Email"].ToString();
                }
                var Email3 = ds1.Tables[2].Rows[0]["Email"].ToString();
                var backup1Mail = ds2.Tables[2].Rows[0]["Email"].ToString();
                var backup2Mail = ds3.Tables[2].Rows[0]["Email"].ToString();
                var Tomail = Email3;
                var cc = Email1 + "," + Email2 + "," + HrMail + "," + backup1Mail + "," + backup2Mail;
                string Subject = "Your Leave Request is Rejected";
                var Html = "Dear " + EmployeeName + ",<br/><br/>";
                if(String.Format("{0:ddd, MMM d, yyyy}", Fromdate) == String.Format("{0:ddd, MMM d, yyyy}", Todate))
                {
                    Html = Html + "Your" + " " + LeaveName + " " + "leave application on" + " " + String.Format("{0:ddd, MMM d, yyyy}", Fromdate) + " " +  "  is rejected by your manager.<br/><br/>";
                }
                else
                {
                    Html = Html + "Your" + " " + LeaveName + " " + "leave application from" + " " + String.Format("{0:ddd, MMM d, yyyy}", Fromdate) + " " + "to" + " " + String.Format("{0:ddd, MMM d, yyyy}", Todate) + "  is rejected by your manager.<br/><br/>";

                }
                Html = Html + "Remarks:" + Comments + ",<br/><br/>";
                Html = Html + "Best Regards, <br/> Red Dot Distribution";
               // Tomail = "mainak@reddotdistribution.com";
               // cc = "pratim.d@reddotdistribution.com" + "," + "mainakg81@gmail.com";
                SendMail.Send(Tomail, cc, Subject, Html, true);

            }
            catch (Exception ex)
            {
                throw ex;
            }
            return RedirectToAction("Index", "RDD_LeaveApproval");
        }

    }
}