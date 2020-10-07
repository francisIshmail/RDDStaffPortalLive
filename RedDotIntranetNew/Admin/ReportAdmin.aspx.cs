using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Data;
using System.Data.SqlClient;
using System.Web.Security;

public partial class Admin_ReportAdmin : System.Web.UI.Page
{
    string imgPth = "~/admin/images/";

    protected void Page_Load(object sender, EventArgs e)
    {
        BtnUser.Attributes.Add("onClick", "return getConfirmation();");
        btnReportdelete.Attributes.Add("onClick", "return getConfirmation();");
        btnBUDelete.Attributes.Add("onClick", "return getConfirmation();");
        lblMsg.Text = "";
        lblError.Text = "";
        if (!Page.IsPostBack)
        {
            loadReports();
            FillReportType();
            loadBUReports();
            FillUsers();
        }
    }


    
    ////////////////////////////////////////////////////////LEVEL III//////////////////////////////////////////////////////////////
  


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
        {
            imgbtnAssign.Enabled = false;
        }
        lblFileCount.Text = "(" + lstFiles.Items.Count.ToString() + ")";
    }

    protected void lnkAddUser_Click(object sender, EventArgs e)
    {
        pnlAddRole.Visible = false;
        pnlAddUser.Visible = true;
    }

    protected void imgBtnClose_Click(object sender, ImageClickEventArgs e)
    {
        pnlAddRole.Visible = true;
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
        {
            imgbtnDelete.Enabled = false;
        }
        lblUserFileCount.Text = "(" + lstUserFiles.Items.Count.ToString() + ")";
    }

    protected void ddlUsers_SelectedIndexChanged(object sender, EventArgs e)
    {
        FillListUserFile();
    }

    protected void imgbtnAssign_Click(object sender, ImageClickEventArgs e)
    {
        for (int i = 0; i < lstFiles.Items.Count; i++)
        {
            if (lstFiles.Items[i].Selected)
            {
                string selectedItem = lstFiles.Items[i].Text;
                string selectedValue = lstFiles.Items[i].Value;

                ListItem tt = lstUserFiles.Items.FindByText(selectedItem);
                if (tt == null)
                {
                    Db.constr = myGlobal.getConnectionStringForDB("EVOTej");
                    string query = "Insert Into webReportsUserRights Values('" + ddlUsers.SelectedItem.Text + "'," + ddlReportType.SelectedValue + "," + selectedValue.ToString() + ",'" + DateTime.Now + "')";
                    Db.myExecuteSQL(query);
                    lblError.Text = "Success:: Selected File(s) Are Successfully Assigned To Selected User";
                }
            }
        }
        FillListUserFile();
        FillListFile();
    }

    protected void imgbtnDelete_Click(object sender, ImageClickEventArgs e)
    {
        for (int i = 0; i < lstUserFiles.Items.Count; i++)
        {
            if (lstUserFiles.Items[i].Selected)
            {
                string selectedValue = lstUserFiles.Items[i].Value;

                Db.constr = myGlobal.getConnectionStringForDB("EVOTej");
                string query = "delete from webReportsUserRights where userName='" + ddlUsers.SelectedItem.Text + "' and fk_BUId=" + selectedValue;
                Db.myExecuteSQL(query);
                lblError.Text = "Success:: Selected File(s) Are Successfully Removed For The Selected User";
            }
        }
        FillListUserFile();
        FillListFile();
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




    protected void imgAddReport_Click(object sender, ImageClickEventArgs e)
    {
        if (pnlAddReport.Visible == false)
        {
            pnlAddReport.Visible = true;
            pnlAddRole.Visible = false;
            pnlAddBU.Visible = false;
            pnlAddUser.Visible = false;

            lblAddReport.Font.Underline = true;
            lblAddReport.ForeColor = System.Drawing.Color.Black;
            lblBU.Font.Underline = false;
            lblBU.ForeColor = System.Drawing.Color.White;
            lblAssignFile.Font.Underline = false;
            lblAssignFile.ForeColor = System.Drawing.Color.White;

            imgAssignFile.ImageUrl = imgPth + "pluss.png";
            imgBU.ImageUrl = imgPth + "pluss.png";
            imgAddReport.ImageUrl = imgPth + "minuss.png";
        }
        else
        {
            lblAddReport.Font.Underline = false;
            lblAddReport.ForeColor = System.Drawing.Color.White;
            pnlAddReport.Visible = false;
            imgAddReport.ImageUrl = imgPth + "pluss.png";
        }
    }

    protected void imgAssignFile_Click(object sender, ImageClickEventArgs e)
    {
        if (pnlAddRole.Visible == false)
        {
            pnlAddRole.Visible = true;
            pnlAddReport.Visible = false;
            pnlAddBU.Visible = false;
            pnlAddUser.Visible = false;

            lblAddReport.Font.Underline = false;
            lblAddReport.ForeColor = System.Drawing.Color.White;
            lblBU.Font.Underline = false;
            lblBU.ForeColor = System.Drawing.Color.White;
            lblAssignFile.Font.Underline = true;
            lblAssignFile.ForeColor = System.Drawing.Color.Black;

            imgAssignFile.ImageUrl = imgPth + "minuss.png";
            imgBU.ImageUrl = imgPth + "pluss.png";
            imgAddReport.ImageUrl = imgPth + "pluss.png";
        }
        else
        {
            lblAssignFile.Font.Underline = false;
            lblAssignFile.ForeColor = System.Drawing.Color.White;
            pnlAddRole.Visible = false;
            imgAssignFile.ImageUrl = imgPth + "pluss.png";
        }
    }

    protected void imgBU_Click(object sender, ImageClickEventArgs e)
    {
        if (pnlAddBU.Visible == false)
        {
            pnlAddRole.Visible = false;
            pnlAddReport.Visible = false;
            pnlAddBU.Visible = true;
            pnlAddUser.Visible = false;

            lblAddReport.Font.Underline = false;
            lblAddReport.ForeColor = System.Drawing.Color.White;
            lblBU.Font.Underline = true;
            lblBU.ForeColor = System.Drawing.Color.Black;
            lblAssignFile.Font.Underline = false;
            lblAssignFile.ForeColor = System.Drawing.Color.White;

            imgAssignFile.ImageUrl = imgPth + "pluss.png";
            imgBU.ImageUrl = imgPth + "minuss.png";
            imgAddReport.ImageUrl = imgPth + "pluss.png";
        }
        else
        {
            lblBU.Font.Underline = false;
            lblBU.ForeColor = System.Drawing.Color.White;
            pnlAddBU.Visible = false;
            imgBU.ImageUrl = imgPth + "pluss.png";
        }
    }



    /////////////////////////////////////////////////////////////////LEVEL I////////////////////////////////////////////////////////////



    public void loadReports()
    {
        string query = "select * from webReportTypes order by reportType";
        Db.constr = myGlobal.getConnectionStringForDB("EVOTej");
        Db.LoadDDLsWithCon(ddlReport, query, "reportType", "repTypeId", Db.constr);
        lblReportCount.Text = "(" + ddlReport.Items.Count.ToString() + ")";

        if (ddlReport.Items.Count > 0)
        {
            ddlReportType.SelectedIndex = 0;
            FillReportTextBox();
            lnkReportEdit.Enabled = true;
        }
        else
            lnkReportEdit.Enabled = false;
    }

    public void FillReportTextBox()
    {
        string query = "select * from webReportTypes where repTypeId=" + ddlReport.SelectedValue + "";
        Db.constr = myGlobal.getConnectionStringForDB("EVOTej");
        DataTable dt;
        dt = Db.myGetDS(query).Tables[0];
        txtReport.Text = dt.Rows[0]["reportType"].ToString();
        txtReportTitle.Text = dt.Rows[0]["reportTitle"].ToString();
        txtReportPath.Text = dt.Rows[0]["reportFilePath"].ToString();
    }

    protected void ddlReport_SelectedIndexChanged(object sender, EventArgs e)
    {
        if(lnkReportAdd.Enabled==true)
        FillReportTextBox();
    }

    protected void lnkReportAdd_Click(object sender, EventArgs e)
    {
        btnReportUpdate.Enabled = true;
        btnReportCancel.Enabled = true;
        btnReportdelete.Enabled = false;
        txtReport.Text = "";
        txtReportTitle.Text = "";
        txtReportPath.Text = "";
        txtReport.Enabled = true;
        txtReportTitle.Enabled = true;
        txtReportPath.Enabled = true;
        lnkReportAdd.Enabled = false;
        lnkReportEdit.Enabled = true;
    }

    protected void lnkReportEdit_Click(object sender, EventArgs e)
    {
        btnReportUpdate.Enabled = true;
        btnReportCancel.Enabled = true;
        btnReportdelete.Enabled = true;
        txtReport.Enabled = true;
        txtReportTitle.Enabled = true;
        txtReportPath.Enabled = true;
        lnkReportAdd.Enabled = true;
        lnkReportEdit.Enabled = false;
        FillReportTextBox();
    }

    protected void btnReportCancel_Click(object sender, EventArgs e)
    {
        txtReport.Enabled = false;
        txtReportTitle.Enabled = false;
        txtReportPath.Enabled = false;
        lnkReportEdit.Enabled = true;
        lnkReportAdd.Enabled = true;
        btnReportUpdate.Enabled = false;
        btnReportCancel.Enabled = false;
        btnReportdelete.Enabled = false;
        FillReportTextBox();
    }
    
    protected void btnReportUpdate_Click(object sender, EventArgs e)
    {
        if (txtReport.Text == "")
        {
            lblError.Text = "Error!! Report Name Can't Be Left Blank";
            return;
        }

        if (txtReportTitle.Text == "")
        {
            lblError.Text = "Error!! Report Title Can't Be Left Blank";
            return;
        }

        if (txtReportPath.Text == "")
        {
            lblError.Text = "Error!! Report Path Can't Be Left Blank";
            return;
        }

        if (lnkReportAdd.Enabled == true)
        {
            String query = "update webReportTypes set reportType='" + txtReport.Text + "',reportTitle='" + txtReportTitle.Text + "',reportFilePath='" + txtReportPath.Text + "' where repTypeId='" + ddlReport.SelectedValue + "'";
            Db.constr = myGlobal.getConnectionStringForDB("EVOTej");
            Db.myExecuteSQL(query);
            loadReports();
            ddlReport.Items.FindByText(txtReport.Text).Selected = true;
            lblError.Text = "Success!! All Fields Updated Successfully";
            return;
        }
        else
        {
            if (ddlReport.Items.FindByText(txtReport.Text) != null)
            {
                lblError.Text = "Error!! Same Report Name Exists ,Cannot Insert Same Report Twice";
                return;
            }

            String query = "insert into webReportTypes values('" + txtReport.Text + "','" + txtReportTitle.Text + "','" + txtReportPath.Text + "')";
            Db.constr = myGlobal.getConnectionStringForDB("EVOTej");
            Db.myExecuteSQL(query);
            loadReports();
            ddlReport.Items.FindByText(txtReport.Text).Selected = true;
            lblError.Text = "Success!! All Fields Inserted Successfully";

            txtReport.Text = "";
            txtReportPath.Text = "";
            txtReportTitle.Text = "";
            return;

        }
        
    }
    protected void btnReportdelete_Click(object sender, EventArgs e)
    {
        String query = "DELETE FROM webReportTypes where reportType='" + ddlReport.SelectedItem.Text + "' and repTypeId='" + ddlReport.SelectedValue + "'";
        Db.constr = myGlobal.getConnectionStringForDB("EVOTej");
        Db.myExecuteSQL(query);
        loadReports();
        lblError.Text = "Success!! Selected Report Deleted Successfully";
        lnkReportEdit.Enabled = false;
        return;
    }



    /////////////////////////////////////////////////////////////////LEVEL II////////////////////////////////////////////////////////////



    public void loadBUReports()
    {
        string query = "select * from webReportTypes order by reportType";
        Db.constr = myGlobal.getConnectionStringForDB("EVOTej");
        Db.LoadDDLsWithCon(ddlBUReport, query, "reportType", "repTypeId", Db.constr);
        lblBUReportCount.Text = "(" + ddlBUReport.Items.Count.ToString() + ")";

        if (ddlBUReport.Items.Count > 0)
        {  
            ddlBUReport.SelectedIndex = 0;
            lnkBUEdit.Enabled = true;
            loadBUName();
            FillBUTextBox();
        }
        else
            lnkBUEdit.Enabled = false;
    }

    public void loadBUName()
    {
        string query = "select * from webReportTypesNBU where fk_repTypeId=" + ddlBUReport.SelectedValue + "";
        Db.constr = myGlobal.getConnectionStringForDB("EVOTej");
        Db.LoadDDLsWithCon(ddlBUName, query, "BU", "BUId", Db.constr);
        lblBUNameCount.Text = "(" + ddlBUName.Items.Count.ToString() + ")";
    }

    public void FillBUTextBox()
    {
        string query = "select * from webReportTypesNBU where fk_repTypeId=" + ddlBUReport.SelectedValue + " and BU='" + ddlBUName.SelectedItem.Text + "'";
        Db.constr = myGlobal.getConnectionStringForDB("EVOTej");
        DataTable dt;
        dt = Db.myGetDS(query).Tables[0];
        txtBUName.Text = dt.Rows[0]["BU"].ToString();
        txtBUFileName.Text = dt.Rows[0]["fileName"].ToString();
    }
    protected void ddlBUReport_SelectedIndexChanged(object sender, EventArgs e)
    {
        if (lnkBUAdd.Enabled == true)
        {
            loadBUName();
            FillBUTextBox(); 
        }
        else
            loadBUName();
    }
    protected void ddlBUName_SelectedIndexChanged(object sender, EventArgs e)
    {
        if (lnkBUAdd.Enabled == true)
        FillBUTextBox();
    }

    protected void lnkBUAdd_Click(object sender, EventArgs e)
    {
        btnBUUpdate.Enabled = true;
        btnBUCancel.Enabled = true;
        btnBUDelete.Enabled = false;
        txtBUFileName.Text = "";
        txtBUName.Text = "";
        txtBUFileName.Enabled = true;
        txtBUName.Enabled = true;
        lnkBUAdd.Enabled = false;
        lnkBUEdit.Enabled = true;
    }
    protected void lnkBUEdit_Click(object sender, EventArgs e)
    {
        btnBUUpdate.Enabled = true;
        btnBUCancel.Enabled = true;
        btnBUDelete.Enabled = true;
        txtBUFileName.Enabled = true;
        txtBUName.Enabled = true;
        lnkBUAdd.Enabled = true;
        lnkBUEdit.Enabled = false;
        FillBUTextBox();
    }
    protected void btnBUCancel_Click(object sender, EventArgs e)
    {
        btnBUUpdate.Enabled = false;
        btnBUCancel.Enabled = false;
        btnBUDelete.Enabled = false;
        txtBUFileName.Enabled = false;
        txtBUName.Enabled = false;
        lnkBUAdd.Enabled = true;
        lnkBUEdit.Enabled = true;
        FillBUTextBox();
    }
    protected void btnBUUpdate_Click(object sender, EventArgs e)
    {
        if (txtBUName.Text == "")
        {
            lblError.Text = "Error!! BU Name Can't Be Left Blank";
            return;
        }

        if (txtBUFileName.Text == "")
        {
            lblError.Text = "Error!! File Name Can't Be Left Blank";
            return;
        }

        if (lnkBUAdd.Enabled == true)
        {
            String query = "update webReportTypesNBU set BU='" + txtBUName.Text + "',fileName='" + txtBUFileName.Text + "' where BUId=" + ddlBUName.SelectedValue + " and fk_repTypeId=" + ddlBUReport.SelectedValue + "";
            Db.constr = myGlobal.getConnectionStringForDB("EVOTej");
            Db.myExecuteSQL(query);
            loadBUReports();
            lblError.Text = "Success!! All Fields Updated Successfully";
            return;
        }

        else
        {
            string qry = "select max(BUId) from webReportTypesNBU where fk_repTypeId=" + ddlBUReport.SelectedValue + "";
            Db.constr = myGlobal.getConnectionStringForDB("EVOTej");
            DataTable dt;
            dt = Db.myGetDS(qry).Tables[0];
            int value = Convert.ToInt32(dt.Rows[0]["Column1"]);
            int value1 = value + 1;
            String query = "insert into webReportTypesNBU values(" + value1 + "," + ddlBUReport.SelectedValue + ",'" + txtBUName.Text + "','" + txtBUFileName.Text + "','" + DateTime.Now + "')";
            Db.myExecuteSQL(query);
            loadBUReports();
            lblError.Text = "Success!! All Fields Inserted Successfully";
            txtBUName.Text = "";
            txtBUFileName.Text = "";
            return;
        }
    }
    protected void btnBUDelete_Click(object sender, EventArgs e)
    {
        string query = "Delete from webReportTypesNBU where BUId='" + ddlBUName.SelectedValue + "' and fk_repTypeId=" + ddlBUReport.SelectedValue + "";
        Db.constr = myGlobal.getConnectionStringForDB("EVOTej");
        Db.myExecuteSQL(query);
        loadBUReports();
        lblError.Text = "Success!! Selected BU Deleted Successfully";
        lnkBUEdit.Enabled = false;
    }
}


