using RDDStaffPortal.DAL.DataModels;
using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Common;
using System.Data.SqlClient;
using System.IO;
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
                            GPForecastAcheivedPercent= !string.IsNullOrWhiteSpace(dr["GPForecastAcheivedPercent"].ToString()) ? Convert.ToInt32(dr["GPForecastAcheivedPercent"].ToString()) : 0,
                            GPTarget = !string.IsNullOrWhiteSpace(dr["GPTarget"].ToString()) ? Convert.ToDecimal(dr["GPTarget"].ToString()) : 0,
                            GPTrgetAcheivedPercent = !string.IsNullOrWhiteSpace(dr["GPTrgetAcheivedPercent"].ToString()) ? Convert.ToInt32(dr["GPTrgetAcheivedPercent"].ToString()) : 0,
                            RevForecast = !string.IsNullOrWhiteSpace(dr["RevForecast"].ToString()) ? Convert.ToDecimal(dr["RevForecast"].ToString()) : 0,
                            RevForecastAcheivedPercent = !string.IsNullOrWhiteSpace(dr["RevForecastAcheivedPercent"].ToString()) ? Convert.ToInt32(dr["RevForecastAcheivedPercent"].ToString()) : 0,
                            RevTarget = !string.IsNullOrWhiteSpace(dr["RevTarget"].ToString()) ? Convert.ToDecimal(dr["RevTarget"].ToString()) : 0,
                            RevTrgetAcheivedPercent = !string.IsNullOrWhiteSpace(dr["RevTrgetAcheivedPercent"].ToString()) ? Convert.ToInt32(dr["RevTrgetAcheivedPercent"].ToString()) : 0,
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
        public DataSet GetMainDash_V1(string username)
        {
            DataSet dsModules = new DataSet();
            try
            {

                SqlParameter[] parm = { };
                SqlParameter[] sqlpar = { new SqlParameter("@p_UserName", username) };
                dsModules = Com.ExecuteDataSet("RDD_Dashboard_Main_V1_Rights", CommandType.StoredProcedure, sqlpar);
            }
            catch (Exception)
            {

                dsModules = null;
            }

            return dsModules;
        }
        public DataSet GetMainDash(string username)
        {
            DataSet dsModules = new DataSet();
            try
            {

                SqlParameter[] parm = { };
                SqlParameter[] sqlpar = { new SqlParameter("@p_UserName", username) };
                dsModules = Com.ExecuteDataSet("RDD_Dashboard_Main", CommandType.StoredProcedure, sqlpar);
            }
            catch (Exception)
            {

                dsModules = null;
            }
            
            return dsModules;
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

        public List<Sales_BU> GetSalesSummery(string username)
        {
            List<Sales_BU> _DtDash = new List<Sales_BU>();
            try
            {
                SqlParameter[] parm = { };
                SqlParameter[] sqlpar = { new SqlParameter("@p_UserName", username) };
                DataSet dsModules = Com.ExecuteDataSet("RDD_Dashboard_GetSalesSummary", CommandType.StoredProcedure, sqlpar);
                if (dsModules.Tables.Count > 0)
                {
                    DataTable dtModule = dsModules.Tables[0];

                    //dtModule.Columns[0].ColumnName = "CompanyName";
                    dtModule.Columns[2].ColumnName = "Points1";
                    dtModule.Columns[3].ColumnName = "Points2";
                    dtModule.Columns[4].ColumnName = "Points3";
                    dtModule.Columns[5].ColumnName = "Points4";
                    dtModule.Columns[6].ColumnName = "Points5";
                    dtModule.Columns[7].ColumnName = "Points6";

                    DataRowCollection drc = dtModule.Rows;
                    foreach (DataRow dr in drc)
                    {
                        _DtDash.Add(new Sales_BU()
                        {
                            Points1 = !string.IsNullOrWhiteSpace(dr["Points1"].ToString()) ? Convert.ToDecimal(dr["Points1"].ToString()) : 0,
                            Points2 = !string.IsNullOrWhiteSpace(dr["Points2"].ToString()) ? Convert.ToDecimal(dr["Points2"].ToString()) : 0,
                            Points3 = !string.IsNullOrWhiteSpace(dr["Points3"].ToString()) ? Convert.ToDecimal(dr["Points3"].ToString()) : 0,
                            Points4 = !string.IsNullOrWhiteSpace(dr["Points4"].ToString()) ? Convert.ToDecimal(dr["Points4"].ToString()) : 0,
                            Points5 = !string.IsNullOrWhiteSpace(dr["Points5"].ToString()) ? Convert.ToDecimal(dr["Points5"].ToString()) : 0,
                            Points6 = !string.IsNullOrWhiteSpace(dr["Points6"].ToString()) ? Convert.ToDecimal(dr["Points6"].ToString()) : 0,
                           


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


        public List<Pichart_Dash> GetPichart1(string username)
        {
            List<Pichart_Dash> _DtDash = new List<Pichart_Dash>();
            try
            {
                SqlParameter[] parm = { };
                SqlParameter[] sqlpar = { new SqlParameter("@p_UserName", username) };
                DataSet dsModules = Com.ExecuteDataSet("RDD_Dashboard_CustomerStatusStatistics", CommandType.StoredProcedure, sqlpar);
                if (dsModules.Tables.Count > 0)
                {
                    DataTable dtModule = dsModules.Tables[0];

                    dtModule.Columns[1].ColumnName = "lblname";
                    dtModule.Columns[0].ColumnName = "Points1";
                    dtModule.Columns[2].ColumnName = "Bgcolor";


                    DataRowCollection drc = dtModule.Rows;
                    foreach (DataRow dr in drc)
                    {
                        _DtDash.Add(new Pichart_Dash()
                        {
                            lblname = !string.IsNullOrWhiteSpace(dr["lblname"].ToString()) ? dr["lblname"].ToString() : "",
                            points = !string.IsNullOrWhiteSpace(dr["Points1"].ToString()) ? Convert.ToDecimal(dr["Points1"].ToString()) : 0,
                            bgcolrs = !string.IsNullOrWhiteSpace(dr["Bgcolor"].ToString()) ? dr["Bgcolor"].ToString() : "",


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
        public List<Pichart_Dash> GetPichart2(string username)
        {
            List<Pichart_Dash> _DtDash = new List<Pichart_Dash>();
            try
            {
                SqlParameter[] parm = { };
                SqlParameter[] sqlpar = { new SqlParameter("@p_UserName", username) };
                DataSet dsModules = Com.ExecuteDataSet("RDD_Dashboard_CustomerStatusStatistics", CommandType.StoredProcedure, sqlpar);
                if (dsModules.Tables.Count > 0)
                {
                    DataTable dtModule = dsModules.Tables[1];

                    dtModule.Columns[1].ColumnName = "lblname";
                    dtModule.Columns[0].ColumnName = "Points1";
                    dtModule.Columns[2].ColumnName = "Bgcolor";


                    DataRowCollection drc = dtModule.Rows;
                    foreach (DataRow dr in drc)
                    {
                        _DtDash.Add(new Pichart_Dash()
                        {
                            lblname = !string.IsNullOrWhiteSpace(dr["lblname"].ToString()) ? dr["lblname"].ToString() : "",
                            points = !string.IsNullOrWhiteSpace(dr["Points1"].ToString()) ? Convert.ToDecimal(dr["Points1"].ToString()) : 0,
                            bgcolrs=!string.IsNullOrWhiteSpace(dr["Bgcolor"].ToString()) ? dr["Bgcolor"].ToString() : "",


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

        public List<SecondCard> GetSecondCard(string username)
        {

            List<SecondCard> _CardDash = new List<SecondCard>();
            try
            {
               
               SqlParameter[] sqlpar = { new SqlParameter("@p_UserName", username) };
                DataSet dsModules = Com.ExecuteDataSet("RDD_Dashboard_Get_Receivable_Payable_BankBalance", CommandType.StoredProcedure, sqlpar);
                if (dsModules.Tables.Count > 0)
                {
                    DataTable dtModule = dsModules.Tables[0];
                    DataRowCollection drc = dtModule.Rows;
                    foreach (DataRow dr in drc)
                    {
                        _CardDash.Add(new SecondCard()
                        {
                            TotalPay = !string.IsNullOrWhiteSpace(dr["TotalPayable"].ToString()) ? Convert.ToDecimal(dr["TotalPayable"].ToString()) : 0,
                           TotalRece = !string.IsNullOrWhiteSpace(dr["TotalReceivable"].ToString()) ? Convert.ToDecimal(dr["TotalReceivable"].ToString()) : 0,
                            BankBalance  = !string.IsNullOrWhiteSpace(dr["BankBalance"].ToString()) ? Convert.ToDecimal(dr["BankBalance"].ToString()) : 0,
                        });
                    }

                }
            }
            catch (Exception ex)
            {
                _CardDash.Add(new SecondCard()
                {
                    TotalPay =  0,
                    TotalRece = 0,

                });
            }
            return _CardDash;
        }


        public List<RDD_Model_tbl> GetRecModel_tbl(string username)
        {

            List<RDD_Model_tbl> _CardDash = new List<RDD_Model_tbl>();
            try
            {
                
                SqlParameter[] sqlpar = { new SqlParameter("@p_UserName", username) };
                DataSet dsModules = Com.ExecuteDataSet("RDD_Dashboard_GetReceivableAgeing", CommandType.StoredProcedure, sqlpar);
                if (dsModules.Tables.Count > 0)
                {
                    DataTable dtModule = dsModules.Tables[0];
                    DataRowCollection drc = dtModule.Rows;
                    foreach (DataRow dr in drc)
                    {
                        _CardDash.Add(new RDD_Model_tbl()
                        {
                           Country = !string.IsNullOrWhiteSpace(dr["Area"].ToString()) ?dr["Area"].ToString() : "",
                           days_0_30 = !string.IsNullOrWhiteSpace(dr["days_0_30"].ToString()) ? Convert.ToDecimal(dr["days_0_30"].ToString()) : 0,
                           days_121_150 = !string.IsNullOrWhiteSpace(dr["days_121_150"].ToString()) ? Convert.ToDecimal(dr["days_121_150"].ToString()) : 0,
                           days_151_180 = !string.IsNullOrWhiteSpace(dr["days_151_180"].ToString()) ? Convert.ToDecimal(dr["days_151_180"].ToString()) : 0,
                           days_181plus = !string.IsNullOrWhiteSpace(dr["days_181plus"].ToString()) ? Convert.ToDecimal(dr["days_181plus"].ToString()) : 0,
                           days_31_37 = !string.IsNullOrWhiteSpace(dr["days_31_37"].ToString()) ? Convert.ToDecimal(dr["days_31_37"].ToString()) : 0,
                           days_38_45 = !string.IsNullOrWhiteSpace(dr["days_38_45"].ToString()) ? Convert.ToDecimal(dr["days_38_45"].ToString()) : 0,
                           days_46_60 = !string.IsNullOrWhiteSpace(dr["days_46_60"].ToString()) ? Convert.ToDecimal(dr["days_46_60"].ToString()) : 0,
                           days_61_90 = !string.IsNullOrWhiteSpace(dr["days_61_90"].ToString()) ? Convert.ToDecimal(dr["days_61_90"].ToString()) : 0,
                           days_91_120 = !string.IsNullOrWhiteSpace(dr["days_91_120"].ToString()) ? Convert.ToDecimal(dr["days_91_120"].ToString()) : 0,

                        });
                    }

                }
            }
            catch (Exception ex)
            {
                _CardDash.Add(new RDD_Model_tbl()
                {
                    Country = "",
                    days_0_30 =  0,
                    days_121_150 =  0,
                    days_151_180 = 0,
                    days_181plus =  0,
                    days_31_37 = 0,
                    days_38_45 =  0,
                    days_46_60 =  0,
                    days_61_90 = 0,
                    days_91_120 =0,

                });
            }
            return _CardDash;
        }

        public List<RDD_Model_tbl> GetPayModel_tbl(string username)
        {

            List<RDD_Model_tbl> _CardDash = new List<RDD_Model_tbl>();
            try
            {

                SqlParameter[] sqlpar = { new SqlParameter("@p_UserName", username) };
                DataSet dsModules = Com.ExecuteDataSet("RDD_Dashboard_GetPayableAgeing", CommandType.StoredProcedure, sqlpar);
                if (dsModules.Tables.Count > 0)
                {
                    DataTable dtModule = dsModules.Tables[0];
                    DataRowCollection drc = dtModule.Rows;
                    foreach (DataRow dr in drc)
                    {
                        _CardDash.Add(new RDD_Model_tbl()
                        {
                            Country = !string.IsNullOrWhiteSpace(dr["DBName"].ToString()) ? dr["DBName"].ToString() : "",
                            days_0_30 = !string.IsNullOrWhiteSpace(dr["days_0_30"].ToString()) ? Convert.ToDecimal(dr["days_0_30"].ToString()) : 0,
                            days_121_150 = !string.IsNullOrWhiteSpace(dr["days_121_150"].ToString()) ? Convert.ToDecimal(dr["days_121_150"].ToString()) : 0,
                            days_151_180 = !string.IsNullOrWhiteSpace(dr["days_151_180"].ToString()) ? Convert.ToDecimal(dr["days_151_180"].ToString()) : 0,
                            days_181plus = !string.IsNullOrWhiteSpace(dr["days_181plus"].ToString()) ? Convert.ToDecimal(dr["days_181plus"].ToString()) : 0,
                            days_31_37 = !string.IsNullOrWhiteSpace(dr["days_31_37"].ToString()) ? Convert.ToDecimal(dr["days_31_37"].ToString()) : 0,
                            days_38_45 = !string.IsNullOrWhiteSpace(dr["days_38_45"].ToString()) ? Convert.ToDecimal(dr["days_38_45"].ToString()) : 0,
                            days_46_60 = !string.IsNullOrWhiteSpace(dr["days_46_60"].ToString()) ? Convert.ToDecimal(dr["days_46_60"].ToString()) : 0,
                            days_61_90 = !string.IsNullOrWhiteSpace(dr["days_61_90"].ToString()) ? Convert.ToDecimal(dr["days_61_90"].ToString()) : 0,
                            days_91_120 = !string.IsNullOrWhiteSpace(dr["days_91_120"].ToString()) ? Convert.ToDecimal(dr["days_91_120"].ToString()) : 0,

                        });
                    }

                }
            }
            catch (Exception ex)
            {
                _CardDash.Add(new RDD_Model_tbl()
                {
                    Country = "",
                    days_0_30 = 0,
                    days_121_150 = 0,
                    days_151_180 = 0,
                    days_181plus = 0,
                    days_31_37 = 0,
                    days_38_45 = 0,
                    days_46_60 = 0,
                    days_61_90 = 0,
                    days_91_120 = 0,

                });
            }
            return _CardDash;
        }


        public DataSet Get_Dashboard_Notification(string UserName)
        {
            DataSet ds = null;
            try
            {
                
                SqlParameter[] ParaDet1 = {new SqlParameter("@p_userName",UserName)};

                ds = Com.ExecuteDataSet("Get_RDD_Dashboard_Notification", CommandType.StoredProcedure, ParaDet1);

            }
            catch (Exception)
            {

                ds = null;
            }
            return ds;
        }
        public List<Pichart_Dash> GetBankBalPichart(string username)
        {
            List<Pichart_Dash> _DtDash = new List<Pichart_Dash>();
            try
            {
                SqlParameter[] parm = { };
                SqlParameter[] sqlpar = { new SqlParameter("@p_UserName", username),
                 new SqlParameter("@p_Type", "BalanceByDB")};
                DataSet dsModules = Com.ExecuteDataSet("RDD_Dashboard_GetBankBalances", CommandType.StoredProcedure, sqlpar);
                if (dsModules.Tables.Count > 0)
                {
                    DataTable dtModule = dsModules.Tables[0];

                    dtModule.Columns[0].ColumnName = "lblname";
                    dtModule.Columns[1].ColumnName = "Points1";
                    dtModule.Columns[2].ColumnName = "Bgcolor";


                    DataRowCollection drc = dtModule.Rows;
                    foreach (DataRow dr in drc)
                    {
                        _DtDash.Add(new Pichart_Dash()
                        {
                            lblname = !string.IsNullOrWhiteSpace(dr["lblname"].ToString()) ? dr["lblname"].ToString() : "",
                            points = !string.IsNullOrWhiteSpace(dr["Points1"].ToString()) ? Convert.ToDecimal(dr["Points1"].ToString()) : 0,
                            bgcolrs = !string.IsNullOrWhiteSpace(dr["Bgcolor"].ToString()) ? dr["Bgcolor"].ToString() : "",


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



        public int CheckAuthorization(string username,string url)
        {
            int i;
            try
            {
                SqlParameter[] sqlpar = { new SqlParameter("@p_UserName", username),
                 new SqlParameter("@p_url", url)};
                i = Com.ExecuteScalar("RDD_URLBase_Authorize", sqlpar);
            }
            catch (Exception)
            {

                i = 1;
            }
            
            return i;
        }
    }
}
