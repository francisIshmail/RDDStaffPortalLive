using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Data;
public partial class IntranetNew_Targets_designationmaster : System.Web.UI.Page
{
    protected void Page_Load(object sender, EventArgs e)
    {
        if (!IsPostBack)
        {
            Db.constr = myGlobal.getAppSettingsDataForKey("tejSAP");
            int count = Db.myExecuteScalar("Select COUNT(*) from dbo.Designation_master");
            if (count > 0)
            {
                BindGrid();
              
            }
        }
        lblMsg.Text = "";
    }
    public void BindGrid()
    {
        try
        {
            Db.constr = myGlobal.getAppSettingsDataForKey("tejSAP");
            DataSet DsForms = Db.myGetDS("select * from dbo.Designation_Master ");
            if (DsForms.Tables.Count > 0)
            {
                Gridview1.DataSource = DsForms.Tables[0];
                Gridview1.DataBind();
            }
        }
        catch (Exception ex)
        {
            lblMsg.Text = "Error in BindGrid() : " + ex.Message;
        }
    }



    protected void BtnSave_Click(object sender, EventArgs e)
    {
        try
        {
            
            if (string.IsNullOrEmpty(txtdesignation.Text))
            {
                lblMsg.Text = "Please enter Designation";
                Page.ClientScript.RegisterStartupScript(this.GetType(), "alert", "<script> alert('Please enter Designation'); </script>");
                return;
            }
           

            Db.constr = myGlobal.getAppSettingsDataForKey("tejSAP");

           

            if (BtnSave.Text == "Save")
            {
                 Db.constr = myGlobal.getAppSettingsDataForKey("tejSAP");
                 int count = Db.myExecuteScalar("Select COUNT(*) from dbo.Designation_master where Designation='"+ txtdesignation.Text +"' ");
                 if (count > 0)
                 {
                     lblMsg.Text = " Designation already Exist.";
                 }
                 else
                 {

                     long LeavePeriodID = Db.myExecuteSQLReturnLatestAutoID("Insert into dbo.Designation_master (Designation,CreatedBy,CreatedOn) Values ('" + txtdesignation.Text + "','" + myGlobal.loggedInUser() + "',GETDATE())");

                     if (LeavePeriodID > 0)
                     {
                         lblMsg.Text = " Designation saved successfully";
                         //Page.ClientScript.RegisterStartupScript(this.GetType(), "alert", "<script> alert('Form saved successfully'); </script>");
                     }
                 }
            }
            else if (BtnSave.Text == "Update")
            {
                Db.myExecuteSQL("Update dbo.Designation_master Set Designation='" + txtdesignation.Text + "',LastUpdatedOn=GETDATE() , LastUpdatedBy='" + myGlobal.loggedInUser() + "' Where ID= " + lblMenuID.Text);

                lblMsg.Text = " Form updated successfully";
                //Page.ClientScript.RegisterStartupScript(this.GetType(), "alert", "<script> alert('Form updated successfully'); </script>");
                BtnSave.Text = "Save";
            }
            BindGrid();
            ClearControl();
        }
        catch (Exception ex)
        {
            lblMsg.Text = "Error in BtnSave_Click() : " + ex.Message;
        }
    }


    public void ClearControl()
    {
        lblMenuID.Text = "";
        txtdesignation.Text = "";
        txtid.Text = "";
        pnlFormList.Visible = true;
        BtnSave.Text = "Save";
        Gridview1.SelectedIndex = -1;
    }


    protected void GridView1_RowCommand(object sender, GridViewCommandEventArgs e)
    {
        try
        {
            string ids = e.CommandArgument.ToString();
            if (e.CommandName == "Delete")
            {
                Db.constr = myGlobal.getAppSettingsDataForKey("tejSAP");
                int count = Db.myExecuteScalar("Select COUNT(*) from dbo.tejSalespersonMap where designation=" + ids + "");
            if (count > 0)
            {
               // lblMsg.Text = " You can not Delete this record.";
                Page.ClientScript.RegisterStartupScript(this.GetType(), "alert", "<script> alert('You can not Delete this record.'); </script>");
              
            }
            else
            {
               
                Db.myExecuteSQL("Delete from dbo.Designation_master  Where ID= " + ids);

                lblMsg.Text = " Record Deleted successfully";
            }
            }
        }
        catch (Exception ex)
        {
            lblMsg.Text = "GridView1_RowCommand() " + ex.Message;
        }

    }


    protected void Griview1_RowDeleting(object sender, GridViewDeleteEventArgs e)
    {
        // cancel the automatic delete action
        e.Cancel = true;
        BindGrid();
      
    }



   
    protected void Gridview1_SelectedIndexChanged(object sender, EventArgs e)
    {
        try
        {
           
            BtnSave.Text = "Update";

            lblMsg.Text = "";
            pnlFormList.Visible = false;
            GridViewRow row = Gridview1.SelectedRow;
            lblMsg.Text = "";
            Label lblID = (Label)row.FindControl("lblID");
            Label lbldesignation = (Label)row.FindControl("lbldesignation");
           string ID = lblID.Text;
            string Designation = lbldesignation.Text;
            lblMenuID.Text = ID;
            txtid.Text = ID;
            txtdesignation.Text = Designation;
           
        }
        catch (Exception ex)
        {
            lblMsg.Text = "Gridview1_SelectedIndexChanged() " + ex.Message;
        }
    }

    protected void BtnCancel_Click(object sender, EventArgs e)
    {
        ClearControl();
        lblMsg.Text = "";
    }


    protected void Griview1_PageIndexChanging(object sender, GridViewPageEventArgs e)
    {

        Gridview1.PageIndex = e.NewPageIndex;
        BindGrid();
    }
}