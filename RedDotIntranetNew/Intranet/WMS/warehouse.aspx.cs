using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Data;
public partial class Intranet_WMS_warehouse : System.Web.UI.Page
{
    ClsAdmin admin = new ClsAdmin();
    String search = "";
    protected void Page_Load(object sender, EventArgs e)
    {

        if (!IsPostBack)
        {
            Page.Title = "Warehouse";
            BindGrid(bindSearch());
        }
    }
    protected void BindGrid(String Search)
    {
        Gvwarehouse.DataSource = admin.BindWarehouse(Search);
        Gvwarehouse.DataBind();
    }

    protected void Gvwarehouse_PageIndexChanging(object sender, GridViewPageEventArgs e)
    {
        BindGrid(bindSearch());
        Gvwarehouse.PageIndex = e.NewPageIndex;
        Gvwarehouse.DataBind();
    }
    protected void btnadd_Click(object sender, EventArgs e)
    {
        String description = txtDescription.Text;
        String Evo = txtEvo.Text;
        String code = txtWarehouseCode.Text;
        String status = txtSatus.Text;
        String warehouseid = hdwarehouseID.Value;


        if (btnadd.Text == "Add New Warehouse")
        {
            txtSearch.Text = null;
            trSerach.Visible = false;
            trdetail.Visible = true;
            btnCancel.Visible = true;
            btnadd.Text = "Save";

            txtDescription.Text = null;
            txtEvo.Text = null;
            txtWarehouseCode.Text = null;
            txtSatus.Text = null;
            hdwarehouseID.Value = null;

        }

        else if (btnadd.Text == "Update")
        {
            admin.updateWarehouse(code, description, Evo, status, warehouseid);
            lbmsg.Visible = true;
            lbmsg.Text = "The Warehouse was updated successfully.";

            txtSearch.Text = null;
            btnadd.Text = "Add New Supplier";
            trSerach.Visible = true;
            trdetail.Visible = false;
           
            BindGrid(bindSearch());
        }
        else
        {
            admin.insertWrehouse(code, description, Evo, status);
            lbmsg.Visible = true;
            lbmsg.Text = "The Warehouse was added successfully.";
            BindGrid(bindSearch());
            txtSearch.Text = null;
            trSerach.Visible = true;
            trdetail.Visible = false;
        }   
    }
    protected void Gvwarehouse_RowCommand(object sender, GridViewCommandEventArgs e)
    {
        int index = Convert.ToInt32(e.CommandArgument) % Gvwarehouse.PageSize;
        
        String warehouseID = ((HiddenField)Gvwarehouse.Rows[index].FindControl("hdwarehouse")).Value;
        if (e.CommandName == "Updating")
        {

            txtSearch.Text = null;
            trSerach.Visible = false;
            btnCancel.Visible = true;
            btnadd.Text = "Update";
            bindata(warehouseID);
            trdetail.Visible = true;

        }
        if (e.CommandName == "Deleting")
        {
            //admin.deleteSupplier(warehouseID);
            BindGrid(bindSearch());
            lbmsg.Text = "The waerehouse has been sucessfully deleted.";
            lbmsg.Visible = true;
        }

    }
    protected void btnCancel_Click(object sender, EventArgs e)
    {
        trSerach.Visible = true;
        txtSearch.Text = null;
        lbmsg.Visible = false;
        btnadd.Text = "Add New Warehouse";
        trdetail.Visible = false;
        btnCancel.Visible = false;

    }
    protected void txtSearch_TextChanged(object sender, EventArgs e)
    {
        lbmsg.Visible = false;
        BindGrid(bindSearch());

    }
    protected void btmSearch_Click(object sender, EventArgs e)
    {
        lbmsg.Visible = false;
        BindGrid(bindSearch());
    }

    private void bindata(String warehouseID)
    {
        DataTable dt = admin.selectWarehouse(warehouseID);
        if (dt.Rows.Count > 0)
        {
            txtDescription.Text = dt.Rows[0]["description"].ToString();
            txtEvo.Text = dt.Rows[0]["warehouseEVO"].ToString();
            txtSatus.Text = dt.Rows[0]["Status"].ToString();
            txtWarehouseCode.Text = dt.Rows[0]["warehouse_code"].ToString();
            hdwarehouseID.Value = dt.Rows[0]["warehouse_id"].ToString();
        }

    }
    public string bindSearch()
    {
        if (RbtCode.Checked == true)
            search = " warehouse_code like '%" + txtSearch.Text + "%'";
        else if (rbtDescription.Checked == true)
            search = " description like '%" + txtSearch.Text + "%'";
        else if (rbtWarehouse.Checked == true)
            search = " warehouseEVO like '%" + txtSearch.Text + "%'";
        return search;
    }
    protected void lbFirst_Click(object sender, EventArgs e)
    {
        BindGrid(bindSearch());
        Gvwarehouse.PageIndex = 0;
        Gvwarehouse.DataBind();
    }
    protected void lbNext_Click(object sender, EventArgs e)
    {
        BindGrid(bindSearch());
        int i = Gvwarehouse.PageIndex + 1;
        if (i <= Gvwarehouse.PageCount)
            Gvwarehouse.PageIndex = i;
        Gvwarehouse.DataBind();

    }
    protected void lbPrev_Click(object sender, EventArgs e)
    {
        BindGrid(bindSearch());
        int i = Gvwarehouse.PageCount;
        if (Gvwarehouse.PageIndex > 0)
            Gvwarehouse.PageIndex = Gvwarehouse.PageIndex - 1;
        Gvwarehouse.DataBind();
    }
    protected void lbLAst_Click(object sender, EventArgs e)
    {
        BindGrid(bindSearch());
        Gvwarehouse.PageIndex = Gvwarehouse.PageCount;
        Gvwarehouse.DataBind();
    }
}