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
using static RDDStaffPortal.DAL.CommonFunction;
using RDDStaffPortal.DAL.DataModels.Voucher;
using RDDStaffPortal.DAL.Voucher;
using System.Security.AccessControl;

namespace RDDStaffPortal.Areas.Admin.Controllers
{
    public class ApprovalStatusReportController : Controller
    {
        // GET: Admin/ApprovalStatusReport
        RDD_Approval_TemplatesDBOperation RDD_Approval_TemplatesDBOperation = new RDD_Approval_TemplatesDBOperation();
        RDD_PV_VoucherDbOperation RDD_PV_VoucherDb =new RDD_PV_VoucherDbOperation();
        CommonFunction Com = new CommonFunction();
        public ActionResult Index()
        {
            //Db.constr = System.Configuration.ConfigurationManager.ConnectionStrings["tejSAP"].ConnectionString;

            //DataSet DS = Db.myGetDS("EXEC RDD_DOC_Get_Originator_List ");
            //List<RDD_APPROVAL_DOC> ddlOriginatorList = new List<RDD_APPROVAL_DOC>();
            //if (DS.Tables.Count > 0)
            //{
            //    for (int i = 0; i < DS.Tables[0].Rows.Count; i++)
            //    {
            //        RDD_APPROVAL_DOC OriginatorList = new RDD_APPROVAL_DOC();
            //        OriginatorList.ORIGINATORCode = DS.Tables[0].Rows[i]["Originator"].ToString();
            //        OriginatorList.ORIGINATOR = DS.Tables[0].Rows[i]["OriginatorName"].ToString();
            //        ddlOriginatorList.Add(OriginatorList);

            //    }
            //}
            //ViewBag.ddlOriginatorList = new SelectList(ddlOriginatorList, "ORIGINATORCode", "ORIGINATOR");

            return View();
        }

        [Route("Get_ApprovalDoc_List")]
        public ActionResult Get_ApprovalDoc_List(string DBName, string UserName, int pagesize, int pageno, string psearch,string Objtype)
        {
            List<RDD_APPROVAL_DOC> _RDD_APPROVAL_DOC = new List<RDD_APPROVAL_DOC>();
            _RDD_APPROVAL_DOC = RDD_Approval_TemplatesDBOperation.Get_ApprovalDoc_List(DBName, UserName, pagesize, pageno, psearch, Objtype);
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
                    
                    
                       SendMailToSignatories(DocKey,ObjectType);
                    #region mailsending
                    if (ObjectType == "18")
                    {
                        SqlParameter[] sqlPar_ = { new SqlParameter("@PVID", DocKey),
                             new SqlParameter("@LoggedInUser", Approver),
                              new SqlParameter("@p_Event", "ApprovalDecision"),

                        };
                        Com.ExecuteNonQuery("PV_SendApprovalRequestEmail", sqlPar_);
                    }
                    
                    #endregion
                    

                    retVal = Content(JsonConvert.SerializeObject(DS), "application/json");
                }
                return retVal;

            }
            catch (Exception ex)
            {
                throw ex;
            }
        }



        public bool SendMailToSignatories(string PVID,string ObjectType)
        {
            bool result = false;
            try
            {
                string signatoryEmail = string.Empty, CFOEmail = string.Empty;

                DataSet DS = Com.ExecuteDataSet("EXEC PV_GetSignatories " + PVID+ ","+ObjectType);
                if (DS.Tables.Count > 0)
                {
                    if (DS.Tables[0].Rows[0]["Signatories"] != null && !DBNull.Value.Equals(DS.Tables[0].Rows[0]["Signatories"]))
                    {
                        signatoryEmail = DS.Tables[0].Rows[0]["Signatories"].ToString();
                    }
                    if (DS.Tables[0].Rows[0]["CFOEmail"] != null && !DBNull.Value.Equals(DS.Tables[0].Rows[0]["CFOEmail"]))
                    {
                        CFOEmail = DS.Tables[0].Rows[0]["CFOEmail"].ToString();
                    }
                    // send mail
                    if (!string.IsNullOrEmpty(signatoryEmail) && !string.IsNullOrEmpty(CFOEmail))
                    {
                        string html = string.Empty;
                        if (DS.Tables[1].Rows[0][0] != null && !DBNull.Value.Equals(DS.Tables[1].Rows[0][0]))
                        {
                            html = DS.Tables[1].Rows[0][0].ToString();
                        }

                        #region : old code to send PV to signatory

                        //                    string html = " Dear " + signatoryEmail + ", <br/><br/>";
                        //                    html = html + " Payment voucher <b>" + txtRefNo.Text + "</b> has been approved by <b> " + "alfarid" + " </b> for <b>" + txtVendorEmployee.Text + " </b>.<br/>";
                        //                    if ((ddlPaymentMethod.SelectedItem.Text == "Cheque" || ddlPaymentMethod.SelectedItem.Text == "TT") && !string.IsNullOrEmpty(ancChequeImage.HRef))
                        //                    {
                        //                        html = html + "Please find attached the " + ddlPaymentMethod.SelectedItem.Text + " image.  <br/> ";
                        //                    }
                        //                    html = html + " Please see below details, <br/><br/>  ";
                        //                    html = html + @"  <table border='1' width='100%' align='center'> <thead> <tr> <th width='8%' align='center' style='background-color:#ce352c;color:White;' >  Country </th> 
                        //																								 <th width='8%' align='center' style='background-color:#ce352c;color:White;' >  Currency  </th> 
                        //																								 <th width='9%' align='center' style='background-color:#ce352c;color:White;'>Approved Amt</th>
                        //																								 <th width='8%' align='center' style='background-color:#ce352c;color:White;'>PayReq Date</th> 
                        //                                                                                                 <th width='8%' align='center' style='background-color:#ce352c;color:White;'>Pymnt Method</th>
                        //                                                                                                 <th width='15%' align='center' style='background-color:#ce352c;color:White;'>Bank</th>
                        //																								 <th width='18%' align='center' style='background-color:#ce352c;color:White;'>Benificiary</th>
                        //																								 <th width='18%' align='center' style='background-color:#ce352c;color:White;'>Being Pay Of</th>
                        //																								  </tr> </thead> <tbody>";

                        //                    html = html + " <tr><td align='center'> " + ddlCountry.SelectedItem.Text + "</td>";
                        //                    html = html + " <td align='center'> " + ddlCurrency.SelectedItem.Text + "</td>";
                        //                    html = html + " <td align='center'> " + txtApprovedAmt.Text + "</td>";
                        //                    html = html + " <td align='center'> " + txtPaymentReqDate.Text + "</td>";
                        //                    html = html + " <td align='center'> " + ddlPaymentMethod.SelectedItem.Text + "</td>";
                        //                    html = html + " <td align='center'> " + txtBankName.Text + "</td>";
                        //                    html = html + " <td align='center'> " + txtBenificiary.Text + "</td>";
                        //                    html = html + " <td align='center'> " + txtBeingPayOf.Text + "</td></tr>";
                        //                    html = html + " </tbody> </table> <br/><br/> ";
                        //                    html = html + " <b>CFO approval Remarks  </b> &nbsp; : " + txtCFOapprovalRemarks.Text;
                        //                    html = html + " <br/><br/> Best Regards, <br/> Red Dot Distribution";

                        #endregion

                        
                        // pls uncomment below to send mail to signatories before deploying it in LIVE

                        #region "Code to send multiple Attachements"

                        string attachmentPath = string.Empty;
                        string subject = string.Empty;

                        //DataSet ds = Com.ExecuteDataSet(" select FilePath from PVLines Where PVId=" + PVId + " And isnull(FilePath,'') <> '' ");
                        if (DS.Tables[2].Rows.Count > 0)
                        {
                            subject = "PV " + DS.Tables[2].Rows[0]["RefNo"].ToString() + " for '" + DS.Tables[2].Rows[0]["VendorEmployee"].ToString() + "' is approved by CFO";
                            int i = 0;
                            while (DS.Tables[3].Rows.Count > i)
                            {
                                if (DS.Tables[3].Rows[i]["FilePath"] != null && !DBNull.Value.Equals(DS.Tables[3].Rows[i]["FilePath"]))
                                {
                                    if (string.IsNullOrEmpty(attachmentPath))
                                    {
                                        attachmentPath = System.IO.Path.Combine(Server.MapPath(DS.Tables[3].Rows[i]["FilePath"].ToString()));
                                    }
                                    else
                                    {
                                        attachmentPath = attachmentPath + "?" + Server.MapPath(DS.Tables[3].Rows[i]["FilePath"].ToString());
                                    }
                                }
                                i++;
                            }
                        }
                        //SqlDataReader rdr = Db.myGetReader(" select FilePath from PVLines Where PVId=" + rDD_PV.PVId + " And isnull(FilePath,'') <> '' ");
                        //if (rdr.HasRows)
                        //{
                        //    while (rdr.Read())
                        //    {
                        //        if (rdr["FilePath"] != null && !DBNull.Value.Equals(rdr["FilePath"]))
                        //        {
                        //            if (string.IsNullOrEmpty(attachmentPath))
                        //            {
                        //                attachmentPath = Server.MapPath(rdr["FilePath"].ToString());
                        //            }
                        //            else
                        //            {
                        //                attachmentPath = attachmentPath + "?" + Server.MapPath(rdr["FilePath"].ToString());
                        //            }
                        //        }
                        //    }
                        //}

                        if (!string.IsNullOrEmpty(Server.MapPath(DS.Tables[2].Rows[0]["FilePath"].ToString())))
                        {
                            if (!string.IsNullOrEmpty(attachmentPath))
                            {
                                attachmentPath = attachmentPath + "?" + Server.MapPath(DS.Tables[2].Rows[0]["FilePath"].ToString());
                            }
                            else
                            {
                                attachmentPath = Server.MapPath(DS.Tables[2].Rows[0]["FilePath"].ToString());
                            }
                        }

                        #endregion

                        //signatoryEmail = "chetan@reddotdistribution.com; pramod@reddotdistribution.com";
                        //CFOEmail = "pramod@reddotdistribution.com";
                        try
                        {
                            //string sendmailresponse = Mail.SendSingleAttachPV("reddotstaff@reddotdistribution.com", signatoryEmail, CFOEmail , subject, html, true, Server.MapPath(ancChequeImage.HRef));
                            string sendmailresponse = SendMail.SendMailWithAttachment(signatoryEmail, CFOEmail, subject, html, true, attachmentPath);
                            //string sendmailresponse = Mail.SendSingleAttachPV("reddotstaff@reddotdistribution.com", "pramod@reddotdistribution.com" , "pramod@reddotdistribution.com", subject, html, true, Server.MapPath(ancChequeImage.HRef));
                            if (sendmailresponse == "Mail Sent Successfully")
                            {
                                //result = true;
                                List<Outcls1> outcls1s = new List<Outcls1>();
                                //Update Mail Sending Flag
                              outcls1s=  RDD_Approval_TemplatesDBOperation.MailSending(PVID);
                                result=outcls1s[0].Outtf;
                            }
                            html = html.Replace('\'', '\"');
                            // Db.constr = myGlobal.getAppSettingsDataForKey("tejSAP");
                            html = "";
                            Com.ExecuteNonQuery(" Insert into PVSendMailLog(PVId,SignatoriesEmail,CFOmail,html,SendMailResponse) Values (" + PVID + ",'" + signatoryEmail + "','" + CFOEmail + "','" + html + "','" + sendmailresponse + "') ");
                        }
                        catch (Exception ex)
                        {
                            // lblMsg.Text = "Error occured in send mail " + ex.Message;
                            html = html.Replace('\'', '\"');
                            html = "";
                            //  Db.constr = myGlobal.getAppSettingsDataForKey("tejSAP");
                            Com.ExecuteNonQuery(" Insert into PVSendMailLog(PVId,SignatoriesEmail,CFOmail,html,SendMailResponse) Values (" + PVID + ",'" + signatoryEmail + "','" + CFOEmail + "','" + html + "',' Failed to send mail - " + ex.Message + "') ");
                        }
                    }
                }
            }
            catch (Exception ex)
            {
                // "Error occured in send mail " + ex.Message;
                // Db.constr = myGlobal.getAppSettingsDataForKey("tejSAP");

                Com.ExecuteNonQuery("Insert into PVSendMailLog(PVId,SignatoriesEmail,CFOmail,html,SendMailResponse) Values (" + PVID + ",'NA','NA','NA',' Failed to send mail - " + ex.Message + " ') ");
            }

            return result;

        }
        [Route("GetApprovalFill")]
        public ActionResult Get_Approver()
        {
            ContentResult retVal = null;
            DataSet DS;
            try
            {
                DS = RDD_Approval_TemplatesDBOperation.GetFillRadioButton();

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