using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Data;
using System.Data.SqlClient;

public partial class IntranetNew_Admin_BPGlobalSetting : System.Web.UI.Page
{
    protected void Page_Load(object sender, EventArgs e)
    {
        if (!IsPostBack)
        {
            try
            {
                Db.constr = myGlobal.getAppSettingsDataForKey("tejSAP");
                int count = Db.myExecuteScalar("Select COUNT(*) from dbo.MenuWiseForms t0 Join dbo.UserAuthorization t1 on t0.MenuId=t1.MenuId and t1.MembershipUserName='" + myGlobal.loggedInUser() + "' And t0.FormURL='BPGlobalSetting.aspx' And t1.IsActive=1");
                if (count > 0)
                {
                    BindData();
                }
                else
                {
                    Response.Redirect("Default.aspx?UserAccess=0&FormName=Customer Status Global Setting");
                }
            }
            catch (Exception ex)
            {
                lblMsg.Text = "Error occured on Page_Load : " + ex.Message;
            }
        }
    }

    private void BindData()
    {
        try
        {
            Db.constr = myGlobal.getAppSettingsDataForKey("tejSAP");
            DataTable DTGSettings = Db.myGetDS("select * from dbo.BPStatus_GlobalSettings").Tables[0];
            if (DTGSettings.Rows.Count > 0)
            {
                if (!DBNull.Value.Equals(DTGSettings.Rows[0]["GracePeriodforCashBP"]))
                    txtGracePforCashBP.Text = DTGSettings.Rows[0]["GracePeriodforCashBP"].ToString();
                if (!DBNull.Value.Equals(DTGSettings.Rows[0]["GracePeriodforLCBP"]))
                    txtGracePeriodforLCBP.Text = DTGSettings.Rows[0]["GracePeriodforLCBP"].ToString();
                if (!DBNull.Value.Equals(DTGSettings.Rows[0]["GracePeriodforPDCBP"]))
                    txtGracePeriodforPDCBP.Text = DTGSettings.Rows[0]["GracePeriodforPDCBP"].ToString();
                if (!DBNull.Value.Equals(DTGSettings.Rows[0]["MaxAllowableDaysforTempCL"]))
                    txtMaxAllowableDaysForTempCL.Text = DTGSettings.Rows[0]["MaxAllowableDaysforTempCL"].ToString();
                
                if (!DBNull.Value.Equals(DTGSettings.Rows[0]["CAMaxAllowableDaysforTempCL"]))
                    txtCAMaxAllowableDaysForTempCL.Text = DTGSettings.Rows[0]["CAMaxAllowableDaysforTempCL"].ToString();

                if (!DBNull.Value.Equals(DTGSettings.Rows[0]["CMMaxAllowableDaysforTempCL"]))
                    txtCMMaxAllowableDaysForTempCL.Text = DTGSettings.Rows[0]["CMMaxAllowableDaysforTempCL"].ToString();

                if (!DBNull.Value.Equals(DTGSettings.Rows[0]["GracePeriodSoftToActive"]))
                    txtGraceForSoftToActive.Text = DTGSettings.Rows[0]["GracePeriodSoftToActive"].ToString();

                if (!DBNull.Value.Equals(DTGSettings.Rows[0]["AllowableCLExpiryMonthCanExtend"]))
                {
                    try
                    {
                        ddlCLExpiryExtnInMonth.SelectedItem.Text = DTGSettings.Rows[0]["AllowableCLExpiryMonthCanExtend"].ToString();
                        ddlCLExpiryExtnInMonth.SelectedItem.Value = DTGSettings.Rows[0]["AllowableCLExpiryMonthCanExtend"].ToString();

                        ddlAllowToSetCLExtensionBeforeMonths.SelectedItem.Text = DTGSettings.Rows[0]["AllowToSetCLExtensionBeforeMonths"].ToString();
                        ddlAllowToSetCLExtensionBeforeMonths.SelectedItem.Value = DTGSettings.Rows[0]["AllowToSetCLExtensionBeforeMonths"].ToString();
                    }
                    catch { }
                }

                BtnSave.Text = "Update";
            }
            else
            {
                BtnSave.Text = "Save";
            }
        }
        catch (Exception ex)
        {
            lblMsg.Text = "Error occured on BindData() : " + ex.Message;
        }
    }

    protected void BtnSave_Click(object sender, EventArgs e)
    {
        try
        {
            lblMsg.Text = "";
            int GracePeriodForCashBP=0, GracePeriodForLCBP=0, GracePeriodForPDCBP=0;
            if (string.IsNullOrEmpty(txtGracePforCashBP.Text) && string.IsNullOrEmpty(txtGracePeriodforLCBP.Text) && string.IsNullOrEmpty(txtGracePeriodforPDCBP.Text))
            {
                lblMsg.Text = " Please enter data for atleast one field";
                return;
            }

            if (string.IsNullOrEmpty(txtMaxAllowableDaysForTempCL.Text) )
            {
                lblMsg.Text = " Please enter Maximum allowable days for temp Credit Limit expiry date" ;
                return;
            }

            if (string.IsNullOrEmpty(txtCAMaxAllowableDaysForTempCL.Text))
            {
                lblMsg.Text = " Please enter CA - Maximum allowable days for temp Credit Limit expiry date";
                return;
            }

            if (Convert.ToInt32(txtCAMaxAllowableDaysForTempCL.Text) > 30)
            {
                lblMsg.Text = " Maximum allowable days to set temp CL Expiry Date for CA is 30 days  ";
                return;
            }

            if (string.IsNullOrEmpty(txtCMMaxAllowableDaysForTempCL.Text))
            {
                lblMsg.Text = " Please enter CM - Maximum allowable days for temp Credit Limit expiry date";
                return;
            }

            if (Convert.ToInt32(txtCMMaxAllowableDaysForTempCL.Text) > 45)
            {
                lblMsg.Text = " Maximum allowable days to set temp CL Expiry Date for CM is 45 days ";
                return;
            }

            if (ddlCLExpiryExtnInMonth.SelectedItem.Text == "--SELECT--")
            {
                lblMsg.Text = " Please select Allowable extension in months for CL Expiry ( After or Before CL is expired ).";
                return;
            }

            if (ddlAllowToSetCLExtensionBeforeMonths.SelectedItem.Text == "--SELECT--")
            {
                lblMsg.Text = " Please select CL Expiry extension can be set before month(s).";
                return;
            }

            if (!string.IsNullOrEmpty(txtGracePforCashBP.Text))
            {
                GracePeriodForCashBP = Convert.ToInt32(txtGracePforCashBP.Text);
            }
            if (!string.IsNullOrEmpty(txtGracePeriodforLCBP.Text))
            {
                GracePeriodForLCBP = Convert.ToInt32(txtGracePeriodforLCBP.Text);
            }
            if (!string.IsNullOrEmpty(txtGracePeriodforPDCBP.Text))
            {
                GracePeriodForPDCBP = Convert.ToInt32(txtGracePeriodforPDCBP.Text);
            }



            Db.constr = myGlobal.getAppSettingsDataForKey("tejSAP");
            if (BtnSave.Text == "Save")
            {
                Db.myExecuteSQL(" Insert into dbo.BPStatus_GlobalSettings(GracePeriodforCashBP,GracePeriodforLCBP,GracePeriodforPDCBP,MaxAllowableDaysforTempCL,AllowableCLExpiryMonthCanExtend,AllowToSetCLExtensionBeforeMonths,CreatedOn,CreatedBy,CAMaxAllowableDaysforTempCL,CMMaxAllowableDaysforTempCL, GracePeriodSoftToActive ) Values (" + GracePeriodForCashBP.ToString() + "," + GracePeriodForLCBP.ToString() + "," + GracePeriodForPDCBP.ToString() + "," + txtMaxAllowableDaysForTempCL.Text + "," + ddlCLExpiryExtnInMonth.SelectedItem.Text + "," + ddlAllowToSetCLExtensionBeforeMonths.SelectedItem.Text + ", getdate(),'" + myGlobal.loggedInUser() + "'," + txtCAMaxAllowableDaysForTempCL.Text + "," + txtCMMaxAllowableDaysForTempCL.Text + ","+txtGraceForSoftToActive.Text+") ");
                lblMsg.Text = "Record saved successfully";
                BindData();
            }
            else if (BtnSave.Text == "Update")
            {
                Db.myExecuteSQL(" Update dbo.BPStatus_GlobalSettings set GracePeriodforCashBP = " + GracePeriodForCashBP.ToString() + ",GracePeriodforLCBP = " + GracePeriodForLCBP.ToString() + " ,GracePeriodforPDCBP =" + GracePeriodForPDCBP.ToString() + ",MaxAllowableDaysforTempCL=" + txtMaxAllowableDaysForTempCL.Text + ",AllowableCLExpiryMonthCanExtend=" + ddlCLExpiryExtnInMonth.SelectedItem.Text + ",AllowToSetCLExtensionBeforeMonths=" + ddlAllowToSetCLExtensionBeforeMonths.SelectedItem.Text + ", LastUpdatedOn=getdate(),LastUpdatedBy='" + myGlobal.loggedInUser() + "' , CAMaxAllowableDaysforTempCL=" + txtCAMaxAllowableDaysForTempCL.Text + " , CMMaxAllowableDaysforTempCL = " + txtCMMaxAllowableDaysForTempCL.Text + " , GracePeriodSoftToActive= "+ txtGraceForSoftToActive.Text );
                lblMsg.Text = "Record updated successfully";
            }
        }
        catch (Exception ex )
        {
            lblMsg.Text = "Error occured in Save : " + ex.Message;
        }
    }
}