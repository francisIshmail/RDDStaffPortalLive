using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Data;

public partial class IntranetNew_Targets_EmailConfigurationSetting : System.Web.UI.Page
{
    string emailids = "";
    protected void Page_Load(object sender, EventArgs e)
    {
        if (!IsPostBack)
        {
            Db.constr = myGlobal.getAppSettingsDataForKey("tejSAP");
            int count = Db.myExecuteScalar("Select COUNT(*) from dbo.EmailConfig");
            if (count > 0)
            {
                string sql;
                sql = "select * from dbo.EmailConfig";
                Db.constr = myGlobal.getAppSettingsDataForKey("tejSAP");
                DataTable dtemail = new DataTable();
                dtemail = Db.myGetDS(sql).Tables[0];
                txtmailids.Text = dtemail.Rows[0][0].ToString();
               // enteredemails.Text = dtemail.Rows[0][0].ToString();
            }
        }
    }
    protected void BtnSave_Click(object sender, EventArgs e)
    {
        try
        {

            if (string.IsNullOrEmpty(txtmailids.Text))
            {
                lblMsg.Text = "Please Configured Email Ids";
                Page.ClientScript.RegisterStartupScript(this.GetType(), "alert", "<script> alert('Please Configured Email Ids'); </script>");
                return;
            }


            Db.constr = myGlobal.getAppSettingsDataForKey("tejSAP");



            if (BtnSave.Text == "Save")
            {
                // string emailids = txtmailids.Text.Replace(',', ';');
                // long LeavePeriodID =
                Db.constr = myGlobal.getAppSettingsDataForKey("tejSAP");
                int count = Db.myExecuteScalar("Select COUNT(*) from dbo.EmailConfig");
                if (count > 0)
                {
                    Db.myExecuteSQL("Update dbo.EmailConfig Set receipient_emailId='" + txtmailids.Text + "',LastUpdatedOn=GETDATE() , LastUpdatedBy='" + myGlobal.loggedInUser() + "' ");
                  lblMsg.Text = " Email Configuration saved successfully";
                }
                else
                {
                    Db.myExecuteSQL("Insert into dbo.EmailConfig (receipient_emailId,CreatedBy,CreatedOn) Values ('" + txtmailids.Text + "', '" + myGlobal.loggedInUser() + "',GETDATE())");


                    lblMsg.Text = " Email Configuration saved successfully";

                }
               
                ClearControl();
            }
        }
        catch (Exception ex)
        {
            lblMsg.Text = "Error in BtnSave_Click() : " + ex.Message;
        }
    }
    protected void BtnCancel_Click(object sender, EventArgs e)
    {
        txtmailids.Text = "";

    }
    public void ClearControl()
    {
       
        txtmailids.Text = "";
        //enteredemails.Text = "";
      
        BtnSave.Text = "Save";
       
    }

    //protected void addemail_Click(object sender, EventArgs e)
    //{
    //    emailids = txtmailids.Text + ";";
    //    enteredemails.Text = enteredemails.Text + emailids;
    //    txtmailids.Text = "";

    //}
}