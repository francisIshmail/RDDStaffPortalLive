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
public partial class Admin_userCreationMembershipDatabase : System.Web.UI.Page
{
    string[] arrRoles;
    string PwdToAccess = "1111111111";
    
    protected void Page_Load(object sender, EventArgs e)
    {
        txtUserPassword.Text = "";
        //txtUserPassword1.Text = "";
        lblErrorMsg.Text = "";

       txtPwd.Text = PwdToAccess;

        btnDeleteRole.Attributes.Add("onClick", "return getConfirmation();");
        btnDeleteUser.Attributes.Add("onClick", "return getConfirmation();");
        btnUnAssignUserRoles.Attributes.Add("onClick", "return getConfirmation();");

        btnRole.Attributes.Add("onClick", "return getConfirmation();");
        BtnUser.Attributes.Add("onClick", "return getConfirmation();");
        btnAssignUserRoles.Attributes.Add("onClick", "return getConfirmation();");

        if (!IsPostBack)
        {
            updateRoleDDL();
            updateUsersDDL();
        }

        lblMsg.Text = "";
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

    private void updateUsersDDL()
    {
        DataTable dtbl=CustomGetAllUsers();
        ddlUser2.DataSource = dtbl;
        ddlUser2.DataTextField = dtbl.Columns["UserName"].ToString();
        ddlUser2.DataValueField = dtbl.Columns["UserName"].ToString();
        ddlUser2.DataBind();
        lblUserCount2.Text = ddlUser2.Items.Count.ToString();

        ddlDeleteUser.DataSource = dtbl;
        ddlDeleteUser.DataTextField = dtbl.Columns["UserName"].ToString();
        ddlDeleteUser.DataValueField = dtbl.Columns["UserName"].ToString();
        ddlDeleteUser.DataBind();
        lblDeleteUser.Text = ddlDeleteUser.Items.Count.ToString();

        ddlUserUnAssign.DataSource = dtbl;
        ddlUserUnAssign.DataTextField = dtbl.Columns["UserName"].ToString();
        ddlUserUnAssign.DataValueField = dtbl.Columns["UserName"].ToString();
        ddlUserUnAssign.DataBind();
        lblUserCountUnAssign.Text = ddlUserUnAssign.Items.Count.ToString();

        //if (ddlUser2.SelectedIndex >= 0)
            bindddlExistingRolesOfUsers();
    }

    private void updateRoleDDL()
    {
        arrRoles = Roles.GetAllRoles();
       
        ddlRoles.DataSource = arrRoles;
        ddlRoles.DataBind();
        lblRoleCount.Text = ddlRoles.Items.Count.ToString();

        ddlRoles2.DataSource = arrRoles;
        ddlRoles2.DataBind();
        lblRoleCount2.Text = ddlRoles.Items.Count.ToString();

        ddlDeleteRole.DataSource = arrRoles;
        ddlDeleteRole.DataBind();
        lblDeleteRoleCount.Text = ddlDeleteRole.Items.Count.ToString();
    }
    protected void btnDeleteRole_Click(object sender, EventArgs e)
    {
        lblMsg.Text = "";

        if (txtPwd.Text.Trim() != PwdToAccess)
        {
            lblMsg.Text = "Invalid Password! Not Autorized to work on this page, retry supplying correct password";
            return; //user exists
        }

        //return;

        if (ddlDeleteRole.SelectedIndex<0)
        {
            lblMsg.Text = "Error! No Role selected to be deleted";
            return; //user exists
        }
        if (Roles.RoleExists(ddlDeleteRole.SelectedItem.Text))
        {
            if (ddlDeleteRole.SelectedItem.Text == "Admin")
            {
                lblMsg.Text = "Sorry! Role 'admin' can't be deleted";
                return; //user exists
            }

            Db.constr = System.Configuration.ConfigurationManager.ConnectionStrings["LocalSqlServer"].ConnectionString;
            string qry = "BEGIN TRANSACTION; delete from dbo.aspnet_UsersInRoles where RoleId=(select RoleId from dbo.aspnet_Roles where roleName='" + ddlDeleteRole.SelectedItem.Text + "'); delete from dbo.aspnet_Roles where roleName='" + ddlDeleteRole.SelectedItem.Text + "'; COMMIT TRANSACTION;";
            Db.myExecuteSQL(qry);
            lblMsg.Text = "All users unassigned from Role '" + ddlDeleteRole.SelectedItem.Text + "' , Role deleted Successfully";
            updateRoleDDL();
            bindddlExistingRolesOfUsers();
        }
        else
            lblMsg.Text = "Error! Role Not found in Database";

    }

    protected void btnRole_Click(object sender, EventArgs e)
    {
        lblMsg.Text = "";

        if (txtPwd.Text.Trim() != PwdToAccess)
        {
            lblMsg.Text = "Invalid Password! Not Autorized to work on this page, retry supplying correct password";
            return; //user exists
        }
        
        //return;

        if (txtRole.Text.Trim() == "")
        {
            lblMsg.Text = "Error! Role field can't be empty";
            return; //user exists
        }
        if (!Roles.RoleExists(txtRole.Text.Trim()))
        {
            Roles.CreateRole(txtRole.Text.Trim());
            updateRoleDDL();
            lblMsg.Text = "Role '" + txtRole.Text.Trim()  + "' Created Successfully";
        }
        else
            lblMsg.Text = "Error! Role Already Exists in Database";

        txtRole.Text = "";
    }
    protected void btnDeleteUser_Click(object sender, EventArgs e)
    {
        lblMsg.Text = "";

        if (txtPwd.Text.Trim() != PwdToAccess)
        {
            lblMsg.Text = "Invalid Password! Not Autorized to work on this page, retry supplying correct password";
            return; //user exists
        }

        //return;

        if (ddlDeleteUser.SelectedIndex < 0)
        {
            lblMsg.Text = "Error! No User selected to be deleted";
            return; //user exists
        }
        
        if(Membership.FindUsersByName(ddlDeleteUser.SelectedItem.Text)!=null)
        {
            if (ddlDeleteUser.SelectedItem.Text == "admin")
            {
                lblMsg.Text = "Sorry! User 'admin' can't be deleted";
                return; //user exists
            }
            Db.constr = System.Configuration.ConfigurationManager.ConnectionStrings["LocalSqlServer"].ConnectionString;
            string qry = "BEGIN TRANSACTION; delete from dbo.aspnet_UsersInRoles where UserId=(select UserId from dbo.aspnet_Users where UserName='" + ddlDeleteUser.SelectedItem.Text + "');delete from dbo.aspnet_Membership where UserId=(select UserId from dbo.aspnet_Users where UserName='" + ddlDeleteUser.SelectedItem.Text + "'); delete from dbo.aspnet_Users where UserName='" + ddlDeleteUser.SelectedItem.Text + "';COMMIT TRANSACTION;";
            Db.myExecuteSQL(qry);
            lblMsg.Text = "User '" + ddlDeleteUser.SelectedItem.Text + "' deleted Successfully";
            updateUsersDDL();
            bindddlExistingRolesOfUsers();
        }
        else
            lblMsg.Text = "Error! User Not found in Database";

    }
    protected void BtnUser_Click(object sender, EventArgs e)
    {
        lblMsg.Text = "";

        if (txtPwd.Text.Trim() != PwdToAccess)
        {
            lblMsg.Text = "Invalid Password! Not Autorized to work on this page, retry supplying correct password";
            return; //user exists
        }

        //return;

        try
        {
            if (txtUser.Text.Trim()=="")
            {
                lblMsg.Text = "Error! User name can't be empty";
                return; //user exists
            }

            if (!Util.IsValidEmail(txtEmail.Text))
            {
                lblMsg.Text = "Error! Invalid Email id, kindly retry with correct email id format .";
                return; //user exists
            }

            if (ddlRoles.SelectedIndex < 0)
            {
                lblMsg.Text = "Error! Please select a Role for the User you are trying to create";
                return; //user exists
            }

            if (Membership.GetUserNameByEmail(txtEmail.Text) != null) //if it gets some user name, it is already registered
            {
                lblMsg.Text = "Error! Desired Email is already registered. Please try with other emailID.";
                return; //user exists
            }

            if (!addUserToMembershipDatabase(txtUser.Text.Trim(), txtEmail.Text.Trim(), "color", "blue", ddlRoles.SelectedItem.Text)) //chech for user id availablity and create user in membership db
            {
                lblMsg.Text = "Error! Desired Login Id is not available, please try another and Submit.";
                return;
            }

            updateUsersDDL();
            txtUser.Text = "";
            txtEmail.Text = "";
        }
        catch (Exception exxxp)
        {
            lblMsg.Text = "Error! " + exxxp.Message;
        }
    }
    protected void ddlUser2_SelectedIndexChanged(object sender, EventArgs e)
    {
        bindddlExistingRolesOfUsers();
    }
    protected void ddlUserUnAssign_SelectedIndexChanged(object sender, EventArgs e)
    {
        bindddlExistingRolesOfUsers();
    }

    protected void btnUnAssignUserRoles_Click(object sender, EventArgs e)
    {
        lblMsg.Text = "";

        if (txtPwd.Text.Trim() != PwdToAccess)
        {
            lblMsg.Text = "Invalid Password! Not Autorized to work on this page, retry supplying correct password";
            return; //user exists
        }


        if (ddlUserRolesUnAssign.SelectedIndex<0)
        {
            lblMsg.Text = "Error! Role not selected to be unassigned";
            return; //user exists
        }

        if (ddlUserUnAssign.SelectedIndex < 0)
        {
            lblMsg.Text = "Error! User not selected to be unassigned a role";
            return; //user exists
        }

        if (Membership.FindUsersByName(ddlUserUnAssign.SelectedItem.Text) == null)
        {
            lblMsg.Text = "Error! User Does not Exists in Database, refresh page for the latest state";
            return;
        }

        if (!Roles.RoleExists(ddlUserRolesUnAssign.SelectedItem.Text))
        {
            lblMsg.Text = "Error! Role Does not Exists in Database, refresh page for the latest state";
        }

        Roles.RemoveUserFromRole(ddlUserUnAssign.SelectedItem.Text, ddlUserRolesUnAssign.SelectedItem.Text);
        lblMsg.Text = "Role '" + ddlUserRolesUnAssign.SelectedItem.Text + "' un-assigned to user '" + ddlUserUnAssign.SelectedItem.Text + "' Successfully";
        bindddlExistingRolesOfUsers(); 
    }

    private void bindddlExistingRolesOfUsers()
    {
        ddlUserRoles.DataSource = null;
        ddlUserRoles.Items.Clear();
        if (ddlUser2.SelectedIndex >= 0)
        {
            string[] roleNames = Roles.GetRolesForUser(ddlUser2.SelectedItem.Text);
            ddlUserRoles.DataSource = roleNames;
            ddlUserRoles.DataBind();
        }
            lblUserRoles.Text = ddlUserRoles.Items.Count.ToString();

        ddlUserRolesUnAssign.DataSource = null;
        ddlUserRolesUnAssign.Items.Clear();
        if (ddlUserUnAssign.SelectedIndex >= 0)
        {
            string[] roleNames = Roles.GetRolesForUser(ddlUserUnAssign.SelectedItem.Text);
            ddlUserRolesUnAssign.DataSource = roleNames;
            ddlUserRolesUnAssign.DataBind();
        }
        lblUserRolesUnAssign.Text = ddlUserRolesUnAssign.Items.Count.ToString();

    }

    protected void btnAssignUserRoles_Click(object sender, EventArgs e)
    {
        lblMsg.Text = "";

        if (txtPwd.Text.Trim() != PwdToAccess)
        {
            lblMsg.Text = "Invalid Password! Not Autorized to work on this page, retry supplying correct password";
            return; //user exists
        }

        if (ddlUserRoles.Items.FindByText(ddlRoles2.SelectedItem.Text)!=null)
        {
            lblMsg.Text = "Error! Selected Role is already assigned to selected User" ;
            return;
        }

        if (ddlRoles2.SelectedItem.Text.ToLower() == "dealer")
        {
            lblMsg.Text = "Sorry! Role '" + ddlRoles2.SelectedItem.Text + "' can't be assigned/unassigned from here as it is assigned automatically when dealer registers from the website";
            return;
        }

        try
        {
            Roles.AddUserToRole(ddlUser2.SelectedItem.Text, ddlRoles2.SelectedItem.Text);
            bindddlExistingRolesOfUsers();
        }
        catch (Exception e2)
        {
            Message.Show(this, e2.Message);
            lblMsg.Text = "Error! " + e2.Message;
        }

        lblMsg.Text = "Successfully Assigned Role '" + ddlRoles2.SelectedItem.Text + "' to  User '" + ddlUser2.SelectedItem.Text + "'" ;
    }
    //public static bool IsValidEmail(string strIn)
    //{
    //    // Return true if strIn is in valid e-mail format.
    //    return Regex.IsMatch(strIn,
    //           @"^(?("")("".+?""@)|(([0-9a-zA-Z]((\.(?!\.))|[-!#\$%&'\*\+/=\?\^`\{\}\|~\w])*)(?<=[0-9a-zA-Z])@))" +
    //           @"(?(\[)(\[(\d{1,3}\.){3}\d{1,3}\])|(([0-9a-zA-Z][-\w]*[0-9a-zA-Z]\.)+[a-zA-Z]{2,6}))$");
    //}

    private Boolean addUserToMembershipDatabase(string dealerDesiredID, string dealerEmail, string quest, string ans,string rol)
    {

        string randomPassword = Membership.GeneratePassword(12, 1);

        try
        {
            MembershipCreateStatus sts;
            MembershipUser newUser = Membership.CreateUser(dealerDesiredID, randomPassword, dealerEmail, quest, ans, true, out sts);

        }
        catch (MembershipCreateUserException e1)
        {
            Message.Show(this, GetErrorMessage(e1.StatusCode));
            return false;
        }
        catch (HttpException e1)
        {
            Message.Show(this, e1.Message);
            return false;
        }
        try
        {
            Roles.AddUserToRole(dealerDesiredID, rol);
        }
        catch (Exception e2)
        {
            Message.Show(this, e2.Message);
            return false;
        }

        //temprorary close this later , open below three lines
        //lblMsg.Text = "Successfully Registered : User '" + dealerDesiredID + "' , Login Information sent to user on EMail Id : '" + dealerEmail + "'  " + randomPassword;

        lblMsg.Text = "Successfully Registered : User '" + dealerDesiredID + "' , Login Information sent to user on EMail Id : '" + dealerEmail + "'";

        txtUserPassword.Text = randomPassword;

        ////sending mail here to the new user 
        myGlobal.sendMailToNewUser(rol, dealerDesiredID, randomPassword, dealerEmail);
        
        return true;
    }

    public string GetErrorMessage(MembershipCreateStatus status)
    {
        switch (status)
        {
            case MembershipCreateStatus.DuplicateUserName:
                return "Login ID already exists. Please enter a different Login ID.";

            case MembershipCreateStatus.DuplicateEmail:
                return "A Login ID for that e-mail address already exists. Please enter a different e-mail address.";

            case MembershipCreateStatus.InvalidPassword:
                return "The password provided is invalid. Please enter a valid password value.";

            case MembershipCreateStatus.InvalidEmail:
                return "The e-mail address provided is invalid. Please check the value and try again.";

            case MembershipCreateStatus.InvalidAnswer:
                return "The password retrieval answer provided is invalid. Please check the value and try again.";

            case MembershipCreateStatus.InvalidQuestion:
                return "The password retrieval question provided is invalid. Please check the value and try again.";

            case MembershipCreateStatus.InvalidUserName:
                return "The Login ID provided is invalid. Please check the value and try again.";

            case MembershipCreateStatus.ProviderError:
                return "The authentication provider returned an error. Please verify your entry and try again. If the problem persists, please contact your system administrator.";

            case MembershipCreateStatus.UserRejected:
                return "The user creation request has been canceled. Please verify your entry and try again. If the problem persists, please contact your system administrator.";

            default:
                return "An unknown error occurred. Please verify your entry and try again. If the problem persists, please contact your system administrator.";
        }
    }



    /////////////---------------------------------------------------------------TABS CODING--------------------------------------------------------//////////////


    protected void imgRoles_Click(object sender, ImageClickEventArgs e)
    {
        if (pnlRoles.Visible == false)
        {
            pnlRoles.Visible = true;
            pnlUsers.Visible = false;
            pnlUserRoles.Visible = false;
            
            lblRoles.Font.Underline = true;
            lblRoles.ForeColor = System.Drawing.Color.Black;
            lblUsers.Font.Underline = false;
            lblUsers.ForeColor = System.Drawing.Color.White;
            lblUserRoles1.Font.Underline = false;
            lblUserRoles1.ForeColor = System.Drawing.Color.White;

            imgRoles.ImageUrl = "~/Admin/images/minuss.png";
            imgUsers.ImageUrl = "~/Admin/images/pluss.png";
            imgUserRoles.ImageUrl = "~/Admin/images/pluss.png";
        }
        else
        {
            lblRoles.Font.Underline = false;
            lblRoles.ForeColor = System.Drawing.Color.White;
            pnlRoles.Visible = false;
            imgRoles.ImageUrl = "~/Admin/images/pluss.png";
        }
    }
    protected void imgUsers_Click(object sender, ImageClickEventArgs e)
    {
        if (pnlUsers.Visible == false)
        {
            pnlRoles.Visible = false;
            pnlUsers.Visible = true;
            pnlUserRoles.Visible = false;
            
            lblUsers.Font.Underline = true;
            lblUsers.ForeColor = System.Drawing.Color.Black;
            lblRoles.Font.Underline = false;
            lblRoles.ForeColor = System.Drawing.Color.White;
            lblUserRoles1.Font.Underline = false;
            lblUserRoles1.ForeColor = System.Drawing.Color.White;

            imgUsers.ImageUrl = "~/Admin/images/minuss.png";
            imgRoles.ImageUrl = "~/Admin/images/pluss.png";
            imgUserRoles.ImageUrl = "~/Admin/images/pluss.png";
        }
        else
        {
            lblUsers.Font.Underline = false;
            lblUsers.ForeColor = System.Drawing.Color.White;
            pnlUsers.Visible = false;
            imgUsers.ImageUrl = "~/Admin/images/pluss.png";
        }
    }
    protected void imgUserRoles_Click(object sender, ImageClickEventArgs e)
    {
        if (pnlUserRoles.Visible == false)
        {
            pnlUserRoles.Visible = true;
            pnlUsers.Visible = false;
            pnlRoles.Visible = false;
            
            lblUserRoles1.Font.Underline = true;
            lblUserRoles1.ForeColor = System.Drawing.Color.Black;
            lblRoles.Font.Underline = false;
            lblRoles.ForeColor = System.Drawing.Color.White;
            lblUsers.Font.Underline = false;
            lblUsers.ForeColor = System.Drawing.Color.White;

            imgUserRoles.ImageUrl = "~/Admin/images/minuss.png";
            imgRoles.ImageUrl = "~/Admin/images/pluss.png";
            imgUsers.ImageUrl = "~/Admin/images/pluss.png";
        }
        else
        {
            lblUserRoles1.Font.Underline = false;
            lblUserRoles1.ForeColor = System.Drawing.Color.White;
            pnlUserRoles.Visible = false;
            imgUserRoles.ImageUrl = "~/Admin/images/pluss.png";
        }
    }
    protected void lnkRestPassword_Click(object sender, EventArgs e)
    {
        lblErrorMsg.Text = "";

        if (txtUserPassword1.Text.Trim().Length < 8)
        {
            lblErrorMsg.Text = "Error : Please enter a valide password. Minimum 8 Chars from  [a-z , 0-9 , * ] , at least one special char is a must";
            return;
        }
        if (ddlDeleteUser.SelectedIndex < 0)
        {
            lblMsg.Text = "Error! No User selected to be deleted";
            return; //user exists
        }

        string NewPwd = "";

        if (Membership.FindUsersByName(ddlDeleteUser.SelectedItem.Text) != null)
        {
            MembershipUser mu = Membership.Providers["AspNetSqlMembershipProvider"].GetUser(ddlDeleteUser.SelectedItem.Text, false);
            if (mu != null)
            {
                if (mu.IsLockedOut) //if is locked then unlock
                    mu.UnlockUser();

                NewPwd = mu.ResetPassword("blue");
                mu.ChangePassword(NewPwd, txtUserPassword1.Text);
                lblErrorMsg.Text = "Account Unlocked, Password changed Successfully";
            }
            else
                lblErrorMsg.Text = "Error , user not found";
        }
    }
}