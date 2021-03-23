using RDDStaffPortal.DAL.DataModels;
using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Web.Mvc;


namespace RDDStaffPortal.DAL.InitialSetup
{
    public class RDD_FunnelDataDbOperation
    {
        CommonFunction Com = new CommonFunction();
        // string username = User.Identity.Name;

            
        public List<Pichart_Funnel> GetPieChartData(string username, DateTime p_FromDate, DateTime p_ToDate)
        {
            List<Pichart_Funnel> _DtFunnel = new List<Pichart_Funnel>();
            try
            {
                SqlParameter[] parm = { };
                SqlParameter[] sqlpar = { new SqlParameter("@p_LoggedInUserName", username),
                 new SqlParameter("@p_FromDate", p_FromDate),
                    new SqlParameter("@p_ToDate", p_ToDate)};
                DataSet dsModules = Com.ExecuteDataSet("RDD_GetFunnelStatistics", CommandType.StoredProcedure, sqlpar);
                if (dsModules.Tables.Count > 0)
                {
                   DataTable dtModule = dsModules.Tables[0];

                 


                    DataRowCollection drc = dtModule.Rows;
                    foreach (DataRow dr in drc)
                    {
                        _DtFunnel.Add(new Pichart_Funnel()
                        {
                            status = !string.IsNullOrWhiteSpace(dr["Status"].ToString()) ? dr["Status"].ToString() : "",
                            percentage = !string.IsNullOrWhiteSpace(dr["Percentage"].ToString()) ? Convert.ToDecimal(dr["Percentage"].ToString()) : 0,
                            color = !string.IsNullOrWhiteSpace(dr["Color"].ToString()) ? dr["Color"].ToString() : "",
                            TotalAmt = !string.IsNullOrWhiteSpace(dr["TotalAmount"].ToString()) ? Convert.ToDecimal(dr["TotalAmount"].ToString()) : 0,

                        });
                    }

                }
            }
            catch (Exception ex)
            {

                _DtFunnel = null;
            }
            return _DtFunnel;
        }


        public RDD_Funnel_Chart GetChartDetails(string username, DateTime p_FromDate, DateTime p_ToDate)
        {
            RDD_Funnel_Chart _Funnel_Chart = new RDD_Funnel_Chart();
            List<Linechart_Funnel> _Line_Funnel_Chart = new List<Linechart_Funnel>();
            List<Pichart_Funnel> _Pi_Funnel_Char = new List<Pichart_Funnel>();
            try
            {
                SqlParameter[] sqlpar = { new SqlParameter("@p_LoggedInUserName", username),
                    new SqlParameter("@p_FromDate", p_FromDate),
                    new SqlParameter("@p_ToDate", p_ToDate)
                };
                DataSet dsModules = Com.ExecuteDataSet("RDD_GetFunnelStatistics", CommandType.StoredProcedure, sqlpar);
               
                if (dsModules.Tables.Count > 0)
                {
                    DataTable dtModule = dsModules.Tables[0];
                    DataRowCollection drc = dtModule.Rows;
                    foreach (DataRow dr in drc)
                    {
                        _Pi_Funnel_Char.Add(new Pichart_Funnel()
                        {
                            status = !string.IsNullOrWhiteSpace(dr["Status"].ToString()) ? dr["Status"].ToString() : "",
                            percentage = !string.IsNullOrWhiteSpace(dr["Percentage"].ToString()) ? Convert.ToDecimal(dr["Percentage"].ToString()) : 0,
                            color = !string.IsNullOrWhiteSpace(dr["Color"].ToString()) ? dr["Color"].ToString() : "",
                            TotalAmt = !string.IsNullOrWhiteSpace(dr["TotalAmount"].ToString()) ? Convert.ToDecimal(dr["TotalAmount"].ToString()) : 0,
                        });
                    }
                    dtModule = dsModules.Tables[1];
                    drc = dtModule.Rows;
                    foreach (DataRow dr in drc)
                    {
                        _Line_Funnel_Chart.Add(new Linechart_Funnel()
                        {
                            status = !string.IsNullOrWhiteSpace(dr["Status"].ToString()) ? dr["Status"].ToString() : "",
                            color = !string.IsNullOrWhiteSpace(dr["Color"].ToString()) ? dr["Color"].ToString() : "",
                            Amount = !string.IsNullOrWhiteSpace(dr["TotalAmount"].ToString()) ? Convert.ToDecimal(dr["TotalAmount"].ToString()) : 0,
                            weekno = !string.IsNullOrWhiteSpace(dr["WeekOfYr"].ToString()) ? Convert.ToInt32(dr["WeekOfYr"].ToString()) : 0
                        });
                    }                  
                }
            }
            catch (Exception)
            {
                _Pi_Funnel_Char.Add(new Pichart_Funnel()
                {
                    status =  "",
                    percentage =  0,
                    color =  "",
                    TotalAmt =  0,
                });
                _Line_Funnel_Chart.Add(new Linechart_Funnel()
                {
                    status = "",
                    Amount = 0,
                    color = "",
                    weekno = 0,
                });
            }
            _Funnel_Chart.GetLinecharts = _Line_Funnel_Chart;
            _Funnel_Chart.Get_Funnels = _Pi_Funnel_Char;
            return _Funnel_Chart;
        }
        public List<Linechart_Funnel> GetLineChartData(string username,DateTime p_FromDate,DateTime p_ToDate)
        {
            List<Linechart_Funnel> _DtFunnel = new List<Linechart_Funnel>();
            try
            {
                SqlParameter[] parm = { };
                SqlParameter[] sqlpar = { new SqlParameter("@p_LoggedInUserName", username),
                    new SqlParameter("@p_FromDate", p_FromDate),
                    new SqlParameter("@p_ToDate", p_ToDate)
                };
                DataSet dsModules = Com.ExecuteDataSet("RDD_GetFunnelStatistics", CommandType.StoredProcedure, sqlpar);
                if (dsModules.Tables.Count > 0)
                {
                    DataTable dtModule = dsModules.Tables[1];

                  


                    DataRowCollection drc = dtModule.Rows;
                    foreach (DataRow dr in drc)
                    {
                        _DtFunnel.Add(new Linechart_Funnel()
                        {
                            status = !string.IsNullOrWhiteSpace(dr["Status"].ToString()) ? dr["Status"].ToString() : "",
                            color = !string.IsNullOrWhiteSpace(dr["Color"].ToString()) ? dr["Color"].ToString() : "",
                            Amount = !string.IsNullOrWhiteSpace(dr["TotalAmount"].ToString()) ? Convert.ToDecimal(dr["TotalAmount"].ToString()) : 0,

                            weekno= !string.IsNullOrWhiteSpace(dr["WeekNo"].ToString()) ? Convert.ToInt32(dr["WeekNo"].ToString()) : 0
                            

                        });
                    }

                }
            }
            catch (Exception ex)
            {

                _DtFunnel = null;
            }
            return _DtFunnel;
        }
        public DataSet GetDrop1(string username)
        {
            DataSet dsModules;
            try
            {
                SqlParameter[] parm = {
                      new SqlParameter("@p_username",username),
                };
                dsModules = Com.ExecuteDataSet("RDD_Funnel_Drop", CommandType.StoredProcedure, parm);
            }
            catch (Exception)
            {
                dsModules = null;
                throw;
            }

            return dsModules;
        }
        public List<RDD_FunnelData> getFunnelData(int pagesize, int pageno,string username, string psearch,string country,string Bu,string QMonth,string QYear,string ClosMonth,string ClosYear,string status)
        {

            List<RDD_FunnelData> _FunneldatList = new List<RDD_FunnelData>();
            try
            {
                SqlParameter[] parm = {
                        new SqlParameter("@p_PageNo", pageno),
                new SqlParameter("@p_PageSize",pagesize),
                 new SqlParameter("@p_username",username),
                 new SqlParameter("@p_Search",psearch),
                 new SqlParameter("@p_country",country),
                  new SqlParameter("@p_Bu",Bu),
                  new SqlParameter("@p_QMonth",QMonth),
                   new SqlParameter("@p_QYear",QYear),
                    new SqlParameter("@p_ClosMonth",ClosMonth),
                   new SqlParameter("@p_ClosYear",ClosYear),
                    new SqlParameter("@p_status",status),
                 new SqlParameter("@p_flag","I")
                };

                //  DataSet dsModules = Com.ExecuteDataSet("retrive_RDD_Customermapping", CommandType.StoredProcedure, parm);
                DataSet dsfuneldata = Com.ExecuteDataSet("RDD_FunnelGetData", CommandType.StoredProcedure, parm);
                // Db.constr = System.Configuration.ConfigurationManager.ConnectionStrings["tejSAP"].ConnectionString;
                //  DataSet dsfuneldata = Db.myGetDS("EXEC dbo.RDD_FunnelGetData");
                if (dsfuneldata.Tables.Count > 0)
                {
                    DataTable dtModule = dsfuneldata.Tables[0];
                    for (int i = 0; i < dsfuneldata.Tables[0].Rows.Count; i++)
                    {
                        // fid,bdm,quoteID,endUser,resellerCode,resellerName,country,BU,goodsDescr,quoteDate,expClosingDt,dealStatus,Cost,Landed,value
                        RDD_FunnelData Fudata = new RDD_FunnelData();
                         
                        if (dsfuneldata.Tables[0].Rows[i]["TotalCount"] != null && !DBNull.Value.Equals(dsfuneldata.Tables[0].Rows[i]["TotalCount"]))
                        {
                            Fudata.TotalCount = Convert.ToInt32(dsfuneldata.Tables[0].Rows[i]["TotalCount"].ToString());
                        }
                        if (dsfuneldata.Tables[0].Rows[i]["RowNum"] != null && !DBNull.Value.Equals(dsfuneldata.Tables[0].Rows[i]["RowNum"]))
                        {
                            Fudata.RowNum = Convert.ToInt32(dsfuneldata.Tables[0].Rows[i]["RowNum"].ToString());
                        }
                        if (dsfuneldata.Tables[0].Rows[i]["fid"] != null && !DBNull.Value.Equals(dsfuneldata.Tables[0].Rows[i]["fid"]))
                        {
                            Fudata.fid = Convert.ToInt32(dsfuneldata.Tables[0].Rows[i]["fid"].ToString());
                        }
                        if (dsfuneldata.Tables[0].Rows[i]["bdm"] != null && !DBNull.Value.Equals(dsfuneldata.Tables[0].Rows[i]["bdm"]))
                        {
                            Fudata.bdm = dsfuneldata.Tables[0].Rows[i]["bdm"].ToString();
                        }
                        if (dsfuneldata.Tables[0].Rows[i]["quoteID"] != null && !DBNull.Value.Equals(dsfuneldata.Tables[0].Rows[i]["quoteID"]))
                        {
                            Fudata.quoteID = dsfuneldata.Tables[0].Rows[i]["quoteID"].ToString();
                        }
                        if (dsfuneldata.Tables[0].Rows[i]["endUser"] != null && !DBNull.Value.Equals(dtModule.Rows[i]["endUser"]))
                        {
                            Fudata.enduser = dsfuneldata.Tables[0].Rows[i]["endUser"].ToString();
                        }
                        if (dsfuneldata.Tables[0].Rows[i]["resellerCode"] != null && !DBNull.Value.Equals(dsfuneldata.Tables[0].Rows[i]["resellerCode"]))
                        {
                            Fudata.CardCode = dsfuneldata.Tables[0].Rows[i]["resellerCode"].ToString();
                        }
                        if (dsfuneldata.Tables[0].Rows[i]["resellerName"] != null && !DBNull.Value.Equals(dsfuneldata.Tables[0].Rows[i]["resellerName"]))
                        {
                            Fudata.CardName = dsfuneldata.Tables[0].Rows[i]["resellerName"].ToString();
                        }
                        if (dsfuneldata.Tables[0].Rows[i]["country"] != null && !DBNull.Value.Equals(dsfuneldata.Tables[0].Rows[i]["country"]))
                        {
                            Fudata.Country = dsfuneldata.Tables[0].Rows[i]["country"].ToString();
                        }
                        if (dsfuneldata.Tables[0].Rows[i]["BU"] != null && !DBNull.Value.Equals(dsfuneldata.Tables[0].Rows[i]["BU"]))
                        {
                            Fudata.BUName = dsfuneldata.Tables[0].Rows[i]["BU"].ToString();
                        }
                        if (dsfuneldata.Tables[0].Rows[i]["goodsDescr"] != null && !DBNull.Value.Equals(dsfuneldata.Tables[0].Rows[i]["goodsDescr"]))
                        {
                            Fudata.goodsDescr = dsfuneldata.Tables[0].Rows[i]["goodsDescr"].ToString();
                        }
                        if (dsfuneldata.Tables[0].Rows[i]["quoteDate"] != null && !DBNull.Value.Equals(dsfuneldata.Tables[0].Rows[i]["quoteDate"]))
                        {
                            Fudata.quoteDate = Convert.ToDateTime(dsfuneldata.Tables[0].Rows[i]["quoteDate"]);
                           
                        }
                        
                        if (dsfuneldata.Tables[0].Rows[i]["expClosingDt"] != null && !DBNull.Value.Equals(dsfuneldata.Tables[0].Rows[i]["expClosingDt"]))
                        {
                            Fudata.expClosingDt = Convert.ToDateTime(dsfuneldata.Tables[0].Rows[i]["expClosingDt"]);
                        }
                        if (dsfuneldata.Tables[0].Rows[i]["dealStatus"] != null && !DBNull.Value.Equals(dsfuneldata.Tables[0].Rows[i]["dealStatus"]))
                        {
                            Fudata.DealStatus = dsfuneldata.Tables[0].Rows[i]["dealStatus"].ToString();
                        }
                        if (dsfuneldata.Tables[0].Rows[i]["Cost"] != null && !DBNull.Value.Equals(dsfuneldata.Tables[0].Rows[i]["Cost"]))
                        {
                            Fudata.Cost = Convert.ToInt32(dsfuneldata.Tables[0].Rows[i]["Cost"]);
                        }
                        if (dsfuneldata.Tables[0].Rows[i]["Landed"] != null && !DBNull.Value.Equals(dsfuneldata.Tables[0].Rows[i]["Landed"]))
                        {
                            Fudata.Landed = Convert.ToInt32(dsfuneldata.Tables[0].Rows[i]["Landed"]);
                        }
                        if (dsfuneldata.Tables[0].Rows[i]["value"] != null && !DBNull.Value.Equals(dsfuneldata.Tables[0].Rows[i]["value"]))
                        {
                            Fudata.value = Convert.ToInt32(dsfuneldata.Tables[0].Rows[i]["value"]);
                        }
                        _FunneldatList.Add(Fudata);
                    }

                }
            }
            catch (Exception ex)
            {
                _FunneldatList = null;
            }
            return _FunneldatList;
        }

        public DataTable Getdata1(string Username)
        {
            DataTable dt = null;
            try
            {
                SqlParameter[] parm = {
                new SqlParameter("@p_flag","II"),
                new SqlParameter("@p_username",Username)
                };

                DataSet dsModules = Com.ExecuteDataSet("RDD_FunnelGetData", CommandType.StoredProcedure, parm);
                dt = dsModules.Tables[0];
            }
            catch (Exception)
            {

                dt = null;
            }

            return dt;
        }

        public string Save(RDD_FunnelData FunnelDataa)
        {
            string response = string.Empty;
            try
            {
                using (var connection = new SqlConnection(Global.getConnectionStringByName("tejSAP")))
                {
                    if (connection.State == ConnectionState.Closed)
                    {
                        connection.Open();
                    }
                    SqlTransaction transaction;
                    using (transaction = connection.BeginTransaction())
                    {
                        try
                        {
                            SqlCommand cmd = new SqlCommand();

                            cmd.CommandType = CommandType.StoredProcedure;
                            cmd.CommandText = "RDD_FunnelData_InsertUpdate";
                            cmd.Connection = connection;
                            cmd.Transaction = transaction;

                            var chcouin = FunnelDataa.ChangeCount + 1;

                       
                            if (FunnelDataa.ChangeCount == 1)
                            {
                                cmd.Parameters.Add("@p_ChangeCount", SqlDbType.Int).Value = chcouin;
                            }
                            else if(FunnelDataa.ChangeCount == 2)
                            {
                                cmd.Parameters.Add("@p_ChangeCount", SqlDbType.Int).Value = chcouin;
                            }
                            else
                            {

                                cmd.Parameters.Add("@p_ChangeCount", SqlDbType.Int).Value = FunnelDataa.ChangeCount;
                            }
                           
                           

                            cmd.Parameters.Add("@p_fid", SqlDbType.VarChar, 255).Value = FunnelDataa.fid;
                            cmd.Parameters.Add("@p_country", SqlDbType.VarChar, 255).Value = FunnelDataa.Country;
                            cmd.Parameters.Add("@p_BU ", SqlDbType.VarChar, 255).Value = FunnelDataa.BUName;
                            cmd.Parameters.Add("@p_bdm", SqlDbType.VarChar, 255).Value = FunnelDataa.bdm;

                            cmd.Parameters.Add("@p_quoteID", SqlDbType.VarChar, 255).Value = FunnelDataa.quoteID;
                            cmd.Parameters.Add("@p_endUser", SqlDbType.VarChar, 255).Value = FunnelDataa.enduser;
                            cmd.Parameters.Add("@p_resellerCode", SqlDbType.VarChar, 250).Value = FunnelDataa.CardCode;
                            cmd.Parameters.Add("@p_resellerName", SqlDbType.VarChar, 255).Value = FunnelDataa.CardName;

                            cmd.Parameters.Add("@p_goodsDescr", SqlDbType.VarChar, 1024).Value = FunnelDataa.goodsDescr;
                            cmd.Parameters.Add("@p_remarks", SqlDbType.VarChar, 255).Value = FunnelDataa.remarks;
                            if (FunnelDataa.Remarks2 == null)
                            {
                                cmd.Parameters.Add("@p_Remarks2", SqlDbType.VarChar, 1020).Value = DBNull.Value;

                            }
                            else
                            { cmd.Parameters.Add("@p_Remarks2", SqlDbType.VarChar, 1020).Value = FunnelDataa.Remarks2; 
                            
                            }
                            if (FunnelDataa.Remarks3 == null)
                            {
                                cmd.Parameters.Add("@p_Remarks3", SqlDbType.VarChar, 1020).Value = DBNull.Value;

                            }
                            else
                            {
                                cmd.Parameters.Add("@p_Remarks3", SqlDbType.VarChar, 1020).Value = FunnelDataa.Remarks3;

                            }
                           
                            cmd.Parameters.Add("@p_CreatedBy", SqlDbType.VarChar, 255).Value = FunnelDataa.CreatedBy;

                            cmd.Parameters.Add("@p_CreatedOn", SqlDbType.Date).Value = FunnelDataa.Createddte;
                            if (FunnelDataa.Updateddte == DateTime.MinValue)
                            {
                                cmd.Parameters.Add("@p_LastUpdatedOn", SqlDbType.Date).Value = DBNull.Value;
                            }
                            else
                            {
                                cmd.Parameters.Add("@p_LastUpdatedOn", SqlDbType.Date).Value = FunnelDataa.Updateddte;
                            }
                            if (FunnelDataa.fid == 0)
                            {
                                cmd.Parameters.Add("@p_LastUpdatedBy", SqlDbType.VarChar, 255).Value = DBNull.Value;
                            }
                            else {
                                cmd.Parameters.Add("@p_LastUpdatedBy", SqlDbType.VarChar, 255).Value = FunnelDataa.CreatedBy;
                            }
                            if (FunnelDataa.NextReminderDt == DateTime.MinValue)//FunnelDataa.NextReminderDt.Year < (DateTime.Now.Year - 1)
                            {
                                cmd.Parameters.Add("@p_NextReminderDt", SqlDbType.Date).Value = DBNull.Value;
                            }
                            else
                            {
                                cmd.Parameters.Add("@p_NextReminderDt", SqlDbType.Date).Value = FunnelDataa.NextReminderDt;
                            }

                            cmd.Parameters.Add("@p_quoteDate", SqlDbType.Date).Value = FunnelDataa.quoteDate;
                            cmd.Parameters.Add("@p_quoteMonth", SqlDbType.Int).Value = FunnelDataa.quoteDate.Month;
                            cmd.Parameters.Add("@p_quoteYear", SqlDbType.Int).Value = FunnelDataa.quoteDate.Year;

                            cmd.Parameters.Add("@p_quoteMonthMMM", SqlDbType.VarChar, 255).Value = FunnelDataa.quoteDate.ToString("MMM");

                           
                            cmd.Parameters.Add("@p_Cost", SqlDbType.Int).Value = FunnelDataa.Cost;
                            cmd.Parameters.Add("@p_Landed", SqlDbType.Int).Value = FunnelDataa.Landed;


                            cmd.Parameters.Add("@p_MarginUSD", SqlDbType.Int).Value = FunnelDataa.MarginUSD;
                            cmd.Parameters.Add("@p_value", SqlDbType.Int).Value = FunnelDataa.value;

                            cmd.Parameters.Add("@p_dealStatus", SqlDbType.VarChar, 100).Value = FunnelDataa.DealStatus;

                            cmd.Parameters.Add("@p_expClosingDt", SqlDbType.Date).Value = FunnelDataa.expClosingDt;
                            cmd.Parameters.Add("@p_expclosingMonth", SqlDbType.Int).Value = FunnelDataa.expClosingDt.Month;
                            cmd.Parameters.Add("@p_expclosingYear", SqlDbType.Int).Value = FunnelDataa.expClosingDt.Year;

                            cmd.Parameters.Add("@p_expclosingMonthMMM", SqlDbType.VarChar, 255).Value = FunnelDataa.expClosingDt.ToString("MMM");

                            if (FunnelDataa.orderBookedDate == DateTime.MinValue)
                            {
                                cmd.Parameters.Add("@p_orderBookedDate", SqlDbType.Date).Value = DBNull.Value;
                            }
                            else
                            {
                                cmd.Parameters.Add("@p_orderBookedDate", SqlDbType.Date).Value = FunnelDataa.orderBookedDate;
                            }
                            if (FunnelDataa.InvoiceDt == DateTime.MinValue)
                            {
                                cmd.Parameters.Add("@p_InvoiceDt", SqlDbType.Date).Value = DBNull.Value;
                            }
                            else
                            {
                                cmd.Parameters.Add("@p_InvoiceDt", SqlDbType.Date).Value = FunnelDataa.InvoiceDt;
                            }







                            cmd.Parameters.Add("@p_Response", SqlDbType.NVarChar, 1000).Direction = ParameterDirection.Output;
                            cmd.Parameters.Add("@p_EmployeeIdOUT", SqlDbType.Int).Direction = ParameterDirection.Output;
                            cmd.ExecuteNonQuery();
                            response = cmd.Parameters["@p_Response"].Value.ToString();
                            cmd.Dispose();
                            transaction.Commit();

                        }

                        catch (Exception ex)
                        {
                            response = "Error occured : " + ex.Message;
                            transaction.Rollback();
                        }
                        finally
                        {
                            if (connection.State == ConnectionState.Open)
                            {
                                connection.Close();
                            }
                        }
                    }
                }
            }
            catch (Exception ex)
            {
                response = "Error occured : " + ex.Message;
            }
            return response;

        }

        public string Savereseller(RDD_FunnelData ResellerDataa,string createdby)
        {
            string response = string.Empty;
            try
            {
                using (var connection = new SqlConnection(Global.getConnectionStringByName("tejSAP")))
                {
                    if (connection.State == ConnectionState.Closed)
                    {
                        connection.Open();
                    }
                    SqlTransaction transaction;
                    using (transaction = connection.BeginTransaction())
                    {
                        try
                        {
                            SqlCommand cmd = new SqlCommand();

                            cmd.CommandType = CommandType.StoredProcedure;
                            cmd.CommandText = "RDD_FunnelNewResellerName";
                            cmd.Connection = connection;
                            cmd.Transaction = transaction;

                            
                           
                            cmd.Parameters.Add("@p_country", SqlDbType.VarChar, 100).Value = ResellerDataa.Country;
                            cmd.Parameters.Add("@p_resellername", SqlDbType.VarChar, 100).Value = ResellerDataa.resellername;

                            cmd.Parameters.Add("@p_CreatedBy", SqlDbType.VarChar, 255).Value = createdby ;

                            cmd.Parameters.Add("@p_Response", SqlDbType.NVarChar, 1000).Direction = ParameterDirection.Output;
                            cmd.Parameters.Add("@p_FunnelIdOUT", SqlDbType.Int).Direction = ParameterDirection.Output;
                            cmd.ExecuteNonQuery();
                            response = cmd.Parameters["@p_Response"].Value.ToString();
                            cmd.Dispose();
                            transaction.Commit();

                        }

                        catch (Exception ex)
                        {
                            response = "Error occured : " + ex.Message;
                            transaction.Rollback();
                        }
                        finally
                        {
                            if (connection.State == ConnectionState.Open)
                            {
                                connection.Close();
                            }
                        }
                    }
                }
            }
            catch (Exception ex)
            {
                response = "Error occured : " + ex.Message;
            }
            return response;


        }
        

        public List<SelectListItem> GetCountryList(string username)
        {

            List<SelectListItem> _FunnelList = new List<SelectListItem>();
            try
            {
                // SqlParameter[] parm = { };


                SqlParameter[] parm = { new SqlParameter("@p_Empname", username) };
                DataSet dsModules = Com.ExecuteDataSet("RDD_GetFunnelCountryList", CommandType.StoredProcedure, parm);

                if (dsModules.Tables.Count > 0)
                {
                    DataTable dtModule = dsModules.Tables[0];
                    DataRowCollection drc = dtModule.Rows;
                    foreach (DataRow dr in drc)
                    {
                        _FunnelList.Add(new SelectListItem()
                        {


                            Text = !string.IsNullOrWhiteSpace(dr["Country"].ToString()) ? dr["Country"].ToString() : "",
                            Value = !string.IsNullOrWhiteSpace(dr["Country"].ToString()) ? dr["Country"].ToString() : "",


                        });
                    }

                }

            }
            catch (Exception ex)
            {
                _FunnelList = null;
            }

            return _FunnelList;
        }


        public List<SelectListItem> GetStatusList()

        {

            List<SelectListItem> _BUList = new List<SelectListItem>();
            try
            {
                SqlParameter[] parm = { };

                // RDD_GetFunnelBUList country ,name

                DataSet dsModules = Com.ExecuteDataSet("RDD_GetFunnelStatus", CommandType.StoredProcedure, parm);

                if (dsModules.Tables.Count > 0)
                {
                    DataTable dtModule = dsModules.Tables[0];
                    DataRowCollection drc = dtModule.Rows;
                    foreach (DataRow dr in drc)
                    {
                        _BUList.Add(new SelectListItem()
                        {


                            Text = !string.IsNullOrWhiteSpace(dr["DealStatus"].ToString()) ? dr["DealStatus"].ToString() : "",
                            Value = !string.IsNullOrWhiteSpace(dr["DealStatus"].ToString()) ? dr["DealStatus"].ToString() : "",


                        });
                    }

                }

            }
            catch (Exception ex)
            {
                _BUList = null;
            }

            return _BUList;
        }

        public DataSet FillCustomer(string Country)
        {
            DataSet _CustomerList = null;
            try
            {
                SqlParameter[] parm = { new SqlParameter("@Country", Country) };
                _CustomerList = Com.ExecuteDataSet("getFunnelCustomerListNF", CommandType.StoredProcedure, parm);
               

            }
            catch (Exception ex)
            {
                _CustomerList = null;
            }

            return _CustomerList;
        }

        //public List<RDD_FunnelData> FillCustomer(string Country)
        //{
        //    List<RDD_FunnelData> _CustomerList = new List<RDD_FunnelData>();
        //    try
        //    {
        //        SqlParameter[] parm = { new SqlParameter("@Country", Country) };
        //        DataSet dsModules = Com.ExecuteDataSet("getFunnelCustomerListNF", CommandType.StoredProcedure, parm);
        //        if (dsModules.Tables.Count > 0)
        //        {
        //            DataTable dtModule = dsModules.Tables[0];
        //            DataRowCollection drc = dtModule.Rows;
        //            foreach (DataRow dr in drc)
        //            {
        //                _CustomerList.Add(new RDD_FunnelData()
        //                {
        //                    CardCode = !string.IsNullOrWhiteSpace(dr["CardCode"].ToString()) ? dr["CardCode"].ToString() : "",
        //                    CardName = !string.IsNullOrWhiteSpace(dr["CardName"].ToString()) ? dr["CardName"].ToString() : "",


        //                });
        //            }

        //        }

        //    }
        //    catch (Exception ex)
        //    {
        //        _CustomerList = null;
        //    }

        //    return _CustomerList;
        //}
        public DataSet FillBU(string Country, string username)
        {
            DataSet _BUList = null;
            try
            {
                SqlParameter[] Para = {


                new SqlParameter("@p_Countryname", Country),
                    new SqlParameter("@p_Empname", username),
                    };
                _BUList = Com.ExecuteDataSet("RDD_GetFunnelBUList", CommandType.StoredProcedure, Para);
               

            }
            catch (Exception ex)
            {
                _BUList = null;
            }

            return _BUList;
        }

        //public List<RDD_FunnelData> FillBU(string Country, string username)
        //{
        //    List<RDD_FunnelData> _BUList = new List<RDD_FunnelData>();
        //    try
        //    {
        //        SqlParameter[] Para = {


        //        new SqlParameter("@p_Countryname", Country),
        //            new SqlParameter("@p_Empname", username),
        //            };
        //        DataSet dsModules = Com.ExecuteDataSet("RDD_GetFunnelBUList", CommandType.StoredProcedure, Para);
        //        if (dsModules.Tables.Count > 0)
        //        {
        //            DataTable dtModule = dsModules.Tables[0];
        //            DataRowCollection drc = dtModule.Rows;
        //            foreach (DataRow dr in drc)
        //            {
        //                _BUList.Add(new RDD_FunnelData()
        //                {

        //                    BUName = !string.IsNullOrWhiteSpace(dr["ItmsGrpNam"].ToString()) ? dr["ItmsGrpNam"].ToString() : "",


        //                });
        //            }

        //        }

        //    }
        //    catch (Exception ex)
        //    {
        //        _BUList = null;
        //    }

        //    return _BUList;
        //}



        public string getQuoiteId(string Country)
        {


            string QuoteId = Com.ExecuteScalar1("select dbo.GetQuoteID('" + Country + "')");

            return QuoteId;
        }


        public RDD_FunnelData getdatabyId(int fid)
        {
            RDD_FunnelData _Fdata = new RDD_FunnelData();
            try
            {
                Db.constr = Global.getConnectionStringByName("tejSAP");
                SqlParameter[] parm = {
                        new SqlParameter("@p_fid", fid)};
                DataSet dsdata = Com.ExecuteDataSet("RDD_FunnelGetDataById", CommandType.StoredProcedure, parm);
                if (dsdata.Tables.Count > 0)
                {
                    DataTable dtDept = dsdata.Tables[0];
                    DataRowCollection drc;
                    drc = dtDept.Rows;
                    foreach (DataRow dr in drc)
                    {
                        _Fdata.ChangeCount = !string.IsNullOrWhiteSpace(dr["ChangeCount"].ToString()) ? Convert.ToInt32(dr["ChangeCount"].ToString()) : 0;
                        _Fdata.fid = !string.IsNullOrWhiteSpace(dr["fid"].ToString()) ? Convert.ToInt32(dr["fid"].ToString()) : 0;
                        _Fdata.bdm = !string.IsNullOrWhiteSpace(dr["bdm"].ToString()) ? dr["bdm"].ToString() : "";
                        _Fdata.quoteID = !string.IsNullOrWhiteSpace(dr["quoteID"].ToString()) ? dr["quoteID"].ToString() : "";
                        _Fdata.enduser = !string.IsNullOrWhiteSpace(dr["enduser"].ToString()) ? dr["enduser"].ToString() : "";
                        _Fdata.CardCode = !string.IsNullOrWhiteSpace(dr["resellerCode"].ToString()) ? dr["resellerCode"].ToString() : "";
                        _Fdata.CardName = !string.IsNullOrWhiteSpace(dr["resellerName"].ToString()) ? dr["resellerName"].ToString() : "";
                        _Fdata.Country = !string.IsNullOrWhiteSpace(dr["country"].ToString()) ? dr["country"].ToString() : "";
                        _Fdata.BUName = !string.IsNullOrWhiteSpace(dr["BU"].ToString()) ? dr["BU"].ToString() : "";
                        _Fdata.goodsDescr = !string.IsNullOrWhiteSpace(dr["goodsDescr"].ToString()) ? dr["goodsDescr"].ToString() : "";
                        _Fdata.quoteDate = !string.IsNullOrWhiteSpace(dr["quoteDate"].ToString()) ?Convert.ToDateTime(dr["quoteDate"].ToString()) : System.DateTime.Now;
                        _Fdata.expClosingDt = !string.IsNullOrWhiteSpace(dr["expClosingDt"].ToString()) ? Convert.ToDateTime(dr["expClosingDt"].ToString()) : System.DateTime.Now;
                        _Fdata.DealStatus = !string.IsNullOrWhiteSpace(dr["dealStatus"].ToString()) ? dr["dealStatus"].ToString() : "";
                        _Fdata.Cost = !string.IsNullOrWhiteSpace(dr["Cost"].ToString()) ? Convert.ToDecimal(dr["Cost"].ToString()) : 0;
                        _Fdata.Landed = !string.IsNullOrWhiteSpace(dr["Landed"].ToString()) ? Convert.ToDecimal(dr["Landed"].ToString()) : 0;
                        _Fdata.value = !string.IsNullOrWhiteSpace(dr["value"].ToString()) ? Convert.ToDecimal(dr["value"].ToString()) : 0;
                        _Fdata.remarks= !string.IsNullOrWhiteSpace(dr["remarks"].ToString()) ? dr["remarks"].ToString() : "";
                        _Fdata.Remarks2 = !string.IsNullOrWhiteSpace(dr["Remarks2"].ToString()) ? dr["Remarks2"].ToString() : "";
                        _Fdata.Remarks3 = !string.IsNullOrWhiteSpace(dr["Remarks3"].ToString()) ? dr["Remarks3"].ToString() : "";
                        _Fdata.orderBookedDate = !string.IsNullOrWhiteSpace(dr["orderBookedDate"].ToString()) ? (DateTime?)Convert.ToDateTime(dr["orderBookedDate"].ToString()): (DateTime?)null;
                        _Fdata.InvoiceDt = !string.IsNullOrWhiteSpace(dr["InvoiceDt"].ToString()) ? (DateTime?)Convert.ToDateTime(dr["InvoiceDt"].ToString()) : (DateTime?)null;
                        _Fdata.NextReminderDt = !string.IsNullOrWhiteSpace(dr["NextReminderDt"].ToString()) ? (DateTime?)Convert.ToDateTime(dr["NextReminderDt"].ToString()) : (DateTime?)null;
                        _Fdata.Createddte = !string.IsNullOrWhiteSpace(dr["CreatedOn"].ToString()) ? Convert.ToDateTime(dr["CreatedOn"].ToString()) : DateTime.Now;
                        _Fdata.MarginUSD = !string.IsNullOrWhiteSpace(dr["MarginUSD"].ToString()) ?Convert.ToDecimal(dr["MarginUSD"].ToString()) : 0;
                        _Fdata.UpdatedBy = !string.IsNullOrWhiteSpace(dr["LastUpdatedBy"].ToString()) ? dr["LastUpdatedBy"].ToString() :"";
                        _Fdata.Updateddte = !string.IsNullOrWhiteSpace(dr["LastUpdatedOn"].ToString()) ? (DateTime?)Convert.ToDateTime(dr["LastUpdatedOn"].ToString()) : (DateTime?)null;

                    }



                }

            }

            catch (Exception ex)
            {
                _Fdata = null;
            }
            return _Fdata;
        }



        public string Deletedata(int fid)
        {

            string response = string.Empty;
            try
            {
                using (var connection = new SqlConnection(Global.getConnectionStringByName("tejSAP")))
                {
                    if (connection.State == ConnectionState.Closed)
                    {
                        connection.Open();
                    }
                    SqlTransaction transaction;
                    using (transaction = connection.BeginTransaction())
                    {
                        try
                        {
                            SqlCommand cmd = new SqlCommand();
                            cmd.CommandType = CommandType.StoredProcedure;
                            cmd.CommandText = "RDD_FunnelDeleteData";
                            cmd.Connection = connection;
                            cmd.Transaction = transaction;

                            cmd.Parameters.Add("@p_fid", SqlDbType.Int).Value = Convert.ToInt16(fid);
                           // cmd.Parameters.Add("@p_Status", SqlDbType.NVarChar).Value = dealstatus;
                            cmd.Parameters.Add("@p_Response", SqlDbType.NVarChar, 1000).Direction = ParameterDirection.Output;
                            cmd.ExecuteNonQuery();
                            response = cmd.Parameters["@p_Response"].Value.ToString();
                            cmd.Dispose();
                            transaction.Commit();

                        }

                        catch (Exception ex)
                        {
                            response = "Error occured : " + ex.Message;
                            transaction.Rollback();
                        }
                        finally
                        {
                            if (connection.State == ConnectionState.Open)
                            {
                                connection.Close();
                            }
                        }
                    }
                }
            }

            catch (Exception ex)
            {
                response = "Error occured : " + ex.Message;
            }
            return response;

        }



    }
}

    

