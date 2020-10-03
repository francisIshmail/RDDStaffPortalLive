using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Data;


public partial class Intranet_WMS_StockAdj : System.Web.UI.Page
{
    WMSClsStock stock = new WMSClsStock();
    protected void Page_Load(object sender, EventArgs e)
    {
        if (!IsPostBack)


        {
            Page.Title = "Stock Adjustement";
            int stockid = int.Parse(Session["StockIDnew"].ToString());
            int warehouse = int.Parse(Session["Stockwarehouseid"].ToString());
            DataTable dt = stock.stockbyboe(stockid, warehouse);

            if (dt.Rows.Count > 0)
            {

                txtPart.Text = dt.Rows[0]["part_number"].ToString();
                txtDescription.Text = dt.Rows[0]["description"].ToString();

                txtBoe.Text = dt.Rows[0]["boe_id"].ToString();

                txtStock.Text = dt.Rows[0]["stock_id"].ToString();

                txtWarehouse.Text = dt.Rows[0]["description_warehouse"].ToString();

                txtQtyAvailable.Text = dt.Rows[0]["quantity"].ToString();


                txtlocation.Text = dt.Rows[0]["location_description"].ToString();

                Hddescription.Value = dt.Rows[0]["warehouse_id"].ToString();
                hdlocation.Value = dt.Rows[0]["location_id"].ToString();

            }

        }
    }
    protected void btncancel_Click(object sender, EventArgs e)
    {
        Response.Redirect("StockbyBoe.aspx");
    }
    protected void btnAdjust_Click(object sender, EventArgs e)
    {
        if (txtAdjustedQty.Text != "")
        {

            int qtyToAdjust = int.Parse(txtAdjustedQty.Text);
            String StokbyboePrice = Request.QueryString["StokbyboePrice"].ToString();
            int warehouseid = int.Parse(Hddescription.Value);
            int stockid = int.Parse(txtStock.Text);
            int locationid = int.Parse(hdlocation.Value);
            String boeid = txtBoe.Text;
            int qtyAvailable = int.Parse(txtQtyAvailable.Text); ;
            String comment = txtcomment.Text;

            stock.adjuststock(qtyToAdjust, StokbyboePrice, warehouseid, stockid, locationid, boeid, qtyAvailable, comment);
            lbmsg.Visible = true;
            lbmsg .Text = "The stock has been adjusted successfully.";

        }
    }
}