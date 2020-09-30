using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Data;
using System.Web.Security;

public partial class Intranet_orders_grantRevokeMyRoleold : System.Web.UI.Page
{
    string loggedInUser;
    protected void Page_Load(object sender, EventArgs e)
    {
        lblMsg.Text = "";
        lblMsg.ForeColor = System.Drawing.Color.Red;
        loggedInUser = User.Identity.Name;
        lblLoggedUser.Text = loggedInUser;
        if (!IsPostBack)
        {
            TabContainer1.ActiveTabIndex=0;
            bindDdlAllUsers();
            bindRbListLoggedRoles();
            bindDdlRevokeUsers();
        }
    }

    protected void bindDdlAllUsers()
    {
        MembershipUserCollection muc = Membership.GetAllUsers();
        ddlAllUsers.DataSource = muc;
        ddlAllUsers.DataTextField = "UserName";
        ddlAllUsers.DataValueField = "UserName";
        ddlAllUsers.DataBind();
    }

    protected void bindRbListLoggedRoles()
    {
        string[] roles = Roles.GetRolesForUser(loggedInUser);
        rbListLoggedUserRoles.DataSource = roles;
        rbListLoggedUserRoles.DataBind();
    }

    protected void bindDdlRevokeUsers()
    {
        string sql;
        sql = "select distinct(toUser) from rolesGranted where grantingUser='"+loggedInUser+"'";
        DataTable dt = new DataTable();
        Db.constr = myGlobal.getIntranetDBConnectionString();
        dt = Db.myGetDS(sql).Tables[0];
        if (dt.Rows.Count > 0)
        {
            tblRevoke.Visible = true;
            lblRevokeUsers.Visible = false;
            ddlRevokeUsers.DataSource = dt;
            ddlRevokeUsers.DataTextField = "toUser";
            ddlRevokeUsers.DataValueField = "toUser";
            ddlRevokeUsers.DataBind();
            bindRbListRevokeRoles();
        }
        else
        {
            tblRevoke.Visible = false;
            lblRevokeUsers.Visible = true;
            lblRevokeUsers.Text = "No user had been granted role by you";
        }

    }

    protected void bindRbListRevokeRoles()
    {
        if (ddlRevokeUsers.Items.Count > 0)
        {
            string sql;
            sql = "select grantingRole from rolesGranted where grantingUser='" + loggedInUser + "' and toUser='" + ddlRevokeUsers.SelectedItem.Text + "'";
            Db.constr = myGlobal.getIntranetDBConnectionString();
            DataTable dtRoles = new DataTable();
            dtRoles = Db.myGetDS(sql).Tables[0];
            rbListRevokeRoles.DataSource = dtRoles;
            rbListRevokeRoles.DataTextField = "grantingRole";
            rbListRevokeRoles.DataValueField = "grantingRole";
            rbListRevokeRoles.DataBind();
        }

    }


    protected void btnAssign_Click(object sender, EventArgs e)
    {
        string selectedUser, selectedRole;
        string sql;
        selectedUser = ddlAllUsers.SelectedItem.Text;
        
        if (rbListLoggedUserRoles.SelectedIndex < 0)
        {
            lblMsg.Text = "Please select a role to be assigned";
            return;
        }
        else
        {
            selectedRole = rbListLoggedUserRoles.SelectedItem.Text;
        }
        if (isSelectedUserOnRole(selectedUser, selectedRole) == true)
        {
            lblMsg.Text = "User is already on selected role";
            addUserToDbTable(selectedUser, selectedRole, loggedInUser,"YES");
            return;
        }

        Roles.AddUserToRole(selectedUser, selectedRole);
        addUserToDbTable(selectedUser, selectedRole, loggedInUser,"NO");
    }

    protected bool isSelectedUserOnRole(string username, string selectedRole)
    {
        string[] selectedUserRoles = Roles.GetRolesForUser(username);
        int indx = Array.IndexOf(selectedUserRoles, selectedRole);
        if (indx == -1)
        {
            return false;
        }
        else
        {
            return true;
        }
    }
    protected void ddlRevokeUsers_SelectedIndexChanged(object sender, EventArgs e)
    {
        bindRbListRevokeRoles();
    }

    protected void addUserToDbTable(string user,string role,string grantingUser,string isRoleByDefault)
    {
        string sql;
        string isRoleByDefaultCalculatedStatus = isRoleByDefault;

        sql = "select * from rolesGranted where grantingRole='" + role + "' and toUser='" + user + "' order by lastUpdated";
        Db.constr = myGlobal.getIntranetDBConnectionString();
        DataTable dt = new DataTable();
        dt = Db.myGetDS(sql).Tables[0];
        int cnt = dt.Rows.Count;
        if (cnt > 0)
        {
            isRoleByDefaultCalculatedStatus = dt.Rows[0]["IsRoleByDefault"].ToString();
        }

        sql = "insert into dbo.rolesGranted(grantingUser,grantingRole,toUser,isRoleByDefault) values('" + grantingUser + "','" + role + "','" + user + "','" + isRoleByDefaultCalculatedStatus + "')";
        Db.constr = myGlobal.getIntranetDBConnectionString();
        try
        {
            Db.myExecuteSQL(sql);
            lblMsg.ForeColor = System.Drawing.Color.Green;
            lblMsg.Text = "Role:" + role + " successfully granted to user:" + user + "";
        }
        catch (Exception ex)
        {
            lblMsg.Text = "Error:" + ex.Message;
        }


    }

    protected void btnRevoke_Click(object sender, EventArgs e)
    {
        string revokeUser, revokeRole;
        revokeUser = ddlRevokeUsers.SelectedItem.Text;
        if (rbListRevokeRoles.SelectedIndex >= 0)
        {
            revokeRole = rbListRevokeRoles.SelectedItem.Text;
        }
        else
        {
            lblMsg.Text = "Please select a role to be revoked";
            return;
        }

        //check if need to revoke role from membership
        string sql,isRoleByDefaultStatus;
        sql = "select * from rolesGranted where toUser='"+revokeUser+"' and grantingRole='"+revokeRole+"' order by lastUpdated";
        Db.constr = myGlobal.getIntranetDBConnectionString();
        DataTable dt = new DataTable();
        dt = Db.myGetDS(sql).Tables[0];
        int count = dt.Rows.Count;
        isRoleByDefaultStatus = dt.Rows[0]["isRoleByDefault"].ToString();

        if (count==1 && isRoleByDefaultStatus == "NO")
        {
            Roles.RemoveUserFromRole(revokeUser, revokeRole);
        }

        //remove from database table
        sql = "delete from rolesGranted where grantingUser='" + loggedInUser + "' and grantingRole='" + revokeRole + "' and toUser='" + revokeUser + "'";
        Db.constr = myGlobal.getIntranetDBConnectionString();
        try
        {
            Db.myExecuteSQL(sql);
            lblMsg.ForeColor = System.Drawing.Color.Green;
            lblMsg.Text = "Role:" + revokeUser + " succesfully revoked from user:" + revokeUser + "";
            bindDdlRevokeUsers();
        }
        catch (Exception ex)
        {
            lblMsg.Text = "Error:" + ex.Message;
        }
    }
    protected void TabContainer1_ActiveTabChanged(object sender, EventArgs e)
    {
        bindDdlRevokeUsers();
    }
}