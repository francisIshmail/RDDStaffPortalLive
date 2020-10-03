using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Data;
using System.Text;

public partial class IntranetNew_Partners_RewardUserAuthorization : System.Web.UI.Page
{
    protected void Page_Load(object sender, EventArgs e)
    {
        
        try
        {
            if (!IsPostBack)
            {
                Db.constr = myGlobal.getAppSettingsDataForKey("tejSAP");
                int count = Db.myExecuteScalar("Select COUNT(*) from dbo.MenuWiseForms t0 Join dbo.UserAuthorization t1 on t0.MenuId=t1.MenuId and t1.MembershipUserName='" + myGlobal.loggedInUser() + "' And t0.FormURL='RewardUserAuthorization.aspx' and t1.IsActive=1");
                if (count > 0)
                {
                    BindDatabaseDDL(ddlDatabase);
                }
                else
                {
                    Response.Redirect("Default.aspx?UserAccess=0&FormName=Reward User Authorization");
                }
            }
        }
        catch (Exception ex)
        {
            lblMsg.Text = "Error Occured in Page_Load() :" + ex.Message;
        }
        
    }

    private void BindDatabaseDDL(DropDownList ddlDatabase)
    {
        try
        {
            Db.LoadDDLsWithCon(ddlDatabase, "Select '--Select--' as Name, '00' as 'Name' Union select Name,Name from sys.databases Where Name in ('SAPAE','SAPTZ','SAPUG','SAPKE','SAPZM')", "Name", "Name", myGlobal.getAppSettingsDataForKey("tejSAP"));
            if (ddlDatabase.Items.Count > 0)
                ddlDatabase.SelectedIndex = 0;
            else
            {
                ddlDatabase.Items.Add("No Rows");
                ddlDatabase.SelectedIndex = 0;
            }
        }
        catch (Exception ex)
        {
            lblMsg.Text = "Error occured in BindDatabaseDDL():" + ex.Message;
        }
    }

    private void BindGrid()
    {
        try
        {
            DataTable TblUserAuth = Db.myGetDS(" EXEC getReward_UserAuthorization '" + ddlDatabase.SelectedItem.Text + "'").Tables[0];
            if (TblUserAuth.Rows.Count > 0)
            {
                grvUserAuth.Visible = true;
                grvUserAuth.DataSource = TblUserAuth;
                grvUserAuth.DataBind();
            }
            else
            {
                lblMsg.Text = "No data found....";
            }
        }
        catch (Exception ex)
        {
            lblMsg.Text = "Error occured in BindGrid() : " + ex.Message;
        }
    }

    protected void ddlDatabase_SelectedIndexChanged(object sender, EventArgs e)
    {
        try
        {
            lblMsg.Text = "";

            grvUserAuth.Visible = false;

            if (ddlDatabase.SelectedIndex == 0 || ddlDatabase.SelectedItem.Text == "--Select--")
            {
                lblMsg.Text = "Please select SAP Database";
               
                Page.ClientScript.RegisterStartupScript(this.GetType(), "alert", "<script> alert('Please select SAP Database'); </script>");
                return;
            }

            BindGrid();

        }
        catch (Exception ex)
        {
            lblMsg.Text = "Error occured in ddlDatabase_SelectedIndexChanged() : " + ex.Message;
        }
    }

    protected void BtnSave_Click(object sender, EventArgs e)
    {
        try
        {
            lblMsg.Text = "";
            StringBuilder strBuilder = new StringBuilder();
            if (grvUserAuth.Rows.Count > 0)
            {
                for (int i = 0; i < grvUserAuth.Rows.Count; i++)
                {
                    string ActivateRewardID = grvUserAuth.DataKeys[i].Value.ToString();
                    string DBName = (grvUserAuth.Rows[i].FindControl("lblDBName") as Label).Text;
                    string SAPUserCode = (grvUserAuth.Rows[i].FindControl("lblSAPUSER_CODE") as Label).Text;
                    string UserName = (grvUserAuth.Rows[i].FindControl("lblU_NAME") as Label).Text;
                    bool IsAuthorize = (grvUserAuth.Rows[i].FindControl("chkIsAuthorize") as CheckBox).Checked;
                    int IsActivate = 0;
                    if (IsAuthorize)
                    {
                        IsActivate = 1;
                    }
                    strBuilder.Append("  if not exists ( select * from dbo.Reward_UserAuthorization Where DBName='" + DBName + "' And SAPUSER_CODE='" + SAPUserCode + "')  Begin  ");
                    strBuilder.Append("   Insert into dbo.Reward_UserAuthorization (DBName,SAPUSER_CODE,U_NAME,IsAuthorize,CreatedOn,CreatedBy) Values ( '" + DBName + "','" + SAPUserCode + "','"+ UserName   +"', "+ IsActivate.ToString() + " , GETDATE() , '" + myGlobal.loggedInUser() + "' ) ; End Else Begin ");
                    strBuilder.Append("  Update dbo.Reward_UserAuthorization Set  U_NAME = '" + UserName + "' , IsAuthorize= " + IsActivate.ToString() + " , LastUpdatedOn=GETDATE(),LastUpdatedBy='" + myGlobal.loggedInUser() + "' Where DBName='" + DBName + "' And SAPUSER_CODE='"+SAPUserCode+"' ; End  ");

                    if (strBuilder.Length > 2000)
                    {
                        Db.constr = myGlobal.getAppSettingsDataForKey("tejSAP");
                        Db.myExecuteSQL(strBuilder.ToString());
                        strBuilder.Length = 0;
                    }
                }

                if (strBuilder.Length > 0)
                {
                    Db.constr = myGlobal.getAppSettingsDataForKey("tejSAP");
                    Db.myExecuteSQL(strBuilder.ToString());
                    strBuilder.Length = 0;
                }

                lblMsg.Text = "Record Saved successfully";
                BindGrid();
            }
            else
            {
                lblMsg.Text = " No Data found to Save";
            }
        }
        catch (Exception ex)
        {
            lblMsg.Text = "Error occured in BtnSave_Click() : " + ex.Message;
        }

    }

    protected void BtnCancel_Click(object sender, EventArgs e)
    {
        lblMsg.Text = "";
        Response.Redirect("Default.aspx");
    }
  
}