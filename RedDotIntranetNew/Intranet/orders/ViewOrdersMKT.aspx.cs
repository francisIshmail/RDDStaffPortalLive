using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Data;
using System.Data.SqlClient;
using System.Configuration;

public partial class Intranet_orders_ViewOrdersMKT : System.Web.UI.Page
{
    string qry;
    //string[] userRole;
    string whereClauseRoleLine, loginConditionField, loginConditionQuerySuffix;
    
    protected void Page_Load(object sender, EventArgs e)
    {
        //Page.ClientScript.RegisterStartupScript(this.GetType(), "refresh", "window.setTimeout('var url = window.location.href;window.location.href = url',60000);", true); //refreshes in 60 secs
        //MaintainScrollPositionOnPostBack = true;

        if (Request.QueryString["wfTypeId"] != null)
            Session["wfTypeId"] = Request.QueryString["wfTypeId"].ToString();

        if (!IsPostBack)
        {
            for (int y = 0; y < 5; y++)
            {
                int x = DateTime.Now.Year;
                ddlBaseYear.Items.Add((DateTime.Now.Year - y).ToString());
            }

            //RadWorkingRoles.Items.Clear();
            //RadWorkingRoles.DataSource = myGlobal.loggedInRoleList(); 
            //RadWorkingRoles.DataBind();
            loadRolesForCurrentLoggedInUser();

            RadWorkingRoles.SelectedIndex = 0;
            whereClauseRoleLine = "'" + RadWorkingRoles.SelectedItem.Text + "'"; 

            bindDdlProcessType();
            bindDdlStatus();

            bindGridOrders();
            bindGridTasks();
        }

        whereClauseRoleLine = "'" + RadWorkingRoles.SelectedItem.Text + "'";
        lblWorkingAs.Text = RadWorkingRoles.SelectedItem.Text;
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

                if (RadWorkingRoles.SelectedItem.Text==rl.ToUpper())  
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

                if (RadWorkingRoles.SelectedItem.Text=="MARKETINGADMIN" && ddlProcessType.SelectedValue.ToString() == "10031")
                //if (RadWorkingRoles.SelectedItem.Text == "MARKETINGEXECUTIVE" && ddlProcessType.SelectedValue.ToString() == "10031")          
                    panelPlansAccess.Visible = true;
                else
                    panelPlansAccess.Visible = false;

    }

    private void loadRolesForCurrentLoggedInUser()
    {
        string[] avoidUserRolesFromListofRoles = myGlobal.getAppSettingsDataForKey("avoidUserRolesFromListofRolesMKT").Split(';');

        string[] rlsNames = myGlobal.loggedInRoleList();

        RadWorkingRoles.Items.Clear();
        foreach (string st in rlsNames)
        {
            // avoidUserRolesFromListofRoles[0] is always user name
            if (myGlobal.loggedInUser().ToUpper() == avoidUserRolesFromListofRoles[0].ToUpper() && avoidUserRolesFromListofRoles.Contains(st.ToUpper()))
            {
                //do nothing
            }
            else
                RadWorkingRoles.Items.Add(st);
        }

    }

    protected void bindDdlProcessType()
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
    
    protected void ddlBaseYear_SelectedIndexChanged(object sender, EventArgs e)
    {
        bindGridOrders();
        bindGridTasks();
    }

    protected void ddlProcessType_SelectedIndexChanged(object sender, EventArgs e)
    {
        lblTypeSelected.Text = ddlProcessType.SelectedItem.Text;
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
    
    private void setLoginConditionField()
    {
        loginConditionQuerySuffix = "";

        if (Convert.ToInt32(Session["wfTypeId"]) == 10011 || Convert.ToInt32(Session["wfTypeId"]) == 10012 || Convert.ToInt32(Session["wfTypeId"]) == 10013)
        {
            if (RadWorkingRoles.SelectedItem.Text == "PRODUCTMANAGER")
            {
                loginConditionField = "bu";  //PM    login case use this field
                loginConditionQuerySuffix = " and k." + loginConditionField + " in (select data from dbo.MySplit((select " + loginConditionField + " from dbo.orderSystemUserMapping where lower(membershipUserRoleName)=lower('" + RadWorkingRoles.SelectedItem.Text + "') and lower(membershipusername)=lower('" + myGlobal.loggedInUser() + "')),','))";
            }
            else if (RadWorkingRoles.SelectedItem.Text == "COUNTRYMANAGER" || RadWorkingRoles.SelectedItem.Text == "PRODUCTSPECIALIST")
            {
                loginConditionField = "dbCode";  //CM, PS    login case use this field
                loginConditionQuerySuffix = " and k." + loginConditionField + " in (select data from dbo.MySplit((select " + loginConditionField + " from dbo.orderSystemUserMapping where lower(membershipUserRoleName)=lower('" + RadWorkingRoles.SelectedItem.Text + "') and lower(membershipusername)=lower('" + myGlobal.loggedInUser() + "')),','))";
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
    }

    //protected void bindGridOrdersOrg()
    //{
    //    string sortExp = "", sortDir = "";
    //    if (ViewState["SortExpressionGridOrders"] != null)
    //        sortExp = ViewState["SortExpressionGridOrders"].ToString();
    //    if (ViewState["SortDirectionGridOrders"] != null)
    //        sortDir = ViewState["SortDirectionGridOrders"].ToString();


    //    if (chkProcessAll.Checked == true)
    //    {
    //        if ((Convert.ToInt32(Session["wfTypeId"])) == 10011)  //in claues changes accordingly 
    //            qry = "select pr.*,ps.processStatusName,pdf.processAbbr from dbo.processRequest as pr join dbo.processStatus as ps on pr.fk_StatusId=ps.processStatusId and pr.fk_processId=ps.fk_processId join dbo.process_def as pdf on pr.fk_processId=pdf.processId where pr.fk_processId in (10011,10012,10013) order by pr.fk_StatusId";
    //        else
    //            qry = "select pr.*,ps.processStatusName,pdf.processAbbr from dbo.processRequest as pr join dbo.processStatus as ps on pr.fk_StatusId=ps.processStatusId and pr.fk_processId=ps.fk_processId join dbo.process_def as pdf on pr.fk_processId=pdf.processId where pr.fk_processId in (" + Session["wfTypeId"].ToString() + ") order by pr.fk_StatusId";
    //    }
    //    else
    //    {
    //        if (ddlStatus.Items.Count > 0) //second case will never arise because the status list comes from distinct statuses of the process itself, it means there is no porocess 
    //        {
    //            if (chkStatusAll.Checked == false)
    //                qry = "select pr.*,ps.processStatusName,pdf.processAbbr from dbo.processRequest as pr join dbo.processStatus as ps on pr.fk_StatusId=ps.processStatusId and pr.fk_processId=ps.fk_processId join dbo.process_def as pdf on pr.fk_processId=pdf.processId where pr.fk_processId=" + ddlProcessType.SelectedValue.ToString() + " and pr.fk_statusId=" + ddlStatus.SelectedValue.ToString() + " order by pr.fk_StatusId";
    //            else
    //                qry = "select pr.*,ps.processStatusName,pdf.processAbbr from dbo.processRequest as pr join dbo.processStatus as ps on pr.fk_StatusId=ps.processStatusId and pr.fk_processId=ps.fk_processId join dbo.process_def as pdf on pr.fk_processId=pdf.processId where pr.fk_processId=" + ddlProcessType.SelectedValue.ToString() + " order by pr.fk_StatusId";
    //        }
    //        else
    //            qry = "select pr.*,ps.processStatusName,pdf.processAbbr from dbo.processRequest as pr join dbo.processStatus as ps on pr.fk_StatusId=ps.processStatusId and pr.fk_processId=ps.fk_processId join dbo.process_def as pdf on pr.fk_processId=pdf.processId where pr.fk_processId=" + ddlProcessType.SelectedValue.ToString() + " order by pr.fk_StatusId";
    //    }
    //    Db.constr = myGlobal.getIntranetDBConnectionString();
    //    DataTable dt = new DataTable();
    //    dt = Db.myGetDS(qry).Tables[0];
    //    if (sortExp != string.Empty || sortDir != string.Empty)
    //    {
    //        dt.DefaultView.Sort = sortExp + " " + sortDir;
    //    }
    //    gridOrders.DataSource = dt;
    //    gridOrders.DataBind();

    //    lblRecords.Text = dt.Rows.Count.ToString();
    //}
   
    //protected void bindGridTasksOrg()
    //{
    //    string sortExp = "", sortDir = "";
    //    if (ViewState["SortExpressionGridTasks"] != null)
    //        sortExp = ViewState["SortExpressionGridTasks"].ToString();
    //    if (ViewState["SortDirectionGridTasks"] != null)
    //        sortDir = ViewState["SortDirectionGridTasks"].ToString();

    //    if (chkProcessAll.Checked == true)
    //    {
    //        if ((Convert.ToInt32(Session["wfTypeId"])) == 10011)  //in claues changes accordingly 
    //            qry = "select pr.*,ps.processStatusName,pdf.processAbbr from dbo.processRequest as pr join dbo.processStatus as ps on pr.fk_StatusId=ps.processStatusId and pr.fk_processId=ps.fk_processId join dbo.process_def as pdf on pr.fk_processId=pdf.processId where pr.fk_escalateLevelId in (select roleId from dbo.roles where fk_deptId in (select autoindex from departments where departmentName in (" + whereClauseRoleLine + "))) and pr.fk_processId in (10011,10012,10013) order by pr.fk_StatusId";
    //        else
    //            qry = "select pr.*,ps.processStatusName,pdf.processAbbr from dbo.processRequest as pr join dbo.processStatus as ps on pr.fk_StatusId=ps.processStatusId and pr.fk_processId=ps.fk_processId join dbo.process_def as pdf on pr.fk_processId=pdf.processId where pr.fk_escalateLevelId in (select roleId from dbo.roles where fk_deptId in (select autoindex from departments where departmentName in (" + whereClauseRoleLine + "))) and pr.fk_processId in (" + Session["wfTypeId"].ToString() + ") order by pr.fk_StatusId";
    //    }
    //    else
    //    {
    //        if (ddlStatus.Items.Count > 0) //second case will never arise because the status list comes from distinct statuses of the process itself, it means there is no porocess 
    //        {
    //            if (chkStatusAll.Checked == false)
    //                qry = "select pr.*,ps.processStatusName,pdf.processAbbr from dbo.processRequest as pr join dbo.processStatus as ps on pr.fk_StatusId=ps.processStatusId and pr.fk_processId=ps.fk_processId join dbo.process_def as pdf on pr.fk_processId=pdf.processId where pr.fk_escalateLevelId in (select roleId from dbo.roles where fk_deptId in (select autoindex from departments where departmentName in (" + whereClauseRoleLine + "))) and pr.fk_processId=" + ddlProcessType.SelectedValue.ToString() + " and pr.fk_statusId=" + ddlStatus.SelectedValue.ToString() + " order by pr.fk_StatusId";
    //            else
    //                qry = "select pr.*,ps.processStatusName,pdf.processAbbr from dbo.processRequest as pr join dbo.processStatus as ps on pr.fk_StatusId=ps.processStatusId and pr.fk_processId=ps.fk_processId join dbo.process_def as pdf on pr.fk_processId=pdf.processId where pr.fk_escalateLevelId in (select roleId from dbo.roles where fk_deptId in (select autoindex from departments where departmentName in (" + whereClauseRoleLine + "))) and pr.fk_processId=" + ddlProcessType.SelectedValue.ToString() + " order by pr.fk_StatusId";
    //        }
    //        else
    //            qry = "select pr.*,ps.processStatusName,pdf.processAbbr from dbo.processRequest as pr join dbo.processStatus as ps on pr.fk_StatusId=ps.processStatusId and pr.fk_processId=ps.fk_processId join dbo.process_def as pdf on pr.fk_processId=pdf.processId where pr.fk_escalateLevelId in (select roleId from dbo.roles where fk_deptId in (select autoindex from departments where departmentName in (" + whereClauseRoleLine + "))) and pr.fk_processId=" + ddlProcessType.SelectedValue.ToString() + " order by pr.fk_StatusId";
    //    }

    //    Db.constr = myGlobal.getIntranetDBConnectionString();
    //    DataTable dt = new DataTable();
    //    dt = Db.myGetDS(qry).Tables[0];
    //    if (sortExp != string.Empty || sortDir != string.Empty)
    //    {
    //        dt.DefaultView.Sort = sortExp + " " + sortDir;
    //    }
    //    gridTasks.DataSource = dt;
    //    gridTasks.DataBind();

    //    lblRecords1.Text = dt.Rows.Count.ToString();
    //}

    protected void bindGridOrders()
    {
        setLoginConditionField();

        string sortExp = "", sortDir = "";
        if (ViewState["SortExpressionGridOrders"] != null)
            sortExp = ViewState["SortExpressionGridOrders"].ToString();
        if (ViewState["SortDirectionGridOrders"] != null)
            sortDir = ViewState["SortDirectionGridOrders"].ToString();

        if (chkProcessAll.Checked == true)
        {
            if ((Convert.ToInt32(Session["wfTypeId"])) == 10011 || (Convert.ToInt32(Session["wfTypeId"])) == 10012 || (Convert.ToInt32(Session["wfTypeId"])) == 10013)  //in claues changes accordingly PO case
                qry = "declare @cnt int ; select @cnt=count(*) from dbo.processEscalate E left join roles R on E.fk_roleId=R.roleId left join departments D on R.fk_deptId=D.autoindex where E.fk_processId in (10011,10012,10013) and R.roleId=(select roleId from roles where fk_deptId=(select autoindex from departments where lower(departmentName)=lower('" + RadWorkingRoles.SelectedItem.Text + "'))); select k.* from ( select @cnt as cnt,pr.*,ps.processStatusName,pdf.processAbbr,po.dbCode,po.bu,po.vendor,po.customerName from dbo.processRequest as pr join dbo.processStatus as ps on pr.fk_StatusId=ps.processStatusId and pr.fk_processId=ps.fk_processId join dbo.process_def as pdf on pr.fk_processId=pdf.processId join dbo.PurchaseOrders as po on pr.refId=po.poId where pr.fk_processId in (10011,10012,10013) ) as k where @cnt>0 " + loginConditionQuerySuffix + " order by k.fk_StatusId";
            else  //MKT or others cases  queries
                qry = "declare @cnt int ; select @cnt=count(*) from dbo.processEscalate E left join roles R on E.fk_roleId=R.roleId left join departments D on R.fk_deptId=D.autoindex where E.fk_processId in (" + Session["wfTypeId"].ToString() + ") and R.roleId=(select roleId from roles where fk_deptId=(select autoindex from departments where lower(departmentName)=lower('" + RadWorkingRoles.SelectedItem.Text + "'))); select k.* from ( select @cnt as cnt, pr.refValue + ' (' + convert(varchar(10),pr.refId) + ')' refValueNew,pr.*,ps.processStatusName,pdf.processAbbr,vb.buName as bu,vb.buName as vendor from dbo.processRequest as pr join dbo.processStatus as ps on pr.fk_StatusId=ps.processStatusId and pr.fk_processId=ps.fk_processId join dbo.process_def as pdf on pr.fk_processId=pdf.processId join dbo.workFlowPlans as wf on pr.refId=wf.autoindex left join dbo.VendorBUDef as vb on wf.fk_VendorBU=vb.autoindex where wf.planYear=" + ddlBaseYear.SelectedItem.Text + " and pr.fk_processId in (" + Session["wfTypeId"].ToString() + ") ) as k where @cnt>0 " + loginConditionQuerySuffix + " order by k.fk_StatusId";
        }
        else
        {
            if ((Convert.ToInt32(Session["wfTypeId"])) == 10011 || (Convert.ToInt32(Session["wfTypeId"])) == 10012 || (Convert.ToInt32(Session["wfTypeId"])) == 10013)  //in claues changes accordingly PO case
            {
                if (ddlStatus.Items.Count > 0) //second case will never arise because the status list comes from distinct statuses of the process itself, it means there is no porocess 
                {
                    if (chkStatusAll.Checked == false)
                        qry = "declare @cnt int ; select @cnt=count(*) from dbo.processEscalate E left join roles R on E.fk_roleId=R.roleId left join departments D on R.fk_deptId=D.autoindex where E.fk_processId in (" + Session["wfTypeId"].ToString() + ") and R.roleId=(select roleId from roles where fk_deptId=(select autoindex from departments where lower(departmentName)=lower('" + RadWorkingRoles.SelectedItem.Text + "'))); select k.* from ( select @cnt as cnt,pr.*,ps.processStatusName,pdf.processAbbr,po.dbCode,po.bu,po.vendor,po.customerName from dbo.processRequest as pr join dbo.processStatus as ps on pr.fk_StatusId=ps.processStatusId and pr.fk_processId=ps.fk_processId join dbo.process_def as pdf on pr.fk_processId=pdf.processId join dbo.PurchaseOrders as po on pr.refId=po.poId where pr.fk_processId=" + ddlProcessType.SelectedValue.ToString() + " and pr.fk_statusId=" + ddlStatus.SelectedValue.ToString() + "  ) as k where @cnt>0 " + loginConditionQuerySuffix + " order by k.fk_StatusId";
                    else
                        qry = "declare @cnt int ; select @cnt=count(*) from dbo.processEscalate E left join roles R on E.fk_roleId=R.roleId left join departments D on R.fk_deptId=D.autoindex where E.fk_processId in (" + Session["wfTypeId"].ToString() + ") and R.roleId=(select roleId from roles where fk_deptId=(select autoindex from departments where lower(departmentName)=lower('" + RadWorkingRoles.SelectedItem.Text + "'))); select k.* from ( select @cnt as cnt,pr.*,ps.processStatusName,pdf.processAbbr,po.dbCode,po.bu,po.vendor,po.customerName from dbo.processRequest as pr join dbo.processStatus as ps on pr.fk_StatusId=ps.processStatusId and pr.fk_processId=ps.fk_processId join dbo.process_def as pdf on pr.fk_processId=pdf.processId join dbo.PurchaseOrders as po on pr.refId=po.poId where pr.fk_processId=" + ddlProcessType.SelectedValue.ToString() + "  ) as k where @cnt>0 " + loginConditionQuerySuffix + " order by k.fk_StatusId";
                }
                else
                    qry = "declare @cnt int ; select @cnt=count(*) from dbo.processEscalate E left join roles R on E.fk_roleId=R.roleId left join departments D on R.fk_deptId=D.autoindex where E.fk_processId in (" + Session["wfTypeId"].ToString() + ") and R.roleId=(select roleId from roles where fk_deptId=(select autoindex from departments where lower(departmentName)=lower('" + RadWorkingRoles.SelectedItem.Text + "'))); select k.* from ( select @cnt as cnt,pr.*,ps.processStatusName,pdf.processAbbr,po.dbCode,po.bu,po.vendor,po.customerName from dbo.processRequest as pr join dbo.processStatus as ps on pr.fk_StatusId=ps.processStatusId and pr.fk_processId=ps.fk_processId join dbo.process_def as pdf on pr.fk_processId=pdf.processId join dbo.PurchaseOrders as po on pr.refId=po.poId where pr.fk_processId=" + ddlProcessType.SelectedValue.ToString() + "  ) as k where @cnt>0 " + loginConditionQuerySuffix + " order by k.fk_StatusId";
            }
            else    //MKT or others cases queries
            {
                if (ddlStatus.Items.Count > 0) //second case will never arise because the status list comes from distinct statuses of the process itself, it means there is no porocess 
                {
                    if (chkStatusAll.Checked == false)
                        qry = "declare @cnt int ; select @cnt=count(*) from dbo.processEscalate E left join roles R on E.fk_roleId=R.roleId left join departments D on R.fk_deptId=D.autoindex where E.fk_processId in (" + Session["wfTypeId"].ToString() + ") and R.roleId=(select roleId from roles where fk_deptId=(select autoindex from departments where lower(departmentName)=lower('" + RadWorkingRoles.SelectedItem.Text + "'))); select k.* from ( select @cnt as cnt, pr.refValue + ' (' + convert(varchar(10),pr.refId) + ')' refValueNew,pr.*,ps.processStatusName,pdf.processAbbr,vb.buName as bu,vb.buName as vendor from dbo.processRequest as pr join dbo.processStatus as ps on pr.fk_StatusId=ps.processStatusId and pr.fk_processId=ps.fk_processId join dbo.process_def as pdf on pr.fk_processId=pdf.processId join dbo.workFlowPlans as wf on pr.refId=wf.autoindex left join dbo.VendorBUDef as vb on wf.fk_VendorBU=vb.autoindex where wf.planYear=" + ddlBaseYear.SelectedItem.Text + " and pr.fk_processId=" + ddlProcessType.SelectedValue.ToString() + " and pr.fk_statusId=" + ddlStatus.SelectedValue.ToString() + "  ) as k where @cnt>0 " + loginConditionQuerySuffix + " order by k.fk_StatusId";
                    else
                        qry = "declare @cnt int ; select @cnt=count(*) from dbo.processEscalate E left join roles R on E.fk_roleId=R.roleId left join departments D on R.fk_deptId=D.autoindex where E.fk_processId in (" + Session["wfTypeId"].ToString() + ") and R.roleId=(select roleId from roles where fk_deptId=(select autoindex from departments where lower(departmentName)=lower('" + RadWorkingRoles.SelectedItem.Text + "'))); select k.* from ( select @cnt as cnt, pr.refValue + ' (' + convert(varchar(10),pr.refId) + ')' refValueNew,pr.*,ps.processStatusName,pdf.processAbbr,vb.buName as bu,vb.buName as vendor from dbo.processRequest as pr join dbo.processStatus as ps on pr.fk_StatusId=ps.processStatusId and pr.fk_processId=ps.fk_processId join dbo.process_def as pdf on pr.fk_processId=pdf.processId join dbo.workFlowPlans as wf on pr.refId=wf.autoindex left join dbo.VendorBUDef as vb on wf.fk_VendorBU=vb.autoindex where wf.planYear=" + ddlBaseYear.SelectedItem.Text + " and pr.fk_processId=" + ddlProcessType.SelectedValue.ToString() + "  ) as k where @cnt>0 " + loginConditionQuerySuffix + " order by k.fk_StatusId";
                }
                else
                    qry = "declare @cnt int ; select @cnt=count(*) from dbo.processEscalate E left join roles R on E.fk_roleId=R.roleId left join departments D on R.fk_deptId=D.autoindex where E.fk_processId in (" + Session["wfTypeId"].ToString() + ") and R.roleId=(select roleId from roles where fk_deptId=(select autoindex from departments where lower(departmentName)=lower('" + RadWorkingRoles.SelectedItem.Text + "'))); select k.* from ( select @cnt as cnt, pr.refValue + ' (' + convert(varchar(10),pr.refId) + ')' refValueNew,pr.*,ps.processStatusName,pdf.processAbbr,vb.buName as bu,vb.buName as vendor from dbo.processRequest as pr join dbo.processStatus as ps on pr.fk_StatusId=ps.processStatusId and pr.fk_processId=ps.fk_processId join dbo.process_def as pdf on pr.fk_processId=pdf.processId join dbo.workFlowPlans as wf on pr.refId=wf.autoindex left join dbo.VendorBUDef as vb on wf.fk_VendorBU=vb.autoindex where wf.planYear=" + ddlBaseYear.SelectedItem.Text + " and pr.fk_processId=" + ddlProcessType.SelectedValue.ToString() + "  ) as k where @cnt>0 " + loginConditionQuerySuffix + " order by k.fk_StatusId";
            }
        }
        Db.constr = myGlobal.getIntranetDBConnectionString();
        DataTable dt = new DataTable();
        dt = Db.myGetDS(qry).Tables[0];
        if (sortExp != string.Empty || sortDir != string.Empty)
        {
            dt.DefaultView.Sort = sortExp + " " + sortDir;
        }
        gridOrders.DataSource = dt;
        gridOrders.DataBind();

        lblRecords.Text = dt.Rows.Count.ToString();
    }

    protected void bindGridTasks()
    {
        setLoginConditionField();

        string sortExp = "", sortDir = "";
        if (ViewState["SortExpressionGridTasks"] != null)
            sortExp = ViewState["SortExpressionGridTasks"].ToString();
        if (ViewState["SortDirectionGridTasks"] != null)
            sortDir = ViewState["SortDirectionGridTasks"].ToString();

        if (chkProcessAll.Checked == true)
        {
            if ((Convert.ToInt32(Session["wfTypeId"])) == 10011 || (Convert.ToInt32(Session["wfTypeId"])) == 10012 || (Convert.ToInt32(Session["wfTypeId"])) == 10013)  //in claues changes accordingly PO case
                qry = "declare @cnt int ; select @cnt=count(*) from dbo.processEscalate E left join roles R on E.fk_roleId=R.roleId left join departments D on R.fk_deptId=D.autoindex where E.fk_processId in (10011,10012,10013) and R.roleId=(select roleId from roles where fk_deptId=(select autoindex from departments where lower(departmentName)=lower('" + RadWorkingRoles.SelectedItem.Text + "'))); select k.* from ( select @cnt as cnt,pr.*,ps.processStatusName,pdf.processAbbr,po.dbCode,po.bu,po.vendor,po.customerName from dbo.processRequest as pr join dbo.processStatus as ps on pr.fk_StatusId=ps.processStatusId and pr.fk_processId=ps.fk_processId join dbo.process_def as pdf on pr.fk_processId=pdf.processId join dbo.PurchaseOrders as po on pr.refId=po.poId where pr.fk_escalateLevelId in (select roleId from dbo.roles where fk_deptId in (select autoindex from departments where departmentName in (" + whereClauseRoleLine + "))) and pr.fk_processId in (10011,10012,10013) ) as k where @cnt>0 " + loginConditionQuerySuffix + " order by k.fk_StatusId";
            else  //MKT or others cases  queries
                qry = "declare @cnt int ; select @cnt=count(*) from dbo.processEscalate E left join roles R on E.fk_roleId=R.roleId left join departments D on R.fk_deptId=D.autoindex where E.fk_processId in (" + Session["wfTypeId"].ToString() + ") and R.roleId=(select roleId from roles where fk_deptId=(select autoindex from departments where lower(departmentName)=lower('" + RadWorkingRoles.SelectedItem.Text + "'))); select k.* from ( select @cnt as cnt, pr.refValue + ' (' + convert(varchar(10),pr.refId) + ')' refValueNew,pr.*,ps.processStatusName,pdf.processAbbr,vb.buName as bu,vb.buName as vendor from dbo.processRequest as pr join dbo.processStatus as ps on pr.fk_StatusId=ps.processStatusId and pr.fk_processId=ps.fk_processId join dbo.process_def as pdf on pr.fk_processId=pdf.processId join dbo.workFlowPlans as wf on pr.refId=wf.autoindex left join dbo.VendorBUDef as vb on wf.fk_VendorBU=vb.autoindex where wf.planYear=" + ddlBaseYear.SelectedItem.Text + " and pr.fk_escalateLevelId in (select roleId from dbo.roles where fk_deptId in (select autoindex from departments where departmentName in (" + whereClauseRoleLine + "))) and pr.fk_processId in (" + Session["wfTypeId"].ToString() + ") ) as k where @cnt>0 " + loginConditionQuerySuffix + " order by k.fk_StatusId";
        }
        else
        {
            if ((Convert.ToInt32(Session["wfTypeId"])) == 10011 || (Convert.ToInt32(Session["wfTypeId"])) == 10012 || (Convert.ToInt32(Session["wfTypeId"])) == 10013)  //in claues changes accordingly PO case
            {
                if (ddlStatus.Items.Count > 0) //second case will never arise because the status list comes from distinct statuses of the process itself, it means there is no porocess 
                {
                    if (chkStatusAll.Checked == false)
                        qry = "declare @cnt int ; select @cnt=count(*) from dbo.processEscalate E left join roles R on E.fk_roleId=R.roleId left join departments D on R.fk_deptId=D.autoindex where E.fk_processId in (" + Session["wfTypeId"].ToString() + ") and R.roleId=(select roleId from roles where fk_deptId=(select autoindex from departments where lower(departmentName)=lower('" + RadWorkingRoles.SelectedItem.Text + "'))); select k.* from ( select @cnt as cnt,pr.*,ps.processStatusName,pdf.processAbbr,po.dbCode,po.bu,po.vendor,po.customerName from dbo.processRequest as pr join dbo.processStatus as ps on pr.fk_StatusId=ps.processStatusId and pr.fk_processId=ps.fk_processId join dbo.process_def as pdf on pr.fk_processId=pdf.processId join dbo.PurchaseOrders as po on pr.refId=po.poId where pr.fk_escalateLevelId in (select roleId from dbo.roles where fk_deptId in (select autoindex from departments where departmentName in (" + whereClauseRoleLine + "))) and pr.fk_processId=" + ddlProcessType.SelectedValue.ToString() + " and pr.fk_statusId=" + ddlStatus.SelectedValue.ToString() + " ) as k where @cnt>0 " + loginConditionQuerySuffix + " order by k.fk_StatusId";
                    else
                        qry = "declare @cnt int ; select @cnt=count(*) from dbo.processEscalate E left join roles R on E.fk_roleId=R.roleId left join departments D on R.fk_deptId=D.autoindex where E.fk_processId in (" + Session["wfTypeId"].ToString() + ") and R.roleId=(select roleId from roles where fk_deptId=(select autoindex from departments where lower(departmentName)=lower('" + RadWorkingRoles.SelectedItem.Text + "'))); select k.* from ( select @cnt as cnt,pr.*,ps.processStatusName,pdf.processAbbr,po.dbCode,po.bu,po.vendor,po.customerName from dbo.processRequest as pr join dbo.processStatus as ps on pr.fk_StatusId=ps.processStatusId and pr.fk_processId=ps.fk_processId join dbo.process_def as pdf on pr.fk_processId=pdf.processId join dbo.PurchaseOrders as po on pr.refId=po.poId where pr.fk_escalateLevelId in (select roleId from dbo.roles where fk_deptId in (select autoindex from departments where departmentName in (" + whereClauseRoleLine + "))) and pr.fk_processId=" + ddlProcessType.SelectedValue.ToString() + " ) as k where @cnt>0 " + loginConditionQuerySuffix + " order by k.fk_StatusId";
                }
                else
                    qry = "declare @cnt int ; select @cnt=count(*) from dbo.processEscalate E left join roles R on E.fk_roleId=R.roleId left join departments D on R.fk_deptId=D.autoindex where E.fk_processId in (" + Session["wfTypeId"].ToString() + ") and R.roleId=(select roleId from roles where fk_deptId=(select autoindex from departments where lower(departmentName)=lower('" + RadWorkingRoles.SelectedItem.Text + "'))); select k.* from ( select @cnt as cnt,pr.*,ps.processStatusName,pdf.processAbbr,po.dbCode,po.bu,po.vendor,po.customerName from dbo.processRequest as pr join dbo.processStatus as ps on pr.fk_StatusId=ps.processStatusId and pr.fk_processId=ps.fk_processId join dbo.process_def as pdf on pr.fk_processId=pdf.processId join dbo.PurchaseOrders as po on pr.refId=po.poId where pr.fk_escalateLevelId in (select roleId from dbo.roles where fk_deptId in (select autoindex from departments where departmentName in (" + whereClauseRoleLine + "))) and pr.fk_processId=" + ddlProcessType.SelectedValue.ToString() + " ) as k where @cnt>0 " + loginConditionQuerySuffix + " order by k.fk_StatusId";
            }
            else    //MKT or others cases queries
            {
                if (ddlStatus.Items.Count > 0) //second case will never arise because the status list comes from distinct statuses of the process itself, it means there is no porocess 
                {
                    if (chkStatusAll.Checked == false)
                        qry = "declare @cnt int ; select @cnt=count(*) from dbo.processEscalate E left join roles R on E.fk_roleId=R.roleId left join departments D on R.fk_deptId=D.autoindex where E.fk_processId in (" + Session["wfTypeId"].ToString() + ") and R.roleId=(select roleId from roles where fk_deptId=(select autoindex from departments where lower(departmentName)=lower('" + RadWorkingRoles.SelectedItem.Text + "'))); select k.* from ( select @cnt as cnt, pr.refValue + ' (' + convert(varchar(10),pr.refId) + ')' refValueNew,pr.*,ps.processStatusName,pdf.processAbbr,vb.buName as bu,vb.buName as vendor from dbo.processRequest as pr join dbo.processStatus as ps on pr.fk_StatusId=ps.processStatusId and pr.fk_processId=ps.fk_processId join dbo.process_def as pdf on pr.fk_processId=pdf.processId join dbo.workFlowPlans as wf on pr.refId=wf.autoindex left join dbo.VendorBUDef as vb on wf.fk_VendorBU=vb.autoindex where wf.planYear=" + ddlBaseYear.SelectedItem.Text + " and pr.fk_escalateLevelId in (select roleId from dbo.roles where fk_deptId in (select autoindex from departments where departmentName in (" + whereClauseRoleLine + "))) and pr.fk_processId=" + ddlProcessType.SelectedValue.ToString() + " and pr.fk_statusId=" + ddlStatus.SelectedValue.ToString() + " ) as k where @cnt>0 " + loginConditionQuerySuffix + " order by k.fk_StatusId";
                    else
                        qry = "declare @cnt int ; select @cnt=count(*) from dbo.processEscalate E left join roles R on E.fk_roleId=R.roleId left join departments D on R.fk_deptId=D.autoindex where E.fk_processId in (" + Session["wfTypeId"].ToString() + ") and R.roleId=(select roleId from roles where fk_deptId=(select autoindex from departments where lower(departmentName)=lower('" + RadWorkingRoles.SelectedItem.Text + "'))); select k.* from ( select @cnt as cnt, pr.refValue + ' (' + convert(varchar(10),pr.refId) + ')' refValueNew,pr.*,ps.processStatusName,pdf.processAbbr,vb.buName as bu,vb.buName as vendor from dbo.processRequest as pr join dbo.processStatus as ps on pr.fk_StatusId=ps.processStatusId and pr.fk_processId=ps.fk_processId join dbo.process_def as pdf on pr.fk_processId=pdf.processId join dbo.workFlowPlans as wf on pr.refId=wf.autoindex left join dbo.VendorBUDef as vb on wf.fk_VendorBU=vb.autoindex where wf.planYear=" + ddlBaseYear.SelectedItem.Text + " and pr.fk_escalateLevelId in (select roleId from dbo.roles where fk_deptId in (select autoindex from departments where departmentName in (" + whereClauseRoleLine + "))) and pr.fk_processId=" + ddlProcessType.SelectedValue.ToString() + " ) as k where @cnt>0 " + loginConditionQuerySuffix + " order by k.fk_StatusId";
                }
                else
                    qry = "declare @cnt int ; select @cnt=count(*) from dbo.processEscalate E left join roles R on E.fk_roleId=R.roleId left join departments D on R.fk_deptId=D.autoindex where E.fk_processId in (" + Session["wfTypeId"].ToString() + ") and R.roleId=(select roleId from roles where fk_deptId=(select autoindex from departments where lower(departmentName)=lower('" + RadWorkingRoles.SelectedItem.Text + "'))); select k.* from ( select @cnt as cnt, pr.refValue + ' (' + convert(varchar(10),pr.refId) + ')' refValueNew,pr.*,ps.processStatusName,pdf.processAbbr,vb.buName as bu,vb.buName as vendor from dbo.processRequest as pr join dbo.processStatus as ps on pr.fk_StatusId=ps.processStatusId and pr.fk_processId=ps.fk_processId join dbo.process_def as pdf on pr.fk_processId=pdf.processId join dbo.workFlowPlans as wf on pr.refId=wf.autoindex left join dbo.VendorBUDef as vb on wf.fk_VendorBU=vb.autoindex where wf.planYear=" + ddlBaseYear.SelectedItem.Text + " and pr.fk_escalateLevelId in (select roleId from dbo.roles where fk_deptId in (select autoindex from departments where departmentName in (" + whereClauseRoleLine + "))) and pr.fk_processId=" + ddlProcessType.SelectedValue.ToString() + " ) as k where @cnt>0 " + loginConditionQuerySuffix + " order by k.fk_StatusId";
            }
        }

        Db.constr = myGlobal.getIntranetDBConnectionString();
        DataTable dt = new DataTable();
        dt = Db.myGetDS(qry).Tables[0];
        if (sortExp != string.Empty || sortDir != string.Empty)
        {
            dt.DefaultView.Sort = sortExp + " " + sortDir;
        }
        gridTasks.DataSource = dt;
        gridTasks.DataBind();

        lblRecords1.Text = dt.Rows.Count.ToString();
    }

    protected void gridTasks_SelectedIndexChanged(object sender, EventArgs e)
    {
        GridViewRow row = gridTasks.SelectedRow;
        Label templblType = (row.FindControl("lblprocessAbbr") as Label);
         Label templblpid = (row.FindControl("lblpid") as Label);
        if (templblType.Text == "RO")
            Response.Redirect("~/Intranet/orders/releaseOrderDetails.aspx?oId=" + gridTasks.SelectedDataKey.Value + "&pid=" + templblpid.Text + "&action=task&qrls=" + RadWorkingRoles.SelectedItem.Text);
        if (templblType.Text == "PO-BTB" || templblType.Text == "PO-RR1" || templblType.Text == "PO-RR2")
            Response.Redirect("~/Intranet/orders/purchaseOrderDetails.aspx?oId=" + gridTasks.SelectedDataKey.Value + "&pid=" + templblpid.Text + "&action=task&qrls=" + RadWorkingRoles.SelectedItem.Text);
        if (templblType.Text == "MKT")
            Response.Redirect("~/Intranet/orders/planDetails.aspx?oId=" + gridTasks.SelectedDataKey.Value + "&pid=" + templblpid.Text + "&action=task&qrls=" + RadWorkingRoles.SelectedItem.Text);
    }

    protected void LinkManagePlans_Click(object sender, EventArgs e)
    {
        Response.Redirect("~/Intranet/orders/ManagePlan.aspx?pid=" + ddlProcessType.SelectedValue.ToString() + "&qrls=" + RadWorkingRoles.SelectedItem.Text);
    }
    protected void LinkMKTDashboard_Click(object sender, EventArgs e)
    {
        Response.Redirect("~/Intranet/orders/marketingDashboard.aspx?&qrls=" + RadWorkingRoles.SelectedItem.Text);
    }
    
    protected void gridOrders_SelectedIndexChanged(object sender, EventArgs e)
    {
        GridViewRow row = gridOrders.SelectedRow;
        Label templblType = (row.FindControl("lblprocessAbbr") as Label);
        Label templblpid = (row.FindControl("lblpid") as Label);

        if (templblType.Text == "RO")
            Response.Redirect("~/Intranet/orders/releaseOrderDetails.aspx?oId=" + gridOrders.SelectedDataKey.Value + "&pid=" + templblpid.Text + "&action=view&qrls="+RadWorkingRoles.SelectedItem.Text);
        if (templblType.Text == "PO-BTB" || templblType.Text == "PO-RR1" || templblType.Text == "PO-RR2")
            Response.Redirect("~/Intranet/orders/purchaseOrderDetails.aspx?oId=" + gridOrders.SelectedDataKey.Value + "&pid=" + templblpid.Text + "&action=view&qrls=" + RadWorkingRoles.SelectedItem.Text);
        if (templblType.Text == "MKT")
            Response.Redirect("~/Intranet/orders/planDetails.aspx?oId=" + gridOrders.SelectedDataKey.Value + "&pid=" + templblpid.Text + "&action=view&qrls=" + RadWorkingRoles.SelectedItem.Text);
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
        Response.Redirect("PurchaseOrder.aspx?ptype=New&poid=0&wfProcessId="+ddlProcessType.SelectedValue.ToString()+"&ordType="+lblprocessSubType.Text + "&qrls=" + RadWorkingRoles.SelectedItem.Text);
    }
    protected void lnkNewRO_Click(object sender, EventArgs e)
    {
        Response.Redirect("ReleaseOrder.aspx?ptype=New&poid=0&qrls=" + RadWorkingRoles.SelectedItem.Text);
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
}




