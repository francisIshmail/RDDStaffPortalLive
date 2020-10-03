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

public partial class Intranet_Marketing_LoadFunnelDataSingleCustBasedQt : System.Web.UI.Page
{
    //Global gbl = new Global();
    string Queryfilter,prevCntryDbCode;

    protected void Page_Load(object sender, EventArgs e)
    {
        Response.CacheControl = "private";
        Response.Expires = 0;
        Response.AddHeader("pragma", "no-cache");

        if (!IsPostBack)
        {
            loadCountries(ddlCountryForNewReseller);// load this before Reseller
            
            loadQT(ddlFltrQT);
            loadCountries(ddlFltrCountry);// load this before Reseller
            
            if(ddlFltrCountry.SelectedIndex>=0)
             loadReseller(ddlFltrReseller,splitdbCodeForCountry(ddlFltrCountry.SelectedValue.ToString()));
            
            loadBUs(ddlFltrBU);
            loadYrs(ddlFltrYear);
            loadMonths(ddlFltrMonth);
            loadDealStatus(ddlFltrDealStatus);

            BindGrid();
        }
        lblMsg.Text = "";
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
    private void loadCountries(DropDownList ddl)
    {
        //Db.LoadDDLsWithCon(ddl, "select CountryCode,Country from [dbo].[rddCountriesList] order by Country", "Country", "Country", myGlobal.getAppSettingsDataForKey("tejSap"));
        Db.LoadDDLsWithCon(ddl, "select ('[' + dbCode + '] ' + CountryCode) as CountryOfDb,Country from [dbo].[rddCountriesList] order by Country", "Country", "CountryOfDb", myGlobal.getAppSettingsDataForKey("tejSap"));
        if (ddl.Items.Count > 0)
            ddl.SelectedIndex = 0;
        else
        {
            ddl.Items.Add("No Rows");
            ddl.SelectedIndex = 0;
        }
    }

    private void loadReseller(DropDownList ddl,String Cntrydbcode)
    {
        //Db.LoadDDLsWithCon(ddl, "select distinct(resellerCode),resellerName from [dbo].[tejFunnel2017] order by resellerName", "resellerName", "resellerCode", myGlobal.getAppSettingsDataForKey("tejSap"));

        if (Cntrydbcode != "" && lblprevCntryDbCode.Text != Cntrydbcode)  //only if country is selected
        {
            Db.LoadDDLsWithCon(ddl, "Exec [getFunnelCustomerList] '" + Cntrydbcode + "'", "resellerName", "resellerCode", myGlobal.getAppSettingsDataForKey("tejSap"));

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

    private void loadQT(DropDownList ddl)
    {
        Db.LoadDDLsWithCon(ddl, "select distinct(quoteID) from [dbo].[tejFunnel2017] where isRowActive=1 and createdBy='" + myGlobal.loggedInUser() + "' and resellerCode<>'XXX' order by quoteID", "quoteID", "quoteID", myGlobal.getAppSettingsDataForKey("tejSap"));
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
        Db.LoadDDLsWithCon(ddl, "select groupBU,BU from tejSap.dbo.Mapping_BUs where region='TRI' and PMForBU='" + myGlobal.loggedInUser() + "' order by BU", "BU", "BU", myGlobal.getAppSettingsDataForKey("tejSap"));
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

    private string generateFilterQuery()
    {
        Queryfilter=" Where CreatedBy='" + myGlobal.loggedInUser() + "'";  //base query

        if (CheckAll.Checked)
            Queryfilter = " Where CreatedBy='" + myGlobal.loggedInUser() + "'";
        else
        {
            if (chkQT.Checked && ddlFltrQT.SelectedValue.ToString() != "No Rows")
            {
                if (Queryfilter == "")
                {
                    Queryfilter =" Where ";
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
                    Queryfilter =" Where ";
                   Queryfilter = Queryfilter + "  resellerCode='" + ddlFltrReseller.SelectedValue.ToString() + "'";
                }
                else
                {
                    Queryfilter = Queryfilter + " and ";
                    Queryfilter = Queryfilter + " resellerCode='" + ddlFltrReseller.SelectedValue.ToString() + "'";
                }
            }

            if (chkCountry.Checked && ddlFltrCountry.SelectedValue.ToString() != "No Rows")
                //Queryfilter = Queryfilter + " and country='" + ddlFltrCountry.SelectedValue.ToString() + "'";
                {
                if (Queryfilter == "")
                {
                    Queryfilter =" Where ";
                   Queryfilter = Queryfilter + "  country='" + ddlFltrCountry.SelectedValue.ToString() + "'";
                }
                else
                {
                    Queryfilter = Queryfilter + " and ";
                    Queryfilter = Queryfilter + " country='" + ddlFltrCountry.SelectedValue.ToString() + "'";
                }
            }

            if (chkBU.Checked && ddlFltrBU.SelectedValue.ToString() != "No Rows")
                //Queryfilter = Queryfilter + " and BU='" + ddlFltrBU.SelectedValue.ToString() + "'";
                {
                if (Queryfilter == "")
                {
                    Queryfilter =" Where ";
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
                    Queryfilter =" Where ";
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
                    Queryfilter =" Where ";
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
            String summarySQL;
            String sortExp = (String)ViewState["sortExpression"];
            String sortDir = (String)ViewState["sortDirection"];

            if (sortExp == null || sortExp == "")
            {
                if (CheckAll.Checked)
                    summarySQL = "select * from tejSap.dbo.getSalesFunnelDataView " + generateFilterQuery()  + " order by fid desc";
                else
                    summarySQL = "select * from tejSap.dbo.getSalesFunnelDataView " + generateFilterQuery() + " order by fid desc";  //put the conditions
            }
            else
            {
                if (CheckAll.Checked)
                    summarySQL = "select * from tejSap.dbo.getSalesFunnelDataView " + generateFilterQuery() + " order by " + sortExp + " " + sortDir;
                else
                    summarySQL = "select * from tejSap.dbo.getSalesFunnelDataView " + generateFilterQuery() + " order by " + sortExp + " " + sortDir;
            }
            
            //lblMsg.Text = summarySQL;

            Db.constr = myGlobal.getAppSettingsDataForKey("tejSap");
            Session["TblGrid1"] = Db.myGetDS(summarySQL).Tables[0];
            Grid1.DataSource = (DataTable)Session["TblGrid1"];
            Grid1.DataBind();
            lblRowCnt.Text = "Rows : " + Grid1.Rows.Count.ToString();
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

        DropDownList tmpddlreseller = (editingRow.FindControl("ddlreseller") as DropDownList);
        DropDownList tmpddlcountry = (editingRow.FindControl("ddlcountry") as DropDownList);
        DropDownList tmpddlBU = (editingRow.FindControl("ddlBU") as DropDownList);
        DropDownList tmpddlexpClosingMonthMMM = (editingRow.FindControl("ddlexpClosingMonthMMM") as DropDownList);
        DropDownList tmpddlexpClosingYear = (editingRow.FindControl("ddlexpClosingYear") as DropDownList);
        DropDownList tmpddldealStatusEdt = (editingRow.FindControl("ddldealStatusEdt") as DropDownList);


        try
        {
        
            loadCountries(tmpddlcountry);
        

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
            tmptxtremarks.Text = tmplblremarks.Text;

        
            if (tmplbluserLastUpdateDate.Text.Trim() == "")
                tmptxtuserLastUpdateDate.Text = DateTime.Now.Date.ToString("MM-dd-yyyy");
            else
                tmptxtuserLastUpdateDate.Text = tmplbluserLastUpdateDate.Text;
        
            //if (tmplblorderBookedDate.Text.Trim() == "")
            //    tmptxtorderBookedDate.Text = DateTime.Now.Date.ToString("MM-dd-yyyy");
            //else

            if (tmplblorderBookedDate.Text.IndexOf("1999") >= 0)
            {
                //do nothing , just leave it null
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

            if (tmpddlreseller.SelectedIndex >= 0)
            {
                tmpddlreseller.SelectedIndex = -1;

                if (tmplblresellerCode.Text != "")
                    tmpddlreseller.Items.FindByValue("[" + splitdbCodeForCountry(tmpddlcountry.SelectedValue.ToString()) + "] " + tmplblresellerCode.Text).Selected = true;
            }

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

        }
        catch (Exception exp)
        {
            lblMsg.Text = "Exception Error : " + exp.Message;
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
    }

    
    protected void Grid1_RowUpdating(object sender, GridViewUpdateEventArgs e)
    {
        GridViewRow myRow = Grid1.Rows[e.RowIndex];
        
        if (myRow != null)
        {
            String sqlQry;
            
            ///////////////
            Label tmplblFidEdt = ((Label)myRow.FindControl("lblFidEdt") as Label);
            TextBox tmptxtBDM = (myRow.FindControl("txtBDM") as TextBox);
            TextBox tmptxtquoteID = (myRow.FindControl("txtquoteID") as TextBox);
            TextBox tmptxtendUser = (myRow.FindControl("txtendUser") as TextBox);
            TextBox tmptxtgoodsDescr = (myRow.FindControl("txtgoodsDescr") as TextBox);
            
            TextBox tmptxtquoteDate = (myRow.FindControl("txtquoteDate") as TextBox);
            TextBox tmptxtvalue = (myRow.FindControl("txtvalue") as TextBox);
            
            TextBox tmptxtremarks = (myRow.FindControl("txtremarks") as TextBox);
            
            TextBox tmptxtuserLastUpdateDate = (myRow.FindControl("txtuserLastUpdateDate") as TextBox);
            TextBox tmptxtorderBookedDate = (myRow.FindControl("txtorderBookedDate") as TextBox);
            
            //TextBox tmptxtrddSalesPerson = (myRow.FindControl("txtrddSalesPerson") as TextBox);

            DropDownList tmpddlreseller = (myRow.FindControl("ddlreseller") as DropDownList);
            DropDownList tmpddlcountry = (myRow.FindControl("ddlcountry") as DropDownList);
            DropDownList tmpddlBU = (myRow.FindControl("ddlBU") as DropDownList);
            DropDownList tmpddlexpClosingMonthMMM = (myRow.FindControl("ddlexpClosingMonthMMM") as DropDownList);
            DropDownList tmpddlexpClosingYear = (myRow.FindControl("ddlexpClosingYear") as DropDownList);
            DropDownList tmpddldealStatusEdt = (myRow.FindControl("ddldealStatusEdt") as DropDownList);

            string rCode =splitResellerCode(tmpddlreseller.SelectedValue.ToString());

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
            if (tmptxtremarks.Text.Trim() == "" || tmptxtremarks.Text.Trim().IndexOf("'") >= 0)
            {
                lblMsg.Text = "Error ! Either the field REMARKS is empty or contains a Invalid character ' , please supply a valid value";
                return;
            }
            //if (tmptxtrddSalesPerson.Text.Trim() == "" || tmptxtrddSalesPerson.Text.Trim().IndexOf("'") >= 0)
            //{
            //    lblMsg.Text = "Error ! Either the field RDD Sales Person is empty or contains a Invalid character ' , please supply a valid value";
            //    return;
            //}

            if (tmptxtvalue.Text.Trim() == "" || Util.isValidDecimalNumber(tmptxtvalue.Text) == false)
            {
                lblMsg.Text = "Error ! Either the field $VALUE is empty or contains a Invalid Numeric value, please supply a valid numeric value";
                return;
            }
            ////////////////

            if (tmptxtquoteDate.Text.Trim() == "" || Util.IsValidDate(tmptxtquoteDate.Text) == false)
            {
                lblMsg.Text = "Error ! Either the field QUOTE DATE is empty or contains a Invalid Date, please supply a valid value in format MM-DD-YYYY";
                return;
            }

            if (tmptxtuserLastUpdateDate.Text.Trim() == "" || Util.IsValidDate(tmptxtuserLastUpdateDate.Text) == false)
            {
                lblMsg.Text = "Error ! Either the field LAST UPDATE is empty or contains a Invalid Date, please supply a valid value in format MM-DD-YYYY";
                return;
            }

            string ordDt;
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
                + ",expClosingMonth,expClosingMonthMMM,expClosingYear,remarks,dealStatus,userLastUpdateDate,orderBookedDate,rddSalesPerson,CreatedOn,CreatedBy,LastUpdatedOn,LastUpdatedBy) "
                + " values('{0}','{1}','{2}','{3}','{4}','{5}','{6}','{7}','{8}',{9},'{10}',{11},{12},{13},'{14}',{15},'{16}','{17}','{18}','{19}','{20}','{21}','{22}','{23}','{24}')"
                    , tmptxtBDM.Text.Trim(), tmptxtquoteID.Text.Trim(), tmptxtendUser.Text.Trim(), rCode, tmpddlreseller.SelectedItem.Text, tmpddlcountry.SelectedItem.Text, tmpddlBU.SelectedValue.ToString()
                    , tmptxtgoodsDescr.Text.Trim(), tmptxtquoteDate.Text.Trim(), qMnthNo, qMMM, qYR
                    , tmptxtvalue.Text.Trim(), tmpddlexpClosingMonthMMM.SelectedValue.ToString(), tmpddlexpClosingMonthMMM.SelectedItem.Text, tmpddlexpClosingYear.SelectedValue.ToString()
                    , tmptxtremarks.Text.Trim(), tmpddldealStatusEdt.SelectedValue.ToString(), tmptxtuserLastUpdateDate.Text.Trim(), ordDt, "NA"
                    , DateTime.Now.ToString("yyyy-MM-dd hh:mm:ss tt"),myGlobal.loggedInUser(),DateTime.Now.ToString("yyyy-MM-dd hh:mm:ss tt"),myGlobal.loggedInUser());
                Db.constr = myGlobal.getAppSettingsDataForKey("tejSap");
                Db.myExecuteSQL(sqlQry);
            }
            else
            {
                sqlQry = string.Format("update tejSap.[dbo].[tejFunnel2017] set bdm='{0}',quoteID='{1}',endUser='{2}',resellerCode='{3}',resellerName='{4}',country='{5}',BU='{6}',goodsDescr='{7}'"
                    +",quoteDate='{8}',quoteMonth={9},quoteMonthMMM='{10}',quoteYear={11},value={12},expClosingMonth={13},expClosingMonthMMM='{14}',expClosingYear={15},remarks='{16}'"
                    +",dealStatus='{17}',userLastUpdateDate='{18}',orderBookedDate='{19}',rddSalesPerson='{20}',LastUpdatedOn='{21}',LastUpdatedBy='{22}'  where fid={23}"
                    , tmptxtBDM.Text.Trim(), tmptxtquoteID.Text.Trim(), tmptxtendUser.Text.Trim(), rCode, tmpddlreseller.SelectedItem.Text, tmpddlcountry.SelectedItem.Text, tmpddlBU.SelectedValue.ToString()
                    , tmptxtgoodsDescr.Text.Trim(), tmptxtquoteDate.Text.Trim(), qMnthNo, qMMM, qYR
                    , tmptxtvalue.Text.Trim(), tmpddlexpClosingMonthMMM.SelectedValue.ToString(), tmpddlexpClosingMonthMMM.SelectedItem.Text, tmpddlexpClosingYear.SelectedValue.ToString()
                    , tmptxtremarks.Text.Trim(), tmpddldealStatusEdt.SelectedValue.ToString(), tmptxtuserLastUpdateDate.Text.Trim(), ordDt, "NA"
                    , DateTime.Now.ToString("yyyy-MM-dd hh:mm:ss tt"), myGlobal.loggedInUser(), tmplblFidEdt.Text.Trim());

                Db.constr = myGlobal.getAppSettingsDataForKey("tejSap");
                Db.myExecuteSQL(sqlQry);
            }

            Grid1.EditIndex = -1;
            btnNewDeal.Enabled = true; //make it true in add/edit case
            BindGrid();
            lblMsg.Text = "Funnel Deal record has been updated successfully  @" + DateTime.Now.ToString("MM-dd-yyyy hh:mm") ;
            setAddBtnEnableStatus(true);
           
            setGridColsVisibleStatus(true);
            loadQT(ddlFltrQT);
        }

        lblprevCntryDbCode.Text = ""; //just clear the old value stored for filter case
    }
    
    protected void Grid1_RowDeleting(object sender, GridViewDeleteEventArgs e)
    {
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

                if (lblFid.Text != "" && lblFid != null) //in case of empty row 
                {
                    Db.constr = myGlobal.getAppSettingsDataForKey("tejSap");
                    String sqlQry = "update tejSap.[dbo].[tejFunnel2017] set isRowActive=0 where fid=" + lblFid.Text;
                    Db.myExecuteSQL(sqlQry);
                    loadQT(ddlFltrQT);
                    lblMsg.Text = "Record has been deleted/deactivated successfully";
                }
            }
        }
        BindGrid();
        setAddBtnEnableStatus(true);
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
}
