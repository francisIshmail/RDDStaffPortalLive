using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Configuration;
using System.Data;
using System.Data.SqlClient;
using System.Collections;
using System.IO;

public partial class Intranet_EVO_PriceListManagement : System.Web.UI.Page
{
    string query;
    int mailSentCounter;

    protected void Page_Load(object sender, EventArgs e)
    {
        lblError.Text = "";

        if (!Page.IsPostBack)
        {
            Loaddefaults();
            bindDDLCountry();
            BindGrid();
        }
    }
    
    private void Loaddefaults()
    {
        txtMsg.Text = "";
        txtSubject.Text = "";
        lblFilePth.Text = "";

        //if (ddlCountry.Items.Count > 0)
        //    ddlCountry.SelectedIndex = 0;
    }

    private void bindDDLCountry()
    {
        Db.LoadDDLsWithCon(ddlCountry, "select CountryID,Country from dbo.tblCountry order by country", "Country", "CountryID", myGlobal.getRDDMarketingMailsDBConnectionString());
        lblCntryCnt.Text = ddlCountry.Items.Count.ToString();
        
        //loads country and intimationmailids
        Db.LoadDDLsWithCon(ddlCountryIntimationMailId, "select CountryID,intimationMailIds from dbo.tblCountry order by country", "intimationMailIds", "CountryID", myGlobal.getRDDMarketingMailsDBConnectionString());
    }

    protected void btnSave_Click(object sender, EventArgs e)
    {
        long aid = 0;

        lblFilePth.Text = "";

        if (lstEmails.Items.Count <= 0)
        {
            lblError.Text = "Error ! No dealer/email id's available for sending Price List mails for the selected country";
            return;
        }

        if (ddlCountry.SelectedIndex < 0)
        {
            lblError.Text = "Error ! No country selected for the dealer.";
            return;
        }

        if (txtSubject.Text.Trim() == "")
        {
            lblError.Text = "Error ! Subject filed can not be empty, Please supply a value and then save (Max 250 Chars).";
            return;
        }

        txtSubject.Text = txtSubject.Text.Trim().Replace("'", "''");

        if (txtMsg.Text.Trim() == "")
        {
            lblError.Text = "Error ! Message filed can not be empty, Please supply a value and then save (Max 4000 Chars).";
            return;
        }

        txtMsg.Text = txtMsg.Text.Trim().Replace("'", "''");

        if (!FileUpload1.HasFile && lblFilePth.Text.Trim() == "")
        {
            lblError.Text = "It's Mandatory to upload a valid PriceList file (excel format only).  ";
            return;
        }

        string exxt = Path.GetExtension(FileUpload1.FileName);
        if (exxt == ".xls" || exxt == ".xlsx" || exxt == ".xlsm")
        {
            //Proceed 
        }
        else
        {
            lblError.Text = "Supports only (.xls  .xlsx .xlsm) formats, kindly retry with a valid file upload.  ";
            return;
        }

        String pth, dirPhyPth, flsavePth="";
        pth = "/DownloadsUploads/priceLists/";

        dirPhyPth = Server.MapPath("~" + pth);

        try
        {

            if (FileUpload1.HasFile)
            {
                flsavePth = dirPhyPth + DateTime.Now.ToString("MM-dd-yyyy hh-mm-ss") + "-" + FileUpload1.FileName;
                lblFilePth.Text = pth + DateTime.Now.ToString("MM-dd-yyyy hh-mm-ss") + "-" + FileUpload1.FileName;
                FileUpload1.SaveAs(flsavePth);
            }
        
            query = string.Format("insert into dbo.tblpricelist(fk_CountryID,Subject,Msg,filePath,byUserName)  values({0},'{1}','{2}','{3}','{4}')", ddlCountry.SelectedItem.Value.ToString(), txtSubject.Text, txtMsg.Text, lblFilePth.Text, myGlobal.loggedInUser());
            Db.constr = myGlobal.getRDDMarketingMailsDBConnectionString();
            aid=  Db.myExecuteSQLReturnLatestAutoID(query);    
            lblError.Text = "PriceList Successfully added to database ";

            if (sendPriceListMails(txtMsg.Text, txtSubject.Text, flsavePth, exxt, "Price List : "))  //finally send mails 
            {
                // update mail sending status in database

                query = string.Format("update dbo.tblpricelist set mailSentStatus='{0}',mailingDate='{1}' where pricelistID=" + aid.ToString(), "Sent", DateTime.Now.ToString());
                Db.constr = myGlobal.getRDDMarketingMailsDBConnectionString();
                Db.myExecuteSQL(query);

                lblError.Text += ", mails attempted to (" + mailSentCounter.ToString() + ") dealers.";
            }
            else
            {
                lblError.Text += " but mails could not be sent to dealers as of now , contact administrator for further query";
            }
            
                Loaddefaults();
                BindGrid();
        }
        catch (Exception exp)
        {
            lblError.Text = "Error Saving PriceList ! " + exp.Message;
        }
    }

    private Boolean sendPriceListMails(string pmsg, string psub, string ppth, string pAttachFileExt, string pTitleType)
    {
        Boolean sts = true;

        string htm = getHtmlContentPriceList();

        //send for all dealers in loop 
        string msg = "", nmailBcc = "";

        try
        {
            for (mailSentCounter = 0; mailSentCounter < lstEmails.Items.Count; mailSentCounter++)
            {
                if (nmailBcc == "")
                    nmailBcc = lstEmails.Items[mailSentCounter].Text;
                else
                    nmailBcc += ";" + lstEmails.Items[mailSentCounter].Text;
            }

            //add bcc mails to staff  
            if (nmailBcc == "")
                nmailBcc = ddlCountryIntimationMailId.SelectedItem.Text; //myGlobal.getAppSettingsDataForKey("MarketingCCIntimationMailId");
            else
                nmailBcc += ";" + ddlCountryIntimationMailId.SelectedItem.Text; //myGlobal.getAppSettingsDataForKey("MarketingCCIntimationMailId");

            //// overwrites all , hardcoded for testting just remove this line
            //nmailBcc = "vishav@eternatec.com;singhvishav_man@rediffmail.com"; 

            msg = Mail.SendSingleAttachMarketing("marketing@reddotdistribution.com", "", nmailBcc, (pTitleType + psub), htm, true, ppth, pAttachFileExt, Server.MapPath("~/DownloadsUploads/MarketingEmailTemplates/signature.png"));
            lblError.Text += msg;
        }
        catch (Exception exp)
        {
            sts = false;
        }

        return sts;
    }

    //private Boolean sendPriceListMailsOrg(string pmsg,string psub,string ppth,string pTitleType)
    //{
    //    Boolean sts = true;

    //    string htm = getHtmlContentPriceList();
        
    //    //send for all dealers in loop 
    //    string msg = "",nmail="";

    //    try
    //    {

    //        for (mailSentCounter = 0; mailSentCounter < lstEmails.Items.Count; mailSentCounter++)
    //        {
    //            nmail = lstEmails.Items[mailSentCounter].Text;
    //            //msg = Mail.SendSingleAttachMarketing("marketing@reddotdistribution.com", nmail, myGlobal.getAppSettingsDataForKey("MarketingCCIntimationMailId"), (pTitleType + psub), htm, true, ppth);
    //            msg = Mail.SendSingleAttachMarketing("marketing@reddotdistribution.com", nmail, ddlCountryIntimationMailId.SelectedItem.Text, (pTitleType + psub), htm, true, ppth, Server.MapPath("~/DownloadsUploads/MarketingEmailTemplates/signature.png"));
    //        }
    //    }
    //    catch (Exception exp)
    //    {
    //        sts = false;
    //    }
        
    //    return sts;
    //}

    private string getHtmlContentPriceList()
    {
        string strMsg, strCC="";

        string[] arrCC = ddlCountryIntimationMailId.SelectedItem.Text.Split(';');
        for (int i = 0; i < arrCC.Length; i++)
        {
            if (arrCC[i].Trim() != "")
            {
                if (strCC == "")
                    strCC = arrCC[i];
                else
                    strCC += " or " + arrCC[i];
            }
        }

        strMsg = "<br/><br/><b>Dear Resellers,</b>";
        strMsg += "<br/><br/>" + "Greetings for the Day!! <br/><br/>";

        strMsg += txtMsg.Text + " <br/><br/>";

        strMsg += "<b>Please find the" + " PriceList file attached..<b>" + "<br/><br/>";

        strMsg += "For more details, please contact " + strCC + " <br/><br/>";
        
        //strMsg += "<br/><br/>Best Regards,<br/>Marketing Team<br/>RedDotDistribution<br/>";
        
        //strMsg+="<img src='"+ Server.MapPath("~/DownloadsUploads/MarketingEmailTemplates/signature.png") +"' alt='RedDot'/>";
        strMsg += "<img src=\"cid:Signature\" alt=\"RedDotIntranet\"/>";
        return strMsg;
    }

    protected void btnCancel_Click(object sender, EventArgs e)
    {
        Loaddefaults();
        BindGrid();
    }


    protected void BindGrid()
    {
        String summarySQL;
        String sortExp = (String)ViewState["sortExpression"];
        String sortDir = (String)ViewState["sortDirection"];

        if (sortExp == null || sortExp == "")
        {
            summarySQL = "select C.Country,P.pricelistID,P.fk_CountryID,P.Subject,P.Msg,P.filepath,P.byUserName,P.ModifiedDate,P.mailSentStatus,P.mailingDate from dbo.tblpricelist P left join dbo.tblCountry C on P.fk_CountryID=C.CountryID where P.fk_CountryID=" + ddlCountry.SelectedValue.ToString() + " order by C.Country,P.ModifiedDate";  //colums are : DeptId,DeptName,LastUpdated
        }
        else
        {
            summarySQL = "select C.Country,P.pricelistID,P.fk_CountryID,P.Subject,P.Msg,P.filepath,P.byUserName,P.ModifiedDate,P.mailSentStatus,P.mailingDate from dbo.tblpricelist P left join dbo.tblCountry C on P.fk_CountryID=C.CountryID where P.fk_CountryID=" + ddlCountry.SelectedValue.ToString() + " order by " + sortExp + " " + sortDir;
        }
        Db.constr = myGlobal.getRDDMarketingMailsDBConnectionString();
        Grid1.DataSource = Db.myGetDS(summarySQL);
        Grid1.DataBind();

        lblListCnt.Text = Grid1.Rows.Count.ToString();

        getDealerCountforSelectedCountry();
    }

    protected void getDealerCountforSelectedCountry()
    {
        lblSelectedCntry.Text = ddlCountry.SelectedItem.Text;
        lblSelectedCntry1.Text = ddlCountry.SelectedItem.Text;

        Db.constr = myGlobal.getRDDMarketingMailsDBConnectionString();
        lstEmails.DataSource = Db.myGetDS("select DealerID,(Email1 + case Email2 when 'NA' then '' else ';'+ Email2 end) as mailIDs from dbo.tblDealer where fk_CountryID=" + ddlCountry.SelectedValue.ToString());
        lstEmails.DataTextField = "mailIDs";
        lstEmails.DataValueField = "DealerID";
        lstEmails.DataBind();
        lblDealerCnt.Text =  lstEmails.Items.Count.ToString();
        //int Dcnt = 0;
        //Dcnt = Db.myExecuteScalar("select COUNT(*) from dbo.tblDealer where fk_CountryID="+ddlCountry.SelectedValue.ToString());
        //lblDealerCnt.Text = Dcnt.ToString();
    }

    protected void Grid1_Sorting(object sender, GridViewSortEventArgs e)
    {
        String sortExp = e.SortExpression.ToString();
        String sortDir = e.SortDirection.ToString();

        String sortExpV = (String)ViewState["sortExpression"];
        String sortDirV = (String)ViewState["sortDirection"];

        if (sortExpV != null && sortExp == sortExpV)
        {
            if (sortDirV == "Asc")
                ViewState["sortDirection"] = "Desc";
            else
                ViewState["sortDirection"] = "Asc";
        }
        else
        {
            ViewState["sortExpression"] = sortExp;
            ViewState["sortDirection"] = "Asc";
        }

        BindGrid();
    }

    protected void Grid1_PageIndexChanging(object sender, GridViewPageEventArgs e)
    {
        Grid1.PageIndex = e.NewPageIndex;
        BindGrid();
    }

    protected void ddlCountry_SelectedIndexChanged(object sender, EventArgs e)
    {
        ListItem selectedListItem = ddlCountryIntimationMailId.Items.FindByValue(ddlCountry.SelectedValue.ToString());

        int tmpID = ddlCountryIntimationMailId.Items.IndexOf(selectedListItem);
        if (tmpID >= 0)
        {
            ddlCountryIntimationMailId.SelectedIndex = tmpID;
        }
        BindGrid();
    }
}