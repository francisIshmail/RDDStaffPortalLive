using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Data;
using System.Text;
using System.Drawing;

public partial class IntranetNew_Admin_Authorization : System.Web.UI.Page
{
    protected void Page_Load(object sender, EventArgs e)
    {
        if (!IsPostBack)
        {
            Db.constr = myGlobal.getAppSettingsDataForKey("tejSAP");
            int count = Db.myExecuteScalar("Select COUNT(*) from dbo.MenuWiseForms t0 Join dbo.UserAuthorization t1 on t0.MenuId=t1.MenuId and t1.MembershipUserName='" + myGlobal.loggedInUser() + "' And t0.FormURL='Authorization.aspx' and t1.IsActive=1");
            if (count > 0)
            {
                BindUserDDL(ddlUser);
            }
            else
            {
                Response.Redirect("Default.aspx?UserAccess=0&FormName=User Authorization");
            }
        }
        lblMsg.Text = "";
    }

    private void BindUserDDL(DropDownList ddlUser)
    {
        try
        {
            Db.LoadDDLsWithCon(ddlUser, "select u.UserName from aspnet_Users u	Join dbo.aspnet_Membership m On u.ApplicationId=m.ApplicationId and u.UserId=m.UserId and m.IsLockedOut=0 ", "UserName", "UserName", myGlobal.getMembershipDBConnectionString());
            if (ddlUser.Items.Count > 0)
                ddlUser.SelectedIndex = 0;
            else
            {
                ddlUser.Items.Add("No Rows");
                ddlUser.SelectedIndex = 0;
            }
        }
        catch (Exception ex)
        {
            lblMsg.Text = "Error in BindUserDDL() : " + ex.Message;
        }
    }

    protected void ddlUser_SelectedIndexChanged(object sender, EventArgs e)
    {
        try
        {
            lblMsg.Text = "";
            Db.constr = myGlobal.getAppSettingsDataForKey("tejSAP");
            DataSet DsForms = Db.myGetDS(" EXEC getMenusForUserAuthorization  '" + ddlUser.SelectedItem.Text + "'");
            if (DsForms.Tables.Count > 0)
            {
                Gridview1.DataSource = DsForms.Tables[0];
                Gridview1.DataBind();
            }
        }
        catch (Exception ex)
        {
            lblMsg.Text = "Error in ddlUser_SelectedIndexChanged() : " + ex.Message;
        }
    }

    protected void BtnCancel_Click(object sender, EventArgs e)
    {
        lblMsg.Text = "";
        Gridview1.DataSource = null;
    }

    protected void BtnSave_Click1(object sender, EventArgs e)
    {
        try
        {
            lblMsg.ForeColor = Color.Red;
            if (Gridview1.Rows.Count == 0)
            {
                lblMsg.Text = "No user authorization to to save.";
            }

            lblMsg.ForeColor = Color.Purple;
            lblMsg.Text = " Please wait....setting user authorization.";
            //for ( int i=0;i<Gridview1.Rows.Count;i++)
            foreach (GridViewRow gvrow in Gridview1.Rows)
            {
                Label lblID = (Label)gvrow.FindControl("lblID");
                CheckBox chkIsActive = (CheckBox)gvrow.FindControl("chkIsActive");

                string qry = " EXEC setUserAuthorization " + lblID.Text + ",'" + ddlUser.SelectedItem.Text + "','" + myGlobal.loggedInUser() + "'," + chkIsActive.Checked;
                Db.constr = myGlobal.getAppSettingsDataForKey("tejSAP");
                Db.myExecuteSQL(qry);
            }
            lblMsg.ForeColor = Color.Green;
            lblMsg.Text = "User Authorization saved successully";
        }
        catch (Exception ex)
        {
            lblMsg.ForeColor = Color.Red;
            lblMsg.Text = "Error in BtnSave_Click() : " + ex.Message;
        }

    }
}