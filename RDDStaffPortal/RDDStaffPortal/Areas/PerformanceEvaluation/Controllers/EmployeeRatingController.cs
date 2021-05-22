using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using System.Data.SqlClient;
using RDDStaffPortal.DAL;
using RDDStaffPortal.DAL.PerformanceEvaluation;
using RDDStaffPortal.DAL.DataModels.PerformanceEvaluation;
using Newtonsoft.Json;
using System.IO;
using System.Text;
using System.Web.UI;
using iTextSharp.text;
using iTextSharp.text.pdf;
using iTextSharp.tool.xml;
using System.Net;
using iTextSharp.text.html.simpleparser;

namespace RDDStaffPortal.Areas.PerformanceEvaluation.Controllers
{
    public class EmployeeRatingController : Controller
    {
        RDD_Employeerating_DbOperation rDD_EmpRating_TemplateDb = new RDD_Employeerating_DbOperation();
        CommonFunction cf = new CommonFunction();
        // GET: PerformanceEvaluation/EmployeeRating
        public ActionResult Index()
        {
            return View();
        }
        public ActionResult GetEmployeeDetails()
        {
            ContentResult retVal = null;
            DataSet ds;
            string LoginName = User.Identity.Name;
            try
            {
                ds = rDD_EmpRating_TemplateDb.GetEmployeeDetails(LoginName);
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
        public ActionResult GetCategoryList()
        {
            ContentResult retVal = null;
            DataSet ds;
            string LoginName = User.Identity.Name;
            try
            {
                ds = rDD_EmpRating_TemplateDb.GetCategoryList(LoginName);
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
        public ActionResult GetQuestionList(int CategoryId,string Qperiod)
        {
            ContentResult retVal = null;
            DataSet ds;
            string LoginName = User.Identity.Name;
            try
            {
                ds = rDD_EmpRating_TemplateDb.GetQuestionList(CategoryId,LoginName,Qperiod);
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
        public ActionResult SaveEmployeeRating(RDD_EmployeeRating rDD_EmpAppraisal)
        {
            rDD_EmpAppraisal.Emp_SubmittedBy = User.Identity.Name;
            if (rDD_EmpAppraisal.EditFlag == true)
            {
                //rDD_EmpAppraisal.E = User.Identity.Name;
                rDD_EmpAppraisal.Emp_LastUpdatedOn = System.DateTime.Now;
            }
            return Json(rDD_EmpRating_TemplateDb.SaveEmployeeRating(rDD_EmpAppraisal), JsonRequestBehavior.AllowGet);
        }
        [Route("FinalSaveEmployeeRating")]
        public ActionResult FinalSaveEmployeeRating(int EmployeeId,int Year,string Period)
        {
            //ContentResult retVal = null;
            string Loginname = User.Identity.Name;
            var t = rDD_EmpRating_TemplateDb.FinalSaveEmployeeRating(EmployeeId, Year, Period, Loginname);
            if (t[0].Id != -1)
            {
                string MailResponse = "";
                try
                {
                    DataSet ds = new DataSet();
                    ds = rDD_EmpRating_TemplateDb.GetMailDetails(EmployeeId, Period, Year);
                    string ToMail = ds.Tables[1].Rows[0]["ToMail"].ToString();
                    string cc = ds.Tables[2].Rows[0]["CC"].ToString();
                    string MailBody = ds.Tables[3].Rows[0]["Body"].ToString();
                    string Subject = ds.Tables[0].Rows[0]["MailSubject"].ToString();
                    MailResponse = SendMail.Send(ToMail, cc, Subject, MailBody, true);
                }
                catch (Exception ex)
                {
                    MailResponse = ex.Message;                  
                }                
            }
            else
            {
                t[0].Outtf = false;
            }
            return Json(t, JsonRequestBehavior.AllowGet);
        }

        public ActionResult GetDetailsByYearAndPeriod(string Qperiod)
        {
            ContentResult retVal = null;
            DataSet ds;            
            try
            {
                string Loginname = User.Identity.Name;
                ds = rDD_EmpRating_TemplateDb.GetDetailsByYearAndPeriod(Qperiod,Loginname);
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

        public ActionResult ViewEmployeerating()
        {
            return View();
        }

        public ActionResult ViewEmployeeRatingData(string UrlId)
        {
            ContentResult retVal = null;
            DataSet ds;
            try
            {
                string Loginname = User.Identity.Name;
                ds = rDD_EmpRating_TemplateDb.GetDetailsByClickUrl(UrlId,Loginname);
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

        public ActionResult GetCategoryWiseDetailsOnClickUrl(int CategoryId, string UrlId)
        {
            ContentResult retVal = null;
            DataSet ds;
            string LoginName = User.Identity.Name;
            try
            {
                ds = rDD_EmpRating_TemplateDb.GetCategoryWiseDetailsOnClickUrl(CategoryId, UrlId);
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

        [HttpPost]
        public JsonResult UploadPDF(string EmployeeId)
        {
            string fname = "";
            string strMappath = "";
            string _imgname = string.Empty;
            string _imgname1 = string.Empty;
            if (Request.Files.Count > 0)
            {
                try
                {
                    //string str = EmployeeId + "_" + User.Identity.Name;
                    //Guid obj = Guid.NewGuid();
                    //strMappath = "~/excelFileUpload/" + "PV/" + User.Identity.Name + "/" + type + "/";
                    strMappath = "~/excelFileUpload/" + "PerformanceEvaluation/";
                    //if (!Directory.Exists(strMappath))
                    //{
                    //    Directory.CreateDirectory(System.IO.Path.Combine(Server.MapPath(strMappath)));
                    //}
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
                            if (_ext != ".PDF")
                            {
                                return Json("Error occurred. Error details: Only Pdf", JsonRequestBehavior.AllowGet);
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

        public ActionResult SavePdf(RDD_EmployeeRating rDD_AppraisalPdfUpload)
        {
            rDD_AppraisalPdfUpload.Emp_SubmittedBy = User.Identity.Name;
            var t = rDD_EmpRating_TemplateDb.SavePdf(rDD_AppraisalPdfUpload);
            if (t[0].Id != -1)
            {
                string Mailresponse = "";
                try
                {
                    DataSet ds;
                    ds = rDD_EmpRating_TemplateDb.GetMailDetailsForPDF(rDD_AppraisalPdfUpload);
                    string ToMail = ds.Tables[1].Rows[0]["ToMail"].ToString();
                    string cc = ds.Tables[2].Rows[0]["CC"].ToString();
                    string MailBody = ds.Tables[3].Rows[0]["Body"].ToString();
                    string Subject = ds.Tables[0].Rows[0]["MailSubject"].ToString();
                    string AttachmentPath = ds.Tables[4].Rows[0]["AttachmentUrl"].ToString();
                    string AttachmentPdf = string.Empty;
                    if(AttachmentPath!=null && !DBNull.Value.Equals(AttachmentPath))
                    {
                        AttachmentPdf = System.IO.Path.Combine(Server.MapPath(AttachmentPath));
                    }
                    Mailresponse = SendMail.SendMailWithAttachment(ToMail, cc, Subject, MailBody, true, AttachmentPdf);
                }
                catch (Exception ex)
                {
                    Mailresponse = ex.Message;
                }
            }
            return Json(t, JsonRequestBehavior.AllowGet);
        }

        public ActionResult GeneratePDF(string UrlId)
        {
            string PDFname = "";
            DataSet ds = new DataSet();
            ds = rDD_EmpRating_TemplateDb.GeneratePDF(UrlId);
            PDFname = ds.Tables[1].Rows[0]["PdfName"].ToString();
            StringBuilder sb = new StringBuilder();
            sb.Append(ds.Tables[0].Rows[0]["GetPDFdata"].ToString());
            using (MemoryStream stream = new System.IO.MemoryStream())
            {
                StringReader reader = new StringReader(sb.ToString());
                Document PdfFile = new Document(PageSize.A4);
                //PdfFile.NewPage();
                PdfWriter writer = PdfWriter.GetInstance(PdfFile, stream);
                PdfFile.Open();
                PdfFile.Add(new Chunk(""));
                XMLWorkerHelper.GetInstance().ParseXHtml(writer, PdfFile, reader);
                PdfFile.Close();
                return File(stream.ToArray(), "application/pdf", "" + PDFname + ".pdf");
            }            
        }
    }
}