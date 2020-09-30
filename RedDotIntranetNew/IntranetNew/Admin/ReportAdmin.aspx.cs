using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Data;
using System.Data.SqlClient;
using System.Web.Security;

public partial class IntranetNew_Admin_ReportAdmin : System.Web.UI.Page
{
    protected void Page_Load(object sender, EventArgs e)
    {
        BtnUser.Attributes.Add("onClick", "return getConfirmation();");
        lblMsg.Text = "";
        lblError.Text = "";
        if (!Page.IsPostBack)
        {
            Db.constr = myGlobal.getAppSettingsDataForKey("tejSAP");
            int count = Db.myExecuteScalar("Select COUNT(*) from dbo.MenuWiseForms t0 Join dbo.UserAuthorization t1 on t0.MenuId=t1.MenuId and t1.MembershipUserName='" + myGlobal.loggedInUser() + "' And t0.FormURL='ReportAdmin.aspx' and t1.IsActive=1");
            if (count > 0)
            {
                FillReportType();
                FillUsers();
            }
            else
            {
                Response.Redirect("Default.aspx?UserAccess=0&FormName=Reports Authorization");
            }
        }
    }
    public void FillReportType()
    {
        Db.constr = myGlobal.getConnectionStringForDB("EVOTej");
        string query = "select * from webReportTypes order by reportType";
        Db.LoadDDLsWithCon(ddlReportType, query, "reportTitle", "repTypeId", Db.constr);
        if (ddlReportType.Items.Count > 0)
        {
            ddlReportType.SelectedIndex = 0;
            FillListFile();
        }
    }

    public void FillListFile()
    {
        string query = "select * from webReportTypesNBU where fk_repTypeid=" + ddlReportType.SelectedValue + "";
        Db.constr = myGlobal.getConnectionStringForDB("EVOTej");
        DataSet ds = new DataSet();
        SqlConnection dbconn = new SqlConnection(Db.constr);
        dbconn.Open();
        SqlDataAdapter da = new SqlDataAdapter(query, dbconn);
        da.Fill(ds, "Table");
        dbconn.Close();
        lstFiles.DataSource = ds.Tables[0];
        lstFiles.DataTextField = "fileName";
        lstFiles.DataValueField = "BUId";
        lstFiles.DataBind();

        if (lstFiles.Items.Count > 0)
        {
            lstFiles.SelectedIndex = 0;
            imgbtnAssign.Enabled = true;
        }
        else
            imgbtnAssign.Enabled = false;

        lblFileCount.Text = "(" + lstFiles.Items.Count.ToString() + ")";
    }

    protected void lnkAddUser_Click(object sender, EventArgs e)
    {
        pnlAddRole.Enabled = false;
        pnlAddUser.Visible = true;
    }

    protected void imgBtnClose_Click(object sender, ImageClickEventArgs e)
    {
        pnlAddRole.Enabled = true;
        pnlAddUser.Visible = false;
    }

    protected void ddlReportType_SelectedIndexChanged(object sender, EventArgs e)
    {
        FillListFile();
        FillListUserFile();
    }

    public void FillUsers()
    {
        Db.constr = myGlobal.getMembershipDBConnectionString();
        string query = "select * from dbo.aspnet_Users order by UserName";
        Db.LoadDDLsWithCon(ddlUsers, query, "UserName", "UserId", Db.constr);
        if (ddlUsers.Items.Count > 0)
        {
            ddlUsers.SelectedIndex = 0;
            FillListUserFile();
        }
    }

    public void FillListUserFile()
    {
        string query = "select WUR.fk_BUId,WC.FileName,BU from webReportsUserRights as WUR join webReportTypesNBU as WC on WUR.fk_repTypeId=WC.fk_repTypeId and WUR.fk_BUId=WC.BUId where WUR.userName='" + ddlUsers.SelectedItem.Text + "' and WUR.fk_repTypeId='" + ddlReportType.SelectedValue + "'";
        Db.constr = myGlobal.getConnectionStringForDB("EVOTej");
        DataSet ds = new DataSet();
        SqlConnection dbconn = new SqlConnection(Db.constr);
        dbconn.Open();
        SqlDataAdapter da = new SqlDataAdapter(query, dbconn);
        da.Fill(ds, "Table");
        dbconn.Close();
        lstUserFiles.DataSource = ds.Tables[0];
        lstUserFiles.DataTextField = "fileName";
        lstUserFiles.DataValueField = "fk_BUId";
        lstUserFiles.DataBind();

        if (lstUserFiles.Items.Count > 0)
        {
            lstUserFiles.SelectedIndex = 0;
            imgbtnDelete.Enabled = true;
        }
        else
            imgbtnDelete.Enabled = false;

        lblUserFileCount.Text = "(" + lstUserFiles.Items.Count.ToString() + ")";
    }

    protected void ddlUsers_SelectedIndexChanged(object sender, EventArgs e)
    {
        FillListUserFile();
    }


    protected void imgbtnAssign_Click(object sender, ImageClickEventArgs e)
    {
        string query;
        Db.constr = myGlobal.getMembershipDBConnectionString();
        query = "select *  from dbo.aspnet_UsersInRoles where UserId=(select UserId  from dbo.aspnet_Users where UserName='" + ddlUsers.SelectedItem.Text.ToLower() + "') and RoleId=(select RoleId  from dbo.aspnet_Roles where RoleName='webReports')";
        SqlDataReader drd111 = Db.myGetReader(query);

        if (!drd111.HasRows)
            Roles.AddUserToRole(ddlUsers.SelectedItem.Text, "webReports");

        drd111.Close();

        query = "";
        for (int i = 0; i < lstFiles.Items.Count; i++)
        {
            if (lstFiles.Items[i].Selected)
            {
                string selectedItem = lstFiles.Items[i].Text;
                string selectedValue = lstFiles.Items[i].Value;

                ListItem tt = lstUserFiles.Items.FindByText(selectedItem);
                if (tt == null)
                {
                    query += " Insert Into webReportsUserRights Values('" + ddlUsers.SelectedItem.Text + "'," + ddlReportType.SelectedValue + "," + selectedValue.ToString() + ",'" + DateTime.Now + "') ; ";
                }
            }
        }

        if (query != "")
        {
            Db.constr = myGlobal.getConnectionStringForDB("EVOTej");
            Db.myExecuteSQL(query);
        }

        FillListUserFile();

        if (lstFiles.Items.Count > 0)
            lstFiles.SelectedIndex = 0;

        if (lstUserFiles.Items.Count > 0)
            lstUserFiles.SelectedIndex = 0;

        lblError.Text = "Success:: Selected File(s) Are Successfully Added to Permitted List For Selected User";
    }

    protected void imgbtnDelete_Click(object sender, ImageClickEventArgs e)
    {
        string query = "";
        for (int i = 0; i < lstUserFiles.Items.Count; i++)
        {
            if (lstUserFiles.Items[i].Selected)
            {
                query += " delete from webReportsUserRights where userName='" + ddlUsers.SelectedItem.Text + "' and fk_BUId=" + lstUserFiles.Items[i].Value + " ;";
            }
        }

        if (query != "")
        {
            Db.constr = myGlobal.getConnectionStringForDB("EVOTej");
            Db.myExecuteSQL(query);
        }

        lblError.Text = "Success:: Selected File(s) Are Successfully Removed For Selected User";

        FillListUserFile();
        if (lstFiles.Items.Count > 0)
            lstFiles.SelectedIndex = 0;

        if (lstUserFiles.Items.Count > 0)
            lstUserFiles.SelectedIndex = 0;

    }

    protected void BtnUser_Click(object sender, EventArgs e)
    {
        String rl = "webReports"; //we assign this role by default to user 

        lblMsg.Text = "";

        try
        {
            if (txtUser.Text.Trim() == "")
            {
                lblMsg.Text = "Error! User name can't be empty";
                return; //user exists
            }

            if (!Util.IsValidEmail(txtEmail.Text))
            {
                lblMsg.Text = "Error! Invalid Email id, kindly retry with correct email id format .";
                return; //user exists
            }


            if (Membership.GetUserNameByEmail(txtEmail.Text) != null) //if it gets some user name, it is already registered
            {
                lblMsg.Text = "Error! Desired Email is already registered. Please try with other emailID.";
                return; //user exists
            }

            if (!addUserToMembershipDatabase(txtUser.Text.Trim(), txtEmail.Text.Trim(), "color", "blue", rl)) //chech for user id availablity and create user in membership db
            {
                lblMsg.Text = "Error! Desired Login Id is not available, please try another and Submit.";
                return;
            }

            FillUsers();
            txtUser.Text = "";
            txtEmail.Text = "";
        }
        catch (Exception exxxp)
        {
            lblMsg.Text = "Error! " + exxxp.Message;
        }
    }
    private Boolean addUserToMembershipDatabase(string dealerDesiredID, string dealerEmail, string quest, string ans, string rol)
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

}


//if (lstFiles.Items[i].Selected)
//{
//    for (int j = 0; j < lstUserFiles.Items.Count; j++)
//    {
//        if (lstFiles.Items[i].Text != lstUserFiles.Items[j].Text)
//        {
//            query = query + "Insert Into webReportsUserRights Values('" + ddlUsers.SelectedItem.Text + "'," + ddlReportType.SelectedValue.ToString() + "," + lstFiles.Items[i].Value + ",'" + DateTime.Now.ToString() + "')";
//        }
//    }