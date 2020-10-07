using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Data;

public partial class Intranet_WMS_StockXfr : System.Web.UI.Page
{
    WMSClsStock stock = new WMSClsStock();
    protected void Page_Load(object sender, EventArgs e)
    {
        if (!IsPostBack)
        {
            Page.Title = "Stock Transfer";
            int stockid = int.Parse(Session["StockIDnew"].ToString());
            int warehouse = int.Parse(Session["Stockwarehouseid"].ToString());
            DataTable dt = stock.stockbyboe(stockid, warehouse);

            if (dt.Rows.Count > 0)
            {
                txtDescription.Text = dt.Rows[0]["description"].ToString();

                txtpart.Text = dt.Rows[0]["part_number"].ToString();

                txtboe.Text = dt.Rows[0]["boe_id"].ToString();

                txtStockid.Text = dt.Rows[0]["stock_id"].ToString();

                txtwarehouse.Text = dt.Rows[0]["description_warehouse"].ToString();

                hdwarehouse.Value = dt.Rows[0]["warehouse_id"].ToString();

                txtlocation.Text = dt.Rows[0]["location_description"].ToString();

                hdlocation.Value = dt.Rows[0]["location_id"].ToString();

                txtQtyAvail.Text = dt.Rows[0]["quantity"].ToString();

              
            }
            bindWarehouse();
            bindlocation();
        }
    }
    private void bindWarehouse()
    { 
        dplwarehouse.DataSource  = stock.BindWarehouse();
        dplwarehouse.DataTextField = "description";
        dplwarehouse.DataValueField = "warehouse_id";
        dplwarehouse.DataBind();

         
    }

    private void bindlocation()
    {
        dplLocation.DataSource = stock.bindlocation();
        dplLocation.DataTextField = "location_description";
        dplLocation.DataValueField = "location_id";
        dplLocation.DataBind();
    }
    protected void Button2_Click(object sender, EventArgs e)
    {
        Response.Redirect("StockbyBoe.aspx");
    }
    protected void Button1_Click(object sender, EventArgs e)
    {
        int Sourcewarehoueid = int.Parse(hdwarehouse .Value );
        string stockid = txtStockid .Text;
        int Destwarehoueid = int.Parse (dplwarehouse .SelectedValue );
        int qtyToXfr = int.Parse(txtQtyTransfer .Text );
        String StokbyboePrice =Request.QueryString["StokbyboePrice"].ToString();
        String boeid = txtboe .Text ;

        int Sourcelocationid =int.Parse (hdlocation .Value ); 
        int Destlocationid= int.Parse (dplLocation .SelectedValue );
        String comment = txtcomment.Text;
        stock.StockXFR(Sourcewarehoueid, stockid, Destwarehoueid, qtyToXfr, StokbyboePrice, boeid, Sourcelocationid, Destlocationid, comment);
        Lbmsg.Visible = true;
        Lbmsg.Text = " The Stock has been successfully transfered"; 

    }
}