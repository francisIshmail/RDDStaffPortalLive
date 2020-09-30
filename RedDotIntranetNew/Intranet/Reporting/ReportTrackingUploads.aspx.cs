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

public partial class Intranet_Reporting_ReportTrackingUploads : System.Web.UI.Page
{
    protected void Page_Load(object sender, EventArgs e)
    {
        if (!IsPostBack)
        {
            string myPg = HttpContext.Current.Request.RawUrl;
            string psql;
            psql = string.Format("select pageVPath,allowedUser from pagePermissions where pageVPath='" + myPg + "'");
            Db.constr = myGlobal.getIntranetDBConnectionString();
            DataSet ds;
            ds = Db.myGetDS(psql);

            Boolean flg=false;
            foreach( DataRow rw in ds.Tables[0].Rows)
            {
                if (rw["allowedUser"].ToString().ToUpper() == myGlobal.loggedInUser().ToUpper())
                {
                    flg = true;
                    break;
                }
            }

            if (flg == false)
            {
                tblMain.Visible = false;
                lblMsg.Text = "Permissions Denied to current User for this Page, Contact administrator for further queries.";
                return;
            }
            else  //load page controls
            {
                txtDate.Text = DateTime.Now.ToString("MM/dd/yyyy");
                txtDateTo.Text = DateTime.Now.ToString("MM/dd/yyyy");
                tblMain.Visible = true;
                
                BindDDLUsers();

                Db.LoadlstWithCon(lstRepType, "select distinct(reportType) from reportTrackUploads order by reportType", "reportType", "reportType", myGlobal.getIntranetDBConnectionString());

                //Db.LoadlstWithCon(lstRepType, "select repTypeId,reportType from tej.dbo.webReportTypes order by reportType", "reportType", "repTypeId", myGlobal.getConnectionStringForDB("TZ"));
                //lblIsApplicable.Text = "Applicable";
                //ddlBus.Enabled = true;

            }
        }

    }

    private void BindDDLUsers()
    {
        Db.LoadDDLsWithCon(ddlUser, "select userId,userName from " + myGlobal.getcurrentMembershipDBName() + ".dbo.aspnet_Users order by username", "userName", "userId", myGlobal.getIntranetDBConnectionString());
        lblUsrCount.Text = ddlUser.Items.Count.ToString();
    }

    protected void btnReport_Click(object sender, EventArgs e)
    {
        lblMsg.Text = "";

        if (lstRepType.SelectedIndex < 0)
        {
            lblMsg.Text = "Invalid Request ! No report selected from the list , Kindly select at least one report and retry.";
            return;
        }

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

        lblWhere.Text = "Where ";

        lblWhere.Text += " (actionDated>='" + txtDate.Text + "' and actionDated<='" + txtDateTo.Text + " 23:59:59')";

        if(!chkAllUsers.Checked)  //if it is not checked means single user
         lblWhere.Text += " and ByUser='" + ddlUser.SelectedItem.Text + "'";


        if (radSingle.Checked)  //it means it is single selection case thus bu is applicable
        {
            lblWhere.Text += " and reportType='" + lstRepType.SelectedItem.Text + "'";
            
            //if(!chkBUAll.Checked)
            //lblWhere.Text += " and BU='" + ddlBus.SelectedItem.Text + "'";
        }
        else  //multiple reports selected, no bu applicable 
        {
            string tmpStr="";
            foreach (ListItem lst in lstRepType.Items)
            {
                if (lst.Selected == true)
                {
                    if (tmpStr == "")
                        tmpStr = lst.Text;  //lblWhere.Text += " reportType='" + lstRepType.SelectedItem.Text + "'";
                    else
                        tmpStr += ";" + lst.Text;
                }
            }

            string[] sfsl = tmpStr.Split(';');
            for (int i = 0; i < sfsl.Length; i++)
            {
                //objMailMessage.To.Add(sfsl[i]);
                if(i==0)
                    lblWhere.Text += " and ( reportType='" + sfsl[i] + "'";
                else
                    lblWhere.Text += " or reportType='" + sfsl[i] + "'";
            }

            lblWhere.Text += " )";
        }
        
        
        Bindgrid();
        gridRep.PageIndex = 0;
    }

    private void Bindgrid()
    {
        string psql;
        psql = "select reportType,BU,reportForDate,fileNameOnly,actionDated,ByUser,comments from reportTrackUploads " + lblWhere.Text.Trim() + " order by actionDated desc,ByUser,reportType,BU";
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
            lblCntRows.Text = dtbl.Rows.Count.ToString();
    }


    private void setgridtonull()
    {
        gridRep.DataSource = null;
        gridRep.DataBind();
        lblCntRows.Text = "0";
    }
    private void setSts()
    {

        if (radSingle.Checked)
        {
            //lblIsApplicable.Text = "Applicable";
            //ddlBus.Enabled = true;
            //chkBUAll.Enabled = true;
            lstRepType.SelectedIndex = -1;
            lstRepType.SelectionMode = System.Web.UI.WebControls.ListSelectionMode.Single;
            //ddlBus.DataSource = null;
            //ddlBus.Items.Clear();
        }
        else
        {
            //lblIsApplicable.Text = "Not Applicable";
            //ddlBus.Enabled = false;
            //chkBUAll.Enabled = false;
            lstRepType.SelectedIndex = -1;
            lstRepType.SelectionMode = System.Web.UI.WebControls.ListSelectionMode.Multiple;
            //ddlBus.DataSource = null;
            //ddlBus.Items.Clear();
        }

        setgridtonull();
    }


    protected void lstRepType_SelectedIndexChanged(object sender, EventArgs e)
    {
        setgridtonull();

        //if (radSingle.Checked)
        //    Db.LoadDDLsWithCon(ddlBus, "select BUId,BU from tej.dbo.webReportTypesNBU where fk_repTypeId=" + lstRepType.SelectedValue.ToString() + " order by BU", "BU", "BUId", myGlobal.getConnectionStringForDB("TZ"));
        //else
        //{
        //    ddlBus.DataSource = null;
        //    ddlBus.Items.Clear();
        //}

    }
    protected void gridRep_PageIndexChanging(object sender, GridViewPageEventArgs e)
    {
        gridRep.PageIndex = e.NewPageIndex;
        Bindgrid();
    }
    protected void radSingle_CheckedChanged(object sender, EventArgs e)
    {
        setSts();
    }
    protected void radMultiple_CheckedChanged(object sender, EventArgs e)
    {
        setSts();
    }
}