using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Data;
public partial class Intranet_WMS_Countries : System.Web.UI.Page
{
    ClsAdmin admin = new ClsAdmin();
    protected void Page_Load(object sender, EventArgs e)
    {

        if (!IsPostBack)
        {
            Page.Title = "Country";
            BindGrid();
        }

    }

    public void BindGrid()
    {
      
        DataTable dt = admin.Bindcountry();
        gvcontry.DataSource = dt;
        gvcontry.DataBind();
    }

    protected void gvcontry_PageIndexChanging(object sender, GridViewPageEventArgs e)
    {
        BindGrid();
        gvcontry.PageIndex = e.NewPageIndex;
        gvcontry.DataBind();
    }
    protected void btback_Click(object sender, EventArgs e)
    {
        Response.Redirect("WMSAdmin.aspx");
    }
    protected void gvcontry_RowCommand(object sender, GridViewCommandEventArgs e)
    {

    }
    protected void Button1_Click(object sender, EventArgs e)
    {
        if (tbcountry.Visible == false)
        {
            tbcountry.Visible = true;
         
            btncountry.Text = "Save";


        }


        else if (btncountry.Text == "Save")
        {

            admin.AddnewCountry(txtcountry.Text);
            tbcountry.Visible = false;
            lbmsg.Visible = true;
            lbmsg.Text = "Country was added succesffuly.";
            btncountry.Text = " Add New Country";
        }
    }
}