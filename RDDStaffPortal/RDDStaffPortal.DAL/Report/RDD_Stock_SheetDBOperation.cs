using RDDStaffPortal.DAL.DataModels;
using RDDStaffPortal.DAL.DataModels.Report;
using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Text;
using System.Threading.Tasks;

namespace RDDStaffPortal.DAL.Report
{
   public  class RDD_Stock_SheetDBOperation
    {
        CommonFunction Com = new CommonFunction();

        public List<RDD_Stock_Sheet> GetRDDCustMergList(string UserName, int pagesize, int pageno, string psearch,string Country,string BUGroup,string BU,string Whsename,string WhseOwn,string WhseStatus)
        {
            List<RDD_Stock_Sheet> Objlist = new List<RDD_Stock_Sheet>();
            try
            {
                SqlParameter[] parm = { new SqlParameter("@p_Search", psearch)
                        , new SqlParameter("@p_PageNo", pageno),
                new SqlParameter("@p_PageSize",pagesize),
                new SqlParameter("@p_SortColumn", "CardName"),
                new SqlParameter("@p_SortOrder", "ASC"),
                new SqlParameter("@p_UserName", UserName),
                 new SqlParameter("@p_Country", Country),
                new SqlParameter("@P_BUGroup", BUGroup),
                new SqlParameter("@p_WhseName", Whsename),
                new SqlParameter("@p_WhseStatus", WhseStatus),
                new SqlParameter("@p_WhseOwn", WhseOwn),
                new SqlParameter("@p_BU", BU)

                };

                //  DataSet dsModules = Com.ExecuteDataSet("retrive_RDD_Customermapping", CommandType.StoredProcedure, parm);
                DataSet dsModules = Com.ExecuteDataSet("RDD_Rpt_GetStockSheet", CommandType.StoredProcedure, parm);

                if (dsModules.Tables.Count > 0)
                {
                    DataTable dtModule = dsModules.Tables[0];
                    DataRowCollection drc = dtModule.Rows;
                    foreach (DataRow dr in drc)
                    {
                        Objlist.Add(new RDD_Stock_Sheet()
                        {
                            QtyBal = !string.IsNullOrWhiteSpace(dr["Qty"].ToString()) ? Convert.ToDecimal(dr["Qty"].ToString()) : 0,
                            BU = !string.IsNullOrWhiteSpace(dr["BU"].ToString()) ? dr["BU"].ToString():"",
                            OrderType = !string.IsNullOrWhiteSpace(dr["OrderType"].ToString()) ? dr["OrderType"].ToString() : "",
                            BUGroup = !string.IsNullOrWhiteSpace(dr["BUGroup"].ToString())?dr["BUGroup"].ToString():"",
                            Country = !string.IsNullOrWhiteSpace(dr["Country"].ToString()) ? dr["Country"].ToString():"",
                           ItemCode = !string.IsNullOrWhiteSpace(dr["ItemCode"].ToString()) ? dr["ItemCode"].ToString():"",
                            ItemDesc = !string.IsNullOrWhiteSpace(dr["ItemDesc"].ToString()) ? dr["ItemDesc"].ToString():"",
                          // CustTyp=dr["CustTyp"].ToString(),
                           ProdCategory = !string.IsNullOrWhiteSpace(dr["ProdCategory"].ToString()) ? dr["ProdCategory"].ToString():"",
                           ProdGroup = !string.IsNullOrWhiteSpace(dr["ProdGroup"].ToString()) ? dr["ProdGroup"].ToString():"",
                           ProdLine= !string.IsNullOrWhiteSpace(dr["ProdLine"].ToString()) ?dr["ProdLine"].ToString() : "",
                           Qty = !string.IsNullOrWhiteSpace(dr["Qty"].ToString()) ? Convert.ToDecimal(dr["Qty"].ToString()):0,
                           QtyCommitted = !string.IsNullOrWhiteSpace(dr["QtyCommitted"].ToString()) ? Convert.ToDecimal(dr["QtyCommitted"].ToString()):0,
                           QtyOrdered = !string.IsNullOrWhiteSpace(dr["QtyOrdered"].ToString()) ? Convert.ToDecimal(dr["QtyOrdered"].ToString()):0,
                           RowNum = !string.IsNullOrWhiteSpace(dr["RowNum"].ToString()) ? Convert.ToInt32(dr["RowNum"].ToString()):0,
                           UnitCost = !string.IsNullOrWhiteSpace(dr["UnitCost"].ToString()) ? Convert.ToDecimal(dr["UnitCost"].ToString()):0,
                           Value = !string.IsNullOrWhiteSpace(dr["Value"].ToString()) ? Convert.ToDecimal(dr["Value"].ToString()):0,
                            WhseName = !string.IsNullOrWhiteSpace(dr["warehousename"].ToString()) ? dr["warehousename"].ToString() : "",
                            WhseOwner = !string.IsNullOrWhiteSpace(dr["WhseOwner"].ToString()) ? dr["WhseOwner"].ToString() : "",
                            WhseStatus = !string.IsNullOrWhiteSpace(dr["WhseStatus"].ToString()) ? dr["WhseStatus"].ToString() : "",
                            TotalCount = !string.IsNullOrWhiteSpace(dr["TotalCount"].ToString()) ? Convert.ToInt32(dr["TotalCount"].ToString()) : 0
                        });
                    }
                }
            }
            catch (Exception ex)
            {

                Objlist = null;
            }



            return Objlist;
        }

        public List<RDD_BackLog_Sheet> GetRDDBackLogList(string UserName, int pagesize, int pageno, string psearch,string MappBU,string BUGroup,string MappProj)
        {
            List<RDD_BackLog_Sheet> Objlist = new List<RDD_BackLog_Sheet>();
            try
            {
                SqlParameter[] parm = { new SqlParameter("@p_Search", psearch)
                        , new SqlParameter("@p_PageNo", pageno),
                new SqlParameter("@p_PageSize",pagesize),
                new SqlParameter("@p_SortColumn", "CardName"),
                new SqlParameter("@p_SortOrder", "ASC"),
                new SqlParameter("@p_UserName", UserName),
                new SqlParameter("@p_mappedBU", MappBU),
                new SqlParameter("@P_BUGroup", BUGroup),
                new SqlParameter("@p_mappedProject", MappProj),
                new SqlParameter("@p_flag","I")
                };
                //  DataSet dsModules = Com.ExecuteDataSet("retrive_RDD_Customermapping", CommandType.StoredProcedure, parm);
                DataSet dsModules = Com.ExecuteDataSet("RDD_Rpt_GetBackLogSheet", CommandType.StoredProcedure, parm);
                if (dsModules.Tables.Count > 0)
                {
                    DataTable dtModule = dsModules.Tables[0];
                    DataRowCollection drc = dtModule.Rows;
                    foreach (DataRow dr in drc)
                    {
                        Objlist.Add(new RDD_BackLog_Sheet()
                        {
                            QtyOutStanding = !string.IsNullOrWhiteSpace(dr["QtyOutStanding"].ToString()) ? dr["QtyOutStanding"].ToString() : "",
                            ShipDate = !string.IsNullOrWhiteSpace(dr["ShipDate"].ToString()) ? dr["ShipDate"].ToString() : "",
                            ShipTo = !string.IsNullOrWhiteSpace(dr["ShipTo"].ToString()) ? dr["ShipTo"].ToString() : "",
                            supplier = !string.IsNullOrWhiteSpace(dr["supplier"].ToString()) ? dr["supplier"].ToString() : "",
                            supplierAcct = !string.IsNullOrWhiteSpace(dr["supplierAcct"].ToString()) ? dr["supplierAcct"].ToString() : "",
                            supplierId = !string.IsNullOrWhiteSpace(dr["supplierId"].ToString()) ? dr["supplierId"].ToString() : "",
                            UnitPriceExcl = !string.IsNullOrWhiteSpace(dr["UnitPriceExcl"].ToString()) ? dr["UnitPriceExcl"].ToString() : "",                            
                            mapped_BU = !string.IsNullOrWhiteSpace(dr["mapped_BU"].ToString()) ? dr["mapped_BU"].ToString() : "",
                            ActualDeliveryDate = !string.IsNullOrWhiteSpace(dr["ActualDeliveryDate"].ToString()) ? dr["ActualDeliveryDate"].ToString() : "",
                            BUGroup = !string.IsNullOrWhiteSpace(dr["BUGroup"].ToString()) ? dr["BUGroup"].ToString() : "",
                            ActualShipDate = !string.IsNullOrWhiteSpace(dr["ActualShipDate"].ToString()) ? dr["ActualShipDate"].ToString() : "",
                            Code = !string.IsNullOrWhiteSpace(dr["Code"].ToString()) ? dr["Code"].ToString() : "",
                            CustomerName =!string.IsNullOrWhiteSpace(dr["CustomerName"].ToString()) ? dr["CustomerName"].ToString() : "",                           
                            db = !string.IsNullOrWhiteSpace(dr["db"].ToString()) ? dr["db"].ToString() : "",
                           DeliveryDate  = !string.IsNullOrWhiteSpace(dr["DeliveryDate"].ToString()) ? dr["DeliveryDate"].ToString() : "",
                            ItemGroup = !string.IsNullOrWhiteSpace(dr["ItemGroup"].ToString()) ? dr["ItemGroup"].ToString() : "",
                            LineTotalExcl = !string.IsNullOrWhiteSpace(dr["LineTotalExcl"].ToString()) ? dr["LineTotalExcl"].ToString():"",
                            LineTotalExclConfirmed = !string.IsNullOrWhiteSpace(dr["LineTotalExclConfirmed"].ToString()) ? dr["LineTotalExclConfirmed"].ToString() : "",
                            mapped_Project = !string.IsNullOrWhiteSpace(dr["mapped_Project"].ToString()) ? dr["mapped_Project"].ToString():"",
                            OrderQty = !string.IsNullOrWhiteSpace(dr["OrderQty"].ToString()) ? dr["OrderQty"].ToString():"",
                           OrderNum = !string.IsNullOrWhiteSpace(dr["OrderNum"].ToString()) ? dr["OrderNum"].ToString():"",
                            OrderStatus = !string.IsNullOrWhiteSpace(dr["OrderStatus"].ToString()) ? dr["OrderStatus"].ToString():"",
                           OrderType = !string.IsNullOrWhiteSpace(dr["OrderType"].ToString()) ? dr["OrderType"].ToString() : "",
                           ProjectCode = !string.IsNullOrWhiteSpace(dr["ProjectCode"].ToString()) ? dr["ProjectCode"].ToString() : "",
                           QtyConfirmed = !string.IsNullOrWhiteSpace(dr["QtyConfirmed"].ToString()) ? dr["QtyConfirmed"].ToString() : "",
                            TotalCount = !string.IsNullOrWhiteSpace(dr["TotalCount"].ToString()) ? Convert.ToInt32(dr["TotalCount"].ToString()):0
                        });
                    }
                }
            }
            catch (Exception ex)
            {

                Objlist = null;
            }



            return Objlist;
        }

        public List<RDD_Expense> GetRDDExpenseList(string UserName, int pagesize, int pageno, string psearch, string Country, string Project, string SourceDb,string Month,string year)
        {
            List<RDD_Expense> Objlist = new List<RDD_Expense>();
            try
            {

                SqlParameter[] parm = {
                    new SqlParameter("@p_Search", psearch)
                        , new SqlParameter("@p_PageNo", pageno),
                new SqlParameter("@p_PageSize",pagesize),
                new SqlParameter("@p_SortColumn", "Country"),
                new SqlParameter("@p_SortOrder", "Desc"),
                new SqlParameter("@p_UserName", UserName),
                new SqlParameter("@p_Country", Country),
                new SqlParameter("@P_Project", Project),
                new SqlParameter("@p_SourceDb", SourceDb),
                new SqlParameter("@p_Month",Month),
                new SqlParameter("@p_Year",year),
                new SqlParameter("@p_flag","I")
                };
                //  DataSet dsModules = Com.ExecuteDataSet("retrive_RDD_Customermapping", CommandType.StoredProcedure, parm);
                DataSet dsModules = Com.ExecuteDataSet("RDD_Rpt_ExpenseSheet", CommandType.StoredProcedure, parm);
                if (dsModules.Tables.Count > 0)
                {
                    DataTable dtModule = dsModules.Tables[0];
                    DataRowCollection drc = dtModule.Rows;
                    foreach (DataRow dr in drc)
                    {
                        Objlist.Add(new RDD_Expense()
                        {
                            SourceDb = !string.IsNullOrWhiteSpace(dr["SourceDb"].ToString()) ? dr["SourceDb"].ToString() : "",
                            Remarks = !string.IsNullOrWhiteSpace(dr["Remarks"].ToString()) ? dr["Remarks"].ToString() : "",
                            Project = !string.IsNullOrWhiteSpace(dr["Project"].ToString()) ? dr["Project"].ToString() : "",
                            Acct = !string.IsNullOrWhiteSpace(dr["Acct"].ToString()) ? dr["Acct"].ToString() : "",
                            AcctCode = !string.IsNullOrWhiteSpace(dr["AcctCode"].ToString()) ? dr["AcctCode"].ToString() : "",
                            Country = !string.IsNullOrWhiteSpace(dr["Country"].ToString()) ? dr["Country"].ToString() : "",
                            Qtr = !string.IsNullOrWhiteSpace(dr["Qtr"].ToString()) ? dr["Qtr"].ToString() : "",
                           Refrence= !string.IsNullOrWhiteSpace(dr["Refrence"].ToString()) ? dr["Refrence"].ToString() : "",
                            TxDate  = !string.IsNullOrWhiteSpace(dr["TxDate"].ToString()) ?Convert.ToDateTime(dr["TxDate"].ToString()) :System.DateTime.Now ,
                            SourceDbCode = !string.IsNullOrWhiteSpace(dr["SourceDbCode"].ToString()) ? dr["SourceDbCode"].ToString() : "",
                            TransType= !string.IsNullOrWhiteSpace(dr["TransType"].ToString()) ? dr["TransType"].ToString() : "",
                           weekNo= !string.IsNullOrWhiteSpace(dr["weekNo"].ToString()) ? dr["weekNo"].ToString() : "",
                           TransId = !string.IsNullOrWhiteSpace(dr["TransId"].ToString()) ? dr["TransId"].ToString() : "",
                           TotalSC = !string.IsNullOrWhiteSpace(dr["TotalSC"].ToString()) ? Convert.ToDecimal(dr["TotalSC"].ToString()) : 0,
                            RowNum = !string.IsNullOrWhiteSpace(dr["RowNum"].ToString()) ? Convert.ToInt32(dr["RowNum"].ToString()) : 0,
                            Rate = !string.IsNullOrWhiteSpace(dr["Rate"].ToString()) ? Convert.ToDecimal(dr["Rate"].ToString()) : 0,
                            DebitSC = !string.IsNullOrWhiteSpace(dr["DebitSC"].ToString()) ? Convert.ToDecimal(dr["DebitSC"].ToString()) : 0,
                            TotalLC = !string.IsNullOrWhiteSpace(dr["TotalLC"].ToString()) ? Convert.ToDecimal(dr["TotalLC"].ToString()) : 0,
                            TotalFC = !string.IsNullOrWhiteSpace(dr["TotalFC"].ToString()) ? Convert.ToDecimal(dr["TotalFC"].ToString()) : 0,
                            DebitLC = !string.IsNullOrWhiteSpace(dr["DebitLC"].ToString()) ? Convert.ToDecimal(dr["DebitLC"].ToString()) : 0,
                            CreditLC = !string.IsNullOrWhiteSpace(dr["CreditLC"].ToString()) ? Convert.ToDecimal(dr["CreditLC"].ToString()) : 0,
                            DebitFC = !string.IsNullOrWhiteSpace(dr["DebitFC"].ToString()) ? Convert.ToDecimal(dr["DebitFC"].ToString()) : 0,
                            CreditFC = !string.IsNullOrWhiteSpace(dr["CreditFC"].ToString()) ?Convert.ToDecimal(dr["CreditFC"].ToString()) : 0,
                            Year = !string.IsNullOrWhiteSpace(dr["Year"].ToString()) ? dr["Year"].ToString() : "",
                            month = !string.IsNullOrWhiteSpace(dr["month"].ToString()) ? dr["month"].ToString() : "",
                            TotalCount = !string.IsNullOrWhiteSpace(dr["TotalCount"].ToString()) ? Convert.ToInt32(dr["TotalCount"].ToString()) : 0
                        });
                    }
                }
            }
            catch (Exception ex)
            {

                Objlist = null;
            }



            return Objlist;
        }
        public DataSet GetDrop()
        {
            DataSet dsModules;
            try
            {
                SqlParameter[] parm = {
                };
               dsModules = Com.ExecuteDataSet("RDD_Rpt_GetStockSheets_Drop", CommandType.StoredProcedure, parm);
            }
            catch (Exception)
            {

                dsModules = null;
            }
            
            return dsModules;
        }
        public DataSet GetDrop1()
        {
            DataSet dsModules;
            try
            {
                SqlParameter[] parm = {
                };
                dsModules = Com.ExecuteDataSet("RDD_Rpt_GetBackLogSheet_Drop", CommandType.StoredProcedure, parm);
            }
            catch (Exception)
            {
                dsModules = null;
                throw;
            }
           
            return dsModules;
        }
        public DataSet GetDrop2()
        {
            DataSet dsModules;
            try
            {
                SqlParameter[] parm = {
                };
                dsModules = Com.ExecuteDataSet("RDD_Rpt_Expense_Drop", CommandType.StoredProcedure, parm);
               
            }
            catch (Exception)
            {
                dsModules = null;
               
            }
            return dsModules;
        }
        public DataTable Getdata3(string Username)
        {
            DataTable dt = null;
            try
            {
                SqlParameter[] parm = {
                new SqlParameter("@p_flag","II")
                };

                DataSet dsModules = Com.ExecuteDataSet("RDD_Rpt_GetBackLogSheet", CommandType.StoredProcedure, parm);
                dt= dsModules.Tables[0];
            }
            catch (Exception)
            {

                dt = null;
            }
            
            return dt;
        }
        public DataTable Getdata4(string Username)
        {
            DataTable dt = null;
            try
            {
                SqlParameter[] parm = {
                new SqlParameter("@p_flag","II")
                };

                DataSet dsModules = Com.ExecuteDataSet("RDD_Rpt_ExpenseSheet", CommandType.StoredProcedure, parm);
                dt = dsModules.Tables[0];
            }
            catch (Exception)
            {

                dt = null;
            }
           
            return dt;
        }
        public DataTable Getdata(string Username)
        {
          DataTable dt = new DataTable();

            string qur = " select Country, cSimpleCode as  [ItemCode], stockdescription as [ItemDesc], warehousename, " +
                "WhseStatus, groupDesc as [BU], BUGroup, AvgUnitCost as [UnitCost], QtyOnHand as  [Qty],    "+
                "QtyOnPO as [QtyOrdered],QtyOnSO as [QtyCommitted],Value,WhseOwner,ProdCategory,ProdLine,"+
                "ProdGroup from tblStockSheetDataFrom5Dbs2017    ";


            DataSet dsModules = Com.ExecuteDataSet(qur);
             dt = dsModules.Tables[0];
            return dt;
           // return dsModules;
        }
        public DataTable Getdata1(string Username)
        {
            DataTable dt = new DataTable();
            try
            {
                string qur = " select Country, cSimpleCode as  [ItemCode], stockdescription as [ItemDesc], warehousename, " +
               "WhseStatus, groupDesc as [BU],OrderType," +
               " QtyOnHand as  [Qty],    " +
               "WhseOwner,ProdCategory,ProdLine," +
               "ProdGroup from tblStockSheetDataFrom5Dbs2017    ";


                DataSet dsModules = Com.ExecuteDataSet(qur);
                dt = dsModules.Tables[0];
            }
            catch (Exception)
            {

                dt = null;
            }
           
            return dt;
            // return dsModules;
        }
    
        public DataTable Getdata5(string UserName)
        {
            DataTable dt = null;
            try
            {
                SqlParameter[] parm = {
                new SqlParameter("@p_UserName", UserName),

                };
                //  DataSet dsModules = Com.ExecuteDataSet("retrive_RDD_Customermapping", CommandType.StoredProcedure, parm);
                DataSet dsModules = Com.ExecuteDataSet("RDD_TrailBalanceData_Sp", CommandType.StoredProcedure, parm);
                dt = dsModules.Tables[0];
            }
            catch (Exception)
            {

                dt = null;
            }

            return dt;
        }
        public List<RDD_TrailBalance> GetTrailBalance(string UserName)
        {
            List<RDD_TrailBalance> Objlist = new List<RDD_TrailBalance>();
            try
            {

                SqlParameter[] parm = { 
                new SqlParameter("@p_UserName", UserName),
                
                };
                //  DataSet dsModules = Com.ExecuteDataSet("retrive_RDD_Customermapping", CommandType.StoredProcedure, parm);
                DataSet dsModules = Com.ExecuteDataSet("RDD_TrailBalanceData_Sp", CommandType.StoredProcedure, parm);
                if (dsModules.Tables.Count > 0)
                {
                    DataTable dtModule = dsModules.Tables[0];
                    DataRowCollection drc = dtModule.Rows;
                    foreach (DataRow dr in drc)
                    {
                        Objlist.Add(new RDD_TrailBalance()
                        {
                       AcctName = !string.IsNullOrWhiteSpace(dr["AcctName"].ToString()) ? dr["AcctName"].ToString(): "",
                        AcctCode = !string.IsNullOrWhiteSpace(dr["AcctCode"].ToString()) ? dr["AcctCode"].ToString(): "",
                        Postable = !string.IsNullOrWhiteSpace(dr["Postable"].ToString()) ? dr["Postable"].ToString():" ",
                        AcctLevel = !string.IsNullOrWhiteSpace(dr["AcctLevel"].ToString()) ? Convert.ToInt32(dr["AcctLevel"].ToString()):0,
                       FatherAcct = !string.IsNullOrWhiteSpace(dr["FatherAcct"].ToString()) ? dr["FatherAcct"].ToString(): "",
                    LocCreditTRI = !string.IsNullOrWhiteSpace(dr["LocCreditTRI"].ToString()) ? Convert.ToDecimal(dr["LocCreditTRI"].ToString()): 0,
                    LocDebitTRI = !string.IsNullOrWhiteSpace(dr["LocDebitTRI"].ToString()) ? Convert.ToDecimal(dr["LocDebitTRI"].ToString()):0,
                    SysCreditTRI = !string.IsNullOrWhiteSpace(dr["SysCreditTRI"].ToString()) ? Convert.ToDecimal(dr["SysCreditTRI"].ToString()):0,
                    SysDebitTRI = !string.IsNullOrWhiteSpace(dr["SysDebitTRI"].ToString()) ? Convert.ToDecimal(dr["SysDebitTRI"].ToString()):0,
                    LocCreditTZ = !string.IsNullOrWhiteSpace(dr["LocCreditTZ"].ToString()) ? Convert.ToDecimal(dr["LocCreditTZ"].ToString()):0,
                    LocDebitTZ = !string.IsNullOrWhiteSpace(dr["LocDebitTZ"].ToString()) ? Convert.ToDecimal(dr["LocDebitTZ"].ToString()):0,
                    SysCreditTZ = !string.IsNullOrWhiteSpace(dr["SysCreditTZ"].ToString()) ? Convert.ToDecimal(dr["SysCreditTZ"].ToString()):0,
                    SysDebitTZ = !string.IsNullOrWhiteSpace(dr["SysDebitTZ"].ToString()) ? Convert.ToDecimal(dr["SysDebitTZ"].ToString()):0,
                    LocCreditKE = !string.IsNullOrWhiteSpace(dr["LocCreditKE"].ToString()) ? Convert.ToDecimal(dr["LocCreditKE"].ToString()):0,
                    LocDebitKE = !string.IsNullOrWhiteSpace(dr["LocDebitKE"].ToString()) ? Convert.ToDecimal(dr["LocDebitKE"].ToString()):0,
                    SysCreditKE = !string.IsNullOrWhiteSpace(dr["SysCreditKE"].ToString()) ? Convert.ToDecimal(dr["SysCreditKE"].ToString()):0,
                    SysDebitKE = !string.IsNullOrWhiteSpace(dr["SysDebitKE"].ToString()) ? Convert.ToDecimal(dr["SysDebitKE"].ToString()):0,
                    LocCreditUG = !string.IsNullOrWhiteSpace(dr["LocCreditUG"].ToString()) ? Convert.ToDecimal(dr["LocCreditUG"].ToString()):0,
                    LocDebitUG = !string.IsNullOrWhiteSpace(dr["LocDebitUG"].ToString()) ? Convert.ToDecimal(dr["LocDebitUG"].ToString()):0,
                    SysCreditUG = !string.IsNullOrWhiteSpace(dr["SysCreditUG"].ToString()) ? Convert.ToDecimal(dr["SysCreditUG"].ToString()):0,
                    SysDebitUG = !string.IsNullOrWhiteSpace(dr["SysDebitUG"].ToString()) ? Convert.ToDecimal(dr["SysDebitUG"].ToString()):0,
                    LocCreditZM = !string.IsNullOrWhiteSpace(dr["LocCreditZM"].ToString()) ? Convert.ToDecimal(dr["LocCreditZM"].ToString()):0,
                    LocDebitZM = !string.IsNullOrWhiteSpace(dr["LocDebitZM"].ToString()) ? Convert.ToDecimal(dr["LocDebitZM"].ToString()):0,
                    SysCreditZM = !string.IsNullOrWhiteSpace(dr["SysCreditZM"].ToString()) ? Convert.ToDecimal(dr["SysCreditZM"].ToString()):0,
                    SysDebitZM = !string.IsNullOrWhiteSpace(dr["SysDebitZM"].ToString()) ? Convert.ToDecimal(dr["SysDebitZM"].ToString()):0,
                    LocCreditMA = !string.IsNullOrWhiteSpace(dr["LocCreditMA"].ToString()) ? Convert.ToDecimal(dr["LocCreditMA"].ToString()):0,
                    LocDebitMA = !string.IsNullOrWhiteSpace(dr["LocDebitMA"].ToString()) ? Convert.ToDecimal(dr["LocDebitMA"].ToString()):0,
                    SysCreditMA = !string.IsNullOrWhiteSpace(dr["SysCreditMA"].ToString()) ? Convert.ToDecimal(dr["SysCreditMA"].ToString()):0,
                    SysDebitMA = !string.IsNullOrWhiteSpace(dr["SysDebitMA"].ToString()) ? Convert.ToDecimal(dr["SysDebitMA"].ToString()):0,
                    LocCreditTRNGL = !string.IsNullOrWhiteSpace(dr["LocCreditTRNGL"].ToString()) ? Convert.ToDecimal(dr["LocCreditTRNGL"].ToString()):0,
                    LocDebitTRNGL = !string.IsNullOrWhiteSpace(dr["LocDebitTRNGL"].ToString()) ? Convert.ToDecimal(dr["LocDebitTRNGL"].ToString()):0,
                    SysCreditTRNGL = !string.IsNullOrWhiteSpace(dr["SysCreditTRNGL"].ToString()) ? Convert.ToDecimal(dr["SysCreditTRNGL"].ToString()):0,
                    SysDebitTRNGL = !string.IsNullOrWhiteSpace(dr["SysDebitTRNGL"].ToString()) ? Convert.ToDecimal(dr["SysDebitTRNGL"].ToString()):0,
                    LocCreditTotal = !string.IsNullOrWhiteSpace(dr["LocCreditTotal"].ToString()) ? Convert.ToDecimal(dr["LocCreditTotal"].ToString()):0,
                    SysCreditTotal = !string.IsNullOrWhiteSpace(dr["SysCreditTotal"].ToString()) ? Convert.ToDecimal(dr["SysCreditTotal"].ToString()):0,
                    LocDebitTotal = !string.IsNullOrWhiteSpace(dr["LocDebitTotal"].ToString()) ? Convert.ToDecimal(dr["LocDebitTotal"].ToString()):0,
                    SysDebitTotal = !string.IsNullOrWhiteSpace(dr["SysDebitTotal"].ToString()) ? Convert.ToDecimal(dr["SysDebitTotal"].ToString()):0,

                        });
                    }
                }
            }
            catch (Exception ex)
            {

                Objlist = null;
            }



            return Objlist;
        }

    }
}
