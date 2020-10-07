using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Text.RegularExpressions;
using System.Data;
using System.Data.SqlClient;
using System.Web.Security;

public partial class Intranet_Reporting_WorkflowReportFPO : System.Web.UI.Page
{
    
    protected void Page_Load(object sender, EventArgs e)
    {
        if (!IsPostBack)
        {
            tblMain.Visible = true;
            txtDate.Text = DateTime.Now.AddDays(-7).ToString("MM-dd-yyyy");
            txtDateTo.Text = DateTime.Now.ToString("MM-dd-yyyy");
            
            bindDdlProcessType();
            BindDDLUsers();
            BindBUs();
        }
    }
    protected void ddlProcessType_SelectedIndexChanged(object sender, EventArgs e)
    {
        lblTypeSelected.Text = ddlProcessType.SelectedItem.Text;
        Session["ProcessTypeName"] = ddlProcessType.SelectedItem.Text;  //set session value

        setgridtonull();
        lstOrders.Items.Clear();
        
        bindDdlStatus();

    }
    protected void bindDdlProcessType()
    {
        Db.LoadDDLsWithCon(ddlProcessType, "select processId,upper(processName) as pname from dbo.process_def where processId in (10011,10012,10013) order by processId ", "pname", "processId", myGlobal.getIntranetDBConnectionString());
        ddlProcessType.SelectedIndex = 0;
        lblTypeSelected.Text = ddlProcessType.SelectedItem.Text;

        bindDdlStatus();
    }

    protected void bindDdlStatus()
    {

        if (ddlProcessType.SelectedIndex >= 0)
            Db.LoadDDLsWithConNew1(ddlStatus, "select processStatusID,processStatusName from dbo.processStatus where fk_processId=" + ddlProcessType.SelectedValue.ToString() + " order by processStatusID", "processStatusName", "processStatusID", myGlobal.getIntranetDBConnectionString());

        if(ddlStatus.Items.Count> 0)
            ddlStatus.SelectedIndex =1 ;
    }
    private void BindBUs()
    {
        Db.LoadDDLsWithCon(ddlBus, "select BUId,BUName as BU from tej.[dbo].[TblVendorsBUMapping] order by BUName", "BU", "BUId", myGlobal.getIntranetDBConnectionString());
        //lblUsrCount.Text = ddlUser.Items.Count.ToString();
    }

    protected void lstOrders_SelectedIndexChanged(object sender, EventArgs e)
    {
       lblMsg.Text = "";

        Bindgrid();
        gridRep.PageIndex = 0;
    }
    private void BindDDLUsers()
    {
        Db.LoadDDLsWithCon(ddlUser, "select userId,userName from " + myGlobal.getcurrentMembershipDBName() + ".dbo.aspnet_Users order by username", "userName", "userId", myGlobal.getIntranetDBConnectionString());
        lblUsrCount.Text = ddlUser.Items.Count.ToString();
    }

    protected void btnReport_Click(object sender, EventArgs e)
    {
        setOrderFiltersBindLst();
    }

    private void setOrderFiltersBindLst()
    {
        lblMsg.Text = "";
        
        if (!Util.IsValidDate(txtDate.Text))
        {
            lblMsg.Text = "Invalid Date in Date Filed, Kindly correct and retry.";
            return;
        }


        if (!Util.IsValidDate(txtDateTo.Text))
        {
            lblMsg.Text = "Invalid Date in Date Filed, Kindly correct and retry.";
            return;
        }


        //Create where clause

        lblWhere.Text = "";

        lblWhere.Text += "and (PoDate>='" + txtDate.Text + "' and PoDate<='" + txtDateTo.Text + "')";

        if (!chkStatus.Checked)  //if it is not checked means single user
            if(ddlStatus.SelectedIndex>=0)
            {
                lblWhere.Text += " and R.fk_StatusId=" + ddlStatus.SelectedItem.Value ;
            }
            else
            {
                lblMsg.Text = "Invalid ! Status List Filed data error, should not be empty.";
                return;
            }
        
        if (!chkBUAll.Checked)  //if it is not checked means single user
            if (ddlBus.SelectedIndex >= 0)
            {
                lblWhere.Text += " and P.bu='" + ddlBus.SelectedItem.Text + "'";
            }
            else
            {
                lblMsg.Text = "Invalid ! BU List Filed data error, should not be empty.";
                return;
            }
        
        if (!chkAllUsers.Checked)  //if it is not checked means single user
            if (ddlUser.SelectedIndex >= 0)
            {
             lblWhere.Text += " and createdBy='" + ddlUser.SelectedItem.Text + "'";
            }
            else
            {
                lblMsg.Text = "Invalid ! User List Filed data error, should not be empty.";
                return;
            }

        BindOrderList();
        
    }

    private void BindOrderList()
    {
        //psql = "select reportType,BU,reportForDate,fileNameOnly,actionDated,ByUser from reportTrackDownloads " + lblWhere.Text.Trim() + " order by actionDated desc,ByUser,reportType,BU";
        Db.LoadlstWithCon(lstOrders, "select (P.Bu + ' - ' + P.evoPoNo + ' - ' + P.fpoNo + ' - ' + convert(varchar(10),convert(varchar, P.PoDate, 110)) + ' - ' + P.createdBy + ' - ' + convert(varchar(20),P.poId)) as POList,R.fk_StatusId,S.processStatusName,R.processRequestId,P.* from dbo.PurchaseOrders P left join dbo.processRequest R on R.refId=P.poId left join dbo.processStatus S on S.processStatusID=R.fk_StatusId and S.fk_processId=" + ddlProcessType.SelectedValue.ToString() + " where R.fk_processId=" + ddlProcessType.SelectedValue.ToString() + lblWhere.Text, "POList", "processRequestId", myGlobal.getIntranetDBConnectionString());
        lblOrdCnt.Text = lstOrders.Items.Count.ToString();
        setgridtonull();
    }

    private void Bindgrid()
    {
        string psql;
        psql = "select * from dbo.processStatusTrack where fk_processRequestId=" + lstOrders.SelectedItem.Value.ToString() + " order by autoIndex";
        psql = "select P.processStatusName,T.autoIndex,T.action_StatusID,T.lastUpdatedBy,T.StatusAccept,T.lastModified,T.comments from dbo.processStatusTrack T join dbo.processStatus P on P.processStatusID=T.action_StatusID and P.fk_processId=" + ddlProcessType.SelectedItem.Value.ToString() + " where T.fk_processRequestId=" + lstOrders.SelectedItem.Value.ToString() + " and T.fk_processId=" + ddlProcessType.SelectedItem.Value.ToString() + " order by autoIndex ";
        Db.constr = myGlobal.getIntranetDBConnectionString();
        DataTable dtbl = Db.myGetDS(psql).Tables[0];
        
        gridRep.DataSource = dtbl;
        gridRep.DataBind();

        if (gridRep.Rows.Count < 1)
        {
            lblMsg.Text = "No data available for current selected user as per current filters.";
            lblCntRows.Text = "0";
        }
        else
        {
            updateGrid();
            lblCntRows.Text = dtbl.Rows.Count.ToString();
        }
    }

    private void updateGrid()
    {
        DateTime dt1, dt2;
        int lp = 0;
        Label lbllastModified, lblTimeTaken;
        int hr=0,mins = 0;

        dt1 = DateTime.Now;
        dt2 = DateTime.Now;

        foreach (GridViewRow row in gridRep.Rows)
        {
            lbllastModified = row.FindControl("lbllastModified") as Label;
            lblTimeTaken = row.FindControl("lblTimeTaken") as Label;

            dt2 = Convert.ToDateTime(lbllastModified.Text);

            if (lp == 0)
            {
                dt1 = dt2;
                hr =Convert.ToInt32(dt2.Subtract(dt1).TotalHours);
                mins = Convert.ToInt32(dt2.Subtract(dt1).Minutes);
                lblTimeTaken.Text = hr.ToString() + " Hrs " + mins.ToString() + " Mins";
            }
            else
            {
                hr = Convert.ToInt32(dt2.Subtract(dt1).TotalHours);
                mins = Convert.ToInt32(dt2.Subtract(dt1).Minutes);
                lblTimeTaken.Text = hr.ToString() + " Hrs " + mins.ToString() + " Mins";
            }

            dt1 = dt2;
            lp = lp + 1;
        }
    }

    private void setgridtonull()
    {
        gridRep.DataSource = null;
        gridRep.DataBind();
        lblCntRows.Text = "0";
    }
    


    protected void gridRep_PageIndexChanging(object sender, GridViewPageEventArgs e)
    {
        gridRep.PageIndex = e.NewPageIndex;
        Bindgrid();
    }

    protected void ddlStatus_SelectedIndexChanged(object sender, EventArgs e)
    {
        setOrderFiltersBindLst();
    }
    protected void ddlBus_SelectedIndexChanged(object sender, EventArgs e)
    {
        setOrderFiltersBindLst();
    }
    protected void ddlUser_SelectedIndexChanged(object sender, EventArgs e)
    {
        setOrderFiltersBindLst();
    }
    protected void chkStatus_CheckedChanged(object sender, EventArgs e)
    {
        setOrderFiltersBindLst();
    }
    protected void chkBUAll_CheckedChanged(object sender, EventArgs e)
    {
        setOrderFiltersBindLst();
    }
    protected void chkAllUsers_CheckedChanged(object sender, EventArgs e)
    {
        setOrderFiltersBindLst();
    }
    protected void txtDate_TextChanged(object sender, EventArgs e)
    {
        setOrderFiltersBindLst();
    }
    protected void txtDateTo_TextChanged(object sender, EventArgs e)
    {
        setOrderFiltersBindLst();
    }
}