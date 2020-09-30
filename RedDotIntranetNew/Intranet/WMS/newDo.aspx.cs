using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Data;
public partial class Intranet_WMS_newDo : System.Web.UI.Page
{
    WMSClsDeleveryorders DeliveryOrder = new WMSClsDeleveryorders();
    protected void Page_Load(object sender, EventArgs e)
    {
        if (!IsPostBack)
        {
            String from = Request.QueryString["from"];
          
            tbCreationDate.Text = DateTime.Now.ToShortDateString ();
            tbStatus.Text = "DRAFT";
            tbDO.Text = DeliveryOrder.lastDo();

           if (string.IsNullOrEmpty(Request.QueryString["tempid"]) == false && from == "SS")
           {

               String doid = Request.QueryString["doID"];
               bindata(doid);
               bindtempdata();
               trdetailds.Visible = true;
           }
        }
    }


    private void bindata(string doid)
    {

        DataTable dt = DeliveryOrder.bindData(doid);

        if (dt.Rows.Count > 0)
        {

            tbDO.Text = dt.Rows[0]["do_id"].ToString();

            if (dt.Rows[0]["driver"].ToString() == "null")
                TbDriver.Text = "";
            else
                TbDriver.Text = dt.Rows[0]["driver"].ToString();

            TbVehicle.Text = dt.Rows[0]["vehicle"].ToString();
            tbStatus.Text = dt.Rows[0]["status"].ToString();
  string shipMethod = dt.Rows[0]["shipping_method"].ToString();

            switch (shipMethod)
            {
                case "1":
                    rbsea.Checked = true;
                    break;
                case "2":
                    rbair.Checked = true;
                    break;
                default:
                    rbland.Checked = true;
                    break;
            }

            hdnWarehouseID.Value = dt.Rows[0]["warehouse_id"].ToString();
            hdnLocationID.Value = dt.Rows[0]["location_id"].ToString();
            hdnStockID.Value = dt.Rows[0]["stock_id"].ToString();
           TbShipReference.Text = dt.Rows[0]["shipping_reference"].ToString();
            TbContainer.Text = dt.Rows[0]["container"].ToString();
            tbCreationDate.Text = dt.Rows[0]["creation_date"].ToString();
            TbEffectiveDate.Text = dt.Rows[0]["effective_date"].ToString();
            Tbinvoicenumber.Text = dt.Rows[0]["invoice_number"].ToString();
            tbReleaseNo.Text = dt.Rows[0]["release_no"].ToString();
            ddlcustomer.SelectedValue = dt.Rows[0]["customer_id"].ToString();
            ddlRelease.SelectedValue = dt.Rows[0]["release_to"].ToString();
            ddlFreightForwarder.SelectedValue = dt.Rows[0]["freight_forwarder_name"].ToString();
            tbcomment.Text = dt.Rows[0]["notes"].ToString();


        }

    }

    private void bindtempdata()
    {
        DataTable dt = DeliveryOrder.bindTempAllocateData(Request.QueryString["tempid"]);
        if (dt.Rows.Count > 0)
        {
            txtcountry.Text = dt.Rows[0]["country_name"].ToString();
            txtwarehouse.Text = dt.Rows[0]["description"].ToString();
            txtlocation.Text = dt.Rows[0]["location_description"].ToString();
            txtpartnumber.Text = dt.Rows[0]["part_number"].ToString();
            txtItemPrice.Text = dt.Rows[0]["price"].ToString();
            txtQty.Text = dt.Rows[0]["quantity"].ToString();
            hdnWarehouseID.Value = dt.Rows[0]["warehouse_id"].ToString();
            hdnLocationID.Value = dt.Rows[0]["location_id"].ToString();
            hdnStockID.Value = dt.Rows[0]["stock_id"].ToString();
            hdnCountry.Value = dt.Rows[0]["country_id"].ToString();
            // txtPoNumber.Text = dt.Rows[0]["PO_number"].ToString();
            txtBoe.Text = dt.Rows[0]["boe_id"].ToString();

        }
    }
 
    protected void btnSaveDetails_Click(object sender, EventArgs e)
    {
        String doid = tbDO.Text;
        String stockid = hdnStockID .Value;
        String quantity =txtQty.Text ;
        String itemprice =txtItemPrice.Text ;
        String itemvolume =txtVol.Text;
        String grossweight = txtGrossWt.Text;
        String totalprice= txtTotPrice.Text;
        String totalgrossweight = txtTotGrossVol.Text;
        String totalvolume =txtTotVol.Text;
        String POnumber =txtPoNumber.Text;
        String warehouseid = hdnWarehouseID.Value;
        String locationid = hdnLocationID.Value;
        String boeid = txtBoe.Text;
        String COO = hdnCountry.Value;
        String invoiceqty = txtInvQty.Text;
        String invoiceprice= "0";
        String tempid = Request.QueryString["tempid"] ;
        // seting tempid this way so that 
        DeliveryOrder.NewdoDetails(tempid,doid, stockid, quantity, itemprice, itemvolume, grossweight, totalprice, totalgrossweight, totalvolume, POnumber, warehouseid, locationid, boeid, COO, invoiceqty, invoiceprice);
        GvPrealertDetail .DataSource = DeliveryOrder.bindDeleveryNoteReport(int.Parse (doid));
        GvPrealertDetail.DataBind ();
    }
    protected void btsave_Click(object sender, EventArgs e)
    {
        trPartSelect.Visible = true;
        String status = tbStatus.Text;
        String customer_id = ddlcustomer.SelectedValue ;
       
        String shipping_method ="";
        if (rbsea.Checked ==true)
            shipping_method="1";
        else  if (rbair.Checked ==true)
             shipping_method="2";
        else if (rbland.Checked ==true)
            shipping_method = "3";

        String invoice_number  =Tbinvoicenumber.Text;
        String EffectiveDate= TbEffectiveDate.Text;
        String last_updated_by = Session[RunningCache.UserID].ToString();
        String driver =TbDriver.Text ;
        String vehicle = TbVehicle.Text;
        String container = TbContainer.Text;
        String notes = txtnotes.Text;
        String release_no =tbReleaseNo.Text ;
        String release_order =txtReleaseOrder.Text;
        String invoice ="";
         String freightForwarder =ddlFreightForwarder.SelectedValue;
        String shippingReference= TbShipReference.Text;
        String releaseTo = ddlRelease.SelectedValue;
        DeliveryOrder .createNewDo(status, customer_id, EffectiveDate, shipping_method, invoice_number, last_updated_by, driver, vehicle, container, notes, release_no, release_order, invoice, freightForwarder,shippingReference ,releaseTo);
        tbDO.Text = DeliveryOrder.lastDo();
       
    }

    protected void ddpPartnumber_SelectedIndexChanged(object sender, EventArgs e)
    {
        String Stokid = ddpPartnumber.SelectedValue;
        string doid = tbDO.Text;
        Response.Redirect("SelectStock.aspx?stockID=" + Stokid + "&from=1&doid=" + doid);
    }
    protected void btnUpdate_Click(object sender, EventArgs e)
    {
        if (btnUpdate.Text == "Save")
        {
        String doid = tbDO.Text;
        String stockid = hdnStockID.Value;
        String quantity = txtQty.Text;
        String itemprice = txtItemPrice.Text;
        String itemvolume = txtVol.Text;
        String grossweight = txtGrossWt.Text;
        String totalprice = txtTotPrice.Text;
        String totalgrossweight = txtTotGrossVol.Text;
        String totalvolume = txtTotVol.Text;
        String POnumber = txtPoNumber.Text;
        String warehouseid = hdnWarehouseID.Value;
        String locationid = hdnLocationID.Value;
        String boeid = txtBoe.Text;
        String COO = hdnCountry.Value;
        String invoiceqty = txtInvQty.Text;
        String invoiceprice = "0";
        String tempid = Request.QueryString["tempid"];
        // seting tempid this way so that 
        DeliveryOrder.NewdoDetails(tempid, doid, stockid, quantity, itemprice, itemvolume, grossweight, totalprice, totalgrossweight, totalvolume, POnumber, warehouseid, locationid, boeid, COO, invoiceqty, invoiceprice);
        GvPrealertDetail.DataSource = DeliveryOrder.bindDeleveryNoteReport(int.Parse(doid));
        GvPrealertDetail.DataBind();
        }
        else
            if (btnUpdate.Text == "Update")
            {
                //update .....
                String doid = tbDO.Text;
                String doDetailID = hdndoDetailsID.Value;
                String stockid = hdnStockID.Value;
                String quantity = "0";
                String itemprice = "0";
                String itemvolume = "0";
                String grossweight = "0";
                String totalgrossweight = "0";
                String totalprice = "0";
                String totalvolume = "0";
                String invoiceqty = "0";
                String invoiceprice = "0";

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
                if (String.IsNullOrEmpty(txtInvQty.Text) == false)
                    invoiceqty = txtInvQty.Text;
                if (String.IsNullOrEmpty(txtInvPrice.Text) == false)
                    invoiceprice = txtInvPrice.Text;

                String POnumber = txtPoNumber.Text;
                String warehouseid = hdnWarehouseID.Value;
                String locationid = hdnLocationID.Value;
                String boeid = txtBoe.Text;
                String COO = hdnLocationID.Value;


                DeliveryOrder.updateDoDetails(doDetailID, stockid, quantity, itemprice, itemvolume, grossweight, totalprice, totalgrossweight, totalvolume, POnumber, warehouseid, locationid, boeid, COO, invoiceqty, invoiceprice);
                GvPrealertDetail.DataSource = DeliveryOrder.bindDeleveryNoteReport(int.Parse(doid));
                GvPrealertDetail.DataBind();
                trdetailds.Visible = false;
                trmessage.Visible = true;
            }

    }
    protected void GvPrealertDetail_RowCommand(object sender, GridViewCommandEventArgs e)
    {

        int index = Convert.ToInt32(e.CommandArgument) % GvPrealertDetail.PageSize;
        String DoDetaiID = ((HiddenField)GvPrealertDetail.Rows[index].FindControl("hdnDoDetaiID")).Value;

        if (e.CommandName == "Editing")
        {
            //btnAdd.Visible = false;
            trdetailds.Visible = true;
            trmessage.Visible = false;
            btnUpdate.Text = "Update";
            DataTable dt = DeliveryOrder.BindUpdatingData(DoDetaiID);
            if (dt.Rows.Count > 0)
            {
                trdetailds.Visible = true;
                hdndoDetailsID.Value = DoDetaiID;
                if (String.IsNullOrEmpty(dt.Rows[0]["stock_id"].ToString()) == false)
                    txtpartnumber.Text = dt.Rows[0]["stock_id"].ToString();
                if (String.IsNullOrEmpty(dt.Rows[0]["location_id"].ToString()) == false)
                    txtlocation.Text = dt.Rows[0]["location_id"].ToString();
                if (String.IsNullOrEmpty(dt.Rows[0]["warehouse_id"].ToString()) == false)
                    txtwarehouse.Text = dt.Rows[0]["warehouse_id"].ToString();
                if (String.IsNullOrEmpty(dt.Rows[0]["country_id"].ToString()) == false)
                    txtcountry.Text = dt.Rows[0]["country_id"].ToString();
                txtBoe.Text = dt.Rows[0]["boe_id"].ToString();
                txtPoNumber.Text = dt.Rows[0]["PO_number"].ToString();
                txtQty.Text = dt.Rows[0]["quantity"].ToString();
                txtInvQty.Text = dt.Rows[0]["invoice_qty"].ToString();
                txtItemPrice.Text = dt.Rows[0]["item_price"].ToString();
                txtInvPrice.Text = dt.Rows[0]["invoice_price"].ToString();
                txtTotPrice.Text = dt.Rows[0]["total_price"].ToString();
                txtVol.Text = dt.Rows[0]["item_volume"].ToString();
                txtTotVol.Text = dt.Rows[0]["total_volume"].ToString();
                txtTotGrossVol.Text = dt.Rows[0]["gross_weight"].ToString();
                txtGrossWt.Text = dt.Rows[0]["total_gross_weight"].ToString();
            }
        }
        if (e.CommandName == "Deleting")
        {
            DeliveryOrder.DeleteDoDetails(DoDetaiID);
            lbmsg.Text = "The delivery order detail has been successfully Deleted";
            trmessage.Visible = true;
            GvPrealertDetail.DataSource = DeliveryOrder.bindDeleveryNoteReport(int.Parse(tbDO.Text));
            GvPrealertDetail.DataBind();
        }
    }
    protected void btnCancel_Click(object sender, EventArgs e)
    {
        trdetailds.Visible = false;
        trmessage.Visible = false;
    }
}