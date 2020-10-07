using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

public partial class IntranetNew_Funnel_Default : System.Web.UI.Page
{
    protected void Page_Load(object sender, EventArgs e)
    {
        try
        {
            if (!IsPostBack)
            {
                if (Request.QueryString["UserAccess"] != null && Request.QueryString["FormName"] != null)
                {
                    string UserAccess = Request.QueryString["UserAccess"].ToString();
                    string FormName = Request.QueryString["FormName"].ToString();

                    if (!string.IsNullOrEmpty(UserAccess) && !string.IsNullOrEmpty(FormName))
                    {
                        lblMsg.Text = " You are not authorized to access " + FormName + ". Please contact IT team...";
                        Page.ClientScript.RegisterStartupScript(this.GetType(), "alert", "<script> alert(' You are not authorized to access " + FormName + " page. Please contact IT team...'); </script>");
                    }
                }
            }
        }
        catch (Exception ex)
        {
            lblMsg.Text = "Error Occured in PageLoad() :" + ex.Message;
        }
    }
}