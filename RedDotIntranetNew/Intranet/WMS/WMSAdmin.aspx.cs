using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

public partial class Intranet_WMS_WMSAdmin : System.Web.UI.Page
{
    protected void Page_Load(object sender, EventArgs e)
    {
        Page.Title = "Admin";
    }
    protected void btcustomer_Click(object sender, EventArgs e)
    {
        Response.Redirect("customer.aspx");
    }
    protected void btproduct_Click(object sender, EventArgs e)
    {
        Session["fromproduct"] = "admin";
        Response.Redirect("stockItem.aspx");
    }
    protected void btFrForwarder_Click(object sender, EventArgs e)
    {
        Response.Redirect("freight_forwarder.aspx");
    }
    protected void btWarehouses_Click(object sender, EventArgs e)
    {
        Response.Redirect("warehouse.aspx");
    }
   
    protected void Users_Click(object sender, EventArgs e)
    {
        Response.Redirect("users.aspx");
    }
    protected void btAuthorisations_Click(object sender, EventArgs e)
    {

    }
    protected void btSuppliers_Click(object sender, EventArgs e)
    {
        Response.Redirect("Supplier.aspx");
    }
    protected void btcountry_Click(object sender, EventArgs e)
    {
        Response.Redirect("Countries.aspx");
    }
}