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
using System.Data.OleDb;

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

        [HttpPost]
        public ActionResult ImportExcelSalesOrder(HttpPostedFileBase file)
        {            
            ContentResult retVal = null;
            DataSet DS = new DataSet();            
            DataTable dt = new DataTable();
            string filePath = string.Empty;

            try
            {
                if (file != null)
                {
                    string path = Server.MapPath("~/Uploads/");
                    if (!Directory.Exists(path))
                    {
                        Directory.CreateDirectory(path);
                    }

                    filePath = path + Path.GetFileName(file.FileName);
                    string extension = Path.GetExtension(file.FileName);
                    file.SaveAs(filePath);

                    string conString = string.Empty;

                    switch (extension)
                    {
                        case ".xls": //Excel 97-03.
                            conString = "Provider=Microsoft.Jet.OLEDB.4.0;Data Source=" + filePath + ";Extended Properties='Excel 8.0;HDR=YES'";
                            break;
                        case ".xlsx": //Excel 07 and above.
                            conString = "Provider=Microsoft.ACE.OLEDB.12.0;Data Source=" + filePath + ";Extended Properties='Excel 8.0;HDR=YES'";
                            break;
                    }
                    conString = string.Format(conString, filePath);

                    using (OleDbConnection connExcel = new OleDbConnection(conString))
                    {
                        using (OleDbCommand cmdExcel = new OleDbCommand())
                        {
                            using (OleDbDataAdapter odaExcel = new OleDbDataAdapter())
                            {
                                cmdExcel.Connection = connExcel;

                                //Get the name of First Sheet.
                                connExcel.Open();
                                DataTable dtExcelSchema;
                                dtExcelSchema = connExcel.GetOleDbSchemaTable(OleDbSchemaGuid.Tables, null);
                                string sheetName = dtExcelSchema.Rows[0]["TABLE_NAME"].ToString();
                                connExcel.Close();

                                //Read Data from First Sheet.
                                connExcel.Open();
                                cmdExcel.CommandText = "SELECT * From [" + sheetName + "]";
                                odaExcel.SelectCommand = cmdExcel;
                                odaExcel.Fill(dt);
                                connExcel.Close();
                                DS.Tables.Add(dt);
                            }
                        }
                    }

                }
                retVal = Content(JsonConvert.SerializeObject(DS), "application/json");
            }
            catch (Exception ex)
            {
                retVal= Content(ex.Message,"application/json");
            }

            return retVal;
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

        public ActionResult Get_BindDDLPayMethod(string dbname, string payterms)
        {
            ContentResult retVal = null;
            DataSet DS;

            try
            {
                DS = SalesOrder_DBOperation.Get_BindDDLPayMethod(dbname, payterms);

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

        public ActionResult Save_SalesOrder(string model, string model1, string model2, string dbname)
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

                        js = new JavaScriptSerializer();
                        RDD_SOR2[] PayDetail = js.Deserialize<RDD_SOR2[]>(model2);

                        if (Header.Length > 0)
                        {
                            SqlCommand cmd = new SqlCommand();
                            string Dbname = Header[0].DBName.ToString();
                            double SorDocTotal = Convert.ToDouble(Header[0].DocTotal.ToString());
                            string loginuser = User.Identity.Name;
                            string CardCode = Header[0].CardCode.ToString();

                            DataSet ds = SalesOrder_DBOperation.CheckCreditLimit(Dbname, SorDocTotal, loginuser, CardCode);
                            string Dsmsg = ds.Tables[0].Rows[0]["ReturnMsg"].ToString();
                            if (Dsmsg != "Success")
                            {
                                Result = "False";
                            }
                            else
                            {
                                Result = "True";
                                cmd = new SqlCommand();
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
                                cmd.Parameters.Add("@SlpName", SqlDbType.VarChar).Value = Header[0].SlpName.ToString();
                                cmd.Parameters.Add("@DocCur", SqlDbType.VarChar).Value = Header[0].DocCur.ToString();

                                //cmd.Parameters.Add("@Pay_Method_1", SqlDbType.VarChar).Value = Header[0].Pay_Method_1.ToString();
                                //cmd.Parameters.Add("@Rcpt_check_No_1", SqlDbType.VarChar).Value = Header[0].Rcpt_check_No_1.ToString();

                                //if (Header[0].Rcpt_check_Date_1.ToString() != null)
                                //    cmd.Parameters.Add("@Rcpt_check_Date_1", SqlDbType.DateTime).Value = Header[0].Rcpt_check_Date_1;

                                //cmd.Parameters.Add("@Remarks_1", SqlDbType.VarChar).Value = Header[0].Remarks_1.ToString();
                                //cmd.Parameters.Add("@Curr_1", SqlDbType.VarChar).Value = Header[0].Curr_1.ToString();
                                //cmd.Parameters.Add("@Amount_1", SqlDbType.Float).Value = Convert.ToDouble(Header[0].Amount_1.ToString());

                                //if (Header[0].Pay_Method_2 != null)
                                //{
                                //    if (Header[0].Pay_Method_2.ToString() != "" && Header[0].Pay_Method_2.ToString() != "0")
                                //    {

                                //        cmd.Parameters.Add("@Pay_Method_2", SqlDbType.VarChar).Value = Header[0].Pay_Method_2.ToString();
                                //        cmd.Parameters.Add("@Rcpt_check_No_2", SqlDbType.VarChar).Value = Header[0].Rcpt_check_No_2.ToString();

                                //        if (Header[0].Rcpt_check_Date_2.ToString() != null)
                                //            cmd.Parameters.Add("@Rcpt_check_Date_2", SqlDbType.DateTime).Value = Header[0].Rcpt_check_Date_2;


                                //        cmd.Parameters.Add("@Remarks_2", SqlDbType.VarChar).Value = Header[0].Remarks_2.ToString();
                                //        cmd.Parameters.Add("@Curr_2", SqlDbType.VarChar).Value = Header[0].Curr_2.ToString();
                                //        cmd.Parameters.Add("@Amount_2", SqlDbType.Float).Value = Convert.ToDouble(Header[0].Amount_2.ToString());

                                //    }
                                //}

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

                                if (SO_ID != 0)
                                {
                                    if (PayDetail.Length > 0)
                                    {
                                        //string sql = "Delete From [RDD_SOR2] Where SO_ID=" + SO_ID.ToString();
                                        // Db.myExecuteSQL(sql);
                                        //string sql1 = "Delete from [RDD_PDCEntry] where SO_ID=" + SO_ID.ToString();
                                        //Db.myExecuteSQL(sql1);

                                        for (int i = 0; i < PayDetail.Length; i++)
                                        {
                                            cmd = new SqlCommand();

                                            cmd.CommandType = CommandType.StoredProcedure;
                                            cmd.CommandText = "RDD_SOR2_Insert_Update_Records";
                                            cmd.Connection = connection;
                                            cmd.Transaction = transaction;

                                            cmd.Parameters.Add("@Pay_Line_Id", SqlDbType.BigInt).Value = PayDetail[i].Pay_Line_Id.ToString();
                                            cmd.Parameters.Add("@SO_ID", SqlDbType.Int).Value = SO_ID;
                                            cmd.Parameters.Add("@Base_Obj", SqlDbType.NVarChar).Value = PayDetail[i].Base_Obj.ToString();
                                            cmd.Parameters.Add("@Base_Id", SqlDbType.NVarChar).Value = PayDetail[i].Base_Id.ToString();
                                            cmd.Parameters.Add("@Base_LinId", SqlDbType.NVarChar).Value = PayDetail[i].Base_LinId.ToString();
                                            cmd.Parameters.Add("@Pay_Method_Id", SqlDbType.NVarChar).Value = PayDetail[i].Pay_Method_Id.ToString();
                                            cmd.Parameters.Add("@Pay_Method", SqlDbType.NVarChar).Value = PayDetail[i].Pay_Method.ToString();
                                            cmd.Parameters.Add("@PDCType", SqlDbType.NVarChar).Value = PayDetail[i].Pdc_Type_Id.ToString();
                                            cmd.Parameters.Add("@Rcpt_Check_No", SqlDbType.NVarChar).Value = PayDetail[i].Rcpt_Check_No.ToString();
                                            cmd.Parameters.Add("@ExchangeRate", SqlDbType.Float).Value = PayDetail[i].ExchangeRate.ToString();
                                            cmd.Parameters.Add("@Bank_Code", SqlDbType.NVarChar).Value = PayDetail[i].Bank_Code.ToString();
                                            cmd.Parameters.Add("@Bank_Name", SqlDbType.NVarChar).Value = PayDetail[i].Bank_Name.ToString();
                                            if (PayDetail[i].Rcpt_Check_Date.ToString() != null)
                                                cmd.Parameters.Add("@Rcpt_Check_Date", SqlDbType.NVarChar).Value = PayDetail[i].Rcpt_Check_Date;

                                            cmd.Parameters.Add("@Curr_Id", SqlDbType.NVarChar).Value = PayDetail[i].Curr_Id.ToString();
                                            cmd.Parameters.Add("@Currency", SqlDbType.NVarChar).Value = PayDetail[i].Currency.ToString();
                                            cmd.Parameters.Add("@PDCAmount", SqlDbType.Float).Value = PayDetail[i].PDCAmount.ToString();
                                            cmd.Parameters.Add("@Rcpt_Check_Amt", SqlDbType.Float).Value = PayDetail[i].Rcpt_Check_Amt.ToString();
                                            cmd.Parameters.Add("@Allocated_Amt", SqlDbType.Float).Value = PayDetail[i].Allocated_Amt.ToString();
                                            cmd.Parameters.Add("@Balance_Amt", SqlDbType.Float).Value = PayDetail[i].Balance_Amt.ToString();
                                            cmd.Parameters.Add("@Remark", SqlDbType.NVarChar).Value = PayDetail[i].Remark.ToString();
                                            cmd.Parameters.Add("@EntryIde", SqlDbType.BigInt).Value = PayDetail[i].EntryId.ToString();
                                            cmd.Parameters.Add("@Editflag",SqlDbType.NVarChar).Value= Header[0].EditFlag.ToString();
                                            cmd.ExecuteNonQuery();

                                            cmd.Dispose();
                                        }
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

                                retVal = Content(JsonConvert.SerializeObject(result_ds), "application/json");
                                //Result = JsonUtil.ToJSONString(result_ds.Tables[0]);

                                transaction.Commit();
                            }
                            else
                            {
                                t1.Rows.Add("False", Dsmsg);
                                result_ds.Tables.Add(t1);
                                retVal = Content(JsonConvert.SerializeObject(result_ds), "application/json");

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
            _RDD_OSOR = SalesOrder_DBOperation.Get_SalesOrder_List(DBName, User.Identity.Name, pagesize, pageno, psearch);
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

        //public ActionResult Get_DeleteRecord(string so_id, string dbname,string cardcode,string model2)
        //{
        //    ContentResult retVal = null;
        //    DataSet DS;
        //    try
        //    {
        //        DS = SalesOrder_DBOperation.Get_DeleteRecord(so_id, dbname, cardcode, model2);

        //        if (DS.Tables.Count > 0)
        //        {
        //            retVal = Content(JsonConvert.SerializeObject(DS), "application/json");
        //        }
        //        return retVal;

        //    }
        //    catch (Exception ex)
        //    {
        //        throw ex;
        //    }

        //}

        public ActionResult Get_DeleteRecord(string so_id, string dbname, string cardcode, string model2)
        {
            ContentResult retVal = null;
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
                       
                        //DataSet DS;
                        JavaScriptSerializer js = new JavaScriptSerializer();

                        js = new JavaScriptSerializer();
                        RDD_SOR2[] PayDetail = js.Deserialize<RDD_SOR2[]>(model2);
                        SqlCommand cmd = new SqlCommand();
                        if (PayDetail.Length > 0)
                        {                            
                            for (int i = 0; i < PayDetail.Length; i++)
                            {
                                string Pdctype = PayDetail[i].Pdc_Type_Id.ToString();                                
                                string Bankcode= PayDetail[i].Bank_Code.ToString();
                                string ChqRefno= PayDetail[i].Rcpt_Check_No.ToString();
                                long Soid = Convert.ToInt64(so_id);
                                DataSet DS = Db.myGetDS("Execute RDD_Doc_Delete_Record '" + dbname + "','" + Soid + "','" + Pdctype + "','" + cardcode + "','" + Bankcode + "','" + ChqRefno + "'");
                                if (DS.Tables.Count > 0)
                                {
                                    retVal = Content(JsonConvert.SerializeObject(DS), "application/json");
                                }
                               
                            }
                        }
                    }
                    catch (Exception)
                    {
                        throw;
                    }
                    
                }
                return retVal;
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

        public ActionResult GetDefaultPayMode(string dbname, string cardcode)
        {
            string retVal = string.Empty;
            DataSet DS;
            try
            {
                DS = SalesOrder_DBOperation.GetDefaultPayMode(dbname, cardcode);
                return Content(JsonConvert.SerializeObject(DS), "application/json");

            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public ActionResult Get_CurrencyList(string dbname)
        {
            ContentResult retVal = null;
            DataSet DS;

            try
            {
                DS = SalesOrder_DBOperation.Get_CurrencyList(dbname);

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

        public ActionResult GetBankList(string prefix, string dbname)
        {
            string retVal = string.Empty;
            SqlDataReader rdr;
            List<string> customers = new List<string>();

            try
            {
                rdr = SalesOrder_DBOperation.GetBankList(prefix, dbname);

                while (rdr.Read())
                {
                    customers.Add(string.Format("{0}#{1}", rdr["AcctName"], rdr["AcctCode"]));
                }
                return Content(JsonConvert.SerializeObject(customers.ToArray()), "application/json");

            }
            catch (Exception ex)
            {
                throw ex;
            }

        }

        public ActionResult GetDetailsForBG(string dbname,string Pdctype,string Chequeno, string cardcode)
        {
            string retVal = string.Empty;
            DataSet DS;
            try
            {
                DS = SalesOrder_DBOperation.GetDetailsForBG(dbname, Pdctype, Chequeno, cardcode);
                return Content(JsonConvert.SerializeObject(DS), "application/json");
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public ActionResult GetDetailsOfPDC(string dbname, string cardcode, string chqno, string bankcode)
        {
            string retVal = string.Empty;
            DataSet DS;
            try
            {
                DS = SalesOrder_DBOperation.GetDetailsOfPDC(dbname, cardcode, chqno, bankcode);
                return Content(JsonConvert.SerializeObject(DS), "application/json");
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public ActionResult GetRcptNoForUpdatePayment(string dbname, string cardcode, string chequeno, string bankcode,int entryid)
        {
            string retVal = string.Empty;
            DataSet DS;
            try
            {
                DS = SalesOrder_DBOperation.GetRcptNoForUpdatePayment(dbname, cardcode, chequeno, bankcode, entryid);
                return Content(JsonConvert.SerializeObject(DS), "application/json");
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
    }
}