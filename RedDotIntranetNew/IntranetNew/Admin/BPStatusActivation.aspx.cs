using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Data;

public partial class IntranetNew_Admin_BPStatusActivation : System.Web.UI.Page
{

    protected void Page_Load(object sender, EventArgs e)
    {
        try
        {
            if (!IsPostBack)
            {
                 Db.constr = myGlobal.getAppSettingsDataForKey("tejSAP");
                int count = Db.myExecuteScalar("Select COUNT(*) from dbo.MenuWiseForms t0 Join dbo.UserAuthorization t1 on t0.MenuId=t1.MenuId and t1.MembershipUserName='" + myGlobal.loggedInUser() + "' And t0.FormURL='BPStatusActivation.aspx' And t1.IsActive=1");
                if (count > 0)
                {
                    BindGrid();
                }
                else
                {
                    Response.Redirect("Default.aspx?UserAccess=0&FormName=Activate customer status");
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
                summarySQL = "select * from dbo.BPStatus_ActivateForCountry Order by " + sortExp + " " + sortDir;
            }
            else
            {
                summarySQL = "select * from dbo.BPStatus_ActivateForCountry";
            }

            Db.constr = myGlobal.getAppSettingsDataForKey("tejSAP");
            DataTable DT = Db.myGetDS(summarySQL).Tables[0];
            if (DT.Rows.Count > 0)
            {
                
                GRVBPStatusActivation.DataSource = DT;
                GRVBPStatusActivation.DataBind();
            }
            else
            {
                DataRow dr = DT.NewRow();  // add new row
                dr["Id"] = 0;
                //dr["DBName"] = "--Select--";
                dr["Country"] = "--Select--";
                dr["IsActive"] = false;
               
                DT.Rows.Add(dr);
                GRVBPStatusActivation.DataSource = DT;
                GRVBPStatusActivation.DataBind();
            }
        }
        catch (Exception ex)
        {
            lblMsg.Text = " Error occured in BindGrid : " + ex.Message;
        }
    }

    protected void GRVBPStatusActivation_RowCommand(object sender, GridViewCommandEventArgs e)
    {
        try
        {
            lblMsg.Text = "";

            if (e.CommandName == "Add")
            {
                //DropDownList ddlDBName = (DropDownList)GRVBPStatusActivation.FooterRow.FindControl("ddlFooterDBName");
                DropDownList ddlCountry = (DropDownList)GRVBPStatusActivation.FooterRow.FindControl("ddlFooterCountry");
                CheckBox chkIsActivate = (CheckBox)GRVBPStatusActivation.FooterRow.FindControl("chkFooterActivate");

                //if (ddlDBName.SelectedItem.Text == "--Select--")
                //{
                //    lblMsg.Text = "Please select Database";
                //    return;
                //}
                if (ddlCountry.SelectedItem.Text == "--Select--")
                {
                    lblMsg.Text = "Please select Country";
                    return;
                }
                

                Db.constr = myGlobal.getAppSettingsDataForKey("tejSAP");
                int NoOfRec = Db.myExecuteScalar("select count(*) NoOfRec from dbo.BPStatus_ActivateForCountry Where Country='" + ddlCountry.SelectedItem.Text + "'");
                if (NoOfRec > 0)
                {
                    lblMsg.Text = "Record already exist for " + ddlCountry.SelectedItem.Text + " country.";
                    return;
                }
                else
                {
                    string qry = "";
                    qry = "Insert into dbo.BPStatus_ActivateForCountry (Country,IsActive,CreatedOn,CreatedBy ) Values ( '" + ddlCountry.SelectedItem.Text + "'";
                    if (chkIsActivate.Checked)
                    {
                        qry = qry + ",1";
                    }
                    else
                    {
                        qry = qry + ",0";
                    }

                    qry = qry + ",GETDATE(),'" + myGlobal.loggedInUser() + "')";

                    Db.myExecuteSQL(qry);

                    lblMsg.Text = "Record saved successfully";
                    BindGrid();
                    //ddlDBName.SelectedItem.Text = "--Select--";
                    ddlCountry.SelectedItem.Text = "--Select--";
                    chkIsActivate.Checked = false;
                }
            }
            else if (e.CommandName == "Update")
            {
                GridViewRow row = (GridViewRow)(((LinkButton)e.CommandSource).NamingContainer);

                Label lblId = (Label)GRVBPStatusActivation.Rows[row.RowIndex].FindControl("lblID");
                //DropDownList ddlDBName = (DropDownList)GRVBPStatusActivation.Rows[row.RowIndex].FindControl("ddlDBName");
                DropDownList ddlCountry = (DropDownList)GRVBPStatusActivation.Rows[row.RowIndex].FindControl("ddlCountry");
                CheckBox chkEditActivate = (CheckBox)GRVBPStatusActivation.Rows[row.RowIndex].FindControl("chkEditActivate");
                
                if (lblId.Text == "0")
                {
                    lblMsg.Text = "You can not edit this record. This is systems default entry.";
                    return;
                }
                //if (ddlDBName.SelectedItem.Text == "--Select--")
                //{
                //    lblMsg.Text = "Please select Database";
                //    return;
                //}
                if (ddlCountry.SelectedItem.Text == "--Select--")
                {
                    lblMsg.Text = "Please select From Status";
                    return;
                }

                Db.constr = myGlobal.getAppSettingsDataForKey("tejSAP");
                int NoOfRec = Db.myExecuteScalar("select count(*) NoOfRec from dbo.BPStatus_ActivateForCountry Where Country='" + ddlCountry.SelectedItem.Text + "' And Id<>" + lblId.Text );
                if (NoOfRec > 0)
                {
                    lblMsg.Text = "Record already exist to activate customer status for " + ddlCountry.SelectedItem.Text ;
                    return;
                }
                else
                {
                    string qry = " Update dbo.BPStatus_ActivateForCountry Set Country='" + ddlCountry.SelectedItem.Text + "'";
                    if (chkEditActivate.Checked)
                        qry = qry + ", IsActive=1";
                    else
                        qry = qry + ", IsActive=0";

                    qry = qry + ",LastUpdatedOn=GETDATE(), LastUpdatedBy='" + myGlobal.loggedInUser() + "'  Where Id=" + lblId.Text;

                    Db.myExecuteSQL(qry);

                    lblMsg.Text = "Record updated successfully";
                }

            }
            else if (e.CommandName == "Delete")
            {
                //GridViewRow row = (GridViewRow)(((LinkButton)e.CommandSource).NamingContainer);
                //Label lblId = (Label)GRVBPStatusActivation.Rows[row.RowIndex].FindControl("lblID");
                //if (lblId.Text == "0")
                //{
                //    lblMsg.Text = "You can not delete this record. This is systems default entry.";
                //    return;
                //}
                //else
                //{
                //    Db.myExecuteSQL("Update BPStatus_ManuallyChanageAlerts Set IsDeleted=1,LastUpdatedOn=getdate(),LastUpdatedBy='" + myGlobal.loggedInUser() + "' Where Id=" + lblId.Text);
                //    lblMsg.Text = "Record Deleted Successfully";
                //}
            }

        }
        catch (Exception ex)
        {
            lblMsg.Text = " Failed to " + e.CommandName + "  , Please retry.. " + ex.Message;
        }
    }

    protected void GRVBPStatusActivation_RowDeleting(object sender, GridViewDeleteEventArgs e)
    {
        try
        {
            BindGrid();
            GRVBPStatusActivation.FooterRow.Visible = true;
        }
        catch (Exception ex)
        {
            lblMsg.Text = "Error occured in Row Deleting " + ex.Message;
        }
    }

    protected void GRVBPStatusActivation_RowEditing(object sender, GridViewEditEventArgs e)
    {
        try
        {
            GRVBPStatusActivation.EditIndex = e.NewEditIndex;
            BindGrid();
            GRVBPStatusActivation.FooterRow.Visible = false;
        }
        catch (Exception ex)
        {
            lblMsg.Text = "Error occured in RowEdit, please retry.." + ex.Message;
        }
    }

    protected void GRVBPStatusActivation_RowCancelingEdit(object sender, GridViewCancelEditEventArgs e)
    {
        try
        {
            GRVBPStatusActivation.EditIndex = -1;
            BindGrid();
            GRVBPStatusActivation.FooterRow.Visible = true;
        }
        catch (Exception ex)
        {
            lblMsg.Text = "Error occured on Cancel RowEdit, please retry.." + ex.Message;
        }
    }

    protected void GRVBPStatusActivation_RowUpdating(object sender, GridViewUpdateEventArgs e)
    {
        try
        {
            GRVBPStatusActivation.EditIndex = -1;
            BindGrid();
            GRVBPStatusActivation.FooterRow.Visible = true;
        }
        catch (Exception ex)
        {
            lblMsg.Text = "Error occured on RowUpdating, please retry.." + ex.Message;
        }
    }

    protected void GRVBPStatusActivation_Sorting(object sender, GridViewSortEventArgs e)
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
            lblMsg.Text = "Error occured Sorting, please retry.." + ex.Message;
        }
    }

}