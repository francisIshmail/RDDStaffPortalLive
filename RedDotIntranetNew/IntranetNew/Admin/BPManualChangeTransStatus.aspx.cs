using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Data;

public partial class IntranetNew_Admin_BPManualChangeTransStatus : System.Web.UI.Page
{
    protected void Page_Load(object sender, EventArgs e)
    {
        try
        {
            if (!IsPostBack)
            {
                Db.constr = myGlobal.getAppSettingsDataForKey("tejSAP");
                int count = Db.myExecuteScalar("Select COUNT(*) from dbo.MenuWiseForms t0 Join dbo.UserAuthorization t1 on t0.MenuId=t1.MenuId and t1.MembershipUserName='" + myGlobal.loggedInUser() + "' And t0.FormURL='BPManualChangeTransStatus.aspx' And t1.IsActive=1");
                if (count > 0)
                {
                    BindGrid();
                }
                else
                {
                    Response.Redirect("Default.aspx?UserAccess=0&FormName=To Manually Change Transactional Status");
                }
            }
        }
        catch (Exception ex)
        {
            lblMsg.Text = " Error occured in PageLoad : "+ ex.Message;
        }

    }

    private void BindGrid()
    {
        try
        {
            String summarySQL = string.Empty;
            String sortExp = (String)ViewState["sortExpression"];
            String sortDir = (String)ViewState["sortDirection"];

            if (sortExp != null && sortDir != null)
            {
                summarySQL = "select * from dbo.BPStatus_ManuallyTStatusChangeAuthUsers Where isnull(IsDeleted,0)=0 Order by " + sortExp + " " + sortDir;
            }
            else
            {
                summarySQL = "select * from dbo.BPStatus_ManuallyTStatusChangeAuthUsers Where isnull(IsDeleted,0)=0 ";
            }

            Db.constr = myGlobal.getAppSettingsDataForKey("tejSAP");
            DataTable DT = Db.myGetDS(summarySQL).Tables[0];
            if (DT.Rows.Count > 0)
            {
                GRVManulStatusChange.DataSource = DT;
                GRVManulStatusChange.DataBind();
            }
            else
            {
                DataRow dr = DT.NewRow();  // add new row
                dr["Id"] = 0;
                dr["DBName"] = "--Select--";
                dr["FromStatus"] = "Active";
                dr["ToStatus"] = "Dormant";
                dr["CanCA"] = false;
                dr["CanCM"] = false;
                dr["CanCR"] = false;
                dr["CanHOF"] = false;
                dr["CanHOIS"] = false;
                dr["CanCOO"] = false;
                dr["CanCEO"] = false;
                DT.Rows.Add(dr);
                GRVManulStatusChange.DataSource = DT;
                GRVManulStatusChange.DataBind();
            }
        }
        catch (Exception ex)
        {
            lblMsg.Text = " Error occured in BindGrid : " + ex.Message;
        }
    }

    protected void GRVManulStatusChange_RowCommand(object sender, GridViewCommandEventArgs e)
    {
        try
        {
            lblMsg.Text = "";

            if (e.CommandName == "Add")
            {
                DropDownList ddlDBName = (DropDownList)GRVManulStatusChange.FooterRow.FindControl("ddlFooterDBName");
                DropDownList ddlFromStatus = (DropDownList)GRVManulStatusChange.FooterRow.FindControl("ddlFooterFromStatus");
                DropDownList ddlToStatus = (DropDownList)GRVManulStatusChange.FooterRow.FindControl("ddlFooterToStatus");
                CheckBox chkCanCA = (CheckBox)GRVManulStatusChange.FooterRow.FindControl("chkFooterCanCA");
                CheckBox chkCanCM = (CheckBox)GRVManulStatusChange.FooterRow.FindControl("chkFooterCanCM");
                CheckBox chkCanCR = (CheckBox)GRVManulStatusChange.FooterRow.FindControl("chkFooterCanCR");
                CheckBox chkCanHOF = (CheckBox)GRVManulStatusChange.FooterRow.FindControl("chkFooterCanHOF");
                CheckBox chkCanHOIS = (CheckBox)GRVManulStatusChange.FooterRow.FindControl("chkFooterCanHOIS");
                CheckBox chkCanCOO = (CheckBox)GRVManulStatusChange.FooterRow.FindControl("chkFooterCanCOO");
                CheckBox chkCanCEO = (CheckBox)GRVManulStatusChange.FooterRow.FindControl("chkFooterCanCEO");

                if (ddlDBName.SelectedItem.Text == "--Select--")
                {
                    lblMsg.Text = "Please select Database";
                    return;
                }

                if (ddlFromStatus.SelectedItem.Text == "--Select--")
                {
                    lblMsg.Text = "Please select From Status";
                    return;
                }
                if (ddlToStatus.SelectedItem.Text == "--Select--")
                {
                    lblMsg.Text = "Please select To Status";
                    return;
                }

                if (ddlToStatus.SelectedItem.Text == ddlFromStatus.SelectedItem.Text)
                {
                    lblMsg.Text = "From Status and To Status can not be same.";
                    return;
                }
                /// Allow only Active to CLosed or Blocked
                if (ddlFromStatus.SelectedItem.Text == "Active" && (ddlToStatus.SelectedItem.Text == "Soft hold" || ddlToStatus.SelectedItem.Text == "Hard hold" || ddlToStatus.SelectedItem.Text == "Dormant"))
                {
                    lblMsg.Text = "You can not change customer status to " + ddlToStatus.SelectedItem.Text + ". You can only change ACTIVE customer to BLOCKED or CLOSED.";
                    return;
                }

                /// Allow only Dormant to Active OR Closed
                if (ddlFromStatus.SelectedItem.Text == "Dormant" && (ddlToStatus.SelectedItem.Text == "Soft hold" || ddlToStatus.SelectedItem.Text == "Hard hold" || ddlToStatus.SelectedItem.Text == "Blocked"))
                {
                    lblMsg.Text = "You can not change customer status to " + ddlToStatus.SelectedItem.Text + ". You can only change DORMANT customer to ACTIVE or CLOSED.";
                    return;
                }

                // Allow only Soft hold to Active, Blocked & Closed
                if (ddlFromStatus.SelectedItem.Text == "Soft hold" && (ddlToStatus.SelectedItem.Text == "Hard hold" || ddlToStatus.SelectedItem.Text == "Dormant" || ddlToStatus.SelectedItem.Text == "Soft hold"))
                {
                    lblMsg.Text = "You can not change customer status to " + ddlToStatus.SelectedItem.Text + ". You can only change SOFT HOLD customer to ACTIVE or BLOCKED or CLOSED.";
                    return;
                }
                // Allow only Hard hold to Active, Blocked and Closed
                if (ddlFromStatus.SelectedItem.Text == "Hard hold" && (ddlToStatus.SelectedItem.Text == "Soft hold" || ddlToStatus.SelectedItem.Text == "Hard hold" || ddlToStatus.SelectedItem.Text == "Dormant"))
                {
                    lblMsg.Text = "You can not change customer status to " + ddlToStatus.SelectedItem.Text + ". You can only change HARD HOLD customer to ACTIVE or BLOCKED or CLOSED.";
                    return;
                }
                // Allow only Blocked to Active and Closed
                if (ddlFromStatus.SelectedItem.Text == "Blocked" && (ddlToStatus.SelectedItem.Text == "Soft hold" || ddlToStatus.SelectedItem.Text == "Hard hold" || ddlToStatus.SelectedItem.Text == "Dormant"))
                {
                    lblMsg.Text = "You can not change customer status to " + ddlToStatus.SelectedItem.Text + ". You can only change BLOCKED customer to ACTIVE OR CLOSED.";
                    return;
                }

                Db.constr = myGlobal.getAppSettingsDataForKey("tejSAP");
                int NoOfRec = Db.myExecuteScalar("select count(*) NoOfRec from dbo.BPStatus_ManuallyTStatusChangeAuthUsers Where FromStatus='" + ddlFromStatus.SelectedItem.Text + "' And ToStatus='" + ddlToStatus.SelectedItem.Text + "' And DBName='" + ddlDBName.SelectedItem.Text + "' And IsDeleted=0 ");
                if (NoOfRec > 0)
                {
                    lblMsg.Text = "Record already exist to change customer status from " + ddlFromStatus.SelectedItem.Text + " to " + ddlToStatus.SelectedItem.Text + " for "+ddlDBName.SelectedItem.Text+" database.";
                    return;
                }
                else
                {
                    string alertqry = "";
                    string qry = " Insert into dbo.BPStatus_ManuallyTStatusChangeAuthUsers (DBName,FromStatus,ToStatus,CanCA,CanCM,CanCR,CanHOF,CanHOIS,CanCOO,CanCEO,CreatedOn,CreatedBy ) Values ( '" + ddlDBName.SelectedItem.Text + "','" + ddlFromStatus.SelectedItem.Text + "','" + ddlToStatus.SelectedItem.Text + "'";
                    alertqry = "Insert into dbo.BPStatus_ManuallyTStatusChangeAlerts (DBName,FromStatus,ToStatus,AlertToCA,AlertToCM,AlertToCR,AlertToHOF,AlertToHOIS,AlertToCOO,AlertToCEO,CreatedOn,CreatedBy ) Values ( '" + ddlDBName.SelectedItem.Text + "', '" + ddlFromStatus.SelectedItem.Text + "','" + ddlToStatus.SelectedItem.Text + "'";
                    if (chkCanCA.Checked)
                    {
                        qry = qry + ",1";
                        alertqry = alertqry + ",1";
                    }
                    else
                    {
                        qry = qry + ",0";
                        alertqry = alertqry + ",0";
                    }
                    if (chkCanCM.Checked)
                    {
                        qry = qry + ",1";
                        alertqry = alertqry + ",1";
                    }
                    else
                    {
                        qry = qry + ",0";
                        alertqry = alertqry + ",0";
                    }
                    if (chkCanCR.Checked)
                    {
                        qry = qry + ",1";
                        alertqry = alertqry + ",1";
                    }
                    else
                    {
                        qry = qry + ",0";
                        alertqry = alertqry + ",0";
                    }
                    if (chkCanHOF.Checked)
                    {
                        qry = qry + ",1";
                        alertqry = alertqry + ",1";
                    }
                    else
                    {
                        qry = qry + ",0";
                        alertqry = alertqry + ",0";
                    }
                    if (chkCanHOIS.Checked)
                    {
                        qry = qry + ",1";
                        alertqry = alertqry + ",1";
                    }
                    else
                    {
                        qry = qry + ",0";
                        alertqry = alertqry + ",0";
                    }
                    if (chkCanCOO.Checked)
                    {
                        qry = qry + ",1";
                        alertqry = alertqry + ",1";
                    }
                    else
                    {
                        qry = qry + ",0";
                        alertqry = alertqry + ",0";
                    }
                    if (chkCanCEO.Checked)
                    {
                        qry = qry + ",1";
                        alertqry = alertqry + ",1";
                    }
                    else
                    {
                        qry = qry + ",0";
                        alertqry = alertqry + ",0";
                    }
                   
                    qry = qry + ",GETDATE(),'"+myGlobal.loggedInUser()+"')" ;
                    alertqry = alertqry + ",GETDATE(),'" + myGlobal.loggedInUser() + "')";

                    Db.myExecuteSQL(qry + " ;    if( ( select count(*) from dbo.BPStatus_ManuallyTStatusChangeAlerts Where FromStatus='" + ddlFromStatus.SelectedItem.Text + "' And ToStatus='" + ddlToStatus.SelectedItem.Text + "' And DBName='" + ddlDBName.SelectedItem.Text + "' And IsDeleted=0 ) = 0 ) begin  " + alertqry + " end");
                    // below is to generate data of who will get Alerts on manual status change and who can manually change the Transactional status
                    qry = "";
                    qry = " DELETE FROM BPStatus_TransStatusChangeSAPUser_EmailList WHERE Type='ManualTStatusChangeEmailAlert' And DBName='"+ddlDBName.SelectedItem.Text+"' ; INSERT INTO dbo.BPStatus_TransStatusChangeSAPUser_EmailList(DBName,Country,FromStatus,ToStatus,SAPUser_EmailIds,Type) EXEC BPStatus_GetManualTStatusChangeEmailAlertList '"+ddlDBName.SelectedItem.Text+"' ; DELETE FROM BPStatus_TransStatusChangeSAPUser_EmailList WHERE Type='ManualTStatusChangeAuthUsers' And DBName='"+ddlDBName.SelectedItem.Text+"' ; INSERT INTO dbo.BPStatus_TransStatusChangeSAPUser_EmailList(DBName,Country,FromStatus,ToStatus,SAPUser_EmailIds,Type) EXEC BPStatus_GetManualTStatusChangeAuthUsersList '"+ddlDBName.SelectedItem.Text+"' ";
                    Db.myExecuteSQL(qry );

                    lblMsg.Text="Record saved successfully";
                   // Page.ClientScript.RegisterStartupScript(this.GetType(), "alert", "<script> alert('Record saved successfully.'); </script>");
                    BindGrid();
                    ddlFromStatus.SelectedItem.Text="--Select--";
                    ddlFromStatus.SelectedItem.Value="--Select--";
                    ddlToStatus.SelectedItem.Text="--Select--";
                    ddlToStatus.SelectedItem.Value="--Select--";
                    chkCanCA.Checked=false;
                    chkCanCM.Checked=false;
                    chkCanCR.Checked=false;
                    chkCanHOF.Checked=false;
                    chkCanHOIS.Checked=false;
                    chkCanCOO.Checked=false;
                    chkCanCEO.Checked=false;
                }
            }
            else if (e.CommandName == "Update")
            {
                GridViewRow row=(GridViewRow) (((LinkButton)e.CommandSource).NamingContainer);

                Label lblId = (Label)GRVManulStatusChange.Rows[row.RowIndex].FindControl("lblID");
                DropDownList ddlDBName = (DropDownList)GRVManulStatusChange.Rows[row.RowIndex].FindControl("ddlDBName");
                DropDownList ddlFromStatus = (DropDownList)GRVManulStatusChange.Rows[row.RowIndex].FindControl("ddlFromStatus");
                DropDownList ddlToStatus = (DropDownList)GRVManulStatusChange.Rows[row.RowIndex].FindControl("ddlToStatus");
                CheckBox chkCanCA = (CheckBox)GRVManulStatusChange.Rows[row.RowIndex].FindControl("chkEditCanCA");
                CheckBox chkCanCM = (CheckBox)GRVManulStatusChange.Rows[row.RowIndex].FindControl("chkEditCanCM");
                CheckBox chkCanCR = (CheckBox)GRVManulStatusChange.Rows[row.RowIndex].FindControl("chkEditCanCR");
                CheckBox chkCanHOF = (CheckBox)GRVManulStatusChange.Rows[row.RowIndex].FindControl("chkEditCanHOF");
                CheckBox chkCanHOIS = (CheckBox)GRVManulStatusChange.Rows[row.RowIndex].FindControl("chkEditCanHOIS");
                CheckBox chkCanCOO = (CheckBox)GRVManulStatusChange.Rows[row.RowIndex].FindControl("chkEditCanCOO");
                CheckBox chkCanCEO = (CheckBox)GRVManulStatusChange.Rows[row.RowIndex].FindControl("chkEditCanCEO");

                if (lblId.Text == "0")
                {
                    lblMsg.Text = "You can not edit this record. This is systems default entry.";
                    return;
                }

                if (ddlDBName.SelectedItem.Text == "--Select--")
                {
                    lblMsg.Text = "Please select Database";
                    return;
                }

                if (ddlFromStatus.SelectedItem.Text == "--Select--")
                {
                    lblMsg.Text = "Please select From Status";
                    return;
                }
                if (ddlToStatus.SelectedItem.Text == "--Select--")
                {
                    lblMsg.Text = "Please select To Status";
                    return;
                }

                if (ddlToStatus.SelectedItem.Text == ddlFromStatus.SelectedItem.Text)
                {
                    lblMsg.Text = "From Status and To Status can not be same.";
                    return;
                }
                /// Allow only Active to CLosed or Blocked
                if (ddlFromStatus.SelectedItem.Text == "Active" && (ddlToStatus.SelectedItem.Text == "Soft hold" || ddlToStatus.SelectedItem.Text == "Hard hold" || ddlToStatus.SelectedItem.Text == "Dormant"))
                {
                    lblMsg.Text = "You can not change customer status to " + ddlToStatus.SelectedItem.Text + ". You can only change ACTIVE customer to BLOCKED or CLOSED.";
                    return;
                }

                /// Allow only Dormant to Active OR Closed
                if (ddlFromStatus.SelectedItem.Text == "Dormant" && (ddlToStatus.SelectedItem.Text == "Soft hold" || ddlToStatus.SelectedItem.Text == "Hard hold" || ddlToStatus.SelectedItem.Text == "Blocked"))
                {
                    lblMsg.Text = "You can not change customer status to " + ddlToStatus.SelectedItem.Text + ". You can only change DORMANT customer to ACTIVE or CLOSED.";
                    return;
                }

                // Allow only Soft hold to Active, Blocked & Closed
                if (ddlFromStatus.SelectedItem.Text == "Soft hold" && (ddlToStatus.SelectedItem.Text == "Hard hold" || ddlToStatus.SelectedItem.Text == "Dormant" || ddlToStatus.SelectedItem.Text == "Soft hold"))
                {
                    lblMsg.Text = "You can not change customer status to " + ddlToStatus.SelectedItem.Text + ". You can only change SOFT HOLD customer to ACTIVE or BLOCKED or CLOSED.";
                    return;
                }
                // Allow only Hard hold to Active, Blocked and Closed
                if (ddlFromStatus.SelectedItem.Text == "Hard hold" && (ddlToStatus.SelectedItem.Text == "Soft hold" || ddlToStatus.SelectedItem.Text == "Hard hold" || ddlToStatus.SelectedItem.Text == "Dormant"))
                {
                    lblMsg.Text = "You can not change customer status to " + ddlToStatus.SelectedItem.Text + ". You can only change HARD HOLD customer to ACTIVE or BLOCKED or CLOSED.";
                    return;
                }
                // Allow only Blocked to Active and Closed
                if (ddlFromStatus.SelectedItem.Text == "Blocked" && (ddlToStatus.SelectedItem.Text == "Soft hold" || ddlToStatus.SelectedItem.Text == "Hard hold" || ddlToStatus.SelectedItem.Text == "Dormant"))
                {
                    lblMsg.Text = "You can not change customer status to " + ddlToStatus.SelectedItem.Text + ". You can only change BLOCKED customer to ACTIVE OR CLOSED.";
                    return;
                }

                Db.constr = myGlobal.getAppSettingsDataForKey("tejSAP");
                int NoOfRec = Db.myExecuteScalar("select count(*) NoOfRec from dbo.BPStatus_ManuallyTStatusChangeAuthUsers Where FromStatus='" + ddlFromStatus.SelectedItem.Text + "' And ToStatus='" + ddlToStatus.SelectedItem.Text + "' And  DBName='" + ddlDBName.SelectedItem.Text + "' And Id<>" + lblId.Text + " And IsDeleted=0 ");
                if (NoOfRec > 0)
                {
                    lblMsg.Text = "Record already exist to change customer status from " + ddlFromStatus.SelectedItem.Text + " to " + ddlToStatus.SelectedItem.Text + " for "+ddlDBName.SelectedItem.Text+" database.";
                    return;
                }
                else
                {
                    string qry = " Update dbo.BPStatus_ManuallyTStatusChangeAuthUsers Set DBName='" + ddlDBName.SelectedItem.Text + "', FromStatus='" + ddlFromStatus.SelectedItem.Text + "',ToStatus='" + ddlToStatus.SelectedItem.Text + "'";
                    if (chkCanCA.Checked)
                        qry = qry + ", CanCA=1";
                    else
                        qry = qry + ", CanCA=0";
                    if (chkCanCM.Checked)
                        qry = qry + ", CanCM=1";
                    else
                        qry = qry + ", CanCM=0";
                    if (chkCanCR.Checked)
                        qry = qry + ", CanCR=1";
                    else
                        qry = qry + ", CanCR=0";
                    if (chkCanHOF.Checked)
                        qry = qry + ", CanHOF=1";
                    else
                        qry = qry + ", CanHOF=0";
                    if (chkCanHOIS.Checked)
                        qry = qry + ", CanHOIS=1";
                    else
                        qry = qry + ", CanHOIS=0";
                    if (chkCanCOO.Checked)
                        qry = qry + ", CanCOO=1";
                    else
                        qry = qry + ", CanCOO=0";
                    if (chkCanCEO.Checked)
                        qry = qry + ", CanCEO=1";
                    else
                        qry = qry + ", CanCEO=0";

                    qry = qry + ",LastUpdatedOn=GETDATE(), LastUpdatedBy='" + myGlobal.loggedInUser() + "'  Where Id="+ lblId.Text ;

                    qry = qry + " ; DELETE FROM BPStatus_TransStatusChangeSAPUser_EmailList WHERE Type='ManualTStatusChangeAuthUsers' And DBName='"+ddlDBName.SelectedItem.Text+"' ; INSERT INTO dbo.BPStatus_TransStatusChangeSAPUser_EmailList(DBName,Country,FromStatus,ToStatus,SAPUser_EmailIds,Type) EXEC BPStatus_GetManualTStatusChangeAuthUsersList '"+ddlDBName.SelectedItem.Text+"' ; ";
                    
                    Db.myExecuteSQL(qry );

                    lblMsg.Text = "Record updated successfully";
                }

            }
            else if (e.CommandName == "Delete")
            {
                GridViewRow row = (GridViewRow)(((LinkButton)e.CommandSource).NamingContainer);
                Label lblId = (Label)GRVManulStatusChange.Rows[row.RowIndex].FindControl("lblID");
                Label lblDBName = (Label)GRVManulStatusChange.Rows[row.RowIndex].FindControl("lblDBName");
                Label lblFromStatus = (Label)GRVManulStatusChange.Rows[row.RowIndex].FindControl("lblFromStatus");
                Label lblToStatus = (Label)GRVManulStatusChange.Rows[row.RowIndex].FindControl("lblToStatus");
                if (lblId.Text == "0")
                {
                    lblMsg.Text = "You can not delete this record. This is systems default entry.";
                    return;
                }
                else
                {
                    Db.myExecuteSQL("Update BPStatus_ManuallyTStatusChangeAuthUsers Set IsDeleted=1,LastUpdatedOn=getdate(),LastUpdatedBy='" + myGlobal.loggedInUser() + "' Where Id=" + lblId.Text + " ; Update dbo.BPStatus_ManuallyTStatusChangeAlerts Set IsDeleted=1,LastUpdatedOn=getdate(),LastUpdatedBy='" + myGlobal.loggedInUser() + "' Where DBName='" + lblDBName.Text + "' And  FromStatus='" + lblFromStatus.Text + "' And ToStatus='" + lblToStatus.Text + "' And IsDeleted=0 ");
                    
                    Db.myExecuteSQL(" DELETE FROM BPStatus_TransStatusChangeSAPUser_EmailList WHERE Type='ManualTStatusChangeAuthUsers' And DBName='"+lblDBName.Text+"' ; INSERT INTO dbo.BPStatus_TransStatusChangeSAPUser_EmailList(DBName,Country,FromStatus,ToStatus,SAPUser_EmailIds,Type) EXEC BPStatus_GetManualTStatusChangeAuthUsersList '"+lblDBName.Text+"' ; DELETE FROM BPStatus_TransStatusChangeSAPUser_EmailList WHERE Type='ManualTStatusChangeEmailAlert' And DBName='"+lblDBName.Text+"' ; INSERT INTO dbo.BPStatus_TransStatusChangeSAPUser_EmailList(DBName,Country,FromStatus,ToStatus,SAPUser_EmailIds,Type)  EXEC BPStatus_GetManualTStatusChangeEmailAlertList '"+lblDBName.Text+"' ");
                    
                    lblMsg.Text = "Record Deleted Successfully";
                }
            }
        }
        catch (Exception ex)
        {
            lblMsg.Text = " Failed to "+e.CommandName+"  , Please retry.. " + ex.Message;
        }
    }

    protected void GRVManulStatusChange_RowDeleting(object sender, GridViewDeleteEventArgs e)
    {
        try
        {
            BindGrid();
            GRVManulStatusChange.FooterRow.Visible = true;
        }
        catch (Exception ex)
        {
            lblMsg.Text = "Error occured in Row Deleting " + ex.Message;
        }
    }

    protected void GRVManulStatusChange_RowEditing(object sender, GridViewEditEventArgs e)
    {
        try
        {
            GRVManulStatusChange.EditIndex = e.NewEditIndex;
            BindGrid();
            GRVManulStatusChange.FooterRow.Visible = false;
        }
        catch (Exception ex)
        {
            lblMsg.Text = "Error occured in RowEdit, please retry.."+ex.Message;
        }
    }

    protected void GRVManulStatusChange_RowCancelingEdit(object sender, GridViewCancelEditEventArgs e)
    {
        try
        {
            GRVManulStatusChange.EditIndex = -1;
            BindGrid();
            GRVManulStatusChange.FooterRow.Visible = true;
        }
        catch (Exception ex)
        {
            lblMsg.Text = "Error occured on Cancel RowEdit, please retry.." + ex.Message;
        }
    }

    protected void GRVManulStatusChange_RowUpdating(object sender, GridViewUpdateEventArgs e)
    {
        try
        {
            GRVManulStatusChange.EditIndex = -1;
            BindGrid();
            GRVManulStatusChange.FooterRow.Visible = true;
        }
        catch (Exception ex)
        {
            lblMsg.Text = "Error occured on RowUpdating, please retry.." + ex.Message;
        }
    }

    protected void GRVManulStatusChange_Sorting(object sender, GridViewSortEventArgs e)
    {
        try
        {
            String sortExp = e.SortExpression.ToString();
            String sortDir = e.SortDirection.ToString();

            String sortExpV = (String)ViewState["sortExpression"];
            String sortDirV = (String)ViewState["sortDirection"];

            if (sortExpV != null && sortExp == sortExpV)
            {
                if (sortDirV == "Asc")
                    ViewState["sortDirection"] = "Desc";
                else
                    ViewState["sortDirection"] = "Asc";
            }
            else
            {
                ViewState["sortExpression"] = sortExp;
                ViewState["sortDirection"] = "Asc";
            }
            BindGrid();
        }
        catch (Exception ex)
        {
            lblMsg.Text = "Error occured in sorting() " + ex.Message;
        }
    }
}