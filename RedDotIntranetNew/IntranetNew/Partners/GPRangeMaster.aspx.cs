using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Data;

public partial class IntranetNew_Partners_GPRangeMaster : System.Web.UI.Page
{
    protected void Page_Load(object sender, EventArgs e)
    {
        if (!IsPostBack)
        {
            Db.constr = myGlobal.getAppSettingsDataForKey("tejSAP");
            int count = Db.myExecuteScalar("Select COUNT(*) from dbo.MenuWiseForms t0 Join dbo.UserAuthorization t1 on t0.MenuId=t1.MenuId and t1.MembershipUserName='" + myGlobal.loggedInUser() + "' And t0.FormURL='GPRangeMaster.aspx'");
            if (count > 0)
            {
                txtGPFrom.Enabled = true;
                txtGPTo.Enabled = true;
                txtRemark.Enabled = true;
                BindGrid();
            }
            else
            {
                Response.Redirect("Default.aspx?UserAccess=0&FormName=GP % Range Master");
            }
        }
    }
    /// <summary>
    /// For Binding GP Range % on on form load and after Save and Update.
    /// </summary>
    private void BindGrid()
    {
        try
        {
            Db.constr = myGlobal.getAppSettingsDataForKey("tejSAP");
            DataSet DsForms = Db.myGetDS("select * from dbo.Reward_GPRange ");
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
    /// To perform validation and to Save / Update GP Range Data
    /// </summary>
    /// <param name="sender"></param>
    /// <param name="e"></param>
    protected void BtnSave_Click(object sender, EventArgs e)
    {
        try
        {
            lblMsg.Text = "";
            if (string.IsNullOrEmpty(txtGPFrom.Text))
            {
                lblMsg.Text = "Please enter GP % From ";
                Page.ClientScript.RegisterStartupScript(this.GetType(), "alert", "<script> alert('Please enter GP % From '); </script>");
                return;
            }

            if (string.IsNullOrEmpty(txtGPTo.Text))
            {
                lblMsg.Text = "Please enter GP % To ";
                Page.ClientScript.RegisterStartupScript(this.GetType(), "alert", "<script> alert('Please enter GP % To '); </script>");
                return;
            }

            if (Convert.ToDouble(txtGPFrom.Text) >= Convert.ToDouble(txtGPTo.Text))
            {
                lblMsg.Text = "To GP % must be greater than From GP %";
                Page.ClientScript.RegisterStartupScript(this.GetType(), "alert", "<script> alert('To GP % must be greater than From GP %'); </script>");
                return;
            }

            if (string.IsNullOrEmpty(txtRemark.Text))
            {
                lblMsg.Text = "Please enter Remark ";
                Page.ClientScript.RegisterStartupScript(this.GetType(), "alert", "<script> alert('Please enter Remark '); </script>");
                return;
            }

            if (string.IsNullOrEmpty(txtRemarkInactive.Text) && BtnSave.Text == "Update" && chkActive.Checked == false)
            {
                lblMsg.Text = "Please enter Remark to Inactive";
                Page.ClientScript.RegisterStartupScript(this.GetType(), "alert", "<script> alert('Please enter Remark to Inactive'); </script>");
                return;
            }

            Db.constr = myGlobal.getAppSettingsDataForKey("tejSAP");
            string SQLqry = " select count(*) from Reward_GPRange where ( " + txtGPFrom.Text + " between GPPercentageFrom and GPPercentageTo ) OR ( " + txtGPTo.Text + " between GPPercentageFrom and GPPercentageTo )";

            int retval = Db.myExecuteScalar(SQLqry);

            if (retval > 0 && BtnSave.Text=="Save" )
            {
                lblMsg.Text = "GP Range is overlapping with Existing GP Range";
                Page.ClientScript.RegisterStartupScript(this.GetType(), "alert", "<script> alert('GP Range is overlapping with Existing GP Range'); </script>");
                return;
            }

            int Active = 0;
            if (chkActive.Checked == true)
            {
                Active = 1;
                txtRemarkInactive.Text = "";
            }
            else
            {
                Active = 0;
            }

            if (BtnSave.Text == "Save")
            {
                long GPRangeId = Db.myExecuteSQLReturnLatestAutoID("Insert into dbo.Reward_GPRange (GPPercentageFrom,GPPercentageTo,IsActive,Remark,CreatedOn,CreatedBy) Values (" + txtGPFrom.Text + "," + txtGPTo.Text + "," + Active + ",'" + txtRemark.Text + "',GETDATE(),'" + myGlobal.loggedInUser() + "')");

                lblMsg.Text = "Record Saved successfully";
            }
            else if (BtnSave.Text == "Update")
            {
                if (!string.IsNullOrEmpty(lblGPRangeID.Text))
                {
                    Db.myExecuteSQL(" Update dbo.Reward_GPRange Set IsActive=" + Active + " , InactiveRemark='" + txtRemarkInactive.Text + "'  Where GPRangeId= " + lblGPRangeID.Text);
                    lblMsg.Text = "Record Updated successfully";
                }
            }

            clearcontrol();
            pnlFormList.Visible = true;
            BindGrid();
            

        }
        catch (Exception ex)
        {
            lblMsg.Text = "Error in BtnSave_Click() : " + ex.Message;
        }
    }
    /// <summary>
    /// To clrear the form
    /// </summary>
    /// <param name="sender"></param>
    /// <param name="e"></param>
    protected void BtnCancel_Click(object sender, EventArgs e)
    {
        try
        {
            lblMsg.Text = "";
            pnlFormList.Visible = true;
            clearcontrol();
            BindGrid();
        }
        catch (Exception ex)
        {
            lblMsg.Text = "Error in BtnCancel_Click() : " + ex.Message;
        }
    }
    /// <summary>
    /// To display data on form controls to enable Update functionality
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
            Label lblID = (Label)row.FindControl("lblID");
            Label lblGPFrom = (Label)row.FindControl("lblGPFrom");
            Label lblGPTo = (Label)row.FindControl("lblGPTo");
            Label lblRemark = (Label)row.FindControl("lblRemark");
            Label lblInactiveRemark = (Label)row.FindControl("lblInactiveRemark");
            CheckBox chkIsActive = (CheckBox)row.FindControl("chkIsActive");
            string ID = lblID.Text;
            txtGPFrom.Text = lblGPFrom.Text;
            txtGPTo.Text = lblGPTo.Text;
            txtRemark.Text = lblRemark.Text;
            txtRemarkInactive.Text = lblInactiveRemark.Text;
            lblGPRangeID.Text = ID;
            if (chkIsActive.Checked)
            {
                chkActive.Checked = true;
                txtGPFrom.Enabled = false;
                txtGPTo.Enabled = false;
                txtRemark.Enabled = false;
            }
            else
            {
                chkActive.Checked = false;
                txtGPFrom.Enabled = true;
                txtGPTo.Enabled = true;
                txtRemark.Enabled = true;
            }
            pnlInactiveRemark.Visible = true;
        }
        catch (Exception ex)
        {
            lblMsg.Text = "Error in Gridview1_SelectedIndexChanged() : " + ex.Message;
        }
    }
    /// <summary>
    /// To Reset all controls on form
    /// </summary>
    private void clearcontrol()
    {
        BtnSave.Text = "Save";
        pnlInactiveRemark.Visible = false;
        txtGPFrom.Enabled = true;
        txtGPTo.Enabled = true;
        txtRemark.Enabled = true;
        txtGPFrom.Text = "";
        txtGPTo.Text = "";
        txtRemark.Text = "";
        Gridview1.SelectedIndex = -1;
    }

}