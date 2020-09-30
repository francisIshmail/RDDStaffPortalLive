using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Data;

public partial class Intranet_WMS_Addsupplier : System.Web.UI.Page
{
    ClsAdmin admin = new ClsAdmin();
    protected void Page_Load(object sender, EventArgs e)
    {
        if (!IsPostBack)
        {
            String SupplierID = Request.QueryString["SupplierID"].ToString();

            if (String.IsNullOrEmpty(SupplierID) == false)
            {
                lbtitle.Text = "Freight Forwarder";
                btnSave.Text = "Update";

                bindata(SupplierID);
            }


        }
    }

    private void bindata(String SupplierID)
    {
        DataTable dt = admin.Selectsupplier(SupplierID);
        if (dt.Rows.Count > 0)
        {
            hdsupplierid.Value = dt.Rows[0]["supplier_id"].ToString();
            txtSupplierName.Text = dt.Rows[0]["supplier_name"].ToString();
            txtcontact.Text = dt.Rows[0]["contact_name"].ToString();
            txtphone.Text = dt.Rows[0]["phone_number"].ToString();
            txtfax.Text = dt.Rows[0]["fax_number"].ToString();
            txtcell.Text = dt.Rows[0]["cell_number"].ToString();
        }


    }
    protected void btnSave_Click(object sender, EventArgs e)
    {

        String supplierID = hdsupplierid.Value;
        String supplierName = txtSupplierName.Text;
        String contact = txtcontact.Text;
        String phone = txtphone.Text;
        String fax = txtfax.Text;
        String cell=  txtcell.Text ;


        if (btnSave.Text == "Update")
        {
            admin.updateSupplier(supplierID , supplierName, contact ,phone, cell,fax);
            lbmsg.Visible = true;
            lbmsg.Text = "The supplier was updated successfully.";
        }
        else
        {
            admin.insertSupplier(supplierName, contact, phone, cell, fax);
            lbmsg.Visible = true;
            lbmsg.Text = "The supplier was added successfully.";
        }    
    }
    protected void btnback_Click(object sender, EventArgs e)
    {
        Response.Redirect("Supplier.aspx");

    }
    
}