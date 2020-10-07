using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Data;

public partial class IntranetNew_DailySalesReport_FunnelSetupMaster : System.Web.UI.Page
{
    protected void Page_Load(object sender, EventArgs e)
    {
        
            Db.constr = myGlobal.getAppSettingsDataForKey("tejSAP");

            int count = Db.myExecuteScalar("Select COUNT(*) from dbo.MenuWiseForms t0 Join dbo.UserAuthorization t1 on t0.MenuId=t1.MenuId and t1.MembershipUserName='" + myGlobal.loggedInUser() + "' And t0.FormURL='ModeOfCallSetUp.aspx' and t1.IsActive=1");
            if (count > 0)
            {
                if (!IsPostBack)
                {
                    Db.constr = myGlobal.getAppSettingsDataForKey("tejSAP");
                    BindGrid();
                }
            }
            else
            {
                Response.Redirect("Default.aspx?UserAccess=0&FormName=Setup - Mode Of Call");
            }

    }

    protected void BindGrid()
    {
        try
        {
            Db.constr = myGlobal.getAppSettingsDataForKey("tejSAP");
            DataTable DT = Db.myGetDS("select * from DSR_ModeOfCall").Tables[0]; 
            if (DT.Rows.Count > 0)
            {
                GRVAutoCLStatusChange.DataSource = DT;
                GRVAutoCLStatusChange.DataBind();
            }
            else
            {
                DataRow dr = DT.NewRow();  // add new row
                dr["ID"] = 0;
                dr["ModeOfCall"] = "";
               
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

            TextBox txtfunnelFooter = (TextBox)GRVAutoCLStatusChange.FooterRow.FindControl("txtfunnelFooter");
            if (txtfunnelFooter.Text == "")
            {
                lblMsg.Text = "Please Enter Status";
                return;
            }
            int CountOfRec = Db.myExecuteScalar("select Count(ModeOfCall) from  DSR_ModeOfCall where ModeOfCall='"+txtfunnelFooter.Text+"'");
            if (CountOfRec > 0)
            {
                lblMsg.Text = "ModeOfCall Already Exist Please Create New";
                return;
            }
            else
            {
                string query = "insert into DSR_ModeOfCall(ModeOfCall,CreatedBy,CreatedOn)values('" + txtfunnelFooter.Text + "','" + myGlobal.loggedInUser() + "',getdate())";
                Db.myExecuteSQL(query);
                lblMsg.Text = "Record saved successfully";
            }
        }
        else if (e.CommandName == "Update")
        {
            GridViewRow row = (GridViewRow)(((LinkButton)e.CommandSource).NamingContainer);

            Label lblID = (Label)GRVAutoCLStatusChange.Rows[row.RowIndex].FindControl("lblID");
            TextBox txtfunnelstedit = (TextBox)GRVAutoCLStatusChange.Rows[row.RowIndex].FindControl("txtfunnelstedit");

            int CountOfRec = Db.myExecuteScalar("select Count(ModeOfCall) from  DSR_ModeOfCall where ModeOfCall='" + txtfunnelstedit.Text + "'");
            if (CountOfRec > 0)
            {

                lblMsg.Text = "ModeOfCall Already Exist Please Create New";
                return;

            }
            string query = "update  DSR_ModeOfCall set ModeOfCall='" + txtfunnelstedit.Text + "',LastUpdatedBy='" + myGlobal.loggedInUser() + "',LastUpdatedOn=getdate()  where ID='" + lblID.Text + "'";
            Db.myExecuteSQL(query);
           
            lblMsg.Text = "Record Updated successfully";
        }
        else if (e.CommandName == "Delete")
        {
            GridViewRow row = (GridViewRow)(((LinkButton)e.CommandSource).NamingContainer);

            Label lblID = (Label)GRVAutoCLStatusChange.Rows[row.RowIndex].FindControl("lblID");

            string query = "Delete from   DSR_ModeOfCall  where ID='" + lblID.Text + "'";
            Db.myExecuteSQL(query);
            lblMsg.Text = "Record Deleted successfully";
        }
        else
        if (e.CommandName == "Edit")
        {
            /*  Asiign value of Grid row level farward details controls to POP up extender controls  */

            GridViewRow row = (GridViewRow)(((LinkButton)e.CommandSource).NamingContainer);
            int visitId = Convert.ToInt32(GRVAutoCLStatusChange.DataKeys[row.RowIndex].Value.ToString());
             
        }

        BindGrid();
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