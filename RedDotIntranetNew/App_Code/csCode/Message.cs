using System;
using System.Data;
using System.Configuration;
using System.Linq;
using System.Web;
using System.Web.Security;
using System.Web.UI;
using System.Web.UI.HtmlControls;
using System.Web.UI.WebControls;
using System.Web.UI.WebControls.WebParts;
using System.Xml.Linq;
using System.Windows.Forms;

/// <summary>
/// Summary description for Message
/// </summary>
public class Message
{
	public Message()
	{
		//
		// TODO: Add constructor logic here
		//
	}

    public static void Show1(Page Page, string Msg)
    {
        MessageBox.Show(Msg);
        return;

        string script = @"alert('" + Msg + "');this.close();";
        ScriptManager.RegisterStartupScript(Page, Page.GetType(), "alertMsg", script, true);
    }
    public static void Show(Page Page, string Msg)
    {
        string script = @"alert('" + Msg + "');";
        ScriptManager.RegisterStartupScript(Page , Page.GetType(), "alertMsg", script, true);
        
    }
}
