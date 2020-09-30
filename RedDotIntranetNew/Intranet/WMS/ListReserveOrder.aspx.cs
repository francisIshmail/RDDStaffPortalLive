using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Data;
public partial class Intranet_WMS_ListReserveOrder : System.Web.UI.Page
{
    WMSclsReserveStock reserverStock = new WMSclsReserveStock();
    String search = "";
    protected void Page_Load(object sender, EventArgs e)
    {
        if (!IsPostBack)
            BindGrid(search);
    }    

   

    public void BindGrid(string search)
    {
        Gvreservestockorder.DataSource = reserverStock.Gvreservestockorder(search);
        Gvreservestockorder.DataBind();
    }
    protected void Gvreservestockorder_PageIndexChanging(object sender, GridViewPageEventArgs e)
    {
        BindGrid(search);
        Gvreservestockorder.PageIndex = e.NewPageIndex;
        Gvreservestockorder.DataBind();
    }
    protected void Gvreservestockorder_RowCommand(object sender, GridViewCommandEventArgs e)
    {
        int index = Convert.ToInt32(e.CommandArgument) % Gvreservestockorder.PageSize;

        if (e.CommandName == "do_id")
        {
            Session[RunningCache.reserveCustomer] = "";
            Session[RunningCache.reserveNote] = "";
            Session[RunningCache.reserveorder] = "";
            String doID = ((Button)Gvreservestockorder.Rows[index].FindControl("btndoId")).Text;

            Response.Redirect("ReserveOrder.aspx?doID=" + doID);


        }
    }
    protected void btreport_Click(object sender, EventArgs e)
    {
        Response.Redirect("Crystal_ReserveOrder.aspx");
    }
    protected void btnew_Click(object sender, EventArgs e)
    {
        Session[RunningCache.reserveCustomer] ="";
        Session[RunningCache.reserveNote] = "";
        Session[RunningCache.reserveorder] ="";
        reserverStock.DeleteTempReserve(Session[RunningCache.UserID].ToString());
        Response.Redirect("NewReseverOder.aspx");
    }
    protected void txtSearch_TextChanged(object sender, EventArgs e)
    {
        if (rdbID.Checked == true)
            search = " deliveryOrder.do_id  like '%" + txtSearch.Text + "%'";
        if (rdbStatus.Checked == true)
            search = " deliveryOrder.status like '%" + txtSearch.Text + "%'";
        if (rdbCustomer.Checked == true)
            search = " customer.customer_name like '%" + txtSearch.Text + "%'";

        BindGrid(search);
    }
    protected void btmSearch_Click(object sender, EventArgs e)
    {
        if (rdbID.Checked == true)
            search = " deliveryOrder.do_id  like '%" + txtSearch.Text + "%'";
        if (rdbStatus.Checked == true)
            search = " deliveryOrder.status like '%" + txtSearch.Text + "%'";
        if (rdbCustomer.Checked == true)
            search = " customer.customer_name like '%" + txtSearch.Text + "%'";

        BindGrid(search);
    }
    protected void lbFirst_Click(object sender, EventArgs e)
    {
        if (rdbID.Checked == true)
            search = " deliveryOrder.do_id  like '%" + txtSearch.Text + "%'";
        if (rdbStatus.Checked == true)
            search = " deliveryOrder.status like '%" + txtSearch.Text + "%'";
        if (rdbCustomer.Checked == true)
            search = " customer.customer_name like '%" + txtSearch.Text + "%'";
       
        BindGrid(search);
        Gvreservestockorder.PageIndex = 0;
        Gvreservestockorder.DataBind();

    }
    protected void lbNext_Click(object sender, EventArgs e)
    {
        if (rdbID.Checked == true)
            search = " deliveryOrder.do_id  like '%" + txtSearch.Text + "%'";
        if (rdbStatus.Checked == true)
            search = " deliveryOrder.status like '%" + txtSearch.Text + "%'";
        if (rdbCustomer.Checked == true)
            search = " customer.customer_name like '%" + txtSearch.Text + "%'";

        BindGrid(search);
        int i = Gvreservestockorder.PageIndex + 1;
        if (i <= Gvreservestockorder.PageCount)
            Gvreservestockorder.PageIndex = i;
        Gvreservestockorder.DataBind();
    }
    protected void lbPrev_Click(object sender, EventArgs e)
    {
        if (rdbID.Checked == true)
            search = " deliveryOrder.do_id  like '%" + txtSearch.Text + "%'";
        if (rdbStatus.Checked == true)
            search = " deliveryOrder.status like '%" + txtSearch.Text + "%'";
        if (rdbCustomer.Checked == true)
            search = " customer.customer_name like '%" + txtSearch.Text + "%'";

        BindGrid(search);
        int i = Gvreservestockorder.PageCount;
        if (Gvreservestockorder.PageIndex > 0)
            Gvreservestockorder.PageIndex = Gvreservestockorder.PageIndex - 1;
        Gvreservestockorder.DataBind();

    }
    protected void lbLAst_Click(object sender, EventArgs e)
    {
        if (rdbID.Checked == true)
            search = " deliveryOrder.do_id  like '%" + txtSearch.Text + "%'";
        if (rdbStatus.Checked == true)
            search = " deliveryOrder.status like '%" + txtSearch.Text + "%'";
        if (rdbCustomer.Checked == true)
            search = " customer.customer_name like '%" + txtSearch.Text + "%'";
       
        BindGrid(search);
        Gvreservestockorder.PageIndex = Gvreservestockorder.PageCount;
        Gvreservestockorder.DataBind();
    }
}