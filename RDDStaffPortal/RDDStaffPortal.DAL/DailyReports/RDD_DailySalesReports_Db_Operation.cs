using RDDStaffPortal.DAL.DataModels.DailyReports;
using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.IO;
using System.Net.Mail;
using System.Transactions;
using System.Web.Mvc;
using static RDDStaffPortal.DAL.CommonFunction;

namespace RDDStaffPortal.DAL.DailyReports
{
    public class RDD_DailySalesReports_Db_Operation
    {
        CommonFunction Com = new CommonFunction();


        public bool SaveExcel(DataTable dt)
        {
            try
            {
                SqlParameter[] parm = { new SqlParameter("VisitDate",SqlDbType.NVarChar,0, "VisitDate"),
                                    new SqlParameter("VisitType",SqlDbType.NVarChar,0, "Distinct / Repeat"),
                                    new SqlParameter("Country",SqlDbType.NVarChar,0, "Country"),
                                    new SqlParameter("CardCode",SqlDbType.NVarChar,0, "CardCode"),
                                    new SqlParameter("Company",SqlDbType.NVarChar,0, "Customer"),
                                    new SqlParameter("PersonMet",SqlDbType.NVarChar,0, "Contact Person"),
                                    new SqlParameter("Email",SqlDbType.NVarChar,0, "Email"),
                                    new SqlParameter("Designation",SqlDbType.NVarChar,0, "Designation"),
                                    new SqlParameter("ContactNo",SqlDbType.NVarChar,0, "PhoneNo"),
                                    new SqlParameter("BU",SqlDbType.NVarChar,0, "BU"),
                                    new SqlParameter("Discussion",SqlDbType.NVarChar,0, "Discussion"),
                                    new SqlParameter("ExpectedBusinessAmt",SqlDbType.NVarChar,0, "Biz Amt"),
                                    new SqlParameter("ModeOfCall",SqlDbType.NVarChar,0, "Call Mode"),
                                    new SqlParameter("CallStatus",SqlDbType.NVarChar,0, "Call Type"),
                                    new SqlParameter("NextAction",SqlDbType.NVarChar,0, "Next Action"),
                                    new SqlParameter("Feedback",SqlDbType.NVarChar,0, "Feedback"),
                                    new SqlParameter("ForwardCallToEmail",SqlDbType.NVarChar,0, "Fwd CallTo"),
                                   new SqlParameter("ForwardRemark",SqlDbType.NVarChar,0, "FwdCall Remarks"),
                                   new SqlParameter("ReminderDate",SqlDbType.NVarChar,0, "Reminder Date"),
                                   new SqlParameter("ReminderDesc", SqlDbType.NVarChar, 0, "Reminder Description"),
                                    new SqlParameter("IsDraft", SqlDbType.Bit, 0, "IsDraft"),
                                    new SqlParameter("CreatedBy", SqlDbType.NVarChar, 0, "CreatedBy"),
                                     new SqlParameter("CreatedOn", SqlDbType.NVarChar, 0, "CreatedOn"),
                                    new SqlParameter("ToDate", SqlDbType.NVarChar, 0, "ToDate"),
                                    new SqlParameter("DocDraftDate",SqlDbType.NVarChar,0,"DocDraftDate"),
                                        new SqlParameter("ActualVisitDate",SqlDbType.NVarChar,0,"ActualVisitDate")};
                return Com.SqlBulkCopyQuery(dt, "dbo.RDD_DailySalesReports", parm);
            }
            catch (Exception ex)
            {

                return false;
            }

        }
        public RDD_DailySalesReports Save(RDD_DailySalesReports rDD_DSR)
        {
            try
            {
                using (TransactionScope scope = new TransactionScope())
                {
                    string response = string.Empty;
                    SqlParameter[] p = {
                        new SqlParameter("@p_VisitId", rDD_DSR.VisitId),
                        new SqlParameter("@p_VisitDate", rDD_DSR.VisitDate),
                        new SqlParameter("@p_ToDate", rDD_DSR.ToDate),
                        new SqlParameter("@p_VisitType", rDD_DSR.VisitType),
                        new SqlParameter("@p_IsNewPartner", rDD_DSR.IsNewPartner),
                        new SqlParameter("@p_Country", rDD_DSR.Country),
                        new SqlParameter("@p_CardCode", rDD_DSR.CardCode),
                        new SqlParameter("@p_Company", rDD_DSR.Company),
                        new SqlParameter("@p_PersonMet", rDD_DSR.PersonMet),
                        new SqlParameter("@p_Email", rDD_DSR.Email),
                        new SqlParameter("@p_Designation", rDD_DSR.Designation),
                        new SqlParameter("@p_ContactNo", rDD_DSR.ContactNo),
                        new SqlParameter("@p_BU", rDD_DSR.BU),
                        new SqlParameter("@p_Discussion", rDD_DSR.Discussion),
                        new SqlParameter("@p_ExpectedBusinessAmt", rDD_DSR.ExpectedBusinessAmt),
                        new SqlParameter("@p_CallStatus", rDD_DSR.CallStatus),
                        new SqlParameter("@p_ModeOfCall", rDD_DSR.ModeOfCall),
                        new SqlParameter("@p_NextAction", rDD_DSR.NextAction),
                        new SqlParameter("@p_Feedback", rDD_DSR.Feedback),
                        new SqlParameter("@p_ForwardCallToEmail", rDD_DSR.ForwardCallToEmail),
                        new SqlParameter("@p_ForwardRemark", rDD_DSR.ForwardRemark),
                        //new SqlParameter("@p_ForwardCallCCEmail",rDD_DSR.ForwardCallCCEmail),
                        //new SqlParameter("@p_Priority",rDD_DSR.Priority),
                        new SqlParameter("@p_IsDraft", rDD_DSR.IsDraft),
                        //  new SqlParameter("@p_NextReminderDate",rDD_DSR.NextReminderDate),
                        //new SqlParameter("@p_DocDraftDate",rDD_DSR.DocDraftDate),
                        new SqlParameter("@p_LoggedInUser", rDD_DSR.CreatedBy),
                        //new SqlParameter("@p_CreatedOn",rDD_DSR.CreatedOn),
                        //new SqlParameter("@p_LastUpdatedBy",rDD_DSR.LastUpdatedBy),
                        //new SqlParameter("@p_LastUpdatedOn",rDD_DSR.LastUpdatedOn),
                        //new SqlParameter("@p_IsActive",rDD_DSR.IsActive),
                        new SqlParameter("@p_ReminderDate", rDD_DSR.ReminderDate),
                        new SqlParameter("@p_ReminderDesc", rDD_DSR.ReminderDesc),
                        //new SqlParameter("@p_IsRead",rDD_DSR.IsRead),
                        //// new SqlParameter("@p_ReportReadBy",rDD_DSR.ReportReadBy),
                        // new SqlParameter("@p_ReportReadOn",rDD_DSR.ReportReadOn),
                        ////     new SqlParameter("@p_Comments",rDD_DSR.Comments),
                        new SqlParameter("@p_ActualVisitDate", rDD_DSR.ActualVisitDate),
                        //         new SqlParameter("@p_IsRptSentToManager",rDD_DSR.IsRptSentToManager),
                        new SqlParameter("@p_response", response)
                    };
                    SqlParameter[] parm = p;

                    List<Outcls> outcls = new List<Outcls>();
                    outcls = Com.ExecuteNonQueryList("RDD_DSR_InsertUpdate", parm);
                    rDD_DSR.SaveFlag = outcls[0].Outtf;
                    rDD_DSR.ErrorMsg = outcls[0].Responsemsg;
                    if(outcls[0].Outtf==true)
                    scope.Complete();
                }

            }
            catch (Exception ex)
            {
                rDD_DSR.ErrorMsg = ex.Message;
                rDD_DSR.SaveFlag = false;
            }

            return rDD_DSR;
        }

        public List<Outcls> saveComment(List<RDD_DailySalesReportComment> rDD_DailySales)
        {
            List<Outcls> outcls = new List<Outcls>();
            bool t = false;
            string errormsg = "Error occur";
            try
            {
                using (TransactionScope scope = new TransactionScope())
                {
                    int i = 0;
                    string visitIds = string.Empty;
                    string response = string.Empty;
                    while (rDD_DailySales.Count > i)
                    {
                        
                        SqlParameter[] p = {
                        new SqlParameter("@p_VisitId", rDD_DailySales[i].visitid),
                         new SqlParameter("@p_ReportReadBy",rDD_DailySales[i].ReportReadBy),
                         new SqlParameter("@p_ReportReadOn",rDD_DailySales[i].ReportReadOn),
                             new SqlParameter("@p_Comments",rDD_DailySales[i].Comments),
                              new SqlParameter("@p_PM_ReportReadBy",rDD_DailySales[i].PM_ReportReadBy),
                         new SqlParameter("@p_PM_ReportReadOn",rDD_DailySales[i].PM_ReportReadOn),
                             new SqlParameter("@p_PM_Comments",rDD_DailySales[i].PM_Comments),
                          new SqlParameter("@p_flag",rDD_DailySales[i].Flag),
                    };
                        SqlParameter[] parm = p;

                        t = Com.ExecuteNonQuery("RDD_Dailysales_Comment", parm);

                        if (string.IsNullOrEmpty(visitIds))
                            visitIds =rDD_DailySales[i].visitid.ToString();
                        else
                            visitIds = visitIds + "," + rDD_DailySales[i].visitid.ToString();

                        i++;
                    }

                    try
                    {
                        //visitIds = visitIds + "'";
                        string RptReadby = string.Empty;
                        if (rDD_DailySales[0].Flag == "H")
                            RptReadby = rDD_DailySales[0].ReportReadBy;
                        else
                            RptReadby = rDD_DailySales[0].PM_ReportReadBy;
                        SqlParameter[] parm = {
                        new SqlParameter("@p_LoggedInUser", RptReadby),
                        new SqlParameter("@p_VisitIds", visitIds),
                    };
                        DataSet ds1 = Com.ExecuteDataSet("DSR_SendReadReportEmail", CommandType.StoredProcedure, parm);

                       // bool retvalue = Com.ExecuteNonQuery("DSR_SendReadReportEmail", parm);
                    }
                    catch (Exception ex)
                    {

                    }

                    scope.Complete();
                   


                    if (t == true)
                    {
                        errormsg = "Save Succesfully";
                    }
                    outcls.Add(new Outcls
                    {
                        Outtf = t,
                        Responsemsg = errormsg
                    });
                }
            }
            catch (Exception ex)
            {
                outcls.Add(new Outcls
                {
                    Outtf = false,
                    Responsemsg = ex.Message
                });
            }
            return outcls;
        }
        public List<Outcls> DeleteActivity(string Username, int visitid)
        {
            List<Outcls> outcls = new List<Outcls>();
            try
            {
                string response = string.Empty;
                SqlParameter[] p = {
                        new SqlParameter("@p_visitid", visitid),
                        new SqlParameter("@p_username", Username),
                          new SqlParameter("@p_response", response)
                    };
                SqlParameter[] parm = p;


                outcls = Com.ExecuteNonQueryList("RDD_DailySalesReport_Delete", parm);
            }
            catch (Exception)
            {

                outcls[0].Outtf = false;
                outcls[0].Responsemsg = "Error Occur";
            }
            return outcls;
        }
        public List<Outcls> FinalSave(List<RDD_DailySalesReports> rDD_DailySales, MemoryStream ms)
        {
            List<Outcls> outcls = new List<Outcls>();
            try
            {
                using (TransactionScope scope = new TransactionScope())
                {
                    int i = 0;
                    string response = string.Empty;
                    while (rDD_DailySales.Count > i)
                    {
                        if (rDD_DailySales[i].IsDraft == true)
                        {
                            rDD_DailySales[i].IsDraft = false;
                            SqlParameter[] p = {
                        new SqlParameter("@p_VisitId", rDD_DailySales[i].VisitId),
                        new SqlParameter("@p_VisitDate", rDD_DailySales[i].VisitDate),
                        new SqlParameter("@p_ToDate", rDD_DailySales[i].ToDate),
                        new SqlParameter("@p_VisitType", rDD_DailySales[i].VisitType),
                        new SqlParameter("@p_IsNewPartner", rDD_DailySales[i].IsNewPartner),
                        new SqlParameter("@p_Country", rDD_DailySales[i].Country),
                        new SqlParameter("@p_CardCode", rDD_DailySales[i].CardCode),
                        new SqlParameter("@p_Company", rDD_DailySales[i].Company),
                        new SqlParameter("@p_PersonMet", rDD_DailySales[i].PersonMet),
                        new SqlParameter("@p_Email", rDD_DailySales[i].Email),
                        new SqlParameter("@p_Designation", rDD_DailySales[i].Designation),
                        new SqlParameter("@p_ContactNo", rDD_DailySales[i].ContactNo),
                        new SqlParameter("@p_BU", rDD_DailySales[i].BU),
                        new SqlParameter("@p_Discussion", rDD_DailySales[i].Discussion),
                        new SqlParameter("@p_ExpectedBusinessAmt", rDD_DailySales[i].ExpectedBusinessAmt),
                        new SqlParameter("@p_CallStatus", rDD_DailySales[i].CallStatus),
                        new SqlParameter("@p_ModeOfCall", rDD_DailySales[i].ModeOfCall),
                        new SqlParameter("@p_NextAction", rDD_DailySales[i].NextAction),
                        new SqlParameter("@p_Feedback", rDD_DailySales[i].Feedback),
                        new SqlParameter("@p_ForwardCallToEmail", rDD_DailySales[i].ForwardCallToEmail),
                        new SqlParameter("@p_ForwardRemark", rDD_DailySales[i].ForwardRemark),
                        //new SqlParameter("@p_ForwardCallCCEmail",rDD_DailySales[i].ForwardCallCCEmail),
                        //new SqlParameter("@p_Priority",rDD_DailySales[i].Priority),
                        new SqlParameter("@p_IsDraft", false),
                        //  new SqlParameter("@p_NextReminderDate",rDD_DailySales[i].NextReminderDate),
                        //new SqlParameter("@p_DocDraftDate",rDD_DailySales[i].DocDraftDate),
                        new SqlParameter("@p_LoggedInUser", rDD_DailySales[i].LastUpdatedBy),
                        //new SqlParameter("@p_CreatedOn",rDD_DailySales[i].CreatedOn),
                        //new SqlParameter("@p_LastUpdatedBy",rDD_DailySales[i].LastUpdatedBy),
                        //new SqlParameter("@p_LastUpdatedOn",rDD_DailySales[i].LastUpdatedOn),
                        //new SqlParameter("@p_IsActive",rDD_DailySales[i].IsActive),
                        new SqlParameter("@p_ReminderDate", rDD_DailySales[i].ReminderDate),
                        new SqlParameter("@p_ReminderDesc", rDD_DailySales[i].ReminderDesc),
                        //new SqlParameter("@p_IsRead",rDD_DailySales[i].IsRead),
                        //// new SqlParameter("@p_ReportReadBy",rDD_DailySales[i].ReportReadBy),
                        // new SqlParameter("@p_ReportReadOn",rDD_DailySales[i].ReportReadOn),
                        ////     new SqlParameter("@p_Comments",rDD_DailySales[i].Comments),
                        new SqlParameter("@p_ActualVisitDate", rDD_DailySales[i].ActualVisitDate),
                        //         new SqlParameter("@p_IsRptSentToManager",rDD_DailySales[i].IsRptSentToManager),
                        new SqlParameter("@p_response", response)
                    };
                            SqlParameter[] parm = p;


                            outcls = Com.ExecuteNonQueryList("RDD_DSR_InsertUpdate", parm);
                        }

                        i++;
                    }



                    DataSet ds1 = null;
                    try
                    {
                        SqlParameter[] parm ={
                        new SqlParameter("@p_username",rDD_DailySales[0].LastUpdatedBy),
                        };

                        ds1 = Com.ExecuteDataSet("RDD_DSR_SendMail_Finalsubmit", CommandType.StoredProcedure, parm);
                        
                        string sendemail = ds1.Tables[0].Rows[0]["SendReport"].ToString()==""?",": ds1.Tables[0].Rows[0]["SendReport"].ToString();
                        string Reportemail = ds1.Tables[0].Rows[0]["ReportMust"].ToString()==""?",": ds1.Tables[0].Rows[0]["ReportMust"].ToString();
                        string Tomail = sendemail.Substring(0, sendemail.Length - 1) + "," + Reportemail.Substring(0, Reportemail.Length - 1);
                        
                        if (Tomail!=",")
                        {
                            string html = " Dear " + Tomail + ", <br/><br/>  Please find attached <b> " + rDD_DailySales[0].LastUpdatedBy + " </b>'s customer visit report. <br/> ";
                            html = html + " Visit Date  -  <b> " + Convert.ToDateTime(rDD_DailySales[0].VisitDate).ToString("dd/MMM/yyyy") + " </b>  to  <b>  " + Convert.ToDateTime(rDD_DailySales[0].ToDate).ToString("dd/MMM/yyyy") + " </b>  <br/><br/>";
                            html = html + "Best Regards, <br/> Red Dot Distribution.";
                            string cc = ds1.Tables[0].Rows[0]["Self"].ToString();

                            string Filename = ds1.Tables[0].Rows[0]["Country"].ToString() + "-" + rDD_DailySales[0].LastUpdatedBy + "-Visit Report.xls";
                            Attachment at = new Attachment(ms, Filename);
                            string subject = Convert.ToDateTime(rDD_DailySales[0].VisitDate).ToString("dd/MMM/yyyy") + " - Customer visit Report -" + rDD_DailySales[0].LastUpdatedBy.ToUpper();

                            //Tomail = "pramod@reddotdistribution.com,Nikhilesh@reddotdistribution.com";
                            //cc = "nikhilesh@reddotdistribution.com";

                            if (SendMail.SendMailWithAttachment(Tomail, cc, subject, html, true, at) == "Mail Sent Succcessfully")
                            {
                                SqlParameter[] parm1 ={
                        new SqlParameter("@p_LoggedInUser",rDD_DailySales[0].LastUpdatedBy),
                        new SqlParameter("@p_VisitDate", rDD_DailySales[0].VisitDate),
                        new SqlParameter("@p_VisitToDate", rDD_DailySales[0].ToDate),
                        };
                                ds1 = Com.ExecuteDataSet("RDD_DSR_SetRptSentToManagerAndForwardCall", CommandType.StoredProcedure, parm1);
                            }

                            scope.Complete();

                        }
                        else
                        {
                            outcls[0].Outtf = false;
                            outcls[0].Responsemsg = "Reporting Frequency is not configured, please contact IT team.";

                        }

                        //Com.ExecuteNonQuery("update RDD_DailySalesReports set IsRptSentToManager=1 where VisitId =" + rDD_DailySales[0].VisitId + "");
                        
                    }
                    catch (Exception ex)
                    {

                        outcls[0].Outtf = false;
                        outcls[0].Responsemsg = ex.Message;
                    }
                }

            }
            catch (Exception ex)
            {
                outcls[0].Outtf = false;
                outcls[0].Responsemsg = ex.Message;
            }
            return outcls;
        }
        public List<Outcls> NewResseller(RDD_DSR_NewResellerEntry _NewResellerEntry)
        {
            List<Outcls> outcls = new List<Outcls>();
            try
            {
                using (TransactionScope scope = new TransactionScope())
                {
                    string response = string.Empty;
                    SqlParameter[] p = {
                        new SqlParameter("@p_Country", _NewResellerEntry.Country),
                        new SqlParameter("@p_CountryCode", _NewResellerEntry.CountryCode),
                        new SqlParameter("@p_NewResellerName", _NewResellerEntry.NewResellerName),
                        new SqlParameter("@p_LoggedInUser", _NewResellerEntry.CreatedBy),
                               new SqlParameter("@p_response", response)
                    };



                    outcls = Com.ExecuteNonQueryList("RDD_DSR_InsertNewRessler", p);


                    scope.Complete();
                }

            }
            catch (Exception ex)
            {
                outcls[0].Outtf = false;
                outcls[0].Responsemsg = ex.Message;
            }
            return outcls;
        }
        public DataSet GetDatas(DateTime FromDate, string UserName)
        {
            DataSet ds = new DataSet();
            try
            {
                SqlParameter[] parm ={
                        new SqlParameter("@username",UserName),
                        new SqlParameter("@p_FromDate",FromDate) };
                ds = Com.ExecuteDataSet("RDD_DSR_GetEmpFrequency", CommandType.StoredProcedure, parm);
            }
            catch (Exception)
            {
                ds = null;
            }
            return ds;
        }

        public DataSet GetRDD_DailyReportDateRange(DateTime FromDate, DateTime ToDate, string UserName)
        {
            //
            DataSet ds = null;
            try
            {
                SqlParameter[] parm ={
                        new SqlParameter("@p_username",UserName)
                ,new SqlParameter("@p_fromdate",FromDate    ),
                new SqlParameter("@p_todate",ToDate)};

                ds = Com.ExecuteDataSet("RDD_DailySalesReportDateRange", CommandType.StoredProcedure, parm);
            }
            catch (Exception)
            {

                ds = null;
            }
            return ds;
        }
        public DataSet GetRDD_PersonDet(string company)
        {
            DataSet ds = new DataSet();
            try
            {
                SqlParameter[] parm ={
                        new SqlParameter("@p_Company",company),
                       };
                ds = Com.ExecuteDataSet("RDD_PersonaDetails_GET", CommandType.StoredProcedure, parm);
            }
            catch (Exception)
            {
                ds = null;
            }
            return ds;
        }
        public DataSet GetWeeklyReports(string UserName)
        {
            DataSet ds = new DataSet();
            try
            {
                SqlParameter[] parm ={
                        new SqlParameter("@LoggedInUser",UserName) };
                ds = Com.ExecuteDataSet("RDD_DSR_GetWeeklyVisitSummary", CommandType.StoredProcedure, parm);
            }
            catch (Exception)
            {
                ds = null;
            }
            return ds;
        }



        public DataSet GetDrop(string UserName)
        {

            DataSet ds = new DataSet();
            try
            {
                SqlParameter[] parm ={
                        new SqlParameter("@p_username",UserName) };
                ds = Com.ExecuteDataSet("RDD_DSR_DROP", CommandType.StoredProcedure, parm);
            }
            catch (Exception)
            {
                ds = null;
            }
            return ds;
        }

        public DataSet FillCompnayName(string UserName, string Country)
        {
            DataSet ds = null;
            try
            {
                SqlParameter[] parm ={
                        new SqlParameter("@Country",Country),
                new SqlParameter("@ResellerName","")};
                ds = Com.ExecuteDataSet("getResellerList", CommandType.StoredProcedure, parm);

            }
            catch (Exception)
            {

                ds = null;
            }
            return ds;
        }


        public List<RDD_DailySalesReports> GetRDD_DailySalesReports(string UserName, DateTime fromdate, DateTime todate)
        {
            DataSet ds = null;
            List<SelectListItem> CountryList = new List<SelectListItem>();
            List<SelectListItem> ModeOfCallList = new List<SelectListItem>();
            List<SelectListItem> CallStatusList = new List<SelectListItem>();
            List<SelectListItem> NextActionList = new List<SelectListItem>();
            try
            {
                ds = GetDrop(UserName);
                CountryList.Add(new SelectListItem()
                {
                    Text = "Select",
                    Value = "0"
                });
                ModeOfCallList.Add(new SelectListItem()
                {
                    Text = "Select",
                    Value = "0"
                });
                CallStatusList.Add(new SelectListItem()
                {
                    Text = "Select",
                    Value = "0"
                });
                NextActionList.Add(new SelectListItem()
                {
                    Text = "Select",
                    Value = "0"
                });
                if (ds.Tables.Count > 0)
                {
                    DataTable dtModule;
                    DataRowCollection drc;
                    try
                    {
                        dtModule = ds.Tables[0];
                        drc = dtModule.Rows;
                        foreach (DataRow dr in drc)
                        {
                            CountryList.Add(new SelectListItem()
                            {
                                Text = dr["country"].ToString(),
                                Value = dr["country"].ToString()
                            }); ;
                        }
                    }
                    catch (Exception)
                    {
                        CountryList.Add(new SelectListItem()
                        {
                            Text = "Error",
                            Value = "-1"
                        });
                    }
                    try
                    {
                        dtModule = ds.Tables[1];
                        drc = dtModule.Rows;
                        foreach (DataRow dr in drc)
                        {
                            CallStatusList.Add(new SelectListItem()
                            {
                                Text = dr["CallStatus"].ToString(),
                                Value = dr["CallStatus"].ToString()
                            }); ;
                        }
                    }
                    catch (Exception)
                    {
                        CallStatusList.Add(new SelectListItem()
                        {
                            Text = "Error",
                            Value = "-1"
                        });
                    }
                    try
                    {
                        dtModule = ds.Tables[2];
                        drc = dtModule.Rows;
                        foreach (DataRow dr in drc)
                        {
                            ModeOfCallList.Add(new SelectListItem()
                            {
                                Text = dr["Modeofcall"].ToString(),
                                Value = dr["Modeofcall"].ToString()
                            }); ;
                        }
                    }
                    catch (Exception)
                    {

                        ModeOfCallList.Add(new SelectListItem()
                        {
                            Text = "Error",
                            Value = "-1"
                        });
                    }
                    try
                    {
                        dtModule = ds.Tables[3];
                        drc = dtModule.Rows;
                        foreach (DataRow dr in drc)
                        {
                            NextActionList.Add(new SelectListItem()
                            {
                                Text = dr["NextAction"].ToString(),
                                Value = dr["NextAction"].ToString()
                            }); ;
                        }
                    }
                    catch (Exception)
                    {
                        NextActionList.Add(new SelectListItem()
                        {
                            Text = "Error",
                            Value = "-1"
                        });
                    }
                }





            }
            catch (Exception)
            {
                CountryList.Add(new SelectListItem()
                {
                    Text = "Error",
                    Value = "-1"
                });
                ModeOfCallList.Add(new SelectListItem()
                {
                    Text = "Error",
                    Value = "-1"
                });
                CallStatusList.Add(new SelectListItem()
                {
                    Text = "Error",
                    Value = "-1"
                });
                NextActionList.Add(new SelectListItem()
                {
                    Text = "Error",
                    Value = "-1"
                });

            }

            List<RDD_DailySalesReports> GetListData = new List<RDD_DailySalesReports>();

            try
            {
                SqlParameter[] parm ={
                        new SqlParameter("@p_username",UserName)
                ,new SqlParameter("@p_fromdate",fromdate),
                new SqlParameter("@p_todate",todate)};

                ds = Com.ExecuteDataSet("Get_RDD_DailySalesReports_Details", CommandType.StoredProcedure, parm);
                if (ds.Tables.Count > 0)
                {
                    DataTable dt = ds.Tables[0];
                    DataRowCollection drc = dt.Rows;
                    foreach (DataRow dr in drc)
                    {
                        GetListData.Add(new RDD_DailySalesReports()
                        {
                            VisitId = !string.IsNullOrWhiteSpace(dr["VisitId"].ToString()) ? Convert.ToInt32(dr["VisitId"].ToString()) : 0,
                            VisitDate = !string.IsNullOrWhiteSpace(dr["VisitDate"].ToString()) ? Convert.ToDateTime(dr["VisitDate"].ToString()) : System.DateTime.Now,
                            ToDate = !string.IsNullOrWhiteSpace(dr["ToDate"].ToString()) ? Convert.ToDateTime(dr["ToDate"].ToString()) : System.DateTime.Now,
                            VisitType = !string.IsNullOrWhiteSpace(dr["VisitType"].ToString()) ? dr["VisitType"].ToString() : "",
                            IsNewPartner = !string.IsNullOrWhiteSpace(dr["IsNewPartner"].ToString()) ? Convert.ToBoolean(dr["IsNewPartner"].ToString()) : false,
                            Country = !string.IsNullOrWhiteSpace(dr["Country"].ToString()) ? dr["Country"].ToString() : "",
                            CardCode = !string.IsNullOrWhiteSpace(dr["CardCode"].ToString()) ? dr["CardCode"].ToString() : "",
                            Company = !string.IsNullOrWhiteSpace(dr["Company"].ToString()) ? dr["Company"].ToString() : "",
                            PersonMet = !string.IsNullOrWhiteSpace(dr["PersonMet"].ToString()) ? dr["PersonMet"].ToString() : "",
                            Email = !string.IsNullOrWhiteSpace(dr["Email"].ToString()) ? dr["Email"].ToString() : "",
                            Designation = !string.IsNullOrWhiteSpace(dr["Designation"].ToString()) ? dr["Designation"].ToString() : "",
                            ContactNo = !string.IsNullOrWhiteSpace(dr["ContactNo"].ToString()) ? dr["ContactNo"].ToString() : "",
                            BU = !string.IsNullOrWhiteSpace(dr["BU"].ToString()) ? dr["BU"].ToString() : "",
                            Discussion = !string.IsNullOrWhiteSpace(dr["Discussion"].ToString()) ? dr["Discussion"].ToString() : "",
                            ExpectedBusinessAmt = !string.IsNullOrWhiteSpace(dr["ExpectedBusinessAmt"].ToString()) ? Convert.ToDecimal(dr["ExpectedBusinessAmt"].ToString()) : 0,
                            CallStatus = !string.IsNullOrWhiteSpace(dr["CallStatus"].ToString()) ? dr["CallStatus"].ToString() : "",
                            ModeOfCall = !string.IsNullOrWhiteSpace(dr["ModeOfCall"].ToString()) ? dr["ModeOfCall"].ToString() : "",
                            NextAction = !string.IsNullOrWhiteSpace(dr["NextAction"].ToString()) ? dr["NextAction"].ToString() : "",
                            Feedback = !string.IsNullOrWhiteSpace(dr["Feedback"].ToString()) ? dr["Feedback"].ToString() : "",
                            ForwardCallToEmail = !string.IsNullOrWhiteSpace(dr["ForwardCallToEmail"].ToString()) ? dr["ForwardCallToEmail"].ToString() : "",
                            ForwardRemark = !string.IsNullOrWhiteSpace(dr["ForwardRemark"].ToString()) ? dr["ForwardRemark"].ToString() : "",
                            ForwardCallCCEmail = !string.IsNullOrWhiteSpace(dr["ForwardCallCCEmail"].ToString()) ? dr["ForwardCallCCEmail"].ToString() : "",
                            Priority = !string.IsNullOrWhiteSpace(dr["Priority"].ToString()) ? dr["Priority"].ToString() : "",
                            IsDraft = !string.IsNullOrWhiteSpace(dr["IsDraft"].ToString()) ? Convert.ToBoolean(dr["IsDraft"].ToString()) : false,
                            NextReminderDate = !string.IsNullOrWhiteSpace(dr["NextReminderDate"].ToString()) ? Convert.ToDateTime(dr["NextReminderDate"].ToString()) : System.DateTime.Now,
                            DocDraftDate = !string.IsNullOrWhiteSpace(dr["DocDraftDate"].ToString()) ? Convert.ToDateTime(dr["DocDraftDate"].ToString()) : System.DateTime.Now,
                            CreatedBy = !string.IsNullOrWhiteSpace(dr["CreatedBy"].ToString()) ? dr["CreatedBy"].ToString() : "",
                            CreatedOn = !string.IsNullOrWhiteSpace(dr["CreatedOn"].ToString()) ? Convert.ToDateTime(dr["CreatedOn"].ToString()) : System.DateTime.Now,
                            LastUpdatedBy = !string.IsNullOrWhiteSpace(dr["LastUpdatedBy"].ToString()) ? dr["LastUpdatedBy"].ToString() : "",
                            LastUpdatedOn = !string.IsNullOrWhiteSpace(dr["LastUpdatedOn"].ToString()) ? Convert.ToDateTime(dr["LastUpdatedOn"].ToString()) : System.DateTime.Now,
                            IsActive = !string.IsNullOrWhiteSpace(dr["IsActive"].ToString()) ? Convert.ToBoolean(dr["IsActive"].ToString()) : false,
                            ReminderDate = !string.IsNullOrWhiteSpace(dr["ReminderDate"].ToString()) ? Convert.ToDateTime(dr["ReminderDate"].ToString()) : System.DateTime.Now,
                            ReminderDesc = !string.IsNullOrWhiteSpace(dr["ReminderDesc"].ToString()) ? dr["ReminderDesc"].ToString() : "",
                            IsRead = !string.IsNullOrWhiteSpace(dr["IsRead"].ToString()) ? Convert.ToBoolean(dr["IsRead"].ToString()) : false,
                            ReportReadBy = !string.IsNullOrWhiteSpace(dr["ReportReadBy"].ToString()) ? dr["ReportReadBy"].ToString() : "",
                            ReportReadOn = !string.IsNullOrWhiteSpace(dr["ReportReadOn"].ToString()) ? Convert.ToDateTime(dr["ReportReadOn"].ToString()) : System.DateTime.Now,
                            Comments = !string.IsNullOrWhiteSpace(dr["Comments"].ToString()) ? dr["Comments"].ToString() : "",
                            ActualVisitDate = !string.IsNullOrWhiteSpace(dr["ActualVisitDate"].ToString()) ? Convert.ToDateTime(dr["ActualVisitDate"].ToString()) : System.DateTime.Now,
                            IsRptSentToManager = !string.IsNullOrWhiteSpace(dr["IsRptSentToManager"].ToString()) ? Convert.ToBoolean(dr["IsRptSentToManager"].ToString()) : false,
                            CountryList = CountryList,
                            ModeOfCallList = ModeOfCallList,
                            CallStatusList = CallStatusList,
                            NextActionList = NextActionList,
                        }
                            );
                    }
                }

            }
            catch (Exception)
            {
                GetListData.Add(new RDD_DailySalesReports()
                {
                    VisitId = 0,
                    VisitDate = System.DateTime.Now,
                    ToDate = System.DateTime.Now,
                    VisitType = "",
                    IsNewPartner = false,
                    Country = "",
                    CardCode = "",
                    Company = "",
                    PersonMet = "",
                    Email = "",
                    Designation = "",
                    ContactNo = "",
                    BU = "",
                    Discussion = "",
                    ExpectedBusinessAmt = 0,
                    CallStatus = "",
                    ModeOfCall = "",
                    NextAction = "",
                    Feedback = "",
                    ForwardCallToEmail = "",
                    ForwardRemark = "",
                    ForwardCallCCEmail = "",
                    Priority = "",
                    IsDraft = false,
                    NextReminderDate = System.DateTime.Now,
                    DocDraftDate = System.DateTime.Now,
                    CreatedBy = "",
                    CreatedOn = System.DateTime.Now,
                    LastUpdatedBy = "",
                    LastUpdatedOn = System.DateTime.Now,
                    IsActive = false,
                    ReminderDate = System.DateTime.Now,
                    ReminderDesc = "",
                    IsRead = false,
                    ReportReadBy = "",
                    ReportReadOn = System.DateTime.Now,
                    Comments = "",
                    ActualVisitDate = System.DateTime.Now,
                    IsRptSentToManager = false,
                    CountryList = CountryList,
                    ModeOfCallList = ModeOfCallList,
                    CallStatusList = CallStatusList,
                    NextActionList = NextActionList,
                });
            }
            return GetListData;

        }







    }
}
