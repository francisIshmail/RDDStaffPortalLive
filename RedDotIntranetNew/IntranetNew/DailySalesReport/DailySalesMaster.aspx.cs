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

public partial class IntranetNew_Daily_Sales_Report_DailySalesMaster : System.Web.UI.Page
{

    protected void Page_Load(object sender, EventArgs e)
    {
        if (!IsPostBack)
        {
            Db.constr = myGlobal.getAppSettingsDataForKey("tejSAP");
            string LoggedInUserName = myGlobal.loggedInUser();



            txtvisitDate.Text = DateTime.Now.ToString("MM/dd/yyyy");
            BindDDL();
            fillgv();


            if (Request.QueryString["Action"] == "DELETED")
            {
                Page.ClientScript.RegisterStartupScript(this.GetType(), "alert", "<script> alert( 'Record Deleted successfully.'); </script>");
            }
            else if (Request.QueryString["Action"] == "SAVE")
            {
                Page.ClientScript.RegisterStartupScript(this.GetType(), "alert", "<script> alert( 'Record Saved successfully.');</script>");
            }
            else if (Request.QueryString["Action"] == "UPDATED")
            {
                Page.ClientScript.RegisterStartupScript(this.GetType(), "alert", "<script> alert( 'Record Updated successfully.');</script>");
            }
            else if (Request.QueryString["Action"] == "DRAFT")
            {
                Page.ClientScript.RegisterStartupScript(this.GetType(), "alert", "<script> alert( 'Record successfully Saved At Draft Mode.');</script>");
            }

            int count = Convert.ToInt32(Request.QueryString["GVid"]);
             int count1 = Convert.ToInt32(Request.QueryString["GVid"]);
            int salid = Convert.ToInt32(Request.QueryString["saleid"]);

            if (count > 0)  /*edit  functionality */
            {
                btndel.Visible = true;
                DataSet ds = Db.myGetDS("select * from DailySalesReports where   VisitId=" + Request.QueryString["GVid"] + "");
                if (ds.Tables[0].Rows.Count > 0)
                {
                    string dft = Session["a"].ToString();
                    string nwpart = Session["b"].ToString();

                    if (dft == "False")
                    {
                        btnsaveasdraft.Visible = false;
                        btnSave.Text = "Update";
                    }
                    else
                    {
                        btnsaveasdraft.Visible = true;
                        btnSave.Text = "Save";
                    }
                    string a = "hghg";
                    lblcountvisited.Text = a;  



                    ddlCountry.SelectedItem.Value = ds.Tables[0].Rows[0]["Country"].ToString();
                    ddlCountry.SelectedItem.Text = ds.Tables[0].Rows[0]["Country"].ToString();
                    ddlCountry.Enabled = false;
                    txtCustomerName.Text = ds.Tables[0].Rows[0]["Company"].ToString();
                    txtCustomerName.Enabled = false;
                    //  txtreminderdate.Text = ds.Tables[0].Rows[0]["NextReminderDate"].ToString().Trim();
                    if (ds.Tables[0].Rows[0]["NextReminderDate"] != null && !DBNull.Value.Equals(ds.Tables[0].Rows[0]["NextReminderDate"]))
                    {
                        DateTime NextRDt = Convert.ToDateTime(ds.Tables[0].Rows[0]["NextReminderDate"]);
                        txtreminderdate.Text = NextRDt.ToString("MM/dd/yyyy");
                    }

                    txtvisitDate.Text = Convert.ToDateTime(ds.Tables[0].Rows[0]["VisitDate"]).ToString("MM/dd/yyyy");
                    txtvisitDate.Enabled = false;
                    ddltypeofvisit.Text = ds.Tables[0].Rows[0]["VisitType"].ToString();
                    txtCardCode.Text = ds.Tables[0].Rows[0]["CardCode"].ToString();

                    if (nwpart == "True")
                    {
                        ddlnewpartner.SelectedItem.Value = "YES";
                        ddlnewpartner.SelectedItem.Text = "YES";
                        ddlnewpartner.Enabled = false;
                    }
                    else
                    {
                        ddlnewpartner.SelectedItem.Value = "NO";
                        ddlnewpartner.SelectedItem.Text = "NO";
                        ddlnewpartner.Enabled = false;
                    }

                    txtprsonmeet.Text = ds.Tables[0].Rows[0]["PersonMet"].ToString();
                    txtemailid.Text = ds.Tables[0].Rows[0]["Email"].ToString();
                    txtconnumber.Text = ds.Tables[0].Rows[0]["ContactNo"].ToString();
                    txtdesig.Text = ds.Tables[0].Rows[0]["Designation"].ToString();
                    ddlpriority.Text = ds.Tables[0].Rows[0]["Priority"].ToString();
                    txtactiondone.Text = ds.Tables[0].Rows[0]["Discussion"].ToString();
                    txtexpectedbuss.Text = ds.Tables[0].Rows[0]["ExpectedBusinessAmt"].ToString();
                    ddlststusofcall.Text = ds.Tables[0].Rows[0]["CallStatus"].ToString();
                    txtfeedback.Text = ds.Tables[0].Rows[0]["Feedback"].ToString();
                    txtforwardcallto.Text = ds.Tables[0].Rows[0]["ForwardCallToEmail"].ToString();
                    txtforwardcallrmk.Text = ds.Tables[0].Rows[0]["ForwardRemark"].ToString();

                    try
                    {
                        if (ds.Tables[0].Rows[0]["BU"] != null && !DBNull.Value.Equals(ds.Tables[0].Rows[0]["BU"]))
                        {

                            string BUcode = ds.Tables[0].Rows[0]["BU"].ToString();
                            string[] codeAry = BUcode.Split(',');

                            foreach (string CardCode in codeAry)
                            {
                                foreach (ListItem BUlist in ddlBU.Items)
                                {
                                    if (BUlist.Value == CardCode)
                                    {
                                        BUlist.Selected = true;
                                    }
                                }
                            }
                        }
                    }

                    catch
                    { }
                }

            }
            //todisplay data at Edit Mode
            if (count1 > 0)
            {
                DataSet ds = Db.myGetDS("select  Top 1 CreatedOn,count(cardcode) as visitcount, isnull(SUM( ExpectedBusinessAmt ),0) as ExAmt   from DailySalesReports where  YEAR(CONVERT(date, CreatedOn)) = YEAR(GETDATE()) and  MONTH(CONVERT(date, CreatedOn))= MONTH(GETDATE()) and CardCode='" + txtCardCode.Text + "' and IsActive=1 and CreatedBy='" + LoggedInUserName + "' group by CreatedOn");

             if (ds.Tables[0].Rows.Count > 0)
             {
                 lblcountvisited.Text = ds.Tables[0].Rows[0]["visitcount"].ToString();
                 lblexpbuss.Text = ds.Tables[0].Rows[0]["ExAmt"].ToString();


                 lblcreatedon.Text = Convert.ToDateTime(ds.Tables[0].Rows[0]["CreatedOn"]).ToString("MM/dd/yyyy");
               

                 string per = string.Empty;
                // (Convert.ToDateTime(rdr["CreatedOn"]).ToString("dd/MM/yyyy"));
                
                // DateTime dt1 = DateTime.Parse(ds.Tables[0].Rows[0]["CreatedOn"].ToString());
               //  DateTime dt1 = DateTime.Parse(Convert.ToDateTime(rdr["CreatedOn"]).ToString());
                 DateTime dt1 = DateTime.Parse(Convert.ToDateTime(ds.Tables[0].Rows[0]["CreatedOn"]).ToString("MM/dd/yyyy"));
                 DateTime dt2 = DateTime.Now;
                 TimeSpan ts = dt2.Subtract(dt1);
                 int days = ts.Days;
                
                 per = string.Format("{0}", days);

                 lbldiff.Text = per;
                
             }
            
            }

        }
    }
    protected void fillgv()
    {
        Db.constr = myGlobal.getAppSettingsDataForKey("tejSAP");
        string LoggedInUserName = myGlobal.loggedInUser();
        DataSet ds = Db.myGetDS("select * , CASE WHEN IsDraft='1' THEN 'Y' ELSE 'N' END ENABLE_STATUS,CASE WHEN IsDraft='1' THEN 'YES' ELSE 'NO' END STATUS  from DailySalesReports  where IsActive=1  and CreatedBy='" + LoggedInUserName + "' order by VisitDate desc");
        // DataSet ds = Db.myGetDS("select * ,  CASE WHEN VisitDate >= CONCAT(FORMAT(GetDate(),'yyyy-MM') ,'-', '04') THEN 'Y' ELSE 'N' END ENABLE_STATUS from DailySalesReports  where IsActive=1 and CreatedBy='" + LoggedInUserName + "' order by VisitDate desc");
        if (ds.Tables.Count > 0)
        {
            Gvdata.DataSource = ds.Tables[0];
            Gvdata.DataBind();
        }
        else
        {
            lblMsg.Text = "No Any Record Found";
        }
    }

    private void BindDDL()
    {
        try
        {

            Db.constr = myGlobal.getAppSettingsDataForKey("tejSAP");
            // DataSet DS = Db.myGetDS("select SC.country from tejSalespersonMap S Join Sales_Employee_country  SC On S.salesperson= Sc.SalesEmpID Where  Membershipuser='" + myGlobal.loggedInUser() + "'");

            DataSet DS = Db.myGetDS("select c.country,c.countrycode from tejSalespersonMap S Join Sales_Employee_country  SC On S.salesperson= Sc.SalesEmpID And  Membershipuser='" + myGlobal.loggedInUser() + "'  JOIN  rddcountrieslist C ON SC.country=C.countrycode");
            ddlCountry.DataSource = DS;// Table [2] for Countries
            ddlCountry.DataTextField = "country";
            ddlCountry.DataValueField = "countrycode";
            ddlCountry.DataBind();
            ddlCountry.Items.Insert(0, "--SELECT--");

            DS = Db.myGetDS("select * from Visittype_Master where IsActive=1 order by DisplaySeq Desc");
            ddltypeofvisit.DataSource = DS;
            ddltypeofvisit.DataTextField = "VisitType";
            ddltypeofvisit.DataValueField = "VisitType";
            ddltypeofvisit.DataBind();
            ddltypeofvisit.Items.Insert(0, "--SELECT--");

            Db.constr = myGlobal.getAppSettingsDataForKey("SApAE");
            DataSet DsForms = Db.myGetDS("select ItmsGrpNam As BU from OITB Where ItmsGrpNam Not in ('C001','D Link','GADGITECH','KINGSTON','REMIX','SAMSUNG DISPLAY','XXXXXXXXXXXXX','TOSHIBA')");

            if (DsForms.Tables.Count > 0)
            {
                ddlBU.DataSource = DsForms.Tables[0];
                ddlBU.DataTextField = "BU";
                ddlBU.DataValueField = "BU";
                ddlBU.DataBind();

            }
        }
        catch (Exception ex)
        {
            lblMsg.Text = "Error BindDDL : " + ex.Message;
            lblMsg.ForeColor = Color.Red;
        }

    }

    protected void btnSave_Click(object sender, EventArgs e)
    {
        try
        {

            if (btnSave.Text == "Save")
            {

                

                if (txtforwardcallto.Text != "" && txtforwardcallrmk.Text == "")
                {
                    lblMsg.Text = "Please Enter Forward Call Remark";
                    return;
                }

                /**********************************31-10-19*******************************/

                DateTime today = DateTime.Today;

                DateTime VisitDay = Convert.ToDateTime(txtvisitDate.Text);

                if (VisitDay < today)
                {
                    lblMsg.Text = "You are submitting late report";
                    return;
                }


                /**********************************END*******************************/
                string ResellerCode = "", ResellerName = "";

                if (ddlBU.SelectedIndex >= 0)
                {
                    foreach (ListItem BU in ddlBU.Items)
                    {
                        if (BU.Selected)
                        {
                            if (string.IsNullOrEmpty(ResellerName))
                            {
                                ResellerCode = BU.Value;
                                ResellerName = BU.Text;
                            }
                            else
                            {
                                ResellerCode = ResellerCode + ";" + BU.Value + "";
                                ResellerName = ResellerName + ";" + BU.Text + "";
                            }
                        }
                    }
                }
                if (string.IsNullOrEmpty(ResellerCode) || string.IsNullOrEmpty(ResellerName))
                {
                    lblMsg.Text = "Please select at least one BU";
                    return;
                }

                /*TO UPDATE THE DRAFT RECORD INTO SAVE MODE*/

                Db.constr = myGlobal.getAppSettingsDataForKey("tejSAP");

                string LoggedInUserName = myGlobal.loggedInUser();

                GridViewRow row = Gvdata.SelectedRow;
                int count = Convert.ToInt32(Request.QueryString["GVid"]);
                int id = Convert.ToInt32(Request.QueryString["GVid"]);

                if (count > 0)
                {

                    int m = 0;
                    string items = string.Empty;
                    foreach (ListItem i in ddlBU.Items)
                    {
                        if (i.Selected == true)
                        {
                            items += i.Text + ",";
                        }
                    }
                    if (ddlnewpartner.Text == "NO")
                    {
                        m = 0;
                    }
                    else
                    {
                        m = 1;
                    }

                    string sqlquery = "update DailySalesReports set Country='" + ddlCountry.SelectedItem.Text + "',Company='" + txtCustomerName.Text + "',CardCode='" + txtCardCode.Text + "',VisitDate='" + txtvisitDate.Text + "',NextReminderDate=case when '" + txtreminderdate.Text.Trim() + "'='' then null else '" + txtreminderdate.Text.Trim() + "' end,VisitType='" + ddltypeofvisit.SelectedItem.Text + "',IsNewPartner=" + m + ",PersonMet='" + txtprsonmeet.Text + "',Email='" + txtemailid.Text + "',Designation='" + txtdesig.Text + "',ContactNo='" + txtconnumber.Text + "',Priority='" + ddlpriority.SelectedItem.Text + "',Discussion='" + txtactiondone.Text + "',ExpectedBusinessAmt='" + txtexpectedbuss.Text + "',CallStatus='" + ddlststusofcall.SelectedItem.Text + "',Feedback='" + txtfeedback.Text + "',BU='" + items + "',ForwardCallToEmail='" + txtforwardcallto.Text + "',ForwardRemark='" + txtforwardcallrmk.Text + "',IsDraft=0 where VisitId='" + id + "'";

                    Db.constr = myGlobal.getAppSettingsDataForKey("tejSAP");

                    Db.myExecuteSQL(sqlquery);

                    fillgv();

                    Response.Redirect("DailySalesMaster.aspx?Action=SAVE");
                }
                //SAVE CODE(FRESH ENTRY)
                else
                {
                    if (txtforwardcallto.Text != "" && txtforwardcallrmk.Text == "")
                    {
                        lblMsg.Text = "Please Enter Forward Call Remark";
                        return;
                    }
                    //DateTime RDate = Convert.ToDateTime(txtreminderdate.Text);

                    //DateTime VDate = Convert.ToDateTime(txtvisitDate.Text);
                    //if (RDate != null && !DBNull.Value.Equals(RDate))
                    //{

                    //    Response.Write("<script> alert( 'Reminder Date Should Not Less than Visit Date.');</script>");
                    //}

                    ResellerCode = ""; ResellerName = "";

                    if (ddlBU.SelectedIndex >= 0)
                    {
                        foreach (ListItem BU in ddlBU.Items)
                        {
                            if (BU.Selected)
                            {
                                if (string.IsNullOrEmpty(ResellerName))
                                {
                                    ResellerCode = BU.Value;
                                    ResellerName = BU.Text;
                                }
                                else
                                {
                                    ResellerCode = ResellerCode + ";" + BU.Value + "";
                                    ResellerName = ResellerName + ";" + BU.Text + "";
                                }
                            }
                        }
                    }
                    if (string.IsNullOrEmpty(ResellerCode) || string.IsNullOrEmpty(ResellerName))
                    {
                        lblMsg.Text = "Please select at least one BU";
                        return;
                    }


  /**********************************31-10-19*******************************/

                     today = DateTime.Today; 
                  
                     VisitDay = Convert.ToDateTime(txtvisitDate.Text);

                    if (VisitDay < today)
                    {
                        lblMsg.Text = "You are submitting late report";
                        return;
                    }


 /**********************************END*******************************/
                    string items = string.Empty;
                    foreach (ListItem i in ddlBU.Items)
                    {
                        if (i.Selected == true)
                        {
                            items += i.Text + ",";
                        }
                    }
                    string sqlquery = "insert into  dbo.DailySalesReports(VisitDate,VisitType,IsNewPartner,Country,Company,CardCode,PersonMet,Email,Designation,ContactNo,BU,Discussion,ExpectedBusinessAmt,CallStatus,Feedback,ForwardCallToEmail,ForwardRemark,ForwardCallCCEmail,Priority,IsDraft,NextReminderDate,CreatedBy,CreatedOn,LastUpdatedBy,LastUpdatedOn,IsActive)values('" + txtvisitDate.Text + "','" + ddltypeofvisit.SelectedItem.Text + "'," + ddlnewpartner.SelectedValue + ",'" + ddlCountry.SelectedItem.Text + "','" + txtCustomerName.Text + "','" + txtCardCode.Text + "','" + txtprsonmeet.Text + "','" + txtemailid.Text + "','" + txtdesig.Text + "','" + txtconnumber.Text + "','" + items + "','" + txtactiondone.Text + "','" + txtexpectedbuss.Text + "','" + ddlststusofcall.SelectedItem.Text + "','" + txtfeedback.Text + "','" + txtforwardcallto.Text + "','" + txtforwardcallrmk.Text + "',0,'" + ddlpriority.SelectedItem.Text + "',0,case when '" + txtreminderdate.Text.Trim() + "'='' then null else '" + txtreminderdate.Text.Trim() + "' end,'" + myGlobal.loggedInUser() + "',getdate(),'',NULL,1)";

                    Db.myExecuteSQL(sqlquery);
                    ClearControl();

                    lblMsg.Text = "Sales Daily Report saved successfully.";
                    fillgv();
                    Response.Redirect("DailySalesMaster.aspx?Action=SAVE");
                }
            }

      /*UPDATE CODE FOR Save*/

            else
            {
                if (txtforwardcallto.Text != "" && txtforwardcallrmk.Text == "")
                {
                    lblMsg.Text = "Please Enter Forward Call Remark";
                    return;
                }
                //if (visitdate > reminderdate)
                //{
                //    lblMsg.Text = "Reminder Date Should be greater than Visit Date";
                //    return;
                //}
                string ResellerCode = ""; string ResellerName = "";

                if (ddlBU.SelectedIndex >= 0)
                {
                    foreach (ListItem BU in ddlBU.Items)
                    {
                        if (BU.Selected)
                        {
                            if (string.IsNullOrEmpty(ResellerName))
                            {
                                ResellerCode = BU.Value;
                                ResellerName = BU.Text;
                            }
                            else
                            {
                                ResellerCode = ResellerCode + ";" + BU.Value + "";
                                ResellerName = ResellerName + ";" + BU.Text + "";
                            }
                        }
                    }
                }
                if (string.IsNullOrEmpty(ResellerCode) || string.IsNullOrEmpty(ResellerName))
                {
                    lblMsg.Text = "Please select at least one BU";
                    return;
                }
                Db.constr = myGlobal.getAppSettingsDataForKey("tejSAP");
                string ID = Request.QueryString["GVid"].ToString();

                int m = 0;
                string items = string.Empty;
                foreach (ListItem i in ddlBU.Items)
                {
                    if (i.Selected == true)
                    {
                        items += i.Text + ",";
                    }
                }
                if (ddlnewpartner.Text == "NO")
                {
                    m = 0;
                }
                else
                {
                    m = 1;
                }

                string sqlquery = "update DailySalesReports set Country='" + ddlCountry.SelectedItem.Text + "',Company='" + txtCustomerName.Text + "',CardCode='" + txtCardCode.Text + "',VisitDate='" + txtvisitDate.Text + "',NextReminderDate=case when '" + txtreminderdate.Text.Trim() + "'='' then null else '" + txtreminderdate.Text.Trim() + "' end,VisitType='" + ddltypeofvisit.SelectedItem.Text + "',IsNewPartner=" + m + ",PersonMet='" + txtprsonmeet.Text + "',Email='" + txtemailid.Text + "',Designation='" + txtdesig.Text + "',ContactNo='" + txtconnumber.Text + "',Priority='" + ddlpriority.SelectedItem.Text + "',Discussion='" + txtactiondone.Text + "',ExpectedBusinessAmt='" + txtexpectedbuss.Text + "',CallStatus='" + ddlststusofcall.SelectedItem.Text + "',Feedback='" + txtfeedback.Text + "',BU='" + items + "',ForwardCallToEmail='" + txtforwardcallto.Text + "',ForwardRemark='" + txtforwardcallrmk.Text + "',LastUpdatedBy='" + myGlobal.loggedInUser() + "',LastUpdatedOn=getdate(),IsDraft=0 where VisitId='" + ID + "'";

                Db.constr = myGlobal.getAppSettingsDataForKey("tejSAP");

                Db.myExecuteSQL(sqlquery);

                fillgv();
                Response.Redirect("DailySalesMaster.aspx?Action=UPDATED");

            }
        }
        catch (Exception ex)
        {

        }
    }

    protected void ClearControl()
    {
        btndel.Visible = false;
        txtactiondone.Text = "";
        // txtcomname.Text = "";
        txtconnumber.Text = "";
        txtdesig.Text = "";
        txtemailid.Text = "";
        txtexpectedbuss.Text = "";
        txtfeedback.Text = "";
        txtforwardcallrmk.Text = "";
        txtforwardcallto.Text = "";

        txtprsonmeet.Text = "";
        txtreminderdate.Text = "";

        // btnsavereseller.Visible = false;
        txtnewpartnername.Text = "";
        txtCardCode.Text = "";
        txtnewpartnername.Visible = false;
        lblMsg.Text = "";

        try
        {
            ddlnewpartner.SelectedIndex = -1;
            ddlnewpartner.SelectedItem.Text = "NO";
        }
        catch (Exception ex)
        { }

        try
        {
            ddlCountry.SelectedIndex = -1;
            ddlCountry.SelectedItem.Text = "--SELECT--";
        }
        catch (Exception ex)
        { }
        try
        {
            ddlpriority.SelectedIndex = -1;
            ddlpriority.SelectedItem.Text = "--SELECT--";
        }
        catch (Exception ex)
        { }
        try
        {
            ddlststusofcall.SelectedIndex = -1;
            ddlststusofcall.SelectedItem.Text = "--SELECT--";
        }
        catch (Exception ex) { }
        try
        {
            ddltypeofvisit.SelectedIndex = -1;
            ddltypeofvisit.SelectedItem.Text = "--SELECT--";
        }
        catch (Exception ex)
        { }

        txtCustomerName.Text = "";

        txtvisitDate.Text = DateTime.Now.ToString("MM/dd/yyyy");
    }

    protected void lnkaddnewpartner_Click(object sender, EventArgs e)
    {
        txtnewpartnername.Visible = true;
    }

    protected void Gvdata_SelectedIndexChanged(object sender, EventArgs e)
    {
        btnSave.Text = "Update";
        lblMsg.Text = "";
        GridViewRow row = Gvdata.SelectedRow;

        Label lbldraft = (Label)row.FindControl("lbldraft");
        string draft = lbldraft.Text;
        Session["a"] = draft;
        Label lblnewpartner = (Label)row.FindControl("lblnewpartner");
        string partner = lblnewpartner.Text;
        Session["b"] = partner;

        Label lblsaleid = (Label)row.FindControl("lblsaleid");

        string sid = lblsaleid.Text;
        Response.Redirect("DailySalesMaster.aspx?GVid=" + sid);
    }

    //protected void Gvdata_PageIndexChanging(object sender, GridViewPageEventArgs e)
    //{
    //    Gvdata.PageIndex = e.NewPageIndex;
    //    fillgv();
    //}

    protected void btncancel_Click(object sender, EventArgs e)
    {
        Response.Redirect("DailySalesMaster.aspx", true);
        ClearControl();
        lblMsg.Text = "";
    }
    protected void btnsaveasdraft_Click1(object sender, EventArgs e)
    {
        Db.constr = myGlobal.getAppSettingsDataForKey("tejSAP");
        int m = 0;
        string sqlquery = "";
        int id = Convert.ToInt32(Request.QueryString["GVid"]);
        if (id > 0) /* DRAFT RECORD UPDATE MODE*/
        {


            string items = string.Empty;
            foreach (ListItem i in ddlBU.Items)
            {
                if (i.Selected == true)
                {
                    items += i.Text + ",";
                }
            }
            if (ddlnewpartner.Text == "NO")
            {
                m = 0;
            }
            else
            {
                m = 1;
            }

            sqlquery = "update DailySalesReports set Country='" + ddlCountry.SelectedItem.Text + "',Company='" + txtCustomerName.Text + "',CardCode='" + txtCardCode.Text + "',VisitDate='" + txtvisitDate.Text + "',NextReminderDate=case when '" + txtreminderdate.Text.Trim() + "'='' then null else '" + txtreminderdate.Text.Trim() + "' end,VisitType='" + ddltypeofvisit.SelectedItem.Text + "',IsNewPartner=" + m + ",PersonMet='" + txtprsonmeet.Text + "',Email='" + txtemailid.Text + "',Designation='" + txtdesig.Text + "',ContactNo='" + txtconnumber.Text + "',Priority='" + ddlpriority.SelectedItem.Text + "',Discussion='" + txtactiondone.Text + "',ExpectedBusinessAmt='" + txtexpectedbuss.Text + "',CallStatus='" + ddlststusofcall.SelectedItem.Text + "',Feedback='" + txtfeedback.Text + "',BU='" + items + "',ForwardCallToEmail='" + txtforwardcallto.Text + "',ForwardRemark='" + txtforwardcallrmk.Text + "',LastUpdatedBy='" + myGlobal.loggedInUser() + "',LastUpdatedOn=getdate(), DocDraftDate=getdate(),IsDraft=1 where VisitId='" + id + "'";
            Db.myExecuteSQL(sqlquery);

            fillgv();

            Response.Redirect("DailySalesMaster.aspx?Action=DRAFT");
            //lblMsg.Text = "Record Saved as Draft.";
        }
        else /* DRAFT NEW RECORD*/
        {


            string item = string.Empty;
            foreach (ListItem i in ddlBU.Items)
            {
                if (i.Selected == true)
                {
                    item += i.Text + ",";
                }

            }
            if (ddlnewpartner.Text == "NO")
            {
                m = 0;
            }
            else
            {
                m = 1;
            }
            sqlquery = "insert into  dbo.DailySalesReports(VisitDate,VisitType,IsNewPartner,Country,Company,CardCode,PersonMet,Email,Designation,ContactNo,BU,Discussion,ExpectedBusinessAmt,CallStatus,Feedback,ForwardCallToEmail,ForwardRemark,ForwardCallCCEmail,Priority,IsDraft,NextReminderDate,CreatedBy,CreatedOn,LastUpdatedBy,LastUpdatedOn,IsActive)values('" + txtvisitDate.Text + "','" + ddltypeofvisit.SelectedItem.Text + "'," + ddlnewpartner.SelectedValue + ",'" + ddlCountry.SelectedItem.Text + "','" + txtCustomerName.Text + "','" + txtCardCode.Text + "','" + txtprsonmeet.Text + "','" + txtemailid.Text + "','" + txtdesig.Text + "','" + txtconnumber.Text + "','" + item + "','" + txtactiondone.Text + "','" + txtexpectedbuss.Text + "','" + ddlststusofcall.SelectedItem.Text + "','" + txtfeedback.Text + "','" + txtforwardcallto.Text + "','" + txtforwardcallrmk.Text + "',0,'" + ddlpriority.SelectedItem.Text + "',1,case when '" + txtreminderdate.Text.Trim() + "'='' then null else '" + txtreminderdate.Text.Trim() + "' end,'" + myGlobal.loggedInUser() + "',getdate(),'" + myGlobal.loggedInUser() + "',getdate(),1)";
            Db.myExecuteSQL(sqlquery);
            fillgv();
            Response.Redirect("DailySalesMaster.aspx?Action=DRAFT");

        }

    }
    protected void btngotolist_Click(object sender, EventArgs e)
    {
        Response.Redirect("DailySaleRptList.aspx");
    }
    protected void Gvdata_RowDataBound(object sender, GridViewRowEventArgs e)
    {
        if (e.Row.RowType == DataControlRowType.DataRow)
        {
            string status = Convert.ToString(DataBinder.Eval(e.Row.DataItem, "ENABLE_STATUS"));
            if (status == "Y")
            {
                e.Row.Cells[0].Controls[0].Visible = true;

            }
            else
            {
                e.Row.Cells[0].Controls[0].Visible = false;

            }
            string draft = Convert.ToString(DataBinder.Eval(e.Row.DataItem, "IsDraft"));
            if (draft == "True")
            {
                e.Row.BackColor = System.Drawing.Color.Silver;
            }
            else
            {
                e.Row.BackColor = System.Drawing.Color.Beige;
            }
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
    public static string visitdiff(string dbcode)
    {
        string per = string.Empty;

        Db.constr = myGlobal.getAppSettingsDataForKey("tejSAP");
        string LoggedInUserName = myGlobal.loggedInUser();

        string query = "select Top 1 CreatedOn from DailySalesReports where CardCode='" + dbcode + "' and CreatedBy='" + LoggedInUserName + "' order by CreatedOn desc";
       // string query = "select Top 1 CONVERT(VARCHAR(10), CreatedOn, 101) from DailySalesReports where CardCode='" + dbcode + "' and CreatedBy='" + LoggedInUserName + "' order by CreatedOn desc";
        SqlDataReader rdr = Db.myGetReader(query);

        while (rdr.Read())
        {
          
            DateTime dt1 = DateTime.Parse(Convert.ToDateTime(rdr["CreatedOn"]).ToString("MM/dd/yyyy"));
            DateTime dt2 = DateTime.Now;
            TimeSpan ts = dt2.Subtract(dt1);
            int days = ts.Days;         
            per = string.Format("{0}", days);
           
        }
        return per;
    }

    [WebMethod]
    public static string GetCustomersDinfo(string dbcode)
    {
       
        string person1 = string.Empty;
        string per = string.Empty;

        Db.constr = myGlobal.getAppSettingsDataForKey("tejSAP");
        string LoggedInUserName = myGlobal.loggedInUser();
        string qry = "select  count(cardcode) as visitcount, isnull(SUM( ExpectedBusinessAmt ),0) as ExAmt,  convert(varchar,max(CreatedOn),103) As CreatedOn   from DailySalesReports where  YEAR(CONVERT(date, CreatedOn)) = YEAR(GETDATE()) and  MONTH(CONVERT(date, CreatedOn))= MONTH(GETDATE()) and CardCode='" + dbcode + "' and IsActive=1 and CreatedBy='" + LoggedInUserName + "'";
      // string qry = "select cardcode, count(cardcode) as visitcount  from DailySalesReports where CONVERT(date, CreatedOn) >= '2019-10-01' and  CONVERT(date, CreatedOn) <= '2019-10-31' and CardCode='" + dbcode + "' group by cardcode";
   
        SqlDataReader rdr = Db.myGetReader(qry);
      
        while (rdr.Read())
        {
            //DateTime dt1 = DateTime.Parse(Convert.ToDateTime(rdr["CreatedOn"]).ToString("MM/dd/yyyy"));
            //DateTime dt2 = DateTime.Now;
            //TimeSpan ts = dt2.Subtract(dt1);
            //int days = ts.Days;
            //per = string.Format("{0}", days);
            person1 = string.Format("{0}#{1}#{2}", rdr["visitcount"].ToString(), rdr["ExAmt"].ToString(), rdr["CreatedOn"].ToString());
          
        }
        return person1;
       
    }

    [WebMethod]
    public static string GetCustomerscreated(string dbcode)
    {
        // List<string> person1 = new List<string>();
        string person2 = string.Empty;

        Db.constr = myGlobal.getAppSettingsDataForKey("tejSAP");
        string LoggedInUserName = myGlobal.loggedInUser();
        string qry = "select Top 1 CreatedOn from DailySalesReports where CardCode='" + dbcode + "' and CreatedBy='" + LoggedInUserName + "' order by CreatedOn desc ";
        // string qry = "select cardcode, count(cardcode) as visitcount  from DailySalesReports where CONVERT(date, CreatedOn) >= '2019-10-01' and  CONVERT(date, CreatedOn) <= '2019-10-31' and CardCode='" + dbcode + "' group by cardcode";

        SqlDataReader rdr = Db.myGetReader(qry);

        while (rdr.Read())
        {
            string a = Convert.ToDateTime(rdr["CreatedOn"]).ToString("dd/MM/yyyy");
            person2 = string.Format("{0}", a);

        }
        return person2;

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
    protected void btndel_Click(object sender, EventArgs e)
    {
        Db.constr = myGlobal.getAppSettingsDataForKey("tejSAP");
        string id = Request.QueryString["GVid"].ToString();

        if (!string.IsNullOrEmpty(id))/*Delete Functionality*/
        {
            Db.myExecuteSQL("update  DailySalesReports set IsActive=0  Where VisitId=" + id);
            fillgv();
            Response.Redirect("DailySalesMaster.aspx?Action=DELETED");

        }

    }
    [WebMethod]
    public static string Addreseller(string partnerName, string country)
    {
        string Response = "1";
        try
        {

            Db.constr = myGlobal.getAppSettingsDataForKey("tejSAP");

            int NoOfRecords = Db.myExecuteScalar("select count(*)  From DailySalesNewResellers Where Country='" + country + "' And ResellerName= '" + partnerName + "'");

            if (NoOfRecords > 0)
            {
                Response = "Reseller already exists.";

            }
            else
            {
                string sqlquery = "insert into  dbo.DailySalesNewResellers(ResellerCode,Country,ResellerName,IsCreatedInSAP,createdOn,CreatedBy)values(dbo.GetResellerNo('" + country + "') ,'" + country + "','" + partnerName + "',0,getdate(),'" + myGlobal.loggedInUser() + "')";
                Db.myExecuteSQL(sqlquery);  //'TZ' +'000'+ convert(varchar(30) ,MAX(ResellerCode)+1 )from DailySalesNewResellers
                Response = "1";
            }

        }
        catch (Exception ex)
        {
            Response = ex.Message;
        }
        return Response;
    }






    //protected void lnkdel_Click(object sender, EventArgs e)
    //{

    //     int id = Convert.ToInt32((((Label)(((LinkButton)sender).NamingContainer as GridViewRow).FindControl("lblsaleid")).Text));


    //    string sql = "delete from DailySalesReports where VisitId ='" + id + "'";
      
    //    Db.myExecuteSQL(sql);
      

    //}
    
}