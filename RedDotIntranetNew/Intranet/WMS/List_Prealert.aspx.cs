using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Data;
using System.Data.OleDb;

public partial class Intranet_WMS_List_Prealert : System.Web.UI.Page
{
    WMSclsPrealert prealert = new WMSclsPrealert();
    string search = "";
    protected void Page_Load(object sender, EventArgs e)
    {
        if (!IsPostBack)
        {
            Page.Title = "Prealert";
            bindgrid();
        }
    }

    protected void btmSearch_Click(object sender, EventArgs e)
    {
        bindgrid();
    }
    
    

    protected void bindgrid()
    {
        String srh="";

        if (rdbPrealert.Checked == true && txtSearch.Text.Trim() != "")
            srh = " where prealert_id like '%" + txtSearch.Text + "%'";

        if (rdbBoe.Checked == true && txtSearch.Text.Trim() != "")
            srh = " where Boe_id like '%" + txtSearch.Text + "%'";


        if (rdbstatus.Checked == true && txtSearch.Text.Trim() != "")
            srh = " where Status like '%" + txtSearch.Text + "%'";

        DataTable dt1 = new DataTable();
        dt1 = prealert.gvprealert(srh);
        GvListprealert.DataSource = dt1;
        GvListprealert.DataBind();
        lblRowCount.Text = "Rows : " + dt1.Rows.Count.ToString();
    }

    protected void Button1_Click(object sender, EventArgs e)
    {
        Response.Redirect("NewPrealert.aspx");
    }

    protected void GvListprealert_PageIndexChanging(object sender, GridViewPageEventArgs e)
    {
        bindgrid();
        GvListprealert.PageIndex = e.NewPageIndex;
        GvListprealert.DataBind();
    }

    protected void GvListprealert_RowCommand(object sender, GridViewCommandEventArgs e)
    {
        int index = Convert.ToInt32(e.CommandArgument) % GvListprealert.PageSize;
         //String prealertID = ((Button)GvListprealert.Rows[index].FindControl("BtPrealert_id")).Text;
         String prealertID = ((LinkButton)GvListprealert.Rows[index].FindControl("lnkBtPrealert_id")).Text;

         //String boeId = ((Button)GvListprealert.Rows[index].FindControl("BtBoe_id")).Text;
         String boeId = ((LinkButton)GvListprealert.Rows[index].FindControl("lnkBtBoe_id")).Text;

        if (e.CommandName == "boe")
          Response.Redirect("Prealert.aspx?boeID=" + boeId);
   
        if (e.CommandName == "prealert")
            Response.Redirect("Prealert.aspx?prealertiD=" + prealertID);

        if (e.CommandName == "Deleting")
        {
            prealert.DeletePrealert(prealertID);
            bindgrid();
            trMessage.Visible = true;
            lbmsg.Text = "The prealert was successfully deleted.";
            lbmsg.Visible = true;
        }
            
    }
  
    protected void lbLAst_Click(object sender, EventArgs e)
    {
        bindgrid();
        GvListprealert.PageIndex = GvListprealert.PageCount;
        GvListprealert.DataBind();
    }

    protected void lbPrev_Click(object sender, EventArgs e)
    {
        bindgrid();
        int i = GvListprealert.PageCount;
                if (GvListprealert.PageIndex > 0)
            GvListprealert.PageIndex = GvListprealert.PageIndex - 1;
                GvListprealert.DataBind();
    }

    protected void lbNext_Click(object sender, EventArgs e)
    {
        bindgrid();
        int i = GvListprealert.PageIndex + 1;
        if (i <= GvListprealert.PageCount)
            GvListprealert.PageIndex = i;
        GvListprealert.DataBind();
    }

    protected void lbFirst_Click(object sender, EventArgs e)
    {
        bindgrid();
        GvListprealert.PageIndex = 0;
        GvListprealert.DataBind();
    }

    
}