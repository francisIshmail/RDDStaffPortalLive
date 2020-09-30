using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Data;

public partial class Intranet_WMS_Supplier : System.Web.UI.Page
{

      ClsAdmin admin = new ClsAdmin();
      String search = "";
      protected void Page_Load(object sender, EventArgs e)
      {

          if (!IsPostBack)
          {
              Page.Title = "Supplier";
              BindGrid(bindSearch());
          }
      }

    public void BindGrid(string search)
    {
        DataTable dt = admin.Bindsuppliers(search);
        gvsupplier.DataSource = dt;
        gvsupplier.DataBind();
    }

    protected void gvsupplier_PageIndexChanging(object sender, GridViewPageEventArgs e)
    {
       BindGrid(bindSearch());
        gvsupplier.PageIndex = e.NewPageIndex;
        gvsupplier.DataBind();
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



    protected void gvsupplier_RowCommand(object sender, GridViewCommandEventArgs e)
    {
        int index = Convert.ToInt32(e.CommandArgument) % gvsupplier.PageSize;
        String supplierID = ((HiddenField)gvsupplier.Rows[index].FindControl("hdsupplier")).Value;
        if (e.CommandName == "Updating")
        {

            txtSearch.Text = null;
            trSerach.Visible = false;
            btnCancel.Visible = true;
            btnadd.Text = "Update";
            bindata(supplierID);
            trdetail.Visible = true;

        }
        if (e.CommandName == "Deleting")
        {
            admin.deleteSupplier(supplierID);
            BindGrid(bindSearch());
            lbmsg.Text = "The Supplier has been sucessfully deleted.";
            lbmsg.Visible = true;
        }

    }

    protected void btmSearch_Click(object sender, EventArgs e)
    {
        lbmsg.Visible = false;
        BindGrid(bindSearch());
    }
    protected void lbFirst_Click(object sender, EventArgs e)
    {
        BindGrid(bindSearch());
        gvsupplier.PageIndex = 0;
        gvsupplier.DataBind();
    }
    protected void lbNext_Click(object sender, EventArgs e)
    {
        BindGrid(bindSearch());
        int i = gvsupplier.PageIndex + 1;
        if (i <= gvsupplier.PageCount)
            gvsupplier.PageIndex = i;
        gvsupplier.DataBind();
    }
    protected void lbPrev_Click(object sender, EventArgs e)
    {
        BindGrid(bindSearch());
        int i = gvsupplier.PageCount;
        if (gvsupplier.PageIndex > 0)
            gvsupplier.PageIndex = gvsupplier.PageIndex - 1;
        gvsupplier.DataBind();
    }
    protected void lbLAst_Click(object sender, EventArgs e)
    {
        BindGrid(bindSearch());
        gvsupplier.PageIndex = gvsupplier.PageCount;
        gvsupplier.DataBind();
    }
    public string  bindSearch()
    {
        if (RbtSupplier.Checked == true)
            search = " supplier_name like '%" + txtSearch.Text + "%'";
        else if (rbtContact.Checked == true)
            search = " contact_name like '%" + txtSearch.Text + "%'";
        return search;
    }
    protected void btnadd_Click(object sender, EventArgs e)
    {
        String supplierID = hdsupplierid.Value;
        String supplierName = txtSupplierName.Text;
        String contact = txtcontact.Text;
        String phone = txtphone.Text;
        String fax = txtfax.Text;
        String cell = txtcell.Text;



        if (btnadd.Text == "Add New Supplier")
        {
            txtSearch.Text = null;
            trSerach.Visible = false;
            trdetail.Visible = true;
            btnCancel.Visible = true;
            btnadd.Text = "Save";

            hdsupplierid.Value = null;
            txtSupplierName.Text = null;
            txtcontact.Text = null;
            txtphone.Text = null;
            txtfax.Text = null;
            txtcell.Text = null;
        }

        else if (btnadd.Text == "Update")
        {
            admin.updateSupplier(supplierID, supplierName, contact, phone, cell, fax);
            txtSearch.Text = null;
            btnadd.Text = "Add New Supplier";
            trSerach.Visible = true;
            trdetail.Visible = false;
            lbmsg.Visible = true;
            lbmsg.Text = "The supplier was updated successfully.";
            BindGrid(bindSearch());
        }
        else
        {
            admin.insertSupplier(supplierName, contact, phone, cell, fax);
            lbmsg.Visible = true;
            lbmsg.Text = "The supplier was added successfully.";
            BindGrid(bindSearch());
            txtSearch.Text = null;
            trSerach.Visible = true;
            trdetail.Visible = false;
        }    
    }
    protected void btnCancel_Click(object sender, EventArgs e)
    {
        trSerach.Visible = true;
        txtSearch.Text = null;
        lbmsg.Visible = false;
        btnadd.Text = "Add New Supplier";
        trdetail.Visible = false;
        btnCancel.Visible = false;
    }
    protected void txtSearch_TextChanged(object sender, EventArgs e)
    {

        lbmsg.Visible = false;
        BindGrid(bindSearch());
    }
}