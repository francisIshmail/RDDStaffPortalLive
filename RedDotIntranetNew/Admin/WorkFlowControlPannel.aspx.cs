using System;
using System.Data;
using System.Data.SqlClient;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Drawing;

public partial class Admin_WorkFlowControlPannel : System.Web.UI.Page
{
    protected void Page_Load(object sender, EventArgs e)
    {
        lblError.Text = "";
        lblError1.Text = "";
        lblError1.ForeColor = Color.Red;
        if (!Page.IsPostBack)
        {
            LoadDepartments();
            LoadDepartments1();
            loadProcessTypeID();
            loadProcessType();
            BindGrid();
            // BindProcessgrid();
        }
    }

    protected void ddldeptName_SelectedIndexChanged(object sender, EventArgs e)
    {
        LoadtxtDeptName();
        lblError.Text = "";
    }

    public void LoadDepartments()
    {
        string query;
        Db.constr = myGlobal.getRDDMarketingDBConnectionString();
        query = "select * from dbo.departments";
        Db.LoadDDLsWithCon(ddldeptName, query, "departmentName", "autoIndex", Db.constr);
        if (ddldeptName.Items.Count == 0)
        {
            btnEdit.Enabled = false;
        }

        ListItem tt = ddldeptName.Items.FindByText(txtDeptName.Text);

        if (tt == null)
        {
            LoadtxtDeptName();
        }
        else
        {
            tt.Selected = true;
        }

        LoadDepartments1();
        loadProcessTypeID();
        BindGrid();
    }


    public void LoadtxtDeptName()
    {
        if (ddldeptName.SelectedIndex >= 0)
            txtDeptName.Text = ddldeptName.SelectedItem.Text;
    }


    protected void btnNew_Click(object sender, EventArgs e)
    {
        txtDeptName.Enabled = true;
        btnEdit.Enabled = false;
        txtDeptName.Text = "";
        ddldeptName.Enabled = false;
        btndelete.Enabled = false;
        btnUpdate.Enabled = true;
        btnCancel.Enabled = true;
    }

    protected void btnEdit_Click(object sender, EventArgs e)
    {
        txtDeptName.Enabled = true;
        btnNew.Enabled = false;
        btndelete.Enabled = true;
        btnUpdate.Enabled = true;
        btnCancel.Enabled = true;
    }

    protected void btnUpdate_Click(object sender, EventArgs e)
    {
        if (txtDeptName.Text == "")
        {
            lblError.Text = "Error:: Depertment Name Can't Be Empty";
            return;
        }

        ListItem tt = ddldeptName.Items.FindByText(txtDeptName.Text);
        if (tt != null)
        {
            lblError.Text = "Error::Can Not Update Department Name, Same Department Name Exists";
            return;
        }


        if (btnEdit.Enabled == true)
        {

            string qry = "update dbo.departments set departmentName='" + txtDeptName.Text + "' where departmentName='" + ddldeptName.SelectedItem.Text + "'";
            Db.constr = myGlobal.getRDDMarketingDBConnectionString();
            Db.myExecuteSQL(qry);
            lblError.ForeColor = Color.Green;
            lblError.Text = "Success:: department Name Sucessfully Updated";
            LoadDepartments();

        }
        else
        {
            if (btnNew.Enabled == true)
            {
                string qry = "insert into dbo.departments values('" + txtDeptName.Text + "')";
                Db.constr = myGlobal.getRDDMarketingDBConnectionString();
                Db.myExecuteSQL(qry);
                lblError.ForeColor = Color.Green;
                lblError.Text = "Success:: department Name Sucessfully Inserted";
                btnNew.Enabled = true;
                LoadDepartments();
                ddldeptName.Enabled = true;
            }
        }
        btndelete.Enabled = false;
        btnUpdate.Enabled = false;
        btnCancel.Enabled = false;
        btnEdit.Enabled = true;
        btnNew.Enabled = true;
        txtDeptName.Enabled = false;
    }
    protected void btndelete_Click(object sender, EventArgs e)
    {
        string qry = "delete from dbo.departments where departmentName='" + ddldeptName.SelectedItem.Text + "'";
        Db.constr = myGlobal.getRDDMarketingDBConnectionString();
        Db.myExecuteSQL(qry);
        lblError.ForeColor = Color.Green;
        lblError.Text = "Success:: department Name Sucessfully Deleted";
        txtDeptName.Text = "";
        LoadDepartments();
    }

    protected void btnCancel_Click(object sender, EventArgs e)
    {
        LoadDepartments();
        btndelete.Enabled = false;
        btnUpdate.Enabled = false;
        btnCancel.Enabled = false;
        ddldeptName.Enabled = true;
        btnNew.Enabled = true;
        btnEdit.Enabled = true;
        txtDeptName.Enabled = false;
    }





    ////////////////////////////////////////////////////////////////////////////////////LEVEL 2/////////////////////////////////////////////////////////////////////////////////////////////////////////////





    public void LoadDepartments1()
    {
        string query;
        Db.constr = myGlobal.getRDDMarketingDBConnectionString();
        query = "select * from dbo.departments";
        Db.LoadDDLsWithCon(ddlDepartments, query, "departmentName", "autoIndex", Db.constr);
        if (ddlDepartments.Items.Count == 0)
        {
            pnlDepartmentRoles.Enabled = false;
            pnlEditRoles.Enabled = false;
        }
        else
        {
            pnlDepartmentRoles.Enabled = true;
            pnlEditRoles.Enabled = true;
            loadListBox();
        }
    }

    public void loadListBox()
    {
        string query = "select * from dbo.roles where fk_deptId=" + ddlDepartments.SelectedValue + " order by roleLevel";
        string constr = myGlobal.getRDDMarketingDBConnectionString();
        DataSet ds = new DataSet();
        SqlConnection dbconn = new SqlConnection(constr);
        dbconn.Open();
        SqlDataAdapter da = new SqlDataAdapter(query, dbconn);
        da.Fill(ds, "Table");
        dbconn.Close();
        lstRoleLevel.DataSource = ds.Tables[0];
        lstRoleLevel.DataTextField = "roleName";
        lstRoleLevel.DataValueField = "roleLevel";
        lstRoleLevel.DataBind();
        if (lstRoleLevel.Items.Count != 0)
        {
            lstRoleLevel.SelectedIndex = 0;
            fillTextBoxes();
            imgBtnUp.Enabled = true;
            imgBtnDown.Enabled = true;
            btnUpdateList.Enabled = true;
            btnEditRole.Enabled = true;
        }
        else
        {
            imgBtnUp.Enabled = false;
            imgBtnDown.Enabled = false;
            btnUpdateList.Enabled = false;
            btnEditRole.Enabled = false;
        }
    }

    protected void lstRoleLevel_SelectedIndexChanged(object sender, EventArgs e)
    {
        fillTextBoxes();
    }

    public void fillTextBoxes()
    {
        DataTable dts = new DataTable();
        string query = "select roleName,emailList from dbo.roles where roleName='" + lstRoleLevel.SelectedItem.Text + "'";
        Db.constr = myGlobal.getRDDMarketingDBConnectionString();
        dts = Db.myGetDS(query).Tables[0];
        txtRoleName.Text = dts.Rows[0][0].ToString();
        txtEmail.Text = dts.Rows[0][1].ToString();
    }

    protected void ddlDepartments_SelectedIndexChanged(object sender, EventArgs e)
    {
        loadListBox();
        clear();
        if (lstRoleLevel.Items.Count != 0)
        {
            fillTextBoxes();
        }
    }

    public void clear()
    {
        txtRoleName.Text = "";
        txtEmail.Text = "";
    }

    protected void btnEditRole_Click(object sender, EventArgs e)
    {
        btnDeleteRole.Enabled = true;
        btnUpdateRole.Enabled = true;
        btnCancelRole.Enabled = true;
        btnNewRole.Enabled = false;
        txtRoleName.Enabled = true;
        txtEmail.Enabled = true;
    }

    protected void btnNewRole_Click(object sender, EventArgs e)
    {
        lblError.Text = "";
        lblError1.Text = "";
        txtRoleName.Text = "";
        txtRoleName.Enabled = true;
        btnEditRole.Enabled = false;

        txtEmail.Text = "";
        txtEmail.Enabled = true;
        clear();
        ddlDepartments.Enabled = false;
        btnUpdateRole.Enabled = true;
        btnDeleteRole.Enabled = false;
        btnCancelRole.Enabled = true;

    }

    protected void btnCancelRole_Click(object sender, EventArgs e)
    {
        lblError.Text = "";
        lblError1.Text = "";

        txtRoleName.Text = "";
        txtEmail.Text = "";
        txtRoleName.Enabled = false;
        txtEmail.Enabled = false;
        ddlDepartments.Enabled = true;
        btnNewRole.Enabled = true;
        btnCancelRole.Enabled = false;
        btnDeleteRole.Enabled = false;
        btnUpdateRole.Enabled = false;
    }

    protected void btnDeleteRole_Click(object sender, EventArgs e)
    {
        string qry = "delete from dbo.roles where roleLevel='" + lstRoleLevel.SelectedValue + "' and fk_deptId='" + ddlDepartments.SelectedValue + "'";
        Db.constr = myGlobal.getRDDMarketingDBConnectionString();
        Db.myExecuteSQL(qry);
        lblError.ForeColor = Color.Green;
        lblError.Text = "Success:: Selected department Sucessfully Deleted";
        loadListBox();
    }

    protected void btnUpdateRole_Click(object sender, EventArgs e)
    {
        if (txtRoleName.Text == "" || txtEmail.Text == "")
        {
            lblError.Text = "Error::One or More Fields Left Blank, All Fields Are Compulsory";
            return;
        }

        if (btnEditRole.Enabled == true)
        {
            string query = "update dbo.roles set rolename='" + txtRoleName.Text + "', emailList='" + txtEmail.Text + "' where roleLevel='" + lstRoleLevel.SelectedValue + "' and fk_deptId='" + ddlDepartments.SelectedValue + "'";
            Db.constr = myGlobal.getRDDMarketingDBConnectionString();
            Db.myExecuteSQL(query);
            lblError.ForeColor = Color.Green;
            lblError.Text = "Success:: All Fields Sucessfully Updated";
            LoadDepartments1();
            clear();
        }

        if (btnNewRole.Enabled == true)
        {
            ListItem tt = lstRoleLevel.Items.FindByText(txtRoleName.Text);
            if (tt != null)
            {
                lblError.Text = "Error::Can Not Create Department Name, Same Department Name Exists";
                return;
            }
            else
            {
                if (lstRoleLevel.Items.Count > 0)
                {
                    int max = lstRoleLevel.Items.Cast<ListItem>().Select(s => Convert.ToInt32(s.Value)).Max();
                    int max1 = max + 1;
                    string query = "insert into dbo.roles values('" + txtRoleName.Text + "', " + max1 + " ,'" + ddlDepartments.SelectedValue + "','" + txtEmail.Text + "')";
                    Db.constr = myGlobal.getRDDMarketingDBConnectionString();
                    Db.myExecuteSQL(query);
                }
                else
                {
                    string query = "insert into dbo.roles values('" + txtRoleName.Text + "', '1' ,'" + ddlDepartments.SelectedValue + "','" + txtEmail.Text + "')";
                    Db.constr = myGlobal.getRDDMarketingDBConnectionString();
                    Db.myExecuteSQL(query);
                }
                lblError.ForeColor = Color.Green;
                lblError.Text = "Success:: All Fields Sucessfully Inserted";
            }
        }
        ddlDepartments.Enabled = true;
        btnNewRole.Enabled = true;
        btnCancelRole.Enabled = false;
        btnUpdateRole.Enabled = false;
        btnDeleteRole.Enabled = false;
        txtRoleName.Enabled = false;
        txtEmail.Enabled = false;
        loadListBox();
    }

    protected void imgBtnUp_Click(object sender, ImageClickEventArgs e)
    {
        lblError.Text = "";
        int selectedIndex = lstRoleLevel.SelectedIndex;
        if (selectedIndex == 0)
        {
            //lblError.Text = "Error::Cant Move Up Its Already At First Position";
        }
        else
        {
            ListItem li = lstRoleLevel.Items[selectedIndex - 1];
            lstRoleLevel.Items.RemoveAt(selectedIndex - 1);
            lstRoleLevel.Items.Insert(selectedIndex, li);
        }
    }

    protected void imgBtnDown_Click(object sender, ImageClickEventArgs e)
    {
        lblError.Text = "";
        int selectedIndex = lstRoleLevel.SelectedIndex;
        if (selectedIndex == lstRoleLevel.Items.Count - 1)
        {
            //lblError.Text = "Error:: Cant Move Down Its Already At Last Position";
        }
        else
        {
            ListItem li = lstRoleLevel.Items[selectedIndex + 1];
            lstRoleLevel.Items.RemoveAt(selectedIndex + 1);
            lstRoleLevel.Items.Insert(selectedIndex, li);
        }
    }

    protected void btnUpdateList_Click(object sender, EventArgs e)
    {
        updateRoles();
    }

    public void updateRoles()
    {
        int count = lstRoleLevel.Items.Count;
        for (int i = 0; i < count; i++)
        {
            string rolename = lstRoleLevel.Items[i].Text;
            lstRoleLevel.SelectedIndex = i;
            int rolelevel = i + 1;

            string qry = "update dbo.roles set roleLevel='" + rolelevel + "' where roleName='" + rolename + "' and fk_deptId='" + ddlDepartments.SelectedValue + "' ";
            Db.constr = myGlobal.getRDDMarketingDBConnectionString();
            Db.myExecuteSQL(qry);
        }
        loadListBox();
        lblError.ForeColor = Color.Green;
        lblError.Text = "Success:: Roles Updated Successfully";
    }



    ///////////////////////////////////////////////////////////////LEVEL 3//////////////////////////////////////////////////////////////////



    public void loadProcessType()
    {
        if (lstRoleLevel.Items.Count > 0)
        {
            Db.constr = myGlobal.getRDDMarketingDBConnectionString();
            string qry = "select * from dbo.process_def";
            Db.LoadDDLsWithCon(ddlProcessType, qry, "processName", "processid", Db.constr);
            pnlEscalation.Enabled = true;
            loadEscRoleName();
        }
        else
        {
            pnlEscalation.Enabled = false;
        }
    }

    protected void ddlProcessType_SelectedIndexChanged(object sender, EventArgs e)
    {
        loadEscList();
    }

    public void loadEscRoleName()
    {
        Db.constr = myGlobal.getRDDMarketingDBConnectionString();
        string qry = "select * from dbo.roles";
        Db.LoadDDLsWithCon(ddlEscRoleName, qry, "roleName", "roleId", Db.constr);

        if (ddlEscRoleName.Items.Count == 0)
        {
            pnlEscalation.Enabled = false;
        }
        else
        {
            pnlEscalation.Enabled = true;
            loadEscList();
        }
    }

    public void loadEscList()
    {
        string query = "Select PE.*,R.roleId,R.RoleName From dbo.processEscalate as PE Join dbo.roles as R ON PE.fk_roleId=R.roleId Where fk_processId=" + ddlProcessType.SelectedValue + " Order By escalateLevelId";
        string constr = myGlobal.getRDDMarketingDBConnectionString();
        DataSet ds = new DataSet();
        SqlConnection dbconn = new SqlConnection(constr);
        dbconn.Open();
        SqlDataAdapter da = new SqlDataAdapter(query, dbconn);
        da.Fill(ds, "Table");
        dbconn.Close();
        lstEscalate.DataSource = ds.Tables[0];
        lstEscalate.DataTextField = "roleName";
        lstEscalate.DataValueField = "escalateLevelId";
        lstEscalate.DataBind();
        
        if(lstEscalate.Items.Count==0 )
        {
            pnlList.Enabled = false;
        }
        else
        {
            lstEscalate.SelectedIndex = 0;
            pnlList.Enabled = true;
        }
    }

    protected void imgbtnEscUp_Click(object sender, ImageClickEventArgs e)
    {
        int selectedIndex = lstEscalate.SelectedIndex;
        if (lstEscalate.SelectedIndex == 0)
        {
            //lblError.Text = "Error::Cant Move Up Its Already At First Position";
        }
        else
        {
            ListItem li = lstEscalate.Items[selectedIndex - 1];
            lstEscalate.Items.RemoveAt(selectedIndex - 1);
            lstEscalate.Items.Insert(selectedIndex, li);
        }
    }

    protected void imgbtnEscDown_Click(object sender, ImageClickEventArgs e)
    {
        int selectedIndex = lstEscalate.SelectedIndex;
        if (selectedIndex == lstEscalate.Items.Count - 1)
        {
            //lblError.Text = "Error:: Cant Move Down Its Already At Last Position";
        }
        else
        {
            ListItem li = lstEscalate.Items[selectedIndex + 1];
            lstEscalate.Items.RemoveAt(selectedIndex + 1);
            lstEscalate.Items.Insert(selectedIndex, li);
        }
    }

    protected void lnkInsert_Click(object sender, EventArgs e)
    {
        if (lstEscalate.Items.Count == 0)
        {
            string query = "insert into dbo.processEscalate values('1','" + ddlEscRoleName.SelectedValue + "'," + ddlProcessType.SelectedValue + ") ";
            Db.constr = myGlobal.getRDDMarketingDBConnectionString();
            Db.myExecuteSQL(query);
        }
        else
        {
            int max = lstEscalate.Items.Cast<ListItem>().Select(s => Convert.ToInt32(s.Value)).Max();
            int max1 = max + 1;
            string query = "insert into dbo.processEscalate values('" + max1 + "','" + ddlEscRoleName.SelectedValue + "'," + ddlProcessType.SelectedValue + ") ";
            Db.constr = myGlobal.getRDDMarketingDBConnectionString();
            Db.myExecuteSQL(query);
        }
            
        lblError.ForeColor = Color.Green;
        lblError.Text = "Success:: All Fields Sucessfully Updated";
        loadEscList();
    }

    protected void btnEscdelete_Click(object sender, EventArgs e)
    {
        string query = "delete from dbo.processEscalate where escalateLevelId='" + lstEscalate.SelectedValue + "' and fk_processId='" + ddlProcessType.SelectedValue + "' ";
        Db.constr = myGlobal.getRDDMarketingDBConnectionString();
        Db.myExecuteSQL(query);
        lblError.ForeColor = Color.Green;
        lblError.Text = "Success:: Selected Field Successfully Deleted";
        loadEscList();
    }

    protected void btnEscReOrder_Click(object sender, EventArgs e)
    {
        int count = lstEscalate.Items.Count;
        for (int i = 0; i < count; i++)
        {
            lstEscalate.SelectedIndex = i;
            int escaleteLevelId = i + 1;
            
            Db.constr = myGlobal.getRDDMarketingDBConnectionString();
            string query = "select roleId from dbo.roles where rolename='" + lstEscalate.Items[i].Text + "' ";
            Db.myExecuteSQL(query);
            DataTable dts = new DataTable();
            dts = Db.myGetDS(query).Tables[0];
            string value = dts.Rows[0][0].ToString();

            string qry = "update dbo.processEscalate set fk_roleId='" + value + "' where escalateLevelId='" + escaleteLevelId + "' and fk_processId=" + ddlProcessType.SelectedValue + " ";
           
            Db.myExecuteSQL(qry);
        }
        loadEscList();
        lblError.ForeColor = Color.Green;
        lblError.Text = "Success:: Roles Updated Successfully";
    }




                                        ///////////////////////////////////////////////////////////////LEVEL 4//////////////////////////////////////////////////////////////////




    public void loadProcessTypeID()
    {
        Db.constr = myGlobal.getRDDMarketingDBConnectionString();
        string qry = "select * from dbo.process_def";
        Db.LoadDDLsWithCon(ddlProcessTypeID, qry, "processName", "processid", Db.constr);
    }

    public void BindGrid()
    {
        if (btnAddField.Enabled == false)
        {
            grdDepartmentProcesses.DataSource = (DataTable)Session["TblGrid"];
            grdDepartmentProcesses.DataBind();
        }
        else
        {
            DataTable dts = new DataTable();
            //string sql = "select a.*,r.roleName as nxtRoleName,r1.roleName as prevRoleName,c.processStatusName as nxtStatusName,c1.processStatusName as prevStatusName from dbo.processStatus as a left join dbo.roles as r on a.nextRole=r.roleId left join dbo.roles as r1 on a.prevRole=r1.roleId left join dbo.processStatus as c on c.processStatusID=a.nextProcessStatusID and a.fk_processId=" + ddlProcessTypeID.SelectedValue + " left join dbo.processStatus as c1 on c1.processStatusID=a.prevProcessStatusID and a.fk_processId=" + ddlProcessTypeID.SelectedValue + " where a.fk_processId=" + ddlProcessTypeID.SelectedValue + " order by a.autoindex";
            string sql = "Select S.*,nxtStatusName=(Select processStatusName From dbo.processStatus where processStatusId=S.nextProcessStatusId and fk_processId=" + ddlProcessTypeID.SelectedValue + "),prevStatusName=(Select processStatusName From dbo.processStatus where processStatusId=S.prevProcessStatusId and fk_processId=" + ddlProcessTypeID.SelectedValue + "),nxtRoleName=(Select roleName From dbo.roles where roleid=S.nextRole and fk_processId=" + ddlProcessTypeID.SelectedValue + "),prevRoleName=(Select roleName From dbo.roles where roleid=S.prevRole and fk_processId=" + ddlProcessTypeID.SelectedValue + ") From dbo.processStatus as S where fk_processId=" + ddlProcessTypeID.SelectedValue;
            Db.constr = myGlobal.getRDDMarketingDBConnectionString();
            dts = Db.myGetDS(sql).Tables[0];
            Session["TblGrid"] = Db.myGetDS(sql).Tables[0];
            grdDepartmentProcesses.DataSource = dts;
            grdDepartmentProcesses.DataBind();
        }
    }

    //public void BindProcessgrid()
    //{
    //    DataTable dts = new DataTable();
    //    string sql = "select * from dbo.processStatus where fk_processId=" + ddlProcessTypeID.SelectedValue + " order by autoindex";
    //    Db.constr = myGlobal.getRDDMarketingDBConnectionString();
    //    dts = Db.myGetDS(sql).Tables[0];
    //    //Session["TblGrid"] = Db.myGetDS(sql).Tables[0];
    //    grdViewProcess.DataSource = dts;
    //    grdViewProcess.DataBind();
    //    if (dts.Rows.Count > 0)
    //        lblMsg.Visible = true;
    //    else
    //        lblMsg.Visible = false;
    //}

    protected void ddlProcessTypeID_SelectedIndexChanged(object sender, EventArgs e)
    {
        BindGrid();
        //BindProcessgrid();
    }

    protected void grdDepartmentProcesses_RowEditing(object sender, GridViewEditEventArgs e)
    {
        DataTable dts1 = new DataTable();
        string query1 = "select * from dbo.roles";
        Db.constr = myGlobal.getRDDMarketingDBConnectionString();
        dts1 = Db.myGetDS(query1).Tables[0];
        if (dts1.Rows.Count == 0)
        {
            lblError1.Text = "Error:: There are no roles defined, Create roles before creating Processes";
            grdDepartmentProcesses.EditIndex = -1;
            btnAddField.Enabled = true;
            BindGrid();
            return;
        }

        Label lblProcessStatusID = grdDepartmentProcesses.Rows[e.NewEditIndex].FindControl("lblProcessStatusID") as Label;
        Label lblProcessName = grdDepartmentProcesses.Rows[e.NewEditIndex].FindControl("lblProcessName") as Label;
        Label lblProcessDescription = grdDepartmentProcesses.Rows[e.NewEditIndex].FindControl("lblProcessDescription") as Label;
        Label lblNextProcessStatusID = grdDepartmentProcesses.Rows[e.NewEditIndex].FindControl("lblNextProcessStatusID") as Label;
        Label lblPrevProcessStatusID = grdDepartmentProcesses.Rows[e.NewEditIndex].FindControl("lblPrevProcessStatusID") as Label;
        Label lblNextRole = grdDepartmentProcesses.Rows[e.NewEditIndex].FindControl("lblNextRole") as Label;
        Label lblPreviousRole = grdDepartmentProcesses.Rows[e.NewEditIndex].FindControl("lblPreviousRole") as Label;


        grdDepartmentProcesses.EditIndex = e.NewEditIndex;
        BindGrid();
        GridViewRow Row = grdDepartmentProcesses.Rows[e.NewEditIndex];

        TextBox txtProcessName1 = (Row.FindControl("txtProcessName") as TextBox);
        if (txtProcessName1.Text == "")
        {
            DropDownList ddlProcessStatusID = Row.FindControl("ddlProcessStatusID") as DropDownList;
            Db.constr = myGlobal.getRDDMarketingDBConnectionString();
            string qry1 = "select processStatusID from dbo.processStatus where fk_processid='" + ddlProcessTypeID.SelectedValue + "'";
            Db.LoadDDLsWithCon(ddlProcessStatusID, qry1, "processStatusID", "processStatusID", Db.constr);
            if (ddlProcessStatusID.Items.Count == 0)
            {
                ddlProcessStatusID.Items.Insert(0, "1");
            }
            else
            {
                DataTable dts = new DataTable();
                string query = "select max(processStatusID) from dbo.processStatus where fk_processid='" + ddlProcessTypeID.SelectedValue + "'";
                Db.constr = myGlobal.getRDDMarketingDBConnectionString();
                dts = Db.myGetDS(query).Tables[0];
                string value = dts.Rows[0][0].ToString();
                int value1 = Convert.ToInt32(value) + 1;
                ddlProcessStatusID.Items.Clear();
                ddlProcessStatusID.Items.Insert(0, Convert.ToString(value1));
            }
            //ddlProcessStatusID.Enabled = false;

            TextBox txtProcessName = (Row.FindControl("txtProcessName") as TextBox);

            TextBox txtProcessDescription = (Row.FindControl("txtProcessDescription") as TextBox);

            DropDownList ddlNextProcessStatusID = Row.FindControl("ddlNextProcessStatusID") as DropDownList;

            DropDownList ddlPrevProcessStatusID = Row.FindControl("ddlPrevProcessStatusID") as DropDownList;

            DropDownList ddlNextRole = Row.FindControl("ddlNextRole") as DropDownList;
            string qry4 = "Select PE.fk_roleId,fk_processId,R.roleName From dbo.processEscalate as PE Join dbo.roles as R ON PE.fk_roleId=R.roleId Where fk_processId=" + ddlProcessType.SelectedValue + " Order By PE.escalateLevelId";
            Db.LoadDDLsWithCon(ddlNextRole, qry4, "roleName", "fk_roleId", Db.constr);
            ddlNextRole.SelectedIndex = -1;

            DropDownList ddlPreviousRole = Row.FindControl("ddlPreviousRole") as DropDownList;
            string qry5 = "Select PE.fk_roleId,fk_processId,R.roleName From dbo.processEscalate as PE Join dbo.roles as R ON PE.fk_roleId=R.roleId Where fk_processId=" + ddlProcessType.SelectedValue + " Order By PE.escalateLevelId";
            Db.LoadDDLsWithCon(ddlPreviousRole, qry5, "roleName", "fk_roleId", Db.constr);
            ddlPreviousRole.SelectedIndex = -1;

        }
        else
        {
            grdDepartmentProcesses.EditIndex = e.NewEditIndex;
            BindGrid();

            GridViewRow rw = grdDepartmentProcesses.Rows[e.NewEditIndex];
            Db.constr = myGlobal.getRDDMarketingDBConnectionString();


            DropDownList ddlProcessStatusID = rw.FindControl("ddlProcessStatusID") as DropDownList;
            string qry1 = "select processStatusID from dbo.processStatus where fk_processid='" + ddlProcessTypeID.SelectedValue + "'";
            Db.LoadDDLsWithCon(ddlProcessStatusID, qry1, "processStatusID", "processStatusID", Db.constr);

            TextBox txtProcessName = (rw.FindControl("txtProcessName") as TextBox);
            TextBox txtProcessDescription = (rw.FindControl("txtProcessDescription") as TextBox);

            DropDownList ddlNextProcessStatusID = rw.FindControl("ddlNextProcessStatusID") as DropDownList;
            string qry2 = "select processStatusName,processStatusID from dbo.processStatus where fk_processid='" + ddlProcessTypeID.SelectedValue + "' order by processStatusID"; //and processStatusName not in('" + txtProcessName.Text + "')";
            Db.LoadDDLsWithCon(ddlNextProcessStatusID, qry2, "processStatusName", "processStatusID", Db.constr);
            if (lblNextProcessStatusID.Text != "-1")
            {
                ListItem val = ddlNextProcessStatusID.Items.FindByValue(lblNextProcessStatusID.Text);
                if (val != null)
                {
                    ddlNextProcessStatusID.Items.FindByValue(lblNextProcessStatusID.Text).Selected = true;
                }
            }
            else
            {
                ddlNextProcessStatusID.SelectedIndex = -1;
            }



            DropDownList ddlPrevProcessStatusID = rw.FindControl("ddlPrevProcessStatusID") as DropDownList;
            string qry3 = "select processStatusName,processStatusID from dbo.processStatus where fk_processid='" + ddlProcessTypeID.SelectedValue + "'  order by processStatusID"; // and processStatusName not in('" + txtProcessName.Text + "')";
            Db.LoadDDLsWithCon(ddlPrevProcessStatusID, qry3, "processStatusName", "processStatusID", Db.constr);
            if (lblPrevProcessStatusID.Text != "-1")
            {
                ListItem val = ddlPrevProcessStatusID.Items.FindByValue(lblPrevProcessStatusID.Text);
                if (val != null)
                {
                    ddlPrevProcessStatusID.Items.FindByValue(lblPrevProcessStatusID.Text).Selected = true;
                }
            }
            else
            {
                ddlPrevProcessStatusID.SelectedIndex = -1;
            }



            DropDownList ddlNextRole = rw.FindControl("ddlNextRole") as DropDownList;
            string qry4 = "Select PE.fk_roleId,fk_processId,R.roleName From dbo.processEscalate as PE Join dbo.roles as R ON PE.fk_roleId=R.roleId Where fk_processId=" + ddlProcessType.SelectedValue + " Order By PE.escalateLevelId";
            Db.LoadDDLsWithCon(ddlNextRole, qry4, "roleName", "fk_roleId", Db.constr);
            if (lblNextRole.Text != "-1")
            {
                ListItem val = ddlNextRole.Items.FindByValue(lblNextRole.Text);
                if (val != null)
                {
                    ddlNextRole.Items.FindByValue(lblNextRole.Text).Selected = true;
                }
            }
            else
            {
                ddlNextRole.SelectedIndex = -1;
            }



            DropDownList ddlPreviousRole = rw.FindControl("ddlPreviousRole") as DropDownList;
            string qry5 = "Select PE.fk_roleId,fk_processId,R.roleName From dbo.processEscalate as PE Join dbo.roles as R ON PE.fk_roleId=R.roleId Where fk_processId=" + ddlProcessType.SelectedValue + " Order By PE.escalateLevelId";
            Db.LoadDDLsWithCon(ddlPreviousRole, qry5, "roleName", "fk_roleId", Db.constr);
            if (lblPreviousRole.Text != "-1")
            {
                ListItem val = ddlPreviousRole.Items.FindByValue(lblPreviousRole.Text);
                if (val != null)
                {
                    ddlPreviousRole.Items.FindByValue(lblPreviousRole.Text).Selected = true;
                }
            }
            else
            {
                ddlPreviousRole.SelectedIndex = -1;
            }
        }
    }

    protected void grdDepartmentProcesses_RowCancelingEdit(object sender, GridViewCancelEditEventArgs e)
    {
        grdDepartmentProcesses.EditIndex = -1;
        btnAddField.Enabled = true;
        BindGrid();
    }

    protected void btnAddField_Click(object sender, EventArgs e)
    {
        DataTable tbl = (DataTable)Session["TblGrid"];
        DataRow drw = tbl.NewRow();
        tbl.Rows.Add(drw);
        Session["TblGrid"] = tbl;
        grdDepartmentProcesses.DataSource = (DataTable)Session["TblGrid"];
        grdDepartmentProcesses.DataBind();
        btnAddField.Enabled = false;
    }

    protected void grdDepartmentProcesses_RowDeleting(object sender, GridViewDeleteEventArgs e)
    {
        if (btnAddField.Enabled == false)
        {
            btnAddField.Enabled = true;
        }
        else
        {

            GridViewRow DeleteRow = grdDepartmentProcesses.Rows[e.RowIndex];

            if (DeleteRow != null)
            {
                Label lblAuto = new Label();
                lblAuto = (Label)DeleteRow.Cells[0].FindControl("lblAuto");

                String qry = "delete from dbo.processStatus where autoIndex=" + lblAuto.Text + "";
                Db.constr = myGlobal.getRDDMarketingDBConnectionString();
                Db.myExecuteSQL(qry);
                lblError1.ForeColor = Color.Green;
                lblError1.Text = "Success:: Record has been deleted successfully";
            }
        }
        BindGrid();
        //BindProcessgrid();
    }

    protected void grdDepartmentProcesses_RowUpdating(object sender, GridViewUpdateEventArgs e)
    {
        GridViewRow myRow = grdDepartmentProcesses.Rows[e.RowIndex];

        if (myRow != null)
        {
            TextBox txtAuto = myRow.FindControl("txtAuto") as TextBox;
            DropDownList ddlProcessStatusID = myRow.FindControl("ddlProcessStatusID") as DropDownList;
            TextBox txtProcessName = (myRow.FindControl("txtProcessName") as TextBox);
            TextBox txtProcessDescription = (myRow.FindControl("txtProcessDescription") as TextBox);
            DropDownList ddlNextProcessStatusID = myRow.FindControl("ddlNextProcessStatusID") as DropDownList;
            DropDownList ddlPrevProcessStatusID = myRow.FindControl("ddlPrevProcessStatusID") as DropDownList;
            DropDownList ddlNextRole = myRow.FindControl("ddlNextRole") as DropDownList;
            DropDownList ddlPreviousRole = myRow.FindControl("ddlPreviousRole") as DropDownList;

            
            if (txtProcessName.Text == "" || txtProcessDescription.Text == "")
            {
                lblError1.Text = "Error:: One or More Fields Left Empty, All field are Compulsary";
                return;
            }

            if (btnAddField.Enabled == true)
            {
                if (ddlNextProcessStatusID.Items.Count == 0)
                {
                    String qry = "update dbo.processStatus set Afk_processid=" + ddlProcessTypeID.SelectedValue + ", processStatusID=" + ddlProcessStatusID.SelectedValue + ", processStatusName='" + txtProcessName.Text + "' ,processStatusDesc='" + txtProcessDescription.Text + "' ,nextprocessStatusID='-1',prevprocessStatusID='-1' ,nextRole='" + ddlNextRole.SelectedValue + "' ,prevRole='" + ddlPreviousRole.SelectedValue + "' where autoIndex=" + txtAuto.Text + ";";
                    Db.constr = myGlobal.getRDDMarketingDBConnectionString();
                    Db.myExecuteSQL(qry);
                }
                else
                {
                    String qry = "update dbo.processStatus set fk_processid=" + ddlProcessTypeID.SelectedValue + ", processStatusID=" + ddlProcessStatusID.SelectedValue + ", processStatusName='" + txtProcessName.Text + "' ,processStatusDesc='" + txtProcessDescription.Text + "' ,nextprocessStatusID='" + ddlNextProcessStatusID.SelectedValue + "',prevprocessStatusID='" + ddlPrevProcessStatusID.SelectedValue + "' ,nextRole='" + ddlNextRole.SelectedValue + "' ,prevRole='" + ddlPreviousRole.SelectedValue + "' where autoIndex=" + txtAuto.Text + ";";
                    Db.constr = myGlobal.getRDDMarketingDBConnectionString();
                    Db.myExecuteSQL(qry);
                }
            }
            else
            {
                String qry = "insert into dbo.processStatus values(" + ddlProcessTypeID.SelectedValue + "," + ddlProcessStatusID.SelectedValue + ",'" + txtProcessName.Text + "','" + txtProcessDescription.Text + "','-1','-1','" + ddlNextRole.SelectedValue + "','" + ddlPreviousRole.SelectedValue + "')";
                Db.constr = myGlobal.getRDDMarketingDBConnectionString();
                Db.myExecuteSQL(qry);
            }

            grdDepartmentProcesses.EditIndex = -1;
            btnAddField.Enabled = true;
            BindGrid();
            lblError1.ForeColor = Color.Green;
            lblError1.Text = "Success:: Record has been updated successfully";
            //BindProcessgrid();
        }
    }


                                              ///////////////////////////////////////-----------------SHOW/HIDE-------------------////////////////////////////




    protected void imgDepartment_Click(object sender, ImageClickEventArgs e)
    {
        if (pnlDepartments.Visible == false)
        {
            pnlEscalation.Visible = false;
            pnlDepartments.Visible = true;
            pnlDepartmentRoles.Visible = false;
            pnlEditRoles.Visible = false;
            pnlProcessState.Visible = false;

            lblProcesses.Font.Underline = false;
            lblProcesses.ForeColor = System.Drawing.Color.White;
            lblDepartment.Font.Underline = true;
            lblDepartment.ForeColor = System.Drawing.Color.Black;
            lblRoles.Font.Underline = false;
            lblRoles.ForeColor = System.Drawing.Color.White;
            lblEsclation.Font.Underline = false;
            lblEsclation.ForeColor = System.Drawing.Color.White;

            imgRoles.ImageUrl = "~/admin/images/pluss.png";
            imgProcesses.ImageUrl = "~/admin/images/pluss.png";
            imgDepartment.ImageUrl = "~/admin/images/minuss.png";
            imgEsclation.ImageUrl = "~/admin/images/pluss.png";
        }
        else
        {
            lblDepartment.Font.Underline = false;
            lblDepartment.ForeColor = System.Drawing.Color.White;

            pnlDepartments.Visible = false;
            imgDepartment.ImageUrl = "~/admin/images/pluss.png";
        }
    }

    protected void imgRoles_Click(object sender, ImageClickEventArgs e)
    {
        if (pnlDepartmentRoles.Visible == false && pnlEditRoles.Visible == false)
        {
            pnlEscalation.Visible = false;
            pnlDepartments.Visible = false;
            pnlDepartmentRoles.Visible = true;
            pnlEditRoles.Visible = true;
            pnlProcessState.Visible = false;

            lblProcesses.Font.Underline = false;
            lblProcesses.ForeColor = System.Drawing.Color.White;
            lblDepartment.Font.Underline = false;
            lblDepartment.ForeColor = System.Drawing.Color.White;
            lblRoles.Font.Underline = true;
            lblRoles.ForeColor = System.Drawing.Color.Black;
            lblEsclation.Font.Underline = false;
            lblEsclation.ForeColor = System.Drawing.Color.White;

            imgRoles.ImageUrl = "~/admin/images/minuss.png";
            imgProcesses.ImageUrl = "~/admin/images/pluss.png";
            imgDepartment.ImageUrl = "~/admin/images/pluss.png";
            imgEsclation.ImageUrl = "~/admin/images/pluss.png";
        }
        else
        {
            lblRoles.Font.Underline = false;
            lblRoles.ForeColor = System.Drawing.Color.White;

            pnlDepartmentRoles.Visible = false;
            pnlEditRoles.Visible = false;
            imgRoles.ImageUrl = "~/admin/images/pluss.png";
        }
    }

    protected void imgProcesses_Click(object sender, ImageClickEventArgs e)
    {
        if (pnlProcessState.Visible == false)
        {
            pnlEscalation.Visible = false;
            pnlDepartments.Visible = false;
            pnlDepartmentRoles.Visible = false;
            pnlEditRoles.Visible = false;
            pnlProcessState.Visible = true;

            lblProcesses.Font.Underline = true;
            lblProcesses.ForeColor = System.Drawing.Color.Black;
            lblDepartment.Font.Underline = false;
            lblDepartment.ForeColor = System.Drawing.Color.White;
            lblRoles.Font.Underline = false;
            lblRoles.ForeColor = System.Drawing.Color.White;
            lblEsclation.Font.Underline = false;
            lblEsclation.ForeColor = System.Drawing.Color.White;

            imgRoles.ImageUrl = "~/admin/images/pluss.png";
            imgProcesses.ImageUrl = "~/admin/images/minuss.png";
            imgDepartment.ImageUrl = "~/admin/images/pluss.png";
            imgEsclation.ImageUrl = "~/admin/images/pluss.png";
        }
        else
        {
            lblProcesses.Font.Underline = false;
            lblProcesses.ForeColor = System.Drawing.Color.White;

            pnlProcessState.Visible = false;
            imgProcesses.ImageUrl = "~/admin/images/pluss.png";
        }
    }

    protected void imgEsclation_Click(object sender, ImageClickEventArgs e)
    {
        if (pnlEscalation.Visible == false)
        {
            pnlEscalation.Visible = true;
            pnlDepartments.Visible = false;
            pnlDepartmentRoles.Visible = false;
            pnlEditRoles.Visible = false;
            pnlProcessState.Visible = false;

            lblProcesses.Font.Underline = false;
            lblProcesses.ForeColor = System.Drawing.Color.White;
            lblDepartment.Font.Underline = false;
            lblDepartment.ForeColor = System.Drawing.Color.White;
            lblRoles.Font.Underline = false;
            lblRoles.ForeColor = System.Drawing.Color.White;
            lblEsclation.Font.Underline = true;
            lblEsclation.ForeColor = System.Drawing.Color.Black;

            imgRoles.ImageUrl = "~/admin/images/pluss.png";
            imgProcesses.ImageUrl = "~/admin/images/pluss.png";
            imgDepartment.ImageUrl = "~/admin/images/pluss.png";
            imgEsclation.ImageUrl = "~/admin/images/minuss.png";
        }
        else
        {
            lblEsclation.Font.Underline = false;
            lblEsclation.ForeColor = System.Drawing.Color.White;

            pnlEscalation.Visible = false;
            imgEsclation.ImageUrl = "~/admin/images/pluss.png";
        }
    }


    
}

