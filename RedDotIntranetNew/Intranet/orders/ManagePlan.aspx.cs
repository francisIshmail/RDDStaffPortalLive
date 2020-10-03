using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Data.SqlClient;

public partial class Intranet_orders_ManagePlan : System.Web.UI.Page
{
    string pid;
    int deletionValidTillStatus = 2;

    protected void Page_Load(object sender, EventArgs e)
    {
        pid = Request.QueryString["pid"].ToString(); //process type id
        //check for valid user
        string[] userRole;
        userRole = myGlobal.loggedInRoleList();


        if (!userRole.Contains("MARKETINGADMIN"))
        //if (!userRole.Contains("MARKETINGEXECUTIVE"))
        {
            lblmsg.Text = "Access Permission Denied for this page";
            btnnew.Enabled = false;
            btnsave.Enabled = false;
            btncancel.Enabled = false;
            btndel.Enabled = false;
            return;
        }

        btndel.Attributes.Add("onClick", "return getConfirmation();");

        if (!IsPostBack)
        {
            for (int y = 0; y < 5; y++)
            {
                int x = DateTime.Now.Year;
                ddlyear.Items.Add((DateTime.Now.Year-y).ToString());
                ddlBaseYear.Items.Add((DateTime.Now.Year - y).ToString());
            }

            Db.LoadDDLsWithCon(ddlVendor, "select autoindex,buName from VendorBUDef order by buName", "buName", "autoindex", myGlobal.getRDDMarketingDBConnectionString());

            bindDDLPlans();
            if (ddlPlans.Items.Count > 0 && ddlPlans.SelectedIndex >= 0)
            {
                bindFields();
                ddlVendor.Enabled = false;
            }
            else
            {
                txtApprovedAmt.Text = "0";
                txtApprovedDate.Text = DateTime.Now.ToString("MM-dd-yyyy");
                txtDeadLineDate.Text = DateTime.Now.ToString("MM-dd-yyyy");
                txtActualCost.Text = "0";
                ddlVendor.Enabled = true;
            }
        }
        lblmsg.Text = "";

        //if(lblautoindex.Text.Trim()=="")  //new case
        //    ddlVendor.Enabled = true;
        //else
        //    ddlVendor.Enabled = false;
    }

    protected void ddlBaseYear_SelectedIndexChanged(object sender, EventArgs e)
    {
        bindDDLPlans();
        if (ddlPlans.Items.Count > 0 && ddlPlans.SelectedIndex >= 0)
        {
            bindFields();
            ddlVendor.Enabled = false;
        }
        else
        {
            clearFields();
            ddlVendor.Enabled = true;
        }
    }

    private void bindDDLPlans()
        {
        //Db.LoadDDLsWithCon(ddlPlans, "select autoindex,planVendor + '-' + planQuater + '-' + convert(varchar(4),planYear) + '-' + vendorActivityId + '-' + convert(varchar(20),VendorApprovedAmount) as planDetail from workFlowPlans", "planDetail", "autoindex", myGlobal.getRDDMarketingDBConnectionString());
            Db.LoadDDLsWithCon(ddlPlans, "select w.autoindex,v.buName + '-' + w.planQuater + '-' + convert(varchar(4),w.planYear) + '-' + w.vendorActivityId + '-' + convert(varchar(20),w.VendorApprovedAmount) as planDetail from workFlowPlans as w join VendorBUDef as v on w.fk_VendorBU=v.autoIndex where w.planYear=" + ddlBaseYear.SelectedItem.Text, "planDetail", "autoindex", myGlobal.getRDDMarketingDBConnectionString());
        lblcnt.Text = ddlPlans.Items.Count.ToString();
    }

    private void clearFields()
    {
        lblautoindex.Text = "";
        //txtVendor.Text = "";
        txtActivityId.Text = "";
        txtQuarter.Text = "";
        txtVendorQuarter.Text = "";
        //txtyear.Text = "";
        txtApprovedAmt.Text = "0";
        txtApprovedDate.Text = DateTime.Now.ToString("MM-dd-yyyy");
        txtDeadLineDate.Text = DateTime.Now.ToString("MM-dd-yyyy");
        txtActualCost.Text = "0";
        txtDesc.Text = "";

        //string qry = "select StatusName from Status where StatusID=1";

        string qry = "select processStatusName from  dbo.processStatus where processStatusID=1 and fk_processId=" + pid;
        Db.constr = myGlobal.getRDDMarketingDBConnectionString();
        SqlDataReader dr = Db.myGetReader(qry);
        if (dr.HasRows)
        {
            dr.Read();
            lblStatus.Text = dr["processStatusName"].ToString();
            dr.Close();
        }

        lblCurrStatusId.Text = "1";

        lblLastModified.Text = DateTime.Now.ToString("MM-dd-yyyy");
        txtcomments.Text = "New Marketing Plan";
        lblFile.Text = "";
        lblFileVendor.Text = "";

        ddlyear.SelectedIndex = -1;
        ddlyear.Items.FindByValue(DateTime.Now.Year.ToString()).Selected = true;
    }
    protected void btnnew_Click(object sender, EventArgs e)
    {
        if(ddlPlans.Items.Count>0)
         clearFields();

        //ddlyear.SelectedIndex = -1;
        //ddlyear.Items.FindByValue(DateTime.Now.Year.ToString()).Selected = true;

        lblAddEdit.Text = "New";
        btnnew.Enabled = false;
        btnsave.Enabled = true;
        btncancel.Enabled = true;
        btndel.Enabled = false;
        ddlVendor.Enabled = true;
    }
    protected void btndel_Click(object sender, EventArgs e)
    {

        // Begin
        //    declare @refId int;
        //    declare @processId int;
        //    declare @requestId int;

        //    set @refId=80001
        //    set @processId=10031
        //    select @requestId=processRequestId from dbo.processRequest where refId=@refId and fk_processId=@processId

        //    delete from dbo.workFlowPlans where autoindex=@refId 
        //    delete from dbo.processRequest where refId=@refId and fk_processId=@processId
        //    delete from dbo.processStatusTrack where fk_processRequestId=@requestId and fk_processId=@processId
        //    delete from dbo.uploadTrack where fk_refId=@refId and fk_processId=@processId
        //End

        //single Line
        //Begin declare @refId int; declare @processId int; declare @requestId int; set @refId=80001; set @processId=10031; select @requestId=processRequestId from dbo.processRequest where refId=@refId and fk_processId=@processId ; delete from dbo.workFlowPlans where autoindex=@refId ; delete from dbo.processRequest where refId=@refId and fk_processId=@processId ; delete from dbo.processStatusTrack where fk_processRequestId=@requestId and fk_processId=@processId ; delete from dbo.uploadTrack where fk_refId=@refId and fk_processId=@processId End

        if (Convert.ToUInt32(lblCurrStatusId.Text) > deletionValidTillStatus)
        {
            lblmsg.Text = "Request Denied !!! Invalid Attempt to delete selected plan as it has gone past (Allocation Stage), can't delete this plan.  ";
            return;
        }

        string qry;
        //qry = "delete from workFlowPlans where autoindex=" + ddlPlans.SelectedValue.ToString();
        qry = "Begin declare @refId int; declare @processId int; declare @requestId int; set @refId=" + ddlPlans.SelectedValue.ToString() + "; set @processId=" + pid + "; select @requestId=processRequestId from dbo.processRequest where refId=@refId and fk_processId=@processId ; delete from dbo.workFlowPlans where autoindex=@refId ; delete from dbo.processRequest where refId=@refId and fk_processId=@processId ; delete from dbo.processStatusTrack where fk_processRequestId=@requestId and fk_processId=@processId ; delete from dbo.uploadTrack where fk_refId=@refId and fk_processId=@processId End";
        Db.constr = myGlobal.getRDDMarketingDBConnectionString();
        Db.myExecuteSQL(qry);
        
        //last step
        bindDDLPlans();
        if (ddlPlans.Items.Count > 0 && ddlPlans.SelectedIndex >= 0)
            bindFields();
        else
        {
            clearFields();
            btndel.Enabled = false;
            btnsave.Enabled = false;
            lblAddEdit.Text = "";
        }
    }
    protected void btncancel_Click(object sender, EventArgs e)
    {
        if (ddlPlans.Items.Count > 0 && ddlPlans.SelectedIndex >= 0)
            bindFields();
        else
        {
            lblAddEdit.Text = "";
            btnnew.Enabled = true;
            btncancel.Enabled = false;
            btnsave.Enabled = false;
            btndel.Enabled = false;
        }
        
    }
    protected void btnsave_Click(object sender, EventArgs e)
    {
        string vendorActID ;

        //if (txtVendor.Text.Trim() == "")
        //{
        //    lblmsg.Text = "It's Mandatory to supply value for Vendor/BU field.  ";
        //    return;
        //}

        vendorActID = txtActivityId.Text.Trim();

        if (txtQuarter.Text.Trim() == "")
        {
            lblmsg.Text = "It's Mandatory to supply value for RDD Quator field.  ";
            return;
        }
        if (txtVendorQuarter.Text.Trim() == "")
        {
            lblmsg.Text = "It's Mandatory to supply value for Vendor Quator field.  ";
            return;
        }
        //if (!Util.isValidNumber(txtyear.Text))
        //{
        //    lblmsg.Text = "Please supply a valid numeric value for year field.  ";
        //    return;
        //}

       if(!Util.isValidDecimalNumber(txtApprovedAmt.Text))
       {
           lblmsg.Text = "Please supply a valid numeric value for Vendor Approved Amount field.  ";
           return;
       }

       if (!Util.isValidDecimalNumber(txtActualCost.Text))
       {
           lblmsg.Text = "Please supply a valid numeric value for Actual Cost Amount field.  ";
           return;
       }

       if (Convert.ToDecimal(txtApprovedAmt.Text) <= 0)
       {
           lblmsg.Text = "Please supply value greater than 0 for Vendor Approved Amount field.  ";
           return;
       }

       if (Convert.ToDecimal(txtActualCost.Text) <= 0)
       {
           lblmsg.Text = "Please supply value greater than 0 for Actual Cost Amount field.  ";
           return;
       }

       if (!Util.IsValidDate(txtApprovedDate.Text))
       {
           lblmsg.Text = "Please supply a valid date (MM-dd-yyyy) value for Vendor Approval field.  ";
           return;
       }

       if (!Util.IsValidDate(txtDeadLineDate.Text))
       {
           lblmsg.Text = "Please supply a valid date (MM-dd-yyyy) value for DeadLine Date field.  ";
           return;
       }

       if (Convert.ToDateTime(txtDeadLineDate.Text) <= Convert.ToDateTime(txtApprovedDate.Text))
       {
           lblmsg.Text = "Error ! Deadline date has to be later date than Vendor Approved Date.";
           return;
       }

       if (!FileUpload1.HasFile && lblFile.Text.Trim()=="")
        {
            lblmsg.Text = "It's Mandatory to upload Actual plan file (excel format) for this plan.  ";
            return;
        }

       if (!FileUpload2.HasFile && lblFileVendor.Text.Trim() == "")
       {
           lblmsg.Text = "It's Mandatory to upload Vendor plan file (excel format) for this plan.  ";
           return;
       }

        if (txtDesc.Text.Trim() == "")
        {
            lblmsg.Text = "It's Mandatory to supply value for description field.  ";
            return;
        }

        if (txtcomments.Text.Trim() == "")
        {
            lblmsg.Text = "It's Mandatory to supply value for comments field.  ";
            return;
        }


        ///////////////check for invalid character ////////////////////////////////////////////////////////////
        if (txtActivityId.Text.Trim().IndexOf("'") >= 0)
        {
            lblmsg.Text = "Invalid Character occurs ' in field Activity ID , Char( ' ) not supported.";
            return;
        }
        if (txtVendorQuarter.Text.Trim().IndexOf("'") >= 0)
        {
            lblmsg.Text = "Invalid Character occurs ' in field Vendor Quater, Char( ' ) not supported.";
            return;
        }
        if (txtQuarter.Text.Trim().IndexOf("'") >= 0)
        {
            lblmsg.Text = "Invalid Character occurs ' in field RDD Quater, Char( ' ) not supported.";
            return;
        }
        if (txtDesc.Text.Trim().IndexOf("'") >= 0)
        {
            lblmsg.Text = "Invalid Character occurs ' in field Description, Char( ' ) not supported.";
            return;
        }
        if (txtcomments.Text.Trim().IndexOf("'") >= 0)
        {
            lblmsg.Text = "Invalid Character occurs ' in field Comments, Char( ' ) not supported.";
            return;
        }


        

        ///////////////check for invalid character ////////////////////////////////////////////////////////////

        string qry;
        long poId=0;

        //qry = "select StatusName from Status where StatusID=1";
        qry = "select processStatusName from  dbo.processStatus where processStatusID=1 and fk_processId=" + pid;

        Db.constr = myGlobal.getRDDMarketingDBConnectionString();
        SqlDataReader dr= Db.myGetReader(qry);
        if (dr.HasRows)
        {
            dr.Read();
            lblStatus.Text = dr["processStatusName"].ToString();
            dr.Close();
        }
       


        //string pth = "/excelFileUpload/Marketing/";
        //string flsavePth = Server.MapPath("~" + pth) + FileUpload1.FileName;

        //if (FileUpload1.HasFile)
        //{
        //    lblFile.Text = pth + FileUpload1.FileName;
        //    FileUpload1.SaveAs(flsavePth);
        //}

        String pth, dirPhyPth, flsavePth, flsavePthvendor;
        pth = "/excelFileUpload/Marketing/";

        string cmts = "";
        int sts, esc;
        
        //SqlDataReader sdrd;
        //Db.constr = myGlobal.getRDDMarketingDBConnectionString(); 
        //sdrd = Db.myGetReader("select nextprocessStatusID,nextRole from dbo.processStatus where processStatusID=1 and fk_processId=" + pid);
        //sdrd.Read();
        //sts = Convert.ToInt32(sdrd["nextprocessStatusID"]);
        //esc = Convert.ToInt32(sdrd["nextRole"]);
        //sdrd.Close();

        sts = 1;
        esc = 9;

        if (lblAddEdit.Text == "New")
        {
            cmts = "New Plan";
            qry = string.Format("insert into workFlowPlans(fk_VendorBU,vendorActivityId,planQuater,vendorQuater,planYear,VendorApprovedAmount,vendorApprovalDate,planActualCost,planXlsFileNamePath,vendorplanXlsFileNamePath,planDescription,status,lastModified,deadlineDate) "
            + "VALUES({0},'{1}','{2}','{3}',{4},{5},'{6}',{7},'{8}','{9}','{10}','{11}','{12}','{13}')", ddlVendor.SelectedValue.ToString(), vendorActID, txtQuarter.Text.Trim(), txtVendorQuarter.Text.Trim(), ddlyear.SelectedValue.ToString(), txtApprovedAmt.Text.Trim(), txtApprovedDate.Text.Trim(), txtActualCost.Text.Trim(), lblFile.Text, lblFileVendor.Text, txtDesc.Text.Trim(), lblStatus.Text.Trim(), DateTime.Now.ToString("MM-dd-yyyy hh:mm:ss tt"), txtDeadLineDate.Text.Trim());
            Db.constr = myGlobal.getRDDMarketingDBConnectionString();
            //Db.myExecuteSQL(qry);
            poId = Db.myExecuteSQLReturnLatestAutoID(qry);
            //get latest id
            //qry = "select max(autoindex) from workFlowPlans";
            //Db.constr = myGlobal.getRDDMarketingDBConnectionString();
            //int idx = Db.myExecuteScalar(qry);



            pth = pth + "planId" + poId.ToString() + "/";

            dirPhyPth = Server.MapPath("~" + pth);

            if (!System.IO.Directory.Exists(dirPhyPth))
            {
                System.IO.Directory.CreateDirectory(dirPhyPth);
            }

            if (FileUpload1.HasFile)
            {
                flsavePth = Server.MapPath("~" + pth) + FileUpload1.FileName;
                lblFile.Text = pth + FileUpload1.FileName;
                FileUpload1.SaveAs(flsavePth);
            }

            if (FileUpload2.HasFile)
            {
                flsavePthvendor = Server.MapPath("~" + pth) + FileUpload2.FileName;
                lblFileVendor.Text = pth + FileUpload2.FileName;
                FileUpload2.SaveAs(flsavePthvendor);
            }

            string refval = ddlVendor.SelectedItem.Text + "-" + vendorActID + "-" + txtVendorQuarter.Text.Trim() + "-" + ddlyear.SelectedItem.Text + "-$" + txtApprovedAmt.Text.Trim();
            //update processrequest table 
            qry = string.Format("insert into processRequest(refId,refValue,comments,fk_StatusId,fk_EscalateLevelId,ByUser,lastModified,fk_processId) "
            + "VALUES({0},'{1}','{2}',{3},{4},'{5}','{6}',{7})", poId.ToString(), refval, txtcomments.Text.Trim(), sts.ToString(), esc.ToString(), myGlobal.loggedInUser(), DateTime.Now.ToString("MM-dd-yyyy hh:mm:ss tt"), pid);

            qry = qry + "; update  workFlowPlans set planXlsFileNamePath='" + lblFile.Text + "',vendorplanXlsFileNamePath='" + lblFileVendor.Text + "' where autoindex=" + poId.ToString();
            Db.constr = myGlobal.getRDDMarketingDBConnectionString();
            Db.myExecuteSQL(qry);

            //fill track log
                               //insert into processStatusTrack(fk_processRequestId,fk_statusId,fk_EscalateLevelId,lastUpdatedBy,StatusAccept,lastModified,comments,fk_processId)

            qry = "select processRequestId from processRequest where refId=" + poId.ToString();
            Db.constr = myGlobal.getRDDMarketingDBConnectionString();
            int rqidx = Db.myExecuteScalar(qry);

            qry = string.Format("insert into processStatusTrack(fk_processRequestId,fk_StatusID,fk_escalateLevelId,lastUpdatedBy,StatusAccept,lastModified,comments,fk_processId,action_StatusID) "
             + " VALUES({0},{1},{2},'{3}','{4}','{5}','{6}',{7},{8})", rqidx.ToString(), sts.ToString(), esc.ToString(), myGlobal.loggedInUser(), cmts, DateTime.Now.ToString("MM-dd-yyyy hh:mm:ss tt"), txtcomments.Text.Trim(), pid, sts);
            Db.constr = myGlobal.getRDDMarketingDBConnectionString();
            Db.myExecuteSQL(qry);

            lblmsg.Text = "New Plan updated successfully";
        }
        else
        {
            cmts = "Plan Edited";
            pth = pth + "planId" + lblautoindex.Text.ToString() + "/";

            if (FileUpload1.HasFile)
            {
                flsavePth = Server.MapPath("~" + pth) + FileUpload1.FileName;
                lblFile.Text = pth + FileUpload1.FileName;
                FileUpload1.SaveAs(flsavePth);
            }

            if (FileUpload2.HasFile)
            {
                flsavePthvendor = Server.MapPath("~" + pth) + FileUpload2.FileName;
                lblFileVendor.Text = pth + FileUpload2.FileName;
                FileUpload2.SaveAs(flsavePthvendor);
            }

            qry = string.Format("update workFlowPlans set fk_VendorBU={0},vendorActivityId='{1}',planQuater='{2}',planYear={3},VendorApprovedAmount={4},vendorApprovalDate='{5}',planActualCost={6},planXlsFileNamePath='{7}',planDescription='{8}',status='{9}',lastModified='{10}',vendorplanXlsFileNamePath='{11}',VendorQuater='{12}',deadlineDate='{13}'"
                + " where autoindex=" + lblautoindex.Text, ddlVendor.SelectedValue.ToString(), vendorActID, txtQuarter.Text.Trim(), ddlyear.SelectedValue.ToString(), txtApprovedAmt.Text.Trim(), txtApprovedDate.Text.Trim(), txtActualCost.Text.Trim(), lblFile.Text, txtDesc.Text.Trim(), lblStatus.Text.Trim(), DateTime.Now.ToString("MM-dd-yyyy hh:mm:ss tt"), lblFileVendor.Text, txtVendorQuarter.Text, txtDeadLineDate.Text.Trim());
            Db.constr = myGlobal.getRDDMarketingDBConnectionString();
            Db.myExecuteSQL(qry);

            //fill track log
            qry = "select processRequestId from processRequest where refId=" + lblautoindex.Text;
            Db.constr = myGlobal.getRDDMarketingDBConnectionString();
            int rqidx = Db.myExecuteScalar(qry);

            qry = string.Format("insert into processStatusTrack(fk_processRequestId,fk_StatusID,fk_escalateLevelId,lastUpdatedBy,StatusAccept,lastModified,comments,fk_processId,action_StatusID) "
             + "VALUES({0},{1},{2},'{3}','{4}','{5}','{6}',{7},{8})", rqidx.ToString(), sts.ToString(), esc.ToString(), myGlobal.loggedInUser(), cmts, DateTime.Now.ToString("MM-dd-yyyy hh:mm:ss tt"), txtcomments.Text.Trim(), pid, sts);
            
            //in editing case we are not changing/updating status to  first level , just updating latest comments 
            qry += " ; update  processRequest set comments='" + txtcomments.Text.Trim() + "'  where refid=" + lblautoindex.Text + " and fk_processId=" + pid; 

            Db.constr = myGlobal.getRDDMarketingDBConnectionString();
            Db.myExecuteSQL(qry);

            lblmsg.Text = "Plan updated successfully";
        }
        
        

        //btnnew.Enabled = true;
        //btnsave.Enabled = false;
        //btncancel.Enabled = false;

        bindDDLPlans();
        if (ddlPlans.Items.Count > 0 && ddlPlans.SelectedIndex >= 0)
            bindFields();
    }

   
    protected void bindFields()
    {
        string qry;
        qry = "select w.*,p.comments,p.fk_StatusId from dbo.workFlowPlans as w join  processRequest as p on w.autoindex=p.refId  where w.autoindex=" + ddlPlans.SelectedValue.ToString();

        Db.constr = myGlobal.getRDDMarketingDBConnectionString();
        SqlDataReader drd = Db.myGetReader(qry);

        if (drd.HasRows)
        {
            //(fk_VendorBU,vendorActivityId,planQuater,planYear,VendorApprovedAmount,vendorApprovalDate,planActualCost,planXlsFileNamePath,planDescription,status,lastModified)
            
            drd.Read();
            
            lblautoindex.Text = drd["autoindex"].ToString();
           
            ddlVendor.Enabled = false;

            
            //txtVendor.Text = drd["planVendor"].ToString();
            ddlVendor.SelectedIndex = -1;
            ddlVendor.Items.FindByValue(drd["fk_VendorBU"].ToString()).Selected = true;
            
            txtActivityId.Text = drd["vendorActivityId"].ToString();
            txtQuarter.Text = drd["planQuater"].ToString();
            txtVendorQuarter.Text = drd["vendorQuater"].ToString();
            //txtyear.Text = drd["planYear"].ToString();
            ddlyear.SelectedIndex = -1;
            ddlyear.Items.FindByValue(drd["planYear"].ToString()).Selected = true;
            
            txtApprovedAmt.Text = drd["VendorApprovedAmount"].ToString();

            DateTime dts;
            dts = Convert.ToDateTime(drd["vendorApprovalDate"]);
            txtApprovedDate.Text = dts.ToString("MM-dd-yyyy");

            dts = Convert.ToDateTime(drd["deadlineDate"]);
            txtDeadLineDate.Text = dts.ToString("MM-dd-yyyy");

            txtActualCost.Text = drd["planActualCost"].ToString();
            lblFile.Text = drd["planXlsFileNamePath"].ToString();
            lblFileVendor.Text = drd["vendorplanXlsFileNamePath"].ToString();
            txtDesc.Text = drd["planDescription"].ToString();
            lblStatus.Text = drd["status"].ToString();
            lblCurrStatusId.Text = drd["fk_StatusId"].ToString();
            txtcomments.Text = drd["comments"].ToString();
            lblLastModified.Text = drd["lastModified"].ToString();
            drd.Close();
            lblAddEdit.Text = "Editable";
            btnnew.Enabled = true;
            btncancel.Enabled = false;
            btnsave.Enabled = true;
            btndel.Enabled = true;
        }
        else
        {
            lblAddEdit.Text = "";
            btnnew.Enabled = true;
            btncancel.Enabled = false;
            btnsave.Enabled = false;
            btndel.Enabled = false;
        }
    }
    protected void ddlPlans_SelectedIndexChanged(object sender, EventArgs e)
    {
        bindFields();
        lblAddEdit.Text = "Editable";
        //btnsave.Enabled = true;


    }
   
}