using RDDStaffPortal.DAL.Admin;
using RDDStaffPortal.DAL.LMS;
using RDDStaffPortal.DAL.DataModels.LMS;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using System.Data;
using System.Data.SqlClient;
using RDDStaffPortal.DAL;
using Newtonsoft.Json;
using System.Text;
using System.IO;
using System.Web.UI;
using System.Net;
using iTextSharp.text.html.simpleparser;
using iTextSharp.text;
using iTextSharp.text.pdf;

namespace RDDStaffPortal.Areas.LMS.Controllers
{
    [Authorize]
    public class RDD_LMSReportController : Controller
    {
        RDD_LMSReport_Db_Operation rDD_LMSReport_TemplatesDb = new RDD_LMSReport_Db_Operation();
        // GET: LMS/RDD_LMSReport
        public ActionResult Index()
        {
            RDD_LMSReport rDD_LMSReport = new RDD_LMSReport();
            rDD_LMSReport.CountryList = rDD_LMSReport_TemplatesDb.GetCountryList();
            //rDD_LMSReport.DepartmentList = rDD_LMSReport_TemplatesDb.GetDepartmentByCountry();
            return View(rDD_LMSReport);
        }
        public ActionResult GetCountryWiseDepartment(string Cid)
        {
            ContentResult retVal = null;
            DataSet DS;
            try
            {
                DS = rDD_LMSReport_TemplatesDb.DepartmentByCountry(Cid);

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
        public ActionResult GetDeptWiseEmployee(int dept, string cid)

        {
            ContentResult retVal = null;
            DataSet DS;
            try
            {
                DS = rDD_LMSReport_TemplatesDb.EmployeeByDept(dept, cid);

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
        public ActionResult ShowEmployeeReport(int Empid)
        {
            List<RDD_LMSReport> rDD_LMSReports = new List<RDD_LMSReport>();
            rDD_LMSReports = rDD_LMSReport_TemplatesDb.GetAllData(Empid);
            return Json(rDD_LMSReports, JsonRequestBehavior.AllowGet);
        }
        public void GeneratePdfDetails(int LeaveRequestId, int EmployeeId, int LeaveTypeId)
        {
            CommonFunction Com = new CommonFunction();
            string Msg = "";
            DataSet ds = new DataSet();
            SqlParameter[] prm =
            {

                new SqlParameter("@EmployeeId",EmployeeId),
                new SqlParameter("@LeaveTypeId",LeaveTypeId),
                new SqlParameter("@LeaveRequestId",LeaveRequestId),

            };
            ds = Com.ExecuteDataSet("RDD_PDFgenerateforReport", CommandType.StoredProcedure, prm);                 

            StringBuilder sb = new StringBuilder();

            sb.Append("<table align:centre border='1'>");
            sb.Append("<tr> ");
            sb.Append("<td colspan = '6' align='center'> <img src='https://www.reddotdistribution.com/images/reddot%20logo%20black.png' alt='' border='0' width='150' class='center'/> </td>");
            sb.Append("</tr>");
            sb.Append("<tr>");
            sb.Append("<th colspan='6'><b><u>Personal Leave Details</u></b>:</th>");
            sb.Append("</tr>");
            sb.Append("<tr>");
            sb.Append("<th ><b>Full Name</b>:</th>");
            sb.Append("<td colspan='2'>" + ds.Tables[0].Rows[0]["FullName"] + "</td>");
            sb.Append("<th ><b>ID No</b>:</th>");
            sb.Append("<td colspan='2'>" + ds.Tables[0].Rows[0]["EmployeeNo"] + "</td>");
            sb.Append("</tr>");
            sb.Append("<tr >");
            sb.Append("<th><b>Department</b>:</th>");
            sb.Append(" <td colspan='2'>" + ds.Tables[0].Rows[0]["DeptName"] + "</td>");            
            sb.Append("<th><b>Leave Name</b>:</th>");
            sb.Append(" <td colspan='2'>" + ds.Tables[0].Rows[0]["LeaveName"] + "</td>");
            sb.Append("</tr>");
            sb.Append("<tr>");
            sb.Append("<th colspan='6'><b>Leave requested from</b>:&nbsp;&nbsp;" + ds.Tables[0].Rows[0]["FromDate"] + " &nbsp;&nbsp;<b> to </b>&nbsp;&nbsp; " + ds.Tables[0].Rows[0]["ToDate"] + " &nbsp; &nbsp; &nbsp; &nbsp;&nbsp; &nbsp; &nbsp; &nbsp;<b> No of Days</b> : &nbsp; &nbsp; " + ds.Tables[0].Rows[0]["NoOfdays"] + " </th>");
            sb.Append("</tr>");
            sb.Append(" <th colspan='2'><b>Reason For Request</b>:</th>");
            sb.Append("<td colspan='4'>" + ds.Tables[0].Rows[0]["Reason"] + "</td>");
            sb.Append("</tr>");
            sb.Append("<tr>");
            sb.Append("<th colspan='2'><b>Home/Leave Address</b>:</th>");
            sb.Append("<td colspan='4'>" + ds.Tables[0].Rows[0]["Current_Address"] + "</td>");
            sb.Append("</tr>");
            sb.Append("<tr>");
            sb.Append("<th colspan='6'><b>Applicant Signature</b>:&nbsp;&nbsp;" + ds.Tables[0].Rows[0]["FullName"] + "&nbsp;&nbsp;  &nbsp; &nbsp;&nbsp;&nbsp;  &nbsp; &nbsp;<b> Date</b> : &nbsp; &nbsp; " + ds.Tables[0].Rows[0]["todayDate"] + " </th>");
            sb.Append("</tr>");
            sb.Append("<tr>");
            sb.Append("<th colspan='2'><b>BackUp Person 1</b>:</th>");
            sb.Append("<td colspan='4'>" + ds.Tables[0].Rows[0]["EmpBackup1"] + "</td>");
            sb.Append("</tr>");
            sb.Append("<th colspan='2'><b>BackUp Person 1 Signature</b>:</th>");
            sb.Append("<td colspan='4'>" + ds.Tables[0].Rows[0]["EmpBackup1"] + "</td>");
            sb.Append("</tr>");            
            sb.Append("<tr>");
            sb.Append("<th colspan='2'><b>BackUp Person 2</b>:</th>");
            sb.Append("<td colspan='4'>" + ds.Tables[0].Rows[0]["EmpBackup2"] + "</td>");
            sb.Append("</tr>");
            sb.Append("<th colspan='2'><b>BackUp Person 2 Signature</b>:</th>");
            sb.Append("<td colspan='4'>" + ds.Tables[0].Rows[0]["EmpBackup2"] + "</td>");
            sb.Append("</tr>");
            sb.Append("<tr>");
            sb.Append(" <th colspan='6'><b><u>Computational Of Outstanding Days</u></b>:</th>");
            sb.Append("</tr>");
            sb.Append("<tr>");
            sb.Append("<th colspan='2'><b>Current Year Entitlement</b>:</th>");
            sb.Append("<td colspan='2'>&nbsp;&nbsp;" + ds.Tables[0].Rows[0]["CurrentYearEntitlement"] + "</td><td colspan='2'> &nbsp;</td>");
            sb.Append("</tr>");
            sb.Append("<tr>");
            sb.Append("<th colspan='2'><b>Less Days Already Taken</b>:</th>");
            sb.Append("<td colspan='2'>&nbsp;&nbsp;" + ds.Tables[0].Rows[0]["TotalTakenLeave"] + "</td><td colspan='2'> &nbsp;</td>");
            sb.Append("</tr>");
            sb.Append("<tr>");
            sb.Append("<th colspan='2'><b>Less Days Applied For</b>:</th>");
            sb.Append("<td colspan='2'>&nbsp;&nbsp;" + ds.Tables[0].Rows[0]["TotalPendingLeave"] + "</td><td colspan='2'> &nbsp;</td>");
            sb.Append("</tr>");
            sb.Append("<tr>");
            sb.Append("<th colspan='2'><b>Balance If Approved</b>:</th>");
            sb.Append("<td colspan='2'>&nbsp;&nbsp;" + ds.Tables[0].Rows[0]["BalanceLeave"] + "</td><td colspan='2'> &nbsp;</td>");
            sb.Append("</tr>");
            sb.Append(" <th colspan='6'><b><u>Recommendation By Head Of Department</u></b>:</th>");
            sb.Append("</tr>");
            sb.Append("<tr>");
            sb.Append("<th colspan='3'><b>Approved To Proceed For Annual Leave</b>:</th>");
            sb.Append("<td>&nbsp;&nbsp;0 Days</td><td colspan='2'> &nbsp;</td>");
            sb.Append("</tr>");
            sb.Append("<tr>");
            sb.Append(" <th colspan='6'><b><u>Recommendation By Managing Director</u></b>:</th>");
            sb.Append("</tr>");
            sb.Append("<tr>");
            sb.Append("<th colspan='4'><b>Approved To Proceed For Annual Leave</b>:</th>");
            sb.Append("<td>&nbsp;&nbsp;0 Days</td><td colspan='2'> &nbsp;</td>");
            sb.Append("</tr>");
            sb.Append("<tr>");
            sb.Append("<th colspan='5'><b>Salary Advance Payment Approved/Not Approved</b>:</th>");
            sb.Append("<td>&nbsp;&nbsp;</td>");
            sb.Append("</tr>");
            sb.Append("<tr>");
            sb.Append("<th colspan='6'><b>Dept Manager</b>:&nbsp;&nbsp;" + ds.Tables[0].Rows[0]["ManagerName"] + " &nbsp;&nbsp;&nbsp; &nbsp;&nbsp;&nbsp;&nbsp; &nbsp;&nbsp;&nbsp;&nbsp; &nbsp;<b>General Manager</b> : &nbsp; &nbsp;</th>");
            sb.Append("<td>&nbsp;&nbsp;</td><td> &nbsp;</td>");
            sb.Append("</tr");
            sb.Append("<tr>");
            sb.Append("<th colspan='6'><b>Checked By</b>:</th>");
            sb.Append("<td colspan='2'></td>");
            sb.Append("</tr>");
            sb.Append("</table>");          
           

            using (StringWriter sw = new StringWriter())
            {
                using (HtmlTextWriter hw = new HtmlTextWriter(sw))
                {
                    //Generate Invoice (Bill) Header.

                    //Export HTML String as PDF.
                    ServicePointManager.SecurityProtocol = SecurityProtocolType.Tls12;
                    StringReader sr = new StringReader(sb.ToString());
                    Document pdfDoc = new Document(PageSize.A4, 10f, 10f, 10f, 0f);
                    HTMLWorker htmlparser = new HTMLWorker(pdfDoc);
                    using (MemoryStream ms = new MemoryStream())
                    {
                        PdfWriter writer = PdfWriter.GetInstance(pdfDoc, ms);
                        pdfDoc.Open();
                        htmlparser.Parse(sr);
                        pdfDoc.Close();
                        byte[] bytes = ms.ToArray();
                        ms.Close();
                        Response.Clear();
                        Response.ContentType = "application/pdf";
                        Response.AddHeader("Content-Disposition", "attachment; filename=Leave Form.pdf");
                        Response.ContentType = "application/pdf";
                        Response.Buffer = true;
                        Response.Cache.SetCacheability(HttpCacheability.NoCache);
                        Response.BinaryWrite(bytes);
                        Response.End();
                        Response.Close();
                    }
                    //Response.End();
                }
            }
        }


    }
}