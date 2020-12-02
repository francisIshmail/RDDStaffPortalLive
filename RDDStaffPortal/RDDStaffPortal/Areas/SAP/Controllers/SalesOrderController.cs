using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using RDDStaffPortal.Areas.HR.Models;
using RDDStaffPortal.DAL.HR;
using RDDStaffPortal.DAL.DataModels;
using RDDStaffPortal.DAL.DataModels.SAP;
using RDDStaffPortal.DAL.SAP;
using System.Data;
using RDDStaffPortal.DAL;
using System.IO;
using System.Web.Helpers;
using System.Net;
using System.Web.Routing;
using RDDStaffPortal.WebServices;
using Microsoft.Ajax.Utilities;

using System.Runtime.InteropServices;
using System.Runtime.Remoting.Messaging;
using System.Text;
using Newtonsoft.Json;

using System.Web.UI;
using System.Web.UI.WebControls;
using DataTable = System.Data.DataTable;
using ClosedXML.Excel;
using System.Data.SqlClient;
using System.Net.Mime;
using System.Web.Script.Serialization;

namespace RDDStaffPortal.Areas.SAP.Controllers
{
    [Authorize]
    public class SalesOrderController : Controller
    {
        // GET: SAP/SalesOrder

        SalesOrder_DBOperation SalesOrder_DBOperation = new SalesOrder_DBOperation();
        public ActionResult Index()
        {

            Db.constr = System.Configuration.ConfigurationManager.ConnectionStrings["tejSAP"].ConnectionString;

            DataSet DS = Db.myGetDS("EXEC RDD_SOR_Get_UserWise_DBList '1','1'");
            List<RDD_OSOR> ddlDBList = new List<RDD_OSOR>();
            if (DS.Tables.Count > 0)
            {
                for (int i = 0; i < DS.Tables[0].Rows.Count; i++)
                {
                    RDD_OSOR DBList = new RDD_OSOR();
                    DBList.DBCode = DS.Tables[0].Rows[i]["Code"].ToString();
                    DBList.DBName = DS.Tables[0].Rows[i]["Descr"].ToString();
                    ddlDBList.Add(DBList);

                }
            }
            ViewBag.ddlDBList = new SelectList(ddlDBList, "DBCode", "DBName");
            return View();
        }

        public ActionResult Get_BindDDLList(string dbname)
        {
            ContentResult retVal = null;
            DataSet DS;

            try
            {
                DS = SalesOrder_DBOperation.Get_BindDDLList(dbname);

                if (DS.Tables.Count > 0)
                    retVal = Content(JsonConvert.SerializeObject(DS), "application/json");
                else
                    retVal = null;

                return retVal;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public ActionResult GetCustomers(string prefix, string dbname, string field)
        {
            string retVal = string.Empty;
            SqlDataReader rdr;
            List<string> customers = new List<string>();

            try
            {
                rdr = SalesOrder_DBOperation.GetCustomers(prefix, dbname, field);

                while (rdr.Read())
                {
                    customers.Add(string.Format("{0}#{1}", rdr["CardName"], rdr["CardCode"]));
                }
                return Content(JsonConvert.SerializeObject(customers.ToArray()), "application/json");

            }
            catch (Exception ex)
            {
                throw ex;
            }

        }

        public ActionResult Get_CustomersDue_Info(string dbname, string cardcode)
        {
            SqlDataReader rdr;
            List<string> customers_due_info = new List<string>();

            try
            {
                rdr = SalesOrder_DBOperation.Get_CustomersDue_Info(dbname, cardcode);

                while (rdr.Read())
                {
                    customers_due_info.Add(string.Format("{0}#{1}#{2}#{3}#{4}#{5}#{6}#{7}#{8}#{9}", rdr["CL"], rdr["0-30"], rdr["31-45"], rdr["46-60"], rdr["61-90"], rdr["91"], rdr["TrnsStatus"], rdr["CLStatus"], rdr["PayTerms"], rdr["ExtraDays"]));
                }

                return Content(JsonConvert.SerializeObject(customers_due_info.ToArray()), "application/json");
            }
            catch (Exception ex)
            {
                throw ex;
            }

        }

        public ActionResult Get_PayTerms_Days(string dbname, string groupnum)
        {
            SqlDataReader rdr;
            List<string> PayTerms_Days = new List<string>();

            try
            {
                rdr = SalesOrder_DBOperation.Get_PayTerms_Days(dbname, groupnum);

                while (rdr.Read())
                {
                    PayTerms_Days.Add(string.Format("{0}", rdr["ExtraDays"]));
                }

                return Content(JsonConvert.SerializeObject(PayTerms_Days.ToArray()), "application/json");
            }
            catch (Exception ex)
            {
                throw ex;
            }

        }

        public ActionResult GetItemList(string prefix, string dbname)
        {
            SqlDataReader rdr;
            List<string> Items = new List<string>();

            try
            {
                rdr = SalesOrder_DBOperation.GetItemList(prefix, dbname);

                while (rdr.Read())
                {
                    Items.Add(string.Format("{0}#{1}#{2}#{3}#{4}#{5}#{6}", rdr["ItemCode"], rdr["ItemName"], rdr["DfltWH"], rdr["VatGourpSa"], rdr["OnHand"], rdr["ActalQty"], rdr["Rate"]));

                }
                return Content(JsonConvert.SerializeObject(Items.ToArray()), "application/json");
            }
            catch (Exception ex)
            {
                throw ex;
            }

        }

        public ActionResult Get_ActiveOPGSelloutList(string basedb, string rebatedb, string itemcode)
        {
            string retVal = string.Empty;
            DataSet DS;
            try
            {
                DS = SalesOrder_DBOperation.Get_ActiveOPGSelloutList(basedb, rebatedb, itemcode);
                return Content(JsonConvert.SerializeObject(DS), "application/json");

            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public ActionResult Get_WarehouseQty(string itemcode, string whscode, string dbname)
        {
            ContentResult retVal = null;
            DataSet DS;
            try
            {
                DS = SalesOrder_DBOperation.Get_WarehouseQty(itemcode, whscode, dbname);

                if (DS.Tables.Count > 0)
                {
                    retVal = Content(JsonConvert.SerializeObject(DS), "application/json");
                }
                return retVal;
            }
            catch (Exception ex)
            {
                throw ex;
            }

        }

        public ActionResult Get_GPAndGPPer(string dbname, string itemcode, string warehouse, string qtysell, string pricesell, string curr, string opgrebateid)
        {
            ContentResult retVal = null;
            DataSet DS;
            try
            {

                DS = SalesOrder_DBOperation.Get_GPAndGPPer(dbname, itemcode, warehouse, qtysell, pricesell, curr, opgrebateid);

                if (DS.Tables.Count > 0)
                {
                    retVal = Content(JsonConvert.SerializeObject(DS), "application/json");
                }
                return retVal;

            }
            catch (Exception ex)
            {
                throw ex;
            }

        }

        public ActionResult Get_TaxCodeRate(string taxcode, string dbname)
        {
            ContentResult retVal = null;
            DataSet DS;
            try
            {
                DS = SalesOrder_DBOperation.Get_TaxCodeRate(taxcode, dbname);

                if (DS.Tables.Count > 0)
                {
                    retVal = Content(JsonConvert.SerializeObject(DS), "application/json");
                }
                return retVal;

            }
            catch (Exception ex)
            {
                throw ex;
            }

        }

        public ActionResult Save_SalesOrder(string model, string model1, string dbname)
        {
            ContentResult retVal = null;
            string Result = string.Empty;
            //string retVal = string.Empty;
            Int32 SO_ID = 0;
            DataSet result_ds = new DataSet();
            DataTable t1 = new DataTable("table");
            t1.Columns.Add("Result");
            t1.Columns.Add("Message");

            Result = "True";

            Db.constr = System.Configuration.ConfigurationManager.ConnectionStrings["tejSAP"].ConnectionString;
            using (var connection = new SqlConnection(Db.constr))
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
                        JavaScriptSerializer js = new JavaScriptSerializer();

                        RDD_OSOR[] Header = js.Deserialize<RDD_OSOR[]>(model);

                        js = new JavaScriptSerializer();
                        RDD_SOR1[] ItemDetail = js.Deserialize<RDD_SOR1[]>(model1);

                        if (Header.Length > 0)
                        {
                            SqlCommand cmd = new SqlCommand();

                            cmd.CommandType = CommandType.StoredProcedure;
                            cmd.CommandText = "RDD_OSOR_Insert_Update_Records";
                            cmd.Connection = connection;
                            cmd.Transaction = transaction;

                            cmd.Parameters.Add("@SO_ID", SqlDbType.Int).Value = Convert.ToInt16(Header[0].SO_ID.ToString());
                            cmd.Parameters.Add("@Doc_Object", SqlDbType.Int).Value = Convert.ToInt16(Header[0].Doc_Object.ToString());
                            cmd.Parameters.Add("@Base_Obj", SqlDbType.Int).Value = Convert.ToInt16(Header[0].Base_Obj.ToString());
                            cmd.Parameters.Add("@Base_ID", SqlDbType.Int).Value = Convert.ToInt16(Header[0].Base_ID.ToString());
                            cmd.Parameters.Add("@DBName", SqlDbType.VarChar).Value = Header[0].DBName.ToString();
                            cmd.Parameters.Add("@PostingDate", SqlDbType.DateTime).Value = Header[0].PostingDate;
                            cmd.Parameters.Add("@DeliveryDate", SqlDbType.DateTime).Value = Header[0].DeliveryDate;
                            cmd.Parameters.Add("@DocStatus", SqlDbType.VarChar).Value = Header[0].DocStatus.ToString();
                            cmd.Parameters.Add("@AprovedBy", SqlDbType.VarChar).Value = Header[0].AprovedBy.ToString();
                            cmd.Parameters.Add("@CreatedBy", SqlDbType.VarChar).Value = Header[0].CreatedBy.ToString();
                            cmd.Parameters.Add("@CardCode", SqlDbType.VarChar).Value = Header[0].CardCode.ToString();
                            cmd.Parameters.Add("@CardName", SqlDbType.NVarChar).Value = Header[0].CardName.ToString();
                            cmd.Parameters.Add("@RefNo", SqlDbType.VarChar).Value = Header[0].RefNo.ToString();
                            cmd.Parameters.Add("@RDD_Project", SqlDbType.VarChar).Value = Header[0].RDD_Project.ToString();
                            cmd.Parameters.Add("@BusinesType", SqlDbType.VarChar).Value = Header[0].BusinesType.ToString();
                            cmd.Parameters.Add("@InvPayTerms", SqlDbType.VarChar).Value = Header[0].InvPayTerms.ToString();
                            cmd.Parameters.Add("@CustPayTerms", SqlDbType.VarChar).Value = Header[0].CustPayTerms.ToString();
                            cmd.Parameters.Add("@Forwarder", SqlDbType.VarChar).Value = Header[0].Forwarder.ToString();
                            cmd.Parameters.Add("@SalesEmp", SqlDbType.VarChar).Value = Header[0].SalesEmp.ToString();

                            cmd.Parameters.Add("@Pay_Method_1", SqlDbType.VarChar).Value = Header[0].Pay_Method_1.ToString();
                            cmd.Parameters.Add("@Rcpt_check_No_1", SqlDbType.VarChar).Value = Header[0].Rcpt_check_No_1.ToString();

                            if (Header[0].Rcpt_check_Date_1.ToString() != null)
                                cmd.Parameters.Add("@Rcpt_check_Date_1", SqlDbType.DateTime).Value = Header[0].Rcpt_check_Date_1;

                            cmd.Parameters.Add("@Remarks_1", SqlDbType.VarChar).Value = Header[0].Remarks_1.ToString();
                            cmd.Parameters.Add("@Curr_1", SqlDbType.VarChar).Value = Header[0].Curr_1.ToString();
                            cmd.Parameters.Add("@Amount_1", SqlDbType.Float).Value = Convert.ToDouble(Header[0].Amount_1.ToString());

                            if (Header[0].Pay_Method_2 != null)
                            {
                                if (Header[0].Pay_Method_2.ToString() != "" && Header[0].Pay_Method_2.ToString() != "0")
                                {

                                    cmd.Parameters.Add("@Pay_Method_2", SqlDbType.VarChar).Value = Header[0].Pay_Method_2.ToString();
                                    cmd.Parameters.Add("@Rcpt_check_No_2", SqlDbType.VarChar).Value = Header[0].Rcpt_check_No_2.ToString();

                                    if (Header[0].Rcpt_check_Date_2.ToString() != null)
                                        cmd.Parameters.Add("@Rcpt_check_Date_2", SqlDbType.DateTime).Value = Header[0].Rcpt_check_Date_2;


                                    cmd.Parameters.Add("@Remarks_2", SqlDbType.VarChar).Value = Header[0].Remarks_2.ToString();
                                    cmd.Parameters.Add("@Curr_2", SqlDbType.VarChar).Value = Header[0].Curr_2.ToString();
                                    cmd.Parameters.Add("@Amount_2", SqlDbType.Float).Value = Convert.ToDouble(Header[0].Amount_2.ToString());

                                }
                            }

                            cmd.Parameters.Add("@Total_Bef_Tax", SqlDbType.Float).Value = Convert.ToDouble(Header[0].Total_Bef_Tax.ToString());
                            cmd.Parameters.Add("@Total_Tx", SqlDbType.Float).Value = Convert.ToDouble(Header[0].Total_Tx.ToString());
                            cmd.Parameters.Add("@DocTotal", SqlDbType.Float).Value = Convert.ToDouble(Header[0].DocTotal.ToString());
                            cmd.Parameters.Add("@GP", SqlDbType.Float).Value = Convert.ToDouble(Header[0].GP.ToString());
                            cmd.Parameters.Add("@GP_Per", SqlDbType.Float).Value = Convert.ToDouble(Header[0].GP_Per.ToString());
                            cmd.Parameters.Add("@Remarks", SqlDbType.VarChar).Value = Header[0].Remarks.ToString();
                            cmd.Parameters.Add("@Validate_Status", SqlDbType.VarChar).Value = Header[0].Validate_Status.ToString();
                            cmd.Parameters.Add("@Post_SAP", SqlDbType.VarChar).Value = Header[0].Post_SAP.ToString();
                            //if (Header[0].CreatedOn == null)
                            //    cmd.Parameters.Add("@CreatedOn", SqlDbType.DateTime).Value = DBNull.Value;
                            //else
                            //    cmd.Parameters.Add("@CreatedOn", SqlDbType.DateTime).Value = Header[0].CreatedOn.ToString();

                            //if (Header[0].LastUpdatedOn == null)
                            //    cmd.Parameters.Add("@LastUpdatedOn", SqlDbType.DateTime).Value = DBNull.Value;
                            //else
                            //{
                            //    string Dt = DateTime.Now.Month.ToString() + "/" + DateTime.Now.Day.ToString() + "/" + DateTime.Now.Year.ToString();
                            //cmd.Parameters.Add("@LastUpdatedOn", SqlDbType.DateTime).Value = Dt;// Header[0].LastUpdatedOn.ToString();
                            //}

                            //if (Header[0].LastUpdatedOn == null)
                            //    cmd.Parameters.Add("@LastUpdatedBy", SqlDbType.VarChar).Value = DBNull.Value;
                            //else
                                cmd.Parameters.Add("@LastUpdatedBy", SqlDbType.VarChar).Value = Header[0].LastUpdatedBy.ToString();

                            cmd.Parameters.Add("@id", SqlDbType.Int).Direction = ParameterDirection.Output;

                            cmd.ExecuteNonQuery();
                            SO_ID = Convert.ToInt32(cmd.Parameters["@id"].Value.ToString());

                            cmd.Dispose();

                            if (SO_ID != 0)
                            {
                                if (ItemDetail.Length > 0)
                                {
                                    string sql = "Delete From [RDD_SOR1] Where SO_ID=" + SO_ID.ToString();
                                    Db.myExecuteSQL(sql);

                                    for (int i = 0; i < ItemDetail.Length; i++)
                                    {
                                        cmd = new SqlCommand();

                                        cmd.CommandType = CommandType.StoredProcedure;
                                        cmd.CommandText = "RDD_SOR1_Insert_Update_Records";
                                        cmd.Connection = connection;
                                        cmd.Transaction = transaction;

                                        cmd.Parameters.Add("@SO_LineId", SqlDbType.BigInt).Value = ItemDetail[i].SO_LineId.ToString();
                                        cmd.Parameters.Add("@SO_ID", SqlDbType.Int).Value = SO_ID;
                                        cmd.Parameters.Add("@Base_Obj", SqlDbType.NVarChar).Value = ItemDetail[i].Base_Obj.ToString();
                                        cmd.Parameters.Add("@Base_Id", SqlDbType.NVarChar).Value = ItemDetail[i].Base_Id.ToString();
                                        cmd.Parameters.Add("@Base_LinId", SqlDbType.NVarChar).Value = ItemDetail[i].Base_LinId.ToString();
                                        cmd.Parameters.Add("@ItemCode", SqlDbType.NVarChar).Value = ItemDetail[i].ItemCode.ToString();
                                        cmd.Parameters.Add("@Description", SqlDbType.NVarChar).Value = ItemDetail[i].Description.ToString();
                                        cmd.Parameters.Add("@Quantity", SqlDbType.Float).Value = ItemDetail[i].Quantity.ToString();
                                        cmd.Parameters.Add("@UnitPrice", SqlDbType.Float).Value = ItemDetail[i].UnitPrice.ToString();
                                        cmd.Parameters.Add("@DiscPer", SqlDbType.Float).Value = ItemDetail[i].DiscPer.ToString();
                                        cmd.Parameters.Add("@LineTotal", SqlDbType.Float).Value = ItemDetail[i].LineTotal.ToString();
                                        cmd.Parameters.Add("@TaxCode", SqlDbType.NVarChar).Value = ItemDetail[i].TaxCode.ToString();
                                        cmd.Parameters.Add("@TaxRate", SqlDbType.Float).Value = ItemDetail[i].TaxRate.ToString();
                                        cmd.Parameters.Add("@WhsCode", SqlDbType.NVarChar).Value = ItemDetail[i].WhsCode.ToString();
                                        cmd.Parameters.Add("@QtyInWhs", SqlDbType.Float).Value = ItemDetail[i].QtyInWhs.ToString();
                                        cmd.Parameters.Add("@QtyAval", SqlDbType.Float).Value = ItemDetail[i].QtyAval.ToString();
                                        cmd.Parameters.Add("@OpgRefAlpha", SqlDbType.NVarChar).Value = ItemDetail[i].OpgRefAlpha.ToString();
                                        cmd.Parameters.Add("@GP", SqlDbType.Float).Value = ItemDetail[i].GP.ToString();
                                        cmd.Parameters.Add("@GPPer", SqlDbType.Float).Value = ItemDetail[i].GPPer.ToString();

                                        cmd.ExecuteNonQuery();

                                        cmd.Dispose();
                                    }
                                }

                            }

                            if (Result == "True")
                            {
                                if (Header[0].SO_ID == 0)
                                    t1.Rows.Add("True", "Saved Successfuly");
                                else
                                    t1.Rows.Add("True", "Updated Successfuly");

                                t1.Rows.Add("SO_ID", SO_ID);
                                result_ds.Tables.Add(t1);

                                retVal= Content(JsonConvert.SerializeObject(result_ds), "application/json");
                                //Result = JsonUtil.ToJSONString(result_ds.Tables[0]);

                                transaction.Commit();
                            }
                        }
                    }
                    catch (Exception ex)
                    {

                        t1.Rows.Add("False", ex.Message);
                        result_ds.Tables.Add(t1);
                        retVal = Content(JsonConvert.SerializeObject(result_ds), "application/json");
                        //Result = JsonUtil.ToJSONString(result_ds.Tables[0]);
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
            return retVal;
        }

        [Route("Get_SalesOrder_List")]
        public ActionResult Get_SalesOrder_List(string DBName, string UserName, int pagesize, int pageno, string psearch)
        {
            List<RDD_OSOR> _RDD_OSOR = new List<RDD_OSOR>();
            _RDD_OSOR = SalesOrder_DBOperation.Get_SalesOrder_List(DBName,User.Identity.Name, pagesize, pageno, psearch);
            return Json(new { data = _RDD_OSOR }, JsonRequestBehavior.AllowGet);
        }

        public ActionResult Get_Rec_SalesOrder(string dbname, string so_id)
        {
            ContentResult retVal = null;
            DataSet DS;
            try
            {
                DS = SalesOrder_DBOperation.Get_Rec_SalesOrder(dbname, so_id);

                if (DS.Tables.Count > 0)
                {
                    retVal = Content(JsonConvert.SerializeObject(DS), "application/json");
                }
                return retVal;

            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public ActionResult Post_SalesOrder_InTo_SAP(string dbname, string _so_id)
        {
            ContentResult retVal = null;
            DataSet DS;
            try
            {
                DS = SalesOrder_DBOperation.Post_SalesOrder_InTo_SAP(dbname, _so_id);

                if (DS.Tables.Count > 0)
                {
                    retVal = Content(JsonConvert.SerializeObject(DS), "application/json");
                }
                return retVal;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
    }
}