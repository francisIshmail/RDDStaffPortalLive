using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Data;
public partial class Intranet_WMS_users : System.Web.UI.Page
{
    protected void Page_Load(object sender, EventArgs e)
    {

        if (!IsPostBack)
        {
            BindGrid();
        }

    }

    public void BindGrid()
    {
        ClsAdmin admin = new ClsAdmin();
        DataTable dt = admin.BindUsers();
        gvusers.DataSource = dt;
        gvusers.DataBind();
    }
    protected void gvusers_PageIndexChanging(object sender, GridViewPageEventArgs e)
    {
        BindGrid();
        gvusers.PageIndex = e.NewPageIndex;
        gvusers.DataBind();
    }
    protected void btback_Click(object sender, EventArgs e)
    {
        Response.Redirect("WMSAdmin.aspx");
    }
}