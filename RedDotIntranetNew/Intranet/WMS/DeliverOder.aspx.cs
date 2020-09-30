using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Data;

public partial class Intranet_WMS_DeliverOder : System.Web.UI.Page
{
    WMSClsDeleveryorders DeliveryOrder = new WMSClsDeleveryorders();
    protected void Page_Load(object sender, EventArgs e)
    {
        if (!IsPostBack)
        {
            Page.Title = "Delivery Order";

            String from = Request.QueryString["from"];
            String doid = Request.QueryString["doid"];
            bindata(doid);
            bindgrid(doid);
            if (string.IsNullOrEmpty(Request.QueryString["tempid"]) == false && from == "SS")
            {
                bindtempdata();
                trDetails.Visible = true;
            }
        }
    }

    private void bindgrid(string doid)
    {
        DataTable dt = DeliveryOrder.bindData(doid);
        GvDeliveryOrder.DataSource = dt;
        GvDeliveryOrder.DataBind();
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


            switch (tbStatus.Text)
            {


                case "RESERVED":
                    btsave.Enabled = false;
                    btconfirm.Enabled = false;
                    btnpickTicket.Enabled = true;
                    btnRelease.Enabled = false;
                    btnDeliveryNote.Enabled = true;
                    btnCustomerInvoice.Enabled = true;
                    btnInvoice.Enabled = true;
                    btnDeliveryAdvice.Enabled = true;
                    btnXfrOwnership.Enabled = false;
                    break;
                case "RELEASED":
                    btsave.Enabled = false;
                    btconfirm.Enabled = false;
                    btnpickTicket.Enabled = true;
                    btnRelease.Enabled = false;
                    btnDeliveryNote.Enabled = true;
                    btnCustomerInvoice.Enabled = true;
                    btnInvoice.Enabled = true;
                    btnDeliveryAdvice.Enabled = true;
                    btnXfrOwnership.Enabled = false;
                    break;
                case "CONFIRMED":
                    btsave.Enabled = false;
                    btconfirm.Enabled = false;
                    btnpickTicket.Enabled = true;
                    btnRelease.Enabled = true;
                    btnDeliveryNote.Enabled = true;
                    btnCustomerInvoice.Enabled = true;
                    btnInvoice.Enabled = true;
                    btnDeliveryAdvice.Enabled = true;
                    btnXfrOwnership.Enabled = true;
                    break;
                default:
                    btsave.Enabled = false;
                    btconfirm.Enabled = true;
                    btnpickTicket.Enabled = true;
                    btnRelease.Enabled = false;
                    btnDeliveryNote.Enabled = true;
                    btnCustomerInvoice.Enabled = true;
                    btnInvoice.Enabled = true;
                    btnDeliveryAdvice.Enabled = true;
                    btnXfrOwnership.Enabled = false;

                    break;
            }



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
            hdnQty.Value = dt.Rows[0]["quantity"].ToString();
            hdnPrice.Value = dt.Rows[0]["item_price"].ToString();
            hdnStockID.Value = dt.Rows[0]["stock_id"].ToString();
            hdnBoeid.Value = dt.Rows[0]["boe_id"].ToString();
            TbShipReference.Text = dt.Rows[0]["shipping_reference"].ToString();
            TContainer.Text = dt.Rows[0]["container"].ToString();
            tbCreationDate.Text = dt.Rows[0]["creation_date"].ToString();
            TbEffectiveDate.Text = dt.Rows[0]["effective_date"].ToString();
            Tbinvoicenumber.Text = dt.Rows[0]["invoice_number"].ToString();
            tbReleaseNo.Text = dt.Rows[0]["release_no"].ToString();
            tbCustomer.Text = dt.Rows[0]["customer_name"].ToString();
            TbReleaseTo.Text = dt.Rows[0]["customer_name"].ToString();
            TbFreightfowarder.Text = dt.Rows[0]["freight_forwarder_name"].ToString();
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
    protected void btnpickTicket_Click(object sender, EventArgs e)
    {
        Response.Redirect("Crystal_pickTicket.aspx?doid=" + tbDO.Text);
    }
    protected void btnDeliveryNote_Click(object sender, EventArgs e)
    {

        Response.Redirect("Crystal_DeliveryNote.aspx?doid=" + tbDO.Text);
    }
    protected void btnCustomerInvoice_Click(object sender, EventArgs e)
    {
        Response.Redirect("Crystal_CustomerInvoice.aspx?doid=" + tbDO.Text);
    }
    protected void btnInvoice_Click(object sender, EventArgs e)
    {
        Response.Redirect("Crystal_Invoice.aspx?doid=" + tbDO.Text);
    }
    protected void btnDeliveryAdvice_Click(object sender, EventArgs e)
    {
        Response.Redirect("AdviseReport.aspx?doid=" + tbDO.Text);
       
    }
    protected void btnBack_Click(object sender, EventArgs e)
    {
        Response.Redirect("ListDo.aspx");
    }
    protected void btconfirm_Click(object sender, EventArgs e)
    {
        string stockid = hdnStockID.Value  ;
        string boeid = hdnBoeid.Value;
        string warehoueid = hdnWarehouseID.Value ;
        string locationid =hdnLocationID.Value ;
        string  qty =hdnQty.Value ;
        string  Price= hdnPrice.Value ;
        string releaseno = tbReleaseNo.Text;
        string dodid = tbDO.Text;
        string username = RunningCache.UserID;

        DeliveryOrder.Confirmation(stockid, boeid, warehoueid, locationid, qty, Price, releaseno, dodid, username);
    }
   

    protected void GvDeliveryOrder_RowCommand(object sender, GridViewCommandEventArgs e)
    {
         int index = Convert.ToInt32(e.CommandArgument) % GvDeliveryOrder.PageSize;
         String DoDetaiID = ((HiddenField)GvDeliveryOrder.Rows[index].FindControl("hdnDoDetaiID")).Value;
         
        if (e.CommandName == "Editing")
        {
            //btnAdd.Visible = false;
            trDetails.Visible = true;
            trmessage.Visible = false;
            btnUpdate.Text = "Update";
           DataTable dt= DeliveryOrder.BindUpdatingData(DoDetaiID);
           if (dt.Rows.Count > 0)
           {
               trDetails.Visible = true;
               hdndoDetailsID.Value = DoDetaiID;
               if(String.IsNullOrEmpty (dt.Rows[0]["stock_id"].ToString())==false)
              txtpartnumber.Text = dt.Rows[0]["stock_id"].ToString();
               if (String.IsNullOrEmpty(dt.Rows[0]["location_id"].ToString()) == false)
               txtlocation.Text = dt.Rows[0]["location_id"].ToString();
               if (String.IsNullOrEmpty(dt.Rows[0]["warehouse_id"].ToString()) == false)
               txtwarehouse.Text = dt.Rows[0]["warehouse_id"].ToString();
               if (String.IsNullOrEmpty(dt.Rows[0]["country_id"].ToString()) == false)
               txtcountry.Text  = dt.Rows[0]["country_id"].ToString();
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
            bindgrid(Request.QueryString["doid"]);
        }
    }
    protected void btnUpdate_Click(object sender, EventArgs e)
    {
       // btnAdd.Visible = true;
        if (btnUpdate.Text == "Save")
        {

            //Add new ....

            String stockid= hdnStockID.Value;
            String quantity=txtQty.Text;
            String itemprice= txtItemPrice.Text ;
            String itemvolume= txtVol.Text;
            String grossweight=  txtGrossWt.Text ;
            String totalgrossweight = txtTotGrossVol.Text;
            String totalprice= txtTotPrice.Text;
            String totalvolume= txtTotVol.Text ;
            String POnumber= txtPoNumber.Text;
            String warehouseid = hdnWarehouseID.Value;
            String locationid = hdnLocationID.Value;
            String boeid= txtBoe.Text;
            String COO = hdnCountry.Value;
            String invoiceqty=    txtInvQty.Text;
            String invoiceprice= txtInvPrice.Text;
            String tempid = Request.QueryString["tempid"];
            DeliveryOrder.NewdoDetails(tempid,tbDO.Text, stockid, quantity, itemprice, itemvolume, grossweight, totalprice, totalgrossweight, totalvolume, POnumber, warehouseid, locationid, boeid, COO, invoiceqty, invoiceprice);
            bindgrid(Request.QueryString["doid"]);
            trDetails.Visible = false;
            lbmsg.Text = "The delivery order detail has been successfully added";
            trmessage.Visible = true;

        }
        else
        {
            //update .....
            String doDetailID = hdndoDetailsID.Value;
            String stockid = hdnStockID.Value;
              String quantity = "0";
            String itemprice = "0";
            String itemvolume ="0";
            String grossweight = "0";
            String totalgrossweight = "0";
            String totalprice = "0";
            String totalvolume = "0";
             String invoiceqty = "0";
            String invoiceprice ="0";

            if (String.IsNullOrEmpty (txtQty.Text) ==false)
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
            bindgrid(Request.QueryString["doid"]);
            trDetails.Visible = false;
            trmessage.Visible = true;
        }
    }
    protected void btnAdd_Click(object sender, EventArgs e)
    {
     //   btnAdd.Visible = false;
        trDetails.Visible = true;
        btnUpdate.Text = "Save";

        trDetails.Visible = true;
    
        txtBoe.Text = null;
        txtPoNumber.Text = null;
        txtQty.Text = null;
        txtInvQty.Text = null;
        txtItemPrice.Text = null;
        txtTotPrice.Text = null;
        txtVol.Text = null;
        txtTotVol.Text = null;
        txtTotGrossVol.Text = null;
        txtGrossWt.Text = null;
        txtInvPrice.Text = null;
    }
    protected void btnCancel_Click(object sender, EventArgs e)
    {
      //  btnAdd.Visible = true;
        trDetails.Visible = false;
        trmessage.Visible = false;
        //trDetails.Visible = true;
       
        txtBoe.Text = null;
        txtPoNumber.Text = null;
        txtQty.Text = null;
        txtInvQty.Text = null;
        txtItemPrice.Text = null;
        txtTotPrice.Text = null;
        txtVol.Text = null;
        txtTotVol.Text = null;
        txtTotGrossVol.Text = null;
        txtGrossWt.Text = null;
        txtInvPrice.Text = null;
    }
  
    protected void ddpPartnumber_SelectedIndexChanged(object sender, EventArgs e)
    {
        String Stokid = ddpPartnumber.SelectedValue;
        string doid = Request.QueryString["doid"];
        Response.Redirect("SelectStock.aspx?stockID=" + Stokid + "&from=1&doid=" + doid);
    }
}