using System;
using System.Data;
using System.Data.SqlClient;
using System.Configuration;
using System.Web;
using System.Web.Security;
using System.Web.UI;
using System.Web.UI.HtmlControls;
using System.Web.UI.WebControls;
using System.Web.UI.WebControls.WebParts;
using System.Net.Mail;
using System.Net.Mime;
using System.IO;
/// <summary>
/// Summary description for Utility
/// </summary>
public class Mail
{
    private static string strMessage = string.Empty;
    private static string GlobalOutput = string.Empty;

    public Mail()
    {
        //
        // TODO: Add constructor logic here
        //
    }

    private static DataTable GetMailSettings()
    {
        DataTable dtMailSettings = new DataTable();
        SqlConnection connMail; SqlDataAdapter sdaMail;

        using (connMail = new SqlConnection(ConfigurationManager.ConnectionStrings["dbEcommerceConnectionString"].ConnectionString))
        {
            using (sdaMail = new SqlDataAdapter("getMailSettings", connMail))
            {
                try
                {
                    sdaMail.SelectCommand.CommandType = CommandType.StoredProcedure;

                    sdaMail.SelectCommand.Parameters.AddWithValue("@Seed", myGlobal.Seed);
                    sdaMail.SelectCommand.Parameters.AddWithValue("@Output", GlobalOutput).Direction = ParameterDirection.Output;
                    sdaMail.Fill(dtMailSettings);
                }
                catch (Exception Ex)
                {
                    throw new Exception(Ex.Message);
                }
                finally
                {
                    sdaMail.SelectCommand.Connection.Close();
                }
            }
        }
        return dtMailSettings;
    }

    /// <summary>
    /// Sending Mail Function...
    /// </summary>
    /// <param name="From">Email From address.</param>
    /// <param name="To">Email To address.</param>
    /// <param name="Subject">Email Subject</param>
    /// <param name="Message">Email Body</param>
    /// <param name="IsHtmlBody">Is Html Body ?</param>
    public static string Send(string From, string To, string Subject, string Body, bool IsHtmlBody)
    {
        try
        {
            ////            DataTable dtMailSettings = new DataTable();
            ////            SqlConnection connMail; SqlCommand cmdMail;
            System.Net.NetworkCredential objNetworkCredential;

            //dtMailSettings = GetMailSettings();

            //if (dtMailSettings.Rows.Count > 0)
            //{
            SmtpClient objSMTPClient;

            //string strHostName = dtMailSettings.Rows[0]["Email_Host_Name"].ToString();
            //string strPort = dtMailSettings.Rows[0]["Email_Port_No"].ToString();

            //string strHostName = "smtp.raha.com";
            //string strPort = "25";
            string strHostName = myGlobal.getSystemConfigValue("smtpHost");//"smtp.gmail.com";
            string strPort = myGlobal.getSystemConfigValue("smtpPort");//"587";

            //string strUserName = dtMailSettings.Rows[0]["Email_User_Name"].ToString();
            //string strPassword = dtMailSettings.Rows[0]["Email_Password"].ToString();

            //string strUserName = "tej@reddot.co.tz";
            //string strPassword = "aMAZINGgRACE";
            //string strUserName = "minhases@gmail.com";
            string strUserName = myGlobal.getSystemConfigValue("smtpUserId");//"minhases@gmail.com";
            string strPassword = myGlobal.getSystemConfigValue("smtpPassword");//"tejgrace";


            if (!string.IsNullOrEmpty(strHostName) && !string.IsNullOrEmpty(strPort))
            {
                objSMTPClient = new SmtpClient(strHostName, Convert.ToInt32(strPort));
            }
            else
            {
                objSMTPClient = new SmtpClient(strHostName);
            }
            objSMTPClient.EnableSsl = false;
            objSMTPClient.UseDefaultCredentials = false;

            if (!string.IsNullOrEmpty(strUserName) && !string.IsNullOrEmpty(strPassword))
            {
                objNetworkCredential = new System.Net.NetworkCredential(strUserName, strPassword);
                objSMTPClient.Credentials = objNetworkCredential;
            }

            MailMessage objMailMessage = new MailMessage();
            MailAddress objMailAdress = new MailAddress(From);
            objMailMessage.IsBodyHtml = IsHtmlBody;

            objMailMessage.From = objMailAdress;
            objMailMessage.To.Add(To);
            objMailMessage.Subject = Subject;
            objMailMessage.Body = Body;

            objSMTPClient.Send(objMailMessage); 

            strMessage = myGlobal.getSystemConfigValue("mailSuccessMsg");
            //}
        }
        catch (Exception Ex)
        {
            strMessage = "Cannot send mail ,  " + Ex.Message;
            //Console.WriteLine("Cannot send msg: {0}", strMessage);
        }
        return strMessage;
    }
    public static string SendCC(string From, string To, string cc, string Subject, string Body, bool IsHtmlBody)
    {
        try
        {
            ////            DataTable dtMailSettings = new DataTable();
            ////            SqlConnection connMail; SqlCommand cmdMail;
            System.Net.NetworkCredential objNetworkCredential;

            //dtMailSettings = GetMailSettings();

            //if (dtMailSettings.Rows.Count > 0)
            //{
            SmtpClient objSMTPClient;

            //string strHostName = dtMailSettings.Rows[0]["Email_Host_Name"].ToString();
            //string strPort = dtMailSettings.Rows[0]["Email_Port_No"].ToString();

            //string strHostName = "smtp.raha.com";
            //string strPort = "25";
            string strHostName = myGlobal.getSystemConfigValue("smtpHost");//"smtp.gmail.com";
            string strPort = myGlobal.getSystemConfigValue("smtpPort");//"587";

            //string strUserName = dtMailSettings.Rows[0]["Email_User_Name"].ToString();
            //string strPassword = dtMailSettings.Rows[0]["Email_Password"].ToString();

            //string strUserName = "tej@reddot.co.tz";
            //string strPassword = "aMAZINGgRACE";
            string strUserName = myGlobal.getSystemConfigValue("smtpUserId");//"minhases@gmail.com";
            string strPassword = myGlobal.getSystemConfigValue("smtpPassword");//"tejgrace";


            if (!string.IsNullOrEmpty(strHostName) && !string.IsNullOrEmpty(strPort))
            {
                objSMTPClient = new SmtpClient(strHostName, Convert.ToInt32(strPort));
            }
            else
            {
                objSMTPClient = new SmtpClient(strHostName);
            }
            objSMTPClient.EnableSsl = false;
            objSMTPClient.UseDefaultCredentials = false;

            if (!string.IsNullOrEmpty(strUserName) && !string.IsNullOrEmpty(strPassword))
            {
                objNetworkCredential = new System.Net.NetworkCredential(strUserName, strPassword);
                objSMTPClient.Credentials = objNetworkCredential;
            }

            MailMessage objMailMessage = new MailMessage();
            MailAddress objMailAdress = new MailAddress(From);
            objMailMessage.IsBodyHtml = IsHtmlBody;

            objMailMessage.From = objMailAdress;

            //objMailMessage.To.Add(To);
            string[] sfsl = To.Split(';');
            for (int i = 0; i < sfsl.Length; i++)
            {
                objMailMessage.To.Add(sfsl[i]);
            }

            if (cc.Trim() != "")
                objMailMessage.CC.Add(cc);

            objMailMessage.Subject = Subject;
            objMailMessage.Body = Body;

            objSMTPClient.Send(objMailMessage); 

            strMessage = myGlobal.getSystemConfigValue("mailSuccessMsg");
        }
        catch (Exception Ex)
        {
            strMessage = "Cannot send mail ,  " + Ex.Message;
            //Console.WriteLine("Cannot send msg: {0}", strMessage);
        }
        return strMessage;
    }

    public static string SendSingleAttachPV(string From, string To, string cc, string Subject, string Body, bool IsHtmlBody, string AttachmentFilePath)
    {
        try
        {

            System.Net.NetworkCredential objNetworkCredential;
            SmtpClient objSMTPClient;

            string strHostName = myGlobal.getSystemConfigValue("smtpHost");//"smtp.gmail.com";
            string strPort = myGlobal.getSystemConfigValue("smtpPort");//"587";
            string strUserName = "reddotstaff@reddotdistribution.com";  //myGlobal.getSystemConfigValue("smtpUserId");//"minhases@gmail.com";
            string strPassword = "8Reddot1"; // myGlobal.getSystemConfigValue("smtpPassword");//"tejgrace";

            if (!string.IsNullOrEmpty(strHostName) && !string.IsNullOrEmpty(strPort))
            {
                objSMTPClient = new SmtpClient(strHostName, Convert.ToInt32(strPort));
            }
            else
            {
                objSMTPClient = new SmtpClient(strHostName);
            }
            objSMTPClient.EnableSsl = false;
            objSMTPClient.UseDefaultCredentials = false;

            if (!string.IsNullOrEmpty(strUserName) && !string.IsNullOrEmpty(strPassword))
            {
                objNetworkCredential = new System.Net.NetworkCredential(strUserName, strPassword);
                objSMTPClient.Credentials = objNetworkCredential;
            }

            MailMessage objMailMessage = new MailMessage();
            MailAddress objMailAdress = new MailAddress(From);
            objMailMessage.IsBodyHtml = IsHtmlBody;

            objMailMessage.From = objMailAdress;

            string[] sfsl = To.Split(';');
            for (int i = 0; i < sfsl.Length; i++)
            {
                objMailMessage.To.Add(sfsl[i].Trim());
            }
            //objMailMessage.To.Add(To);
            
            if (cc != "")
            {
                string[] sfsl1 = cc.Split(';');
                for (int i = 0; i < sfsl1.Length; i++)
                {
                    objMailMessage.CC.Add(sfsl1[i].Trim());
                }
                //objMailMessage.CC.Add(cc);
            }

            //objMailMessage.Bcc.Add("pramod@reddotdistribution.com");

            objMailMessage.Subject = Subject;
            objMailMessage.Body = Body;

            if (AttachmentFilePath.Trim() != "")
            {
                string[] sfslattachmnt = AttachmentFilePath.Split('?');
                for (int i = 0; i < sfslattachmnt.Length; i++)
                {
                    if (sfslattachmnt[i].Trim() != "")
                    {
                        Attachment attachFile = new Attachment( sfslattachmnt[i]);
                        objMailMessage.Attachments.Add(attachFile);
                    }
                }
            }
            //Attachment atchFile = new Attachment(AttachmentFilePath);
            //objMailMessage.Attachments.Add(atchFile);

            objSMTPClient.Send(objMailMessage);  //open later sending mails 

            strMessage = myGlobal.getSystemConfigValue("mailSuccessMsg");
        }
        catch (Exception Ex)
        {
            strMessage = "Cannot send mail ,  " + Ex.Message;
            //Console.WriteLine("Cannot send msg: {0}", strMessage);
        }
        return strMessage;
    }


    public static string SendSingleAttach(string From, string To, string cc, string Subject, string Body, bool IsHtmlBody, string AttachmentFilePath)
    {
        try
        {
            
            System.Net.NetworkCredential objNetworkCredential;
            SmtpClient objSMTPClient;

            string strHostName = myGlobal.getSystemConfigValue("smtpHost");//"smtp.gmail.com";
            string strPort = myGlobal.getSystemConfigValue("smtpPort");//"587";
            string strUserName = myGlobal.getSystemConfigValue("smtpUserId");//"minhases@gmail.com";
            string strPassword = myGlobal.getSystemConfigValue("smtpPassword");//"tejgrace";

            if (!string.IsNullOrEmpty(strHostName) && !string.IsNullOrEmpty(strPort))
            {
                objSMTPClient = new SmtpClient(strHostName, Convert.ToInt32(strPort));
            }
            else
            {
                objSMTPClient = new SmtpClient(strHostName);
            }
            objSMTPClient.EnableSsl = false;
            objSMTPClient.UseDefaultCredentials = false;

            if (!string.IsNullOrEmpty(strUserName) && !string.IsNullOrEmpty(strPassword))
            {
                objNetworkCredential = new System.Net.NetworkCredential(strUserName, strPassword);
                objSMTPClient.Credentials = objNetworkCredential;
            }

            MailMessage objMailMessage = new MailMessage();
            MailAddress objMailAdress = new MailAddress(From);
            objMailMessage.IsBodyHtml = IsHtmlBody;

            objMailMessage.From = objMailAdress;
            objMailMessage.To.Add(To);
            if (cc != "")
            {
                objMailMessage.CC.Add(cc);
            }
            objMailMessage.Subject = Subject;
            objMailMessage.Body = Body;


            Attachment atchFile = new Attachment(AttachmentFilePath);
            objMailMessage.Attachments.Add(atchFile);

            objSMTPClient.Send(objMailMessage);  //open later sending mails 

            strMessage = myGlobal.getSystemConfigValue("mailSuccessMsg");
        }
        catch (Exception Ex)
        {
            strMessage = "Cannot send mail ,  " + Ex.Message;
            //Console.WriteLine("Cannot send msg: {0}", strMessage);
        }
        return strMessage;
    }

    public static string SendSingleAttachMarketing_OldWorking(string From, string To, string cc, string Subject, string Body, bool IsHtmlBody, string AttachmentFilePath)
    {
        try
        {

            System.Net.NetworkCredential objNetworkCredential;
            SmtpClient objSMTPClient;

            string strHostName = "mail.reddotdistribution.com"; myGlobal.getSystemConfigValue("smtpHost");//"smtp.gmail.com";
            string strPort = "25"; myGlobal.getSystemConfigValue("smtpPort");//"587";
            string strUserName = "marketing@reddotdistribution.com"; //myGlobal.getSystemConfigValue("smtpUserId");//"minhases@gmail.com";
            string strPassword = "marketing123"; //myGlobal.getSystemConfigValue("smtpPassword");//"tejgrace";


            if (!string.IsNullOrEmpty(strHostName) && !string.IsNullOrEmpty(strPort))
            {
                objSMTPClient = new SmtpClient(strHostName, Convert.ToInt32(strPort));
            }
            else
            {
                objSMTPClient = new SmtpClient(strHostName);
            }
            objSMTPClient.EnableSsl = false;
            objSMTPClient.UseDefaultCredentials = false;

            if (!string.IsNullOrEmpty(strUserName) && !string.IsNullOrEmpty(strPassword))
            {
                objNetworkCredential = new System.Net.NetworkCredential(strUserName, strPassword);
                objSMTPClient.Credentials = objNetworkCredential;
            }

            MailMessage objMailMessage = new MailMessage();
            MailAddress objMailAdress = new MailAddress(From);
            objMailMessage.IsBodyHtml = IsHtmlBody;

            objMailMessage.From = objMailAdress;

            //objMailMessage.To.Add(To);
            //if (cc != "")
            //{
            //    objMailMessage.CC.Add(cc);
            //}
            string[] arrTO = To.Split(';');
            for (int i = 0; i < arrTO.Length; i++)
            {
                if (arrTO[i].Trim() != "")
                    objMailMessage.To.Add(arrTO[i]);
            }

            string[] arrCC = cc.Split(';');
            for (int i = 0; i < arrCC.Length; i++)
            {
                if (arrCC[i].Trim() != "")
                    objMailMessage.Bcc.Add(arrCC[i]);  //change here  BCC from CC
            }

            objMailMessage.Subject = Subject;
            objMailMessage.Body = Body;


            if (AttachmentFilePath.Trim() != "")
            {
                Attachment atchFile = new Attachment(AttachmentFilePath);
                objMailMessage.Attachments.Add(atchFile);
            }

            // objSMTPClient.Send(objMailMessage);//open later sending mails

            strMessage = myGlobal.getSystemConfigValue("mailSuccessMsg");
        }
        catch (Exception Ex)
        {
            strMessage = "Cannot send mail ,  " + Ex.Message;
            //Console.WriteLine("Cannot send msg: {0}", strMessage);
        }
        return strMessage;
    }

    public static string SendSingleAttachMarketing(string From, string To, string cc, string Subject, string Body, bool IsHtmlBody, string AttachmentFilePath, string pAttachFileExt, string signatureFilePath)
    {
        try
        {

            System.Net.NetworkCredential objNetworkCredential;
            SmtpClient objSMTPClient;

            string strHostName = "mail.reddotdistribution.com"; myGlobal.getSystemConfigValue("smtpHost");//"smtp.gmail.com";
            string strPort = "25"; myGlobal.getSystemConfigValue("smtpPort");//"587";
            string strUserName = "marketing@reddotdistribution.com"; //myGlobal.getSystemConfigValue("smtpUserId");//"minhases@gmail.com";
            string strPassword = "marketing123"; //myGlobal.getSystemConfigValue("smtpPassword");//"tejgrace";


            if (!string.IsNullOrEmpty(strHostName) && !string.IsNullOrEmpty(strPort))
            {
                objSMTPClient = new SmtpClient(strHostName, Convert.ToInt32(strPort));
            }
            else
            {
                objSMTPClient = new SmtpClient(strHostName);
            }
            objSMTPClient.EnableSsl = false;
            objSMTPClient.UseDefaultCredentials = false;

            if (!string.IsNullOrEmpty(strUserName) && !string.IsNullOrEmpty(strPassword))
            {
                objNetworkCredential = new System.Net.NetworkCredential(strUserName, strPassword);
                objSMTPClient.Credentials = objNetworkCredential;
            }

            MailMessage objMailMessage = new MailMessage();
            MailAddress objMailAdress = new MailAddress(From);
            objMailMessage.IsBodyHtml = IsHtmlBody;

            objMailMessage.From = objMailAdress;

            string[] arrTO = To.Split(';');
            for (int i = 0; i < arrTO.Length; i++)
            {
                if (arrTO[i].Trim() != "")
                    objMailMessage.To.Add(arrTO[i]);
            }

            string[] arrCC = cc.Split(';');
            for (int i = 0; i < arrCC.Length; i++)
            {
                if (arrCC[i].Trim() != "")
                    objMailMessage.Bcc.Add(arrCC[i]);  //change here  BCC from CC
            }

            objMailMessage.Subject = Subject;

            ////////////////////////////////////////////// new code //////////////////////////////////////////////////////////////////////
            System.Net.Mail.AlternateView htmlView = null;

            if (signatureFilePath != "" && File.Exists(signatureFilePath))  //if signauture image is avaiable then just create htmlview 
            {
                htmlView = System.Net.Mail.AlternateView.CreateAlternateViewFromString(Body, null, "text/html");
                LinkedResource pic2 = new LinkedResource(signatureFilePath, MediaTypeNames.Image.Jpeg);
                pic2.ContentId = "Signature";
                htmlView.LinkedResources.Add(pic2);
            }
            else   //if signauture image is not avaiable then just add regards line and then create htmlview
            {
                Body += "<br/><br/>Best Regards,<br/>Marketing Team<br/>RedDotDistribution<br/>";
                htmlView = System.Net.Mail.AlternateView.CreateAlternateViewFromString(Body, null, "text/html");
                //objMailMessage.Body = Body;
            }


            if (AttachmentFilePath != "" && File.Exists(AttachmentFilePath)) ////Image path will be available only in case of mailshot , but not in excel case
            {
                if (pAttachFileExt == ".jpg" || pAttachFileExt == ".jpeg" || pAttachFileExt == ".png" || pAttachFileExt == ".bmp")
                {
                    LinkedResource pic1 = new LinkedResource(AttachmentFilePath, MediaTypeNames.Image.Jpeg);
                    pic1.ContentId = "MailShot";
                    htmlView.LinkedResources.Add(pic1);
                }
                else  //just simply attach the file   if (pAttachFileExt == ".xls" || pAttachFileExt == ".xlsx" || pAttachFileExt == ".xlsm")
                {
                    Attachment atchFile = new Attachment(AttachmentFilePath);
                    objMailMessage.Attachments.Add(atchFile);
                }
            }

            objMailMessage.AlternateViews.Add(htmlView);

            /////////////////////////////////////////////// new code ends /////////////////////////////////////////////////////////////////////

            //if (AttachmentFilePath.Trim() != "")
            //{
            //    Attachment atchFile = new Attachment(AttachmentFilePath);
            //    objMailMessage.Attachments.Add(atchFile);
            //}

            objSMTPClient.Send(objMailMessage);//open later sending mails  

            strMessage = myGlobal.getSystemConfigValue("mailSuccessMsg");
        }
        catch (Exception Ex)
        {
            strMessage = "Cannot send mail ,  " + Ex.Message;
            //Console.WriteLine("Cannot send msg: {0}", strMessage);
        }
        return strMessage;
    }
    
    public static string SendMultipleAttach(string From, string To, string cc, string Subject, string Body, bool IsHtmlBody, string AttachmentFilePath, string fls)
    {
        try
        {
            System.Net.NetworkCredential objNetworkCredential;
            SmtpClient objSMTPClient;

            string strHostName = myGlobal.getSystemConfigValue("smtpHost");//"smtp.gmail.com";
            string strPort = myGlobal.getSystemConfigValue("smtpPort");//"587";
            string strUserName = myGlobal.getSystemConfigValue("smtpUserId");//"minhases@gmail.com";
            string strPassword = myGlobal.getSystemConfigValue("smtpPassword");//"tejgrace";

            if (!string.IsNullOrEmpty(strHostName) && !string.IsNullOrEmpty(strPort))
            {
                objSMTPClient = new SmtpClient(strHostName, Convert.ToInt32(strPort));
            }
            else
            {
                objSMTPClient = new SmtpClient(strHostName);
            }
            objSMTPClient.EnableSsl = false;
            objSMTPClient.UseDefaultCredentials = false;

            if (!string.IsNullOrEmpty(strUserName) && !string.IsNullOrEmpty(strPassword))
            {
                objNetworkCredential = new System.Net.NetworkCredential(strUserName, strPassword);
                objSMTPClient.Credentials = objNetworkCredential;
            }

            MailMessage objMailMessage = new MailMessage();
            MailAddress objMailAdress = new MailAddress(From);
            objMailMessage.IsBodyHtml = IsHtmlBody;

            objMailMessage.From = objMailAdress;


            //To = "vishav@eternatec.com";//;gulamabbas@reddotdistribution.com"; //vishav close this later
            //cc = "";// vishav close this later

            string[] arrTO = To.Split(';');
            for (int i = 0; i < arrTO.Length; i++)
            {
                if (arrTO[i].Trim() != "")
                objMailMessage.To.Add(arrTO[i]);
            }

            string[] arrCC = cc.Split(';');
            for (int i = 0; i < arrCC.Length; i++)
            {
                if (arrCC[i].Trim()!= "")
                objMailMessage.CC.Add(arrCC[i]);
            }

            //objMailMessage.To.Add(To);
            //objMailMessage.To.Add(cc);

            objMailMessage.Subject = Subject;
            objMailMessage.Body = Body;

            //if (System.IO.Directory.Exists(AttachmentFilePath))
            //{
            //    System.IO.DirectoryInfo di = new System.IO.DirectoryInfo(AttachmentFilePath);
            //    System.IO.FileInfo[] rgFiles = di.GetFiles("*.*");
            //    foreach (System.IO.FileInfo fi in rgFiles)
            //    {
            //        Attachment attachFile = new Attachment(AttachmentFilePath + fi.Name);
            //        objMailMessage.Attachments.Add(attachFile);
            //    }
            //}

            if (fls.Trim() != "")
            {
                string[] sfsl = fls.Split(';');
                for (int i = 0; i < sfsl.Length; i++)
                {
                    if (sfsl[i].Trim() != "")
                    {
                        Attachment attachFile = new Attachment(AttachmentFilePath + sfsl[i]);
                        objMailMessage.Attachments.Add(attachFile);
                    }
                }
            }

           objSMTPClient.Send(objMailMessage); 

            strMessage = myGlobal.getSystemConfigValue("mailSuccessMsg");
            objSMTPClient.Dispose();
        }
        catch (Exception Ex)
        {
            strMessage = "Cannot send mail ,  " + Ex.Message;
            //Console.WriteLine("Cannot send msg: {0}", strMessage);
        }
        return strMessage;
    }

    /// <summary>
    /// Sending Mail Function...
    /// </summary>
    /// <param name="From">Email From address.</param>
    /// <param name="To">Email To address.</param>
    /// <param name="Subject">Email Subject</param>
    /// <param name="Message">Email Body</param>
    /// <param name="IsHtmlBody">Is Html Body ?</param>
    /// <param name="AttachmentFilePath">Path of attached file name</param>
    public static string Send(string From, string To, string Subject, string Body, bool IsHtmlBody, string AttachmentFilePath)
    {
        DataTable dtMailSettings = new DataTable();
        SqlConnection connMail; SqlCommand cmdMail; System.Net.NetworkCredential objNetworkCredencial;

        dtMailSettings = GetMailSettings();

        if (dtMailSettings.Rows.Count > 0)
        {
            SmtpClient objSMTPClient;

            string strHostName = dtMailSettings.Rows[0]["Email_Host_Name"].ToString();
            string strPort = dtMailSettings.Rows[0]["Email_Port_No"].ToString();

            string strUserName = dtMailSettings.Rows[0]["Email_User_Name"].ToString();
            string strPassword = dtMailSettings.Rows[0]["Email_Password"].ToString();

            if (!string.IsNullOrEmpty(strPort))
            {
                objSMTPClient = new SmtpClient(strHostName, Convert.ToInt32(strPort));
            }
            else
            {
                objSMTPClient = new SmtpClient(strHostName);
            }

            if (!string.IsNullOrEmpty(strUserName) && !string.IsNullOrEmpty(strPassword))
            {
                objNetworkCredencial = new System.Net.NetworkCredential(strUserName, strPassword);
                objSMTPClient.Credentials = objNetworkCredencial;
            }

            MailMessage objMailMessage = new MailMessage();
            MailAddress objMailAdress = new MailAddress(From);
            objMailMessage.IsBodyHtml = IsHtmlBody;

            objMailMessage.From = objMailAdress;
            objMailMessage.To.Add(To);
            objMailMessage.Subject = Subject;
            objMailMessage.Body = Body;

            Attachment atchFile = new Attachment(AttachmentFilePath);
            objMailMessage.Attachments.Add(atchFile);
            try
            {
               objSMTPClient.Send(objMailMessage); 

                strMessage = myGlobal.getSystemConfigValue("mailSuccessMsg");
            }
            catch (Exception Ex)
            {
                strMessage = "Cannot send mail ,  " + Ex.Message;
                //Console.WriteLine("Cannot send msg: {0}", strMessage);
            }

        }
        return strMessage;
    }

    public static void SendMailToAll(string FromAddress, string Subject, string Body)
    {
        string strHostName = "", strUserName = "", strPassword = "", strMessage = "", strQuery = "";

        SqlConnection connMail; SqlCommand cmdMail;

        MailMessage objMailMessage = new MailMessage();
        MailAddress objMailAdress = new MailAddress(FromAddress);
        objMailMessage.From = objMailAdress;

        objMailMessage.Subject = Subject;
        objMailMessage.Body = Body;

        using (connMail = new SqlConnection(ConfigurationManager.ConnectionStrings["dbEcommerceConnectionString"].ConnectionString))
        {
            strQuery = "SELECT MAIL_SETTINGS_HOST_NAME,MAIL_SETTINGS_USER_NAME,MAIL_SETTINGS_PASSWORD FROM ECS_MAIL_SETTINGS";
            using (cmdMail = new SqlCommand(strQuery, connMail))
            {
                try
                {
                    SqlDataReader sdrMail;
                    cmdMail.Connection.Open();
                    sdrMail = cmdMail.ExecuteReader();
                    if (sdrMail.Read())
                    {
                        if (!string.IsNullOrEmpty(sdrMail["MAIL_SETTINGS_HOST_NAME"].ToString()))
                            strHostName = sdrMail["MAIL_SETTINGS_HOST_NAME"].ToString();
                        if (!string.IsNullOrEmpty(sdrMail["MAIL_SETTINGS_USER_NAME"].ToString()))
                            strUserName = sdrMail["MAIL_SETTINGS_USER_NAME"].ToString();
                        if (!string.IsNullOrEmpty(sdrMail["MAIL_SETTINGS_PASSWORD"].ToString()))
                            strPassword = sdrMail["MAIL_SETTINGS_PASSWORD"].ToString();
                    }
                    else
                    {
                        strMessage = "Mail settings not found, Please cantact to admin";
                    }
                }
                catch (Exception Ex)
                {
                    throw new Exception(Ex.Message);
                }
                finally
                {
                    cmdMail.Connection.Close();
                }
            }

            if (!string.IsNullOrEmpty(strHostName) && !string.IsNullOrEmpty(strUserName) && !string.IsNullOrEmpty(strPassword))
            {
                SmtpClient objSMTPClient = new SmtpClient(strHostName);
                System.Net.NetworkCredential objNetworkCredencial = new System.Net.NetworkCredential(strUserName, strPassword);
                objSMTPClient.Credentials = objNetworkCredencial;
                objMailMessage.IsBodyHtml = true;

                strQuery = "SELECT EMAIL_REC_EMAIL FROM ECS_EMAIL_RECEIPIENTS";
                using (cmdMail = new SqlCommand(strQuery, connMail))
                {
                    try
                    {
                        SqlDataReader sdrMail;
                        cmdMail.Connection.Open();
                        sdrMail = cmdMail.ExecuteReader();
                        while (sdrMail.Read())
                        {
                            objMailMessage.To.Add(sdrMail["EMAIL_REC_EMAIL"].ToString());
                            objSMTPClient.Send(objMailMessage); 
                        }
                    }
                    catch (Exception Ex)
                    {
                        throw new Exception(Ex.Message);
                    }
                    finally
                    {
                        cmdMail.Connection.Close();
                    }
                }
            }
        }
    }
}
