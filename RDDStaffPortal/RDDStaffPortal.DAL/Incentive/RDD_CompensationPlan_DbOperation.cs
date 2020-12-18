using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using RDDStaffPortal.DAL.DataModels.Incentive;
using System.Data;
using System.Data.SqlClient;
using System.Configuration;
using static RDDStaffPortal.DAL.CommonFunction;
using RDDStaffPortal.DAL.Incentive;
using System.Transactions;
using System.Web.Mvc;

namespace RDDStaffPortal.DAL.Incentive
{
    public class RDD_CompensationPlan_DbOperation
    {
        CommonFunction cf = new CommonFunction();
        public DataSet FillDropdown()
        {
            DataSet ds = new DataSet();
            SqlParameter[] parm = { };
            ds = cf.ExecuteDataSet("RDD_GetDesignationAndYear", CommandType.StoredProcedure, parm);
            return ds;
        }
        public RDD_CompensationPlan GetDropList(string username, string Eflag)
        {
            RDD_CompensationPlan RDD_CompPlan = new RDD_CompensationPlan();
            List<SelectListItem> DesignationList = new List<SelectListItem>();
            List<SelectListItem> YearsList = new List<SelectListItem>();
            DesignationList.Add(new SelectListItem()
            {
                Text = "--Select--",
                Value = "0",
            });
            YearsList.Add(new SelectListItem()
            {
                Text = "--Select--",
                Value = "0",
            });
            try
            {
                SqlParameter[] parm = { new SqlParameter("@p_username", username) };

                DataSet dsModules = cf.ExecuteDataSet("RDD_GetDesignationAndYear", CommandType.StoredProcedure, parm);
                if (dsModules.Tables.Count > 0)
                {
                    DataTable dtModule;
                    DataRowCollection drc;
                    dtModule = dsModules.Tables[0];
                    try
                    {
                        drc = dtModule.Rows;
                        foreach (DataRow dr in drc)
                        {
                            DesignationList.Add(new SelectListItem()
                            {
                                Text = !string.IsNullOrWhiteSpace(dr["DesigName"].ToString()) ? dr["DesigName"].ToString() : "",
                                Value = !string.IsNullOrWhiteSpace(dr["DesigId"].ToString()) ? dr["DesigId"].ToString() : "",
                            });

                        }
                    }
                    catch (Exception)
                    {
                        DesignationList.Add(new SelectListItem()
                        {
                            Text = "Error",
                            Value = "-1",
                        });

                    }
                    try
                    {
                        dtModule = dsModules.Tables[1];
                        drc = dtModule.Rows;
                        foreach (DataRow dr in drc)
                        {
                            YearsList.Add(new SelectListItem()
                            {
                                Text = !string.IsNullOrWhiteSpace(dr["Years"].ToString()) ? dr["Years"].ToString() : "",
                                Value = !string.IsNullOrWhiteSpace(dr["Years"].ToString()) ? dr["Years"].ToString() : "",
                            });
                        }
                    }
                    catch (Exception)
                    {
                        YearsList.Add(new SelectListItem()
                        {
                            Text = "Error",
                            Value = "-1",
                        });
                    }
                }
            }
            catch (Exception)
            {
                DesignationList.Add(new SelectListItem()
                {
                    Text = "Error",
                    Value = "-1",
                });
                YearsList.Add(new SelectListItem()
                {
                    Text = "Error",
                    Value = "-1",
                });
            }
            RDD_CompPlan.DesignationNameList = DesignationList;
            RDD_CompPlan.YearList = YearsList;
            return RDD_CompPlan;
        }
        //public RDD_IncentiveKPI GetData(string UserName, int KPI_Id, RDD_IncentiveKPI RDD_Incentive)
        //{
        //    try
        //    {
        //        SqlParameter[] Para = {
        //            new SqlParameter("@KpiIde",KPI_Id),
        //            new SqlParameter("@p_UserName",UserName),
        //            new SqlParameter("@p_type","Single"),
        //        };
        //        DataSet dsModules = cf.ExecuteDataSet("RDD_IncentievKPI_GetData", CommandType.StoredProcedure, Para);
        //        if (dsModules.Tables.Count > 0)
        //        {
        //            DataTable dtModule = dsModules.Tables[0];
        //            DataRowCollection drc = dtModule.Rows;
        //            foreach (DataRow dr in drc)
        //            {
        //                RDD_Incentive.KPI_Id = !string.IsNullOrWhiteSpace(dr["KPI_Id"].ToString()) ? Convert.ToInt32(dr["KPI_Id"].ToString()) : 0;
        //                RDD_Incentive.DesigId = !string.IsNullOrWhiteSpace(dr["DesigId"].ToString()) ? Convert.ToInt32(dr["DesigId"].ToString()) : 0;
        //                RDD_Incentive.DesigName = !string.IsNullOrWhiteSpace(dr["DesigName"].ToString()) ? dr["DesigName"].ToString() : "";
        //                RDD_Incentive.KPIname = !string.IsNullOrWhiteSpace(dr["KPIname"].ToString()) ? dr["KPIname"].ToString() : "";
        //                RDD_Incentive.Retain_Percentage = !string.IsNullOrWhiteSpace(dr["Retain_Percentage"].ToString()) ? dr["Retain_Percentage"].ToString() : "";
        //                RDD_Incentive.Period = !string.IsNullOrWhiteSpace(dr["Period"].ToString()) ? dr["Period"].ToString() : "";
        //                RDD_Incentive.Years = !string.IsNullOrWhiteSpace(dr["Years"].ToString()) ? dr["Years"].ToString() : "";
        //                RDD_Incentive.TermsAndCondition = !string.IsNullOrWhiteSpace(dr["TermsAndCondition"].ToString()) ? dr["TermsAndCondition"].ToString() : "";
        //                RDD_Incentive.CreatedOn = !string.IsNullOrWhiteSpace(dr["CreatedOn"].ToString()) ? Convert.ToDateTime(dr["CreatedOn"].ToString()) : System.DateTime.Now;
        //                RDD_Incentive.CreatedBy = !string.IsNullOrWhiteSpace(dr["CreatedBy"].ToString()) ? dr["CreatedBy"].ToString() : "";
        //                RDD_Incentive.LastUpdatedOn = !string.IsNullOrWhiteSpace(dr["LastUpdatedOn"].ToString()) ? Convert.ToDateTime(dr["LastUpdatedOn"].ToString()) : System.DateTime.Now;
        //                RDD_Incentive.LastUpdatedBy = !string.IsNullOrWhiteSpace(dr["LastUpdatedBy"].ToString()) ? dr["LastUpdatedBy"].ToString() : "";
        //            }

        //            DataTable dtModule1 = dsModules.Tables[1];
        //            DataRowCollection drc1 = dtModule1.Rows;
        //            List<RDD_IncentiveKPI_Parameter> RDDKpiParam = new List<RDD_IncentiveKPI_Parameter>();
        //            foreach (DataRow dr in drc1)
        //            {
        //                RDDKpiParam.Add(new RDD_IncentiveKPI_Parameter
        //                {
        //                    KPI_Parameter_Id = !string.IsNullOrWhiteSpace(dr["KPI_Parameter_Id"].ToString()) ? Convert.ToInt32(dr["KPI_Parameter_Id"].ToString()) : 0,
        //                    KPI_Id = !string.IsNullOrWhiteSpace(dr["KPI_Id"].ToString()) ? Convert.ToInt32(dr["KPI_Id"].ToString()) : 0,
        //                    KPI_Parameter = !string.IsNullOrWhiteSpace(dr["KPI_Parameter"].ToString()) ? dr["KPI_Parameter"].ToString() : "",
        //                    KPIType = !string.IsNullOrWhiteSpace(dr["KPIType"].ToString()) ? dr["KPIType"].ToString() : "",
        //                    Split_Percentage = !string.IsNullOrWhiteSpace(dr["Split_Percentage"].ToString()) ? Convert.ToInt32(dr["Split_Percentage"].ToString()) : 0,

        //                });
        //            }
        //            RDD_Incentive.RDD_IncentiveKPI_ParameterList = RDDKpiParam;

        //        }
        //    }
        //    catch (Exception ex)
        //    {
        //        RDD_Incentive.KPI_Id = 0;
        //        RDD_Incentive.DesigId = 0;
        //        RDD_Incentive.DesigName = "";
        //        RDD_Incentive.KPIname = "";
        //        RDD_Incentive.Retain_Percentage = "";
        //        RDD_Incentive.Period = "";
        //        RDD_Incentive.Years = "";
        //        RDD_Incentive.TermsAndCondition = "";
        //        RDD_Incentive.CreatedOn = System.DateTime.Now;
        //        RDD_Incentive.CreatedBy = "";
        //        RDD_Incentive.LastUpdatedOn = System.DateTime.Now;
        //        RDD_Incentive.LastUpdatedBy = "";
        //        RDD_Incentive.RDD_IncentiveKPI_ParameterList = null;
        //    }
        //    return RDD_Incentive;
        //}
    }
}
