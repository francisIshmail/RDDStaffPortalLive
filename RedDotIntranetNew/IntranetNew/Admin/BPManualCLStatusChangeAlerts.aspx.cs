using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Data;

public partial class IntranetNew_Admin_BPManualCLStatusChangeAlerts : System.Web.UI.Page
{
    protected void Page_Load(object sender, EventArgs e)
    {
        try
        {
            if (!IsPostBack)
            {
                Db.constr = myGlobal.getAppSettingsDataForKey("tejSAP");
                int count = Db.myExecuteScalar("Select COUNT(*) from dbo.MenuWiseForms t0 Join dbo.UserAuthorization t1 on t0.MenuId=t1.MenuId and t1.MembershipUserName='" + myGlobal.loggedInUser() + "' And t0.FormURL='BPManualCLStatusChangeAlerts.aspx' And t1.IsActive=1");
                if (count > 0)
                {
                    BindGrid();
                }
                else
                {
                    Response.Redirect("Default.aspx?UserAccess=0&FormName=Manual Credit Limit Status Change Alerts");
                }
            }
        }
        catch (Exception ex)
        {
            lblMsg.Text = " Error occured in PageLoad : " + ex.Message;
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
                summarySQL = "select * from dbo.BPStatus_ManuallyCLStatusChangeAlerts Where isnull(IsDeleted,0)=0  Order by " + sortExp + " " + sortDir;
            }
            else
            {
                summarySQL = "select * from dbo.BPStatus_ManuallyCLStatusChangeAlerts Where isnull(IsDeleted,0)=0 ";
            }

            Db.constr = myGlobal.getAppSettingsDataForKey("tejSAP");
            DataTable DT = Db.myGetDS(summarySQL).Tables[0];
            if (DT.Rows.Count > 0)
            {
                GRVManualCLStatusChangeAlert.DataSource = DT;
                GRVManualCLStatusChangeAlert.DataBind();
            }
            else
            {
                DataRow dr = DT.NewRow();  // add new row
                dr["Id"] = 0;
                dr["DBName"] = "--Select--";
                dr["FromStatus"] = "--Select--";
                dr["ToStatus"] = "--Select--";
                dr["AlertToCA"] = false;
                dr["AlertToCM"] = false;
                dr["AlertToCR"] = false;
                dr["AlertToHOF"] = false;
                dr["AlertToHOIS"] = false;
                dr["AlertToCOO"] = false;
                dr["AlertToCEO"] = false;
                DT.Rows.Add(dr);
                GRVManualCLStatusChangeAlert.DataSource = DT;
                GRVManualCLStatusChangeAlert.DataBind();
            }
        }
        catch (Exception ex)
        {
            lblMsg.Text = " Error occured in BindGrid : " + ex.Message;
        }
    }

    protected void GRVManualCLStatusChangeAlert_RowCommand(object sender, GridViewCommandEventArgs e)
    {
        try
        {
            lblMsg.Text = "";

            if (e.CommandName == "Add")
            {
                DropDownList ddlDBName = (DropDownList)GRVManualCLStatusChangeAlert.FooterRow.FindControl("ddlFooterDBName");
                DropDownList ddlFromStatus = (DropDownList)GRVManualCLStatusChangeAlert.FooterRow.FindControl("ddlFooterFromStatus");
                DropDownList ddlToStatus = (DropDownList)GRVManualCLStatusChangeAlert.FooterRow.FindControl("ddlFooterToStatus");
                CheckBox chkCanCA = (CheckBox)GRVManualCLStatusChangeAlert.FooterRow.FindControl("chkFooterCanCA");
                CheckBox chkCanCM = (CheckBox)GRVManualCLStatusChangeAlert.FooterRow.FindControl("chkFooterCanCM");
                CheckBox chkCanCR = (CheckBox)GRVManualCLStatusChangeAlert.FooterRow.FindControl("chkFooterCanCR");
                CheckBox chkCanHOF = (CheckBox)GRVManualCLStatusChangeAlert.FooterRow.FindControl("chkFooterCanHOF");
                CheckBox chkCanHOIS = (CheckBox)GRVManualCLStatusChangeAlert.FooterRow.FindControl("chkFooterCanHOIS");
                CheckBox chkCanCOO = (CheckBox)GRVManualCLStatusChangeAlert.FooterRow.FindControl("chkFooterCanCOO");
                CheckBox chkCanCEO = (CheckBox)GRVManualCLStatusChangeAlert.FooterRow.FindControl("chkFooterCanCEO");

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
                

                Db.constr = myGlobal.getAppSettingsDataForKey("tejSAP");
                int NoOfRec = Db.myExecuteScalar("select count(*) NoOfRec from dbo.BPStatus_ManuallyCLStatusChangeAlerts Where FromStatus='" + ddlFromStatus.SelectedItem.Text + "' And ToStatus='" + ddlToStatus.SelectedItem.Text + "' And DBName='" + ddlDBName.SelectedItem.Text + "' And IsDeleted=0 ");
                if (NoOfRec > 0)
                {
                    lblMsg.Text = "Record already exist to get alerts  when credit limit status is change from " + ddlFromStatus.SelectedItem.Text + " to " + ddlToStatus.SelectedItem.Text + " for " + ddlDBName.SelectedItem.Text + " database.";
                    return;
                }
                else
                {
                    string alertqry = "";
                    alertqry = "Insert into dbo.BPStatus_ManuallyCLStatusChangeAlerts (DBName,FromStatus,ToStatus,AlertToCA,AlertToCM,AlertToCR,AlertToHOF,AlertToHOIS,AlertToCOO,AlertToCEO,CreatedOn,CreatedBy ) Values ( '" + ddlDBName.SelectedItem.Text + "','" + ddlFromStatus.SelectedItem.Text + "','" + ddlToStatus.SelectedItem.Text + "'";
                    if (chkCanCA.Checked)
                    {
                        alertqry = alertqry + ",1";
                    }
                    else
                    {
                        alertqry = alertqry + ",0";
                    }
                    if (chkCanCM.Checked)
                    {
                        alertqry = alertqry + ",1";
                    }
                    else
                    {
                        alertqry = alertqry + ",0";
                    }
                    if (chkCanCR.Checked)
                    {
                        alertqry = alertqry + ",1";
                    }
                    else
                    {
                        alertqry = alertqry + ",0";
                    }
                    if (chkCanHOF.Checked)
                    {
                        alertqry = alertqry + ",1";
                    }
                    else
                    {
                        alertqry = alertqry + ",0";
                    }
                    if (chkCanHOIS.Checked)
                    {
                        alertqry = alertqry + ",1";
                    }
                    else
                    {
                        alertqry = alertqry + ",0";
                    }
                    if (chkCanCOO.Checked)
                    {
                        alertqry = alertqry + ",1";
                    }
                    else
                    {
                        alertqry = alertqry + ",0";
                    }
                    if (chkCanCEO.Checked)
                    {
                        alertqry = alertqry + ",1";
                    }
                    else
                    {
                        alertqry = alertqry + ",0";
                    }

                    alertqry = alertqry + ",GETDATE(),'" + myGlobal.loggedInUser() + "')";

                    alertqry = alertqry + " ; DELETE FROM BPStatus_CLStatusChangeSAPUser_EmailList WHERE Type='ManualCLStatusChangeEmailAlert' And DBName='" + ddlDBName.SelectedItem.Text + "' ; INSERT INTO dbo.BPStatus_CLStatusChangeSAPUser_EmailList(DBName,Country,FromStatus,ToStatus,SAPUser_EmailIds,Type) EXEC BPStatus_GetManualCLStatusChangeEmailAlertList '" + ddlDBName.SelectedItem.Text + "' ;";

                    Db.myExecuteSQL(alertqry);

                    lblMsg.Text = "Record saved successfully";
                    // Page.ClientScript.RegisterStartupScript(this.GetType(), "alert", "<script> alert('Record saved successfully.'); </script>");
                    BindGrid();
                    ddlDBName.SelectedItem.Text = "--Select--";
                    ddlFromStatus.SelectedItem.Text = "--Select--";
                    ddlFromStatus.SelectedItem.Value = "--Select--";
                    ddlToStatus.SelectedItem.Text = "--Select--";
                    ddlToStatus.SelectedItem.Value = "--Select--";
                    chkCanCA.Checked = false;
                    chkCanCM.Checked = false;
                    chkCanCR.Checked = false;
                    chkCanHOF.Checked = false;
                    chkCanHOIS.Checked = false;
                    chkCanCOO.Checked = false;
                    chkCanCEO.Checked = false;
                }
            }
            else if (e.CommandName == "Update")
            {
                GridViewRow row = (GridViewRow)(((LinkButton)e.CommandSource).NamingContainer);

                Label lblId = (Label)GRVManualCLStatusChangeAlert.Rows[row.RowIndex].FindControl("lblID");
                DropDownList ddlDBName = (DropDownList)GRVManualCLStatusChangeAlert.Rows[row.RowIndex].FindControl("ddlDBName");
                DropDownList ddlFromStatus = (DropDownList)GRVManualCLStatusChangeAlert.Rows[row.RowIndex].FindControl("ddlFromStatus");
                DropDownList ddlToStatus = (DropDownList)GRVManualCLStatusChangeAlert.Rows[row.RowIndex].FindControl("ddlToStatus");
                CheckBox chkCanCA = (CheckBox)GRVManualCLStatusChangeAlert.Rows[row.RowIndex].FindControl("chkEditCanCA");
                CheckBox chkCanCM = (CheckBox)GRVManualCLStatusChangeAlert.Rows[row.RowIndex].FindControl("chkEditCanCM");
                CheckBox chkCanCR = (CheckBox)GRVManualCLStatusChangeAlert.Rows[row.RowIndex].FindControl("chkEditCanCR");
                CheckBox chkCanHOF = (CheckBox)GRVManualCLStatusChangeAlert.Rows[row.RowIndex].FindControl("chkEditCanHOF");
                CheckBox chkCanHOIS = (CheckBox)GRVManualCLStatusChangeAlert.Rows[row.RowIndex].FindControl("chkEditCanHOIS");
                CheckBox chkCanCOO = (CheckBox)GRVManualCLStatusChangeAlert.Rows[row.RowIndex].FindControl("chkEditCanCOO");
                CheckBox chkCanCEO = (CheckBox)GRVManualCLStatusChangeAlert.Rows[row.RowIndex].FindControl("chkEditCanCEO");

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

                Db.constr = myGlobal.getAppSettingsDataForKey("tejSAP");
                int NoOfRec = Db.myExecuteScalar("select count(*) NoOfRec from dbo.BPStatus_ManuallyCLStatusChangeAlerts Where FromStatus='" + ddlFromStatus.SelectedItem.Text + "' And ToStatus='" + ddlToStatus.SelectedItem.Text + "' And DBName='" + ddlDBName.SelectedItem.Text + "'  And Id<>" + lblId.Text + " And IsDeleted=0 ");
                if (NoOfRec > 0)
                {
                    lblMsg.Text = "Record already exist to get alerts for credit limit status change from " + ddlFromStatus.SelectedItem.Text + " to " + ddlToStatus.SelectedItem.Text + " for " + ddlDBName.SelectedItem.Text + " database.";
                    return;
                }
                else
                {
                    string qry = " Update dbo.BPStatus_ManuallyCLStatusChangeAlerts Set DBName='" + ddlDBName.SelectedItem.Text + "',FromStatus='" + ddlFromStatus.SelectedItem.Text + "',ToStatus='" + ddlToStatus.SelectedItem.Text + "'";
                    if (chkCanCA.Checked)
                        qry = qry + ", AlertToCA=1";
                    else
                        qry = qry + ", AlertToCA=0";
                    if (chkCanCM.Checked)
                        qry = qry + ", AlertToCM=1";
                    else
                        qry = qry + ", AlertToCM=0";
                    if (chkCanCR.Checked)
                        qry = qry + ", AlertToCR=1";
                    else
                        qry = qry + ", AlertToCR=0";
                    if (chkCanHOF.Checked)
                        qry = qry + ", AlertToHOF=1";
                    else
                        qry = qry + ", AlertToHOF=0";
                    if (chkCanHOIS.Checked)
                        qry = qry + ", AlertToHOIS=1";
                    else
                        qry = qry + ", AlertToHOIS=0";
                    if (chkCanCOO.Checked)
                        qry = qry + ", AlertToCOO=1";
                    else
                        qry = qry + ", AlertToCOO=0";
                    if (chkCanCEO.Checked)
                        qry = qry + ", AlertToCEO=1";
                    else
                        qry = qry + ", AlertToCEO=0";

                    qry = qry + ",LastUpdatedOn=GETDATE(), LastUpdatedBy='" + myGlobal.loggedInUser() + "'  Where Id=" + lblId.Text;

                    qry = qry + " ; DELETE FROM BPStatus_CLStatusChangeSAPUser_EmailList WHERE Type='ManualCLStatusChangeEmailAlert' And DBName='" + ddlDBName.SelectedItem.Text + "' ; INSERT INTO dbo.BPStatus_CLStatusChangeSAPUser_EmailList(DBName,Country,FromStatus,ToStatus,SAPUser_EmailIds,Type) EXEC BPStatus_GetManualCLStatusChangeEmailAlertList '" + ddlDBName.SelectedItem.Text + "' ;";

                    Db.myExecuteSQL(qry);

                    lblMsg.Text = "Record updated successfully";
                }

            }
            else if (e.CommandName == "Delete")
            {
                GridViewRow row = (GridViewRow)(((LinkButton)e.CommandSource).NamingContainer);
                Label lblId = (Label)GRVManualCLStatusChangeAlert.Rows[row.RowIndex].FindControl("lblID");
                Label lblDBName = (Label)GRVManualCLStatusChangeAlert.Rows[row.RowIndex].FindControl("lblDBName");
                if (lblId.Text == "0")
                {
                    lblMsg.Text = "You can not delete this record. This is systems default entry.";
                    return;
                }
                else
                {
                    Db.myExecuteSQL("Update BPStatus_ManuallyCLStatusChangeAlerts Set IsDeleted=1,LastUpdatedOn=getdate(),LastUpdatedBy='" + myGlobal.loggedInUser() + "' Where Id=" + lblId.Text);

                    Db.myExecuteSQL(" DELETE FROM BPStatus_CLStatusChangeSAPUser_EmailList WHERE Type='ManualCLStatusChangeEmailAlert' And DBName='" + lblDBName.Text + "' ; INSERT INTO dbo.BPStatus_CLStatusChangeSAPUser_EmailList(DBName,Country,FromStatus,ToStatus,SAPUser_EmailIds,Type) EXEC BPStatus_GetManualCLStatusChangeEmailAlertList '" + lblDBName.Text + "'");
                    
                    lblMsg.Text = "Record Deleted Successfully";
                }
            }

        }
        catch (Exception ex)
        {
            lblMsg.Text = " Failed to " + e.CommandName + "  , Please retry.. " + ex.Message;
        }
    }

    protected void GRVManualCLStatusChangeAlert_RowDeleting(object sender, GridViewDeleteEventArgs e)
    {
        try
        {
            BindGrid();
            GRVManualCLStatusChangeAlert.FooterRow.Visible = true;
        }
        catch (Exception ex)
        {
            lblMsg.Text = "Error occured in Row Deleting " + ex.Message;
        }
    }

    protected void GRVManualCLStatusChangeAlert_RowEditing(object sender, GridViewEditEventArgs e)
    {
        try
        {
            GRVManualCLStatusChangeAlert.EditIndex = e.NewEditIndex;
            BindGrid();
            GRVManualCLStatusChangeAlert.FooterRow.Visible = false;
        }
        catch (Exception ex)
        {
            lblMsg.Text = "Error occured in RowEdit, please retry.." + ex.Message;
        }
    }

    protected void GRVManualCLStatusChangeAlert_RowCancelingEdit(object sender, GridViewCancelEditEventArgs e)
    {
        try
        {
            GRVManualCLStatusChangeAlert.EditIndex = -1;
            BindGrid();
            GRVManualCLStatusChangeAlert.FooterRow.Visible = true;
        }
        catch (Exception ex)
        {
            lblMsg.Text = "Error occured on Cancel RowEdit, please retry.." + ex.Message;
        }
    }

    protected void GRVManualCLStatusChangeAlert_RowUpdating(object sender, GridViewUpdateEventArgs e)
    {
        try
        {
            GRVManualCLStatusChangeAlert.EditIndex = -1;
            BindGrid();
            GRVManualCLStatusChangeAlert.FooterRow.Visible = true;
        }
        catch (Exception ex)
        {
            lblMsg.Text = "Error occured on RowUpdating, please retry.." + ex.Message;
        }
    }
    protected void GRVManualCLStatusChangeAlert_Sorting(object sender, GridViewSortEventArgs e)
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
            lblMsg.Text = "Error occured in sorting() : " + ex.Message;
        }
    }
}