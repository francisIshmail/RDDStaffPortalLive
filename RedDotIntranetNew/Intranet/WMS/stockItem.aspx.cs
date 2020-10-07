using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Data;

public partial class Intranet_WMS_stockItem : System.Web.UI.Page
{
    String search = "";
    WMSclsPrealert prealert = new WMSclsPrealert();
    protected void Page_Load(object sender, EventArgs e)
    {
        if (!IsPostBack)
        {

            bindgrid(search);
            Page.Title = "Stock Item";
        }
    }


    protected void bindgrid(String Search)
    {

        DataTable dt = prealert.NewPart(Search);
        GvNewPart.DataSource = dt;
        GvNewPart.DataBind();
    }

       
    protected void GvNewPart_PageIndexChanging(object sender, GridViewPageEventArgs e)
    {
        bindgrid(search);
        GvNewPart.PageIndex = e.NewPageIndex;
        GvNewPart.DataBind();
    }
    protected void btnAdd_Click(object sender, EventArgs e)
    {
        Response.Redirect("Addproduct.aspx");
    }
    protected void GvNewPart_RowCommand(object sender, GridViewCommandEventArgs e)
    {

    }
    protected void btnBack_Click(object sender, EventArgs e)
    {
        string from = Request.QueryString["from"].ToString();
        if (from =="1")
            Response.Redirect("NewPrealert.aspx");      
        else
            Response.Redirect("List_Prealert.aspx");
    }
    protected void lbFirst_Click(object sender, EventArgs e)
    {
        if (rdbPartNumber.Checked == true)
            search = " part_number like '%" + txtSearch.Text + "%'";
        if (rdbSupplier.Checked == true)
            search = " supplier_name like '%" + txtSearch.Text + "%'";
        bindgrid(search);
        GvNewPart.PageIndex = 0;
        GvNewPart.DataBind();

    }
    protected void lbNext_Click(object sender, EventArgs e)
    {
        if (rdbPartNumber.Checked == true)
            search = " part_number like '%" + txtSearch.Text + "%'";
        if (rdbSupplier.Checked == true)
            search = " supplier_name like '%" + txtSearch.Text + "%'";
        bindgrid(search);
        int i = GvNewPart.PageIndex + 1;
        if (i <= GvNewPart.PageCount)
            GvNewPart.PageIndex = i;
        GvNewPart.DataBind();
    }
    protected void lbPrev_Click(object sender, EventArgs e)
    {
        if (rdbPartNumber.Checked == true)
            search = " part_number like '%" + txtSearch.Text + "%'";
        if (rdbSupplier.Checked == true)
            search = " supplier_name like '%" + txtSearch.Text + "%'";
        bindgrid(search);
        int i = GvNewPart.PageCount;
        if (GvNewPart.PageIndex > 0)
            GvNewPart.PageIndex = GvNewPart.PageIndex - 1;
        GvNewPart.DataBind();
    }
    protected void lbLAst_Click(object sender, EventArgs e)
    {
        if (rdbPartNumber.Checked == true)
            search = " part_number like '%" + txtSearch.Text + "%'";
        if (rdbSupplier.Checked == true)
            search = " supplier_name like '%" + txtSearch.Text + "%'";
        bindgrid(search);
        GvNewPart.PageIndex = GvNewPart.PageCount;
        GvNewPart.DataBind();
    }
    protected void txtSearch_TextChanged(object sender, EventArgs e)
    {
        if (rdbPartNumber.Checked == true)
            search = " part_number like '%" + txtSearch.Text + "%'";
        if (rdbSupplier.Checked == true)
            search = " supplier_name like '%" + txtSearch.Text + "%'";
        bindgrid(search);

    }
    protected void btmSearch_Click(object sender, EventArgs e)
    {

    }
}