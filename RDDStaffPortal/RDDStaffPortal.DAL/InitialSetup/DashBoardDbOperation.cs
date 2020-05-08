﻿using RDDStaffPortal.DAL.DataModels;
using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Common;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using static RDDStaffPortal.DAL.CommonFunction;

namespace RDDStaffPortal.DAL.InitialSetup
{
   public  class DashBoardDbOperation
    {
        CommonFunction Com = new CommonFunction();
        public string save1(RDD_DashBoard Dash)
        {
            List<Outcls> str = new List<Outcls>();
            string response = string.Empty;
            try
            {
                SqlParameter[] Para = {
                    new SqlParameter("@p_DashboardId",Dash.DashboardId),
                    new SqlParameter("@p_DashboardName",Dash.DashboardName),
                    new SqlParameter("@p_ModuleId",Dash.ModuleId),
                    new SqlParameter("@p_cssClass",Dash.cssClass),
                    new SqlParameter("@p_URL",Dash.URL),
                    new SqlParameter("@p_DisplaySeq",Dash.DisplaySeq),
                    new SqlParameter("@p_IsDefault",Dash.IsDefault),
                    new SqlParameter("@p_CreatedBy",Dash.CreatedBy),
                    new SqlParameter("@p_Levels",Dash.Levels),
                    new SqlParameter("@p_response",response),
                };
                str = Com.ExecuteNonQueryList("RDD_Dashboard_InsertUpdate", Para);
                response = str[0].Responsemsg;
            }
            catch (Exception ex)
            {
                response = "Error occured : " + ex.Message;
            }
            return response;
        }
        public string DeleteDashBoard(int menuid)
        {
            List<Outcls> str = new List<Outcls>();
            string response = string.Empty;
            try
            {
                SqlParameter[] Para = {
                    new SqlParameter("@p_DashboardId",menuid),
                    new SqlParameter("@p_response",response),
                };
                str = Com.ExecuteNonQueryList("RDD_Dashboard_Delete", Para);
                response = str[0].Responsemsg;
            }
            catch (Exception ex)
            {
                response = "Error occured : " + ex.Message;
            }
            return response;
        }


        public List<Card_Dash> GetCard_Dash(string username)
        {
            List<Card_Dash> _CardDash = new List<Card_Dash>();
            try
            {
                SqlParameter[] parm = { };                
                SqlParameter[] sqlpar =  {new SqlParameter("@p_UserName",username)  };
                DataSet dsModules = Com.ExecuteDataSet("RDD_Dashboard_GetRevVsForecastVsActual", CommandType.StoredProcedure, sqlpar);
                if (dsModules.Tables.Count > 0)
                {
                    DataTable dtModule = dsModules.Tables[0];
                    DataRowCollection drc = dtModule.Rows;
                    foreach (DataRow dr in drc)
                    {
                        _CardDash.Add(new Card_Dash()
                        {
                            ActualGP = !string.IsNullOrWhiteSpace(dr["ActualGP"].ToString()) ? Convert.ToDecimal(dr["ActualGP"].ToString()) : 0,
                           ActualRev = !string.IsNullOrWhiteSpace(dr["ActualRev"].ToString()) ? Convert.ToDecimal(dr["ActualRev"].ToString()) : 0,
                           GPForecast = !string.IsNullOrWhiteSpace(dr["GPForecast"].ToString()) ? Convert.ToDecimal(dr["GPForecast"].ToString()) : 0,
                            GPForecastAcheivedPercent= !string.IsNullOrWhiteSpace(dr["GPForecastAcheivedPercent"].ToString()) ? Convert.ToDecimal(dr["GPForecastAcheivedPercent"].ToString()) : 0,
                            GPTarget = !string.IsNullOrWhiteSpace(dr["GPTarget"].ToString()) ? Convert.ToDecimal(dr["GPTarget"].ToString()) : 0,
                            GPTrgetAcheivedPercent = !string.IsNullOrWhiteSpace(dr["GPTrgetAcheivedPercent"].ToString()) ? Convert.ToDecimal(dr["GPTrgetAcheivedPercent"].ToString()) : 0,
                            RevForecast = !string.IsNullOrWhiteSpace(dr["RevForecast"].ToString()) ? Convert.ToDecimal(dr["RevForecast"].ToString()) : 0,
                            RevForecastAcheivedPercent = !string.IsNullOrWhiteSpace(dr["RevForecastAcheivedPercent"].ToString()) ? Convert.ToDecimal(dr["RevForecastAcheivedPercent"].ToString()) : 0,
                            RevTarget = !string.IsNullOrWhiteSpace(dr["RevTarget"].ToString()) ? Convert.ToDecimal(dr["RevTarget"].ToString()) : 0,
                            RevTrgetAcheivedPercent = !string.IsNullOrWhiteSpace(dr["RevTrgetAcheivedPercent"].ToString()) ? Convert.ToDecimal(dr["RevTrgetAcheivedPercent"].ToString()) : 0,
                        });
                    }

                }
            }
            catch (Exception ex)
            {

                _CardDash = null;
            }
            return _CardDash;
        }

        public List<Datatables_Dash> GetData_Dash1(string username)
        {
            List<Datatables_Dash> _DtDash = new List<Datatables_Dash>();
            try
            {
                SqlParameter[] parm = { };
                SqlParameter[] sqlpar = { new SqlParameter("@p_UserName", username) };
                DataSet dsModules = Com.ExecuteDataSet("RDD_Dashboard_GetTopPerformer", CommandType.StoredProcedure, sqlpar);
                if (dsModules.Tables.Count > 0)
                {
                    DataTable dtModule = dsModules.Tables[0];
                    DataRowCollection drc = dtModule.Rows;
                    foreach (DataRow dr in drc)
                    {
                        _DtDash.Add(new Datatables_Dash()
                        {
                            Amount = !string.IsNullOrWhiteSpace(dr["Amount"].ToString()) ? Convert.ToDecimal(dr["Amount"].ToString()) : 0,
                            CompanyName = !string.IsNullOrWhiteSpace(dr["CompanyName"].ToString()) ? dr["CompanyName"].ToString() : "",
                            Date1 = !string.IsNullOrWhiteSpace(dr["Date"].ToString()) ? Convert.ToDateTime(dr["Date"].ToString()) : DateTime.Now,
                           
                        });
                    }

                }
            }
            catch (Exception ex)
            {

                _DtDash = null;
            }
            return _DtDash;
        }




        public List<Datatables_Dash> GetData_Dash2(string username,int i)
        {
            List<Datatables_Dash> _DtDash = new List<Datatables_Dash>();
            try
            {
                SqlParameter[] parm = { };
                SqlParameter[] sqlpar = { new SqlParameter("@p_UserName", username) };
                DataSet dsModules = Com.ExecuteDataSet("RDD_Dashboard_GetTopPerformer", CommandType.StoredProcedure, sqlpar);
                if (dsModules.Tables.Count > 0)
                {
                    DataTable dtModule = dsModules.Tables[i];

                    dtModule.Columns[0].ColumnName = "CompanyName";
                    DataRowCollection drc = dtModule.Rows;
                    foreach (DataRow dr in drc)
                    {
                        _DtDash.Add(new Datatables_Dash()
                        {
                            Amount = !string.IsNullOrWhiteSpace(dr["Amount"].ToString()) ? Convert.ToDecimal(dr["Amount"].ToString()) : 0,
                            CompanyName = !string.IsNullOrWhiteSpace(dr["CompanyName"].ToString()) ? dr["CompanyName"].ToString() : "",
                           

                        });
                    }

                }
            }
            catch (Exception ex)
            {

                _DtDash = null;
            }
            return _DtDash;
        }



        public List<Sales_BU> GetSales_BU(string username)
        {
            List<Sales_BU> _DtDash = new List<Sales_BU>();
            try
            {
                SqlParameter[] parm = { };
                SqlParameter[] sqlpar = { new SqlParameter("@p_UserName", username) };
                DataSet dsModules = Com.ExecuteDataSet("RDD_Dashboard_SalesByBU", CommandType.StoredProcedure, sqlpar);
                if (dsModules.Tables.Count > 0)
                {
                    DataTable dtModule = dsModules.Tables[0];

                    dtModule.Columns[0].ColumnName = "CompanyName";
                    dtModule.Columns[1].ColumnName = "Points1";
                    dtModule.Columns[2].ColumnName = "Points2";
                    dtModule.Columns[3].ColumnName = "Points3";
                    dtModule.Columns[4].ColumnName = "Points4";

                    DataRowCollection drc = dtModule.Rows;
                    foreach (DataRow dr in drc)
                    {
                        _DtDash.Add(new Sales_BU()
                        {
                            Points1 = !string.IsNullOrWhiteSpace(dr["Points1"].ToString()) ? Convert.ToDecimal(dr["Points1"].ToString()) : 0,
                            Points2 = !string.IsNullOrWhiteSpace(dr["Points2"].ToString()) ? Convert.ToDecimal(dr["Points2"].ToString()) : 0,
                            Points3 = !string.IsNullOrWhiteSpace(dr["Points3"].ToString()) ? Convert.ToDecimal(dr["Points3"].ToString()) : 0,
                            Points4 = !string.IsNullOrWhiteSpace(dr["Points4"].ToString()) ? Convert.ToDecimal(dr["Points4"].ToString()) : 0,
                            CompanyName  = !string.IsNullOrWhiteSpace(dr["CompanyName"].ToString()) ? dr["CompanyName"].ToString() : "",


                        });
                    }

                }
            }
            catch (Exception ex)
            {

                _DtDash = null;
            }
            return _DtDash;
        }
    }
}
