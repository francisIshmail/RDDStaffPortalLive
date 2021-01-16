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
using System.Web.Mvc;
using System.Transactions;

namespace RDDStaffPortal.DAL.Incentive
{
    public class RDD_GenerateCompensation_DbOperation
    {
        CommonFunction cf = new CommonFunction();

        public RDD_GenerateComp GetDropList(string username, string Eflag)
        {
            RDD_GenerateComp RDD_GenComp = new RDD_GenerateComp();
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
                    //new SqlParameter("@p_username",username),
                    new SqlParameter("@p_type","GetEmployee")
                };
                DataSet dsModules1 = cf.ExecuteDataSet("RDD_Get_Compplan_Employee", CommandType.StoredProcedure, parm1);
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
            RDD_GenComp.DesignationNameList = DesignationList;
            RDD_GenComp.YearList = YearsList;
            RDD_GenComp.EmployeeList = EmployeeList;
            RDD_GenComp.CurrencyList = CurrencyList;
            return RDD_GenComp;
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
                foreach (DataRow dr in drc)
                {
                    DeignationList.DesigId = !string.IsNullOrWhiteSpace(dr["DesigId"].ToString()) ? Convert.ToInt32(dr["DesigId"].ToString()) : 0;
                    DeignationList.DesigName = !string.IsNullOrWhiteSpace(dr["DesigName"].ToString()) ? dr["DesigName"].ToString() : "";
                    DeignationList.Email = !string.IsNullOrWhiteSpace(dr["Email"].ToString()) ? dr["Email"].ToString() : "";
                }
            }
            return DeignationList;
        }

        public DataSet GetCompAmount(int Empide, string Period, int Year)
        {
            DataSet ds = new DataSet();
            SqlParameter[] prm =
            {
                new SqlParameter("@p_type","GetCompAmount"),
                new SqlParameter("@EmployeeId",Empide),
                new SqlParameter("@Year",Year),
                new SqlParameter("@Period",Period)
            };
            ds = cf.ExecuteDataSet("RDD_Get_Compplan_Employee", CommandType.StoredProcedure, prm);
            return ds;
        }

        public RDD_GenerateComp GetData(string UserName, int CompPlanId, RDD_GenerateComp RDD_GenCompplan)
        {
            try
            {
                SqlParameter[] Para = {
                    new SqlParameter("@CompCalId",CompPlanId),
                    new SqlParameter("@p_UserName",UserName),
                    new SqlParameter("@p_type","Single"),
                };
                DataSet dsModules = cf.ExecuteDataSet("RDD_CompensationCalculation_GetData", CommandType.StoredProcedure, Para);
                if (dsModules.Tables.Count > 0)
                {
                    DataTable dtModule = dsModules.Tables[0];
                    DataRowCollection drc = dtModule.Rows;
                    foreach (DataRow dr in drc)
                    {
                        RDD_GenCompplan.Comp_Calc_Id = !string.IsNullOrWhiteSpace(dr["Comp_Calc_Id"].ToString()) ? Convert.ToInt32(dr["Comp_Calc_Id"].ToString()) : 0;
                        RDD_GenCompplan.DesigId = !string.IsNullOrWhiteSpace(dr["DesigId"].ToString()) ? Convert.ToInt32(dr["DesigId"].ToString()) : 0;
                        RDD_GenCompplan.DesigName = !string.IsNullOrWhiteSpace(dr["DesigName"].ToString()) ? dr["DesigName"].ToString() : "";
                        RDD_GenCompplan.EmployeeId = !string.IsNullOrWhiteSpace(dr["EmployeeId"].ToString()) ? Convert.ToInt32(dr["EmployeeId"].ToString()) : 0;
                        RDD_GenCompplan.EmployeeName = !string.IsNullOrWhiteSpace(dr["EmployeeName"].ToString()) ? dr["EmployeeName"].ToString() : "";
                        RDD_GenCompplan.Total_Comp_Earned = !string.IsNullOrWhiteSpace(dr["Total_Comp_Earned"].ToString()) ? dr["Total_Comp_Earned"].ToString() : "";
                        RDD_GenCompplan.Total_Deduction = !string.IsNullOrWhiteSpace(dr["Total_Deduction"].ToString()) ? dr["Total_Deduction"].ToString() : "";
                        RDD_GenCompplan.TotalCompensation = !string.IsNullOrWhiteSpace(dr["TotalCompensation"].ToString()) ? dr["TotalCompensation"].ToString() : "";
                        RDD_GenCompplan.Period = !string.IsNullOrWhiteSpace(dr["Period"].ToString()) ? dr["Period"].ToString() : "";
                        RDD_GenCompplan.Years = !string.IsNullOrWhiteSpace(dr["Years"].ToString()) ? dr["Years"].ToString() : "";
                        RDD_GenCompplan.Currency = !string.IsNullOrWhiteSpace(dr["Currency"].ToString()) ? dr["Currency"].ToString() : "";
                        RDD_GenCompplan.Final_Comp_Earned = !string.IsNullOrWhiteSpace(dr["Final_Comp_Earned"].ToString()) ? dr["Final_Comp_Earned"].ToString() : "";
                        //RDD_GenCompplan.TotalSplitRevPercent = !string.IsNullOrWhiteSpace(dr["TotalSplitRevPercent"].ToString()) ? dr["TotalSplitRevPercent"].ToString() : "";
                        //RDD_GenCompplan.TargetNumber = !string.IsNullOrWhiteSpace(dr["TargetNumber"].ToString()) ? dr["TargetNumber"].ToString() : "";
                        RDD_GenCompplan.CreatedOn = !string.IsNullOrWhiteSpace(dr["CreatedOn"].ToString()) ? Convert.ToDateTime(dr["CreatedOn"].ToString()) : System.DateTime.Now;
                        RDD_GenCompplan.CreatedBy = !string.IsNullOrWhiteSpace(dr["CreatedBy"].ToString()) ? dr["CreatedBy"].ToString() : "";
                        RDD_GenCompplan.LastUpdatedOn = !string.IsNullOrWhiteSpace(dr["LastUpdatedOn"].ToString()) ? Convert.ToDateTime(dr["LastUpdatedOn"].ToString()) : System.DateTime.Now;
                        RDD_GenCompplan.LastUpdatedBy = !string.IsNullOrWhiteSpace(dr["LastUpdatedBy"].ToString()) ? dr["LastUpdatedBy"].ToString() : "";
                    }

                    DataTable dtModule1 = dsModules.Tables[1];
                    DataRowCollection drc1 = dtModule1.Rows;
                    List<RDD_BU_CompensationCalculation> RDDbuCompCalc = new List<RDD_BU_CompensationCalculation>();
                    foreach (DataRow dr in drc1)
                    {
                        RDDbuCompCalc.Add(new RDD_BU_CompensationCalculation
                        {
                            BUComp_Calc_Id = !string.IsNullOrWhiteSpace(dr["BUComp_Calc_Id"].ToString()) ? Convert.ToInt32(dr["BUComp_Calc_Id"].ToString()) : 0,
                            Comp_Calc_Id = !string.IsNullOrWhiteSpace(dr["Comp_Calc_Id"].ToString()) ? Convert.ToInt32(dr["Comp_Calc_Id"].ToString()) : 0,
                            BU = !string.IsNullOrWhiteSpace(dr["BU"].ToString()) ? dr["BU"].ToString() : "",
                            Earned = !string.IsNullOrWhiteSpace(dr["Earned"].ToString()) ? dr["Earned"].ToString() : "",
                            Achieved_Percentage = !string.IsNullOrWhiteSpace(dr["Achieved_Percentage"].ToString()) ? dr["Achieved_Percentage"].ToString() : "",
                            TotalAcheived = !string.IsNullOrWhiteSpace(dr["TotalAcheived"].ToString()) ? dr["TotalAcheived"].ToString() : "",
                            TotalTarget = !string.IsNullOrWhiteSpace(dr["TotalTarget"].ToString()) ? dr["TotalTarget"].ToString() : "",
                            M1 = !string.IsNullOrWhiteSpace(dr["M1"].ToString()) ? dr["M1"].ToString() : "",
                            M2 = !string.IsNullOrWhiteSpace(dr["M2"].ToString()) ? dr["M2"].ToString() : "",
                            M3 = !string.IsNullOrWhiteSpace(dr["M3"].ToString()) ? dr["M3"].ToString() : "",
                            M4 = !string.IsNullOrWhiteSpace(dr["M4"].ToString()) ? dr["M4"].ToString() : "",
                            M5 = !string.IsNullOrWhiteSpace(dr["M5"].ToString()) ? dr["M5"].ToString() : "",
                            M6 = !string.IsNullOrWhiteSpace(dr["M6"].ToString()) ? dr["M6"].ToString() : ""
                        });
                    }
                    RDD_GenCompplan.RDD_BU_CompensationCalculationList = RDDbuCompCalc;

                    DataTable dtModule2 = dsModules.Tables[2];
                    DataRowCollection drc2 = dtModule2.Rows;
                    List<RDD_KPI_CompensationCalculation> RDDkpiCompCal = new List<RDD_KPI_CompensationCalculation>();
                    foreach (DataRow dr in drc2)
                    {
                        RDDkpiCompCal.Add(new RDD_KPI_CompensationCalculation
                        {
                            KPIComp_Calc_Id = !string.IsNullOrWhiteSpace(dr["KPIComp_Calc_Id"].ToString()) ? Convert.ToInt32(dr["KPIComp_Calc_Id"].ToString()) : 0,
                            Comp_Calc_Id = !string.IsNullOrWhiteSpace(dr["Comp_Calc_Id"].ToString()) ? Convert.ToInt32(dr["Comp_Calc_Id"].ToString()) : 0,
                            KPI_Parameter = !string.IsNullOrWhiteSpace(dr["KPI_Parameter"].ToString()) ? dr["KPI_Parameter"].ToString() : "",
                            Earned = !string.IsNullOrWhiteSpace(dr["Earned"].ToString()) ? dr["Earned"].ToString() : "",
                            Achieved_Percentage = !string.IsNullOrWhiteSpace(dr["Achieved_Percentage"].ToString()) ? dr["Achieved_Percentage"].ToString() : "",
                            TotalAcheived = !string.IsNullOrWhiteSpace(dr["TotalAcheived"].ToString()) ? dr["TotalAcheived"].ToString() : "",
                            TotalTarget = !string.IsNullOrWhiteSpace(dr["TotalTarget"].ToString()) ? dr["TotalTarget"].ToString() : "",
                            M1 = !string.IsNullOrWhiteSpace(dr["M1"].ToString()) ? dr["M1"].ToString() : "",
                            M2 = !string.IsNullOrWhiteSpace(dr["M2"].ToString()) ? dr["M2"].ToString() : "",
                            M3 = !string.IsNullOrWhiteSpace(dr["M3"].ToString()) ? dr["M3"].ToString() : "",
                            M4 = !string.IsNullOrWhiteSpace(dr["M4"].ToString()) ? dr["M4"].ToString() : "",
                            M5 = !string.IsNullOrWhiteSpace(dr["M5"].ToString()) ? dr["M5"].ToString() : "",
                            M6 = !string.IsNullOrWhiteSpace(dr["M6"].ToString()) ? dr["M6"].ToString() : ""
                        });
                    }
                    RDD_GenCompplan.RDD_KPI_CompensationCalculationList = RDDkpiCompCal;
                }
            }
            catch (Exception ex)
            {
                RDD_GenCompplan.Comp_Calc_Id = 0;
                RDD_GenCompplan.DesigId = 0;
                RDD_GenCompplan.DesigName = "";
                RDD_GenCompplan.EmployeeId = 0;
                RDD_GenCompplan.EmployeeName = "";
                RDD_GenCompplan.TotalCompensation = "";
                RDD_GenCompplan.Period = "";
                RDD_GenCompplan.Years = "";
                RDD_GenCompplan.Currency = "";
                RDD_GenCompplan.Total_Comp_Earned = "";
                RDD_GenCompplan.Total_Deduction = "";
                RDD_GenCompplan.Final_Comp_Earned = "";
                RDD_GenCompplan.CreatedOn = System.DateTime.Now;
                RDD_GenCompplan.CreatedBy = "";
                RDD_GenCompplan.LastUpdatedOn = System.DateTime.Now;
                RDD_GenCompplan.LastUpdatedBy = "";
                RDD_GenCompplan.RDD_BU_CompensationCalculationList = null;
                RDD_GenCompplan.RDD_KPI_CompensationCalculationList = null;
            }
            return RDD_GenCompplan;
        }

        public DataSet GetBuCompdetails(int EmployeeId, string Period, int Years)
        {
            DataSet ds = new DataSet();
            SqlParameter[] prm =
            {
                new SqlParameter("@p_EmployeeId",EmployeeId),
                new SqlParameter("@p_Year",Years),
                new SqlParameter("@p_Period",Period)
            };
            ds = cf.ExecuteDataSet("RDD_Comp_GetRevenueAndGPAcheivedToGenerateComp", CommandType.StoredProcedure, prm);
            return ds;
        }

        public DataSet GetKpiTncs(int DesigId, string Period, int Years)
        {
            DataSet ds = new DataSet();
            SqlParameter[] prm =
            {
                new SqlParameter("@Desigid",DesigId),
                new SqlParameter("@Year",Years),
                new SqlParameter("@Period",Period)
            };
            ds = cf.ExecuteDataSet("RDD_GetKPITnC", CommandType.StoredProcedure, prm);
            return ds;
        }

        public DataSet GetDeductAmount(int EmpId, string Period, int Years)
        {
            DataSet ds = new DataSet();
            SqlParameter[] prm =
            {
                new SqlParameter("@p_EmployeeId",EmpId),
                new SqlParameter("@p_Year",Years),
                new SqlParameter("@p_Period",Period)
            };
            ds = cf.ExecuteDataSet("RDD_Comp_GetTotalCompDeducation", CommandType.StoredProcedure, prm);
            return ds;
        }

        public DataSet GetIncentiveSlab(int Empid, string Period, int Years)
        {
            DataSet ds = new DataSet();
            SqlParameter[] prm =
            {          
                new SqlParameter("@Empid",Empid),
                new SqlParameter("@Year",Years),
                new SqlParameter("@Period",Period)
            };
            ds = cf.ExecuteDataSet("RDD_GetIncentiveSlab", CommandType.StoredProcedure, prm);
            return ds;
        }

        public List<Outcls1> Save1(RDD_GenerateComp RGENCOMP)
        {
            List<Outcls1> str = new List<Outcls1>();
            RGENCOMP.RType = "Insert";
            if (RGENCOMP.EditFlag == true)
            {
                RGENCOMP.RType = "Update";
            }
            try
            {
                using (TransactionScope scope = new TransactionScope())
                {
                    SqlParameter[] Para = {
                        new SqlParameter("@p_type",RGENCOMP.RType),
                        new SqlParameter("@CompCalId",RGENCOMP.Comp_Calc_Id),
                        new SqlParameter("@Empid",RGENCOMP.EmployeeId),
                        new SqlParameter("@DesigId",RGENCOMP.DesigId),
                        new SqlParameter("@TotalCompensation",RGENCOMP.TotalCompensation),
                        new SqlParameter("@Period",RGENCOMP.Period),
                        new SqlParameter("@Currency",RGENCOMP.Currency),
                        new SqlParameter("@Years",RGENCOMP.Years),
                        new SqlParameter("@TotalCompEarn",RGENCOMP.Total_Comp_Earned),
                        new SqlParameter("@TotalDeduction",RGENCOMP.Total_Deduction),
                        new SqlParameter("@FinalCompEarn",RGENCOMP.Final_Comp_Earned),                       
                        new SqlParameter("@Createdby",RGENCOMP.CreatedBy),
                        new SqlParameter("@Lastupdateby",RGENCOMP.LastUpdatedBy),
                        new SqlParameter("@Lastupdateon",RGENCOMP.LastUpdatedOn),
                        new SqlParameter("@p_id",RGENCOMP.id),
                        new SqlParameter("@p_response",RGENCOMP.ErrorMsg)
                    };

                    str = cf.ExecuteNonQueryListID("RDD_CompensationCalculation_Insert_Update", Para);
                    if (str[0].Outtf == true)
                    {
                        int k = 0;
                        int m = 0;
                        if (RGENCOMP.EditFlag == false)
                        {
                            RGENCOMP.Ptype = "I";
                        }
                        else
                        {
                            RGENCOMP.Ptype = "D";

                            SqlParameter[] ParaDet2 = { new SqlParameter("@CompPlanId", str[0].Id),
                             new SqlParameter("@p_typ",RGENCOMP.Ptype)};
                            var det1 = cf.ExecuteNonQuery("RDD_BU_CompensationCalculation_Insert_Update_Delete", ParaDet2);

                            SqlParameter[] ParaDet3 = { new SqlParameter("@CompPlanId", str[0].Id),
                             new SqlParameter("@p_typ",RGENCOMP.Ptype)};
                            var det2 = cf.ExecuteNonQuery("RDD_KPI_CompensationCalculation_Insert_Update_Delete", ParaDet3);

                            if (det1 == true && det2 == true)
                            {
                                RGENCOMP.Ptype = "U";
                            }
                            else
                            {
                                RGENCOMP.Ptype = "";
                            }
                        }
                        
                        while (RGENCOMP.RDD_BU_CompensationCalculationList.Count > m)
                        {
                            SqlParameter[] ParaDet1 = {
                                new SqlParameter("@p_typ",RGENCOMP.Ptype),
                                new SqlParameter("@CompCalId",str[0].Id),
                                new SqlParameter("@BU",RGENCOMP.RDD_BU_CompensationCalculationList[m].BU),
                                new SqlParameter("@Earn",RGENCOMP.RDD_BU_CompensationCalculationList[m].Earned),
                                new SqlParameter("@AchievePercentage",RGENCOMP.RDD_BU_CompensationCalculationList[m].Achieved_Percentage),
                                new SqlParameter("@TotalAchieve",RGENCOMP.RDD_BU_CompensationCalculationList[m].TotalAcheived),
                                new SqlParameter("@TotalTarget",RGENCOMP.RDD_BU_CompensationCalculationList[m].TotalTarget),
                                new SqlParameter("@M1",RGENCOMP.RDD_BU_CompensationCalculationList[m].M1),
                                new SqlParameter("@M2",RGENCOMP.RDD_BU_CompensationCalculationList[m].M2),
                                new SqlParameter("@M3",RGENCOMP.RDD_BU_CompensationCalculationList[m].M3),
                                new SqlParameter("@M4",RGENCOMP.RDD_BU_CompensationCalculationList[m].M4),
                                new SqlParameter("@M5",RGENCOMP.RDD_BU_CompensationCalculationList[m].M5),
                                new SqlParameter("@M6",RGENCOMP.RDD_BU_CompensationCalculationList[m].M6)
                            };

                            var det1 = cf.ExecuteNonQuery("RDD_BU_CompensationCalculation_Insert_Update_Delete", ParaDet1);

                            if (det1 == false)
                            {
                                str.Clear();
                                str.Add(new Outcls1
                                {
                                    Outtf = false,
                                    Id = -1,
                                    Responsemsg = "Error occured : BU Compensation Calculation Details "
                                });
                                return str;
                            }
                            m++;
                        }
                        if (RGENCOMP.RDD_KPI_CompensationCalculationList != null)
                        {
                            while (RGENCOMP.RDD_KPI_CompensationCalculationList.Count > k)
                            {
                                SqlParameter[] ParaDet1 = {
                                new SqlParameter("@p_typ",RGENCOMP.Ptype),
                                new SqlParameter("@CompCalId",str[0].Id),
                                new SqlParameter("@KPI_Parameter",RGENCOMP.RDD_KPI_CompensationCalculationList[k].KPI_Parameter),
                                new SqlParameter("@Earn",RGENCOMP.RDD_KPI_CompensationCalculationList[k].Earned),
                                new SqlParameter("@AchievePercentage",RGENCOMP.RDD_KPI_CompensationCalculationList[k].Achieved_Percentage),
                                new SqlParameter("@TotalAchieve",RGENCOMP.RDD_KPI_CompensationCalculationList[k].TotalAcheived),
                                new SqlParameter("@TotalTarget",RGENCOMP.RDD_KPI_CompensationCalculationList[k].TotalTarget),
                                new SqlParameter("@M1",RGENCOMP.RDD_KPI_CompensationCalculationList[k].M1),
                                new SqlParameter("@M2",RGENCOMP.RDD_KPI_CompensationCalculationList[k].M2),
                                new SqlParameter("@M3",RGENCOMP.RDD_KPI_CompensationCalculationList[k].M3),
                                new SqlParameter("@M4",RGENCOMP.RDD_KPI_CompensationCalculationList[k].M4),
                                new SqlParameter("@M5",RGENCOMP.RDD_KPI_CompensationCalculationList[k].M5),
                                new SqlParameter("@M6",RGENCOMP.RDD_KPI_CompensationCalculationList[k].M6)
                            };

                                var det1 = cf.ExecuteNonQuery("RDD_KPI_CompensationCalculation_Insert_Update_Delete", ParaDet1);

                                if (det1 == false)
                                {
                                    str.Clear();
                                    str.Add(new Outcls1
                                    {
                                        Outtf = false,
                                        Id = -1,
                                        Responsemsg = "Error occured : KPI Compensation Calculation Details "
                                    });
                                    return str;
                                }
                                k++;
                            }
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

        public List<RDD_GenerateComp> GetALLDATA(string UserName, int pagesize, int pageno, string psearch)
        {
            List<RDD_GenerateComp> _RDD_GenComp = new List<RDD_GenerateComp>();

            try
            {
                SqlParameter[] Para = {
                    new SqlParameter("@p_UserName",UserName),
                    new SqlParameter("@p_Search", psearch),
                    new SqlParameter("@p_PageNo", pageno),
                    new SqlParameter("@p_PageSize",pagesize),
                    new SqlParameter("@p_SortColumn", "Comp_Calc_Id"),
                    new SqlParameter("@p_SortOrder", "ASC"),
                    new SqlParameter("@p_type","GetAll")
                };
                DataSet dsModules = cf.ExecuteDataSet("RDD_CompensationCalculation_GetData", CommandType.StoredProcedure, Para);
                if (dsModules.Tables.Count > 0)
                {
                    DataTable dtModule = dsModules.Tables[0];
                    DataRowCollection drc = dtModule.Rows;
                    foreach (DataRow dr in drc)
                    {
                        _RDD_GenComp.Add(new RDD_GenerateComp()
                        {
                            Comp_Calc_Id = !string.IsNullOrWhiteSpace(dr["Comp_Calc_Id"].ToString()) ? Convert.ToInt32(dr["Comp_Calc_Id"].ToString()) : 0,
                            DesigId = !string.IsNullOrWhiteSpace(dr["DesigId"].ToString()) ? Convert.ToInt32(dr["DesigId"].ToString()) : 0,
                            DesigName = !string.IsNullOrWhiteSpace(dr["DesigName"].ToString()) ? dr["DesigName"].ToString() : "",
                            EmployeeName = !string.IsNullOrWhiteSpace(dr["EmployeeName"].ToString()) ? dr["EmployeeName"].ToString() : "",                            
                            TotalCompensation = !string.IsNullOrWhiteSpace(dr["TotalCompensation"].ToString()) ? dr["TotalCompensation"].ToString() : "",
                            Period = !string.IsNullOrWhiteSpace(dr["Period"].ToString()) ? dr["Period"].ToString() : "",
                            Years = !string.IsNullOrWhiteSpace(dr["Years"].ToString()) ? dr["Years"].ToString() : "",
                            Currency = !string.IsNullOrWhiteSpace(dr["Currency"].ToString()) ? dr["Currency"].ToString() : "",
                            Total_Comp_Earned = !string.IsNullOrWhiteSpace(dr["Total_Comp_Earned"].ToString()) ? dr["Total_Comp_Earned"].ToString() : "",
                            Total_Deduction = !string.IsNullOrWhiteSpace(dr["Total_Deduction"].ToString()) ? dr["Total_Deduction"].ToString() : "",
                            Final_Comp_Earned = !string.IsNullOrWhiteSpace(dr["Final_Comp_Earned"].ToString()) ? dr["Final_Comp_Earned"].ToString() : "",
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
                _RDD_GenComp.Add(new RDD_GenerateComp()
                {
                    Comp_Calc_Id = 0,
                    DesigName = "",
                    EmployeeName = "",
                    Period = "",
                    Years = "",
                    TotalCompensation = "",
                    Currency = "",
                    Total_Comp_Earned = "",
                    Total_Deduction = "",
                    Final_Comp_Earned = "",
                    CreatedOn = System.DateTime.Now,
                    CreatedBy = "",
                    LastUpdatedOn = System.DateTime.Now,
                    LastUpdatedBy = ""
                });
            }
            return _RDD_GenComp;
        }
    }
}
