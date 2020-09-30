using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Data;
public partial class Intranet_WMS_StockMoveHistory : System.Web.UI.Page
{
    protected void Page_Load(object sender, EventArgs e)
    {
        if (!IsPostBack)
        {
            Page.Title = "Stock Mouvement History";
            bindgrid();
        }
    }
    protected void bindgrid()
    {
        WMSclsStockMovHistory sd1 = new WMSclsStockMovHistory();
        DataTable dt1 = new DataTable();

        dt1 = sd1.GVStockMoveHIst();

        GVStockMoveHIst.DataSource = dt1;
        GVStockMoveHIst.DataBind();
    }

    protected void GVStockMoveHIst_PageIndexChanging(object sender, GridViewPageEventArgs e)
    {
        bindgrid();
        GVStockMoveHIst.PageIndex = e.NewPageIndex;
        GVStockMoveHIst.DataBind();
    }
    protected void BTPrint_Click(object sender, EventArgs e)
    {

    }
}