
///////////////////////////////////// Last update 16-may  5.44 pm:  roles and grant users working ok. cleaned commented code , tested creation comments. work on Granting page.
 
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Data;
using System.Data.SqlClient;
using System.Configuration;

public partial class Intranet_orders_SapViewOrdersPO : System.Web.UI.Page
{
    string qry;
    //string[] userRole;
    string whereClauseRoleLine, loginConditionField, loginConditionQuerySuffix,createdByClause;//,workForRole,lblworkForUser.Text;

    protected void Page_Load(object sender, EventArgs e)
    {
        //Page.ClientScript.RegisterStartupScript(this.GetType(), "refresh", "window.setTimeout('var url = window.location.href;window.location.href = url',60000);", true); //refreshes in 60 secs
        if (Request.QueryString["wfTypeId"] != null)
            Session["wfTypeId"] = Request.QueryString["wfTypeId"].ToString();

        if (!IsPostBack)
        {
            //RadWorkingRoles.Items.Clear();
        
            //ListItem lst;
            //string[] userInRoles = myGlobal.loggedInRoleList();
            //foreach (String rs in userInRoles)
            //{
            //    lst = new ListItem();
            //    lst.Value = rs + "-" + myGlobal.loggedInUser();
            //    lst.Text = rs;
            //    RadWorkingRoles.Items.Add(lst);
            //}

            loadRolesAndGrantedPermissions();

            bindDdlProcessType();
            bindDdlStatus();

            bindGridOrders();
            bindGridTasks();
        }
        else
        {
            setRoleNUserlbls();
        }

        string rl = "", sql;
        SqlDataReader dr;
        sql = "select departmentName from dbo.departments where autoindex in (select fk_deptId from dbo.roles where roleId in (select fk_roleid from dbo.processEscalate where escalateLevelId=1 and fk_processId=" + ddlProcessType.SelectedValue.ToString() + "))"; //fetch first loggin role for the process
           
           Db.constr = myGlobal.getIntranetDBConnectionString();
            dr = Db.myGetReader(sql);
            while (dr.Read())
            {
                rl = dr["departmentName"].ToString();
            }
            dr.Close();

               if (lblworkForRole.Text == rl.ToUpper())  
                {
                    if (ddlProcessType.SelectedValue.ToString() == "10011" || ddlProcessType.SelectedValue.ToString() == "10012" || ddlProcessType.SelectedValue.ToString() == "10013" || ddlProcessType.SelectedValue.ToString() == "10021")
                    {
                        panelButtons.Visible = true;
                        if (ddlProcessType.SelectedValue.ToString() == "10011" || ddlProcessType.SelectedValue.ToString() == "10012" || ddlProcessType.SelectedValue.ToString() == "10013")
                        {
                            panelROLinks.Visible = false;
                        }
                        else //if "RO"
                        {
                            panelPOLinks.Visible = false;
                        }
                                                    
                    }
                    else
                        panelButtons.Visible = false;
                }
                else
                    panelButtons.Visible = false;

               if (lblworkForRole.Text == "MARKETINGADMIN" && ddlProcessType.SelectedValue.ToString() == "10031")          
                    panelPlansAccess.Visible = true;
                else
                    panelPlansAccess.Visible = false;

    }

    protected void loadRolesAndGrantedPermissions()
    {

        RadWorkingRoles.Items.Clear();

        ListItem lst;
        string[] userInRoles = myGlobal.loggedInRoleList();
        foreach (String rs in userInRoles)
        {
            lst = new ListItem();
            lst.Value = rs + "-" + myGlobal.loggedInUser();
            lst.Text = rs;
            RadWorkingRoles.Items.Add(lst);
        }

        Db.constr = myGlobal.getIntranetDBConnectionString();
        DataTable dtbGrantedRoles= Db.myGetDS("select grantingRole+'-'+grantingUser as rol,grantingUser usr from dbo.rolesGranted where toUser='" + myGlobal.loggedInUser() + "'").Tables[0];

        //continues to add to existing list from ------- dbo.rolesGranted
        
        foreach (DataRow rw in dtbGrantedRoles.Rows)
        {
            lst = new ListItem();
            lst.Value = rw["rol"].ToString();
            lst.Text = rw["usr"].ToString();
            RadWorkingRoles.Items.Add(lst);
        }

        RadWorkingRoles.SelectedIndex = 0;

        //now set workFor user and role lables
        setRoleNUserlbls();
    }

    protected void bindDdlProcessType()
    {

        if ((Convert.ToInt32(Session["wfTypeId"])) == 10011)
            Db.LoadDDLsWithCon(ddlProcessType, "select processId,processName as pname from dbo.process_def where processId in (10011,10012,10013) order by processId ", "pname", "processId", myGlobal.getIntranetDBConnectionString());
        else
            Db.LoadDDLsWithCon(ddlProcessType, "select processId,processName as pname from dbo.process_def where processId=" + Session["wfTypeId"].ToString() + " order by processId ", "pname", "processId", myGlobal.getIntranetDBConnectionString());

        if (ddlProcessType.Items.Count > 0)
        {
            if (Session["ProcessTypeName"] == null)// || Session["ProcessTypeName"].ToString()=="")
            {
                ddlProcessType.SelectedIndex = 0;
                lblTypeSelected.Text = ddlProcessType.SelectedItem.Text;
                Session["ProcessTypeName"] = ddlProcessType.SelectedItem.Text;  //set session value
            }
            else
            {
                //ddlProcessType.SelectedIndex = 0;
                //lblTypeSelected.Text = ddlProcessType.SelectedItem.Text;
                
                //string sty = Session["ProcessTypeName"].ToString();
                
                ddlProcessType.SelectedIndex = -1;
                ddlProcessType.Items.FindByText(Session["ProcessTypeName"].ToString()).Selected = true;
                lblTypeSelected.Text = ddlProcessType.SelectedItem.Text;
            }
        }
    }
    protected void bindDdlProcessTypeOrg()
    {

        if ((Convert.ToInt32(Session["wfTypeId"])) == 10011)
            Db.LoadDDLsWithCon(ddlProcessType, "select processId,processName as pname from dbo.process_def where processId in (10011,10012,10013) order by processId ", "pname", "processId", myGlobal.getIntranetDBConnectionString());
        else
            Db.LoadDDLsWithCon(ddlProcessType, "select processId,processName as pname from dbo.process_def where processId=" + Session["wfTypeId"].ToString() + " order by processId ", "pname", "processId", myGlobal.getIntranetDBConnectionString());

        if (ddlProcessType.Items.Count > 0)
        {
            ddlProcessType.SelectedIndex = 0;
            lblTypeSelected.Text = ddlProcessType.SelectedItem.Text;
        }
    }

    protected void bindDdlStatus()
    {
        if (ddlProcessType.Items.Count > 0)
        {
            
            SqlDataReader dr;
            Db.constr = myGlobal.getIntranetDBConnectionString();
            dr = Db.myGetReader("select processSubType from dbo.process_def where processId=" + ddlProcessType.SelectedValue.ToString());
            dr.Read();
            lblprocessSubType.Text = dr["processSubType"].ToString();
            dr.Close();
        
            ////////////////

            Db.LoadDDLsWithCon(ddlStatus, "select distinct(ps.processStatusName),pr.fk_StatusId from dbo.processRequest as pr join dbo.processStatus as ps on pr.fk_StatusId=ps.processStatusId and pr.fk_processId=ps.fk_processId where pr.fk_processId=" + ddlProcessType.SelectedValue.ToString() + " order by pr.fk_StatusId", "processStatusName", "fk_StatusId", myGlobal.getIntranetDBConnectionString());
         
            if (ddlStatus.Items.Count > 0)
                ddlStatus.SelectedIndex = 0;
        }
    }

    protected void ddlProcessType_SelectedIndexChanged(object sender, EventArgs e)
    {
        lblTypeSelected.Text = ddlProcessType.SelectedItem.Text;
        //string sty = Session["ProcessTypeName"].ToString();
        Session["ProcessTypeName"] = ddlProcessType.SelectedItem.Text;  //set session value
        //sty = Session["ProcessTypeName"].ToString();

        bindDdlStatus();
        bindGridOrders();
        bindGridTasks();
    }

    protected void chkProcessAll_CheckedChanged(object sender, EventArgs e)
    {
        if (chkProcessAll.Checked == true)
        {
            ddlProcessType.Enabled = false;
            ddlStatus.Enabled = false;
            chkStatusAll.Enabled = false;
        }
        else
        {
            ddlProcessType.Enabled = true;
            chkStatusAll.Enabled = true;

            if (chkStatusAll.Checked == true)
                ddlStatus.Enabled = false;
            else
                ddlStatus.Enabled = true;
        }

        bindGridOrders();
        bindGridTasks();
    }

    protected void ddlStatus_SelectedIndexChanged(object sender, EventArgs e)
    {
        bindGridOrders();
        bindGridTasks();
    }

    protected void chkStatusAll_CheckedChanged(object sender, EventArgs e)
    {
        if (chkStatusAll.Checked == true)
            ddlStatus.Enabled = false;
        else
            ddlStatus.Enabled = true;

        bindGridOrders();
        bindGridTasks();

    }

    private void setLoginConditionField(string pRole,string pUser)
    {
        loginConditionQuerySuffix = "";
        createdByClause = "";

        if (pRole == "PRODUCTSPECIALIST")
            createdByClause = " and k.createdBy='" + pUser + "' ";

        if (Convert.ToInt32(Session["wfTypeId"]) == 10011 || Convert.ToInt32(Session["wfTypeId"]) == 10012 || Convert.ToInt32(Session["wfTypeId"]) == 10013)
        {
            if (pRole == "PRODUCTMANAGER")
            {
                loginConditionField = "bu";  //PM    login case use this field
                loginConditionQuerySuffix = " and k." + loginConditionField + " in (select data from dbo.MySplit((select " + loginConditionField + " from dbo.orderSystemUserMapping where lower(membershipUserRoleName)=lower('" + pRole + "') and lower(membershipusername)=lower('" + pUser + "')),','))";
            }
            else if (pRole == "COUNTRYMANAGER" || pRole == "PRODUCTSPECIALIST")
            {
                loginConditionField = "dbCode";  //CM, PS    login case use this field
                loginConditionQuerySuffix = " and k." + loginConditionField + " in (select data from dbo.MySplit((select " + loginConditionField + " from dbo.orderSystemUserMapping where lower(membershipUserRoleName)=lower('" + pRole + "') and lower(membershipusername)=lower('" + pUser + "')),','))"; 
            }
            else //role other than PRODUCTMANAGER,COUNTRYMANAGER,PRODUCTSPECIALIST
            {
                //no need of field as query will not be suffixed to filter record
                loginConditionQuerySuffix = "";
            }
        }
        else  //process other than PO
        {
            //no need of field as query will not be suffixed to filter record 
            loginConditionQuerySuffix = "";
        }

        loginConditionQuerySuffix = createdByClause + loginConditionQuerySuffix;
    }

    protected void bindGridOrders()
    {
        DataTable dtView;
        dtView = bindGridOrdersNew(lblworkForRole.Text, lblworkForUser.Text);

        string sortExp = "", sortDir = "";
        if (ViewState["SortExpressionGridOrders"] != null)
            sortExp = ViewState["SortExpressionGridOrders"].ToString();
        if (ViewState["SortDirectionGridOrders"] != null)
            sortDir = ViewState["SortDirectionGridOrders"].ToString();

        if (sortExp != string.Empty || sortDir != string.Empty)
        {
            dtView.DefaultView.Sort = sortExp + " " + sortDir;
        }
        gridOrders.DataSource = dtView;
        gridOrders.DataBind();

        lblRecords.Text = dtView.Rows.Count.ToString();
    }
    protected DataTable bindGridOrdersNew(string pRole, string pUser)
    {
        setLoginConditionField(pRole, pUser);

        if (chkProcessAll.Checked == true)
        {
            if ((Convert.ToInt32(Session["wfTypeId"])) == 10011 || (Convert.ToInt32(Session["wfTypeId"])) == 10012 || (Convert.ToInt32(Session["wfTypeId"])) == 10013)  //in claues changes accordingly PO case
                qry = "declare @cnt int ; select @cnt=count(*) from dbo.processEscalate E left join roles R on E.fk_roleId=R.roleId left join departments D on R.fk_deptId=D.autoindex where E.fk_processId in (10011,10012,10013) and R.roleId=(select roleId from roles where fk_deptId=(select autoindex from departments where lower(departmentName)=lower('" + lblworkForRole.Text + "'))); select k.* from ( select @cnt as cnt,isnull(upper(D.departmentName),'CLOSED') as RoleLevel ,isnull((select sum(totalSelling) as totalSelling from dbo.PurchaseOrderlines where fk_poid=pr.refId),0) as totalSelling ,pr.*,ps.processStatusName,pdf.processAbbr,po.dbCode,po.bu,po.vendor,po.customerName,po.createdBy from dbo.processRequest as pr join dbo.processStatus as ps on pr.fk_StatusId=ps.processStatusId and pr.fk_processId=ps.fk_processId join dbo.process_def as pdf on pr.fk_processId=pdf.processId join dbo.PurchaseOrders as po on pr.refId=po.poId left join dbo.processEscalate E on E.escalateLevelId=pr.fk_escalateLevelId and E.fk_processId=pr.fk_processId left join roles R on E.fk_roleId=R.roleId left join departments D on R.fk_deptId=D.autoindex where pr.fk_processId in (10011,10012,10013) ) as k where @cnt>0 " + loginConditionQuerySuffix + " order by k.fk_StatusId";
            else  //MKT or others cases  queries
                qry = "declare @cnt int ; select @cnt=count(*) from dbo.processEscalate E left join roles R on E.fk_roleId=R.roleId left join departments D on R.fk_deptId=D.autoindex where E.fk_processId in (" + Session["wfTypeId"].ToString() + ") and R.roleId=(select roleId from roles where fk_deptId=(select autoindex from departments where lower(departmentName)=lower('" + lblworkForRole.Text + "'))); select k.* from ( select @cnt as cnt,pr.*,ps.processStatusName,pdf.processAbbr,vb.buName as bu,vb.buName as vendor from dbo.processRequest as pr join dbo.processStatus as ps on pr.fk_StatusId=ps.processStatusId and pr.fk_processId=ps.fk_processId join dbo.process_def as pdf on pr.fk_processId=pdf.processId join dbo.workFlowPlans as wf on pr.refId=wf.autoindex left join dbo.VendorBUDef as vb on wf.fk_VendorBU=vb.autoindex where pr.fk_processId in (" + Session["wfTypeId"].ToString() + ") ) as k where @cnt>0 " + loginConditionQuerySuffix + " order by k.fk_StatusId";
        }
        else
        {
            if ((Convert.ToInt32(Session["wfTypeId"])) == 10011 || (Convert.ToInt32(Session["wfTypeId"])) == 10012 || (Convert.ToInt32(Session["wfTypeId"])) == 10013)  //in claues changes accordingly PO case
            {
                if (ddlStatus.Items.Count > 0) //second case will never arise because the status list comes from distinct statuses of the process itself, it means there is no porocess 
                {
                    if (chkStatusAll.Checked == false)
                        qry = "declare @cnt int ; select @cnt=count(*) from dbo.processEscalate E left join roles R on E.fk_roleId=R.roleId left join departments D on R.fk_deptId=D.autoindex where E.fk_processId in (" + Session["wfTypeId"].ToString() + ") and R.roleId=(select roleId from roles where fk_deptId=(select autoindex from departments where lower(departmentName)=lower('" + lblworkForRole.Text + "'))); select k.* from ( select @cnt as cnt,isnull(upper(D.departmentName),'CLOSED') as RoleLevel ,isnull((select sum(totalSelling) as totalSelling from dbo.PurchaseOrderlines where fk_poid=pr.refId),0) as totalSelling ,pr.*,ps.processStatusName,pdf.processAbbr,po.dbCode,po.bu,po.vendor,po.customerName,po.createdBy from dbo.processRequest as pr join dbo.processStatus as ps on pr.fk_StatusId=ps.processStatusId and pr.fk_processId=ps.fk_processId join dbo.process_def as pdf on pr.fk_processId=pdf.processId join dbo.PurchaseOrders as po on pr.refId=po.poId left join dbo.processEscalate E on E.escalateLevelId=pr.fk_escalateLevelId and E.fk_processId=pr.fk_processId left join roles R on E.fk_roleId=R.roleId left join departments D on R.fk_deptId=D.autoindex where pr.fk_processId=" + ddlProcessType.SelectedValue.ToString() + " and pr.fk_statusId=" + ddlStatus.SelectedValue.ToString() + "  ) as k where @cnt>0 " + loginConditionQuerySuffix + " order by k.fk_StatusId";
                    else
                        qry = "declare @cnt int ; select @cnt=count(*) from dbo.processEscalate E left join roles R on E.fk_roleId=R.roleId left join departments D on R.fk_deptId=D.autoindex where E.fk_processId in (" + Session["wfTypeId"].ToString() + ") and R.roleId=(select roleId from roles where fk_deptId=(select autoindex from departments where lower(departmentName)=lower('" + lblworkForRole.Text + "'))); select k.* from ( select @cnt as cnt,isnull(upper(D.departmentName),'CLOSED') as RoleLevel ,isnull((select sum(totalSelling) as totalSelling from dbo.PurchaseOrderlines where fk_poid=pr.refId),0) as totalSelling ,pr.*,ps.processStatusName,pdf.processAbbr,po.dbCode,po.bu,po.vendor,po.customerName,po.createdBy from dbo.processRequest as pr join dbo.processStatus as ps on pr.fk_StatusId=ps.processStatusId and pr.fk_processId=ps.fk_processId join dbo.process_def as pdf on pr.fk_processId=pdf.processId join dbo.PurchaseOrders as po on pr.refId=po.poId left join dbo.processEscalate E on E.escalateLevelId=pr.fk_escalateLevelId and E.fk_processId=pr.fk_processId left join roles R on E.fk_roleId=R.roleId left join departments D on R.fk_deptId=D.autoindex where pr.fk_processId=" + ddlProcessType.SelectedValue.ToString() + "  ) as k where @cnt>0 " + loginConditionQuerySuffix + " order by k.fk_StatusId";
                }
                else
                    qry = "declare @cnt int ; select @cnt=count(*) from dbo.processEscalate E left join roles R on E.fk_roleId=R.roleId left join departments D on R.fk_deptId=D.autoindex where E.fk_processId in (" + Session["wfTypeId"].ToString() + ") and R.roleId=(select roleId from roles where fk_deptId=(select autoindex from departments where lower(departmentName)=lower('" + lblworkForRole.Text + "'))); select k.* from ( select @cnt as cnt,isnull(upper(D.departmentName),'CLOSED') as RoleLevel ,isnull((select sum(totalSelling) as totalSelling from dbo.PurchaseOrderlines where fk_poid=pr.refId),0) as totalSelling ,pr.*,ps.processStatusName,pdf.processAbbr,po.dbCode,po.bu,po.vendor,po.customerName,po.createdBy from dbo.processRequest as pr join dbo.processStatus as ps on pr.fk_StatusId=ps.processStatusId and pr.fk_processId=ps.fk_processId join dbo.process_def as pdf on pr.fk_processId=pdf.processId join dbo.PurchaseOrders as po on pr.refId=po.poId left join dbo.processEscalate E on E.escalateLevelId=pr.fk_escalateLevelId and E.fk_processId=pr.fk_processId left join roles R on E.fk_roleId=R.roleId left join departments D on R.fk_deptId=D.autoindex where pr.fk_processId=" + ddlProcessType.SelectedValue.ToString() + "  ) as k where @cnt>0 " + loginConditionQuerySuffix + " order by k.fk_StatusId";
            }
            else    //MKT or others cases queries
            {
                if (ddlStatus.Items.Count > 0) //second case will never arise because the status list comes from distinct statuses of the process itself, it means there is no porocess 
                {
                    if (chkStatusAll.Checked == false)
                        qry = "declare @cnt int ; select @cnt=count(*) from dbo.processEscalate E left join roles R on E.fk_roleId=R.roleId left join departments D on R.fk_deptId=D.autoindex where E.fk_processId in (" + Session["wfTypeId"].ToString() + ") and R.roleId=(select roleId from roles where fk_deptId=(select autoindex from departments where lower(departmentName)=lower('" + lblworkForRole.Text + "'))); select k.* from ( select @cnt as cnt,pr.*,ps.processStatusName,pdf.processAbbr,vb.buName as bu,vb.buName as vendor from dbo.processRequest as pr join dbo.processStatus as ps on pr.fk_StatusId=ps.processStatusId and pr.fk_processId=ps.fk_processId join dbo.process_def as pdf on pr.fk_processId=pdf.processId join dbo.workFlowPlans as wf on pr.refId=wf.autoindex left join dbo.VendorBUDef as vb on wf.fk_VendorBU=vb.autoindex where pr.fk_processId=" + ddlProcessType.SelectedValue.ToString() + " and pr.fk_statusId=" + ddlStatus.SelectedValue.ToString() + "  ) as k where @cnt>0 " + loginConditionQuerySuffix + " order by k.fk_StatusId";
                    else
                        qry = "declare @cnt int ; select @cnt=count(*) from dbo.processEscalate E left join roles R on E.fk_roleId=R.roleId left join departments D on R.fk_deptId=D.autoindex where E.fk_processId in (" + Session["wfTypeId"].ToString() + ") and R.roleId=(select roleId from roles where fk_deptId=(select autoindex from departments where lower(departmentName)=lower('" + lblworkForRole.Text + "'))); select k.* from ( select @cnt as cnt,pr.*,ps.processStatusName,pdf.processAbbr,vb.buName as bu,vb.buName as vendor from dbo.processRequest as pr join dbo.processStatus as ps on pr.fk_StatusId=ps.processStatusId and pr.fk_processId=ps.fk_processId join dbo.process_def as pdf on pr.fk_processId=pdf.processId join dbo.workFlowPlans as wf on pr.refId=wf.autoindex left join dbo.VendorBUDef as vb on wf.fk_VendorBU=vb.autoindex where pr.fk_processId=" + ddlProcessType.SelectedValue.ToString() + "  ) as k where @cnt>0 " + loginConditionQuerySuffix + " order by k.fk_StatusId";
                }
                else
                    qry = "declare @cnt int ; select @cnt=count(*) from dbo.processEscalate E left join roles R on E.fk_roleId=R.roleId left join departments D on R.fk_deptId=D.autoindex where E.fk_processId in (" + Session["wfTypeId"].ToString() + ") and R.roleId=(select roleId from roles where fk_deptId=(select autoindex from departments where lower(departmentName)=lower('" + lblworkForRole.Text + "'))); select k.* from ( select @cnt as cnt,pr.*,ps.processStatusName,pdf.processAbbr,vb.buName as bu,vb.buName as vendor from dbo.processRequest as pr join dbo.processStatus as ps on pr.fk_StatusId=ps.processStatusId and pr.fk_processId=ps.fk_processId join dbo.process_def as pdf on pr.fk_processId=pdf.processId join dbo.workFlowPlans as wf on pr.refId=wf.autoindex left join dbo.VendorBUDef as vb on wf.fk_VendorBU=vb.autoindex where pr.fk_processId=" + ddlProcessType.SelectedValue.ToString() + "  ) as k where @cnt>0 " + loginConditionQuerySuffix + " order by k.fk_StatusId";
            }
        }

        Db.constr = myGlobal.getIntranetDBConnectionString();
        return Db.myGetDS(qry).Tables[0];
    }
    protected DataTable bindGridOrdersNewOrg(string pRole, string pUser)
    {
        setLoginConditionField(pRole, pUser);

        if (chkProcessAll.Checked == true)
        {
            if ((Convert.ToInt32(Session["wfTypeId"])) == 10011 || (Convert.ToInt32(Session["wfTypeId"])) == 10012 || (Convert.ToInt32(Session["wfTypeId"])) == 10013)  //in claues changes accordingly PO case
                qry = "declare @cnt int ; select @cnt=count(*) from dbo.processEscalate E left join roles R on E.fk_roleId=R.roleId left join departments D on R.fk_deptId=D.autoindex where E.fk_processId in (10011,10012,10013) and R.roleId=(select roleId from roles where fk_deptId=(select autoindex from departments where lower(departmentName)=lower('" + lblworkForRole.Text + "'))); select k.* from ( select @cnt as cnt,pr.*,ps.processStatusName,pdf.processAbbr,po.dbCode,po.bu,po.vendor,po.customerName,po.createdBy from dbo.processRequest as pr join dbo.processStatus as ps on pr.fk_StatusId=ps.processStatusId and pr.fk_processId=ps.fk_processId join dbo.process_def as pdf on pr.fk_processId=pdf.processId join dbo.PurchaseOrders as po on pr.refId=po.poId where pr.fk_processId in (10011,10012,10013) ) as k where @cnt>0 " + loginConditionQuerySuffix + " order by k.fk_StatusId";
            else  //MKT or others cases  queries
                qry = "declare @cnt int ; select @cnt=count(*) from dbo.processEscalate E left join roles R on E.fk_roleId=R.roleId left join departments D on R.fk_deptId=D.autoindex where E.fk_processId in (" + Session["wfTypeId"].ToString() + ") and R.roleId=(select roleId from roles where fk_deptId=(select autoindex from departments where lower(departmentName)=lower('" + lblworkForRole.Text + "'))); select k.* from ( select @cnt as cnt,pr.*,ps.processStatusName,pdf.processAbbr,vb.buName as bu,vb.buName as vendor from dbo.processRequest as pr join dbo.processStatus as ps on pr.fk_StatusId=ps.processStatusId and pr.fk_processId=ps.fk_processId join dbo.process_def as pdf on pr.fk_processId=pdf.processId join dbo.workFlowPlans as wf on pr.refId=wf.autoindex left join dbo.VendorBUDef as vb on wf.fk_VendorBU=vb.autoindex where pr.fk_processId in (" + Session["wfTypeId"].ToString() + ") ) as k where @cnt>0 " + loginConditionQuerySuffix + " order by k.fk_StatusId";
        }
        else
        {
            if ((Convert.ToInt32(Session["wfTypeId"])) == 10011 || (Convert.ToInt32(Session["wfTypeId"])) == 10012 || (Convert.ToInt32(Session["wfTypeId"])) == 10013)  //in claues changes accordingly PO case
            {
                if (ddlStatus.Items.Count > 0) //second case will never arise because the status list comes from distinct statuses of the process itself, it means there is no porocess 
                {
                    if (chkStatusAll.Checked == false)
                        qry = "declare @cnt int ; select @cnt=count(*) from dbo.processEscalate E left join roles R on E.fk_roleId=R.roleId left join departments D on R.fk_deptId=D.autoindex where E.fk_processId in (" + Session["wfTypeId"].ToString() + ") and R.roleId=(select roleId from roles where fk_deptId=(select autoindex from departments where lower(departmentName)=lower('" + lblworkForRole.Text + "'))); select k.* from ( select @cnt as cnt,pr.*,ps.processStatusName,pdf.processAbbr,po.dbCode,po.bu,po.vendor,po.customerName,po.createdBy from dbo.processRequest as pr join dbo.processStatus as ps on pr.fk_StatusId=ps.processStatusId and pr.fk_processId=ps.fk_processId join dbo.process_def as pdf on pr.fk_processId=pdf.processId join dbo.PurchaseOrders as po on pr.refId=po.poId where pr.fk_processId=" + ddlProcessType.SelectedValue.ToString() + " and pr.fk_statusId=" + ddlStatus.SelectedValue.ToString() + "  ) as k where @cnt>0 " + loginConditionQuerySuffix + " order by k.fk_StatusId";
                    else
                        qry = "declare @cnt int ; select @cnt=count(*) from dbo.processEscalate E left join roles R on E.fk_roleId=R.roleId left join departments D on R.fk_deptId=D.autoindex where E.fk_processId in (" + Session["wfTypeId"].ToString() + ") and R.roleId=(select roleId from roles where fk_deptId=(select autoindex from departments where lower(departmentName)=lower('" + lblworkForRole.Text + "'))); select k.* from ( select @cnt as cnt,pr.*,ps.processStatusName,pdf.processAbbr,po.dbCode,po.bu,po.vendor,po.customerName,po.createdBy from dbo.processRequest as pr join dbo.processStatus as ps on pr.fk_StatusId=ps.processStatusId and pr.fk_processId=ps.fk_processId join dbo.process_def as pdf on pr.fk_processId=pdf.processId join dbo.PurchaseOrders as po on pr.refId=po.poId where pr.fk_processId=" + ddlProcessType.SelectedValue.ToString() + "  ) as k where @cnt>0 " + loginConditionQuerySuffix + " order by k.fk_StatusId";
                }
                else
                    qry = "declare @cnt int ; select @cnt=count(*) from dbo.processEscalate E left join roles R on E.fk_roleId=R.roleId left join departments D on R.fk_deptId=D.autoindex where E.fk_processId in (" + Session["wfTypeId"].ToString() + ") and R.roleId=(select roleId from roles where fk_deptId=(select autoindex from departments where lower(departmentName)=lower('" + lblworkForRole.Text + "'))); select k.* from ( select @cnt as cnt,pr.*,ps.processStatusName,pdf.processAbbr,po.dbCode,po.bu,po.vendor,po.customerName,po.createdBy from dbo.processRequest as pr join dbo.processStatus as ps on pr.fk_StatusId=ps.processStatusId and pr.fk_processId=ps.fk_processId join dbo.process_def as pdf on pr.fk_processId=pdf.processId join dbo.PurchaseOrders as po on pr.refId=po.poId where pr.fk_processId=" + ddlProcessType.SelectedValue.ToString() + "  ) as k where @cnt>0 " + loginConditionQuerySuffix + " order by k.fk_StatusId";
            }
            else    //MKT or others cases queries
            {
                if (ddlStatus.Items.Count > 0) //second case will never arise because the status list comes from distinct statuses of the process itself, it means there is no porocess 
                {
                    if (chkStatusAll.Checked == false)
                        qry = "declare @cnt int ; select @cnt=count(*) from dbo.processEscalate E left join roles R on E.fk_roleId=R.roleId left join departments D on R.fk_deptId=D.autoindex where E.fk_processId in (" + Session["wfTypeId"].ToString() + ") and R.roleId=(select roleId from roles where fk_deptId=(select autoindex from departments where lower(departmentName)=lower('" + lblworkForRole.Text + "'))); select k.* from ( select @cnt as cnt,pr.*,ps.processStatusName,pdf.processAbbr,vb.buName as bu,vb.buName as vendor from dbo.processRequest as pr join dbo.processStatus as ps on pr.fk_StatusId=ps.processStatusId and pr.fk_processId=ps.fk_processId join dbo.process_def as pdf on pr.fk_processId=pdf.processId join dbo.workFlowPlans as wf on pr.refId=wf.autoindex left join dbo.VendorBUDef as vb on wf.fk_VendorBU=vb.autoindex where pr.fk_processId=" + ddlProcessType.SelectedValue.ToString() + " and pr.fk_statusId=" + ddlStatus.SelectedValue.ToString() + "  ) as k where @cnt>0 " + loginConditionQuerySuffix + " order by k.fk_StatusId";
                    else
                        qry = "declare @cnt int ; select @cnt=count(*) from dbo.processEscalate E left join roles R on E.fk_roleId=R.roleId left join departments D on R.fk_deptId=D.autoindex where E.fk_processId in (" + Session["wfTypeId"].ToString() + ") and R.roleId=(select roleId from roles where fk_deptId=(select autoindex from departments where lower(departmentName)=lower('" + lblworkForRole.Text + "'))); select k.* from ( select @cnt as cnt,pr.*,ps.processStatusName,pdf.processAbbr,vb.buName as bu,vb.buName as vendor from dbo.processRequest as pr join dbo.processStatus as ps on pr.fk_StatusId=ps.processStatusId and pr.fk_processId=ps.fk_processId join dbo.process_def as pdf on pr.fk_processId=pdf.processId join dbo.workFlowPlans as wf on pr.refId=wf.autoindex left join dbo.VendorBUDef as vb on wf.fk_VendorBU=vb.autoindex where pr.fk_processId=" + ddlProcessType.SelectedValue.ToString() + "  ) as k where @cnt>0 " + loginConditionQuerySuffix + " order by k.fk_StatusId";
                }
                else
                    qry = "declare @cnt int ; select @cnt=count(*) from dbo.processEscalate E left join roles R on E.fk_roleId=R.roleId left join departments D on R.fk_deptId=D.autoindex where E.fk_processId in (" + Session["wfTypeId"].ToString() + ") and R.roleId=(select roleId from roles where fk_deptId=(select autoindex from departments where lower(departmentName)=lower('" + lblworkForRole.Text + "'))); select k.* from ( select @cnt as cnt,pr.*,ps.processStatusName,pdf.processAbbr,vb.buName as bu,vb.buName as vendor from dbo.processRequest as pr join dbo.processStatus as ps on pr.fk_StatusId=ps.processStatusId and pr.fk_processId=ps.fk_processId join dbo.process_def as pdf on pr.fk_processId=pdf.processId join dbo.workFlowPlans as wf on pr.refId=wf.autoindex left join dbo.VendorBUDef as vb on wf.fk_VendorBU=vb.autoindex where pr.fk_processId=" + ddlProcessType.SelectedValue.ToString() + "  ) as k where @cnt>0 " + loginConditionQuerySuffix + " order by k.fk_StatusId";
            }
        }
        
        Db.constr = myGlobal.getIntranetDBConnectionString();
        return Db.myGetDS(qry).Tables[0];
    }

    protected void bindGridTasks()
    {
        DataTable dtTasks;
        dtTasks = bindGridTasksNew(lblworkForRole.Text, lblworkForUser.Text);

        string sortExp = "", sortDir = "";

        if (ViewState["SortExpressionGridTasks"] != null)
            sortExp = ViewState["SortExpressionGridTasks"].ToString();
        if (ViewState["SortDirectionGridTasks"] != null)
            sortDir = ViewState["SortDirectionGridTasks"].ToString();

        if (sortExp != string.Empty || sortDir != string.Empty)
        {
            dtTasks.DefaultView.Sort = sortExp + " " + sortDir;
        }
        gridTasks.DataSource = dtTasks;
        gridTasks.DataBind();

        lblRecords1.Text = dtTasks.Rows.Count.ToString();
    }

    protected DataTable bindGridTasksNew(string pRole, string pUser)
    {
        setLoginConditionField(pRole, pUser);

        if (chkProcessAll.Checked == true)
        {
            if ((Convert.ToInt32(Session["wfTypeId"])) == 10011 || (Convert.ToInt32(Session["wfTypeId"])) == 10012 || (Convert.ToInt32(Session["wfTypeId"])) == 10013)  //in claues changes accordingly PO case
                qry = "declare @cnt int ; select @cnt=count(*) from dbo.processEscalate E left join roles R on E.fk_roleId=R.roleId left join departments D on R.fk_deptId=D.autoindex where E.fk_processId in (10011,10012,10013) and R.roleId=(select roleId from roles where fk_deptId=(select autoindex from departments where lower(departmentName)=lower('" + lblworkForRole.Text + "'))); select k.* from ( select @cnt as cnt,isnull(upper(D.departmentName),'CLOSED') as RoleLevel ,isnull((select sum(totalSelling) as totalSelling from dbo.PurchaseOrderlines where fk_poid=pr.refId),0) as totalSelling,pr.*,ps.processStatusName,pdf.processAbbr,po.dbCode,po.bu,po.vendor,po.customerName,po.createdBy from dbo.processRequest as pr join dbo.processStatus as ps on pr.fk_StatusId=ps.processStatusId and pr.fk_processId=ps.fk_processId join dbo.process_def as pdf on pr.fk_processId=pdf.processId join dbo.PurchaseOrders as po on pr.refId=po.poId left join dbo.processEscalate E on E.escalateLevelId=pr.fk_escalateLevelId and E.fk_processId=pr.fk_processId left join roles R on E.fk_roleId=R.roleId left join departments D on R.fk_deptId=D.autoindex  where pr.fk_escalateLevelId in (select roleId from dbo.roles where fk_deptId in (select autoindex from departments where departmentName in (" + whereClauseRoleLine + "))) and pr.fk_processId in (10011,10012,10013) ) as k where @cnt>0 " + loginConditionQuerySuffix + " order by k.fk_StatusId";
            else  //MKT or others cases  queries
                qry = "declare @cnt int ; select @cnt=count(*) from dbo.processEscalate E left join roles R on E.fk_roleId=R.roleId left join departments D on R.fk_deptId=D.autoindex where E.fk_processId in (" + Session["wfTypeId"].ToString() + ") and R.roleId=(select roleId from roles where fk_deptId=(select autoindex from departments where lower(departmentName)=lower('" + lblworkForRole.Text + "'))); select k.* from ( select @cnt as cnt,pr.*,ps.processStatusName,pdf.processAbbr,vb.buName as bu,vb.buName as vendor from dbo.processRequest as pr join dbo.processStatus as ps on pr.fk_StatusId=ps.processStatusId and pr.fk_processId=ps.fk_processId join dbo.process_def as pdf on pr.fk_processId=pdf.processId join dbo.workFlowPlans as wf on pr.refId=wf.autoindex left join dbo.VendorBUDef as vb on wf.fk_VendorBU=vb.autoindex where pr.fk_escalateLevelId in (select roleId from dbo.roles where fk_deptId in (select autoindex from departments where departmentName in (" + whereClauseRoleLine + "))) and pr.fk_processId in (" + Session["wfTypeId"].ToString() + ") ) as k where @cnt>0 " + loginConditionQuerySuffix + " order by k.fk_StatusId";
        }
        else
        {
            if ((Convert.ToInt32(Session["wfTypeId"])) == 10011 || (Convert.ToInt32(Session["wfTypeId"])) == 10012 || (Convert.ToInt32(Session["wfTypeId"])) == 10013)  //in claues changes accordingly PO case
            {
                if (ddlStatus.Items.Count > 0) //second case will never arise because the status list comes from distinct statuses of the process itself, it means there is no porocess 
                {
                    if (chkStatusAll.Checked == false)
                        qry = "declare @cnt int ; select @cnt=count(*) from dbo.processEscalate E left join roles R on E.fk_roleId=R.roleId left join departments D on R.fk_deptId=D.autoindex where E.fk_processId in (" + Session["wfTypeId"].ToString() + ") and R.roleId=(select roleId from roles where fk_deptId=(select autoindex from departments where lower(departmentName)=lower('" + lblworkForRole.Text + "'))); select k.* from ( select @cnt as cnt,isnull(upper(D.departmentName),'CLOSED') as RoleLevel ,isnull((select sum(totalSelling) as totalSelling from dbo.PurchaseOrderlines where fk_poid=pr.refId),0) as totalSelling,pr.*,ps.processStatusName,pdf.processAbbr,po.dbCode,po.bu,po.vendor,po.customerName,po.createdBy from dbo.processRequest as pr join dbo.processStatus as ps on pr.fk_StatusId=ps.processStatusId and pr.fk_processId=ps.fk_processId join dbo.process_def as pdf on pr.fk_processId=pdf.processId join dbo.PurchaseOrders as po on pr.refId=po.poId left join dbo.processEscalate E on E.escalateLevelId=pr.fk_escalateLevelId and E.fk_processId=pr.fk_processId left join roles R on E.fk_roleId=R.roleId left join departments D on R.fk_deptId=D.autoindex where pr.fk_escalateLevelId in (select roleId from dbo.roles where fk_deptId in (select autoindex from departments where departmentName in (" + whereClauseRoleLine + "))) and pr.fk_processId=" + ddlProcessType.SelectedValue.ToString() + " and pr.fk_statusId=" + ddlStatus.SelectedValue.ToString() + " ) as k where @cnt>0 " + loginConditionQuerySuffix + " order by k.fk_StatusId";
                    else
                        qry = "declare @cnt int ; select @cnt=count(*) from dbo.processEscalate E left join roles R on E.fk_roleId=R.roleId left join departments D on R.fk_deptId=D.autoindex where E.fk_processId in (" + Session["wfTypeId"].ToString() + ") and R.roleId=(select roleId from roles where fk_deptId=(select autoindex from departments where lower(departmentName)=lower('" + lblworkForRole.Text + "'))); select k.* from ( select @cnt as cnt,isnull(upper(D.departmentName),'CLOSED') as RoleLevel ,isnull((select sum(totalSelling) as totalSelling from dbo.PurchaseOrderlines where fk_poid=pr.refId),0) as totalSelling,pr.*,ps.processStatusName,pdf.processAbbr,po.dbCode,po.bu,po.vendor,po.customerName,po.createdBy from dbo.processRequest as pr join dbo.processStatus as ps on pr.fk_StatusId=ps.processStatusId and pr.fk_processId=ps.fk_processId join dbo.process_def as pdf on pr.fk_processId=pdf.processId join dbo.PurchaseOrders as po on pr.refId=po.poId left join dbo.processEscalate E on E.escalateLevelId=pr.fk_escalateLevelId and E.fk_processId=pr.fk_processId left join roles R on E.fk_roleId=R.roleId left join departments D on R.fk_deptId=D.autoindex where pr.fk_escalateLevelId in (select roleId from dbo.roles where fk_deptId in (select autoindex from departments where departmentName in (" + whereClauseRoleLine + "))) and pr.fk_processId=" + ddlProcessType.SelectedValue.ToString() + " ) as k where @cnt>0 " + loginConditionQuerySuffix + " order by k.fk_StatusId";
                }
                else
                    qry = "declare @cnt int ; select @cnt=count(*) from dbo.processEscalate E left join roles R on E.fk_roleId=R.roleId left join departments D on R.fk_deptId=D.autoindex where E.fk_processId in (" + Session["wfTypeId"].ToString() + ") and R.roleId=(select roleId from roles where fk_deptId=(select autoindex from departments where lower(departmentName)=lower('" + lblworkForRole.Text + "'))); select k.* from ( select @cnt as cnt,isnull(upper(D.departmentName),'CLOSED') as RoleLevel ,isnull((select sum(totalSelling) as totalSelling from dbo.PurchaseOrderlines where fk_poid=pr.refId),0) as totalSelling,pr.*,ps.processStatusName,pdf.processAbbr,po.dbCode,po.bu,po.vendor,po.customerName,po.createdBy from dbo.processRequest as pr join dbo.processStatus as ps on pr.fk_StatusId=ps.processStatusId and pr.fk_processId=ps.fk_processId join dbo.process_def as pdf on pr.fk_processId=pdf.processId join dbo.PurchaseOrders as po on pr.refId=po.poId left join dbo.processEscalate E on E.escalateLevelId=pr.fk_escalateLevelId and E.fk_processId=pr.fk_processId left join roles R on E.fk_roleId=R.roleId left join departments D on R.fk_deptId=D.autoindex where pr.fk_escalateLevelId in (select roleId from dbo.roles where fk_deptId in (select autoindex from departments where departmentName in (" + whereClauseRoleLine + "))) and pr.fk_processId=" + ddlProcessType.SelectedValue.ToString() + " ) as k where @cnt>0 " + loginConditionQuerySuffix + " order by k.fk_StatusId";
            }
            else    //MKT or others cases queries
            {
                if (ddlStatus.Items.Count > 0) //second case will never arise because the status list comes from distinct statuses of the process itself, it means there is no porocess 
                {
                    if (chkStatusAll.Checked == false)
                        qry = "declare @cnt int ; select @cnt=count(*) from dbo.processEscalate E left join roles R on E.fk_roleId=R.roleId left join departments D on R.fk_deptId=D.autoindex where E.fk_processId in (" + Session["wfTypeId"].ToString() + ") and R.roleId=(select roleId from roles where fk_deptId=(select autoindex from departments where lower(departmentName)=lower('" + lblworkForRole.Text + "'))); select k.* from ( select @cnt as cnt,pr.*,ps.processStatusName,pdf.processAbbr,vb.buName as bu,vb.buName as vendor from dbo.processRequest as pr join dbo.processStatus as ps on pr.fk_StatusId=ps.processStatusId and pr.fk_processId=ps.fk_processId join dbo.process_def as pdf on pr.fk_processId=pdf.processId join dbo.workFlowPlans as wf on pr.refId=wf.autoindex left join dbo.VendorBUDef as vb on wf.fk_VendorBU=vb.autoindex where pr.fk_escalateLevelId in (select roleId from dbo.roles where fk_deptId in (select autoindex from departments where departmentName in (" + whereClauseRoleLine + "))) and pr.fk_processId=" + ddlProcessType.SelectedValue.ToString() + " and pr.fk_statusId=" + ddlStatus.SelectedValue.ToString() + " ) as k where @cnt>0 " + loginConditionQuerySuffix + " order by k.fk_StatusId";
                    else
                        qry = "declare @cnt int ; select @cnt=count(*) from dbo.processEscalate E left join roles R on E.fk_roleId=R.roleId left join departments D on R.fk_deptId=D.autoindex where E.fk_processId in (" + Session["wfTypeId"].ToString() + ") and R.roleId=(select roleId from roles where fk_deptId=(select autoindex from departments where lower(departmentName)=lower('" + lblworkForRole.Text + "'))); select k.* from ( select @cnt as cnt,pr.*,ps.processStatusName,pdf.processAbbr,vb.buName as bu,vb.buName as vendor from dbo.processRequest as pr join dbo.processStatus as ps on pr.fk_StatusId=ps.processStatusId and pr.fk_processId=ps.fk_processId join dbo.process_def as pdf on pr.fk_processId=pdf.processId join dbo.workFlowPlans as wf on pr.refId=wf.autoindex left join dbo.VendorBUDef as vb on wf.fk_VendorBU=vb.autoindex where pr.fk_escalateLevelId in (select roleId from dbo.roles where fk_deptId in (select autoindex from departments where departmentName in (" + whereClauseRoleLine + "))) and pr.fk_processId=" + ddlProcessType.SelectedValue.ToString() + " ) as k where @cnt>0 " + loginConditionQuerySuffix + " order by k.fk_StatusId";
                }
                else
                    qry = "declare @cnt int ; select @cnt=count(*) from dbo.processEscalate E left join roles R on E.fk_roleId=R.roleId left join departments D on R.fk_deptId=D.autoindex where E.fk_processId in (" + Session["wfTypeId"].ToString() + ") and R.roleId=(select roleId from roles where fk_deptId=(select autoindex from departments where lower(departmentName)=lower('" + lblworkForRole.Text + "'))); select k.* from ( select @cnt as cnt,pr.*,ps.processStatusName,pdf.processAbbr,vb.buName as bu,vb.buName as vendor from dbo.processRequest as pr join dbo.processStatus as ps on pr.fk_StatusId=ps.processStatusId and pr.fk_processId=ps.fk_processId join dbo.process_def as pdf on pr.fk_processId=pdf.processId join dbo.workFlowPlans as wf on pr.refId=wf.autoindex left join dbo.VendorBUDef as vb on wf.fk_VendorBU=vb.autoindex where pr.fk_escalateLevelId in (select roleId from dbo.roles where fk_deptId in (select autoindex from departments where departmentName in (" + whereClauseRoleLine + "))) and pr.fk_processId=" + ddlProcessType.SelectedValue.ToString() + " ) as k where @cnt>0 " + loginConditionQuerySuffix + " order by k.fk_StatusId";
            }
        }

        Db.constr = myGlobal.getIntranetDBConnectionString();
        return Db.myGetDS(qry).Tables[0];
    }

    protected DataTable bindGridTasksNewOrg(string pRole, string pUser)
    {
        setLoginConditionField(pRole, pUser);

        if (chkProcessAll.Checked == true)
        {
            if ((Convert.ToInt32(Session["wfTypeId"])) == 10011 || (Convert.ToInt32(Session["wfTypeId"])) == 10012 || (Convert.ToInt32(Session["wfTypeId"])) == 10013)  //in claues changes accordingly PO case
                qry = "declare @cnt int ; select @cnt=count(*) from dbo.processEscalate E left join roles R on E.fk_roleId=R.roleId left join departments D on R.fk_deptId=D.autoindex where E.fk_processId in (10011,10012,10013) and R.roleId=(select roleId from roles where fk_deptId=(select autoindex from departments where lower(departmentName)=lower('" + lblworkForRole.Text + "'))); select k.* from ( select @cnt as cnt,pr.*,ps.processStatusName,pdf.processAbbr,po.dbCode,po.bu,po.vendor,po.customerName,po.createdBy from dbo.processRequest as pr join dbo.processStatus as ps on pr.fk_StatusId=ps.processStatusId and pr.fk_processId=ps.fk_processId join dbo.process_def as pdf on pr.fk_processId=pdf.processId join dbo.PurchaseOrders as po on pr.refId=po.poId where pr.fk_escalateLevelId in (select roleId from dbo.roles where fk_deptId in (select autoindex from departments where departmentName in (" + whereClauseRoleLine + "))) and pr.fk_processId in (10011,10012,10013) ) as k where @cnt>0 " + loginConditionQuerySuffix + " order by k.fk_StatusId";
            else  //MKT or others cases  queries
                qry = "declare @cnt int ; select @cnt=count(*) from dbo.processEscalate E left join roles R on E.fk_roleId=R.roleId left join departments D on R.fk_deptId=D.autoindex where E.fk_processId in (" + Session["wfTypeId"].ToString() + ") and R.roleId=(select roleId from roles where fk_deptId=(select autoindex from departments where lower(departmentName)=lower('" + lblworkForRole.Text + "'))); select k.* from ( select @cnt as cnt,pr.*,ps.processStatusName,pdf.processAbbr,vb.buName as bu,vb.buName as vendor from dbo.processRequest as pr join dbo.processStatus as ps on pr.fk_StatusId=ps.processStatusId and pr.fk_processId=ps.fk_processId join dbo.process_def as pdf on pr.fk_processId=pdf.processId join dbo.workFlowPlans as wf on pr.refId=wf.autoindex left join dbo.VendorBUDef as vb on wf.fk_VendorBU=vb.autoindex where pr.fk_escalateLevelId in (select roleId from dbo.roles where fk_deptId in (select autoindex from departments where departmentName in (" + whereClauseRoleLine + "))) and pr.fk_processId in (" + Session["wfTypeId"].ToString() + ") ) as k where @cnt>0 " + loginConditionQuerySuffix + " order by k.fk_StatusId";
        }
        else
        {
            if ((Convert.ToInt32(Session["wfTypeId"])) == 10011 || (Convert.ToInt32(Session["wfTypeId"])) == 10012 || (Convert.ToInt32(Session["wfTypeId"])) == 10013)  //in claues changes accordingly PO case
            {
                if (ddlStatus.Items.Count > 0) //second case will never arise because the status list comes from distinct statuses of the process itself, it means there is no porocess 
                {
                    if (chkStatusAll.Checked == false)
                        qry = "declare @cnt int ; select @cnt=count(*) from dbo.processEscalate E left join roles R on E.fk_roleId=R.roleId left join departments D on R.fk_deptId=D.autoindex where E.fk_processId in (" + Session["wfTypeId"].ToString() + ") and R.roleId=(select roleId from roles where fk_deptId=(select autoindex from departments where lower(departmentName)=lower('" + lblworkForRole.Text + "'))); select k.* from ( select @cnt as cnt,pr.*,ps.processStatusName,pdf.processAbbr,po.dbCode,po.bu,po.vendor,po.customerName,po.createdBy from dbo.processRequest as pr join dbo.processStatus as ps on pr.fk_StatusId=ps.processStatusId and pr.fk_processId=ps.fk_processId join dbo.process_def as pdf on pr.fk_processId=pdf.processId join dbo.PurchaseOrders as po on pr.refId=po.poId where pr.fk_escalateLevelId in (select roleId from dbo.roles where fk_deptId in (select autoindex from departments where departmentName in (" + whereClauseRoleLine + "))) and pr.fk_processId=" + ddlProcessType.SelectedValue.ToString() + " and pr.fk_statusId=" + ddlStatus.SelectedValue.ToString() + " ) as k where @cnt>0 " + loginConditionQuerySuffix + " order by k.fk_StatusId";
                    else
                        qry = "declare @cnt int ; select @cnt=count(*) from dbo.processEscalate E left join roles R on E.fk_roleId=R.roleId left join departments D on R.fk_deptId=D.autoindex where E.fk_processId in (" + Session["wfTypeId"].ToString() + ") and R.roleId=(select roleId from roles where fk_deptId=(select autoindex from departments where lower(departmentName)=lower('" + lblworkForRole.Text + "'))); select k.* from ( select @cnt as cnt,pr.*,ps.processStatusName,pdf.processAbbr,po.dbCode,po.bu,po.vendor,po.customerName,po.createdBy from dbo.processRequest as pr join dbo.processStatus as ps on pr.fk_StatusId=ps.processStatusId and pr.fk_processId=ps.fk_processId join dbo.process_def as pdf on pr.fk_processId=pdf.processId join dbo.PurchaseOrders as po on pr.refId=po.poId where pr.fk_escalateLevelId in (select roleId from dbo.roles where fk_deptId in (select autoindex from departments where departmentName in (" + whereClauseRoleLine + "))) and pr.fk_processId=" + ddlProcessType.SelectedValue.ToString() + " ) as k where @cnt>0 " + loginConditionQuerySuffix + " order by k.fk_StatusId";
                }
                else
                    qry = "declare @cnt int ; select @cnt=count(*) from dbo.processEscalate E left join roles R on E.fk_roleId=R.roleId left join departments D on R.fk_deptId=D.autoindex where E.fk_processId in (" + Session["wfTypeId"].ToString() + ") and R.roleId=(select roleId from roles where fk_deptId=(select autoindex from departments where lower(departmentName)=lower('" + lblworkForRole.Text + "'))); select k.* from ( select @cnt as cnt,pr.*,ps.processStatusName,pdf.processAbbr,po.dbCode,po.bu,po.vendor,po.customerName,po.createdBy from dbo.processRequest as pr join dbo.processStatus as ps on pr.fk_StatusId=ps.processStatusId and pr.fk_processId=ps.fk_processId join dbo.process_def as pdf on pr.fk_processId=pdf.processId join dbo.PurchaseOrders as po on pr.refId=po.poId where pr.fk_escalateLevelId in (select roleId from dbo.roles where fk_deptId in (select autoindex from departments where departmentName in (" + whereClauseRoleLine + "))) and pr.fk_processId=" + ddlProcessType.SelectedValue.ToString() + " ) as k where @cnt>0 " + loginConditionQuerySuffix + " order by k.fk_StatusId";
            }
            else    //MKT or others cases queries
            {
                if (ddlStatus.Items.Count > 0) //second case will never arise because the status list comes from distinct statuses of the process itself, it means there is no porocess 
                {
                    if (chkStatusAll.Checked == false)
                        qry = "declare @cnt int ; select @cnt=count(*) from dbo.processEscalate E left join roles R on E.fk_roleId=R.roleId left join departments D on R.fk_deptId=D.autoindex where E.fk_processId in (" + Session["wfTypeId"].ToString() + ") and R.roleId=(select roleId from roles where fk_deptId=(select autoindex from departments where lower(departmentName)=lower('" + lblworkForRole.Text + "'))); select k.* from ( select @cnt as cnt,pr.*,ps.processStatusName,pdf.processAbbr,vb.buName as bu,vb.buName as vendor from dbo.processRequest as pr join dbo.processStatus as ps on pr.fk_StatusId=ps.processStatusId and pr.fk_processId=ps.fk_processId join dbo.process_def as pdf on pr.fk_processId=pdf.processId join dbo.workFlowPlans as wf on pr.refId=wf.autoindex left join dbo.VendorBUDef as vb on wf.fk_VendorBU=vb.autoindex where pr.fk_escalateLevelId in (select roleId from dbo.roles where fk_deptId in (select autoindex from departments where departmentName in (" + whereClauseRoleLine + "))) and pr.fk_processId=" + ddlProcessType.SelectedValue.ToString() + " and pr.fk_statusId=" + ddlStatus.SelectedValue.ToString() + " ) as k where @cnt>0 " + loginConditionQuerySuffix + " order by k.fk_StatusId";
                    else
                        qry = "declare @cnt int ; select @cnt=count(*) from dbo.processEscalate E left join roles R on E.fk_roleId=R.roleId left join departments D on R.fk_deptId=D.autoindex where E.fk_processId in (" + Session["wfTypeId"].ToString() + ") and R.roleId=(select roleId from roles where fk_deptId=(select autoindex from departments where lower(departmentName)=lower('" + lblworkForRole.Text + "'))); select k.* from ( select @cnt as cnt,pr.*,ps.processStatusName,pdf.processAbbr,vb.buName as bu,vb.buName as vendor from dbo.processRequest as pr join dbo.processStatus as ps on pr.fk_StatusId=ps.processStatusId and pr.fk_processId=ps.fk_processId join dbo.process_def as pdf on pr.fk_processId=pdf.processId join dbo.workFlowPlans as wf on pr.refId=wf.autoindex left join dbo.VendorBUDef as vb on wf.fk_VendorBU=vb.autoindex where pr.fk_escalateLevelId in (select roleId from dbo.roles where fk_deptId in (select autoindex from departments where departmentName in (" + whereClauseRoleLine + "))) and pr.fk_processId=" + ddlProcessType.SelectedValue.ToString() + " ) as k where @cnt>0 " + loginConditionQuerySuffix + " order by k.fk_StatusId";
                }
                else
                    qry = "declare @cnt int ; select @cnt=count(*) from dbo.processEscalate E left join roles R on E.fk_roleId=R.roleId left join departments D on R.fk_deptId=D.autoindex where E.fk_processId in (" + Session["wfTypeId"].ToString() + ") and R.roleId=(select roleId from roles where fk_deptId=(select autoindex from departments where lower(departmentName)=lower('" + lblworkForRole.Text + "'))); select k.* from ( select @cnt as cnt,pr.*,ps.processStatusName,pdf.processAbbr,vb.buName as bu,vb.buName as vendor from dbo.processRequest as pr join dbo.processStatus as ps on pr.fk_StatusId=ps.processStatusId and pr.fk_processId=ps.fk_processId join dbo.process_def as pdf on pr.fk_processId=pdf.processId join dbo.workFlowPlans as wf on pr.refId=wf.autoindex left join dbo.VendorBUDef as vb on wf.fk_VendorBU=vb.autoindex where pr.fk_escalateLevelId in (select roleId from dbo.roles where fk_deptId in (select autoindex from departments where departmentName in (" + whereClauseRoleLine + "))) and pr.fk_processId=" + ddlProcessType.SelectedValue.ToString() + " ) as k where @cnt>0 " + loginConditionQuerySuffix + " order by k.fk_StatusId";
            }
        }

        Db.constr = myGlobal.getIntranetDBConnectionString();
        return Db.myGetDS(qry).Tables[0];
    }

    protected void gridTasks_SelectedIndexChanged(object sender, EventArgs e)
    {
        GridViewRow row = gridTasks.SelectedRow;
        Label templblType = (row.FindControl("lblprocessAbbr") as Label);
         Label templblpid = (row.FindControl("lblpid") as Label);
        if (templblType.Text == "RO")
            Response.Redirect("~/Intranet/orders/releaseOrderDetails.aspx?oId=" + gridTasks.SelectedDataKey.Value + "&pid=" + templblpid.Text + "&action=task&qrls=" + lblworkForRole.Text);
        if (templblType.Text == "PO-BTB" || templblType.Text == "PO-RR1" || templblType.Text == "PO-RR2")
            Response.Redirect("~/Intranet/orders/SapPurchaseOrderDetails.aspx?oId=" + gridTasks.SelectedDataKey.Value + "&pid=" + templblpid.Text + "&action=task&qrls=" + lblworkForRole.Text);
        if (templblType.Text == "MKT")
            Response.Redirect("~/Intranet/orders/planDetails.aspx?oId=" + gridTasks.SelectedDataKey.Value + "&pid=" + templblpid.Text + "&action=task&qrls=" + lblworkForRole.Text);
    }

    protected void LinkManagePlans_Click(object sender, EventArgs e)
    {
        Response.Redirect("~/Intranet/orders/ManagePlan.aspx?pid=" + ddlProcessType.SelectedValue.ToString() + "&qrls=" + lblworkForRole.Text);
    }
    protected void LinkMKTDashboard_Click(object sender, EventArgs e)
    {
        Response.Redirect("~/Intranet/orders/marketingDashboard.aspx?&qrls=" + lblworkForRole.Text);
    }

    protected void gridOrders_SelectedIndexChanged(object sender, EventArgs e)
    {
        GridViewRow row = gridOrders.SelectedRow;
        Label templblType = (row.FindControl("lblprocessAbbr") as Label);
        Label templblpid = (row.FindControl("lblpid") as Label);

        if (templblType.Text == "RO")
            Response.Redirect("~/Intranet/orders/releaseOrderDetails.aspx?oId=" + gridOrders.SelectedDataKey.Value + "&pid=" + templblpid.Text + "&action=view&qrls="+lblworkForRole.Text);
        if (templblType.Text == "PO-BTB" || templblType.Text == "PO-RR1" || templblType.Text == "PO-RR2")
            Response.Redirect("~/Intranet/orders/SapPurchaseOrderDetails.aspx?oId=" + gridOrders.SelectedDataKey.Value + "&pid=" + templblpid.Text + "&action=view&qrls=" + lblworkForRole.Text);
        if (templblType.Text == "MKT")
            Response.Redirect("~/Intranet/orders/planDetails.aspx?oId=" + gridOrders.SelectedDataKey.Value + "&pid=" + templblpid.Text + "&action=view&qrls=" + lblworkForRole.Text);
    }

    protected void gridOrders_RowDataBound(object sender, GridViewRowEventArgs e)
    {
        if (e.Row.RowType == DataControlRowType.DataRow)
        {

            e.Row.Attributes["onmouseover"] = "this.style.cursor='pointer';this.style.color='red';this.style.textDecoration='underline';";
            e.Row.Attributes["onmouseout"] = "this.style.color='black';this.style.textDecoration='none';";
            e.Row.Attributes["onclick"] = ClientScript.GetPostBackClientHyperlink(this.gridOrders, "select$" + e.Row.RowIndex);
        }
    }
    protected void gridTasks_RowDataBound(object sender, GridViewRowEventArgs e)
    {
        if (e.Row.RowType == DataControlRowType.DataRow)
        {
            e.Row.Attributes["onmouseover"] = "this.style.cursor='pointer';this.style.color='red';this.style.textDecoration='underline';";
            e.Row.Attributes["onmouseout"] = "this.style.color='black';this.style.textDecoration='none';";
            e.Row.Attributes["onclick"] = ClientScript.GetPostBackClientHyperlink(this.gridTasks, "select$" + e.Row.RowIndex);
        }
    }
    protected void gridOrders_PageIndexChanging(object sender, GridViewPageEventArgs e)
    {
        gridOrders.PageIndex = e.NewPageIndex;
        bindGridOrders();
        bindGridTasks();
    }
    protected void gridTasks_PageIndexChanging(object sender, GridViewPageEventArgs e)
    {
        gridTasks.PageIndex = e.NewPageIndex;
        bindGridTasks();
        bindGridTasks();
    }

    protected void lnkNewPO_Click(object sender, EventArgs e)
    {
        Response.Redirect("SapPurchaseOrder.aspx?ptype=New&poid=0&wfProcessId="+ddlProcessType.SelectedValue.ToString()+"&ordType="+lblprocessSubType.Text + "&qrls=" + lblworkForRole.Text);
    }
    protected void lnkNewRO_Click(object sender, EventArgs e)
    {
        Response.Redirect("ReleaseOrder.aspx?ptype=New&poid=0&qrls=" + lblworkForRole.Text);
    }
    protected void gridOrders_Sorting(object sender, GridViewSortEventArgs e)
    {
        if (ViewState["SortExpressionGridOrders"] != null)
        {
            if (e.SortExpression == ViewState["SortExpressionGridOrders"].ToString())
            {
                if (ViewState["SortDirectionGridOrders"].ToString() == "ASC")
                {
                    ViewState["SortDirectionGridOrders"] = "DESC";
                }
                else
                {
                    ViewState["SortDirectionGridOrders"] = "ASC";
                }
            }
            else
            {
                ViewState["SortDirectionGridOrders"] = "ASC";
            }
        }
        else
        {
            ViewState["SortDirectionGridOrders"] = "ASC";
        }

        ViewState["SortExpressionGridOrders"] = e.SortExpression;
        bindGridOrders();

    }
    protected void gridTasks_Sorting(object sender, GridViewSortEventArgs e)
    {
        if (ViewState["SortExpressionGridTasks"] != null)
        {
            if (e.SortExpression == ViewState["SortExpressionGridTasks"].ToString())
            {
                if (ViewState["SortDirectionGridTasks"].ToString() == "ASC")
                {
                    ViewState["SortDirectionGridTasks"] = "DESC";
                }
                else
                {
                    ViewState["SortDirectionGridTasks"] = "ASC";
                }
            }
            else
            {
                ViewState["SortDirectionGridTasks"] = "ASC";
            }
        }
        else
        {
            ViewState["SortDirectionGridTasks"] = "ASC";
        }

        ViewState["SortExpressionGridTasks"] = e.SortExpression;
        bindGridTasks();
    }
    protected void RadWorkingRoles_SelectedIndexChanged(object sender, EventArgs e)
    {
        bindGridOrders();
        bindGridTasks();
    }

   private void setRoleNUserlbls()
    {
            string[] rlNusr;
            rlNusr = RadWorkingRoles.SelectedValue.ToString().Split('-');

            lblworkForRole.Text = rlNusr[0];  //role is at first position
            lblworkForUser.Text = rlNusr[1];  //user is at second position
            lblWorkingAs.Text = lblworkForRole.Text + " ( " + lblworkForUser.Text + " )"; 

            whereClauseRoleLine = "'" + lblworkForRole.Text + "'";
    }
}




