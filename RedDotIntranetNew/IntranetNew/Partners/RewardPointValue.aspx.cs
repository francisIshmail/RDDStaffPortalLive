using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Data;

public partial class IntranetNew_Partners_RewardPointValue : System.Web.UI.Page
{
    protected void Page_Load(object sender, EventArgs e)
    {
        if (!IsPostBack)
        {
            BindCountryDDL(ddlCountry);
            BindGrid();
        }
    }

    /// <summary>
    ///  For Binding Data to Gridview on PageLoad and Search Button click events
    /// </summary>
    private void BindGrid()
    {
        try
        {
            string WhereClause = "", SQLQry;
            if (!(string.IsNullOrEmpty(txtFromDt.Text)) && !(string.IsNullOrEmpty(txtToDt.Text)))
            {
                //WhereClause = "  (( FromDate between '" + txtFromDt.Text + "' and '" + txtToDt.Text + "' ) or ( ToDate between '" + txtFromDt.Text + "' and '" + txtToDt.Text + "' )) ";
                //WhereClause = "  FromDate='" + txtFromDt.Text + "' And ToDate='" + txtToDt.Text + "' ";
                WhereClause = "( ('" + txtFromDt.Text + "' between FromDate and ToDate) OR ('" + txtToDt.Text + "'  between FromDate and ToDate ) )";
            }

            if (ddlCountry.SelectedIndex > 0)
            {
                if (string.IsNullOrEmpty(WhereClause))
                {
                    WhereClause = " PV.Country='" + ddlCountry.SelectedItem.Value + "'";
                }
                else
                {
                    WhereClause = WhereClause + "  And PV.Country='" + ddlCountry.SelectedItem.Value + "'";
                }
            }

            SQLQry = " select PV.RewardValueId,C.Country, PV.Country as CountryCode , convert( varchar(20),PV.FromDate,101) as FromDate, convert( varchar(20),PV.ToDate,101)  as ToDate , PV.RewardValue, PV.Remark , PV.IsActive  from dbo.Reward_PointValue  PV  Join dbo.rddcountriesList  C On PV.Country=C.CountryCode ";
            if (!string.IsNullOrEmpty(WhereClause))
            {
                SQLQry = SQLQry + "   Where  "+ WhereClause;
            }
            SQLQry = SQLQry + " Order by  PV.RewardValueId,PV.FromDate ";

            Db.constr = myGlobal.getAppSettingsDataForKey("tejSAP");
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
            lblMsg.Text = " Error in BindGrid() " + ex.Message;
        }
    }

    /// <summary>
    ///  For Binding countries to dropdownlist
    /// </summary>
    /// <param name="ddlCountry"></param>
    private void BindCountryDDL(DropDownList ddlCountry)
    {
        try
        {
            Db.LoadDDLsWithCon(ddlCountry, "Select '--Select--' as CountryCode , '--Select--' as Country Union select distinct CountryCode,Country from dbo.rddCountriesList", "Country", "CountryCode", myGlobal.getAppSettingsDataForKey("tejSAP"));
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

    /// <summary>
    ///  To perform validation before SAVE And UPDATE and then SAVE/UPDATE data.
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
            if (ddlCountry.SelectedIndex == 0)
            {
                Page.ClientScript.RegisterStartupScript(this.GetType(), "alert", "<script> alert('Please select country'); </script>");
                return;
            }
            if (string.IsNullOrEmpty(txtRewardValue.Text))
            {
                Page.ClientScript.RegisterStartupScript(this.GetType(), "alert", "<script> alert('Please enter one reward point value in USD'); </script>");
                return;
            }
            if (string.IsNullOrEmpty(txtRemark.Text))
            {
                Page.ClientScript.RegisterStartupScript(this.GetType(), "alert", "<script> alert('Please enter Remark'); </script>");
                return;
            }

            Db.constr = myGlobal.getAppSettingsDataForKey("tejSAP");

            string RewardValueID = "0";
            if (!string.IsNullOrEmpty(lblRewardValueID.Text))
            {
                RewardValueID = lblRewardValueID.Text;
            }

            int RecCount = Db.myExecuteScalar("select COUNT(*)  as RecCount from Reward_PointValue  where  RewardValueID<>" + RewardValueID + "  and  country='" + ddlCountry.SelectedItem.Value + "' and  ( ('" + txtFromDt.Text + "' between FromDate and ToDate) OR ('" + txtToDt.Text + "'  between FromDate and ToDate ) )");
            if (RecCount > 0)
            {
                Page.ClientScript.RegisterStartupScript(this.GetType(), "alert", "<script> alert('Enter data is overlapping with existing record, To find all exising matching record click on Search..'); </script>");
                return;
            }

            int isActive = 0;
            if (chkIsActive.Checked == true)
                isActive = 1;
            else
                isActive = 0;

            if (BtnSave.Text == "Save")
            {
                long RewardPointID = Db.myExecuteSQLReturnLatestAutoID(" Insert into dbo.Reward_PointValue ( Country,FromDate,ToDate,RewardValue, IsActive, Remark , CreatedOn,CreatedBy) Values ('" + ddlCountry.SelectedItem.Value + "','" + txtFromDt.Text + "','" + txtToDt.Text + "'," + txtRewardValue.Text + "," + isActive + ", '" + txtRemark.Text + "', GETDATE(),'" + myGlobal.loggedInUser() + "')");
               if (RewardPointID > 0)
               {
                   lblMsg.Text = "Record saved successfully.";
               }
            }
            else if (BtnSave.Text == "Update")
            {
                if (!string.IsNullOrEmpty(lblRewardValueID.Text))
                {
                    Db.myExecuteSQL(" Update dbo.Reward_PointValue  set  Country='" + ddlCountry.SelectedItem.Value + "' , FromDate='" + txtFromDt.Text + "', ToDate='" + txtToDt.Text + "', RewardValue= " + txtRewardValue.Text + ", IsActive= " + isActive + " , Remark='" + txtRemark.Text + "',  LastUpdatedOn= GETDATE() , LastUpdatedBy='" + myGlobal.loggedInUser() + "' Where RewardValueId=" + lblRewardValueID.Text);
                    lblMsg.Text = "Record Updated successfully";
                }
            }
            
            ClearControl();
            pnlRewardValueList.Visible = true;
            BindGrid();

            BtnSave.Text = "Save";

        }
        catch (Exception ex)
        {
            lblMsg.Text = "Error in BtnSave_Click() : " + ex.Message;
        }
    }

    /// <summary>
    ///  To Reset and Clear controls data 
    /// </summary>
    private void ClearControl()
    {
        try
        {
            txtFromDt.Text = "";
            txtToDt.Text = "";
            lblRewardValueID.Text = "";
            txtRewardValue.Text = "";
            txtRemark.Text = "";
            BindCountryDDL(ddlCountry);
            ddlCountry.SelectedIndex = 0;
            chkIsActive.Checked = false;
            Gridview1.SelectedIndex = -1;
        }
        catch (Exception ex)
        {
            lblMsg.Text = "ClearControl()  " + ex.Message;
        }
    }

    /// <summary>
    ///  To Reset all controls
    /// </summary>
    /// <param name="sender"></param>
    /// <param name="e"></param>
    protected void BtnCancel_Click(object sender, EventArgs e)
    {
        try
        {
            BtnSave.Text = "Save";
            lblMsg.Text = "";
            pnlRewardValueList.Visible = true;
            ClearControl();
            BindGrid();
        }
        catch (Exception ex)
        {
            lblMsg.Text = "BtnCancel_Click()  " + ex.Message;
        }
    }

    /// <summary>
    ///  To search the data for selected criteria and show data into GridView control
    /// </summary>
    /// <param name="sender"></param>
    /// <param name="e"></param>
    protected void BtnSearch_Click(object sender, EventArgs e)
    {
        try
        {
            pnlRewardValueList.Visible = true;
            BindGrid();

        }
        catch (Exception ex )
        {
            lblMsg.Text = "BtnSearch_Click()  " + ex.Message;
        }
    }

    /// <summary>
    /// After selecting record from gridview ... display data into controls on form to edit it.
    /// </summary>
    /// <param name="sender"></param>
    /// <param name="e"></param>
    protected void Gridview1_SelectedIndexChanged(object sender, EventArgs e)
    {
        try
        {
            BtnSave.Text = "Update";
            lblMsg.Text = "";
            pnlRewardValueList.Visible = false;
            GridViewRow row = Gridview1.SelectedRow;
            Label lblID = (Label)row.FindControl("lblID");
            Label lblCountry = (Label)row.FindControl("lblCountry");
            Label lblCountryCode = (Label)row.FindControl("lblCountryCode");
            Label lblFromDate = (Label)row.FindControl("lblFromDate");
            Label lblToDate = (Label)row.FindControl("lblToDate");
            Label lblRewardValue = (Label)row.FindControl("lblRewardValue");
            Label lblRemark = (Label)row.FindControl("lblRemark");
            CheckBox grbchkIsActive = (CheckBox)row.FindControl("chkIsActive");
           
            string ID = lblID.Text;
            lblRewardValueID.Text = ID;
            txtFromDt.Text = lblFromDate.Text;
            txtToDt.Text = lblToDate.Text;
            txtRewardValue.Text = lblRewardValue.Text;
            txtRemark.Text = lblRemark.Text;
            string contryName = lblCountry.Text;
            string countrycode = lblCountryCode.Text;
            ddlCountry.SelectedItem.Text = contryName;
            ddlCountry.SelectedValue = countrycode;

            if (grbchkIsActive.Checked)
                chkIsActive.Checked = true;
            else
                chkIsActive.Checked = false;

        }
        catch (Exception ex)
        {
            lblMsg.Text = "Gridview1_SelectedIndexChanged()  " + ex.Message;
        }
    }

    protected void Gridview1_PageIndexChanging(object sender, GridViewPageEventArgs e)
    {
        try
        {
            Gridview1.PageIndex = e.NewPageIndex;
            BindGrid();
        }
        catch (Exception ex)
        {
            lblMsg.Text = "Error in Gridview1_PageIndexChanging() : " + ex.Message;
        }
    }
}