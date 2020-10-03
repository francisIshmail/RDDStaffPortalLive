using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Data;

public partial class Intranet_WMS_BOelist : System.Web.UI.Page
{
    WMSclsBoes boes = new WMSclsBoes();
    WMSclsPrealert prealert = new WMSclsPrealert();
    string search = "";
    protected void Page_Load(object sender, EventArgs e)
    {
        if (!IsPostBack)
            bindgrid(search);
        Page.Title = "Bills Of Entry";
    }

    public void bindgrid(String search)
    {

        DataTable dt = boes.Gvboes(search);
        Gvboes.DataSource = dt;
        Gvboes.DataBind();
    }


    protected void Gvboes_PageIndexChanging(object sender, GridViewPageEventArgs e)
    {
        bindgrid(search );
        Gvboes.PageIndex = e.NewPageIndex;
        Gvboes.DataBind();
    }
    protected void btreceiveGood_Click(object sender, EventArgs e)
    {

    }
    protected void Gvboes_RowCommand(object sender, GridViewCommandEventArgs e)
    {
        int index = Convert.ToInt32(e.CommandArgument) % Gvboes.PageSize;
        string boeID = ((Label)Gvboes.Rows[index].FindControl("lbBoeID")).Text;
        if (e.CommandName == "view")
            Response.Redirect("BoeDetails.aspx?boeID=" +boeID );

        if (e.CommandName == "Deleting")
        {

           prealert.deleteboe(boeID);
           trMessage.Visible = true;
           lbmsg.Text = "The bill of Entry has been successfully deleted.";
            bindgrid(search);
        }
    }


    protected void lbFirst_Click(object sender, EventArgs e)
    {
        if (rdbBoe.Checked == true)
            search = " Boe_id like '%" + txtSearch.Text + "%'";
        if (rdbPrealert.Checked == true)
            search = " prealert_id like '%" + txtSearch.Text + "%'";
        if (rdbstatus.Checked == true)
            search = " status like '%" + txtSearch.Text + "%'";
        if (rdbsupplier.Checked == true)
            search = " supplier_name like '%" + txtSearch.Text + "%'";
        bindgrid(search);
        Gvboes.PageIndex = 0;
        Gvboes.DataBind();
    }
    protected void lbNext_Click(object sender, EventArgs e)
    {
        if (rdbBoe.Checked == true)
            search = " Boe_id like '%" + txtSearch.Text + "%'";
        if (rdbPrealert.Checked == true)
            search = " prealert_id like '%" + txtSearch.Text + "%'";
        if (rdbstatus.Checked == true)
            search = " status like '%" + txtSearch.Text + "%'";
        if (rdbsupplier.Checked == true)
            search = " supplier_name like '%" + txtSearch.Text + "%'";
        bindgrid(search);
        int i = Gvboes.PageIndex + 1;
        if (i <= Gvboes.PageCount)
            Gvboes.PageIndex = i;
        Gvboes.DataBind();
    }
    protected void lbPrev_Click(object sender, EventArgs e)
    {
        if (rdbBoe.Checked == true)
            search = " Boe_id like '%" + txtSearch.Text + "%'";
        if (rdbPrealert.Checked == true)
            search = " prealert_id like '%" + txtSearch.Text + "%'";
        if (rdbstatus.Checked == true)
            search = " status like '%" + txtSearch.Text + "%'";
        if (rdbsupplier.Checked == true)
            search = " supplier_name like '%" + txtSearch.Text + "%'";
        bindgrid(search);
        int i = Gvboes.PageCount;
        if (Gvboes.PageIndex > 0)
            Gvboes.PageIndex = Gvboes.PageIndex - 1;
        Gvboes.DataBind();
    }
    protected void lbLAst_Click(object sender, EventArgs e)
    {
        if (rdbBoe.Checked == true)
            search = " Boe_id like '%" + txtSearch.Text + "%'";
        if (rdbPrealert.Checked == true)
            search = " prealert_id like '%" + txtSearch.Text + "%'";
        if (rdbstatus.Checked == true)
            search = " status like '%" + txtSearch.Text + "%'";
        if (rdbsupplier.Checked == true)
            search = " supplier_name like '%" + txtSearch.Text + "%'";
        bindgrid(search);
        Gvboes.PageIndex = Gvboes.PageCount;
        Gvboes.DataBind();
    }
    protected void txtSearch_TextChanged(object sender, EventArgs e)
    {
        if (rdbBoe.Checked == true)
            search = " Boe_id like '%" + txtSearch.Text + "%'";
        if (rdbPrealert.Checked == true)
            search = " prealert_id like '%" + txtSearch.Text + "%'";
        if (rdbstatus .Checked == true)
            search = " status like '%" + txtSearch.Text + "%'";
        if (rdbsupplier.Checked == true)
            search = " supplier_name like '%" + txtSearch.Text + "%'";
        bindgrid(search);
    }
    protected void btmSearch_Click(object sender, EventArgs e)
    {
        if (rdbBoe.Checked == true)
            search = " Boe_id like '%" + txtSearch.Text + "%'";
        if (rdbPrealert.Checked == true)
            search = " prealert_id like '%" + txtSearch.Text + "%'";
        if (rdbstatus.Checked == true)
            search = " status like '%" + txtSearch.Text + "%'";
        if (rdbsupplier.Checked == true)
            search = " supplier_name like '%" + txtSearch.Text + "%'";
        bindgrid(search);
    }
}