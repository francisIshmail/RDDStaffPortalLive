using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Net.Mail;
using System.Text;
using System.Threading.Tasks;

namespace RDDStaffPortal.DAL
{
    public class SendMail
    {
        private static string strMessage = string.Empty;

        /// <summary>
        /// This function is to send mail using default email reddotstaff@reeddotdistribution.com
        /// </summary>
        /// <param name="To">use semicolon(;) separator for multiple emails</param>
        /// <param name="CC">Optional Parameter</param>
        /// <param name="Subject"></param>
        /// <param name="Body"></param>
        /// <param name="IsHtmlBody"></param>
        /// <returns></returns>
        public static string Send(string To, string CC, string Subject, string Body, bool IsHtmlBody)
        {
            try
            {
                System.Net.NetworkCredential objNetworkCredential;
                SmtpClient objSMTPClient;

                string strHostName = Global.getAppSettingsDataForKey("smtphost");  /// "mail.cctz.co.tz";//"smtp.gmail.com";
                string strPort = Global.getAppSettingsDataForKey("smtpPort");//"587";

                string strUserName = Global.getAppSettingsDataForKey("smtpUserEmail");//"reddotstaff@reddotdistribution.com";
                string strPassword = Global.getAppSettingsDataForKey("smtpPassword");//"8Reddot1";

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
                MailAddress objMailAdress = new MailAddress(strUserName);
                objMailMessage.IsBodyHtml = IsHtmlBody;

                objMailMessage.From = objMailAdress;
                // objMailMessage.To.Add(To);

                string[] ToEmailArray = To.Split(';');
                for (int i = 0; i < ToEmailArray.Length; i++)
                {
                    if(!string.IsNullOrEmpty(ToEmailArray[i]))
                        objMailMessage.To.Add(ToEmailArray[i]);
                }

                if (!string.IsNullOrEmpty(CC))
                {
                    string[] CCEmailArray = CC.Split(';');
                    for (int j = 0; j < CCEmailArray.Length; j++)
                    {
                        if (!string.IsNullOrEmpty(CCEmailArray[j]))
                            objMailMessage.CC.Add(CCEmailArray[j]);
                    }
                }

                objMailMessage.Subject = Subject;
                objMailMessage.Body = Body;

                objSMTPClient.Send(objMailMessage);
                strMessage = "Mail Sent Succcessfully";
            }
            catch (Exception Ex)
            {
                strMessage = "Cannot send mail ,  " + Ex.Message;
            }
            return strMessage;
        }

        /// <summary>
        /// This function is used to send mail from specific email
        /// </summary>
        /// <param name="FromEmail"></param>
        /// <param name="password"></param>
        /// <param name="To">use semicolon(;) separator for multiple emails</param>
        /// <param name="CC">Optional Parameter</param>
        /// <param name="Subject"></param>
        /// <param name="Body"></param>
        /// <param name="IsHtmlBody"></param>
        /// <returns></returns>
        public static string Send(string FromEmail, string password, string To, string CC, string Subject, string Body, bool IsHtmlBody)
        {
            try
            {
                System.Net.NetworkCredential objNetworkCredential;
                SmtpClient objSMTPClient;

                string strHostName = Global.getAppSettingsDataForKey("smtphost");  /// "mail.cctz.co.tz";//"smtp.gmail.com";
                string strPort = Global.getAppSettingsDataForKey("smtpPort");//"587";

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

                if (!string.IsNullOrEmpty(FromEmail) && !string.IsNullOrEmpty(password))
                {
                    objNetworkCredential = new System.Net.NetworkCredential(FromEmail, password);
                    objSMTPClient.Credentials = objNetworkCredential;
                }

                MailMessage objMailMessage = new MailMessage();
                MailAddress objMailAdress = new MailAddress(FromEmail);
                objMailMessage.IsBodyHtml = IsHtmlBody;

                objMailMessage.From = objMailAdress;

                string[] ToEmailArray = To.Split(';');
                for (int i = 0; i < ToEmailArray.Length; i++)
                {
                    if (!string.IsNullOrEmpty(ToEmailArray[i]))
                        objMailMessage.To.Add(ToEmailArray[i]);
                }

                if (!string.IsNullOrEmpty(CC))
                {
                    string[] CCEmailArray = CC.Split(';');
                    for (int j = 0; j < CCEmailArray.Length; j++)
                    {
                        if (!string.IsNullOrEmpty(CCEmailArray[j]))
                            objMailMessage.CC.Add(CCEmailArray[j]);
                    }
                }
                

                objMailMessage.Subject = Subject;
                objMailMessage.Body = Body;

                objSMTPClient.Send(objMailMessage);
                strMessage = "Mail Sent Succcessfully";
            }
            catch (Exception Ex)
            {
                strMessage = "Cannot send mail ,  " + Ex.Message;
            }
            return strMessage;
        }

        /// <summary>
        /// This function is to send email with attachments [In case of multiple attachmant separate by '?' (Question mark) sign]
        /// </summary>
        /// <param name="To">use semicolon(;) separator for multiple emails</param>
        /// <param name="cc">Optional</param>
        /// <param name="Subject"></param>
        /// <param name="Body"></param>
        /// <param name="IsHtmlBody"></param>
        /// <param name="AttachmentFilePath">Seperate by ? (Question mark) in case of multiple file paths</param>
        /// <returns></returns>
        public static string SendMailWithAttachment(string To, string cc, string Subject, string Body, bool IsHtmlBody, Attachment at)
        {
            try
            {

                System.Net.NetworkCredential objNetworkCredential;
                SmtpClient objSMTPClient;

                string strHostName = Global.getAppSettingsDataForKey("smtphost");  /// "mail.cctz.co.tz";//"smtp.gmail.com";
                string strPort = Global.getAppSettingsDataForKey("smtpPort");//"587";

                string strUserName = Global.getAppSettingsDataForKey("smtpUserEmail");//"reddotstaff@reddotdistribution.com";
                string strPassword = Global.getAppSettingsDataForKey("smtpPassword");//"8Reddot1";

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
                MailAddress objMailAdress = new MailAddress(strUserName);
                objMailMessage.IsBodyHtml = IsHtmlBody;

                objMailMessage.From = objMailAdress;

                string[] ToEmailArray = To.Split(';');
                for (int i = 0; i < ToEmailArray.Length; i++)
                {
                    if (!string.IsNullOrEmpty(ToEmailArray[i]))
                        objMailMessage.To.Add(ToEmailArray[i]);
                }

                if (!string.IsNullOrEmpty(cc))
                {
                    string[] CCEmailArray = cc.Split(';');
                    for (int j = 0; j < CCEmailArray.Length; j++)
                    {
                        if (!string.IsNullOrEmpty(CCEmailArray[j]))
                            objMailMessage.CC.Add(CCEmailArray[j]);
                    }
                }
                //objMailMessage.Bcc.Add("pramod@reddotdistribution.com");

                objMailMessage.Subject = Subject;
                objMailMessage.Body = Body;
                //objMailMessage.Attachments.Add(AttachmentFilePath);

                
                objMailMessage.Attachments.Add(at);

                //if (AttachmentFilePath.Trim() != "")
                //{
                //    string[] sfslattachmnt = AttachmentFilePath.Split('?');
                //    for (int i = 0; i < sfslattachmnt.Length; i++)
                //    {
                //        if (sfslattachmnt[i].Trim() != "")
                //        {
                //            Attachment attachFile = new Attachment(sfslattachmnt[i]);
                //            objMailMessage.Attachments.Add(attachFile);
                //        }
                //    }
                //}
                //Attachment atchFile = new Attachment(AttachmentFilePath);
                //objMailMessage.Attachments.Add(atchFile);

                objSMTPClient.Send(objMailMessage);  //open later sending mails 

                strMessage = "Mail Sent Succcessfully";
            }
            catch (Exception Ex)
            {
                strMessage = "Cannot send mail ,  " + Ex.Message;
                //Console.WriteLine("Cannot send msg: {0}", strMessage);
            }
            return strMessage;
        }

        /// <summary>
        /// This function is to send email with attachments [In case of multiple attachmant separate by '?' (Question mark) sign]
        /// </summary>
        /// <param name="FromEmail"></param>
        /// <param name="Password"></param>
        /// <param name="To">use semicolon(;) separator for multiple emails</param>
        /// <param name="cc">Optional</param>
        /// <param name="Subject"></param>
        /// <param name="Body"></param>
        /// <param name="IsHtmlBody"></param>
        /// <param name="AttachmentFilePath">Seperate by ? (Question mark) in case of multiple file paths</param>
        /// <returns></returns>
        public static string SendMailWithAttachment(string FromEmail, string Password, string To, string cc, string Subject, string Body, bool IsHtmlBody, string AttachmentFilePath)
        {
            try
            {

                System.Net.NetworkCredential objNetworkCredential;
                SmtpClient objSMTPClient;

                string strHostName = Global.getAppSettingsDataForKey("smtphost");  /// "mail.cctz.co.tz";//"smtp.gmail.com";
                string strPort = Global.getAppSettingsDataForKey("smtpPort");//"587";

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

                if (!string.IsNullOrEmpty(FromEmail) && !string.IsNullOrEmpty(Password))
                {
                    objNetworkCredential = new System.Net.NetworkCredential(FromEmail, Password);
                    objSMTPClient.Credentials = objNetworkCredential;
                }

                MailMessage objMailMessage = new MailMessage();
                MailAddress objMailAdress = new MailAddress(FromEmail);
                objMailMessage.IsBodyHtml = IsHtmlBody;

                objMailMessage.From = objMailAdress;

                string[] ToEmailArray = To.Split(';');
                for (int i = 0; i < ToEmailArray.Length; i++)
                {
                    if (!string.IsNullOrEmpty(ToEmailArray[i]))
                        objMailMessage.To.Add(ToEmailArray[i]);
                }

                if (!string.IsNullOrEmpty(cc))
                {
                    string[] CCEmailArray = cc.Split(';');
                    for (int j = 0; j < CCEmailArray.Length; j++)
                    {
                        if (!string.IsNullOrEmpty(CCEmailArray[j]))
                            objMailMessage.CC.Add(CCEmailArray[j]);
                    }
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
                            Attachment attachFile = new Attachment(sfslattachmnt[i]);
                            objMailMessage.Attachments.Add(attachFile);
                        }
                    }
                }
                //Attachment atchFile = new Attachment(AttachmentFilePath);
                //objMailMessage.Attachments.Add(atchFile);

                objSMTPClient.Send(objMailMessage);  //open later sending mails 

                strMessage = "Mail Sent Succcessfully";
            }
            catch (Exception Ex)
            {
                strMessage = "Cannot send mail ,  " + Ex.Message;
                //Console.WriteLine("Cannot send msg: {0}", strMessage);
            }
            return strMessage;
        }

    }
}
