using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

public partial class Intranet_WMS_UserControl_WebUserControl : System.Web.UI.UserControl
{
    protected void Page_Load(object sender, EventArgs e)
    {

    }
    protected void btcustomer_Click(object sender, EventArgs e)
    {
        Response.Redirect("customer.aspx");
    }
    protected void btFrForwarder_Click(object sender, EventArgs e)
    {
        Response.Redirect("freight_forwarder.aspx");
    }
    protected void btSuppliers_Click(object sender, EventArgs e)
    {
        Response.Redirect("Supplier.aspx");

    }
    protected void btproduct_Click(object sender, EventArgs e)
    {
        Response.Redirect("Adminproduct.aspx");
    }
    protected void btWarehouses_Click(object sender, EventArgs e)
    {
        Response.Redirect("warehouse.aspx");
    }
    protected void btcountry_Click(object sender, EventArgs e)
    {
        Response.Redirect("Countries.aspx");
    }
}