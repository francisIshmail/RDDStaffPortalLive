using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Data;
public partial class Intranet_WMS_NewCustomer : System.Web.UI.Page
{
    ClsAdmin admin = new ClsAdmin();
    protected void Page_Load(object sender, EventArgs e)
    {
        if (!IsPostBack)
        {

            String custID = Request.QueryString["custID"].ToString();

            if (String.IsNullOrEmpty(custID) == false)
            {
                lbtitle.Text = "Update Customer";
                btnSave.Text = "Update";

                bindata(custID);
            }
        }
    }


    private void bindata(String custID)
    {
        DataTable dt = admin.selectCustomer(custID);
        if (dt.Rows.Count > 0)
        {
            txtcustName.Text = dt.Rows[0]["customer_name"].ToString();
            txtcell.Text = dt.Rows[0]["cell"].ToString();
            txtAddress.Text = dt.Rows[0]["address1"].ToString();
            txtAddress2.Text = dt.Rows[0]["address2"].ToString();
            txtAddress3.Text = dt.Rows[0]["address3"].ToString();
            txtAddress4.Text = dt.Rows[0]["address4"].ToString();
         //  txtAddress5.Text = dt.Rows[0]["address5"].ToString();
            txtTerritory.Text = dt.Rows[0]["territory"].ToString();
            txtphone.Text = dt.Rows[0]["telephone"].ToString();
            txtphone2.Text = dt.Rows[0]["telephone2"].ToString();
            txtfax.Text = dt.Rows[0]["fax1"].ToString();
            txtEmail.Text = dt.Rows[0]["email"].ToString();
            txtpost1.Text = dt.Rows[0]["post1"].ToString();
            txtpost2.Text = dt.Rows[0]["post2"].ToString();
            txtPost3.Text = dt.Rows[0]["post3"].ToString();
            txtpost4.Text = dt.Rows[0]["post4"].ToString();
            txtpost5.Text = dt.Rows[0]["post5"].ToString();
            txtcontact.Text = dt.Rows[0]["customer_contact"].ToString();
            hdcustid.Value = dt.Rows[0]["customer_id"].ToString();
        }

    }
    protected void btnSave_Click(object sender, EventArgs e)
    {
        Session["customerUpdated"] = hdcustid.Value;
        String customername = txtcustName.Text;
        String cell = txtcell.Text;
        String address = txtAddress.Text;
        String address2 = txtAddress2.Text;
        String address3 = txtAddress3.Text;
        String address4 = txtAddress4.Text;
        String address5 = "";
        String territory = txtTerritory.Text;
        String phone = txtphone.Text;
        String phone2 = txtphone2.Text;
        String fax = txtfax.Text;
        String email = txtEmail.Text;
        String post1 = txtpost1.Text;
        String post2 = txtpost2.Text;
        String post3 = txtPost3.Text;
        String post4 = txtpost4.Text;
        String post5 = txtpost5.Text;
        String contact = txtcontact.Text;

        // update cusotmer 
        if (btnSave.Text == "Update")
        {

            if (admin.UpdateCustomer(customername, contact,cell, address, address2, address3, address4, address5, territory, phone, phone2, fax, email, post1, post2, post3, post4, post5, Session["customerUpdated"].ToString()) == true)
            {
                bindata(Session["customerUpdated"].ToString ());
                lbmsg.Visible = true;
                lbmsg.Text = "The customer was updated sucessfully.";
            }
        }
        // Add new customer 
        else
        {
            if (admin.insertCustomer(customername,contact, cell, address, address2, address3, address4, address5, territory, phone, phone2, fax, email, post1, post2, post3, post4, post5) == true)
            {
                Response.Redirect("customer.aspx");
                
            }
        }


    }
    protected void btnback_Click(object sender, EventArgs e)
    {
        Response.Redirect("customer.aspx");
    }
}