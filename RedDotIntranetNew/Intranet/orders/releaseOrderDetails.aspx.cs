using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Data;
using System.Data.SqlClient;
using System.Configuration;
using System.IO;


public partial class Intranet_orders_releaseOrderDetails : System.Web.UI.Page
{
    SqlDataReader dr;
    DataTable dt;
    string newStatusId, newescalateLevelId, sql, refId, pid, action, mailUrl, userEmail, mailList, fls, FilesForuploadTrack, strMessage;
    string[] userRole;
    string whereClauseRoleLine;

    protected void Page_Load(object sender, EventArgs e)
    {
        refId = Request.QueryString["oId"].ToString();
        pid = Request.QueryString["pid"].ToString();
        action = Request.QueryString["action"].ToString();

        lblOrderId.Text = refId.ToString();

        mailUrl = myGlobal.getSystemConfigValue("RedDotHostRootUrlIntra") + "Intranet/orders/releaseOrderDetails.aspx?oId=" + refId + "&pid=" + pid + "&action=task";

        userRole = myGlobal.loggedInRoleList();
        whereClauseRoleLine = "";
        for (int i = 0; i < userRole.Length; i++)
        {
            if (whereClauseRoleLine == "")
                whereClauseRoleLine = "'" + userRole[i] + "'";
            else
                whereClauseRoleLine += ",'" + userRole[i] + "'";
        }

        userEmail = myGlobal.membershipUserEmail(myGlobal.loggedInUser());

        if (!IsPostBack)
        {
            sql = "select * from dbo.processRequest where fk_escalateLevelId in (select roleId from dbo.roles where fk_deptId in (select autoindex from dbo.departments where upper(departmentName) in (" + whereClauseRoleLine + "))) and fk_processId=" + pid + " and refId=" + refId;
            Db.constr = myGlobal.getIntranetDBConnectionString();
            dt = Db.myGetDS(sql).Tables[0];

            if (action.ToUpper() == "VIEW")
            {
                tblTask.Visible = false;
                lblHeader.Text = "Viewable Release Order";
            }
            else
                if (action.ToUpper() == "TASK")
                {
                    if (dt.Rows.Count > 0)
                    {
                        tblTask.Visible = true;
                        lblHeader.Text = "Editable Release Order";
                    }
                    else
                    {
                        tblTask.Visible = false;
                        lblHeader.Text = "No order";
                    }
                }

            sql = "select * from dbo.releaseOrderLines where fk_roId=" + refId + "";
            Db.constr = myGlobal.getIntranetDBConnectionString();
            dt = Db.myGetDS(sql).Tables[0];
            gridDetails1.DataSource = dt;
            gridDetails1.DataBind();

            sql = "select * from dbo.releaseOrders where roId=" + refId + "";
            Db.constr = myGlobal.getIntranetDBConnectionString();
            dr = Db.myGetReader(sql);
            while (dr.Read())
            {
                lblRONumber.Text = dr["releaseOrdNo"].ToString();
                lblRODate.Text = dr["relOrdDate"].ToString();
                lblNameBillto.Text = dr["billToName"].ToString();
                lblNameShipto.Text = dr["shipToName"].ToString();
                lblNameConsignee.Text = dr["consigneeToName"].ToString();
                lblNameNotify.Text = dr["notifyToName"].ToString();

                lblAddBillto.Text = dr["billToAddress"].ToString();
                lblAddShipto.Text = dr["shipToAddress"].ToString();
                lblAddConsignee.Text = dr["consigneeToAddress"].ToString();
                lblAddNotify.Text = dr["notifyToAddress"].ToString();

                lblCityBillto.Text = dr["billToCity"].ToString();
                lblCityShipto.Text = dr["shipToCity"].ToString();
                lblCityConsignee.Text = dr["consigneeToCity"].ToString();
                lblCityNotify.Text = dr["notifyToCity"].ToString();

                lblCntryBillto.Text = dr["billToCountry"].ToString();
                lblCntryShipto.Text = dr["shipToCountry"].ToString();
                lblCntryConsignee.Text = dr["consigneeToCountry"].ToString();
                lblCntryNotify.Text = dr["notifyToCountry"].ToString();

                lblContactBillto.Text = dr["billToContact"].ToString();
                lblContactShipto.Text = dr["shipToContact"].ToString();
                lblContactConsignee.Text = dr["consigneeToContact"].ToString();
                lblContactNotify.Text = dr["notifyToContact"].ToString();

                //lblPhoneBillto.Text = dr[""].ToString();
                //lblPhoneShipto.Text = dr[""].ToString();
                //lblPhoneConsignee.Text = dr[""].ToString();
                //lblPhoneNotify.Text = dr[""].ToString();

                lblTotalQty.Text = dr["qty"].ToString();
                lblNetTotal.Text = dr["netTotal"].ToString();
                lblDiscount.Text = dr["discount"].ToString();
                lblGrandTotal.Text = dr["grandtotal"].ToString();

                lblCDCTick.Text = dr["tickCDC"].ToString();
                lblCDCDays.Text = dr["daysCDC"].ToString();

                lblPDCTick.Text = dr["tickPDC"].ToString();
                lblPDCDays.Text = dr["daysPDC"].ToString();

                lblCashTick.Text = dr["tickCash"].ToString();
                lblCashDays.Text = dr["daysCash"].ToString();

                lblCADTick.Text = dr["tickCAD"].ToString();
                lblCADDays.Text = dr["daysCAD"].ToString();

                lblCreditTick.Text = dr["tickCredit"].ToString();
                lblCreditDays.Text = dr["daysCredit"].ToString();

                lblDetailedDesc.Text = dr["termsDescription"].ToString();

                lblSpclShippingInst.Text = dr["specShippingIns"].ToString();
                lblSpecialPacking.Text = dr["specInstruction"].ToString();

                if(dr["shipmentModeID"].ToString()=="1")
                lblAirMOS.Text = "Yes";
                else
                    if (dr["shipmentModeID"].ToString() == "2")
                        lblLandMOS.Text = "Yes";
                        else
                        lblSeaMOS.Text = "Yes";

                if(dr["ordPacking"].ToString().ToUpper()=="NORMAL")
                lblNormalPacking.Text = dr["ordPacking"].ToString();
                else
                lblSpecialPacking.Text = dr["ordPacking"].ToString();

                lblExWorks.Text = dr["ordExWorks"].ToString();
                lblFOB.Text = dr["ordFOB"].ToString();
                lblCF.Text = dr["ordCF"].ToString();

                lblInspectionMand.Text = dr["ordInspecMandatory"].ToString();
                lblDocSubmission.Text = dr["ordDocSubmission"].ToString();
                lblChamberDocSub.Text = dr["ordBankSubmission"].ToString();

                lblAWB.Text = dr["ordAWB"].ToString();
                lblFinalInvoice.Text = dr["ordFinalInvoice"].ToString();
                lblCOO.Text = dr["ordCOO"].ToString();
                lblLoadList.Text = dr["ordPackList"].ToString();
            }

            sql = "select  p.fk_StatusId,p.fk_escalateLevelId,p.processRequestId,p.comments,p.ByUser,pd.processAbbr,s.processStatusName from dbo.processRequest as p join dbo.process_def as pd on p.fk_processId=pd.processId join dbo.processStatus as s on p.fk_StatusId=s.processStatusId and p.fk_processId=s.fk_processId where p.refId=" + refId + " and p.fk_processId=" + pid;
            Db.constr = myGlobal.getIntranetDBConnectionString();
            dr = Db.myGetReader(sql);
            while (dr.Read())
            {
                Session["currStatusId"] = dr["fk_StatusId"].ToString();
                Session["currescalateLevelId"] = dr["fk_escalateLevelId"].ToString();
                Session["processRequestId"] = dr["processRequestId"].ToString();

                Session["processAbbr"] = dr["processAbbr"].ToString();
                lblCurrStatus.Text = dr["processStatusName"].ToString();

                lblLatestComments.Text = dr["comments"].ToString();
                lblLatestUser.Text = dr["ByUser"].ToString();
            }
            dr.Close();

            //sql = "select role from dbo.orderEscalate where escalateLevelId=1"; //fetch first role for the process
            sql = "select departmentName from dbo.departments where autoindex=(select fk_deptId from dbo.roles where roleId in (select fk_roleid from dbo.processEscalate where escalateLevelId=1 and fk_processId=" + pid + "))"; //fetch first role for the process
            
            string rl="";

            dr = Db.myGetReader(sql);
            while (dr.Read())
            {
                rl = dr["departmentName"].ToString(); //basically a logger role
            }
            dr.Close();


            if (userRole.Contains(rl.ToUpper()))
            {
                btnCancel.Visible = true;
                btnAccept.Visible = false;
                btnDecline.Visible = false;
                btnEdit.Visible = false;
                btnConfirm.Visible = true;
                btnReimport.Visible = true;
            }

            if (Convert.ToInt32(Session["currStatusId"]) > 2)
            {
                btnDecline.Visible = false;
            }
            if (Convert.ToInt32(Session["currStatusId"]) == 0)
            {
                btnAccept.Visible = false;
                btnDecline.Visible = false;
            }
         }
    }

   
    private void processNow(string actionString)
    {
        if (actionString.ToLower() == "decline")
        {
            sql = "select prevprocessStatusId,prevRole from processStatus where processStatusId=" + Session["currStatusId"].ToString() + " and fk_processId= " + pid;
            Db.constr = myGlobal.getIntranetDBConnectionString();
            dr = Db.myGetReader(sql);
            if (dr.HasRows)
            {
                dr.Read();
                newStatusId = dr["prevprocessStatusId"].ToString();
                newescalateLevelId = dr["prevRole"].ToString();
                dr.Close();
            }
        }
        else
        {
            sql = "select nextprocessStatusId,nextRole from processStatus where processStatusId=" + Session["currStatusId"].ToString() + " and fk_processId= " + pid;
            Db.constr = myGlobal.getIntranetDBConnectionString();
            dr = Db.myGetReader(sql);
            if (dr.HasRows)
            {
                dr.Read();
                newStatusId = dr["nextprocessStatusId"].ToString();
                newescalateLevelId = dr["nextRole"].ToString();
                dr.Close();
            }
        }

        //gets the email List
        if (newStatusId == "0") //if next role is going to be 0 , means there is no role actually, select email for the role of 1 stauts of the paricular process
            sql = "select emailList from dbo.roles where roleId=(select nextRole from processStatus where processStatusId=" + "1" + " and fk_processId=" + pid + ")";
        else
            sql = "select emailList from dbo.roles where roleId=(select nextRole from processStatus where processStatusId=" + newStatusId + " and fk_processId=" + pid + ")";

        Db.constr = myGlobal.getIntranetDBConnectionString();
        dr = Db.myGetReader(sql);
        if (dr.HasRows)
        {
            dr.Read();
            mailList = dr["emailList"].ToString();
            dr.Close();
        }

        sql = "update dbo.processRequest set fk_StatusId=" + newStatusId + ", fk_escalateLevelId=" + newescalateLevelId + ",ByUser='" + myGlobal.loggedInUser().ToString() + "',comments='" + txtComments.Text.Trim() + "',lastModified='" + DateTime.Now.ToString("yyyy-MM-dd hh:mm:ss tt") + "' where refId=" + refId + " and fk_processId=" + pid;
        Db.constr = myGlobal.getIntranetDBConnectionString();
        Db.myExecuteSQL(sql);

        //sql = "insert into dbo.orderStatusTrack values(" + Session["processRequestId"].ToString() + "," + newStatusId + "," + newescalateLevelId + ",'" + myGlobal.loggedInUser().ToString() + "','Confirmed','" + DateTime.Now.ToString("yyyy-MM-dd hh:mm:ss tt") + "','" + txtComments.Text.Trim() + "')";
        sql = "insert into processStatusTrack(fk_processRequestId,fk_statusId,fk_EscalateLevelId,lastUpdatedBy,StatusAccept,lastModified,comments,fk_processId) values(" + Session["processRequestId"].ToString() + "," + newStatusId + "," + newescalateLevelId + ",'" + myGlobal.loggedInUser().ToString() + "','" + actionString + "','" + DateTime.Now.ToString("MM-dd-yyyy hh:mm:ss tt") + "','" + txtComments.Text.Trim() + "'," + pid + ")";
        Db.constr = myGlobal.getIntranetDBConnectionString();
        Db.myExecuteSQL(sql);

        sql = "";

        if (FilesForuploadTrack.Trim() != "")
        {
            string[] sfsl = FilesForuploadTrack.Split(';');
            for (int x = 0; x < sfsl.Length; x++)
            {
                if (sql.Trim() == "")
                    sql = "insert into [dbo].[uploadTrack](fk_refId,fk_StatusId,fk_processId,srNo,websiteFilePath,ByUser) values(" + refId + "," + Session["currStatusId"].ToString() + "," + pid + "," + (x + 1).ToString() + ",'" + sfsl[x] + "','" + myGlobal.loggedInUser().ToString() + "')";
                else
                    sql += " ; " + "insert into [dbo].[uploadTrack](fk_refId,fk_StatusId,fk_processId,srNo,websiteFilePath,ByUser) values(" + refId + "," + Session["currStatusId"].ToString() + "," + pid + "," + (x + 1).ToString() + ",'" + sfsl[x] + "','" + myGlobal.loggedInUser().ToString() + "')";
            }
        }

        if (sql.Trim() != "") //if it is not empty then only run
        {
            Db.constr = myGlobal.getIntranetDBConnectionString();
            Db.myExecuteSQL(sql);
        }
    }

    private string getHtmlMsg()
    {
        string strtmp;
        sql = "select processStatusName from processStatus where processStatusId=" + newStatusId + " and fk_processId=" + pid;
        Db.constr = myGlobal.getIntranetDBConnectionString();
        dr = Db.myGetReader(sql);

        string nxtStatus;
        nxtStatus = "";

        while (dr.Read())
        {
            nxtStatus = dr[0].ToString();
        }
        dr.Close();

        strtmp = "Escalated to next Level : <b>" + nxtStatus + "</b><br/>";
        strtmp += "By user : <b>" + myGlobal.loggedInUser() + "</b><br/>";
        strtmp += "Updation comments : <b>" + txtComments.Text + "</b><br/>";

        if (fls.Trim() != "")
            strtmp += " <br/>Please Find the Updated files attached to this mail<br/><br/>";

        return strtmp;
    }

    protected void btnConfirm_Click(object sender, EventArgs e)
    {
        SaveFileAtWebsiteLocation("/excelFileUpload/wfUploads/");  //this fills fls variable with all the files uploaded, whch can be mailed further
        processNow("Confirmed");

        strMessage = " <b>Release Order Under Process :  New Release Order Confirmed</b><br/></br>";
        strMessage += getHtmlMsg();

        string msg = myGlobal.sendRoleBasedMail(mailUrl, strMessage, userEmail, newescalateLevelId, mailList, fls);
        Message.Show(this, msg);

        Response.Redirect("~/Intranet/orders/viewOrdersPO.aspx");
    }

    protected void btnAccept_Click(object sender, EventArgs e)
    {
        SaveFileAtWebsiteLocation("/excelFileUpload/wfUploads/");  //this fills fls variable with all the files uploaded, whch can be mailed further

        processNow("Accept");

        strMessage = " <b>Release Order Under Process :  Accepted/Approved</b><br/></br>";
        strMessage += getHtmlMsg();
        string msg = myGlobal.sendRoleBasedMail(mailUrl, strMessage, userEmail, newescalateLevelId, mailList, fls);
        Message.Show(this, msg);

        Response.Redirect("~/Intranet/orders/viewOrdersPO.aspx");
    }

    protected void btnDecline_Click(object sender, EventArgs e)
    {
        SaveFileAtWebsiteLocation("/excelFileUpload/wfUploads/");
        processNow("Decline");

        strMessage = " <b>Release Order Under Process :  Declined/DisApproved</b><br/></br>";
        strMessage += getHtmlMsg();

        string msg = myGlobal.sendRoleBasedMail(mailUrl, strMessage, userEmail, newescalateLevelId, mailList, fls);
        Message.Show(this, msg);

        Response.Redirect("~/Intranet/orders/viewOrdersPO.aspx");
    }

    protected void btnCancel_Click(object sender, EventArgs e)
    {
        sql = "insert into dbo.processStatusTrack values(" + Session["processRequestId"].ToString() + ",0,0,'" + myGlobal.loggedInUser().ToString() + "','Cancel','" + DateTime.Now.ToString("yyyy-MM-dd hh:mm:ss tt") + "','" + txtComments.Text.Trim() + "')";
        Db.constr = myGlobal.getIntranetDBConnectionString();
        Db.myExecuteSQL(sql);

        sql = "update dbo.processRequest set fk_StatusId=0,fk_escalateLevelId=0,ByUser='" + myGlobal.loggedInUser().ToString() + "',comments='" + txtComments.Text.Trim() + "',lastModified='" + DateTime.Now.ToString("yyyy-MM-dd hh:mm:ss tt") + "' where refId=" + refId + " and fk_processId=" + pid;
        Db.constr = myGlobal.getIntranetDBConnectionString();
        Db.myExecuteSQL(sql);
       
        Response.Redirect("~/Intranet/orders/viewOrdersPO.aspx");
    }

    protected void btnEdit_Click(object sender, EventArgs e)
    {
        Response.Redirect("~/Intranet/orders/addOrder.aspx?refId="+refId+"&type=Edit");
    }

    

    protected void btnReimport_Click(object sender, EventArgs e)
    {
        //call page here
    }

    public Byte[] GetFileContent(System.IO.Stream inputstm)
    {
        Stream fs = inputstm;
        BinaryReader br = new BinaryReader(fs);
        Int32 lnt = Convert.ToInt32(fs.Length);
        byte[] bytes = br.ReadBytes(lnt);
        return bytes;
    }

    private void SaveFileAtWebsiteLocation(string saveFileAtWebSitePath)
    {
        String phySavePth;
        HttpPostedFile postFile;

        string ImageName = string.Empty;

        byte[] path;

        string[] keys;

        fls = "";
        FilesForuploadTrack = "";
        try
        {

            string contentType = string.Empty;

            //byte[] imgContent=null;

            string[] PhotoTitle;

            string PhotoTitlename, trimmedNameWithExt;
            int pikMaxFileName = myGlobal.trimFileLength;

            HttpFileCollection files = Request.Files;

            keys = files.AllKeys;
            string tmpPth;
            int cnt = 0;
            for (int i = 0; i < files.Count; i++)
            {
                trimmedNameWithExt = "";
                postFile = files[i];
                if (postFile.ContentLength > 0)
                {
                    contentType = postFile.ContentType;
                    path = GetFileContent(postFile.InputStream);
                    ImageName = System.IO.Path.GetFileName(postFile.FileName);
                    PhotoTitle = ImageName.Split('.');
                    PhotoTitlename = PhotoTitle[0];

                    if (PhotoTitlename.Length > pikMaxFileName)
                        PhotoTitlename = PhotoTitlename.Substring(0, pikMaxFileName);

                    trimmedNameWithExt = PhotoTitlename + "." + PhotoTitle[1];

                    cnt++;

                    tmpPth = "";
                    tmpPth = Session["processAbbr"].ToString() + "-" + refId + "-" + lblCurrStatus.Text + "-" + myGlobal.loggedInUser() + "-" + DateTime.Now.ToString("dd-MM-yyyy-hh-mm-ss") + "-FL" + cnt.ToString() + "-";
                    //tmpPth = myGlobal.loggedInUser() + "-" + DateTime.Now.ToString("dd-MM-yyyy-hh-mm-ss") + "-FL" + cnt.ToString() + "-";

                    phySavePth = Server.MapPath("~" + saveFileAtWebSitePath + tmpPth) + trimmedNameWithExt;
                    postFile.SaveAs(phySavePth);

                    if (fls.Trim() == "")
                        fls = phySavePth;
                    else
                        fls += ";" + phySavePth;

                    if (FilesForuploadTrack.Trim() == "")
                        FilesForuploadTrack = saveFileAtWebSitePath + tmpPth + trimmedNameWithExt;
                    else
                        FilesForuploadTrack += ";" + saveFileAtWebSitePath + tmpPth + trimmedNameWithExt;
                }
            }
        }
        catch (Exception ex)
        {
            ex.Message.ToString();
        }
    }

    //private void farwardMail()
    //{
    //    string strMessage = string.Empty;
    //    strMessage = " Marketing Plan has been updated <br/>";

    //    if (fls.Trim() != "")
    //        strMessage += " Updated files attached to this mail<br/><br/>";

    //    strMessage += "<br/>Plan Vendor/BU : " + txtVendor.Text;
    //    strMessage += "<br/>Plan Quater : " + txtQuarter.Text;
    //    strMessage += "<br/>Plan Year : " + txtyear.Text;
    //    strMessage += "<br/>Stage has been Processed and updated by '" + myGlobal.loggedInUser() + "'";
    //    strMessage += "<br/>Comments by the user : " + txtupdtComments.Text.Trim();
    //    strMessage += "<br/><br/>Please follow up the process link:";
    //    strMessage += "<br/>" + processUrl;
    //    string ret, dbMsg;

    //    dbMsg = myGlobal.getSystemConfigValue("mailSuccessMsg");

    //    sql = "select emailList from dbo.roleBasedEmails where fk_escalateLevelId=" + newescalateLevelId + "";
    //    Db.constr = myGlobal.getRDDMarketingDBConnectionString();
    //    drd = Db.myGetReader(sql);

    //    string emailadds = "";
    //    while (drd.Read())
    //    {
    //        emailadds = drd[0].ToString();
    //    }
    //    drd.Close();

    //    ret = Mail.SendMultipleAttach(myGlobal.getSystemConfigValue("websiteEmailer"), emailadds, myGlobal.membershipUserEmail(myGlobal.loggedInUser()), "Marketing Plan Updation", strMessage, true, "", fls);
    //    Message.Show(this, ret);
    //}
    protected void btnAddPhoto_Click1(object sender, EventArgs e)
    {
        SaveFileAtWebsiteLocation("/excelFileUpload/wfUploads/");
    }
}
