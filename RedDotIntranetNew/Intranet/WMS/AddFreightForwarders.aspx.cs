using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Data;
public partial class Intranet_WMS_AddFreightForwarders : System.Web.UI.Page
{
    ClsAdmin admin = new ClsAdmin(); 
    protected void Page_Load(object sender, EventArgs e)
    {
        if (!IsPostBack)
        {
            try
            {
                Page.Title = "Add Freight Forwarder";
                String freightID = Request.QueryString["freightID"].ToString();

                if (String.IsNullOrEmpty(freightID) == false)
                {
                    lbtitle.Text = "Freight Forwarder";
                    btnSave.Text = "Update";

                    bindata(freightID);
                }
            }
            catch (Exception ex)
            {
                Response.Redirect("~/login.aspx");
            }

        }
    }
    private void bindata(String freightID)
    {
        DataTable dt = admin.selectFreightForwarder(freightID);
        if (dt.Rows.Count > 0)
        {
            hdfreight.Value = dt.Rows[0]["freight_forwarder_id"].ToString();
            txtFreightFowarderName.Text = dt.Rows[0]["freight_forwarder_name"].ToString();
            txtcontact.Text = dt.Rows[0]["contact"].ToString();
            txtcell.Text = dt.Rows[0]["cell"].ToString();
            txtfax.Text = dt.Rows[0]["fax"].ToString();
            txtphone.Text = dt.Rows[0]["phone"].ToString();
            txtEmail.Text = dt.Rows[0]["email"].ToString();
            txtAddress.Text = dt.Rows[0]["address1"].ToString();
            txtAddress2.Text = dt.Rows[0]["address2"].ToString();
            txtAddress3.Text = dt.Rows[0]["address3"].ToString();
        }
    }
    protected void btnSave_Click(object sender, EventArgs e)
    {

        String freightID = hdfreight.Value;
        String forwardername = txtFreightFowarderName.Text;
        String contact = txtcontact.Text;
        String cell = txtcell.Text;
        String fax = txtfax.Text;
        String phone = txtphone.Text;
        String Email = txtEmail.Text;
        String Address = txtAddress.Text;
        String address2 = txtAddress2.Text;
        String Address3 = txtAddress3.Text;

        if (btnSave.Text == "Update")
        {
            admin.updateFreightForwarder(forwardername, contact, cell, fax, phone, Email, Address, address2, Address3, freightID);
            Lbmsg.Visible = true;
            Lbmsg.Text = "The Freight Forwarder was updated sucessfully";
        }
        else
        {
            admin.InsertFreightForwarder(forwardername, contact, cell, fax, phone, Email, Address, address2, Address3);
            Lbmsg.Visible = true;
            Lbmsg.Text = "The Freight Forwarder was added sucessfully";
        }
    }
    protected void btnback_Click(object sender, EventArgs e)
    {
        Response.Redirect("freight_forwarder.aspx");
    }
}