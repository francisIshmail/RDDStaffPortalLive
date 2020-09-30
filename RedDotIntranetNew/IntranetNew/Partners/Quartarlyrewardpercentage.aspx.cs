using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Data;
using System.Text;

public partial class IntranetNew_Partners_Quartarlyrewardpercentage : System.Web.UI.Page
{
    

    protected void Page_Load(object sender, EventArgs e)
    {
        if (!IsPostBack)
        {
            try
            {
                //DataTable TblReward = new DataTable();
                //DataColumn colRewardSettingID = TblReward.Columns.Add("RewardSettingID", typeof(Int32));
                //DataColumn colRewardSettingLineID = TblReward.Columns.Add("RewardSettingLineID", typeof(Int32));
                //DataColumn colCountrycode = TblReward.Columns.Add("Countrycode", typeof(string));
                //DataColumn colYear = TblReward.Columns.Add("Year", typeof(Int32));
                //DataColumn colQuarter = TblReward.Columns.Add("Quarter", typeof(string));
                //DataColumn colGPPercentageFrom = TblReward.Columns.Add("GPPercentageFrom", typeof(float));
                //DataColumn colGPPercentageTo = TblReward.Columns.Add("GPPercentageTo", typeof(float));
                //DataColumn colRewardPercentage = TblReward.Columns.Add("RewardPercentage", typeof(float));5
                
                Db.constr = myGlobal.getAppSettingsDataForKey("tejSAP");
                int count = Db.myExecuteScalar("Select COUNT(*) from dbo.MenuWiseForms t0 Join dbo.UserAuthorization t1 on t0.MenuId=t1.MenuId and t1.MembershipUserName='" + myGlobal.loggedInUser() + "' And t0.FormURL='Quarterlyrewardpercentage.aspx' and t1.IsActive=1");
                if (count > 0)
                {

                    txtYear.Text = DateTime.Now.Year.ToString();
                    BindCountryDDL(ddlCountry);
                    BindQuarter();
                    // To Bind empty row to Add New record on Page Load
                    BindGridAddNew();
                    // To reset datatable of AddNew row data grid on pageLoad
                    myGlobal.dt_temp = null;
                    myGlobal.autoId_temp = 0;
                    ddlCountry.Enabled = true;
                    ddlQuarter.Enabled = true;
                    BindGrid();

                    int Month = DateTime.Now.Month;
                    if (Month >= 1 && Month <= 3)
                    {
                        ddlQuarter.SelectedIndex = 1;
                    }
                    else if (Month >= 4 && Month <= 6)
                    {
                        ddlQuarter.SelectedIndex = 2;
                    }
                    else if (Month >= 7 && Month <= 9)
                    {
                        ddlQuarter.SelectedIndex = 3;
                    }
                    else if (Month >= 10 && Month <= 12)
                    {
                        ddlQuarter.SelectedIndex = 4;
                    }

                }
                else
                {
                    Response.Redirect("Default.aspx?UserAccess=0&FormName=Quarterly Reward Percentage");
                }

            }
            catch (Exception ex)
            {
                lblMsg.Text = "Error occured in Page_Load : " + ex.Message;
            }
        }
    }

    /// <summary>
    /// To Bind empty row to Add New record on Page Load
    /// </summary>
    private void BindGridAddNew()
    {
        try
        {
            DataTable TblReward = new DataTable();
            DataColumn colRewardSettingLineID = TblReward.Columns.Add("RewardSettingLineID", typeof(Int32));
            DataColumn colGPPercentageFrom = TblReward.Columns.Add("GPPercentageFrom", typeof(float));
            DataColumn colGPPercentageTo = TblReward.Columns.Add("GPPercentageTo", typeof(float));
            DataColumn colRewardPercentage = TblReward.Columns.Add("RewardPercentage", typeof(float));

            TblReward.Rows.Add(TblReward.NewRow());
            grdQuartlyRowDetail.DataSource = TblReward;
            grdQuartlyRowDetail.DataBind();
            grdQuartlyRowDetail.Rows[0].Cells.Clear();
            grdQuartlyRowDetail.Rows[0].Cells.Add(new TableCell());
        }
        catch(Exception ex)
        {
            lblMsg.Text = "Error occured in BindGridAddNew : " + ex.Message;
        }
    }

    private void BindQuarter()
    {
        try
        {
            ddlQuarter.Items.Clear();
            List<ListItem> items = new List<ListItem>();
            items.Add(new ListItem("--Select--", "0"));
            items.Add(new ListItem("Q1", "Q1"));
            items.Add(new ListItem("Q2", "Q2"));
            items.Add(new ListItem("Q3", "Q3"));
            items.Add(new ListItem("Q4", "Q4"));
            ddlQuarter.Items.AddRange(items.ToArray());
        }
        catch (Exception ex)
        {
            lblMsg.Text = "Error Occured in BindQuarter : " + ex.Message;
        }

    }

    private void BindGrid()
    {
        try
        {
            pnlRewardList.Visible = true;
            Db.constr = myGlobal.getAppSettingsDataForKey("tejSAP");
            DataTable TblReward = Db.myGetDS(@" select Q.RewardSettingID,Q1.RewardSettingLineID,Q.Countrycode,C.Country,Q.Year,Q.Quarter,
	                                    Q1.GPPercentageFrom,Q1.GPPercentageTo,Q1.RewardPercentage 
                                        From dbo.Reward_Quarterly Q 
											Join dbo.Reward_QuarterlyLine Q1 On Q.RewardSettingID=Q1.Fk_RewardSettingID
											Join dbo.rddCountriesList C ON Q.CountryCode=C.CountryCode ").Tables[0];

            if (TblReward.Rows.Count > 0)
            {
                GrvListAll.DataSource = TblReward;
                GrvListAll.DataBind();
            }
            else
            {
                pnlRewardList.Visible = false;
            }
        }
        catch (Exception ex)
        {
            lblMsg.Text = "Error occured in BindGrid : " + ex.Message;
        }
    }

    private void BindCountryDDL(DropDownList ddlCountry)
    {
        try
        {
            Db.LoadDDLsWithCon(ddlCountry, "Select '--Select--' as Country, '00' as 'CountryCode' Union select distinct Country,CountryCode from dbo.rddCountriesList  Where CountryCode<>'SR'  ", "Country", "CountryCode", myGlobal.getAppSettingsDataForKey("tejSAP"));
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

    protected void grdQuartlyRowDetail_RowCommand(object sender, GridViewCommandEventArgs e)
    {
        try
        {
            if (e.CommandName == "AddNew")
            {
                lblMsg.Text = "";
                TextBox txtGPFrom = (grdQuartlyRowDetail.FooterRow.FindControl("txtGPPercentageFromFooter")) as TextBox;
                TextBox txtGPTo = (grdQuartlyRowDetail.FooterRow.FindControl("txtGPPercentageToFooter")) as TextBox;
                TextBox txtGPPercentage = (grdQuartlyRowDetail.FooterRow.FindControl("txtRewardPercentageFooter")) as TextBox;

                #region  "Validation "

                if (string.IsNullOrEmpty(txtGPFrom.Text))
                {
                    lblMsg.Text = "Please enter From GP % ";
                   // Page.ClientScript.RegisterStartupScript(this.GetType(), "alert", "<script> alert(' Please enter From GP %  '); </script>");
                    return;
                }
                if (string.IsNullOrEmpty(txtGPTo.Text))
                {
                    lblMsg.Text = "Please enter To GP %";
                    return;
                }

                if (Convert.ToDecimal(txtGPFrom.Text) >= Convert.ToDecimal(txtGPTo.Text))
                {
                    lblMsg.Text = "To GP % must be greater than From GP %";
                    return;
                }

                if (string.IsNullOrEmpty(txtGPPercentage.Text))
                {
                    lblMsg.Text = "Please enter Reward %";
                    return;
                }

                #endregion


                if (myGlobal.dt_temp == null)
                {
                    myGlobal.dt_temp = new DataTable();

                    DataColumn colRewardSettingLineID = myGlobal.dt_temp.Columns.Add("RewardSettingLineID", typeof(Int32));
                    DataColumn colGPPercentageFrom = myGlobal.dt_temp.Columns.Add("GPPercentageFrom", typeof(float));
                    DataColumn colGPPercentageTo = myGlobal.dt_temp.Columns.Add("GPPercentageTo", typeof(float));
                    DataColumn colRewardPercentage = myGlobal.dt_temp.Columns.Add("RewardPercentage", typeof(float));
                }
                DataRow DRow = myGlobal.dt_temp.NewRow();
                DRow["RewardSettingLineID"] = myGlobal.autoId_temp + 1;
                DRow["GPPercentageFrom"] = txtGPFrom.Text;
                DRow["GPPercentageTo"] = txtGPTo.Text;
                DRow["RewardPercentage"] = txtGPPercentage.Text;

                myGlobal.dt_temp.Rows.Add(DRow);
                grdQuartlyRowDetail.DataSource = myGlobal.dt_temp;
                grdQuartlyRowDetail.DataBind();
                myGlobal.autoId_temp = myGlobal.autoId_temp + 1;
                if (!string.IsNullOrEmpty(txtGPFrom.Text))
                {
                    txtGPFrom.Text = (Convert.ToInt32(txtGPTo.Text) + 0.00001).ToString();
                    (grdQuartlyRowDetail.FooterRow.FindControl("txtGPPercentageFromFooter") as TextBox).Text = txtGPFrom.Text;
                }
                txtGPTo.Text = "";
                txtGPPercentage.Text = "";
            }
        }
        catch (Exception  ex)
        {
            lblMsg.Text = "Error in Grid RowCommand () : " + ex.Message;
        }
    }
  
    protected void BtnCancel_Click(object sender, EventArgs e)
    {
        try
        {
            lblMsg.Text = "";
            BtnSave.Text = "Save";
            BtnSave.Enabled = true;
            RewardSettingID.Text = "";
            myGlobal.dt_temp = null;
            myGlobal.autoId_temp = 0;
            grdQuartlyRowDetail.Enabled = true;
            ddlCountry.Enabled = true;
            ddlQuarter.Enabled = true;
            BindGridAddNew();
            BindGrid();
            BindQuarter();
            try
            {
                GrvListAll.SelectedIndex = -1;
            }
            catch { }
        }
        catch (Exception ex )
        {
            lblMsg.Text = "Error in Grid BtnCancel_Click () : " + ex.Message;
        }
       
    }

    protected void grdQuartlyRowDetail_RowEditing(object sender, GridViewEditEventArgs e)
    {
        try
        {
            grdQuartlyRowDetail.EditIndex = e.NewEditIndex;
            if (myGlobal.dt_temp != null)
            {
                grdQuartlyRowDetail.DataSource = myGlobal.dt_temp;
                grdQuartlyRowDetail.DataBind();
            }
        }
        catch (Exception ex)
        {
            lblMsg.Text = "Error occured in RowEditing() :" + ex.Message;
        }
    }

    protected void grdQuartlyRowDetail_RowCancelingEdit(object sender, GridViewCancelEditEventArgs e)
    {
        try
        {
            grdQuartlyRowDetail.EditIndex = -1;
            if (myGlobal.dt_temp != null)
            {
                grdQuartlyRowDetail.DataSource = myGlobal.dt_temp;
                grdQuartlyRowDetail.DataBind();
            }
        }
        catch (Exception ex )
        {
            lblMsg.Text = "Error occured in RowCancelingEdit() :" + ex.Message; 
        }
    }

    protected void grdQuartlyRowDetail_RowUpdating(object sender, GridViewUpdateEventArgs e)
    {
        try
        {
            TextBox txtGPFrom = (grdQuartlyRowDetail.Rows[e.RowIndex].FindControl("txtGPPercentageFrom")) as TextBox;
            TextBox txtGPTo = (grdQuartlyRowDetail.Rows[e.RowIndex].FindControl("txtGPPercentageTo")) as TextBox;
            TextBox txtGPPercentage = (grdQuartlyRowDetail.Rows[e.RowIndex].FindControl("txtRewardPercentage")) as TextBox;
            string GPFrom = txtGPFrom.Text;
            string GPTo = txtGPTo.Text;
            string GPPercentage = txtGPPercentage.Text;

           int RewardSettingLineID= Convert.ToInt32(grdQuartlyRowDetail.DataKeys[e.RowIndex].Value.ToString());
           DataRow DR = myGlobal.dt_temp.Select("RewardSettingLineID=" + RewardSettingLineID.ToString()).FirstOrDefault();
           if (DR != null)
           {
               DR["GPPercentageFrom"] = GPFrom;
               DR["GPPercentageTo"] = GPTo;
               DR["RewardPercentage"] = GPPercentage;
           }
           myGlobal.dt_temp.AcceptChanges();
           grdQuartlyRowDetail.EditIndex = -1;
           grdQuartlyRowDetail.DataSource = myGlobal.dt_temp;
           grdQuartlyRowDetail.DataBind();

        }
        catch (Exception ex)
        {
            lblMsg.Text = "Error occured in RowUpdating() :" + ex.Message;
        }

    }

    protected void BtnSave_Click(object sender, EventArgs e)
    {
        try
        {
            lblMsg.Text = "" ;
            if (ddlCountry.SelectedIndex == 0 || ddlCountry.SelectedItem.Text == "--Select--")
            {
                lblMsg.Text = "Please select country";
                Page.ClientScript.RegisterStartupScript(this.GetType(), "alert", "<script> alert('Please select country'); </script>");
                return;
            }

            if (ddlQuarter.SelectedItem.Text == "--Select--")
            {
                lblMsg.Text = "Please select Quarter";
                Page.ClientScript.RegisterStartupScript(this.GetType(), "alert", "<script> alert('Please select Quarter'); </script>");
                return;
            }

            StringBuilder strqry = new StringBuilder();
            strqry.Length = 0;


            strqry.Append("   select count(*) From dbo.Reward_Quarterly Where  Countrycode='" + ddlCountry.SelectedItem.Value + "' And  Quarter='" + ddlQuarter.SelectedItem.Text + "' And  Year=" + txtYear.Text);
            if (!string.IsNullOrEmpty(RewardSettingID.Text))
            {
                strqry.Append( "   And RewardSettingID<> " + RewardSettingID.Text );
            }

            Db.constr = myGlobal.getAppSettingsDataForKey("tejSAP");
            int countRec= Db.myExecuteScalar(strqry.ToString());
            if (countRec > 0)
            {
                lblMsg.Text = "Record already exist for selected country, Quarter and Year";
                Page.ClientScript.RegisterStartupScript(this.GetType(), "alert", "<script> alert('Record already exist for selected country, Quarter and Year'); </script>");
                return;
            }

            strqry.Length = 0;
            if (BtnSave.Text == "Save")
            {
                strqry.Append("  Declare @RewardSettingID int ;    Insert into dbo.Reward_Quarterly (Countrycode,Year,Quarter,CreatedOn,CreatedBy) Values ( '" + ddlCountry.SelectedItem.Value + "'," + txtYear.Text + ",'" + ddlQuarter.SelectedItem.Text + "',getdate(),'" + myGlobal.loggedInUser() + "');   ");
                strqry.Append("        set @RewardSettingID = @@Identity ; ");

                for (int i = 0; i < grdQuartlyRowDetail.Rows.Count; i++)
                {
                    string GPFrom = (grdQuartlyRowDetail.Rows[i].FindControl("lblGPPercentageFrom") as Label).Text;
                    string GPTo = (grdQuartlyRowDetail.Rows[i].FindControl("lblGPPercentageTo") as Label).Text;
                    string GPPercentage = (grdQuartlyRowDetail.Rows[i].FindControl("lblRewardPercentage") as Label).Text;

                    if (grdQuartlyRowDetail.Rows.Count == 1 && (string.IsNullOrEmpty(GPFrom) || string.IsNullOrEmpty(GPTo)))
                    {
                        lblMsg.Text = "Please Add atleast one row";
                        return;
                    }
                    strqry.Append("   Insert into dbo.Reward_QuarterlyLine (Fk_RewardSettingID,GPPercentageFrom,GPPercentageTo,RewardPercentage) Values ( @RewardSettingID , " + GPFrom + ", " + GPTo + " , " + GPPercentage + "  )   ;  ");
                }
               
                Db.constr = myGlobal.getAppSettingsDataForKey("tejSAP");
                Db.myExecuteSQL(strqry.ToString());
                lblMsg.Text = "Record saved successfully";
            }
            else if (BtnSave.Text == "Update")
            {
                strqry.Append("  Update dbo.Reward_Quarterly Set Countrycode='" + ddlCountry.SelectedItem.Value + "', Year=" + txtYear.Text + ", Quarter='" + ddlQuarter.SelectedItem.Text + "'  Where RewardSettingID="+ RewardSettingID.Text + "   ;  "  );
                for (int i = 0; i < grdQuartlyRowDetail.Rows.Count; i++)
                {
                    string rewardsettingLineId = grdQuartlyRowDetail.DataKeys[i].Value.ToString();
                    string GPFrom = (grdQuartlyRowDetail.Rows[i].FindControl("lblGPPercentageFrom") as Label).Text;
                    string GPTo = (grdQuartlyRowDetail.Rows[i].FindControl("lblGPPercentageTo") as Label).Text;
                    string GPPercentage = (grdQuartlyRowDetail.Rows[i].FindControl("lblRewardPercentage") as Label).Text;

                    if (grdQuartlyRowDetail.Rows.Count == 1 && (string.IsNullOrEmpty(GPFrom) || string.IsNullOrEmpty(GPTo)))
                    {
                        lblMsg.Text = "Please Add atleast one row";
                        return;
                    }
                    
                    strqry.Append("  if ( (  Select count(*) From dbo.Reward_QuarterlyLine Where Fk_RewardSettingID=" + RewardSettingID.Text + " And RewardSettingLineID =" + rewardsettingLineId + " ) = 0 )  Begin    ");
                    strqry.Append("    Insert into dbo.Reward_QuarterlyLine (Fk_RewardSettingID,GPPercentageFrom,GPPercentageTo,RewardPercentage) Values ( " + RewardSettingID.Text + " , " + GPFrom + ", " + GPTo + " , " + GPPercentage + "  )   ; End  Else  Begin  ");
                    strqry.Append("   Update  dbo.Reward_QuarterlyLine Set GPPercentageFrom=" + GPFrom + ", GPPercentageTo=" + GPTo + ", RewardPercentage=" + GPPercentage + " Where Fk_RewardSettingID=" + RewardSettingID.Text + " And RewardSettingLineID ="+rewardsettingLineId  + " ;  End " );
                }
                
                Db.constr = myGlobal.getAppSettingsDataForKey("tejSAP");
                Db.myExecuteSQL(strqry.ToString());
                lblMsg.Text = "Record updated successfully";
            }
            CLearControl();
            //BindGrid();
        }
        catch (Exception ex)
        {
            lblMsg.Text = "Error occured in BtnSave_Click() :" + ex.Message;
        }
    }

    protected void GrvListAll_PageIndexChanging(object sender, GridViewPageEventArgs e)
    {
        try
        {
            lblMsg.Text = "";
            GrvListAll.PageIndex = e.NewPageIndex;
            BindGrid();
        }
        catch (Exception ex)
        {
            lblMsg.Text = "Error in GrvListAll_PageIndexChanging() : " + ex.Message;
        }
    }

    protected void GrvListAll_SelectedIndexChanged(object sender, EventArgs e)
    {
        try
        {
            BtnSave.Text = "Update";
            lblMsg.Text = "";

            pnlRewardList.Visible = false;
            BtnSave.Enabled = true;
            grdQuartlyRowDetail.Enabled = true;
            ddlCountry.Enabled = true;
            ddlQuarter.Enabled = true;

            GridViewRow row = GrvListAll.SelectedRow;
            Label lblRewardSetttingID = (Label)row.FindControl("lblID");
            Label lblCountry = (Label)row.FindControl("lblCountry");
            Label lblCountryCode = (Label)row.FindControl("lblCountryCode");
            Label lblQuarter = (Label)row.FindControl("lblQuarter");
            Label lblYear = (Label)row.FindControl("lblYear");

            string Quarter=lblQuarter.Text;
            string Country=lblCountry.Text;
            string CountryCode=lblCountryCode.Text;

            RewardSettingID.Text= lblRewardSetttingID.Text;

            txtYear.Text = lblYear.Text;
            
            ddlQuarter.SelectedItem.Text = Quarter;
            ddlQuarter.SelectedValue = Quarter;
            try
            {
                ddlQuarter.SelectedValue = ddlQuarter.Items.FindByText(Quarter).Value;
            }
            catch { }

            int Month = DateTime.Now.Month;
            if (Month >= 4 && Month <= 6)
            {
                if (Quarter == "Q1")
                {
                    lblMsg.Text = "You can't change data for previous quarter of year...";
                    BtnSave.Enabled = false;
                    grdQuartlyRowDetail.Enabled = false;
                    ddlCountry.Enabled = false;
                    ddlQuarter.Enabled = false;
                }
            }
            else if (Month >= 7 && Month <= 9)
            {
                if (Quarter == "Q1" || Quarter == "Q2")
                {
                    lblMsg.Text = "You can't change data for previous quarter of year...";
                    BtnSave.Enabled = false;
                    grdQuartlyRowDetail.Enabled = false;
                    ddlCountry.Enabled = false;
                    ddlQuarter.Enabled = false;
                }
            }
            else if (Month >= 10 && Month <= 12)
            {
                if (Quarter == "Q1" || Quarter == "Q2" || Quarter == "Q3")
                {
                    lblMsg.Text = "You can't change data for previous quarter of year...";
                    BtnSave.Enabled = false;
                    grdQuartlyRowDetail.Enabled = false;
                    ddlCountry.Enabled = false;
                    ddlQuarter.Enabled = false;
                }
            }


            BindCountryDDL(ddlCountry);
            ddlCountry.SelectedItem.Text = Country;
            ddlCountry.SelectedValue = CountryCode;

            Db.constr= myGlobal.getAppSettingsDataForKey("tejSAP");
            myGlobal.dt_temp = Db.myGetDS(@"select Q1.RewardSettingLineID, Q1.GPPercentageFrom,Q1.GPPercentageTo,Q1.RewardPercentage 
                                From dbo.Reward_Quarterly Q 
	                                Join dbo.Reward_QuarterlyLine Q1 On Q.RewardSettingID=Q1.Fk_RewardSettingID 
                                Where Q.RewardSettingID=" + lblRewardSetttingID.Text).Tables[0] ;

            grdQuartlyRowDetail.DataSource = myGlobal.dt_temp;
            grdQuartlyRowDetail.DataBind();

        }
        catch (Exception ex)
        {
            lblMsg.Text = "Error in GrvListAll_SelectedIndexChanged() : " + ex.Message;
        }
    }

    public void CLearControl()
    {
        try
        {
            BtnSave.Text = "Save";
            RewardSettingID.Text = "";
            ddlQuarter.SelectedIndex = -1;
            ddlQuarter.SelectedItem.Text = "--Select--";
            ddlCountry.SelectedIndex = -1;
            ddlCountry.SelectedItem.Text = "--Select--";
            myGlobal.dt_temp = null;
            myGlobal.autoId_temp = 0;
            BindGridAddNew();
            BindGrid();
            try
            {
                GrvListAll.SelectedIndex = -1;
            }
            catch { }
        }
        catch (Exception ex)
        {
            lblMsg.Text = "Error Occured in CLearControl():"+ ex.Message ;
        }
    }

    protected void ddlQuarter_SelectedIndexChanged(object sender, EventArgs e)
    {
        try
        {
            lblMsg.Text = "";

            int Month = DateTime.Now.Month;
            string Quarter = ddlQuarter.SelectedItem.Text;
            if (Month >= 4 && Month <= 6)
            {
                if (Quarter == "Q1")
                {
                    lblMsg.Text = "You can't select previous quarter of year...";
                    try
                    {
                        BindQuarter();
                        ddlQuarter.SelectedIndex = 2;
                        //ddlQuarter.SelectedItem.Text = "Q2";
                        //ddlQuarter.SelectedItem.Value = "Q2";
                    }
                    catch{}
                }
            }
            else if (Month >= 7 && Month <= 9)
            {
                if (Quarter == "Q1" || Quarter == "Q2")
                {
                    lblMsg.Text = "You can't select previous quarter of year...";
                    try
                    {
                        BindQuarter();
                        ddlQuarter.SelectedIndex = 3;
                        //ddlQuarter.SelectedItem.Text = "Q3";
                        //ddlQuarter.SelectedItem.Value = "Q3";
                    }
                    catch { }
                }
            }
            else if (Month >= 10 && Month <= 12)
            {
                if (Quarter == "Q1" || Quarter == "Q2" || Quarter == "Q3")
                {
                    lblMsg.Text = "You can't select previous quarter of year...";
                    try
                    {
                        BindQuarter();
                        ddlQuarter.SelectedIndex = 4;
                        //ddlQuarter.SelectedItem.Text = "Q4";
                        //ddlQuarter.SelectedItem.Value = "Q4";
                    }
                    catch { }
                }
            }
        }
        catch (Exception ex)
        {
            lblMsg.Text = "Error Occured in ddlQuarter_SelectedIndexChanged() " + ex.Message;
        }
    }
}