using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Text.RegularExpressions;
using System.Data;
using System.Data.SqlClient;
using System.Web.Security;

public partial class Intranet_Reporting_SubmitReports : System.Web.UI.Page
{
    string siteUsed;
    protected void Page_Load(object sender, EventArgs e)
    {
        siteUsed = "Intranet";

        if (!IsPostBack)
        {
            setFieldsDefult();
            tblMain.Visible = true;
        }

    }

    protected void btnReport_Click(object sender, EventArgs e)
    {
        String pth, dirPhyPth, flsavePth, cmts, repName;

        lblMsg.Text = "";

        if (txtRepName.Text.Trim()=="")
        {
            lblMsg.Text = "Error ! Report Name field can't be empty. ";
            return;
        }

        if (txtRepName.Text.Trim().IndexOf("'") >= 0)
        {
            lblMsg.Text = "Invalid Character occurs ' in field Report Name , Char( ' ) not supported.";
            return;
        }

        repName = txtRepName.Text.Trim();

        if (!Util.IsValidDate(txtDate.Text))
        {
            lblMsg.Text = "Invalid Date in Date Filed, Kindly correct and retry.";
            return;
        }

        cmts = "No Comments";

        if (txtComments.Text.Trim() != "")
        {
            if (txtComments.Text.Trim().IndexOf("'") >= 0)
            {
                lblMsg.Text = "Invalid Character occurs ' in field Comments , Char( ' ) not supported.";
                return;
            }
            cmts = txtComments.Text.Trim().Replace("'", "");
        }

        if (!FileUpload1.HasFile)
        {
            lblMsg.Text = "It's Mandatory to select a file of format (PDF or WORD or EXCEL) to upload. ";
            return;
        }

        pth = myGlobal.getAppSettingsDataForKey("uploadReportsFolderPath");  //we have direct physical path

       // dirPhyPth = pth; //Server.MapPath("~" + pth);

        //if (!System.IO.Directory.Exists(dirPhyPth))
        //{
        //    System.IO.Directory.CreateDirectory(dirPhyPth);
        //}

        if (FileUpload1.HasFile)
        {
            int pikMaxFileName = myGlobal.trimFileLength;
            string[] PhotoTitle;
            string PhotoTitlename, trimmedNameWithExt;

            PhotoTitle = FileUpload1.FileName.Split('.');
            PhotoTitlename = PhotoTitle[0];

            if (PhotoTitlename.Length > pikMaxFileName)
                PhotoTitlename = PhotoTitlename.Substring(0, pikMaxFileName);

            trimmedNameWithExt = PhotoTitlename + "-" + myGlobal.loggedInUser() + "-" + DateTime.Now.ToString("dd-MM-yyyy-hh-mm-ss") + "." + PhotoTitle[1];

            if (PhotoTitle[1].ToLower() == "pdf" || PhotoTitle[1].ToLower() == "xls" || PhotoTitle[1].ToLower() == "xlsx" || PhotoTitle[1].ToLower() == "xlsm" || PhotoTitle[1].ToLower() == "doc" || PhotoTitle[1].ToLower() == "docx")
            {
                //flsavePth = Server.MapPath("~" + pth) + trimmedNameWithExt;
                flsavePth = pth + trimmedNameWithExt; 
                FileUpload1.SaveAs(flsavePth);
                updateDBNMail(repName, txtDate.Text.Trim(), cmts, flsavePth, trimmedNameWithExt, myGlobal.getAppSettingsDataForKey("uploadReportsIntimationMailId"));
                lblMsg.Text = "File uploaded successfully.";
            }
            else
            { 
              lblMsg.Text = "Invalid file type not supported. only supports file of format ( PDF or WORD or EXCEL) .";
                return;
            }
        }

    }

    private void updateDBNMail(string pRepNm, string pRepForDate, string pCmts, string pFilewithFullPth, string pfln, string pIntimationMailId)
    {
        string psql;
        psql = string.Format("insert into reportTrackUploads(reportType,BU,reportForDate,comments,fileFullPthName,fileNameOnly,ByUser,intimationMailId,intimationMailSent,siteUsed) values('{0}','{1}','{2}','{3}','{4}','{5}','{6}','{7}',{8},'{9}') ", pRepNm, "NA", pRepForDate, pCmts, pFilewithFullPth, pfln, myGlobal.loggedInUser().ToUpper(), pIntimationMailId, "1", siteUsed);
        Db.constr = myGlobal.getIntranetDBConnectionString();
        //Db.myExecuteSQL(psql);
        long newId = Db.myExecuteSQLReturnLatestAutoID(psql);

        string strtmp = "", mailList;
        strtmp = "<br/>Please find Upload details below <br/><br/>";
        strtmp += "Report Name    <b>     : " + pRepNm + " </b><br/>";
        strtmp += "Report For Date <b> : " + pRepForDate + "</b><br/>";
        strtmp += "File Name <b>       : " + pfln + "</b><br/>";
        strtmp += "Comments : <b> : " + pCmts + "</b><br/>";
        
        strtmp += "Uploaded By <b>   : " + myGlobal.loggedInUser().ToUpper() + "</b><br/>";
        strtmp += "Uploaded time <b>   : " + DateTime.Now.ToString() + "</b><br/>";

        strtmp += "<br/><br/>Best Regards,<br/>Red Dot Distribution";

        mailList = pIntimationMailId;  //picked up local or live from webconfig

        try
        {
            string msg = Mail.SendMultipleAttach(myGlobal.getSystemConfigValue("websiteEmailer"), mailList, "", myGlobal.loggedInUser().ToUpper() + " Uploaded Report on " + siteUsed + " Site", strtmp, true, "", pFilewithFullPth);
            string dbMsg = myGlobal.getSystemConfigValue("mailSuccessMsg");

            if (msg != dbMsg)  //if mail fails then only it updates to 0
            {
                psql = "update reportTrackUploads set intimationMailSent=0 where id=" + newId.ToString();
                Db.constr = myGlobal.getIntranetDBConnectionString();
                Db.myExecuteSQL(psql);
            }

            setFieldsDefult();
        }
        catch (Exception exp)
        {
            //nothing
        }

    }

    private void setFieldsDefult()
    {
        txtDate.Text = DateTime.Now.ToString("MM/dd/yyyy");
        txtRepName.Text = "";
        txtComments.Text = "";
    }
    
}