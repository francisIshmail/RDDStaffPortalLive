using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using RDDStaffPortal.DAL;
using RDDStaffPortal.DAL.PerformanceEvaluation;
using RDDStaffPortal.DAL.DataModels.PerformanceEvaluation;
using Newtonsoft.Json;
using System.IO;
using System.Data;
using System.Text;
using iTextSharp.text;
using iTextSharp.text.pdf;
using iTextSharp.tool.xml;

namespace RDDStaffPortal.Areas.PerformanceEvaluation.Controllers
{
    public class ViewAllAppraisalController : Controller
    {
        RDD_ViewAllAppraisal_DbOperation rDD_ViewAppraisal_TemplateDb = new RDD_ViewAllAppraisal_DbOperation();
        // GET: PerformanceEvaluation/ViewAllAppraisal
        public ActionResult Index()
        {
            return View();
        }
        public ActionResult GetPartial()
        {
            return PartialView("~/Areas/PerformanceEvaluation/Views/ViewAllAppraisal/ManagerRatingPartial.cshtml");
        }
        public ActionResult GetDetailsForHR()
        {
            ContentResult retVal = null;
            DataSet ds;
            //string LoginName = User.Identity.Name;
            try
            {
                ds = rDD_ViewAppraisal_TemplateDb.GetDetailsForHR();
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
        public ActionResult GetEmployeeDetailsOnPeriod(string Qperiod)
        {
            ContentResult retVal = null;
            DataSet ds;
            //string LoginName = User.Identity.Name;
            try
            {
                ds = rDD_ViewAppraisal_TemplateDb.GetEmployeeDetailsOnPeriod(Qperiod);
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
        public ActionResult GeneratePDF(string UrlId)
        {
            string PDFname = "";
            DataSet ds = new DataSet();
            ds = rDD_ViewAppraisal_TemplateDb.GeneratePDF(UrlId);
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