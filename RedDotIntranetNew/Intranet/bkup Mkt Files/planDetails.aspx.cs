using System;
using System.Data;
using System.Data.SqlClient;
using System.IO;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Collections;
using System.Web.UI.HtmlControls;

public partial class Intranet_orders_PlanDetails : System.Web.UI.Page
{
    SqlDataReader dr, drd;
    string newStatusId, newescalateLevelId, sql, refId, action, pid, qrls, processUrl, userEmail, fls,mufFls, FilesForuploadTrack, activityDetailHtmlString, iffDetailHtmlString, mufDetailHtmlString, mufUpdateSql;
    //string[] userRole;
    string whereClauseRoleLine;
    int deleteRow, testCurrentStatus;
    string toIffStatusId = "10", toIffRoleId = "10";
    
    protected void Page_Load(object sender, EventArgs e)
    {
        /*
         * 
         * 
                     ----------------------findings 07-june -2013 -------------,6,32,33,34,35,36,37----, order no ----- 80005 , 80030 , 80031,32,33-------
           
         * ----select sno from dbo.TblActivities order by sno
            ----select distinct(fk_ActivitySno) from TblActivitiesDetails order by fk_ActivitySno
            ----------This query fect all activities those do not have details line
            ----select * from dbo.TblActivities where sno not in (select distinct(fk_ActivitySno) from TblActivitiesDetails) order by hasexpenses

            -------------correct Live data with this
            ------update dbo.TblActivities set hasExpenses='No' where sno in (6,32,33,34,35,36,37)
            -------------------------------------------

         */
        
        //AddCost();
        
        //MaintainScrollPositionOnPostBack = true;

        lblThisVerDate.Text = "04-13-2013";  //mm-dd-yyyy database compatibility change
        lblMsg.Text = "";
        
        //btnSubmit.Attributes.Add("onClick", "return getConfirmation();");

        chkExecutionCompleted.Attributes.Add("onchange", "CheckChanged(this);");

        refId = Request.QueryString["oId"].ToString();
        pid = Request.QueryString["pid"].ToString(); //process type id
        action = Request.QueryString["action"].ToString();
        qrls = Request.QueryString["qrls"].ToString();

        if (!IsPostBack)
            BindGrid(); //changed the row for call from r21 to here

        lblrefId.Text = refId.ToString();

        //processUrl = myGlobal.getSystemConfigValue("RedDotHostRootUrlIntra") + "Intranet/orders/PlanDetails.aspx?oId=" + refId + "&pid=" + pid + "&action=task";
        
        //processUrl = myGlobal.getSystemConfigValue("RedDotHostRootUrlIntra") + "Intranet/orders/viewOrdersMKT.aspx?wfTypeId=10031";

        processUrl = myGlobal.getSiteIPwithPortNo() + "/Intranet/orders/viewOrdersMKT.aspx?wfTypeId=10031";

        //userRole = myGlobal.loggedInRoleList();
        //whereClauseRoleLine = "";
        //for (int i = 0; i < userRole.Length; i++)
        //{
        //    if (whereClauseRoleLine == "")
        //        whereClauseRoleLine = "'" + userRole[i] + "'";
        //    else
        //        whereClauseRoleLine += ",'" + userRole[i] + "'";
        //}

        whereClauseRoleLine = "'" + qrls + "'";

        userEmail = myGlobal.membershipUserEmail(myGlobal.loggedInUser());

        try
        {
            if (!IsPostBack)
            {
                sql = "select * from dbo.processRequest where fk_escalateLevelId in (select roleId from dbo.roles where fk_deptId in (select autoindex from dbo.departments where upper(departmentName) in (" + whereClauseRoleLine + "))) and fk_processId=" + pid + " and refId=" + refId;
                //sql = "select * from dbo.processRequest where refId=" + refId + " and processType='MKT'";
                Db.constr = myGlobal.getRDDMarketingDBConnectionString();
                dr = Db.myGetReader(sql);

                if (action.ToUpper() == "VIEW")
                {
                    tblTask.Visible = false;
                    lblHeader.Text = "Viewable Plan Information";
                }
                else
                    if (action.ToUpper() == "TASK")
                    {
                        if (dr.HasRows)
                        {
                            tblTask.Visible = true;
                            lblHeader.Text = "Updatable Plan Information";
                        }
                        else
                        {
                            tblTask.Visible = false;
                            lblHeader.Text = "Plan Process to next Status , No task pending for the status you logged in for";
                        }
                    }


                if (dr.HasRows)
                {
                    dr.Read();
                    //Session["currStatusId"] = dr["fk_StatusId"].ToString();
                    
                    if (dr["fk_StatusId"].ToString() == "0")   //if status is closed set it to 7 status for Admin to created MUFs after IFF
                        Session["currStatusId"] = "7";
                    else
                        Session["currStatusId"] = dr["fk_StatusId"].ToString();

                    Session["currescalateLevelId"] = dr["fk_escalateLevelId"].ToString();
                    Session["processRequestId"] = dr["processRequestId"].ToString();
                    dr.Close();

                }


                sql = "select  vd.buName,vd.buAbbr,w.*,p.comments,s.processStatusName as currentStatus,pd.processAbbr from dbo.workFlowPlans as w join  processRequest as p on w.autoindex=p.refId and p.fk_processId=" + pid + " join dbo.processStatus as s on p.fk_StatusID=s.processStatusId and s.fk_processId=" + pid + " join dbo.process_def as pd on p.fk_processId=pd.processId join dbo.VendorBUDef as vd on w.fk_vendorBU=vd.autoindex where w.autoindex=" + refId;
                Db.constr = myGlobal.getRDDMarketingDBConnectionString();
                drd = Db.myGetReader(sql);
                if (drd.HasRows)
                {
                    //(planVendor,vendorActivityId,planQuater,planYear,VendorApprovedAmount,vendorApprovalDate,planActualCost,planXlsFileNamePath,planDescription,status,lastModified)
                    drd.Read();
                    txtVendor.Text = drd["buName"].ToString();
                    lblVendorBUAbbr.Text = drd["buAbbr"].ToString();
                    lblVendorBUId.Text = drd["fk_VendorBU"].ToString();
                    txtActivityId.Text = drd["vendorActivityId"].ToString();
                    txtQuarter.Text = drd["planQuater"].ToString();
                    txtVendorQuarter.Text = drd["vendorQuater"].ToString();
                    txtyear.Text = drd["planYear"].ToString();
                    txtApprovedAmt.Text = drd["VendorApprovedAmount"].ToString();

                    DateTime dts;
                    dts = Convert.ToDateTime(drd["vendorApprovalDate"]);
                    txtApprovedDate.Text = dts.ToString("MM-dd-yyyy");

                    dts = Convert.ToDateTime(drd["deadlineDate"]);
                    lblDeadLineDate.Text = dts.ToString("MM-dd-yyyy");

                    txtActualCost.Text = drd["planActualCost"].ToString();
                    lblFile.Text = drd["planXlsFileNamePath"].ToString();
                    lblFileVendor.Text = drd["vendorplanXlsFileNamePath"].ToString();
                    txtDesc.Text = drd["planDescription"].ToString();
                    lblStatus.Text = drd["currentStatus"].ToString();
                    lblStatusCopy.Text = lblStatus.Text;
                    txtcomments.Text = drd["comments"].ToString();
                    lblLastModified.Text = drd["lastModified"].ToString();
                    lblPlanTableSts.Text = drd["status"].ToString();
                    Session["processAbbr"] = drd["processAbbr"].ToString();
                    drd.Close();
                }

                lnkPlan.HRef = "/download.aspx?file=~" + lblFile.Text; //sets path for download link
                lnkPlanVendor.HRef = "/download.aspx?file=~" + lblFileVendor.Text; //sets path for download link

                if (Convert.ToInt32(Session["currStatusId"]) > 10) //status reached ClaimsPayments stage
                    lblClaimsPaymentsMsg.Text = "Claims and Payments Stage";
                else
                    lblClaimsPaymentsMsg.Text = "";

                if (lblPlanTableSts.Text.ToUpper() == "Plan Closed".ToUpper()) //status reached ClaimsPayments stage
                {
                    lblClaimsPaymentsMsg.Text = "Marketing Admin Rights for MUF creation stage after the Plan is Closed.";
                    lblPlanTableSts.Visible = true;
                }
                else
                    lblPlanTableSts.Visible = false;

                if (Convert.ToInt32(Session["currStatusId"]) < 0 || Convert.ToInt32(Session["currescalateLevelId"]) < 0)
                {
                    tblTask.Visible = false;
                    panelDownLoads.Visible = false;
                }
                else
                    loadTasks();

                testCurrentStatus = Convert.ToInt32(Session["currStatusId"]);

                Db.constr = myGlobal.getRDDMarketingDBConnectionString();
                int nextAccrualSrno;

                if (Convert.ToInt32(Session["currStatusId"]) <= 2) //gets next id when it is just creation of activities
                    nextAccrualSrno = Db.myExecuteScalar("select isnull(max(serialBuWise),0)+1 as sn from dbo.TblAccrualList where fk_VendorBU=" + lblVendorBUId.Text);
                else  //gets old id when it is editing case of activities 3,4
                    nextAccrualSrno = Db.myExecuteScalar("select serialBuWise as sn from dbo.TblAccrualList where fk_VendorBU=" + lblVendorBUId.Text + " and fk_workFlowPlansId=" + refId);

                lblnxtAccrualFormSerial.Text = nextAccrualSrno.ToString();
                lblAccrualFormNo.Text = lblVendorBUAbbr.Text + lblnxtAccrualFormSerial.Text;

                if (Convert.ToInt32(Session["currStatusId"]) >= 2 && Convert.ToInt32(Session["currStatusId"]) <= 4 && pid == "10031") //activity allocation
                {
                    freshDataGrid();
                    panelActivity.Visible = true;
                }
                else
                    panelActivity.Visible = false;

                if ((Convert.ToInt32(Session["currStatusId"]) >= 3) && pid == "10031") //Accrual case
                {
                    GenerateAccrualForm();
                    if ((Convert.ToInt32(Session["currStatusId"]) >= 3) && (Convert.ToInt32(Session["currStatusId"]) <= 5))
                    {
                        lblAccrualFormView.Visible = true;
                        if (Convert.ToInt32(Session["currStatusId"]) == 5)
                            panelAccrual.Visible = false;
                        else
                            panelAccrual.Visible = true;
                    }
                    else
                        lblAccrualFormView.Visible = false;
                }
                else
                    lblAccrualFormView.Text = "";

                if (Convert.ToInt32(Session["currStatusId"]) >= 10 && Convert.ToInt32(Session["currStatusId"]) <= 12 && pid == "10031")  //iff form case visible 9,10,11
                {
                    LoadIffData();
                    GenerateIffHtml();

                    panelIFF.Visible = true;
                    if (Convert.ToInt32(Session["currStatusId"]) >= 12)
                        panelIFF.Enabled = false;
                    else
                        panelIFF.Enabled = true;
                }
                else
                    panelIFF.Visible = false;


                if (Convert.ToInt32(Session["currStatusId"]) == 6)  //right only to MKT executives
                    panelExecutionCheck.Visible = true;
                else
                    panelExecutionCheck.Visible = false;


                //10-jan commented for new below
                //if ((Convert.ToInt32(Session["currStatusId"]) == 9 || Convert.ToInt32(Session["currStatusId"]) == 14) && pid == "10031")  //panelStatusUpdate  visible for only 8,13 status
                if ((Convert.ToInt32(Session["currStatusId"]) == 6 || Convert.ToInt32(Session["currStatusId"]) == 7 || Convert.ToInt32(Session["currStatusId"]) == 14) && pid == "10031")  //panelStatusUpdate  visible for only 8,13 status
                {
                    panelStatusUpdate.Visible = true;
                }
                else
                {
                    panelStatusUpdate.Visible = false;
                }

                ////work here on after 22-3-2013
                //if ((Convert.ToInt32(Session["currStatusId"]) == 0) && action.ToUpper()=="TASK" && pid == "10031")  //panelStatusUpdate visible exceptionally to admin TASK case when plan is closed (0 status). Admin to create MUF's here
                //{
                //    panelStatusUpdate.Visible = true;
                //}

                //call function to upload existing attachments, these are the attahements from the previous status

                // if (Convert.ToInt32(Session["currStatusId"]) == 4 || Convert.ToInt32(Session["currStatusId"]) == 7)
                loadExistingAttachmentsForStatus();

                testCurrentStatus = Convert.ToInt32(Session["currStatusId"]);

                loadMUFForms();
            }

            //string status = Request.QueryString["action"];
            if (action.ToUpper() == "VIEW")
            {
                foreach (GridViewRow rw in GridActStatusUpdate.Rows)
                {
                    Button btn = rw.FindControl("btnAddField") as Button;
                    DropDownList ddl = rw.FindControl("ddlActExeStatus") as DropDownList;
                    btn.Enabled = false;
                    ddl.Enabled = false;
                    Grid2.Enabled = false;
                }
            }

            //if (userRole.Contains("countryFinance".ToUpper()))
            if (qrls == "countryFinance".ToUpper())
            {
                panelInfo.Visible = false;
                panelDownLoads.Visible = false;
            }

        }
        catch (Exception exps)
        {
            lblMsg.Text = "Error !!! " + exps.Message + " , Kindly retry";
            MsgBoxControl1.show(lblMsg.Text, "Error !!! ");
        }
    }

    private void loadMUFForms()
    {
        if ((Convert.ToInt32(Session["currStatusId"]) >= 7) && (Convert.ToInt32(Session["currStatusId"]) <= 9))
        {
            GenerateMUFForm("Load");
            paneMUFFormView.Visible = true;
        }
    }

    private void loadExistingAttachmentsForStatus()
    {
        int prevSts = Convert.ToInt32(Session["currStatusId"]) - 1;
        //sql = "select * from dbo.uploadTrack where fk_refId=" + refId + " and fk_statusId=" + prevSts.ToString() + " order by srNo";
        //sql = "select * from dbo.uploadTrack where fk_refId=" + refId + " order by srNo";
        sql = "select * from (select websitefilePath from dbo.uploadTrack where fk_refId=" + refId + " ) as a union select * from (select mufFilePath as websitefilePath from TblActivitiesDetails where fk_ActivitySno in (select sno from dbo.TblActivities where fk_workflowplansid=" + refId + ") and mufFilePath is not null ) as b ";
        Db.constr = myGlobal.getRDDMarketingDBConnectionString();
        DataTable dt = new DataTable();
        dt = Db.myGetDS(sql).Tables[0];
        GridFiles.DataSource = dt;
        GridFiles.DataBind();

        if (GridFiles.Rows.Count > 0)
        {
            CheckBox chk;
            HyperLink lnk;

            foreach (GridViewRow rw in GridFiles.Rows)
            {
                chk = rw.FindControl("chklstAttachedFiles") as CheckBox;
                lnk = rw.FindControl("lnkFileLoc") as HyperLink;

                string tmpstr;
                int strt, ends;

                tmpstr = chk.Text;
                strt = tmpstr.LastIndexOf("/") + 1;
                ends = tmpstr.Length - strt;

                chk.Text = tmpstr.Substring(strt, ends);
                lnk.NavigateUrl = "/download.aspx?file=~" + tmpstr; //sets path for download link;
                chk.Checked = false;
            }
        }

        if (GridFiles.Rows.Count <= 0)
            lblNone.Visible = true;
        else
            lblNone.Visible = false;
    }

    private void LoadActivityGridEditable()
    {

        DataTable tbl = new DataTable();
        tbl.Columns.Add("autoindex", typeof(int));
        tbl.Columns.Add("Serial", typeof(int));
        tbl.Columns.Add("month", typeof(string));
        tbl.Columns.Add("date", typeof(DateTime));
        tbl.Columns.Add("activityTypeId", typeof(int));
        tbl.Columns.Add("amount", typeof(decimal));
        tbl.Columns.Add("detail", typeof(string));

        sql = "select * from TblActivities where fk_workFlowPlansId=" + refId;
        Db.constr = myGlobal.getRDDMarketingDBConnectionString();
        drd = Db.myGetReader(sql);

        while (drd.Read())
        {
            tbl.Rows.Add(
                Convert.ToInt32(drd["autoindex"]),
                Convert.ToInt32(drd["sno"]),
                drd["ActivityCreationMonth"].ToString(),
                Convert.ToDateTime(drd["ActivityCreationDate"]),
                Convert.ToInt32(drd["fk_activityTypeId"]),
                Convert.ToDecimal(drd["ActivityVendorAmount"]),
                drd["ActivityDetail"].ToString());

            //tbl.Rows.Add(0, 1, -1, 0.0, "");
        }
        drd.Close();

        ViewState["CurrentTable"] = tbl;
        GridView1.DataSource = (DataTable)ViewState["CurrentTable"];
        GridView1.DataBind();
        LoadActivityGrid();
    }

    private void freshDataGrid()
    {
        if (Convert.ToInt32(Session["currStatusId"]) == 2)
        {
            ViewState["CurrentTable"] = GetTableAtLoadOnly();
            GridView1.DataSource = (DataTable)ViewState["CurrentTable"];
            GridView1.DataBind();
            LoadActivityGrid();
        }

        if (Convert.ToInt32(Session["currStatusId"]) == 3 || Convert.ToInt32(Session["currStatusId"]) == 4)
            LoadActivityGridEditable();

    }


    DataTable GetTableAtLoadOnly()
    {
        //fetch latest activityid +1 to initiate i
        Db.constr = myGlobal.getRDDMarketingDBConnectionString();
        lblnxtActivitySrno.Text = Db.myExecuteScalar("select isnull(max(sno),0)+1 as sno from dbo.TblActivities").ToString();

        DataTable tbl = new DataTable();
        tbl.Columns.Add("autoindex", typeof(int));
        tbl.Columns.Add("Serial", typeof(int));
        tbl.Columns.Add("month", typeof(string));
        tbl.Columns.Add("date", typeof(string));
        tbl.Columns.Add("activityTypeId", typeof(int));
        tbl.Columns.Add("amount", typeof(decimal));
        tbl.Columns.Add("detail", typeof(string));

        int rws = 1;

        for (int i = 0; i < rws; i++)
            tbl.Rows.Add(0, (Convert.ToInt32(lblnxtActivitySrno.Text) + i), DateTime.Now.ToString("MMM"), DateTime.Now.ToString("MM-dd-yyyy"), -1, 0.0, "");

        return tbl;
    }

    private void refreshLatestActivitySerialsFromDB()
    {
        Db.constr = myGlobal.getRDDMarketingDBConnectionString();
        int nextAccrualSrno;

        if (Convert.ToInt32(Session["currStatusId"]) == 2) //gets next id when it is just creation of activities
            nextAccrualSrno = Db.myExecuteScalar("select isnull(max(serialBuWise),0)+1 as sn from dbo.TblAccrualList where fk_VendorBU=" + lblVendorBUId.Text);
        else  //gets old id when it is editing case of activities 3,4
            nextAccrualSrno = Db.myExecuteScalar("select serialBuWise as sn from dbo.TblAccrualList where fk_VendorBU=" + lblVendorBUId.Text + " and fk_workFlowPlansId=" + refId);

        lblnxtAccrualFormSerial.Text = nextAccrualSrno.ToString();
        lblAccrualFormNo.Text = lblVendorBUAbbr.Text + lblnxtAccrualFormSerial.Text;

        //fetch latest activityid from db to create valid serial no
        Db.constr = myGlobal.getRDDMarketingDBConnectionString();
        lblnxtActivitySrno.Text = Db.myExecuteScalar("select isnull(max(sno),0)+1 as sno from dbo.TblActivities").ToString();

        int cnt = 0;
        for (int i = 0; i < GridView1.Rows.Count; i++)
        {
            Label tmplblautoindex = (Label)GridView1.Rows[i].FindControl("lblautoindex") as Label;
            Label tmplblSerial = (Label)GridView1.Rows[i].FindControl("lblSerial") as Label;

            if (tmplblautoindex.Text != "0")
                cnt = cnt + 1;
            else
                tmplblSerial.Text = (Convert.ToInt32(lblnxtActivitySrno.Text) + i - cnt).ToString();

        }
        LoadActivityGrid();
    }

    private void LoadActivityGrid()
    {
        DropDownList ddltmp;
        foreach (GridViewRow rw in GridView1.Rows)
        {
            ddltmp = (rw.FindControl("ddlActivityType") as DropDownList);
            if (ddltmp != null)
            {
                //where isInternal=0 stands for vendor activities 1 stands for internal activities
                Db.LoadDDLsWithCon(ddltmp, "select autoindex,activityAbbr + '- ' + activityType as activity from ActivityDef where isInternal=0 order by activity", "activity", "autoindex", myGlobal.getRDDMarketingDBConnectionString());

                if (ddltmp.Items.Count > 0)
                {
                    Label tmplblSerial = (Label)rw.FindControl("lblSerial") as Label;
                    Label tmplblMonth = (Label)rw.FindControl("lblMonth") as Label;

                    Label tmplblActivityTypeID = (Label)rw.FindControl("lblActivityTypeID") as Label;
                    Label tmplblActivityIDFormed = (Label)rw.FindControl("lblActivityIDFormed") as Label;

                    if (tmplblActivityTypeID.Text == "-1")
                    {
                        tmplblActivityTypeID.Text = ddltmp.SelectedItem.Value.ToString();
                    }
                    else
                        ddltmp.Items.FindByValue(tmplblActivityTypeID.Text).Selected = true;

                    string sss = ddltmp.SelectedItem.Text.Substring(0, ddltmp.SelectedItem.Text.IndexOf("-"));
                    tmplblActivityIDFormed.Text = tmplblSerial.Text + "-" + tmplblMonth.Text + "-" + sss + lblVendorBUAbbr.Text.Trim();
                }
            }
        }
    }

    protected void btnClearAll_Click(object sender, EventArgs e)
    {
        freshDataGrid();
    }

    protected void btnAddRow_Click(object sender, EventArgs e)
    {
        deleteRow = -1;
        copyDataToTableAddNewRow();
    }

    protected void ddlActivityType_SelectedIndexChanged(object sender, EventArgs e)
    {
        DropDownList ddl = sender as DropDownList;

        //12-Jan-BBSAM

        foreach (GridViewRow row in GridView1.Rows)
        {
            Control ctrl = row.FindControl("ddlActivityType") as DropDownList;
            if (ctrl != null)
            {
                DropDownList ddl1 = (DropDownList)ctrl;
                if (ddl.ClientID == ddl1.ClientID)
                {
                    Label tmplblSerial = (Label)row.FindControl("lblSerial") as Label;
                    Label tmplblMonth = (Label)row.FindControl("lblMonth") as Label;

                    Label tmplblActivityTypeID = (Label)row.FindControl("lblActivityTypeID") as Label;
                    Label tmplblActivityIDFormed = (Label)row.FindControl("lblActivityIDFormed") as Label;

                    tmplblActivityTypeID.Text = ddl1.SelectedValue.ToString();
                    string sss = ddl1.SelectedItem.Text.Substring(0, ddl1.SelectedItem.Text.IndexOf("-"));
                    tmplblActivityIDFormed.Text = tmplblSerial.Text + "-" + tmplblMonth.Text + "-" + sss + lblVendorBUAbbr.Text.Trim();
                    break;
                }
            }
        }
    }

    protected void imgBtnClose_Click(object sender, ImageClickEventArgs e)
    {
        ImageButton btn = sender as ImageButton;

        foreach (GridViewRow row in GridView1.Rows)
        {
            Control ctrl = row.FindControl("imgBtnClose") as ImageButton;
            if (ctrl != null)
            {
                ImageButton btn1 = (ImageButton)ctrl;
                if (btn.ClientID == btn1.ClientID)
                {
                    deleteRow = row.RowIndex;
                    copyDataToTableAddNewRow();
                }
            }
        }
    }

    private void copyDataToTableAddNewRow()
    {
        DataTable dt = (DataTable)ViewState["CurrentTable"];
        Label tmplblautoindex;
        Label tmplblSerial;
        Label tmplblmonth;
        Label tmplbldate;
        Label tmplblActivityTypeID;
        TextBox tmptxtAmount;
        TextBox tmptxtActivityDetail;
        DropDownList ddlPbx;

        foreach (GridViewRow rw in GridView1.Rows)
        {
            tmptxtAmount = (rw.FindControl("txtAmount") as TextBox);

            if (!Util.isValidDecimalNumber(tmptxtAmount.Text))
            {
                lblMsg.Text = "Please supply a valid numeric value for Amount field in row :" + (rw.RowIndex + 1).ToString();
                MsgBoxControl1.show(lblMsg.Text, "Error !!! ");
                return;
            }
        }

        int i = 0;
        foreach (GridViewRow rw in GridView1.Rows)
        {
            tmplblautoindex = (Label)rw.FindControl("lblautoindex") as Label;
            tmplblSerial = (Label)rw.FindControl("lblSerial") as Label;
            tmplblmonth = (Label)rw.FindControl("lblmonth") as Label;
            tmplbldate = (Label)rw.FindControl("lbldate") as Label;
            ddlPbx = (rw.FindControl("ddlActivityType") as DropDownList);
            tmplblActivityTypeID = (rw.FindControl("lblActivityTypeID") as Label);
            tmptxtAmount = (rw.FindControl("txtAmount") as TextBox);
            tmptxtActivityDetail = (rw.FindControl("txtActivityDetail") as TextBox);

            //tmplblActivityTypeID.Text = ddlPbx.SelectedValue.ToString();

            dt.Rows[i]["autoindex"] = Convert.ToInt32(tmplblautoindex.Text);
            dt.Rows[i]["Serial"] = Convert.ToInt32(tmplblSerial.Text);
            dt.Rows[i]["month"] = tmplblmonth.Text;
            dt.Rows[i]["date"] = Convert.ToDateTime(tmplbldate.Text);
            dt.Rows[i]["activityTypeId"] = Convert.ToInt32(tmplblActivityTypeID.Text);
            dt.Rows[i]["amount"] = Convert.ToDecimal(tmptxtAmount.Text);
            dt.Rows[i]["detail"] = tmptxtActivityDetail.Text;

            i++;
        }

        if (deleteRow >= 0)
        {
            //delete new row
            dt.Rows.RemoveAt(deleteRow);
            deleteRow = -1;

            for (i = 0; i < dt.Rows.Count; i++)
            {
                dt.Rows[i]["Serial"] = (i + Convert.ToInt32(lblnxtActivitySrno.Text));
            }
        }
        else
        {
            //Add new row
            if (Convert.ToInt32(Session["currStatusId"]) == 2)
                dt.Rows.Add(0, (Convert.ToInt32(lblnxtActivitySrno.Text) + i), DateTime.Now.ToString("MMM"), DateTime.Now, -1, 0.0, "");

            if (Convert.ToInt32(Session["currStatusId"]) == 3 || Convert.ToInt32(Session["currStatusId"]) == 4)
                dt.Rows.Add(0, (11), DateTime.Now.ToString("MMM"), DateTime.Now, -1, 0.0, "");
        }

        ViewState["CurrentTable"] = dt;

        GridView1.DataSource = (DataTable)ViewState["CurrentTable"];
        GridView1.DataBind();
        refreshLatestActivitySerialsFromDB();
    }

    private void loadTasks()
    {
        int st = Convert.ToInt32(Session["currStatusId"]);
        sql = "select * from TaskList where fk_StatusID=" + st + " and fk_processId=" + pid + " order by autoindex";

        Db.constr = myGlobal.getRDDMarketingDBConnectionString();
        gridTaskList.DataSource = Db.myGetDS(sql).Tables[0];
        gridTaskList.DataBind();
    }

    private bool insertIffData()
    {
        bool retns = false;
        decimal totAmt = 0;
        string tmpaccrnofromgrid = "";

        if (!Util.IsValidDate(txtInvDate.Text))
        {
            lblMsg.Text = "Please supply a valid date for Invoice date (mm-dd-yyyy) field in Iff Form";
            MsgBoxControl1.show(lblMsg.Text, "Error !!! ");
            return false;
        }

        if (txtBilledTo.Text.Trim() == "")
        {
            lblMsg.Text = "BilledTo field can't be empty in Iff Form";
            MsgBoxControl1.show(lblMsg.Text, "Error !!! ");
            return false;
        }

        if (txtInvDetail.Text.Trim() == "")
        {
            lblMsg.Text = "Invoice Detail field can't be empty in Iff Form";
            MsgBoxControl1.show(lblMsg.Text, "Error !!! ");
            return false;
        }

        if (txtAdd1.Text.Trim() == "")
        {
            lblMsg.Text = "Address Field 1 can't be empty in Iff Form";
            MsgBoxControl1.show(lblMsg.Text, "Error !!! ");
            return false;
        }

        if (txtAdd2.Text.Trim() == "")
        {
            lblMsg.Text = "Address Field 2 can't be empty in Iff Form";
            MsgBoxControl1.show(lblMsg.Text, "Error !!! ");
            return false;
        }
        Label tmplblautoindex;
        Label tmplblAccrualFormNo;
        TextBox tmptxtActivityActualCost;
        TextBox tmptxtactivityProcessDate;
        string tmpqry = "";

        foreach (GridViewRow rw in GridViewIff.Rows)
        {
            tmplblautoindex = (Label)rw.FindControl("lblautoindex") as Label;
            tmplblAccrualFormNo = (Label)rw.FindControl("lblAccrualFormNo") as Label;
            tmptxtActivityActualCost = (rw.FindControl("txtActivityActualCost") as TextBox);
            tmptxtactivityProcessDate = (rw.FindControl("txtactivityProcessDate") as TextBox);

            tmpaccrnofromgrid = tmplblAccrualFormNo.Text;

            if (tmptxtactivityProcessDate.Text.Trim() == "")
                tmptxtactivityProcessDate.Text = DateTime.Now.ToString("MM-dd-yyyy");

            if (!Util.IsValidDate(tmptxtactivityProcessDate.Text))
            {
                lblMsg.Text = "Please supply a valid date (mm-dd-yyyy) for Activity Date field in Iff Form at row :" + (rw.RowIndex + 1).ToString();
                MsgBoxControl1.show(lblMsg.Text, "Error !!! ");
                return false;
            }

            if (!Util.isValidDecimalNumber(tmptxtActivityActualCost.Text))
            {
                lblMsg.Text = "Please supply a valid numeric value for Actual Cost field in Iff Form at row :" + (rw.RowIndex + 1).ToString();
                MsgBoxControl1.show(lblMsg.Text, "Error !!! ");
                return false;
            }

            totAmt = totAmt + Convert.ToDecimal(tmptxtActivityActualCost.Text);

            if (tmpqry == "")
                tmpqry = string.Format("update TblActivities set ActivityActualCost={0},activityProcessDate='{1}',lastModified='{2}' where autoindex=" + tmplblautoindex.Text, tmptxtActivityActualCost.Text, tmptxtactivityProcessDate.Text,DateTime.Now.ToString());
            else
                tmpqry += " ; " + string.Format("update TblActivities set ActivityActualCost={0},activityProcessDate='{1}',lastModified='{2}' where autoindex=" + tmplblautoindex.Text, tmptxtActivityActualCost.Text, tmptxtactivityProcessDate.Text,DateTime.Now.ToString());
        }

        if (tmpqry != "")
        {
            if (Convert.ToInt32(Session["currStatusId"]) == 10)
                tmpqry += " ; " + string.Format("insert into TblIFF(fk_workFlowPlansId,invoiceDate,billedto,add1,add2,invoiceDetail,accrualFormNo,submittedBy,submittedDate) "
                    + "values({0},'{1}','{2}','{3}','{4}','{5}','{6}','{7}','{8}')", refId, txtInvDate.Text, txtBilledTo.Text, txtAdd1.Text, txtAdd2.Text, txtInvDetail.Text, tmpaccrnofromgrid, myGlobal.loggedInUser(), DateTime.Now.ToString("MM-dd-yyyy"));

            if (Convert.ToInt32(Session["currStatusId"]) == 11)
                tmpqry += " ; " + string.Format("update TblIFF set invoiceDate='{0}',billedto='{1}',add1='{2}',add2='{3}',invoiceDetail='{4}',accrualFormNo='{5}',verifiedBy='{6}',verifiedDate='{7}' where fk_workFlowPlansId=" + refId, txtInvDate.Text, txtBilledTo.Text, txtAdd1.Text, txtAdd2.Text, txtInvDetail.Text, tmpaccrnofromgrid, myGlobal.loggedInUser(), DateTime.Now.ToString("MM-dd-yyyy"));

            Db.constr = myGlobal.getRDDMarketingDBConnectionString();
            Db.myExecuteSQL(tmpqry);
        }

        retns = true; //if process reachess here it means it was successfull

        return retns;
    }

    private void LoadIffData()
    {
        DataTable dts;
        sql = "select (isnull(C.Costed,0)+isnull(C.Vat,0)) as actualActivityCostedNVat,a.*,b.invoiceDate,b.billedto,b.add1,b.add2,b.invoiceDetail,b.submittedBy,b.verifiedBy,C.* from TblActivities as a left join TblActivitiesDetails as C on a.autoIndex=C.fk_ActivitySno left join TblIFF b on a.fk_workFlowPlansId=b.fk_workFlowPlansId where a.fk_workFlowPlansId=" + refId + " and (C.mufFormFilledStatus='Yes' or C.mufFormFilledStatus is null)";
        Db.constr = myGlobal.getRDDMarketingDBConnectionString();
        dts = Db.myGetDS(sql).Tables[0];
        GridViewIff.DataSource = dts;
        GridViewIff.DataBind();

        DateTime ddd;

        if (dts.Rows.Count > 0)
        {
            if (dts.Rows[0]["invoiceDate"] != DBNull.Value)
            {
                ddd = Convert.ToDateTime(dts.Rows[0]["invoiceDate"]);
                txtInvDate.Text = ddd.ToString("MM-dd-yyyy");
            }

            if (dts.Rows[0]["billedto"] != DBNull.Value)
                txtBilledTo.Text = dts.Rows[0]["billedto"].ToString();

            if (dts.Rows[0]["add1"] != DBNull.Value)
                txtAdd1.Text = dts.Rows[0]["add1"].ToString();

            if (dts.Rows[0]["add2"] != DBNull.Value)
                txtAdd2.Text = dts.Rows[0]["add2"].ToString();

            if (dts.Rows[0]["invoiceDetail"] != DBNull.Value)
                txtInvDetail.Text = dts.Rows[0]["invoiceDetail"].ToString();
        }
    }

    private void GenerateIffHtml()
    {

        string submitusr = "", verifyuser = "", rddRefValue, prevActivityCode;
        DateTime submitDt, verifiedDt;
        decimal amts = 0;
        submitDt = verifiedDt = DateTime.Now;


        sql = "select submittedBy,submittedDate,verifiedBy,verifiedDate from TblIFF where fk_workFlowPlansId=" + refId;
        Db.constr = myGlobal.getRDDMarketingDBConnectionString();
        dr = Db.myGetReader(sql);

        if (dr.HasRows)
            while (dr.Read())
            {
                if (dr["submittedBy"] != DBNull.Value)
                    submitusr = dr["submittedBy"].ToString();

                if (dr["submittedDate"] != DBNull.Value)
                    submitDt = Convert.ToDateTime(dr["submittedDate"].ToString());

                if (dr["verifiedBy"] != DBNull.Value)
                    verifyuser = dr["verifiedBy"].ToString();

                if (dr["verifiedDate"] != DBNull.Value)
                    verifiedDt = Convert.ToDateTime(dr["verifiedDate"].ToString());

            }

        dr.Close();

        iffDetailHtmlString = "<br><table style='border-color:Black;width:90%' border=2><tr>";

        //Header Row
        iffDetailHtmlString += "<td style='font-weight:bold' align='center' colspan='6'>Invoice Issuing Form (IIF)";
        iffDetailHtmlString += "</td><tr>";

        //row 1
        iffDetailHtmlString += "<tr>";

        iffDetailHtmlString += "<td style='width:12%'>";
        iffDetailHtmlString += "BU";
        iffDetailHtmlString += "</td>";

        iffDetailHtmlString += "<td style='width:12%;color:Red'>";
        iffDetailHtmlString += txtVendor.Text;
        iffDetailHtmlString += "</td>";

        iffDetailHtmlString += "<td style='width:40%' align='right'>";
        iffDetailHtmlString += "Vendor Qtr";
        iffDetailHtmlString += "</td>";

        iffDetailHtmlString += "<td style='width:12%;color:Red'>";
        iffDetailHtmlString += txtVendorQuarter.Text;
        iffDetailHtmlString += "</td>";

        iffDetailHtmlString += "<td style='width:12%'>";
        iffDetailHtmlString += "RDD Qtr";
        iffDetailHtmlString += "</td>";

        iffDetailHtmlString += "<td style='width:12%;color:red'>";
        iffDetailHtmlString += txtQuarter.Text;
        iffDetailHtmlString += "</td></tr>";

        //row 2
        iffDetailHtmlString += "<tr>";

        iffDetailHtmlString += "<td>";
        iffDetailHtmlString += "Invoice Amount";
        iffDetailHtmlString += "</td>";

        iffDetailHtmlString += "<td style='color:Red'>";
        iffDetailHtmlString += txtApprovedAmt.Text;
        iffDetailHtmlString += "</td>";

        iffDetailHtmlString += "<td>";
        iffDetailHtmlString += "";
        iffDetailHtmlString += "</td>";

        iffDetailHtmlString += "<td style='color:Red'>";
        iffDetailHtmlString += "";
        iffDetailHtmlString += "</td>";

        iffDetailHtmlString += "<td>";
        iffDetailHtmlString += "";
        iffDetailHtmlString += "</td>";

        iffDetailHtmlString += "<td style='color:Red'>";
        iffDetailHtmlString += "";
        iffDetailHtmlString += "</td></tr>";

        //row 3

        iffDetailHtmlString += "<tr>";

        iffDetailHtmlString += "<td>";
        iffDetailHtmlString += "Vendor Activity ID";
        iffDetailHtmlString += "</td>";

        iffDetailHtmlString += "<td colspan='5' style='color:Red'>";
        iffDetailHtmlString += txtActivityId.Text;
        iffDetailHtmlString += "</td></tr>";

        //row 4

        iffDetailHtmlString += "<tr>";

        iffDetailHtmlString += "<td>";
        iffDetailHtmlString += "Invoice Date";
        iffDetailHtmlString += "</td>";

        iffDetailHtmlString += "<td colspan='5' style='color:Red'>";
        iffDetailHtmlString += txtInvDate.Text;
        iffDetailHtmlString += "</td></tr>";

        //row 5
        iffDetailHtmlString += "<tr>";

        iffDetailHtmlString += "<td style='width:50px'>";
        iffDetailHtmlString += "";//"Billed To";
        iffDetailHtmlString += "</td>";

        iffDetailHtmlString += "<td colspan='5' style='color:Red'>";
        iffDetailHtmlString += txtBilledTo.Text;
        iffDetailHtmlString += "</td></tr>";

        //row 7

        iffDetailHtmlString += "<tr>";

        iffDetailHtmlString += "<td>";
        iffDetailHtmlString += "";//"Address Line1";
        iffDetailHtmlString += "</td>";

        iffDetailHtmlString += "<td colspan='5' style='color:Red'>";
        iffDetailHtmlString += txtAdd1.Text;
        iffDetailHtmlString += "</td></tr>";

        //row 8

        iffDetailHtmlString += "<tr>";

        iffDetailHtmlString += "<td>";
        iffDetailHtmlString += "";//"Address Line2";
        iffDetailHtmlString += "</td>";

        iffDetailHtmlString += "<td colspan='5' style='color:Red'>";
        iffDetailHtmlString += txtAdd2.Text;
        iffDetailHtmlString += "</td></tr>";

        //row 9

        iffDetailHtmlString += "<tr>";

        iffDetailHtmlString += "<td>";
        iffDetailHtmlString += "Details Of Invoice";
        iffDetailHtmlString += "</td>";

        iffDetailHtmlString += "<td colspan='5' style='color:Red'>";
        iffDetailHtmlString += txtInvDetail.Text;
        iffDetailHtmlString += "</td></tr>";

        //row 10
        prevActivityCode = "";
        rddRefValue = "";
        Label tmplblActivityCode111;
        foreach (GridViewRow rw in GridViewIff.Rows)
        {
            tmplblActivityCode111 = (Label)rw.FindControl("lblActivityCode") as Label;

            if (prevActivityCode != tmplblActivityCode111.Text)
            {
                if (rddRefValue == "")
                    rddRefValue += tmplblActivityCode111.Text;
                else
                    rddRefValue += " , " + tmplblActivityCode111.Text;
            }
            prevActivityCode = tmplblActivityCode111.Text;
        }

        iffDetailHtmlString += "<tr>";
        iffDetailHtmlString += "<td colspan='6' style='color:Red' align='center'>RDD Ref :<b>";
        iffDetailHtmlString += rddRefValue;
        iffDetailHtmlString += "</b></td></tr>";

        //row 11
        iffDetailHtmlString += "<tr style='height:50px'>";
        iffDetailHtmlString += "<td colspan='6' align='center'>";
        iffDetailHtmlString += "Note: Only Information in Red will appear on the Invoice";
        iffDetailHtmlString += "</td></tr>";

        //row 12
        iffDetailHtmlString += "<tr>";
        iffDetailHtmlString += "<td colspan='6' align='center' style='color:Blue'>";
        iffDetailHtmlString += "For Internal Use Only";
        iffDetailHtmlString += "</td></tr>";

        //grid header

        iffDetailHtmlString += "<tr>";

        iffDetailHtmlString += "<td style='font-weight:bold'>";
        iffDetailHtmlString += "Activity No.";
        iffDetailHtmlString += "</td>";

        iffDetailHtmlString += "<td style='font-weight:bold'>";
        iffDetailHtmlString += "AF No.";
        iffDetailHtmlString += "</td>";

        iffDetailHtmlString += "<td  style='font-weight:bold'>"; //colspan='1'
        iffDetailHtmlString += "Activity Description";
        iffDetailHtmlString += "</td>";

        iffDetailHtmlString += "<td style='font-weight:bold'>";
        iffDetailHtmlString += "Date";
        iffDetailHtmlString += "</td>";

        iffDetailHtmlString += "<td style='font-weight:bold'>";
        iffDetailHtmlString += "Actual Cost $";
        iffDetailHtmlString += "</td>";

        iffDetailHtmlString += "<td style='font-weight:bold'>";
        iffDetailHtmlString += "Vat $";
        iffDetailHtmlString += "</td></tr>";
        /////////////Loop Grid

        Label tmplblActivityCode;
        Label tmplblAccrualFormNo;
        Label tmplblExpenseDetail;
        TextBox tmptxtActivityActualCost, tmptxtVat;
        TextBox tmptxtactivityProcessDate;
        prevActivityCode = "";
        foreach (GridViewRow rw in GridViewIff.Rows)
        {
            tmplblActivityCode = (Label)rw.FindControl("lblActivityCode") as Label;
            tmplblAccrualFormNo = (Label)rw.FindControl("lblAccrualFormNo") as Label;
            tmplblExpenseDetail = (Label)rw.FindControl("lblExpenseDetail") as Label;
            tmptxtActivityActualCost = (rw.FindControl("txtActivityActualCost") as TextBox);
            tmptxtVat = (rw.FindControl("txtVat") as TextBox);
            tmptxtactivityProcessDate = (rw.FindControl("txtactivityProcessDate") as TextBox);

            if (tmptxtActivityActualCost.Text == "")
                tmptxtActivityActualCost.Text = "0";

            if (tmptxtVat.Text == "")
                tmptxtVat.Text = "0";

            iffDetailHtmlString += "<tr>";

            iffDetailHtmlString += "<td style='color:Blue'>";

            if (prevActivityCode != tmplblActivityCode.Text)
                iffDetailHtmlString += tmplblActivityCode.Text;
            else
                iffDetailHtmlString += "";

            iffDetailHtmlString += "</td>";

            iffDetailHtmlString += "<td style='color:Blue'>";

            if (prevActivityCode != tmplblActivityCode.Text)
                iffDetailHtmlString += tmplblAccrualFormNo.Text;
            else
                iffDetailHtmlString += "";

            iffDetailHtmlString += "</td>";

            iffDetailHtmlString += "<td  style='color:Blue'>"; //colspan='2'
            iffDetailHtmlString += tmplblExpenseDetail.Text;
            iffDetailHtmlString += "</td>";

            iffDetailHtmlString += "<td style='color:Blue'>";
            iffDetailHtmlString += tmptxtactivityProcessDate.Text;
            iffDetailHtmlString += "</td>";

            iffDetailHtmlString += "<td style='color:Blue'>";
            iffDetailHtmlString += tmptxtActivityActualCost.Text;
            iffDetailHtmlString += "</td>";

            iffDetailHtmlString += "<td style='color:Blue'>";
            iffDetailHtmlString += tmptxtVat.Text;
            iffDetailHtmlString += "</td></tr>";

            

            amts = amts + Convert.ToDecimal(tmptxtActivityActualCost.Text);
            prevActivityCode = tmplblActivityCode.Text;
        }
        /////////////////

        //grid bottom1

        iffDetailHtmlString += "<tr>";

        iffDetailHtmlString += "<td colspan='2' style='color:Blue'>";
        iffDetailHtmlString += "";
        iffDetailHtmlString += "</td>";

        iffDetailHtmlString += "<td colspan='2' style='color:Blue' align='center'>";
        iffDetailHtmlString += "Total Spent";
        iffDetailHtmlString += "</td>";

        iffDetailHtmlString += "<td colspan='2' style='color:Blue' align='left'>";
        iffDetailHtmlString += "<b>$ " + amts + "</b> (Excluding Vat)";
        iffDetailHtmlString += "</td></tr>";

        //grid bottom2

        iffDetailHtmlString += "<tr>";

        iffDetailHtmlString += "<td colspan='2' style='color:Blue'>";
        iffDetailHtmlString += "";
        iffDetailHtmlString += "</td>";

        iffDetailHtmlString += "<td colspan='2' style='color:Blue' align='center'>";
        iffDetailHtmlString += "Total Saved";
        iffDetailHtmlString += "</td>";

        iffDetailHtmlString += "<td colspan='2' style='color:Blue' align='left'>";
        iffDetailHtmlString += "<b>$ " + (Convert.ToDecimal(txtApprovedAmt.Text) - amts).ToString() + "</b>";
        iffDetailHtmlString += "</td></tr>";

        /////////////last line of IFF

        iffDetailHtmlString += "<tr>";

        iffDetailHtmlString += "<td>";
        iffDetailHtmlString += "Submitted By";
        iffDetailHtmlString += "</td>";

        iffDetailHtmlString += "<td style='color:Blue'>";
        iffDetailHtmlString += submitusr;
        iffDetailHtmlString += "</td>";

        iffDetailHtmlString += "<td>&nbsp;";
        iffDetailHtmlString += "</td>";

        iffDetailHtmlString += "<td>&nbsp;";
        iffDetailHtmlString += "</td>";

        iffDetailHtmlString += "<td>";
        iffDetailHtmlString += "Verified By";
        iffDetailHtmlString += "</td>";

        iffDetailHtmlString += "<td style='color:Blue'>";
        iffDetailHtmlString += verifyuser;
        iffDetailHtmlString += "</td></tr>";

        iffDetailHtmlString += "</table>";

        lblIffFormHtml.Text = iffDetailHtmlString;

    }

    private bool InsertActivities()
    {
        bool retns = false;
        decimal totAmt = 0;

        refreshLatestActivitySerialsFromDB();
        activityDetailHtmlString = "<br/><b>Activity Allocation Details</b><br>";
        activityDetailHtmlString += "<table border=2 width='860px'><tr><td>Serial</td><td>Month</td><td>Date</td><td>ActivityID</td><td>Amount$</td><td>ActivityDetails</td><tr>";

        Label tmplblautoindex;
        Label tmplblSerial;
        Label tmplblMonth;
        Label tmplblDate;
        Label tmplblActivityTypeID;
        Label tmplblActivityIDFormed;
        TextBox tmptxtAmount;
        TextBox tmptxtActivityDetail;
        string tmpqry = "", suppDoctxt = "Supporting Document", attachedEmailtxt = "Attached Email";

        if (txtAccrualSupportingDocumentText.Text.Trim() != "")
            suppDoctxt = txtAccrualSupportingDocumentText.Text;

        if (txtAttachedEmailtxt.Text.Trim() != "")
            attachedEmailtxt = txtAttachedEmailtxt.Text;

        foreach (GridViewRow rw in GridView1.Rows)
        {
            tmplblautoindex = (Label)rw.FindControl("lblautoindex") as Label;
            tmplblSerial = (Label)rw.FindControl("lblSerial") as Label;
            tmplblMonth = (rw.FindControl("lblMonth") as Label);
            tmplblDate = (rw.FindControl("lblDate") as Label);
            tmplblActivityTypeID = (Label)rw.FindControl("lblActivityTypeID") as Label;
            tmplblActivityIDFormed = (rw.FindControl("lblActivityIDFormed") as Label);
            tmptxtAmount = (rw.FindControl("txtAmount") as TextBox);
            tmptxtActivityDetail = (rw.FindControl("txtActivityDetail") as TextBox);


            if (!Util.isValidDecimalNumber(tmptxtAmount.Text))
            {
                lblMsg.Text = "Please supply a valid numeric value for Amount field in row :" + (rw.RowIndex + 1).ToString();
                MsgBoxControl1.show(lblMsg.Text, "Error !!! ");
                return false;
            }

            if (tmptxtActivityDetail.Text.Trim() == "")
            {
                lblMsg.Text = "Activity Detail field can't be empty , value missing in row :" + (rw.RowIndex + 1).ToString();
                MsgBoxControl1.show(lblMsg.Text, "Error !!! ");
                return false;
            }

            activityDetailHtmlString += "<tr>";
            activityDetailHtmlString += "<td>" + tmplblSerial.Text + "</td>";
            activityDetailHtmlString += "<td>" + tmplblMonth.Text + "</td>";
            activityDetailHtmlString += "<td>" + tmplblDate.Text + "</td>";
            activityDetailHtmlString += "<td>" + tmplblActivityIDFormed.Text + "</td>";
            activityDetailHtmlString += "<td>" + tmptxtAmount.Text + "</td>";
            activityDetailHtmlString += "<td>" + tmptxtActivityDetail.Text + "</td>";
            activityDetailHtmlString += "</tr>";

            totAmt = totAmt + Convert.ToDecimal(tmptxtAmount.Text);

            if (tmplblautoindex.Text == "0") //inserts
            {
                if (tmpqry == "")
                    tmpqry = string.Format("insert into dbo.TblActivities(sno,ActivityCreationMonth,ActivityCreationDate,ActivityCode,EnteredBy,ActivityVendorAmount,AccrualFormNo,ActivityDetail,fk_workFlowPlansId,fk_activityTypeId) "
                    + "values({0},'{1}','{2}','{3}','{4}',{5},'{6}','{7}',{8},{9})", tmplblSerial.Text, tmplblMonth.Text, tmplblDate.Text, tmplblActivityIDFormed.Text, myGlobal.loggedInUser(), tmptxtAmount.Text, lblAccrualFormNo.Text, tmptxtActivityDetail.Text, refId, tmplblActivityTypeID.Text);
                else
                    tmpqry += " ; " + string.Format("insert into dbo.TblActivities(sno,ActivityCreationMonth,ActivityCreationDate,ActivityCode,EnteredBy,ActivityVendorAmount,AccrualFormNo,ActivityDetail,fk_workFlowPlansId,fk_activityTypeId) "
                    + "values({0},'{1}','{2}','{3}','{4}',{5},'{6}','{7}',{8},{9})", tmplblSerial.Text, tmplblMonth.Text, tmplblDate.Text, tmplblActivityIDFormed.Text, myGlobal.loggedInUser(), tmptxtAmount.Text, lblAccrualFormNo.Text, tmptxtActivityDetail.Text, refId, tmplblActivityTypeID.Text);
            }
            else //updates
            {
                if (tmpqry == "")
                    tmpqry = string.Format("update dbo.TblActivities set sno={0},ActivityCreationMonth='{1}',ActivityCreationDate='{2}',ActivityCode='{3}',EnteredBy='{4}',ActivityVendorAmount={5},AccrualFormNo='{6}',ActivityDetail='{7}',fk_workFlowPlansId={8},fk_activityTypeId={9},lastModified='{10}' where autoindex="
                        + tmplblautoindex.Text, tmplblSerial.Text, tmplblMonth.Text, tmplblDate.Text, tmplblActivityIDFormed.Text, myGlobal.loggedInUser(), tmptxtAmount.Text, lblAccrualFormNo.Text, tmptxtActivityDetail.Text, refId, tmplblActivityTypeID.Text,DateTime.Now.ToString());
                else
                    tmpqry += " ; " + string.Format("update dbo.TblActivities set sno={0},ActivityCreationMonth='{1}',ActivityCreationDate='{2}',ActivityCode='{3}',EnteredBy='{4}',ActivityVendorAmount={5},AccrualFormNo='{6}',ActivityDetail='{7}',fk_workFlowPlansId={8},fk_activityTypeId={9},lastModified='{10}' where autoindex="
                        + tmplblautoindex.Text, tmplblSerial.Text, tmplblMonth.Text, tmplblDate.Text, tmplblActivityIDFormed.Text, myGlobal.loggedInUser(), tmptxtAmount.Text, lblAccrualFormNo.Text, tmptxtActivityDetail.Text, refId, tmplblActivityTypeID.Text, DateTime.Now.ToString());
            }
        }

        activityDetailHtmlString += "</table>";

        if (Convert.ToDecimal(txtApprovedAmt.Text) != totAmt)
        {
            lblMsg.Text = "Error Plan total differs ! Activity Allocation total should be same as Vendor Approved Amount, hence can't proceed untill it matches";
            MsgBoxControl1.show(lblMsg.Text, "Error !!! ");
            return false;
        }

        if (tmpqry != "")
        {
            if (Convert.ToInt32(Session["currStatusId"]) == 2)  //inserts accrual record for first time
                tmpqry += " ; " + string.Format("insert into dbo.TblAccrualList(fk_workFlowPlansId,fk_VendorBU,serialBuWise,accrualFormNo,SupportingDocument,AttachedEmailtxt) values({0},{1},{2},'{3}','{4}','{5}')", refId, lblVendorBUId.Text, lblnxtAccrualFormSerial.Text, lblAccrualFormNo.Text, suppDoctxt, attachedEmailtxt);

            if (Convert.ToInt32(Session["currStatusId"]) == 3) //updates accrual record at this stages "Submittion stage"
                tmpqry += " ; " + string.Format("update dbo.TblAccrualList set fk_VendorBU={0},serialBuWise={1},accrualFormNo='{2}',submittedBy='{3}',submittedDate='{4}',SupportingDocument='{5}',AttachedEmailtxt='{6}' where fk_workFlowPlansId=" + refId, lblVendorBUId.Text, lblnxtAccrualFormSerial.Text, lblAccrualFormNo.Text, myGlobal.loggedInUser(), DateTime.Now.ToString(), suppDoctxt, attachedEmailtxt);

            if (Convert.ToInt32(Session["currStatusId"]) == 4) //updates accrual record at this stages "Verifying stage"
                tmpqry += " ; " + string.Format("update dbo.TblAccrualList set fk_VendorBU={0},serialBuWise={1},accrualFormNo='{2}',verifiedBy='{3}',verifiedDate='{4}' where fk_workFlowPlansId=" + refId, lblVendorBUId.Text, lblnxtAccrualFormSerial.Text, lblAccrualFormNo.Text, myGlobal.loggedInUser(), DateTime.Now.ToString());


            Db.constr = myGlobal.getRDDMarketingDBConnectionString();
            Db.myExecuteSQL(tmpqry);
        }

        lbltest.Text = activityDetailHtmlString;  //just to show output testing, close this line of code later

        retns = true; //if process reaches here it means it was successful

        return retns;
    }

    private void updateActivityExecutedStates()
    {
        String tmpqry = "";

        foreach (GridViewRow rw in GridActStatusUpdate.Rows)
        {
            Label lblsno = rw.FindControl("lblsno") as Label;
            DropDownList ddlActExeStatus = rw.FindControl("ddlActExeStatus") as DropDownList;

            if (tmpqry != "")
            {
                tmpqry += " ; " + "update dbo.TblActivities set lastModified='" + DateTime.Now.ToString() + "',ActivityExecutionStatus='" + ddlActExeStatus.SelectedItem.Text + "' where sno=" + lblsno.Text + "";
            }
            else
            {
                tmpqry += "update dbo.TblActivities set lastModified='" + DateTime.Now.ToString() + "',ActivityExecutionStatus='" + ddlActExeStatus.SelectedItem.Text + "' where sno=" + lblsno.Text + "";
            }
        }

        Db.constr = myGlobal.getRDDMarketingDBConnectionString();
        Db.myExecuteSQL(tmpqry);
    }

    public void GetExecutionStatus()
    {
        DataSet ds = new DataSet();
        sql = "select ActivityExecutionStatus from dbo.TblActivities";

        Db.constr = myGlobal.getRDDMarketingDBConnectionString();
        ds = Db.myGetDS(sql);

        string status;
        for (int i = 0; i < ds.Tables[0].Rows.Count; i++)
        {
            status = ds.Tables[0].Rows[i]["ActivityExecutionStatus"].ToString();
            DropDownList ddl = GridActStatusUpdate.Rows[i].FindControl("ddlActExeStatus") as DropDownList;
            for (int j = 0; j < ddl.Items.Count; j++)
            {
                if (status == ddl.Items[j].ToString())
                {
                    ddl.SelectedIndex = j;
                    break;
                }
            }
        }
    }

    protected void btnSubmit_Click(object sender, EventArgs e)
    {
        lblError.Text = "";
        lblMsg.Text = "";

        if (txtupdtComments.Text.Trim() == "")
        {
            lblMsg.Text = "It's Mandatory to supply value for Updation Comments field.  ";
            MsgBoxControl1.show(lblMsg.Text, "Error !!! ");
            return;
        }

        if (txtupdtComments.Text.Trim().IndexOf("'") >= 0)
        {
            lblMsg.Text = "Invalid Character occurs ' in field Comments, Char( ' ) not supported.";
            MsgBoxControl1.show(lblMsg.Text, "Error !!! ");
            return;
        }

        //////////////////////MUF case///////////////////////////

        if (!chkExecutionCompleted_IsValid() && chkExecutionCompleted.Checked)
        {
            lblError.Text = "Invalid Select ! MUF form not submitted/pending for one or more expences of this plan, You can move to IFF Form stage only after submitting MUF form for all your expences. So, you need to first submitt the MUF forms for approval without selecting this option.";
            chkExecutionCompleted.Checked = false;
            MsgBoxControl1.show(lblError.Text, "Error !!! ");
            return;
        }

        if (Convert.ToInt32(Session["currStatusId"]) >= 6 && Convert.ToInt32(Session["currStatusId"]) <= 9)
        {
            if (!getChildCountForActivities())
            {
                lblError.Text = "Error !!! One of the activites shows 'Has Expenses as Yes' where as no expenses have been updated into the system yet for that activity, OR you can set 'Has Expenses as No' to proceed. ";
                MsgBoxControl1.show(lblError.Text, "Error !!! ");
                return;
            }

            if (Convert.ToInt32(Session["currStatusId"]) == 6 && chkExecutionCompleted.Checked == false)
                sql = "select * from dbo.TblActivitiesDetails where fk_ActivitySno in (select sno from dbo.TblActivities where fk_workFlowPlansId=" + refId + ") and mufFormFilledStatus='No'";
            else
                sql = "select * from dbo.TblActivitiesDetails where fk_ActivitySno in (select sno from dbo.TblActivities where fk_workFlowPlansId=" + refId + ") and mufFormFilledStatus!='Yes'";

            Db.constr = myGlobal.getRDDMarketingDBConnectionString();
            drd = Db.myGetReader(sql);

            if (!drd.HasRows)
            {
                drd.Close();

                if (chkExecutionCompleted.Checked == false)
                {
                    lblError.Text = "Warning ! No MUF form could be created as there is no pending activity for the form to be generated, OR you can Check on 'Plan Execution Completed' to ecalate to IFF form submisstion stage ";
                    MsgBoxControl1.show(lblError.Text, "Error !!! ");
                    return;
                }
            }
            else
                drd.Close();
        }


        ///////////////////////////////////////////////////////////
        //if ((Convert.ToInt32(Session["currStatusId"]) == 9 || Convert.ToInt32(Session["currStatusId"]) == 14) && pid == "10031")  //panelStatusUpdate  visible for only 8,13 status
        if ((Convert.ToInt32(Session["currStatusId"]) == 10 || Convert.ToInt32(Session["currStatusId"]) == 14) && pid == "10031")  //panelStatusUpdate  visible for only 8,13 status
        {
            updateActivityExecutedStates();
            if (!GetActivityChildRows())
            {
                lblError.Text = "You Must Enter At Least 1 Detailed Row For Each Activity!!";
                MsgBoxControl1.show(lblError.Text, "Error !!! ");
                return;
            }
        }

        //Check for mandatory file upload case at least one file should nbe uploaded , Accrual and MUF stage

        //if ((Convert.ToInt32(Session["currStatusId"]) == 3 || Convert.ToInt32(Session["currStatusId"]) == 6) && chkExecutionCompleted.Checked == false)
        if ((Convert.ToInt32(Session["currStatusId"]) == 3) && chkExecutionCompleted.Checked == false)
        {
            if (!varifyMandatoryUploadCase())
            {
                lblMsg.Text = "Warning ! It's madatory to upload at least one supporting document while submitting at this stage, upload and retry to submit !!";
                MsgBoxControl1.show(lblMsg.Text, "Error !!! ");
                return;
            }
        }



        ////////////////new addition just in case plan is closed case//////////
        if ((lblStatus.Text.ToUpper()=="Plan Closed".ToUpper()) && pid == "10031")  //panelStatusUpdate  visible for only 14 status
        {
            SqlDataReader drd;
            //sql = "select A.ActivityCode,B.* from TblActivities as A join ( select * from dbo.TblActivitiesDetails where fk_ActivitySno in (select sno from dbo.TblActivities where fk_workFlowPlansId=" + refId.ToString() + ")) as B on B.fk_ActivitySno=A.sno";
            //sql = "select A.ActivityCode,B.* from TblActivities as A join ( select * from dbo.TblActivitiesDetails where mufFormFilledStatus='Yes' and fk_ActivitySno in (select sno from dbo.TblActivities where fk_workFlowPlansId=" + refId.ToString() + ")) as B on B.fk_ActivitySno=A.sno";
            sql = "select A.ActivityCode,B.* from TblActivities as A join ( select * from dbo.TblActivitiesDetails where mufFormFilledStatus='NO' and fk_ActivitySno in (select sno from dbo.TblActivities where fk_workFlowPlansId=" + refId.ToString() + ")) as B on B.fk_ActivitySno=A.sno";
            Db.constr = myGlobal.getRDDMarketingDBConnectionString();
            drd = Db.myGetReader(sql);

            bool flag = true;
            string p201, p301, p501;
            while (drd.Read())
            {
                p201 = drd["ThirdPartyPaidStatus"].ToString();

                if (drd["ThirdpartyInvoiceNo"] != DBNull.Value)
                    p301 = drd["ThirdpartyInvoiceNo"].ToString();
                else
                    p301 = "";

                if (drd["ThirdpartyInvoiceReference"] != DBNull.Value)
                    p501 = drd["ThirdpartyInvoiceReference"].ToString();
                else
                    p501 = "";

                
                if (p201.ToLower() != "paid")
                {
                    flag = false;
                }

                if (!flag)
                {
                    lblError.Text = "Error !Activity '" + drd["ActivityCode"].ToString() + "' , Please verify paid status Field for this activity";
                    MsgBoxControl1.show(lblError.Text, "Error !!! ");
                    return;
                }

                if (p301 == "" ||p501 == "")
                {
                    flag = false;
                }

                if (!flag)
                {
                    lblError.Text = "Error !Activity '" + drd["ActivityCode"].ToString() + "' , Invoice/Reference Field can not be empty";
                    MsgBoxControl1.show(lblError.Text, "Error !!! ");
                    return;
                }
            }
            drd.Close();
        }

        ///////////////////////////


        if ((Convert.ToInt32(Session["currStatusId"]) == 14) && pid == "10031")  //panelStatusUpdate  visible for only 14 status
        {
            SqlDataReader drd;
            //sql = "select A.ActivityCode,B.* from TblActivities as A join ( select * from dbo.TblActivitiesDetails where fk_ActivitySno in (select sno from dbo.TblActivities where fk_workFlowPlansId=" + refId.ToString() + ")) as B on B.fk_ActivitySno=A.sno";
            sql = "select A.ActivityCode,B.* from TblActivities as A join ( select * from dbo.TblActivitiesDetails where mufFormFilledStatus='Yes' and fk_ActivitySno in (select sno from dbo.TblActivities where fk_workFlowPlansId=" + refId.ToString() + ")) as B on B.fk_ActivitySno=A.sno";
            Db.constr = myGlobal.getRDDMarketingDBConnectionString();
            drd = Db.myGetReader(sql);

            bool flag = true;
            string p1, p2, p3, p4,p55;
            while (drd.Read())
            {
                p1 = drd["RddPaidStatus"].ToString();
                p2 = drd["ThirdPartyPaidStatus"].ToString();

                if (drd["ThirdpartyInvoiceNo"] != DBNull.Value)
                    p3 = drd["ThirdpartyInvoiceNo"].ToString();
                else
                    p3 = "";

                if (drd["ThirdpartyInvoiceReference"] != DBNull.Value)
                    p55 = drd["ThirdpartyInvoiceReference"].ToString();
                else
                    p55 = "";

                if (drd["RddInvoiceNo"] != DBNull.Value)
                    p4 = drd["RddInvoiceNo"].ToString();
                else
                    p4 = "";

                if (p1.ToLower() == "not paid" || p2.ToLower() != "paid")
                {
                    flag = false;
                }

                if (!flag)
                {
                    lblError.Text = "Error !Activity '" + drd["ActivityCode"].ToString() + "' , Please verify paid status Field for this activity";
                    MsgBoxControl1.show(lblError.Text, "Error !!! ");
                    return;
                }

                if (p3 == "" || p4 == "" || p55=="")
                {
                    flag = false;
                }

                if (!flag)
                {
                    lblError.Text = "Error !Activity '" + drd["ActivityCode"].ToString() + "' , Invoice/Reference Field can not be empty";
                    MsgBoxControl1.show(lblError.Text, "Error !!! ");
                    return;
                }
            }
            drd.Close();

            sql = "select * from dbo.TblActivities where fk_workFlowPlansId=" + refId;
            Db.constr = myGlobal.getRDDMarketingDBConnectionString();
            drd = Db.myGetReader(sql);

            // bool flag1 = true;
            string p5;
            while (drd.Read())
            {
                p5 = drd["ActivityExecutionStatus"].ToString();
                if (p5.ToLower() != "executed")
                {
                    flag = false;
                }
                if (!flag)
                {
                    lblError.Visible = true;
                    lblError.Text = "Error !Activity '" + drd["ActivityCode"].ToString() + "' , Execution State Should Be Executed";
                    MsgBoxControl1.show(lblError.Text, "Error !!! ");
                    return;
                }
            }
            drd.Close();
        }

        lblMsg.Text = "";
        bool flg = true;

        activityDetailHtmlString = "";

        //this runs in case of specific status

        if (panelActivity.Visible == true)
        {
            if (!InsertActivities())
                return;
        }

        if ((Convert.ToInt32(Session["currStatusId"]) == 10 || Convert.ToInt32(Session["currStatusId"]) == 11) && pid == "10031")
        {
            if (!insertIffData())
                return;
        }

        string suppDoctxt = "Supporting Document", attachedEmailtxt = "Attached Email"; ;

        if (txtAccrualSupportingDocumentText.Text.Trim() != "")
            suppDoctxt = txtAccrualSupportingDocumentText.Text;

        if (txtAttachedEmailtxt.Text.Trim() != "")
            attachedEmailtxt = txtAttachedEmailtxt.Text;

        if (lblAccrualFormView.Visible == true && Convert.ToInt32(Session["currStatusId"]) == 3 && pid == "10031") // if being submitted by user to admin
        {
            sql = string.Format("update dbo.TblAccrualList set submittedBy='{0}',submittedDate='{1}',SupportingDocument='{2}',AttachedEmailtxt='{3}' where  fk_workFlowPlansId=" + refId, myGlobal.loggedInUser(), DateTime.Now.ToString("MM-dd-yyyy"), suppDoctxt, attachedEmailtxt);
            Db.constr = myGlobal.getRDDMarketingDBConnectionString();
            Db.myExecuteSQL(sql);
        }

        if (lblAccrualFormView.Visible == true && Convert.ToInt32(Session["currStatusId"]) == 4 && pid == "10031") //if approval by admin
        {
            sql = string.Format("update dbo.TblAccrualList set verifiedBy='{0}',verifiedDate='{1}',SupportingDocument='{2}',AttachedEmailtxt='{3}' where  fk_workFlowPlansId=" + refId, myGlobal.loggedInUser(), DateTime.Now.ToString("MM-dd-yyyy"), suppDoctxt, attachedEmailtxt);
            Db.constr = myGlobal.getRDDMarketingDBConnectionString();
            Db.myExecuteSQL(sql);
        }

        lblMsg.Text = "";
        CheckBox tmpchk;
        foreach (GridViewRow rw in gridTaskList.Rows)
        {
            tmpchk = (CheckBox)rw.FindControl("chk") as CheckBox;
            if (tmpchk.Checked == false)
            {
                lblMsg.Text = "Invalid Request! Taks (" + (rw.RowIndex + 1).ToString() + ") in the task list is unchecked, request can't be submitted untill all the tasks are acomplished and checked";
                MsgBoxControl1.show(lblMsg.Text, "Error !!! ");
                flg = false;
                break;
            }
        }

        if (flg == false)
            return;

        if (txtupdtComments.Text.Trim() == "")
        {
            lblMsg.Text = "It's Mandatory to supply value for Updation Comments field.  ";
            MsgBoxControl1.show(lblMsg.Text, "Error !!! ");
            return;
        }

        string pt = lblFile.Text.Substring(0, (lblFile.Text.LastIndexOf("/")) + 1);
        SaveFileAtWebsiteLocation(pt);  //this fills fls variable with all the files uploaded, whch can be mailed further

        //if (uploadFiles())
        if (true)
        {
            lblAccrualFormView.Text = "";
            iffDetailHtmlString = "";

            //This code has to be before update now to create a muf form with existing status not the updated one which it will be escalated to
            mufFls = "";
            if (Convert.ToInt32(Session["currStatusId"]) >= 6 && Convert.ToInt32(Session["currStatusId"]) <= 9 && chkExecutionCompleted.Checked == false)
                GenerateMUFForm("Submit"); //generates fresh accrual form

            updateNow();
            updateForMUFSubmissions();

            //if (Convert.ToInt32(Session["currStatusId"]) >=3 )
            //    GenerateAccrualForm(); //generates fresh accrual form

            if ((Convert.ToInt32(Session["currStatusId"]) >= 2) && (Convert.ToInt32(Session["currStatusId"]) <= 5))
                GenerateAccrualForm(); //generates fresh accrual form


            if (Convert.ToInt32(Session["currStatusId"]) >= 10 && Convert.ToInt32(Session["currStatusId"]) <= 11)
                GenerateIffHtml();

            farwardMail(); //open this later

            Response.Redirect("~/Intranet/orders/viewOrdersMKT.aspx?wfTypeId=10031");
        }
        else
        {
            lblMsg.Text = "Error Uploading files please retry or contact administrator";
            MsgBoxControl1.show(lblMsg.Text, "Error !!! ");
        }

    }

    private Boolean getChildCountForActivities()
    {
        int retCnt = 0;
        sql = "select count(*) cnt from ( select A.*,isnull(B.childRowsCount,0) as childRowsCount from dbo.TblActivities as A left outer join ( select fk_ActivitySno as activityId,count(*) as childRowsCount from dbo.TblActivitiesDetails where fk_ActivitySno in (select sno from dbo.TblActivities) group by fk_ActivitySno ) as B on A.sno=B.activityId where fk_workFlowPlansId=" + refId + " ) as yyy where hasExpenses='Yes' and childRowsCount=0";
        Db.constr = myGlobal.getRDDMarketingDBConnectionString();
        retCnt = Db.myExecuteScalar(sql);

        if (retCnt > 0)
            return false;
        else
            return true;
    }

    private void updateNow()
    {

        //////small new update for muf after closed plan/////////////////////////////////////

        //string nxtStsName = "",PlanTableSts="";
        //sql = "select status from dbo.workFlowPlans where autoindex=" + refId;
        //Db.constr = myGlobal.getRDDMarketingDBConnectionString();
        //drd = Db.myGetReader(sql);
        //if (drd.HasRows)
        //{
        //    drd.Read();
        //    PlanTableSts = drd["status"].ToString();
        //    drd.Close();
        //}

        string nxtStsName="";
        ///////////////////////////////////////////

        //sql = "select nextprocessStatusId,nextRole from processStatus where processStatusId=" + Session["currStatusId"].ToString() + " and fk_processId= " + pid;
        sql = "select b.processStatusName as nxtStsName,a.nextprocessStatusId,a.nextRole from processStatus as a left join processStatus as b on a.nextprocessStatusId=b.processStatusId and a.fk_processId=b.fk_processId where a.processStatusId=" + Session["currStatusId"].ToString() + " and a.fk_processId=" + pid;

        Db.constr = myGlobal.getRDDMarketingDBConnectionString();
        drd = Db.myGetReader(sql);
        if (drd.HasRows)
        {
            drd.Read();
            newStatusId = drd["nextprocessStatusId"].ToString();
            newescalateLevelId = drd["nextRole"].ToString();
            nxtStsName = drd["nxtStsName"].ToString();
            drd.Close();
        }

        //completed here 
        if (chkExecutionCompleted.Checked == true)
        {
            newStatusId = toIffStatusId;
            newescalateLevelId = toIffRoleId;
        }

        if (lblPlanTableSts.Text.ToUpper() == "Plan Closed".ToUpper() && Convert.ToInt32(Session["currStatusId"]) == 9)
        {
            newStatusId = "0";
            newescalateLevelId = "9"; //escalate id marketing admin
        }

        //sql = "insert into dbo.processStatusTrack values(" + Session["processRequestId"].ToString() + "," + newStatusId + "," + newescalateLevelId + ",'" + myGlobal.loggedInUser().ToString() + "','done','" + DateTime.Now.ToString("yyyy-MM-dd hh:mm:ss tt") + "','" + txtupdtComments.Text.Trim() + "')";
        sql = "insert into processStatusTrack(fk_processRequestId,action_StatusID,fk_statusId,fk_EscalateLevelId,lastUpdatedBy,StatusAccept,lastModified,comments,fk_processId) values(" + Session["processRequestId"].ToString() + "," + Session["currStatusId"].ToString() + "," + newStatusId + "," + newescalateLevelId + ",'" + myGlobal.loggedInUser().ToString() + "','" + "Processed" + "','" + DateTime.Now.ToString("MM-dd-yyyy hh:mm:ss tt") + "','" + txtupdtComments.Text.Trim() + "'," + pid + ")";

        //sql += "; update dbo.processRequest set fk_StatusId=" + newStatusId + ",fk_escalateLevelId=" + newescalateLevelId + ", comments='" + txtupdtComments.Text.Trim() + "',ByUser='" + myGlobal.loggedInUser() + "', lastModified='" + DateTime.Now.ToString("yyyy-MM-dd hh:mm:ss tt") + "' where refId=" + refId + " and processType='MKT'";
        sql += "; update dbo.processRequest set fk_StatusId=" + newStatusId + ", fk_escalateLevelId=" + newescalateLevelId + ",ByUser='" + myGlobal.loggedInUser().ToString() + "',comments='" + txtupdtComments.Text.Trim() + "',lastModified='" + DateTime.Now.ToString("yyyy-MM-dd hh:mm:ss tt") + "' where refId=" + refId + " and fk_processId=" + pid;

        //new addition to update workflow table status field
        if (lblPlanTableSts.Text.ToUpper() != "Plan Closed".ToUpper())
        sql += "; update dbo.workFlowPlans set status='" + nxtStsName + "' where autoIndex=" + refId;
        
        Db.constr = myGlobal.getRDDMarketingDBConnectionString();
        Db.myExecuteSQL(sql);

        sql = "";

        if (FilesForuploadTrack.Trim() != "")
        {
            string[] sfsl = FilesForuploadTrack.Split(';');
            for (int x = 0; x < sfsl.Length; x++)
            {
                if (sql.Trim() == "")
                    sql = "insert into [dbo].[uploadTrack](fk_refId,fk_StatusId,fk_processId,srNo,websiteFilePath,ByUser) values(" + refId + "," + Session["currStatusId"].ToString() + "," + pid + "," + (x + 1).ToString() + ",'" + sfsl[x] + "','" + myGlobal.loggedInUser().ToString() + "')";
                else
                    sql += " ; " + "insert into [dbo].[uploadTrack](fk_refId,fk_StatusId,fk_processId,srNo,websiteFilePath,ByUser) values(" + refId + "," + Session["currStatusId"].ToString() + "," + pid + "," + (x + 1).ToString() + ",'" + sfsl[x] + "','" + myGlobal.loggedInUser().ToString() + "')";
            }
        }

        if (sql.Trim() != "") //if it is not empty then only run
        {
            Db.constr = myGlobal.getRDDMarketingDBConnectionString();
            Db.myExecuteSQL(sql);
        }
    }

    private void farwardMail()
    {
        sql = "select processStatusName from processStatus where processStatusId=" + newStatusId + " and fk_processId=" + pid;
        Db.constr = myGlobal.getRDDMarketingDBConnectionString();
        drd = Db.myGetReader(sql);

        string nxtStatus;
        nxtStatus = "";

        while (drd.Read())
        {
            nxtStatus = drd[0].ToString();
        }
        drd.Close();


        string strMessage = string.Empty;
        strMessage = " Marketing Plan has been updated , Escalated to next Level ----- <b>" + nxtStatus + "</b><br/>";

        if (mufFls.Trim() != "")
        {
            if (fls.Trim() == "")
                fls = mufFls;
            else
                fls += ";" + mufFls;
        }

        if (fls.Trim() != "")
            strMessage += " Updated files attached to this mail<br/><br/>";

        strMessage += "<br/>Plan Vendor/BU : " + txtVendor.Text;
        strMessage += "<br/>RDD Quater : " + txtQuarter.Text;
        strMessage += "<br/>Vendor Quater : " + txtVendorQuarter.Text;
        strMessage += "<br/>Plan Year : " + txtyear.Text;
        strMessage += "<br/>Stage has been Processed and updated by '" + myGlobal.loggedInUser() + "'";
        strMessage += "<br/>Comments by the user : " + txtupdtComments.Text.Trim();
        strMessage += "<br/><br/>Please follow up the process link:";
        strMessage += "<br/>" + processUrl + "<br/>";

        if (Convert.ToInt32(Session["currStatusId"]) < 4)
            strMessage += activityDetailHtmlString; //adds activity deails if processed otherwise it will be space

        if (Convert.ToInt32(Session["currStatusId"]) >= 6 && Convert.ToInt32(Session["currStatusId"]) <= 9)
            strMessage += mufDetailHtmlString; //adds activity deails if processed otherwise it will be space

        strMessage += lblAccrualFormView.Text;//adds accrual form deails if processed otherwise it will be space
        strMessage += iffDetailHtmlString;//adds iff form deails if processed otherwise it will be space
        string ret, dbMsg;

        dbMsg = myGlobal.getSystemConfigValue("mailSuccessMsg");

        if (newStatusId == "0") //if next role is going to be 0 , means there is no role actually, select email for the role of 1 stauts of the paricular process
            sql = "select emailList" + "TZ" + " from dbo.roles where roleId=(select nextRole from processStatus where processStatusId=" + "1" + " and fk_processId=" + pid + ")";
        else
           
           if(chkExecutionCompleted.Checked == false)
              sql = "select emailList" + "TZ" + " from dbo.roles where roleId=(select nextRole from processStatus where processStatusId=" + Session["currStatusId"].ToString() +" and fk_processId=" + pid + ")";
           else
              sql = "select emailList" + "TZ" + " from dbo.roles where roleId=(select prevRole from processStatus where processStatusId=" + toIffStatusId + " and fk_processId=" + pid + ")";

        if (lblPlanTableSts.Text.ToUpper() == "Plan Closed".ToUpper() && Convert.ToInt32(Session["currStatusId"]) == 9)
            sql = "select emailList" + "TZ" + " from dbo.roles where roleId=9";

        Db.constr = myGlobal.getRDDMarketingDBConnectionString();
        drd = Db.myGetReader(sql);

        string emailadds = "";
        while (drd.Read())
        {
            emailadds = drd[0].ToString();
        }
        drd.Close();

        string tmpUserEmail = myGlobal.membershipUserEmail(myGlobal.loggedInUser());


        ////Close this code line befor live
        //strMessage += "<br/><br/>In Actuall Mails will be sent to roleBasedmailid : " + emailadds + " , CC (user itself) : " + tmpUserEmail;

        //emailadds = "kuldip@eternatec.com";//Overwriting email, just remove these two lines later 
        //tmpUserEmail = "vishav@eternatec.com";//Overwriting email, just remove these two lines later 
        

         //emailadds = "tapinder@reddotdistribution.com";//Overwriting email, just remove these two lines later

        sendIntimationMails();
        ret = Mail.SendMultipleAttach(myGlobal.getSystemConfigValue("websiteEmailer"), emailadds, tmpUserEmail, "Marketing Plan Updation", strMessage, true, "", fls);
        //Message.Show(this, ret);
    }

    private void sendIntimationMails()  //this functions works only when there are items in the list. which gets it's data while creating MUFs and only at stages(9) other wise list is cleared, so just simple call to this function will work
    {
        if (LstIntimation.Items.Count > 0)
        {
            string msgBk = "", midTO, midCC, matter, attachedFl, txt;
            int idx1 = 0, idx2 = 0, idx3 = 0;

            for (int x = 0; x < LstIntimation.Items.Count; x++)
            {
                midTO = "";
                midCC = "";
                attachedFl = "";
                matter = "";

                txt = LstIntimation.Items[x].Text;

                idx1 = txt.IndexOf("[");
                idx2 = txt.IndexOf("]");
                idx3 = txt.IndexOf("<table");

                midTO = txt.Substring(0, idx1);
                midCC = "";
                attachedFl = txt.Substring((idx1 + 1), (idx2 - idx1 - 1));

                if(attachedFl!="")
                attachedFl = Server.MapPath("~" + attachedFl);

                matter = txt.Substring(idx3, txt.Length - idx3);

                matter = "<b>Marketing Plan Update  , MUF Intimation </b><br/><br/>" + matter;
                

                matter = midTO + matter;   //close this later

                ////Close this code line befor live
                //matter += "<br/><br/>In Actuall Mails will be sent to roleBasedmailid : " + midTO + " , CC (user itself) : " + midCC;

                //midTO = "kuldip@eternatec.com"; //Overwriting email, just remove these two lines later 
                //midCC = "vishav@eternatec.com"; //Overwriting email, just remove these two lines later 
               
                try
                {
                    msgBk = Mail.SendMultipleAttach(myGlobal.getSystemConfigValue("websiteEmailer"), midTO, midCC, "MUF Approved", matter, true, "", attachedFl);
                }
                catch (Exception exps)
                {
                    lblMsg.Text = "Error Occured while sending intimation mails for MUf, Please contact system administrator.";
                }
            }
        }
    }

    public Byte[] GetFileContent(System.IO.Stream inputstm)
    {
        Stream fs = inputstm;
        BinaryReader br = new BinaryReader(fs);
        Int32 lnt = Convert.ToInt32(fs.Length);
        byte[] bytes = br.ReadBytes(lnt);
        return bytes;
    }

    private Boolean varifyMandatoryUploadCase()
    {
        Boolean flg = false;
        int cnt = 0;
        HttpFileCollection files = Request.Files;
        HttpPostedFile postFile;

        for (int i = 0; i < files.Count; i++)
        {
            postFile = files[i];
            if (postFile.ContentLength > 0)
            {
                cnt++;
            }
        }

        if (cnt > 0)
            flg = true;

        return flg;
    }

    private void SaveFileAtWebsiteLocation(string saveFileAtWebSitePath)
    {
        String phySavePth;
        HttpPostedFile postFile;

        string ImageName = string.Empty;

        byte[] path;

        string[] keys;

        fls = "";
        FilesForuploadTrack = "";
        try
        {

            string contentType = string.Empty;

            //byte[] imgContent=null;

            string[] PhotoTitle;

            string PhotoTitlename,trimmedNameWithExt;
            int pikMaxFileName = myGlobal.trimFileLength; 

            HttpFileCollection files = Request.Files;

            keys = files.AllKeys;
            string tmpPth;
            int cnt = 0;
            lblMsg.Text = "uploaded files : " + files.Count.ToString();
            for (int i = 0; i < files.Count; i++)
            {
                trimmedNameWithExt = "";
                postFile = files[i];
                if (postFile.ContentLength > 0)
                {
                    contentType = postFile.ContentType;
                    path = GetFileContent(postFile.InputStream);
                    ImageName = System.IO.Path.GetFileName(postFile.FileName);
                    PhotoTitle = ImageName.Split('.');
                    PhotoTitlename = PhotoTitle[0];

                    if (PhotoTitlename.Length > pikMaxFileName)
                        PhotoTitlename = PhotoTitlename.Substring(0, pikMaxFileName);

                    trimmedNameWithExt = PhotoTitlename + "." + PhotoTitle[1];
                    
                    cnt++;

                    tmpPth = "";
                    tmpPth = Session["processAbbr"].ToString() + "-" + refId + "-" + lblStatus.Text + "-" + myGlobal.loggedInUser() + "-" + DateTime.Now.ToString("dd-MM-yyyy-hh-mm-ss") + "-FL" + cnt.ToString() + "-";
                    //tmpPth = myGlobal.loggedInUser() + "-" + DateTime.Now.ToString("dd-MM-yyyy-hh-mm-ss") + "-FL" + cnt.ToString() + "-";

                    phySavePth = Server.MapPath("~" + saveFileAtWebSitePath + tmpPth) + trimmedNameWithExt;
                    postFile.SaveAs(phySavePth);

                    if (fls.Trim() == "")
                        fls = phySavePth;
                    else
                        fls += ";" + phySavePth;

                    if (FilesForuploadTrack.Trim() == "")
                        FilesForuploadTrack = saveFileAtWebSitePath + tmpPth + trimmedNameWithExt;
                    else
                        FilesForuploadTrack += ";" + saveFileAtWebSitePath + tmpPth + trimmedNameWithExt;
                }
            }

            if (GridFiles.Rows.Count > 0)
            {
                CheckBox chk;
                HyperLink lnk;

                foreach (GridViewRow rw in GridFiles.Rows)
                {
                    chk = rw.FindControl("chklstAttachedFiles") as CheckBox;
                    lnk = rw.FindControl("lnkFileLoc") as HyperLink;

                    string tmpstr;
                    int strt, ends;
                    tmpstr = lnk.NavigateUrl;
                    strt = tmpstr.LastIndexOf("~") + 1;
                    ends = tmpstr.Length - strt;
                    lnk.NavigateUrl = tmpstr.Substring(strt, ends);

                    if (chk.Checked == true)
                    {
                        if (fls.Trim() == "")
                            fls = Server.MapPath("~" + lnk.NavigateUrl);
                        else
                            fls += ";" + Server.MapPath("~" + lnk.NavigateUrl);
                    }
                }
            }

        }
        catch (Exception ex)
        {
            ex.Message.ToString();
        }
    }

    private void GenerateAccrualForm()
    {
        string acts, amts, accrualfrmno = "", submitusr = "", verifyuser = "";
        DateTime submitDt, verifiedDt;

        submitDt = verifiedDt = DateTime.Now;

        acts = amts = "";

        sql = "select a.*,b.submittedBy,b.submittedDate,b.verifiedBy,b.verifiedDate,b.SupportingDocument,b.AttachedEmailtxt from dbo.TblActivities as a join TblAccrualList as b on a.accrualFormNo=b.accrualFormNo where a.fk_workFlowPlansId=" + refId;
        Db.constr = myGlobal.getRDDMarketingDBConnectionString();
        dr = Db.myGetReader(sql);
        if (dr.HasRows)
            while (dr.Read())
            {
                if (acts == "")
                    acts = dr["ActivityCode"].ToString();
                else
                    acts += ", " + dr["ActivityCode"].ToString();

                if (amts == "")
                    amts = dr["ActivityVendorAmount"].ToString();
                else
                    amts += ", " + dr["ActivityVendorAmount"].ToString();

                accrualfrmno = dr["AccrualFormNo"].ToString();
                //submitDt = Convert.ToDateTime(dr["ActivityCreationDate"]);

                if (dr["submittedBy"] != DBNull.Value)
                    submitusr = dr["submittedBy"].ToString();

                if (dr["submittedDate"] != DBNull.Value)
                    submitDt = Convert.ToDateTime(dr["submittedDate"].ToString());

                if (dr["verifiedBy"] != DBNull.Value)
                    verifyuser = dr["verifiedBy"].ToString();

                if (dr["verifiedDate"] != DBNull.Value)
                    verifiedDt = Convert.ToDateTime(dr["verifiedDate"].ToString());

                if (dr["SupportingDocument"] != DBNull.Value)
                    txtAccrualSupportingDocumentText.Text = dr["SupportingDocument"].ToString();

                if (dr["AttachedEmailtxt"] != DBNull.Value)
                    txtAttachedEmailtxt.Text = dr["AttachedEmailtxt"].ToString();

            }

        dr.Close();

        lblAccrualFormView.Text = "<br><table style='border-color:Black;width:65%' border=2><tr>";
        //lblAccrualDocumentation.Text = "<br><table style='border-color:Black;width:700px' border=2><tr>";

        //Header Row
        lblAccrualFormView.Text += "<td style='font-weight:bold' align='center' colspan='6'>MDF Accural Form (AF)";
        lblAccrualFormView.Text += "</td><tr>";

        //row 1
        lblAccrualFormView.Text += "<tr>";

        lblAccrualFormView.Text += "<td style='width:30px'>";
        lblAccrualFormView.Text += "BU";
        lblAccrualFormView.Text += "</td>";

        lblAccrualFormView.Text += "<td style='width:70px;color:Blue'>";
        lblAccrualFormView.Text += txtVendor.Text;
        lblAccrualFormView.Text += "</td>";

        lblAccrualFormView.Text += "<td style='width:30px'>";
        lblAccrualFormView.Text += "</td>";

        lblAccrualFormView.Text += "<td style='width:30px'>";
        lblAccrualFormView.Text += "</td>";

        lblAccrualFormView.Text += "<td style='width:30px'>";
        lblAccrualFormView.Text += "AF No.";
        lblAccrualFormView.Text += "</td>";

        lblAccrualFormView.Text += "<td style='width:70px;color:Blue'>";
        lblAccrualFormView.Text += accrualfrmno;
        lblAccrualFormView.Text += "</td></tr>";

        //row 2

        lblAccrualFormView.Text += "<tr>";

        lblAccrualFormView.Text += "<td style='width:30px'>";
        lblAccrualFormView.Text += "RDD Qtr";
        lblAccrualFormView.Text += "</td>";

        lblAccrualFormView.Text += "<td style='width:70px;color:Blue'>";
        lblAccrualFormView.Text += txtQuarter.Text;
        lblAccrualFormView.Text += "</td>";

        lblAccrualFormView.Text += "<td style='width:30px'>";
        lblAccrualFormView.Text += "</td>";

        lblAccrualFormView.Text += "<td style='width:30px'>";
        lblAccrualFormView.Text += "</td>";

        lblAccrualFormView.Text += "<td style='width:30px'>";
        lblAccrualFormView.Text += "Vendor Qtr";
        lblAccrualFormView.Text += "</td>";

        lblAccrualFormView.Text += "<td style='width:70px;color:Blue'>";
        lblAccrualFormView.Text += txtVendorQuarter.Text;
        lblAccrualFormView.Text += "</td></tr>";

        //row 3

        lblAccrualFormView.Text += "<tr>";

        lblAccrualFormView.Text += "<td colspan='2'>";
        lblAccrualFormView.Text += "Amount To be Accrued";
        lblAccrualFormView.Text += "</td>";

        lblAccrualFormView.Text += "<td style='width:30px'>";
        lblAccrualFormView.Text += "</td>";

        lblAccrualFormView.Text += "<td style='color:Blue' colspan='3'>";
        lblAccrualFormView.Text += amts;
        lblAccrualFormView.Text += "</td></tr>";

        //row 4

        lblAccrualFormView.Text += "<tr>";

        lblAccrualFormView.Text += "<td colspan='2'>";
        lblAccrualFormView.Text += "Activity No./Details";
        lblAccrualFormView.Text += "</td>";

        lblAccrualFormView.Text += "<td style='width:30px'>";
        lblAccrualFormView.Text += "</td>";

        lblAccrualFormView.Text += "<td style='color:Blue' colspan='3'>";
        lblAccrualFormView.Text += acts;
        lblAccrualFormView.Text += "</td></tr>";

        //row 5
        lblAccrualFormView.Text += "<tr>";
        lblAccrualFormView.Text += "<td style='font-weight:bold' align='center' colspan='6'>";
        lblAccrualFormView.Text += txtAccrualSupportingDocumentText.Text;
        lblAccrualFormView.Text += "</td>";
        lblAccrualFormView.Text += "</td></tr>";

        //row 6
        lblAccrualFormView.Text += "<tr style='Height:50px'>";
        lblAccrualFormView.Text += "<td style='font-weight:bold' align='center' colspan='6' valign='middle'>";
        lblAccrualFormView.Text += txtAttachedEmailtxt.Text;// "MDF Confrimation email from Nandan Dated 10Jan. Subject MDF for Reddot";
        lblAccrualFormView.Text += "</td>";
        lblAccrualFormView.Text += "</td></tr>";

        //row 7

        lblAccrualFormView.Text += "<tr>";

        lblAccrualFormView.Text += "<td style='width:30px'>";
        lblAccrualFormView.Text += "Submitted By";
        lblAccrualFormView.Text += "</td>";

        lblAccrualFormView.Text += "<td style='width:70px;color:Blue'>";

        if (submitusr == "" && Convert.ToInt32(Session["currStatusId"]) == 3 && pid == "10031")
            lblAccrualFormView.Text += myGlobal.loggedInUser();
        else
            lblAccrualFormView.Text += submitusr;

        lblAccrualFormView.Text += "</td>";

        lblAccrualFormView.Text += "<td style='width:30px'>";
        lblAccrualFormView.Text += "</td>";

        lblAccrualFormView.Text += "<td style='width:30px'>";
        lblAccrualFormView.Text += "</td>";

        lblAccrualFormView.Text += "<td style='width:30px'>";
        lblAccrualFormView.Text += "Verified By";
        lblAccrualFormView.Text += "</td>";

        lblAccrualFormView.Text += "<td style='width:70px;color:Blue'>";

        if (verifyuser == "" && Convert.ToInt32(Session["currStatusId"]) == 4 && pid == "10031")
            lblAccrualFormView.Text += myGlobal.loggedInUser();
        else
            lblAccrualFormView.Text += verifyuser;

        lblAccrualFormView.Text += "</td></tr>";

        //row 8

        lblAccrualFormView.Text += "<tr>";

        lblAccrualFormView.Text += "<td style='width:30px'>";
        lblAccrualFormView.Text += "Date";
        lblAccrualFormView.Text += "</td>";

        lblAccrualFormView.Text += "<td style='width:70px;color:Blue'>";
        lblAccrualFormView.Text += submitDt.ToString("dd-MMM-yy");
        lblAccrualFormView.Text += "</td>";

        lblAccrualFormView.Text += "<td style='width:30px'>";
        lblAccrualFormView.Text += "</td>";

        lblAccrualFormView.Text += "<td style='width:30px'>";
        lblAccrualFormView.Text += "</td>";

        lblAccrualFormView.Text += "<td style='width:30px'>";
        lblAccrualFormView.Text += "Date";
        lblAccrualFormView.Text += "</td>";

        lblAccrualFormView.Text += "<td style='width:70px;color:Blue'>";
        lblAccrualFormView.Text += verifiedDt.ToString("dd-MMM-yy");
        lblAccrualFormView.Text += "</td></tr>";

        lblAccrualFormView.Text += "</table>";
    }

    private void setExecute()
    {
        Label tmplblExeStatus, tmplblhasExpenses, tmplblchildRowsCount, tmplblFundsAvailable, tmplblActivityVendorAmount;
        DropDownList tmpddlExeStatus, tmpddlhasExpenses;
        CheckBox chkView;
        Button tmpbtnAddField;

        foreach (GridViewRow rw in GridActStatusUpdate.Rows)
        {
            tmplblExeStatus = rw.FindControl("lblActExeStatus") as Label;
            tmpddlExeStatus = rw.FindControl("ddlActExeStatus") as DropDownList;

            tmplblFundsAvailable = rw.FindControl("lblFundsAvailable") as Label;
            tmpbtnAddField = rw.FindControl("btnAddField") as Button;
            tmplblActivityVendorAmount = rw.FindControl("lblActivityVendorAmount") as Label;

            if (Convert.ToDouble(tmplblFundsAvailable.Text) <= 0)  //means there is no funds avaialble for further expenses close the add item button
            {
                //tmpbtnAddField.Text = "No Funds";
                tmpbtnAddField.Visible = false;
            }
            else
            {
                //tmpbtnAddField.Text = "Add Expense";
                tmpbtnAddField.Visible = true;
            }

            if (tmplblExeStatus.Text != "")
                tmpddlExeStatus.Items.FindByText(tmplblExeStatus.Text).Selected = true;

            tmplblhasExpenses = rw.FindControl("lblhasExpenses") as Label;
            tmpddlhasExpenses = rw.FindControl("ddlhasExpenses") as DropDownList;
            if (tmplblhasExpenses.Text != "")
                tmpddlhasExpenses.Items.FindByText(tmplblhasExpenses.Text).Selected = true;


            chkView = rw.FindControl("chkView") as CheckBox;
            if (tmplblhasExpenses.Text.ToUpper() == "NO")
            {
                chkView.Checked = false;
                chkView.Enabled = false;
            }
            else
            {
                chkView.Checked = false;
                chkView.Enabled = true;
            }

            tmplblchildRowsCount = rw.FindControl("lblchildRowsCount") as Label;

            if (tmplblchildRowsCount.Text == "0")
            {
                if (Convert.ToInt32(Session["currStatusId"]) == 14)
                    tmpddlhasExpenses.Enabled = false;
                else
                    tmpddlhasExpenses.Enabled = true;
            }
            else
                tmpddlhasExpenses.Enabled = false;

            decimal fundconsumed=Convert.ToDecimal(tmplblActivityVendorAmount.Text)- Convert.ToDecimal(tmplblFundsAvailable.Text);

            if (fundconsumed > (Convert.ToDecimal(tmplblActivityVendorAmount.Text) / 2))
                tmplblFundsAvailable.ForeColor = System.Drawing.Color.Red;
            else
                tmplblFundsAvailable.ForeColor = System.Drawing.Color.Green;

        }
    }

    protected void BindGrid()
    {
        DataTable dts;
        //sql = "select X.*,Y.BalanceFunds as FundsAvailable,Y.vat from (select A.*,isnull(B.childRowsCount,0) as childRowsCount from dbo.TblActivities as A left outer join (select fk_ActivitySno as activityId,count(*) as childRowsCount from dbo.TblActivitiesDetails where fk_ActivitySno in (select sno from dbo.TblActivities) group by fk_ActivitySno) as B on A.sno=B.activityId where fk_workFlowPlansId=" + refId + ") as X left join ( select sno,ActivityVendorAmount,Costed,vat,(T.ActivityVendorAmount-isnull(T.Costed,0)-isnull(vat,0)) as BalanceFunds from ( select A.sno,max(A.ActivityVendorAmount) ActivityVendorAmount,sum(D.Costed) costed,sum(vat) vat from dbo.TblActivities A  left join dbo.TblActivitiesDetails D on A.sno=D.fk_ActivitySno where fk_workFlowPlansId=" + refId + " group by A.sno ) as T ) as Y on X.sno=Y.sno";
        sql = "select X.*,Y.BalanceFunds as FundsAvailable,Y.vat from (select A.*,isnull(B.childRowsCount,0) as childRowsCount from dbo.TblActivities as A left outer join (select fk_ActivitySno as activityId,count(*) as childRowsCount from dbo.TblActivitiesDetails where fk_ActivitySno in (select sno from dbo.TblActivities) group by fk_ActivitySno) as B on A.sno=B.activityId where fk_workFlowPlansId=" + refId + ") as X left join ( select sno,ActivityVendorAmount,Costed,vat,(T.ActivityVendorAmount-isnull(T.Costed,0)) as BalanceFunds from ( select A.sno,max(A.ActivityVendorAmount) ActivityVendorAmount,sum(D.Costed) costed,sum(vat) vat from dbo.TblActivities A  left join dbo.TblActivitiesDetails D on A.sno=D.fk_ActivitySno where fk_workFlowPlansId=" + refId + " group by A.sno ) as T ) as Y on X.sno=Y.sno";
        Db.constr = myGlobal.getRDDMarketingDBConnectionString();
        dts = Db.myGetDS(sql).Tables[0];
        GridActStatusUpdate.DataSource = dts;
        GridActStatusUpdate.DataBind();
        setExecute();
    }

    private void setvisibleEditDeleteGrid2()
    {
        LinkButton lnk, lnk1;
        Label lblmufstatus, lblmufLable,lblBy,lblDt;

        foreach (GridViewRow rw in Grid2.Rows)
        {
            lblmufstatus = (Label)rw.FindControl("lblMUFStatus");
            if (lblmufstatus != null)
            {
                    lnk = (LinkButton)rw.FindControl("LinkEdit");
                    lnk1 = (LinkButton)rw.FindControl("LinkDelete");
                    lblmufLable = (Label)rw.FindControl("lblMUFLable");

                    ////////////////////////////////////////////////////////
                    lblBy = (Label)rw.FindControl("lblmufsubmittedBy");
                    lblDt = (Label)rw.FindControl("lblmufsubmittedDate");
                    if (lblBy.Text == "" && lblBy != null)
                      if (lblDt != null)
                        lblDt.Text = "";

                    lblBy = (Label)rw.FindControl("lblmufverfiedBy");
                    lblDt = (Label)rw.FindControl("lblmufverfiedDate");
                    if (lblBy.Text == "" && lblBy != null)
                        if (lblDt != null)
                            lblDt.Text = "";

                    lblBy = (Label)rw.FindControl("lblmufaccruedBy");
                    lblDt = (Label)rw.FindControl("lblmufaccruedDate");
                    if (lblBy.Text == "" && lblBy != null)
                        if (lblDt != null)
                            lblDt.Text = "";

                    lblBy = (Label)rw.FindControl("lblmufAuthorisedBy");
                    lblDt = (Label)rw.FindControl("lblmufAuthorisedDate");
                    if (lblBy.Text == "" && lblBy != null)
                        if (lblDt != null)
                            lblDt.Text = "";
            ////////////////////////////////////////////////////////////////

            
                if (lblmufstatus.Text.ToUpper() == "YES")
                {
                    if ((Convert.ToInt32(Session["currStatusId"]) != 14)) //in updation case all are open
                    {
                        lnk.Visible = false;
                     
                    }
                    lnk1.Visible = false;
                    lblmufLable.Visible = true;
                }
                else
                {
                    lnk.Visible = true;
                    lnk1.Visible = true;
                    lblmufLable.Visible = false;
                }
            }
        }
    }
    
    private void setVisibleToUpdatePanelOfChildGridTemplateCase()
    {
        //visble true for updateable panles of a child grid
        if ((Convert.ToInt32(Session["currStatusId"]) == 14) || lblStatus.Text.ToUpper() == "Plan Closed".ToUpper())
        {
            Panel pnlTmp;
            foreach (GridViewRow rw in Grid2.Rows)
            {
                pnlTmp = rw.FindControl("pnlItemTemplateChild") as Panel;
                pnlTmp.Visible = true;
            }
        }

        setvisibleEditDeleteGrid2();
    }

    private void setVisibleToUpdatePanelOfChildGridEditTemplateCase(Panel pnlEdt)
    {
        //visble true for updateable panles of a child grid
        if ((Convert.ToInt32(Session["currStatusId"]) == 14) || lblStatus.Text.ToUpper() == "Plan Closed".ToUpper())
        {
            pnlEdt.Visible = true;
        }

        setvisibleEditDeleteGrid2();

    }

    public void BindGrid2(string psno)
    {
        //DataTable dts = new DataTable();
        sql = "select * from TblActivitiesDetails where fk_ActivitySno=" + psno + " order by autoindex_MUFNO desc";
        Db.constr = myGlobal.getRDDMarketingDBConnectionString();
        //dts = Db.myGetDS(sql).Tables[0];
        Session["TblGrid"] = Db.myGetDS(sql).Tables[0];

        Grid2.DataSource = null;
        Grid2.DataSource = (DataTable)Session["TblGrid"];
        Grid2.DataBind();

        setVisibleToUpdatePanelOfChildGridTemplateCase();
    }

    public void BindGrid2old(string psno)
    {
        if (Grid2.Rows.Count > 0)
        {
            TextBox uniqueID = Grid2.Rows[0].FindControl("txtUniqueID") as TextBox;
            Label uniqueID1 = Grid2.Rows[0].FindControl("lblUniqueID") as Label;

            string uid = "";

            if (uniqueID != null)
                uid = uniqueID.Text;
            else
                uid = uniqueID1.Text;

            if (uid == "")
                uid = "0";

            //DataTable dts = new DataTable();
            sql = "select * from TblActivitiesDetails where fk_ActivitySno=" + uid + " order by autoindex_MUFNO desc";
            Db.constr = myGlobal.getRDDMarketingDBConnectionString();
            //dts = Db.myGetDS(sql).Tables[0];
            Session["TblGrid"] = Db.myGetDS(sql).Tables[0];

            Grid2.DataSource = null;
            Grid2.DataSource = (DataTable)Session["TblGrid"];
            Grid2.DataBind();

            setVisibleToUpdatePanelOfChildGridTemplateCase();
        }
    }

    public void BindGrid3()
    {
        //foreach (GridViewRow rw in Grid2.Rows)
        //{
        //        DataTable tbl = (DataTable)Session["TblGrid"];
        //        Grid2.DataSource = null;
        //        Grid2.DataSource = tbl;
        //        Grid2.DataBind();
        //}

        ////setVisibleToUpdatePanelOfChildGridTemplateCase();
    }

    private void setlblFundBalanceAvailableForSelectedActivity(string actId)
    {
        Label lblActivityCode;
        Label tmplblFundsAvailable;

        foreach (GridViewRow row in GridActStatusUpdate.Rows)
        {
            lblActivityCode = row.FindControl("lblActivityCode") as Label;
            if (lblSelectedActivity.Text == lblActivityCode.Text)
            {
                tmplblFundsAvailable = row.FindControl("lblFundsAvailable") as Label;
                lblFundBalanceAvailableForSelectedActivity.Text = tmplblFundsAvailable.Text;
                break;
            }

        }
    }

    protected void chkView_CheckedChanged(object sender, EventArgs e)
    {
        Grid2.EditIndex = -1;

        lblError.Text = "";
        Grid2.Visible = true;
        lblGridMsg.Visible = true;
        CheckBox ctrl = sender as CheckBox;
        GridViewRow row = (GridViewRow)((DataControlFieldCell)((CheckBox)sender).Parent).Parent;

        Button btn;
        CheckBox chk;
        DropDownList ddlTmp;

        if (ctrl != null)
        {
            foreach (GridViewRow rws in GridActStatusUpdate.Rows)
            {
                btn = rws.FindControl("btnAddField") as Button;
                chk = rws.FindControl("chkView") as CheckBox;

                chk.AutoPostBack = false;
                chk.Checked = false;

                if (rws.RowIndex != row.RowIndex)
                    btn.Enabled = false;
                else
                    btn.Enabled = true;
            }

            btn = row.FindControl("btnAddField") as Button;
            chk = row.FindControl("chkView") as CheckBox;

            if (chk.ClientID == ctrl.ClientID)
            {
                Label lblActivityCode = row.FindControl("lblActivityCode") as Label;
                Label tmplblFundsAvailable = row.FindControl("lblFundsAvailable") as Label;
                ddlTmp = row.FindControl("ddlhasExpenses") as DropDownList;

                Label tmpLbl = row.FindControl("lblsno") as Label;

                DataTable dts;
                sql = "select * from TblActivitiesDetails where fk_ActivitySno=" + tmpLbl.Text + " order by autoindex_MUFNO desc";
                Session["lblsno"] = tmpLbl.Text; //line added on 12-Apr-2013

                Db.constr = myGlobal.getRDDMarketingDBConnectionString();
                dts = Db.myGetDS(sql).Tables[0];
                Session["TblGrid"] = Db.myGetDS(sql).Tables[0];
                Grid2.DataSource = dts;
                Grid2.DataBind();

                if (ddlTmp.Text.ToUpper() == "NO")
                    btn.Enabled = false;

                lblSelectedActivity.Text = lblActivityCode.Text;
                lblFundBalanceAvailableForSelectedActivity.Text = tmplblFundsAvailable.Text;

                if (Grid2.Rows.Count < 1)
                    lblSelectedActivity.Text += " ( No rows are available for this Activity, Please click on Add Row Button above, to create new activity details)";
            }
        }

        setVisibleToUpdatePanelOfChildGridTemplateCase();

        //if ((Convert.ToInt32(Session["currStatusId"]) == 14 || Request.QueryString["action"] == "view") && pid == "10031")
        if ((Convert.ToInt32(Session["currStatusId"]) == 14 || action.ToUpper() == "VIEW") && pid == "10031")
        {
            //Grid2.Columns[2].Visible = false;
            foreach (GridViewRow rw in GridActStatusUpdate.Rows)
            {
                btn = rw.FindControl("btnAddField") as Button;
                btn.Enabled = false;
            }
        }

        foreach (GridViewRow rws in GridActStatusUpdate.Rows)
        {
            chk = rws.FindControl("chkView") as CheckBox;
            chk.AutoPostBack = true;
        }
    }

    private void setChildGridDropDownValues()  //not used yet
    {
        Label tmplblType, tmplblLoc, tmplblDesc;
        DropDownList tmpddlType, tmpddlLoc, tmpddlDesc;

        foreach (GridViewRow rw in Grid2.Rows)
        {
            tmplblType = rw.FindControl("lblTypeOfActivity") as Label;
            tmpddlType = rw.FindControl("ddlTypeOfActivity") as DropDownList;

            tmplblLoc = rw.FindControl("lblTypeOfLocation") as Label;
            tmpddlLoc = rw.FindControl("ddlTypeOfLocation") as DropDownList;

            tmplblDesc = rw.FindControl("lblActivityDescrition") as Label;
            tmpddlDesc = rw.FindControl("ddlActivityDescription") as DropDownList;

            if (tmplblType.Text != "")
                tmpddlType.Items.FindByText(tmplblType.Text).Selected = true;

            if (tmplblLoc.Text != "")
                tmpddlLoc.Items.FindByText(tmplblLoc.Text).Selected = true;

            if (tmplblDesc.Text != "")
                tmpddlDesc.Items.FindByText(tmplblDesc.Text).Selected = true;
        }
    }

    protected void btnAddField_Click(object sender, EventArgs e)
    {
        Button btn = sender as Button;
        foreach (GridViewRow row in GridActStatusUpdate.Rows)
        {
            Button ctrl = row.FindControl("btnAddField") as Button;
            if (ctrl != null)
            {
                if (btn.ClientID == ctrl.ClientID)
                {
                    Label lblActivityCode = row.FindControl("lblActivityCode") as Label;
                    lblSelectedActivity.Text = lblActivityCode.Text;
                    Label lblsno = row.FindControl("lblsno") as Label;
                    Session["lblsno"] = lblsno.Text;

                    DataTable tbl = (DataTable)Session["TblGrid"];
                    DataRow drw = tbl.NewRow();



                    //select top 1 autoIndex_MUFNO,fk_ActivitySno,RddInvoiceNo,RddPaidStatus,ReceivedInRDDQTR,RDDPaymentDetails from TblActivitiesDetails where fk_ActivitySno=122 and RddInvoiceNo IS NOT NULL and ReceivedInRDDQTR IS NOT NULL and RDDPaymentDetails IS NOT NULL and upper(RddInvoiceNo)<>'NULL' and upper(ReceivedInRDDQTR)<>'NULL' and upper(RDDPaymentDetails)<>'NULL' and upper(rddPaidStatus)='PAID' 

                    //tbl.Rows.Add(drw);
                    tbl.Rows.InsertAt(drw, 0);

                    Session["TblGrid"] = tbl;
                    Grid2.DataSource = (DataTable)Session["TblGrid"];
                    Grid2.DataBind();
                    Grid2.Rows[0].BackColor = System.Drawing.Color.PaleGreen;
                    setVisibleToUpdatePanelOfChildGridTemplateCase();

                }
            }
        }
    }

    protected void Grid2_RowEditing(object sender, GridViewEditEventArgs e)
    {

        Label lblAutoIndex_MufNo = (Label)Grid2.Rows[e.NewEditIndex].FindControl("lblAutoIndex_MufNo") as Label;
        Label lblLastModified = (Label)Grid2.Rows[e.NewEditIndex].FindControl("lblLastModified") as Label;
        Label lblAcDateOfExecution = (Label)Grid2.Rows[e.NewEditIndex].FindControl("lblAcDateOfExecution") as Label;
        Label lblCosted = (Label)Grid2.Rows[e.NewEditIndex].FindControl("lblCosted") as Label;
        Label lblVat = (Label)Grid2.Rows[e.NewEditIndex].FindControl("lblVat") as Label;
        Label lblExpDetail = (Label)Grid2.Rows[e.NewEditIndex].FindControl("lblExpDetail") as Label;
        Label lblThirdPartyName = (Label)Grid2.Rows[e.NewEditIndex].FindControl("lblThirdPartyName") as Label;
        Label lblThirdPartyInvoiceReference = (Label)Grid2.Rows[e.NewEditIndex].FindControl("lblThirdPartyInvoiceReference") as Label;
        Label lblThirdPartyInvoiceNo = (Label)Grid2.Rows[e.NewEditIndex].FindControl("lblThirdPartyInvoiceNo") as Label;
        Label lblThirdPartyPaidStatus = (Label)Grid2.Rows[e.NewEditIndex].FindControl("lblThirdPartyPaidStatus") as Label;
        Label lblRddInvoiceNo = (Label)Grid2.Rows[e.NewEditIndex].FindControl("lblRddInvoiceNo") as Label;
        Label lblRddPaidStatus = (Label)Grid2.Rows[e.NewEditIndex].FindControl("lblRddPaidStatus") as Label;
        Label lblTotalPaymentRecieved = (Label)Grid2.Rows[e.NewEditIndex].FindControl("lblTotalPaymentRecieved") as Label;
        //Label lblPending = (Label)Grid2.Rows[e.NewEditIndex].FindControl("lblPending") as Label;
        Label lblRecievedInRDDQtr = (Label)Grid2.Rows[e.NewEditIndex].FindControl("lblRecievedInRDDQtr") as Label;
        Label lblRddPaymentDetails = (Label)Grid2.Rows[e.NewEditIndex].FindControl("lblRddPaymentDetails") as Label;

        Label lblTypeOfActivity = (Label)Grid2.Rows[e.NewEditIndex].FindControl("lblTypeOfActivity") as Label;
        Label lblLocationOfActivity = (Label)Grid2.Rows[e.NewEditIndex].FindControl("lblLocationOfActivity") as Label;
        Label lblExpensedAt = (Label)Grid2.Rows[e.NewEditIndex].FindControl("lblExpensedAt") as Label;
        Label lblActivityDescription = (Label)Grid2.Rows[e.NewEditIndex].FindControl("lblActivityDescription") as Label;

        Label lblmuffilePath = (Label)Grid2.Rows[e.NewEditIndex].FindControl("lblmuffilePath") as Label;

        if (lblAutoIndex_MufNo.Text == "")
        {
            Session["insert"] = "insert";
        }
        else
        {
            Session["insert"] = "";
        }


        Grid2.EditIndex = e.NewEditIndex;

        //BindGrid3();
        Grid2.DataSource = (DataTable)Session["TblGrid"];
        Grid2.DataBind();

        GridViewRow myRow = Grid2.Rows[e.NewEditIndex];

        Grid2.Rows[e.NewEditIndex].BackColor = System.Drawing.Color.PaleGreen;

        TextBox txtAutoIndex_MufNo = (TextBox)myRow.FindControl("txtAutoIndex_MufNo") as TextBox;
        Label lbllastModifiedEdt = (Label)Grid2.Rows[e.NewEditIndex].FindControl("lbllastModifiedEdt") as Label;
        TextBox txtAcDateOfExecution = (TextBox)myRow.FindControl("txtAcDateOfExecution") as TextBox;
        TextBox txtCosted = (TextBox)myRow.FindControl("txtCosted") as TextBox;
        Label lblCostedPrev = (myRow.FindControl("lblCostedPrev") as Label);

        TextBox txtVat = (TextBox)myRow.FindControl("txtVat") as TextBox;
        Label lblVatPrev = (myRow.FindControl("lblVatPrev") as Label);

        TextBox txtExpDetail = (TextBox)myRow.FindControl("txtExpDetail") as TextBox;
        TextBox txtThirdPartyName = (TextBox)myRow.FindControl("txtThirdPartyName") as TextBox;
        TextBox txtThirdPartyInvoiceReference = (TextBox)myRow.FindControl("txtThirdPartyInvoiceReference") as TextBox;
        TextBox txtThirdPartyInvoiceNo = (TextBox)myRow.FindControl("txtThirdPartyInvoiceNo") as TextBox;
        DropDownList ddlThirdPartyPaidStatus = (DropDownList)myRow.FindControl("ddlThirdPartyPaidStatus") as DropDownList;
        TextBox txtRddInvoiceNo = (TextBox)myRow.FindControl("txtRddInvoiceNo") as TextBox;
        DropDownList ddlRddPaidStatus = (DropDownList)myRow.FindControl("ddlRddPaidStatus") as DropDownList;
        TextBox txtTotalPaymentRecieved = (TextBox)myRow.FindControl("txtTotalPaymentRecieved") as TextBox;
        //TextBox txtPending = (TextBox)myRow.FindControl("txtPending") as TextBox;
        TextBox txtRecievedInRDDQtr = (TextBox)myRow.FindControl("txtRecievedInRDDQtr") as TextBox;
        TextBox txtRddPaymentDetails = (TextBox)myRow.FindControl("txtRddPaymentDetails") as TextBox;

        DropDownList ddlTypeOfActivity = (DropDownList)myRow.FindControl("ddlTypeOfActivity") as DropDownList;
        DropDownList ddlLocationOfActivity = (DropDownList)myRow.FindControl("ddlLocationOfActivity") as DropDownList;
        DropDownList tmpddlExpensedAt = (DropDownList)myRow.FindControl("ddlExpensedAt") as DropDownList;
        DropDownList ddlActivityDescription = (DropDownList)myRow.FindControl("ddlActivityDescription") as DropDownList;

        Label lblmuffilePathEdt = (Label)Grid2.Rows[e.NewEditIndex].FindControl("lblmuffilePathEdt") as Label;
        FileUpload tmpfileUploadMuf = (myRow.FindControl("fileUploadMuf") as FileUpload);

        Panel pnlEdt = myRow.FindControl("pnlEditItemTemplateChild") as Panel;
        setVisibleToUpdatePanelOfChildGridEditTemplateCase(pnlEdt);


        ///////////////////////////////////////////////////////////////
        //where isInternal=0 stands for vendor activities 1 stands for internal activities
        Db.LoadDDLsWithCon(ddlTypeOfActivity, "select autoindex,activityType as activity from ActivityDef where isInternal=1 order by activity", "activity", "autoindex", myGlobal.getRDDMarketingDBConnectionString());

        ListItem tt1 = ddlTypeOfActivity.Items.FindByText(lblTypeOfActivity.Text);
        if (tt1 != null)
        {
            for (int i = 0; i < ddlTypeOfActivity.Items.Count; i++)
            {
                if (lblTypeOfActivity.Text == ddlTypeOfActivity.Items[i].Text)
                {
                    ddlTypeOfActivity.SelectedIndex = i;
                    break;
                }
            }
        }

        Db.LoadDDLsWithCon(ddlLocationOfActivity, "select autoindex,Location from ActivityLocations order by Location", "Location", "autoindex", myGlobal.getRDDMarketingDBConnectionString());
        if (lblLocationOfActivity.Text == "")
            lblLocationOfActivity.Text = "NA";
        ListItem tt11 = ddlLocationOfActivity.Items.FindByText(lblLocationOfActivity.Text);
        if (tt11 != null)
        {
            for (int i = 0; i < ddlLocationOfActivity.Items.Count; i++)
            {
                if (lblLocationOfActivity.Text == ddlLocationOfActivity.Items[i].Text)
                {
                    ddlLocationOfActivity.SelectedIndex = i;
                    break;
                }
            }
        }

        Db.LoadDDLsWithCon(tmpddlExpensedAt, "select autoindex,Location from ExpenseLocations order by Location", "Location", "autoindex", myGlobal.getRDDMarketingDBConnectionString());

        if (lblExpensedAt.Text == "")
            lblExpensedAt.Text = "NA";
        ListItem tt111 = tmpddlExpensedAt.Items.FindByText(lblExpensedAt.Text);
        if (tt111 != null)
        {
            for (int i = 0; i < tmpddlExpensedAt.Items.Count; i++)
            {
                if (lblExpensedAt.Text == tmpddlExpensedAt.Items[i].Text)
                {
                    tmpddlExpensedAt.SelectedIndex = i;
                    break;
                }
            }
        }

        if (lblActivityDescription.Text != "")
            ddlActivityDescription.Items.FindByText(lblActivityDescription.Text).Selected = true;

        ///////////////////////////////////////////////////////////////

        lblmuffilePathEdt.Text = lblmuffilePath.Text;

        lbllastModifiedEdt.Text = lblLastModified.Text;
        txtAutoIndex_MufNo.Text = lblAutoIndex_MufNo.Text;

        txtAcDateOfExecution.Text = lblAcDateOfExecution.Text;
        if (txtAcDateOfExecution.Text == "")
        {
            txtAcDateOfExecution.Text = DateTime.Now.ToString("MM-dd-yyyy");
        }

        txtCosted.Text = lblCosted.Text;

        if (lblCosted.Text == "")
            lblCostedPrev.Text = "0";
        else
            lblCostedPrev.Text = lblCosted.Text;

        txtVat.Text = lblVat.Text;

        if (lblVat.Text == "")
            lblVatPrev.Text = "0";
        else
            lblVatPrev.Text = lblVat.Text;

        txtExpDetail.Text = lblExpDetail.Text;
        txtThirdPartyName.Text = lblThirdPartyName.Text;
        txtThirdPartyInvoiceReference.Text = lblThirdPartyInvoiceReference.Text;
        txtThirdPartyInvoiceNo.Text = lblThirdPartyInvoiceNo.Text;

        //ddlThirdPartyPaidStatus.SelectedItem.Text = lblThirdPartyPaidStatus.Text;
        ListItem tt = ddlThirdPartyPaidStatus.Items.FindByText(lblThirdPartyPaidStatus.Text);
        if (tt != null)
        {
            for (int i = 0; i < ddlThirdPartyPaidStatus.Items.Count; i++)
            {
                if (lblThirdPartyPaidStatus.Text == ddlThirdPartyPaidStatus.Items[i].Text)
                {
                    ddlThirdPartyPaidStatus.SelectedIndex = i;
                    break;
                }
            }
        }

        txtRddInvoiceNo.Text = lblRddInvoiceNo.Text;

        //ddlRddPaidStatus.SelectedItem.Text = lblRddPaidStatus.Text;
        ListItem ll = ddlRddPaidStatus.Items.FindByText(lblRddPaidStatus.Text);
        if (ll != null)
        {
            for (int i = 0; i < ddlRddPaidStatus.Items.Count; i++)
            {
                if (lblRddPaidStatus.Text == ddlRddPaidStatus.Items[i].Text)
                {
                    ddlRddPaidStatus.SelectedIndex = i;
                    break;
                }
            }
        }

        txtTotalPaymentRecieved.Text = lblTotalPaymentRecieved.Text;
        //txtPending.Text = lblPending.Text;
        txtRecievedInRDDQtr.Text = lblRecievedInRDDQtr.Text;
        txtRddPaymentDetails.Text = lblRddPaymentDetails.Text;

        if (txtCosted.Text == "")
        {
            txtCosted.Text = "0";
        }

        if (txtVat.Text == "")
        {
            txtVat.Text = "0";
        }

        if (txtTotalPaymentRecieved.Text == "")
        {
            txtTotalPaymentRecieved.Text = "0";
        }


        if ((Convert.ToInt32(Session["currStatusId"]) == 14) && pid == "10031")  //last status few fields have been disabled
        {
            foreach (GridViewRow rw in GridActStatusUpdate.Rows)
            {
                Button btn = rw.FindControl("btnAddField") as Button;
                btn.Enabled = false;
            }

            txtAutoIndex_MufNo.Enabled = false;
            txtAcDateOfExecution.Enabled = false;
            txtCosted.Enabled = false;
            ddlTypeOfActivity.Enabled = false;
            ddlLocationOfActivity.Enabled = false;
            tmpddlExpensedAt.Enabled = false;
            txtVat.Enabled = false;
            //txtExpDetail.Enabled = false;
            ddlActivityDescription.Enabled = false;
            txtThirdPartyName.Enabled = false;
            txtThirdPartyInvoiceReference.Enabled = false;
            tmpfileUploadMuf.Enabled = false;
        }

        //foreach (GridViewRow rw in GridActStatusUpdate.Rows)
        //{
        //    CheckBox chkView = rw.FindControl("chkView") as CheckBox;
        //    chkView.Enabled = false;
        //}
    }

    protected void Grid2_RowUpdating(object sender, GridViewUpdateEventArgs e)
    {
        lblError.Text = "";
        GridViewRow myRow = Grid2.Rows[e.RowIndex];
        if (myRow != null)
        {

            TextBox EDtxtAutoIndex_MufNo = (TextBox)myRow.FindControl("txtAutoIndex_MufNo");
            //Label lbllastModified = (myRow.FindControl("lbllastModified") as Label);
            Label lblLastModifiedEdt = (myRow.FindControl("lblLastModifiedEdt") as Label);
            TextBox EDtxtAcDateOfExecution = (myRow.FindControl("txtAcDateOfExecution") as TextBox);
            TextBox EDtxtCosted = (myRow.FindControl("txtCosted") as TextBox);
            Label lblCostedPrev = (myRow.FindControl("lblCostedPrev") as Label);

            TextBox EDtxtVat = (TextBox)myRow.FindControl("txtVat") as TextBox;
            Label EDlblVatPrev = (myRow.FindControl("lblVatPrev") as Label);

            TextBox EDtxtExpDetail = (myRow.FindControl("txtExpDetail") as TextBox);
            TextBox EDtxtThirdPartyName = (myRow.FindControl("txtThirdPartyName") as TextBox);
            TextBox EDtxtThirdPartyInvoiceReference = (myRow.FindControl("txtThirdPartyInvoiceReference") as TextBox);
            TextBox EDtxtThirdPartyInvoiceNo = (myRow.FindControl("txtThirdPartyInvoiceNo") as TextBox);
            DropDownList EDddlThirdPartyPaidStatus = (myRow.FindControl("ddlThirdPartyPaidStatus") as DropDownList);
            TextBox EDtxtRddInvoiceNo = (myRow.FindControl("txtRddInvoiceNo") as TextBox);
            DropDownList EDddlRddPaidStatus = (myRow.FindControl("ddlRddPaidStatus") as DropDownList);
            TextBox EDtxtTotalPaymentRecieved = (myRow.FindControl("txtTotalPaymentRecieved") as TextBox);
            //TextBox EDtxtPending = (myRow.FindControl("txtPending") as TextBox);
            TextBox EDtxtRecievedInRDDQtr = (myRow.FindControl("txtRecievedInRDDQtr") as TextBox);
            TextBox EDtxtRddPaymentDetails = myRow.FindControl("txtRddPaymentDetails") as TextBox;

            DropDownList ddlTypeOfActivity = (DropDownList)myRow.FindControl("ddlTypeOfActivity") as DropDownList;
            DropDownList ddlLocationOfActivity = (DropDownList)myRow.FindControl("ddlLocationOfActivity") as DropDownList;
            DropDownList tmpddlExpensedAt = (DropDownList)myRow.FindControl("ddlExpensedAt") as DropDownList;
            DropDownList ddlActivityDescription = (DropDownList)myRow.FindControl("ddlActivityDescription") as DropDownList;

            
            
            Label tmplblmuffilePathEdt = (myRow.FindControl("lblmuffilePathEdt") as Label);
            FileUpload tmpfileUploadMuf = (myRow.FindControl("fileUploadMuf") as FileUpload);

            Label lblmufsubmittedDateEdt = (myRow.FindControl("lblmufsubmittedDateEdt") as Label);

            if (!Util.IsValidDate(EDtxtAcDateOfExecution.Text))
            {
                lblError.Text = "Please supply a valid date for Actual Execution Date(mm-dd-yyyy)";
                //MsgBoxControl1.show(lblError.Text, "Error !!! ");
                return;
            }

            if (!Util.isValidDecimalNumber(EDtxtCosted.Text))
            {
                lblError.Text = "Invalid value in Costed Field, supports only numeric";
                //MsgBoxControl1.show(lblError.Text, "Error !!! ");
                return;
            }

            if (Convert.ToDecimal(EDtxtCosted.Text) <= 0)
            {
                lblError.Text = "Invalid value in Costed Field, enter valid positive value";
                //MsgBoxControl1.show(lblError.Text, "Error !!! ");
                return;
            }

            if (!Util.isValidDecimalNumber(EDtxtVat.Text))
            {
                lblError.Text = "Invalid value in Vat Field, supports only numeric";
                //MsgBoxControl1.show(lblError.Text, "Error !!! ");
                return;
            }

            if (Convert.ToDecimal(EDtxtVat.Text) < 0)
            {
                lblError.Text = "Invalid value in Vat Field, enter valid positive value";
                //MsgBoxControl1.show(lblError.Text, "Error !!! ");
                return;
            }
            
            DateTime lblDt;

            if (lblmufsubmittedDateEdt.Text.Trim() != "")
                lblDt = Convert.ToDateTime(lblmufsubmittedDateEdt.Text);
            else
                lblDt = DateTime.Now;

            if (ddlLocationOfActivity.SelectedItem.Text == "NA")
            {
                lblError.Text = "Invalid value selected ! Please select a valid Activity location from the list.";
                //MsgBoxControl1.show(lblError.Text, "Error !!! ");
                return;
            }

            if (tmpddlExpensedAt.SelectedItem.Text == "NA" && lblDt >= Convert.ToDateTime(lblThisVerDate.Text) && Convert.ToInt32(Session["currStatusId"]) < 14)     /////////////////// work here lblLastModifiedEdt
            {
                lblError.Text = "Invalid value selected ! Please select a valid Expense location from the list.";
                //MsgBoxControl1.show(lblError.Text, "Error !!! ");
                return;
            }

            //if (lblDt>=Convert.ToDateTime(lblThisVerDate.Text))     /////////////////// work here lblLastModifiedEdt
            //{
            //    lblError.Text = "Invalid value selected ! Please select a valid Expense location from the list.";
            //    //MsgBoxControl1.show(lblError.Text, "Error !!! ");
            //    return;
            //}

            //decimal totCostVatPrev=Convert.ToDecimal(lblCostedPrev.Text) + Convert.ToDecimal(EDlblVatPrev.Text);
            //decimal totCostVatNow = Convert.ToDecimal(EDtxtCosted.Text) + Convert.ToDecimal(EDtxtVat.Text);

            decimal totCostVatPrev = Convert.ToDecimal(lblCostedPrev.Text);
            decimal totCostVatNow = Convert.ToDecimal(EDtxtCosted.Text);

            //different calcs  vat + cost
            if (totCostVatNow > (Convert.ToDecimal(lblFundBalanceAvailableForSelectedActivity.Text) + totCostVatPrev))
            {
                //lblError.Text = "Invalid Value!  (Cost + Vat) Exceeds the funds avallable value for this activity, (Cost + Vat) should be in range ( 1 to " + lblFundBalanceAvailableForSelectedActivity.Text + ") $ .";
                lblError.Text = "Invalid Value!  Cost Exceeds the funds avallable value for this activity, Cost should be in range ( 1 to " + lblFundBalanceAvailableForSelectedActivity.Text + ") $ ., Vat not included.";
                //MsgBoxControl1.show(lblError.Text, "Error !!! ");
                return;
            }

            if (EDtxtExpDetail.Text.Trim() == "")
            {
                lblError.Text = "Error ! Fields Expence Details Cant Be Left Blank.";
                //MsgBoxControl1.show(lblError.Text, "Error !!! ");
                return;
            }

            if (EDtxtThirdPartyName.Text.Trim() == "")
            {
                lblError.Text = "Error ! Fields Third Party Name Cant Be Left Blank.";
                //MsgBoxControl1.show(lblError.Text, "Error !!! ");
                return;
            }

            if (EDtxtThirdPartyInvoiceReference.Text.Trim() == "")
            {
                lblError.Text = "Error ! Fields Third Party Reference Cant Be Left Blank.";
                //MsgBoxControl1.show(lblError.Text, "Error !!! ");
                return;
            }

            /////////////////////////////////////
            
            if (tmpfileUploadMuf.HasFile)
            {
                // Getimage path from database config admin/uploaded Images
                String dbPth, pth, newNametmpPth;

                dbPth = lblFile.Text.Substring(0, (lblFile.Text.LastIndexOf("/")) + 1);

                pth = Server.MapPath("~" + dbPth);
                //newNametmpPth = Session["processAbbr"].ToString() + "-" + refId + "-" + lblStatus.Text + "-" + myGlobal.loggedInUser() + "-" + DateTime.Now.ToString("dd-MM-yyyy-hh-mm-ss") + "-" + tmpfileUploadMuf.FileName;
                newNametmpPth = myGlobal.loggedInUser() + "-" + DateTime.Now.ToString("dd-MM-yyyy-hh-mm-ss") + "-" + tmpfileUploadMuf.FileName;

                tmpfileUploadMuf.SaveAs(pth + newNametmpPth);

                tmplblmuffilePathEdt.Text = dbPth + newNametmpPth;
            }

            if (tmplblmuffilePathEdt.Text.Trim() == "" && lblDt >= Convert.ToDateTime(lblThisVerDate.Text))
            {
                lblError.Text = "Invalid value ! MUF file document not attached, field can't be empty";
                return;
            }

            ////////////////////////////////////////////////

            ///validation only for last stage closed plan.  MUF being created after that madatory update for third party paid stauts , invoice no.
            if (lblStatus.Text.ToUpper()== "Plan Closed".ToUpper())  //checked only at last stage
            {
                if (EDtxtThirdPartyInvoiceNo.Text.Trim() == "")
                {
                    lblError.Text = "Error ! Fields Third Party Invoice No. Cant Be Left Blank.";
                    //MsgBoxControl1.show(lblError.Text, "Error !!! ");
                    return;
                }

                if (EDddlThirdPartyPaidStatus.SelectedItem.Text.ToUpper() != "Paid".ToUpper())
                {
                    lblError.Text = "Error ! Fields Third Party Paid status should be paid , as you are doing MUF after the plan is already closed.";
                    //MsgBoxControl1.show(lblError.Text, "Error !!! ");
                    return;
                }

                if (!Util.isValidDecimalNumber(EDtxtTotalPaymentRecieved.Text))
                {
                    lblError.Text = "Error! Invalid value in Total Paymeny Received Field, supports only numeric";
                    //MsgBoxControl1.show(lblError.Text, "Error !!! ");
                    return;
                }
            }

            ///validation only for last stage updation
            if (Convert.ToInt32(Session["currStatusId"]) == 14)  //checked only at last stage
            {
                if (EDtxtRecievedInRDDQtr.Text.Trim() == "")
                {
                    lblError.Text = "Error ! Fields RDD Received Quater Cant Be Left Blank.";
                    //MsgBoxControl1.show(lblError.Text, "Error !!! ");
                    return;
                }

                if (EDtxtRddInvoiceNo.Text.Trim() == "")
                {
                    lblError.Text = "Error ! Fields RDD Invoice No. Cant Be Left Blank.";
                    //MsgBoxControl1.show(lblError.Text, "Error !!! ");
                    return;
                }

                if (EDtxtThirdPartyInvoiceNo.Text.Trim() == "")
                {
                    lblError.Text = "Error ! Fields Third Party Invoice No. Cant Be Left Blank.";
                    //MsgBoxControl1.show(lblError.Text, "Error !!! ");
                    return;
                }

                if (!Util.isValidDecimalNumber(EDtxtTotalPaymentRecieved.Text))
                {
                    lblError.Text = "Error! Invalid value in Total Paymeny Received Field, supports only numeric";
                    //MsgBoxControl1.show(lblError.Text, "Error !!! ");
                    return;
                }

                if (EDtxtRddPaymentDetails.Text.Trim() == "")
                {
                    lblError.Text = "Error ! Fields RDD Payment Details. Cant Be Left Blank.";
                    //MsgBoxControl1.show(lblError.Text, "Error !!! ");
                    return;
                }

                if (EDtxtRddPaymentDetails.Text.Trim().IndexOf("'")>=0)
                {
                    lblError.Text = "Invalid Character occurs ' in field  RDD Payment Details, Char( ' ) not supported.";
                    //MsgBoxControl1.show(lblError.Text, "Error !!! ");
                    return;
                }
            }
            // last stage validations end

            string qry;

            if (Session["insert"].ToString() == "")
            {
                if (Convert.ToInt32(Session["currStatusId"]) == 14 || lblStatus.Text.ToUpper()=="Plan Closed".ToUpper())  //checked only at last stage, full qry
                    qry = "update TblActivitiesDetails set lastModified='" + DateTime.Now.ToString() + "',ActualDateofexecution='" + EDtxtAcDateOfExecution.Text + "', Costed=" + EDtxtCosted.Text + ",vat=" + EDtxtVat.Text + ", ExpenseDetail='" + EDtxtExpDetail.Text + "', ThirdpartyName='" + EDtxtThirdPartyName.Text + "', ThirdPartyInvoiceReference='" + EDtxtThirdPartyInvoiceReference.Text + "', ThirdPartyInvoiceNo='" + EDtxtThirdPartyInvoiceNo.Text + "', ThirdPartyPaidStatus='" + EDddlThirdPartyPaidStatus.SelectedItem.Text + "', RddInvoiceNo='" + EDtxtRddInvoiceNo.Text + "', RddPaidStatus='" + EDddlRddPaidStatus.SelectedItem.Text + "', TotalPaymentReceived='" + EDtxtTotalPaymentRecieved.Text + "', Pending='" + "0" + "',ReceivedInRDDQTR='" + EDtxtRecievedInRDDQtr.Text + "', RDDPaymentDetails='" + EDtxtRddPaymentDetails.Text + "', ActivityType='" + ddlTypeOfActivity.SelectedItem.Text + "', ActivityLocation='" + ddlLocationOfActivity.SelectedItem.Text + "', ExpensedAt='" + tmpddlExpensedAt.SelectedItem.Text + "', ActivityDesc='" + ddlActivityDescription.SelectedItem.Text + "',mufFilePath='" + tmplblmuffilePathEdt.Text + "' where autoindex_MUFNo='" + EDtxtAutoIndex_MufNo.Text + "'";
                else
                    qry = "update TblActivitiesDetails set lastModified='" + DateTime.Now.ToString() + "',ActualDateofexecution='" + EDtxtAcDateOfExecution.Text + "', Costed=" + EDtxtCosted.Text + ",vat=" + EDtxtVat.Text + ", ActivityType='" + ddlTypeOfActivity.SelectedItem.Text + "', ActivityLocation='" + ddlLocationOfActivity.SelectedItem.Text + "', ExpensedAt='" + tmpddlExpensedAt.SelectedItem.Text + "', ActivityDesc='" + ddlActivityDescription.SelectedItem.Text + "', ExpenseDetail='" + EDtxtExpDetail.Text + "', ThirdpartyName='" + EDtxtThirdPartyName.Text + "', ThirdPartyInvoiceReference='" + EDtxtThirdPartyInvoiceReference.Text + "',mufFilePath='" + tmplblmuffilePathEdt.Text + "' where autoindex_MUFNo='" + EDtxtAutoIndex_MufNo.Text + "'";

                Db.constr = myGlobal.getRDDMarketingDBConnectionString();
                Db.myExecuteSQL(qry);
                lblError.Text = "Updations Successfully Done";
                //MsgBoxControl1.show(lblError.Text, "Success !!! ");
            }
            else
            {
                if (Convert.ToInt32(Session["currStatusId"]) == 14 || lblStatus.Text.ToUpper() == "Plan Closed".ToUpper())  //checked only at last stage, full qry
                    qry = " insert into dbo.TblActivitiesDetails (fk_ActivitySno,ActualDateofExecution,Costed,vat,ExpenseDetail,ThirdPartyName,ThirdPartyInvoiceReference,ThirdPartyInvoiceNo,ThirdPartyPaidStatus,RddInvoiceNo,RddPaidStatus,TotalPaymentReceived,Pending,ReceivedInRDDQTR,RDDPaymentDetails,ActivityType,ActivityLocation,ExpensedAt,ActivityDesc,mufFilePath) values(" + Session["lblsno"].ToString() + ",'" + EDtxtAcDateOfExecution.Text + "'," + EDtxtCosted.Text + "," + EDtxtVat.Text + ",'" + EDtxtExpDetail.Text + "','" + EDtxtThirdPartyName.Text + "','" + EDtxtThirdPartyInvoiceReference.Text + "','" + EDtxtThirdPartyInvoiceNo.Text + "','" + EDddlThirdPartyPaidStatus.SelectedItem.Text + "','" + EDtxtRddInvoiceNo.Text + "','" + EDddlRddPaidStatus.SelectedItem.Text + "','" + EDtxtTotalPaymentRecieved.Text + "','" + "0" + "','" + EDtxtRecievedInRDDQtr.Text + "','" + EDtxtRddPaymentDetails.Text + "','" + ddlTypeOfActivity.SelectedItem.Text + "','" + ddlLocationOfActivity.SelectedItem.Text + "','" + tmpddlExpensedAt.SelectedItem.Text + "','" + ddlActivityDescription.SelectedItem.Text + "','" + tmplblmuffilePathEdt.Text + "')";
                else
                    qry = " insert into dbo.TblActivitiesDetails (fk_ActivitySno,ActualDateofExecution,Costed,vat,ActivityType,ActivityLocation,ExpensedAt,ActivityDesc,ExpenseDetail,ThirdPartyName,ThirdPartyInvoiceReference,mufFilePath) values(" + Session["lblsno"].ToString() + ",'" + EDtxtAcDateOfExecution.Text + "'," + EDtxtCosted.Text + "," + EDtxtVat.Text + ",'" + ddlTypeOfActivity.SelectedItem.Text + "','" + ddlLocationOfActivity.SelectedItem.Text + "','" + tmpddlExpensedAt.SelectedItem.Text + "','" + ddlActivityDescription.SelectedItem.Text + "','" + EDtxtExpDetail.Text + "','" + EDtxtThirdPartyName.Text + "','" + EDtxtThirdPartyInvoiceReference.Text + "','" + tmplblmuffilePathEdt.Text + "')";

                qry = qry + " ; Exec updateRddPmtDetailsForMUFs @fk_ActivitySno=" + Session["lblsno"].ToString();  //an addition to update RDD pmts status fields using procedure
                Db.constr = myGlobal.getRDDMarketingDBConnectionString();
                Db.myExecuteSQL(qry);
                lblError.Text = "Row Inserted Successfully";
                //MsgBoxControl1.show(lblError.Text, "Success !!! ");
            }
            Grid2.EditIndex = -1;

            if (Session["lblsno"] != null)
                BindGrid2(Session["lblsno"].ToString());
            else
                BindGrid2("0");

            BindGrid(); //refresh grid
            setlblFundBalanceAvailableForSelectedActivity(lblSelectedActivity.Text);

            //setBtnAddFieldEnabledFalse();

            loadMUFForms();
        }
    }

    private void setBtnAddFieldEnabledFalse()
    {
        if ((Convert.ToInt32(Session["currStatusId"]) == 14) && pid == "10031")
        {
            foreach (GridViewRow rw in GridActStatusUpdate.Rows)
            {
                Button btn = rw.FindControl("btnAddField") as Button;
                btn.Enabled = false;
            }
        }
    }

    protected void Grid2_RowCancelingEdit(object sender, GridViewCancelEditEventArgs e)
    {
        Grid2.EditIndex = -1;
        if (Session["lblsno"] != null)
            BindGrid2(Session["lblsno"].ToString());
        else
            BindGrid2("0");

        //setBtnAddFieldEnabledFalse();
    }

    protected void Grid2_RowDeleting(object sender, GridViewDeleteEventArgs e)
    {
        GridViewRow myRow = Grid2.Rows[e.RowIndex];
        if (myRow != null)
        {
            Label lblAutoIndex_MufNo = (Label)myRow.FindControl("lblAutoIndex_MufNo");
            if (lblAutoIndex_MufNo.Text != "")
            {
                String sqlQry = "delete from  TblActivitiesDetails where autoIndex_MUFNo='" + lblAutoIndex_MufNo.Text + "'";
                Db.constr = myGlobal.getRDDMarketingDBConnectionString();
                Db.myExecuteSQL(sqlQry);
                lblError.Text = "Record has been deleted successfully";
                MsgBoxControl1.show(lblError.Text, "Success !!! ");
            }

            if (Session["lblsno"] != null)
                BindGrid2(Session["lblsno"].ToString());
            else
                BindGrid2("0");

            BindGrid(); //refresh grid
            setlblFundBalanceAvailableForSelectedActivity(lblSelectedActivity.Text);
            loadMUFForms();
        }
    }

    public bool GetActivityChildRows()
    {
        bool flag = true;
        int rowCount = 0;
        string hasExp = "";

        foreach (GridViewRow row in GridActStatusUpdate.Rows)
        {
            Label tmpLbl = row.FindControl("lblsno") as Label;
            //DataTable dts;
            sql = "select sno,hasExpenses,B.fk_ActivitySno,(select count(*) cnt from TblActivitiesDetails where fk_ActivitySno=A.Sno) cnt from dbo.TblActivities A left join TblActivitiesDetails B on A.Sno=B.fk_ActivitySno where sno=" + tmpLbl.Text;
            Db.constr = myGlobal.getRDDMarketingDBConnectionString();
            drd = Db.myGetReader(sql);

            drd.Read();
            rowCount = Convert.ToInt32(drd["cnt"].ToString());
            hasExp = drd["hasExpenses"].ToString();
            drd.Close();

            if (rowCount <= 0 && hasExp.ToUpper() == "YES")
            {
                flag = false;
            }

            if (flag == false)
            {
                return false;
            }
        }
        return flag;
    }

    public bool GetActivityChildRowsOld()
    {
        bool flag = true;
        //int rowCount = 0;
        //foreach (GridViewRow row in GridActStatusUpdate.Rows)
        //{
        //    Label tmpLbl = row.FindControl("lblsno") as Label;
        //    //DataTable dts;
        //    sql = "select count(*) cnt from TblActivitiesDetails where fk_ActivitySno=" + tmpLbl.Text + "";
        //    Db.constr = myGlobal.getRDDMarketingDBConnectionString();
        //    //dts = Db.myGetDS(sql).Tables[0];
        //    rowCount =Db.myExecuteScalar(sql);

        //    if (rowCount<=0)
        //    {
        //        flag = false;
        //    }
        //    if (flag == false)
        //    {
        //        return false;
        //    }
        //}
        return flag;
    }

    protected void ddlhasExpenses_SelectedIndexChanged(object sender, EventArgs e)
    {
        DropDownList ddl = sender as DropDownList;
        GridViewRow grdrw = (GridViewRow)((DataControlFieldCell)((DropDownList)sender).Parent).Parent;
        Control ctrl;
        Button btn;
        ctrl = grdrw.FindControl("ddlhasExpenses") as DropDownList;
        btn = grdrw.FindControl("btnAddField") as Button;
        if (ctrl != null)
        {
            string tmpqry = "";
            DropDownList ddl1 = (DropDownList)ctrl;
            if (ddl.ClientID == ddl1.ClientID)
            {
                CheckBox chkView = grdrw.FindControl("chkView") as CheckBox;
                Label tmplblsno = (Label)grdrw.FindControl("lblsno") as Label;
                if (ddl.SelectedItem.Text.ToUpper() == "NO")
                {
                    btn.Enabled = false;
                }
                else
                {
                    Label lblActivityCode = grdrw.FindControl("lblActivityCode") as Label;
                    if (lblSelectedActivity.Text.IndexOf(lblActivityCode.Text) >= 0)
                        btn.Enabled = true;
                }

                //tmpqry = string.Format("update TblActivities set hasExpenses='{0}' where sno=" + tmplblsno.Text, ddl.SelectedItem.Text);
                tmpqry = string.Format("update TblActivities set hasExpenses='{0}',lastModified='{1}' where sno=" + tmplblsno.Text, ddl.SelectedItem.Text, DateTime.Now.ToString());
                
                Db.constr = myGlobal.getRDDMarketingDBConnectionString();
                Db.myExecuteSQL(tmpqry);

                if (Session["lblsno"] != null)
                    BindGrid2(Session["lblsno"].ToString());
                else
                    BindGrid2("0");
            }
        }
    }

    private void GenerateMUFForm(string evnt)
    {
        mufUpdateSql = "";

        if (Convert.ToInt32(Session["currStatusId"]) == 6)  //executive can submitt only NO case
            //sql = "select EE.*,isnull(WW.mufApprovedCost,0) mufApprovedCost from (select B.mufFormFilledStatus,B.autoindex_MUFNO as MUFNO,B.ActualDateOfExecution,V.buName,W.fk_VendorBU,W.planQuater as RddQuater,W.vendorQuater,A.ActivityVendorAmount as AmtAccrued,B.ActivityLocation,B.ExpensedAt,B.ActivityDesc,A.sno ActivitySno, A.ActivityCode,R.accrualFormNo,B.ActivityType,B.ExpenseDetail,B.Costed,B.Vat,B.ThirdPartyName,B.ThirdPartyInvoiceNo,isnull(B.submittedBy,'') submittedBy,isnull(B.verifiedBy,'') verifiedBy,isnull(B.accruedBy,'') accruedBy,isnull(B.authorisedBy,'') authorisedBy from TblActivities as A join ( select * from dbo.TblActivitiesDetails where fk_ActivitySno in (select sno from dbo.TblActivities where fk_workFlowPlansId=" + refId + ")) as B on B.fk_ActivitySno=A.sno left join dbo.workFlowPlans W on W.autoindex=A.fk_workFlowPlansId left join dbo.VendorBUDef V on V.autoIndex=W.fk_VendorBU left join dbo.TblAccrualList R on R.fk_workFlowPlansId=A.fk_workFlowPlansId where B.mufFormFilledStatus='No' ) as EE left join ( select A.sno as ActivitySno,A.ActivityCode,count(*) as childRowsCount,isnull(sum(costed+vat),0) as mufApprovedCost from dbo.TblActivities A left join TblActivitiesDetails B on A.sno=B.fk_ActivitySno where B.mufFormFilledStatus='Yes' group by fk_ActivitySno,A.ActivityCode,A.sno ) as WW on EE.ActivitySno=WW.ActivitySno order by EE.ActivityCode,EE.MUFNO Asc";
            //sql = "select EE.*,isnull(WW.mufApprovedCost,0) mufApprovedCost from (select B.mufFormFilledStatus,B.autoindex_MUFNO as MUFNO,B.ActualDateOfExecution,V.buName,W.fk_VendorBU,W.planQuater as RddQuater,W.vendorQuater,A.ActivityVendorAmount as AmtAccrued,B.ActivityLocation,B.ExpensedAt,B.ActivityDesc,A.sno ActivitySno, A.ActivityCode,R.accrualFormNo,B.ActivityType,B.ExpenseDetail,B.Costed,B.Vat,B.ThirdPartyName,B.ThirdPartyInvoiceNo,isnull(B.submittedBy,'') submittedBy,isnull(B.verifiedBy,'') verifiedBy,isnull(B.accruedBy,'') accruedBy,isnull(B.authorisedBy,'') authorisedBy,M.intimationMailId,B.mufFilePath from TblActivities as A join ( select * from dbo.TblActivitiesDetails where fk_ActivitySno in (select sno from dbo.TblActivities where fk_workFlowPlansId=" + refId + ")) as B on B.fk_ActivitySno=A.sno left join dbo.workFlowPlans W on W.autoindex=A.fk_workFlowPlansId left join dbo.VendorBUDef V on V.autoIndex=W.fk_VendorBU left join dbo.ExpenseLocations M on M.Location=B.ExpensedAt left join dbo.TblAccrualList R on R.fk_workFlowPlansId=A.fk_workFlowPlansId where B.mufFormFilledStatus='No' ) as EE left join ( select A.sno as ActivitySno,A.ActivityCode,count(*) as childRowsCount,isnull(sum(costed+vat),0) as mufApprovedCost from dbo.TblActivities A left join TblActivitiesDetails B on A.sno=B.fk_ActivitySno where B.mufFormFilledStatus='Yes' group by fk_ActivitySno,A.ActivityCode,A.sno ) as WW on EE.ActivitySno=WW.ActivitySno order by EE.ActivityCode,EE.MUFNO Asc";
            sql = "select EE.*,isnull(WW.mufApprovedCost,0) mufApprovedCost from (select B.mufFormFilledStatus,B.autoindex_MUFNO as MUFNO,B.ActualDateOfExecution,V.buName,W.fk_VendorBU,W.planQuater as RddQuater,W.vendorQuater,A.ActivityVendorAmount as AmtAccrued,B.ActivityLocation,B.ExpensedAt,B.ActivityDesc,A.sno ActivitySno, A.ActivityCode,R.accrualFormNo,B.ActivityType,B.ExpenseDetail,B.Costed,B.Vat,B.ThirdPartyName,B.ThirdPartyInvoiceNo,isnull(B.submittedBy,'') submittedBy,isnull(B.verifiedBy,'') verifiedBy,isnull(B.accruedBy,'') accruedBy,isnull(B.authorisedBy,'') authorisedBy,M.intimationMailId,B.mufFilePath from TblActivities as A join ( select * from dbo.TblActivitiesDetails where fk_ActivitySno in (select sno from dbo.TblActivities where fk_workFlowPlansId=" + refId + ")) as B on B.fk_ActivitySno=A.sno left join dbo.workFlowPlans W on W.autoindex=A.fk_workFlowPlansId left join dbo.VendorBUDef V on V.autoIndex=W.fk_VendorBU left join dbo.ExpenseLocations M on M.Location=B.ExpensedAt left join dbo.TblAccrualList R on R.fk_workFlowPlansId=A.fk_workFlowPlansId where B.mufFormFilledStatus='No' ) as EE left join ( select A.sno as ActivitySno,A.ActivityCode,count(*) as childRowsCount,isnull(sum(costed),0) as mufApprovedCost from dbo.TblActivities A left join TblActivitiesDetails B on A.sno=B.fk_ActivitySno where B.mufFormFilledStatus='Yes' group by fk_ActivitySno,A.ActivityCode,A.sno ) as WW on EE.ActivitySno=WW.ActivitySno order by EE.ActivityCode,EE.MUFNO Asc";
        else  // others can submitt where status is not finally YES
            //sql = "select EE.*,isnull(WW.mufApprovedCost,0) mufApprovedCost from (select B.mufFormFilledStatus,B.autoindex_MUFNO as MUFNO,B.ActualDateOfExecution,V.buName,W.fk_VendorBU,W.planQuater as RddQuater,W.vendorQuater,A.ActivityVendorAmount as AmtAccrued,B.ActivityLocation,B.ExpensedAt,B.ActivityDesc,A.sno ActivitySno, A.ActivityCode,R.accrualFormNo,B.ActivityType,B.ExpenseDetail,B.Costed,B.Vat,B.ThirdPartyName,B.ThirdPartyInvoiceNo,isnull(B.submittedBy,'') submittedBy,isnull(B.verifiedBy,'') verifiedBy,isnull(B.accruedBy,'') accruedBy,isnull(B.authorisedBy,'') authorisedBy from TblActivities as A join ( select * from dbo.TblActivitiesDetails where fk_ActivitySno in (select sno from dbo.TblActivities where fk_workFlowPlansId=" + refId + ")) as B on B.fk_ActivitySno=A.sno left join dbo.workFlowPlans W on W.autoindex=A.fk_workFlowPlansId left join dbo.VendorBUDef V on V.autoIndex=W.fk_VendorBU left join dbo.TblAccrualList R on R.fk_workFlowPlansId=A.fk_workFlowPlansId where B.mufFormFilledStatus!='Yes' ) as EE left join ( select A.sno as ActivitySno,A.ActivityCode,count(*) as childRowsCount,isnull(sum(costed+vat),0) as mufApprovedCost from dbo.TblActivities A left join TblActivitiesDetails B on A.sno=B.fk_ActivitySno where B.mufFormFilledStatus='Yes' group by fk_ActivitySno,A.ActivityCode,A.sno ) as WW on EE.ActivitySno=WW.ActivitySno order by EE.ActivityCode,EE.MUFNO Asc";
            //sql = "select EE.*,isnull(WW.mufApprovedCost,0) mufApprovedCost from (select B.mufFormFilledStatus,B.autoindex_MUFNO as MUFNO,B.ActualDateOfExecution,V.buName,W.fk_VendorBU,W.planQuater as RddQuater,W.vendorQuater,A.ActivityVendorAmount as AmtAccrued,B.ActivityLocation,B.ExpensedAt,B.ActivityDesc,A.sno ActivitySno, A.ActivityCode,R.accrualFormNo,B.ActivityType,B.ExpenseDetail,B.Costed,B.Vat,B.ThirdPartyName,B.ThirdPartyInvoiceNo,isnull(B.submittedBy,'') submittedBy,isnull(B.verifiedBy,'') verifiedBy,isnull(B.accruedBy,'') accruedBy,isnull(B.authorisedBy,'') authorisedBy,M.intimationMailId,B.mufFilePath from TblActivities as A join ( select * from dbo.TblActivitiesDetails where fk_ActivitySno in (select sno from dbo.TblActivities where fk_workFlowPlansId=" + refId + ")) as B on B.fk_ActivitySno=A.sno left join dbo.workFlowPlans W on W.autoindex=A.fk_workFlowPlansId left join dbo.VendorBUDef V on V.autoIndex=W.fk_VendorBU left join dbo.ExpenseLocations M on M.Location=B.ExpensedAt left join dbo.TblAccrualList R on R.fk_workFlowPlansId=A.fk_workFlowPlansId where B.mufFormFilledStatus!='Yes' ) as EE left join ( select A.sno as ActivitySno,A.ActivityCode,count(*) as childRowsCount,isnull(sum(costed+vat),0) as mufApprovedCost from dbo.TblActivities A left join TblActivitiesDetails B on A.sno=B.fk_ActivitySno where B.mufFormFilledStatus='Yes' group by fk_ActivitySno,A.ActivityCode,A.sno ) as WW on EE.ActivitySno=WW.ActivitySno order by EE.ActivityCode,EE.MUFNO Asc";
            sql = "select EE.*,isnull(WW.mufApprovedCost,0) mufApprovedCost from (select B.mufFormFilledStatus,B.autoindex_MUFNO as MUFNO,B.ActualDateOfExecution,V.buName,W.fk_VendorBU,W.planQuater as RddQuater,W.vendorQuater,A.ActivityVendorAmount as AmtAccrued,B.ActivityLocation,B.ExpensedAt,B.ActivityDesc,A.sno ActivitySno, A.ActivityCode,R.accrualFormNo,B.ActivityType,B.ExpenseDetail,B.Costed,B.Vat,B.ThirdPartyName,B.ThirdPartyInvoiceNo,isnull(B.submittedBy,'') submittedBy,isnull(B.verifiedBy,'') verifiedBy,isnull(B.accruedBy,'') accruedBy,isnull(B.authorisedBy,'') authorisedBy,M.intimationMailId,B.mufFilePath from TblActivities as A join ( select * from dbo.TblActivitiesDetails where fk_ActivitySno in (select sno from dbo.TblActivities where fk_workFlowPlansId=" + refId + ")) as B on B.fk_ActivitySno=A.sno left join dbo.workFlowPlans W on W.autoindex=A.fk_workFlowPlansId left join dbo.VendorBUDef V on V.autoIndex=W.fk_VendorBU left join dbo.ExpenseLocations M on M.Location=B.ExpensedAt left join dbo.TblAccrualList R on R.fk_workFlowPlansId=A.fk_workFlowPlansId where B.mufFormFilledStatus!='Yes' ) as EE left join ( select A.sno as ActivitySno,A.ActivityCode,count(*) as childRowsCount,isnull(sum(costed),0) as mufApprovedCost from dbo.TblActivities A left join TblActivitiesDetails B on A.sno=B.fk_ActivitySno where B.mufFormFilledStatus='Yes' group by fk_ActivitySno,A.ActivityCode,A.sno ) as WW on EE.ActivitySno=WW.ActivitySno order by EE.ActivityCode,EE.MUFNO Asc";
        Db.constr = myGlobal.getRDDMarketingDBConnectionString();
        dr = Db.myGetReader(sql);

        mufDetailHtmlString = "";
        mufFls = ""; //from null to space when enters

        string submittedBy = "", verifiedBy = "", accruedBy = "", authorisedBy = "", MufForm = "";
        double totAccrualAmt, cost, vat, fundsAvail = 0, prevFundsAvail = 0, ttlfundsAvail = 0, mufApprovedCost = 0;
        string activityCode, prevActivityCode = "",varmufFile;


        if (dr.HasRows)
        {
            LstIntimation.Items.Clear();
            mufFls = "";

            while (dr.Read())
            {
                if (dr["MUFNO"] != DBNull.Value) //It means there are expences
                {
                    //string MUFNo = "", ActualDateOfExecution = "", buName="", fk_VendorBU="", RddQuater="", vendorQuater="", AmtAccrued="", FundsAvailable="", ActivityLocation="", ActivityDesc="", ActivityCode="", accrualFormNo="", ExpenceDetail="", Costed="", VAt="", ThirdPartyName="", ThirdPartyInvoiceNo="", submittedBy="", verifiedBy="", accruedBy="", authorisedBy="", ActivityType=""  ;
                    submittedBy = "";
                    verifiedBy = "";
                    accruedBy = "";
                    authorisedBy = "";
                    MufForm = "";
                    varmufFile = "";

                    activityCode = dr["ActivityCode"].ToString();

                    if (prevActivityCode == "" || prevActivityCode != activityCode)
                    {
                        totAccrualAmt = Convert.ToDouble(dr["AmtAccrued"]);
                        mufApprovedCost = Convert.ToDouble(dr["mufApprovedCost"].ToString());
                        cost = Convert.ToDouble(dr["Costed"]);
                        vat = Convert.ToDouble(dr["vat"]);

                        ttlfundsAvail = Math.Round(totAccrualAmt - mufApprovedCost,2);
                        //fundsAvail = ttlfundsAvail - cost - vat;
                        fundsAvail = Math.Round(ttlfundsAvail - cost,2);
                    }
                    if (prevActivityCode == activityCode)
                    {
                        cost = Convert.ToDouble(dr["Costed"]);
                        vat = Convert.ToDouble(dr["vat"]);
                        ttlfundsAvail = fundsAvail;
                        //fundsAvail = prevFundsAvail - cost - vat;
                        fundsAvail = prevFundsAvail - cost;
                    }

                    if (dr["submittedBy"] != DBNull.Value)
                        submittedBy = dr["submittedBy"].ToString();

                    if (dr["verifiedBy"] != DBNull.Value)
                        verifiedBy = dr["verifiedBy"].ToString();

                    if (dr["accruedBy"] != DBNull.Value)
                        accruedBy = dr["accruedBy"].ToString();

                    if (dr["authorisedBy"] != DBNull.Value)
                        authorisedBy = dr["authorisedBy"].ToString();

                    MufForm = "<table style='border-color:Black;width:90%' border='2'>";

                    //Header Row
                    MufForm += "<tr>";

                    MufForm += "<td style='font-weight:bold;background-color:#D6DCE4' align='center' colspan='6'>MUF Form</td></tr>";
                    //MufForm += "<td style='width:11%'>&nbsp;</td><td style='width:11%'>&nbsp;</td><td style='width:11%'>&nbsp;</td><td style='width:11%'>&nbsp;</td><td style='width:11%'>&nbsp;</td><td style='width:11%'>&nbsp;</td><td style='width:11%'>&nbsp;</td><td style='width:11%'>&nbsp;</td><td style='width:11%'>&nbsp;</td>";

                    //1st Row
                    MufForm += "<tr>";

                    MufForm += "<td>MUF No</td>";

                    MufForm += "<td colspan='2' style='color:blue'>";
                    MufForm += dr["MUFNO"].ToString();
                    MufForm += "</td>";

                    MufForm += "<td>Date</td>";

                    MufForm += "<td colspan='2' style='color:blue'>";
                    MufForm += Convert.ToDateTime(dr["ActualDateOfExecution"].ToString());
                    MufForm += "</td>";

                    MufForm += "</tr>";


                    //2nd Row
                    MufForm += "<tr><td colspan='6'>&nbsp;</td></tr>";

                    //3rd Row
                    MufForm += "<tr>";

                    MufForm += "<td style='width:16%'>BU</td>";

                    MufForm += "<td style='color:blue;width:16%'>";
                    MufForm += dr["buName"].ToString();
                    MufForm += "</td>";

                    MufForm += "<td style='width:16%'>RDD Qtr</td>";

                    MufForm += "<td style='color:blue;width:16%'>";
                    MufForm += dr["RddQuater"].ToString();
                    MufForm += "</td>";

                    MufForm += "<td style='width:16%'>Vendor Qtr</td>";

                    MufForm += "<td style='color:blue;width:16%'>";
                    MufForm += dr["vendorQuater"].ToString();
                    MufForm += "</td>";

                    MufForm += "</tr>";


                    //5th Row
                    MufForm += "<tr><td colspan='6'>&nbsp;</td></tr>";


                    //6th Row
                    MufForm += "<tr>";

                    MufForm += "<td>Total Amount Accrued</td>";

                    MufForm += "<td colspan='2' style='color:blue'>$ ";
                    MufForm += dr["AmtAccrued"].ToString();
                    MufForm += "</td>";

                    MufForm += "<td>Total Funds Available</td>";

                    //MufForm += "<td>$nbsp;</td>";

                    MufForm += "<td colspan='2' style='color:blue'>$ ";
                    MufForm += ttlfundsAvail;
                    MufForm += "</td>";

                    //MufForm += "<td>$nbsp;</td>";

                    MufForm += "</tr>";


                    //7th Row
                    MufForm += "<tr><td colspan='6'>&nbsp;</td></tr>";


                    //8th Row
                    MufForm += "<tr>";

                    MufForm += "<td>Location of the Activity</td>";

                    ///////////////////////////////////////////////////////////////

                    //MufForm += "<td colspan='2' style='color:blue'>";
                    //MufForm += dr["ActivityLocation"].ToString();
                    //MufForm += "</td>";

                    /////////////////////////////////////////////////////
                    MufForm += "<td colspan='1' style='color:blue'><b>";
                    MufForm += dr["ActivityLocation"].ToString();
                    MufForm += "</b></td>";

                    MufForm += "<td colspan='1' style='color:blue'>Expensed At :&nbsp;<b>";
                    MufForm += dr["ExpensedAt"].ToString();
                    MufForm += "</b></td>";
                    /////////////////////////////////////////////

                    MufForm += "<td>Activity Description</td>";

                    MufForm += "<td colspan='2' style='color:blue'>";
                    MufForm += dr["ActivityDesc"].ToString();
                    MufForm += "</td>";

                    MufForm += "</tr>";


                    //9th Row
                    MufForm += "<tr><td colspan='6'>&nbsp;</td></tr>";


                    //10th Row
                    MufForm += "<tr>";

                    MufForm += "<td>Activity ID</td>";

                    MufForm += "<td style='color:blue'>";
                    MufForm += dr["ActivityCode"].ToString();
                    MufForm += "</td>";

                    MufForm += "<td>Accrual form no.</td>";

                    MufForm += "<td style='color:blue'>";
                    MufForm += dr["accrualFormNo"].ToString();
                    MufForm += "</td>";

                    MufForm += "<td>Activity Type</td>";

                    MufForm += "<td style='color:blue'>";
                    MufForm += dr["ActivityType"].ToString();
                    MufForm += "</td>";

                    MufForm += "</tr>";



                    //11th Row
                    MufForm += "<tr><td colspan='6'>&nbsp;</td></tr>";


                    //12th Row
                    MufForm += "<tr><td colspan='6'>Expense Detail</td></tr>";


                    //13th Row
                    MufForm += "<tr>";

                    MufForm += "<td colspan='6' style='font-weight:bold' align='center'>";
                    MufForm += dr["ExpenseDetail"].ToString();
                    MufForm += "</td>";

                    MufForm += "</tr>";


                    //14th Row
                    MufForm += "<tr><td colspan='6'>&nbsp;</td></tr>";


                    //15th Row
                    MufForm += "<tr>";

                    MufForm += "<td>Cost of Activity</td>";

                    MufForm += "<td colspan='2' style='color:blue'>$ ";
                    MufForm += dr["Costed"].ToString();
                    MufForm += "</td>";

                    MufForm += "<td>VAT</td>";

                    MufForm += "<td colspan='2' style='color:blue'>$ ";
                    MufForm += dr["Vat"].ToString();
                    MufForm += "</td>";

                    MufForm += "</tr>";


                    //16th Row
                    MufForm += "<tr><td colspan='6'>&nbsp;</td></tr>";


                    //17th Row
                    MufForm += "<tr>";

                    MufForm += "<td>Paid To(Third Party Name)</td>";

                    MufForm += "<td colspan='5' style='color:blue'>";
                    MufForm += dr["ThirdPartyName"].ToString();
                    MufForm += "</td>";

                    MufForm += "</tr>";


                    //18th Row
                    MufForm += "<tr><td colspan='6'>&nbsp;</td></tr>";


                    //19th Row
                    MufForm += "<tr>";

                    MufForm += "<td>Ref No. Of Third Party</td>";

                    MufForm += "<td colspan='5' style='color:blue'>";
                    MufForm += dr["ThirdPartyInvoiceNo"].ToString();
                    MufForm += "</td>";

                    MufForm += "</tr>";


                    //20th Row
                    MufForm += "<tr><td colspan='6'>&nbsp;</td></tr>";


                    //21st Row
                    MufForm += "<tr>";

                    MufForm += "<td>Balance Funds</td>";

                    MufForm += "<td colspan='5' style='color:blue'>$ ";
                    MufForm +=  fundsAvail;
                    MufForm += "</td>";

                    MufForm += "</tr>";


                    //22nd Row
                    MufForm += "<tr><td colspan='6'>&nbsp;</td></tr>";


                    //23rd Row
                    MufForm += "<tr><td colspan='6'>Comments</td></tr>";


                    //24th Row
                    MufForm += "<tr>";

                    MufForm += "<td colspan='6' style='font-weight:bold' align='center'>";
                    MufForm += "";
                    MufForm += "</td>";

                    MufForm += "</tr>";


                    //25th Row
                    MufForm += "<tr><td colspan='6'>&nbsp;</td></tr>";


                    //26th Row
                    MufForm += "<tr>";

                    if (evnt == "Submit" && Convert.ToInt32(Session["currStatusId"]) == 6)
                        MufForm += "<td colspan='2' align='center'>" + myGlobal.loggedInUser() + "</td>";
                    else
                        MufForm += "<td colspan='2' align='center'>" + dr["submittedBy"].ToString() + "</td>";


                    if (evnt == "Submit" && Convert.ToInt32(Session["currStatusId"]) == 7)
                        MufForm += "<td colspan='2' align='center'>" + myGlobal.loggedInUser() + "</td>";
                    else
                        MufForm += "<td colspan='2' align='center'>" + dr["verifiedBy"].ToString() + "</td>";


                    if (evnt == "Submit" && Convert.ToInt32(Session["currStatusId"]) == 8)
                        MufForm += "<td colspan='2' align='center'>" + myGlobal.loggedInUser() + "</td>";
                    else
                        MufForm += "<td colspan='2' align='center'>" + dr["accruedBy"].ToString() + "</td>";

                    MufForm += "</tr>";


                    //27th Row
                    MufForm += "<tr>";

                    MufForm += "<td>Prepared By:</td>";
                    MufForm += "<td>&nbsp;</td>";

                    MufForm += "<td>Verified By:</td>";
                    MufForm += "<td>&nbsp;</td>";

                    MufForm += "<td>Accrued By:</td>";
                    MufForm += "<td>&nbsp;</td>";

                    MufForm += "</tr>";


                    //28th Row
                    MufForm += "<tr>";

                    MufForm += "<td colspan='2' >&nbsp;</td>";
                    MufForm += "<td colspan='2' >&nbsp;</td>";
                    MufForm += "<td colspan='2' >&nbsp;</td>";

                    MufForm += "</tr>";


                    //29th Row
                    MufForm += "<tr>";

                    MufForm += "<td colspan='2' align='center'>Authorised By:</td>";



                    if (evnt == "Submit" && Convert.ToInt32(Session["currStatusId"]) == 9)
                        MufForm += "<td colspan='4'>" + myGlobal.loggedInUser() + "</td>";
                    else
                        MufForm += "<td colspan='4'>" + dr["authorisedBy"].ToString() + "</td>";

                    MufForm += "</tr></table><br>";


                    mufDetailHtmlString += MufForm;

                    if (evnt == "Submit" && Convert.ToInt32(Session["currStatusId"]) == 9) //only when controler finance updates MUF
                    {
                        LstIntimation.Items.Add(dr["intimationMailId"].ToString() + "[" + dr["mufFilePath"].ToString() + "]" + MufForm);
                    }
                    
                    //LstIntimation.Items.Add(dr["intimationMailId"].ToString() + "[" + dr["mufFilePath"].ToString() + "]" + MufForm);

                    if(dr["mufFilePath"]!=DBNull.Value)
                     varmufFile = dr["mufFilePath"].ToString();

                    if (varmufFile != "")
                    {
                        if (mufFls.Trim() == "")
                            mufFls = Server.MapPath("~" + varmufFile);
                        else
                            mufFls += ";" + Server.MapPath("~" + varmufFile);
                    }

                    prevActivityCode = activityCode;
                    prevFundsAvail = fundsAvail;

                    if (mufUpdateSql != "")
                        mufUpdateSql += "; ";

                    if (Convert.ToInt32(Session["currStatusId"]) == 6) //updates muf record at this stages "Submittion stage"
                        mufUpdateSql += "update TblActivitiesDetails set lastModified='" + DateTime.Now.ToString() + "',mufFormFilledStatus='toAdminForApproval',submittedBy='" + myGlobal.loggedInUser() + "',submittedDate='" + DateTime.Now.ToString() + "' where autoIndex_MUFNO=" + dr["MUFNO"].ToString() + " and fk_activitySno in (select sno from dbo.TblActivities where fk_workFlowPlansId=" + refId + ")";

                    if (Convert.ToInt32(Session["currStatusId"]) == 7) //updates muf record at this stages "Verifying stage"
                    {
                        mufUpdateSql += "update TblActivitiesDetails set lastModified='" + DateTime.Now.ToString() + "',mufFormFilledStatus='toFinanceForApproval',verifiedBy='" + myGlobal.loggedInUser() + "',verifiedDate='" + DateTime.Now.ToString() + "' where autoIndex_MUFNO=" + dr["MUFNO"].ToString() + " and fk_activitySno in (select sno from dbo.TblActivities where fk_workFlowPlansId=" + refId + ")";
                        mufUpdateSql += "update TblActivitiesDetails set lastModified='" + DateTime.Now.ToString() + "',submittedBy='" + myGlobal.loggedInUser() + "',submittedDate='" + DateTime.Now.ToString() + "' where submittedBy is null and autoIndex_MUFNO=" + dr["MUFNO"].ToString() + " and fk_activitySno in (select sno from dbo.TblActivities where fk_workFlowPlansId=" + refId + ")";
                    }
                    if (Convert.ToInt32(Session["currStatusId"]) == 8) //updates muf record at this stages "Verifying stage"
                        mufUpdateSql += "update TblActivitiesDetails set lastModified='" + DateTime.Now.ToString() + "',mufFormFilledStatus='toHOFForApproval',accruedBy='" + myGlobal.loggedInUser() + "',accruedDate='" + DateTime.Now.ToString() + "' where autoIndex_MUFNO=" + dr["MUFNO"].ToString() + " and fk_activitySno in (select sno from dbo.TblActivities where fk_workFlowPlansId=" + refId + ")";

                    if (Convert.ToInt32(Session["currStatusId"]) == 9) //updates muf record at this stages "Authorised stage"
                        mufUpdateSql += "update TblActivitiesDetails set lastModified='" + DateTime.Now.ToString() + "',mufFormFilledStatus='Yes',authorisedBy='" + myGlobal.loggedInUser() + "',authorisedDate='" + DateTime.Now.ToString() + "' where autoIndex_MUFNO=" + dr["MUFNO"].ToString() + " and fk_activitySno in (select sno from dbo.TblActivities where fk_workFlowPlansId=" + refId + ")";

                }

                else //MUF null case means no expences on activity
                {
                    mufDetailHtmlString += getHtmlForNoExpenceActivity(dr["buName"].ToString(), dr["RddQuater"].ToString(), dr["vendorQuater"].ToString(), dr["AmtAccrued"].ToString(), dr["ActivityCode"].ToString(), dr["accrualFormNo"].ToString());
                }
            }
        }

        //last line of code
        lblMUFFormView.Text = mufDetailHtmlString;

    }

    private Boolean updateForMUFSubmissions()
    {
        Boolean workflg = false;

        if (Convert.ToInt32(Session["currStatusId"]) >= 6 && Convert.ToInt32(Session["currStatusId"]) <= 9)
        {
            try
            {
                if (mufUpdateSql != "")
                {
                    Db.constr = myGlobal.getRDDMarketingDBConnectionString();
                    Db.myExecuteSQL(mufUpdateSql);
                }
                workflg = true;
            }
            catch (Exception exp)
            {
                workflg = false;
            }
        }
        return workflg;
    }

    private string getHtmlForNoExpenceActivity(string pbu, string pRQ, string pVQ, string pAMTACCR, string pACTID, string pACCRID)
    {
        //No Expences As Yet
        string MufForm = "";

        MufForm = "<br /><table style='border-color:Black;width:90%' border='2'>";

        //Header Row
        MufForm += "<tr>";

        MufForm += "<td style='font-weight:bold;background-color:#D6DCE4' align='center' colspan='6'>MUF Form</td></tr>";
        //MufForm += "<td style='width:11%'>&nbsp;</td><td style='width:11%'>&nbsp;</td><td style='width:11%'>&nbsp;</td><td style='width:11%'>&nbsp;</td><td style='width:11%'>&nbsp;</td><td style='width:11%'>&nbsp;</td><td style='width:11%'>&nbsp;</td><td style='width:11%'>&nbsp;</td><td style='width:11%'>&nbsp;</td>";


        //2nd Row
        MufForm += "<tr><td colspan='6'>&nbsp;</td></tr>";

        //3rd Row
        MufForm += "<tr>";

        MufForm += "<td style='width:16%'>BU</td>";

        MufForm += "<td style='color:blue;width:16%'>";
        MufForm += pbu;
        MufForm += "</td>";

        MufForm += "<td style='width:16%'>RDD Qtr</td>";

        MufForm += "<td style='color:blue;width:16%'>";
        MufForm += pRQ;
        MufForm += "</td>";

        MufForm += "<td style='width:16%'>Vendor Qtr</td>";

        MufForm += "<td style='color:blue;width:16%'>";
        MufForm += pVQ;
        MufForm += "</td>";

        MufForm += "</tr>";


        //5th Row
        MufForm += "<tr><td colspan='6'>&nbsp;</td></tr>";

        //6th Row
        MufForm += "<tr>";

        MufForm += "<td>Total Amount Accrued</td>";

        MufForm += "<td colspan='2' style='color:blue'>$ ";
        MufForm += pAMTACCR;
        MufForm += "</td>";

        MufForm += "<td>&nbsp;</td>";

        //MufForm += "<td>$nbsp;</td>";

        MufForm += "<td colspan='2' style='color:blue'>";
        MufForm += "";
        MufForm += "</td>";

        //MufForm += "<td>$nbsp;</td>";

        MufForm += "</tr>";

        //10th Row
        MufForm += "<tr>";

        MufForm += "<td>Activity ID</td>";

        MufForm += "<td colspan='2' style='color:blue'>";
        MufForm += pACTID;
        MufForm += "</td>";

        MufForm += "<td>accrual form no.</td>";

        MufForm += "<td colspan='2' style='color:blue'>";
        MufForm += pACCRID;
        MufForm += "</td>";

        MufForm += "</tr>";

        //12th Row
        MufForm += "<tr><td colspan='6'>Expense Detail</td></tr>";


        //13th Row
        MufForm += "<tr>";

        MufForm += "<td colspan='6' style='font-weight:bold' align='center'>";
        MufForm += "No Expences On this Activity";
        MufForm += "</td>";

        MufForm += "</tr>";


        //14th Row
        MufForm += "<tr><td colspan='6'>&nbsp;</td></tr>";
        MufForm += "</table>";

        return MufForm;
    }

    private Boolean chkExecutionCompleted_IsValid()
    {
        Boolean sFlg = false;

        sql = "select * from dbo.TblActivitiesDetails where fk_ActivitySno in (select sno from dbo.TblActivities where fk_workFlowPlansId=" + refId + ") and mufFormFilledStatus='No'";

        Db.constr = myGlobal.getRDDMarketingDBConnectionString();
        drd = Db.myGetReader(sql);

        if (drd.HasRows)
            sFlg = false;
        else
            sFlg = true;

        drd.Close();

        return sFlg;
    }

    protected void Grid2_RowDataBound(object sender, GridViewRowEventArgs e)
    {
    //  if(e.Row.RowIndex>0)
    //  {
    //    LinkButton lk = (LinkButton) Grid2.Rows[e.Row.RowIndex].FindControl("LinkEdit");
    //}
        //PostBackTrigger pt = new PostBackTrigger();
        //if(e.Row.RowIndex>0)
        //if (Grid2.Rows[e.Row.RowIndex].FindControl("LinkEdit") != null)
        //{
        //    pt.ControlID = Grid2.Rows[e.Row.RowIndex].FindControl("LinkEdit").UniqueID;
        //    UpdatePanel1.Triggers.Add(pt);
        //}
    }
}