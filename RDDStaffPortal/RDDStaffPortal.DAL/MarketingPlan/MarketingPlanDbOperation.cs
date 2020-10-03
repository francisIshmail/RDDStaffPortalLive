﻿using RDDStaffPortal.DAL.DataModels.MarketingPlan;
using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Markup;

namespace RDDStaffPortal.DAL.MarketingPlan
{
   public class MarketingPlanDbOperation
    {
        CommonFunction Com = new CommonFunction();
        public List<RDD_VenderBu> GetBUList()
        {
            List<RDD_VenderBu> _BUList = new List<RDD_VenderBu>();
 try
            {
                Db.constr = Global.getConnectionStringByName("tejSAP");
                DataSet dscont = Db.myGetDS("RDD_VenderBu");
                if (dscont.Tables.Count > 0)
                {
                    DataTable dtCountry = dscont.Tables[0];
                    for (int i = 0; i < dtCountry.Rows.Count; i++)
                    {
                        RDD_VenderBu _BU = new RDD_VenderBu();

                        if (dtCountry.Rows[i]["BU"] != null && !DBNull.Value.Equals(dtCountry.Rows[i]["BU"]))
                        {
                            _BU.BU= dtCountry.Rows[i]["BU"].ToString();
                        }
                      

                        _BUList.Add(_BU);
                    }
                }
            }
            catch (Exception ex)
            {
                _BUList = null;
            }
            return _BUList;
           

        }

       public string SaveMaketingPlan(MarketingPlanMaster MPlanmas,List<MarketingPlanLines> mPlanLines,string usernm)
        {
            int result = 0;
            string result1 = "";
            string rslt = "";
            string drftval = "";
            try
            {
                if(MPlanmas.planStatus=="D")
                {
                    drftval = "1";
                }
                else
                {
                    drftval = "0";
                }
                
              
                SqlConnection SqlConn = null;
                SqlCommand SqlCmd = null;
                SqlDataAdapter da = null;
                SqlTransaction trans = null;
                string errormsg;
                DataSet ds = null;
                string Conn;
                
                    Conn = ConfigurationManager.ConnectionStrings["tejSAP"].ToString();

                SqlConn = new SqlConnection(Conn);
                    SqlConn.Open();
SqlCommand cmd = new SqlCommand("RDD_SaveMarketingPlan", SqlConn);
                cmd.CommandType = CommandType.StoredProcedure;
                

                

                cmd.Parameters.AddWithValue("@p_SourceOfFund", MPlanmas.SourceOfFund);
                cmd.Parameters.AddWithValue("@p_RefNo", MPlanmas.RefNo);
                cmd.Parameters.AddWithValue("@p_Country", MPlanmas.Country);
                cmd.Parameters.AddWithValue("@p_CountryName", MPlanmas.CountryName);
                cmd.Parameters.AddWithValue("@p_Vendor", MPlanmas.Vendor);
                cmd.Parameters.AddWithValue("@p_VendorApprovedAmt", MPlanmas.VendorApprovedAmt);
                cmd.Parameters.AddWithValue("@p_RDDApprovedAmt", MPlanmas.RDDApprovedAmt);
                cmd.Parameters.AddWithValue("@p_BalanceAmount", MPlanmas.BalanceAmount);
                cmd.Parameters.AddWithValue("@p_BalanceFromApp", MPlanmas.BalanceFromApp);
                cmd.Parameters.AddWithValue("@p_UsedAmount", MPlanmas.UsedAmount);
                cmd.Parameters.AddWithValue("@p_Description", MPlanmas.Description);
                cmd.Parameters.AddWithValue("@p_planStatus", MPlanmas.planStatus);
                cmd.Parameters.AddWithValue("@p_ApprovalStatus", MPlanmas.ApprovalStatus);
                cmd.Parameters.AddWithValue("@p_ApprovedBy", MPlanmas.ApprovedBy);
                cmd.Parameters.AddWithValue("@p_ApprovedOn", MPlanmas.ApprovedOn);
                cmd.Parameters.AddWithValue("@p_ApproverRemark", MPlanmas.ApproverRemark);
                cmd.Parameters.AddWithValue("@p_StartDate", MPlanmas.StartDate);
                cmd.Parameters.AddWithValue("@p_EndDate", MPlanmas.EndDate);
                cmd.Parameters.AddWithValue("@p_IsDraft", drftval);
                cmd.Parameters.AddWithValue("@p_CreatedOn", MPlanmas.CreatedOn);
                cmd.Parameters.AddWithValue("@p_CreatedBy", usernm);
                cmd.Parameters.AddWithValue("@p_LastUpdatedOn", MPlanmas.LastUpdatedOn);
                cmd.Parameters.AddWithValue("@p_LastUpdateBy", MPlanmas.LastUpdateBy);
                cmd.Parameters.AddWithValue("@p_Response", SqlDbType.Int);
                cmd.Parameters["@p_Response"].Direction = ParameterDirection.Output;

                cmd.ExecuteNonQuery();
                result = (int)cmd.Parameters["@p_Response"].Value;
           

                if (result !=0)
                {
                    for (int i = 0; i < mPlanLines.Count; i++) {
                        SqlParameter[] Para1 = {

                   new SqlParameter("@p_PlanId",Convert.ToInt32(result)),
new SqlParameter("@p_LineRefNo",mPlanLines[i].LineRefNo),
new SqlParameter("@p_VenderPONo",mPlanLines[i].VenderPONo),
new SqlParameter("@p_SAPPONo",mPlanLines[i].SAPPONo),
new SqlParameter("@p_ActivityDate ",mPlanLines[i].ActivityDate),
new SqlParameter("@p_Description",mPlanLines[i].Description),
        new SqlParameter("@p_Vendor",mPlanLines[i].Vendor),
        new SqlParameter("@p_Amount",mPlanLines[i].Amount),
        new SqlParameter("@p_Country",mPlanLines[i].Country),
        new SqlParameter("@p_CountryName",mPlanLines[i].CountryName),
        new SqlParameter("@p_Status",mPlanLines[i].Status),
        new SqlParameter("@p_Status1",mPlanLines[i].Status1),
        new SqlParameter("@p_ApprovedBy",mPlanLines[i].ApprovedBy),
        new SqlParameter("@p_ApprovedOn",""),
        new SqlParameter("@p_ApproverRemark",mPlanLines[i].ApproverRemark),
        new SqlParameter("@p_CreatedOn",""),
        new SqlParameter("@p_CreatedBy",usernm),
        new SqlParameter("@p_LastUpdatedOn",""),
        new SqlParameter("@p_Lastupdatedby",usernm),
        new SqlParameter("@p_Flag",""),
        new SqlParameter("@p_Response",1)

                };
                        Para1[20].Direction = ParameterDirection.Output;
                        
                        result1 = Com.ExecuteScalars("RDD_SaveMarketingPlanLines", Para1);// Db.myGetDS("RDD_MonthlyCountryBU");

                    }



   
                }
                rslt = "Record Saved Successfully";
               
            }catch(Exception e)
            {
                rslt = "Record Not Saved";
                
            }
            return rslt;
        }



        public List<Marketing_SearchData> GetFilList(string Country, string fund, string BU, string status, string Fromdt, string Todate)
        {
            List<Marketing_SearchData> _filList = new List<Marketing_SearchData>();
            try
            {
                Db.constr = Global.getConnectionStringByName("tejSAP");
                string cond = "";
                int cnt = 0;
                if(Country != "-- Select--" || fund !="0" || BU != "0" || status !="0" || Fromdt !="" || Todate != "")
                {
                    cond = "where ";
                }
                if(Country!= "-- Select--")
                {
                    cond += "CountryName='" + Country+"'";
                    cnt += 1;
                }
                if (fund != "0" )
                {
                    if (cnt >= 1)
                    {
                        cond += "  and SourceOfFund='" + fund +"'";
                        cnt += 1;
                    }
                    else { cond += "  SourceOfFund='" + fund+"'"; 
                        cnt += 1; }
                }
                if (BU != "0")
                {
                    if (cnt >= 1)
                    {
                        cond += " and Vendor='" + BU +"'";
                        cnt += 1;
                    }
                    else
                    {
                        cond += " Vendor='" + BU +"'";
                        cnt += 1;
                    }
                }
                if (status != "0")
                {
                    if (cnt >=1)
                    {
                        cond += " and planStatus='" + status +"'";
                        cnt += 1;
                    }
                    else
                    {
                        cond += " planStatus='" + status +"'";
                        cnt += 1;
                    }
                }
                if (Fromdt != "" && Todate!="")
                {
                    if (cnt >= 1)
                    {
                        cond += " and CreatedOn>='" + Fromdt + "'  and CreatedOn<='" + Todate+"'";
                    }
                    else
                    {
                        cond += " CreatedOn>='" + Fromdt + "'  and CreatedOn<='" + Todate +"'";
                    }
                }

                SqlParameter[] Para = {
                    new SqlParameter("@p_condition",cond)
                   
                };

                DataSet dscont = new DataSet();


                    dscont = Com.ExecuteDataSet("RDD_GetFilterMarketingPlan", CommandType.StoredProcedure, Para);// Db.myGetDS("RDD_MonthlyCountryBU");
                if (dscont.Tables.Count > 0)
                {
                    DataTable dtBU = dscont.Tables[0];
                    for (int i = 0; i < dtBU.Rows.Count; i++)
                    {
                        Marketing_SearchData _BU = new Marketing_SearchData();
                        _BU.Planid =Convert.ToInt32( dtBU.Rows[i]["PlanId"]);
                        _BU.SourceOfFund= dtBU.Rows[i]["SourceOfFund"].ToString();
                        _BU.CountryName= dtBU.Rows[i]["CountryName"].ToString();
                        _BU.StartDate= dtBU.Rows[i]["StartDate"].ToString();
                        _BU.EndDate = dtBU.Rows[i]["EndDate"].ToString();
                        _BU.Vendor= dtBU.Rows[i]["Vendor"].ToString();
                        _BU.VendorApprovedAmt= dtBU.Rows[i]["VendorApprovedAmt"].ToString();
                        _BU.RDDApprovedAmt= dtBU.Rows[i]["RDDApprovedAmt"].ToString();
                        _BU.UsedAmount = dtBU.Rows[i]["UsedAmount"].ToString();
                        _BU.BalanceAmount = dtBU.Rows[i]["BalanceAmount"].ToString();
                        _BU.BalanceFromApp = dtBU.Rows[i]["BalanceFromApp"].ToString();
                        _BU.Description = dtBU.Rows[i]["Description"].ToString();
                        _BU.ApprovedBy= dtBU.Rows[i]["ApprovedBy"].ToString();
                        _BU.planStatus = dtBU.Rows[i]["planStatus"].ToString();
                        _BU.ApprovalStatus= dtBU.Rows[i]["ApprovalStatus"].ToString();
                        _BU.CreatedOn = dtBU.Rows[i]["CreatedOn"].ToString();

                        _filList.Add(_BU);
                    }
                  
                }
            }
            catch (Exception ex)
            {
                _filList = null;
            }
            return _filList;


        }

        public List<Marketing_SearchData> GetAllList()
        {
            List<Marketing_SearchData> _filList = new List<Marketing_SearchData>();
            try
            {
                Db.constr = Global.getConnectionStringByName("tejSAP");
                string cond = "";
                int cnt = 0;
              

                DataSet dscont = new DataSet();


                dscont = Db.myGetDS("RDD_GetAllMarketingPlan");// Db.myGetDS("RDD_MonthlyCountryBU");
                if (dscont.Tables.Count > 0)
                {
                    DataTable dtBU = dscont.Tables[0];
                    for (int i = 0; i < dtBU.Rows.Count; i++)
                    {
                        Marketing_SearchData _BU = new Marketing_SearchData();
                        _BU.Planid = Convert.ToInt32(dtBU.Rows[i]["PlanId"]);
                        _BU.SourceOfFund = dtBU.Rows[i]["SourceOfFund"].ToString();
                        _BU.CountryName = dtBU.Rows[i]["CountryName"].ToString();
                        _BU.StartDate = dtBU.Rows[i]["StartDate"].ToString();
                        _BU.EndDate = dtBU.Rows[i]["EndDate"].ToString();
                        _BU.Vendor = dtBU.Rows[i]["Vendor"].ToString();
                        _BU.VendorApprovedAmt = dtBU.Rows[i]["VendorApprovedAmt"].ToString();
                        _BU.RDDApprovedAmt = dtBU.Rows[i]["RDDApprovedAmt"].ToString();
                        _BU.UsedAmount = dtBU.Rows[i]["UsedAmount"].ToString();
                        _BU.BalanceAmount = dtBU.Rows[i]["BalanceAmount"].ToString();
                        _BU.BalanceFromApp = dtBU.Rows[i]["BalanceFromApp"].ToString();
                        _BU.Description = dtBU.Rows[i]["Description"].ToString();
                        _BU.ApprovedBy = dtBU.Rows[i]["ApprovedBy"].ToString();
                        _BU.planStatus = dtBU.Rows[i]["planStatus"].ToString();
                        _BU.ApprovalStatus = dtBU.Rows[i]["ApprovalStatus"].ToString();
                        _BU.CreatedOn = dtBU.Rows[i]["CreatedOn"].ToString();

                        _filList.Add(_BU);
                    }

                }
            }
            catch (Exception ex)
            {
                _filList = null;
            }
            return _filList;
        }

        public  MarketingPlanMaster GetMarketingplan (string id)
        {
            //RDD_getMeketingPlanDtl
            //@p_planid
            // marketing> _BUList = new List<RDD_VenderBu>();
            MarketingPlanMaster _BU = new MarketingPlanMaster();
            try
            {
                Db.constr = Global.getConnectionStringByName("tejSAP");
                SqlParameter[] Para = {
                    new SqlParameter("@p_planid",id),
                   
                };
                DataSet dscont = Com.ExecuteDataSet("RDD_getMeketingPlanDtl", CommandType.StoredProcedure, Para);// Db.myGetDS("RDD_MonthlyCountryBU");

               // DataSet dscont = Db.myGetDS("RDD_getMeketingPlanDtl");
                if (dscont.Tables.Count > 0)
                {
                    DataTable dtCountry = dscont.Tables[0];
                    if(dtCountry.Rows.Count>0)
                    {
                        _BU.SourceOfFund= dtCountry.Rows[0]["SourceOfFund"].ToString();
                        _BU .RefNo = dtCountry.Rows[0]["RefNo"].ToString();
                        _BU.Country = dtCountry.Rows[0]["Country"].ToString();
                        _BU.CountryName = dtCountry.Rows[0]["CountryName"].ToString();
                        _BU. Vendor = dtCountry.Rows[0]["Vendor"].ToString();
                        _BU.VendorApprovedAmt =Convert.ToInt32( dtCountry.Rows[0]["VendorApprovedAmt"]);
                        _BU.RDDApprovedAmt =Convert.ToInt32( dtCountry.Rows[0]["RDDApprovedAmt"]);
                        _BU. BalanceAmount =Convert.ToInt32( dtCountry.Rows[0]["BalanceAmount"]);
                        _BU.BalanceFromApp =Convert.ToInt32( dtCountry.Rows[0]["BalanceFromApp"]);
                        _BU.UsedAmount = Convert.ToInt32( dtCountry.Rows[0]["UsedAmount"]);
                        _BU.Description = dtCountry.Rows[0]["Description"].ToString();
                        _BU.planStatus = dtCountry.Rows[0]["planStatus"].ToString();
                        _BU. ApprovalStatus = dtCountry.Rows[0]["ApprovalStatus"].ToString();
                       // _BU. ApprovedBy = dtCountry.Rows[i]["SourceOfFund"].ToString();
                      //  _BU. ApprovedOn = dtCountry.Rows[i]["SourceOfFund"].ToString();
                        _BU. ApproverRemark = dtCountry.Rows[0]["ApproverRemark"].ToString();
                        _BU.StartDate = dtCountry.Rows[0]["StartDate"].ToString();
                        _BU. EndDate = dtCountry.Rows[0]["EndDate"].ToString();
                       // _BU.IsDraft = dtCountry.Rows[i]["SourceOfFund"].ToString();
                        _BU.CreatedOn = dtCountry.Rows[0]["CreatedOn"].ToString();




                    }
                }
            }
            catch (Exception ex)
            {
                _BU= null;
            }
            return _BU;
        }

        public List<MarketingPlanLines> GetMarketingplanLine(string id)
        {
            List<MarketingPlanLines> _BU = new List<MarketingPlanLines>();
            try
            {
                Db.constr = Global.getConnectionStringByName("tejSAP");
                SqlParameter[] Para = {
                    new SqlParameter("@p_planid",id),

                };
                DataSet dscont = Com.ExecuteDataSet("RDD_GetmarketingPlanLines", CommandType.StoredProcedure, Para);// Db.myGetDS("RDD_MonthlyCountryBU");

                // DataSet dscont = Db.myGetDS("RDD_getMeketingPlanDtl");
                if (dscont.Tables.Count > 0)
                {
                   
                        DataTable dtBU = dscont.Tables[0];
                        for (int i = 0; i < dtBU.Rows.Count; i++)
                        {
                        MarketingPlanLines bu = new MarketingPlanLines();

                        bu.ActivityDate = dtBU.Rows[i]["ActivityDate"].ToString();
                            bu.CountryName = dtBU.Rows[i]["CountryName"].ToString();
                        bu.Vendor = dtBU.Rows[i]["Vendor"].ToString();
                        bu.Description = dtBU.Rows[i]["Description"].ToString();
                        bu.Amount = dtBU.Rows[i]["Amount"].ToString();
                        bu.VenderPONo = dtBU.Rows[i]["VenderPONo"].ToString();
                        bu.SAPPONo = dtBU.Rows[i]["SAPPONo"].ToString();
                        bu.Status = dtBU.Rows[i]["Status"].ToString();
                        bu.Status1 = dtBU.Rows[i]["Status1"].ToString();
                        bu.ApproverRemark = dtBU.Rows[i]["ApproverRemark"].ToString();
                        bu.LineRefNo = dtBU.Rows[i]["LineRefNo"].ToString();
                        _BU.Add(bu);
                     }
                }
            }
            catch (Exception ex)
            {
                _BU = null;
            }
            return _BU;
        }
    }
}