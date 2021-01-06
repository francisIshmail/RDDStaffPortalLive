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
using System.IO;
using System.Net.Mail;
using System.Web.UI.WebControls;
using System.Windows.Media;
using System.Web.UI;
using iTextSharp.text;
using iTextSharp.text.pdf;
using iTextSharp.text.html.simpleparser;

namespace RDDStaffPortal.DAL.Incentive
{
    public class RDD_CompensationPlan_DbOperation
    {
        CommonFunction cf = new CommonFunction();

        public DataSet GetLoginMail(int Empid)
        {
            DataSet ds = new DataSet();
            SqlParameter[] prm =
            {
                new SqlParameter("@p_LoginUserId",Empid)                
            };
            ds = cf.ExecuteDataSet("RDD_GetEmployeeEmailByLoginId", CommandType.StoredProcedure, prm);
            return ds;
        }

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
            List<SelectListItem> EmployeeList = new List<SelectListItem>();
            List<SelectListItem> CurrencyList = new List<SelectListItem>();
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
            EmployeeList.Add(new SelectListItem()
            {
                Text = "--Select--",
                Value = "0",
            });
            CurrencyList.Add(new SelectListItem()
            {
                Text = "--Select--",
                Value = "0",
            });
            try
            {
                SqlParameter[] parm = 
                {
                    new SqlParameter("@p_username", username)                   
                };

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
                SqlParameter[] parm1 =
                {
                    new SqlParameter("@p_username",username),
                    new SqlParameter("@p_type","GetEmployee")
                };
                DataSet dsModules1 = cf.ExecuteDataSet("RDD_GetEmployeeDetails", CommandType.StoredProcedure, parm1);
                if (dsModules1.Tables.Count > 0)
                {
                    DataTable dtModule1;
                    DataRowCollection drc1;
                    dtModule1 = dsModules1.Tables[0];
                    try
                    {
                        drc1 = dtModule1.Rows;
                        foreach (DataRow dr in drc1)
                        {
                            EmployeeList.Add(new SelectListItem()
                            {
                                Text = !string.IsNullOrWhiteSpace(dr["EmployeeName"].ToString()) ? dr["EmployeeName"].ToString() : "",
                                Value = !string.IsNullOrWhiteSpace(dr["EmployeeId"].ToString()) ? dr["EmployeeId"].ToString() : "",
                            });

                        }
                    }
                    catch (Exception)
                    {
                        EmployeeList.Add(new SelectListItem()
                        {
                            Text = "Error",
                            Value = "-1",
                        });
                    }                    
                }
                SqlParameter[] parm2 =
                {
                   
                };
                DataSet dsModules2 = cf.ExecuteDataSet("RDD_GetCurrencies", CommandType.StoredProcedure, parm2);
                if (dsModules2.Tables.Count > 0)
                {
                    DataTable dtModule2;
                    DataRowCollection drc2;
                    dtModule2 = dsModules2.Tables[0];
                    try
                    {
                        drc2 = dtModule2.Rows;
                        foreach (DataRow dr in drc2)
                        {
                            CurrencyList.Add(new SelectListItem()
                            {
                                Text = !string.IsNullOrWhiteSpace(dr["CurrCode"].ToString()) ? dr["CurrCode"].ToString() : "",
                                Value = !string.IsNullOrWhiteSpace(dr["CurrCode"].ToString()) ? dr["CurrCode"].ToString() : "",
                            });
                        }
                    }
                    catch (Exception)
                    {
                        CurrencyList.Add(new SelectListItem()
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
                EmployeeList.Add(new SelectListItem()
                {
                    Text = "Error",
                    Value = "-1",
                });
                CurrencyList.Add(new SelectListItem()
                {
                    Text = "Error",
                    Value = "-1",
                });
            }
            RDD_CompPlan.DesignationNameList = DesignationList;
            RDD_CompPlan.YearList = YearsList;
            RDD_CompPlan.EmployeeList = EmployeeList;
            RDD_CompPlan.CurrencyList = CurrencyList;
            return RDD_CompPlan;
        }

        public DesignationLists DesignationDetails(int Empide)
        {
            //List<RDD_CompensationPlan> _RDD_CompPlan = new List<RDD_CompensationPlan>();
            DesignationLists DeignationList = new DesignationLists();
            SqlParameter[] prm =
            {
                new SqlParameter("@EmployeeId", Empide),
                new SqlParameter("@p_type","GetDesignation")
            };
            DataSet dsModules = new DataSet();
            dsModules = cf.ExecuteDataSet("RDD_GetEmployeeDetails", CommandType.StoredProcedure, prm);
            if (dsModules.Tables.Count > 0)
            {
                DataTable dtModule = dsModules.Tables[0];
                DataRowCollection drc = dtModule.Rows;
                foreach(DataRow dr in drc)
                {
                    DeignationList.DesigId = !string.IsNullOrWhiteSpace(dr["DesigId"].ToString()) ? Convert.ToInt32(dr["DesigId"].ToString()) : 0;
                    DeignationList.DesigName = !string.IsNullOrWhiteSpace(dr["DesigName"].ToString()) ? dr["DesigName"].ToString() : "";
                    DeignationList.Email = !string.IsNullOrWhiteSpace(dr["Email"].ToString()) ? dr["Email"].ToString() : "";
                }                
            }
            return DeignationList;
        }

        public DataSet GetModedetails(int CompPlanid, string Loginusername)
        {
            DataSet ds = new DataSet();
            SqlParameter[] prm =
            {
                new SqlParameter("@p_CompPlanId",CompPlanid),
                new SqlParameter("@p_LoggedInUser",Loginusername)                
            };
            ds = cf.ExecuteDataSet("RDD_CompPlan_GetPlanAccessByUser", CommandType.StoredProcedure, prm);
            return ds;
        }

        public DataSet GetSalesdetails(int EmployeeId, string Period, int Years)
        {            
            DataSet ds = new DataSet();
            SqlParameter[] prm =
            {
                new SqlParameter("@p_EmployeeId",EmployeeId),
                new SqlParameter("@p_Year",Years),
                new SqlParameter("@p_Period",Period)
            };
            ds = cf.ExecuteDataSet("RDD_CompPlan_GetRevenueAndGPTarget", CommandType.StoredProcedure, prm);
            return ds;
        }

        public DataSet GetKPIdetails(int DesigId, string Period, int Years)
        {
            DataSet ds = new DataSet();
            SqlParameter[] prm =
            {
                new SqlParameter("@DesinationId",DesigId),
                new SqlParameter("@Year",Years),
                new SqlParameter("@Period",Period)
            };
            ds = cf.ExecuteDataSet("GetEmployeeKpiDetails", CommandType.StoredProcedure, prm);
            return ds;
        }
        public DataSet GetKPItargets(int DesigId, string Period, int Years)
        {
            DataSet ds = new DataSet();
            SqlParameter[] prm =
            {
                new SqlParameter("@DesinationId",DesigId),
                new SqlParameter("@Year",Years),
                new SqlParameter("@Period",Period)
            };
            ds = cf.ExecuteDataSet("GetEmployeeKpiDetailsUpdate", CommandType.StoredProcedure, prm);
            return ds;
        }
        //public DataSet GetSalesRevSplitDetail(int DesigId, string Period, int Years)
        //{
        //    DataSet ds = new DataSet();
        //    SqlParameter[] prm =
        //    {
        //        new SqlParameter("@DesigId",DesigId),
        //        new SqlParameter("@Year",Years),
        //        new SqlParameter("@Period",Period)
        //    };
        //    ds = cf.ExecuteDataSet("RDD_SalesRevSplitPercent_GetData", CommandType.StoredProcedure, prm);
        //    return ds;
        //}

        public List<Outcls1> Save1(RDD_CompensationPlan RCOMPPLAN)
        {
            List<Outcls1> str = new List<Outcls1>();
            RCOMPPLAN.RType = "Insert";
            if (RCOMPPLAN.EditFlag == true)
            {
                RCOMPPLAN.RType = "Update";
            }
            try
            {                    
                using (TransactionScope scope = new TransactionScope())
                {
                    SqlParameter[] Para = {
                        new SqlParameter("@p_type",RCOMPPLAN.RType),
                        new SqlParameter("@CompPlanId",RCOMPPLAN.CompPlanId),
                        new SqlParameter("@Empid",RCOMPPLAN.EmployeeId),
                        new SqlParameter("@DesigId",RCOMPPLAN.DesigId),
                        new SqlParameter("@TotalCompensation",RCOMPPLAN.TotalCompensation),
                        new SqlParameter("@Period",RCOMPPLAN.Period),
                        new SqlParameter("@Currency",RCOMPPLAN.Currency),
                        new SqlParameter("@Years",RCOMPPLAN.Years),
                        new SqlParameter("@TotalSplitGpPercent",RCOMPPLAN.TotalSplitGpPercent),
                        new SqlParameter("@AcceptedBySalesperson",RCOMPPLAN.AcceptedBySalesperson),
                        new SqlParameter("@TotalSplitRevPercent",RCOMPPLAN.TotalSplitRevPercent),
                        new SqlParameter("@TargetNumber",RCOMPPLAN.TargetNumber),
                        new SqlParameter("@Description",RCOMPPLAN.Description),
                        new SqlParameter("@Createdby",RCOMPPLAN.CreatedBy),
                        new SqlParameter("@Lastupdateby",RCOMPPLAN.LastUpdatedBy),
                        new SqlParameter("@Lastupdateon",RCOMPPLAN.LastUpdatedOn),
                        new SqlParameter("@p_id",RCOMPPLAN.id),
                        new SqlParameter("@p_response",RCOMPPLAN.ErrorMsg)
                    };
                    
                    str = cf.ExecuteNonQueryListID("RDD_CompensationPlan_Insert_Update", Para);
                    if (str[0].Outtf == true)
                    {
                        int k = 0;
                        int m = 0;
                        if (RCOMPPLAN.EditFlag == false)
                        {
                            RCOMPPLAN.Ptype = "I";
                        }
                        else
                        {
                            RCOMPPLAN.Ptype = "D";

                            SqlParameter[] ParaDet2 = { new SqlParameter("@CompPlanId", str[0].Id),
                             new SqlParameter("@p_typ",RCOMPPLAN.Ptype)};
                            var det1 = cf.ExecuteNonQuery("RDD_BU_CompensationPlan_Insert_Update_Delete", ParaDet2);

                            SqlParameter[] ParaDet3 = { new SqlParameter("@CompPlanId", str[0].Id),
                             new SqlParameter("@p_typ",RCOMPPLAN.Ptype)};
                            var det2 = cf.ExecuteNonQuery("RDD_KPI_CompensationPlan_Insert_Update_Delete", ParaDet3);

                            if (det1 == true && det2 == true)
                            {
                                RCOMPPLAN.Ptype = "U";
                            }
                            else
                            {
                                RCOMPPLAN.Ptype = "";
                            }
                        }
                        while (RCOMPPLAN.RDD_BU_CompensationPlanList.Count > m)
                        {
                            SqlParameter[] ParaDet1 = {
                                new SqlParameter("@p_typ",RCOMPPLAN.Ptype),
                                new SqlParameter("@CompPlanId",str[0].Id),
                                new SqlParameter("@BU",RCOMPPLAN.RDD_BU_CompensationPlanList[m].BU),
                                new SqlParameter("@GPTarget",RCOMPPLAN.RDD_BU_CompensationPlanList[m].GPTarget),
                                new SqlParameter("@RevenueTarget",RCOMPPLAN.RDD_BU_CompensationPlanList[m].RevenueTarget),
                                new SqlParameter("@Rev_Split_Percentage",RCOMPPLAN.RDD_BU_CompensationPlanList[m].Rev_Split_Percentage),
                                new SqlParameter("@GP_Split_Percentage",RCOMPPLAN.RDD_BU_CompensationPlanList[m].GP_Split_Percentage)
                            };
                           
                            var det1 = cf.ExecuteNonQuery("RDD_BU_CompensationPlan_Insert_Update_Delete", ParaDet1);
                            
                            if (det1 == false)
                            {
                                str.Clear();
                                str.Add(new Outcls1
                                {
                                    Outtf = false,
                                    Id = -1,
                                    Responsemsg = "Error occured : BU Compensation Plan Details "
                                });
                                return str;
                            }
                            m++;                            
                        }
                        
                        while (RCOMPPLAN.RDD_KPI_CompensationPlanList.Count > k)
                        {
                            SqlParameter[] ParaDet1 = {
                                new SqlParameter("@p_typ",RCOMPPLAN.Ptype),
                                new SqlParameter("@CompPlanId",str[0].Id),
                                new SqlParameter("@KPI_Parameter",RCOMPPLAN.RDD_KPI_CompensationPlanList[k].KPI_Parameter),
                                new SqlParameter("@KPI_Target",RCOMPPLAN.RDD_KPI_CompensationPlanList[k].KPI_Target),
                                new SqlParameter("@KPI_Split_Percentage",RCOMPPLAN.RDD_KPI_CompensationPlanList[k].KPI_Split_Percentage)
                            };
                            
                            var det1 = cf.ExecuteNonQuery("RDD_KPI_CompensationPlan_Insert_Update_Delete", ParaDet1);
                           
                            if (det1 == false)
                            {
                                str.Clear();
                                str.Add(new Outcls1
                                {
                                    Outtf = false,
                                    Id = -1,
                                    Responsemsg = "Error occured : KPI Compensation Plan Details "
                                });
                                return str;
                            }
                            k++;
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

        public List<RDD_CompensationPlan> GetALLDATA(string UserName, int pagesize, int pageno, string psearch)
        {
            List<RDD_CompensationPlan> _RDD_CompPlan = new List<RDD_CompensationPlan>();

            try
            {
                SqlParameter[] Para = {
                    new SqlParameter("@p_UserName",UserName),
                    new SqlParameter("@p_Search", psearch),
                    new SqlParameter("@p_PageNo", pageno),
                    new SqlParameter("@p_PageSize",pagesize),
                    new SqlParameter("@p_SortColumn", "CompPlanId"),
                    new SqlParameter("@p_SortOrder", "ASC"),
                    new SqlParameter("@p_type","GetAll")
                };
                DataSet dsModules = cf.ExecuteDataSet("RDD_CompensationPlan_GetData", CommandType.StoredProcedure, Para);
                if (dsModules.Tables.Count > 0)
                {
                    DataTable dtModule = dsModules.Tables[0];
                    DataRowCollection drc = dtModule.Rows;
                    foreach (DataRow dr in drc)
                    {
                        _RDD_CompPlan.Add(new RDD_CompensationPlan()
                        {
                            CompPlanId = !string.IsNullOrWhiteSpace(dr["CompPlanId"].ToString()) ? Convert.ToInt32(dr["CompPlanId"].ToString()) : 0,
                            DesigId = !string.IsNullOrWhiteSpace(dr["DesigId"].ToString()) ? Convert.ToInt32(dr["DesigId"].ToString()) : 0,
                            DesigName = !string.IsNullOrWhiteSpace(dr["DesigName"].ToString()) ? dr["DesigName"].ToString() : "",
                            EmployeeName = !string.IsNullOrWhiteSpace(dr["EmployeeName"].ToString()) ? dr["EmployeeName"].ToString() : "",
                            AcceptedBySalesperson = !string.IsNullOrWhiteSpace(dr["AcceptedBySalesperson"].ToString()) ? dr["AcceptedBySalesperson"].ToString() : "",
                            Description= !string.IsNullOrWhiteSpace(dr["Description"].ToString()) ? dr["Description"].ToString() : "",
                            TotalCompensation = !string.IsNullOrWhiteSpace(dr["TotalCompensation"].ToString()) ? dr["TotalCompensation"].ToString() : "",
                            Period = !string.IsNullOrWhiteSpace(dr["Period"].ToString()) ? dr["Period"].ToString() : "",
                            Years = !string.IsNullOrWhiteSpace(dr["Years"].ToString()) ? dr["Years"].ToString() : "",
                            Currency = !string.IsNullOrWhiteSpace(dr["Currency"].ToString()) ? dr["Currency"].ToString() : "",
                            TotalSplitGpPercent = !string.IsNullOrWhiteSpace(dr["TotalSplitGpPercent"].ToString()) ? dr["TotalSplitGpPercent"].ToString() : "",
                            TotalSplitRevPercent = !string.IsNullOrWhiteSpace(dr["TotalSplitRevPercent"].ToString()) ? dr["TotalSplitRevPercent"].ToString() : "",
                            TargetNumber = !string.IsNullOrWhiteSpace(dr["TargetNumber"].ToString()) ? dr["TargetNumber"].ToString() : "",                            
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
                _RDD_CompPlan.Add(new RDD_CompensationPlan()
                {
                    CompPlanId = 0,
                    DesigName = "",
                    EmployeeName = "",
                    Period = "",
                    Years = "",
                    TotalCompensation = "",
                    Currency="",
                    TotalSplitGpPercent = "",
                    TotalSplitRevPercent = "",
                    TargetNumber = "",
                    CreatedOn = System.DateTime.Now,
                    CreatedBy = "",
                    LastUpdatedOn = System.DateTime.Now,
                    LastUpdatedBy = ""
                });
            }
            return _RDD_CompPlan;
        }

        public RDD_CompensationPlan GetData(string UserName, int CompPlanId, RDD_CompensationPlan RDD_Compplan)
        {
            try
            {
                SqlParameter[] Para = {
                    new SqlParameter("@CompPlanId",CompPlanId),
                    new SqlParameter("@p_UserName",UserName),
                    new SqlParameter("@p_type","Single"),
                };
                DataSet dsModules = cf.ExecuteDataSet("RDD_CompensationPlan_GetData", CommandType.StoredProcedure, Para);
                if (dsModules.Tables.Count > 0)
                {
                    DataTable dtModule = dsModules.Tables[0];
                    DataRowCollection drc = dtModule.Rows;
                    foreach (DataRow dr in drc)
                    {
                        RDD_Compplan.CompPlanId = !string.IsNullOrWhiteSpace(dr["CompPlanId"].ToString()) ? Convert.ToInt32(dr["CompPlanId"].ToString()) : 0;
                        RDD_Compplan.DesigId = !string.IsNullOrWhiteSpace(dr["DesigId"].ToString()) ? Convert.ToInt32(dr["DesigId"].ToString()) : 0;
                        RDD_Compplan.DesigName = !string.IsNullOrWhiteSpace(dr["DesigName"].ToString()) ? dr["DesigName"].ToString() : "";
                        RDD_Compplan.EmployeeId = !string.IsNullOrWhiteSpace(dr["EmployeeId"].ToString()) ? Convert.ToInt32(dr["EmployeeId"].ToString()) : 0;
                        RDD_Compplan.EmployeeName = !string.IsNullOrWhiteSpace(dr["EmployeeName"].ToString()) ? dr["EmployeeName"].ToString() : "";
                        RDD_Compplan.AcceptedBySalesperson = !string.IsNullOrWhiteSpace(dr["AcceptedBySalesperson"].ToString()) ? dr["AcceptedBySalesperson"].ToString() : "";
                        RDD_Compplan.Description = !string.IsNullOrWhiteSpace(dr["Description"].ToString()) ? dr["Description"].ToString() : "";
                        RDD_Compplan.TotalCompensation = !string.IsNullOrWhiteSpace(dr["TotalCompensation"].ToString()) ? dr["TotalCompensation"].ToString() : "";
                        RDD_Compplan.Period = !string.IsNullOrWhiteSpace(dr["Period"].ToString()) ? dr["Period"].ToString() : "";
                        RDD_Compplan.Years = !string.IsNullOrWhiteSpace(dr["Years"].ToString()) ? dr["Years"].ToString() : "";
                        RDD_Compplan.Currency = !string.IsNullOrWhiteSpace(dr["Currency"].ToString()) ? dr["Currency"].ToString() : "";
                        RDD_Compplan.TotalSplitGpPercent = !string.IsNullOrWhiteSpace(dr["TotalSplitGpPercent"].ToString()) ? dr["TotalSplitGpPercent"].ToString() : "";
                        RDD_Compplan.TotalSplitRevPercent = !string.IsNullOrWhiteSpace(dr["TotalSplitRevPercent"].ToString()) ? dr["TotalSplitRevPercent"].ToString() : "";
                        RDD_Compplan.TargetNumber = !string.IsNullOrWhiteSpace(dr["TargetNumber"].ToString()) ? dr["TargetNumber"].ToString() : "";
                        RDD_Compplan.CreatedOn = !string.IsNullOrWhiteSpace(dr["CreatedOn"].ToString()) ? Convert.ToDateTime(dr["CreatedOn"].ToString()) : System.DateTime.Now;
                        RDD_Compplan.CreatedBy = !string.IsNullOrWhiteSpace(dr["CreatedBy"].ToString()) ? dr["CreatedBy"].ToString() : "";
                        RDD_Compplan.LastUpdatedOn = !string.IsNullOrWhiteSpace(dr["LastUpdatedOn"].ToString()) ? Convert.ToDateTime(dr["LastUpdatedOn"].ToString()) : System.DateTime.Now;
                        RDD_Compplan.LastUpdatedBy = !string.IsNullOrWhiteSpace(dr["LastUpdatedBy"].ToString()) ? dr["LastUpdatedBy"].ToString() : "";
                    }

                    DataTable dtModule1 = dsModules.Tables[1];
                    DataRowCollection drc1 = dtModule1.Rows;
                    List<RDD_BU_CompensationPlan> RDDbuComp = new List<RDD_BU_CompensationPlan>();
                    foreach (DataRow dr in drc1)
                    {
                        RDDbuComp.Add(new RDD_BU_CompensationPlan
                        {
                            BUCompId = !string.IsNullOrWhiteSpace(dr["BUCompId"].ToString()) ? Convert.ToInt32(dr["BUCompId"].ToString()) : 0,
                            CompPlanId = !string.IsNullOrWhiteSpace(dr["CompPlanId"].ToString()) ? Convert.ToInt32(dr["CompPlanId"].ToString()) : 0,
                            BU = !string.IsNullOrWhiteSpace(dr["BU"].ToString()) ? dr["BU"].ToString() : "",
                            RevenueTarget = !string.IsNullOrWhiteSpace(dr["RevenueTarget"].ToString()) ? dr["RevenueTarget"].ToString() : "",
                            GPTarget = !string.IsNullOrWhiteSpace(dr["GPTarget"].ToString()) ? dr["GPTarget"].ToString() : "",
                            Rev_Split_Percentage = !string.IsNullOrWhiteSpace(dr["Rev_Split_Percentage"].ToString()) ? dr["Rev_Split_Percentage"].ToString() : "",
                            GP_Split_Percentage = !string.IsNullOrWhiteSpace(dr["GP_Split_Percentage"].ToString()) ? dr["GP_Split_Percentage"].ToString() : "",
                        });
                    }
                    RDD_Compplan.RDD_BU_CompensationPlanList = RDDbuComp;

                    DataTable dtModule2 = dsModules.Tables[2];
                    DataRowCollection drc2 = dtModule2.Rows;
                    List<RDD_KPI_CompensationPlan> RDDkpiComp = new List<RDD_KPI_CompensationPlan>();
                    foreach (DataRow dr in drc2)
                    {
                        RDDkpiComp.Add(new RDD_KPI_CompensationPlan
                        {
                            KPICompId = !string.IsNullOrWhiteSpace(dr["KPICompId"].ToString()) ? Convert.ToInt32(dr["KPICompId"].ToString()) : 0,
                            CompPlanId = !string.IsNullOrWhiteSpace(dr["CompPlanId"].ToString()) ? Convert.ToInt32(dr["CompPlanId"].ToString()) : 0,
                            KPI_Parameter = !string.IsNullOrWhiteSpace(dr["KPI_Parameter"].ToString()) ? dr["KPI_Parameter"].ToString() : "",
                            KPI_Target = !string.IsNullOrWhiteSpace(dr["KPI_Target"].ToString()) ? dr["KPI_Target"].ToString() : "",
                            KPI_Split_Percentage = !string.IsNullOrWhiteSpace(dr["KPI_Split_Percentage"].ToString()) ? dr["KPI_Split_Percentage"].ToString() : ""
                            
                        });
                    }
                    RDD_Compplan.RDD_KPI_CompensationPlanList = RDDkpiComp;
                }
            }
            catch (Exception ex)
            {

                RDD_Compplan.CompPlanId = 0;
                RDD_Compplan.DesigId = 0;
                RDD_Compplan.DesigName = "";
                RDD_Compplan.EmployeeId = 0;
                RDD_Compplan.EmployeeName = "";
                RDD_Compplan.TotalCompensation = "";
                RDD_Compplan.Period = "";
                RDD_Compplan.Years = "";
                RDD_Compplan.Currency = "";
                RDD_Compplan.TotalSplitGpPercent = "";
                RDD_Compplan.TotalSplitRevPercent = "";
                RDD_Compplan.TargetNumber = "";
                RDD_Compplan.CreatedOn = System.DateTime.Now;
                RDD_Compplan.CreatedBy = "";
                RDD_Compplan.LastUpdatedOn = System.DateTime.Now;
                RDD_Compplan.LastUpdatedBy = "";
                RDD_Compplan.RDD_BU_CompensationPlanList = null;
                RDD_Compplan.RDD_KPI_CompensationPlanList = null;
            }
            return RDD_Compplan;
        }

        public int GetEmployeeIdByLoginName(string LoginName)        {            int EmployeeId = 0;            using (var connection = new SqlConnection(Global.getConnectionStringByName("tejSAP")))            {                if (connection.State == ConnectionState.Closed)                {                    connection.Open();                }                SqlTransaction transaction;                using (transaction = connection.BeginTransaction())                {                    try                    {                        SqlCommand cmd = new SqlCommand();                        cmd.CommandType = CommandType.StoredProcedure;                        cmd.CommandText = "RDD_GetEmployeeIdByLoginName";                        cmd.Connection = connection;                        cmd.Transaction = transaction;                        cmd.Parameters.Add("@p_LoginName", SqlDbType.NVarChar, 50).Value = LoginName;                        cmd.Parameters.Add("@p_EmployeeId", SqlDbType.Int).Direction = ParameterDirection.Output;                        cmd.ExecuteNonQuery();                        EmployeeId = (int)cmd.Parameters["@p_EmployeeId"].Value;                        cmd.Dispose();                        transaction.Commit();                    }                    catch (Exception ex)                    {                        EmployeeId = 0;                        transaction.Rollback();                    }                    finally                    {                        if (connection.State == ConnectionState.Open)                        {                            connection.Close();                        }                    }                }            }            return EmployeeId;        }
        
    }
}
