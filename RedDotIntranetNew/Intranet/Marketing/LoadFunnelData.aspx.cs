using System;
using System.Collections;
using System.Configuration;
using System.Data;
using System.Linq;
using System.Web;
using System.Web.Security;
using System.Web.UI;
using System.Web.UI.HtmlControls;
using System.Web.UI.WebControls;
using System.Web.UI.WebControls.WebParts;
using System.Xml.Linq;
using System.Text.RegularExpressions;
using System.Data.SqlClient;

public partial class Intranet_Marketing_LoadFunnelData : System.Web.UI.Page
{
    //Global gbl = new Global();
    string Queryfilter;// prevCntryDbCode;

    protected void Page_Load(object sender, EventArgs e)
    {
        Response.CacheControl = "private";
        Response.Expires = 0;
        Response.AddHeader("pragma", "no-cache");

        lblMsg.Text = "";

        if (!IsPostBack)
        {
            Db.constr = myGlobal.getAppSettingsDataForKey("tejSap");
            DataSet DSRegions = Db.myGetDS(" EXEC getRegionsForUser  " + myGlobal.loggedInUser() +" , 'Country' "  );
            string regions="";
            if (DSRegions.Tables.Count > 0)
            {
                if (DSRegions.Tables[0].Rows.Count > 0)
                {
                    regions = DSRegions.Tables[0].Rows[0][0].ToString();
                    Session["pregions"] = regions;
                    if (string.IsNullOrEmpty(regions))
                    {
                        lblMsg.Text = "Countries are not assigned to you, Please contact IT team.";
                    }
                }
                else
                {
                    lblMsg.Text = "Countries are not assigned to you, Please contact IT team.";
                }
            }
            else
            {
                lblMsg.Text = "Countries are not assigned to you, Please contact IT team.";
            }

            loadCountries(ddlCountryForNewReseller, regions);// load this before Reseller
            
            loadQT(ddlFltrQT);
            loadCountries(ddlFltrCountry, regions);// load this before Reseller
            
            if(ddlFltrCountry.SelectedIndex>=0)
             loadReseller(ddlFltrReseller,splitdbCodeForCountry(ddlFltrCountry.SelectedValue.ToString()));
            
            loadBUs(ddlFltrBU);
            loadYrs(ddlFltrYear);
            loadMonths(ddlFltrMonth);
            loadDealStatus(ddlFltrDealStatus);

            BindGrid();
        }
        
    }

   
    private string splitdbCodeForCountry(string pstr)
    {
        string pRet="";
        int tindx = pstr.IndexOf(']');
        pRet = pstr.Substring(1, tindx-1);
        return pRet;
    }

    private string splitResellerCode(string pstr)
    {
        string pRet = "";
        int tindx = pstr.IndexOf(']');
        pRet = pstr.Substring(tindx+2, pstr.Length-(tindx + 2));
        return pRet;
    }

    private string splitResellerName(string pstr)
    {
        string pRet = "";
        int tindx = pstr.LastIndexOf('[');
        pRet = pstr.Substring(0, tindx);
        return pRet;
    }

    private void loadCountries(DropDownList ddl, string regions)
    {
        if (!string.IsNullOrEmpty(regions))
        {
            string strSQL = "";
            if (regions == "ALL")
            {
                strSQL = "select ('[' + dbCode + '] ' + CountryCode) as CountryOfDb,Country from [dbo].[rddCountriesList] order by Country";
            }
            else
            {
                strSQL = "select ('[' + dbCode + '] ' + CountryCode) as CountryOfDb,Country from [dbo].[rddCountriesList] Where dbCode in (Select * from dbo.SplitString('"+regions+"',';'))  order by Country";
            }
            //Db.LoadDDLsWithCon(ddl, "select CountryCode,Country from [dbo].[rddCountriesList] order by Country", "Country", "Country", myGlobal.getAppSettingsDataForKey("tejSap"));
            Db.LoadDDLsWithCon(ddl, strSQL, "Country", "CountryOfDb", myGlobal.getAppSettingsDataForKey("tejSap"));
            if (ddl.Items.Count > 0)
                ddl.SelectedIndex = 0;
            else
            {
                ddl.Items.Add("No Rows");
                ddl.SelectedIndex = 0;
            }
        }
    }

    private void loadReseller(DropDownList ddl,String Cntrydbcode)
    {
        //Db.LoadDDLsWithCon(ddl, "select distinct(resellerCode),resellerName from [dbo].[tejFunnel2017] order by resellerName", "resellerName", "resellerCode", myGlobal.getAppSettingsDataForKey("tejSap"));

        if (Cntrydbcode != "" && lblprevCntryDbCode.Text != Cntrydbcode)  //only if country is selected
        {
            Db.LoadDDLsWithCon(ddl, "Exec [getFunnelCustomerListNew] '" + Cntrydbcode + "','YES'", "resellerName", "resellerCode", myGlobal.getAppSettingsDataForKey("tejSap"));

            lblprevCntryDbCode.Text = Cntrydbcode;

            if (ddl.Items.Count > 0)
                ddl.SelectedIndex = 0;
            else
            {
                ddl.Items.Add("No Rows");
                ddl.SelectedIndex = 0;
            }
        }
    }

    //private void loadResellerForGridEditing(DropDownList ddl, String Cntrydbcode)
    //{
    //    //Db.LoadDDLsWithCon(ddl, "select distinct(resellerCode),resellerName from [dbo].[tejFunnel2017] order by resellerName", "resellerName", "resellerCode", myGlobal.getAppSettingsDataForKey("tejSap"));

    //    if (Cntrydbcode != "" && lblprevCntryDbCode.Text != Cntrydbcode)  //only if country is selected
    //    {
    //        Db.LoadDDLsWithConRelsellersNCodeView(ddl, "Exec [getFunnelCustomerList] '" + Cntrydbcode + "'", "resellerName", "resellerCode", myGlobal.getAppSettingsDataForKey("tejSap"));

    //        lblprevCntryDbCode.Text = Cntrydbcode;

    //        if (ddl.Items.Count > 0)
    //            ddl.SelectedIndex = 0;
    //        else
    //        {
    //            ddl.Items.Add("No Rows");
    //            ddl.SelectedIndex = 0;
    //        }
    //    }
    //}

    private void loadQT(DropDownList ddl)
    {
        string SqlQry = " EXEC getQuoteIDForUser " + myGlobal.loggedInUser();
        Db.LoadDDLsWithCon(ddl,SqlQry, "quoteID", "quoteID", myGlobal.getAppSettingsDataForKey("tejSap"));
            //Db.LoadDDLsWithCon(ddl, "select distinct(quoteID) from [dbo].[tejFunnel2017] where isRowActive=1 and createdBy='" + myGlobal.loggedInUser() + "' and resellerCode<>'XXX' order by quoteID", "quoteID", "quoteID", myGlobal.getAppSettingsDataForKey("tejSap"));
        if (ddl.Items.Count > 0)
            ddl.SelectedIndex = 0;
        else
        {
            ddl.Items.Add("No Rows");
            ddl.SelectedIndex = 0;
        }
    }

    private void loadBUs(DropDownList ddl)
    {
        string SQLQry = " EXEC getRegionsForUser " + myGlobal.loggedInUser() + " , 'BU'"; 
        //Db.LoadDDLsWithCon(ddl, "select groupBU,BU from tejSap.dbo.Mapping_BUs where region='TRI' and PMForBU='" + myGlobal.loggedInUser() + "' order by BU", "BU", "BU", myGlobal.getAppSettingsDataForKey("tejSap"));
        Db.LoadDDLsWithCon(ddl, SQLQry , "BU", "BU", myGlobal.getAppSettingsDataForKey("tejSap"));
        if (ddl.Items.Count > 0)
            ddl.SelectedIndex = 0;
        else
        {
            ddl.Items.Add("No Rows");
            ddl.SelectedIndex = 0;
        }
    }

    private void loadYrs(DropDownList ddl)
    {
        Db.LoadDDLsWithCon(ddl, "SELECT yr from [dbo].[tejFunnelYears]", "yr", "yr", myGlobal.getAppSettingsDataForKey("tejSap"));
        if (ddl.Items.Count > 0)
            ddl.SelectedIndex = 0;
        else
        {
            ddl.Items.Add("No Rows");
            ddl.SelectedIndex = 0;
        }
    }

    private void loadMonths(DropDownList ddl)
    {
        Db.LoadDDLsWithCon(ddl, "SELECT MonthNo,MonthMMM from [dbo].[tejFunnelMonth]", "MonthMMM", "MonthNo", myGlobal.getAppSettingsDataForKey("tejSap"));
        if (ddl.Items.Count > 0)
            ddl.SelectedIndex = 0;
        else
        {
            ddl.Items.Add("No Rows");
            ddl.SelectedIndex = 0;
        }
    }

    private void loadDealStatus(DropDownList ddl)
    {
        Db.LoadDDLsWithCon(ddl, "SELECT DealStatus from [dbo].[tejFunnelDealStatus]", "DealStatus", "DealStatus", myGlobal.getAppSettingsDataForKey("tejSap"));
        if (ddl.Items.Count > 0)
            ddl.SelectedIndex = 0;
        else
        {
            ddl.Items.Add("No Rows");
            ddl.SelectedIndex = 0;
        }
    }

    private string generateFilterQuery( string WhereCondition )
    {

        //Queryfilter=" Where CreatedBy='" + myGlobal.loggedInUser() + "'";  //base query
        Queryfilter = WhereCondition;
        
        if (CheckAll.Checked)
        {
            Queryfilter = WhereCondition;
            //Queryfilter = " Where CreatedBy='" + myGlobal.loggedInUser() + "'";
        }
        else
        {
            if (chkQT.Checked && ddlFltrQT.SelectedValue.ToString() != "No Rows")
            {
                if (Queryfilter == "")
                {
                    Queryfilter = " Where ";
                    Queryfilter = Queryfilter + "  quoteID='" + ddlFltrQT.SelectedValue.ToString() + "'";
                }
                else
                {
                    Queryfilter = Queryfilter + " and ";
                    Queryfilter = Queryfilter + "  quoteID='" + ddlFltrQT.SelectedValue.ToString() + "'";
                }
            }


            if (chkReseller.Checked && ddlFltrReseller.SelectedValue.ToString() != "No Rows")
            //Queryfilter = Queryfilter + " and resellerCode='" + ddlFltrReseller.SelectedValue.ToString() + "'";
            {
                if (Queryfilter == "")
                {
                    Queryfilter = " Where ";
                    Queryfilter = Queryfilter + "  resellerCode='" + splitResellerCode(ddlFltrReseller.SelectedValue.ToString()) + "'";
                }
                else
                {
                    Queryfilter = Queryfilter + " and ";
                    Queryfilter = Queryfilter + " resellerCode='" + splitResellerCode(ddlFltrReseller.SelectedValue.ToString()) + "'";
                }
            }

            if (chkCountry.Checked && ddlFltrCountry.SelectedValue.ToString() != "No Rows")
            //Queryfilter = Queryfilter + " and country='" + ddlFltrCountry.SelectedValue.ToString() + "'";
            {
                if (Queryfilter == "")
                {
                    Queryfilter = " Where ";
                    Queryfilter = Queryfilter + "  country='" + ddlFltrCountry.SelectedItem.Text + "'";
                }
                else
                {
                    Queryfilter = Queryfilter + " and ";
                    Queryfilter = Queryfilter + " country='" + ddlFltrCountry.SelectedItem.Text + "'";
                }
            }

            if (chkBU.Checked && ddlFltrBU.SelectedValue.ToString() != "No Rows")
            //Queryfilter = Queryfilter + " and BU='" + ddlFltrBU.SelectedValue.ToString() + "'";
            {
                if (Queryfilter == "")
                {
                    Queryfilter = " Where ";
                    Queryfilter = Queryfilter + "  BU='" + ddlFltrBU.SelectedValue.ToString() + "'";
                }
                else
                {
                    Queryfilter = Queryfilter + " and ";
                    Queryfilter = Queryfilter + " BU='" + ddlFltrBU.SelectedValue.ToString() + "'";
                }
            }

            if (chkDealStatus.Checked && ddlFltrDealStatus.SelectedValue.ToString() != "No Rows")
            //Queryfilter = Queryfilter + " and dealStatus='" + ddlFltrDealStatus.SelectedValue.ToString() + "'";
            {
                if (Queryfilter == "")
                {
                    Queryfilter = " Where ";
                    Queryfilter = Queryfilter + "  dealStatus='" + ddlFltrDealStatus.SelectedValue.ToString() + "'";
                }
                else
                {
                    Queryfilter = Queryfilter + " and ";
                    Queryfilter = Queryfilter + " dealStatus='" + ddlFltrDealStatus.SelectedValue.ToString() + "'";
                }
            }

            if (chkYear.Checked && ddlFltrYear.SelectedValue.ToString() != "No Rows")
            //Queryfilter = Queryfilter + " and quoteYear=" + ddlFltrYear.SelectedValue.ToString() + "";
            {
                if (Queryfilter == "")
                {
                    Queryfilter = " Where ";
                    Queryfilter = Queryfilter + " quoteYear=" + ddlFltrYear.SelectedValue.ToString() + "";
                }
                else
                {
                    Queryfilter = Queryfilter + " and ";
                    Queryfilter = Queryfilter + " quoteYear=" + ddlFltrYear.SelectedValue.ToString() + "";
                }
            }
            if (chkMonth.Checked && ddlFltrMonth.SelectedValue.ToString() != "No Rows")
            //Queryfilter = Queryfilter + " and quoteMonth=" + ddlFltrMonth.SelectedValue.ToString() + "";
            {
                if (Queryfilter == "")
                {
                    Queryfilter = " Where ";
                    Queryfilter = Queryfilter + " quoteMonth=" + ddlFltrMonth.SelectedValue.ToString() + "";
                }
                else
                {
                    Queryfilter = Queryfilter + " and ";
                    Queryfilter = Queryfilter + " quoteMonth=" + ddlFltrMonth.SelectedValue.ToString() + "";
                }
            }
        }
        //lblMsg.Text=Queryfilter;
        return Queryfilter;
    }
    protected void BindGrid()
    {
        //if (Grid1.EditIndex >= 0)
        //    Grid1.EditIndex = -1;

        if (btnNewDeal.Enabled == false)
        {
            Grid1.DataSource = (DataTable)Session["TblGrid1"];
            Grid1.DataBind();
        }
        else
        {
            String summarySQL=string.Empty;
            String sortExp = (String)ViewState["sortExpression"];
            String sortDir = (String)ViewState["sortDirection"];

            //report query
            //select * from tejSap.dbo.getSalesFunnelDataView Where CreatedBy='HARIDWAR' and quoteDate between convert(varchar(10),'02-06-2017',110) and convert(varchar(10),'02-09-2017',110) order by fid desc

            Db.constr = myGlobal.getAppSettingsDataForKey("tejSap");
            DataSet DSWhereCondition = Db.myGetDS(" EXEC getConditionToShowFunnelData  " + myGlobal.loggedInUser());
            string WhereCondition = "";
            if (DSWhereCondition.Tables.Count > 0)
            {
                if (DSWhereCondition.Tables[0].Rows.Count > 0)
                {
                    WhereCondition = DSWhereCondition.Tables[0].Rows[0][0].ToString();
                }
            }

            if (!string.IsNullOrEmpty(WhereCondition))
            {
                if (sortExp == null || sortExp == "")
                {
                    if (CheckAll.Checked)
                        summarySQL = "select * from tejSap.dbo.getSalesFunnelDataView " + generateFilterQuery(WhereCondition) + " order by fid desc";
                    else
                        summarySQL = "select * from tejSap.dbo.getSalesFunnelDataView " + generateFilterQuery(WhereCondition) + " order by fid desc";  //put the conditions
                }
                else
                {
                    if (CheckAll.Checked)
                        summarySQL = "select * from tejSap.dbo.getSalesFunnelDataView " + generateFilterQuery(WhereCondition) + " order by " + sortExp + " " + sortDir;
                    else
                        summarySQL = "select * from tejSap.dbo.getSalesFunnelDataView " + generateFilterQuery(WhereCondition) + " order by " + sortExp + " " + sortDir;
                }

                Db.constr = myGlobal.getAppSettingsDataForKey("tejSap");
                Session["TblGrid1"] = Db.myGetDS(summarySQL).Tables[0];
                Grid1.DataSource = (DataTable)Session["TblGrid1"];
                Grid1.DataBind();
                sumGridValue();

                lblRowCnt.Text = "Rows : " + Grid1.Rows.Count.ToString();
            }
            
            //lblMsg.Text = summarySQL;
        }
    }
    private void sumGridValue()
    {
        try
        {
            double tot = 0, totalCost = 0, totalLanded = 0;
            Label tmplblvalue;
            Label tmplbCost;
            Label tmplbLanded;

            foreach (GridViewRow prw in Grid1.Rows)
            {
                tmplblvalue = (Label)prw.FindControl("lblvalue") as Label;
                tmplbCost = (Label)prw.FindControl("lblCost") as Label;
                tmplbLanded = (Label)prw.FindControl("lblLanded") as Label;

                if (tmplblvalue != null)
                {
                    if (Util.isValidDecimalNumber(tmplblvalue.Text))
                        tot = tot + Convert.ToDouble(tmplblvalue.Text);
                }

                if (tmplbCost != null)
                {
                    if (Util.isValidDecimalNumber(tmplbCost.Text))
                        totalCost = totalCost + Convert.ToDouble(tmplbCost.Text);
                }

                if (tmplbLanded != null)
                {
                    if (Util.isValidDecimalNumber(tmplbLanded.Text))
                        totalLanded = totalLanded + Convert.ToDouble(tmplbLanded.Text);
                }
            }

            lblTotalUSD.Text = tot.ToString();
            lblTotalCostUSD.Text = totalCost.ToString();
            lblTotalLandedUSD.Text = totalLanded.ToString();
        }
        catch (Exception ex)
        {

        }
    }
    protected void BindGridforNew()
    {
        Queryfilter = " Where CreatedBy='" + myGlobal.loggedInUser() + "'";  //base query

        Db.constr = myGlobal.getAppSettingsDataForKey("tejSap");
        Session["TblGrid1"] = Db.myGetDS("select top 3 * from tejSap.dbo.getSalesFunnelDataView " + Queryfilter + " order by fid desc").Tables[0];
        Grid1.DataSource = (DataTable)Session["TblGrid1"];
        Grid1.DataBind();
        lblRowCnt.Text = "Rows : " + Grid1.Rows.Count.ToString();
    }

    protected void btnNewDeal_Click(object sender, EventArgs e)
    {
        chkQT.Checked = false;
        chkReseller.Checked = false;
        chkCountry.Checked = false;
        chkBU.Checked = false;
        chkDealStatus.Checked = false;
        chkYear.Checked = false;
        chkMonth.Checked = false;
        CheckAll.Checked = true;
        
        BindGridforNew();



        setAddBtnEnableStatus(false);
        DataTable tbl = (DataTable)Session["TblGrid1"];
        DataRow drw = tbl.NewRow();
        tbl.Rows.Add(drw);
        Session["TblGrid1"] = tbl;
        Grid1.DataSource = (DataTable)Session["TblGrid1"];
        Grid1.DataBind();
        PanelFltr.Enabled = false;
    }
    private void setAddBtnEnableStatus(Boolean flg)
    {
        if (flg)
        {
            btnNewDeal.Enabled = true;
            btnNewDeal.ForeColor = System.Drawing.Color.Blue;
        }
        else
        {
            btnNewDeal.Enabled = false;
            btnNewDeal.ForeColor = System.Drawing.Color.Silver;
        }
    }
    protected void Grid1_Sorting(object sender, GridViewSortEventArgs e)
    {
        String sortExp = e.SortExpression.ToString();
        String sortDir = e.SortDirection.ToString();

        String sortExpV = (String)ViewState["sortExpression"];
        String sortDirV = (String)ViewState["sortDirection"];

        if (sortExpV != null && sortExp == sortExpV)
        {
            if (sortDirV == "Asc")
                ViewState["sortDirection"] = "Desc";
            else
                ViewState["sortDirection"] = "Asc";
        }
        else
        {
            ViewState["sortExpression"] = sortExp;
            ViewState["sortDirection"] = "Asc";
        }

        BindGrid();
    }

    protected void Grid1_PageIndexChanging(object sender, GridViewPageEventArgs e)
    {
        Grid1.PageIndex = e.NewPageIndex;
        BindGrid();
        setAddBtnEnableStatus(true);
    }

    protected void Grid1_RowEditing(object sender, GridViewEditEventArgs e)
    {

        lblprevCntryDbCode.Text = ""; //just clear the old value stored for filter case

        Label tmplblFid = (Label)Grid1.Rows[e.NewEditIndex].FindControl("lblFid") as Label;
        Label tmplblBDM = (Label)Grid1.Rows[e.NewEditIndex].FindControl("lblBDM") as Label;
        Label tmplblquoteID = (Label)Grid1.Rows[e.NewEditIndex].FindControl("lblquoteID") as Label;
        Label tmplblendUser = (Label)Grid1.Rows[e.NewEditIndex].FindControl("lblendUser") as Label;
       
        Label tmplblresellerCode = (Label)Grid1.Rows[e.NewEditIndex].FindControl("lblresellerCode") as Label;
        Label tmplblresellerName = (Label)Grid1.Rows[e.NewEditIndex].FindControl("lblresellerName") as Label;

        Label tmplblcountry = (Label)Grid1.Rows[e.NewEditIndex].FindControl("lblcountry") as Label;
        Label tmplblBU = (Label)Grid1.Rows[e.NewEditIndex].FindControl("lblBU") as Label;
        Label tmplblgoodsDescr = (Label)Grid1.Rows[e.NewEditIndex].FindControl("lblgoodsDescr") as Label;
        Label tmplblquoteDate = (Label)Grid1.Rows[e.NewEditIndex].FindControl("lblquoteDate") as Label;
        Label tmplblquoteMonthMMM = (Label)Grid1.Rows[e.NewEditIndex].FindControl("lblquoteMonthMMM") as Label;
        Label tmplblquoteYear = (Label)Grid1.Rows[e.NewEditIndex].FindControl("lblquoteYear") as Label;
        Label tmplblvalue = (Label)Grid1.Rows[e.NewEditIndex].FindControl("lblvalue") as Label;
        Label tmplblexpClosingMonthMMM = (Label)Grid1.Rows[e.NewEditIndex].FindControl("lblexpClosingMonthMMM") as Label;
        Label tmplblexpClosingYear = (Label)Grid1.Rows[e.NewEditIndex].FindControl("lblexpClosingYear") as Label;
        Label tmplblremarks = (Label)Grid1.Rows[e.NewEditIndex].FindControl("lblremarks") as Label;
        Label tmplbldealStatus = (Label)Grid1.Rows[e.NewEditIndex].FindControl("lbldealStatus") as Label;
        Label tmplbluserLastUpdateDate = (Label)Grid1.Rows[e.NewEditIndex].FindControl("lbluserLastUpdateDate") as Label;
        Label tmplblorderBookedDate = (Label)Grid1.Rows[e.NewEditIndex].FindControl("lblorderBookedDate") as Label;
        //Label tmplblrddSalesPerson = (Label)Grid1.Rows[e.NewEditIndex].FindControl("lblrddSalesPerson") as Label;

        Label tmplblCost = (Label)Grid1.Rows[e.NewEditIndex].FindControl("lblCost") as Label;
        Label tmplblLanded = (Label)Grid1.Rows[e.NewEditIndex].FindControl("lblLanded") as Label;

        Grid1.EditIndex = e.NewEditIndex;
        BindGrid();
        GridViewRow editingRow = Grid1.Rows[e.NewEditIndex];

        Label tmplblFidEdt = (editingRow.FindControl("lblFidEdt") as Label);
        TextBox tmptxtBDM = (editingRow.FindControl("txtBDM") as TextBox);
        TextBox tmptxtquoteID = (editingRow.FindControl("txtquoteID") as TextBox);
        TextBox tmptxtendUser = (editingRow.FindControl("txtendUser") as TextBox);
        TextBox tmptxtgoodsDescr = (editingRow.FindControl("txtgoodsDescr") as TextBox);
        TextBox tmptxtquoteDate = (editingRow.FindControl("txtquoteDate") as TextBox);

        TextBox tmptxtquoteMonthMMM = (editingRow.FindControl("txtquoteMonthMMM") as TextBox);
        TextBox tmptxtquoteYear = (editingRow.FindControl("txtquoteYear") as TextBox);

        TextBox tmptxtvalue = (editingRow.FindControl("txtvalue") as TextBox);
        TextBox tmptxtremarks = (editingRow.FindControl("txtremarks") as TextBox);
        TextBox tmptxtuserLastUpdateDate = (editingRow.FindControl("txtuserLastUpdateDate") as TextBox);
        TextBox tmptxtorderBookedDate = (editingRow.FindControl("txtorderBookedDate") as TextBox);
        //TextBox tmptxtrddSalesPerson = (editingRow.FindControl("txtrddSalesPerson") as TextBox);

        TextBox tmptxtCost = (editingRow.FindControl("txtCost") as TextBox);
        TextBox tmptxtLanded = (editingRow.FindControl("txtLanded") as TextBox);

        DropDownList tmpddlreseller = (editingRow.FindControl("ddlreseller") as DropDownList);
        ListBox tmplstResellerForQt = (editingRow.FindControl("lstResellerForQt") as ListBox);


        DropDownList tmpddlcountry = (editingRow.FindControl("ddlcountry") as DropDownList);
        DropDownList tmpddlBU = (editingRow.FindControl("ddlBU") as DropDownList);
        DropDownList tmpddlexpClosingMonthMMM = (editingRow.FindControl("ddlexpClosingMonthMMM") as DropDownList);
        DropDownList tmpddlexpClosingYear = (editingRow.FindControl("ddlexpClosingYear") as DropDownList);
        DropDownList tmpddldealStatusEdt = (editingRow.FindControl("ddldealStatusEdt") as DropDownList);

        try
        {
            if (Session["pregions"] != null)
            {
                loadCountries(tmpddlcountry, Session["pregions"].ToString());
            }
        
            loadBUs(tmpddlBU);
            loadMonths(tmpddlexpClosingMonthMMM);
            loadYrs(tmpddlexpClosingYear);
            loadDealStatus(tmpddldealStatusEdt);


            tmplblFidEdt.Text = tmplblFid.Text;
            tmptxtBDM.Text = tmplblBDM.Text;
            tmptxtquoteID.Text = tmplblquoteID.Text;
            tmptxtendUser.Text = tmplblendUser.Text;
            tmptxtgoodsDescr.Text = tmplblgoodsDescr.Text;

            if (tmplblquoteDate.Text.Trim() == "")
            {
                tmptxtquoteDate.Text = DateTime.Now.Date.ToString("MM-dd-yyyy");
                tmptxtquoteMonthMMM.Text = DateTime.Now.Date.ToString("MMM");
                tmptxtquoteYear.Text = DateTime.Now.Date.ToString("yyyy");
            }
            else
            {
                tmptxtquoteDate.Text = tmplblquoteDate.Text;
                tmptxtquoteMonthMMM.Text = tmplblquoteMonthMMM.Text;
                tmptxtquoteYear.Text = tmplblquoteYear.Text;
            }

            tmptxtvalue.Text=tmplblvalue.Text;
            tmptxtCost.Text = tmplblCost.Text;
            tmptxtLanded.Text = tmplblLanded.Text;
            tmptxtremarks.Text = tmplblremarks.Text;


            if (tmplbluserLastUpdateDate.Text.Trim() == "")
                tmptxtuserLastUpdateDate.Text = ""; //DateTime.Now.Date.ToString("MM-dd-yyyy");
            else
                tmptxtuserLastUpdateDate.Text = tmplbluserLastUpdateDate.Text;
        
            //if (tmplblorderBookedDate.Text.Trim() == "")
            //    tmptxtorderBookedDate.Text = DateTime.Now.Date.ToString("MM-dd-yyyy");
            //else

            if (tmplblorderBookedDate.Text.IndexOf("1999") >= 0)
            {
                tmptxtorderBookedDate.Text = "";
            }
            else
            {
                tmptxtorderBookedDate.Text = tmplblorderBookedDate.Text;
            }
        
        
          //  tmptxtrddSalesPerson.Text = tmplblrddSalesPerson.Text;

            if (tmpddlcountry.SelectedIndex >= 0)
            {
                tmpddlcountry.SelectedIndex = -1;

                if (tmplblcountry.Text != "")
                    tmpddlcountry.Items.FindByText(tmplblcountry.Text.ToUpper()).Selected = true;
            }

            //reseller is based on country selected , so this comes later

            if (tmpddlcountry.SelectedIndex >= 0)
                loadReseller(tmpddlreseller, splitdbCodeForCountry(tmpddlcountry.SelectedValue.ToString()));

            ////if (tmpddlreseller.SelectedIndex >= 0)
            ////{
            ////    tmpddlreseller.SelectedIndex = -1;

            ////    if (tmplblresellerCode.Text != "")
            ////        tmpddlreseller.Items.FindByValue("[" + splitdbCodeForCountry(tmpddlcountry.SelectedValue.ToString()) + "] " + tmplblresellerCode.Text).Selected = true;
            ////}

            //////////////add to list items////////////////

       try
       {
            string[] cCodes, cNames;

            if (tmplblresellerCode.Text.Trim() != "")
            {
                cCodes = tmplblresellerCode.Text.Split(';');

                cNames = tmplblresellerName.Text.Split(';');
            
           
                 ListItem lst;

                for (int zxi = 0; zxi < cCodes.Length ; zxi++)
                {
                    lst = new ListItem(cNames[zxi] + "[" + cCodes[zxi] + "]", cCodes[zxi]);  // both suppose to have same no. of customers.
                   tmplstResellerForQt.Items.Add(lst);
                }

                if (tmplstResellerForQt.Items.Count > 0)
                    tmpddlcountry.Enabled = false;
                else
                    tmpddlcountry.Enabled = true;
            }
        }
        catch (Exception exp)
        {
            lblMsg.Text = "Exception Error Mutiple customer codes/Names seems to have mismatch in funnel data , " + exp.Message;
        }
            ////////////////////////



            if (tmpddlBU.SelectedIndex >= 0)
            {
                tmpddlBU.SelectedIndex = -1;

                if (tmplblBU.Text != "")
                    tmpddlBU.Items.FindByText(tmplblBU.Text.ToUpper()).Selected = true;
            }

            if (tmpddldealStatusEdt.SelectedIndex >= 0)
            {
                tmpddldealStatusEdt.SelectedIndex = -1;

                if (tmplbldealStatus.Text != "")
                    tmpddldealStatusEdt.Items.FindByText(tmplbldealStatus.Text.ToUpper()).Selected = true;
            }


            if (tmpddlexpClosingMonthMMM.SelectedIndex >= 0)
            {
                tmpddlexpClosingMonthMMM.SelectedIndex = -1;

                if (tmplblexpClosingMonthMMM.Text != "")
                    tmpddlexpClosingMonthMMM.Items.FindByText(tmplblexpClosingMonthMMM.Text.ToUpper()).Selected = true;
            }

            if (tmpddlexpClosingYear.SelectedIndex >= 0)
            {
                tmpddlexpClosingYear.SelectedIndex = -1;

                if (tmplblexpClosingYear.Text != "")
                    tmpddlexpClosingYear.Items.FindByText(tmplblexpClosingYear.Text.ToUpper()).Selected = true;
            }

            ddlFltrCountry.Enabled = false;

            setGridColsVisibleStatus(false);
            PanelFltr.Enabled = false;
        }
        catch (Exception exp)
        {
            lblMsg.Text = "Exception Error : " + exp.Message;
            PanelFltr.Enabled = true;
        }
    }
    private void setGridColsVisibleStatus(Boolean pFlag)
    {
        Grid1.Columns[10].Visible = pFlag;
        Grid1.Columns[11].Visible = pFlag;
        Grid1.Columns[15].Visible = pFlag;
        Grid1.Columns[16].Visible = pFlag;
        Grid1.Columns[17].Visible = pFlag;
    }
    protected void Grid1_RowCancelingEdit(object sender, GridViewCancelEditEventArgs e)
    {
        Grid1.EditIndex = -1;
        BindGrid();
        setGridColsVisibleStatus(true);
        setAddBtnEnableStatus(true);
        ddlFltrCountry.Enabled = true;
        lblprevCntryDbCode.Text = ""; //just clear the old value stored for filter case
        PanelFltr.Enabled = true;
    }
    protected void Grid1_RowDataBound(object sender, GridViewRowEventArgs e)
    {
        if (e.Row.RowType == DataControlRowType.DataRow)
        {
            //GridViewRow myRow = Grid1.Rows[e.Row.RowIndex];

            //if (myRow != null)
            //{
            if (Grid1.EditIndex < 0)
            {
                Label tmplbluserLastUpdateDate = (Label)e.Row.FindControl("lbluserLastUpdateDate") as Label;
                Label tmplblorderBookedDate = (Label)e.Row.FindControl("lblorderBookedDate") as Label;

                if (tmplbluserLastUpdateDate.Text.IndexOf("1999") >= 0)
                    tmplbluserLastUpdateDate.Text = "";

                if (tmplblorderBookedDate.Text.IndexOf("1999") >= 0)
                    tmplblorderBookedDate.Text = "";
            }
           // }
        }
    }
    
    protected void Grid1_RowUpdating(object sender, GridViewUpdateEventArgs e)
    {
        GridViewRow myRow = Grid1.Rows[e.RowIndex];
        
        if (myRow != null)
        {
            String sqlQry;
            lblMsg.Text = "";
            ///////////////
            Label tmplblFidEdt = ((Label)myRow.FindControl("lblFidEdt") as Label);
            TextBox tmptxtBDM = (myRow.FindControl("txtBDM") as TextBox);
            TextBox tmptxtquoteID = (myRow.FindControl("txtquoteID") as TextBox);
            TextBox tmptxtendUser = (myRow.FindControl("txtendUser") as TextBox);
            TextBox tmptxtgoodsDescr = (myRow.FindControl("txtgoodsDescr") as TextBox);
            
            TextBox tmptxtquoteDate = (myRow.FindControl("txtquoteDate") as TextBox);
            TextBox tmptxtvalue = (myRow.FindControl("txtvalue") as TextBox);

            TextBox tmptxtCost = (myRow.FindControl("txtCost") as TextBox);
            TextBox tmptxtLanded = (myRow.FindControl("txtLanded") as TextBox);
            
            TextBox tmptxtremarks = (myRow.FindControl("txtremarks") as TextBox);
            
            TextBox tmptxtuserLastUpdateDate = (myRow.FindControl("txtuserLastUpdateDate") as TextBox);
            TextBox tmptxtorderBookedDate = (myRow.FindControl("txtorderBookedDate") as TextBox);
            
            //TextBox tmptxtrddSalesPerson = (myRow.FindControl("txtrddSalesPerson") as TextBox);

            DropDownList tmpddlreseller = (myRow.FindControl("ddlreseller") as DropDownList);
            ListBox tmplstResellerForQt = (myRow.FindControl("lstResellerForQt") as ListBox);

            DropDownList tmpddlcountry = (myRow.FindControl("ddlcountry") as DropDownList);
            DropDownList tmpddlBU = (myRow.FindControl("ddlBU") as DropDownList);
            DropDownList tmpddlexpClosingMonthMMM = (myRow.FindControl("ddlexpClosingMonthMMM") as DropDownList);
            DropDownList tmpddlexpClosingYear = (myRow.FindControl("ddlexpClosingYear") as DropDownList);
            DropDownList tmpddldealStatusEdt = (myRow.FindControl("ddldealStatusEdt") as DropDownList);

            //string rCode =splitResellerCode(tmpddlreseller.SelectedValue.ToString());


            string rCodes = "", rNames = "";

            if (tmplstResellerForQt.Items.Count < 1)
            {
                lblMsg.Text = "Error ! No Reseller is selected for current Quote, at least one reseller is must for a quote to be validated.";
                return;
            }
            else
            {
                for (int zxi = 0; zxi < tmplstResellerForQt.Items.Count; zxi++)
                {
                    if (rCodes == "")
                        rCodes = tmplstResellerForQt.Items[zxi].Value.ToString();
                    else
                        rCodes =rCodes + ";" + tmplstResellerForQt.Items[zxi].Value.ToString();

                    
                    if (rNames == "")
                        rNames = splitResellerName(tmplstResellerForQt.Items[zxi].Text);
                    else
                        rNames = rNames + ";" + splitResellerName(tmplstResellerForQt.Items[zxi].Text);

                }
            }

            if (rCodes.Trim() == "" || rNames.Trim()=="")
            {
                lblMsg.Text = "Error ! No Reseller is selected for current Quote, at least one reseller is must for a quote to be validated.";
                return;
            }

            ///////////////
            if (tmptxtBDM.Text.Trim() == "" || tmptxtBDM.Text.Trim().IndexOf("'") >= 0)
            {
                lblMsg.Text = "Error ! Either the field BDM is empty or contains a Invalid character ' , please supply a valid value";
                return;
            }
            if (tmptxtquoteID.Text.Trim() == "" || tmptxtquoteID.Text.Trim().IndexOf("'") >= 0)
            {
                lblMsg.Text = "Error ! Either the field Quote ID is empty or contains a Invalid character ' , please supply a valid value";
                return;
            }
            if (tmptxtendUser.Text.Trim() == "" || tmptxtendUser.Text.Trim().IndexOf("'") >= 0)
            {
                lblMsg.Text = "Error ! Either the field END USER is empty or contains a Invalid character ' , please supply a valid value";
                return;
            }
            if (tmptxtgoodsDescr.Text.Trim() == "" || tmptxtgoodsDescr.Text.Trim().IndexOf("'") >= 0)
            {
                lblMsg.Text = "Error ! Either the field Goods Desc is empty or contains a Invalid character ' , please supply a valid value";
                return;
            }
            //if (tmptxtremarks.Text.Trim() == "" || tmptxtremarks.Text.Trim().IndexOf("'") >= 0)
            if (tmptxtremarks.Text.Trim().IndexOf("'") >= 0)
            {
                lblMsg.Text = "Error ! Either the field REMARKS is empty or contains a Invalid character ' , please supply a valid value";
                return;
            }
            //if (tmptxtrddSalesPerson.Text.Trim() == "" || tmptxtrddSalesPerson.Text.Trim().IndexOf("'") >= 0)
            //{
            //    lblMsg.Text = "Error ! Either the field RDD Sales Person is empty or contains a Invalid character ' , please supply a valid value";
            //    return;
            //}

            if (tmptxtCost.Text.Trim() == "" || Util.isValidDecimalNumber(tmptxtCost.Text) == false)
            {
                lblMsg.Text = "Error ! Either the field $ Cost is empty or contains a Invalid Numeric value, please supply a valid numeric value";
                return;
            }

            if (tmptxtLanded.Text.Trim() == "" || Util.isValidDecimalNumber(tmptxtLanded.Text) == false)
            {
                lblMsg.Text = "Error ! Either the field $ Landed is empty or contains a Invalid Numeric value, please supply a valid numeric value";
                return;
            }

            if (tmptxtvalue.Text.Trim() == "" || Util.isValidDecimalNumber(tmptxtvalue.Text) == false)
            {
                lblMsg.Text = "Error ! Either the field $Total Sale is empty or contains a Invalid Numeric value, please supply a valid numeric value";
                return;
            }
            //////////////// tmptxtLanded

            if ((Convert.ToDouble(tmptxtLanded.Text.Trim()) < Convert.ToDouble(tmptxtCost.Text.Trim())))
            {
                lblMsg.Text = "Error ! Landed must be greater than or equal to cost";
                return;
            }

            if (Convert.ToDouble(tmptxtvalue.Text.Trim()) < (Convert.ToDouble(tmptxtCost.Text.Trim())))
            {
                lblMsg.Text = "Error ! Total Sales must be greater then or qual to cost";
                return;
            }

            if (tmptxtquoteDate.Text.Trim() == "" || Util.IsValidDate(tmptxtquoteDate.Text) == false)
            {
                lblMsg.Text = "Error ! Either the field QUOTE DATE is empty or contains a Invalid Date, please supply a valid value in format MM-DD-YYYY";
                return;
            }

            string ordDt,lstUsrUpd;

            //if (tmptxtuserLastUpdateDate.Text.Trim() == "" || Util.IsValidDate(tmptxtuserLastUpdateDate.Text) == false)
            //{
            //    lblMsg.Text = "Error ! Either the field LAST UPDATE is empty or contains a Invalid Date, please supply a valid value in format MM-DD-YYYY";
            //    return;
            //}

            if (tmptxtuserLastUpdateDate.Text.Trim() != "")
            {
                if (Util.IsValidDate(tmptxtuserLastUpdateDate.Text) == false)
                {
                    lblMsg.Text = "Error ! Either the field LAST UPDATE is empty or contains a Invalid Date, please supply a valid value in format MM-DD-YYYY";
                    return;
                }
                else
                    lstUsrUpd = tmptxtuserLastUpdateDate.Text;
            }
            else
            {
                lstUsrUpd = "01-01-1999";
            }
            
            if (tmptxtorderBookedDate.Text.Trim() != "")
            {
                if (Util.IsValidDate(tmptxtorderBookedDate.Text) == false)
                {
                    lblMsg.Text = "Error ! Either the field ORDER BOOKED DATE has Invalid Date, please supply a valid date in format MM-DD-YYYY or leave the field empty";
                    return;
                }
                else
                    ordDt = tmptxtorderBookedDate.Text;
            }
            else
            {
                ordDt = "01-01-1999";
            }


            string qMnthNo, qMMM, qYR;
            DateTime dt;
            dt = DateTime.Parse(tmptxtquoteDate.Text.Trim());
            qMnthNo = dt.ToString("MM");
            qMMM = dt.ToString("MMM");
            qYR = dt.ToString("yyy");


            if (btnNewDeal.Enabled == false)
            {
                sqlQry = string.Format("insert into tejSap.[dbo].[tejFunnel2017](bdm,quoteID,endUser,resellerCode,resellerName,country,BU,goodsDescr,quoteDate,quoteMonth,quoteMonthMMM,quoteYear,value"
                + ",expClosingMonth,expClosingMonthMMM,expClosingYear,remarks,dealStatus,userLastUpdateDate,orderBookedDate,rddSalesPerson,CreatedOn,CreatedBy,LastUpdatedOn,LastUpdatedBy,Cost,Landed) "
                + " values('{0}','{1}','{2}','{3}','{4}','{5}','{6}','{7}','{8}',{9},'{10}',{11},{12},{13},'{14}',{15},'{16}','{17}','{18}','{19}','{20}','{21}','{22}','{23}','{24}','{25}','{26}')"
                    , tmptxtBDM.Text.Trim(), tmptxtquoteID.Text.Trim(), tmptxtendUser.Text.Trim(), rCodes, rNames, tmpddlcountry.SelectedItem.Text, tmpddlBU.SelectedValue.ToString()
                    , tmptxtgoodsDescr.Text.Trim(), tmptxtquoteDate.Text.Trim(), qMnthNo, qMMM, qYR
                    , tmptxtvalue.Text.Trim(), tmpddlexpClosingMonthMMM.SelectedValue.ToString(), tmpddlexpClosingMonthMMM.SelectedItem.Text, tmpddlexpClosingYear.SelectedValue.ToString()
                    , tmptxtremarks.Text.Trim(), tmpddldealStatusEdt.SelectedValue.ToString(), lstUsrUpd, ordDt, "NA"
                    , DateTime.Now.ToString("yyyy-MM-dd hh:mm:ss tt"), myGlobal.loggedInUser(), DateTime.Now.ToString("yyyy-MM-dd hh:mm:ss tt"), myGlobal.loggedInUser(), tmptxtCost.Text.Trim(), tmptxtLanded.Text.Trim());
                Db.constr = myGlobal.getAppSettingsDataForKey("tejSap");
                Db.myExecuteSQL(sqlQry);
            }
            else
            {
                sqlQry = string.Format("update tejSap.[dbo].[tejFunnel2017] set bdm='{0}',quoteID='{1}',endUser='{2}',resellerCode='{3}',resellerName='{4}',country='{5}',BU='{6}',goodsDescr='{7}'"
                    +",quoteDate='{8}',quoteMonth={9},quoteMonthMMM='{10}',quoteYear={11},value={12},expClosingMonth={13},expClosingMonthMMM='{14}',expClosingYear={15},remarks='{16}'"
                    +",dealStatus='{17}',userLastUpdateDate='{18}',orderBookedDate='{19}',rddSalesPerson='{20}',LastUpdatedOn='{21}',LastUpdatedBy='{22}',Cost='{23}', Landed='{24}'  where fid={25}"
                    , tmptxtBDM.Text.Trim(), tmptxtquoteID.Text.Trim(), tmptxtendUser.Text.Trim(), rCodes, rNames, tmpddlcountry.SelectedItem.Text, tmpddlBU.SelectedValue.ToString()
                    , tmptxtgoodsDescr.Text.Trim(), tmptxtquoteDate.Text.Trim(), qMnthNo, qMMM, qYR
                    , tmptxtvalue.Text.Trim(), tmpddlexpClosingMonthMMM.SelectedValue.ToString(), tmpddlexpClosingMonthMMM.SelectedItem.Text, tmpddlexpClosingYear.SelectedValue.ToString()
                    , tmptxtremarks.Text.Trim(), tmpddldealStatusEdt.SelectedValue.ToString(), lstUsrUpd, ordDt, "NA"
                    , DateTime.Now.ToString("yyyy-MM-dd hh:mm:ss tt"), myGlobal.loggedInUser(), tmptxtCost.Text.Trim(), tmptxtLanded.Text.Trim(), tmplblFidEdt.Text.Trim() );

                Db.constr = myGlobal.getAppSettingsDataForKey("tejSap");
                Db.myExecuteSQL(sqlQry);
            }

            Grid1.EditIndex = -1;
            btnNewDeal.Enabled = true; //make it true in add/edit case
            BindGrid();
            lblMsg.Text = "Funnel Deal record has been updated successfully  @" + DateTime.Now.ToString("MM-dd-yyyy hh:mm") ;
            setAddBtnEnableStatus(true);
           
            setGridColsVisibleStatus(true);
            PanelFltr.Enabled = true;
            loadQT(ddlFltrQT);
        }

        lblprevCntryDbCode.Text = ""; //just clear the old value stored for filter case
    }
    
    protected void Grid1_RowDeleting(object sender, GridViewDeleteEventArgs e)
    {
        string mailSts = "";

        if (btnNewDeal.Enabled == false)
        {
            btnNewDeal.Enabled = true;
        }
        else
        {
            GridViewRow DeleteRow = Grid1.Rows[e.RowIndex];

            if (DeleteRow != null)
            {
                Label lblFid = new Label();
                lblFid = (Label)DeleteRow.Cells[0].FindControl("lblFid");


                Label tmplblBDM = (Label)Grid1.Rows[e.RowIndex].FindControl("lblBDM") as Label;
                Label tmplblquoteID = (Label)Grid1.Rows[e.RowIndex].FindControl("lblquoteID") as Label;
                Label tmplblquoteDate = (Label)Grid1.Rows[e.RowIndex].FindControl("lblquoteDate") as Label;
                Label tmplblcountry = (Label)Grid1.Rows[e.RowIndex].FindControl("lblcountry") as Label;
                Label tmplblresellerName = (Label)Grid1.Rows[e.RowIndex].FindControl("lblresellerName") as Label;
                
                Label tmplblBU = (Label)Grid1.Rows[e.RowIndex].FindControl("lblBU") as Label;
                Label tmplblgoodsDescr = (Label)Grid1.Rows[e.RowIndex].FindControl("lblgoodsDescr") as Label;
                
                Label tmplblvalue = (Label)Grid1.Rows[e.RowIndex].FindControl("lblvalue") as Label;
                Label tmplblremarks = (Label)Grid1.Rows[e.RowIndex].FindControl("lblremarks") as Label;
                Label tmplbldealStatus = (Label)Grid1.Rows[e.RowIndex].FindControl("lbldealStatus") as Label;

                if (lblFid.Text != "" && lblFid != null) //in case of empty row 
                {
                    Db.constr = myGlobal.getAppSettingsDataForKey("tejSap");
                    String sqlQry = "update tejSap.[dbo].[tejFunnel2017] set isRowActive=0 where fid=" + lblFid.Text;
                    Db.myExecuteSQL(sqlQry);
                    loadQT(ddlFltrQT);

                    if (myGlobal.getAppSettingsDataForKey("SendFunnelDeletedMailIntimationOrNot").ToUpper() == "YES")
                    {
                        mailSts = sendMailOnDelete(tmplblBDM.Text.Trim(), tmplblquoteID.Text.Trim(), tmplblquoteDate.Text.Trim(), tmplblcountry.Text.Trim(), tmplblresellerName.Text.Trim()
                             , tmplblBU.Text.Trim(), tmplblgoodsDescr.Text.Trim(), tmplblvalue.Text.Trim(), tmplblremarks.Text.Trim(), tmplbldealStatus.Text.Trim(), myGlobal.getAppSettingsDataForKey("FunnelDeletedMailIntimationTo"));
                    
                    }
                    lblMsg.Text = "Record has been deleted/deactivated successfully , " + mailSts;
                }
            }
        }
        BindGrid();
        PanelFltr.Enabled = true;
        setAddBtnEnableStatus(true);
    }
    
    private string sendMailOnDelete(string pbdm,string pQtId,string pQtDate,string pCntry,string pReseller,string pBU,string pGoodDesc,string pVal,string pRem,string pSts,string pToMailID)
    {
        string strMsgCan, msg = "", pret = "", htmlString;


        strMsgCan = "<html><head><title>" + "Funnel Deal Deleted" + "</title></head><body><div>";
       
        strMsgCan += "</b><b>Quote has been Deleted By : <b>" + myGlobal.loggedInUser() + "</b><br/><br/>";
        strMsgCan += "Quote ID : <b>" + pQtId + "</b><br/>";
        strMsgCan += "Quote Date : <b>" + pQtDate + "</b><br/>";
        
        strMsgCan += "For Value (USD) : <b>" + pVal + "</b><br/>";

        strMsgCan += "For Country : <b>" + pCntry + "</b><br/>";
        strMsgCan += "For Releseller  : <b>" + pReseller + "</b><br/>";

        strMsgCan += "BU : <b>" + pBU + "</b><br/>";
        strMsgCan += "Goods Desc : <b>" + pGoodDesc + "</b><br/>";
        
        strMsgCan += "Latest Remarks : <b>" + pRem   + "</b><br/>";
        strMsgCan += "Deal Status Was: <b>" + pSts + "</b><br/>";

        
        strMsgCan += "</div></body></html>";

        pret = Mail.SendMultipleAttach(myGlobal.getSystemConfigValue("websiteEmailer"), pToMailID, "", "Funnel Deal Deleted", strMsgCan, true, "", "");
        return pret;
    }
    protected void CheckAll_CheckedChanged(object sender, EventArgs e)
    {
        //btnNewDeal.Enabled = true;

        if (CheckAll.Checked)
        {
            chkQT.Checked = false;
            chkReseller.Checked = false;
            chkCountry.Checked = false;
            chkBU.Checked = false;
            chkDealStatus.Checked = false;
            chkYear.Checked = false;
            chkMonth.Checked = false;
        }

        BindGrid();
    }

protected void  chkQT_CheckedChanged(object sender, EventArgs e)
{
    if (chkQT.Checked == true)
    {
        CheckAll.Checked = false;
        BindGrid();
    }
}
protected void  chkReseller_CheckedChanged(object sender, EventArgs e)
{
    if (chkReseller.Checked == true)
    {
        CheckAll.Checked = false;
        BindGrid();
    }
}
protected void  chkCountry_CheckedChanged(object sender, EventArgs e)
{
    if (chkCountry.Checked == true)
    {
        CheckAll.Checked = false;
        BindGrid();
    }
}
protected void  chkBU_CheckedChanged(object sender, EventArgs e)
{
   if (chkBU.Checked == true)
     CheckAll.Checked = false;

    BindGrid();
}
protected void  chkDealStatus_CheckedChanged(object sender, EventArgs e)
{
    if (chkDealStatus.Checked == true)
    {
        CheckAll.Checked = false;
        BindGrid();
    }
}
protected void  chkYear_CheckedChanged(object sender, EventArgs e)
{
    if (chkYear.Checked == true)
    {
        CheckAll.Checked = false;
        BindGrid();
    }
}
protected void  chkMonth_CheckedChanged(object sender, EventArgs e)
{
    if (chkMonth.Checked == true)
    {
        CheckAll.Checked = false;
        BindGrid();
    }
}

//protected void ddlhasExpenses_SelectedIndexChanged(object sender, EventArgs e)
//{
//    DropDownList ddl = sender as DropDownList;
//    GridViewRow grdrw = (GridViewRow)((DataControlFieldCell)((DropDownList)sender).Parent).Parent;
//    Control ctrl;
//    Button btn;
//    ctrl = grdrw.FindControl("ddlhasExpenses") as DropDownList;
//    btn = grdrw.FindControl("btnAddField") as Button;
//    if (ctrl != null)
//    {
//        string tmpqry = "";
//        DropDownList ddl1 = (DropDownList)ctrl;
//        if (ddl.ClientID == ddl1.ClientID)
//        {
//            CheckBox chkView = grdrw.FindControl("chkView") as CheckBox;
//            Label tmplblsno = (Label)grdrw.FindControl("lblsno") as Label;
//            if (ddl.SelectedItem.Text.ToUpper() == "NO")
//            {
//                btn.Enabled = false;
//            }
//            else
//            {
//                Label lblActivityCode = grdrw.FindControl("lblActivityCode") as Label;
//                if (lblSelectedActivity.Text.IndexOf(lblActivityCode.Text) >= 0)
//                    btn.Enabled = true;
//            }

//            //tmpqry = string.Format("update TblActivities set hasExpenses='{0}' where sno=" + tmplblsno.Text, ddl.SelectedItem.Text);
//            tmpqry = string.Format("update TblActivities set hasExpenses='{0}',lastModified='{1}' where sno=" + tmplblsno.Text, ddl.SelectedItem.Text, DateTime.Now.ToString());

//            Db.constr = myGlobal.getRDDMarketingDBConnectionString();
//            Db.myExecuteSQL(tmpqry);

//            if (Session["lblsno"] != null)
//                BindGrid2(Session["lblsno"].ToString());
//            else
//                BindGrid2("0");
//        }
//    }
//}

protected void lnkBtnAddCustForQuote_Click(object sender, EventArgs e)
{
    GridViewRow grdrw = (GridViewRow)((DataControlFieldCell)((LinkButton)sender).Parent).Parent;

    DropDownList ddl1 = grdrw.FindControl("ddlreseller") as DropDownList;
    if (ddl1 != null)
    {
        ListBox lstcust = grdrw.FindControl("lstResellerForQt") as ListBox;

        ListItem lst;
        lst = new ListItem(ddl1.SelectedItem.Text, splitResellerCode(ddl1.SelectedValue.ToString()));  // both suppose to have same no. of customers.
        //open this for test
        //lst = new ListItem(ddl1.SelectedItem.Text + "[" + splitResellerCode(ddl1.SelectedValue.ToString()) + "]", splitResellerCode(ddl1.SelectedValue.ToString()));  // both suppose to have same no. of customers.

        if (lstcust.Items.Contains(lst))
            lblMsg.Text = "Warning ! Reseller already exists in the Reseller List for Current Quote.";
        else
        {
            lstcust.Items.Add(lst);
            lblMsg.Text = "Success ! Reseller Added to Reseller List for Current Quote.";
            //it means there is selected customer added to list for selected country now
            DropDownList ddlCntry = grdrw.FindControl("ddlcountry") as DropDownList;
            ddlCntry.Enabled = false;
        }
    }


    
}
protected void lnkBtnRemoveCustForQuote_Click(object sender, EventArgs e)
{
    GridViewRow grdrw = (GridViewRow)((DataControlFieldCell)((LinkButton)sender).Parent).Parent;

    ListBox lstcust = grdrw.FindControl("lstResellerForQt") as ListBox;
    if (lstcust != null)
    {
        if (lstcust.SelectedIndex >= 0)
        {
            lstcust.Items.RemoveAt(lstcust.SelectedIndex);
            lblMsg.Text = "Success ! Reseller removed from Reseller List for Current Quote.";

            if (lstcust.Items.Count <= 0)
            {
                //it means there is selected customer added to list for selected country now
                DropDownList ddlCntry = grdrw.FindControl("ddlcountry") as DropDownList;
                ddlCntry.Enabled = true;
            }
        }
        else
            lblMsg.Text = "Warning ! Select a Reseller to remove from Reseller List for Current Quote.";
    }
}

protected void ddlcountry_SelectedIndexChanged(object sender, EventArgs e)
{
    DropDownList ddlCntrySender = sender as DropDownList;
    GridViewRow grdrw = (GridViewRow)((DataControlFieldCell)((DropDownList)sender).Parent).Parent;
    Control ctrl;
    ctrl = grdrw.FindControl("ddlCountry") as DropDownList;
    if (ctrl != null)
    {
        DropDownList ddl1 = (DropDownList)ctrl;
        if (ddlCntrySender.ClientID == ddl1.ClientID)
        {
            DropDownList ddlcust = grdrw.FindControl("ddlreseller") as DropDownList;
            loadReseller(ddlcust, splitdbCodeForCountry(ddlCntrySender.SelectedValue.ToString()));
        }
    }
}
protected void ddlFltrCountry_SelectedIndexChanged(object sender, EventArgs e)
{
    chkReseller.Checked = false;
    loadReseller(ddlFltrReseller, splitdbCodeForCountry(ddlFltrCountry.SelectedValue.ToString()));

    BindGrid();
}
protected void ddlFltrReseller_SelectedIndexChanged(object sender, EventArgs e)
{

    BindGrid();
}
protected void ddlFltrQT_SelectedIndexChanged(object sender, EventArgs e)
{
    BindGrid();
}

protected void ddlFltrBU_SelectedIndexChanged(object sender, EventArgs e)
{
    BindGrid();
}
protected void ddlFltrDealStatus_SelectedIndexChanged(object sender, EventArgs e)
{
    BindGrid();
}
protected void ddlFltrYear_SelectedIndexChanged(object sender, EventArgs e)
{
    BindGrid();
}
protected void ddlFltrMonth_SelectedIndexChanged(object sender, EventArgs e)
{
    BindGrid();
}

protected void btnCancel_Click(object sender, EventArgs e)
{
    tblMain.Visible = true;
    PanelNewCreation.Visible = false;

    lblAddNewMsg.Text = "";
    txtnewDesc.Text = "";
}
protected void btnSaveNew_Click(object sender, EventArgs e)
{
    lblAddNewMsg.Text = "";

    if (txtnewDesc.Text.Trim() == "" || txtnewDesc.Text.Trim().IndexOf("'") >= 0)
    {
        lblAddNewMsg.Text = "Error ! Either the field NEW RESELLER is empty or contains a Invalid character ' , please supply a valid value";
        return;
    }

    string sqlQryins = string.Format("insert into tblFunnelNewResellers(CardCode,CardName,CardType,dbCode,dbName,CreatedBy) values((select 'NewRCode' + convert(varchar(30),(isnull(MAX(cid),0)+1)) maxId from [tejSap].[dbo].[tblFunnelNewResellers]),'{0}','C',(select dbCode from [tejSap].[dbo].[rddCountriesList] where Country='{1}'),(select dbName from [tejSap].[dbo].[rddCountriesList] where Country='{1}'),'{2}')"
        , txtnewDesc.Text.Trim(), ddlCountryForNewReseller.SelectedItem.Text, myGlobal.loggedInUser());

    Db.constr = myGlobal.getAppSettingsDataForKey("tejSap");
    Db.myExecuteSQL(sqlQryins);

    lblAddNewMsg.Text = "New Reseller  '" + txtnewDesc.Text.Trim().ToUpper() + "'  for Funnel has been updated successfully  @" + DateTime.Now.ToString("MM-dd-yyyy hh:mm");

    txtnewDesc.Text = "";
}

protected void btnNewReseller_Click(object sender, EventArgs e)
{
    tblMain.Visible = false;
    PanelNewCreation.Visible = true;
    txtnewDesc.Text = "";
    
    

}

protected void btnRep_Click(object sender, EventArgs e)
{
    tblMain.Visible = false;
    PanelNewCreation.Visible = false;
    PnlReport.Visible = true;
    btnRep.Visible = false;
    txtnewDesc.Text = "";
    
    txtFromDate.Text = "";
    txtToDate.Text = "";
    GridView1.DataSource = null;
    GridView1.DataBind();
    lblCnt.Text = "0";

}


protected void btnExit_Click(object sender, EventArgs e)
{
    tblMain.Visible = true;
    PanelNewCreation.Visible = false;
    PnlReport.Visible = false;
    btnRep.Visible = true;
    lblAddNewMsg.Text = "";
    txtnewDesc.Text = "";
}

protected void btnViewData_Click(object sender, EventArgs e)
{
    lblUseQryNow.Text = "";
    lblFileName.Text = "";
    lblMsg.Text = "";
    lblCnt.Text = "0";
    GridView1.DataSource = null;

    if (checkDates()) //if both dates  are correct
    {

        lblUseQryNow.Text = "Exec tejSap.[dbo].[getSalesFunnelReport] '" + myGlobal.loggedInUser() + "','" + txtFromDate.Text + "','" + txtToDate.Text + "'";

        lblFileName.Text = "FunnelReport-" + txtFromDate.Text + "-" + txtToDate.Text;

        Db.constr = myGlobal.getAppSettingsDataForKey("tejSap");//myGlobal.ConnectionString;
        GridView1.DataSource = Db.myGetDS(lblUseQryNow.Text).Tables[0];
        GridView1.DataBind();

        lblCnt.Text = GridView1.Rows.Count.ToString();

        if (GridView1.Rows.Count > 0)
            btnReportExl.Enabled = true;
        else
        {
            btnReportExl.Enabled = false;
            lblMsg.Text = "Sorry ! no data available for this period, Try another period.";
        }


    }
}

protected void btnReportExl_Click(object sender, EventArgs e)
{
    Db.constr = myGlobal.getAppSettingsDataForKey("tejSap"); //myGlobal.ConnectionString;
    DataTable pdt = Db.myGetDS(lblUseQryNow.Text).Tables[0];
    ExportToExcel(pdt, lblFileName.Text);
}

void ExportToExcel(DataTable dt, string FileName)
{
    if (dt.Rows.Count > 0)
    {
        string filename = FileName + ".xls";
        System.IO.StringWriter tw = new System.IO.StringWriter();
        System.Web.UI.HtmlTextWriter hw = new System.Web.UI.HtmlTextWriter(tw);
        DataGrid dgGrid = new DataGrid();
        dgGrid.DataSource = dt;
        dgGrid.DataBind();

        //Get the HTML for the control.
        dgGrid.RenderControl(hw);
        //Write the HTML back to the browser.
        //Response.ContentType = application/vnd.ms-excel;
        Response.ContentType = "application/vnd.ms-excel";
        Response.AppendHeader("Content-Disposition", "attachment; filename=" + filename + "");
        this.EnableViewState = false;
        Response.Write(tw.ToString());
        Response.End();
    }
}

private Boolean checkDates()
{
    Boolean flg = true;

    if (flg == true && Util.IsValidDate(txtFromDate.Text.Trim()) == false)
    {
        lblMsg.Text = "Error ! Please enter a valid date in FROM-DATE filed and retry.";
        flg = false;
    }

    if (flg == true && Util.IsValidDate(txtToDate.Text.Trim()) == false)
    {
        lblMsg.Text = "Error ! Please enter a valid date in TO-DATE filed and retry.";
        flg = false;
    }

    if (flg == true && Convert.ToDateTime(txtToDate.Text.Trim()) < Convert.ToDateTime(txtFromDate.Text.Trim()))
    {
        lblMsg.Text = "Error ! TO-DATE can not be smaller than FROM-DATE.";
        flg = false;
    }

    return flg;
}


}
