using RDDStaffPortal.DAL.DataModels.SAP;
using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RDDStaffPortal.DAL.SAP
{
    public class SalesOrder_DBOperation
    {
        CommonFunction Com = new CommonFunction();
        public DataSet Get_BindDDLList(string dbname)
        {
            try
            {
                Db.constr = System.Configuration.ConfigurationManager.ConnectionStrings["tejSAP"].ConnectionString;

                DataSet DS = Db.myGetDS("EXEC RDD_SOR_Get_DDL_Lists '" + dbname + "'");

                return DS;

            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public SqlDataReader GetCustomers(string prefix, string dbname, string field)
        {
            try
            {
                Db.constr = System.Configuration.ConfigurationManager.ConnectionStrings["tejSAP"].ConnectionString;

                SqlDataReader Dr = Db.myGetReader("RDD_SOR_GetList_Customers '" + prefix + "','" + dbname + "','" + field + "'");

                return Dr;

            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public SqlDataReader Get_CustomersDue_Info(string dbname, string cardcode)
        {
            try
            {
                Db.constr = System.Configuration.ConfigurationManager.ConnectionStrings["tejSAP"].ConnectionString;

                SqlDataReader Dr = Db.myGetReader("RDD_SOR_Get_CustomerDue '" + cardcode + "','" + dbname + "'");

                return Dr;

            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public SqlDataReader Get_PayTerms_Days(string dbname, string groupnum)
        {
            try
            {
                Db.constr = System.Configuration.ConfigurationManager.ConnectionStrings["tejSAP"].ConnectionString;

                SqlDataReader Dr = Db.myGetReader("Select ExtraDays From [" + dbname + "].[dbo].[OCTG] Where GroupNum= " + groupnum);

                return Dr;

            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public SqlDataReader GetItemList(string prefix, string dbname)
        {
            try
            {
                Db.constr = System.Configuration.ConfigurationManager.ConnectionStrings["tejSAP"].ConnectionString;

                SqlDataReader Dr = Db.myGetReader("Exec RDD_SOR_GetList_ItemCode '" + prefix + "','" + dbname + "'");

                return Dr;

            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public DataSet Get_ActiveOPGSelloutList(string basedb, string rebatedb, string itemcode)
        {
            try
            {
                Db.constr = System.Configuration.ConfigurationManager.ConnectionStrings["tejSAP"].ConnectionString;

                DataSet DS = Db.myGetDS("EXEC RDD_SOR_Get_ActiveOPGSelloutList '" + basedb + "','" + rebatedb + "','" + itemcode + "'");

                return DS;

            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public DataSet Get_WarehouseQty(string itemcode, string whscode, string dbname)
        {
            try
            {
                Db.constr = System.Configuration.ConfigurationManager.ConnectionStrings["tejSAP"].ConnectionString;

                DataSet DS = Db.myGetDS("Select Convert(Numeric(10,2),T1.OnHand) OnHand,Convert(Numeric(10,2),T1.OnHand-(T1.IsCommited+T1.OnOrder)) ActalQty  From [" + dbname + "].[dbo].[OITW] T1 Where T1.ItemCode='" + itemcode + "' And T1.WhsCode='" + whscode + "'");

                return DS;

            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public DataSet Get_GPAndGPPer(string dbname, string itemcode, string warehouse, string qtysell, string pricesell, string curr, string opgrebateid)
        {
            try
            {
                Db.constr = System.Configuration.ConfigurationManager.ConnectionStrings["tejSAP"].ConnectionString;

                DataSet DS = Db.myGetDS("Execute RDD_SOR_Get_GPAndGPPer '" + dbname + "', '" + itemcode + "', '" + warehouse + "', " + qtysell + ", " + pricesell + ", '" + curr + "', '" + opgrebateid + "'");

                return DS;

            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public DataSet Get_TaxCodeRate(string taxcode, string dbname)
        {
            try
            {
                Db.constr = System.Configuration.ConfigurationManager.ConnectionStrings["tejSAP"].ConnectionString;

                DataSet DS = Db.myGetDS(" Select Convert(Numeric(10,2),Rate) Rate  From [" + dbname + "].[dbo].[OVTG] T1 Where T1.Code='" + taxcode + "'");

                return DS;

            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public List<RDD_OSOR> Get_SalesOrder_List(string DBName, string UserName, int pagesize, int pageno, string psearch)
        {
            List<RDD_OSOR> _RDD_OSOR = new List<RDD_OSOR>();
            try
            {
                SqlParameter[] Para = {
                    new SqlParameter("@DBName",DBName),
                    new SqlParameter("@p_UserName",UserName),
                    new SqlParameter("@SearchCriteria", psearch),
                    new SqlParameter("@p_PageNo", pageno),
                    new SqlParameter("@p_PageSize",pagesize),
                    new SqlParameter("@p_SortColumn", "Country"),
                    new SqlParameter("@p_SortOrder", "ASC"),
                    new SqlParameter("@p_type","GetAll"),

                };
                DataSet dsModules = Com.ExecuteDataSet("RDD_SOR_Get_SalesOrderLsit", CommandType.StoredProcedure, Para);
                if (dsModules.Tables.Count > 0)
                {
                    DataTable dtModule = dsModules.Tables[0];
                    DataRowCollection drc = dtModule.Rows;
                    foreach (DataRow dr in drc)
                    {
                        _RDD_OSOR.Add(new RDD_OSOR()
                        {

                            DBName = !string.IsNullOrWhiteSpace(dr["SRC"].ToString()) ? dr["SRC"].ToString() : "",
                            SO_ID = !string.IsNullOrWhiteSpace(dr["SO_ID"].ToString()) ? Convert.ToInt32(dr["SO_ID"].ToString()) : 0,
                            RefNo = !string.IsNullOrWhiteSpace(dr["RefNo"].ToString()) ? dr["RefNo"].ToString() : "",
                            SAP_DocNum = !string.IsNullOrWhiteSpace(dr["SAPDocNum"].ToString()) ? dr["SAPDocNum"].ToString() : "",
                            PostingDate = Convert.ToDateTime(dr["PostDate"].ToString()),
                            CardCode = !string.IsNullOrWhiteSpace(dr["CardCode"].ToString()) ? dr["CardCode"].ToString() : "",
                            CardName = !string.IsNullOrWhiteSpace(dr["CardName"].ToString()) ? dr["CardName"].ToString() : "",
                            RDD_Project = !string.IsNullOrWhiteSpace(dr["Project"].ToString()) ? dr["Project"].ToString() : "",
                            BusinesType = !string.IsNullOrWhiteSpace(dr["BusinesType"].ToString()) ? dr["BusinesType"].ToString() : "",
                            Total_Tx = !string.IsNullOrWhiteSpace(dr["Tax"].ToString()) ? Convert.ToDecimal(dr["Tax"].ToString()) : 0,
                            DocTotal = !string.IsNullOrWhiteSpace(dr["DocTotal"].ToString()) ? Convert.ToDecimal(dr["DocTotal"].ToString()) : 0,
                            DocStatus = !string.IsNullOrWhiteSpace(dr["ApprStatus"].ToString()) ? dr["ApprStatus"].ToString() : "",
                            GP = !string.IsNullOrWhiteSpace(dr["GP"].ToString()) ? Convert.ToDecimal(dr["GP"].ToString()) : 0,
                            GP_Per = !string.IsNullOrWhiteSpace(dr["GP_Per"].ToString()) ? Convert.ToDecimal(dr["GP_Per"].ToString()) : 0,
                            AprovedBy = !string.IsNullOrWhiteSpace(dr["AprovedBy"].ToString()) ? dr["AprovedBy"].ToString() : "",
                            Remarks = !string.IsNullOrWhiteSpace(dr["Remarks"].ToString()) ? dr["Remarks"].ToString() : "",

                        });
                    }
                }
            }
            catch (Exception)
            {
                _RDD_OSOR.Add(new RDD_OSOR()
                {

                    DBName = "",
                    SO_ID = 0,
                    RefNo = "",
                    SAP_DocNum = "",
                    PostingDate = null,
                    CardCode = "",
                    CardName = "",
                    RDD_Project = "",
                    BusinesType = "",
                    Total_Tx = 0,
                    DocTotal = 0,
                    DocStatus = "",
                    GP = 0,
                    GP_Per = 0,
                    AprovedBy = "",
                    Remarks = "",

                });

            }
            return _RDD_OSOR;
        }

        public DataSet Get_Rec_SalesOrder(string dbname, string so_id)
        {
            try
            {
                Db.constr = System.Configuration.ConfigurationManager.ConnectionStrings["tejSAP"].ConnectionString;

                DataSet DS = Db.myGetDS("Execute RDD_SOR_GetRec_SalesOrder '" + dbname + "'," + so_id);

                return DS;

            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public DataSet Post_SalesOrder_InTo_SAP(string dbname, string _so_id)
        {
            Int32 SO_ID = 0;
            DataSet result_ds = new DataSet();
            DataTable t1 = new DataTable("table");
            t1.Columns.Add("Result");
            t1.Columns.Add("Message");
            SO_ID = Convert.ToInt32(_so_id);
            try
            {
                Db.constr = System.Configuration.ConfigurationManager.ConnectionStrings["tejSAP"].ConnectionString;

                DataSet DS = Db.myGetDS("Execute RDD_SOR_Get_SalesOrderDetail_Posting '" + dbname + "'," + _so_id);

                if (DS.Tables.Count > 0)
                {
                    if (SAP_ConnectToCompany.ConnectToSAP(dbname) == true)
                    {
                        string errItemCodes = "", errMachineNos = "";
                        bool errRowFlag = false;
                        int ErrorCode;
                        string ErrMessage;
                        int RefDocSeries, iOrd;
                        string RefDocNum = "", RefDocDate = "", DocNum = "", Usrid = "";

                        SAPbobsCOM.Documents oSalesOrder;
                        oSalesOrder = (SAPbobsCOM.Documents)SAP_ConnectToCompany.mCompany.GetBusinessObject(SAPbobsCOM.BoObjectTypes.oOrders);

                        oSalesOrder.CardCode = DS.Tables[0].Rows[0]["CardCode"].ToString();
                        oSalesOrder.CardName = DS.Tables[0].Rows[0]["CardName"].ToString();
                        oSalesOrder.DocDate = Convert.ToDateTime(DS.Tables[0].Rows[0]["PostingDate"].ToString());
                        oSalesOrder.DocDueDate = Convert.ToDateTime(DS.Tables[0].Rows[0]["DeliveryDate"].ToString());
                        oSalesOrder.TaxDate = Convert.ToDateTime(DS.Tables[0].Rows[0]["PostingDate"].ToString());

                        //oSalesOrder.Series=0;
                        oSalesOrder.NumAtCard = DS.Tables[0].Rows[0]["RefNo"].ToString();

                        oSalesOrder.GroupNumber = Convert.ToInt16(DS.Tables[0].Rows[0]["CustPayTerms"].ToString());
                        oSalesOrder.SalesPersonCode = Convert.ToInt16(DS.Tables[0].Rows[0]["SalesEmp"].ToString());
                        oSalesOrder.Comments = DS.Tables[0].Rows[0]["Remarks"].ToString();
                        oSalesOrder.DocCurrency = "USD";

                        oSalesOrder.UserFields.Fields.Item("U_SO_ID").Value = DS.Tables[0].Rows[0]["SO_ID"].ToString();
                        oSalesOrder.UserFields.Fields.Item("U_PayTerm").Value = DS.Tables[0].Rows[0]["InvPayTerms"].ToString();
                        oSalesOrder.UserFields.Fields.Item("U_Project").Value = DS.Tables[0].Rows[0]["RDD_Project"].ToString();
                        oSalesOrder.UserFields.Fields.Item("U_BizType").Value = DS.Tables[0].Rows[0]["BusinesType"].ToString();

                        oSalesOrder.UserFields.Fields.Item("U_paymethod1").Value = DS.Tables[0].Rows[0]["Pay_Method_1"].ToString();
                        oSalesOrder.UserFields.Fields.Item("U_refno1").Value = DS.Tables[0].Rows[0]["Rcpt_check_No_1"].ToString();
                        oSalesOrder.UserFields.Fields.Item("U_refdate1").Value = DS.Tables[0].Rows[0]["Rcpt_check_Date_1"].ToString();
                        oSalesOrder.UserFields.Fields.Item("U_memo1").Value = DS.Tables[0].Rows[0]["Remarks_1"].ToString();
                        oSalesOrder.UserFields.Fields.Item("U_currency1").Value = DS.Tables[0].Rows[0]["Curr_1"].ToString();
                        oSalesOrder.UserFields.Fields.Item("U_amount1").Value = DS.Tables[0].Rows[0]["Amount_1"].ToString();

                        oSalesOrder.UserFields.Fields.Item("U_SOR_User").Value = DS.Tables[0].Rows[0]["CreatedBy"].ToString();

                        if (DS.Tables[0].Rows[0]["Pay_Method_2"].ToString() != "0" && DS.Tables[0].Rows[0]["Pay_Method_2"].ToString() != "")
                        {
                            oSalesOrder.UserFields.Fields.Item("U_paymethod2").Value = DS.Tables[0].Rows[0]["Pay_Method_2"].ToString();
                            oSalesOrder.UserFields.Fields.Item("U_refno2").Value = DS.Tables[0].Rows[0]["Rcpt_check_No_2"].ToString();
                            oSalesOrder.UserFields.Fields.Item("U_refdate2").Value = DS.Tables[0].Rows[0]["Rcpt_check_Date_2"].ToString();
                            oSalesOrder.UserFields.Fields.Item("U_memo2").Value = DS.Tables[0].Rows[0]["Remarks_2"].ToString();
                            oSalesOrder.UserFields.Fields.Item("U_currency2").Value = DS.Tables[0].Rows[0]["Curr_2"].ToString();
                            oSalesOrder.UserFields.Fields.Item("U_amount2").Value = DS.Tables[0].Rows[0]["Amount_2"].ToString();

                        }

                        if (DS.Tables[1].Rows.Count > 0)
                        {
                            for (int iRow = 0; iRow < DS.Tables[1].Rows.Count; iRow++)
                            {
                                oSalesOrder.Lines.ItemCode = DS.Tables[1].Rows[iRow]["ItemCode"].ToString();
                                oSalesOrder.Lines.ItemDescription = DS.Tables[1].Rows[iRow]["Description"].ToString();
                                oSalesOrder.Lines.Quantity = Convert.ToDouble(DS.Tables[1].Rows[iRow]["Quantity"].ToString());
                                oSalesOrder.Lines.Price = Convert.ToDouble(DS.Tables[1].Rows[iRow]["UnitPrice"].ToString());
                                oSalesOrder.Lines.DiscountPercent = Convert.ToDouble(DS.Tables[1].Rows[iRow]["DiscPer"].ToString());
                                oSalesOrder.Lines.TaxCode = DS.Tables[1].Rows[iRow]["TaxCode"].ToString();
                                oSalesOrder.Lines.WarehouseCode = DS.Tables[1].Rows[iRow]["WhsCode"].ToString();

                                //oSalesOrder.Lines.UserFields.Fields.Item("U_QtyAval").Value = "";
                                oSalesOrder.Lines.UserFields.Fields.Item("U_GPValue").Value = DS.Tables[1].Rows[iRow]["GP"].ToString();
                                oSalesOrder.Lines.UserFields.Fields.Item("U_GPPercent").Value = DS.Tables[1].Rows[iRow]["GPPer"].ToString();
                                oSalesOrder.Lines.UserFields.Fields.Item("U_opgRefAlpha").Value = DS.Tables[1].Rows[iRow]["OpgRefAlpha"].ToString();
                                oSalesOrder.Lines.UserFields.Fields.Item("U_opgSellinIdLink").Value = "NA";

                                oSalesOrder.Lines.Add();

                            }
                        }

                        int Result = oSalesOrder.Add();

                        if (Result != 0)
                        {
                            SAP_ConnectToCompany.mCompany.GetLastError(out ErrorCode, out ErrMessage);
                            t1.Rows.Add("False", "Sales Order Posting Failed");
                            t1.Rows.Add("Err", "ErrCode-" + ErrorCode.ToString() + " - ErrMsg-" + ErrMessage);
                            result_ds.Tables.Add(t1);

                            System.Runtime.InteropServices.Marshal.ReleaseComObject(oSalesOrder);

                           // return result_ds;

                        }
                        else
                        {
                            string dockey = SAP_ConnectToCompany.mCompany.GetNewObjectKey();
                            string docType = SAP_ConnectToCompany.mCompany.GetNewObjectType();

                            DataSet oDs;

                            if (docType == "112")
                            {
                                oDs = Db.myGetDS("Select DocNum From [" + dbname + "].[dbo].[ODRF] Where DocEntry=" + dockey + " And ObjType='17' And U_SO_ID=" + DS.Tables[0].Rows[0]["SO_ID"].ToString());

                                Db.myExecuteSQL("Update RDD_OSOR Set DocStatus='Post', Post_SAP='Y' ,ObjType='" + docType + "', SAP_DocEntry=" + dockey + ", SAP_DocNum='" + oDs.Tables[0].Rows[0]["DocNum"].ToString() + "' Where SO_ID=" + _so_id);

                                t1.Rows.Add("True", "Sales Order Created Successfuly - Draft-Pending");
                                t1.Rows.Add("DocNum", oDs.Tables[0].Rows[0]["DocNum"].ToString());
                            }
                            else
                            {
                                oDs = Db.myGetDS("Select DocNum From [" + dbname + "].[dbo].[ORDR] Where U_SO_ID=" + DS.Tables[0].Rows[0]["SO_ID"].ToString());

                                t1.Rows.Add("True", "Sales Order Created Successfuly");
                                t1.Rows.Add("DocNum", oDs.Tables[0].Rows[0]["DocNum"].ToString());
                            }


                            result_ds.Tables.Add(t1);

                            System.Runtime.InteropServices.Marshal.ReleaseComObject(oSalesOrder);
                           // return result_ds;

                           
                        }
                    }
                }
              
            }
            catch (Exception ex)
            {
                throw ex;
            }
            return result_ds;
        }


    }
}
