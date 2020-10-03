using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Drawing;
using System.Data;
using System.Web.Services;
using System.Web.Script.Services;
using System.Data.SqlClient;
using System.Web.Security;

public partial class IntranetNew_DailySalesReport_DSREntry : System.Web.UI.Page
{
    protected void Page_Load(object sender, EventArgs e)
    {
        try
        {
            if (!IsPostBack)
            {
                if (string.IsNullOrEmpty(myGlobal.loggedInUser()))
                {
                    FormsAuthentication.SignOut();
                    Response.Redirect("~/Login.aspx");
                }

                Db.constr = myGlobal.getAppSettingsDataForKey("tejSAP");
                string LoggedInUserName = myGlobal.loggedInUser();

                if (Session["DSR"] != null)
                {
                    Session.Remove("DSR");
                }

                if (Session["AutoVisitId"] != null)
                {
                    Session.Remove("AutoVisitId");
                    Session["AutoVisitId"] = 0;
                }

                if (Request.QueryString["Action"] == "DRAFT")
                {
                    Page.ClientScript.RegisterStartupScript(this.GetType(), "alert", "<script> alert( 'Report is saved as draft successfully'); </script>");
                }
                else if (Request.QueryString["Action"] == "SUBMIT")
                {
                    Page.ClientScript.RegisterStartupScript(this.GetType(), "alert", "<script> alert( 'Report submitted successfully.'); </script>");
                }
                else if (Request.QueryString["Action"] == "SUBMITFailed")
                {
                    Page.ClientScript.RegisterStartupScript(this.GetType(), "alert", "<script> alert( 'Failed to submit report, please retry.'); </script>");
                }
                else if (Request.QueryString["Action"] == "DRAFTFailed")
                {
                    Page.ClientScript.RegisterStartupScript(this.GetType(), "alert", "<script> alert( 'Failed to save draft report, please retry.'); </script>");
                }

                fillgvSummary();
                BindGridAddNew();

                GetVisitFrom_ToDate();
                GetVisitDataByDate();

            }
        }
        catch (Exception ex)
        {
            lblErrorMsg.Text = "Failed: Page Load " + ex.Message;
        }
    }

    protected void GvRptSummary_RowCreated(object sender, GridViewRowEventArgs e)
    {
        if (e.Row.RowType == DataControlRowType.Header)
        {
            GridView HeaderGrid = (GridView)sender;
            GridViewRow HeaderGridRow = new GridViewRow(0, 0, DataControlRowType.Header, DataControlRowState.Insert);

            TableCell HeaderCell = new TableCell();
            HeaderCell.Text = "";
            HeaderCell.ColumnSpan = 2;
            HeaderCell.BorderColor = Color.Black;
            HeaderGridRow.Cells.Add(HeaderCell);

            HeaderCell = new TableCell();
            HeaderCell.Text = "Customer";        
            HeaderCell.ColumnSpan = 3;
            HeaderCell.BorderColor = Color.Black;
            HeaderGridRow.Cells.Add(HeaderCell);


            HeaderCell = new TableCell();
            HeaderCell.Text = "Items Status";           
            HeaderCell.ColumnSpan = 2;
            HeaderCell.BorderColor = Color.Black;
            HeaderGridRow.Cells.Add(HeaderCell);

            HeaderCell = new TableCell();
            HeaderCell.Text = "Prospect";
            HeaderCell.ColumnSpan = 2;
            HeaderCell.BorderColor = Color.Black;
            HeaderGridRow.Cells.Add(HeaderCell);



            GvRptSummary.Controls[0].Controls.AddAt(0, HeaderGridRow);

        }
    }
    [WebMethod]
    public static string[] GetCustomers(string prefix, string dbcountry)
    {
        List<string> customers = new List<string>();

        Db.constr = myGlobal.getAppSettingsDataForKey("tejSAP");

        string qry = "Exec getResellerList '" + dbcountry + "' , '" + prefix + "'";

        SqlDataReader rdr = Db.myGetReader(qry);
        while (rdr.Read())
        {
            customers.Add(string.Format("{0}#{1}", rdr["CardName"].ToString(), rdr["CardCode"].ToString()));
        }

        return customers.ToArray();
    }

    [WebMethod]
    public static string[] GetPersonDetails(string prefix, string dbcustomer, string dbpermeet)
    {
        List<string> detail = new List<string>();
        Db.constr = myGlobal.getAppSettingsDataForKey("tejSAP");
        string qry = "select PersonMet,Email,Designation,ContactNo from DailySalesReports where Company='" + dbcustomer + "' and PersonMet like '%" + prefix + "%'";
        SqlDataReader rdr = Db.myGetReader(qry);
        while (rdr.Read())
        {
            detail.Add(string.Format("{0}#{1}#{2}#{3}", rdr["PersonMet"].ToString(), rdr["Email"].ToString(), rdr["Designation"].ToString(), rdr["ContactNo"].ToString()));
        }
        return detail.ToArray();
    }

    [WebMethod]
    public static string GetCutomerDisRpt(string dbcode)
    {
        string value = string.Empty;
        string monthwise = DateTime.Now.ToString("MM");
        string LoggedInUserName = myGlobal.loggedInUser();
        Db.constr = myGlobal.getAppSettingsDataForKey("tejSAP");
        string qry = "select Count(VisitType) as vitpe from DailySalesReports where    MONTH(createdOn) = '" + monthwise + "' and CreatedBy ='" + LoggedInUserName + "' and company='" + dbcode + "'";
        SqlDataReader rdr = Db.myGetReader(qry);
        while (rdr.Read())
        {
            string a = rdr["vitpe"].ToString();
            if (Convert.ToInt32(a) > 0)
            {
                value = "R";
            }
            else
            {
                value = "D";
            }
        }
        return value;
    }

    protected void Gvdata_RowCreated(object sender, GridViewRowEventArgs e)
    {
        if (e.Row.RowType == DataControlRowType.DataRow)
        {
            DataTable dt = (DataTable)ViewState["CurrentTable"];
            LinkButton lb = (LinkButton)e.Row.FindControl("LinkButton1");
            if (lb != null)
            {
                if (dt.Rows.Count > 1)
                {
                    if (e.Row.RowIndex == dt.Rows.Count - 1)
                    {
                        lb.Visible = false;
                    }
                }
                else
                {
                    lb.Visible = false;
                }
            }
        }
    }

    protected void grdQuartlyRowDetail_RowCancelingEdit(object sender, GridViewCancelEditEventArgs e)
    {
        try
        {
            grdQuartlyRowDetail.EditIndex = -1;
            if (Session["DSR"] != null)
            {
                DataTable DT_DSR = (DataTable)Session["DSR"];
                grdQuartlyRowDetail.DataSource = DT_DSR;
                grdQuartlyRowDetail.DataBind();
            }
        }
        catch (Exception ex)
        {
            lblms.Text = "Error occured in RowCancelingEdit() :" + ex.Message;
        }
    }

    protected void grdQuartlyRowDetail_RowEditing(object sender, GridViewEditEventArgs e)
    {
        try
        {
            txtfrdcall.Text = string.Empty;
            txtdesc.Text = string.Empty;

            txtreminderdate.Text = string.Empty;
            txtdescription.Text = string.Empty;

            grdQuartlyRowDetail.ShowFooter = false;
            grdQuartlyRowDetail.FooterRow.Visible = false;

            Label lblcall = (Label)grdQuartlyRowDetail.Rows[e.NewEditIndex].FindControl("lblcall") as Label;
            Label lblrnk = (Label)grdQuartlyRowDetail.Rows[e.NewEditIndex].FindControl("lblrnk") as Label;
            Label lblreminder = (Label)grdQuartlyRowDetail.Rows[e.NewEditIndex].FindControl("lblreminder") as Label;
            Label lblremidesc = (Label)grdQuartlyRowDetail.Rows[e.NewEditIndex].FindControl("lblremidesc") as Label;

            DropDownList ddlstatuscall = (DropDownList)grdQuartlyRowDetail.Rows[e.NewEditIndex].FindControl("ddlstatuscall") as DropDownList;

            DropDownList ddlnextaction = (DropDownList)grdQuartlyRowDetail.Rows[e.NewEditIndex].FindControl("ddlNextActionEdit") as DropDownList;

            string calll = lblcall.Text;
            string rnkk = lblrnk.Text;
            string remidate = lblreminder.Text;
            string remidesc = lblremidesc.Text;
            // string bu = items;
            int RewardSettingLineID = Convert.ToInt32(grdQuartlyRowDetail.DataKeys[e.NewEditIndex].Value.ToString());

            ViewState["GVID"] = RewardSettingLineID;

            DataTable DT_DSR = new DataTable();

            DataColumn colVisitIdID = DT_DSR.Columns.Add("VisitId", typeof(Int32));
            DataColumn colCc = DT_DSR.Columns.Add("CardCode", typeof(string));
            DataColumn colComName = DT_DSR.Columns.Add("Company", typeof(string));
            DataColumn colPerMet = DT_DSR.Columns.Add("PersonMet", typeof(string));
            DataColumn colEmail = DT_DSR.Columns.Add("Email", typeof(string));
            DataColumn colTel = DT_DSR.Columns.Add("ContactNo", typeof(string));
            DataColumn colDesig = DT_DSR.Columns.Add("Designation", typeof(string));
            DataColumn colDiscu = DT_DSR.Columns.Add("Discussion", typeof(string));
            DataColumn colBuss = DT_DSR.Columns.Add("ExpectedBusinessAmt", typeof(string));
            DataColumn colcallsta = DT_DSR.Columns.Add("CallStatus", typeof(string));
            DataColumn colfeedback = DT_DSR.Columns.Add("Feedback", typeof(string));
            DataColumn coldisrpt = DT_DSR.Columns.Add("VisitType", typeof(string));
            DataColumn colBU = DT_DSR.Columns.Add("BU", typeof(string));
            DataColumn colcall = DT_DSR.Columns.Add("ForwardCallToEmail", typeof(string));
            DataColumn coldesc = DT_DSR.Columns.Add("ForwardRemark", typeof(string));
            DataColumn colcallmode = DT_DSR.Columns.Add("ModeOfCall", typeof(string));

            DataColumn colRemi = DT_DSR.Columns.Add("ReminderDate", typeof(string));
            DataColumn colreminderdesc = DT_DSR.Columns.Add("ReminderDesc", typeof(string));
            DataColumn colNextAction = DT_DSR.Columns.Add("NextAction", typeof(string));

            DataColumn colCountry = DT_DSR.Columns.Add("country", typeof(string));
            DataColumn colActualDate = DT_DSR.Columns.Add("ActualVisitDate", typeof(string));

            //if (myGlobal.dt_temp == null)
            if (Session["DSR"] == null)
            {
                foreach (GridViewRow gvr in grdQuartlyRowDetail.Rows)
                {
                    Label lblvisitid = ((Label)gvr.FindControl("lblvisitid"));
                    Label lblCompany = ((Label)gvr.FindControl("lblcustomername"));
                    Label lblcardcode = ((Label)gvr.FindControl("lblcardcode"));
                    Label lblPersonMet = ((Label)gvr.FindControl("lblPermeet"));
                    Label lblEMail = ((Label)gvr.FindControl("lblEmail"));
                    Label lblContactNo = ((Label)gvr.FindControl("lblconno"));
                    Label lblDesignation = ((Label)gvr.FindControl("lbldesign"));
                    Label lblBU = ((Label)gvr.FindControl("lblBU"));
                    Label lblVisitType = ((Label)gvr.FindControl("lbldisrpt"));
                    Label lblDiscussion = ((Label)gvr.FindControl("lbldiscussion"));
                    Label lblExpectedBusinessAmt = ((Label)gvr.FindControl("lblexpbusAmt"));
                    Label lblCallStatus = ((Label)gvr.FindControl("lblcallstatus"));
                    Label lblFeedback = ((Label)gvr.FindControl("lblfeedbck"));
                    Label lblForwardCallToEmail = ((Label)gvr.FindControl("lblcall"));
                    Label ForwardRemark = ((Label)gvr.FindControl("lblrnk"));
                    Label lblcallMode = ((Label)gvr.FindControl("lblcallMode"));

                    Label lblreminder1 = ((Label)gvr.FindControl("lblreminder"));
                    Label lblremidesc1 = ((Label)gvr.FindControl("lblremidesc"));
                    Label lblnextaction = ((Label)gvr.FindControl("lblnextaction"));

                    Label lblcountry = ((Label)gvr.FindControl("lblcountry"));
                    Label lbldate = ((Label)gvr.FindControl("lbldate"));

                    DataRow DRow = DT_DSR.NewRow();
                    DRow["VisitId"] = lblvisitid.Text;
                    DRow["CardCode"] = lblcardcode.Text;
                    DRow["Company"] = lblCompany.Text;
                    DRow["PersonMet"] = lblPersonMet.Text;
                    DRow["Email"] = lblEMail.Text;
                    DRow["ContactNo"] = lblContactNo.Text;
                    DRow["Designation"] = lblDesignation.Text;
                    DRow["Discussion"] = lblDiscussion.Text;
                    DRow["ExpectedBusinessAmt"] = lblExpectedBusinessAmt.Text;
                    DRow["CallStatus"] = lblCallStatus.Text;
                    DRow["Feedback"] = lblFeedback.Text;
                    DRow["VisitType"] = lblVisitType.Text;
                    DRow["BU"] = lblBU.Text;
                    DRow["ForwardCallToEmail"] = lblForwardCallToEmail.Text;
                    DRow["ForwardRemark"] = ForwardRemark.Text;
                    DRow["ModeOfCall"] = lblcallMode.Text;

                    DRow["ReminderDate"] = lblreminder1.Text;
                    DRow["ReminderDesc"] = lblremidesc1.Text;
                    DRow["NextAction"] = lblnextaction.Text;

                    DRow["country"] = lblcountry.Text;
                    DRow["ActualVisitDate"] = lbldate.Text;

                    DT_DSR.Rows.Add(DRow);
                    //  grdQuartlyRowDetail.DataSource = myGlobal.dt_temp;
                    //  grdQuartlyRowDetail.DataBind();
                    //   myGlobal.autoId_temp = myGlobal.autoId_temp + 1;
                }

                Session["DSR"] = DT_DSR;
            }
            if (Session["DSR"] != null)
            {
                DT_DSR = (DataTable)Session["DSR"];
            }

            DataRow DR = DT_DSR.Select("VisitId=" + RewardSettingLineID.ToString()).FirstOrDefault();
            if (DR != null)
            {
                DR["ForwardCallToEmail"] = calll;
                DR["ForwardRemark"] = rnkk;
            }
            DataRow DR1 = DT_DSR.Select("VisitId=" + RewardSettingLineID.ToString()).FirstOrDefault();
            if (DR1 != null)
            {
                DR1["ReminderDate"] = remidate;
                DR1["ReminderDesc"] = remidesc;
                //  DR["BU"] = items;
            }
            grdQuartlyRowDetail.EditIndex = e.NewEditIndex;
            if (DT_DSR != null)
            {
                grdQuartlyRowDetail.DataSource = DT_DSR;
                grdQuartlyRowDetail.DataBind();
            }

            txtfrdcall.Text = calll;
            txtdesc.Text = rnkk;

            txtreminderdate.Text = remidate;
            txtdescription.Text = remidesc;
        }
        catch (Exception ex)
        {

        }

    }

    protected void grdQuartlyRowDetail_RowCommand(object sender, GridViewCommandEventArgs e)
    {
        try
        {
            lblms.Text = "";
            if (e.CommandName == "AddNew")
            {

                Db.constr = myGlobal.getAppSettingsDataForKey("tejSAP");
                string LoggedInUserName = myGlobal.loggedInUser();
                txtfrdcall.Text = "";
                txtdesc.Text = "";
                txtreminderdate.Text = "";
                txtdescription.Text = "";

                TextBox txtCustomerName = (grdQuartlyRowDetail.FooterRow.FindControl("txtCustomerName")) as TextBox;
                TextBox txtCardCode = (grdQuartlyRowDetail.FooterRow.FindControl("txtCardCode")) as TextBox;
                TextBox txtprsonmeet = (grdQuartlyRowDetail.FooterRow.FindControl("txtprsonmeet")) as TextBox;
                TextBox txtemailid = (grdQuartlyRowDetail.FooterRow.FindControl("txtemailid")) as TextBox;
                TextBox txtconnumber = (grdQuartlyRowDetail.FooterRow.FindControl("txtconnumber")) as TextBox;
                TextBox txtdesig = (grdQuartlyRowDetail.FooterRow.FindControl("txtdesig")) as TextBox;
                // TextBox txtdisrpt = (grdQuartlyRowDetail.FooterRow.FindControl("lbldisrptt")) as TextBox;

                TextBox txtactiondone = (grdQuartlyRowDetail.FooterRow.FindControl("txtactiondone")) as TextBox;
                TextBox txtexpectedbuss = (grdQuartlyRowDetail.FooterRow.FindControl("txtexpectedbuss")) as TextBox;
                DropDownList ddlststusofcall = (grdQuartlyRowDetail.FooterRow.FindControl("ddlststusofcall")) as DropDownList;
                TextBox txtfeedback = (grdQuartlyRowDetail.FooterRow.FindControl("txtfeedback")) as TextBox;
                TextBox txtBU = (grdQuartlyRowDetail.FooterRow.FindControl("txtBU")) as TextBox;
                // ListBox ddlBU = (grdQuartlyRowDetail.FooterRow.FindControl("ddlBU")) as ListBox;
                Button frd = (grdQuartlyRowDetail.FooterRow.FindControl("btnShowModalPopup")) as Button;
                TextBox txtdisrptt = (grdQuartlyRowDetail.FooterRow.FindControl("txtdisrptt")) as TextBox;
                TextBox txtcall = (grdQuartlyRowDetail.FooterRow.FindControl("txtcall")) as TextBox;
                TextBox txtdescc = (grdQuartlyRowDetail.FooterRow.FindControl("txtdescc")) as TextBox;

                TextBox txtreminder = (grdQuartlyRowDetail.FooterRow.FindControl("txtreminder")) as TextBox;
                TextBox txtremdescc = (grdQuartlyRowDetail.FooterRow.FindControl("txtremdescc")) as TextBox;

                DropDownList ddlststusofcallMode = (grdQuartlyRowDetail.FooterRow.FindControl("ddlststusofcallMode")) as DropDownList;
                DropDownList ddlnxtactiFooter = (grdQuartlyRowDetail.FooterRow.FindControl("ddlnxtactiFooter")) as DropDownList;
                DropDownList ddlcountryfooter = (grdQuartlyRowDetail.FooterRow.FindControl("ddlcountryfooter")) as DropDownList;
                TextBox txtDateFooter = (grdQuartlyRowDetail.FooterRow.FindControl("txtDateFooter")) as TextBox;

                /////*condition to check wheter selected customer of same date is in db or not*/
                ////DataSet ds = Db.myGetDS("select Company from DailySalesReports where Company='" + txtCustomerName.Text + "' and VisitDate='" + txtvisitDate.Text + "' and CreatedBy='" + LoggedInUserName + "'");
                ////if (ds.Tables[0].Rows.Count > 0)
                ////{
                ////    txtCustomerName.Text = "";
                ////}
                //foreach (GridViewRow gvr in grdQuartlyRowDetail.Rows)
                //{
                //    ((ImageButton)gvr.FindControl("BtnEdit")).Visible = false;
                //    ((ImageButton)gvr.FindControl("BtnDelete")).Visible = false;
                //}

                string call = string.Empty, desc = string.Empty, Reminderdate = string.Empty, Reminderdesc = string.Empty;
                if (ViewState["Call"] != null)
                {
                    call = ViewState["Call"].ToString();
                }
                if (ViewState["Desc"] != null)
                {
                    desc = ViewState["Desc"].ToString();
                }
                if (ViewState["remindedate"] != null)
                {
                    Reminderdate = ViewState["remindedate"].ToString();
                }
                if (ViewState["reminderDesc"] != null)
                {
                    Reminderdesc = ViewState["reminderDesc"].ToString();
                }

                string value = string.Empty;
                if (string.IsNullOrEmpty(txtdisrptt.Text) && !string.IsNullOrEmpty(txtCustomerName.Text))
                {
                    LoggedInUserName = myGlobal.loggedInUser();
                    Db.constr = myGlobal.getAppSettingsDataForKey("tejSAP");

                    DataSet ds = Db.myGetDS(" EXEC DSR_GetDistict_RepeatVisit '"+myGlobal.loggedInUser()+"','"+txtCustomerName.Text+"','"+txtCardCode.Text+"' ");
                    if (ds.Tables[0].Rows.Count > 0)
                    {
                        string a = ds.Tables[0].Rows[0]["vitpe"].ToString();
                        if (Convert.ToInt32(a) > 0)
                        {
                            value = "R";
                        }
                        else
                        {
                            value = "D";
                        }
                    }
                }
                else
                {
                    value = txtdisrptt.Text;
                }


                #region  "Validation "

                if (string.IsNullOrEmpty(txtexpectedbuss.Text))
                {
                    txtexpectedbuss.Text = "0";
                }
                if (ddlcountryfooter.SelectedItem.Text == "--Select--")
                {
                    lblms.Text = "Please Select Country";
                    return;
                }
                if (string.IsNullOrEmpty(txtDateFooter.Text))
                {
                    lblms.Text = "Please Select Date";
                    return;
                }
                if (ddlststusofcallMode.Text == "--Select--")
                {
                    lblms.Text = "Please Select Call Mode";
                    return;
                }
                if (ddlststusofcall.Text == "--Select--")
                {
                    lblms.Text = "Please Select Call Type";
                    return;
                }
                if (txtCustomerName.Text == "")
                {
                    lblms.Text = "Please Select Customer";
                    return;
                }
                if (string.IsNullOrEmpty(txtprsonmeet.Text))
                {
                    lblms.Text = "Please Enter Name Of Person";
                    return;
                }
                if (string.IsNullOrEmpty(txtactiondone.Text))
                {
                    lblms.Text = "Please enter Discussion";
                    return;
                }
                if (ddlnxtactiFooter.Text == "--Select--")
                {
                    lblms.Text = "Please Select NextAction";
                    return;
                }
                if (string.IsNullOrEmpty(txtfeedback.Text))
                {
                    lblms.Text = "Please enter FeedBack";
                    return;
                }

                #endregion

                DataTable DT_DSR = new DataTable();
                int AutoVisitId = 0;

                DataColumn colVisitIdID = DT_DSR.Columns.Add("VisitId", typeof(Int32));
                DataColumn colComName = DT_DSR.Columns.Add("Company", typeof(string));
                DataColumn colCCc = DT_DSR.Columns.Add("CardCode", typeof(string));
                DataColumn colPerMet = DT_DSR.Columns.Add("PersonMet", typeof(string));
                DataColumn colEmail = DT_DSR.Columns.Add("Email", typeof(string));
                DataColumn colTel = DT_DSR.Columns.Add("ContactNo", typeof(string));
                DataColumn colDesig = DT_DSR.Columns.Add("Designation", typeof(string));
                DataColumn colDiscu = DT_DSR.Columns.Add("Discussion", typeof(string));
                DataColumn colBuss = DT_DSR.Columns.Add("ExpectedBusinessAmt", typeof(string));
                DataColumn colcallsta = DT_DSR.Columns.Add("CallStatus", typeof(string));
                DataColumn colfeedback = DT_DSR.Columns.Add("Feedback", typeof(string));
                DataColumn coldisrpt = DT_DSR.Columns.Add("VisitType", typeof(string));
                DataColumn colBU = DT_DSR.Columns.Add("BU", typeof(string));
                DataColumn colcall = DT_DSR.Columns.Add("ForwardCallToEmail", typeof(string));
                DataColumn coldesc = DT_DSR.Columns.Add("ForwardRemark", typeof(string));
                DataColumn colreminder = DT_DSR.Columns.Add("ReminderDate", typeof(string));
                DataColumn colRemindesc = DT_DSR.Columns.Add("ReminderDesc", typeof(string));
                DataColumn colcallmode = DT_DSR.Columns.Add("ModeOfCall", typeof(string));
                DataColumn colNextAction = DT_DSR.Columns.Add("NextAction", typeof(string));
                DataColumn colCountry = DT_DSR.Columns.Add("country", typeof(string));
                DataColumn colActualDate = DT_DSR.Columns.Add("ActualVisitDate", typeof(string));

                //if (myGlobal.dt_temp == null)
                if (Session["DSR"] == null)
                {
                    foreach (GridViewRow gvr in grdQuartlyRowDetail.Rows)
                    {
                        Label lblvisitid = ((Label)gvr.FindControl("lblvisitid"));
                        Label lblCompany = ((Label)gvr.FindControl("lblcustomername"));
                        Label lblCC = ((Label)gvr.FindControl("lblcardcode"));
                        Label lblPersonMet = ((Label)gvr.FindControl("lblPermeet"));
                        Label lblEMail = ((Label)gvr.FindControl("lblEmail"));
                        Label lblContactNo = ((Label)gvr.FindControl("lblconno"));
                        Label lblDesignation = ((Label)gvr.FindControl("lbldesign"));
                        Label lblBU = ((Label)gvr.FindControl("lblBU"));
                        Label lblVisitType = ((Label)gvr.FindControl("lbldisrpt"));
                        Label lblDiscussion = ((Label)gvr.FindControl("lbldiscussion"));
                        Label lblExpectedBusinessAmt = ((Label)gvr.FindControl("lblexpbusAmt"));
                        Label lblCallStatus = ((Label)gvr.FindControl("lblcallstatus"));
                        Label lblFeedback = ((Label)gvr.FindControl("lblfeedbck"));
                        Label lblForwardCallToEmail = ((Label)gvr.FindControl("lblcall"));
                        Label ForwardRemark = ((Label)gvr.FindControl("lblrnk"));

                        Label lblreminder = ((Label)gvr.FindControl("lblreminder"));
                        Label lblreminddesc = ((Label)gvr.FindControl("lblremidesc"));

                        Label lblcallMode = ((Label)gvr.FindControl("lblcallMode"));
                        Label lblnextaction = ((Label)gvr.FindControl("lblnextaction"));
                        Label lblcountry = ((Label)gvr.FindControl("lblcountry"));
                        Label lbldate = ((Label)gvr.FindControl("lbldate"));

                        if (lblvisitid.Text.Trim() != "")
                        {
                            if (Session["AutoVisitId"] != null)
                            {
                                AutoVisitId = Convert.ToInt32(Session["AutoVisitId"]);
                            }

                            DataRow DRow = DT_DSR.NewRow();
                            DRow["VisitId"] = AutoVisitId + 1;
                            DRow["Company"] = lblCompany.Text;
                            DRow["CardCode"] = lblCC.Text;
                            DRow["PersonMet"] = lblPersonMet.Text;
                            DRow["Email"] = lblEMail.Text;
                            DRow["ContactNo"] = lblContactNo.Text;
                            DRow["Designation"] = lblDesignation.Text;
                            DRow["Discussion"] = lblDiscussion.Text;
                            DRow["ExpectedBusinessAmt"] = lblExpectedBusinessAmt.Text;
                            DRow["CallStatus"] = lblCallStatus.Text;
                            DRow["Feedback"] = lblFeedback.Text;
                            DRow["VisitType"] = lblVisitType.Text;
                            DRow["BU"] = lblBU.Text;
                            DRow["ForwardCallToEmail"] = lblForwardCallToEmail.Text;
                            DRow["ForwardRemark"] = ForwardRemark.Text;
                            DRow["ReminderDate"] = lblreminder.Text;
                            DRow["ReminderDesc"] = lblreminddesc.Text;

                            DRow["ModeOfCall"] = lblcallMode.Text;
                            DRow["NextAction"] = lblnextaction.Text;
                            DRow["country"] = lblcountry.Text;
                            DRow["ActualVisitDate"] = lbldate.Text;
                            DT_DSR.Rows.Add(DRow); //for vineet//(open)

                            Session["AutoVisitId"] = AutoVisitId + 1;
                            //  myGlobal.dt_temp.Rows.Add(DRow); properly running for mohammaded(clse)
                            //grdQuartlyRowDetail.DataSource = myGlobal.dt_temp;
                            //grdQuartlyRowDetail.DataBind();
                            //myGlobal.autoId_temp = myGlobal.autoId_temp + 1;
                            //  }
                        }
                    }
                }
                else
                {
                    DT_DSR = (DataTable)Session["DSR"];
                }

                if (Session["AutoVisitId"] != null)
                {
                    AutoVisitId = Convert.ToInt32(Session["AutoVisitId"]);
                }

                DataRow DRow1 = DT_DSR.NewRow();
                DRow1["VisitId"] = AutoVisitId + 1; //its give double record and edit id
                DRow1["Company"] = txtCustomerName.Text;
                DRow1["CardCode"] = txtCardCode.Text;
                DRow1["PersonMet"] = txtprsonmeet.Text;
                DRow1["Email"] = txtemailid.Text;
                DRow1["ContactNo"] = txtconnumber.Text;
                DRow1["Designation"] = txtdesig.Text;
                DRow1["Discussion"] = txtactiondone.Text;
                DRow1["ExpectedBusinessAmt"] = txtexpectedbuss.Text;
                DRow1["CallStatus"] = ddlststusofcall.Text;
                DRow1["Feedback"] = txtfeedback.Text;
                DRow1["VisitType"] = value;
                DRow1["BU"] = txtBU.Text;
                DRow1["ForwardCallToEmail"] = call;
                DRow1["ForwardRemark"] = desc;
                DRow1["ReminderDate"] = Reminderdate;
                DRow1["ReminderDesc"] = Reminderdesc;

                DRow1["ModeOfCall"] = ddlststusofcallMode.Text;
                DRow1["NextAction"] = ddlnxtactiFooter.Text;
                DRow1["country"] = ddlcountryfooter.Text;
                DRow1["ActualVisitDate"] = txtDateFooter.Text;

                DT_DSR.Rows.Add(DRow1);
                grdQuartlyRowDetail.DataSource = DT_DSR;
                grdQuartlyRowDetail.DataBind();
                //myGlobal.autoId_temp = myGlobal.autoId_temp + 1;
                Session["AutoVisitId"] = AutoVisitId + 1;
                Session["DSR"] = DT_DSR;

                if (call != null && desc != null)
                {
                    ViewState["Call"] = null;
                    ViewState["Desc"] = null;
                }

                if (Reminderdate != null && Reminderdesc != null)
                {
                    ViewState["remindedate"] = null;
                    ViewState["reminderDesc"] = null;
                }

                //}
            }
            else if (e.CommandName == "Edit")
            {
                /*  Asiign value of Grid row level farward details controls to POP up extender controls  */

                GridViewRow row = (GridViewRow)(((ImageButton)e.CommandSource).NamingContainer);
                int visitId = Convert.ToInt32(grdQuartlyRowDetail.DataKeys[row.RowIndex].Value.ToString());
                grdQuartlyRowDetail.FooterRow.Visible = false;
                //this.ModalPopupExtender1.Show();
                // this.ModalPopupExtender2.Show();
                // this.ModalPopupExtender3.Show();

            }
            else if (e.CommandName == "Delete")
            {
                Db.constr = myGlobal.getAppSettingsDataForKey("tejSAP");
                string LoggedInUserName = myGlobal.loggedInUser();

                //for (int i = 0; i < grdQuartlyRowDetail.Rows.Count; i++)
                //{
                GridViewRow row = (GridViewRow)(((ImageButton)e.CommandSource).NamingContainer);
                Label lblID = (Label)grdQuartlyRowDetail.Rows[row.RowIndex].FindControl("lblvisitid");
                //GridViewRow row = (GridViewRow)(((LinkButton)e.CommandSource).NamingContainer); 
                int visitId = Convert.ToInt32(lblID.Text);

                string date = txtvisitDate.Text;
                //  string country = ddlCountry.SelectedItem.Text;
                Label lblcountry = (Label)grdQuartlyRowDetail.Rows[row.RowIndex].FindControl("lblcountry");
                Label lblcusname = (Label)grdQuartlyRowDetail.Rows[row.RowIndex].FindControl("lblcustomername");
                Label lbldate = (Label)grdQuartlyRowDetail.Rows[row.RowIndex].FindControl("lbldate");
                string cusname = lblcusname.Text;
                string country = lblcountry.Text;
                //Label lblID = (Label)GRVAutoCLStatusChange.Rows[row.RowIndex].FindControl("lblID");

                // Date - Cusotmer Name - Country And By LoggedInUser And VisitId  -- If Exist -- Delete BindGrid , If doesnt exist then Delete Row And Bind

                int CountOfRec = Db.myExecuteScalar("select count(*) from DailySalesReports where Company='" + cusname + "' And ActualVisitDate='" + lbldate.Text + "' VisitDate='" + txtvisitDate.Text + "' And ToDate='"+txttodate.Text+"'");
                if (CountOfRec > 0)
                {
                    string query = "Delete from  DailySalesReports  where VisitDate='" + date + "' and Company='" + cusname + "' and Country='" + country + "' and CreatedBy='" + LoggedInUserName + "' and  VisitId='" + visitId + "'";
                    Db.myExecuteSQL(query);
                }
                else
                {
                    GridViewRow row1 = (GridViewRow)(((ImageButton)e.CommandSource).NamingContainer);
                    visitId = Convert.ToInt32(grdQuartlyRowDetail.DataKeys[row.RowIndex].Value.ToString());
                }
            }
            else if (e.CommandName == "Forward CallTo")
            {

                for (int i = 0; i < grdQuartlyRowDetail.Rows.Count; i++)
                {
                    string customer = (grdQuartlyRowDetail.Rows[i].FindControl("lblcustomername") as Label).Text;
                    if (customer == "")
                    {
                        foreach (GridViewRow gvr in grdQuartlyRowDetail.Rows)
                        {
                            ((ImageButton)gvr.FindControl("BtnEdit")).Visible = false;
                            ((ImageButton)gvr.FindControl("BtnDelete")).Visible = false;
                        }
                    }
                    else
                    {
                        foreach (GridViewRow gvr in grdQuartlyRowDetail.Rows)
                        {
                            ((ImageButton)gvr.FindControl("BtnEdit")).Visible = true;
                            ((ImageButton)gvr.FindControl("BtnDelete")).Visible = true;
                        }
                    }
                }
                //  ModalPopupExtender1.Show();
            }
            else if (e.CommandName == "Reminder")
            {
                //  txtdescription.Text = "";
                // txtreminderdate.Text = "";

                for (int i = 0; i < grdQuartlyRowDetail.Rows.Count; i++)
                {
                    string customer = (grdQuartlyRowDetail.Rows[i].FindControl("lblcustomername") as Label).Text;
                    if (customer == "")
                    {
                        foreach (GridViewRow gvr in grdQuartlyRowDetail.Rows)
                        {
                            ((ImageButton)gvr.FindControl("BtnEdit")).Visible = false;
                            ((ImageButton)gvr.FindControl("BtnDelete")).Visible = false;
                        }
                    }
                    else
                    {
                        foreach (GridViewRow gvr in grdQuartlyRowDetail.Rows)
                        {
                            ((ImageButton)gvr.FindControl("BtnEdit")).Visible = true;
                            ((ImageButton)gvr.FindControl("BtnDelete")).Visible = true;
                        }
                    }
                }
                //  ModalPopupExtender1.Show();
            }
            else if (e.CommandName == "Cancel")
            {
                grdQuartlyRowDetail.ShowFooter = true;
                grdQuartlyRowDetail.FooterRow.Visible = true;
            }
            else if (e.CommandName == "Update")
            {
                grdQuartlyRowDetail.ShowFooter = true;
                grdQuartlyRowDetail.FooterRow.Visible = true;
            }
        }
        catch (Exception ex)
        {
            //  lblms.Text = "Error in Grid RowCommand () : " + ex.Message;
            //lblms.Text = "Please Save ForwardCall and ForwardRemark";
        }
    }

    private void BindGridAddNew()
    {
        try
        {
            DataTable DSR_DT = new DataTable();
            DataColumn colVisitIdID = DSR_DT.Columns.Add("VisitId", typeof(Int32));
            DataColumn colComName = DSR_DT.Columns.Add("Company", typeof(string));
            DataColumn colCC = DSR_DT.Columns.Add("CardCode", typeof(string));
            DataColumn colPerMet = DSR_DT.Columns.Add("PersonMet", typeof(string));
            DataColumn colEmail = DSR_DT.Columns.Add("Email", typeof(string));
            DataColumn colTel = DSR_DT.Columns.Add("ContactNo", typeof(string));
            DataColumn colDesig = DSR_DT.Columns.Add("Designation", typeof(string));
            // DataColumn colVistpe = TblReward.Columns.Add("VisitType", typeof(string));

            DataColumn colDiscu = DSR_DT.Columns.Add("Discussion", typeof(string));
            DataColumn colBuss = DSR_DT.Columns.Add("ExpectedBusinessAmt", typeof(Int32));
            DataColumn colcallsta = DSR_DT.Columns.Add("CallStatus", typeof(string));
            DataColumn colfeedback = DSR_DT.Columns.Add("Feedback", typeof(string));
            DataColumn coldisrpt = DSR_DT.Columns.Add("VisitType", typeof(string));
            DataColumn colBU = DSR_DT.Columns.Add("BU", typeof(string));
            DataColumn colfrd = DSR_DT.Columns.Add("ForwardCallToEmail", typeof(string));

            DataColumn colFrmk = DSR_DT.Columns.Add("ForwardRemark", typeof(string));
            DataColumn colcallmode = DSR_DT.Columns.Add("ModeOfCall", typeof(string));
            DataColumn colremindate = DSR_DT.Columns.Add("ReminderDate", typeof(string));
            DataColumn colreminderdesc = DSR_DT.Columns.Add("ReminderDesc", typeof(string));
           
            DataColumn colCountry = DSR_DT.Columns.Add("country", typeof(string));
            DataColumn colNextAction = DSR_DT.Columns.Add("NextAction", typeof(string));
            DataColumn colActualVisitDate = DSR_DT.Columns.Add("ActualVisitDate", typeof(string));

            DSR_DT.Rows.Add(DSR_DT.NewRow());
            grdQuartlyRowDetail.DataSource = DSR_DT;
            grdQuartlyRowDetail.DataBind();
            grdQuartlyRowDetail.Rows[0].Cells.Clear();
            grdQuartlyRowDetail.Rows[0].Cells.Add(new TableCell());

            btnsaveasdraft.Visible = true;
            btnSave.Enabled = true;
            grdQuartlyRowDetail.FooterRow.Visible = true;
            grdQuartlyRowDetail.Enabled = true;
        }
        catch (Exception ex)
        {
            lblms.Text = "Error occured in BindGridAddNew : " + ex.Message;
        }
    }

    protected void grdQuartlyRowDetail_RowDeleting(object sender, GridViewDeleteEventArgs e)
    {
        if (Session["DSR"] != null)
        {
            DataTable DT_DSR = new DataTable();
            DT_DSR = (DataTable)Session["DSR"];
            if (DT_DSR != null)
            {
                int VisitId = Convert.ToInt32(grdQuartlyRowDetail.DataKeys[e.RowIndex].Value.ToString());
                // DataRow DR = myGlobal.dt_temp.Select("VisitId=" + RewardSettingLineID.ToString()).FirstOrDefault();
                DT_DSR.Rows.RemoveAt(VisitId - 1);

                // if(DR != null)
                //{
                //    DR["Company"] = "";
                //    DR["PersonMet"] = "";
                //    DR["Email"] = "";
                //    DR["ContactNo"] = "";
                //    DR["Designation"] = "";
                //    DR["Discussion"] = "";
                //    DR["ExpectedBusinessAmt"] = "";
                //    DR["CallStatus"] = "";
                //    DR["Feedback"] = "";
                //    DR["BU"] = "";
                //    DR["VisitType"] = "";

                //    DR["ForwardCallToEmail"] = "";
                //    DR["ForwardRemark"] = "";
                //}
                //myGlobal.dt_temp.AcceptChanges();
                grdQuartlyRowDetail.EditIndex = -1;
                grdQuartlyRowDetail.DataSource = DT_DSR;
                grdQuartlyRowDetail.DataBind();

                Session["DSR"] = DT_DSR;

                if (grdQuartlyRowDetail.Rows.Count == 0)
                {
                    BindGridAddNew();
                }
            }
        }
        else
        {
            GetVisitDataByDate();
        }

    }

    protected void grdQuartlyRowDetail_RowUpdating(object sender, GridViewUpdateEventArgs e)
    {
        try
        {
            ////  TextBox txtcardcodeEdit = (grdQuartlyRowDetail.Rows[e.RowIndex].FindControl("txtcardcodeEdit")) as TextBox;
            TextBox txtcustomername = (grdQuartlyRowDetail.Rows[e.RowIndex].FindControl("txtcustomernameEdit")) as TextBox;
            TextBox txtPermeet = (grdQuartlyRowDetail.Rows[e.RowIndex].FindControl("txtPermeet")) as TextBox;
            TextBox txtEmail = (grdQuartlyRowDetail.Rows[e.RowIndex].FindControl("txtEmail")) as TextBox;
            TextBox txtconnumber = (grdQuartlyRowDetail.Rows[e.RowIndex].FindControl("txtconnumber")) as TextBox;
            TextBox txtdesign = (grdQuartlyRowDetail.Rows[e.RowIndex].FindControl("txtdesign")) as TextBox;
            TextBox txtctiondon = (grdQuartlyRowDetail.Rows[e.RowIndex].FindControl("txtctiondon")) as TextBox;
            TextBox txtexpbusAmt = (grdQuartlyRowDetail.Rows[e.RowIndex].FindControl("txtexpbusAmt")) as TextBox;
            DropDownList ddlstatuscall = (grdQuartlyRowDetail.Rows[e.RowIndex].FindControl("ddlstatuscall")) as DropDownList;
            TextBox txtfeedbck = (grdQuartlyRowDetail.Rows[e.RowIndex].FindControl("txtfeedbck")) as TextBox;
            // TextBox txtfeedbck = (grdQuartlyRowDetail.Rows[e.RowIndex].FindControl("txtfeedbck")) as TextBox;
            ///  ListBox ddlBU = (grdQuartlyRowDetail.Rows[e.RowIndex].FindControl("ddlBU")) as ListBox;

            TextBox txtBUUedit = (grdQuartlyRowDetail.Rows[e.RowIndex].FindControl("txtBUUedit")) as TextBox;
            DropDownList ddlcallmode = (grdQuartlyRowDetail.Rows[e.RowIndex].FindControl("ddlcallmode")) as DropDownList;
            DropDownList ddlNextActionEdit = (grdQuartlyRowDetail.Rows[e.RowIndex].FindControl("ddlNextActionEdit")) as DropDownList;

            DropDownList ddlCountryEdit = (grdQuartlyRowDetail.Rows[e.RowIndex].FindControl("ddlCountryEdit")) as DropDownList;
            TextBox txtDateEdit = (grdQuartlyRowDetail.Rows[e.RowIndex].FindControl("txtDateEdit")) as TextBox;

            string a = txtfrdcall.Text;
            string b = txtdesc.Text;

            string c = txtreminderdate.Text;
            string d = txtdescription.Text;

            string Country = ddlCountryEdit.Text;
            if (Country == "--Select--")
            {
                lblms.Text = "Please Select Country";
                return;
            }

            string ActualDate = txtDateEdit.Text;
            if (string.IsNullOrEmpty(ActualDate))
            {
                lblms.Text = "Date Should Not Be Blank";
                return;
            }

            string callMode = ddlcallmode.Text;
            if (callMode == "--Select--")
            {
                lblms.Text = "Please Select Call Mode";
                return;
            }
            string callstatus = ddlstatuscall.Text;
            if (callstatus == "--Select--")
            {
                lblms.Text = "Please Select Call Type";
                return;
            }
            string Cutname = txtcustomername.Text;
            if (string.IsNullOrEmpty(Cutname))
            {
                lblms.Text = "Customer Should Not Be Blank";
                return;
            }
            string Permeet = txtPermeet.Text;
            if (string.IsNullOrEmpty(Permeet))
            {
                lblms.Text = "Contact Person Should Not Be Blank";
                return;
            }
            string email = txtEmail.Text;

            string contno = txtconnumber.Text;

            string desi = txtdesign.Text;
            string actiondone = txtctiondon.Text;
            if (string.IsNullOrEmpty(actiondone))
            {
                lblms.Text = "Discussion Should Not Be Blank";
                return;
            }

            string ActionDone = ddlNextActionEdit.Text;
            if (ActionDone == "--Select--")
            {
                lblms.Text = "Please Select NextAction";
                return;
            }

            string expbuss = txtexpbusAmt.Text;
            
            string feedback = txtfeedbck.Text;
            if (string.IsNullOrEmpty(feedback))
            {
                lblms.Text = "FeedBack Should Not Be Blank";
                return;
            }

            string Bu = txtBUUedit.Text;
            string frdcall = a;
            string frdrmk = b;

            string reminderdate = c;
            string remindesc = d;
            
            DataTable DT_DSR = new DataTable();
            if (Session["DSR"] != null)
                DT_DSR = (DataTable)Session["DSR"];

            int RewardSettingLineID = Convert.ToInt32(grdQuartlyRowDetail.DataKeys[e.RowIndex].Value.ToString());
            DataRow DR = DT_DSR.Select("VisitId=" + RewardSettingLineID.ToString()).FirstOrDefault();
            if (DR != null)
            {
                //   DR["CardCode"] = Ccde;
                DR["Company"] = Cutname;
                DR["PersonMet"] = Permeet;
                DR["Email"] = email;
                DR["ContactNo"] = contno;
                DR["Designation"] = desi;
                DR["Discussion"] = actiondone;
                DR["ExpectedBusinessAmt"] = expbuss;
                DR["CallStatus"] = callstatus;
                DR["Feedback"] = feedback;
                DR["BU"] = Bu;
                DR["ForwardCallToEmail"] = frdcall;
                DR["ForwardRemark"] = frdrmk;
                DR["ReminderDate"] = reminderdate;
                DR["ReminderDesc"] = remindesc;

                DR["ModeOfCall"] = callMode;
                DR["NextAction"] = ActionDone;
                DR["country"] = Country;
                DR["ActualVisitDate"] = ActualDate;
            }
            DT_DSR.AcceptChanges();
            grdQuartlyRowDetail.EditIndex = -1;
            grdQuartlyRowDetail.DataSource = DT_DSR;
            grdQuartlyRowDetail.DataBind();

            Session["DSR"] = DT_DSR;

            grdQuartlyRowDetail.ShowFooter = true;
            grdQuartlyRowDetail.FooterRow.Visible = true;

        }
        catch (Exception ex)
        {
            lblms.Text = "Error occured in RowUpdating() :" + ex.Message;
        }

    }

    protected void btngotolist_Click(object sender, EventArgs e)
    {
        Response.Redirect("DailySaleRptList.aspx");
    }

    protected void btncancel_Click(object sender, EventArgs e)
    {
        Response.Redirect("DSREntry.aspx");
        //  ClearControl();
        lblms.Text = "";


    }

    //protected void fillgvdata()
    //{
    //    pnlRewardList.Visible = true;
    //    Db.constr = myGlobal.getAppSettingsDataForKey("tejSAP");
    //    string LoggedInUserName = myGlobal.loggedInUser();
    //    DataSet ds = Db.myGetDS("SELECT  VisitDate ,  CreatedBy  , COUNT(VisitId) VIST_COUNT, SUM(ExpectedBusinessAmt ) TOTAL_EXPECTED_AMT  FROM DailySalesReports WHERE CONVERT(VARCHAR(10),VisitDate, 111) >= CONVERT(VARCHAR(10), DATEADD(DAY, -10, GETDATE()), 111) AND CONVERT(VARCHAR(10),VisitDate, 111) <=CONVERT(VARCHAR(10), getdate(), 111) AND CreatedBy='" + LoggedInUserName + "' GROUP BY CreatedBy ,VisitDate");
    //    //   DataSet ds = Db.myGetDS("select Distinct(VisitDate),VisitId from DailySalesReports where CreatedBy='" + LoggedInUserName + "'");
    //    if (ds.Tables[0].Rows.Count > 0)
    //    {
    //        GrvListAll.DataSource = ds.Tables[0];
    //        GrvListAll.DataBind();

    //    }
    //}

    //protected void GrvListAll_SelectedIndexChanged(object sender, EventArgs e)
    //{
    //    Db.constr = myGlobal.getAppSettingsDataForKey("tejSAP");
    //    pnlRewardList.Visible = false;
    //    btnSave.Text = "Update";
    //    lblms.Text = "";

    //    Label id = (Label)GrvListAll.SelectedRow.Cells[1].FindControl("lblID");
    //    string date = id.Text.ToString();


    //    myGlobal.dt_temp = Db.myGetDS("select * from DailySalesReports where VisitDate ='" + date + "'").Tables[0];

    //    grdQuartlyRowDetail.DataSource = myGlobal.dt_temp;
    //    grdQuartlyRowDetail.DataBind();
    //    foreach (GridViewRow gvr in grdQuartlyRowDetail.Rows)
    //    {
    //        ((ImageButton)gvr.FindControl("BtnEdit")).Visible = false;
    //        ((ImageButton)gvr.FindControl("BtnDelete")).Visible = false;
    //        grdQuartlyRowDetail.ShowFooter = false;
    //        grdQuartlyRowDetail.FooterRow.Visible = false;
    //        btnSave.Enabled = false;
    //    }
    //}

    protected void btnSave_Click(object sender, EventArgs e)
    {
        try
        {
            string result = SaveUpdate(false);
            if(result=="success")
            {
                #region " Send Mail"
                try
                {
                    string emailsql = " EXEC  DSR_ForwardVisitCallSendEmail '" + myGlobal.loggedInUser() + "','" + txtvisitDate.Text + "','" + txttodate.Text + "'";
                    // emailsql = emailsql + " ; EXEC  DSR_SendCustomerVisitReport '" + myGlobal.loggedInUser() + "','" + txtvisitDate.Text + "' ";
                    Db.myExecuteSQL(emailsql);
                }
                catch { }
                // SendDailyReportMails();
                #endregion
                //Page.ClientScript.RegisterStartupScript(this.GetType(), "alert", "<script> alert( 'Record Saved  successfully .'); </script>");
                Response.Redirect("DSREntry.aspx?Action=SUBMIT");
                //Response.Redirect("DSREntry.aspx");
            }
            else if (!string.IsNullOrEmpty(result) && result.Contains("Error"))
            {
                Response.Redirect("DSREntry.aspx?Action=SUBMITFailed");
            }
            else
            {
                return;
            }

        }

        catch (Exception ex)
        {
            lblms.Text = "Error occured in BtnSave_Click() :" + ex.Message;
            //lblms.Text = "Please Remove Row Fom Editable Mode and Save Record";
        }
    }

    protected void CLearControl()
    {
        try
        {
            btnSave.Text = "Save";

            //BindDDL();
            //myGlobal.dt_temp = null;
            //myGlobal.autoId_temp = 0;

            // fillgvdata();
            BindGridAddNew();

        }
        catch (Exception ex)
        {
            lblms.Text = "Error Occured in CLearControl():" + ex.Message;
        }
    }

    protected void OnDataBound(object sender, EventArgs e)
    {
        Db.constr = myGlobal.getAppSettingsDataForKey("tejSAP");
        DropDownList ddlstatus = grdQuartlyRowDetail.FooterRow.FindControl("ddlststusofcall") as DropDownList;
        DataSet DsForms = Db.myGetDS("EXEC DSR_getCallTypeModeOfCall_CountryByUser '" + myGlobal.loggedInUser() + "' ");
        ddlstatus.DataSource = DsForms.Tables[0];
        ddlstatus.DataTextField = "CallStatus";
        ddlstatus.DataValueField = "CallStatus";

        ddlstatus.DataBind();
        ddlstatus.Items.Insert(0, "--Select--");

        DropDownList ddlMode = grdQuartlyRowDetail.FooterRow.FindControl("ddlststusofcallMode") as DropDownList;
        // DsForms = Db.myGetDS("select ModeOfCall from DSR_ModeOfCall");
        ddlMode.DataSource = DsForms.Tables[1];
        ddlMode.DataTextField = "ModeOfCall";
        ddlMode.DataValueField = "ModeOfCall";

        ddlMode.DataBind();
        ddlMode.Items.Insert(0, "--Select--");


        DropDownList ddlNextAction = grdQuartlyRowDetail.FooterRow.FindControl("ddlnxtactiFooter") as DropDownList;
        //DsForms = Db.myGetDS("select c.country as country,c.countrycode from tejSalespersonMap S Join Sales_Employee_country  SC On S.salesperson= Sc.SalesEmpID And  Membershipuser='" + myGlobal.loggedInUser() + "'  JOIN  rddcountrieslist C ON SC.country=C.countrycode");

        ddlNextAction.DataSource = DsForms.Tables[2];
        ddlNextAction.DataTextField = "NextAction";
        ddlNextAction.DataValueField = "NextAction";
        ddlNextAction.DataBind();
        ddlNextAction.Items.Insert(0, "--Select--");

        DropDownList ddlcountry = grdQuartlyRowDetail.FooterRow.FindControl("ddlcountryfooter") as DropDownList;
        //DsForms = Db.myGetDS("select c.country as country,c.countrycode from tejSalespersonMap S Join Sales_Employee_country  SC On S.salesperson= Sc.SalesEmpID And  Membershipuser='" + myGlobal.loggedInUser() + "'  JOIN  rddcountrieslist C ON SC.country=C.countrycode");
        ddlcountry.DataSource = DsForms.Tables[3];
        ddlcountry.DataTextField = "country";
        ddlcountry.DataValueField = "country";
        ddlcountry.DataBind();
        ddlcountry.Items.Insert(0, "--Select--");

        ddlcountryList.DataSource = DsForms.Tables[3];
        ddlcountryList.DataTextField = "country";
        ddlcountryList.DataValueField = "CountryCode";
        ddlcountryList.DataBind();
        ddlcountryList.Items.Insert(0, "--Select--");

      



    }

    public DataSet GetBU()
    {
        DataSet DSBU = null;
        try
        {
            Db.constr = myGlobal.getAppSettingsDataForKey("tejSAP");
            DSBU = Db.myGetDS(" EXEC DailySalesReport_GetBU ");

        }
        catch (Exception ex)
        {
            lblms.Text = "Error occured in GetBU() : " + ex.Message;
        }
        return DSBU;

    }

    [WebMethod]
    public static string[] GetPersonMeet(string prefix, string dbcustomer)
    {
        List<string> person = new List<string>();

        Db.constr = myGlobal.getAppSettingsDataForKey("tejSAP");

        string qry = "select PersonMet from DailySalesReports where Company='" + dbcustomer + "' and PersonMet like '%" + prefix + "%'";

        SqlDataReader rdr = Db.myGetReader(qry);
        while (rdr.Read())
        {
            person.Add(string.Format("{0}", rdr["PersonMet"].ToString()));
        }

        return person.ToArray();
    }

    //protected void btnShowModalPopup_Click(object sender, EventArgs e)
    //{
    //    ModalPopupExtender1.Show();
    //}
    protected void btnShowModalPopup1_Click(object sender, EventArgs e)
    {
        //ModalPopupExtender1.Show();
        ModalPopupExtender2.Show();
    }

    protected void btnShowModalPopup2_Click(object sender, EventArgs e)
    {
        ModalPopupExtender3.Show();

    }

    protected void btSave_Click(object sender, EventArgs e)
    {
        // txtfrdcall.Text = ViewState["Call"].ToString();


        if (string.IsNullOrEmpty(txtfrdcall.Text))
        {
            lblms.Text = "Please Select call ForwardTo ";
            return;
        }

        if (string.IsNullOrEmpty(txtdesc.Text))
        {
            lblms.Text = "Please Enter Desc";
            return;
        }


        string frdcall = txtfrdcall.Text;
        string descr = txtdesc.Text;



        ViewState["Call"] = frdcall;
        ViewState["Desc"] = descr;
        ViewState["ID"] = LblForwardCallId.Text;



    }

    protected void btnsavee_Click(object sender, EventArgs e)
    {
        // txtfrdcall.Text = ViewState["Call"].ToString();


        if (string.IsNullOrEmpty(txtreminderdate.Text))
        {
            lblms.Text = "Please Select Date ";
            return;
        }

        if (string.IsNullOrEmpty(txtdescription.Text))
        {
            lblms.Text = "Please Enter Description";
            return;
        }


        string remindate = txtreminderdate.Text;
        string remindesc = txtdescription.Text;


        ViewState["remindedate"] = remindate;
        ViewState["reminderDesc"] = remindesc;
        ViewState["ID"] = LblreminderDateID.Text;

    }

    protected void grdQuartlyRowDetail_RowDataBound(object sender, GridViewRowEventArgs e)
    {
        if (e.Row.RowType == DataControlRowType.DataRow)
        {
            if ((e.Row.RowState & DataControlRowState.Edit) > 0)
            {
                DropDownList ddList = (DropDownList)e.Row.FindControl("ddlstatuscall");
                //bind dropdown-list

                DataSet DSCallType_Mode_Country = Db.myGetDS("EXEC DSR_getCallTypeModeOfCall_CountryByUser '" + myGlobal.loggedInUser() + "'");
                //DataSet DsForms = Db.myGetDS("Select CallStatus from DSR_CallStatus");
                ddList.DataSource = DSCallType_Mode_Country.Tables[0];
                ddList.DataTextField = "CallStatus";
                ddList.DataValueField = "CallStatus";
                ddList.DataBind();
                DataRowView dr = e.Row.DataItem as DataRowView;
                ddList.SelectedValue = dr["CallStatus"].ToString();



                DropDownList ddList1 = (DropDownList)e.Row.FindControl("ddlcallmode");
                //bind dropdown-list
                //DsForms = Db.myGetDS("select  ModeOfCall from DSR_ModeOfCall");
                ddList1.DataSource = DSCallType_Mode_Country.Tables[1];
                ddList1.DataTextField = "ModeOfCall";
                ddList1.DataValueField = "ModeOfCall";
                ddList1.DataBind();
                dr = e.Row.DataItem as DataRowView;
                ddList1.SelectedValue = dr["ModeOfCall"].ToString();



                DropDownList ddlNextAction = (DropDownList)e.Row.FindControl("ddlNextActionEdit");
                //bind dropdown-list
                //DsForms = Db.myGetDS("select c.country as country ,c.countrycode  from tejSalespersonMap S Join Sales_Employee_country  SC On S.salesperson= Sc.SalesEmpID And  Membershipuser='" + myGlobal.loggedInUser() + "'  JOIN  rddcountrieslist C ON SC.country=C.countrycode");
                ddlNextAction.DataSource = DSCallType_Mode_Country.Tables[2];
                ddlNextAction.DataTextField = "NextAction";
                ddlNextAction.DataValueField = "NextAction";
                ddlNextAction.DataBind();
                dr = e.Row.DataItem as DataRowView;
               ddlNextAction.SelectedValue = dr["NextAction"].ToString();




                DropDownList ddlcountry = (DropDownList)e.Row.FindControl("ddlCountryEdit");
                //bind dropdown-list
                //DsForms = Db.myGetDS("select c.country as country ,c.countrycode  from tejSalespersonMap S Join Sales_Employee_country  SC On S.salesperson= Sc.SalesEmpID And  Membershipuser='" + myGlobal.loggedInUser() + "'  JOIN  rddcountrieslist C ON SC.country=C.countrycode");
                ddlcountry.DataSource = DSCallType_Mode_Country.Tables[3];
                ddlcountry.DataTextField = "country";
                ddlcountry.DataValueField = "country";
                ddlcountry.DataBind();
                dr = e.Row.DataItem as DataRowView;
                ddlcountry.SelectedValue = dr["country"].ToString();


            }
        }

    }

    protected void btnsaveasdraft_Click1(object sender, EventArgs e)
    {
        lblms.Text = "";

        string result = SaveUpdate(true);
        if (result == "success")
        {
            //Page.ClientScript.RegisterStartupScript(this.GetType(), "alert", "<script> alert( 'Record Saved  successfully .'); </script>");
            Response.Redirect("DSREntry.aspx?Action=DRAFT");
            //Response.Redirect("DSREntry.aspx");
        }
        else if (!string.IsNullOrEmpty(result) && result.Contains("Error"))
        {
            Response.Redirect("DSREntry.aspx?Action=DRAFTFailed");
        }
        else
        {
            return ;
        }
        //bool result = SaveUpdate(true);
        //if (result)
        //{
        //        Page.ClientScript.RegisterStartupScript(this.GetType(), "alert", "<script> alert( 'Record Saved As Draft Successfully.'); </script>");
        //        //Response.Redirect("DSREntry.aspx?Action=SUBMIT");
        //        Response.Redirect("DSREntry.aspx");
        //}
        //else
        //{
        //    Page.ClientScript.RegisterStartupScript(this.GetType(), "alert", "<script> alert( 'Failed to save draft record.'); </script>");
        //}
    }

    protected void txtvisitDate_TextChanged(object sender, EventArgs e)
    {
        GetVisitDataByDate();
    }
    
    private void GetVisitFrom_ToDate()
    {
        try
        {
            DataSet ds = Db.myGetDS("exec DSR_GetEmpFrequency '" + myGlobal.loggedInUser() + "','"+ txtvisitDate.Text +"'");
            string FreqOfRpt = ds.Tables[0].Rows[0]["FreqOfRpt"].ToString();
            string Visit_FromDt=string.Empty, Visit_ToDt = string.Empty;
            if (ds.Tables.Count > 1)
            {
                Visit_FromDt = ds.Tables[1].Rows[0]["FromDate"].ToString();
                Visit_ToDt = ds.Tables[1].Rows[0]["ToDate"].ToString();
            }
            
            if (FreqOfRpt == "WEEKLY")
            {
                if (string.IsNullOrEmpty(txtvisitDate.Text))
                {
                    txtvisitDate.Text = Visit_FromDt;
                }
                else if (txtvisitDate.Text.Trim() != Visit_FromDt.Trim())
                {
                    txtvisitDate.Text = Visit_FromDt;
                }
                //else if (Convert.ToDateTime(txtvisitDate.Text.Trim()) != Convert.ToDateTime(Visit_FromDt.Trim()))
                //{
                //    txtvisitDate.Text = Visit_FromDt;
                //}

                //txtvisitDate.Text = Visit_FromDt;
                txttodate.Text = Visit_ToDt;
                txttodate.Visible = true;             
            }
            else
            {
                if (string.IsNullOrEmpty(txtvisitDate.Text))
                {
                    txtvisitDate.Text = Visit_FromDt;
                }
                else if (txtvisitDate.Text.Trim() != Visit_FromDt.Trim())
                {
                    txtvisitDate.Text = Visit_FromDt;
                }
                //else if (Convert.ToDateTime(txtvisitDate.Text.Trim()) != Convert.ToDateTime(Visit_FromDt.Trim()))
                //{
                //    txtvisitDate.Text = Visit_FromDt;
                //}
                //txtvisitDate.Text = Visit_FromDt;
                txttodate.Text = Visit_ToDt;
                txttodate.Enabled = false;

            }
        }
        catch (Exception ex)
        {
            lblms.Text = "Error occured in GetVisitFrom_ToDate() " + ex.Message;
        }
    }

    private void GetVisitDataByDate()
    {
        try
        {
            Db.constr = myGlobal.getAppSettingsDataForKey("tejSAP");
            string LoggedInUserName = myGlobal.loggedInUser();
            lblms.Text = "";

            GetVisitFrom_ToDate();

            #region " Old logic for Visit from & to Date
            //DataSet ds2 = Db.myGetDS("exec DSR_GetEmpFrequency '" + LoggedInUserName + "'");
            //string rfarpt = ds2.Tables[0].Rows[0]["FreqOfRpt"].ToString();
            //if (rfarpt == "WEEKLY")
            //{
            //    //txtvisitDate.Text = DateTime.Now.ToString("MM/dd/yyyy");
            //    DateTime baseDate = DateTime.Today;
            //    if (!string.IsNullOrEmpty(txtvisitDate.Text))
            //    {
            //        baseDate = Convert.ToDateTime(txtvisitDate.Text);
            //    }// DateTime.Today;
            //    var today = baseDate;
            //    var yesterday = baseDate.AddDays(-1);
            //    var thisWeekStart = baseDate.AddDays(-(int)baseDate.DayOfWeek);
            //    var thisWeekEnd = thisWeekStart.AddDays(7).AddSeconds(-1);
            //    txttodate.Text = thisWeekEnd.ToString("MM/dd/yyyy");
            //    Label1.Visible = true;
            //    txttodate.Visible = true;
            //}
            //else if (rfarpt == "DAILY")
            //{
            //    if (string.IsNullOrEmpty(txtvisitDate.Text))
            //    {
            //        txtvisitDate.Text = DateTime.Now.ToString("MM/dd/yyyy");
            //        txttodate.Text = DateTime.Now.ToString("MM/dd/yyyy");
            //    }
            //    else
            //    {
            //        txttodate.Text = txtvisitDate.Text;
            //    }
            //    txttodate.Enabled = false;
            //}
            #endregion

            DataSet ds = Db.myGetDS(" EXEC DSR_GetCustVisitDataByDates '" + myGlobal.loggedInUser() + "','" + txtvisitDate.Text + "','" + txttodate.Text + "' ");
            if (ds.Tables[0].Rows.Count > 0)
            {
                grdQuartlyRowDetail.DataSource = ds.Tables[0];
                grdQuartlyRowDetail.DataBind();

                bool IsDraft = Convert.ToBoolean(ds.Tables[0].Rows[0]["IsDraft"]);
                if (IsDraft)
                {
                    btnsaveasdraft.Visible = true;
                    btnSave.Enabled = true;
                    grdQuartlyRowDetail.FooterRow.Visible = true;
                    grdQuartlyRowDetail.Enabled = true;
                }
                else
                {
                    btnsaveasdraft.Visible = false;
                    btnSave.Enabled = false;
                    grdQuartlyRowDetail.FooterRow.Visible = false;
                    grdQuartlyRowDetail.Enabled = false;
                    lblms.Text = "You have already submitted report for selected visit date";
                }
            }
            else
            {
                BindGridAddNew();
            }
        }
        catch (Exception ex)
        {
            lblms.Text = "Error occured in GetVisitDataByDate() " + ex.Message;
        }
    }

    protected void fillgvSummary()
    {
        try
        {
            Db.constr = myGlobal.getAppSettingsDataForKey("tejSAP");
            string LoggedInUserName = myGlobal.loggedInUser();

            DataSet ds2 = Db.myGetDS("exec DSR_GetWeeklyVisitSummary '" + LoggedInUserName + "'");

            if (ds2.Tables.Count > 0)
            {
                GvRptSummary.DataSource = ds2.Tables[0];
                GvRptSummary.DataBind();
            }
        }
        catch (Exception ex)
        {
            lblms.Text = "Error occured in fillgvSummary() " + ex.Message;
        }
    }

    protected void txtCustomerName_OnTextChanged(object sender, EventArgs e)
    {
        lblms.Text = string.Empty;
        Db.constr = myGlobal.getAppSettingsDataForKey("tejSAP");
        string LoggedInUserName = myGlobal.loggedInUser();

        for (int i = 0; i < grdQuartlyRowDetail.Rows.Count; i++)
        {
            TextBox cutname = grdQuartlyRowDetail.FooterRow.FindControl("txtCustomerName") as TextBox;
            string cutn = cutname.Text;

            string customer = (grdQuartlyRowDetail.Rows[i].FindControl("lblcustomername") as Label).Text;

            DropDownList ddlcountryFooter = grdQuartlyRowDetail.FooterRow.FindControl("ddlcountryfooter") as DropDownList;

            foreach (GridViewRow gvr in grdQuartlyRowDetail.Rows)
            {
                if (customer == "")
                {

                    ((ImageButton)gvr.FindControl("BtnEdit")).Visible = false;
                    ((ImageButton)gvr.FindControl("BtnDelete")).Visible = false;
                    TextBox txtconpe = grdQuartlyRowDetail.FooterRow.FindControl("txtprsonmeet") as TextBox;
                    txtconpe.Focus();
                }
                else
                {

                    ((ImageButton)gvr.FindControl("BtnEdit")).Visible = true;
                    ((ImageButton)gvr.FindControl("BtnDelete")).Visible = true;
                    TextBox txtconpe = grdQuartlyRowDetail.FooterRow.FindControl("txtprsonmeet") as TextBox;
                    txtconpe.Focus();

                }

            }
            // }
            TextBox txtCustomerName = grdQuartlyRowDetail.FooterRow.FindControl("txtCustomerName") as TextBox;
            TextBox txtCardCode = grdQuartlyRowDetail.FooterRow.FindControl("txtCardCode") as TextBox;

            if (ddlcountryFooter.Text == "--Select--")
            {
                txtCustomerName.Text = "";
                lblms.Text = "Please Select Country";
                return;
            }

            string Distinct_Repeat = string.Empty;
            //   string val22 = string.Empty;
            // int CountOfVisits = Db.myExecuteScalar("select Count(VisitType) as vitpe from DailySalesReports where    MONTH(createdOn) = '" + monthwise + "' and CreatedBy ='" + LoggedInUserName + "' and company='" + cutn1 + "'");
            int CountOfVisits = Db.myExecuteScalar(" EXEC DSR_GetDistict_RepeatVisit '" + myGlobal.loggedInUser() + "','" + txtCustomerName.Text + "','" + txtCardCode.Text + "' ");
            if (CountOfVisits > 0)
            {
                Distinct_Repeat = "R";
            }
            else
            {
                Distinct_Repeat = "D";
            }

            TextBox txtDR = grdQuartlyRowDetail.FooterRow.FindControl("txtdisrptt") as TextBox;
            txtDR.Text = Distinct_Repeat;

            DataSet ds2 = Db.myGetDS("exec getResellerCodeByName '" + ddlcountryFooter.SelectedValue + "','" + txtCustomerName.Text + "'");
            if (ds2.Tables[0].Rows.Count > 0)
            {
                string custCardCode = ds2.Tables[0].Rows[0]["CardCode"].ToString();
                txtCardCode = grdQuartlyRowDetail.FooterRow.FindControl("txtCardCode") as TextBox;
                txtCardCode.Text = custCardCode;
            }
            else
            {
                txtCustomerName.Text = "";
                txtCustomerName.Focus();
                lblms.Text = "Please Select Reseller from List";
            }
        }

        //lblms.Text = "";
        //Db.constr = myGlobal.getAppSettingsDataForKey("tejSAP");
        //string LoggedInUserName = myGlobal.loggedInUser();

        //for (int i = 0; i < grdQuartlyRowDetail.Rows.Count; i++)
        //{
        //    TextBox cutname = grdQuartlyRowDetail.FooterRow.FindControl("txtCustomerName") as TextBox;
        //    string cutn = cutname.Text;

        //    string customer = (grdQuartlyRowDetail.Rows[i].FindControl("lblcustomername") as Label).Text;

        //    DropDownList ddlcountryFooter = grdQuartlyRowDetail.FooterRow.FindControl("ddlcountryfooter") as DropDownList;


        //    foreach (GridViewRow gvr in grdQuartlyRowDetail.Rows)
        //    {
        //        if (customer == "")
        //        {
        //            ((ImageButton)gvr.FindControl("BtnEdit")).Visible = false;
        //            ((ImageButton)gvr.FindControl("BtnDelete")).Visible = false;
        //            TextBox txtconpe = grdQuartlyRowDetail.FooterRow.FindControl("txtprsonmeet") as TextBox;
        //            txtconpe.Focus();
        //        }
        //        else
        //        {

        //            ((ImageButton)gvr.FindControl("BtnEdit")).Visible = true;
        //            ((ImageButton)gvr.FindControl("BtnDelete")).Visible = true;
        //            TextBox txtconpe = grdQuartlyRowDetail.FooterRow.FindControl("txtprsonmeet") as TextBox;
        //            txtconpe.Focus();
        //        }

        //    }
        //    // }
        //    TextBox txtCustomerName = grdQuartlyRowDetail.FooterRow.FindControl("txtCustomerName") as TextBox;
        //    TextBox txtCardCode= grdQuartlyRowDetail.FooterRow.FindControl("txtCardCode") as TextBox;

        //    string Distinct_Repeat = string.Empty;
        //    //   string val22 = string.Empty;
        //   // int CountOfVisits = Db.myExecuteScalar("select Count(VisitType) as vitpe from DailySalesReports where    MONTH(createdOn) = '" + monthwise + "' and CreatedBy ='" + LoggedInUserName + "' and company='" + cutn1 + "'");
        //    int CountOfVisits = Db.myExecuteScalar(" EXEC DSR_GetDistict_RepeatVisit '" + myGlobal.loggedInUser() + "','" + txtCustomerName.Text + "','" + txtCardCode.Text + "' ");
        //    if (CountOfVisits > 0)
        //    {
        //        Distinct_Repeat = "R";
        //    }
        //    else
        //    {
        //        Distinct_Repeat = "D";
        //    }

        //    TextBox txtDR = grdQuartlyRowDetail.FooterRow.FindControl("txtdisrptt") as TextBox;
        //    txtDR.Text = Distinct_Repeat;

        //    DataSet ds2 = Db.myGetDS("exec getResellerCodeByName '" + ddlcountryFooter.SelectedValue + "','" + txtCustomerName.Text + "'");
        //    if (ds2.Tables[0].Rows.Count == 0)
        //    {
        //        /*if comapny name is not availablr at procedure*/
        //    }
        //    else
        //    {
        //        string custCardCode = ds2.Tables[0].Rows[0]["CardCode"].ToString();
        //        txtCardCode = grdQuartlyRowDetail.FooterRow.FindControl("txtCardCode") as TextBox;
        //        txtCardCode.Text = custCardCode;
        //    }
        //}
    }

    protected void ddlCountry_SelectedIndexChanged(object sender, EventArgs e)
    {
        lblms.Text = string.Empty;
     
        DropDownList ddlcountry = (DropDownList)sender;
        lblCountry.Text = ddlcountry.SelectedItem.Text;

        for (int i = 0; i < grdQuartlyRowDetail.Rows.Count; i++)
        {
            TextBox cutname = grdQuartlyRowDetail.FooterRow.FindControl("txtCustomerName") as TextBox;
            string cutn = cutname.Text;

            string customer = (grdQuartlyRowDetail.Rows[i].FindControl("lblcustomername") as Label).Text;

            DropDownList ddlcountryFooter = grdQuartlyRowDetail.FooterRow.FindControl("ddlcountryfooter") as DropDownList;
            foreach (GridViewRow gvr in grdQuartlyRowDetail.Rows)
            {
                if (customer == "")
                {
                    ((ImageButton)gvr.FindControl("BtnEdit")).Visible = false;
                    ((ImageButton)gvr.FindControl("BtnDelete")).Visible = false;
                    TextBox txtconpe = grdQuartlyRowDetail.FooterRow.FindControl("txtprsonmeet") as TextBox;
                    txtconpe.Focus();
                }
                else
                {

                    ((ImageButton)gvr.FindControl("BtnEdit")).Visible = true;
                    ((ImageButton)gvr.FindControl("BtnDelete")).Visible = true;
                    TextBox txtconpe = grdQuartlyRowDetail.FooterRow.FindControl("txtprsonmeet") as TextBox;
                    txtconpe.Focus();
                }
            }
        }
    }

    protected void txtDateFooter_TextChanged(object sender, EventArgs e)
    {
        try
        {
            lblms.Text = "";

            DateTime fromdate = Convert.ToDateTime(txtvisitDate.Text);
            DateTime todate = Convert.ToDateTime(txttodate.Text);

            TextBox datefooter = grdQuartlyRowDetail.FooterRow.FindControl("txtDateFooter") as TextBox;
            if (datefooter != null)
            {
                var datefootedit = Convert.ToDateTime(datefooter.Text);
                if (datefootedit < fromdate)
                {
                    datefooter.Text = "";
                    lblms.Text = "Selected Date Should Not Less Than From Date!!";
                    return;
                }
                else if (datefootedit > todate)
                {
                    datefooter.Text = "";
                    lblms.Text = "Selected Date Should Not Greater Than To Date!!";
                    return;
                }
            }
        }
        catch (Exception ex)
        {

        }
    }

    protected void txtDateEdit_TextChanged(object sender, EventArgs e)
    {
        try
        {
            lblms.Text = "";
            DateTime fromdate = Convert.ToDateTime(txtvisitDate.Text);
            DateTime todate = Convert.ToDateTime(txttodate.Text);
            int gvid = Convert.ToInt32(ViewState["GVID"].ToString());

            lblms.Text = "";

            DateTime fromdatee = Convert.ToDateTime(txtvisitDate.Text);
            DateTime todatee = Convert.ToDateTime(txttodate.Text);
            string VisitEdit = (((TextBox)grdQuartlyRowDetail.Rows[grdQuartlyRowDetail.EditIndex].FindControl("txtDateEdit")).Text);
            DateTime Vdate = Convert.ToDateTime(VisitEdit.ToString());
            if (Vdate < fromdate)
            {
                (((TextBox)grdQuartlyRowDetail.Rows[grdQuartlyRowDetail.EditIndex].FindControl("txtDateEdit")).Text) = "";
                lblms.Text = "Selected Date Should Not Less Than From Date!!";
                return;
            }
            else
                if (Vdate > todate)
                {
                    (((TextBox)grdQuartlyRowDetail.Rows[grdQuartlyRowDetail.EditIndex].FindControl("txtDateEdit")).Text) = "";
                    lblms.Text = "Selected Date Should Not Greater Than To Date!!";
                    return;
                }

        }

        catch (Exception ex)
        {

        }
    }

    public string SaveUpdate( Boolean IsDraft )
    {
        string result = string.Empty;
        try
        {
            try
            {
                    Db.constr = myGlobal.getAppSettingsDataForKey("tejSAP");
                    string LoggedInUserName = myGlobal.loggedInUser();
                
                    TextBox cutname1 = grdQuartlyRowDetail.FooterRow.FindControl("txtCustomerName") as TextBox;

                    string Vdate = txtvisitDate.Text;

                    TextBox cutCC = grdQuartlyRowDetail.FooterRow.FindControl("txtCardCode") as TextBox;
                    TextBox cutnamee = grdQuartlyRowDetail.FooterRow.FindControl("txtCustomerName") as TextBox;
                    TextBox person = grdQuartlyRowDetail.FooterRow.FindControl("txtprsonmeet") as TextBox;
                    TextBox bu = grdQuartlyRowDetail.FooterRow.FindControl("txtBU") as TextBox;
                    TextBox actiondne = grdQuartlyRowDetail.FooterRow.FindControl("txtactiondone") as TextBox;
                    DropDownList status = grdQuartlyRowDetail.FooterRow.FindControl("ddlststusofcall") as DropDownList;
                    TextBox feedbck = grdQuartlyRowDetail.FooterRow.FindControl("txtfeedback") as TextBox;

                    TextBox visittype = grdQuartlyRowDetail.FooterRow.FindControl("txtdisrptt") as TextBox;
                    TextBox desigg = grdQuartlyRowDetail.FooterRow.FindControl("txtdesig") as TextBox;
                    TextBox Contactt = grdQuartlyRowDetail.FooterRow.FindControl("txtconnumber") as TextBox;
                    TextBox Emaill = grdQuartlyRowDetail.FooterRow.FindControl("txtemailid") as TextBox;
                    TextBox Bizz = grdQuartlyRowDetail.FooterRow.FindControl("txtexpectedbuss") as TextBox;
                    DropDownList CallMode = grdQuartlyRowDetail.FooterRow.FindControl("ddlststusofcallMode") as DropDownList;

                    DropDownList NextAction = grdQuartlyRowDetail.FooterRow.FindControl("ddlnxtactiFooter") as DropDownList;

                    TextBox ActualDate = grdQuartlyRowDetail.FooterRow.FindControl("txtDateFooter") as TextBox;
                    DropDownList Country = grdQuartlyRowDetail.FooterRow.FindControl("ddlcountryfooter") as DropDownList;

                    string Fcall = txtfrdcall.Text;
                    string Fremark = txtdesc.Text;

                    string ReminDate = txtreminderdate.Text;
                    string reminDesc = txtdescription.Text;


                    if ( !string.IsNullOrEmpty(cutnamee.Text) && !string.IsNullOrEmpty(person.Text) && !string.IsNullOrEmpty(actiondne.Text) && !string.IsNullOrEmpty(status.Text) && !string.IsNullOrEmpty(feedbck.Text))
                    {
                        if (Country.Text == "--Select--")
                        {
                            lblms.Text = "Please Select Country";
                            return "Please Select Country";
                        }
                        if (String.IsNullOrEmpty(ActualDate.Text))
                        {
                            lblms.Text = "Please Select Date";
                            return "Please Select Date";
                        }
                        if (CallMode.Text == "--Select--")
                        {
                            lblms.Text = "Please Select Call Mode";
                            return "Please Select Call Mode";
                        }
                        if (status.Text == "--Select--")
                        {
                            lblms.Text = "Please Select Call Type";
                            return "Please Select Call Type";
                        }

                        if (NextAction.Text == "--Select--")
                        {
                            lblms.Text = "Please Select Next Action";
                            return "Please Select Next Action";
                        }

                         Db.constr = myGlobal.getAppSettingsDataForKey("tejSAP");
                         using (var connection = new SqlConnection(Db.constr))
                         {
                             if (connection.State == ConnectionState.Closed)
                             {
                                 connection.Open();
                             }

                             SqlCommand cmd = new SqlCommand();

                             cmd.CommandType = CommandType.StoredProcedure;
                             cmd.CommandText = "DSR_InsertUpdate";
                             cmd.Connection = connection;

                             cmd.Parameters.Add("@p_VisitId", SqlDbType.Int).Value = 0;
                             cmd.Parameters.Add("@p_VisitDate", SqlDbType.NVarChar).Value = txtvisitDate.Text;
                             cmd.Parameters.Add("@p_ToDate", SqlDbType.NVarChar).Value = txttodate.Text;
                             cmd.Parameters.Add("@p_VisitType", SqlDbType.NVarChar).Value = visittype.Text;
                             cmd.Parameters.Add("@p_IsNewPartner", SqlDbType.Bit).Value = false;
                             cmd.Parameters.Add("@p_Country", SqlDbType.NVarChar).Value = Country.Text;
                             cmd.Parameters.Add("@p_CardCode", SqlDbType.NVarChar).Value = cutCC.Text;
                             cmd.Parameters.Add("@p_Company", SqlDbType.NVarChar).Value = cutnamee.Text;
                             cmd.Parameters.Add("@p_PersonMet", SqlDbType.NVarChar).Value = person.Text;
                             cmd.Parameters.Add("@p_Email", SqlDbType.NVarChar).Value = Emaill.Text;
                             cmd.Parameters.Add("@p_Designation", SqlDbType.NVarChar).Value = desigg.Text;
                             cmd.Parameters.Add("@p_ContactNo", SqlDbType.NVarChar).Value = Contactt.Text;
                             cmd.Parameters.Add("@p_BU", SqlDbType.NVarChar).Value = bu.Text;
                             cmd.Parameters.Add("@p_Discussion", SqlDbType.NVarChar).Value = actiondne.Text;
                             if (string.IsNullOrEmpty(Bizz.Text))
                                 Bizz.Text = "0";
                             cmd.Parameters.Add("@p_ExpectedBusinessAmt", SqlDbType.Decimal).Value = Bizz.Text;
                             cmd.Parameters.Add("@p_CallStatus", SqlDbType.NVarChar).Value = status.Text;
                             cmd.Parameters.Add("@p_ModeOfCall", SqlDbType.NVarChar).Value = CallMode.Text;
                             cmd.Parameters.Add("@p_NextAction", SqlDbType.NVarChar).Value = NextAction.Text;
                             cmd.Parameters.Add("@p_Feedback", SqlDbType.NVarChar).Value = feedbck.Text;
                             cmd.Parameters.Add("@p_ForwardCallToEmail", SqlDbType.NVarChar).Value = Fcall;
                             cmd.Parameters.Add("@p_ForwardRemark", SqlDbType.NVarChar).Value = Fremark;
                             cmd.Parameters.Add("@p_IsDraft", SqlDbType.Bit).Value = IsDraft;
                             //cmd.Parameters.Add("@p_NextReminderDate", SqlDbType.DateTime).Value = ;
                             //cmd.Parameters.Add("@p_DocDraftDate", SqlDbType.DateTime).Value = false;
                             cmd.Parameters.Add("@p_LoggedInUser", SqlDbType.NVarChar).Value = myGlobal.loggedInUser();
                             cmd.Parameters.Add("@p_ActualVisitDate", SqlDbType.NVarChar).Value = ActualDate.Text;
                             if (string.IsNullOrEmpty(ReminDate))
                             {
                                 cmd.Parameters.Add("@p_ReminderDate", SqlDbType.NVarChar).Value = DBNull.Value;
                             }
                             else
                             {
                                 cmd.Parameters.Add("@p_ReminderDate", SqlDbType.NVarChar).Value = ReminDate;
                             }
                             cmd.Parameters.Add("@p_ReminderDesc", SqlDbType.NVarChar).Value = reminDesc;
                             cmd.ExecuteNonQuery();
                             cmd.Dispose();

                             if (connection.State == ConnectionState.Open)
                             {
                                 connection.Close();
                             }
                             result = "success";
                         }
                    }
                    
                    Db.constr = myGlobal.getAppSettingsDataForKey("tejSAP");
                    using (var connection = new SqlConnection(Db.constr))
                    {
                        if (connection.State == ConnectionState.Closed)
                        {
                            connection.Open();
                        }
                        for (int i = 0; i < grdQuartlyRowDetail.Rows.Count; i++)
                        {
                            string LineId = grdQuartlyRowDetail.DataKeys[i].Value.ToString();
                            string id = (grdQuartlyRowDetail.Rows[i].FindControl("lblvisitid") as Label).Text;
                            string CC = (grdQuartlyRowDetail.Rows[i].FindControl("lblcardcode") as Label).Text;
                            string customer = (grdQuartlyRowDetail.Rows[i].FindControl("lblcustomername") as Label).Text;
                            customer = customer.Replace("'", "");

                            string permeet = (grdQuartlyRowDetail.Rows[i].FindControl("lblPermeet") as Label).Text;
                            permeet = permeet.Replace("'", "");

                            string Email = (grdQuartlyRowDetail.Rows[i].FindControl("lblEmail") as Label).Text;
                            Email = Email.Replace("'", "");

                            string Contact = (grdQuartlyRowDetail.Rows[i].FindControl("lblconno") as Label).Text;
                            string desig = (grdQuartlyRowDetail.Rows[i].FindControl("lbldesign") as Label).Text;
                            desig = desig.Replace("'", "");

                            string discrpt = (grdQuartlyRowDetail.Rows[i].FindControl("lbldisrpt") as Label).Text;

                            string expamt = (grdQuartlyRowDetail.Rows[i].FindControl("lblexpbusAmt") as Label).Text;
                            expamt = expamt.Replace(",", "");

                            string calstatus = (grdQuartlyRowDetail.Rows[i].FindControl("lblcallstatus") as Label).Text;
                            string feedback = (grdQuartlyRowDetail.Rows[i].FindControl("lblfeedbck") as Label).Text;
                            feedback = feedback.Replace("'", "");
                            string discussion = (grdQuartlyRowDetail.Rows[i].FindControl("lbldiscussion") as Label).Text;
                            discussion = discussion.Replace("'", "");
                            string BU = (grdQuartlyRowDetail.Rows[i].FindControl("lblBU") as Label).Text;
                            BU = BU.Replace("'", "");
                            string call = (grdQuartlyRowDetail.Rows[i].FindControl("lblcall") as Label).Text;
                            call = call.Replace("'", "");
                            string desc = (grdQuartlyRowDetail.Rows[i].FindControl("lblrnk") as Label).Text;
                            desc = desc.Replace("'", "");
                            string callmo = (grdQuartlyRowDetail.Rows[i].FindControl("lblcallMode") as Label).Text;
                            string remdate = (grdQuartlyRowDetail.Rows[i].FindControl("lblreminder") as Label).Text;
                            string remdesc = (grdQuartlyRowDetail.Rows[i].FindControl("lblremidesc") as Label).Text;
                            remdesc = remdesc.Replace("'", "");
                            string nextaction = (grdQuartlyRowDetail.Rows[i].FindControl("lblnextaction") as Label).Text;
                            string Actualdate = (grdQuartlyRowDetail.Rows[i].FindControl("lbldate") as Label).Text;
                            string cuntry = (grdQuartlyRowDetail.Rows[i].FindControl("lblcountry") as Label).Text;

                            if (!string.IsNullOrEmpty(customer) && !string.IsNullOrEmpty(Actualdate) && cuntry != "--Select--" && callmo != "--Select--" && calstatus!="--Select--" && !string.IsNullOrEmpty(discussion) && !string.IsNullOrEmpty(feedback))
                            {
                                SqlCommand cmd = new SqlCommand();
                                cmd.CommandType = CommandType.StoredProcedure;
                                cmd.CommandText = "DSR_InsertUpdate";
                                cmd.Connection = connection;

                                cmd.Parameters.Add("@p_VisitId", SqlDbType.Int).Value = id;
                                cmd.Parameters.Add("@p_VisitDate", SqlDbType.NVarChar).Value = txtvisitDate.Text;
                                cmd.Parameters.Add("@p_ToDate", SqlDbType.NVarChar).Value = txttodate.Text;
                                cmd.Parameters.Add("@p_VisitType", SqlDbType.NVarChar).Value = discrpt;
                                cmd.Parameters.Add("@p_IsNewPartner", SqlDbType.Bit).Value = false;
                                cmd.Parameters.Add("@p_Country", SqlDbType.NVarChar).Value = cuntry;
                                cmd.Parameters.Add("@p_CardCode", SqlDbType.NVarChar).Value = CC;
                                cmd.Parameters.Add("@p_Company", SqlDbType.NVarChar).Value = customer;
                                cmd.Parameters.Add("@p_PersonMet", SqlDbType.NVarChar).Value = permeet;
                                cmd.Parameters.Add("@p_Email", SqlDbType.NVarChar).Value = Email;
                                cmd.Parameters.Add("@p_Designation", SqlDbType.NVarChar).Value = desig;
                                cmd.Parameters.Add("@p_ContactNo", SqlDbType.NVarChar).Value = Contact;
                                cmd.Parameters.Add("@p_BU", SqlDbType.NVarChar).Value = BU;
                                cmd.Parameters.Add("@p_Discussion", SqlDbType.NVarChar).Value = discussion;
                                if (string.IsNullOrEmpty(expamt))
                                {
                                    expamt = "0";
                                }
                                cmd.Parameters.Add("@p_ExpectedBusinessAmt", SqlDbType.Decimal).Value = expamt;
                                cmd.Parameters.Add("@p_CallStatus", SqlDbType.NVarChar).Value = calstatus;
                                cmd.Parameters.Add("@p_ModeOfCall", SqlDbType.NVarChar).Value = callmo;
                                cmd.Parameters.Add("@p_NextAction", SqlDbType.NVarChar).Value = nextaction;
                                cmd.Parameters.Add("@p_Feedback", SqlDbType.NVarChar).Value = feedback;
                                cmd.Parameters.Add("@p_ForwardCallToEmail", SqlDbType.NVarChar).Value = call;
                                cmd.Parameters.Add("@p_ForwardRemark", SqlDbType.NVarChar).Value = desc;
                                cmd.Parameters.Add("@p_IsDraft", SqlDbType.Bit).Value = IsDraft;
                                //cmd.Parameters.Add("@p_NextReminderDate", SqlDbType.DateTime).Value = ;
                                //cmd.Parameters.Add("@p_DocDraftDate", SqlDbType.DateTime).Value = false;
                                cmd.Parameters.Add("@p_LoggedInUser", SqlDbType.NVarChar).Value = myGlobal.loggedInUser();
                                cmd.Parameters.Add("@p_ActualVisitDate", SqlDbType.NVarChar).Value = Actualdate;
                                if (string.IsNullOrEmpty(remdate))
                                {
                                    cmd.Parameters.Add("@p_ReminderDate", SqlDbType.NVarChar).Value = DBNull.Value;
                                }
                                else
                                {
                                    cmd.Parameters.Add("@p_ReminderDate", SqlDbType.NVarChar).Value = remdate;
                                }
                                cmd.Parameters.Add("@p_ReminderDesc", SqlDbType.NVarChar).Value = remdesc;
                                cmd.ExecuteNonQuery();
                                cmd.Dispose();
                                result = "success";
                            }
                        }

                        if (connection.State == ConnectionState.Open)
                        {
                            connection.Close();
                        }
                 
                    //btnsaveasdraft.Visible = true;
            }
        }
        catch (Exception ex)
        {
            lblms.Text = "Error occured in BtnSave_Click() :" + ex.Message;
            //lblms.Text = "Please Remove Row Fom Editable Mode and Save Record";
            result = "Error occured in BtnSave_Click() :" + ex.Message;
            try
            {
                Db.myExecuteSQL("Insert into DSR_Errorlog ( ErrorMsg,Remarks,createdOn,createdBy ) values (' IsDraft : " + IsDraft.ToString() + "','" + ex.Message + "',getdate(),'" + myGlobal.loggedInUser() + "')");
            }
            catch { }
        }
          
        }
        catch (Exception ex)
        {
            lblms.Text = "Error occured in SaveUpdate() : " + ex.Message;
            result = "Error occured in SaveUpdate() : " + ex.Message;
            try
            {
                Db.myExecuteSQL("Insert into DSR_Errorlog ( ErrorMsg,Remarks,createdOn,createdBy ) values (' IsDraft : " + IsDraft.ToString() + "','" + ex.Message + "',getdate(),'" + myGlobal.loggedInUser() + "')");
            }
            catch { }
        }
        return result;
    }

    protected void btNewReseller_Click(object sender, EventArgs e)
    {

        try
        {
            // txtnewreseller.Text = string.Empty;


            string result = string.Empty;
            Db.constr = myGlobal.getAppSettingsDataForKey("tejSAP");
            string LoggedInUserName = myGlobal.loggedInUser();

            int maxid = Db.myExecuteScalar("select IsNull(max(resellerId),0) as maxid from DSR_NewResellerEntry");

            int cardcodeno = maxid + 1;
            using (var connection = new SqlConnection(Db.constr))
            {
                if (connection.State == ConnectionState.Closed)
                {
                    connection.Open();
                }
                string NewResellercrdCde = ddlcountryList.SelectedValue + "000" + cardcodeno;
                SqlCommand cmd = new SqlCommand();

                cmd.CommandType = CommandType.StoredProcedure;
                cmd.CommandText = "DSR_InsertNewRessler";
                cmd.Connection = connection;


                cmd.Parameters.Add("@p_CountryCode", SqlDbType.NVarChar).Value = ddlcountryList.SelectedValue;
                cmd.Parameters.Add("@p_Country", SqlDbType.NVarChar).Value = ddlcountryList.SelectedItem.Text;
                cmd.Parameters.Add("@p_CardCode", SqlDbType.NVarChar).Value = NewResellercrdCde;
                cmd.Parameters.Add("@p_NewResellerName", SqlDbType.NVarChar).Value = txtnewreseller.Text;
                cmd.Parameters.Add("@p_LoggedInUser", SqlDbType.NVarChar).Value = LoggedInUserName;
                cmd.Parameters.Add("@p_Response", SqlDbType.NVarChar, 1000).Direction = ParameterDirection.Output;

                cmd.ExecuteNonQuery();

                result = cmd.Parameters["@p_Response"].Value.ToString();
                cmd.Dispose();

                if (connection.State == ConnectionState.Open)
                {
                    connection.Close();
                }
                lblms.Text = result;
                ddlcountryList.SelectedIndex = -1;
                ddlcountryList.SelectedItem.Text = "--Select--";

                txtnewreseller.Text = string.Empty;

                for (int i = 0; i < grdQuartlyRowDetail.Rows.Count; i++)
                {
                    string customer = (grdQuartlyRowDetail.Rows[i].FindControl("lblcustomername") as Label).Text;
                    if (customer == "")
                    {
                        foreach (GridViewRow gvr in grdQuartlyRowDetail.Rows)
                        {
                            ((ImageButton)gvr.FindControl("BtnEdit")).Visible = false;
                            ((ImageButton)gvr.FindControl("BtnDelete")).Visible = false;
                        }
                    }
                    else
                    {
                        foreach (GridViewRow gvr in grdQuartlyRowDetail.Rows)
                        {
                            ((ImageButton)gvr.FindControl("BtnEdit")).Visible = true;
                            ((ImageButton)gvr.FindControl("BtnDelete")).Visible = true;
                        }
                    }
                }


            }

            ModalPopupExtender8.Hide();
        }

        catch (Exception ex)
        {
            lblms.Text = "Error occured in Savereseller: " + ex.Message;

        }
    }

    protected void btnexit_Click(object sender, EventArgs e)
    {
        lblms.Text = string.Empty;
        for (int i = 0; i < grdQuartlyRowDetail.Rows.Count; i++)
        {
            string customer = (grdQuartlyRowDetail.Rows[i].FindControl("lblcustomername") as Label).Text;
            if (customer == "")
            {
                foreach (GridViewRow gvr in grdQuartlyRowDetail.Rows)
                {
                    ((ImageButton)gvr.FindControl("BtnEdit")).Visible = false;
                    ((ImageButton)gvr.FindControl("BtnDelete")).Visible = false;
                }
            }
            else
            {
                foreach (GridViewRow gvr in grdQuartlyRowDetail.Rows)
                {
                    ((ImageButton)gvr.FindControl("BtnEdit")).Visible = true;
                    ((ImageButton)gvr.FindControl("BtnDelete")).Visible = true;
                }
            }
        }
        ModalPopupExtender8.Hide();
    }


    protected void btnShowModalPopup_Click(object sender, EventArgs e)
    {

        //ModalPopupExtender1.Show();
        ModalPopupExtender8.Show();

    }



    //public void SendDailyReportMails()
    //{

    //    try
    //    {


    //        DataSet DS = Db.myGetDS(" DSR_GetDailyVisitReportToSendEMail '" + myGlobal.loggedInUser() + "','" + txtvisitDate.Text + "','" + txttodate.Text + "' ");
    //        if (DS.Tables.Count > 0)
    //        {
    //            if (DS.Tables[0].Rows.Count > 0)
    //            {
    //                if (DS.Tables.Count > 1)
    //                {
    //                    string Country = DS.Tables[1].Rows[0]["Country"].ToString();
    //                    string FilePath = string.Empty;
    //                    if (Country == "KE")
    //                        FilePath = Server.MapPath("~\\excelFileUpload\\DSR\\KE- Daily Visit Report.xlsx");
    //                    else if (Country == "TZ")
    //                        FilePath = Server.MapPath("~\\excelFileUpload\\DSR\\TZ- Daily Visit Report.xlsx");
    //                    else if (Country == "UG")
    //                        FilePath = Server.MapPath("~\\excelFileUpload\\DSR\\UG- Daily Visit Report.xlsx");
    //                    else if (Country == "ZA")
    //                        FilePath = Server.MapPath("~\\excelFileUpload\\DSR\\ZA- Daily Visit Report.xlsx");
    //                    else if (Country == "MA")
    //                        FilePath = Server.MapPath("~\\excelFileUpload\\DSR\\MA- Daily Visit Report.xlsx");
    //                    else if (Country == "DU")
    //                        FilePath = Server.MapPath("~\\excelFileUpload\\DSR\\DU- Daily Visit Report.xlsx");
    //                    else  // this is for all countries except above
    //                        FilePath = Server.MapPath("~\\excelFileUpload\\DSR\\Daily Visit Report.xlsx");

    //                    ExportToExcel exportToExcel = new ExportToExcel();
    //                    string result = exportToExcel.ExportDataToExcel(DS.Tables[0], FilePath);
    //                    if (result == "success")
    //                    {
    //                        string SlpName = DS.Tables[1].Rows[0]["SlpName"].ToString();
    //                        string ccMail = DS.Tables[1].Rows[0]["CCMail"].ToString();
    //                        string ToEmail = DS.Tables[1].Rows[0]["ToEmail"].ToString();
    //                        if (string.IsNullOrEmpty(ToEmail))
    //                        {
    //                            ToEmail = "pramod@reddotdistribution.com";
    //                        }
    //                        string html = " Dear " + ToEmail + ", <br/><br/>  Please find attached <b> " + SlpName + " </b>'s customer visit report. <br/> ";
    //                        html = html + " Visit Date  -  <b> " + Convert.ToDateTime(txtvisitDate.Text).ToString("dd/MMM/yyyy") + " </b>  to  <b>  " + Convert.ToDateTime(txttodate.Text).ToString("dd/MMM/yyyy") + " </b>  <br/><br/>";
    //                        html = html + "Best Regards, <br/> Red Dot Distribution.";

    //                        //ToEmail = "pramod@reddotdistribution.com";
    //                        //ccMail = "pramod@reddotdistribution.com";

    //                        try
    //                        {
    //                            Mail.SendSingleAttachPV("reddotstaff@reddotdistribution.com", ToEmail, ccMail, DateTime.Now.ToString("dd/MMM/yyyy") + " - Customer visit Report - " + SlpName, html, true, FilePath);
    //                        }
    //                        catch (Exception ex)
    //                        {
    //                            string sql = " Insert into DSR_ErrorLog(ErrorMsg,Remarks,CreatedOn,CreatedBy) Values ( ' " + ex.Message + " ', 'Error occured to send mail - ToEmail - " + ToEmail + " ; ccMail - " + ccMail + " ' , getdate(),'" + myGlobal.loggedInUser() + "') ";
    //                            Db.myExecuteSQL(sql);

    //                            lblErrorMsg.Text = "Error to send mail - 1 " + ex.Message + sql;
    //                            Page.ClientScript.RegisterStartupScript(this.GetType(), "alert", "<script> alert( '" + "Error to send mail - 1 " + ex.Message + "'); </script>");
    //                        }
    //                    }
    //                    else
    //                    {
    //                        string sqlqry = " Insert into DSR_ErrorLog(ErrorMsg,Remarks,CreatedOn,CreatedBy) Values ( ' " + result + " ', 'Error occured to white data in an excel', getdate(),'" + myGlobal.loggedInUser() + "') ";
    //                        Db.myExecuteSQL(sqlqry);

    //                        lblErrorMsg.Text = "Error to generate excel - " + result + sqlqry;
    //                        Page.ClientScript.RegisterStartupScript(this.GetType(), "alert", "<script> alert( '" + "Error to generate excel - " + result + "'); </script>");
    //                    }
    //                }
    //            }
    //        }
    //    }
    //    catch (Exception ex)
    //    {
    //        string sql = " Insert into DSR_ErrorLog(ErrorMsg,Remarks,CreatedOn,CreatedBy) Values ( ' " + ex.Message + " ', 'Error occured to send mail '  , getdate(),'" + myGlobal.loggedInUser() + "') ";
    //        Db.myExecuteSQL(sql);
    //        lblErrorMsg.Text = "Error to send mail -2 " + ex.Message + sql;
    //        Page.ClientScript.RegisterStartupScript(this.GetType(), "alert", "<script> alert( '" + "Error to send mail -2 " + ex.Message + "'); </script>");
    //    }

    //}


}
