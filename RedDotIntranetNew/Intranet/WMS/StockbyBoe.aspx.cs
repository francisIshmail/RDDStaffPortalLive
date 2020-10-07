using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Data;
public partial class Intranet_WMS_StockbyBoe : System.Web.UI.Page
{
    WMSClsStock stock = new WMSClsStock();
    protected void Page_Load(object sender, EventArgs e)
    {
        if (!IsPostBack)
        {
            Page.Title = "Stock by BOE";
            int stockid = int.Parse(Session["StockIDnew"].ToString());
            int warehouse = int.Parse(Session["Stockwarehouseid"].ToString());
            DataTable dt = stock.stockbyboe(stockid, warehouse);

            if (dt.Rows.Count > 0)
            {
                txtWarehouse.Text = dt.Rows[0]["description_warehouse"].ToString();

                txtStock.Text = dt.Rows[0]["stock_id"].ToString();

                txtDescription.Text = dt.Rows[0]["description"].ToString();

                txtPart.Text = dt.Rows[0]["part_number"].ToString();
                gvStockbyboe.DataSource = dt;
                gvStockbyboe.DataBind();

            }
        }
    }
    protected void btnadj_Click(object sender, EventArgs e)
    {
        String StokbyboePrice =((Label)gvStockbyboe.Rows[0].FindControl("lbprice")).Text;
        Response.Redirect("StockAdj.aspx?StokbyboePrice=" + StokbyboePrice);
    }
    protected void btnxfr_Click(object sender, EventArgs e)
    {
        String StokbyboePrice = ((Label)gvStockbyboe.Rows[0].FindControl("lbprice")).Text;
        Response.Redirect("StockXfr.aspx?StokbyboePrice=" + StokbyboePrice);
    }
}