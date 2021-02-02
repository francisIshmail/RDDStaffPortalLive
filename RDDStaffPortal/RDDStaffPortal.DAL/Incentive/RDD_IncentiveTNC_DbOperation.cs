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
    public class RDD_IncentiveTNC_DbOperation
    {
        CommonFunction cf = new CommonFunction();

        public RDD_IncentiveTNC GetDropList(string username, string Eflag)
        {
            RDD_IncentiveTNC RDD_Itnc = new RDD_IncentiveTNC();
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
            RDD_Itnc.DesignationNameList = DesignationList;
            RDD_Itnc.YearList = YearsList;
            return RDD_Itnc;
        }

        public RDD_IncentiveTNC GetData(string UserName, int TnCId, RDD_IncentiveTNC RDD_IncentiveTnC)
        {
            try
            {
                SqlParameter[] Para = {
                    new SqlParameter("@TnCIde",TnCId),
                    new SqlParameter("@p_UserName",UserName),
                    new SqlParameter("@p_type","Single"),
                };
                DataSet dsModules = cf.ExecuteDataSet("RDD_IncentieveTnC_GetData", CommandType.StoredProcedure, Para);
                if (dsModules.Tables.Count > 0)
                {
                    DataTable dtModule = dsModules.Tables[0];
                    DataRowCollection drc = dtModule.Rows;
                    foreach (DataRow dr in drc)
                    {
                        RDD_IncentiveTnC.TnCId = !string.IsNullOrWhiteSpace(dr["TnCId"].ToString()) ? Convert.ToInt32(dr["TnCId"].ToString()) : 0;
                        RDD_IncentiveTnC.DesigId = !string.IsNullOrWhiteSpace(dr["DesigId"].ToString()) ? Convert.ToInt32(dr["DesigId"].ToString()) : 0;
                        RDD_IncentiveTnC.DesigName = !string.IsNullOrWhiteSpace(dr["DesigName"].ToString()) ? dr["DesigName"].ToString() : "";                        
                        RDD_IncentiveTnC.Period = !string.IsNullOrWhiteSpace(dr["Period"].ToString()) ? dr["Period"].ToString() : "";
                        RDD_IncentiveTnC.Years = !string.IsNullOrWhiteSpace(dr["Years"].ToString()) ? dr["Years"].ToString() : "";                        
                        RDD_IncentiveTnC.CreatedOn = !string.IsNullOrWhiteSpace(dr["CreatedOn"].ToString()) ? Convert.ToDateTime(dr["CreatedOn"].ToString()) : System.DateTime.Now;
                        RDD_IncentiveTnC.CreatedBy = !string.IsNullOrWhiteSpace(dr["CreatedBy"].ToString()) ? dr["CreatedBy"].ToString() : "";
                        RDD_IncentiveTnC.LastUpdatedOn = !string.IsNullOrWhiteSpace(dr["LastUpdatedOn"].ToString()) ? Convert.ToDateTime(dr["LastUpdatedOn"].ToString()) : System.DateTime.Now;
                        RDD_IncentiveTnC.LastUpdatedBy = !string.IsNullOrWhiteSpace(dr["LastUpdatedBy"].ToString()) ? dr["LastUpdatedBy"].ToString() : "";
                    }

                    DataTable dtModule1 = dsModules.Tables[1];
                    DataRowCollection drc1 = dtModule1.Rows;
                    List<RDD_IncentiveTermsAndConditionTrans> RDDKpiTncTrans = new List<RDD_IncentiveTermsAndConditionTrans>();
                    foreach (DataRow dr in drc1)
                    {
                        RDDKpiTncTrans.Add(new RDD_IncentiveTermsAndConditionTrans
                        {
                            TransId = !string.IsNullOrWhiteSpace(dr["TransId"].ToString()) ? Convert.ToInt32(dr["TransId"].ToString()) : 0,
                            TnCId = !string.IsNullOrWhiteSpace(dr["TnCId"].ToString()) ? Convert.ToInt32(dr["TnCId"].ToString()) : 0,
                            TnC = !string.IsNullOrWhiteSpace(dr["TnC"].ToString()) ? dr["TnC"].ToString() : "",
                           
                        });
                    }
                    RDD_IncentiveTnC.RDD_IncentiveTermsAndConditionTransList = RDDKpiTncTrans;

                }
            }
            catch (Exception ex)
            {

                RDD_IncentiveTnC.TnCId = 0;
                RDD_IncentiveTnC.DesigId = 0;
                RDD_IncentiveTnC.DesigName = "";               
                RDD_IncentiveTnC.Period = "";
                RDD_IncentiveTnC.Years = "";                
                RDD_IncentiveTnC.CreatedOn = System.DateTime.Now;
                RDD_IncentiveTnC.CreatedBy = "";
                RDD_IncentiveTnC.LastUpdatedOn = System.DateTime.Now;
                RDD_IncentiveTnC.LastUpdatedBy = "";
                RDD_IncentiveTnC.RDD_IncentiveTermsAndConditionTransList = null;
            }
            return RDD_IncentiveTnC;
        }

        public List<Outcls1> Save1(RDD_IncentiveTNC RITNC)
        {
            List<Outcls1> str = new List<Outcls1>();
            RITNC.RType = "Insert";
            if (RITNC.EditFlag == true)
            {
                RITNC.RType = "Update";
            }
            try
            {
                using (TransactionScope scope = new TransactionScope())
                {
                    SqlParameter[] Para = {
                        new SqlParameter("@p_type",RITNC.RType),
                        new SqlParameter("@TnCId",RITNC.TnCId),
                        new SqlParameter("@Desigid",RITNC.DesignationId),
                        new SqlParameter("@Period",RITNC.Period),
                        new SqlParameter("@Years",RITNC.Years),
                        new SqlParameter("@Createdby",RITNC.CreatedBy),
                        new SqlParameter("@Lastupdateby",RITNC.LastUpdatedBy),
                        new SqlParameter("@Lastupdateon",RITNC.LastUpdatedOn),
                        new SqlParameter("@p_id",RITNC.id),
                        new SqlParameter("@p_response",RITNC.ErrorMsg)
                };
                    str = cf.ExecuteNonQueryListID("RDD_IncentiveTnC", Para);
                    if (str[0].Outtf == true)
                    {
                        int m = 0;
                        if (RITNC.EditFlag == false)
                        {
                            RITNC.Ptype = "I";
                        }
                        else
                        {
                            RITNC.Ptype = "D";

                            SqlParameter[] ParaDet2 = { new SqlParameter("@TnCIde", str[0].Id),
                             new SqlParameter("@p_typ",RITNC.Ptype)};
                            var det1 = cf.ExecuteNonQuery("RDD_IncentiveTnCTrans", ParaDet2);

                            if (det1 == true)
                            {
                                RITNC.Ptype = "U";
                            }
                            else
                            {
                                RITNC.Ptype = "";
                            }
                        }
                        while (RITNC.RDD_IncentiveTermsAndConditionTransList.Count > m)
                        {
                            SqlParameter[] ParaDet1 = {
                                new SqlParameter("@p_typ",RITNC.Ptype),
                                new SqlParameter("@TnCIde",str[0].Id),
                                new SqlParameter("@Tnc",RITNC.RDD_IncentiveTermsAndConditionTransList[m].TnC)                                
                            };
                            var det1 = cf.ExecuteNonQuery("RDD_IncentiveTnCTrans", ParaDet1);
                            if (det1 == false)
                            {
                                str.Clear();
                                str.Add(new Outcls1
                                {
                                    Outtf = false,
                                    Id = -1,
                                    Responsemsg = "Error occured : Incentive T&C Details "
                                });
                                return str;
                            }
                            m++;
                        }
                    }
                    else
                    {

                        return str;
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

        public List<RDD_IncentiveTNC> GetALLDATA(string UserName, int pagesize, int pageno, string psearch)
        {
            List<RDD_IncentiveTNC> _RDD_IncentiveTnc = new List<RDD_IncentiveTNC>();

            try
            {
                SqlParameter[] Para = {
                    new SqlParameter("@p_UserName",UserName),
                    new SqlParameter("@p_Search", psearch),
                    new SqlParameter("@p_PageNo", pageno),
                    new SqlParameter("@p_PageSize",pagesize),
                    new SqlParameter("@p_SortColumn", "TnCId"),
                    new SqlParameter("@p_SortOrder", "ASC"),
                    new SqlParameter("@p_type","GetAll")
                };
                DataSet dsModules = cf.ExecuteDataSet("RDD_IncentieveTnC_GetData", CommandType.StoredProcedure, Para);
                if (dsModules.Tables.Count > 0)
                {
                    DataTable dtModule = dsModules.Tables[0];
                    DataRowCollection drc = dtModule.Rows;
                    foreach (DataRow dr in drc)
                    {
                        _RDD_IncentiveTnc.Add(new RDD_IncentiveTNC()
                        {
                            TnCId = !string.IsNullOrWhiteSpace(dr["TnCId"].ToString()) ? Convert.ToInt32(dr["TnCId"].ToString()) : 0,
                            DesigId = !string.IsNullOrWhiteSpace(dr["DesigId"].ToString()) ? Convert.ToInt32(dr["DesigId"].ToString()) : 0,
                            DesigName = !string.IsNullOrWhiteSpace(dr["DesigName"].ToString()) ? dr["DesigName"].ToString() : "",                            
                            Period = !string.IsNullOrWhiteSpace(dr["Period"].ToString()) ? dr["Period"].ToString() : "",
                            Years = !string.IsNullOrWhiteSpace(dr["Years"].ToString()) ? dr["Years"].ToString() : "",                            
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
                _RDD_IncentiveTnc.Add(new RDD_IncentiveTNC()
                {
                    TnCId = 0,
                    DesigName = "",                    
                    Period = "",
                    Years = "",                    
                    CreatedOn = System.DateTime.Now,
                    CreatedBy = "",
                    LastUpdatedOn = System.DateTime.Now,
                    LastUpdatedBy = ""
                });
            }
            return _RDD_IncentiveTnc;
        }
    }
}
