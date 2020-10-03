using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Data;

public partial class Intranet_WMS_AddWarehousel : System.Web.UI.Page
{
    ClsAdmin admin = new ClsAdmin();
    protected void Page_Load(object sender, EventArgs e)
    {
        if (!IsPostBack)
        {
            String warehouseID = Request.QueryString["warehouseID"].ToString();

            if (String.IsNullOrEmpty(warehouseID) == false)
            {
                lbtitle.Text = "Warehouse";
                btnSave.Text = "Update";

                bindata(warehouseID);
            }


        }
    }
    private void bindata(String warehouseID )
    {
        DataTable dt = admin.selectWarehouse(warehouseID);
        if (dt.Rows.Count > 0)
        {
            txtDescription.Text  = dt.Rows[0]["description"].ToString();
            txtEvo.Text = dt.Rows[0]["warehouseEVO"].ToString();
            txtSatus.Text = dt.Rows[0]["Status"].ToString();
            txtWarehouseCode.Text = dt.Rows[0]["warehouse_code"].ToString();
            hdwarehouseID.Value =  dt.Rows[0]["warehouse_id"].ToString();
        }

    }
    protected void btnSave_Click(object sender, EventArgs e)
    {
        String description = txtDescription.Text;
        String Evo = txtEvo.Text;
        String code = txtWarehouseCode.Text;
        String status = txtSatus.Text;
        String warehouseid = hdwarehouseID .Value ;
        if (btnSave.Text == "Update")
        {
            admin.updateWarehouse(code, description, Evo, status, warehouseid);
            lbmsg.Visible = true;
            lbmsg.Text = "The Warehouse was updated successfully.";
        }
        else
        {
            admin.insertWrehouse(code, description, Evo, status);
            lbmsg.Visible = true;
            lbmsg.Text = "The Warehouse was added successfully.";
        }   

    }
    protected void btnback_Click(object sender, EventArgs e)
    {
        Response.Redirect("warehouse.aspx");
    }
}