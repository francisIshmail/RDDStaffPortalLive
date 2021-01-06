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
    public class RDD_IncentiveSlab_DbOperation
    {
        CommonFunction cf = new CommonFunction();
        public DataSet FillDropdown()
        {
            DataSet ds = new DataSet();
            SqlParameter[] parm = { };
            ds = cf.ExecuteDataSet("RDD_GetDesignationAndYear", CommandType.StoredProcedure, parm);
            return ds;
        }
        public RDD_IncentiveSlab GetDropList(string username, string Eflag)
        {
            RDD_IncentiveSlab RDD_Islab = new RDD_IncentiveSlab();
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
            RDD_Islab.DesignationNameList = DesignationList;
            RDD_Islab.YearList = YearsList;
            return RDD_Islab;
        }
        public List<Outcls1> Save1(RDD_IncentiveSlab RISLAB)
        {
            List<Outcls1> str = new List<Outcls1>();
            RISLAB.RType = "Insert";
            if (RISLAB.EditFlag == true)
            {
                RISLAB.RType = "Update";
            }
            try
            {
                using (TransactionScope scope = new TransactionScope())
                {
                    SqlParameter[] Para = {
                        new SqlParameter("@p_type",RISLAB.RType),
                        new SqlParameter("@Slabid",RISLAB.SlabId),                        
                        new SqlParameter("@Period",RISLAB.Period),                        
                        new SqlParameter("@Years",RISLAB.Years),                       
                        new SqlParameter("@Createdby",RISLAB.CreatedBy),
                        new SqlParameter("@Lastupdateby",RISLAB.LastUpdatedBy),
                        new SqlParameter("@Lastupdateon",RISLAB.LastUpdatedOn),
                        new SqlParameter("@p_id",RISLAB.id),
                        new SqlParameter("@p_response",RISLAB.ErrorMsg)
                };
                    str = cf.ExecuteNonQueryListID("RDD_IncentiveSlab_Save_Update", Para);
                    if (str[0].Outtf == true)
                    {
                        int m = 0;
                        if (RISLAB.EditFlag == false)
                        {
                            RISLAB.Ptype = "I";
                        }
                        else
                        {
                            RISLAB.Ptype = "D";

                            SqlParameter[] ParaDet2 = { new SqlParameter("@Slabide", str[0].Id),
                             new SqlParameter("@p_typ",RISLAB.Ptype)};
                            var det1 = cf.ExecuteNonQuery("RDD_IncentiveSlabTrans_Save_Update_Delete", ParaDet2);

                            if (det1 == true)
                            {
                                RISLAB.Ptype = "U";
                            }
                            else
                            {
                                RISLAB.Ptype = "";
                            }
                        }
                        while (RISLAB.RDD_IncentiveSlabs_TransList.Count > m)
                        {
                            SqlParameter[] ParaDet1 = {
                                new SqlParameter("@p_typ",RISLAB.Ptype),
                                new SqlParameter("@Slabide",str[0].Id),
                                new SqlParameter("@Achieved_Percentage_From",RISLAB.RDD_IncentiveSlabs_TransList[m].Achieved_Percentage_From),
                                new SqlParameter("@Achieved_Percentage_To",RISLAB.RDD_IncentiveSlabs_TransList[m].Achieved_Percentage_To),
                                new SqlParameter("@Incentive_Percentage",RISLAB.RDD_IncentiveSlabs_TransList[m].Incentive_Percentage)
                            };
                            var det1 = cf.ExecuteNonQuery("RDD_IncentiveSlabTrans_Save_Update_Delete", ParaDet1);
                            if (det1 == false)
                            {
                                str.Clear();
                                str.Add(new Outcls1
                                {
                                    Outtf = false,
                                    Id = -1,
                                    Responsemsg = "Error occured : Incentive Slab Details "
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
        public List<RDD_IncentiveSlab> GetALLDATA(string UserName, int pagesize, int pageno, string psearch)
        {
            List<RDD_IncentiveSlab> _RDD_IncentiveSlab = new List<RDD_IncentiveSlab>();

            try
            {
                SqlParameter[] Para = {
                    new SqlParameter("@p_UserName",UserName),
                    new SqlParameter("@p_Search", psearch),
                    new SqlParameter("@p_PageNo", pageno),
                    new SqlParameter("@p_PageSize",pagesize),
                    new SqlParameter("@p_SortColumn", "SlabId"),
                    new SqlParameter("@p_SortOrder", "ASC"),
                    new SqlParameter("@p_type","GetAll")
                };
                DataSet dsModules = cf.ExecuteDataSet("RDD_IncentiveSlab_GetData", CommandType.StoredProcedure, Para);
                if (dsModules.Tables.Count > 0)
                {
                    DataTable dtModule = dsModules.Tables[0];
                    DataRowCollection drc = dtModule.Rows;
                    foreach (DataRow dr in drc)
                    {
                        _RDD_IncentiveSlab.Add(new RDD_IncentiveSlab()
                        {
                            SlabId = !string.IsNullOrWhiteSpace(dr["SlabId"].ToString()) ? Convert.ToInt32(dr["SlabId"].ToString()) : 0,                            
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
                _RDD_IncentiveSlab.Add(new RDD_IncentiveSlab()
                {
                    SlabId = 0,                                      
                    Period = "",
                    Years = "",                    
                    CreatedOn = System.DateTime.Now,
                    CreatedBy = "",
                    LastUpdatedOn = System.DateTime.Now,
                    LastUpdatedBy = ""
                });
            }
            return _RDD_IncentiveSlab;
        }
        public RDD_IncentiveSlab GetData(string UserName, int SlabId, RDD_IncentiveSlab RDD_Incentive)
        {
            try
            {
                SqlParameter[] Para = {
                    new SqlParameter("@Slabide",SlabId),
                    new SqlParameter("@p_UserName",UserName),
                    new SqlParameter("@p_type","Single"),
                };
                DataSet dsModules = cf.ExecuteDataSet("RDD_IncentiveSlab_GetData", CommandType.StoredProcedure, Para);
                if (dsModules.Tables.Count > 0)
                {
                    DataTable dtModule = dsModules.Tables[0];
                    DataRowCollection drc = dtModule.Rows;
                    foreach (DataRow dr in drc)
                    {
                        RDD_Incentive.SlabId = !string.IsNullOrWhiteSpace(dr["SlabId"].ToString()) ? Convert.ToInt32(dr["SlabId"].ToString()) : 0;
                        RDD_Incentive.Period = !string.IsNullOrWhiteSpace(dr["Period"].ToString()) ? dr["Period"].ToString() : "";
                        RDD_Incentive.Years = !string.IsNullOrWhiteSpace(dr["Years"].ToString()) ? dr["Years"].ToString() : "";
                        RDD_Incentive.CreatedOn = !string.IsNullOrWhiteSpace(dr["CreatedOn"].ToString()) ? Convert.ToDateTime(dr["CreatedOn"].ToString()) : System.DateTime.Now;
                        RDD_Incentive.CreatedBy = !string.IsNullOrWhiteSpace(dr["CreatedBy"].ToString()) ? dr["CreatedBy"].ToString() : "";
                        RDD_Incentive.LastUpdatedOn = !string.IsNullOrWhiteSpace(dr["LastUpdatedOn"].ToString()) ? Convert.ToDateTime(dr["LastUpdatedOn"].ToString()) : System.DateTime.Now;
                        RDD_Incentive.LastUpdatedBy = !string.IsNullOrWhiteSpace(dr["LastUpdatedBy"].ToString()) ? dr["LastUpdatedBy"].ToString() : "";
                    }
                    DataTable dtModule1 = dsModules.Tables[1];
                    DataRowCollection drc1 = dtModule1.Rows;
                    List<RDD_IncentiveSlabs_Trans> RDDSlabTrans = new List<RDD_IncentiveSlabs_Trans>();
                    foreach (DataRow dr in drc1)
                    {
                        RDDSlabTrans.Add(new RDD_IncentiveSlabs_Trans
                        {
                            TransSlabId = !string.IsNullOrWhiteSpace(dr["TransSlabId"].ToString()) ? Convert.ToInt32(dr["TransSlabId"].ToString()) : 0,
                            SlabId = !string.IsNullOrWhiteSpace(dr["SlabId"].ToString()) ? Convert.ToInt32(dr["SlabId"].ToString()) : 0,
                            Achieved_Percentage_From = !string.IsNullOrWhiteSpace(dr["Achieved_Percentage_From"].ToString()) ? dr["Achieved_Percentage_From"].ToString() : "",
                            Achieved_Percentage_To = !string.IsNullOrWhiteSpace(dr["Achieved_Percentage_To"].ToString()) ? dr["Achieved_Percentage_To"].ToString() : "",
                            Incentive_Percentage = !string.IsNullOrWhiteSpace(dr["Incentive_Percentage"].ToString()) ? dr["Incentive_Percentage"].ToString() : "",

                        });
                    }
                    RDD_Incentive.RDD_IncentiveSlabs_TransList = RDDSlabTrans;
                }
            }
            catch (Exception ex)
            {
                string Msg = ex.Message;
                RDD_Incentive.SlabId = 0;
                RDD_Incentive.Period = "";
                RDD_Incentive.Years = "";
                RDD_Incentive.CreatedOn = System.DateTime.Now;
                RDD_Incentive.CreatedBy = "";
                RDD_Incentive.LastUpdatedOn = System.DateTime.Now;
                RDD_Incentive.LastUpdatedBy = "";
                RDD_Incentive.RDD_IncentiveSlabs_TransList = null;
            }
            return RDD_Incentive;
        }
    }
}
