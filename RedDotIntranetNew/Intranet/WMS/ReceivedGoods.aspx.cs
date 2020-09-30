using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Data;


public partial class Intranet_WMS_ReceivedGoods : System.Web.UI.Page
{
    WMSclsPrealert prealert = new WMSclsPrealert();
    protected void Page_Load(object sender, EventArgs e)
    {
        if (!IsPostBack)
        {
           
            Page.Title = "Receved Goods";
            lbmsgtop.Visible = false;
            BindField(Request.QueryString["boeid"]);
            bindgrid(Request.QueryString["boeid"]);

        }

    }

    private void bindgrid(string boedID)
    {
       
        GvreceiveGood.DataSource = prealert.receivedgood(boedID);
        GvreceiveGood.DataBind();
    }
    protected void BindField(string boedID)
    {
 
        DataTable dt = prealert.receivedgood(boedID);
        if (dt.Rows.Count > 0)
        {

            tbPrealertID.Text = dt.Rows[0]["prealert_id"].ToString();
            TbStatus.Text = dt.Rows[0]["status"].ToString();

            //if (row["status"].ToString() != "DRAFT")
            //{
            //    btReceive.Enabled = true;
            //}
            //else if (row["status"].ToString() != "RECEIVED")
            //{
            //    btconfirm.Enabled = false;
            //    btReceive.Enabled = false;
            //    bttally.Enabled = false;
            //    btsave.Enabled = false;
            //}

            TbBoe.Text = dt.Rows[0]["boe_id"].ToString();
            TbCreateDate.Text = dt.Rows[0]["creation_date"].ToString();


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
            string deliveryMeth = dt.Rows[0]["delivery_method"].ToString();


            switch (deliveryMeth)
            {
                case "1":
                    rbddu.Checked = true;
                    break;
                case "2":
                    rbfob.Checked = true;
                    break;
                case "3":
                    rbexword.Checked = true;
                    break;
                default:
                    rbcnf.Checked = true;
                    break;
            }
            txtarrivalDate.Text = dt.Rows[0]["actual_arrival_date"].ToString();
            tbcomment.Text = dt.Rows[0]["remarks"].ToString();     
            tbShipRef.Text = dt.Rows[0]["shipping_reference"].ToString();
            TbToal.Text = dt.Rows[0]["total_quantity"].ToString();
            tbsupplier.Text = dt.Rows[0]["supplier_name"].ToString();
            TbSupReference.Text = dt.Rows[0]["supplier_reference"].ToString();
            tbForwarder.Text = dt.Rows[0]["freight_forwarder"].ToString();

        }

    }
    protected void btsave_Click(object sender, EventArgs e)
    {
        string arrivalDate = txtarrivalDate.Text;

        String remark = tbcomment.Text;
        String boe = TbBoe.Text;
        prealert.updateArrivalDate(boe, arrivalDate, remark);
        lbmsgtop.Visible = true;


    }
    protected void GvreceiveGood_RowCommand(object sender, GridViewCommandEventArgs e)
    {
        int index = Convert.ToInt32(e.CommandArgument) % GvreceiveGood.PageSize;
        String BoeDetaiID = ((HiddenField)GvreceiveGood.Rows[index].FindControl("hdnBoeDetailID")).Value;

        if (e.CommandName == "Editing")
        {
           // btnAdd.Visible = false;
            lbmsgtop.Visible = false;
            trDetails.Visible = true;
            trmessage.Visible = false;
            btnUpdate.Text = "Update";
            DataTable dt = prealert.BindReceivedGoodUpdatingData(BoeDetaiID);
            if (dt.Rows.Count > 0)
            {
                trDetails.Visible = true;
                hdnBoeDetailID1.Value = BoeDetaiID;
                ddlPartNumber.SelectedValue = dt.Rows[0]["stock_id"].ToString();
                ddlLocation.SelectedValue = dt.Rows[0]["location_id"].ToString();
                ddlwarehouse.SelectedValue = dt.Rows[0]["warehouse_id"].ToString();
                dplCountry.SelectedValue = dt.Rows[0]["country_id"].ToString();
                txtBoe.Text = dt.Rows[0]["boe_id"].ToString();
                txtPoNumber.Text = dt.Rows[0]["PO_number"].ToString();
                txtQty.Text = dt.Rows[0]["quantity"].ToString();

                txtDamaged.Text = dt.Rows[0]["damaged"].ToString();
                txtpackages.Text = dt.Rows[0]["packages"].ToString();
                txtheight.Text = dt.Rows[0]["item_height"].ToString();
                txtWidth.Text = dt.Rows[0]["item_width"].ToString();
                txtLength.Text = dt.Rows[0]["item_length"].ToString();

                txtItemPrice.Text = dt.Rows[0]["item_price"].ToString();
              
                txtTotPrice.Text = dt.Rows[0]["total_price"].ToString();
                txtVol.Text = dt.Rows[0]["item_volume"].ToString();
                txtTotVol.Text = dt.Rows[0]["total_volume"].ToString();
                txtTotGrossVol.Text = dt.Rows[0]["gross_weight"].ToString();
                txtGrossWt.Text = dt.Rows[0]["total_gross_weight"].ToString();
            }
        }
        //if (e.CommandName == "Deleting")
        //{
        //   // DeliveryOrder.DeleteDoDetails(DoDetaiID);
        //    lbmsg.Text = "The Received good detail has been successfully Deleted";
        //    trmessage.Visible = true;
        //    bindgrid(Request.QueryString["boeid"]);
        //}
    }
    protected void btnUpdate_Click(object sender, EventArgs e)
    {
        lbmsgtop.Visible = false;
        String BOeDetailID = hdnBoeDetailID1.Value;
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
        String Damaged = txtDamaged.Text;
        String package = txtpackages.Text;
        String height = txtheight.Text;
        String width = txtWidth.Text;
        String length = txtLength.Text;
        prealert.updatereceivedgood(warehouseid, locationid, POnumber, COO, quantity, Damaged, package, height, width, length, itemvolume, totalvolume, grossweight, totalgrossweight, itemprice, totalprice, BOeDetailID);
      
        trDetails.Visible = false;
        lbmsg.Text = "The Received good detail has been successfully Updated";
        trmessage.Visible = true;
        bindgrid(Request.QueryString["boeid"]);
    }
    protected void btnCancel_Click(object sender, EventArgs e)
    {
        lbmsgtop.Visible = false;
        trDetails.Visible = false;
        trmessage.Visible = false;
      
        ddlPartNumber.SelectedValue = null;
        ddlLocation.SelectedValue = null;
        ddlwarehouse.SelectedValue = null;
        txtBoe.Text = null;
        txtPoNumber.Text = null;
        txtQty.Text = null;
        txtDamaged.Text = null;
        txtpackages.Text = null;
        txtheight.Text = null;
        txtWidth.Text = null;
        txtLength.Text = null;
        txtItemPrice.Text = null;
        txtTotPrice.Text = null;
        txtVol.Text = null;
        txtTotVol.Text = null;
        txtTotGrossVol.Text = null;
        txtGrossWt.Text = null;


    }
    protected void bttally_Click(object sender, EventArgs e)
    {
        Response.Redirect("Crystal_Prealert_Unstuffing.aspx?PrealertId=" + tbPrealertID.Text + "&from=2&boeid=" +TbBoe.Text);
        lbmsgtop.Visible = false;
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