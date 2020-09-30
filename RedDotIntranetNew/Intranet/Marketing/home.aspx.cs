using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Data;
public partial class home : System.Web.UI.Page
{
    MailShot mailshot = new MailShot();
    protected void Page_Load(object sender, EventArgs e)
    {
        if (!IsPostBack)
        {
            bindgried();
        }
    }
    protected void bindgried()
    {
      DataTable dt = mailshot.gethistory ();
      gvsnap.DataSource = dt;
      gvsnap.DataBind();

    }
    protected void gvsnap_PageIndexChanging(object sender, GridViewPageEventArgs e)
    {
        bindgried();
        gvsnap.PageIndex = e.NewPageIndex;
        gvsnap.DataBind();
    }

    protected void bindpopup()
    {
        DataTable dt = mailshot.gethistorybyID(Session[RunningCache.mailshotid].ToString());
    
    lbdate.Text  =dt.Rows[0] ["ModifiedDate"].ToString ();
          lbSender.Text  =dt.Rows[0] ["username"].ToString ();
          lbu.Text  =dt.Rows[0] ["bu"].ToString ();
          lbSubject.Text  =dt.Rows[0] ["suject"].ToString ();
          lbtarget.Text  =dt.Rows[0] ["TargetSale"].ToString ();
          txtArchieved.Text  =dt.Rows[0] ["SaleArchived"].ToString ();
          txtResponse.Text  =dt.Rows[0] ["Response"].ToString ();
         

    }
    protected void gvsnap_RowCommand(object sender, GridViewCommandEventArgs e)
    {
        int index = 0;
        index = Convert.ToInt32(e.CommandArgument) % gvsnap.PageSize;
        Session[RunningCache.mailshotid] = ((HiddenField)gvsnap.Rows[index].FindControl("hdnmailshotid")).Value;
        if (e.CommandName == "Details")
            {
              
                ModalPopupExtender1.Show();
                bindpopup();
               // ImageButton4_Click(null, null);
            }
    }

    protected void btnSave_Click(object sender, EventArgs e)
    {
        try
        {
            mailshot.updatemailShot(txtArchieved.Text, txtArchieved.Text, Session[RunningCache.mailshotid].ToString());
            trmsg.Visible = true;
            lbmsg.Text = "The Details has been successfully updated ";
            bindgried();
            trError.Visible = false ;

        }
        catch (Exception ex)
        {
            trError.Visible = true;
            lbmsgErr.Text = ex.Message;
        }
    
    }
}