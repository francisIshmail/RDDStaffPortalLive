using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Data;

public partial class Intranet_WMS_ReserveOrder : System.Web.UI.Page
{
    WMSclsReserveStock reserve = new WMSclsReserveStock();
    protected void Page_Load(object sender, EventArgs e)
    {
        try
        {

       
        if (!IsPostBack)
        {
            Page.Title = "Reserve Order";
            int doid = int.Parse(Request.QueryString["doID"].ToString());
            DataTable dt = reserve.reservesorder(doid);
            DataTable dt1 = reserve.reservesordergrid(doid, Session[RunningCache.UserID].ToString());
            if (dt.Rows.Count > 0)
            {
                txtDoid.Text = dt.Rows[0]["do_id"].ToString();
                txtExperiyDate.Text = dt.Rows[0]["effective_date"].ToString();
                txtStatus.Text = dt.Rows[0]["status"].ToString();
                txtreserveOrder.Text = dt.Rows[0]["release_no"].ToString();
                txtCreationDate.Text = dt.Rows[0]["creation_date"].ToString();
                ddpCustname.SelectedValue = dt.Rows[0]["customer_id"].ToString();
                txtnote.Text = dt.Rows[0]["notes"].ToString();
                gvReserveOrder.DataSource = dt1;
                gvReserveOrder.DataBind();

                if (dt.Rows[0]["status"].ToString() == "EXPIRED")
                {
                    txtDoid.ReadOnly = true;
                    txtExperiyDate.ReadOnly = true;
                    txtStatus.ReadOnly = true;
                    txtreserveOrder.ReadOnly = true;
                    txtCreationDate.ReadOnly = true;
                    ddpCustname.Enabled  = false ;
                    txtnote.ReadOnly = true;
                    gvReserveOrder.Columns[5].Visible = false;
                }
            }

            int sum = 0;
            if (dt1.Rows.Count > 0)
            {
                foreach (DataRow row in dt1.Rows)
                {
                    if (string.IsNullOrEmpty(row["quantity"].ToString()) == false)
                        sum = sum + int.Parse(row["quantity"].ToString());
                }
            }
            lbTotal.Text = "TOTAL=" + sum.ToString();
        }
        }
        catch (Exception ex)
        {
            Response.Redirect("~/login.aspx");
        }
    }
    protected void gvReserveOrder_RowCommand(object sender, GridViewCommandEventArgs e)
    {
        int index = Convert.ToInt32(e.CommandArgument) % gvReserveOrder.PageSize;
        var indexrow = e.CommandArgument;

        if (String.IsNullOrEmpty(indexrow.ToString()) == false)
        {

            if (e.CommandName == "Deleting")
            {
                String stockid = ((HiddenField)gvReserveOrder.Rows[index].FindControl("hdnDodetailID")).Value;
                String flag = ((HiddenField)gvReserveOrder.Rows[index].FindControl("hdnflag")).Value;
                reserve.DeleteRevserve(stockid, int.Parse(flag));
                lbmsg.Visible = true;
                lbmsg.Text = "The Reserve order has been  successfully deleted.";
                int doid = int.Parse(Request.QueryString["doID"].ToString());
                DataTable dt1 = reserve.reservesordergrid(doid, Session[RunningCache.UserID].ToString());
                gvReserveOrder.DataSource = dt1;
                gvReserveOrder.DataBind();
                int sum = 0;
                if (dt1.Rows.Count > 0)
                {
                    foreach (DataRow row in dt1.Rows)
                    {
                        if (string.IsNullOrEmpty(row["quantity"].ToString()) == false)
                            sum = sum + int.Parse(row["quantity"].ToString());
                    }
                }
                lbTotal.Text = "TOTAL=" + sum.ToString();
            }

        }
    }
    protected void gvReserveOrder_PageIndexChanging(object sender, GridViewPageEventArgs e)
    {
        bindgrid();
        gvReserveOrder.PageIndex = e.NewPageIndex;
        gvReserveOrder.DataBind();
    }
    protected void Button1_Click(object sender, EventArgs e)
    {
        Response.Redirect("ListReserveOrder.aspx");
    }

    private void bindgrid()
    {
        int doid = int.Parse(txtDoid.Text);
        WMSclsReserveStock reserve = new WMSclsReserveStock();
        DataTable dt = reserve.reservesorder(doid);
        gvReserveOrder.DataSource = dt;
        gvReserveOrder.DataBind();
    }
    protected void gvReserveOrder_SelectedIndexChanged(object sender, EventArgs e)
    {

    }
    protected void btadd_Click(object sender, EventArgs e)
    {

    }
    protected void Dplpart_SelectedIndexChanged(object sender, EventArgs e)
    {
        string stockid = ((DropDownList)gvReserveOrder.FooterRow.FindControl("Dplpart")).SelectedValue;
        Response.Redirect("ReserveStock.aspx?Stokid=" + stockid + "&from=1&doID=" + Request.QueryString["doID"]);
    }
    protected void ddpPartnumber_SelectedIndexChanged(object sender, EventArgs e)
    {
        Session[RunningCache.reserveCustomer] = ddpCustname.SelectedValue;
        Session[RunningCache.reserveorder] = txtreserveOrder.Text;
        Session[RunningCache.reserveNote] = txtnote.Text;



        string stockid = ddpPartnumber.SelectedValue;
        Response.Redirect("ReserveStock.aspx?Stokid=" + stockid + "&from=1&doID=" + Request.QueryString["doID"]);
    }
    protected void btnsave_Click(object sender, EventArgs e)
    {
        reserve.updateOldReserve(Session[RunningCache.UserID].ToString(), Request.QueryString["doID"]);
        lbmsg.Visible = true;
        lbmsg.Text = "The Reserve order has been updated successfully";

    }
}