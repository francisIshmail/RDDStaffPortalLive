using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

public partial class Intranet_Marketing_DefaultImages : System.Web.UI.Page
{
    protected void Page_Load(object sender, EventArgs e)
    {
        Label1.Text = @"<asp:Image ID='Image1' runat='server'   ImageUrl='~/images/sc/tiraje.jpg' />
                    <br/>
                    <img src='../images/sc/tiraje.jpg' />
                    ";

    }
}