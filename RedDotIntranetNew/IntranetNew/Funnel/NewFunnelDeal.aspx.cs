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
using System.Globalization;

public partial class IntranetNew_Funnel_NewFunnelDeal : System.Web.UI.Page
{
    protected void Page_Load(object sender, EventArgs e)
    {
        try
        {
            if (!IsPostBack)
            {
                Db.constr = myGlobal.getAppSettingsDataForKey("tejSAP");
                int count = Db.myExecuteScalar("Select COUNT(*) from dbo.MenuWiseForms t0 Join dbo.UserAuthorization t1 on t0.MenuId=t1.MenuId and t1.MembershipUserName='" + myGlobal.loggedInUser() + "' And t0.FormURL='NewFunnelDeal.aspx' And t1.IsActive=1");
                if (count > 0)
                {
                    if (Request.QueryString["fid"] != null && Request.QueryString["Access"] != null && Request.QueryString["Action"] != null)
                    {
                        string fid = Request.QueryString["fid"].ToString();
                        string Access = Request.QueryString["Access"].ToString();
                        string Action = Request.QueryString["Action"].ToString(); // is paramer is To show the message if record is updated or deleted 
                        lblChangeCount.Text = "";

                        if (!string.IsNullOrEmpty(fid) && !string.IsNullOrEmpty(Access))
                        {
                            BindDDL(Access);
                            if (Access == "NEW" && fid == "0" && (Action == "Add" || Action == "Updated" || Action == "Deleted")) //New Deal
                            {
                                if (Action == "Updated" || Action == "Deleted") // To Show the message, Afer the record is deleted or Updated.
                                {
                                    Page.ClientScript.RegisterStartupScript(this.GetType(), "alert", "<script> alert('Funnel Deal " + Action + " Successfully.'); </script>");
                                }
                                txtCreatedBy.Text = myGlobal.loggedInUser();
                                txtCreatedDate.Text = DateTime.Now.ToString("MM/dd/yyyy");
                                txtQuoteDate.Text = DateTime.Now.ToString("MM/dd/yyyy");
                                chkNewReseller.Enabled = true;
                                lblformName.InnerText = "Funnel New Deal";
                            }
                            else
                            {
                                lblformName.InnerText = "Update Funnel Deal";

                                BtnDelete.Enabled = true;
                                btnSave.Text = "Update";
                                txtRemark1.Enabled = false;
                                chkNewReseller.Enabled = false;
                                Db.constr = myGlobal.getAppSettingsDataForKey("tejSAP");
                                DataTable DtFunnel = Db.myGetDS(" select fid,bdm,quoteID,endUser,resellerCode,resellerName,country,BU,goodsDescr,quoteDate,convert(numeric(19,2),value) as value,remarks,dealStatus,orderBookedDate,CreatedOn,CreatedBy,LastUpdatedOn,LastUpdatedBy,Cost,Landed,InvoiceDt,NextReminderDt,MarginUSD,Remarks2,Remarks3, case when expClosingDt is null then cast( cast(expClosingMonth as varchar) +'/01/'+ cast(expClosingYear as varchar) as varchar) else expClosingDt end expClosingDt,ChangeCount from tejfunnel2017 Where fid=" + fid).Tables[0];

                                if (DtFunnel.Rows.Count > 0)
                                {
                                    lblFid.Text = fid;
                                    txtBDM.Text = DtFunnel.Rows[0]["bdm"].ToString();
                                    txtEndUser.Text = DtFunnel.Rows[0]["endUser"].ToString();
                                    txtGoodsDesc.Text = DtFunnel.Rows[0]["goodsDescr"].ToString();
                                    txtQuoteID.Text = DtFunnel.Rows[0]["quoteID"].ToString();
                                    txtRemark1.Text = DtFunnel.Rows[0]["remarks"].ToString();

                                    txtTotalSell.Text = DtFunnel.Rows[0]["value"].ToString();

                                    if (DtFunnel.Rows[0]["Cost"] != null && !DBNull.Value.Equals(DtFunnel.Rows[0]["Cost"]))
                                    {
                                        txtCost.Text = DtFunnel.Rows[0]["Cost"].ToString();
                                    }
                                    else
                                    {
                                        txtCost.Text = "0";
                                    }

                                    if (DtFunnel.Rows[0]["Landed"] != null && !DBNull.Value.Equals(DtFunnel.Rows[0]["Landed"]))
                                    {
                                        txtLanded.Text = DtFunnel.Rows[0]["Landed"].ToString();
                                    }
                                    else
                                    {
                                        txtLanded.Text = "0";
                                    }

                                    try  // to calculate and set margin $ and margin %
                                    {
                                        decimal TotalSell = 0, Landed = 0, MarginDoller = 0, MardinPercent = 0;
                                        if (!string.IsNullOrEmpty(txtTotalSell.Text))
                                        {
                                            TotalSell = Convert.ToDecimal(txtTotalSell.Text);
                                        }

                                        if (!string.IsNullOrEmpty(txtLanded.Text))
                                        {
                                            Landed = Convert.ToDecimal(txtLanded.Text);
                                        }

                                        if (Landed > 0)
                                        {
                                            MarginDoller = TotalSell - Landed;
                                            txtMargin.Text = MarginDoller.ToString();

                                            MardinPercent = (MarginDoller / Landed) * 100;
                                            txtMarginPercent.Text = MardinPercent.ToString("#.##");
                                        }
                                        else
                                        {
                                            txtMargin.Text = "0";
                                            txtMarginPercent.Text = "0";
                                        }

                                    }
                                    catch { }

                                    if (DtFunnel.Rows[0]["Remarks2"] != null && !DBNull.Value.Equals(DtFunnel.Rows[0]["Remarks2"]))
                                    {
                                        txtRemark2.Text = DtFunnel.Rows[0]["Remarks2"].ToString();
                                    }
                                    if (DtFunnel.Rows[0]["Remarks3"] != null && !DBNull.Value.Equals(DtFunnel.Rows[0]["Remarks3"]))
                                    {
                                        txtRemark3.Text = DtFunnel.Rows[0]["Remarks3"].ToString();
                                    }

                                    try // To Enabel - Disable the Remarks fields.
                                    {
                                        if (DtFunnel.Rows[0]["ChangeCount"] != null && !DBNull.Value.Equals(DtFunnel.Rows[0]["ChangeCount"]))
                                        {
                                            int ChangeCount = Convert.ToInt32(DtFunnel.Rows[0]["ChangeCount"]);
                                            lblChangeCount.Text = ChangeCount.ToString();
                                            if (ChangeCount == 1)
                                            {
                                                txtRemark1.Enabled = false;
                                                txtRemark2.Enabled = true;
                                                txtRemark3.Enabled = false;
                                            }
                                            else if (ChangeCount >= 2)
                                            {
                                                txtRemark1.Enabled = false;
                                                txtRemark2.Enabled = false;
                                                txtRemark3.Enabled = true;
                                            }
                                        }
                                    }
                                    catch { }

                                    try // to Set all dates fields
                                    {
                                        DateTime quotedate = Convert.ToDateTime(DtFunnel.Rows[0]["quoteDate"]);
                                        DateTime expClosingDt = Convert.ToDateTime(DtFunnel.Rows[0]["expClosingDt"]);
                                        txtQuoteDate.Text = quotedate.ToString("MM/dd/yyyy");
                                        txtClodingDate.Text = expClosingDt.ToString("MM/dd/yyyy");
                                        if (DtFunnel.Rows[0]["NextReminderDt"] != null && !DBNull.Value.Equals(DtFunnel.Rows[0]["NextReminderDt"]))
                                        {
                                            DateTime NextReminderDt = Convert.ToDateTime(DtFunnel.Rows[0]["NextReminderDt"]);
                                            txtReminderOn.Text = NextReminderDt.ToString("MM/dd/yyyy");
                                        }
                                        if (DtFunnel.Rows[0]["orderBookedDate"] != null && !DBNull.Value.Equals(DtFunnel.Rows[0]["orderBookedDate"]))
                                        {
                                            DateTime orderBookedDate = Convert.ToDateTime(DtFunnel.Rows[0]["orderBookedDate"]);
                                            txtOrderDate.Text = orderBookedDate.ToString("MM/dd/yyyy");
                                        }
                                        if (DtFunnel.Rows[0]["InvoiceDt"] != null && !DBNull.Value.Equals(DtFunnel.Rows[0]["InvoiceDt"]))
                                        {
                                            DateTime InvoiceDt = Convert.ToDateTime(DtFunnel.Rows[0]["InvoiceDt"]);
                                            txtInvoiceDate.Text = InvoiceDt.ToString("MM/dd/yyyy");
                                        }

                                        if (DtFunnel.Rows[0]["CreatedBy"] != null && !DBNull.Value.Equals(DtFunnel.Rows[0]["CreatedBy"]))
                                        {
                                            txtCreatedBy.Text = DtFunnel.Rows[0]["CreatedBy"].ToString();
                                        }
                                        if (DtFunnel.Rows[0]["CreatedOn"] != null && !DBNull.Value.Equals(DtFunnel.Rows[0]["CreatedOn"]))
                                        {
                                            txtCreatedDate.Text = Convert.ToDateTime(DtFunnel.Rows[0]["CreatedOn"]).ToString("MM/dd/yyyy");
                                        }
                                        if (DtFunnel.Rows[0]["LastUpdatedBy"] != null && !DBNull.Value.Equals(DtFunnel.Rows[0]["LastUpdatedBy"]))
                                        {
                                            txtUpdatedBy.Text = DtFunnel.Rows[0]["LastUpdatedBy"].ToString();
                                        }
                                        if (DtFunnel.Rows[0]["LastUpdatedOn"] != null && !DBNull.Value.Equals(DtFunnel.Rows[0]["LastUpdatedOn"]))
                                        {
                                            txtUpdatedDt.Text = Convert.ToDateTime(DtFunnel.Rows[0]["LastUpdatedOn"]).ToString("MM/dd/yyyy");
                                        }

                                    }
                                    catch { }

                                    try // to set country, BU and DealStatus
                                    {
                                        ddlStatus.SelectedItem.Text = DtFunnel.Rows[0]["dealStatus"].ToString();
                                        ddlStatus.SelectedItem.Value = DtFunnel.Rows[0]["dealStatus"].ToString();

                                        ddlCountry.SelectedItem.Text = DtFunnel.Rows[0]["country"].ToString();
                                        ddlCountry.SelectedItem.Value = DtFunnel.Rows[0]["country"].ToString();

                                        ddlBU.SelectedItem.Text = DtFunnel.Rows[0]["BU"].ToString();
                                        ddlBU.SelectedItem.Value = DtFunnel.Rows[0]["BU"].ToString();

                                        ddlCountry_SelectedIndexChanged(null, null);
                                    }
                                    catch { }

                                    // to set customers
                                    try
                                    {
                                        if (DtFunnel.Rows[0]["resellerCode"] != null && !DBNull.Value.Equals(DtFunnel.Rows[0]["resellerCode"]))
                                        {
                                            string resellercode = DtFunnel.Rows[0]["resellerCode"].ToString();
                                            string[] resellercodeAry = resellercode.Split(';');

                                            foreach (string cardcode in resellercodeAry)
                                            {
                                                foreach (ListItem customer in ddlcustomer.Items)
                                                {
                                                    if (customer.Value == cardcode.ToLower())
                                                    {
                                                        customer.Selected = true;
                                                    }
                                                }
                                            }
                                        }
                                    }
                                    catch { }

                                    #region "Enable Disable controls"

                                    if (ddlStatus.SelectedItem.Text == "OPEN")
                                    {
                                        ddlCountry.Enabled = false;
                                        ddlBU.Enabled = false;
                                    }
                                    else if (ddlStatus.SelectedItem.Text == "WON-R OPG" || ddlStatus.SelectedItem.Text == "WON OPG")
                                    {
                                        ddlCountry.Enabled = false;
                                        ddlBU.Enabled = false;
                                        txtBDM.Enabled = false;
                                        txtEndUser.Enabled = false;
                                        ddlcustomer.Enabled = false;
                                        txtGoodsDesc.Enabled = false;
                                        txtRemark1.Enabled = false;
                                        txtRemark2.Enabled = false;
                                        txtRemark3.Enabled = true;
                                        txtReminderOn.Enabled = false;
                                        txtQuoteDate.Enabled = false;
                                        txtCost.Enabled = false;
                                        txtLanded.Enabled = false;
                                        txtMargin.Enabled = false;
                                        txtMarginPercent.Enabled = false;
                                        txtTotalSell.Enabled = false;
                                        ddlStatus.Enabled = false;
                                        txtClodingDate.Enabled = false;
                                        BtnDelete.Enabled = false;
                                    }
                                    else
                                    {
                                        ddlCountry.Enabled = false;
                                        ddlBU.Enabled = false;
                                        txtBDM.Enabled = false;
                                        txtEndUser.Enabled = false;
                                        ddlcustomer.Enabled = false;
                                        txtGoodsDesc.Enabled = false;
                                        txtRemark1.Enabled = false;
                                        txtRemark2.Enabled = false;
                                        txtRemark3.Enabled = true;
                                        txtReminderOn.Enabled = false;
                                        txtQuoteDate.Enabled = false;
                                        txtCost.Enabled = false;
                                        txtLanded.Enabled = false;
                                        txtMargin.Enabled = false;
                                        txtMarginPercent.Enabled = false;
                                        txtTotalSell.Enabled = false;
                                        ddlStatus.Enabled = false;
                                        txtClodingDate.Enabled = false;
                                        txtOrderDate.Enabled = false;
                                        txtInvoiceDate.Enabled = false;
                                        BtnDelete.Enabled = false;
                                    }

                                    #endregion

                                }
                                if (Access == "READ ONLY")
                                {
                                    DisableForReadOnlyUser();
                                }
                            }
                        }
                        else
                        {
                            // Lbl Mesge == Invalid request to open/create funnel deal. Please try again
                        }
                    }
                }
                else
                {
                    Response.Redirect("Default.aspx?UserAccess=0&FormName=Funnel New Deal");
                }
            }
        }
        catch (Exception ex)
        {
            throw;
        }
    }

    private void DisableForReadOnlyUser()
    {
        try
        {
            ddlCountry.Enabled = false;
            ddlBU.Enabled = false;
            ddlcustomer.Enabled = false;
            txtBDM.Enabled = false;
            txtEndUser.Enabled = false;
            txtGoodsDesc.Enabled = false;
            txtRemark1.Enabled = false;
            txtRemark2.Enabled = false;
            txtRemark3.Enabled = false;
            txtReminderOn.Enabled = false;
            txtQuoteDate.Enabled = false;
            txtCost.Enabled = false;
            txtLanded.Enabled = false;
            txtMargin.Enabled = false;
            txtMarginPercent.Enabled = false;
            txtTotalSell.Enabled = false;
            ddlStatus.Enabled = false;
            txtClodingDate.Enabled = false;
            txtOrderDate.Enabled = false;
            txtInvoiceDate.Enabled = false;
            btnSave.Enabled = false;
            BtnDelete.Enabled = false;
        }
        catch { }
    }

    private void BindDDL(string Access)
    {
        try
        {
            lblMsg.Text = "";
            Db.constr = myGlobal.getAppSettingsDataForKey("tejSAP");
            DataSet DS = Db.myGetDS("select DealStatus from tejfunneldealstatus ; select '--SELECT--' as BU union all select distinct f.BU from tejsalespersonmap s Join funnelsetup f on s.salesperson=f.salesperson And Membershipuser='" + myGlobal.loggedInUser() + "' And f.Access in ('FULL ACCESS', case when '" + Access + "' ='NEW' then '' else 'READ ONLY' end ) ");
            ddlStatus.DataSource = DS.Tables[0];  // Table [0] for Deal Status
            ddlStatus.DataTextField = "DealStatus";
            ddlStatus.DataValueField = "DealStatus";
            ddlStatus.DataBind();

            ddlBU.DataSource = DS.Tables[1];    // Table [1] for BU's
            ddlBU.DataTextField = "BU";
            ddlBU.DataValueField = "BU";
            ddlBU.DataBind();

            DataTable DTCountry = Db.myGetDS(" select '--SELECT--' as Country union all select distinct C.Country from tejsalespersonmap s Join funnelsetup f on s.salesperson=f.salesperson And Membershipuser='" + myGlobal.loggedInUser() + "' And f.Access in ('FULL ACCESS','READ ONLY') JOIN rddcountrieslist C ON f.country=C.countrycode ").Tables[0];
            ddlCountry.DataSource = DTCountry;// Table [2] for Countries
            ddlCountry.DataTextField = "country";
            ddlCountry.DataValueField = "country";
            ddlCountry.DataBind();

        }
        catch (Exception ex)
        {
            lblMsg.Text = "Error BindDDL : " + ex.Message;
            lblMsg.ForeColor = Color.Red;
        }
    }
    
    protected void BtnGoBack_Click(object sender, EventArgs e)
    {
        try
        {
            Response.Redirect("FunnelList.aspx");
        }
        catch (Exception ex)
        {
            lblMsg.Text = "Error occured in Redirect";
        }

    }

    protected void ddlCountry_SelectedIndexChanged(object sender, EventArgs e)
    {
        try
        {
            lblMsg.Text = "";

            if (ddlCountry.SelectedItem.Text != "--SELECT--")
            {
                string country = ddlCountry.SelectedItem.Text;
                //string DBName = "";
                //if (country == "UGANDA")
                //    DBName = "UG";
                //else if (country == "KENYA")
                //    DBName = "KE";
                //else if (country == "TANZANIA")
                //    DBName = "TZ";
                //else
                //    DBName = "TRI";
                // DataTable DTCust = Db.myGetDS(" Select CardCode,lower(CardName) as CardName from [" + DBName + "].[dbo].[OCRD] Where CardType='C' ").Tables[0];

                string prvcountry = "";
                if (Session["FResellerCountry"] != null)
                {
                    prvcountry = (string)Session["FResellerCountry"];
                }
                else
                {
                    Session["FResellerCountry"] = country;
                }

                DataTable DTCust;
                if (Session["FResellers"] == null || prvcountry != country)
                {
                    DTCust = Db.myGetDS(" exec [getFunnelCustomerListNF] " + country).Tables[0];
                    Session["FResellers"] = DTCust;
                    Session["FResellerCountry"] = country;
                }
                else
                {
                    DTCust = (DataTable)Session["FResellers"];
                }

                ddlcustomer.DataSource = DTCust;// Table [2] for Countries
                ddlcustomer.DataTextField = "CardName";
                ddlcustomer.DataValueField = "CardCode";
                ddlcustomer.DataBind();

                //Generate New QuoteID Only for New Deals
                if (btnSave.Text == "Save")
                {
                    try
                    {
                        Db.constr = myGlobal.getAppSettingsDataForKey("tejSAP");
                        txtQuoteID.Text = Db.myExecuteScalar2("Select dbo.GetQuoteID('" + ddlCountry.SelectedItem.Text + "')");
                        chkNewReseller.Visible = true;
                    }
                    catch { }
                }

            }
            else
            {
                ddlcustomer.Items.Clear();
            }
        }
        catch (Exception ex)
        {
            lblMsg.Text = "Error occured in ddlCountry_SelectedIndexChanged : "+ex.Message;
            lblMsg.ForeColor = Color.Red;
        }

    }

    protected void BtnSaveReseller_Click(object sender, EventArgs e)
    {
        try
        {
            lblMsg.Text = "";
            if (ddlCountry.SelectedIndex != -1)
            {
               string country = ddlCountry.SelectedItem.Text;

               Db.constr=myGlobal.getAppSettingsDataForKey("tejSAP");
               Db.myExecuteSQL(" insert into tblFunnelNewResellers(CardCode,CardName,CardType,dbCode,dbName,CreatedBy) values((select 'NewRCode' + convert(varchar(30),(isnull(MAX(cid),0)+1)) maxId from [tejSap].[dbo].[tblFunnelNewResellers]),'" + txtNewReseller.Text + "','C',(select dbCode from [tejSap].[dbo].[rddCountriesList] where Country='" + country + "'),(select dbName from [tejSap].[dbo].[rddCountriesList] where Country='" + country + "'),'"+myGlobal.loggedInUser()+"')");
               txtNewReseller.Text = "";
               lblMsg.Text = "New Reseller is added successfully";
               lblMsg.ForeColor = Color.Green;

               DataTable   DTCust = Db.myGetDS(" exec [getFunnelCustomerListNF] " + country).Tables[0];
               Session["FResellers"] = DTCust;
               ddlcustomer.DataSource = DTCust;// Table [2] for Countries
               ddlcustomer.DataTextField = "CardName";
               ddlcustomer.DataValueField = "CardCode";
               ddlcustomer.DataBind();
            }
        }
        catch (Exception ex)
        {
            lblMsg.Text = "Error occured in BtnSaveReseller_Click : " + ex.Message;
            lblMsg.ForeColor = Color.Red;
        }
    }

    protected void btnSave_Click(object sender, EventArgs e)
    {
        try
        {
            lblMsg.Text = "";

            if (ddlCountry.SelectedIndex == -1)
            {
                lblMsg.Text = "Please select country.";
                return;
            }

            if (ddlBU.SelectedIndex == -1)
            {
                lblMsg.Text = "Please select BU.";
                return;
            }

            if (string.IsNullOrEmpty(txtBDM.Text.Trim()))
            {
                lblMsg.Text = "Please enter BDM.";
                return;
            }

            if (string.IsNullOrEmpty(txtEndUser.Text.Trim()))
            {
                lblMsg.Text = "Please enter End User.";
                return;
            }

            if (string.IsNullOrEmpty(txtGoodsDesc.Text.Trim()))
            {
                lblMsg.Text = "Please enter Goods Description.";
                return;
            }

            if (txtRemark1.Enabled)
            {
                if (string.IsNullOrEmpty(txtRemark1.Text.Trim()))
                {
                    lblMsg.Text = "Please enter Remark 1.";
                    return;
                }
            }

            if (txtRemark2.Enabled)
            {
                if (string.IsNullOrEmpty(txtRemark2.Text.Trim()))
                {
                    lblMsg.Text = "Please enter Remark 2.";
                    return;
                }
            }

            if (txtRemark3.Enabled)
            {
                if (string.IsNullOrEmpty(txtRemark3.Text.Trim()))
                {
                    lblMsg.Text = "Please enter Remark 3.";
                    return;
                }
            }

            if (string.IsNullOrEmpty(txtQuoteDate.Text.Trim()))
            {
                lblMsg.Text = "Please enter Quote Date.";
                return;
            }

            if (string.IsNullOrEmpty(txtCost.Text.Trim()))
            {
                lblMsg.Text = "Please enter Cost.";
                return;
            }

            if (string.IsNullOrEmpty(txtLanded.Text.Trim()))
            {
                lblMsg.Text = "Please enter Landed.";
                return;
            }

            if (string.IsNullOrEmpty(txtMargin.Text.Trim()))
            {
                lblMsg.Text = "Margin can't be blank, Please enter margin $";
                return;
            }

            if (string.IsNullOrEmpty(txtTotalSell.Text.Trim()))
            {
                lblMsg.Text = "Please enter Total Sell Amount.";
                return;
            }

            if (string.IsNullOrEmpty(txtClodingDate.Text.Trim()))
            {
                lblMsg.Text = "Please enter expected closing date.";
                return;
            }

            string ResellerCode="", ResellerName="";

            if (ddlcustomer.SelectedIndex >= 0)
            {
                foreach (ListItem customer in ddlcustomer.Items)
                {
                    if (customer.Selected)
                    {
                        if (string.IsNullOrEmpty(ResellerName))
                        {
                            ResellerCode =  customer.Value ;
                            ResellerName =  customer.Text ;
                        }
                        else
                        {
                            ResellerCode = ResellerCode + ";" + customer.Value + "";
                            ResellerName = ResellerName + ";" + customer.Text + "";
                        }
                    }
                }
            }

            string LoggedInUserName = myGlobal.loggedInUser();
            if (string.IsNullOrEmpty(LoggedInUserName))
            {
                LoggedInUserName = HttpContext.Current.User.Identity.Name;
            }

            if (string.IsNullOrEmpty(ResellerCode) || string.IsNullOrEmpty(ResellerName))
            {
                lblMsg.Text = "Please select at least one Customer";
                return;
            }
            //DateTime QuoteDate = Convert.ToDateTime(txtQuoteDate.Text);
            //DateTime ClosingDate = Convert.ToDateTime(txtClodingDate.Text);

            DateTime QuoteDate = DateTime.Now;
            DateTime ClosingDate = DateTime.Now;
            try
            {
                QuoteDate = DateTime.ParseExact(txtQuoteDate.Text, "M/dd/yyyy", CultureInfo.CurrentCulture); //Convert.ToDateTime(txtQuoteDate.Text);
                ClosingDate = DateTime.ParseExact(txtClodingDate.Text, "M/dd/yyyy", CultureInfo.CurrentCulture); //Convert.ToDateTime(txtQuoteDate.Text);
            }
            catch(Exception ex) 
            {
                try
                {
                    QuoteDate = DateTime.Now;
                    ClosingDate = DateTime.Now;
                }
                catch (Exception ex1) { }
            }

            if (btnSave.Text == "Save")
            {

                string sql = " declare @Fid int ; insert into dbo.tejFunnel2017(bdm,quoteID,endUser,resellerCode,resellerName,country,BU,goodsDescr,quoteDate,quoteMonth,quoteMonthMMM,quoteYear,expClosingDt,expClosingMonth,expClosingMonthMMM,expClosingYear,Cost,Landed,value,MarginUSD,dealStatus,remarks,CreatedOn,userLastUpdateDate,CreatedBy,NextReminderDt,ChangeCount,orderBookedDate,InvoiceDt) Values ('" + txtBDM.Text + "',dbo.GetQuoteID('" + ddlCountry.SelectedItem.Text + "'),'" + txtEndUser.Text + "','" + ResellerCode + "','" + ResellerName + "','" + ddlCountry.SelectedItem.Text + "','" + ddlBU.SelectedItem.Text + "','" + txtGoodsDesc.Text + "','" + txtQuoteDate.Text + "'," + QuoteDate.Month + ",'" + QuoteDate.ToString("MMM") + "'," + QuoteDate.Year + ",'" + txtClodingDate.Text + "'," + ClosingDate.Month + ",'" + ClosingDate.ToString("MMM") + "'," + ClosingDate.Year + "," + txtCost.Text + "," + txtLanded.Text + "," + txtTotalSell.Text + "," + txtMargin.Text + ",'" + ddlStatus.SelectedItem.Text + "','" + txtRemark1.Text + "',Getdate(),Getdate(),'" + LoggedInUserName + "', case when '" + txtReminderOn.Text.Trim() + "'='' then null else '" + txtReminderOn.Text + "' end ,1,case when '" + txtOrderDate.Text.Trim() + "'='' then null else '" + txtOrderDate.Text.Trim() + "' end, case when  '" + txtInvoiceDate.Text.Trim() + "'='' then null else '" + txtInvoiceDate.Text.Trim() + "' end )  ; 	set @Fid =  @@Identity ;  exec RDD_UpdateFunnel @Fid ";
                Db.constr = myGlobal.getAppSettingsDataForKey("tejSAP");
                Db.myExecuteSQL(sql);

                lblMsg.Text = "Funnel Deal saved successfully.";
                Page.ClientScript.RegisterStartupScript(this.GetType(), "alert", "<script> alert('Funnel Deal saved successfully.'); </script>");
                lblMsg.ForeColor = Color.Green;
                ClearControl();
            }
            else if(btnSave.Text=="Update")
            {
                int changeCount = 0;
                try
                {
                    if (!string.IsNullOrEmpty(lblChangeCount.Text))
                    {
                        changeCount = Convert.ToInt32(lblChangeCount.Text);
                        changeCount = changeCount + 1;
                    }
                    else // If changecount is blank then consider default as 2 
                    {
                        changeCount = 2;
                    }
                }
                catch 
                {
                    changeCount = 2;
                }

                string sql = " Update dbo.tejFunnel2017 Set bdm='" + txtBDM.Text + "',endUser='" + txtEndUser.Text + "',resellerCode='" + ResellerCode + "',resellerName='" + ResellerName + "',goodsDescr='" + txtGoodsDesc.Text + "',quoteDate='" + txtQuoteDate.Text + "',quoteMonth=" + QuoteDate.Month + ",quoteMonthMMM='" + QuoteDate.ToString("MMM") + "',quoteYear=" + QuoteDate.Year + ",expClosingDt='" + txtClodingDate.Text + "',expClosingMonth=" + ClosingDate.Month + ",expClosingMonthMMM='" + ClosingDate.ToString("MMM") + "',expClosingYear=" + ClosingDate.Year + ",Cost=" + txtCost.Text + ",Landed=" + txtLanded.Text + ",value=" + txtTotalSell.Text + ",MarginUSD=" + txtMargin.Text + ",dealStatus='" + ddlStatus.SelectedItem.Text + "',userLastUpdateDate=getdate(),NextReminderDt=case when '" + txtReminderOn.Text.Trim() + "'='' then null else '" + txtReminderOn.Text.Trim() + "' end,LastUpdatedOn=getdate(),LastUpdatedBy='" + LoggedInUserName + "', ChangeCount=" + changeCount.ToString() + ",Remarks2='" + txtRemark2.Text + "',Remarks3='" + txtRemark3.Text + "',orderBookedDate= case when '" + txtOrderDate.Text.Trim() + "'='' then null else '" + txtOrderDate.Text.Trim() + "' end ,InvoiceDt= case when '" + txtInvoiceDate.Text.Trim() + "'='' then null else '" + txtInvoiceDate.Text.Trim() + "' end  Where Fid=" + lblFid.Text + " ; exec RDD_UpdateFunnel  " + lblFid.Text;

                Db.constr = myGlobal.getAppSettingsDataForKey("tejSAP");
                Db.myExecuteSQL(sql);

                //lblMsg.Text = "Funnel Deal updated successfully.";
                //Page.ClientScript.RegisterStartupScript(this.GetType(), "alert", "<script> alert('Funnel Deal updated successfully.'); </script>");
                //lblMsg.ForeColor = Color.Green;

                Response.Redirect("NewFunnelDeal.aspx?fid=0&Access=NEW&Action=Updated");
            }
        }
        catch (Exception ex)
        {
            lblMsg.Text = "Error occured in btnSave_Click : " + ex.Message;
            lblMsg.ForeColor = Color.Red;
        }
    }

    public void ClearControl()
    {

        txtBDM.Text = "";
        txtEndUser.Text = "";
        txtGoodsDesc.Text = "";
        txtRemark1.Text = "";
        txtRemark2.Text = "";
        txtRemark3.Text = "";
        txtCost.Text = "";
        txtLanded.Text = "";
        txtMarginPercent.Text = "";
        txtMargin.Text = "";
        txtTotalSell.Text = "";
        txtClodingDate.Text = "";
        txtOrderDate.Text = "";
        txtInvoiceDate.Text = "";
        txtReminderOn.Text = "";

        try
        {
            txtCreatedBy.Text = myGlobal.loggedInUser();
            txtCreatedDate.Text = DateTime.Now.ToString("MM/dd/yyyy");
            txtQuoteDate.Text = DateTime.Now.ToString("MM/dd/yyyy");
            //txtQuoteID.Text = Db.myExecuteScalar2("Select dbo.GetQuoteID('" + ddlCountry.SelectedItem.Text + "')");
        }
        catch { }
       
        try
        {
            ddlCountry.SelectedIndex = -1;
            ddlCountry.SelectedItem.Text = "--SELECT--";
        }
        catch{}

        try
        {
            ddlBU.SelectedIndex = -1;
            ddlBU.SelectedItem.Text = "--SELECT--";
        }
        catch{}
        try
        {
            ddlcustomer.SelectedIndex = -1;
        }
        catch{}
        try
        {
            ddlStatus.SelectedItem.Text = "OPEN";
            ddlStatus.SelectedItem.Value = "OPEN";
        }
        catch{}

    }

    protected void BtnDelete_Click(object sender, EventArgs e)
    {
        try
        {
            Db.constr = myGlobal.getAppSettingsDataForKey("tejSAP");
            Db.myExecuteSQL(" Update dbo.tejfunnel2017 set isRowActive=0,LastUpdatedBy='"+myGlobal.loggedInUser()+"',LastUpdatedOn=getdate() where fid= "+lblFid.Text);
            //lblMsg.Text = "Funnel Deal Deleted Successfully.";
            //Page.ClientScript.RegisterStartupScript(this.GetType(), "alert", "<script> alert('Funnel Deal Deleted Successfully.'); </script>");
            //lblMsg.ForeColor = Color.Green;

            //BindDDL("NEW");
            //ClearControl();
            //ddlcustomer.Items.Clear();
            //ddlCountry.Enabled = true;
            //ddlBU.Enabled = true;
            //btnSave.Text = "Save";
            //BtnDelete.Enabled = false;
            Response.Redirect("NewFunnelDeal.aspx?fid=0&Access=NEW&Action=Deleted");
        }
        catch (Exception ex)
        {
            lblMsg.Text = "Error occured in BtnDelete_Click : " + ex.Message;
            lblMsg.ForeColor = Color.Red;
        }
    }

    //[WebMethod]
    //public static List<customers> GetCustomers()
    //{
    //    List<customers> custList = new List<customers>();

    //    DataTable DTCust = Db.myGetDS(" exec [getFunnelCustomerListNF] 'UGANDA'"  ).Tables[0];

    //    custList = (from DataRow dr in DTCust.Rows
    //                select new customers()
    //                   {
    //                       CardCode = dr["CardCode"].ToString(),
    //                       CardName = dr["CardName"].ToString(),
    //                   }).ToList();   

    //    return custList;
    //}
}


//public class customers
//{
//    public string CardCode { get; set; }
//    public string CardName { get; set; }
//}