using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Data;
using System.Data.SqlClient;
using System.Configuration;
using System.Collections;
using System.IO;
using System.Text.RegularExpressions;

public partial class UpdatePopup : System.Web.UI.Page
{
    SqlDataReader dr;
    string sql, refId, pid, rl, sts,action, mailUrl,lnkAccept,lnkDecline, strMessage, userEmail, newStatusId, newescalateLevelId, mailList, fls, cmts, EvoPONO,procReqId,usr;

    protected void Page_Load(object sender, EventArgs e)
    {
        if (!IsPostBack)
        {
            refId = Request.QueryString["oId"].ToString();
            pid = Request.QueryString["pid"].ToString(); //process type id
            rl = Request.QueryString["rl"].ToString();
            sts = Request.QueryString["sts"].ToString();
            action = Request.QueryString["action"].ToString();

            sql = "select * from dbo.processRequest where refId=" + refId + " and fk_StatusId=" + sts + " and fk_EscalateLevelId=" + rl + " and fk_processId=" + pid;
            Db.constr = myGlobal.getIntranetDBConnectionString();
            dr = Db.myGetReader(sql);
            
            if (dr.HasRows)
            {
                dr.Read();
                EvoPONO= dr["refValue"].ToString();
                procReqId = dr["processRequestId"].ToString();
                dr.Close();

                lblMsg.Text = doUpdations();
            }
            else
            {
                dr.Close();
                lblMsg.Text = "Order updation not available or already done";
            }
                               
        }

    }

    private string doUpdations()
    {
        usr = "Mail Box";
        cmts = "Updation from mail box Link";
        

        mailUrl = myGlobal.getSystemConfigValue("RedDotHostRootUrlIntra") + "Intranet/orders/purchaseOrderDetails.aspx?oId=" + refId + "&pid=" + pid + "&action=task";

        processNow(action);

        fls = "";
        strMessage = " <b>Purchase Order Under Process :  " + action.ToUpper() + " </b><br/><br/>";
        strMessage += getHtmlMsg();

        
        string msg;
            msg = myGlobal.sendRoleBasedMail(mailUrl, strMessage, userEmail, newescalateLevelId, mailList, fls);

        return "Order " + action + "ed , successfull.";

    }

    private void processNow(string actionString)
    {
        if (actionString.ToLower() == "decline")
        {
            sql = "select prevprocessStatusId,prevRole from processStatus where processStatusId=" + sts.ToString() + " and fk_processId= " + pid;
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
            sql = "select nextprocessStatusId,nextRole from processStatus where processStatusId=" + sts.ToString() + " and fk_processId= " + pid;
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

        sql = "update dbo.processRequest set fk_StatusId=" + newStatusId + ", fk_escalateLevelId=" + newescalateLevelId + ",ByUser='" + usr + "',comments='" + cmts + "',lastModified='" + DateTime.Now.ToString("yyyy-MM-dd hh:mm:ss tt") + "' where refId=" + refId + " and fk_processId=" + pid;
        Db.constr = myGlobal.getIntranetDBConnectionString();
        Db.myExecuteSQL(sql);

        sql = "insert into processStatusTrack(fk_processRequestId,fk_statusId,fk_EscalateLevelId,lastUpdatedBy,StatusAccept,lastModified,comments,fk_processId) values(" + procReqId + "," + newStatusId + "," + newescalateLevelId + ",'" + usr + "','" + actionString + "','" + DateTime.Now.ToString("MM-dd-yyyy hh:mm:ss tt") + "','" + cmts + "'," + pid + ")";
        Db.constr = myGlobal.getIntranetDBConnectionString();
        Db.myExecuteSQL(sql);
    }

    private string getHtmlMsg()
    {
        string strtmp, nxtStatus;
        nxtStatus = "";
        
        sql = "select processStatusName from processStatus where processStatusId=" + newStatusId + " and fk_processId=" + pid;
        Db.constr = myGlobal.getIntranetDBConnectionString();
        dr = Db.myGetReader(sql);
        
        
        while (dr.Read())
        {
            nxtStatus = dr[0].ToString();
        }
        dr.Close();

        strtmp = " EVO Purchase Order No. :  <b>" + EvoPONO + "</b><br/></br>";
        strtmp += "Escalated to next Level : <b>" + nxtStatus + "</b><br/>";
        strtmp += "By user : <b>" + usr + "</b><br/>";
        strtmp += "Updation comments : <b>" + cmts + "</b><br/><br/>";

        if (fls.Trim() != "" && fls !=null)
            strtmp += " <br/>Please Find the Updated files attached to this mail<br/><br/><br/>";

        strtmp += "To <b>" + "Accept " + "</b>Click the link <br/>";
        //strtmp += "http://localhost:2366/UpdatePopup.aspx?oId=" + refId + "&pid=" + pid + "&rl=" + newescalateLevelId + "&sts=" + newStatusId + "&action=accept" + "<br/><br/>";
        strtmp += myGlobal.getSystemConfigValue("RedDotHostRootUrlIntra") + "UpdatePopup.aspx?oId=" + refId + "&pid=" + pid + "&rl=" + newescalateLevelId + "&sts=" + newStatusId + "&action=accept" + "<br/><br/>";

        strtmp += "To <b>" + "Decline " + "</b>Click the link <br/>";
        //strtmp += "http://localhost:2366/UpdatePopup.aspx?oId=" + refId + "&pid=" + pid + "&rl=" + newescalateLevelId + "&sts=" + newStatusId + "&action=decline" + "<br/><br/>";
        strtmp += myGlobal.getSystemConfigValue("RedDotHostRootUrlIntra") + "UpdatePopup.aspx?oId=" + refId + "&pid=" + pid + "&rl=" + newescalateLevelId + "&sts=" + newStatusId + "&action=decline" + "<br/><br/>";
        
        return strtmp;
    }

}