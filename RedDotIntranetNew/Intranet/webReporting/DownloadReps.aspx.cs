using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.IO;
using System.Data;
using System.Data.SqlClient;
using System.Collections;

public partial class Intranet_webReporting_DownloadReps : System.Web.UI.Page
{
    string repType, siteUsed;

    protected void Page_Load(object sender, EventArgs e)
    {
        siteUsed = "Intranet";
        lblMsg.Text = "";
        lblMsg.ForeColor = System.Drawing.Color.Black;

        if (Request.QueryString["repType"] != null)
            repType = Request.QueryString["repType"].ToString();
        else
            repType = "";

        if (repType == "")
        {
            lblMsg.Text = "Sorry ! Invalid call to the page, can't process your request";
            return;
        }

        if (!IsPostBack)
        {
            try 
            {
                loadRolesAndGrantedPermissions();
                bindGrid();

                if (lblCurrentReportBasePath.Text == "")
                    PanelFolderList.Visible = false;
                else
                {
                    PanelFolderList.Visible = true;
                    LoadFolderDDL();
                }
            }
            catch (Exception eee)
            {
                lblMsg.Text = "Error!  " + eee.ToString();
            }
        }
    }

    protected void RadWorkingUsers_SelectedIndexChanged(object sender, EventArgs e)
    {
        try
        {
            setRoleNUserlbls();
            bindGrid();

            if (lblCurrentReportBasePath.Text == "")
                PanelFolderList.Visible = false;
            else
            {
                PanelFolderList.Visible = true;
                LoadFolderDDL();
            }
        }
        catch (Exception eee)
        {
            lblMsg.Text = "Error!  " + eee.ToString();
        }
    }

    private void setRoleNUserlbls()
    {
        lblworkForUser.Text = RadWorkingUsers.SelectedItem.Text.ToUpper();  //user is at second position
        lblWorkingAs.Text = lblworkForUser.Text;
    }

    protected void loadRolesAndGrantedPermissions()
    {
        RadWorkingUsers.Items.Clear();

        RadWorkingUsers.Items.Add(myGlobal.loggedInUser().ToUpper());

        Db.constr = myGlobal.getIntranetDBConnectionString();
        DataTable dtbGrantedRoles = Db.myGetDS("select distinct(grantingUser) usr from dbo.rolesGranted where toUser='" + myGlobal.loggedInUser() + "'").Tables[0];

        //continues to add to existing list from ------- dbo.rolesGranted

        foreach (DataRow rw in dtbGrantedRoles.Rows)
        {
            RadWorkingUsers.Items.Add(rw["usr"].ToString().ToUpper());
        }

        RadWorkingUsers.SelectedIndex = 0;

        //now set workFor user and role lables
        setRoleNUserlbls();
    }

    protected void LoadFolderDDL()
    {
        List<string> lst = new List<string>();
        DirectoryInfo[] di = new DirectoryInfo(lblCurrentReportBasePath.Text).GetDirectories("*.*", SearchOption.AllDirectories);

        DateTime[] dtArr = new DateTime[di.Length];

        System.Globalization.DateTimeFormatInfo info = new System.Globalization.DateTimeFormatInfo();

        int j = 0;
        for (int i = 0; i < di.Length; i++)
        {
            try
            {
                dtArr[j] = DateTime.Parse(di[i].Name, info);
                j = j + 1;
            }
            catch (Exception eee)
            {
                dtArr[j] = DateTime.Parse("01-01-2001", info);
                j = j + 1;
                lblMsg.Text = "Warning !  Folder Name '" + di[i].Name + "' is not compatible name, thus ignored. ";
            }
        }

        Array.Sort(dtArr);
        Array.Reverse(dtArr);

        for (int i = 0; i < di.Length; i++)
        {
            if (dtArr[i].ToString("MM-dd-yyyy") != "01-01-2001")
              lst.Add(dtArr[i].ToString("MM-dd-yyyy"));
        }

        ddlFoldersList.DataSource = lst;
        ddlFoldersList.DataBind();

        
        lblnoOfReps.Text = ddlFoldersList.Items.Count.ToString();
    }

    protected void Old_LoadFolderDDL()
    {
        List<string> lst = new List<string>();
        DirectoryInfo[] di = new DirectoryInfo(lblCurrentReportBasePath.Text).GetDirectories("*.*", SearchOption.AllDirectories);
        for (int i = 0; i < di.Length; i++)
        {
            lst.Add(di[i].Name);
        }

        ////test values
        //lst.Add("08-17-2012");
        //lst.Add("08-13-2012");
        //lst.Add("08-15-2012");

        lst.Sort();
        lst.Reverse();

        ddlFoldersList.DataSource = lst;
        ddlFoldersList.DataBind();


        lblnoOfReps.Text = ddlFoldersList.Items.Count.ToString();
    }

    protected void bindGrid()
    {
        string sql;
        //sql = "select c.*,a.reportType,a.reportTitle,a.reportFilePath,b.BU,b.FileName from (Select * from tej.dbo.webReportsUserRights where upper(userName)='" + myGlobal.loggedInUser().ToUpper().ToString() + "' and fk_repTypeId=(select repTypeId from tej.dbo.webReportTypes where upper(reportType)='" + repType.ToUpper() + "')) as c join tej.dbo.webReportTypes as a on a.repTypeId=c.fk_repTypeId join tej.dbo.webReportTypesNBU as b on b.BUId=c.fk_BUId and b.fk_repTypeId=c.fk_repTypeId";
        sql = "select c.*,a.reportType,a.reportTitle,a.reportFilePath,b.BU,b.FileName,a.intimationMailId,a.sendDownloadIntimation,a.sendUploadIntimation from (Select * from tej.dbo.webReportsUserRights where upper(userName)='" + lblworkForUser.Text + "' and fk_repTypeId=(select repTypeId from tej.dbo.webReportTypes where upper(reportType)='" + repType.ToUpper() + "')) as c join tej.dbo.webReportTypes as a on a.repTypeId=c.fk_repTypeId join tej.dbo.webReportTypesNBU as b on b.BUId=c.fk_BUId and b.fk_repTypeId=c.fk_repTypeId";
        Db.constr = myGlobal.getConnectionStringForDB("TZ");
        DataSet ds;
        ds = Db.myGetDS(sql);
        grdWeeklyFiles.DataSource = ds.Tables[0];
        grdWeeklyFiles.DataBind();

        if (ds.Tables[0].Rows.Count == 0)
        {
            lblCurrentReportBasePath.Text = "";
            lblTitle.Text = "";

            lblMsg.Text = "Sorry ! No Download available/permissions denied to current logged in user, for selected report category. Contact Report Admin for permissions for the same";
            lblMsg.ForeColor = System.Drawing.Color.Red;
            return;
        }
        else
        {
            lblCurrentReportBasePath.Text = ds.Tables[0].Rows[0]["reportFilePath"].ToString();
            lblTitle.Text = ds.Tables[0].Rows[0]["reportTitle"].ToString() + " Downloads";
        }
        
    }

    protected void btnDownload_Click(object sender, EventArgs e)
    {
        lblMsg.Text = "";

        if (ddlFoldersList.SelectedIndex < 0)
        {
            lblMsg.Text = "Error ! file to be downloaded is not available currently, Please try later";
            lblMsg.ForeColor = System.Drawing.Color.Red;
            return;
        }

        string fullPath;
        
        ImageButton btn = sender as ImageButton;

        Label lblFile, lblPth, lblBU0, lblintimationMailId, lblsendDownloadIntimation, lblsendUploadIntimation;
        Control ctrl;
        foreach (GridViewRow row in grdWeeklyFiles.Rows)
        {
             ctrl = row.FindControl("btnDownload") as ImageButton;
             lblFile = row.FindControl("lblFileName") as Label;
             lblPth = row.FindControl("lblPth") as Label;
             lblBU0 = row.FindControl("lblBU") as Label;
             lblintimationMailId = row.FindControl("lblintimationMailId") as Label;
             lblsendDownloadIntimation = row.FindControl("lblsendDownloadIntimation") as Label;
             lblsendUploadIntimation = row.FindControl("lblsendUploadIntimation") as Label;

            if (ctrl != null)
            {
                ImageButton btn1 = (ImageButton)ctrl;
                if (btn.ClientID == btn1.ClientID)
                {
                    //fullPath = lblPth.Text + lblFile.Text;

                    fullPath = lblPth.Text + ddlFoldersList.SelectedItem.Text + @"\" + lblFile.Text;
                    lblDwnldFilePathnow.Text = fullPath;
                    if (System.IO.File.Exists(fullPath))
                        DownloadFileNow(fullPath, lblFile.Text, lblBU0.Text, ddlFoldersList.SelectedItem.Text, repType, lblintimationMailId.Text, lblsendDownloadIntimation.Text);
                    else
                    {
                        lblMsg.Text = "Error ! file to be downloaded is not available currently, Please try later";
                        lblMsg.ForeColor = System.Drawing.Color.Red;
                        break;
                    }
                }
            }
        }           
    }
   
    public static string MimeType(string Extension)
    {
        string mime = "application/octetstream";
        if (string.IsNullOrEmpty(Extension))
            return mime;
        string ext = Extension.ToLower();
        Microsoft.Win32.RegistryKey rk = Microsoft.Win32.Registry.ClassesRoot.OpenSubKey(ext);
        if (rk != null && rk.GetValue("Content Type") != null)
            mime = rk.GetValue("Content Type").ToString();
        return mime;
    }

    protected void grdWeeklyFiles_RowDataBound(object sender, GridViewRowEventArgs e)
    {
        if (e.Row.RowType == DataControlRowType.DataRow)
        {
            ImageButton tmpBtn = (ImageButton)e.Row.FindControl("btnDownload");
            tmpBtn.Attributes.Add("onmouseover","btnOverEffect("+ (e.Row.RowIndex).ToString() +");");
            tmpBtn.Attributes.Add("onmouseout", "btnOutEffect(" + (e.Row.RowIndex).ToString() + ");");
        }
    }

    protected void DownloadFileNowORG(string strfile)
    {
        //string fName = Server.MapPath(strfile); //use this when receiving virtual path
        string fName = strfile;         //use this when receiving physical path

        FileInfo fi = new FileInfo(fName);
        long sz = fi.Length;

        Response.ClearContent();
        Response.ContentType = MimeType(Path.GetExtension(fName));
        Response.AddHeader("Content-Disposition", string.Format("attachment; filename = {0}", System.IO.Path.GetFileName(fName)));
        Response.AddHeader("Content-Length", sz.ToString("F0"));
        Response.TransmitFile(fName);
        Response.End();

    }

    protected void DownloadFileNow(string pFilewithFullPth,string pfln,string pBU,string pRepForDate,string pRepType,string pIntimationMailId,string TrackRecordOrNot)
    {
        //string fName = Server.MapPath(strfile); //use this when receiving virtual path
        string fName = pFilewithFullPth;         //use this when receiving physical path

        FileInfo fi = new FileInfo(fName);
        long sz = fi.Length;

        Response.ClearContent();
        Response.ContentType = MimeType(Path.GetExtension(fName));

        /////////////////////update table here////////////////////////
        if (TrackRecordOrNot == "1")
        {
            string psql;
            psql = string.Format("insert into reportTrackDownloads(reportType,BU,reportForDate,fileFullPthName,fileNameOnly,ByUser,intimationMailId,intimationMailSent,siteUsed) values('{0}','{1}','{2}','{3}','{4}','{5}','{6}',{7},'{8}') ", pRepType, pBU, pRepForDate, pFilewithFullPth, pfln, myGlobal.loggedInUser().ToUpper(), pIntimationMailId, "1",siteUsed);
            Db.constr = myGlobal.getIntranetDBConnectionString();
            //Db.myExecuteSQL(psql);
            long newId = Db.myExecuteSQLReturnLatestAutoID(psql);

            string strtmp = "", mailList;
            //strtmp = "<br/>Report Downloaded from Intranet Site, find the details below<br/><br/> ";

            strtmp = "<br/> Please find download details below <br/><br/>";
            strtmp += "Report      <b>     : " + pRepType + " </b><br/>";
            strtmp += "BU          <b>     : " + pBU + " </b><br/>";
            strtmp += "Report For Date <b> : " + pRepForDate + "</b><br/>";
            strtmp += "File Name <b>       : " + pfln + "</b><br/>";
            strtmp += "Downloaded By <b>   : " + myGlobal.loggedInUser().ToUpper() + "</b><br/>";
            strtmp += "Downloaded time <b>   : " + DateTime.Now.ToString() + "</b><br/>";

            strtmp += "<br/><br/>Best Regards,<br/>Red Dot Distribution";

            mailList = pIntimationMailId;
            //strtmp += "<br/><br/>In Actuall Mails will be sent to : " + mailList;
            //mailList = "vishav@eternatec.com"; //actual comes as parameter 
            //mailList = "victor@eternatec.com";
            try
            {
                string msg = Mail.SendMultipleAttach(myGlobal.getSystemConfigValue("websiteEmailer"), mailList, "", myGlobal.loggedInUser().ToUpper() + " Downloaded a report from " + siteUsed + " Site", strtmp, true, "", "");
                string dbMsg = myGlobal.getSystemConfigValue("mailSuccessMsg");

                if (msg != dbMsg)  //if mail fails then only it updates to 0
                {
                    psql = "update reportTrackDownloads set intimationMailSent=0 where id=" + newId.ToString();
                    Db.constr = myGlobal.getIntranetDBConnectionString();
                    Db.myExecuteSQL(psql);
                }
            }
            catch (Exception exp)
            {
                //nothing
            }
        }
                
        /////////////////////////////////////////////////////////////
        Response.AddHeader("Content-Disposition", string.Format("attachment; filename = {0}", System.IO.Path.GetFileName(fName)));
        Response.AddHeader("Content-Length", sz.ToString("F0"));
        Response.TransmitFile(fName);
        Response.End();

    }

    

}