using RDDStaffPortal.DAL.DataModels;
using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
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

        public DataSet GetDrop()
        {
            SqlParameter[] parm = {               
                };
              DataSet dsModules = Com.ExecuteDataSet("RDD_Rpt_GetStockSheets_Drop", CommandType.StoredProcedure, parm);
            return dsModules;
        }
        public DataSet GetDrop1()
        {
            SqlParameter[] parm = {
                };
            DataSet dsModules = Com.ExecuteDataSet("RDD_Rpt_GetBackLogSheet_Drop", CommandType.StoredProcedure, parm);
            return dsModules;
        }
        public DataTable Getdata3(string Username)
        {
            SqlParameter[] parm = { 
                new SqlParameter("@p_flag","I1")
                };
           
            DataSet dsModules = Com.ExecuteDataSet("RDD_Rpt_GetBackLogSheet", CommandType.StoredProcedure, parm);
            DataTable dt = dsModules.Tables[0];
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

            string qur = " select Country, cSimpleCode as  [ItemCode], stockdescription as [ItemDesc], warehousename, " +
                "WhseStatus, groupDesc as [BU],OrderType," +
                " QtyOnHand as  [Qty],    " +
                "WhseOwner,ProdCategory,ProdLine," +
                "ProdGroup from tblStockSheetDataFrom5Dbs2017    ";


            DataSet dsModules = Com.ExecuteDataSet(qur);
            dt = dsModules.Tables[0];
            return dt;
            // return dsModules;
        }

    }
}
