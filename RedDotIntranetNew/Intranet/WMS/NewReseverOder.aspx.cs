using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Data;
public partial class Intranet_WMS_NewReseverOder : System.Web.UI.Page
{
    WMSclsReserveStock Reserve = new WMSclsReserveStock();
    protected void Page_Load(object sender, EventArgs e)
    {
        if (!IsPostBack)
        {
            DataTable dt = Reserve.newreserve();
            txtDoid.Text = (int.Parse(dt.Rows[0]["doi_id"].ToString()) + 1).ToString();
            txtCreationDate.Text = dt.Rows[0]["creattiondate"].ToString();
            txtExperiyDate.Text = dt.Rows[0]["expriyDate"].ToString();
            ddpPartnumber.SelectedIndex = -1;
            txtStatus.Text = "RESERVED";
            bindgrid();

            if (string.IsNullOrEmpty(Session[RunningCache.reserveCustomer].ToString()) == false)
            ddpCustname.SelectedValue = Session[RunningCache.reserveCustomer].ToString();

            if (string.IsNullOrEmpty(Session[RunningCache.reserveNote].ToString()) == false)
            txtnote.Text = Session[RunningCache.reserveNote].ToString();

            if (string.IsNullOrEmpty(Session[RunningCache.reserveorder].ToString()) == false)
            txtreserveOrder.Text = Session[RunningCache.reserveorder].ToString();

        }
    }

 


    private void bindgrid()
    {
        DataTable dt = Reserve.Bindgrid(Session [RunningCache.UserID].ToString ());
        gvReserve.DataSource = dt;
        gvReserve.DataBind();
    }

    protected void Button1_Click(object sender, EventArgs e)
    {
        Response.Redirect("ListReserveOrder.aspx");
    }
    protected void gvReserve_PageIndexChanging(object sender, GridViewPageEventArgs e)
    {
        bindgrid();
        gvReserve.PageIndex = e.NewPageIndex;
        gvReserve.DataBind();
    }
    protected void gvReserve_RowCommand(object sender, GridViewCommandEventArgs e)
    {

    }
    protected void ddpPartnumber_SelectedIndexChanged(object sender, EventArgs e)
    {
       Session [RunningCache.reserveCustomer ] = ddpCustname.SelectedValue;
       Session[RunningCache.reserveorder] = txtreserveOrder.Text;
       Session[RunningCache.reserveNote] = txtnote.Text;
        Response.Redirect("ReserveStock.aspx?Stokid=" + ddpPartnumber.SelectedValue);
    }
    protected void btnSave_Click(object sender, EventArgs e)
    {
        //String user = Session["sessUserDB"].ToString ();

        if (ddpCustname.SelectedValue != null && txtreserveOrder.Text != "")
        {
            Session[RunningCache.reserveCustomer] = null;
            Session[RunningCache.reserveNote] = null;
            Session[RunningCache.reserveorder] = null;
            String CreationDate = txtCreationDate.Text;
            String ExperiyDate = txtExperiyDate.Text;
            String customer = ddpCustname.SelectedValue;
            String reserveorder = txtreserveOrder.Text;
            String note = txtnote.Text;
            Reserve.SaveReserve(Session[RunningCache.UserID].ToString(), int.Parse(customer), CreationDate, ExperiyDate, note, reserveorder);
        }

    }
}