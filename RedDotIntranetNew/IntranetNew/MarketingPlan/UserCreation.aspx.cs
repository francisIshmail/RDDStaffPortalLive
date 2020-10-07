using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Web.Security;
using System.Text.RegularExpressions;
using System.Collections;
using System.Data;
using System.Data.SqlClient;

public partial class IntranetNew_MarketingPlan_UserCreation : System.Web.UI.Page
{
    string query = "";
    protected void Page_Load(object sender, EventArgs e)
    {
       
        if (!IsPostBack)
        {
                Db.constr = myGlobal.getAppSettingsDataForKey("tejSAP");
                int count = Db.myExecuteScalar("Select COUNT(*) from dbo.MenuWiseForms t0 Join dbo.UserAuthorization t1 on t0.MenuId=t1.MenuId and t1.MembershipUserName='" + myGlobal.loggedInUser() + "' And t0.FormURL='UserCreation.aspx' And t1.IsActive=1");
                if (count > 0)
                {
                    UsersDDL();
                    fillgv();
                }
                else
                {
                    Response.Redirect("Default.aspx?UserAccess=0&FormName=Marketing Setup");
                }
        }
    }
    public DataTable CustomGetAllUsers()
    {
        DataSet ds = new DataSet();

        DataTable dt = new DataTable();

        MembershipUserCollection muc;
        muc = Membership.GetAllUsers();

        dt.Columns.Add("UserName", Type.GetType("System.String"));
        dt.Columns.Add("Email", Type.GetType("System.String"));
        dt.Columns.Add("CreationDate", Type.GetType("System.DateTime"));

        /* Here is the list of columns returned of the Membership.GetAllUsers() method
         * UserName, Email, PasswordQuestion, Comment, IsApproved
         * IsLockedOut, LastLockoutDate, CreationDate, LastLoginDate
         * LastActivityDate, LastPasswordChangedDate, IsOnline, ProviderName
         */

        foreach (MembershipUser mu in muc)
        {
            DataRow dr;
            dr = dt.NewRow();
            dr["UserName"] = mu.UserName;
            dr["Email"] = mu.Email;
            dr["CreationDate"] = mu.CreationDate;
            dt.Rows.Add(dr);
        }
        return dt;
    }

    private void UsersDDL()
    {
        DataTable dtbl = CustomGetAllUsers();

        ddlorignator.DataSource = dtbl;
        ddlorignator.DataTextField = dtbl.Columns["UserName"].ToString();
        ddlorignator.DataValueField = dtbl.Columns["UserName"].ToString();
        ddlorignator.DataBind();

        ddlapprover.DataSource = dtbl;
        ddlapprover.DataTextField = dtbl.Columns["UserName"].ToString();
        ddlapprover.DataValueField = dtbl.Columns["UserName"].ToString();
        ddlapprover.DataBind();

    }


  

    protected void btnsave_Click(object sender, EventArgs e)
    {
        Db.constr = myGlobal.getAppSettingsDataForKey("tejSAP");
        string orinator = ddlorignator.SelectedItem.Text;

        DataSet ds = Db.myGetDS("select Approver from Marketing_Authentication where originator='" + orinator + "'");
        if (ds.Tables[0].Rows.Count > 0)
        {
            query = "Update Marketing_Authentication set Approver='" + ddlapprover.SelectedItem.Text + "' where originator='" + orinator + "'";
            Db.myExecuteSQL(query);
            Page.ClientScript.RegisterStartupScript(this.GetType(), "alert", "<script> alert('saved successfully.'); </script>");
            fillgv();
        }
        else
        {
            query = "insert into Marketing_Authentication(originator,Approver,Flag)values('" + ddlorignator.SelectedItem.Text + "','" + TextBox1.Text + "','A')";
            Db.myExecuteSQL(query);
            Page.ClientScript.RegisterStartupScript(this.GetType(), "alert", "<script> alert('saved successfully.'); </script>");
            fillgv();
        }
    }

    protected void ddlapprover_SelectedIndexChanged1(object sender, EventArgs e)
    {
        string items = string.Empty;
        foreach (ListItem i in ddlapprover.Items)
        {
            if (i.Selected == true)
            {
                items += i.Text + ",";
            }
        }
        TextBox1.Text = items;
    }

    protected void fillgv()
    {
        Db.constr = myGlobal.getAppSettingsDataForKey("tejSAP");
         DataSet ds = Db.myGetDS("select * from Marketing_Authentication where Flag='A'");
       
      GvData.DataSource = ds;
      GvData.DataBind();
          
    }
    protected void lnkdelete_Click(object sender, EventArgs e)
    {
        int id = Convert.ToInt32((((Label)(((LinkButton)sender).NamingContainer as GridViewRow).FindControl("lblauthid")).Text));
        string query = "Update Marketing_Authentication set  Flag='D' where  Authentication_ID= '" + id + "'";
        Db.myExecuteSQL(query);
        Page.ClientScript.RegisterStartupScript(this.GetType(), "alert", "<script> alert('Deleted successfully.'); </script>");
        fillgv();


    }
 
}