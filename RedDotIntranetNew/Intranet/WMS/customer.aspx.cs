using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Data;
public partial class Intranet_WMS_customer : System.Web.UI.Page
{
    ClsAdmin admin = new ClsAdmin();
    String search = "";
    protected void Page_Load(object sender, EventArgs e)
    {
        if (!IsPostBack)
        {
            bindgrid(search);
        Page.Title = "Customer";
    }
    }
    protected void bindgrid(String search)
    {
        

        DataTable dt = admin.bincustomer(search);
        Gvcustomer.DataSource = dt;
        Gvcustomer.DataBind();

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
            txtAddress5.Text = dt.Rows[0]["address5"].ToString();
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
        

    }




    protected void Gvcustomer_PageIndexChanging(object sender, GridViewPageEventArgs e)
    {
        bindgrid(search);
        Gvcustomer.PageIndex = e.NewPageIndex;
        Gvcustomer.DataBind();

    }
  
    protected void Gvcustomer_RowCommand(object sender, GridViewCommandEventArgs e)
    {
        int index = Convert.ToInt32(e.CommandArgument) % Gvcustomer.PageSize;
           String custID = ((HiddenField)Gvcustomer.Rows[index].FindControl("hdcustid")).Value;
        if (e.CommandName == "Updating")
        {
            txtSearch.Text = null; 
            trSerach.Visible = false;
            btnCancel.Visible = true;
            btadd.Text = "Update";
         
          bindata ( custID);
          trdetail.Visible = true;
        }
        if (e.CommandName == "Deleting")
        {
            admin.deletecustomer(custID);
            bindgrid(search);
            lbmsg.Text = "The customer has been sucessfully deleted.";
            lbmsg.Visible = true;
        }
    }
    protected void Gvcustomer_RowEditing(object sender, GridViewEditEventArgs e)
    {
        Gvcustomer.EditIndex = e.NewEditIndex;
        bindgrid(search );
    }
    protected void Gvcustomer_RowUpdating(object sender, GridViewUpdateEventArgs e)
    {

    }

    protected void btadd_Click(object sender, EventArgs e)
    {
        Session["customerUpdated"] = hdcustid.Value;
        String customername = txtcustName.Text;
        String cell = txtcell.Text;
        String address = txtAddress.Text;
        String address2 = txtAddress2.Text;
        String address3 = txtAddress3.Text;
        String address4 = txtAddress4.Text;
        String address5 = txtAddress5.Text;
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
        if (btadd.Text == "Add New Customer")
        {
            txtSearch.Text = null; 
            trSerach.Visible = false;
            trdetail.Visible = true;
            btnCancel.Visible = true;
            btadd.Text = "Save";
            Session["customerUpdated"] = hdcustid.Value = null;
            txtcustName.Text = null;
            txtcell.Text = null;
            txtAddress.Text = null;
            txtAddress2.Text = null;
            txtAddress3.Text = null;
            txtAddress4.Text = null;
            txtAddress5.Text = null;
            txtTerritory.Text = null;
            txtphone.Text = null;
            txtphone2.Text = null;
            txtfax.Text = null;
            txtEmail.Text = null;
            txtpost1.Text = null;
            txtpost2.Text = null;
            txtPost3.Text = null;
            txtpost4.Text = null;
            txtpost5.Text = null;
            txtcontact.Text = null;
        }
        else if (btadd.Text == "Update")
        {
           
            if (admin.UpdateCustomer(customername, contact, cell, address, address2, address3, address4, address5, territory, phone, phone2, fax, email, post1, post2, post3, post4, post5, Session["customerUpdated"].ToString()) == true)
            {
                txtSearch.Text = null; 
                btadd.Text = "Add New Customer";
                trSerach.Visible = true;
                trdetail.Visible = false;
                lbmsg.Visible = true;
                lbmsg.Text = "The customer was updated sucessfully.";
                bindgrid(search );
            }
        }
        // Add new customer 
        else if (btadd.Text == "Save")
        {
          
            btadd.Text = "Add New Customer";
            if (String.IsNullOrEmpty(customername) == false && String.IsNullOrEmpty(email) == false && String.IsNullOrEmpty(contact) == false && String.IsNullOrEmpty(cell) == false)
            {
                txtSearch.Text = null; 
                trSerach.Visible = true;
                trdetail.Visible = false;
                admin.insertCustomer(customername, contact, cell, address, address2, address3, address4, address5, territory, phone, phone2, fax, email, post1, post2, post3, post4, post5);
                bindgrid(search);
                lbmsg.Text = "The customer has been sucessfully added.";
                lbmsg.Visible = true;
            }
            else
            {
                lbmsg.Visible = true;
                lbmsg.Text = "Please Enter all required Field";
            }
        }

    }
    protected void btnCancel_Click(object sender, EventArgs e)
    {
        trSerach.Visible = true;
        txtSearch.Text = null; 
        lbmsg.Visible = false;
        btadd.Text = "Add New Customer";
        trdetail.Visible = false;
        btnCancel.Visible = false;
    }
    protected void btmSearch_Click(object sender, EventArgs e)
    {
        search = " customer_name like '%" + txtSearch.Text + "%'";
        bindgrid(search);
    }
    protected void txtSearch_TextChanged(object sender, EventArgs e)
    {
        search = " customer_name like '%" + txtSearch.Text + "%'";
        bindgrid(search);
    }
    protected void lbFirst_Click(object sender, EventArgs e)
    {
        search = " customer_name like '%" + txtSearch.Text + "%'";
        bindgrid(search);
        Gvcustomer.PageIndex = 0;
        Gvcustomer.DataBind();
    }
    protected void lbNext_Click(object sender, EventArgs e)
    {
        search = " customer_name like '%" + txtSearch.Text + "%'";
        bindgrid(search);
        int i = Gvcustomer.PageIndex + 1;
        if (i <= Gvcustomer.PageCount)
            Gvcustomer.PageIndex = i;
        Gvcustomer.DataBind();
    }
    protected void lbPrev_Click(object sender, EventArgs e)
    {
        search = " customer_name like '%" + txtSearch.Text + "%'";
        bindgrid(search);
        int i = Gvcustomer.PageCount;
        if (Gvcustomer.PageIndex > 0)
            Gvcustomer.PageIndex = Gvcustomer.PageIndex - 1;
        Gvcustomer.DataBind();
    }
    protected void lbLAst_Click(object sender, EventArgs e)
    {
        search = " customer_name like '%" + txtSearch.Text + "%'";
        bindgrid(search);
        Gvcustomer.PageIndex = Gvcustomer.PageCount;
        Gvcustomer.DataBind();
    }
}