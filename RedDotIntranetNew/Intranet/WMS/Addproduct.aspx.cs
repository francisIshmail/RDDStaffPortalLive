using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Data;
public partial class Intranet_WMS_Addproduct : System.Web.UI.Page
{


    ClsAdmin admin = new ClsAdmin();
    protected void Page_Load(object sender, EventArgs e)
    {
        if (!IsPostBack)
        {
            String StockID = Request.QueryString["StockID"].ToString();

            if (String.IsNullOrEmpty(StockID) == false)
            {
                lbtitle.Text = "Product";
                btnSave.Text = "Update";

                bindata(StockID);
            }

        }
    }
    private void bindata(String productID)
    {
        DataTable dt = admin.SelectStock (productID);
        if (dt.Rows.Count >0)
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
    private void bindsupplier()
    {
        
        ddplSuppplier.DataSource = admin.bindsupplier();
        ddplSuppplier.DataTextField = "supplier_name";
        ddplSuppplier.DataValueField = "supplier_id";
    }
    protected void btnSave_Click(object sender, EventArgs e)
    {
 

        String stockid = hdstockID.Value;
        String partnumber = txtPartNumber.Text;
        String desc = txtHscode.Text;
        String hscode = txtHscode .Text ;
        String price = txtprice.Text;
        String packid = txtpackid.Text;
        String grossweight = txtGrossWeight.Text;
        String lenght = txtLength.Text;
        String width = txtWidth.Text;
        String height = txtHeight.Text;
        String supplierId = ddplSuppplier.SelectedValue;
        String category = txtcategory.Text;
        String productline = txtProductLine.Text;

        if (btnSave.Text == "Update")
        {
            admin.updateStock(partnumber,stockid, desc, hscode, price, packid, grossweight, lenght, width, height, supplierId, category, productline);
            Lbmsg.Visible = true;
            Lbmsg.Text = "The Freight Forwarder was updated sucessfully";
        }
        else
        {
           // admin.insertStock(desc, hscode, price, packid, grossweight, lenght, width, height, supplierId, category, productline);
            Lbmsg.Visible = true;
            Lbmsg.Text = "The Freight Forwarder was added sucessfully";
        }
    }
    protected void btnBack_Click(object sender, EventArgs e)
    {

    }
}