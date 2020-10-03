using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Data;

public partial class Intranet_WMS_BoeDetails : System.Web.UI.Page
{
    WMSclsPrealert prealert = new WMSclsPrealert();
    protected void Page_Load(object sender, EventArgs e)
    {


      
        if (!IsPostBack)
        {
            Page.Title = "Receive Goods";
            if (Request.QueryString["boeID"] != null)
                Session[RunningCache.boeID] = Request.QueryString["boeID"];
                BindField(Session[RunningCache.boeID].ToString());
        }
    }           
    protected void BindField(string boeid)
    {
   DataTable dt1 = prealert.receivedgood(boeid);
             if (dt1.Rows.Count > 0)
        {
            gvBoeDetails.DataSource = dt1;
            gvBoeDetails.DataBind();
            tbPrealertID.Text = dt1.Rows[0]["prealert_id"].ToString();
            TbStatus.Text = dt1.Rows[0]["status"].ToString();

            switch (dt1.Rows[0]["status"].ToString())
            {
                case "DRAFT":
                    btconfirm.Enabled = true;
                    bttally.Enabled = true;
                    btsave.Enabled = true;
                    break;
                case "RECEIVED":
                    btconfirm.Enabled = false;
                    bttally.Enabled = true;
                    btsave.Enabled = false;
                    break;
                case "CONFIRMED":
                    btconfirm.Enabled = false;
                    bttally.Enabled = true;
                    btsave.Enabled = false;
                    break;
                default:
                    break;
            }



            txtArrivalDate.Text = dt1.Rows[0]["actual_arrival_date"].ToString();
            TbBoe.Text = dt1.Rows[0]["boe_id"].ToString();
            TbCreateDate.Text = dt1.Rows[0]["creation_date"].ToString();


            if (int.Parse(dt1.Rows[0]["shipping_method"].ToString()) == 1)
                rbsea.Checked = true;

            else if (int.Parse(dt1.Rows[0]["shipping_method"].ToString()) == 2)
                rbair.Checked = true;

            else if (int.Parse(dt1.Rows[0]["shipping_method"].ToString()) == 3)
                rbland.Checked = true;

            if (int.Parse(dt1.Rows[0]["delivery_method"].ToString()) == 1)
                rbddu.Checked = true;

            else if (int.Parse(dt1.Rows[0]["delivery_method"].ToString()) == 2)
                rbfob.Checked = true;

            else if (int.Parse(dt1.Rows[0]["delivery_method"].ToString()) == 3)
                rbexword.Checked = true;

            else if (int.Parse(dt1.Rows[0]["delivery_method"].ToString()) == 4)
                rbcnf.Checked = true;


            tbShipRef.Text = dt1.Rows[0]["shipping_reference"].ToString();
            tbcomment.Text = dt1.Rows[0]["remarks"].ToString();
            TbToal.Text = dt1.Rows[0]["total_quantity"].ToString();
            tbsupplier.Text = dt1.Rows[0]["supplier_name"].ToString();
            TbSupReference.Text = dt1.Rows[0]["supplier_reference"].ToString();
            tbForwarder.Text = dt1.Rows[0]["freight_forwarder"].ToString();

        }
    }
    protected void bttally_Click(object sender, EventArgs e)
    {
        Response.Redirect("Crystal_Prealert_Unstuffing.aspx?PrealertId=" + tbPrealertID.Text+ "&from=1");
    }
    protected void btnUpdate_Click(object sender, EventArgs e)
    {

        btnAdd.Visible = true;
        if (btnUpdate.Text == "Save")
        {

            //Add new ....

            String stockid = ddlPartNumber.SelectedValue;
            String quantity = txtQty.Text;
            String itemprice = txtItemPrice.Text;
            String itemvolume = txtVol.Text;
            String grossweight = txtGrossWt.Text;
            String totalgrossweight = txtTotGrossVol.Text;
            String totalprice = txtTotPrice.Text;
            String totalvolume = txtTotVol.Text;
            String POnumber = txtPoNumber.Text;
            String warehouseid = ddlwarehouse.SelectedValue;
            String locationid = ddlLocation.SelectedValue;
            String boeid = txtBoe.Text;
            String COO = dplCountry.SelectedValue;
           

          //  DeliveryOrder.NewdoDetails(tbDO.Text, stockid, quantity, itemprice, itemvolume, grossweight, totalprice, totalgrossweight, totalvolume, POnumber, warehouseid, locationid, boeid, COO, invoiceqty, invoiceprice);
            BindField(Session[RunningCache.boeID].ToString());
            trDetails.Visible = false;
            lbmsg.Text = "The bill of entry order detail has been successfully added";
            trmessage.Visible = true;

        }
        else
        {
            //update .....
            String BoeDetaiID = hdnBoeDetaiID1.Value;
            String stockid = ddlPartNumber.SelectedValue;
            String quantity = "0";
            String itemprice = "0";
            String itemvolume = "0";
            String grossweight = "0";
            String totalgrossweight = "0";
            String totalprice = "0";
            String totalvolume = "0";
            
            if (String.IsNullOrEmpty(txtQty.Text) == false)
                quantity = txtQty.Text;
            if (String.IsNullOrEmpty(txtItemPrice.Text) == false)
                itemprice = txtItemPrice.Text;
            if (String.IsNullOrEmpty(txtVol.Text) == false)
                itemvolume = txtVol.Text;
            if (String.IsNullOrEmpty(txtGrossWt.Text) == false)
                grossweight = txtGrossWt.Text;
            if (String.IsNullOrEmpty(txtTotGrossVol.Text) == false)
                totalgrossweight = txtTotGrossVol.Text;
            if (String.IsNullOrEmpty(txtTotPrice.Text) == false)
                totalprice = txtTotPrice.Text;
            if (String.IsNullOrEmpty(txtTotVol.Text) == false)
                totalvolume = txtTotVol.Text;
          
            String POnumber = txtPoNumber.Text;
            String warehouseid = ddlwarehouse.SelectedValue;
            String locationid = ddlLocation.SelectedValue;
            String boeid = txtBoe.Text;
            String COO = dplCountry.SelectedValue;


            prealert.updateBoeDetails(warehouseid, locationid, POnumber, COO, quantity, itemvolume, totalvolume, grossweight, totalgrossweight, itemprice, totalprice, BoeDetaiID);
            BindField(Request.QueryString["boeID"]);
            trDetails.Visible = false;
            trmessage.Visible = true;
        }


       
    }
    protected void btnCancel_Click(object sender, EventArgs e)
    {
        btnAdd.Visible = true;
        trDetails.Visible = false;
        trmessage.Visible = false;
        
        ddlPartNumber.SelectedValue = null;
        ddlLocation.SelectedValue = null;
        ddlwarehouse.SelectedValue = null;
        txtBoe.Text = null;
        txtPoNumber.Text = null;
        txtQty.Text = null;
       
        txtItemPrice.Text = null;
        txtTotPrice.Text = null;
        txtVol.Text = null;
        txtTotVol.Text = null;
        txtTotGrossVol.Text = null;
        txtGrossWt.Text = null;
       
    }
    protected void gvBoeDetails_RowCommand(object sender, GridViewCommandEventArgs e)
    {
        int index = Convert.ToInt32(e.CommandArgument) % gvBoeDetails.PageSize;
        String BoeDetaiID = ((HiddenField)gvBoeDetails.Rows[index].FindControl("hdnBoeDetaiID")).Value;

        if (e.CommandName == "Editing")
        {
            btnAdd.Visible = false;
            trDetails.Visible = true;
            trmessage.Visible = false;
            btnUpdate.Text = "Update";
            DataTable dt = prealert.BindReceivedGoodUpdatingData(BoeDetaiID);
            if (dt.Rows.Count > 0)
            {
                trDetails.Visible = true;
                hdnBoeDetaiID1.Value = BoeDetaiID;
                if (String.IsNullOrEmpty(dt.Rows[0]["stock_id"].ToString()) == false)
                    ddlPartNumber.SelectedValue = dt.Rows[0]["stock_id"].ToString();
                if (String.IsNullOrEmpty(dt.Rows[0]["location_id"].ToString()) == false)
                    ddlLocation.SelectedValue = dt.Rows[0]["location_id"].ToString();
                if (String.IsNullOrEmpty(dt.Rows[0]["warehouse_id"].ToString()) == false)
                    ddlwarehouse.SelectedValue = dt.Rows[0]["warehouse_id"].ToString();
                if (String.IsNullOrEmpty(dt.Rows[0]["country_id"].ToString()) == false)
                    dplCountry.SelectedValue = dt.Rows[0]["country_id"].ToString();
                txtBoe.Text = dt.Rows[0]["boe_id"].ToString();
                txtPoNumber.Text = dt.Rows[0]["PO_number"].ToString();
                txtQty.Text = dt.Rows[0]["quantity"].ToString();
                             txtItemPrice.Text = dt.Rows[0]["item_price"].ToString();
                           txtTotPrice.Text = dt.Rows[0]["total_price"].ToString();
                txtVol.Text = dt.Rows[0]["item_volume"].ToString();
                txtTotVol.Text = dt.Rows[0]["total_volume"].ToString();
                txtTotGrossVol.Text = dt.Rows[0]["gross_weight"].ToString();
                txtGrossWt.Text = dt.Rows[0]["total_gross_weight"].ToString();
            }
        }
        if (e.CommandName == "Deleting")
        {
            prealert.DeleteBoeDetails(BoeDetaiID);
            lbmsg.Text = "The delivery order detail has been successfully Deleted";
            trmessage.Visible = true;
            BindField(Session[RunningCache.boeID].ToString());
        }

    }
    protected void btnAdd_Click(object sender, EventArgs e)
    {
        btnAdd.Visible = false;
        trDetails.Visible = true;
        btnUpdate.Text = "Save";

        trDetails.Visible = true;
        ddlPartNumber.SelectedValue = null;
        ddlLocation.SelectedValue = null;
        ddlwarehouse.SelectedValue = null;
        txtBoe.Text = null;
        txtPoNumber.Text = null;
        txtQty.Text = null;
              txtItemPrice.Text = null;
        txtTotPrice.Text = null;
        txtVol.Text = null;
        txtTotVol.Text = null;
        txtTotGrossVol.Text = null;
        txtGrossWt.Text = null;
      
    }
}