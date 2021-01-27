using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using RDDStaffPortal.DAL.Incentive;
using RDDStaffPortal.DAL.DataModels.Incentive;
using System.Data;
using Newtonsoft.Json;
using System.Data.SqlClient;
using RDDStaffPortal.DAL;
using System.Web.UI;
using System.IO;
using System.Web.UI.WebControls;
using System.Drawing;
using System.Reflection;
using iTextSharp.text;
using iTextSharp.text.html.simpleparser;
using iTextSharp.text.pdf;
using System.Text;
using static RDDStaffPortal.DAL.CommonFunction;
using System.Net.Mail;
using System.Net;

namespace RDDStaffPortal.Areas.Incentive.Controllers
{
    [Authorize]
    public class CompensationPlanController : Controller
    {
        CommonFunction Com = new CommonFunction();
        RDD_CompensationPlan_DbOperation CompPlanDbOp = new RDD_CompensationPlan_DbOperation();
        // GET: Incentive/CompensationPlan
        public ActionResult Index()
        {            
            return View();
        }
        [Route("GETLOGINUSERID")]
        public ActionResult GetLoginUserId()
        {
            RDD_CompensationPlan rDD_Compplan = new RDD_CompensationPlan();
            rDD_Compplan.EmployeeId = CompPlanDbOp.GetEmployeeIdByLoginName(User.Identity.Name);
            return Json(rDD_Compplan, JsonRequestBehavior.AllowGet);
        }
        [Route("GETLOGINUSERMAIL")]
        public ActionResult GetEmail(int Empid)
        {
            ContentResult retVal = null;
            DataSet ds;
            try
            {
                ds = CompPlanDbOp.GetLoginMail(Empid);
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
        [Route("GETMODE")]
        public ActionResult GetModedetail(int CompPlanid)
        {
            ContentResult retVal = null;
            DataSet ds;
            try
            {
                string Loginusername = User.Identity.Name;
                ds = CompPlanDbOp.GetModedetails(CompPlanid, Loginusername);
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
        
        [Route("ADDCOMPPLAN")]
        public ActionResult ADDCompensationPlan(int TEMPId = -1)
        {
            RDD_CompensationPlan RDD_CompPlan = new RDD_CompensationPlan();
            RDD_CompPlan.SaveFlag = false;
            RDD_CompPlan.CreatedOn = DateTime.Now;
            RDD_CompPlan.CreatedBy = User.Identity.Name;

            if (TEMPId != -1)
            {
                bool ClickFlag = true;
                RDD_CompPlan = CompPlanDbOp.GetData(User.Identity.Name, TEMPId, CompPlanDbOp.GetDropList(User.Identity.Name, "E", ClickFlag));
                RDD_CompPlan.EditFlag = true;
            }
            else
            {
                RDD_CompPlan.EditFlag = false;
                bool ClickFlag = false;
                RDD_CompPlan = CompPlanDbOp.GetDropList(User.Identity.Name, "N", ClickFlag);
            }
            return PartialView("~/Areas/Incentive/Views/CompensationPlan/ADDCompensationPlan.cshtml", RDD_CompPlan);
        }
        [Route("VIEWRDDCOMPPLAN")]
        public ActionResult VIEWRddCompplan(int CompPlanId)
        {
            RDD_CompensationPlan Compplan = new RDD_CompensationPlan();
            //PV = _RDDPVOp.GetDropList(User.Identity.Name);

            Compplan.IsDraft = true;
            bool ClickFlag = true;
            Compplan = CompPlanDbOp.GetData(User.Identity.Name, CompPlanId, CompPlanDbOp.GetDropList(User.Identity.Name, "E", ClickFlag));
            Compplan.EditFlag = true;
            Compplan.CreatedBy = User.Identity.Name;

            return PartialView("~/Areas/Incentive/Views/CompensationPlan/VIEWRddCompplan.cshtml", Compplan);
        }
        [Route("GETDESIGNATION")]
        public ActionResult GetDesignationDetails(int Empide)
        {
            DesignationLists DesigList = new DesignationLists();
            DesigList = CompPlanDbOp.DesignationDetails(Empide);
            return Json(DesigList, JsonRequestBehavior.AllowGet);
        }
        [Route("GETSALEDETAILS")]
        public ActionResult GetSalesdetail(int EmployeeId, string Period, int Years)
        {
            ContentResult retVal = null;
            DataSet ds;
            try
            {
                ds = CompPlanDbOp.GetSalesdetails(EmployeeId, Period, Years);
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
        [Route("GETKPIDETAILS")]
        public ActionResult GetKPIdetail(int DesigId, string Period, int Years)
        {
            ContentResult retVal = null;
            DataSet ds;
            try
            {
                ds = CompPlanDbOp.GetKPIdetails(DesigId, Period, Years);
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
        [Route("GETKPITARGET")]
        public ActionResult GetKPItarget(int DesigId, string Period, int Years)
        {
            ContentResult retVal = null;
            DataSet ds;
            try
            {
                ds = CompPlanDbOp.GetKPItargets(DesigId, Period, Years);
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

        public ActionResult SaveCompensationPlan(RDD_CompensationPlan RDD_CompPlan)
        {
            if (RDD_CompPlan.EditFlag == true)
            {
                RDD_CompPlan.LastUpdatedBy = User.Identity.Name;
                RDD_CompPlan.LastUpdatedOn = DateTime.Now;
            }
            else
            {
                RDD_CompPlan.CreatedBy = User.Identity.Name;
                //RDD_CompPlan.LastUpdatedBy = User.Identity.Name;
            }
            return Json(CompPlanDbOp.Save1(RDD_CompPlan), JsonRequestBehavior.AllowGet);
        }

        [Route("GETCOMPENSATION")]
        public ActionResult GetDataKPI(int pagesize, int pageno, string psearch)
        {
            List<RDD_CompensationPlan> rDD_CompPlan_s = new List<RDD_CompensationPlan>();
            rDD_CompPlan_s = CompPlanDbOp.GetALLDATA(User.Identity.Name, pagesize, pageno, psearch);
            return Json(new { data = rDD_CompPlan_s }, JsonRequestBehavior.AllowGet);
        }

        public ActionResult GeneratePdf(int CompPlanid, string Email2, string MailFlag, string Acceptstatus)
        {
            try
            {
                DataSet ds;
                DataSet ds2;
                SqlParameter[] prm =
                {
                     new SqlParameter("@Compplanid",CompPlanid)
                };
                ds = Com.ExecuteDataSet("RDD_CompensationPlanPDF", CommandType.StoredProcedure, prm);
                int EmployeeId= Convert.ToInt32(ds.Tables[0].Rows[0]["EmployeeId"]);
                var EmployeeName = ds.Tables[0].Rows[0]["EmployeeName"];
                var Period = ds.Tables[0].Rows[0]["Period"];
                var Year = ds.Tables[0].Rows[0]["Years"];
                var Managername = ds.Tables[0].Rows[0]["CreatedBy"];
                string Username = User.Identity.Name;
                ds2 = CompPlanDbOp.GetLoginMail(EmployeeId);
                string Email1=ds2.Tables[0].Rows[0]["Email"].ToString();
                if (MailFlag == "False")
                {
                    StringBuilder sb = new StringBuilder();
                    //sb.Append("<table width='100%' align='center' border='1'><tr><td width='100%' colspan='6'><table width='100%' align='center' border='0'><tr><td colspan = '6'> &nbsp; </td></tr><tr><td colspan = '6' align = 'center'><span style = 'font-size:medium'> <b> <u> PAYMENT VOUCHER </u> </b> </span> </td></tr><tr><td colspan = '4' width = '70%'> &nbsp; </td><td width = '15%'><span style = 'font-size:10px;font-family:calibri;'> PV No </span> </td><td width = '15%'><span style = 'font-size:10px;font-family:calibri;'> <b> DU000001 </b> </span> </td></tr><tr><td width = '17%'><span style = 'font-size:10px;font-family:calibri;'> Paid to Mr./ Ms.</span>   </td><td width = '52%' colspan = '3'><span style = 'font-size:10px;font-family:calibri;'> <b><u> FRONTLINE LOGISTICS JAFZA </u></b> </span> </td><td width = '15%'><span style = 'font-size:10px;font-family:calibri;'> Date </span> </td><td width = '15%'><span style = 'font-size:10px;font-family:calibri;'> <b> 14 - 01 - 2020 </b> </span> </td></tr><tr><td width = '17%'><span style = 'font-size:10px;font-family:calibri;'> Cash / Cheque No </span> </td><td width = '18%'><span style = 'font-size:10px;font-family:calibri;'> <b> 711075 </b> </span> </td><td width = '15%'><span style = 'font-size:10px;font-family:calibri;'> Bank Name </span> </td><td width = '20%'><span style = 'font-size:10px;font-family:calibri;'> <b> Citibank AED </b> </span> </td><td width = '15%'><span style = 'font-size:10px;font-family:calibri;'> Payment Date </span> </td> <td width = '15%'><span style = 'font-size:10px;font-family:calibri;'> <b> 14 - 01 - 2020 </b> </span> </td></tr><tr><td width = '100%' colspan = '6'> &nbsp;<br/></td></tr><tr><td width = '100%' colspan = '6'><table width = '100%' align = 'left' border = '1'><tr><td width = '8%'> &nbsp;<span style = 'font-size:10px;font-family:calibri;'>  <b> SrNo </b> </span>  </td> <td width = '72%' align = 'center'> &nbsp;<span style = 'font-size:10px;font-family:calibri;'>  <b> Particulars </b> </span> </td><td width = '20%' align = 'right'> &nbsp;<span style = 'font-size:10px;font-family:calibri;'>  <b> Amount(AED) </b> </span> </td></tr><tr><td width='8%' align='center' >  <span style='font-size:10px;font-family:calibri;'></span></td><td width='72%'>  <span style='font-size:10px;font-family:calibri;'>Payment against Nov Dec 19 Invoices</span></td><td width='20%' align='right' >  <span style='font-size:10px;font-family:calibri;'>15485.00</span></td></tr><tr> <td width='8%' >  <span style='font-size:10px;font-family:calibri;'>  &nbsp; </span> </td>   <td width='72%' align='right'  >  <span style='font-size:10px;font-family:calibri;'> <b> TOTAL ( AED ) </b> </span> </td><td width='20%' align='right' >  <span style='font-size:10px;font-family:calibri;'> <b> 15,485.00 </b> </span> </td>  </tr></table></td></tr></tr><tr><td width = '17%'> <span style = 'font-size:10px;font-family:calibri;'> Amount in Word</span></td><td width = '83%' colspan = '5'> <span style = 'font-size:10px;font-family:calibri;'> <b> (AED) Fifteen Thousand Four Hundred Eighty Five Only.</b></span></td></tr><tr><td width = '100%' colspan = '6'> <span style = 'font-size:10px;font-family:calibri;'> &nbsp; &nbsp;</span></td></tr><tr><td width = '17%'> <span style = 'font-size:10px;font-family:calibri;'> Prepared by</span></td><td width = '18%'> <span style = 'font-size:10px;font-family:calibri;'> <b> shirin</b></span></td></tr><br/><tr></tr><tr><td width = '17%'> <span style = 'font-size:10px;font-family:calibri;'> Received by Name</span></td><td width = '53%' colspan = '3'> <span style = 'font-size:10px;font-family:calibri;'> <b> _____________________________</b></span></td><td width = '15%'> <span style = 'font-size:10px;font-family:calibri;'> &nbsp;</span></td><td width = '15%'> <span style = 'font-size:10px;font-family:calibri;'> &nbsp;</span></td></tr></table></tr></td></table>");
                    #region html
                    sb.Append("<table width='100%' align='center' border='1'>");
                    sb.Append("<tr><td width='100%' colspan='6'>");
                    sb.Append("<table width='100%' align='center' border='0'>");
                    sb.Append("<tr width='100%' style='text-align: center;'> ");
                    sb.Append("<td colspan = '6' align='center'> <img src='https://www.reddotdistribution.com/images/reddot%20logo%20black.png' alt='' border='0' width='150' class='center'/> </td>");
                    sb.Append("</tr>");
                    sb.Append("<tr>");
                    sb.Append("<td colspan = '6' align = 'center'><span style = 'font-size:medium'> <b> <u> COMPENSATION PLAN </u> </b> </span> </td>");
                    sb.Append("</tr>");
                    //sb.Append("<tr>");
                    //sb.Append("<td colspan = '4' width = '70%'> &nbsp; </td>");
                    //sb.Append("<td width = '15%'><span style = 'font-size:10px;font-family:calibri;'> Date </span> </td>");
                    //sb.Append("<td width = '15%'><span style = 'font-size:10px;font-family:calibri;'> <b> " + DateTime.Now.ToString("dd-MM-yyyy") + @" </b> </span> </td>");
                    //sb.Append("</tr>");
                    sb.Append("<tr>");
                    sb.Append("<td width = '17%'><span style = 'font-size:10px;font-family:calibri;'>Employee Name </span>   </td>");
                    sb.Append("<td width = '26%'><span style = 'font-size:10px;font-family:calibri;'> <b> " + ds.Tables[0].Rows[0]["EmployeeName"] + @" </b> </span> </td>");
                    sb.Append("<td width = '12%'><span style = 'font-size:10px;font-family:calibri;'> Period </span> </td>");
                    sb.Append("<td width = '15%'><span style = 'font-size:10px;font-family:calibri;'> <b> " + ds.Tables[0].Rows[0]["Period"] + " " + "-" + " " + ds.Tables[0].Rows[0]["Years"] + @" </b> </span> </td>");
                    sb.Append("<td width = '15%'><span style = 'font-size:10px;font-family:calibri;'> Date </span> </td> ");
                    sb.Append("<td width = '15%'><span style = 'font-size:10px;font-family:calibri;'> <b> " + DateTime.Now.ToString("dd-MM-yyyy") + @" </b> </span> </td>");
                    sb.Append("</tr>");
                    sb.Append("<tr>");
                    sb.Append("<td width = '15%'><span style = 'font-size:10px;font-family:calibri;'> Designation </span> </td>");
                    sb.Append("<td width = '30%'><span style = 'font-size:10px;font-family:calibri;'> <b> " + ds.Tables[0].Rows[0]["DesigName"] + @" </b> </span> </td>");
                    sb.Append("<td width = '5%' align='right'><span style = 'font-size:10px;font-family:calibri;'> </span> </td>");
                    sb.Append("<td width = '6%'><span style = 'font-size:10px;font-family:calibri;'> </span> </td>");
                    sb.Append("<td width = '22%'><span style = 'font-size:10px;font-family:calibri;'> Total Compensation </span> </td> ");
                    sb.Append("<td width = '22%'><span style = 'font-size:10px;font-family:calibri;'> <b> " + ds.Tables[0].Rows[0]["Currency"] + " " + ds.Tables[0].Rows[0]["TotalCompensation"] + @" </b> </span> </td>");
                    sb.Append("</tr>");

                    sb.Append("<tr>");
                    sb.Append("<td width = '100%' colspan = '6'> &nbsp;<br/></td>");
                    sb.Append("</tr>");
                    sb.Append("<tr>");
                    sb.Append("<td width = '100%' colspan = '6'>");
                    sb.Append("<table width = '100%' align = 'left' border = '1'>");
                    sb.Append("<tr>");
                    sb.Append("<td width = '20%' align = 'center'> &nbsp;<span style = 'font-size:10px;font-family:calibri;'>  <b> BU </b> </span>  </td> ");
                    sb.Append("<td width = '20%' align = 'center'> &nbsp;<span style = 'font-size:10px;font-family:calibri;'>  <b> Revenue Target </b> </span> </td>");
                    sb.Append("<td width = '20%' align = 'center'> &nbsp;<span style = 'font-size:10px;font-family:calibri;'>  <b> GP Target </b> </span> </td>");
                    sb.Append("<td width = '20%' align = 'center'> &nbsp;<span style = 'font-size:10px;font-family:calibri;'>  <b> Revenue Split Percent </b> </span> </td>");
                    sb.Append("<td width = '20%' align = 'center'> &nbsp;<span style = 'font-size:10px;font-family:calibri;'>  <b> GP Split Percent </b> </span> </td>");
                    sb.Append("</tr>");
                    int i = 0;
                    int j = 0;
                    decimal total = 0;
                    decimal total1 = 0;
                    decimal total2 = 0;
                    decimal total3 = 0;
                    while (ds.Tables[1].Rows.Count > i)
                    {
                        sb.Append("<tr>");
                        sb.Append("<td width='20%' align='center' padding: 4px;'>  <span style='font-size:10px;font-family:calibri;'>");
                        sb.Append(ds.Tables[1].Rows[i]["BU"]);
                        sb.Append("</span></td>");
                        sb.Append("<td width='20%' align='right'>  <span style='font-size:10px;font-family:calibri;'>");
                        sb.Append(string.Format("{0:#,0.00}", Convert.ToDecimal(ds.Tables[1].Rows[i]["RevenueTarget"])));
                        total = total + Convert.ToDecimal(ds.Tables[1].Rows[i]["RevenueTarget"]);
                        sb.Append("</span></td>");
                        sb.Append("<td width='20%' align='right' >  <span style='font-size:10px;font-family:calibri;'>");
                        sb.Append(string.Format("{0:#,0.00}", Convert.ToDecimal(ds.Tables[1].Rows[i]["GPTarget"])));
                        total1 = total1 + Convert.ToDecimal(ds.Tables[1].Rows[i]["GPTarget"]);
                        sb.Append("</span></td>");
                        sb.Append("<td width='20%' align='right' >  <span style='font-size:10px;font-family:calibri;'>");
                        sb.Append(ds.Tables[1].Rows[i]["Rev_Split_Percentage"]);
                        sb.Append("</span></td>");
                        sb.Append("<td width='20%' align='right' >  <span style='font-size:10px;font-family:calibri;'>");
                        sb.Append(ds.Tables[1].Rows[i]["GP_Split_Percentage"]);
                        sb.Append("</span></td>");
                        sb.Append("</tr>");
                        i++;
                    }

                    sb.Append("<tr>");
                    sb.Append("<td width='20%' align='right' >  <span style='font-size:10px;font-family:calibri; align='right'  > <b> TOTAL ( " + ds.Tables[0].Rows[0]["Currency"] + " ) </b> </span> </td>");
                    sb.Append("<td width='20%' align='right' >  <span style='font-size:10px;font-family:calibri;'> <b> " + string.Format("{0:#,0.00}", total) + " </b> </span> </td>");
                    sb.Append("<td width='20%' align='right' >  <span style='font-size:10px;font-family:calibri;'> <b> " + string.Format("{0:#,0.00}", total1) + " </b> </span> </td>");
                    sb.Append("<td width='20%' align='right' >  <span style='font-size:10px;font-family:calibri;'> <b> " + ds.Tables[0].Rows[0]["TotalSplitRevPercent"] + " </b> </span> </td>");
                    sb.Append("<td width='20%' align='right' >  <span style='font-size:10px;font-family:calibri;'> <b> " + ds.Tables[0].Rows[0]["TotalSplitGPPercent"] + " </b> </span> </td>");
                    sb.Append("</tr>");
                    sb.Append("</table>");

                    sb.Append("<tr>");
                    sb.Append("<td width = '100%' colspan = '6'> &nbsp;<br/></td>");
                    sb.Append("</tr>");
                    sb.Append("<tr>");
                    sb.Append("<td width = '100%' colspan = '6'>");
                    sb.Append("<table width = '100%' align = 'left' border = '1'>");
                    sb.Append("<tr>");
                    sb.Append("<td width = '50%' align = 'center'> &nbsp;<span style = 'font-size:10px;font-family:calibri;'>  <b> KPI Parameter </b> </span>  </td> ");
                    sb.Append("<td width = '25%' align = 'center'> &nbsp;<span style = 'font-size:10px;font-family:calibri;'>  <b> KPI Target </b> </span> </td>");
                    sb.Append("<td width = '25%' align = 'center'> &nbsp;<span style = 'font-size:10px;font-family:calibri;'>  <b> KPI Split Percent </b> </span> </td>");
                    sb.Append("</tr>");

                    while (ds.Tables[2].Rows.Count > j)
                    {
                        sb.Append("<tr>");
                        sb.Append("<td width='50%' align='center' padding: 4px;'>  <span style='font-size:10px;font-family:calibri;'>");
                        sb.Append(ds.Tables[2].Rows[j]["KPI_Parameter"]);
                        sb.Append("</span></td>");
                        sb.Append("<td width='25%' align='right'>  <span style='font-size:10px;font-family:calibri;'>");
                        sb.Append(ds.Tables[2].Rows[j]["KPI_Target"]);
                        total3 = total3 + Convert.ToDecimal(ds.Tables[2].Rows[j]["KPI_Target"].ToString());
                        sb.Append("</span></td>");
                        sb.Append("<td width='25%' align='right' >  <span style='font-size:10px;font-family:calibri;'>");
                        sb.Append(ds.Tables[2].Rows[j]["KPI_Split_Percentage"]);
                        total2 = total2 + Convert.ToDecimal(ds.Tables[2].Rows[j]["KPI_Split_Percentage"].ToString());
                        sb.Append("</span></td>");
                        sb.Append("</tr>");
                        j++;
                    }

                    sb.Append("<tr> <td width='50%' align='right' >  <span style='font-size:10px;font-family:calibri;align='right'><b> TOTAL ( " + ds.Tables[0].Rows[0]["Currency"] + " ) </b> </span> </td>");
                    sb.Append("<td width='25%' align='right' >  <span style='font-size:10px;font-family:calibri;'> <b> " + string.Format("{0:#,0.00}", total3) + " </b> </span> </td>");
                    sb.Append("<td width='25%' align='right' >  <span style='font-size:10px;font-family:calibri;'> <b> " + string.Format("{0:#,0.00}", total2) + " </b> </span> </td></tr>");

                    sb.Append("</table>");

                    sb.Append("</td></tr>");

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
                                string Tomail = Email1;
                                string cc = Email2;
                                var Html = " Dear " + EmployeeName + ", <br/><br/>  Your compensation plan for <b> " + Period + "-" + Year + " </b> is set by your line manager. Kindly click on below link to take action. <br/> ";
                                Html = Html + "http://localhost:28986/Incentive/CompensationPlan?CompPlanid=" + CompPlanid + "" + "<br/><br/>";
                                Html = Html + "Best Regards, <br/> Red Dot Distribution.";
                                string Filename = EmployeeName + "-" + Period + "-" + Year + " " + "Comp Plan.pdf";
                                Attachment at = new Attachment(new MemoryStream(bytes), Filename);
                                string Subject = "Your compensation plan is set by your manager for" + " " + Period + "-" + Year;


                                Tomail = "mainak@reddotdistribution.com";
                                cc = "pratim.d@reddotdistribution.com";

                                SendMail.SendMailWithAttachment(Tomail, cc, Subject, Html, true, at);
                            }
                            //Response.End();
                        }
                    }
                }

                else
                {
                    string Tomail = Email2;
                    string cc = Email1;
                    var Html = "";
                    string Subject = "";
                    if (Acceptstatus == "Accepted")
                    {
                        Html = "Dear" + Managername + ",<br/><br/>";
                        Html = "" + EmployeeName + " accepted compensation plan for <b> " + Period + "-" + Year + " </b> <br/><br/> ";
                        Html = Html + "Best Regards, <br/> Red Dot Distribution.";
                        Subject = EmployeeName + " accepted compensation plan for" + " " + Period + "-" + Year;
                    }
                    else
                    {
                        Html = "" + EmployeeName + " requested you to change the compensation plan for <b> " + Period + "-" + Year + " </b>. Kindly click on the below link to take action. <br/> ";
                        Html = Html + "http://localhost:28986/Incentive/CompensationPlan?CompPlanid=" + CompPlanid + "" + "<br/><br/>";
                        Html = Html + "Best Regards, <br/> Red Dot Distribution.";
                        Subject = EmployeeName + " requested to change the compensation plan for" + " " + Period + "-" + Year;
                    }

                    Tomail = "pratim.d@reddotdistribution.com";
                    cc = "mainak@reddotdistribution.com";
                   
                    SendMail.Send(Tomail, cc, Subject, Html, true);
                }

                return RedirectToAction("Index", "CompensationPlan");
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

        public ActionResult GenerateSamplePdf()
        {
            DataSet ds;
            SqlParameter[] prm =
            {
                     new SqlParameter("@Compplanid",1)
                };
            ds = Com.ExecuteDataSet("RDD_CompensationPlanPDF", CommandType.StoredProcedure, prm);

            var EmployeeName = ds.Tables[0].Rows[0]["EmployeeName"];
            var Period = ds.Tables[0].Rows[0]["Period"];
            var Year = ds.Tables[0].Rows[0]["Years"];
            var Managername = ds.Tables[0].Rows[0]["CreatedBy"];
            
                StringBuilder sb = new StringBuilder();
                //sb.Append("<table width='100%' align='center' border='1'><tr><td width='100%' colspan='6'><table width='100%' align='center' border='0'><tr><td colspan = '6'> &nbsp; </td></tr><tr><td colspan = '6' align = 'center'><span style = 'font-size:medium'> <b> <u> PAYMENT VOUCHER </u> </b> </span> </td></tr><tr><td colspan = '4' width = '70%'> &nbsp; </td><td width = '15%'><span style = 'font-size:10px;font-family:calibri;'> PV No </span> </td><td width = '15%'><span style = 'font-size:10px;font-family:calibri;'> <b> DU000001 </b> </span> </td></tr><tr><td width = '17%'><span style = 'font-size:10px;font-family:calibri;'> Paid to Mr./ Ms.</span>   </td><td width = '52%' colspan = '3'><span style = 'font-size:10px;font-family:calibri;'> <b><u> FRONTLINE LOGISTICS JAFZA </u></b> </span> </td><td width = '15%'><span style = 'font-size:10px;font-family:calibri;'> Date </span> </td><td width = '15%'><span style = 'font-size:10px;font-family:calibri;'> <b> 14 - 01 - 2020 </b> </span> </td></tr><tr><td width = '17%'><span style = 'font-size:10px;font-family:calibri;'> Cash / Cheque No </span> </td><td width = '18%'><span style = 'font-size:10px;font-family:calibri;'> <b> 711075 </b> </span> </td><td width = '15%'><span style = 'font-size:10px;font-family:calibri;'> Bank Name </span> </td><td width = '20%'><span style = 'font-size:10px;font-family:calibri;'> <b> Citibank AED </b> </span> </td><td width = '15%'><span style = 'font-size:10px;font-family:calibri;'> Payment Date </span> </td> <td width = '15%'><span style = 'font-size:10px;font-family:calibri;'> <b> 14 - 01 - 2020 </b> </span> </td></tr><tr><td width = '100%' colspan = '6'> &nbsp;<br/></td></tr><tr><td width = '100%' colspan = '6'><table width = '100%' align = 'left' border = '1'><tr><td width = '8%'> &nbsp;<span style = 'font-size:10px;font-family:calibri;'>  <b> SrNo </b> </span>  </td> <td width = '72%' align = 'center'> &nbsp;<span style = 'font-size:10px;font-family:calibri;'>  <b> Particulars </b> </span> </td><td width = '20%' align = 'right'> &nbsp;<span style = 'font-size:10px;font-family:calibri;'>  <b> Amount(AED) </b> </span> </td></tr><tr><td width='8%' align='center' >  <span style='font-size:10px;font-family:calibri;'></span></td><td width='72%'>  <span style='font-size:10px;font-family:calibri;'>Payment against Nov Dec 19 Invoices</span></td><td width='20%' align='right' >  <span style='font-size:10px;font-family:calibri;'>15485.00</span></td></tr><tr> <td width='8%' >  <span style='font-size:10px;font-family:calibri;'>  &nbsp; </span> </td>   <td width='72%' align='right'  >  <span style='font-size:10px;font-family:calibri;'> <b> TOTAL ( AED ) </b> </span> </td><td width='20%' align='right' >  <span style='font-size:10px;font-family:calibri;'> <b> 15,485.00 </b> </span> </td>  </tr></table></td></tr></tr><tr><td width = '17%'> <span style = 'font-size:10px;font-family:calibri;'> Amount in Word</span></td><td width = '83%' colspan = '5'> <span style = 'font-size:10px;font-family:calibri;'> <b> (AED) Fifteen Thousand Four Hundred Eighty Five Only.</b></span></td></tr><tr><td width = '100%' colspan = '6'> <span style = 'font-size:10px;font-family:calibri;'> &nbsp; &nbsp;</span></td></tr><tr><td width = '17%'> <span style = 'font-size:10px;font-family:calibri;'> Prepared by</span></td><td width = '18%'> <span style = 'font-size:10px;font-family:calibri;'> <b> shirin</b></span></td></tr><br/><tr></tr><tr><td width = '17%'> <span style = 'font-size:10px;font-family:calibri;'> Received by Name</span></td><td width = '53%' colspan = '3'> <span style = 'font-size:10px;font-family:calibri;'> <b> _____________________________</b></span></td><td width = '15%'> <span style = 'font-size:10px;font-family:calibri;'> &nbsp;</span></td><td width = '15%'> <span style = 'font-size:10px;font-family:calibri;'> &nbsp;</span></td></tr></table></tr></td></table>");
                #region html
                sb.Append("<table width='100%' align='center' border='1'>");
                sb.Append("<tr><td width='100%' colspan='6'>");
                sb.Append("<table width='70%' align='center' border='0'>");
                sb.Append("<tr width='100%' style='text-align: center;'> ");
                sb.Append("<td colspan = '6'> <img src='https://www.reddotdistribution.com/images/reddot%20logo%20black.png' alt='' border='0' width='150'/> </td>");
                sb.Append("</tr>");
                sb.Append("<tr>");
                sb.Append("<td colspan = '6' align = 'center'><span style = 'font-size:medium'> <b> <u> COMPENSATION PLAN </u> </b> </span> </td>");
                sb.Append("</tr>");
                sb.Append("<tr>");
                sb.Append("<td width='100%' colspan='6'> &nbsp;<br /></td>");
                sb.Append("</tr>");

                sb.Append("<tr style='font-size:10px;font-family:calibri;'>");
                sb.Append("<th width='20%' align='left'>Employee Name</th>");
                sb.Append("<th width='20%' align='left'>Period</th>");
                sb.Append("<th width='20%' align='left'>Date</th>");
                sb.Append("<th width='20%' align='left'>Designation</th>");
                sb.Append("<th width='20%' align='left'>Total Compensation</th>");
                sb.Append("</tr>");

                sb.Append("<tr>");
                sb.Append("<td width='20%' align='left'>" + ds.Tables[0].Rows[0]["EmployeeName"] + @"</td>");
                sb.Append("<td width='20%' align='left'>" + ds.Tables[0].Rows[0]["Period"] + " " + "-" + " " + ds.Tables[0].Rows[0]["Years"] + @"</td>");
                sb.Append("<td width='20%' align='left'>" + DateTime.Now.ToString("dd-MM-yyyy") + @"</td>");
                sb.Append("<td width='20%' align='left'>" + ds.Tables[0].Rows[0]["DesigName"] + @"</td>");
                sb.Append("<td width='20%' align='left'>" + ds.Tables[0].Rows[0]["Currency"] + " " + ds.Tables[0].Rows[0]["TotalCompensation"] + @"</td>");
                sb.Append("</tr>");

                sb.Append("<tr>");
                sb.Append("<td width = '100%' colspan = '6'> &nbsp;<br/></td>");
                sb.Append("</tr>");

                sb.Append("<tr>");
                sb.Append("<td width = '100%' colspan = '6'>");
                sb.Append("<table style='font - size: 16px;' width='100%' align='center' border='1'>");
                sb.Append("<tr>");
                sb.Append("<td width = '20%' align = 'center' style='background: grey; color: white; padding: 4px;'> &nbsp;<span style = 'font-size:10px;font-family:calibri;'>  <b> BU </b> </span>  </td> ");
                sb.Append("<td width = '20%' align = 'center' style='background: grey; color: white; padding: 4px;'> &nbsp;<span style = 'font-size:10px;font-family:calibri;'>  <b> Revenue Target </b> </span> </td>");
                sb.Append("<td width = '20%' align = 'center' style='background: grey; color: white; padding: 4px;'> &nbsp;<span style = 'font-size:10px;font-family:calibri;'>  <b> GP Target </b> </span> </td>");
                sb.Append("<td width = '20%' align = 'center' style='background: grey; color: white; padding: 4px;'> &nbsp;<span style = 'font-size:10px;font-family:calibri;'>  <b> Revenue Split Percent </b> </span> </td>");
                sb.Append("<td width = '20%' align = 'center' style='background: grey; color: white; padding: 4px;'> &nbsp;<span style = 'font-size:10px;font-family:calibri;'>  <b> GP Split Percent </b> </span> </td>");
                sb.Append("</tr>");
                int i = 0;
                int j = 0;
                decimal total = 0;
                decimal total1 = 0;
                decimal total2 = 0;
                decimal total3 = 0;
                while (ds.Tables[1].Rows.Count > i)
                {
                    sb.Append("<tr>");
                    sb.Append("<td width='20%' align='center' style='background: grey; color: white; padding: 4px;'>  <span style='font-size:10px;font-family:calibri;'>");
                    sb.Append(ds.Tables[1].Rows[i]["BU"]);
                    sb.Append("</span></td>");
                    sb.Append("<td width='20%' align='right'>  <span style='font-size:10px;font-family:calibri;'>");
                    sb.Append(ds.Tables[1].Rows[i]["RevenueTarget"]);
                    total = total + Convert.ToDecimal(string.Format("{0:#,0.00}", ds.Tables[1].Rows[i]["RevenueTarget"].ToString()));
                    sb.Append("</span></td>");
                    sb.Append("<td width='20%' align='right' >  <span style='font-size:10px;font-family:calibri;'>");
                    sb.Append(ds.Tables[1].Rows[i]["GPTarget"]);
                    total1 = total1 + Convert.ToDecimal(string.Format("{0:#,0.00}", ds.Tables[1].Rows[i]["GPTarget"].ToString()));
                    sb.Append("</span></td>");
                    sb.Append("<td width='20%' align='right' >  <span style='font-size:10px;font-family:calibri;'>");
                    sb.Append(ds.Tables[1].Rows[i]["Rev_Split_Percentage"]);
                    sb.Append("</span></td>");
                    sb.Append("<td width='20%' align='right' >  <span style='font-size:10px;font-family:calibri;'>");
                    sb.Append(ds.Tables[1].Rows[i]["GP_Split_Percentage"]);
                    sb.Append("</span></td>");
                    sb.Append("</tr>");
                    i++;
                }
                sb.Append("<tr>");
                sb.Append("<td width='20%' align='right' style='background: grey; color: white; padding: 4px;'>  <span style='font-size:10px;font-family:calibri; align='right'  > <b> TOTAL ( " + ds.Tables[0].Rows[0]["Currency"] + " ) </b> </span> </td>");
                sb.Append("<td width='20%' align='right' >  <span style='font-size:10px;font-family:calibri;'> <b> " + string.Format("{0:#,0.00}", total) + " </b> </span> </td>");
                sb.Append("<td width='20%' align='right' >  <span style='font-size:10px;font-family:calibri;'> <b> " + string.Format("{0:#,0.00}", total1) + " </b> </span> </td>");
                sb.Append("<td width='20%' align='right' >  <span style='font-size:10px;font-family:calibri;'> <b> " + ds.Tables[0].Rows[0]["TotalSplitRevPercent"] + " </b> </span> </td>");
                sb.Append("<td width='20%' align='right' >  <span style='font-size:10px;font-family:calibri;'> <b> " + ds.Tables[0].Rows[0]["TotalSplitGPPercent"] + " </b> </span> </td>");
                sb.Append("</tr>");
                sb.Append("</table>");

                sb.Append("<tr>");
                sb.Append("<td width = '100%' colspan = '6'> &nbsp;<br/></td>");
                sb.Append("</tr>");

                sb.Append("<tr>");
                sb.Append("<td width = '100%' colspan = '6'>");
                sb.Append("<table width = '100%' align = 'center' border = '1'>");
                sb.Append("<tr>");
                sb.Append("<td width = '50%' align = 'center' style='background: grey; color: white; padding: 4px;'> &nbsp;<span style = 'font-size:10px;font-family:calibri;'>  <b> KPI Parameter </b> </span>  </td> ");
                sb.Append("<td width = '25%' align = 'center' style='background: grey; color: white; padding: 4px;'> &nbsp;<span style = 'font-size:10px;font-family:calibri;'>  <b> KPI Target </b> </span> </td>");
                sb.Append("<td width = '25%' align = 'center' style='background: grey; color: white; padding: 4px;'> &nbsp;<span style = 'font-size:10px;font-family:calibri;'>  <b> KPI Split Percent </b> </span> </td>");
                sb.Append("</tr>");

                while (ds.Tables[2].Rows.Count > j)
                {
                    sb.Append("<tr>");
                    sb.Append("<td width='50%' align='center' style='background: grey; color: white; padding: 4px;'>  <span style='font-size:10px;font-family:calibri;'>");
                    sb.Append(ds.Tables[2].Rows[j]["KPI_Parameter"]);
                    sb.Append("</span></td>");
                    sb.Append("<td width='25%' align='right'>  <span style='font-size:10px;font-family:calibri;'>");
                    sb.Append(ds.Tables[2].Rows[j]["KPI_Target"]);
                    total3 = total3 + Convert.ToDecimal(ds.Tables[2].Rows[j]["KPI_Target"].ToString());
                    sb.Append("</span></td>");
                    sb.Append("<td width='25%' align='right' >  <span style='font-size:10px;font-family:calibri;'>");
                    sb.Append(ds.Tables[2].Rows[j]["KPI_Split_Percentage"]);
                    total2 = total2 + Convert.ToDecimal(ds.Tables[2].Rows[j]["KPI_Split_Percentage"].ToString());
                    sb.Append("</span></td>");
                    sb.Append("</tr>");
                    j++;
                }

                sb.Append("<tr> <td width='50%' align='right' style='background: grey; color: white; padding: 4px;'>  <span style='font-size:10px;font-family:calibri;align='right'><b> TOTAL ( " + ds.Tables[0].Rows[0]["Currency"] + " ) </b> </span> </td>");
                sb.Append("<td width='25%' align='right' >  <span style='font-size:10px;font-family:calibri;'> <b> " + string.Format("{0:#,0.00}", total3) + " </b> </span> </td>");
                sb.Append("<td width='25%' align='right' >  <span style='font-size:10px;font-family:calibri;'> <b> " + string.Format("{0:#,0.00}", total2) + " </b> </span> </td></tr>");

                sb.Append("</table>");

                sb.Append("</td></tr>");

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
                    Response.AddHeader("content-disposition", "attachment;filename=" + EmployeeName + "-" + Period + "-" + Year + " " + "Comp Plan.pdf");
                    Response.Cache.SetCacheability(HttpCacheability.NoCache);
                    Response.Write(pdfDoc);
                    Response.End();
                }
            }
            return RedirectToAction("Index", "CompensationPlan");
        }        
    }
}