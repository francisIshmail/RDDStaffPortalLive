using iTextSharp.text;
using iTextSharp.text.html.simpleparser;
using iTextSharp.text.pdf;
using Newtonsoft.Json;
using RDDStaffPortal.DAL;
using RDDStaffPortal.DAL.DataModels.Voucher;
using RDDStaffPortal.DAL.Voucher;
using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.IO;
using System.Text;
using System.Web;
using System.Web.Mvc;
using System.Web.UI;


namespace RDDStaffPortal.Areas.SAP.Controllers
{
    [Authorize]
    public class RDD_PVController : Controller
    {
        CommonFunction Com = new CommonFunction();
        RDD_PV_VoucherDbOperation _RDDPVOp = new RDD_PV_VoucherDbOperation();
        // GET: SAP/RDD_PV
        public ActionResult Index()
        {
            RDD_PV PV = new RDD_PV();
            PV = _RDDPVOp.GetDropList(User.Identity.Name);
            return View(PV);
        }
        [Route("DeleteRDDPV")]
        public ActionResult DeletePV(int PVId)
        {
            return Json(new { data = _RDDPVOp.Delete1(PVId) }, JsonRequestBehavior.AllowGet);
        }


        [Route("ADDRDDPV")]
        public ActionResult ADDRDDPV(int PVId = -1)
        {
            RDD_PV PV = new RDD_PV();
            PV = _RDDPVOp.GetDropList(User.Identity.Name);
            PV.EditFlag = false;
            PV.SaveFlag = false;
            PV.CreatedOn = System.DateTime.Now;
            PV.CreatedBy = User.Identity.Name;
            PV.IsDraft = true;
            if (PVId != -1)
            {
                PV = _RDDPVOp.GetData(User.Identity.Name, PVId, PV);
                PV.EditFlag = true;
            }
            else
            {
                string str = PV.RefNo + "_" + User.Identity.Name;

                string strMappath = "~/excelFileUpload/" + "PV/" + str + "/";
                string fullPath = Request.MapPath(strMappath);
                if (Directory.Exists(fullPath))
                {


                    string[] filePaths = Directory.GetFiles(fullPath);
                    foreach (string filePath in filePaths)
                        System.IO.File.Delete(filePath);

                    Directory.Delete(fullPath);
                }
            }
            return PartialView("~/Areas/SAP/Views/RDD_PV/ADDRDDPV.cshtml", PV);
        }

        [Route("VIEWRDDPV")]
        public ActionResult VIEWRDDPV(int PVId)
        {
            RDD_PV PV = new RDD_PV();
            //PV = _RDDPVOp.GetDropList(User.Identity.Name);

            PV.IsDraft = true;

            PV = _RDDPVOp.GetData(User.Identity.Name, PVId, PV);
            PV.EditFlag = true;
            PV.CreatedBy = User.Identity.Name;

            return PartialView("~/Areas/SAP/Views/RDD_PV/VIEWRDDPV.cshtml", PV);
        }
        [HttpPost]
        public JsonResult UploadDoc(string Refno, string type)
        {
            string fname = "";
            string strMappath = "";
            string _imgname = string.Empty;
            string _imgname1 = string.Empty;
            if (Request.Files.Count > 0)
            {
                try
                {
                    string str = Refno + "_" + User.Identity.Name;
                    //strMappath = "~/excelFileUpload/" + "PV/" + User.Identity.Name + "/" + type + "/";
                    strMappath = "~/excelFileUpload/" + "PV/" + str + "/";

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
                            if ((_ext != ".JPG" && _ext != ".PNG" && _ext != ".GIF" && _ext != ".PDF") && type == "Header")
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

        [Route("SAVERDDPV")]
        public ActionResult SaveRDDPV(RDD_PV RDDPV)
        {

            RDDPV.CreatedBy = User.Identity.Name;
            RDDPV.CreatedOn = System.DateTime.Now;
            if (RDDPV.EditFlag == true)
            {
                RDDPV.LastUpdatedBy = User.Identity.Name;
                RDDPV.LastUpdatedOn = System.DateTime.Now;
            }



            //RDDPV.CAapprovedOn = System.DateTime.Now;
            //RDDPV.CFOapprovedOn = System.DateTime.Now;
            //RDDPV.CMapprovedOn = System.DateTime.Now;

            //RDDPV.ClosedDate = System.DateTime.Now;


            return Json(_RDDPVOp.Save1(RDDPV), JsonRequestBehavior.AllowGet);
        }
        [Route("ChangeVoucherStatus")]
        public ActionResult VoucherStatus(string DOC_Status, int PVID)
        {
            return Json(_RDDPVOp.ChangeVoucherStatus(DOC_Status, PVID), JsonRequestBehavior.AllowGet);
        }

        [Route("GETRDDPV")]
        public ActionResult GetRDDPV(int pagesize, int pageno, string psearch, string SearchCon)
        {
            List<RDD_PV> RPV = new List<RDD_PV>();
            RPV = _RDDPVOp.GetALLDATA(User.Identity.Name, pagesize, pageno, psearch, SearchCon);
            return Json(new { data = RPV }, JsonRequestBehavior.AllowGet);
        }
        [Route("GetVendor")]
        public ActionResult GetVendor(string DBName, string Vtype)
        {
            ContentResult ret = null;
            DataSet DS = _RDDPVOp.GetVendor(DBName, Vtype);

            try
            {
                if (DS.Tables.Count > 0)
                {
                    ret = Content(JsonConvert.SerializeObject(DS), "application/json");
                }
            }
            catch (Exception)
            {
                ret = null;
            }
            return ret;

        }

        [Route("GetBank")]
        public ActionResult GetBank(string DBName)
        {
            ContentResult ret = null;
            DataSet DS = _RDDPVOp.GetBank(DBName);

            try
            {
                if (DS.Tables.Count > 0)
                {
                    ret = Content(JsonConvert.SerializeObject(DS), "application/json");
                }
            }
            catch (Exception)
            {
                ret = null;
            }
            return ret;

        }


        [Route("GetVendorAgeing")]
        public ActionResult GetVendorDue(string DBName, string BP)
        {
            ContentResult ret = null;
            DataSet DS = _RDDPVOp.GetVendorAgeing(DBName, BP);

            try
            {
                if (DS.Tables.Count > 0)
                {
                    ret = Content(JsonConvert.SerializeObject(DS), "application/json");
                }
            }
            catch (Exception)
            {
                ret = null;
            }
            return ret;

        }

        public ActionResult DownloadPdf(string  p_pvid)
        {
            DataSet ds;
            try
            {
                SqlParameter[] Para = {

                    new SqlParameter("@p_PVId",p_pvid),


                };
                ds = Com.ExecuteDataSet("PV_GetDataToPrintPV", CommandType.StoredProcedure, Para);
                StringBuilder sb = new StringBuilder();
              

                #region html
                sb.Append("<table width='100%' align='center' border='1'>");
                sb.Append("<tr><td width='100%' colspan='6'>");
                sb.Append("<table width='100%' align='center' border='0'>");
                sb.Append("<tr>");
                sb.Append("<td colspan = '6'> &nbsp; </td>");
                sb.Append("</tr>");
                sb.Append("<tr>");
                sb.Append("<td colspan = '6' align = 'center'><span style = 'font-size:medium'> <b> <u> PAYMENT VOUCHER </u> </b> </span> </td>");
                sb.Append("</tr>");
                sb.Append("<tr>");
                sb.Append("<td colspan = '4' width = '70%'> &nbsp; </td>");
                sb.Append("<td width = '15%'><span style = 'font-size:10px;font-family:calibri;'> PV No </span> </td>");
                sb.Append("<td width = '15%'><span style = 'font-size:10px;font-family:calibri;'> <b> " + ds.Tables[2].Rows[0]["RefNo"] + @" </b> </span> </td>");
                sb.Append("</tr>");
                sb.Append("<tr>");
                sb.Append("<td width = '17%'><span style = 'font-size:10px;font-family:calibri;'> Paid to Mr./ Ms.</span>   </td>");
                sb.Append("<td width = '52%' colspan = '3'><span style = 'font-size:10px;font-family:calibri;'> <b><u> " + ds.Tables[2].Rows[0]["VendorEmployee"] + @" </u></b> </span> </td>");
                sb.Append("<td width = '15%'><span style = 'font-size:10px;font-family:calibri;'> Date </span> </td>");
                sb.Append("<td width = '15%'><span style = 'font-size:10px;font-family:calibri;'> <b> " + Convert.ToDateTime(ds.Tables[2].Rows[0]["PayDate"]).ToString("dd / MM / yyyy") + @" </b> </span> </td>");
                sb.Append("</tr>");
                sb.Append("<tr>");
                sb.Append("<td width = '17%'><span style = 'font-size:10px;font-family:calibri;'> Cash / Cheque No </span> </td>");
                sb.Append("<td width = '18%'><span style = 'font-size:10px;font-family:calibri;'> <b> " + ds.Tables[2].Rows[0]["PayRefNO"] + @" </b> </span> </td>");
                sb.Append("<td width = '15%'><span style = 'font-size:10px;font-family:calibri;'> Bank Name </span> </td>");
                sb.Append("<td width = '20%'><span style = 'font-size:10px;font-family:calibri;'> <b> " + ds.Tables[2].Rows[0]["BankName"] + @" </b> </span> </td>");
                sb.Append("<td width = '15%'><span style = 'font-size:10px;font-family:calibri;'> Payment Date </span> </td> ");
                sb.Append("<td width = '15%'><span style = 'font-size:10px;font-family:calibri;'> <b> " + Convert.ToDateTime(ds.Tables[2].Rows[0]["PayDate"]).ToString("dd / MM / yyyy") + @" </b> </span> </td>");
                sb.Append("</tr>");
                sb.Append("<tr>");
                sb.Append("<td width = '100%' colspan = '6'> &nbsp;<br/></td>");
                sb.Append("</tr>");
                sb.Append("<tr>");
                sb.Append("<td width = '100%' colspan = '6'>");
                sb.Append("<table width = '100%' align = 'left' border = '1'>");
                sb.Append("<tr>");
                sb.Append("<td width = '8%'> &nbsp;<span style = 'font-size:10px;font-family:calibri;'>  <b> SrNo </b> </span>  </td> ");
                sb.Append("<td width = '72%' align = 'center'> &nbsp;<span style = 'font-size:10px;font-family:calibri;'>  <b> Particulars </b> </span> </td>");
                sb.Append("<td width = '20%' align = 'right'> &nbsp;<span style = 'font-size:10px;font-family:calibri;'>  <b> Amount(" + ds.Tables[2].Rows[0]["Currency"] + @") </b> </span> </td>");
                sb.Append("</tr>");
                int i = 0;
                decimal total = 0;
                while (ds.Tables[3].Rows.Count > i)
                {
                    sb.Append("<tr>");
                    sb.Append("<td width='8%' align='center' >  <span style='font-size:10px;font-family:calibri;'>");
                    sb.Append(ds.Tables[3].Rows[i]["LineRefNo"]);
                    sb.Append("</span></td>");
                    sb.Append("<td width='72%'>  <span style='font-size:10px;font-family:calibri;'>");
                    sb.Append(ds.Tables[3].Rows[i]["Description"]);
                    sb.Append("</span></td>");
                    sb.Append("<td width='20%' align='right' >  <span style='font-size:10px;font-family:calibri;'>");
                    sb.Append(ds.Tables[3].Rows[i]["AMount"]);
                    total = total + Convert.ToDecimal(ds.Tables[3].Rows[i]["Amount"].ToString());
                    sb.Append("</span></td>");
                    sb.Append("</tr>");
                    i++;
                }


                sb.Append("<tr> <td width='8%' >  <span style='font-size:10px;font-family:calibri;'>  &nbsp; </span> </td>   <td width='72%' align='right'  >  <span style='font-size:10px;font-family:calibri;'> <b> TOTAL ( " + ds.Tables[2].Rows[0]["Currency"] + " ) </b> </span> </td>");
                sb.Append("<td width='20%' align='right' >  <span style='font-size:10px;font-family:calibri;'> <b> " + string.Format("{0:#,0.00}", total) + " </b> </span> </td>  </tr>");
                sb.Append("</table>");
                sb.Append("</td></tr>");
                //  sb.Append("</table>");
                sb.Append("</tr>");
                //sb.Append("</td></tr>");
                // sb.Append("<table>");
                sb.Append("<tr>");
                sb.Append("<td width = '17%'> <span style = 'font-size:10px;font-family:calibri;'> Amount in Word</span></td>");
                sb.Append("<td width = '83%' colspan = '5'> <span style = 'font-size:10px;font-family:calibri;'> <b> (" + ds.Tables[2].Rows[0]["Currency"] + ") " + ds.Tables[0].Rows[0]["AMtInWord"] + @"</b></span></td>");
                sb.Append("</tr>");
                sb.Append("<tr>");
                sb.Append("<td width = '100%' colspan = '6'> <span style = 'font-size:10px;font-family:calibri;'> &nbsp; &nbsp;</span></td>");

                sb.Append("</tr>");

                sb.Append("<tr>");

                sb.Append("<td width = '17%'> <span style = 'font-size:10px;font-family:calibri;'> Prepared by</span></td>");

                sb.Append("<td width = '18%'> <span style = 'font-size:10px;font-family:calibri;'> <b> " + ds.Tables[2].Rows[0]["CreatedBy"] + @"</b></span></td>");

                sb.Append("</tr>");


                i = 0;
                sb.Append("<br/><tr>");
                while (ds.Tables[1].Rows.Count > i)
                {
                    if (!string.IsNullOrWhiteSpace(ds.Tables[1].Rows[i]["APPROVAL_Remark"].ToString()))
                    {
                        if (i > 3)
                        {
                            sb.Append("<tr>");
                        }


                        sb.Append("<td width = '15%'>  <span style = 'font-size:10px;font-family:calibri;'> Approved By</span></td> ");

                        sb.Append("<td width = '15%'>  <span style = 'font-size:10px;font-family:calibri;'> <b> " + ds.Tables[1].Rows[i]["APPROVER"] + @"</b></span></td>");

                        if (i > 3)
                        {
                            sb.Append("</tr>");
                        }
                    }

                    i++;
                }
                sb.Append("</tr>");

                sb.Append("<tr>");
                sb.Append("<td width = '17%'> <span style = 'font-size:10px;font-family:calibri;'> Received by Name</span></td>");

                sb.Append("<td width = '53%' colspan = '3'> <span style = 'font-size:10px;font-family:calibri;'> <b> _____________________________</b></span></td>");

                sb.Append("<td width = '15%'> <span style = 'font-size:10px;font-family:calibri;'> &nbsp;</span></td>");

                sb.Append("<td width = '15%'> <span style = 'font-size:10px;font-family:calibri;'> &nbsp;</span></td>");

                sb.Append("</tr></table></tr></td>");
                sb.Append("</table>");

                #endregion
                using (StringWriter sw = new StringWriter())
                {
                    using (HtmlTextWriter hw = new HtmlTextWriter(sw))
                    {

                        //Generate Invoice (Bill) Header.

                        //Export HTML String as PDF.
                        StringReader sr = new StringReader(sb.ToString());
                        Document pdfDoc = new Document(PageSize.A4, 10f, 10f, 10f, 0f);
                        HTMLWorker htmlparser = new HTMLWorker(pdfDoc);
                        PdfWriter writer = PdfWriter.GetInstance(pdfDoc, Response.OutputStream);
                        pdfDoc.Open();
                        htmlparser.Parse(sr);
                        pdfDoc.Close();
                        Response.ContentType = "application/pdf";
                        Response.AddHeader("content-disposition", "attachment;filename=" + ds.Tables[2].Rows[0]["RefNo"] + ".pdf");
                        Response.Cache.SetCacheability(HttpCacheability.NoCache);
                        Response.Write(pdfDoc);
                        Response.End();

                    }
                }
                return View();
            }
            catch (Exception ex)
            {

                
                return HandleErrorCondition(ex.Message);
            }


        }

        private ActionResult HandleErrorCondition(string message)
        {
            throw new NotImplementedException();
        }

        [Route("GetVendorRef")]
        public ActionResult GetVendorRef(string Country)
        {
            ContentResult ret = null;
            DataSet DS = _RDDPVOp.GetRefNo(Country);
            try
            {
                if (DS.Tables.Count > 0)
                {
                    ret = Content(JsonConvert.SerializeObject(DS), "application/json");
                }
            }
            catch (Exception)
            {
                ret = null;
            }
            return ret;

        }

    }
}