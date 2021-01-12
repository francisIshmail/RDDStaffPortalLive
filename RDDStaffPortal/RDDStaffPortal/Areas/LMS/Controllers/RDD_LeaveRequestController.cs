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
using System.Data.SqlClient;
using RDDStaffPortal.DAL;
using System.Net.Mail;
using Newtonsoft.Json;

namespace RDDStaffPortal.Areas.LMS.Controllers
{   
    [Authorize]
    public class RDD_LeaveRequestController : Controller
    {
        CommonFunction Com = new CommonFunction();
        RDD_LeaveRequest_Db_Operation rDD_LeaveRequest_TemplatesDb = new RDD_LeaveRequest_Db_Operation();
        // GET: LMS/RDD_LeaveRequest
        public ActionResult Index()
        {
            RDD_LeaveRequest rDD_LeaveRequest = new RDD_LeaveRequest();
            rDD_LeaveRequest.EmployeeLists = rDD_LeaveRequest_TemplatesDb.GetEmployee();                       
            rDD_LeaveRequest.EmployeeId = rDD_LeaveRequest_TemplatesDb.GetEmployeeIdByLoginName(User.Identity.Name);
            rDD_LeaveRequest.LeaveTypeList = rDD_LeaveRequest_TemplatesDb.GetLeaveTypeName(rDD_LeaveRequest.EmployeeId);
            rDD_LeaveRequest.WeeklyOffDays = rDD_LeaveRequest_TemplatesDb.GetWeeklyOffDay(rDD_LeaveRequest.EmployeeId);
            DataSet ds = rDD_LeaveRequest_TemplatesDb.GetHRRole(User.Identity.Name);
            if (ds.Tables.Count > 0)
            {


                rDD_LeaveRequest.HR = Convert.ToBoolean(ds.Tables[0].Rows[0]["IsHRUser"].ToString());


            }
            return View(rDD_LeaveRequest);
        }
        public ActionResult GetLeaveRequest_List()
        {

            List<RDD_LeaveRequest> modules = new List<RDD_LeaveRequest>();
            modules = rDD_LeaveRequest_TemplatesDb.GetList();
            return PartialView(modules);
        }
        [Route("SaveLeaveRequest")]
        public ActionResult SaveLeaveRequest(RDD_LeaveRequest RDD_LeaveRequest)
        {
            RDD_LeaveRequest.CreatedBy = User.Identity.Name;
            RDD_LeaveRequest.CreatedOn = DateTime.Now;
            if (RDD_LeaveRequest.Editflag == true)
            {
                RDD_LeaveRequest.LastUpdatedBy = User.Identity.Name;
                RDD_LeaveRequest.LastUpdatedOn = System.DateTime.Now;
            }
            return Json( rDD_LeaveRequest_TemplatesDb.Save(RDD_LeaveRequest), JsonRequestBehavior.AllowGet);
        }
        [Route("GetLeaveRequest")]
        public ActionResult GetData(string LeaveRequestId)
        {
            return Json(new { data = rDD_LeaveRequest_TemplatesDb.GetData(LeaveRequestId) }, JsonRequestBehavior.AllowGet);
        }
        [Route("GetWeeklyOffDay")]
        public ActionResult GetWeeklyOff(int EmployeeId)
        {
            return Json(new { data = rDD_LeaveRequest_TemplatesDb.GetWeeklyOffDay(EmployeeId) }, JsonRequestBehavior.AllowGet);
        }
       
        [HttpPost]
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
        public ActionResult SendEmail(int LeaveRequestId, int LoginUserid)
        {
            try
            {
                DataSet ds = new DataSet();
                DataSet ds1 = new DataSet();
                SqlParameter[] prm =
                {
                   new SqlParameter("@LeaveRequestId",LeaveRequestId)
                };
                ds = Com.ExecuteDataSet("RDD_GetLeaverequestDetailformail", CommandType.StoredProcedure, prm);

                SqlParameter[] prm1 =
                {
                   new SqlParameter("@LoginUserId",LoginUserid)
                };
                ds1 = Com.ExecuteDataSet("RDD_GetManagerDetails", CommandType.StoredProcedure, prm1);
                var LeaveName= ds.Tables[0].Rows[0]["LeaveName"];
                var Fromdate = ds.Tables[0].Rows[0]["FromDate"];
                var Todate = ds.Tables[0].Rows[0]["ToDate"];
                
                var L1ManagerName= ds1.Tables[0].Rows[0]["EmployeeName"].ToString();
                var Email1 = ds1.Tables[0].Rows[0]["Email"].ToString();
                var Email2 = "";
                if(ds1.Tables[1].Rows.Count>0)
                {
                    Email2 = ds1.Tables[1].Rows[0]["Email"].ToString();
                } 
                var EmployeeName= ds1.Tables[2].Rows[0]["EmployeeName"].ToString();
                var Email3 = ds1.Tables[2].Rows[0]["Email"].ToString();
                
                var Tomail = Email1;
                var cc = Email3 + "," + Email2;
                string Subject = "Leave Approval Request";
                
                var Html = "Dear " + L1ManagerName + ",<br/><br/>";
                if (String.Format("{0:ddd, MMM d, yyyy}", Fromdate) == String.Format("{0:ddd, MMM d, yyyy}", Todate))
                {
                    Html = Html + "" + EmployeeName + " has applied for " + LeaveName + " " + "leave on" + " " + String.Format("{0:ddd, MMM d, yyyy}", Fromdate) + " and it is pending for approval by you.<br/><br/>";
                }
                else
                {
                    Html = Html + "" + EmployeeName + " has applied for " + LeaveName + " " + "leave from" + " " + String.Format("{0:ddd, MMM d, yyyy}", Fromdate) + " " + "to" + " " + String.Format("{0:ddd, MMM d, yyyy}", Todate) + " and it is pending for approval by you.<br/><br/>";
                }                
                Html = Html + "Best Regards, <br/> Red Dot Distribution";
                Tomail = "mainak@reddotdistribution.com";
                cc = "pratim.d@reddotdistribution.com";
                SendMail.Send(Tomail, cc, Subject, Html, true);
                
            }
            catch(Exception ex)
            {
                throw ex;
            }
            return RedirectToAction("Index", "RDD_LeaveRequest");
        }
        [Route("GETHOLIDAY")]
        public ActionResult GetHolidayCountryWise(int EmployeeId)
        {
            ContentResult retVal = null;
            DataSet ds;
            try
            {
                ds = rDD_LeaveRequest_TemplatesDb.GetHolidayCountryWise(EmployeeId);
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

    }
}