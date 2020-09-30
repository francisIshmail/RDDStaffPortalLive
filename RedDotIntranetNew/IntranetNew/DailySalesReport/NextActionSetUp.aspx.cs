using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Data;

public partial class IntranetNew_DailySalesReport_NextActionSetUp : System.Web.UI.Page
{
    protected void Page_Load(object sender, EventArgs e)
    {


        Db.constr = myGlobal.getAppSettingsDataForKey("tejSAP");

        //int count = Db.myExecuteScalar("Select COUNT(*) from dbo.MenuWiseForms t0 Join dbo.UserAuthorization t1 on t0.MenuId=t1.MenuId and t1.MembershipUserName='" + myGlobal.loggedInUser() + "' And t0.FormURL='DSRSetupMaster.aspx' and t1.IsActive=1");
        // if (count > 0)
        /// {
        if (!IsPostBack)
        {
            Db.constr = myGlobal.getAppSettingsDataForKey("tejSAP");
            BindGrid();
        }
        // }
        // else
        // {
        //  Response.Redirect("Default.aspx?UserAccess=0&FormName=Setup - Call Status");
        // }

    }

    protected void BindGrid()
    {
        try
        {
            Db.constr = myGlobal.getAppSettingsDataForKey("tejSAP");
            DataTable DT = Db.myGetDS("select * from DSR_NextAction").Tables[0];
            if (DT.Rows.Count > 0)
            {
                GRVAutoCLStatusChange.DataSource = DT;
                GRVAutoCLStatusChange.DataBind();

            }

            else
            {
                DataRow dr = DT.NewRow();  // add new row
                dr["ID"] = 0;
                dr["NextAction"] = "";

                DT.Rows.Add(dr);
                GRVAutoCLStatusChange.DataSource = DT;
                GRVAutoCLStatusChange.DataBind();
            }
        }
        catch (Exception ex)
        {
            lblMsg.Text = " Error occured in BindGrid : " + ex.Message;
        }
    }
    protected void GRVAutoCLStatusChange_RowCommand(object sender, GridViewCommandEventArgs e)
    {
        lblMsg.Text = "";
        if (e.CommandName == "Add")
        {
            Db.constr = myGlobal.getAppSettingsDataForKey("tejSAP");
            string LoggedInUserName = myGlobal.loggedInUser();

            TextBox txtNxtActionFooter = (TextBox)GRVAutoCLStatusChange.FooterRow.FindControl("txtNxtActionFooter");
            if (txtNxtActionFooter.Text == "")
            {
                lblMsg.Text = "Please Enter NextAction";
                return;
            }

            int CountOfRec = Db.myExecuteScalar("select Count(NextAction) from  DSR_NextAction where NextAction='" + txtNxtActionFooter.Text + "'");
            if (CountOfRec > 0)
            {

                lblMsg.Text = "NextAction Already Exist Please Create New";
                return;

            }
            else
            {
                string query = "insert into DSR_NextAction(NextAction,CreatedBy,CreatedOn)values('" + txtNxtActionFooter.Text + "','" + myGlobal.loggedInUser() + "',getdate())";
                Db.myExecuteSQL(query);
                lblMsg.Text = "Record saved successfully";

                BindGrid();
            }
        }


        else if (e.CommandName == "Update")
        {
            GridViewRow row = (GridViewRow)(((LinkButton)e.CommandSource).NamingContainer);

            Label lblID = (Label)GRVAutoCLStatusChange.Rows[row.RowIndex].FindControl("lblID");
            TextBox txtNxtActionedit = (TextBox)GRVAutoCLStatusChange.Rows[row.RowIndex].FindControl("txtNxtActionedit");

            int CountOfRec = Db.myExecuteScalar("select Count(NextAction) from  DSR_NextAction where NextAction='" + txtNxtActionedit.Text + "'");
            if (CountOfRec > 0)
            {

                lblMsg.Text = "Next Action Already Exist Please Create New";
                return;

            }

            string query = "update  DSR_NextAction set NextAction='" + txtNxtActionedit.Text + "',LastUpdatedBy='" + myGlobal.loggedInUser() + "',LastUpdatedOn=getdate()  where ID='" + lblID.Text + "'";
            Db.myExecuteSQL(query);
            lblMsg.Text = "Record Updated successfully";

        }

        else if (e.CommandName == "Delete")
        {
            GridViewRow row = (GridViewRow)(((LinkButton)e.CommandSource).NamingContainer);

            Label lblID = (Label)GRVAutoCLStatusChange.Rows[row.RowIndex].FindControl("lblID");

            string query = "Delete from   DSR_NextAction  where ID='" + lblID.Text + "'";
            Db.myExecuteSQL(query);
            lblMsg.Text = "Record Deleted successfully";
            BindGrid();

        }
        else
            if (e.CommandName == "Edit")
            {
                /*  Asiign value of Grid row level farward details controls to POP up extender controls  */

                GridViewRow row = (GridViewRow)(((LinkButton)e.CommandSource).NamingContainer);
                int visitId = Convert.ToInt32(GRVAutoCLStatusChange.DataKeys[row.RowIndex].Value.ToString());

            }
    }
    protected void GRVAutoCLStatusChange_RowDeleting(object sender, GridViewDeleteEventArgs e)
    {
        try
        {
            BindGrid();
            GRVAutoCLStatusChange.FooterRow.Visible = true;
        }
        catch (Exception ex)
        {
            lblMsg.Text = "Error occured in Row Deleting " + ex.Message;
        }
    }

    protected void GRVAutoCLStatusChange_RowEditing(object sender, GridViewEditEventArgs e)
    {
        try
        {
            GRVAutoCLStatusChange.EditIndex = e.NewEditIndex;
            //   GRVAutoCLStatusChange.FooterRow.Visible = false;
        }
        catch (Exception ex)
        {
            lblMsg.Text = "Error occured in RowEdit, please retry.." + ex.Message;
        }
    }

    protected void GRVAutoCLStatusChange_RowCancelingEdit(object sender, GridViewCancelEditEventArgs e)
    {
        try
        {
            GRVAutoCLStatusChange.EditIndex = -1;
            BindGrid();
            GRVAutoCLStatusChange.FooterRow.Visible = true;
        }
        catch (Exception ex)
        {
            lblMsg.Text = "Error occured on Cancel RowEdit, please retry.." + ex.Message;
        }
    }

    protected void GRVAutoCLStatusChange_RowUpdating(object sender, GridViewUpdateEventArgs e)
    {
        try
        {
            GRVAutoCLStatusChange.EditIndex = -1;
            BindGrid();
            GRVAutoCLStatusChange.FooterRow.Visible = true;
        }
        catch (Exception ex)
        {
            lblMsg.Text = "Error occured on RowUpdating, please retry.." + ex.Message;
        }
    }
   
}