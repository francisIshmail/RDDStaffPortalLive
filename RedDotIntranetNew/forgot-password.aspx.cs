using System;
using System.Collections;
using System.Configuration;
using System.Data;
using System.Linq;
using System.Web;
using System.Web.Security;
using System.Web.UI;
using System.Web.UI.HtmlControls;
using System.Web.UI.WebControls;
using System.Web.UI.WebControls.WebParts;
using System.Xml.Linq;

public partial class forgotpassword : System.Web.UI.Page
{
    protected void Page_Load(object sender, EventArgs e)
    {
       
    }

    protected void btnSubmit_Click(object sender, EventArgs e)
    {
        /*
               try
               {
                   clsDealer objDealer = new clsDealer();
                   string strResult = objDealer.GetPassword(txtEmail.Text.Trim(), Convert.ToDateTime(txtDOB.Text.Trim()));
                   if (!string.IsNullOrEmpty(strResult))
                   {
                       string strBody = string.Empty;
                       strBody = "<br/><br/>Your Red Dot Password:  " + strResult + "<br/><br/><br/>";
                       Mail.Send("tej@reddot.co.tz", txtEmail.Text.Trim(), "Red Dot Password Confirmation!", strBody, true);
                       Clear();
                       Message.Show(Page, "Your password has been sent successfully, please check the email.");
                   }
                   else
                   {
                       Message.Show(Page, "Sorry, given information is not valid.");
                   }
               }
               catch (Exception Ex)
               {
                   Message.Show(Page, Ex.Message);
               }
       */    
    }

    private void Clear()
    {
        //txtEmail.Text = "";
        //txtDOB.Text = "";
    }
    protected void PasswordRecovery1_SendingMail(object sender, MailMessageEventArgs e)
    {
/*
        MailMessage mm = new MailMessage();


        mm.From = e.Message.From;

        mm.Subject = e.Message.Subject.ToString();

        mm.To.Add(e.Message.To[0]);

        mm.Body = e.Message.Body;
        SmtpClient smtp = new SmtpClient();
        smtp.EnableSsl = true;

        smtp.Send(mm);
        e.Cancel = true;
*/
    }

}
