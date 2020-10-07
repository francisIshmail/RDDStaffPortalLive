using System;
using System.Collections;
using System.Configuration;
using System.Data;
using System.Linq;
using System.Web;
using System.Web.Security;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Web.UI.WebControls.WebParts;
using System.Web.UI.HtmlControls;
using System.Xml.Linq;

public partial class reddotIntranet : System.Web.UI.MasterPage
{
    protected void Page_Load(object sender, EventArgs e)
    {

        if (Session["sessionMacName"] == null)
        {
            Session["sessionMacName"] = "";
        }
        if (Session["sessionMacName"].ToString() != Session.SessionID.ToString())
        {
            if (myGlobal.loggedInUser() != "")
            {
                FormsAuthentication.SetAuthCookie(myGlobal.loggedInUser(), false);
                Session["sessionMacName"] = Session.SessionID.ToString();
            }
        
        }

        if (myGlobal.isCurrentUserOnRole("admin"))
            hrefAdmHome.Visible = true;
        else
            hrefAdmHome.Visible = false;
       
    }
}
