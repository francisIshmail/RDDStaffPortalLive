using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Data;

public partial class IntranetNew_Admin_FormsMaster : System.Web.UI.Page
{
    protected void Page_Load(object sender, EventArgs e)
    {
        if (!IsPostBack)
        {
            Db.constr = myGlobal.getAppSettingsDataForKey("tejSAP");
            int count = Db.myExecuteScalar("Select COUNT(*) from dbo.MenuWiseForms t0 Join dbo.UserAuthorization t1 on t0.MenuId=t1.MenuId and t1.MembershipUserName='" + myGlobal.loggedInUser() + "' And t0.FormURL='FormsMaster.aspx' And t1.IsActive=1");
            if (count > 0)
            {
                BindGrid();
                BindMenuDDL(ddlMenu);
            }
            else
            {
                Response.Redirect("Default.aspx?UserAccess=0&FormName=Forms Master");
            }
        }
        lblMsg.Text = "";
    }

    /// <summary>
    /// To Load menus into dropdownlist control...
    /// </summary>
    /// <param name="ddlMenu"></param>
    private void BindMenuDDL(DropDownList ddlMenu)
    {
        try
        {
            Db.LoadDDLsWithCon(ddlMenu, " Select  0 as ID, '--Select--' as 'MenuName' union select Id,MenuName from Menus ", "MenuName", "Id", myGlobal.getAppSettingsDataForKey("tejSAP"));
            if (ddlMenu.Items.Count > 0)
                ddlMenu.SelectedIndex = 0;
            else
            {
                ddlMenu.Items.Add("No Rows");
                ddlMenu.SelectedIndex = 0;
            }
        }
        catch (Exception ex)
        {
            lblMsg.Text = "Error in BindMenuDDL() : " + ex.Message;
        }
    }

    /// <summary>
    /// To bind data to GridView control
    /// </summary>
    public void BindGrid()
    {
        try
        {
            Db.constr = myGlobal.getAppSettingsDataForKey("tejSAP");
            DataSet DsForms = Db.myGetDS("select * from dbo.MenuWiseForms ");
            if (DsForms.Tables.Count > 0)
            {
                Gridview1.DataSource = DsForms.Tables[0];
                Gridview1.DataBind();
            }
        }
        catch (Exception ex)
        {
            lblMsg.Text = "Error in BindGrid() : " + ex.Message;
        }
    }

    /// <summary>
/// To perform validation and to SAVE/UPDATE data
/// </summary>
/// <param name="sender"></param>
/// <param name="e"></param>
    protected void BtnSave_Click(object sender, EventArgs e)
    {
        try
        {
            if (string.IsNullOrEmpty(ddlMenu.SelectedItem.Text) || ddlMenu.SelectedItem.Text == "--Select--")
            {
                lblMsg.Text = "Please select Menu";
                Page.ClientScript.RegisterStartupScript(this.GetType(), "alert", "<script> alert('Please select Menu'); </script>");
                return;
            }
            if (string.IsNullOrEmpty(txtFormName.Text))
            {
                lblMsg.Text = "Please enter FormName";
                Page.ClientScript.RegisterStartupScript(this.GetType(), "alert", "<script> alert('Please enter FormName'); </script>");
                return;
            }
            if (string.IsNullOrEmpty(txtFormURL.Text))
            {
                lblMsg.Text = "Please enter Form URL";
                Page.ClientScript.RegisterStartupScript(this.GetType(), "alert", "<script> alert('Please enter Form URL'); </script>");
                return;
            }

            Db.constr = myGlobal.getAppSettingsDataForKey("tejSAP");

            string StrSQL = " EXEC ValidationFormMaster NULL";
            if (!string.IsNullOrEmpty(lblMenuID.Text) && BtnSave.Text == "Update")
            {
                StrSQL = " EXEC ValidationFormMaster " + lblMenuID.Text;
            }

            StrSQL = StrSQL + ",'" + ddlMenu.SelectedItem.Text + "','" + txtFormName.Text + "','" + txtFormURL.Text + "'";
            string errorMsg = "";
            System.Data.SqlClient.SqlDataReader sdr = Db.myGetReader(StrSQL);
            if (sdr.HasRows)
            {
                while (sdr.Read())
                {
                    errorMsg = sdr[0].ToString();
                }
            }
            if (!string.IsNullOrEmpty(errorMsg))
            {
                lblMsg.Text = errorMsg;
                Page.ClientScript.RegisterStartupScript(this.GetType(), "alert", "<script> alert('" + errorMsg + "'); </script>");
            }

            if (BtnSave.Text == "Save")
            {
                long LeavePeriodID = Db.myExecuteSQLReturnLatestAutoID("Insert into dbo.MenuWiseForms (MenuName,FormName,FormURL,IsActive,CreatedBy) Values ('" + ddlMenu.SelectedItem.Text + "','" + txtFormName.Text + "','" + txtFormURL.Text + "','" + chkActive.Checked + "','" + myGlobal.loggedInUser() + "')");

                if (LeavePeriodID > 0)
                {
                    lblMsg.Text = " Form saved successfully";
                    //Page.ClientScript.RegisterStartupScript(this.GetType(), "alert", "<script> alert('Form saved successfully'); </script>");
                }
            }
            else if (BtnSave.Text == "Update")
            {
                Db.myExecuteSQL("Update dbo.MenuWiseForms Set MenuName='" + ddlMenu.SelectedItem.Text + "', FormName='" + txtFormName.Text + "', FormURL='" + txtFormURL.Text + "',IsActive='" + chkActive.Checked + "',LastUpdatedOn=GETDATE() , LastUpdatedBy='" + myGlobal.loggedInUser() + "' Where MenuID= " + lblMenuID.Text);

                lblMsg.Text = " Form updated successfully";
                //Page.ClientScript.RegisterStartupScript(this.GetType(), "alert", "<script> alert('Form updated successfully'); </script>");
                BtnSave.Text = "Save";
            }
            BindGrid();
            ClearControl();
        }
        catch (Exception ex)
        {
            lblMsg.Text = "Error in BtnSave_Click() : " + ex.Message;
        }
    }

    /// <summary>
    /// To reset the form in same as Page_Load event..
    /// </summary>
    /// <param name="sender"></param>
    /// <param name="e"></param>
    protected void BtnCancel_Click(object sender, EventArgs e)
    {
        ClearControl();
        lblMsg.Text = "";
    }

    /// <summary>
    ///  To show data on form controls to UPDATE record
    /// </summary>
    /// <param name="sender"></param>
    /// <param name="e"></param>
    protected void Gridview1_SelectedIndexChanged(object sender, EventArgs e)
    {
        try
        {
            BtnSave.Text = "Update";
            lblMsg.Text = "";
            pnlFormList.Visible = false;
            GridViewRow row = Gridview1.SelectedRow;
            lblMsg.Text = "";
            Label lblID = (Label)row.FindControl("lblID");
            Label lblMenu = (Label)row.FindControl("lblMenu");
            Label lblFormName = (Label)row.FindControl("lblFormName");
            Label lblFormURL = (Label)row.FindControl("lblFormURL");
            //Label lblDisplaySeq = (Label)row.FindControl("lblDisplaySeq");
            CheckBox grdchkIsActive = (CheckBox)row.FindControl("chkIsActive");
            string ID = lblID.Text;
            string Menu = lblMenu.Text;
            string FormName = lblFormName.Text;
            string FormURL = lblFormURL.Text;
            lblMenuID.Text = ID;
            BindMenuDDL(ddlMenu);
            ddlMenu.SelectedItem.Value = ID;
            ddlMenu.SelectedItem.Text = Menu;
            txtFormName.Text = FormName;
            txtFormURL.Text = FormURL;
            //txtDisplySeq.Text = lblDisplaySeq.Text;
            if (grdchkIsActive.Checked)
            {
                chkActive.Checked = true;
            }
            else
            {
                chkActive.Checked = false;
            }
        }
        catch (Exception ex)
        {
            lblMsg.Text = "Gridview1_SelectedIndexChanged() " + ex.Message;
        }
    }

    /// <summary>
    /// To reset/clear controls after save/update etc...
    /// </summary>
    public void ClearControl()
    {
        lblMenuID.Text = "";
        txtFormName.Text = "";
        txtFormURL.Text = "";
        //txtDisplySeq.Text = "";
        ddlMenu.SelectedIndex = 0;
        chkActive.Checked = false;
        pnlFormList.Visible = true;
        BtnSave.Text = "Save";
        Gridview1.SelectedIndex = -1;
    }

    protected void Gridview1_PageIndexChanging(object sender, GridViewPageEventArgs e)
    {
        try
        {
            lblMsg.Text = "";
            Gridview1.PageIndex = e.NewPageIndex;
            BindGrid();
        }
        catch (Exception ex)
        {
            lblMsg.Text = " Error Occured  PageIndex Changing:" + ex.Message;
        }
    }



}