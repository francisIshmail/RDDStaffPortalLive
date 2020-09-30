using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Drawing;
using System.Data;
using System.IO;

public partial class IntranetNew_Funnel_FunnelList : System.Web.UI.Page
{
    protected void Page_Load(object sender, EventArgs e)
    {
        try
        {
            if (!IsPostBack)
            {
                Db.constr = myGlobal.getAppSettingsDataForKey("tejSAP");

                int count = Db.myExecuteScalar("Select COUNT(*) from dbo.MenuWiseForms t0 Join dbo.UserAuthorization t1 on t0.MenuId=t1.MenuId and t1.MembershipUserName='" + myGlobal.loggedInUser() + "' And t0.FormURL='FunnelList.aspx' And t1.IsActive=1");
                if (count > 0)
                {
                    lblMsg.Text = "";
                    BindDDL();
                }
                else
                {
                    Response.Redirect("Default.aspx?UserAccess=0&FormName=Funnel List");
                }
            }
        }
        catch (Exception ex)
        {
            lblMsg.Text = "Page Load Error : " + ex.Message;
            lblMsg.ForeColor = Color.Red;
        }
    }

    private void BindDDL()
    {
        try
        {
            lblMsg.Text = "";
            Db.constr = myGlobal.getAppSettingsDataForKey("tejSAP");
            DataSet DS = Db.myGetDS("select DealStatus from tejfunneldealstatus ; select distinct f.BU from tejsalespersonmap s Join funnelsetup f on s.salesperson=f.salesperson And Membershipuser='" + myGlobal.loggedInUser() + "' And f.Access in ('FULL ACCESS','READ ONLY') ; SELECT yr from [dbo].[tejFunnelYears] ");
            ddlDealStatus.DataSource = DS.Tables[0];  // Table [0] for Deal Status
            ddlDealStatus.DataTextField = "DealStatus";
            ddlDealStatus.DataValueField = "DealStatus";
            ddlDealStatus.DataBind();

            ddlBU.DataSource = DS.Tables[1];    // Table [1] for BU's
            ddlBU.DataTextField = "BU";
            ddlBU.DataValueField = "BU";
            ddlBU.DataBind();

            ddlQuoteYear.DataSource = DS.Tables[2];    // Table [1] for Year
            ddlQuoteYear.DataTextField = "yr";
            ddlQuoteYear.DataValueField = "yr";
            ddlQuoteYear.DataBind();

            ddlCloseYear.DataSource = DS.Tables[2];    // Table [1] for Year
            ddlCloseYear.DataTextField = "yr";
            ddlCloseYear.DataValueField = "yr";
            ddlCloseYear.DataBind();

            DataTable DTCountry = Db.myGetDS(" select distinct f.country as countrycode, C.Country from tejsalespersonmap s Join funnelsetup f on s.salesperson=f.salesperson And Membershipuser='"+myGlobal.loggedInUser()+"' And f.Access in ('FULL ACCESS','READ ONLY') JOIN rddcountrieslist C ON f.country=C.countrycode ").Tables[0];
            ddlCountry.DataSource = DTCountry;// Table [2] for Countries
            ddlCountry.DataTextField = "country";
            ddlCountry.DataValueField = "countrycode";
            ddlCountry.DataBind();

        }
        catch (Exception ex)
        {
            lblMsg.Text = "Error BindDDL : " + ex.Message;
            lblMsg.ForeColor = Color.Red;
        }
    }

    //protected void ddlCountry_SelectedIndexChanged(object sender, EventArgs e)
    //{
    //    try
    //    {
    //        if (ddlCountry.SelectedIndex >= 0)
    //        {
    //            string country = ddlCountry.SelectedItem.Value;

    //            int selectedCountrycount = 0;
    //            foreach (ListItem item in ddlCountry.Items)
    //            {
    //                if (item.Selected)
    //                {
    //                    selectedCountrycount = selectedCountrycount + 1;
    //                }
    //            }
    //            if (selectedCountrycount == 1)
    //            {
    //                string DBName = "";
    //                if (country == "UG")
    //                    DBName = "SAPUG";
    //                else if (country == "KE")
    //                    DBName = "SAPKE";
    //                else if (country == "TZ")
    //                    DBName = "SAPTZ";
    //                else
    //                    DBName = "SAPAE";

    //                DataTable DTCust = Db.myGetDS(" Select CardCode,lower(CardName) as CardName from [" + DBName + "].[dbo].[OCRD] Where CardType='C' ").Tables[0];
    //                ddlcustomer.DataSource = DTCust;// Table [2] for Countries
    //                ddlcustomer.DataTextField = "CardName";
    //                ddlcustomer.DataValueField = "CardCode";
    //                ddlcustomer.DataBind();
    //                ddlcustomer.Enabled = true;
    //            }
    //            else
    //            {
    //                ddlcustomer.DataSource = null;
    //                ddlcustomer.DataBind();
    //                ddlcustomer.Enabled = false;
    //            }
    //        }
    //        else
    //        {
    //            ddlcustomer.DataSource = null;
    //            ddlcustomer.DataBind();
    //            ddlcustomer.Enabled = false;
    //        }
    //    }
    //    catch (Exception ex )
    //    {
    //        lblMsg.Text = "Error ddlCountry_SelectedIndexChanged : " + ex.Message;
    //        lblMsg.ForeColor = Color.Red;
    //    }
    //}
 
    protected void btnNewDeal_Click(object sender, EventArgs e)
    {
        try
        {
            Response.Redirect("NewFunnelDeal.aspx?fid=0&Access=NEW&Action=Add");
        }
        catch (Exception ex)
        {
            lblMsg.Text = "Error occured in Search : " + ex.Message;
            lblMsg.ForeColor = Color.Red;
        }
    }

    protected void BtnSearch_Click(object sender, EventArgs e)
    {
        try
        {
            lblMsg.Text = "";
            lblMsg.ForeColor = Color.Red;
            //if (ddlCountry.SelectedIndex == -1)
            //{
            //    lblMsg.Text = "Please select country";
            //    return;
            //}
            //if (ddlBU.SelectedIndex == -1)
            //{
            //    lblMsg.Text = "Please select BU";
            //    return;
            //}
            //if (ddlQuoteYear.SelectedIndex == -1)
            //{
            //    lblMsg.Text = "Please select Quote Year";
            //    return;
            //}

            string Country = "", BUs = "", DealStatus = "", QuoteMnth = "", QuoteYr = "", CloseMnth = "", CloseYr = "";

            foreach (ListItem ctry in ddlCountry.Items)
            {
                if (ctry.Selected)
                {
                    if (string.IsNullOrEmpty(Country))
                        Country = "'" + ctry.Text + "'";
                    else
                        Country = Country + ",'" + ctry.Text + "'";
                }
            }

            foreach (ListItem BU in ddlBU.Items)
            {
                if (BU.Selected)
                {
                    if (string.IsNullOrEmpty(BUs))
                        BUs = "'" + BU.Text + "'";
                    else
                        BUs = BUs + ",'" + BU.Text + "'";
                }
            }

            if (ddlDealStatus.SelectedIndex >= 0)
            {
                foreach (ListItem Status in ddlDealStatus.Items)
                {
                    if (Status.Selected)
                    {
                        if (string.IsNullOrEmpty(DealStatus))
                            DealStatus = "'" + Status.Text + "'";
                        else
                            DealStatus = DealStatus + ",'" + Status.Text + "'";
                    }
                }
            }

            if (ddlQuoteMonth.SelectedIndex >= 0)
            {
                foreach (ListItem QMonth in ddlQuoteMonth.Items)
                {
                    if (QMonth.Selected)
                    {
                        if (string.IsNullOrEmpty(QuoteMnth))
                            QuoteMnth = "'" + QMonth.Text + "'";
                        else
                            QuoteMnth = QuoteMnth + ",'" + QMonth.Text + "'";
                    }
                }
            }

            if (ddlQuoteYear.SelectedIndex >= 0)
            {
                foreach (ListItem QYear in ddlQuoteYear.Items)
                {
                    if (QYear.Selected)
                    {
                        if (string.IsNullOrEmpty(QuoteYr))
                            QuoteYr = QYear.Text;
                        else
                            QuoteYr = QuoteYr + "," + QYear.Text;
                    }
                }
            }

            if (ddlCloseMonth.SelectedIndex >= 0)
            {
                foreach (ListItem CMonth in ddlCloseMonth.Items)
                {
                    if (CMonth.Selected)
                    {
                        if (string.IsNullOrEmpty(CloseMnth))
                            CloseMnth = "'" + CMonth.Text + "'";
                        else
                            CloseMnth = CloseMnth + ",'" + CMonth.Text + "'";
                    }
                }
            }

            if (ddlCloseYear.SelectedIndex >= 0)
            {
                foreach (ListItem CYear in ddlCloseYear.Items)
                {
                    if (CYear.Selected)
                    {
                        if (string.IsNullOrEmpty(CloseYr))
                            CloseYr = CYear.Text;
                        else
                            CloseYr = CloseYr + "," + CYear.Text;
                    }
                }
            }

            string sql = "";

            sql = @"Select F.fid,F.bdm,F.quoteID,F.endUser,F.resellerCode,F.resellerName,F.country,F.BU,F.goodsDescr, convert(varchar,F.quoteDate,101) quoteDate,F.quoteMonth,F.QuoteYear,F.expClosingMonth,F.expClosingYear, cast(F.expClosingMonth as varchar)+'/'+cast(F.expClosingYear as varchar) as CloseDt, F.DealStatus, F.Cost,F.Landed,F.value ,F.MarginUSD, F.Remarks,F.Remarks2,Remarks3, convert(varchar, F.CreatedOn , 101) CreatedOn, F.CreatedBy,convert(varchar, F.LastUpdatedOn , 101) LastUpdatedOn, F.LastUpdatedBy,  S.Access from tejfunnel2017 F 
	                    Join FunnelSetUp S ON F.BU=S.BU
	                    JOIN rddcountrieslist C ON C.CountryCode=S.Country And F.Country=C.Country
                    Where F.isRowActive=1  And S.Access in ('FULL ACCESS','READ ONLY') And S.MembershipUserName='" + myGlobal.loggedInUser() + "'  ";


            if (!string.IsNullOrEmpty(Country))
            {
                sql = sql + " And F.Country in (" + Country + ") ";
            }
            if (!string.IsNullOrEmpty(BUs))
            {
                sql = sql + " And F.BU in (" + BUs + ") ";
            }
            if (!string.IsNullOrEmpty(DealStatus))
            {
                sql = sql + " And F.dealStatus in (" + DealStatus + ") ";
            }
            if (!string.IsNullOrEmpty(QuoteMnth))
            {
                sql = sql + " And F.quoteMonthMMM in (" + QuoteMnth + ") ";
            }
            if (!string.IsNullOrEmpty(QuoteYr))
            {
                sql = sql + " And F.quoteYear in (" + QuoteYr + ") ";
            }
            if (!string.IsNullOrEmpty(CloseMnth))
            {
                sql = sql + " And F.expClosingMonthMMM in (" + CloseMnth + ") ";
            }
            if (!string.IsNullOrEmpty(CloseYr))
            {
                sql = sql + " And F.expClosingYear in (" + CloseYr + ") ";
            }

            sql = sql + "  Order by F.fid desc";

            Db.constr = myGlobal.getAppSettingsDataForKey("tejSAP");
            DataSet DS = Db.myGetDS(sql);

            if (DS.Tables.Count > 0)
            {
                Session["FunnelDeals"] = DS.Tables[0];
            }
            if (DS.Tables.Count > 0)
            {
                if (DS.Tables[0].Rows.Count > 0)
                {
                    BtnExportToExcel.Enabled = true;
                    Grid1.DataSource = DS;
                    Grid1.DataBind();
                }
                else
                {
                    lblMsg.Text = "No data found";
                }
            }
        }
        catch (Exception ex)
        {
            lblMsg.Text = "Error occured in Search : " + ex.Message;
            lblMsg.ForeColor = Color.Red;
        }
    }

    protected void BtnExportToExcel_Click(object sender, EventArgs e)
    {
        try
        {
            if (Session["FunnelDeals"] != null)
            {
                DataTable dt = (DataTable)Session["FunnelDeals"];
                string attachment = "attachment; filename=FunnelDeals_" + DateTime.Now.ToString("yyyy-MM-dd") + ".xls";
                Response.ClearContent();
                Response.AddHeader("content-disposition", attachment);
                Response.ContentType = "application/vnd.ms-excel";
                string tab = "";
                foreach (DataColumn dc in dt.Columns)
                {
                    Response.Write(tab + dc.ColumnName);
                    tab = "\t";
                }
                Response.Write("\n");
                int i;
                foreach (DataRow dr in dt.Rows)
                {
                    tab = "";
                    for (i = 0; i < dt.Columns.Count; i++)
                    {
                        Response.Write(tab + dr[i].ToString());
                        tab = "\t";
                    }
                    Response.Write("\n");
                }
                Response.End();
            }
        }
        catch (Exception ex)
        {
            lblMsg.Text = "Error occured in Export : " + ex.Message;
            lblMsg.ForeColor = Color.Red;
        }
    }

    protected void Grid1_SelectedIndexChanged(object sender, EventArgs e)
    {
        try
        {

            GridViewRow row = Grid1.SelectedRow;
            lblMsg.Text = "";
            Label lblfid = (Label)row.FindControl("lblfid");
            Label lblAccess = (Label)row.FindControl("lblAccess");

            Response.Redirect("NewFunnelDeal.aspx?fid=" + lblfid.Text + "&Access=" + lblAccess.Text + "&Action=Edit");

        }
        catch (Exception ex)
        {
            lblMsg.Text = "Error occured in SelectedIndexChanged : " + ex.Message;
            lblMsg.ForeColor = Color.Red;
        }
    }

}