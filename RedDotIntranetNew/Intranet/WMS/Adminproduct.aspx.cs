using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Data;

public partial class Intranet_WMS_Adminproduct : System.Web.UI.Page
{
    ClsAdmin admin = new ClsAdmin();
    WMSclsPrealert prealert = new WMSclsPrealert();
    String search = "";
    protected void Page_Load(object sender, EventArgs e)
    {
        Page.Title = "Products";
        if (!IsPostBack)
            bindgrid(bindSearch());
    }

    protected void bindgrid(string search)
    {
        DataTable dt = prealert.NewPart(search);

        GvNewPart.DataSource = dt;
        GvNewPart.DataBind();
       
    }
    

    protected void GvNewPart_PageIndexChanging(object sender, GridViewPageEventArgs e)
    {
        bindgrid(bindSearch());
        GvNewPart.PageIndex = e.NewPageIndex;
        GvNewPart.DataBind();
    }
 
    protected void GvNewPart_RowCommand(object sender, GridViewCommandEventArgs e)
    {
        int index = Convert.ToInt32(e.CommandArgument) % GvNewPart.PageSize;
        String StockID = ((HiddenField)GvNewPart.Rows[index].FindControl("HdnStockID")).Value;
        hdstockID.Value = StockID;
        if (e.CommandName == "Updating")
        {

            txtSearch.Text = null;
            trSerach.Visible = false;
            btnCancel.Visible = true;
            btnadd.Text = "Update";
            bindata(StockID);
            trdetail.Visible = true;

        }
        if (e.CommandName == "Deleting")
        {
             admin.DeleteStock(StockID);
            bindgrid(bindSearch());
            lbmsg.Text = "The Product has been sucessfully deleted.";
            lbmsg.Visible = true;
        }
    }



    protected void btnadd_Click(object sender, EventArgs e)
    {

        String stockid = hdstockID.Value;
        String partnumber = txtPartNumber.Text;
        String desc = txtDescription.Text;
        String hscode = txtHscode.Text;
        String price = txtprice.Text;
        String packid = txtpackid.Text;
        String grossweight = txtGrossWeight.Text;
        String lenght = txtLength.Text;
        String width = txtWidth.Text;
        String height = txtHeight.Text;
        String supplierId = ddplSuppplier.SelectedValue;
        String category = txtcategory.Text;
        String productline = txtProductLine.Text;

        if (btnadd.Text == "Add New Product")
        {

            txtSearch.Text = null;
            trSerach.Visible = false;
            trdetail.Visible = true;
            btnCancel.Visible = true;
            btnadd.Text = "Save";
            hdstockID.Value = null;
            txtPartNumber.Text = null;
            txtHscode.Text = null;
            txtHscode.Text = null;
            txtprice.Text = null;
            txtpackid.Text = null;
            txtGrossWeight.Text = null;
            txtLength.Text = null;
            txtWidth.Text = null;
            txtHeight.Text = null;
            ddplSuppplier.SelectedValue = null;
            txtcategory.Text = null;
            txtProductLine.Text = null;
            txtDescription.Text = null;
        }

        else if (btnadd.Text == "Update")
        {
            admin.updateStock(partnumber,stockid, desc, hscode, price, packid, grossweight, lenght, width, height, supplierId, category, productline);
            lbmsg.Visible = true;
            lbmsg.Text = "The Product was updated sucessfully";
            txtSearch.Text = null;
            btnadd.Text = "Add New Product";
            trSerach.Visible = true;
            trdetail.Visible = false;
            bindgrid(bindSearch());

        }
        else
        {
            if (string.IsNullOrEmpty(supplierId) == false  && string.IsNullOrEmpty(desc) == false && string.IsNullOrEmpty(hscode) == false)
            {
                admin.insertStock(partnumber,desc, hscode, price, packid, grossweight, lenght, width, height, supplierId, category, productline);
                lbmsg.Visible = true;
                lbmsg.Text = "The Product was added sucessfully";
                bindgrid(bindSearch());
                txtSearch.Text = null;
                trSerach.Visible = true;
                trdetail.Visible = false;
            }
            else
            {
                lbmsg.Visible = true;
                lbmsg.Text = "Please insert all required fields.";
            }
        }
    }
    protected void btnCancel_Click(object sender, EventArgs e)
    {
        trSerach.Visible = true;
        txtSearch.Text = null;
        lbmsg.Visible = false;
        btnadd.Text = "Add New Product";
        trdetail.Visible = false;
        btnCancel.Visible = false;
    }
    protected void lbFirst_Click(object sender, EventArgs e)
    {
        bindgrid(bindSearch());
        GvNewPart.PageIndex = 0;
        GvNewPart.DataBind();
    }
    protected void lbNext_Click(object sender, EventArgs e)
    {
        bindgrid(bindSearch());
        int i = GvNewPart.PageIndex + 1;
        if (i <= GvNewPart.PageCount)
            GvNewPart.PageIndex = i;
        GvNewPart.DataBind();
    }
    protected void lbPrev_Click(object sender, EventArgs e)
    {
        bindgrid(bindSearch());
        int i = GvNewPart.PageCount;
        if (GvNewPart.PageIndex > 0)
            GvNewPart.PageIndex = GvNewPart.PageIndex - 1;
        GvNewPart.DataBind();
    }
    protected void lbLAst_Click(object sender, EventArgs e)
    {
        bindgrid(bindSearch());
        GvNewPart.PageIndex = GvNewPart.PageCount;
        GvNewPart.DataBind();
    }
    protected void btmSearch_Click(object sender, EventArgs e)
    {
        lbmsg.Visible = false;
        bindgrid(bindSearch());
    }
    protected void txtSearch_TextChanged(object sender, EventArgs e)
    {
        lbmsg.Visible = false;
        bindgrid(bindSearch());
    }

    private void bindata(String productID)
    {
        DataTable dt = admin.SelectStock(productID);
        if (dt.Rows.Count > 0)
        {
            hdstockID.Value = dt.Rows[0]["stock_id"].ToString();
            txtPartNumber.Text = dt.Rows[0]["part_number"].ToString();
            txtDescription.Text = dt.Rows[0]["description"].ToString();
            txtHscode.Text = dt.Rows[0]["HScode"].ToString();
            txtprice.Text = dt.Rows[0]["price"].ToString();
            txtpackid.Text = dt.Rows[0]["PackID"].ToString();
            txtGrossWeight.Text = dt.Rows[0]["gross_weight"].ToString();
            txtLength.Text = dt.Rows[0]["length"].ToString();
            txtWidth.Text = dt.Rows[0]["width"].ToString();
            txtHeight.Text = dt.Rows[0]["height"].ToString();
            ddplSuppplier.SelectedValue = dt.Rows[0]["supplier_id"].ToString();
            txtcategory.Text = dt.Rows[0]["category_id"].ToString();
            txtProductLine.Text = dt.Rows[0]["product_line"].ToString();
        }

    }
    public string bindSearch()
    {
        if (RbtSupplier.Checked == true)
            search = " supplier_name like '%" + txtSearch.Text + "%'";
        else if (rbtpartnumber.Checked == true)
            search = " part_number like '%" + txtSearch.Text + "%'";
        return search;
    }
}