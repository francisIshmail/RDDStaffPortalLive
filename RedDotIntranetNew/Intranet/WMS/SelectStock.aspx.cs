using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Data;

public partial class Intranet_WMS_SelectStock : System.Web.UI.Page
{
    WMSClsDeleveryorders deleveryOder = new WMSClsDeleveryorders();
    WMSclsReserveStock Stock = new WMSclsReserveStock();
    protected void Page_Load(object sender, EventArgs e)
    {
        if (!IsPostBack)
        {
            Page.Title = "Select Stock";
          DataTable dt =  deleveryOder.selectStock(Request.QueryString["stockID"]);
          if (dt.Rows.Count > 0)
          {
              String qtyStok = dt.Rows[0]["quantity"].ToString();
              String qtyalloc = dt.Rows[0]["allocd"].ToString();
              String QtyReserve = dt.Rows[0]["reserved"].ToString();
              if (string.IsNullOrEmpty(qtyStok) == true)
                  qtyStok = "0";
              if (string.IsNullOrEmpty(qtyalloc) == true)
                  qtyalloc = "0";
              if (string.IsNullOrEmpty(QtyReserve) == true)
                  QtyReserve = "0";


              txtQtyAllocated.Text = qtyalloc;
              txtQtyReserved.Text = QtyReserve;
              txtQtyStock.Text = qtyStok;

              txtQtyAvailable.Text = (int.Parse(qtyStok) - (int.Parse(qtyalloc) + int.Parse(QtyReserve))).ToString();
              txtQtyToAllocate.Text = (int.Parse(qtyStok) - (int.Parse(qtyalloc) + int.Parse(QtyReserve))).ToString();
              gvSelectStock.DataSource = dt;
              gvSelectStock.DataBind();
          
          }

        }
    }

    protected void BtnAllocate_Click(object sender, EventArgs e)
    {
        if (int.Parse(txtQtyAvailable.Text) < int.Parse(txtQtyToAllocate.Text))
        {
            lbmsg.Visible = true;
        }
        else
        {

            String Stockid = ((HiddenField)gvSelectStock.Rows[0].FindControl("hfstockid")).Value;
            String warehouseid = ((HiddenField)gvSelectStock.Rows[0].FindControl("hdwarehouse")).Value;
            String locationid = ((HiddenField)gvSelectStock.Rows[0].FindControl("hdlocation")).Value;
            String coo = ((HiddenField)gvSelectStock.Rows[0].FindControl("hdcoo")).Value;
            String price = ((Label)gvSelectStock.Rows[0].FindControl("lbprice")).Text;
            String boeid = ((Label)gvSelectStock.Rows[0].FindControl("lbboe")).Text;
            String POnumber = ((Label)gvSelectStock.Rows[0].FindControl("lbPonumber")).Text;
            if (coo == "")
                coo = "1";


            int newqty = int.Parse(txtQtyAvailable.Text) - int.Parse(txtQtyToAllocate.Text);

            Stock.updateReserveORAllocate(Stockid, newqty);
            String tempid = deleveryOder.insertTempAllocate(Stockid, warehouseid, locationid, price, boeid, POnumber, Session[RunningCache.UserID].ToString(), coo, txtQtyToAllocate.Text);
            String doid = Request.QueryString["doid"];
            if (Request.QueryString["from"] == "1")

                Response.Redirect("DeliverOder.aspx?doID=" + doid + "&from=SS&tempid=" + tempid);
            else
                Response.Redirect("newDo.aspx?from=SS&tempid=" + tempid + "doID=" + doid);
        }
    }
    protected void btnCancel_Click(object sender, EventArgs e)

    {
        if (Request.QueryString["from"] == "1")
        Response.Redirect("DeliverOder.aspx?from=SS&tempid=&doid=" + Request.QueryString["doid"]);
        else
            Response.Redirect("newDo.aspx?from=SS&tempid=");
    }
}