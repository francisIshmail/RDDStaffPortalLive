using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Data;

public partial class IntranetNew_Admin_BPWithholdingTaxSlab : System.Web.UI.Page
{
    protected void Page_Load(object sender, EventArgs e)
    {
        try
        {
            if (!IsPostBack)
            {
                Db.constr = myGlobal.getAppSettingsDataForKey("tejSAP");
                int count = Db.myExecuteScalar("Select COUNT(*) from dbo.MenuWiseForms t0 Join dbo.UserAuthorization t1 on t0.MenuId=t1.MenuId and t1.MembershipUserName='" + myGlobal.loggedInUser() + "' And t0.FormURL='BPWithholdingTaxSlab.aspx' And t1.IsActive=1");
                if (count > 0)
                {
                    BindGrid();
                }
                else
                {
                    Response.Redirect("Default.aspx?UserAccess=0&FormName=Withholding tax slab");
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
                summarySQL = "select Id,DBName,convert(varchar,FromDate,101) As FromDate, convert(varchar,ToDate,101) As ToDate, WTaxPercentage  from dbo.BPStatus_WithholdingTax Where isnull(IsDeleted,0)=0 Order by " + sortExp + " " + sortDir;
            }
            else
            {
                summarySQL = "select Id,DBName,convert(varchar,FromDate,101) As FromDate, convert(varchar,ToDate,101) As ToDate, WTaxPercentage  from dbo.BPStatus_WithholdingTax Where isnull(IsDeleted,0)=0 ";
            }

            Db.constr = myGlobal.getAppSettingsDataForKey("tejSAP");
            DataTable DT = Db.myGetDS(summarySQL).Tables[0];
            if (DT.Rows.Count > 0)
            {
                GRVWithholdingTax.DataSource = DT;
                GRVWithholdingTax.DataBind();
            }
            else
            {
                DataRow dr = DT.NewRow();  // add new row
                dr["Id"] = 0;
                dr["DBName"] = "--Select--";
                dr["FromDate"] = "01/01/1990";
                dr["ToDate"] = "01/01/1990";
                dr["WTaxPercentage"] = "0.0";
                DT.Rows.Add(dr);
                GRVWithholdingTax.DataSource = DT;
                GRVWithholdingTax.DataBind();
            }
        }
        catch (Exception ex)
        {
            lblMsg.Text = " Error occured in BindGrid : " + ex.Message;
        }
    }

    protected void GRVWithholdingTax_RowCommand(object sender, GridViewCommandEventArgs e)
    {
        try
        {
            lblMsg.Text = "";

            if (e.CommandName == "Add")
            {
                DropDownList ddlDBName = (DropDownList)GRVWithholdingTax.FooterRow.FindControl("ddlFooterDBName");
                TextBox txtFromDate = (TextBox)GRVWithholdingTax.FooterRow.FindControl("txtFromDateFooter");
                TextBox txtToDate = (TextBox)GRVWithholdingTax.FooterRow.FindControl("txtToDateFooter");
                TextBox txtWTaxPercentage = (TextBox)GRVWithholdingTax.FooterRow.FindControl("txtWTaxPercentageFooter");

                if (ddlDBName.SelectedItem.Text == "--Select--")
                {
                    lblMsg.Text = "Please select Database";
                    return;
                }

                if (string.IsNullOrEmpty(txtFromDate.Text))
                {
                    lblMsg.Text = "Please enter from date";
                    return;
                }

                if (string.IsNullOrEmpty(txtToDate.Text))
                {
                    lblMsg.Text = "Please enter To date";
                    return;
                }

                if (string.IsNullOrEmpty(txtWTaxPercentage.Text))
                {
                    lblMsg.Text = "Please enter withholding tax percentage";
                    return;
                }

                DateTime FromDate, ToDate;

                FromDate = Convert.ToDateTime(txtFromDate.Text);
                ToDate = Convert.ToDateTime(txtToDate.Text);

                if (FromDate > ToDate)
                {
                    lblMsg.Text = "ToDate must be greater than FromDate";
                    return;
                }
                
                Db.constr = myGlobal.getAppSettingsDataForKey("tejSAP");
                int NoOfRec = Db.myExecuteScalar("select count(*) NoOfRec from dbo.BPStatus_WithholdingTax Where ('" + txtFromDate.Text + "' between FromDate And ToDate OR '" + txtToDate.Text + "' between FromDate And ToDate ) And DBName='" + ddlDBName.SelectedItem.Text + "' And IsDeleted=0 ");
                if (NoOfRec > 0)
                {
                    lblMsg.Text = "Withholding tax slab already exist for " + ddlDBName.SelectedItem.Text + " between " + txtFromDate.Text + " and " + txtToDate.Text;
                    return;
                }
                else
                {
                    string alertqry = "";
                    string qry = " Insert into dbo.BPStatus_WithholdingTax (DBName,FromDate,ToDate,WTaxPercentage,CreatedOn,CreatedBy) Values ('" + ddlDBName.SelectedItem.Text + "','" + txtFromDate.Text + "','" + txtToDate.Text + "','" + txtWTaxPercentage.Text + "',getdate(),'" + myGlobal.loggedInUser() + "')";
                    Db.myExecuteSQL(qry);
                    lblMsg.Text = "Record saved successfully";
                    BindGrid();
                    ddlDBName.SelectedItem.Text = "--Select--";
                    txtFromDate.Text= "";
                    txtToDate.Text= "";
                    txtWTaxPercentage.Text= "";
                   
                }
            }
            else if (e.CommandName == "Update")
            {
                GridViewRow row = (GridViewRow)(((LinkButton)e.CommandSource).NamingContainer);

                Label lblId = (Label)GRVWithholdingTax.Rows[row.RowIndex].FindControl("lblID");
                DropDownList ddlDBName = (DropDownList)GRVWithholdingTax.Rows[row.RowIndex].FindControl("ddlDBName");
                TextBox txtFromDate = (TextBox)GRVWithholdingTax.Rows[row.RowIndex].FindControl("txtFromDate");
                TextBox txtToDate = (TextBox)GRVWithholdingTax.Rows[row.RowIndex].FindControl("txtToDate");
                TextBox txtWTaxPercentage= (TextBox)GRVWithholdingTax.Rows[row.RowIndex].FindControl("txtWTaxPercentage");

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

                if (string.IsNullOrEmpty(txtFromDate.Text))
                {
                    lblMsg.Text = "Please enter from date";
                    return;
                }

                if (string.IsNullOrEmpty(txtToDate.Text))
                {
                    lblMsg.Text = "Please enter To date";
                    return;
                }

                if (string.IsNullOrEmpty(txtWTaxPercentage.Text))
                {
                    lblMsg.Text = "Please enter withholding tax percentage";
                    return;
                }

                DateTime FromDate, ToDate;

                FromDate = Convert.ToDateTime(txtFromDate.Text);
                ToDate = Convert.ToDateTime(txtToDate.Text);

                if (FromDate > ToDate)
                {
                    lblMsg.Text = "ToDate must be greater than FromDate";
                    return;
                }

                Db.constr = myGlobal.getAppSettingsDataForKey("tejSAP");
                int NoOfRec = Db.myExecuteScalar("select count(*) NoOfRec from dbo.BPStatus_WithholdingTax Where ('" + txtFromDate.Text + "' between FromDate And ToDate OR '" + txtToDate.Text + "' between FromDate And ToDate )  And  DBName='" + ddlDBName.SelectedItem.Text + "' And Id<>" + lblId.Text + " And IsDeleted=0 ");
                if (NoOfRec > 0)
                {
                    lblMsg.Text = "Withholding tax slab already exist for " + ddlDBName.SelectedItem.Text + " between " + txtFromDate.Text + " and " + txtToDate.Text;
                    return;
                }
                else
                {
                    string qry = " Update dbo.BPStatus_WithholdingTax Set DBName='" + ddlDBName.SelectedItem.Text + "', FromDate='" + txtFromDate.Text + "',ToDate='" + txtToDate.Text + "',WTaxPercentage='" + txtWTaxPercentage.Text + "',LastUpdatedOn=getdate(),LastUpdatedBy='" + myGlobal.loggedInUser() + "'  Where Id=" + lblId.Text;
                    Db.myExecuteSQL(qry);

                    lblMsg.Text = "Record updated successfully";
                }
            }
            else if (e.CommandName == "Delete")
            {
                GridViewRow row = (GridViewRow)(((LinkButton)e.CommandSource).NamingContainer);
                Label lblId = (Label)GRVWithholdingTax.Rows[row.RowIndex].FindControl("lblID");
                if (lblId.Text == "0")
                {
                    lblMsg.Text = "You can not delete this record. This is systems default entry.";
                    return;
                }
                else
                {
                    Db.myExecuteSQL("Update BPStatus_WithholdingTax Set IsDeleted=1,LastUpdatedOn=getdate(),LastUpdatedBy='" + myGlobal.loggedInUser() + "' Where Id=" + lblId.Text );
                    lblMsg.Text = "Record Deleted Successfully";
                }
            }
        }
        catch (Exception ex)
        {
            lblMsg.Text = " Failed to " + e.CommandName + "  , Please retry.. " + ex.Message;
        }
    }

    protected void GRVWithholdingTax_RowDeleting(object sender, GridViewDeleteEventArgs e)
    {
        try
        {
            BindGrid();
            GRVWithholdingTax.FooterRow.Visible = true;
        }
        catch (Exception ex)
        {
            lblMsg.Text = "Error occured in Row Deleting " + ex.Message;
        }
    }

    protected void GRVWithholdingTax_RowEditing(object sender, GridViewEditEventArgs e)
    {
        try
        {
            GRVWithholdingTax.EditIndex = e.NewEditIndex;
            BindGrid();
            GRVWithholdingTax.FooterRow.Visible = false;
        }
        catch (Exception ex)
        {
            lblMsg.Text = "Error occured in RowEdit, please retry.." + ex.Message;
        }
    }

    protected void GRVWithholdingTax_RowCancelingEdit(object sender, GridViewCancelEditEventArgs e)
    {
        try
        {
            GRVWithholdingTax.EditIndex = -1;
            BindGrid();
            GRVWithholdingTax.FooterRow.Visible = true;
        }
        catch (Exception ex)
        {
            lblMsg.Text = "Error occured on Cancel RowEdit, please retry.." + ex.Message;
        }
    }

    protected void GRVWithholdingTax_RowUpdating(object sender, GridViewUpdateEventArgs e)
    {
        try
        {
            GRVWithholdingTax.EditIndex = -1;
            BindGrid();
            GRVWithholdingTax.FooterRow.Visible = true;
        }
        catch (Exception ex)
        {
            lblMsg.Text = "Error occured on RowUpdating, please retry.." + ex.Message;
        }
    }
    protected void GRVWithholdingTax_Sorting(object sender, GridViewSortEventArgs e)
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