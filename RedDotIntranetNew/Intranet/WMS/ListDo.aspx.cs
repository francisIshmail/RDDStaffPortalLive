using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Data;

public partial class Intranet_WMS_ListDo : System.Web.UI.Page
{
    WMSClsDeleveryorders DeliveryOrder = new WMSClsDeleveryorders();
    String search = "";
    protected void Page_Load(object sender, EventArgs e)
    {
        if (!IsPostBack)
            Page.Title = "Delivery Order";
            bindGrid(search);
    }

   public void  bindGrid(String search)
    {

        DataTable dt = DeliveryOrder.GVDolist(search);
        Gvdolist.DataSource = dt;
        Gvdolist.DataBind();
    }
    protected void Gvdolist_PageIndexChanging(object sender, GridViewPageEventArgs e)
    {
        bindGrid(search);
        Gvdolist.PageIndex = e.NewPageIndex;
        Gvdolist.DataBind();
    }
    protected void Gvdolist_RowCommand(object sender, GridViewCommandEventArgs e)
    {
        int index = Convert.ToInt32(e.CommandArgument) % Gvdolist.PageSize;

        if (e.CommandName == "view")
            //tempid should be nothing we only get the empty after selecting the partnummber
            //we set it here so the program will NOT fall out 
        Response.Redirect("DeliverOder.aspx?from=DO&tempid=&doid=" + ((Button)Gvdolist.Rows[index].FindControl("btndo_id")).Text);

        
    }
    protected void Btnewdo_Click(object sender, EventArgs e)
    {
        Response.Redirect("newDo.aspx?from=DO&tempid=");
    }
    protected void txtSearch_TextChanged(object sender, EventArgs e)
    {

        if (rdbDO.Checked == true)
            search = " deliveryOrder.do_id  like '%" + txtSearch.Text + "%'";
        if (rdbStatus.Checked == true)
            search = " status like '%" + txtSearch.Text + "%'";
        if (rdbRelease.Checked == true)
            search = " release_no like '%" + txtSearch.Text + "%'";
        if (rdbinvoice.Checked == true)
            search = " invoice_number like '%" + txtSearch.Text + "%'";
        bindGrid(search);

    }
    protected void btmSearch_Click(object sender, EventArgs e)
    {

        if (rdbDO.Checked == true)
            search = " deliveryOrder.do_id  like '%" + txtSearch.Text + "%'";
        if (rdbStatus.Checked == true)
            search = " status like '%" + txtSearch.Text + "%'";
        if (rdbRelease.Checked == true)
            search = " release_no like '%" + txtSearch.Text + "%'";
        if (rdbinvoice.Checked == true)
            search = " invoice_number like '%" + txtSearch.Text + "%'";
        bindGrid(search);

    }
    protected void lbFirst_Click(object sender, EventArgs e)
    {
        if (rdbDO.Checked == true)
            search = " deliveryOrder.do_id  like '%" + txtSearch.Text + "%'";
        if (rdbStatus.Checked == true)
            search = " status like '%" + txtSearch.Text + "%'";
        if (rdbRelease.Checked == true)
            search = " release_no like '%" + txtSearch.Text + "%'";
        if (rdbinvoice.Checked == true)
            search = " invoice_number like '%" + txtSearch.Text + "%'";
        bindGrid(search);
        Gvdolist.PageIndex = 0;
        Gvdolist.DataBind();

    }
    protected void lbNext_Click(object sender, EventArgs e)
    {
        if (rdbDO.Checked == true)
            search = " deliveryOrder.do_id  like '%" + txtSearch.Text + "%'";
        if (rdbStatus.Checked == true)
            search = " status like '%" + txtSearch.Text + "%'";
        if (rdbRelease.Checked == true)
            search = " release_no like '%" + txtSearch.Text + "%'";
        if (rdbinvoice.Checked == true)
            search = " invoice_number like '%" + txtSearch.Text + "%'";
        bindGrid(search);
        int i = Gvdolist.PageIndex + 1;
        if (i <= Gvdolist.PageCount)
            Gvdolist.PageIndex = i;
        Gvdolist.DataBind();
    }
    protected void lbPrev_Click(object sender, EventArgs e)
    {
        if (rdbDO .Checked == true)
            search = " deliveryOrder.do_id  like '%" + txtSearch.Text + "%'";
        if (rdbStatus.Checked == true)
            search = " status like '%" + txtSearch.Text + "%'";
        if (rdbRelease.Checked == true)
            search = " release_no like '%" + txtSearch.Text + "%'";
        if (rdbinvoice.Checked == true)
            search = " invoice_number like '%" + txtSearch.Text + "%'";
        bindGrid(search);
        int i = Gvdolist.PageCount;
        if (Gvdolist.PageIndex > 0)
            Gvdolist.PageIndex = Gvdolist.PageIndex - 1;
        Gvdolist.DataBind();
    }
    protected void lbLAst_Click(object sender, EventArgs e)
    {
        if (rdbDO.Checked == true)
            search = " deliveryOrder.do_id  like '%" + txtSearch.Text + "%'";
        if (rdbStatus.Checked == true)
            search = " status like '%" + txtSearch.Text + "%'";
        if (rdbRelease.Checked == true)
            search = " release_no like '%" + txtSearch.Text + "%'";
        if (rdbinvoice.Checked == true)
            search = " invoice_number like '%" + txtSearch.Text + "%'";
        bindGrid(search);
        Gvdolist.PageIndex = Gvdolist.PageCount;
        Gvdolist.DataBind();
    }
}