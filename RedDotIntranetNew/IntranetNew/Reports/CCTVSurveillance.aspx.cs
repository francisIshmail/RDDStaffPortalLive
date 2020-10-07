using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

public partial class IntranetNew_Reports_CCTVSurveillance : System.Web.UI.Page
{
    protected void Page_Load(object sender, EventArgs e)
    {
        try
        {
            if (!IsPostBack)
            {
                string[] cctvusers = myGlobal.getAppSettingsDataForKey("CCTVAuthUsers").Split(';');
                if (!cctvusers.Contains(myGlobal.loggedInUser()))
                {
                    Response.Redirect("Default.aspx?UserAccess=0&FormName=Red Dot CCTV");
                }
            }
        }
        catch { }
    }
}