using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Data;
using System.Net.Mail;
using System.IO;

using System.Net.Mime;
using System.Configuration;

/// <summary>
/// Summary description for clsEmail
/// </summary>
public class clsEmail
{
    public clsEmail()
    {
        //
        // TODO: Add constructor logic here
        //
    }

    public Boolean EmailConfig(string emailAddress, string template, MailMessage msg, String Signature, String MailShopt)
    {

        msg.IsBodyHtml = true;


        System.Net.Mail.AlternateView htmlView = null;
        string StrMailShopt = MailShopt;
        string StrSignature = Signature;
        try
        {
            if (File.Exists(StrMailShopt))
            {


                htmlView = System.Net.Mail.AlternateView.CreateAlternateViewFromString(template, null, "text/html");
                LinkedResource pic1 = new LinkedResource(StrMailShopt, MediaTypeNames.Image.Jpeg);
                LinkedResource pic2 = new LinkedResource(StrSignature, MediaTypeNames.Image.Jpeg);
                pic1.ContentId = "MailShot";
                pic2.ContentId = "Signature";
                htmlView.LinkedResources.Add(pic1);
                htmlView.LinkedResources.Add(pic2);
                msg.AlternateViews.Add(htmlView);

            }
            else
            {
                msg.Body = template;
            }
            msg.To.Add(emailAddress);
            msg.From = new MailAddress("marketing@reddotdistribution.com");



            SmtpClient objSMTPClient;
            System.Net.NetworkCredential objNetworkCredential;
            //  objSMTPClient = new SmtpClient("smtp.gmail.com", 25);
            objSMTPClient = new SmtpClient("mail.reddotdistribution.com", 25);

            objSMTPClient.EnableSsl = false;
            objSMTPClient.UseDefaultCredentials = false;
            //  objNetworkCredential = new System.Net.NetworkCredential("samthia07@gmail.com", "fog465farm5241");
            objNetworkCredential = new System.Net.NetworkCredential("marketing@reddotdistribution.com", "marketing123");
            objSMTPClient.Credentials = objNetworkCredential;
            objSMTPClient.Send(msg);
            return true;
        }
        catch (Exception ex)
        {
            return false;
        }
    }


    public Boolean EmailPricelistConfig(string emailAddress, string template, MailMessage msg, String Signature, String PriceList)
    {

        msg.IsBodyHtml = true;
        // emailAddress = "samuel@eternatec.com";

        System.Net.Mail.AlternateView htmlView = null;
        string strPriceListPath = PriceList;
        string StrSignature = Signature;
        try
        {
            if (File.Exists(strPriceListPath))
            {


                htmlView = System.Net.Mail.AlternateView.CreateAlternateViewFromString(template, null, "text/html");

                LinkedResource pic2 = new LinkedResource(StrSignature, MediaTypeNames.Image.Jpeg);

                pic2.ContentId = "Signature";

                htmlView.LinkedResources.Add(pic2);
                msg.AlternateViews.Add(htmlView);

                Attachment Data = new Attachment(strPriceListPath, MediaTypeNames.Application.Octet);

                msg.Attachments.Add(Data);


            }
            else
            {
                msg.Body = template;
            }
            msg.To.Add(emailAddress);
            msg.From = new MailAddress("marketing@reddotdistribution.com");



            SmtpClient objSMTPClient;
            System.Net.NetworkCredential objNetworkCredential;
            //  objSMTPClient = new SmtpClient("smtp.gmail.com", 25);
            objSMTPClient = new SmtpClient("mail.reddotdistribution.com", 25);

            objSMTPClient.EnableSsl = false;
            objSMTPClient.UseDefaultCredentials = false;
            //  objNetworkCredential = new System.Net.NetworkCredential("samthia07@gmail.com", "fog465farm5241");
            objNetworkCredential = new System.Net.NetworkCredential("marketing@reddotdistribution.com", "marketing123");
            objSMTPClient.Credentials = objNetworkCredential;
            objSMTPClient.Send(msg);
            return true;
        }
        catch (Exception ex)
        {
            return false;
        }
    }
}