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
    public class RDD_IncentiveKPI_DbOperation
    {        
        CommonFunction cf = new CommonFunction();    
        
        public DataSet FillDropdown()
        {
            DataSet ds = new DataSet();
            SqlParameter[] parm = { };            
            ds= cf.ExecuteDataSet("RDD_GetDesignationAndYear", CommandType.StoredProcedure, parm);
            return ds;
        }
        public RDD_IncentiveKPI GetDropList(string username, string Eflag)
        {
            RDD_IncentiveKPI RDD_Approval = new RDD_IncentiveKPI();
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
            RDD_Approval.DesignationNameList = DesignationList;
            RDD_Approval.YearList = YearsList;
            return RDD_Approval;
        }
        public List<RDD_IncentiveKPI> GetALLDATA(string UserName, int pagesize, int pageno, string psearch)
        {
            List<RDD_IncentiveKPI> _RDD_IncentiveKpi = new List<RDD_IncentiveKPI>();

            try
            {
                SqlParameter[] Para = {
                    new SqlParameter("@p_UserName",UserName),
                    new SqlParameter("@p_Search", psearch),
                    new SqlParameter("@p_PageNo", pageno),
                    new SqlParameter("@p_PageSize",pagesize),
                    new SqlParameter("@p_SortColumn", "KPI_Id"),
                    new SqlParameter("@p_SortOrder", "ASC"),
                    new SqlParameter("@p_type","GetAll")
                };
                DataSet dsModules = cf.ExecuteDataSet("RDD_IncentievKPI_GetData", CommandType.StoredProcedure, Para);
                if (dsModules.Tables.Count > 0)
                {
                    DataTable dtModule = dsModules.Tables[0];
                    DataRowCollection drc = dtModule.Rows;
                    foreach (DataRow dr in drc)
                    {
                        _RDD_IncentiveKpi.Add(new RDD_IncentiveKPI()
                        {
                            KPI_Id = !string.IsNullOrWhiteSpace(dr["KPI_Id"].ToString()) ? Convert.ToInt32(dr["KPI_Id"].ToString()) : 0,
                            DesigId= !string.IsNullOrWhiteSpace(dr["DesigId"].ToString()) ? Convert.ToInt32(dr["DesigId"].ToString()) : 0,
                            DesigName = !string.IsNullOrWhiteSpace(dr["DesigName"].ToString()) ? dr["DesigName"].ToString() : "",
                            KPIname = !string.IsNullOrWhiteSpace(dr["KPIname"].ToString()) ? dr["KPIname"].ToString() : "",
                            Retain_Percentage = !string.IsNullOrWhiteSpace(dr["Retain_Percentage"].ToString()) ? dr["Retain_Percentage"].ToString() : "",
                            Period = !string.IsNullOrWhiteSpace(dr["Period"].ToString()) ? dr["Period"].ToString() : "",
                            Years = !string.IsNullOrWhiteSpace(dr["Years"].ToString()) ? dr["Years"].ToString() : "",
                            TermsAndCondition = !string.IsNullOrWhiteSpace(dr["TermsAndCondition"].ToString()) ? dr["TermsAndCondition"].ToString() : "",                            
                            CreatedOn = !string.IsNullOrWhiteSpace(dr["CreatedOn"].ToString()) ? Convert.ToDateTime(dr["CreatedOn"].ToString()) : System.DateTime.Now,
                            CreatedBy = !string.IsNullOrWhiteSpace(dr["CreatedBy"].ToString()) ? dr["CreatedBy"].ToString() : "",
                            LastUpdatedOn = !string.IsNullOrWhiteSpace(dr["LastUpdatedOn"].ToString()) ? Convert.ToDateTime(dr["LastUpdatedOn"].ToString()) : System.DateTime.Now,
                            LastUpdatedBy = !string.IsNullOrWhiteSpace(dr["LastUpdatedBy"].ToString()) ? dr["LastUpdatedBy"].ToString() : "",
                            TotalCount = !string.IsNullOrWhiteSpace(dr["TotalCount"].ToString()) ? Convert.ToInt32(dr["TotalCount"].ToString()) : 0,
                            RowNum = !string.IsNullOrWhiteSpace(dr["RowNum"].ToString()) ? Convert.ToInt32(dr["RowNum"].ToString()) : 0

                        });
                    }
                }
            }
            catch (Exception)
            {
                _RDD_IncentiveKpi.Add(new RDD_IncentiveKPI()
                {
                    KPI_Id = 0,
                    DesigName = "",
                    Retain_Percentage = "",
                    Period = "",
                    Years = "",
                    TermsAndCondition = "",                    
                    CreatedOn = System.DateTime.Now,
                    CreatedBy = "",
                    LastUpdatedOn = System.DateTime.Now,
                    LastUpdatedBy = ""
                });
            }
            return _RDD_IncentiveKpi;
        }
        public List<Outcls1> Save1(RDD_IncentiveKPI RIKPI)
        {
            List<Outcls1> str = new List<Outcls1>();
            RIKPI.RType = "Insert";
            if (RIKPI.EditFlag == true)
            {
                RIKPI.RType = "Update";
            }
            try
            {
                using (TransactionScope scope = new TransactionScope())
                {
                    SqlParameter[] Para = {
                        new SqlParameter("@p_type",RIKPI.RType),
                        new SqlParameter("@KpiId",RIKPI.KPI_Id),
                        new SqlParameter("@Desigid",RIKPI.DesignationId),
                        new SqlParameter("@KpiNm",RIKPI.KPIname),
                        new SqlParameter("@Period",RIKPI.Period),
                        new SqlParameter("@Retainpercentg",RIKPI.Retain_Percentage),
                        new SqlParameter("@Years",RIKPI.Years),
                        new SqlParameter("@Totalpercentg",RIKPI.Total_Percentage),
                        new SqlParameter("@TnC",RIKPI.TermsAndCondition),                      
                        new SqlParameter("@Createdby",RIKPI.CreatedBy),
                        new SqlParameter("@Lastupdateby",RIKPI.LastUpdatedBy),
                        new SqlParameter("@Lastupdateon",RIKPI.LastUpdatedOn),                        
                        new SqlParameter("@p_id",RIKPI.id),
                        new SqlParameter("@p_response",RIKPI.ErrorMsg)
                };
                    str = cf.ExecuteNonQueryListID("RDD_IncentiveKPIparameter", Para);
                    if (str[0].Outtf == true)
                    {
                        int m = 0;
                        if (RIKPI.EditFlag == false)
                        {
                            RIKPI.Ptype = "I";
                        }
                        else
                        {
                            RIKPI.Ptype = "D";                          

                            SqlParameter[] ParaDet2 = { new SqlParameter("@KpiIde", str[0].Id),
                             new SqlParameter("@p_typ",RIKPI.Ptype)};
                            var det1 = cf.ExecuteNonQuery("RDD_IncentiveKPIparameterTrans", ParaDet2);

                            if (det1 == true)
                            {
                                RIKPI.Ptype = "U";
                            }
                            else
                            {
                                RIKPI.Ptype = "";
                            }
                        }
                        while (RIKPI.RDD_IncentiveKPI_ParameterList.Count > m)
                        {
                            SqlParameter[] ParaDet1 = {
                                new SqlParameter("@p_typ",RIKPI.Ptype),
                                new SqlParameter("@KpiIde",str[0].Id),
                                new SqlParameter("@KpiParamNm",RIKPI.RDD_IncentiveKPI_ParameterList[m].KPI_Parameter),
                                new SqlParameter("@Splitpercentg",RIKPI.RDD_IncentiveKPI_ParameterList[m].Split_Percentage),
                                new SqlParameter("@KpiType",RIKPI.RDD_IncentiveKPI_ParameterList[m].KPIType)
                            };
                            var det1 = cf.ExecuteNonQuery("RDD_IncentiveKPIparameterTrans", ParaDet1);
                            if (det1 == false)
                            {
                                str.Clear();
                                str.Add(new Outcls1
                                {
                                    Outtf = false,
                                    Id = -1,
                                    Responsemsg = "Error occured : KPI Parameter Details "
                                });
                                return str;
                            }
                            m++;
                        }
                    }
                    scope.Complete();
                }
            }
            catch (Exception ex)
            {
                str.Add(new Outcls1
                {
                    Outtf = false,
                    Id = -1,
                    Responsemsg = "Error occured : " + ex.Message
                });
            }
            return str;
        }
        public List<Outcls> SaveKPIparameter(string _KpiParam)
        {
            List<Outcls> Msg = new List<Outcls>();
            string Errormsg = "";
            try
            {
                SqlParameter[] prm =
                {
                    new SqlParameter("@Type","Save"),
                    new SqlParameter("@Kpiname",_KpiParam),
                    new SqlParameter("@p_response",Errormsg)
                };
                Msg = cf.ExecuteNonQueryList("RDD_SaveKPIparameter", prm);                
            }
            catch (Exception ex)
            {
                Msg.Add(new Outcls
                {
                    Outtf = false,                    
                    Responsemsg = "Error occured : " + ex.Message
                });
            }
            return Msg;
        }
        public DataSet GetKPIdetails()
        {
            string Msg = "";
            DataSet ds = new DataSet();
            SqlParameter[] prm =
            {
                new SqlParameter("@Type","GetKPIdetails"),
                new SqlParameter("@p_response",Msg)
            };
            ds = cf.ExecuteDataSet("RDD_SaveKPIparameter", CommandType.StoredProcedure, prm);
            return ds;
        }
        public List<Outcls> DeleteKPIparameter(string _KPIParamId)
        {
            List<Outcls> Msg = new List<Outcls>();
            string Errormsg = "";
            try
            {
                SqlParameter[] prm =
                {
                    new SqlParameter("@Type","DeleteParameter"),
                    new SqlParameter("@Kpiid",_KPIParamId),
                    new SqlParameter("@p_response",Errormsg)
                };
                Msg = cf.ExecuteNonQueryList("RDD_SaveKPIparameter", prm);
            }
            catch (Exception ex)
            {
                Msg.Add(new Outcls
                {
                    Outtf = false,                    
                    Responsemsg = "Error occured : " + ex.Message
                });
            }
            return Msg;
        }
        public RDD_IncentiveKPI GetData(string UserName, int KPI_Id, RDD_IncentiveKPI RDD_Incentive)
        {
            try
            {
                SqlParameter[] Para = {
                    new SqlParameter("@KpiIde",KPI_Id),
                    new SqlParameter("@p_UserName",UserName),
                    new SqlParameter("@p_type","Single"),
                };
                DataSet dsModules = cf.ExecuteDataSet("RDD_IncentievKPI_GetData", CommandType.StoredProcedure, Para);
                if (dsModules.Tables.Count > 0)
                {
                    DataTable dtModule = dsModules.Tables[0];
                    DataRowCollection drc = dtModule.Rows;
                    foreach (DataRow dr in drc)
                    {
                        RDD_Incentive.KPI_Id = !string.IsNullOrWhiteSpace(dr["KPI_Id"].ToString()) ? Convert.ToInt32(dr["KPI_Id"].ToString()) : 0;
                        RDD_Incentive.DesigId = !string.IsNullOrWhiteSpace(dr["DesigId"].ToString()) ? Convert.ToInt32(dr["DesigId"].ToString()) : 0;
                        RDD_Incentive.DesigName = !string.IsNullOrWhiteSpace(dr["DesigName"].ToString()) ? dr["DesigName"].ToString() : "";
                        RDD_Incentive.KPIname = !string.IsNullOrWhiteSpace(dr["KPIname"].ToString()) ? dr["KPIname"].ToString() : "";
                        RDD_Incentive.Retain_Percentage = !string.IsNullOrWhiteSpace(dr["Retain_Percentage"].ToString()) ? dr["Retain_Percentage"].ToString() : "";
                        RDD_Incentive.Period = !string.IsNullOrWhiteSpace(dr["Period"].ToString()) ? dr["Period"].ToString() : "";
                        RDD_Incentive.Years = !string.IsNullOrWhiteSpace(dr["Years"].ToString()) ? dr["Years"].ToString() : "";
                        RDD_Incentive.TermsAndCondition = !string.IsNullOrWhiteSpace(dr["TermsAndCondition"].ToString()) ? dr["TermsAndCondition"].ToString() : "";
                        RDD_Incentive.CreatedOn = !string.IsNullOrWhiteSpace(dr["CreatedOn"].ToString()) ? Convert.ToDateTime(dr["CreatedOn"].ToString()) : System.DateTime.Now;
                        RDD_Incentive.CreatedBy = !string.IsNullOrWhiteSpace(dr["CreatedBy"].ToString()) ? dr["CreatedBy"].ToString() : "";
                        RDD_Incentive.LastUpdatedOn = !string.IsNullOrWhiteSpace(dr["LastUpdatedOn"].ToString()) ? Convert.ToDateTime(dr["LastUpdatedOn"].ToString()) : System.DateTime.Now;
                        RDD_Incentive.LastUpdatedBy = !string.IsNullOrWhiteSpace(dr["LastUpdatedBy"].ToString()) ? dr["LastUpdatedBy"].ToString() : "";
                    }
                    
                    DataTable dtModule1 = dsModules.Tables[1];
                    DataRowCollection drc1 = dtModule1.Rows;
                    List<RDD_IncentiveKPI_Parameter> RDDKpiParam = new List<RDD_IncentiveKPI_Parameter>();
                    foreach (DataRow dr in drc1)
                    {
                        RDDKpiParam.Add(new RDD_IncentiveKPI_Parameter
                        {
                            KPI_Parameter_Id = !string.IsNullOrWhiteSpace(dr["KPI_Parameter_Id"].ToString()) ? Convert.ToInt32(dr["KPI_Parameter_Id"].ToString()) : 0,
                            KPI_Id = !string.IsNullOrWhiteSpace(dr["KPI_Id"].ToString()) ? Convert.ToInt32(dr["KPI_Id"].ToString()) : 0,
                            KPI_Parameter = !string.IsNullOrWhiteSpace(dr["KPI_Parameter"].ToString()) ? dr["KPI_Parameter"].ToString() : "",
                            KPIType = !string.IsNullOrWhiteSpace(dr["KPIType"].ToString()) ? dr["KPIType"].ToString() : "",
                            Split_Percentage = !string.IsNullOrWhiteSpace(dr["Split_Percentage"].ToString()) ? Convert.ToInt32(dr["Split_Percentage"].ToString()) : 0,
                            
                        });
                    }
                    RDD_Incentive.RDD_IncentiveKPI_ParameterList = RDDKpiParam;

                }
            }
            catch (Exception ex)
            {

                RDD_Incentive.KPI_Id = 0;
                RDD_Incentive.DesigId = 0;
                RDD_Incentive.DesigName = "";
                RDD_Incentive.KPIname = "";
                RDD_Incentive.Retain_Percentage = "";
                RDD_Incentive.Period = "";
                RDD_Incentive.Years = "";
                RDD_Incentive.TermsAndCondition = "";                
                RDD_Incentive.CreatedOn = System.DateTime.Now;
                RDD_Incentive.CreatedBy = "";
                RDD_Incentive.LastUpdatedOn = System.DateTime.Now;
                RDD_Incentive.LastUpdatedBy = "";
                RDD_Incentive.RDD_IncentiveKPI_ParameterList = null;                
            }
            return RDD_Incentive;
        }
    }
}
