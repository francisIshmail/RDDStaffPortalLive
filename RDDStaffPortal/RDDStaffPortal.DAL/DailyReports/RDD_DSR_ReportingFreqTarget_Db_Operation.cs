using RDDStaffPortal.DAL.DataModels.DailyReports;
using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Transactions;
using System.Web.Mvc;
using static RDDStaffPortal.DAL.CommonFunction;

namespace RDDStaffPortal.DAL.DailyReports
{
  public  class RDD_DSR_ReportingFreqTarget_Db_Operation
    {

        CommonFunction Com = new CommonFunction();

        public RDD_DSR_ReportingFreqTarget Save(RDD_DSR_ReportingFreqTarget rDD_DSR_Reporting)
        {
            try
            {
                using (TransactionScope scope = new TransactionScope())
                {
                    string response = string.Empty;
                    int i=0;                    
                    if(rDD_DSR_Reporting.RDD_DSR_ReportingFreqTargetDetailnew!= null)
                    {                        
                        while (rDD_DSR_Reporting.RDD_DSR_ReportingFreqTargetDetailnew.Count > i)
                        {                           
                            SqlParameter[] parm ={
                              
                                new SqlParameter("@p_countrycode",rDD_DSR_Reporting.Country),
                                new SqlParameter("@p_CreatedOn",System.DateTime.Now),
                                new SqlParameter("@p_DesigId",rDD_DSR_Reporting.RDD_DSR_ReportingFreqTargetDetailnew[i].DesigId),
                                new SqlParameter("@p_EmpId",rDD_DSR_Reporting.RDD_DSR_ReportingFreqTargetDetailnew[i].EmpId),
                                new SqlParameter("@p_freqOfRpt",rDD_DSR_Reporting.RDD_DSR_ReportingFreqTargetDetailnew[i].freqOfRpt),
                                new SqlParameter("@p_ReportMustReadBy",rDD_DSR_Reporting.RDD_DSR_ReportingFreqTargetDetailnew[i].ReportMustReadBy),
                                new SqlParameter("@p_SendReportTo",rDD_DSR_Reporting.RDD_DSR_ReportingFreqTargetDetailnew[i].SendReportTo),
                                new SqlParameter("@p_VisitPerMonth",rDD_DSR_Reporting.RDD_DSR_ReportingFreqTargetDetailnew[i].VisitPerMonth),
                                new SqlParameter("@p_response",response)   };
                            List<Outcls> outcls = new List<Outcls>();
                            outcls = Com.ExecuteNonQueryList("RDD_DSR_ReportingFreqTarget_Insert_Update", parm);
                            rDD_DSR_Reporting.Saveflag = outcls[0].Outtf;
                            rDD_DSR_Reporting.ErrorMsg = outcls[0].Responsemsg;
                            if (outcls[0].Outtf == false)
                            {
                                i = rDD_DSR_Reporting.RDD_DSR_ReportingFreqTargetDetailnew.Count;                                   
                                return rDD_DSR_Reporting;
                            }
                            i++;
                        }
                    }
                    if (rDD_DSR_Reporting.Saveflag == true)
                    {
                        scope.Complete();
                    }
                   
                }

            }
            catch (Exception ex)
            {
                rDD_DSR_Reporting.ErrorMsg = ex.Message;
                rDD_DSR_Reporting.Saveflag = false;
            }

            return rDD_DSR_Reporting;
        }

        public RDD_DSR_ReportingFreqTarget GetDropList(string username)
        {
            RDD_DSR_ReportingFreqTarget RDRFT = new RDD_DSR_ReportingFreqTarget();
            List<SelectListItem> CountryList = new List<SelectListItem>();
            List<SelectListItem> freqOfRptList = new List<SelectListItem>();
            List<SelectListItem> SendReportList = new List<SelectListItem>();
            List<SelectListItem> ReportMustReadList = new List<SelectListItem>();          
            CountryList.Add(new SelectListItem()
            {
                Text = "--Select--",
                Value = "0",
            });
            freqOfRptList.Add(new SelectListItem()
            {
                Text = "--Select--",
                Value = "0",
            });
            SendReportList.Add(new SelectListItem()
            {
                Text = "--Select--",
                Value = "0",
            });
            ReportMustReadList.Add(new SelectListItem()
            {
                Text = "--Select--",
                Value = "0",
            });            
            try
            {
                SqlParameter[] parm = { new SqlParameter("@p_username", username) };
                DataSet dsModules = Com.ExecuteDataSet("RDD_SendReportTo_GetList", CommandType.StoredProcedure, parm);
                if (dsModules.Tables.Count > 0)
                {
                    DataTable dtModule;
                    DataRowCollection drc;
                    try
                    {
                        dtModule = dsModules.Tables[1];
                        drc = dtModule.Rows;
                        foreach (DataRow dr in drc)
                        {
                            CountryList.Add(new SelectListItem()
                            {
                                Text = !string.IsNullOrWhiteSpace(dr["Country"].ToString()) ? dr["Country"].ToString() : "",
                                Value = !string.IsNullOrWhiteSpace(dr["CountryCode"].ToString()) ? dr["CountryCode"].ToString() : "",
                            });
                        }
                    }
                    catch (Exception)
                    {
                        CountryList.Add(new SelectListItem()
                        {
                            Text = "Error",
                            Value = "-1",
                        });

                    }
                    try
                    {

                        dtModule = dsModules.Tables[3];
                        drc = dtModule.Rows;
                        foreach (DataRow dr in drc)
                        {
                            freqOfRptList.Add(new SelectListItem()
                            {
                                Text = !string.IsNullOrWhiteSpace(dr["FreqType"].ToString()) ? dr["FreqType"].ToString() : "",
                                Value = !string.IsNullOrWhiteSpace(dr["FreqType"].ToString()) ? dr["FreqType"].ToString() : "",
                            });
                        }
                    }
                    catch (Exception)
                    {

                        freqOfRptList.Add(new SelectListItem()
                        {
                            Text = "Error",
                            Value = "-1",
                        });
                    }
                    try
                    {
                        dtModule = dsModules.Tables[0];
                        drc = dtModule.Rows;
                        foreach (DataRow dr in drc)
                        {
                            SendReportList.Add(new SelectListItem()
                            {
                                Text = !string.IsNullOrWhiteSpace(dr["SendReportTo"].ToString()) ? dr["SendReportTo"].ToString() : "",
                                Value = !string.IsNullOrWhiteSpace(dr["SendReportTo"].ToString()) ? dr["SendReportTo"].ToString() : "",
                            });
                        }
                    }
                    catch (Exception)
                    {

                        SendReportList.Add(new SelectListItem()
                        {
                            Text = "Error",
                            Value = "-1",
                        });
                    }
                    try
                    {
                        dtModule = dsModules.Tables[2];
                        drc = dtModule.Rows;
                        foreach (DataRow dr in drc)
                        {
                            ReportMustReadList.Add(new SelectListItem()
                            {
                                Text = !string.IsNullOrWhiteSpace(dr["Email"].ToString()) ? dr["Email"].ToString() : "",
                                Value = !string.IsNullOrWhiteSpace(dr["EmployeeId"].ToString()) ? dr["EmployeeId"].ToString() : "",
                            });
                        }
                    }
                    catch (Exception)
                    {

                        ReportMustReadList.Add(new SelectListItem()
                        {
                            Text = "Error",
                            Value = "-1",
                        });
                    }                                  
                }
            }
            catch (Exception)
            {                
                CountryList.Add(new SelectListItem()
                {
                    Text = "Error",
                    Value = "-1",
                });
                freqOfRptList.Add(new SelectListItem()
                {
                    Text = "Error",
                    Value = "-1",
                });
                SendReportList.Add(new SelectListItem()
                {
                    Text = "Error",
                    Value = "-1",
                });
                ReportMustReadList.Add(new SelectListItem()
                {
                    Text = "Error",
                    Value = "-1",
                });
            }
            RDRFT.CountryList = CountryList;
            RDRFT.freqOfRptList = freqOfRptList;
            RDRFT.SendReportList = SendReportList;
            RDRFT.ReportMustReadList = ReportMustReadList;
            return RDRFT;
        }
        public DataSet GetReportingTarget(string username,string countrycode)
        {
            DataSet ds = null;
            try
            {
                SqlParameter[] parm = { new SqlParameter("@p_username", username),
                    new SqlParameter("@p_CountryCode", countrycode),
                };
                ds = Com.ExecuteDataSet("RDD_DSR_GetCountryWiseReportingFrequencyAndTrgets", CommandType.StoredProcedure, parm);
            }
            catch (Exception)
            {
                ds = null;
            }
            return ds;


        }
        public DataSet GetPerson(string username, string countrycode) {
            DataSet ds = null;
            try
            {
                SqlParameter[] parm = { new SqlParameter("@p_LoggedInUser", username),
                    new SqlParameter("@p_CountryCode", countrycode),
                };
                ds = Com.ExecuteDataSet("RDD_DSR_EmpUnderManager", CommandType.StoredProcedure, parm);
            }
            catch (Exception)
            {
                ds = null;
            }
            return ds;


        }
        public DataSet GetPersonData(string username, int yy,int month)
        {
            DataSet ds = null;
            try
            {
                SqlParameter[] parm = { new SqlParameter("@p_Month", month),
                    new SqlParameter("@p_Year", yy),
                    new SqlParameter("@p_person",username)
                };
                ds = Com.ExecuteDataSet("RDD_DSR_GetScoreCardByNameNEW", CommandType.StoredProcedure, parm);
            }
            catch (Exception)
            {
                ds = null;
            }
            return ds;


        }

    }
}
