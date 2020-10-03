using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Data;
public partial class Intranet_WMS_freight_forwarder : System.Web.UI.Page
{
    ClsAdmin admin = new ClsAdmin();
    String search = "";
    protected void Page_Load(object sender, EventArgs e)
    {
        if (!IsPostBack)
        {
            Page.Title = "freight Forwarder";
            BindGrid(search);
        }
   }
 


    public void BindGrid( String search)
    {

        gvfreightfowarder.DataSource = admin.bindFreightForwarder(search);
        gvfreightfowarder.DataBind();
    }

    protected void gvfreightfowarder_PageIndexChanging(object sender, GridViewPageEventArgs e)
    {
        BindGrid(search);
        gvfreightfowarder.PageIndex = e.NewPageIndex;
        gvfreightfowarder.DataBind();
    }
 
   
    protected void btnAdd_Click1(object sender, EventArgs e)
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
        if (btnAdd.Text == "Add New Freight Forwarder")
        {
            txtSearch.Text = null;
            trSerach.Visible = false;
            trdetail.Visible = true;
            btnCancel.Visible = true;
            btnAdd.Text = "Save";
            hdfreight.Value = null;
            txtFreightFowarderName.Text = null;
            txtcontact.Text = null;
            txtcell.Text = null;
            txtfax.Text = null;
            txtphone.Text = null;
            txtEmail.Text = null;
            txtAddress.Text = null;
            txtAddress2.Text = null;
            txtAddress3.Text = null;
        }

        else if (btnAdd.Text == "Update")
        {
          

            admin.updateFreightForwarder(forwardername, contact, cell, fax, phone, Email, Address, address2, Address3, freightID);
            txtSearch.Text = null;
            btnAdd.Text = "Add New Freight Forwarder";
            trSerach.Visible = true;
            trdetail.Visible = false;
             lbmsg.Visible = true;
            lbmsg.Text = "The Freight Forwarder was updated sucessfully";
            BindGrid(search);
        }
        else
        {
            admin.InsertFreightForwarder(forwardername, contact, cell, fax, phone, Email, Address, address2, Address3);
            lbmsg.Visible = true;
            lbmsg.Text = "The Freight Forwarder was added sucessfully";
            BindGrid(search);
            txtSearch.Text = null;
            trSerach.Visible = true;
            trdetail.Visible = false;
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
    protected void gvfreightfowarder_RowCommand(object sender, GridViewCommandEventArgs e)
    {
        int index = Convert.ToInt32(e.CommandArgument) % gvfreightfowarder.PageSize;
        String freightID = ((HiddenField)gvfreightfowarder.Rows[index].FindControl("hdfreightID")).Value;
        if (e.CommandName == "Updating")
        {
           
            txtSearch.Text = null;
            trSerach.Visible = false;
            btnCancel.Visible = true;
            btnAdd.Text = "Update";
            bindata(freightID);
            trdetail.Visible = true;

        }
        if (e.CommandName == "Deleting")
        {
            admin.deleteFreightForwarder(freightID);
            BindGrid(search);
            lbmsg.Text = "The Freight Forwarder has been sucessfully deleted.";
            lbmsg.Visible = true;
        }
        
       
    }
    protected void btnCancel_Click(object sender, EventArgs e)
    {
        trSerach.Visible = true;
        txtSearch.Text = null;
        lbmsg.Visible = false;
        btnAdd.Text = "Add New Freight Forwarder";
        trdetail.Visible = false;
        btnCancel.Visible = false;
    }
    protected void txtSearch_TextChanged(object sender, EventArgs e)
    {
        BindGrid(bindSearch());
     
    }
    protected void btmSearch_Click(object sender, EventArgs e)
    {
        BindGrid(bindSearch());
        
    }
    protected void lbFirst_Click(object sender, EventArgs e)
    {
        BindGrid(bindSearch());
        gvfreightfowarder.PageIndex = 0;
        gvfreightfowarder.DataBind();
    }
    protected void lbNext_Click(object sender, EventArgs e)
    {
        BindGrid(bindSearch());
       
        int i = gvfreightfowarder.PageIndex + 1;
        if (i <= gvfreightfowarder.PageCount)
            gvfreightfowarder.PageIndex = i;
        gvfreightfowarder.DataBind();
    }
    protected void lbPrev_Click(object sender, EventArgs e)
    {
      
        BindGrid(bindSearch());
        int i = gvfreightfowarder.PageCount;
        if (gvfreightfowarder.PageIndex > 0)
            gvfreightfowarder.PageIndex = gvfreightfowarder.PageIndex - 1;
        gvfreightfowarder.DataBind();
    }
    protected void lbLAst_Click(object sender, EventArgs e)
    {

        BindGrid(bindSearch());
        gvfreightfowarder.PageIndex = gvfreightfowarder.PageCount;
        gvfreightfowarder.DataBind();
    }

    public String bindSearch()
    {
        if (RbtCompany.Checked == true)
            search = " freight_forwarder_name like '%" + txtSearch.Text + "%'";
        else if (rbtContact.Checked == true)
            search = " contact like '%" + txtSearch.Text + "%'";
        return search;
    }
}


