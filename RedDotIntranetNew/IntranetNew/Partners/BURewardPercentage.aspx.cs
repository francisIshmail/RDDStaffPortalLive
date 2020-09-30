using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Data;

public partial class IntranetNew_Partners_BURewardPercentage : System.Web.UI.Page
{
    protected void Page_Load(object sender, EventArgs e)
    {
        if (!Page.IsPostBack)
        {
            BindCountryDDL( ddlCountry);
            BindBUDDL(ddlBU);
            BindGPRangePercentDDL(ddlGPRangePercent);
            BindGridforSearch();
        }
    }

    /// <summary>
    /// For Bindig/Loading GP Rage % to dropdown control
    /// </summary>
    /// <param name="ddlGPRangePercent"></param>
    private void BindGPRangePercentDDL(DropDownList ddlGPRangePercent)
    {
        try
        {
            Db.LoadDDLsWithCon(ddlGPRangePercent, "Select 0 as GPRangeID, '--Select--' as Ranges union select GPRangeId, cast(GPPercentageFrom as varchar) +' TO ' + cast( GPPercentageTo as Varchar)  as Ranges from Reward_GPRange Where IsActive=1", "Ranges", "GPRangeId", myGlobal.getAppSettingsDataForKey("tejSAP"));
            if (ddlBU.Items.Count > 0)
                ddlBU.SelectedIndex = 0;
            else
            {
                ddlBU.Items.Add("No Rows");
                ddlBU.SelectedIndex = 0;
            }
        }
        catch (Exception ex)
        {
            lblMsg.Text = "Error in BindGPRangePercentDDL() : " + ex.Message;
        }     
    }

    /// <summary>
    ///  For Binding/Loading distinct BU's from SAPAE database to dropdown control 
    /// </summary>
    /// <param name="ddlBU"></param>
    private void BindBUDDL(DropDownList ddlBU)
    {
        try
        {
            Db.LoadDDLsWithCon(ddlBU, " Select A.code,A.Name From(Select 0 as code , '--Select--' as 'Name'  union  select ItmsGrpCod as code, ItmsGrpNam as Name from dbo.OITB) A Order by A.code", "Name", "Name", myGlobal.getAppSettingsDataForKey("SAPAE"));
            if (ddlBU.Items.Count > 0)
                ddlBU.SelectedIndex = 0;
            else
            {
                ddlBU.Items.Add("No Rows");
                ddlBU.SelectedIndex = 0;
            }
        }
        catch (Exception ex)
        {
            lblMsg.Text = "Error in BindBUDDL() : " + ex.Message;
        }
    }

    /// <summary>
    /// For Binding/Loading distinct RDD countries from tejSAP.dbo.rddCountriesList table to country dropdown control.
    /// </summary>
    /// <param name="ddlCountry"></param>
    private void BindCountryDDL(DropDownList ddlCountry)
    {
        try
        {
            Db.LoadDDLsWithCon(ddlCountry, "Select '--Select--' as Country, '00' as 'CountryCode' Union select distinct Country,CountryCode from dbo.rddCountriesList", "Country", "CountryCode", myGlobal.getAppSettingsDataForKey("tejSAP"));
            if (ddlCountry.Items.Count > 0)
                ddlCountry.SelectedIndex = 0;
            else
            {
                ddlCountry.Items.Add("No Rows");
                ddlCountry.SelectedIndex = 0;
            }
        }
        catch (Exception ex)
        {
            lblMsg.Text = "Error in BindCountryDDL() : " + ex.Message;
        }
    }

    protected void BtnSearch_Click(object sender, EventArgs e)
    {
        try
        {
            lblMsg.Text = "";
            pnlBURewardPerList.Visible = true;
            BindGridforSearch();
        }
        catch (Exception ex)
        {
            lblMsg.Text = "Error in BtnSearch_Click() : " + ex.Message;
        }
    }

    /// <summary>
    ///  This function Bind Gridview data( BU Wise % Data ) on Search Button and GridView PageIndexChanged
    /// </summary>
    private void BindGridforSearch()
    {
        try
        {
           
            string WhereClause = "", SQLQry;
            if (!(string.IsNullOrEmpty(txtFromDt.Text)) && !(string.IsNullOrEmpty(txtToDt.Text)))
            {
                //WhereClause = "  (( FromDate between '" + txtFromDt.Text + "' and '" + txtToDt.Text + "' ) or ( ToDate between '" + txtFromDt.Text + "' and '" + txtToDt.Text + "' )) ";
                //WhereClause = "  FromDate='" + txtFromDt.Text + "' And ToDate='" + txtToDt.Text + "' ";
                WhereClause = " ( ('" + txtFromDt.Text + "' between FromDate and ToDate) OR ('" + txtToDt.Text + "'  between FromDate and ToDate ) ) ";
            }

            if (ddlCountry.SelectedItem.Text != "--Select--")
            {
                if (string.IsNullOrEmpty(WhereClause))
                {
                    WhereClause = " t1.Country='" + ddlCountry.SelectedItem.Value + "'";
                }
                else
                {
                    WhereClause = WhereClause + "  And t1.Country='" + ddlCountry.SelectedItem.Value + "'";
                }
            }

            if (ddlBU.SelectedItem.Text !="--Select--")
            {
                if (string.IsNullOrEmpty(WhereClause))
                {
                    WhereClause = " BU='" + ddlBU.SelectedItem.Text + "'";
                }
                else
                {
                    WhereClause = WhereClause + "  And BU='" + ddlBU.SelectedItem.Text + "'";
                }
            }

            if (ddlGPRangePercent.SelectedItem.Text != "--Select--")
            {
                if (string.IsNullOrEmpty(WhereClause))
                {
                    WhereClause = " t1.GPRangeID='" + ddlGPRangePercent.SelectedItem.Value + "'";
                }
                else
                {
                    WhereClause = WhereClause + "  And t1.GPRangeID='" + ddlGPRangePercent.SelectedItem.Value + "'";
                }
            }

            Db.constr = myGlobal.getAppSettingsDataForKey("tejSAP");

            SQLQry = @"select t1.BURewardId 
		                        , CONVERT( varchar(20),t1.FromDate ,101) FromDate
		                        , CONVERT( varchar(20),t1.ToDate ,101) ToDate
		                        , t1.Country as CountryCode
                                , C.Country
		                        , t1.BU
		                        , t1.GPRangeID
		                        , cast(GPPercentageFrom as varchar) +' TO ' + cast( GPPercentageTo as Varchar)  as GPRangePercent
		                        , t1.RewardPercentage
                        from Reward_GPRange t0 
	                        Join Reward_BUWisePercentage t1 on t0.GPRangeId=t1.GPRangeId 
                            Join dbo.rddcountrieslist C On  t1.Country= C.CountryCode  ";
            if (!string.IsNullOrEmpty(WhereClause))
                SQLQry = SQLQry + " Where " + WhereClause;
            DataSet DS = Db.myGetDS(SQLQry);
            if (DS.Tables.Count > 0)
            {
                if (DS.Tables[0].Rows.Count == 0)
                {
                    lblMsg.Text = " No data found for selected criteria.";
                }
                Gridview1.DataSource = DS.Tables[0];
                Gridview1.DataBind();
            }
        }
        catch (Exception ex)
        {
            lblMsg.Text = "Error in BindGridforSearch() : " + ex.Message;
        }
    }

    /// <summary>
    ///  for showing the new page data after PageIndex change click event.
    /// </summary>
    /// <param name="sender"></param>
    /// <param name="e"></param>
    protected void Gridview1_PageIndexChanging(object sender, GridViewPageEventArgs e)
    {
        try
        {
            lblMsg.Text = "";
            Gridview1.PageIndex = e.NewPageIndex;
            BindGridforSearch();
        }
        catch (Exception ex)
        {
            lblMsg.Text = "Error in Gridview1_PageIndexChanging() : " + ex.Message;
        }
    }

    protected void BtnCancel_Click(object sender, EventArgs e)
    {
        try
        {
            BtnSave.Text = "Save";
            lblMsg.Text = "";
            ClearControl(); 
            pnlBURewardPerList.Visible = true;
            BindGridforSearch();
        }
        catch (Exception ex)
        {
            lblMsg.Text = " BtnCancel_Click " + ex.Message;
        }
    }

    /// <summary>
    ///  To clear and reset all controls..
    /// </summary>
    private void ClearControl()
    {
        txtFromDt.Text = "";
        txtToDt.Text = "";
        lblBURewardID.Text = "";
        BindCountryDDL(ddlCountry);
        ddlCountry.SelectedIndex = 0;
        BindBUDDL(ddlBU);
        ddlBU.SelectedIndex = 0;
        BindGPRangePercentDDL(ddlGPRangePercent);
        ddlGPRangePercent.SelectedIndex = 0;
        txtRewardPercent.Text = "";
        BindGridforSearch();
        if (Gridview1.SelectedIndex != -1)
        {
            Gridview1.SelectedIndex = -1;
        }
    }

    /// <summary>
    /// To perform validation and SAVE and UPDATE data
    /// </summary>
    /// <param name="sender"></param>
    /// <param name="e"></param>
    protected void BtnSave_Click(object sender, EventArgs e)
    {
        try
        {
            lblMsg.Text = "";
            if (string.IsNullOrEmpty(txtFromDt.Text))
            {
                Page.ClientScript.RegisterStartupScript(this.GetType(), "alert", "<script> alert('Please enter From Date '); </script>");
                return;
            }
            if (string.IsNullOrEmpty(txtToDt.Text))
            {
                Page.ClientScript.RegisterStartupScript(this.GetType(), "alert", "<script> alert('Please enter To Date '); </script>");
                return;
            }
            if (Convert.ToDateTime(txtFromDt.Text) >= Convert.ToDateTime(txtToDt.Text))
            {
                Page.ClientScript.RegisterStartupScript(this.GetType(), "alert", "<script> alert('To Date must be greater then From Date'); </script>");
                return;
            }
            if (ddlCountry.SelectedItem.Text=="--Select--")
            {
                Page.ClientScript.RegisterStartupScript(this.GetType(), "alert", "<script> alert('Please select country'); </script>");
                return;
            }
            if (ddlBU.SelectedItem.Text == "--Select--")
            {
                Page.ClientScript.RegisterStartupScript(this.GetType(), "alert", "<script> alert('Please select BU'); </script>");
                return;
            }
            if (ddlGPRangePercent.SelectedItem.Text == "--Select--")
            {
                Page.ClientScript.RegisterStartupScript(this.GetType(), "alert", "<script> alert('Please select GP Range %'); </script>");
                return;
            }
             Db.constr = myGlobal.getAppSettingsDataForKey("tejSAP");
             string _BUrewardID ="0";
              if( !string.IsNullOrEmpty(lblBURewardID.Text ))
              {
                  _BUrewardID = lblBURewardID.Text;
              }

              int RecordCount = Db.myExecuteScalar("Select COUNT(*) from Reward_BUWisePercentage Where   BURewardID <> " + _BUrewardID + " and  Country='" + ddlCountry.SelectedItem.Value + "' and BU='" + ddlBU.SelectedItem.Text + "' and GPRangeId= " + ddlGPRangePercent.SelectedItem.Value + " and ( ('" + txtFromDt.Text + "' between FromDate and ToDate) OR ('" + txtToDt.Text + "'  between FromDate and ToDate ) )");
             if (RecordCount > 0)
             {
                 Page.ClientScript.RegisterStartupScript(this.GetType(), "alert", "<script> alert('BU wise reward % is overlapping with existing data..To Know please click on search'); </script>");
                 return;
             }
            if ( string.IsNullOrEmpty(txtRewardPercent.Text))
            {
                Page.ClientScript.RegisterStartupScript(this.GetType(), "alert", "<script> alert('Please enter Reward %'); </script>");
                return;
            }
            if (BtnSave.Text == "Save")
            {
                long BURewardID = Db.myExecuteSQLReturnLatestAutoID("Insert into dbo.Reward_BUWisePercentage ( FromDate,ToDate,Country,BU,GPRangeId,RewardPercentage,CreatedOn,CreatedBy) Values ('" + txtFromDt.Text + "','" + txtToDt.Text + "','" + ddlCountry.SelectedItem.Value + "','" + ddlBU.SelectedItem.Text + "'," + ddlGPRangePercent.SelectedItem.Value + ",'" + txtRewardPercent.Text + "' , GETDATE(),'" + myGlobal.loggedInUser() + "')");
                if (BURewardID > 0)
                {
                    lblMsg.Text = "Record saved successfully";
                }
            }
            else if (BtnSave.Text == "Update")
            {
                if (!string.IsNullOrEmpty(lblBURewardID.Text))
                {
                    string sql = " Update dbo.Reward_BUWisePercentage  set FromDate='" + txtFromDt.Text + "', ToDate='" + txtToDt.Text + "', Country='" + ddlCountry.SelectedItem.Value + "', BU = '" + ddlBU.SelectedItem.Text + "' , GPRangeId=" + ddlGPRangePercent.SelectedItem.Value + " , RewardPercentage='" + txtRewardPercent.Text + "' , LastUpdatedOn=getdate(), LastUpdatedBy='" + myGlobal.loggedInUser() + "'   Where BURewardId=" + lblBURewardID.Text;
                    Db.myExecuteSQL(sql);
                    lblMsg.Text = "Record updated successfully";
                }
            }
            BtnSave.Text = "Save";
            ClearControl();
            pnlBURewardPerList.Visible = true;
            BindGridforSearch();
           
        }
        catch (Exception ex)
        {
            lblMsg.Text = "BtnSave_Click()"+ ex.Message;
        }
    }

    /// <summary>
    /// To show selected data in form controls to update it
    /// </summary>
    /// <param name="sender"></param>
    /// <param name="e"></param>
    protected void Gridview1_SelectedIndexChanged(object sender, EventArgs e)
    {
        try
        {
            BtnSave.Text = "Update";
            lblMsg.Text = "";
            pnlBURewardPerList.Visible = false;

            GridViewRow row = Gridview1.SelectedRow;
            Label lblID = (Label)row.FindControl("lblID");
            Label lblCountry = (Label)row.FindControl("lblCountry");
            Label lblCountryCode = (Label)row.FindControl("lblCountryCode");
            Label lblFromDate = (Label)row.FindControl("lblFromDate");
            Label lblToDate = (Label)row.FindControl("lblToDate");
            Label lblBU = (Label)row.FindControl("lblBU");
            Label lblGPRangeId = (Label)row.FindControl("lblGPRangeId");
            Label lblGPRangePercent = (Label)row.FindControl("lblGPRangePercent");
            Label lblRewardPercentage = (Label)row.FindControl("lblRewardPercentage");

            lblBURewardID.Text = lblID.Text;
            txtFromDt.Text = lblFromDate.Text;
            txtToDt.Text = lblToDate.Text;
            txtRewardPercent.Text = lblRewardPercentage.Text;
           
            string country = lblCountry.Text;
            string countrycode = lblCountryCode.Text;
            ddlCountry.SelectedItem.Text = country;
            try
            {
                ddlCountry.SelectedValue = ddlCountry.Items.FindByText(country).Value;
            }
            catch { }
            ddlCountry.SelectedValue = countrycode;

            string BU = lblBU.Text;
            ddlBU.SelectedItem.Text = BU;
            ddlBU.SelectedValue = ddlBU.Items.FindByText(BU).Value;

            string GPRangePercent = lblGPRangePercent.Text;
            ddlGPRangePercent.SelectedItem.Text = GPRangePercent;
            string GPRangeID = lblGPRangeId.Text;
            ddlGPRangePercent.SelectedValue = GPRangeID;

        }
        catch (Exception ex )
        {
            lblMsg.Text = "Gridview1_SelectedIndexChanged()"+ ex.Message;
        }
    }
}