using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

public partial class MsgBoxControl : System.Web.UI.UserControl
{
    protected void Page_Load(object sender, EventArgs e)
    {

    }
    public void show(string msg,string msgHeader)
    {
        pnlMessage.Visible = true;
        lblMsg.Text = msg;
        if (msgHeader == "")
            lblHeader.Text = "Message!!";
        else
            lblHeader.Text = msgHeader;
    }

    public void clearMessage()
    {
        lblMsg.Text = "";
        pnlMessage.Visible = false;
    }

    protected void lnkBtnOK_Click(object sender, EventArgs e)
    {
        clearMessage();
    }
}