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
using System.IO;
using System.Data;
public partial class Sendmail : System.Web.UI.Page
{
    MailShot mailShot = new MailShot();
    clsEmail SendMail = new clsEmail();
    protected void Page_Load(object sender, EventArgs e)
    {
        if (!IsPostBack)
        {
            txtCountry.Text = "";// Session[RunningCache.Country].ToString();
            bindbu();
        }
    }
    protected void bindbu()
    {
       
        ddlBu.DataSource = mailShot.getbu();
        ddlBu.DataValueField = "buid";
        ddlBu.DataTextField = "bu";
        ddlBu.DataBind();
    }
    protected void btnSend_Click(object sender, EventArgs e)
    {
        
        String strGuid = "";
        String strPicExtension = "";
        String strFileName = "";


        //  Firstly upload the picture associated with the news

        try
        {


            // Add the record to the database
            if (FileUpload1.HasFile)
            {
                strGuid = fnGuid();
                strPicExtension = fnGetPictureExtension(FileUpload1.FileName);
                strFileName = strGuid + strPicExtension;
                //   Add picture to folder
                subAddNewPicture(FileUpload1, strFileName, "Uploads");
                String MailShoptPath = "~/Uploads/" + strFileName;

                mailShot.addmailShot(ddlBu.SelectedValue, txtSuject.Text, txttargetSale.Text, Session[RunningCache.CountryID].ToString (), Session[RunningCache.UserID ].ToString ());

               
                DataTable dtemail = mailShot.getEmail();
                String Email = "";
                foreach (DataRow row in dtemail.Rows)
                {
                    Email = row["email"].ToString().Trim();
                    trmsg.Visible = true;
                    lbmsg.Text = "Sending Email to :" + Email;


                    SmtpClient smtp = new SmtpClient();

                    string MailShopIc = Server.MapPath(MailShoptPath);
                    string Signature = Server.MapPath("~/EmailTemplates/signature.png");
                    string MailTemplate = Server.MapPath("~/EmailTemplates/MailShot.htm");
                    string template = string.Empty;
                    if (File.Exists(MailTemplate))
                    {
                        template = File.ReadAllText(MailTemplate);
                    }


                    MailMessage msg = new MailMessage();
                    msg.Subject = txtSuject.Text;
                    SendMail.EmailConfig("samuel@eternatec.com", template, msg, Signature, MailShopIc);


                }
            }

            else
            {
                trmsg.Visible = true;
                lbmsg.Text = " Kindly Select the Mail Shot to be uploaded ";

            }
        }
        catch (Exception ex)
        {
            trError.Visible = true;
            lbmsgErr.Text = ex.Message;
        }


        }
  

            //   Function to generate a GUID using current date and time
    private string fnGuid()
    {
        string strGuid = "";

        try
        {
            var _with1 = DateTime.Now;
            strGuid = _with1.Year.ToString() + _with1.Month.ToString() + _with1.Day.ToString() + _with1.Hour.ToString() + _with1.Minute.ToString() + _with1.Second.ToString();
        }
        catch (Exception ex)
        {
            throw ex;
        }

        //   returning the guid generated
        return strGuid;
    }

    //   Returns true if file name has the extension .jpg, .gif, .jpeg
    private string fnGetPictureExtension(string strPictureName)
    {
        try
        {
            String _with1 = strPictureName.ToUpper();
            if (_with1.EndsWith(".JPG"))
            {
                return ".JPG";
            }
            else if (_with1.EndsWith(".GIF"))
            {
                return ".GIF";
            }
            else if (_with1.EndsWith(".JPEG"))
            {
                return ".JPEG";
            }
            else if (_with1.EndsWith(".PNG"))
            {
                return ".PNG";
            }
        }
        catch (Exception ex)
        {
            throw ex;
        }

        // Else if has no extension so it returns ""
        return "";
    }

    //   Adds picture to specified folder
    private void subAddNewPicture(FileUpload FileUploader, string strPictureName, string strImgFolder)
    {
        string strImageFolderPath = null;
        string strImagePath = null;

        try
        {
            //   Construct saving path
            strImageFolderPath = Path.Combine(Request.PhysicalApplicationPath, strImgFolder);
            strImagePath = Path.Combine(strImageFolderPath, strPictureName);

            //   Upload image
            FileUploader.SaveAs(strImagePath);
        }
        catch (Exception ex)
        {
            throw ex;
        }
    }
   


    }
