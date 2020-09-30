using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Data;

public partial class Intranet_WMS_ReserveStock : System.Web.UI.Page
{
    WMSclsReserveStock Stock = new WMSclsReserveStock();
    protected void Page_Load(object sender, EventArgs e)
    {
        if (!IsPostBack)
        {
            Page.Title = "Reserve Stock";
            Session["stockid"] =Request.QueryString["Stokid"];

            DataTable dt = Stock.checkStock(int.Parse(Request.QueryString["Stokid"].ToString()));

            if (dt.Rows.Count > 0)
            {


                if (dt.Rows[0]["quantity"].ToString() == "")
                    txtQtyInStock.Text = "0";
                else
                    txtQtyInStock.Text = dt.Rows[0]["quantity"].ToString();

                if (dt.Rows[0]["allocd"].ToString() == "")
                    txtQtyAllocated.Text = "0";
                else
                    txtQtyAllocated.Text = dt.Rows[0]["allocd"].ToString();
                if (dt.Rows[0]["reserved"].ToString() == "")
                    txtQtyReserved.Text = "0";
                else
                    txtQtyReserved.Text = dt.Rows[0]["reserved"].ToString();

                txtQtyAvailable.Text = (int.Parse(txtQtyInStock.Text) - int.Parse(txtQtyReserved.Text) - int.Parse(txtQtyAllocated.Text)).ToString();
                txtQtyToReserve.Text = (int.Parse(txtQtyInStock.Text) - int.Parse(txtQtyReserved.Text) - int.Parse(txtQtyAllocated.Text)).ToString(); 
                bindgrid(int.Parse(Request.QueryString["Stokid"].ToString()));
            }
            else
                if (Request.QueryString["from"]=="1")
                    Response.Redirect("ReserveOrder.aspx?doID=" + Request.QueryString["doID"]);
                else
                    Response.Redirect("NewReseverOder.aspx");
        }
    }



    private void bindgrid(int stockid )
    {
        DataTable dt = Stock.checkStock(stockid );
        gvReserveStock.DataSource = dt;
        gvReserveStock.DataBind();
    }
    protected void Button1_Click(object sender, EventArgs e)
    {


        if (int.Parse(txtQtyAvailable.Text) < int.Parse(txtQtyToReserve.Text))
        {
            lbmsg.Visible = true;
        }
        else
        {

            String partnumber = Stock.getpartnumber(int.Parse(Request.QueryString["Stokid"].ToString()));
            String Stockid = Request.QueryString["Stokid"].ToString();
            String warehouseid = ((HiddenField)gvReserveStock.Rows[0].FindControl("hdwarehouse")).Value;
            String locationid = ((HiddenField)gvReserveStock.Rows[0].FindControl("hdlocation")).Value;
            String itemprice = ((HiddenField)gvReserveStock.Rows[0].FindControl("hdprice")).Value;
            String coo = ((HiddenField)gvReserveStock.Rows[0].FindControl("hdcoo")).Value;
            if (coo == "")
                coo = "1";
            String boeid = ((Label)gvReserveStock.Rows[0].FindControl("loboedid")).Text;
            String qty = txtQtyToReserve.Text;

             int newreserved = int.Parse(txtQtyReserved.Text) + int.Parse(txtQtyToReserve.Text);

            Stock.updateReserveORAllocate(Stockid, newreserved);
            Stock.inserttempreserve(Session[RunningCache.UserID].ToString(), partnumber, int.Parse(Stockid), int.Parse(warehouseid), int.Parse(locationid), int.Parse(qty), boeid, itemprice, int.Parse(coo));

            if (Request.QueryString["from"] == "1")
                Response.Redirect("ReserveOrder.aspx?doID=" + Request.QueryString["doID"]);
            else
                Response.Redirect("NewReseverOder.aspx");
        }
    }
    protected void Button2_Click(object sender, EventArgs e)
    {
        if (Request.QueryString["from"] == "1")
            Response.Redirect("ReserveOrder.aspx?doID=" + Request.QueryString["doID"]);
        else
            Response.Redirect("NewReseverOder.aspx");
       

    }
    protected void gvReserveStock_PageIndexChanging(object sender, GridViewPageEventArgs e)
    {

    }
}