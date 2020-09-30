using System;
using Ad.Json;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Data;
using System.IO;
using System.Web.Services;
using System.Data.SqlClient;
using System.Web.Script.Serialization;
using System.Web.Security;


public partial class IntranetNew_Orders_SalesOrder : System.Web.UI.Page
{
    public static string sCode = "", sIPAdress = "", sServerName = "", sDBName = "", sDBPassword = "", sDBUserName = "", sB1Password = "", sB1UserName = "";
    public static string SAPServer = "", LICServer = "";
    public static SAPbobsCOM.Company mCompany;

    protected void Page_Load(object sender, EventArgs e)
    {
        try
        {
            lblMsg.Text = "";
            // select CurrCode from SAPAE.dbo.OCRN -- Currencies
            if (!IsPostBack)
            {
                if (string.IsNullOrEmpty(myGlobal.loggedInUser()))
                {
                    FormsAuthentication.SignOut();
                    Response.Redirect("~/Login.aspx");
                }
                // Bind_DBCombobox();
                //SetInitialRow();

                txtPostingDate.Text = DateTime.Now.ToString("dd/MM/yyyy");
                txtDelDate.Text = DateTime.Now.AddDays(30).ToString("dd/MM/yyyy");
                txtCreatedBy.Text = myGlobal.loggedInUser();
                //DataTable dummy = new DataTable();
                //dummy.Columns.Add("itemcode");
                //dummy.Columns.Add("description");
                //dummy.Columns.Add("qty");
                //dummy.Rows.Add();
                //gvItem.DataSource = dummy;
                //gvItem.DataBind();

            }
        }
        catch (Exception Ex)
        {
            lblMsg.Text = "Error occured in Page_Load : " + Ex.Message;
        }

    }

    [WebMethod]
    public static string GetUserDataBase(string prefix, string usercode)
    {
        string retVal = string.Empty;
        try
        {
            Db.constr = myGlobal.getAppSettingsDataForKey("tejSAP");
            string sql = " Exec SO_Get_UserWise_DBList '" + usercode + "','" + prefix + "'";
            DataSet DS = Db.myGetDS(sql);

            if (DS.Tables.Count > 0)
            {
                retVal = Ad.Json.JsonUtil.ToJSONString(DS.Tables[0]);
            }

        }
        catch (Exception ex)
        {
            throw ex;
        }
        return retVal;

    }

    [WebMethod]
    public static string Get_BindDLList(string dbname)
    {
        string retVal = string.Empty;
        try
        {
            Db.constr = myGlobal.getAppSettingsDataForKey("tejSAP");
            string sql = " Exec SO_Get_RDDProject_List '" + dbname + "'";
            DataSet DS = Db.myGetDS(sql);

            if (DS.Tables.Count > 0)
            {
                retVal = Ad.Json.JsonUtil.ToJSONString(DS);
            }

        }
        catch (Exception ex)
        {
            throw ex;
        }
        return retVal;

    }

    [WebMethod]
    public static string[] Get_CurrentLoginUser(string dbname)
    {
       
            List<string> _loginuser = new List<string>();

            string loginuser = myGlobal.loggedInUser();
            _loginuser.Add(string.Format("{0}", loginuser));

            return _loginuser.ToArray();
       
       
    }

    [WebMethod]
    public static string Get_ActiveOPGSelloutList(string basedb, string rebatedb, string itemcode)
    {
        string retVal = string.Empty;
        try
        {
            Db.constr = myGlobal.getAppSettingsDataForKey("tejSAP");
            string sql = " Exec SO_Get_ActiveOPGSelloutList '" + basedb + "','" + rebatedb + "','" + itemcode + "'";
            DataSet DS = Db.myGetDS(sql);

            if (DS.Tables.Count > 0)
            {
                retVal = Ad.Json.JsonUtil.ToJSONString(DS);
            }

        }
        catch (Exception ex)
        {
            throw ex;
        }
        return retVal;

    }

    [WebMethod]
    public static string Get_GPAndGPPer(string dbname, string itemcode, string warehouse, string qtysell, string pricesell, string curr, string opgrebateid)
    {
        string retVal = string.Empty;

        try
        {

            DataSet DS = Db.myGetDS("Execute SO_Get_GPAndGPPer '" + dbname + "','" + itemcode + "','" + warehouse + "'," + qtysell + "," + pricesell + ",'" + curr + "','" + opgrebateid + "'");

            if (DS.Tables.Count > 0)
            {
                retVal = Ad.Json.JsonUtil.ToJSONString(DS.Tables[0]);
            }


        }
        catch (Exception ex)
        {
            throw ex;
        }
        return retVal;
    }

    [WebMethod]
    public static string[] GetCustomers(string prefix, string dbname, string field)
    {

        List<string> customers = new List<string>();

        Db.constr = myGlobal.getAppSettingsDataForKey("tejSAP");


        string qry = " Exec SO_GetList_Customers '" + prefix + "','" + dbname + "','" + field + "'";

        SqlDataReader rdr = Db.myGetReader(qry);
        while (rdr.Read())
        {
            customers.Add(string.Format("{0}#{1}", rdr["CardName"], rdr["CardCode"]));
        }

        return customers.ToArray();

    }

    [WebMethod]
    public static string[] Get_CustomersDue_Info(string dbname, string cardcode)
    {

        List<string> customers_due_info = new List<string>();

        Db.constr = myGlobal.getAppSettingsDataForKey("tejSAP");


        string qry = " Exec SO_Get_CustomerDue '" + cardcode + "','" + dbname + "'";

        SqlDataReader rdr = Db.myGetReader(qry);
        while (rdr.Read())
        {
            customers_due_info.Add(string.Format("{0}#{1}#{2}#{3}#{4}#{5}#{6}#{7}#{8}#{9}", rdr["CL"], rdr["0-30"], rdr["31-45"], rdr["46-60"], rdr["61-90"], rdr["91"], rdr["TrnsStatus"], rdr["CLStatus"], rdr["PayTerms"], rdr["ExtraDays"]));
        }

        return customers_due_info.ToArray();

    }

    [WebMethod]
    public static string[] Get_PayTerms_Days(string dbname, string groupnum)
    {

        List<string> PayTerms_Days = new List<string>();

        Db.constr = myGlobal.getAppSettingsDataForKey("tejSAP");


        string qry = " Select ExtraDays From [" + dbname + "].[dbo].[OCTG] Where GroupNum= " + groupnum;

        SqlDataReader rdr = Db.myGetReader(qry);
        while (rdr.Read())
        {
            PayTerms_Days.Add(string.Format("{0}", rdr["ExtraDays"]));
        }

        return PayTerms_Days.ToArray();

    }

    [WebMethod]
    public static string[] GetItemList(string prefix, string dbname)
    {

        List<string> Items = new List<string>();

        Db.constr = myGlobal.getAppSettingsDataForKey("tejSAP");


        string qry = " Exec SO_GetList_ItemCode '" + prefix + "','" + dbname + "'";

        SqlDataReader rdr = Db.myGetReader(qry);
        while (rdr.Read())
        {
            Items.Add(string.Format("{0}${1}${2}${3}${4}${5}${6}", rdr["ItemCode"], rdr["ItemName"], rdr["DfltWH"], rdr["VatGourpSa"], rdr["OnHand"], rdr["ActalQty"], rdr["Rate"]));
        }

        return Items.ToArray();

    }

    [WebMethod]
    public static string Get_WarehouseQty(string itemcode, string whscode, string dbname)
    {
        string retVal = string.Empty;

        try
        {

            DataSet DS = Db.myGetDS(" Select Convert(Numeric(10,2),T1.OnHand) OnHand,Convert(Numeric(10,2),T1.OnHand-(T1.IsCommited+T1.OnOrder)) ActalQty  From [" + dbname + "].[dbo].[OITW] T1 Where T1.ItemCode='" + itemcode + "' And T1.WhsCode='" + whscode + "'");

            if (DS.Tables.Count > 0)
            {
                retVal = Ad.Json.JsonUtil.ToJSONString(DS.Tables[0]);
            }


        }
        catch (Exception ex)
        {
            throw ex;
        }
        return retVal;
    }

    [WebMethod]
    public static string Get_TaxCodeRate(string taxcode, string dbname)
    {
        string retVal = string.Empty;

        try
        {

            DataSet DS = Db.myGetDS(" Select Convert(Numeric(10,2),Rate) Rate  From [" + dbname + "].[dbo].[OVTG] T1 Where T1.Code='" + taxcode + "'");

            if (DS.Tables.Count > 0)
            {
                retVal = Ad.Json.JsonUtil.ToJSONString(DS.Tables[0]);
            }


        }
        catch (Exception ex)
        {
            throw ex;
        }
        return retVal;
    }

    [WebMethod]
    public static WhsLists[] GetWarehouseList(string dbname)
    {
        List<WhsLists> WhsLists = new List<WhsLists>();
        try
        {
            DataSet DS = Db.myGetDS(" EXEC SO_Get_RDDProject_List '" + dbname + "' ");

            if (DS.Tables.Count > 0)
            {
                foreach (DataRow dtrow in DS.Tables[7].Rows)
                {
                    WhsLists WhsList = new WhsLists();
                    WhsList.WhsCode = dtrow["Code"].ToString();
                    WhsLists.Add(WhsList);
                }
            }
        }
        catch { }

        return WhsLists.ToArray();
    }

    //=================================================================================/
    [WebMethod]
    public static string GetDataModel(string dbname)
    {
        string retVal = string.Empty;
        //int userId = Convert.ToInt32(HttpContext.Current.Session["UserID"]);
        try
        {


            DataSet DS = Db.myGetDS(" Select Top 10 CardCode,CardName From [" + dbname + "].[dbo].[OCRD] ");

            if (DS.Tables.Count > 0)
            {
                retVal = Ad.Json.JsonUtil.ToJSONString(DS.Tables[0]);
            }


        }
        catch (Exception ex)
        {
            throw ex;
        }
        return retVal;
    }
    //=================================================================================/Search Inquiry

    [WebMethod]
    public static string Save_SO(string model, string model1, string dbname)
    {
        string Result = string.Empty;
        string retVal = string.Empty;
        Int32 SO_ID = 0;
        DataSet result_ds = new DataSet();
        DataTable t1 = new DataTable("table");
        t1.Columns.Add("Result");
        t1.Columns.Add("Message");


        Result = "True";

        Db.constr = myGlobal.getAppSettingsDataForKey("tejSAP");

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

                    OSOR[] Header = js.Deserialize<OSOR[]>(model);

                    js = new JavaScriptSerializer();
                    SOR1[] ItemDetail = js.Deserialize<SOR1[]>(model1);

                    if (Header.Length > 0)
                    {
                        SqlCommand cmd = new SqlCommand();

                        cmd.CommandType = CommandType.StoredProcedure;
                        cmd.CommandText = "SO_OSOR_Insert_Update_Records";
                        cmd.Connection = connection;
                        cmd.Transaction = transaction;

                        string CreatedBy = Header[0].CreatedBy.ToString();
                        if (string.IsNullOrEmpty(CreatedBy))
                        {
                            CreatedBy = myGlobal.loggedInUser();
                        }

                        cmd.Parameters.Add("@SO_ID", SqlDbType.Int).Value = Convert.ToInt16(Header[0].SO_ID.ToString());
                        cmd.Parameters.Add("@DBName", SqlDbType.VarChar).Value = Header[0].DBName.ToString();
                        cmd.Parameters.Add("@PostingDate", SqlDbType.VarChar).Value = Header[0].PostingDate.ToString();
                        cmd.Parameters.Add("@DeliveryDate", SqlDbType.VarChar).Value = Header[0].DeliveryDate.ToString();
                        cmd.Parameters.Add("@DocStatus", SqlDbType.VarChar).Value = Header[0].DocStatus.ToString();
                        cmd.Parameters.Add("@AprovedBy", SqlDbType.VarChar).Value = Header[0].AprovedBy.ToString();
                        cmd.Parameters.Add("@CreatedBy", SqlDbType.VarChar).Value = CreatedBy;
                        cmd.Parameters.Add("@CardCode", SqlDbType.VarChar).Value = Header[0].CardCode.ToString();
                        cmd.Parameters.Add("@CardName", SqlDbType.NVarChar).Value = Header[0].CardName.ToString();
                        cmd.Parameters.Add("@RefNo", SqlDbType.VarChar).Value = Header[0].RefNo.ToString();
                        cmd.Parameters.Add("@RDD_Project", SqlDbType.VarChar).Value = Header[0].RDD_Project.ToString();
                        cmd.Parameters.Add("@BusinesType", SqlDbType.VarChar).Value = Header[0].BusinesType.ToString();
                        cmd.Parameters.Add("@InvPayTerms", SqlDbType.VarChar).Value = Header[0].InvPayTerms.ToString();
                        cmd.Parameters.Add("@CustPayTerms", SqlDbType.VarChar).Value = Header[0].CustPayTerms.ToString();
                        cmd.Parameters.Add("@Forwarder", SqlDbType.VarChar).Value = Header[0].Forwarder.ToString();
                        cmd.Parameters.Add("@SalesEmp", SqlDbType.VarChar).Value = Header[0].SalesEmp.ToString();
                        //cmd.Parameters.Add("@Credit_Limit", SqlDbType.Float).Value = Header[0].Credit_Limit.ToString().Replace(",", "");
                        //cmd.Parameters.Add("@Aging_0_30", SqlDbType.Float).Value = Header[0].Aging_0_30.ToString().Replace(",", "");
                        //cmd.Parameters.Add("@Aging_31_45", SqlDbType.Float).Value = Header[0].Aging_31_45.ToString().Replace(",", "");
                        //cmd.Parameters.Add("@Aging_46_60", SqlDbType.Float).Value = Header[0].Aging_46_60.ToString().Replace(",", "");
                        //cmd.Parameters.Add("@Aging_61_90", SqlDbType.Float).Value = Header[0].Aging_61_90.ToString().Replace(",", "");
                        //cmd.Parameters.Add("@Aging_91_Abv", SqlDbType.Float).Value = Header[0].Aging_91_Abv.ToString().Replace(",", "");
                        //cmd.Parameters.Add("@TRNS_Status", SqlDbType.VarChar).Value = Header[0].TRNS_Status.ToString();
                        //cmd.Parameters.Add("@CL_Status", SqlDbType.VarChar).Value = Header[0].CL_Status.ToString();
                        cmd.Parameters.Add("@Pay_Method_1", SqlDbType.VarChar).Value = Header[0].Pay_Method_1.ToString();
                        cmd.Parameters.Add("@Rcpt_check_No_1", SqlDbType.VarChar).Value = Header[0].Rcpt_check_No_1.ToString();

                        if (Header[0].Rcpt_check_Date_1.ToString() == "")
                            cmd.Parameters.Add("@Rcpt_check_Date_1", SqlDbType.VarChar).Value = DBNull.Value;
                        else
                            cmd.Parameters.Add("@Rcpt_check_Date_1", SqlDbType.VarChar).Value = Header[0].Rcpt_check_Date_1.ToString();

                        cmd.Parameters.Add("@Remarks_1", SqlDbType.VarChar).Value = Header[0].Remarks_1.ToString();
                        cmd.Parameters.Add("@Curr_1", SqlDbType.VarChar).Value = Header[0].Curr_1.ToString();
                        cmd.Parameters.Add("@Amount_1", SqlDbType.Float).Value = Convert.ToDouble(Header[0].Amount_1.ToString());

                        if (Header[0].Pay_Method_2.ToString() != "" || Header[0].Pay_Method_2.ToString() != "0")
                        {

                            cmd.Parameters.Add("@Pay_Method_2", SqlDbType.VarChar).Value = Header[0].Pay_Method_2.ToString();
                            cmd.Parameters.Add("@Rcpt_check_No_2", SqlDbType.VarChar).Value = Header[0].Rcpt_check_No_2.ToString();

                            if (Header[0].Rcpt_check_Date_2.ToString() == "")
                                cmd.Parameters.Add("@Rcpt_check_Date_2", SqlDbType.VarChar).Value = DBNull.Value;
                            else
                                cmd.Parameters.Add("@Rcpt_check_Date_2", SqlDbType.VarChar).Value = Header[0].Rcpt_check_Date_2.ToString();

                            cmd.Parameters.Add("@Remarks_2", SqlDbType.VarChar).Value = Header[0].Remarks_2.ToString();
                            cmd.Parameters.Add("@Curr_2", SqlDbType.VarChar).Value = Header[0].Curr_2.ToString();
                            cmd.Parameters.Add("@Amount_2", SqlDbType.Float).Value = Convert.ToDouble(Header[0].Amount_2.ToString());

                        }

                        cmd.Parameters.Add("@Total_Bef_Tax", SqlDbType.Float).Value = Convert.ToDouble(Header[0].Total_Bef_Tax.ToString());
                        cmd.Parameters.Add("@Total_Tx", SqlDbType.Float).Value = Convert.ToDouble(Header[0].Total_Tx.ToString());
                        cmd.Parameters.Add("@DocTotal", SqlDbType.Float).Value = Convert.ToDouble(Header[0].DocTotal.ToString());
                        cmd.Parameters.Add("@GP", SqlDbType.Float).Value = Convert.ToDouble(Header[0].GP.ToString());
                        cmd.Parameters.Add("@GP_Per", SqlDbType.Float).Value = Convert.ToDouble(Header[0].GP_Per.ToString());
                        cmd.Parameters.Add("@Remarks", SqlDbType.VarChar).Value = Header[0].Remarks.ToString();
                        cmd.Parameters.Add("@Validate_Status", SqlDbType.VarChar).Value = Header[0].Validate_Status.ToString();
                        cmd.Parameters.Add("@Post_SAP", SqlDbType.VarChar).Value = Header[0].Post_SAP.ToString();
                        if (Header[0].CreatedOn == null)
                            cmd.Parameters.Add("@CreatedOn", SqlDbType.VarChar).Value = DBNull.Value;
                        else
                            cmd.Parameters.Add("@CreatedOn", SqlDbType.VarChar).Value = Header[0].CreatedOn.ToString();

                        if (Header[0].LastUpdatedOn == "")
                            cmd.Parameters.Add("@LastUpdatedOn", SqlDbType.VarChar).Value = DBNull.Value;
                        else
                        {
                            string Dt = DateTime.Now.Month.ToString() + "/" + DateTime.Now.Day.ToString() + "/" + DateTime.Now.Year.ToString();
                            cmd.Parameters.Add("@LastUpdatedOn", SqlDbType.VarChar).Value = Dt;// Header[0].LastUpdatedOn.ToString();
                        }

                        if (Header[0].LastUpdatedOn == "")
                            cmd.Parameters.Add("@LastUpdatedBy", SqlDbType.VarChar).Value = DBNull.Value;
                        else
                            cmd.Parameters.Add("@LastUpdatedBy", SqlDbType.VarChar).Value = Header[0].LastUpdatedBy.ToString();

                        cmd.Parameters.Add("@id", SqlDbType.Int).Direction = ParameterDirection.Output;

                        cmd.ExecuteNonQuery();
                        SO_ID = Convert.ToInt32(cmd.Parameters["@id"].Value.ToString());

                        cmd.Dispose();

                        if (SO_ID != 0)
                        {
                            if (ItemDetail.Length > 0)
                            {
                                string sql = "Delete From [SOR1] Where SO_ID=" + SO_ID.ToString();
                                Db.myExecuteSQL(sql);

                                for (int i = 0; i < ItemDetail.Length; i++)
                                {
                                    cmd = new SqlCommand();

                                    cmd.CommandType = CommandType.StoredProcedure;
                                    cmd.CommandText = "SO_SOR1_Insert_Update_Records";
                                    cmd.Connection = connection;
                                    cmd.Transaction = transaction;

                                    cmd.Parameters.Add("@SO_LineId", SqlDbType.BigInt).Value = ItemDetail[i].SO_LineId.ToString();
                                    cmd.Parameters.Add("@SO_ID", SqlDbType.Int).Value = SO_ID;
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

                            Result = JsonUtil.ToJSONString(result_ds.Tables[0]);

                            transaction.Commit();
                        }
                    }
                }
                catch (Exception ex)
                {

                    t1.Rows.Add("False", ex.Message);
                    result_ds.Tables.Add(t1);
                    Result = JsonUtil.ToJSONString(result_ds.Tables[0]);
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

        return Result;
    }

    [WebMethod]
    public static string Get_SalesOrder_List(string dbname, string searchcriteria)
    {
        string retVal = string.Empty;
        //int userId = Convert.ToInt32(HttpContext.Current.Session["UserID"]);
        try
        {
            Db.constr = myGlobal.getAppSettingsDataForKey("tejSAP");
            string Parameters = string.Empty;

            Parameters = "'" + dbname + "'";

            if (searchcriteria == "")
                Parameters = Parameters + ",Null";
            else
                Parameters = Parameters + ",'" + searchcriteria + "'";

            DataSet DS = Db.myGetDS("Execute SO_Get_SalesOrderLsit " + Parameters);

            if (DS.Tables.Count > 0)
            {
                retVal = Ad.Json.JsonUtil.ToJSONString(DS.Tables[0]);
                retVal = retVal.Replace("'", "\"");
            }

        }
        catch (Exception ex)
        {
            throw ex;
        }
        return retVal;
    }

    [WebMethod]
    public static string Get_Rec_SalesOrder(string dbname, string so_id)
    {
        string retVal = string.Empty;
        //int userId = Convert.ToInt32(HttpContext.Current.Session["UserID"]);
        try
        {
            Db.constr = myGlobal.getAppSettingsDataForKey("tejSAP");

            DataSet DS = Db.myGetDS("Execute SO_GetRec_SalesOrder '" + dbname + "'," + so_id);

            if (DS.Tables.Count > 0)
            {
                retVal = Ad.Json.JsonUtil.ToJSONString(DS);
            }


        }
        catch (Exception ex)
        {
            throw ex;
        }
        return retVal;
    }

    [WebMethod]
    public static string Post_SalesOrder_InTo_SAP(string dbname, string _so_id)
    {

        string retVal = string.Empty;
        Int32 SO_ID = 0;
        DataSet result_ds = new DataSet();
        DataTable t1 = new DataTable("table");
        t1.Columns.Add("Result");
        t1.Columns.Add("Message");
        try
        {

            SO_ID = Convert.ToInt32(_so_id);
            retVal = "True";

            Db.constr = myGlobal.getAppSettingsDataForKey("tejSAP");

            DataSet DS = Db.myGetDS("Execute SO_Get_SalesOrderDetail_Posting '" + dbname + "'," + _so_id);

            if (DS.Tables.Count > 0)
            {

                if (ConnectToSAP(dbname) == true)
                {
                    string errItemCodes = "", errMachineNos = "";
                    bool errRowFlag = false;
                    int ErrorCode;
                    string ErrMessage;
                    int RefDocSeries, iOrd;
                    string RefDocNum = "", RefDocDate = "", DocNum = "", Usrid = "";

                    SAPbobsCOM.Documents oSalesOrder;
                    oSalesOrder = (SAPbobsCOM.Documents)mCompany.GetBusinessObject(SAPbobsCOM.BoObjectTypes.oOrders);

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
                    try
                    {
                        if (dbname.Trim().ToUpper() == "SAPAE")
                        {
                            oSalesOrder.UserFields.Fields.Item("U_forwarder").Value = DS.Tables[0].Rows[0]["Forwarder"].ToString();
                        }
                    }
                    catch { }

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
                        mCompany.GetLastError(out ErrorCode, out ErrMessage);
                        t1.Rows.Add("False", "Sales Order Posting Failed");
                        t1.Rows.Add("Err", "ErrCode-" + ErrorCode.ToString() + " - ErrMsg-" + ErrMessage);
                        result_ds.Tables.Add(t1);

                        retVal = JsonUtil.ToJSONString(result_ds.Tables[0]);

                        System.Runtime.InteropServices.Marshal.ReleaseComObject(oSalesOrder);
                    }
                    else
                    {
                        string dockey = mCompany.GetNewObjectKey();
                        string docType = mCompany.GetNewObjectType();

                        DataSet oDs;

                        if (docType == "112")
                        {
                            oDs = Db.myGetDS("Select DocNum From [" + dbname + "].[dbo].[ODRF] Where DocEntry=" + dockey + " And ObjType='17' And U_SO_ID=" + DS.Tables[0].Rows[0]["SO_ID"].ToString());

                            Db.myExecuteSQL("Update OSOR Set IsDraft='Y',DocStatus='Draft', Post_SAP='Y' ,ObjType='" + docType + "', SAP_DocEntry=" + dockey + ", SAP_DocNum='" + oDs.Tables[0].Rows[0]["DocNum"].ToString() + "' Where SO_ID=" + _so_id);

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

                        retVal = JsonUtil.ToJSONString(result_ds.Tables[0]);

                        System.Runtime.InteropServices.Marshal.ReleaseComObject(oSalesOrder);
                    }
                }
            }
        }
        catch (Exception ex)
        {
            throw ex;
        }
        return retVal;
    }

    private static bool ConnectToSAP(string sDBName)
    {
        try
        {
            string SAPconstring = string.Empty;

            if (sDBName == "SAPAE")
            {
                SAPconstring = myGlobal.getAppSettingsDataForKey("SAPCompanyConnectCredsSAPAE");
            }
            else if (sDBName == "SAPKE")
            {
                SAPconstring = myGlobal.getAppSettingsDataForKey("SAPCompanyConnectCredsKE");
            }
            else if (sDBName == "SAPTZ")
            {
                SAPconstring = myGlobal.getAppSettingsDataForKey("SAPCompanyConnectCredsTZ");
            }
            else if (sDBName == "SAPUG")
            {
                SAPconstring = myGlobal.getAppSettingsDataForKey("SAPCompanyConnectCredsUG");
            }
            else if (sDBName == "SAPZM")
            {
                SAPconstring = myGlobal.getAppSettingsDataForKey("SAPCompanyConnectCredsZM");
            }

            if (string.IsNullOrEmpty(SAPconstring) == false)
            {
                string[] connElements = SAPconstring.Split(';');

                SAPServer = connElements[0];
                sDBUserName = connElements[1];
                sDBPassword = connElements[2];
                sDBName = connElements[3];
                LICServer = connElements[6];  // "192.168.56.131:30000";// 
                sB1UserName = connElements[4];
                sB1Password = connElements[5];

                mCompany = new SAPbobsCOM.Company();

                mCompany.UseTrusted = false;
                mCompany.DbServerType = SAPbobsCOM.BoDataServerTypes.dst_MSSQL2008;
                mCompany.Server = SAPServer;
                mCompany.LicenseServer = LICServer;
                mCompany.CompanyDB = sDBName;
                mCompany.UserName = sB1UserName;
                mCompany.Password = sB1Password;
                mCompany.language = SAPbobsCOM.BoSuppLangs.ln_English;
                mCompany.DbUserName = sDBUserName;
                mCompany.DbPassword = sDBPassword;


                int iErrCode = 0;
                int iCounter = 0;

                do
                {

                    iErrCode = mCompany.Connect();
                    if (iErrCode != 0)
                    {
                        iCounter = iCounter + 1;
                        if (iCounter > 5)
                        {
                            break;
                        }
                        System.Threading.Thread.Sleep(iCounter * 50);
                        GC.Collect();
                    }
                    else
                    {
                        System.Threading.Thread.Sleep(iCounter * 50);
                        GC.Collect();

                        break;
                    }
                }
                while (mCompany.Connected == false);

                if (iErrCode != 0)
                {
                    string strErr;
                    mCompany.GetLastError(out iErrCode, out strErr);

                    Db.constr = myGlobal.getAppSettingsDataForKey("tejSAP");

                    
                    return false;
                }
                else
                {
                    return true;
                }
            }
            else { return false; }
        }
        catch (Exception ex)
        {
            return false;
        }
    }

    [WebMethod]
    public static string To_GetData_From_Excel(string model1, string dbname)
    {
        string Result = string.Empty;
        string retVal = string.Empty;
        DataSet result_ItemDetails = new DataSet();
        DataTable Tb_Item = new DataTable("table");
        Tb_Item.Columns.Add("pvlineid");
        Tb_Item.Columns.Add("itemcode");
        Tb_Item.Columns.Add("description");
        Tb_Item.Columns.Add("qty");
        Tb_Item.Columns.Add("price");
        Tb_Item.Columns.Add("taxcode");
        Tb_Item.Columns.Add("taxrate");
        Tb_Item.Columns.Add("dis");
        Tb_Item.Columns.Add("total");
        Tb_Item.Columns.Add("whscode");
        Tb_Item.Columns.Add("qtyinwhs");
        Tb_Item.Columns.Add("qtyaval");
        Tb_Item.Columns.Add("opgrefalpha");
        Tb_Item.Columns.Add("gp");
        Tb_Item.Columns.Add("gpper");

        DataTable Tb_Validate = new DataTable("table1");
        Tb_Validate.Columns.Add("rowno");
        Tb_Validate.Columns.Add("result");
        Tb_Validate.Columns.Add("msg");

        try
        {
            Db.constr = myGlobal.getAppSettingsDataForKey("tejSAP");

            JavaScriptSerializer js = new JavaScriptSerializer();

            SOR1[] ItemDetail = js.Deserialize<SOR1[]>(model1);

            if (ItemDetail.Length > 0)
            {
                for (int i = 0; i < ItemDetail.Length; i++)
                {
                    DataSet DS = Db.myGetDS(" Execute SO_Get_ItemData_FromExcel '"+dbname+"','"+ItemDetail[i].ItemCode.ToString()+"','"+ItemDetail[i].WhsCode.ToString()+"',"+ItemDetail[i].Quantity.ToString()+","+ItemDetail[i].UnitPrice.ToString()+",'USD','"+ItemDetail[i].OpgRefAlpha.ToString()+"','"+ItemDetail[i].TaxCode+"'");

                    if (DS.Tables.Count > 0 && DS.Tables.Count == 2)
                    {
                        Tb_Item.Rows.Add("0", ItemDetail[i].ItemCode.ToString(), ItemDetail[i].Description.ToString(), ItemDetail[i].Quantity, ItemDetail[i].UnitPrice, ItemDetail[i].TaxCode, DS.Tables[1].Rows[0]["TaxRate"].ToString(), ItemDetail[i].DiscPer, DS.Tables[1].Rows[0]["Total"], ItemDetail[i].WhsCode, DS.Tables[1].Rows[0]["QtyInWhs"].ToString(), DS.Tables[1].Rows[0]["QtyAval"].ToString(), DS.Tables[1].Rows[0]["opgRebateID"].ToString(), DS.Tables[1].Rows[0]["GPValRowUSD"].ToString(), DS.Tables[1].Rows[0]["GPPercRowUSD"].ToString());
                    }
                    else if (DS.Tables.Count>0 && DS.Tables.Count==1)
                    {
                        Tb_Validate.Rows.Add("Row No-" +(i+1).ToString(),DS.Tables[0].Rows[0]["Result"].ToString(), DS.Tables[0].Rows[0]["Msg"].ToString());
                        Result = DS.Tables[0].Rows[0]["Result"].ToString();
                       
                    }

                }

                if (Result == "False")
                    result_ItemDetails.Tables.Add(Tb_Validate);
                else
                    result_ItemDetails.Tables.Add(Tb_Item);

                retVal = JsonUtil.ToJSONString(result_ItemDetails.Tables[0]);
            }                          

        }
        catch (Exception ex)
        {
            throw ex;
        }
        return retVal;
    }

}
public class WhsLists
{
    public string WhsCode { get; set; }
}


public class OSOR
{
    public int SO_ID { get; set; }
    public string DBName { get; set; }
    public string PostingDate { get; set; }
    public string DeliveryDate { get; set; }
    public string DocStatus { get; set; }
    public string AprovedBy { get; set; }
    public string CreatedBy { get; set; }
    public string CardCode { get; set; }
    public string CardName { get; set; }
    public string RefNo { get; set; }
    public string RDD_Project { get; set; }
    public string BusinesType { get; set; }
    public string InvPayTerms { get; set; }
    public string CustPayTerms { get; set; }
    public string Forwarder { get; set; }
    public string SalesEmp { get; set; }
    public string Credit_Limit { get; set; }
    public string Aging_0_30 { get; set; }
    public string Aging_31_45 { get; set; }
    public string Aging_46_60 { get; set; }
    public string Aging_61_90 { get; set; }
    public string Aging_91_Abv { get; set; }
    public string TRNS_Status { get; set; }
    public string CL_Status { get; set; }
    public string Pay_Method_1 { get; set; }
    public string Rcpt_check_No_1 { get; set; }
    public string Rcpt_check_Date_1 { get; set; }
    public string Remarks_1 { get; set; }
    public string Curr_1 { get; set; }
    public double Amount_1 { get; set; }
    public string Pay_Method_2 { get; set; }
    public string Rcpt_check_No_2 { get; set; }
    public string Rcpt_check_Date_2 { get; set; }
    public string Remarks_2 { get; set; }
    public string Curr_2 { get; set; }
    public string Amount_2 { get; set; }
    public string Total_Bef_Tax { get; set; }
    public string Total_Tx { get; set; }
    public string DocTotal { get; set; }
    public string GP { get; set; }
    public string GP_Per { get; set; }
    public string Remarks { get; set; }
    public string Validate_Status { get; set; }
    public string Post_SAP { get; set; }
    public string CreatedOn { get; set; }
    public string LastUpdatedOn { get; set; }
    public string LastUpdatedBy { get; set; }

}

public class SOR1
{
    public int SO_LineId { get; set; }
    public int SO_ID { get; set; }
    public string ItemCode { get; set; }
    public string Description { get; set; }
    public double Quantity { get; set; }
    public double UnitPrice { get; set; }
    public string TaxCode { get; set; }
    public double TaxRate { get; set; }
    public double DiscPer { get; set; }
    public double LineTotal { get; set; }
    public string WhsCode { get; set; }
    public double QtyInWhs { get; set; }
    public double QtyAval { get; set; }
    public string OpgRefAlpha { get; set; }
    public double GP { get; set; }
    public double GPPer { get; set; }

}


