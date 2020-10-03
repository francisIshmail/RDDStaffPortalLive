using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Web.UI.HtmlControls;
using System.Data;
using System.Data.SqlClient;
using System.Configuration;
using System.Collections;
using System.IO;
using System.Text.RegularExpressions;
using System.Web.Security;
using System.Net;
using System.Text;

//using iTextSharp.text;
//using iTextSharp.text.pdf;
//using iTextSharp.text.html;
//using iTextSharp.text.html.simpleparser;


public partial class Intranet_orders_purchaseOrderDetails1 : System.Web.UI.Page
{
    SqlDataReader dr; // , drd;
    DataTable dt;
    string newStatusId, newescalateLevelId, sql, refId, pid, action, mailUrl, userEmail, mailList, fls, FilesForuploadTrack, strMessage,qrls;
    //string[] userRole;
    string whereClauseRoleLine,webFileUploadsBasePath;
    int editableFalseStageBTB, editableFalseStageCntry, editableFalseStageCmpny;
    int intimationEmailStageBTB, intimationEmailStageCntry, intimationEmailStageCmpny;

    protected void Page_Load(object sender, EventArgs e)
    {
        //MaintainScrollPositionOnPostBack = true;
        
        btnCancel.Attributes.Add("onClick", "return getConfirmation();");
        btnConfirm.Attributes.Add("onClick", "return getConfirmation();");
        btnAccept.Attributes.Add("onClick", "return getConfirmation();");
        btnDecline.Attributes.Add("onClick", "return getConfirmation();");
        btnHold.Attributes.Add("onClick", "return getConfirmation();");

        lblError.Text = "";
        lblMsg.Text = "";
        refId = Request.QueryString["oId"].ToString();
        pid = Request.QueryString["pid"].ToString(); //process type id
        action = Request.QueryString["action"].ToString();
        qrls = Request.QueryString["qrls"].ToString();
        qrls = qrls.ToUpper();

        webFileUploadsBasePath = "/excelFileUpload/wfUploads/";

        lblOrderId.Text = refId;

        //Editable options should be shown only till these status
        editableFalseStageBTB = 4;   // BTB stage---------- 10011
        editableFalseStageCntry = 4; // RRCountry stage ---- 10012
        editableFalseStageCmpny = 2; // RRCompany stage---- 10013

        //Printable options should be shown only at these status  (PM cordinator level as last status)
        intimationEmailStageBTB = 7;   // BTB stage---------- 10011
        intimationEmailStageCntry = 7; // RRCountry stage ---- 10012
        intimationEmailStageCmpny = 5; // RRCompany stage---- 10013

        //mailUrl = myGlobal.getSystemConfigValue("RedDotHostRootUrlIntra") + "Intranet/orders/purchaseOrderDetails.aspx?oId=" + refId + "&pid=" + pid + "&action=task";
        
        //mailUrl = myGlobal.getSystemConfigValue("RedDotHostRootUrlIntra") + "Intranet/orders/viewOrdersPO.aspx?wfTypeId=10011";
        mailUrl = myGlobal.getSiteIPwithPortNo() + "/Intranet/orders/viewOrdersPO.aspx?wfTypeId=10011";

        //userRole = myGlobal.loggedInRoleList();
        //whereClauseRoleLine = "";
        //for (int i = 0; i < userRole.Length; i++)
        //{
        //    if (whereClauseRoleLine == "")
        //        whereClauseRoleLine = "'" + userRole[i] + "'";
        //    else
        //        whereClauseRoleLine += ",'" + userRole[i] + "'";
        //}

        whereClauseRoleLine = "'" + qrls + "'";

        userEmail = myGlobal.membershipUserEmail(myGlobal.loggedInUser());

        try
        {
            if (!IsPostBack)
            {
                Db.LoadDDLsWithCon(ddlCBNName, "select * from cbnNamesList order by id", "cbnName", "Id", myGlobal.getIntranetDBConnectionString());

                // freshDataGrid(1);
            
                sql = "select * from dbo.processRequest where fk_escalateLevelId in (select roleId from dbo.roles where fk_deptId in (select autoindex from dbo.departments where upper(departmentName) in (" + whereClauseRoleLine + "))) and fk_processId=" + pid + " and refId=" + refId;
                Db.constr = myGlobal.getIntranetDBConnectionString();
                dt = Db.myGetDS(sql).Tables[0];

                if (action.ToUpper() == "VIEW")
                {
                    // tblTask.Visible = false;
                    pnlTaskUpd.Visible = false;
                    lblHeader.Text = "Viewable Purchase Order";
                }
                else
                    if (action.ToUpper() == "TASK")
                    {
                        if (dt.Rows.Count > 0)
                        {
                            // tblTask.Visible = true;
                            pnlTaskUpd.Visible = true;
                            lblHeader.Text = "Editable Purchase Order";
                        }
                        else
                        {
                            // tblTask.Visible = false;
                            pnlTaskUpd.Visible = false;
                            lblHeader.Text = "No order";
                        }
                    }

                //BindGrid();  //move down on 4-mar-2013
                
                //sql = "select * from dbo.purchaseOrders where poId=" + refId;
                sql = "select processSubType=(select processSubType from dbo.process_def where processId=" + pid + "),* from dbo.purchaseOrders where poId=" + refId;
                Db.constr = myGlobal.getIntranetDBConnectionString();
                dr = Db.myGetReader(sql);

                double LineTotal, LineTotalRebate, LineTotalSelling;
                while (dr.Read())
                {
                    
                    lblVendor.Text = dr["vendor"].ToString();
                    lblComments.Text = dr["comments"].ToString();
                    
                    txtFpoRef.Text = dr["fpoNo"].ToString();
                    txtEvoPONO.Text = dr["evoPoNo"].ToString();
                    txtPODate.Text = Convert.ToDateTime(dr["PoDate"]).ToString("MM-dd-yyyy");
                    txtReqDelDate.Text = Convert.ToDateTime(dr["reqDelDate"]).ToString("MM-dd-yyyy");
                    txtOPGCode.Text = dr["opgCode"].ToString();
                    txtCBNNo.Text = dr["cbnNo"].ToString();

                    lblFpoRef.Text = dr["fpoNo"].ToString();
                    lblEvoPONO.Text = dr["evoPoNo"].ToString();
                    lblPODate.Text = Convert.ToDateTime(dr["PoDate"]).ToString("MM-dd-yyyy");
                    lblReqDelDate.Text = Convert.ToDateTime(dr["reqDelDate"]).ToString("MM-dd-yyyy");
                    lblOPGCode.Text = dr["opgCode"].ToString();
                    lblCBNNo.Text = dr["cbnNo"].ToString();
                    lblCBNName1.Text = dr["cbnName"].ToString();
                    ddlCBNName.Items.FindByText(lblCBNName1.Text).Selected = true;

                    LineTotal = Convert.ToDouble(dr["total"].ToString());
                    LineTotalRebate = Convert.ToDouble(dr["totalCostAfterRebate"].ToString());
                    LineTotalSelling = Convert.ToDouble(dr["totalSelling"].ToString());
                    lblLineMargin.Text = (Decimal.Parse(dr["margin"].ToString())).ToString() + "%";

                    lblLineTotal.Text = string.Format("{0:n}", LineTotal);
                    lblLineTotalRebate.Text = string.Format("{0:n}", LineTotalRebate);
                    lblLineTotalSelling.Text = string.Format("{0:n}", LineTotalSelling);

                    txtProdctManager.Text = dr["productManager"].ToString();
                    txtHeadOfOffice.Text = dr["headOfFinance"].ToString();
                    lblProdctManager.Text = dr["productManager"].ToString();
                    lblHeadOfOffice.Text = dr["headOfFinance"].ToString();
                    lblprocessSubType.Text = dr["processSubType"].ToString();
                    lbldbCode.Text = dr["dbCode"].ToString(); //just saved to use later
                    lblCustomer.Text = dr["customerName"].ToString(); //just saved to use later
                    
                    lblBU.Text = dr["bu"].ToString(); //just saved to use later
                    lblCreatedBy.Text = dr["createdBy"].ToString(); //just saved to use later

                    lblCustAcct.Text = dr["custAcct"].ToString(); //just saved to use later
                    lblVendorAcct.Text = dr["vendorAcct"].ToString();
                }

                if (action.ToUpper() == "VIEW" || (qrls.ToUpper() != "PRODUCTSPECIALIST" && qrls.ToUpper() != "PRODUCTMANAGERCORDINATOR"))
                {
                    txtFpoRef.ReadOnly = true;
                    txtFpoRef.BackColor = System.Drawing.Color.Silver;
                }

                if (action.ToUpper() == "VIEW" || qrls.ToUpper() != "PRODUCTMANAGERCORDINATOR")
                {
                    txtEvoPONO.ReadOnly = true;
                    txtPODate.ReadOnly = true;
                    txtReqDelDate.ReadOnly = true;
                    txtOPGCode.ReadOnly = true;
                    txtCBNNo.ReadOnly = true;

                    txtEvoPONO.BackColor = System.Drawing.Color.Silver;
                    txtPODate.BackColor = System.Drawing.Color.Silver;
                    txtReqDelDate.BackColor = System.Drawing.Color.Silver;
                    txtOPGCode.BackColor = System.Drawing.Color.Silver;
                    txtCBNNo.BackColor = System.Drawing.Color.Silver;

                }
                ddlOrderType.Items.Add(lblprocessSubType.Text);

                if (lblprocessSubType.Text == "BTB")
                {
                    getCustInfo();
                    pnlCustomerInfo.Visible = true;
                    pnlVendorInfo.Visible = true;
                }
                else //RR1 or RR2
                {

                    pnlCustomerInfo.Visible = false;
                    pnlVendorInfo.Visible = true;
                }

                ///////code shifted few lines ahead after detail grid filled up

                //try
                //{
                // getVendorInfo();
                //}
                //catch (Exception exps)
                //{
                //    lblMsg.Text = "Error !!! Loading statistics data ," + exps.Message + " , Kindly retry";
                //    //MsgBoxControl1.show(lblMsg.Text,"Error !!!");
                //}

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

                BindGrid();
                
                try
                {
                    getVendorInfo();
                }
                catch (Exception exps)
                {
                    lblMsg.Text = "Error !!! Loading statistics data ," + exps.Message + " , Kindly retry";
                    //MsgBoxControl1.show(lblMsg.Text,"Error !!!");
                }

                //get history data from escalation table
                loadExistingAttachmentsForStatus();
                
                sql = "select ROW_NUMBER() Over (Order by A.autoindex) As [SrNo], A.*,B.processStatusName as action_Stage from dbo.processStatusTrack A join  dbo.processStatus B on B.processStatusID=A.action_StatusID and A.fk_processID=B.fk_processID where fk_processRequestId=(select processRequestId from dbo.processRequest where refId=" + refId + ")  order by autoindex";
                Db.constr = myGlobal.getIntranetDBConnectionString();
                GridEscalationHistory.DataSource = Db.myGetDS(sql).Tables[0];
                GridEscalationHistory.DataBind();


                //sql = "select role from dbo.orderEscalate where escalateLevelId=1"; //fetch first role for the process
                sql = "select departmentName from dbo.departments where autoindex=(select fk_deptId from dbo.roles where roleId in (select fk_roleid from dbo.processEscalate where escalateLevelId=1 and fk_processId=" + pid + "))"; //fetch first role for the process

                string rl = "";

                dr = Db.myGetReader(sql);
                while (dr.Read())
                {
                    rl = dr["departmentName"].ToString(); //basically a logger role
                }
                dr.Close();

                //if (userRole.Contains(rl.ToUpper()))
                if (qrls.ToUpper() == rl.ToUpper())
                {
                    btnEdit.Visible = false;

                    if (Convert.ToInt32(Session["currStatusId"]) <= 2)
                    {
                        btnCancel.Visible = true;
                        
                        btnConfirm.Visible = true;
                        //btnReimport.Visible = true;

                        btnAccept.Visible = false;
                        btnDecline.Visible = false;
                    }
                    else
                    {
                        btnCancel.Visible = false;
                        btnConfirm.Visible = false;
                        
                        //btnReimport.Visible = false;

                        btnAccept.Visible = true;
                        btnDecline.Visible = true;
                    }

                }

                if (Convert.ToInt32(Session["currStatusId"]) == 0)
                {
                    btnCancel.Visible = false;
                    btnConfirm.Visible = false;
                    //btnReimport.Visible = false;
                    btnAccept.Visible = false;
                    btnDecline.Visible = false;
                }

                

                //if (Convert.ToInt32(Session["currStatusId"]) == 7)
                //{
                //    PanelInitimationMailId.Visible = true;
                //    lblIntimationMailIdTitle.Visible = true;
                //}
                //else
                //{
                //    PanelInitimationMailId.Visible = false;
                //    lblIntimationMailIdTitle.Visible = false;
                //}
                
                //if ((Convert.ToInt32(Session["currStatusId"]) == printStageBTB && pid == "10011") || (Convert.ToInt32(Session["currStatusId"]) == printStageCntry && pid == "10012") || (Convert.ToInt32(Session["currStatusId"]) == printStageCmpny && pid == "10013"))
                //{
                //    divPrnSaveDwnl.Visible = true;
                //}

                //if ((Convert.ToInt32(Session["currStatusId"]) == printStageBTB && pid == "10011") || (Convert.ToInt32(Session["currStatusId"]) == printStageCntry && pid == "10012") || (Convert.ToInt32(Session["currStatusId"]) == printStageCmpny && pid == "10013"))
                if ((Convert.ToInt32(Session["currStatusId"]) >= intimationEmailStageBTB && pid == "10011") || (Convert.ToInt32(Session["currStatusId"]) >= intimationEmailStageCntry && pid == "10012") || (Convert.ToInt32(Session["currStatusId"]) >= intimationEmailStageCmpny && pid == "10013"))
                {
                    PanelInitimationMailId.Visible = true;
                    lblIntimationMailIdTitle.Visible = true;
                    btnDecline.Visible = false;
                    btnHold.Visible = false;
                }
                else
                {
                    PanelInitimationMailId.Visible = false;
                    lblIntimationMailIdTitle.Visible = false;
                    btnDecline.Visible = true;
                    btnHold.Visible = true;
                }

                //bindAssignRolesDDL();
                //bindUnassignRolesDDL();

                if (Convert.ToInt32(Session["currStatusId"]) <= 2)
                {
                    btnDecline.Visible = false;
                }

            } //ispostback ends

            btnCancel.Visible = true;   //Cancel order remains open for all wef. 03-June-2013
        }
        catch (Exception exps)
        {
            lblMsg.Text = "Error !!! " + exps.Message + " , Kindly retry";
            //MsgBoxControl1.show(lblMsg.Text,"Error !!! ");
        }
    }

    private void BindGrid()
    {
        sql = "select * from dbo.purchaseOrderLines where fk_poId=" + refId + " order by poLineId";
        Db.constr = myGlobal.getIntranetDBConnectionString();
        dt = Db.myGetDS(sql).Tables[0];
        gridDetails1.DataSource = dt;
        gridDetails1.DataBind();
        if (action == "view" )
        {
            lnkNewItem.Visible = false;
            foreach (GridViewRow rw in gridDetails1.Rows)
            {
                LinkButton LinkEdit = rw.FindControl("LinkEdit") as LinkButton;
                LinkButton LinkDelete = rw.FindControl("LinkDelete") as LinkButton;

                LinkEdit.Enabled = false;
                LinkDelete.Enabled = false;

            }
        }
        else   //Task case , handle differently for three cases
        {
            int tmpCurrStatusId = Convert.ToInt32(Session["currStatusId"]);

            if ((Convert.ToInt32(Session["currStatusId"]) > editableFalseStageBTB && pid == "10011") || (Convert.ToInt32(Session["currStatusId"]) > editableFalseStageCntry && pid == "10012") || (Convert.ToInt32(Session["currStatusId"]) > editableFalseStageCmpny && pid == "10013"))    //BTB ---------- 10011
            {
                lnkNewItem.Visible = false;
                foreach (GridViewRow rw in gridDetails1.Rows)
                {
                    LinkButton LinkEdit = rw.FindControl("LinkEdit") as LinkButton;
                    LinkButton LinkDelete = rw.FindControl("LinkDelete") as LinkButton;

                    LinkEdit.Enabled = false;
                    LinkDelete.Enabled = false;

                }
            }

            foreach (GridViewRow rw in gridDetails1.Rows)
            {
                LinkButton LinkDelete = (LinkButton) rw.FindControl("LinkDelete");
                if (LinkDelete != null && LinkDelete.Enabled==true)
                {
                    LinkDelete.Attributes.Add("onClick", "return getConfirmation();");
                }

                LinkButton LinkUpd = (LinkButton) rw.FindControl("LinkUpd");
                if (LinkUpd != null && LinkUpd.Enabled == true)
                {
                    LinkUpd.Attributes.Add("onClick", "return getConfirmation();");
                }

            }
        }

        if(gridDetails1.EditIndex<0)
          getStockLevelsByProduct();
    }

    private void getCustInfo()
    {
        double CustomerCreditLimit, CustomerOutstanding;
        //sql = "BEGIN declare @dbCode varchar(10) declare @cRate float declare @pCustName varchar(255) declare @dcLink int set @dbCode='" + lbldbCode.Text + "' set @pCustName='" + lblCustomer.Text.Trim() + "' if(@dbCode='UG') set @cRate=1 else select @cRate=fSellRate from dbo.CurrencyHist where iCurrencyID=1 and idCurrencyHist=(select max(idCurrencyHist) from dbo.CurrencyHist) select @dcLink=DCLink from dbo.client where ltrim(rtrim(Name))=@pCustName Select C.DCLink,C.Account,C.Name,Outstanding=isnull(tblOut.Outstanding,0),SettlementDays=isnull(tblOut.SettlementDays,0),CreditLimit=isnull(tblOut.CreditLimit,0),GP=isnull(tblGP.GP,0) From dbo.Client as C Left Join (Select DCLInk,round(SUM(Outstanding),2) as 'Outstanding',Max(PaymentTerms) 'SettlementDays',Max(Credit_Limit) as 'CreditLimit' From (SELECT C.DCLink,AR.fForeignOutstanding AS 'Outstanding',CASE C.AccountTerms WHEN '0' THEN '1' WHEN '1' THEN '30' WHEN '2' THEN '45' WHEN '3' THEN '60' WHEN '4' THEN '90' WHEN '5' THEN '120' WHEN '6' THEN '150' END AS 'PaymentTerms',(C.Credit_Limit/@cRate) as 'Credit_Limit' FROM dbo.PostAR AS AR LEFT OUTER JOIN dbo.Client AS C ON AR.AccountLink = C.DCLink WHERE AR.Reference<>'' ) as tbl Group By DCLInk ) as tblOut ON tblOut.DCLink=C.DCLink Left Join (SELECT C.DCLink,GP=isnull(round(SUM((([Credit]-[Debit])/@cRate)- CASE WHEN [Credit]-[Debit]>=0 THEN (Quantity*GP.avgCostUSD) ELSE (-Quantity*GP.avgCostUSD) END),2),0) FROM dbo.PostST AS T LEFT OUTER JOIN dbo.Client AS C ON T.DrCrAccount=C.DCLink LEFT OUTER JOIN [Tej].[dbo].PostSTGP" + lbldbCode.Text + " AS GP ON T.AutoIdx=GP.postSTID WHERE ((T.[Id]='Inv') OR (T.[Id]='OInv') OR (T.[Id]='Crn') OR (T.[Id]='JL')) GROUP BY C.DCLink ) as tblGP ON tblGP.DClink=C.DCLink Where C.DCLink=@dcLink END";
        //sql = "BEGIN declare @dbCode varchar(10) declare @cRate float declare @pCustName varchar(255) declare @dcLink int set @dbCode='" + lbldbCode.Text + "' set @pCustName='" + lblCustomer.Text.Trim() + "' if(@dbCode='UG') set @cRate=1 else select @cRate=fSellRate from dbo.CurrencyHist where iCurrencyID=1 and idCurrencyHist=(select max(idCurrencyHist) from dbo.CurrencyHist) select @dcLink=DCLink from dbo.client where ltrim(rtrim(Name))=@pCustName Select C.DCLink,C.Account,C.Name,totalOutstanding=isnull(tblOut.Outstanding,0),SettlementDays=isnull(tblOut.SettlementDays,0),CreditLimit=isnull(tblOut.CreditLimit,0),GP=isnull(tblGP.GP,0) From dbo.Client as C Left Join (Select DCLInk,round(SUM(Outstanding),2) as 'Outstanding',Max(PaymentTerms) 'SettlementDays',Max(Credit_Limit) as 'CreditLimit' From (SELECT C.DCLink,AR.fForeignOutstanding AS 'Outstanding',CASE C.AccountTerms WHEN '0' THEN '1' WHEN '1' THEN '30' WHEN '2' THEN '45' WHEN '3' THEN '60' WHEN '4' THEN '90' WHEN '5' THEN '120' WHEN '6' THEN '150' END AS 'PaymentTerms',(C.Credit_Limit/@cRate) as 'Credit_Limit' FROM dbo.PostAR AS AR LEFT OUTER JOIN dbo.Client AS C ON AR.AccountLink = C.DCLink WHERE AR.Reference<>'' ) as tbl Group By DCLInk ) as tblOut ON tblOut.DCLink=C.DCLink Left Join (SELECT C.DCLink,GP=isnull(round(SUM((([Credit]-[Debit])/@cRate)- CASE WHEN [Credit]-[Debit]>=0 THEN (Quantity*GP.avgCostUSD) ELSE (-Quantity*GP.avgCostUSD) END),2),0) FROM dbo.PostST AS T LEFT OUTER JOIN dbo.Client AS C ON T.DrCrAccount=C.DCLink LEFT OUTER JOIN [Tej].[dbo].PostSTGP" + lbldbCode.Text + " AS GP ON T.AutoIdx=GP.postSTID WHERE ((T.[Id]='Inv') OR (T.[Id]='OInv') OR (T.[Id]='Crn') OR (T.[Id]='JL')) GROUP BY C.DCLink ) as tblGP ON tblGP.DClink=C.DCLink Where C.DCLink=@dcLink END";

        lblCustomerName.Text = lblCustomer.Text.Trim() + " ( " + lblCustAcct.Text.Trim() + " ) ";
        lblCustomerGPMargin.Text = lblLineMargin.Text;//this order

        sql = "exec Tej.dbo.customerOutstandingAgeWise4Web1 @dbCode='" + lbldbCode.Text.Trim() + "',@cstAcct='" + lblCustAcct.Text.Trim() + "'";
        //sql = "exec Tej.dbo.customerOutstandingAgeWise4Web1 @dbCode='TRI',@cstAcct='RDDTZ'";
        Db.constr = myGlobal.getConnectionStringForDB(lbldbCode.Text);
        SqlDataReader drTmp = Db.myGetReader(sql);
        if (drTmp.HasRows)
        {
            drTmp.Read();
            
            CustomerCreditLimit = Convert.ToDouble(drTmp["CreditLimit"]);
            lblCustomerCreditLimit.Text = string.Format("{0:n}", CustomerCreditLimit);

            lblCustomerSettlementDay.Text = drTmp["SettlementDays"].ToString();

                ////////////////////Age30///////////////////////
                CustomerOutstanding = 0;
                if (drTmp["Age0"] != DBNull.Value)
                    CustomerOutstanding = Convert.ToDouble(drTmp["Age0"]);

                lblCustomerOutstanding0.Text = string.Format("{0:n}", CustomerOutstanding);

                ////////////////////Age30///////////////////////
                CustomerOutstanding = 0;
                if (drTmp["Age30"] != DBNull.Value)
                    CustomerOutstanding = Convert.ToDouble(drTmp["Age30"]);

                lblCustomerOutstanding30.Text = string.Format("{0:n}", CustomerOutstanding);

                ////////////////////Age30///////////////////////
                CustomerOutstanding = 0;
                if (drTmp["Age45"] != DBNull.Value)
                    CustomerOutstanding = Convert.ToDouble(drTmp["Age45"]);

                lblCustomerOutstanding45.Text = string.Format("{0:n}", CustomerOutstanding);

                ////////////////////Age30///////////////////////
                CustomerOutstanding = 0;
                if (drTmp["Age60"] != DBNull.Value)
                    CustomerOutstanding = Convert.ToDouble(drTmp["Age60"]);

                lblCustomerOutstanding60.Text = string.Format("{0:n}", CustomerOutstanding);

                ////////////////////Age30///////////////////////
                CustomerOutstanding = 0;
                if (drTmp["Age90"] != DBNull.Value)
                    CustomerOutstanding = Convert.ToDouble(drTmp["Age90"]);

                lblCustomerOutstanding90.Text = string.Format("{0:n}", CustomerOutstanding);

                ////////////////////Age30///////////////////////
                CustomerOutstanding = 0;
                if (drTmp["Age120"] != DBNull.Value)
                    CustomerOutstanding = Convert.ToDouble(drTmp["Age120"]);

                lblCustomerOutstanding120.Text = string.Format("{0:n}", CustomerOutstanding);

                ////////////////////Age30///////////////////////
                CustomerOutstanding = 0;
                if (drTmp["Age150"] != DBNull.Value)
                    CustomerOutstanding = Convert.ToDouble(drTmp["Age150"]);

                lblCustomerOutstanding150.Text = string.Format("{0:n}", CustomerOutstanding);

                ////////////////////Age30///////////////////////
                CustomerOutstanding = 0;
                if (drTmp["Age150Plus"] != DBNull.Value)
                    CustomerOutstanding = Convert.ToDouble(drTmp["Age150Plus"]);

                lblCustomerOutstanding150plus.Text = string.Format("{0:n}", CustomerOutstanding);

                ////////////////////Total///////////////////////
                CustomerOutstanding = 0;
                if (drTmp["totalOutstanding"] != DBNull.Value)
                    CustomerOutstanding = Convert.ToDouble(drTmp["totalOutstanding"]);

                lblCustomerOutstandingTotal.Text = string.Format("{0:n}", CustomerOutstanding);
            
            
        }
        drTmp.Close();

    }

    private void getCustInfoOrg()
    {
        double CustomerCreditLimit, CustomerOutstanding;
        //in this query ug case $ conversion is divided to 1 rest all as per currency hist table as per the database
        //sql = "BEGIN declare @dbCode varchar(10) declare @cRate float declare @dcLink int set @dbCode='" + lbldbCode.Text + "' if(@dbCode='UG') set @cRate=1 else select @cRate=fSellRate from dbo.CurrencyHist where iCurrencyID=1 and idCurrencyHist=(select max(idCurrencyHist) from dbo.CurrencyHist) select @dcLink=DCLink from dbo.client where ltrim(rtrim(Name))='" + lblCustomer.Text.Trim() + "' select ccc1.*,ccc2.GP from ( Select DCLInk,Account,Name,round(SUM(Outstanding),2) as 'Outstanding',Max(PaymentTerms) 'SettlementDays',Max(Credit_Limit)/@cRate as 'CreditLimit' From (SELECT C.DCLink,C.Account,C.Name,AR.fForeignOutstanding AS 'Outstanding', CASE C.AccountTerms WHEN '0' THEN '1' WHEN '1' THEN '30' WHEN '2' THEN '45' WHEN '3' THEN '60' WHEN '4' THEN '90' WHEN '5' THEN '120' WHEN '6' THEN '150' END AS 'PaymentTerms',C.Credit_Limit FROM dbo.PostAR AS AR LEFT OUTER JOIN dbo.Client AS C ON AR.AccountLink = C.DCLink WHERE AR.Reference<>'' AND C.DCLink=@dcLink ) as tbl Group By DCLInk,Account,Name) as ccc1 left join (SELECT C.DCLink,C.Account, C.Name, GP=isnull(round(SUM((([Credit]-[Debit])/GP.fBuyRate)- CASE WHEN [Credit]-[Debit]>=0 THEN (Quantity*GP.avgCostUSD) ELSE (-Quantity*GP.avgCostUSD) END),2),0) FROM dbo.PostST AS T LEFT OUTER JOIN dbo.Client AS C ON T.DrCrAccount=C.DCLink LEFT OUTER JOIN [Tej].[dbo].PostSTGPTRI AS GP ON T.AutoIdx=GP.postSTID WHERE ((T.[Id]='Inv') OR (T.[Id]='OInv') OR (T.[Id]='Crn') OR (T.[Id]='JL')) AND C.DcLink=@dcLink GROUP BY C.DCLink,C.Account,C.Name) as ccc2 on ccc1.DCLink=ccc2.DCLink END";
        sql = "BEGIN declare @dbCode varchar(10) declare @cRate float declare @pCustName varchar(255) declare @dcLink int set @dbCode='" + lbldbCode.Text + "' set @pCustName='" + lblCustomer.Text.Trim() + "' if(@dbCode='UG') set @cRate=1 else select @cRate=fSellRate from dbo.CurrencyHist where iCurrencyID=1 and idCurrencyHist=(select max(idCurrencyHist) from dbo.CurrencyHist) select @dcLink=DCLink from dbo.client where ltrim(rtrim(Name))=@pCustName Select C.DCLink,C.Account,C.Name,Outstanding=isnull(tblOut.Outstanding,0),SettlementDays=isnull(tblOut.SettlementDays,0),CreditLimit=isnull(tblOut.CreditLimit,0),GP=isnull(tblGP.GP,0) From dbo.Client as C Left Join (Select DCLInk,round(SUM(Outstanding),2) as 'Outstanding',Max(PaymentTerms) 'SettlementDays',Max(Credit_Limit) as 'CreditLimit' From (SELECT C.DCLink,AR.fForeignOutstanding AS 'Outstanding',CASE C.AccountTerms WHEN '0' THEN '1' WHEN '1' THEN '30' WHEN '2' THEN '45' WHEN '3' THEN '60' WHEN '4' THEN '90' WHEN '5' THEN '120' WHEN '6' THEN '150' END AS 'PaymentTerms',(C.Credit_Limit/@cRate) as 'Credit_Limit' FROM dbo.PostAR AS AR LEFT OUTER JOIN dbo.Client AS C ON AR.AccountLink = C.DCLink WHERE AR.Reference<>'' ) as tbl Group By DCLInk ) as tblOut ON tblOut.DCLink=C.DCLink Left Join (SELECT C.DCLink,GP=isnull(round(SUM((([Credit]-[Debit])/@cRate)- CASE WHEN [Credit]-[Debit]>=0 THEN (Quantity*GP.avgCostUSD) ELSE (-Quantity*GP.avgCostUSD) END),2),0) FROM dbo.PostST AS T LEFT OUTER JOIN dbo.Client AS C ON T.DrCrAccount=C.DCLink LEFT OUTER JOIN [Tej].[dbo].PostSTGP" + lbldbCode.Text + " AS GP ON T.AutoIdx=GP.postSTID WHERE ((T.[Id]='Inv') OR (T.[Id]='OInv') OR (T.[Id]='Crn') OR (T.[Id]='JL')) GROUP BY C.DCLink ) as tblGP ON tblGP.DClink=C.DCLink Where C.DCLink=@dcLink END";
        Db.constr = myGlobal.getConnectionStringForDB(lbldbCode.Text);
        SqlDataReader drTmp = Db.myGetReader(sql);
        if (drTmp.HasRows)
        {
            drTmp.Read();
            lblCustomerName.Text = drTmp["Name"].ToString() + " ( " + drTmp["Account"].ToString() + " ) ";
            CustomerCreditLimit = Convert.ToDouble(drTmp["CreditLimit"]);
            lblCustomerCreditLimit.Text = string.Format("{0:n}", CustomerCreditLimit);
            CustomerOutstanding = Convert.ToDouble(drTmp["totalOutstanding"]);
            lblCustomerOutstandingTotal.Text = string.Format("{0:n}", CustomerOutstanding);
            lblCustomerGPMargin.Text = lblLineMargin.Text;//drTmp["GP"].ToString();
            lblCustomerSettlementDay.Text = drTmp["SettlementDays"].ToString();
        }
        drTmp.Close();
    }

    private void getVendorInfo()
    {
        double VendorCreditLimit, VendorAmountinbackLog, VendorOutstandingAmount;
        string ven = lblVendor.Text.Trim();
        //sql = "BEGIN declare @dbCode varchar(10) declare @cRate float declare @dcLink int set @dbCode='DU' if(@dbCode='UG')  set @cRate=1 else   select @cRate=fSellRate from dbo.CurrencyHist where iCurrencyID=1 and idCurrencyHist=(select max(idCurrencyHist) from dbo.CurrencyHist) select @dcLink=DCLink from dbo.Vendor where ltrim(rtrim(Name))='" + ven + "' select  vndr.Account, vndr.Name, vndr.DClink,  isnull(ccc2.Outstanding,0) as Outstanding, isnull(ccc2.Terms,0) as Terms, isnull(ccc2.CreditLimit,0) as CreditLimit , isnull(ccc1.backLog,0) as backLog from  (  Select   *   From   dbo.Vendor  Where DCLink=@dcLink ) as vndr left Join (    SELECT    V.DCLink, V.Account  , V.Name ,    isnull(SUM(fQuantityLineTotInclForeign),0) as 'backLog'     FROM      [dbo].[InvNum]   I ,    [dbo].[Project]   P ,    [dbo]._btblInvoiceLines L ,    [dbo].Vendor   V   WHERE  (DocState=3 or DocState=1) AND UPPER(Description) LIKE '%PURCHASE%' AND I.ProjectID=P.ProjectLink AND L.IinvoiceID=I.AutoIndex AND I.AccountID=V.DCLink AND (fQtyProcessed<>fQuantity) AND DCLink=@dcLink   GROUP BY  V.DCLink , V.Account , V.Name ) as ccc1 on vndr.DCLink=ccc1.DClink left join   (  Select    DCLink,Account,Name,   SUM(USDOutstanding) as 'Outstanding',Max(PaymentTerms) as 'Terms',Max(Credit_Limit) as 'CreditLimit'   From   (     SELECT    V.DCLink,   V.Account,    V.Name,   AP.fForeignOutstanding AS 'USDOutstanding',            CASE V.AccountTerms WHEN 0 THEN '1' WHEN 1 THEN '30' WHEN 2 THEN '45' WHEN 3 THEN '60' WHEN 4 THEN '90' WHEN 5 THEN '120' WHEN 6 THEN '150' END AS 'PaymentTerms',        (V.Credit_Limit/@cRate) as 'Credit_Limit'         FROM  dbo.PostAP AS AP LEFT OUTER JOIN         dbo.Vendor AS V ON AP.AccountLink = V.DCLink         WHERE    ABS(AP.fForeignOutstanding) > 0   AND V.DCLink=@dcLink   ) as tbl  GROUP BY DCLink,Account,Name  ) as ccc2  on ccc1.DCLink=ccc2.DCLink END";
        
        //sql = "BEGIN declare @dbCode varchar(10) declare @pVendorName varchar(255) declare @cRate float declare @dcLink int set @dbCode='DU' set @pVendorName='" + ven + "' if(@dbCode='UG') set @cRate=1 else select @cRate=fSellRate from dbo.CurrencyHist where iCurrencyID=1 and idCurrencyHist=(select max(idCurrencyHist) from dbo.CurrencyHist) select @dcLink=DCLink from dbo.Vendor where ltrim(rtrim(Name))=@pVendorName Select V.DCLink,V.Account,V.Name,Outstanding=isnull(tblOut.Outstanding,0),Terms=isnull(tblOut.Terms,0),CreditLimit=isnull(tblOut.CreditLimit,0),Backlog=isnull(tblBcklg.Backlog,0) From dbo.Vendor as V Left Join (Select DCLink,SUM(USDOutstanding) as 'Outstanding',Max(PaymentTerms) as 'Terms',Max(Credit_Limit) as 'CreditLimit' From (SELECT V.DCLink,AP.fForeignOutstanding AS 'USDOutstanding',CASE V.AccountTerms WHEN 0 THEN '1' WHEN 1 THEN '30' WHEN 2 THEN '45' WHEN 3 THEN '60' WHEN 4 THEN '90' WHEN 5 THEN '120' WHEN 6 THEN '150' END AS 'PaymentTerms',(V.Credit_Limit/@cRate) as 'Credit_Limit' FROM dbo.PostAP AS AP LEFT OUTER JOIN dbo.Vendor AS V ON AP.AccountLink = V.DCLink WHERE ABS(AP.fForeignOutstanding) > 0) as tbl GROUP BY DCLink ) as tblOut ON tblOut.DCLink=V.DCLink Left Join (SELECT V.DCLink,isnull(SUM(fQuantityLineTotInclForeign),0) as 'backLog' FROM [dbo].[InvNum] I,	[dbo].[Project]	P,	[dbo]._btblInvoiceLines	L,	[dbo].Vendor V	WHERE (DocState=3 or DocState=1) AND	UPPER(Description) LIKE '%PURCHASE%' AND	I.ProjectID=P.ProjectLink AND L.IinvoiceID=I.AutoIndex 	AND	I.AccountID=V.DCLink AND (fQtyProcessed<>fQuantity) GROUP BY V.DCLink ) as tblBcklg ON tblBcklg.DClink=V.DCLink Where V.DCLink=@dcLink END";
        sql = "BEGIN declare @dbCode varchar(10) declare @pVendorAcct varchar(255) declare @cRate float declare @dcLink int set @dbCode='DU' set @pVendorAcct='" + lblVendorAcct.Text.Trim() + "' if(@dbCode='UG') set @cRate=1 else select @cRate=fSellRate from dbo.CurrencyHist where iCurrencyID=1 and idCurrencyHist=(select max(idCurrencyHist) from dbo.CurrencyHist) select @dcLink=DCLink from dbo.Vendor where ltrim(rtrim(Account))=@pVendorAcct Select V.DCLink,V.Account,V.Name,Outstanding=isnull(tblOut.Outstanding,0),Terms=isnull(tblOut.Terms,0),CreditLimit=isnull(tblOut.CreditLimit,0),Backlog=isnull(tblBcklg.Backlog,0) From dbo.Vendor as V Left Join (Select DCLink,SUM(USDOutstanding) as 'Outstanding',Max(PaymentTerms) as 'Terms',Max(Credit_Limit) as 'CreditLimit' From (SELECT V.DCLink,AP.fForeignOutstanding AS 'USDOutstanding',CASE V.AccountTerms WHEN 0 THEN '1' WHEN 1 THEN '30' WHEN 2 THEN '45' WHEN 3 THEN '60' WHEN 4 THEN '90' WHEN 5 THEN '120' WHEN 6 THEN '150' END AS 'PaymentTerms',(V.Credit_Limit/@cRate) as 'Credit_Limit' FROM dbo.PostAP AS AP LEFT OUTER JOIN dbo.Vendor AS V ON AP.AccountLink = V.DCLink WHERE ABS(AP.fForeignOutstanding) > 0) as tbl GROUP BY DCLink ) as tblOut ON tblOut.DCLink=V.DCLink Left Join (SELECT V.DCLink,isnull(SUM(fQuantityLineTotInclForeign),0) as 'backLog' FROM [dbo].[InvNum] I,	[dbo].[Project]	P,	[dbo]._btblInvoiceLines	L,	[dbo].Vendor V	WHERE (DocState=3 or DocState=1) AND	UPPER(Description) LIKE '%PURCHASE%' AND	I.ProjectID=P.ProjectLink AND L.IinvoiceID=I.AutoIndex 	AND	I.AccountID=V.DCLink AND (fQtyProcessed<>fQuantity) GROUP BY V.DCLink ) as tblBcklg ON tblBcklg.DClink=V.DCLink Where V.DCLink=@dcLink END";

        Db.constr = myGlobal.getConnectionStringForDB("TRI");
        SqlDataReader drTmp = Db.myGetReader(sql);
        if (drTmp.HasRows)
        {
            drTmp.Read();
            lblVendorName.Text = drTmp["Name"].ToString() + " ( " + drTmp["Account"].ToString() + " ) ";
            if (drTmp["CreditLimit"] != DBNull.Value)
            {
                VendorCreditLimit = Convert.ToDouble(drTmp["CreditLimit"]);
                lblVendorCreditLimit.Text = string.Format("{0:n}", VendorCreditLimit);
            }
            else
                lblVendorCreditLimit.Text = "0";

            if (drTmp["backlog"] != DBNull.Value)
            {
                VendorAmountinbackLog = Convert.ToDouble(drTmp["backlog"]);
                lblVendorAmountinbackLog.Text = string.Format("{0:n}", VendorAmountinbackLog);
            }
            else
                lblVendorAmountinbackLog.Text = "0";

            if (drTmp["Outstanding"] != DBNull.Value)
            {
                VendorOutstandingAmount = Convert.ToDouble(drTmp["Outstanding"]);
                lblVendorOutstandingAmount.Text = string.Format("{0:n}", VendorOutstandingAmount);
            }
            else
                lblVendorOutstandingAmount.Text = "0";

            if (drTmp["Terms"] != DBNull.Value)
                lblVendorPaymentTerms.Text = drTmp["Terms"].ToString();
            else
                lblVendorPaymentTerms.Text = "0";

        }
        drTmp.Close();

        //work for items here 
        getStockLevelsByBU();
    }

    private void getStockLevelsByBU()
    {
        DataTable tblFinalBUWise = new DataTable();
        tblFinalBUWise.Columns.Add("BU", typeof(string));
        tblFinalBUWise.Columns.Add("TZ", typeof(string));
        tblFinalBUWise.Columns.Add("DU", typeof(string));
        tblFinalBUWise.Columns.Add("KE", typeof(string));
        tblFinalBUWise.Columns.Add("EPZ", typeof(string));
        tblFinalBUWise.Columns.Add("UG", typeof(string));

        double DU=0, TZ=0, KE=0, EPZ=0, UG=0;

        DataTable dtBUEVO;
        sql = "SELECT BU, isnull(round([DU],2),0) as DU,isnull(round([TZ],2),0) as TZ FROM (Select * from ((Select 'DU' as region,G.Description  as 'BU',TotValUSD=((S.Qty_On_Hand*AveUCst)/(Select top 1 fBuyRate from Triangle.dbo.CurrencyHist where iCurrencyId=1 Order by dRateDate desc)) From Triangle.dbo.stkItem as S Left Join Triangle.dbo.grpTbl as G on S.ItemGroup=G.StGroup )	UNION ALL (Select 'TZ' as region,G.Description as 'BU',TotValUSD=((S.Qty_On_Hand*AveUCst)/(Select top 1 fBuyRate from ReddotTanzania.dbo.CurrencyHist where iCurrencyId=1 Order by dRateDate desc)) From ReddotTanzania.dbo.stkItem as S Left Join ReddotTanzania.dbo.grpTbl as G on S.ItemGroup=G.StGroup )) as tbl Where BU ='" + lblBU.Text.Trim() + "') ps PIVOT (SUM (TotValUSD) FOR region IN ( [DU],[TZ])) AS pvt ";
        Db.constr = myGlobal.getConnectionStringForDB("JA");
        dtBUEVO = Db.myGetDS(sql).Tables[0];

        DataTable dtBUOB1;
        sql = "SELECT BU, isnull(round([KE],2),0) as KE,isnull(round([EPZ],2),0) as EPZ,isnull(round([UG],2),0) as UG FROM (Select * from ((Select 'KE' as region,G.Description  as 'BU',TotValUSD=((S.Qty_On_Hand*AveUCst)/(Select top 1 fBuyRate from [Red Dot Distribution Limited - Kenya].dbo.CurrencyHist where iCurrencyId=1 Order by dRateDate desc)) From [Red Dot Distribution Limited - Kenya].dbo.stkItem as S Left Join [Red Dot Distribution Limited - Kenya].dbo.grpTbl as G on S.ItemGroup=G.StGroup )UNION ALL (Select 'EPZ' as region,G.Description as 'BU', TotValUSD=((S.Qty_On_Hand*AveUCst)/(Select top 1 fBuyRate from [RED DOT DISTRIBUTION EPZ LTD].dbo.CurrencyHist where iCurrencyId=1 Order by dRateDate desc)) From [RED DOT DISTRIBUTION EPZ LTD].dbo.stkItem as S Left Join [RED DOT DISTRIBUTION EPZ LTD].dbo.grpTbl as G on S.ItemGroup=G.StGroup ) UNION ALL (Select 'UG' as region,G.Description as 'BU',TotValUSD=((S.Qty_On_Hand*AveUCst)/1) From [UgandaKE].dbo.stkItem as S Left Join [UgandaKE].dbo.grpTbl as G on S.ItemGroup=G.StGroup )) as tbl Where BU='" + lblBU.Text.Trim() + "' ) ps PIVOT (SUM (TotValUSD) FOR region IN ( [KE],[EPZ],[UG]) ) AS pvt ";
        Db.constr = myGlobal.getConnectionStringForDB("KE");
        dtBUOB1 = Db.myGetDS(sql).Tables[0];

        if (dtBUEVO.Rows.Count > 0)
        {
            DU = Convert.ToDouble(dtBUEVO.Rows[0]["DU"]);
            TZ = Convert.ToDouble(dtBUEVO.Rows[0]["TZ"]);
        }

        if (dtBUOB1.Rows.Count > 0)
        {
            KE = Convert.ToDouble(dtBUOB1.Rows[0]["KE"]);
            EPZ = Convert.ToDouble(dtBUOB1.Rows[0]["EPZ"]);
            UG = Convert.ToDouble(dtBUOB1.Rows[0]["UG"]);
        }

        //for (int i = 0; i < dtBUEVO.Rows.Count; i++)
        //tblFinalBUWise.Rows.Add(dtBUEVO.Rows[0]["BU"], string.Format("{0:n}", DU).ToString(), string.Format("{0:n}", TZ).ToString(), string.Format("{0:n}", KE).ToString(), string.Format("{0:n}", EPZ).ToString(), string.Format("{0:n}", UG).ToString());
        tblFinalBUWise.Rows.Add(lblBU.Text.Trim(), string.Format("{0:n}", DU).ToString(), string.Format("{0:n}", TZ).ToString(), string.Format("{0:n}", KE).ToString(), string.Format("{0:n}", EPZ).ToString(), string.Format("{0:n}", UG).ToString());

        GridViewBU.DataSource = tblFinalBUWise;
        GridViewBU.DataBind();

    }
    private void getStockLevelsByProduct()
    {
        DataTable tblFinalPrductWise = new DataTable();
        tblFinalPrductWise.Columns.Add("cSimpleCode", typeof(string));
        tblFinalPrductWise.Columns.Add("TZ", typeof(string));
        tblFinalPrductWise.Columns.Add("DU", typeof(string));
        tblFinalPrductWise.Columns.Add("KE", typeof(string));
        tblFinalPrductWise.Columns.Add("EPZ", typeof(string));
        tblFinalPrductWise.Columns.Add("UG", typeof(string));
        tblFinalPrductWise.Columns.Add("Order Qty", typeof(string));

        string itmList = "", itm = "", Ord_qty = "";
        double DU, TZ, KE, EPZ, UG;
        Label lblTmpPrt, lblTmpOrdQty;

        //gridDetails1  col3 item
        for (int i = 0; i < gridDetails1.Rows.Count; i++)
        {
            lblTmpPrt = (Label)gridDetails1.Rows[i].FindControl("lblPartNo");
            
            if (lblTmpPrt.Text.IndexOf('/') >= 0)
                itm = lblTmpPrt.Text.Substring(0, lblTmpPrt.Text.IndexOf('/'));
            else
                itm = lblTmpPrt.Text;

            if (itmList == "")
                itmList = "'" + itm + "'";
            else
                itmList += ",'" + itm + "'";

            lblTmpOrdQty = (Label)gridDetails1.Rows[i].FindControl("lblQty");
            Ord_qty = lblTmpOrdQty.Text;

            tblFinalPrductWise.Rows.Add(itm, 0, 0, 0, 0, 0, Ord_qty);
        }
        if (itmList.Trim() != "")
        {
            DataTable dtBUEVO;
            sql = "SELECT cSimpleCode, isnull(round([DU],2),0) as DU,isnull(round([TZ],2),0) as TZ FROM (Select * from ((Select 'DU' as region,cSimpleCode,Qty_On_Hand From Triangle.dbo.stkItem Where cSimpleCode in (" + itmList + ")) UNION ALL (Select 'TZ' as region,cSimpleCode,Qty_On_Hand From ReddotTanzania.dbo.stkItem Where cSimpleCode in (" + itmList + "))) as tbl ) ps PIVOT (SUM (Qty_On_Hand) FOR region IN ( [DU],[TZ])) AS pvt ";
            Db.constr = myGlobal.getConnectionStringForDB("JA");
            dtBUEVO = Db.myGetDS(sql).Tables[0];


            for (int i = 0; i < dtBUEVO.Rows.Count; i++)
            {
                for (int j = 0; j < tblFinalPrductWise.Rows.Count; j++)
                {
                    //if (dtBUEVO.Rows[i]["cSimpleCode"].ToString() == tblFinalPrductWise.Rows[j]["cSimpleCode"].ToString())
                    if (dtBUEVO.Rows[i]["cSimpleCode"].ToString() == tblFinalPrductWise.Rows[j]["cSimpleCode"].ToString())
                    {
                        DU = Convert.ToDouble(dtBUEVO.Rows[i]["DU"]);
                        TZ = Convert.ToDouble(dtBUEVO.Rows[i]["TZ"]);

                        tblFinalPrductWise.Rows[j]["TZ"] = TZ.ToString();
                        tblFinalPrductWise.Rows[j]["DU"] = DU.ToString();
                        break;
                    }
                }
            }

            DataTable dtBUOB1;
            sql = "SELECT cSimpleCode, isnull(round([KE],2),0) as KE,isnull(round([EPZ],2),0) as EPZ, isnull(round([UG],2),0) as UG FROM (Select * from ((Select 'KE' as region,cSimpleCode,Qty_On_Hand From [Red Dot Distribution Limited - Kenya].dbo.stkItem Where cSimpleCode in(" + itmList + ")) UNION ALL (Select 'EPZ' as region,cSimpleCode,Qty_On_Hand From [RED DOT DISTRIBUTION EPZ LTD].dbo.stkItem Where cSimpleCode in(" + itmList + ") ) UNION ALL (Select 'UG' as region,cSimpleCode,Qty_On_Hand From [UgandaKE].dbo.stkItem Where cSimpleCode in(" + itmList + "))) as tbl ) ps PIVOT (SUM (Qty_On_Hand) FOR region IN ( [KE],[EPZ],[UG])) AS pvt ";
            Db.constr = myGlobal.getConnectionStringForDB("KE");
            dtBUOB1 = Db.myGetDS(sql).Tables[0];

            for (int i = 0; i < dtBUOB1.Rows.Count; i++)
            {
                for (int j = 0; j < tblFinalPrductWise.Rows.Count; j++)
                {
                    if (dtBUOB1.Rows[i]["cSimpleCode"].ToString() == tblFinalPrductWise.Rows[j]["cSimpleCode"].ToString())
                    {
                        KE = Convert.ToDouble(dtBUOB1.Rows[i]["KE"]);
                        EPZ = Convert.ToDouble(dtBUOB1.Rows[i]["EPZ"]);
                        UG = Convert.ToDouble(dtBUOB1.Rows[i]["UG"]);

                        tblFinalPrductWise.Rows[j]["KE"] = KE.ToString();
                        tblFinalPrductWise.Rows[j]["EPZ"] = EPZ.ToString();
                        tblFinalPrductWise.Rows[j]["UG"] = UG.ToString();
                        break;
                    }
                }
            }

            GridViewProducts.DataSource = tblFinalPrductWise;
            GridViewProducts.DataBind();
           
            for(int i=0;i<GridViewProducts.Rows.Count;i++)
              GridViewProducts.Rows[i].Cells[6].ForeColor = System.Drawing.Color.Red;
        }
    }
    private void getStockLevelsByProductOrg()
    {
        DataTable tblFinalPrductWise = new DataTable();
        tblFinalPrductWise.Columns.Add("cSimpleCode", typeof(string));
        tblFinalPrductWise.Columns.Add("TZ", typeof(string));
        tblFinalPrductWise.Columns.Add("DU", typeof(string));
        tblFinalPrductWise.Columns.Add("KE", typeof(string));
        tblFinalPrductWise.Columns.Add("EPZ", typeof(string));
        tblFinalPrductWise.Columns.Add("UG", typeof(string));

        string itmList = "",itm="";
        double DU, TZ, KE, EPZ, UG;
        Label lblTmpPrt;

        //gridDetails1  col3 item
        for (int i = 0; i < gridDetails1.Rows.Count; i++)
        {
            lblTmpPrt = (Label)gridDetails1.Rows[i].FindControl("lblPartNo");

            if (lblTmpPrt.Text.IndexOf('/') >= 0)
                itm = lblTmpPrt.Text.Substring(0, lblTmpPrt.Text.IndexOf('/'));
            else
                itm = lblTmpPrt.Text;

            if (itmList == "")
                itmList = "'" + itm + "'";
            else
                itmList += ",'" + itm + "'";

            tblFinalPrductWise.Rows.Add(itm, 0, 0, 0, 0, 0);
        }
        if (itmList.Trim() != "")
        {
            DataTable dtBUEVO;
            sql = "SELECT cSimpleCode, isnull(round([DU],2),0) as DU,isnull(round([TZ],2),0) as TZ FROM (Select * from ((Select 'DU' as region,cSimpleCode,Qty_On_Hand From Triangle.dbo.stkItem Where cSimpleCode in (" + itmList + ")) UNION ALL (Select 'TZ' as region,cSimpleCode,Qty_On_Hand From ReddotTanzania.dbo.stkItem Where cSimpleCode in (" + itmList + "))) as tbl ) ps PIVOT (SUM (Qty_On_Hand) FOR region IN ( [DU],[TZ])) AS pvt ";
            Db.constr = myGlobal.getConnectionStringForDB("JA");
            dtBUEVO = Db.myGetDS(sql).Tables[0];


            for (int i = 0; i < dtBUEVO.Rows.Count; i++)
            {
                for (int j = 0; j < tblFinalPrductWise.Rows.Count; j++)
                {
                    //if (dtBUEVO.Rows[i]["cSimpleCode"].ToString() == tblFinalPrductWise.Rows[j]["cSimpleCode"].ToString())
                    if (dtBUEVO.Rows[i]["cSimpleCode"].ToString() == tblFinalPrductWise.Rows[j]["cSimpleCode"].ToString())
                    {
                        DU = Convert.ToDouble(dtBUEVO.Rows[i]["DU"]);
                        TZ = Convert.ToDouble(dtBUEVO.Rows[i]["TZ"]);

                        tblFinalPrductWise.Rows[j]["TZ"] = TZ.ToString();
                        tblFinalPrductWise.Rows[j]["DU"] = DU.ToString();
                        break;
                    }
                }
            }

            DataTable dtBUOB1;
            sql = "SELECT cSimpleCode, isnull(round([KE],2),0) as KE,isnull(round([EPZ],2),0) as EPZ, isnull(round([UG],2),0) as UG FROM (Select * from ((Select 'KE' as region,cSimpleCode,Qty_On_Hand From [Red Dot Distribution Limited - Kenya].dbo.stkItem Where cSimpleCode in(" + itmList + ")) UNION ALL (Select 'EPZ' as region,cSimpleCode,Qty_On_Hand From [RED DOT DISTRIBUTION EPZ LTD].dbo.stkItem Where cSimpleCode in(" + itmList + ") ) UNION ALL (Select 'UG' as region,cSimpleCode,Qty_On_Hand From [UgandaKE].dbo.stkItem Where cSimpleCode in(" + itmList + "))) as tbl ) ps PIVOT (SUM (Qty_On_Hand) FOR region IN ( [KE],[EPZ],[UG])) AS pvt ";
            Db.constr = myGlobal.getConnectionStringForDB("KE");
            dtBUOB1 = Db.myGetDS(sql).Tables[0];

            for (int i = 0; i < dtBUOB1.Rows.Count; i++)
            {
                for (int j = 0; j < tblFinalPrductWise.Rows.Count; j++)
                {
                    if (dtBUOB1.Rows[i]["cSimpleCode"].ToString() == tblFinalPrductWise.Rows[j]["cSimpleCode"].ToString())
                    {
                        KE = Convert.ToDouble(dtBUOB1.Rows[i]["KE"]);
                        EPZ = Convert.ToDouble(dtBUOB1.Rows[i]["EPZ"]);
                        UG = Convert.ToDouble(dtBUOB1.Rows[i]["UG"]);

                        tblFinalPrductWise.Rows[j]["KE"] = KE.ToString();
                        tblFinalPrductWise.Rows[j]["EPZ"] = EPZ.ToString();
                        tblFinalPrductWise.Rows[j]["UG"] = UG.ToString();
                        break;
                    }
                }
            }

            GridViewProducts.DataSource = tblFinalPrductWise;
            GridViewProducts.DataBind();
        }
    }

    private void getStockLevelsByProduct_Old()  //till 27-may-2013
    {
        DataTable tblFinalPrductWise = new DataTable();
        tblFinalPrductWise.Columns.Add("cSimpleCode", typeof(string));
        tblFinalPrductWise.Columns.Add("TZ", typeof(string));
        tblFinalPrductWise.Columns.Add("DU", typeof(string));
        tblFinalPrductWise.Columns.Add("KE", typeof(string));
        tblFinalPrductWise.Columns.Add("EPZ", typeof(string));
        tblFinalPrductWise.Columns.Add("UG", typeof(string));

        string itmList = "";
        double DU, TZ, KE, EPZ, UG;
        Label lblTmpPrt;

        //gridDetails1  col3 item
        for (int i = 0; i < gridDetails1.Rows.Count; i++)
        {
            lblTmpPrt = (Label)gridDetails1.Rows[i].FindControl("lblPartNo");

            if (itmList == "")
                itmList = "'" + lblTmpPrt.Text + "'";
            else
                itmList += ",'" + lblTmpPrt.Text + "'";

            tblFinalPrductWise.Rows.Add(lblTmpPrt.Text, 0, 0, 0, 0, 0);
        }

        if (itmList.Trim() != "")
        {
            DataTable dtBUEVO;
            sql = "SELECT cSimpleCode, isnull(round([DU],2),0) as DU,isnull(round([TZ],2),0) as TZ FROM (Select * from ((Select 'DU' as region,cSimpleCode,TotValUSD=((Qty_On_Hand*AveUCst)/(Select top 1 fBuyRate from Triangle.dbo.CurrencyHist where iCurrencyId=1 Order by dRateDate desc)) From Triangle.dbo.stkItem ) UNION ALL (Select 'TZ' as region,cSimpleCode,TotValUSD=((Qty_On_Hand*AveUCst)/(Select top 1 fBuyRate from ReddotTanzania.dbo.CurrencyHist where iCurrencyId=1 Order by dRateDate desc)) From ReddotTanzania.dbo.stkItem )) as tbl Where cSimpleCode in(" + itmList + ")) ps PIVOT (SUM (TotValUSD) FOR region IN ( [DU],[TZ])) AS pvt ";
            Db.constr = myGlobal.getConnectionStringForDB("JA");
            dtBUEVO = Db.myGetDS(sql).Tables[0];


            for (int i = 0; i < dtBUEVO.Rows.Count; i++)
            {
                for (int j = 0; j < tblFinalPrductWise.Rows.Count; j++)
                {
                    if (dtBUEVO.Rows[i]["cSimpleCode"].ToString() == tblFinalPrductWise.Rows[j]["cSimpleCode"].ToString())
                    {
                        DU = Convert.ToDouble(dtBUEVO.Rows[i]["DU"]);
                        TZ = Convert.ToDouble(dtBUEVO.Rows[i]["TZ"]);

                        tblFinalPrductWise.Rows[j]["TZ"] = string.Format("{0:n}", TZ).ToString();
                        tblFinalPrductWise.Rows[j]["DU"] = string.Format("{0:n}", DU).ToString();
                        break;
                    }
                }
            }

            DataTable dtBUOB1;
            sql = "SELECT cSimpleCode, isnull(round([KE],2),0) as KE,isnull(round([EPZ],2),0) as EPZ, isnull(round([UG],2),0) as UG FROM (Select * from ((Select 'KE' as region,cSimpleCode,TotValUSD=((Qty_On_Hand*AveUCst)/(Select top 1 fBuyRate from [Red Dot Distribution Limited - Kenya].dbo.CurrencyHist where iCurrencyId=1 Order by dRateDate desc)) From [Red Dot Distribution Limited - Kenya].dbo.stkItem ) UNION ALL (Select 'EPZ' as region,cSimpleCode,TotValUSD=((Qty_On_Hand*AveUCst)/(Select top 1 fBuyRate from [RED DOT DISTRIBUTION EPZ LTD].dbo.CurrencyHist where iCurrencyId=1 Order by dRateDate desc)) From [RED DOT DISTRIBUTION EPZ LTD].dbo.stkItem) UNION ALL (Select 'UG' as region,cSimpleCode,TotValUSD=((Qty_On_Hand*AveUCst)/1) From [UgandaKE].dbo.stkItem )) as tbl Where cSimpleCode in(" + itmList + ")) ps PIVOT (SUM (TotValUSD) FOR region IN ( [KE],[EPZ],[UG])) AS pvt ";
            Db.constr = myGlobal.getConnectionStringForDB("KE");
            dtBUOB1 = Db.myGetDS(sql).Tables[0];

            for (int i = 0; i < dtBUOB1.Rows.Count; i++)
            {
                for (int j = 0; j < tblFinalPrductWise.Rows.Count; j++)
                {
                    if (dtBUOB1.Rows[i]["cSimpleCode"].ToString() == tblFinalPrductWise.Rows[j]["cSimpleCode"].ToString())
                    {
                        KE = Convert.ToDouble(dtBUOB1.Rows[i]["KE"]);
                        EPZ = Convert.ToDouble(dtBUOB1.Rows[i]["EPZ"]);
                        UG = Convert.ToDouble(dtBUOB1.Rows[i]["UG"]);

                        tblFinalPrductWise.Rows[j]["KE"] = string.Format("{0:n}", KE).ToString();
                        tblFinalPrductWise.Rows[j]["EPZ"] = string.Format("{0:n}", EPZ).ToString();
                        tblFinalPrductWise.Rows[j]["UG"] = string.Format("{0:n}", UG).ToString();
                        break;
                    }
                }
            }

            //for (int i = 0; i < dtBUEVO.Rows.Count; i++)
            //   tblFinalPrductWise.Rows.Add(dtBUEVO.Rows[0]["cSimpleCode"], Convert.ToDouble(dtBUEVO.Rows[0]["DU"]), Convert.ToDouble(dtBUEVO.Rows[0]["TZ"]), Convert.ToDouble(dtBUOB1.Rows[0]["KE"]), Convert.ToDouble(dtBUOB1.Rows[0]["EPZ"]), Convert.ToDouble(dtBUOB1.Rows[0]["UG"]));


            GridViewProducts.DataSource = tblFinalPrductWise;
            GridViewProducts.DataBind();
        }
    }
    
    private string setConditionAccordingToRole()
    {
        string loginConditionField = "", loginConditionQuerySuffix="";

        if (qrls.ToUpper() == "PRODUCTMANAGER")
            {
                loginConditionField = "bu";  //PM    login case use this field
                loginConditionQuerySuffix = " and " + loginConditionField + " like '%'+'" + lblBU.Text + "'+'%'";
            }
        else if (qrls.ToUpper() == "COUNTRYMANAGER" || qrls.ToUpper() == "PRODUCTSPECIALIST")
            {
                loginConditionField = "dbCode";  //CM, PS    login case use this field
                loginConditionQuerySuffix = " and " + loginConditionField + " like '%'+'" + lbldbCode.Text + "'+'%'";
            }
            else //role other than PRODUCTMANAGER,COUNTRYMANAGER,PRODUCTSPECIALIST
            {
                //no need of field as query will not be suffixed to filter record
                loginConditionQuerySuffix = "";
            }

        return "";
    }

    private void processNow(string actionString)
    {
        mailList = "";

        if (actionString.ToLower() == "decline")
        
            sql = "exec [dbo].[getEmaillistforPOEscalation] " + Session["currStatusId"].ToString() + ",'" + lblBU.Text + "','" + lbldbCode.Text + "',0," + pid;   // 0/1 stands for direction 0 is prev, 1 is next
        else
            sql = "exec [dbo].[getEmaillistforPOEscalation] " + Session["currStatusId"].ToString() + ",'" + lblBU.Text + "','" + lbldbCode.Text + "',1," + pid;   // 0/1 stands for direction 0 is prev, 1 is next            Db.constr = myGlobal.getIntranetDBConnectionString();

        Db.constr = myGlobal.getIntranetDBConnectionString();
        dr = Db.myGetReader(sql);
        
        Boolean flgworked = false;
        
            while (dr.Read())
            {
                    newStatusId = dr["toStatusId"].ToString();
                    newescalateLevelId = dr["toRoleId"].ToString();

                    if (dr["Email"] != DBNull.Value)
                    {
                        if (mailList == "")
                            mailList = dr["Email"].ToString();
                        else
                            mailList += ";" + dr["Email"].ToString();
                    }
                    flgworked = true;
            }
            dr.Close();
        
         if (!flgworked)  //old verion to get roles
        {
            if (actionString.ToLower() == "decline")
            {
                sql = "select p.prevProcessStatusId,p.prevRole,isnull(r.roleName,'') as roleName,isnull(r.emailList" + lbldbCode.Text + ",'') as roleEmail from dbo.processStatus as p left join dbo.roles as r on p.prevRole=r.roleId where p.processStatusId=" + Session["currStatusId"].ToString() + " and p.fk_processId=" + pid;
                Db.constr = myGlobal.getIntranetDBConnectionString();
                dr = Db.myGetReader(sql);
                if (dr.HasRows)
                {
                    dr.Read();
                    newStatusId = dr["prevProcessStatusId"].ToString();
                    newescalateLevelId = dr["prevRole"].ToString();
                    mailList = dr["roleEmail"].ToString(); //not required though
                    dr.Close();
                }
            }
            else
            {
                sql = "select p.nextProcessStatusId,p.nextRole,isnull(r.roleName,'') as roleName,isnull(r.emailList" + lbldbCode.Text + ",'') as roleEmail from dbo.processStatus as p left join dbo.roles as r on p.nextRole=r.roleId where p.processStatusId=" + Session["currStatusId"].ToString() + " and p.fk_processId=" + pid;
                Db.constr = myGlobal.getIntranetDBConnectionString();
                dr = Db.myGetReader(sql);
                if (dr.HasRows)
                {
                    dr.Read();
                    newStatusId = dr["nextProcessStatusId"].ToString();
                    newescalateLevelId = dr["nextRole"].ToString();
                    mailList = dr["roleEmail"].ToString(); //not required though
                    dr.Close();
                }
            }
        }

        if (newescalateLevelId == "0") //if next role is going to be 0 , means there is no role actually, select email for the role of 1 stauts of the paricular process
        {
            ///// qry gets email of all the users involved + vendorbu email id 
            sql = "select Email from " + myGlobal.getcurrentMembershipDBName() + ".dbo.aspnet_Membership where userId in(select userId from " + myGlobal.getcurrentMembershipDBName() + ".dbo.aspnet_Users where username in (select distinct(lastUpdatedBy) from dbo.processStatusTrack where fk_processRequestID=(select processRequestId from dbo.processRequest where refId=" + refId + ") and lastUpdatedBy<>'" + myGlobal.loggedInUser() + "')) ";
            sql += " UNION select Email from orderSystemUserMapping where membershipUserRoleName='EVOENTRY' and dbCode like '%" + lbldbCode.Text + "%'";
            
            Db.constr = myGlobal.getIntranetDBConnectionString();
            dr = Db.myGetReader(sql);

            if (dr.HasRows)
            {
                mailList = "";

                //if else lines  added for order booked intimation 19-sep-2013
                if (lbldbCode.Text == "TZ" || lbldbCode.Text == "TRI")
                    mailList = myGlobal.getAppSettingsDataForKey("orderBookedIntimationMailIdTZ");
                else
                    mailList = myGlobal.getAppSettingsDataForKey("orderBookedIntimationMailIdKE");

                    while (dr.Read())
                    {
                        if (mailList == "")
                            mailList = dr["Email"].ToString();
                        else
                            mailList += ";" + dr["Email"].ToString();

                    }
                dr.Close();

            }


        }

        sql = "update dbo.processRequest set fk_StatusId=" + newStatusId + ", fk_escalateLevelId=" + newescalateLevelId + ",ByUser='" + myGlobal.loggedInUser().ToString() + "',comments='" + txtComments.Text.Trim() + "',lastModified='" + DateTime.Now.ToString("yyyy-MM-dd hh:mm:ss tt") + "' where refId=" + refId + " and fk_processId=" + pid;
        
        //Db.constr = myGlobal.getIntranetDBConnectionString();
        //Db.myExecuteSQL(sql);
        //sql += "; insert into processStatusTrack(fk_processRequestId,action_StatusID,fk_statusId,fk_EscalateLevelId,lastUpdatedBy,StatusAccept,lastModified,comments,fk_processId) values(" + Session["processRequestId"].ToString() + "," + Session["currStatusId"].ToString() + "," + newStatusId + "," + newescalateLevelId + ",'" + myGlobal.loggedInUser().ToString() + "','" + actionString + "','" + DateTime.Now.ToString("MM-dd-yyyy hh:mm:ss tt") + "','" + txtComments.Text.Trim() + "'," + pid + ")";

        sql += "; insert into processStatusTrack(fk_processRequestId,action_StatusID,fk_statusId,fk_EscalateLevelId,lastUpdatedBy,StatusAccept,lastModified,comments,fk_processId,mailTo,ccTo) values(" + Session["processRequestId"].ToString() + "," 
            + Session["currStatusId"].ToString() + "," + newStatusId + "," + newescalateLevelId + ",'" + myGlobal.loggedInUser().ToString() + "','" + actionString + "','" + DateTime.Now.ToString("MM-dd-yyyy hh:mm:ss tt") + "','" + txtComments.Text.Trim() + "'," + pid + ",'" + mailList + "','" + userEmail + "')";
        
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

    
    private void updatesOnHoldCase()
    {
        sql = "select p.nextProcessStatusId,p.nextRole from dbo.processStatus as p  where p.processStatusId=(" + Session["currStatusId"].ToString() + "-1) and p.fk_processId=" + pid;
        Db.constr = myGlobal.getIntranetDBConnectionString();
        dr = Db.myGetReader(sql);
        if (dr.HasRows)
        {
            dr.Read();
            newStatusId = dr["nextProcessStatusId"].ToString();
            newescalateLevelId = dr["nextRole"].ToString();
            dr.Close();
        }

        sql = "update dbo.processRequest set fk_StatusId=" + newStatusId + ", fk_escalateLevelId=" + newescalateLevelId + ",ByUser='" + myGlobal.loggedInUser().ToString() + "',comments='On Hold, " + txtComments.Text.Trim() + "',lastModified='" + DateTime.Now.ToString("yyyy-MM-dd hh:mm:ss tt") + "' where refId=" + refId + " and fk_processId=" + pid;
        sql += "; insert into processStatusTrack(fk_processRequestId,action_StatusID,fk_statusId,fk_EscalateLevelId,lastUpdatedBy,StatusAccept,lastModified,comments,fk_processId) values(" + Session["processRequestId"].ToString() + "," + Session["currStatusId"].ToString() + "," + newStatusId + "," + newescalateLevelId + ",'" + myGlobal.loggedInUser().ToString() + "','" + "On Hold" + "','" + DateTime.Now.ToString("MM-dd-yyyy hh:mm:ss tt") + "','On Hold, " + txtComments.Text.Trim() + "'," + pid + ")";
        Db.constr = myGlobal.getIntranetDBConnectionString();
        Db.myExecuteSQL(sql);

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

        strtmp = "";
        //strtmp = "Test Orders in -------------- Training session for KE : <b><br/><br/>";
        strtmp += "FPO Reference : <b>" + txtFpoRef.Text.Trim() + "</b><br/>";
        strtmp += "Region/Country : <b>" + lbldbCode.Text + "</b><br/>";
        strtmp += "System Order ID : <b>" + refId + "</b><br/>";

        strtmp += "Vendor [BU]: <b>" + lblVendor.Text + " [" + lblBU.Text + "]</b><br/></br>";
        
        if (lblCustomer.Text.Trim().ToUpper() != "NA")
            strtmp += "Customer : <b>" + lblCustomer.Text + " ( " + lblCustAcct.Text + ")</b><br/></br>";

        strtmp += "Updation comments : <b>" + txtComments.Text + "</b><br/></br>";
        strtmp += "Updated By : <b>" + myGlobal.loggedInUser() + "</b><br/>";

        strtmp += "Escalated to Level : <b>" + nxtStatus + "</b><br/>";

        if (fls.Trim() != "")
            strtmp += " <br/>Please Find the Updated files attached to this mail<br/><br/>";



        return strtmp;
    }

    protected bool updateOrderInfo()
    {
        string qry, evoPoNo, PODate, reqDelDate, opgCode, cbnNo, productmanager, headOfFinance;

        //if (userRole.Contains("productManagerCordinator".ToUpper()))  //these are only verified if it is the said role , usually it will be the last stage
        if (qrls.ToUpper() == "productManagerCordinator".ToUpper())  //these are only verified if it is the said role , usually it will be the last stage
        {
            if (txtEvoPONO.Text.Trim() == "" || txtEvoPONO.Text.Trim().ToUpper() == "UNASSIGNED")
            {
                lblMsg.Text = "Error ! Field EVO PO no. can't be empty or UNASSIGNED,  To book this order please provide correct EVO PO no.";
                //MsgBoxControl1.show(lblMsg.Text, "Error !!!");
                lblError.Text = lblMsg.Text;
                return false;
            }

            if (txtOPGCode.Text.Trim() == "" || txtOPGCode.Text.Trim().ToUpper() == "UNASSIGNED")
            {
                lblMsg.Text = "Error ! Field OPG Code can't be empty or UNASSIGNED, To book this order please provide correct OPG Code.";
                lblError.Text = lblMsg.Text;
                //MsgBoxControl1.show(lblMsg.Text, "Error !!!");
                return false;
            }

            if (txtCBNNo.Text.Trim() == "" || txtCBNNo.Text.Trim().ToUpper() == "UNASSIGNED")
            {
                lblMsg.Text = "Error ! Field Ship To. can't be empty  or UNASSIGNED, To book this order please provide Ship To. information";
                lblError.Text = lblMsg.Text;
                //MsgBoxControl1.show(lblMsg.Text, "Error !!!");
                return false;
            }
        }

      
        if (qrls.ToUpper() == "productManagerCordinator".ToUpper())  //these are only verified if it is the said role , usually it will be the last stage
        {
            if (txtFpoRef.Text.Trim() == "" || txtFpoRef.Text.Trim().ToUpper() == "UNASSIGNED")
            {
                lblMsg.Text = "Error ! Field FPO no. can't be empty or UNASSIGNED,  To book this order please provide correct FPO no.";
                //MsgBoxControl1.show(lblMsg.Text, "Error !!!");
                lblError.Text = lblMsg.Text;
                return false;
            }
        }
        if (txtEvoPONO.Text.Trim() == "")
        {
            txtEvoPONO.Text = "UNASSIGNED";
        }

        if (!Util.IsValidDate(txtPODate.Text.Trim()))
        {
            lblMsg.Text = "Error ! Invalid PO Date";
            //MsgBoxControl1.show(lblMsg.Text, "Error !!!");
            lblError.Text = lblMsg.Text;
            return false;
        }

        if (!Util.IsValidDate(txtReqDelDate.Text.Trim()))
        {
            lblMsg.Text = "Error ! Invalid PO Delivery Date";
            //MsgBoxControl1.show(lblMsg.Text, "Error !!!");
            lblError.Text = lblMsg.Text;
            return false;
        }

        if (txtOPGCode.Text.Trim() == "")
        {
            txtOPGCode.Text = "UNASSIGNED";
        }

        if (txtCBNNo.Text.Trim() == "")
        {
            txtCBNNo.Text = "UNASSIGNED";
        }

        if (txtProdctManager.Text.Trim() == "")
        {
            txtProdctManager.Text = "UNASSIGNED";
        }
        if (txtHeadOfOffice.Text.Trim() == "")
        {
            txtHeadOfOffice.Text = "UNASSIGNED";
        }

        //if (userRole.Contains("headOfFinance".ToUpper()))
        if (qrls.ToUpper() == "headOfFinance".ToUpper())
        {
            txtHeadOfOffice.Text = myGlobal.loggedInUser();
            lblHeadOfOffice.Text = myGlobal.loggedInUser();
        }

        //if (userRole.Contains("productManager".ToUpper()))
        if (qrls.ToUpper() == "productManager".ToUpper())
        {
            txtProdctManager.Text = myGlobal.loggedInUser();
            lblProdctManager.Text = myGlobal.loggedInUser();
        }

        evoPoNo = checkSingleQuote(txtEvoPONO.Text);
        PODate = checkSingleQuote(txtPODate.Text);
        reqDelDate = checkSingleQuote(txtReqDelDate.Text);
        opgCode = checkSingleQuote(txtOPGCode.Text);
        cbnNo = checkSingleQuote(txtCBNNo.Text);
        productmanager = checkSingleQuote(txtProdctManager.Text);
        headOfFinance = checkSingleQuote(txtHeadOfOffice.Text);

        //txtComments.Text = "Vendor - " + lblVendor.Text + ", " + txtComments.Text;
        txtComments.Text = txtComments.Text;

        qry = "update dbo.processRequest set refValue='" + evoPoNo + "' where refId=" + lblOrderId.Text;
        qry = qry + " ; update dbo.PurchaseOrders set evoPoNo='" + evoPoNo + "',PoDate='" + PODate + "',reqDelDate='" + reqDelDate + "',opgCode='" + opgCode + "',cbnNo='" + cbnNo + "',productManager='" + productmanager + "',headOfFinance='" + headOfFinance + "',comments='" + txtComments.Text + "',fpoNo='" + txtFpoRef.Text.Trim() + "',cbnName='" + ddlCBNName.SelectedItem.Text + "' where poId=" + lblOrderId.Text + "";
        Db.constr = myGlobal.getIntranetDBConnectionString();
        Db.myExecuteSQL(qry);
        return true;
    }

    //protected bool updateOrderInfoOld()
    //{
    //    if (txtEvoPONO.Text.Trim() == "" || txtEvoPONO.Text.Trim().ToUpper() == "UNASSIGNED")
    //    {
    //        lblMsg.Text = "Error ! Field EVO PO no. can't be empty or UNASSIGNED";
    //        return false;
    //    }

    //    if (!Util.IsValidDate(txtPODate.Text.Trim()))
    //    {
    //        lblMsg.Text = "Error ! Invalid PO Date";
    //        return false;
    //    }

    //    if (!Util.IsValidDate(txtReqDelDate.Text.Trim()))
    //    {
    //        lblMsg.Text = "Error ! Invalid PO Delivery Date";
    //        return false;
    //    }

    //    if (txtOPGCode.Text.Trim() == "")
    //    {
    //        lblMsg.Text = "Error ! Field OPG Codecan't be empty";
    //        return false;
    //    }

    //    if (txtCBNNo.Text.Trim() == "")
    //    {
    //        lblMsg.Text = "Error ! Field CBN No. can't be empty";
    //        return false;
    //    }

    //    if (txtProdctManager.Text.Trim() == "")
    //    {
    //        lblMsg.Text = "Error ! Field Prodct Manager can't be empty";
    //        return false;
    //    }
    //    if (txtHeadOfOffice.Text.Trim() == "")
    //    {
    //        lblMsg.Text = "Error ! Field HEAD OF FINANCE can't be empty";
    //        return false;
    //    }
    //    string qry, evoPoNo, PODate, reqDelDate, opgCode, cbnNo, productmanager, headOfFinance;
    //    evoPoNo = checkSingleQuote(txtEvoPONO.Text);
    //    PODate = checkSingleQuote(txtPODate.Text);
    //    reqDelDate = checkSingleQuote(txtReqDelDate.Text);
    //    opgCode = checkSingleQuote(txtOPGCode.Text);
    //    cbnNo = checkSingleQuote(txtCBNNo.Text);
    //    productmanager = checkSingleQuote(txtProdctManager.Text);
    //    headOfFinance = checkSingleQuote(txtHeadOfOffice.Text);

    //    qry = "update dbo.processRequest set refValue='" + evoPoNo + "' where refId=" + lblOrderId.Text;
    //    qry = qry + " ; update dbo.PurchaseOrders set evoPoNo='" + evoPoNo + "',PoDate='" + PODate + "',reqDelDate='" + reqDelDate + "',opgCode='" + opgCode + "',cbnNo='" + cbnNo + "',productManager='" + productmanager + "',headOfFinance='" + headOfFinance + "' where poId=" + lblOrderId.Text + "";
    //    Db.constr = myGlobal.getIntranetDBConnectionString();
    //    Db.myExecuteSQL(qry);
    //    return true;
    //}

    protected string checkSingleQuote(string val)
    {
        string retVal;
        retVal = val.Replace("'", "''");
        return retVal;
    }

    private Boolean varifyMandatoryUploadCase()
    {
        Boolean flg = false;
        int cnt = 0;
        HttpFileCollection files = Request.Files;
        HttpPostedFile postFile;

        for (int i = 0; i < files.Count; i++)
        {
            postFile = files[i];
            if (postFile.ContentLength > 0)
            {
                cnt++;
            }
        }

        if (cnt > 0)
            flg = true;

        return flg;
    }

    private Boolean setMandatoryUploadCheckedStatus()
    {
        Boolean flg = false;
        int cnt = 0;
        HttpFileCollection files = Request.Files;
        HttpPostedFile postFile;

        for (int i = 0; i < files.Count; i++)
        {
            postFile = files[i];
            if (postFile.ContentLength > 0)
            {
                cnt++;
                chkUploadFilesWish.Checked = true;
                break;
            }
        }

        if (cnt > 0)
            flg = true;

        return flg;
    }

    protected void btnConfirm_Click(object sender, EventArgs e)
    {
        ClearTextBox();

        if (chkUploadFilesWish.Checked == false)
            setMandatoryUploadCheckedStatus();
        try
        {
            txtComments.Text = txtComments.Text.Trim();
            if (txtComments.Text.Length >= 3900)
            {
                lblMsg.Text = "Comments field value can't exceed 4000 characters.";
                //MsgBoxControl1.show(lblMsg.Text, "Error !!!");
                return;
            }

            //if (txtComments.Text.Trim() == "")
            //{
            //    lblMsg.Text = "It's Mandatory to supply value for Comments field while you confirm the order ";
            //    //MsgBoxControl1.show(lblMsg.Text, "Error !!!");
            //    return;
            //}

            if (txtComments.Text.Trim() == "")
            {
                txtComments.Text = "No Comments.";
            }

            if (chkUploadFilesWish.Checked == true)
            {
                if (!varifyMandatoryUploadCase())
                {
                    lblMsg.Text = "Warning ! You have checked 'Upload New Files' option but no files are selected to be uploaded or you may Un-check 'Upload New Files' option to proceed !!";
                     //MsgBoxControl1.show(lblMsg.Text, "Error !!!");
                    return;
                }
            }

            if (txtComments.Text.Trim().IndexOf("'") >= 0)
            {
                lblMsg.Text = "Invalid Character occurs ' in field Comments, Char( ' ) not supported.";
                //MsgBoxControl1.show(lblMsg.Text, "Error !!!");
                return;
            }

            /////////////////////////

            if (Convert.ToInt32(Session["currStatusId"]) == 2)
            {
                if (updateOrderInfo() == false)
                    return;
            }

            SaveFileAtWebsiteLocation(webFileUploadsBasePath);  //this fills fls variable with all the files uploaded, whch can be mailed further
            processNow("Confirmed");


            strMessage += " Purchase Order " + lblprocessSubType.Text + " Under Process :  <b>New Purchase Order Confirmed</b><br/><br/>";

            strMessage += getHtmlMsg();


            string msg = myGlobal.sendRoleBasedMail(mailUrl, strMessage, userEmail, newescalateLevelId, mailList, fls);

            Response.Redirect("~/Intranet/orders/viewOrdersPO.aspx?wfTypeId=10011");
         }
         catch (Exception exps)
         {
                    lblMsg.Text = "Error !!! " + exps.Message;
                    lblError.Text = lblMsg.Text;
                    //MsgBoxControl1.show(lblMsg.Text,"Error !!!");
         }
        
    }

    protected void btnHold_Click(object sender, EventArgs e)
    {
        ClearTextBox();

        if (chkUploadFilesWish.Checked == false)
            setMandatoryUploadCheckedStatus();

        try
        {
            txtComments.Text = txtComments.Text.Trim();
            if (txtComments.Text.Length >= 3900)
            {
                lblMsg.Text = "Comments field value can't exceed 4000 characters.";
                //MsgBoxControl1.show(lblMsg.Text, "Error !!!");
                return;
            }

            if (chkStats.Checked == false)
            {
                //ScriptManager.RegisterStartupScript(this, typeof(Page), "Alert", "<script>alert('Warning! Verify Statistics checkbox need to be checked in order to proceed');</script>", false);
                lblMsg.Text = "Warning! Verify Statistics checkbox need to be checked in order to proceed";
                //MsgBoxControl1.show(lblMsg.Text, "Error !!!");
                return;
            }

            if (txtComments.Text.Trim() == "")
            {
                lblMsg.Text = "It's Mandatory to supply value for Comments field while you put order on HOLD.  ";
                //MsgBoxControl1.show(lblMsg.Text, "Error !!!");
                return;
            }

            if (txtComments.Text.Trim().IndexOf("'") >= 0)
            {
                lblMsg.Text = "Invalid Character occurs ' in field Comments, Char( ' ) not supported.";
                //MsgBoxControl1.show(lblMsg.Text, "Error !!!");
                return;
            }

            /////////////////////////

            if (updateOrderInfo() == false)
                return;

            SaveFileAtWebsiteLocation(webFileUploadsBasePath);  //this fills fls variable with all the files uploaded, whch can be mailed further
            updatesOnHoldCase();


            Response.Redirect("~/Intranet/orders/viewOrdersPO.aspx?wfTypeId=10011");
        }
        catch (Exception exps)
        {
            lblMsg.Text = "Error !!! " + exps.Message;
            lblError.Text = lblMsg.Text;
            //MsgBoxControl1.show(lblMsg.Text,"Error !!!");
        }
    }
    
    protected void btnAccept_Click(object sender, EventArgs e)
    {
        ClearTextBox();
        
        if (chkUploadFilesWish.Checked == false)
            setMandatoryUploadCheckedStatus();

        try
        {
            txtComments.Text = txtComments.Text.Trim();
            if (txtComments.Text.Length >= 3900)
            {
                lblMsg.Text = "Comments field value can't exceed 4000 characters.";
                lblError.Text = lblMsg.Text;
                //MsgBoxControl1.show(lblMsg.Text, "Error !!!");
                return;
            }

            if (!isChildItemPresent())
                {
                    lblMsg.Text = "Warning ! Order can not be escalated since there are no items present for this order !!";
                    lblError.Text = lblMsg.Text;
                    //MsgBoxControl1.show(lblMsg.Text,"Error !!!");
                    return;
                }

            if (chkUploadFilesWish.Checked == true)
            {
                if (!varifyMandatoryUploadCase())
                {
                    //lblMsg.Text = "Warning ! You have checked 'Upload New Files' option but no files are selected to be uploaded or you may Un-check 'Upload New Files' option to proceed !!";
                    lblMsg.Text = "Warning ! You attempted to upload few files before, kindly select your files again or you may Un-check 'Upload New Files' option to proceed without uploading your files!!";
                    lblError.Text = lblMsg.Text;
                    //MsgBoxControl1.show(lblMsg.Text, "Error !!!");
                    return;
                }
            }

            if (chkStats.Checked == false)
            {
                //ScriptManager.RegisterStartupScript(this, typeof(Page), "Alert", "<script>alert('Warning! Verify Statistics checkbox need to be checked in order to proceed');</script>", false);
                lblMsg.Text = "Warning! Verify Statistics checkbox need to be checked in order to proceed";
                lblError.Text = lblMsg.Text;
                //MsgBoxControl1.show(lblMsg.Text, "Error !!!");
                return;
            }

            //if (txtComments.Text.Trim() == "")
            //{
            //    lblMsg.Text = "It's Mandatory to supply value for Comments field.  ";
            //    MsgBoxControl1.show(lblMsg.Text, "Error !!!");
            //    return;
            //}

            if (txtComments.Text.Trim() == "")
            {
                txtComments.Text = "No Comments.";
            }

            if (txtComments.Text.Trim().IndexOf("'") >= 0)
            {
                lblMsg.Text = "Invalid Character occurs ' in field Comments, Char( ' ) not supported.";
                lblError.Text = lblMsg.Text;
                //MsgBoxControl1.show(lblMsg.Text, "Error !!!");
                return;
            }

            lblWait.Visible = true;
            /////////////////////////

            if (updateOrderInfo() == false)
                return;

            //if (Convert.ToInt32(Session["currStatusId"]) == 7) //if atall we need this , but it is out
            //{
            //    if (!Util.IsValidEmail(txtInitimationMailId.Text.Trim()))
            //    {
            //        Message.Show(this, "Error ! You must provide a valid email id for Intimation ID field");
            //        return;
            //    }
            //}


            SaveFileAtWebsiteLocation(webFileUploadsBasePath);  //this fills fls variable with all the files uploaded, whch can be mailed further
            processNow("Accept");

            strMessage = "Purchase Order " + lblprocessSubType.Text + " Under Process :  <b>Accepted/Approved</b><br/><br/>";
            strMessage += getHtmlMsg();

            string msg;

            if (txtInitimationMailId.Text.Trim() != "")
                msg = myGlobal.sendRoleBasedMail(mailUrl, strMessage, (userEmail + ";" + txtInitimationMailId.Text), newescalateLevelId, mailList, fls);
            else
                msg = myGlobal.sendRoleBasedMail(mailUrl, strMessage, userEmail, newescalateLevelId, mailList, fls);

            Response.Redirect("~/Intranet/orders/viewOrdersPO.aspx?wfTypeId=10011");
            lblWait.Visible = false;
        }
        catch (Exception exps)
        {
            lblMsg.Text = "Error !!! " + exps.Message;
            lblError.Text = lblMsg.Text;
            lblWait.Visible = false;
            //MsgBoxControl1.show(lblMsg.Text, "Error !!!");
        }
    }

    protected void btnDecline_Click(object sender, EventArgs e)
    {
        ClearTextBox();
        
        try
        {
            txtComments.Text = txtComments.Text.Trim();
            if (txtComments.Text.Length >=3900)
            {
                lblMsg.Text = "Comments field value can't exceed 4000 characters.";
                //MsgBoxControl1.show(lblMsg.Text, "Error !!!");
                return;
            }

            if (chkUploadFilesWish.Checked == true)
            {
                if (!varifyMandatoryUploadCase())
                {
                    lblMsg.Text = "Warning ! You have checked 'Upload New Files' option but no files are selected to be uploaded or you may Un-check 'Upload New Files' option to proceed !!";
                    //MsgBoxControl1.show(lblMsg.Text, "Error !!!");
                    return;
                }
            }

            if (txtComments.Text.Trim() == "")
            {
                lblMsg.Text = "It's Mandatory to supply value for Comments field. while you Decline ";
                //MsgBoxControl1.show(lblMsg.Text, "Error !!!");
                return;
            }

            if (txtComments.Text.Trim().IndexOf("'") >= 0)
            {
                lblMsg.Text = "Invalid Character occurs ' in field Comments, Char( ' ) not supported.";
                //MsgBoxControl1.show(lblMsg.Text, "Error !!!");
                return;
            }

            if (chkStats.Checked == false)
            {
                //ScriptManager.RegisterStartupScript(this, typeof(Page), "Alert", "<script>alert('Warning! Verify Statistics checkbox need to be checked in order to proceed');</script>", false);
                lblMsg.Text = "Warning! Verify Statistics checkbox need to be checked in order to proceed";
                lblError.Text = lblMsg.Text;
                //MsgBoxControl1.show(lblMsg.Text, "Error !!!");
                return;
            }
            /////////////////////////
            lblWait.Visible = true;

            SaveFileAtWebsiteLocation(webFileUploadsBasePath);
            processNow("Decline");

            strMessage = "Purchase Order " + lblprocessSubType.Text + " Under Process : <b> Declined/DisApproved</b><br/></br>";
            strMessage += getHtmlMsg();

            string msg = myGlobal.sendRoleBasedMail(mailUrl, strMessage, userEmail, newescalateLevelId, mailList, fls);
            //Message.Show(this, msg);

            lblWait.Visible = false;
            Response.Redirect("~/Intranet/orders/viewOrdersPO.aspx?wfTypeId=10011");
        }
        catch (Exception exps)
        {
            lblMsg.Text = "Error !!! " + exps.Message;
            lblError.Text = lblMsg.Text;
            lblWait.Visible = false;

            //MsgBoxControl1.show(lblMsg.Text,"Error !!!");
        }

    }

    protected void btnCancel_Click(object sender, EventArgs e)
    {
        ClearTextBox();

        try
        {
            txtComments.Text = txtComments.Text.Trim();
            if (txtComments.Text.Length >= 3900)
            {
                lblMsg.Text = "Comments field value can't exceed 4000 characters.";
                //MsgBoxControl1.show(lblMsg.Text, "Error !!!");
                return;
            }

            if (txtComments.Text.Trim() == "")
            {
                lblMsg.Text = "It's Mandatory to supply value for Comments field while you cancel the order. ";
                //MsgBoxControl1.show(lblMsg.Text, "Error !!!");
                return;
            }

            if (txtComments.Text.Trim().IndexOf("'") >= 0)
            {
                lblMsg.Text = "Invalid Character occurs ' in field Comments, Char( ' ) not supported.";
                //MsgBoxControl1.show(lblMsg.Text, "Error !!!");
                return;
            }

            if (Convert.ToInt32(Session["currStatusId"]) > 2)  //if order has surpast 2 level ie. confirmation level then only we send emails
            {
                string strMsgCan, msg = "";

                string mailTo = "", usrEmail = "";
                mailTo = myGlobal.membershipUserEmail(lblCreatedBy.Text);
                usrEmail = userEmail;
                /////////////////////////
                sql = "insert into processStatusTrack(fk_processRequestId,action_StatusID,fk_statusId,fk_EscalateLevelId,lastUpdatedBy,StatusAccept,lastModified,comments,fk_processId,mailTo,ccTo) values(" + Session["processRequestId"].ToString() + "," + Session["currStatusId"].ToString() + ",0,0,'" + myGlobal.loggedInUser().ToString() + "','Cancel','" + DateTime.Now.ToString("MM-dd-yyyy hh:mm:ss tt") + "','" + txtComments.Text.Trim() + "'," + pid + ",'" + mailTo + "','" + usrEmail + "')";
                //Db.constr = myGlobal.getIntranetDBConnectionString();
                //Db.myExecuteSQL(sql);

                sql = sql + " update dbo.processRequest set fk_StatusId=0,fk_escalateLevelId=0,ByUser='" + myGlobal.loggedInUser().ToString() + "',comments='" + txtComments.Text.Trim() + "',lastModified='" + DateTime.Now.ToString("yyyy-MM-dd hh:mm:ss tt") + "' where refId=" + refId + " and fk_processId=" + pid;
                Db.constr = myGlobal.getIntranetDBConnectionString();
                Db.myExecuteSQL(sql);

                strMsgCan = "<b>Order has been Canceled.</b><br/><br/>";

                strMsgCan += "FPO Reference : <b>" + txtFpoRef.Text.Trim() + "</b><br/>";
                strMsgCan += "Region/Country : <b>" + lbldbCode.Text + "</b><br/>";
                strMsgCan += "System Order ID : <b>" + refId + "</b><br/>";

                strMsgCan += "Vendor : <b>" + lblVendor.Text + "</b><br/></br>";

                if (lblCustomer.Text.Trim().ToUpper() != "NA")
                    strMsgCan += "Customer : <b>" + lblCustomer.Text + " ( " + lblCustAcct.Text + ")</b><br/></br>";

                strMsgCan += "Updation comments : <b>" + txtComments.Text + "</b><br/></br>";
                strMsgCan += "Updated By : <b>" + myGlobal.loggedInUser() + "</b><br/>";

                msg = myGlobal.sendRoleBasedMail(mailUrl, strMsgCan, userEmail, newescalateLevelId, mailTo, "");
            }
            else
            {
                sql = "insert into processStatusTrack(fk_processRequestId,action_StatusID,fk_statusId,fk_EscalateLevelId,lastUpdatedBy,StatusAccept,lastModified,comments,fk_processId) values(" + Session["processRequestId"].ToString() + "," + Session["currStatusId"].ToString() + ",0,0,'" + myGlobal.loggedInUser().ToString() + "','Cancel','" + DateTime.Now.ToString("MM-dd-yyyy hh:mm:ss tt") + "','" + txtComments.Text.Trim() + "'," + pid + ")";
                //Db.constr = myGlobal.getIntranetDBConnectionString();
                //Db.myExecuteSQL(sql);

                sql = sql + " update dbo.processRequest set fk_StatusId=0,fk_escalateLevelId=0,ByUser='" + myGlobal.loggedInUser().ToString() + "',comments='" + txtComments.Text.Trim() + "',lastModified='" + DateTime.Now.ToString("yyyy-MM-dd hh:mm:ss tt") + "' where refId=" + refId + " and fk_processId=" + pid;
                Db.constr = myGlobal.getIntranetDBConnectionString();
                Db.myExecuteSQL(sql);

            }

            Response.Redirect("~/Intranet/orders/viewOrdersPO.aspx?wfTypeId=10011");
        }
        catch (Exception exps)
        {
            lblMsg.Text = "Error !!! " + exps.Message;
            lblError.Text = lblMsg.Text;
            //MsgBoxControl1.show(lblMsg.Text,"Error !!!");
        }
     }

    
    protected void btnEdit_Click(object sender, EventArgs e)
    {
        Response.Redirect("~/Intranet/orders/addOrder.aspx?refId=" + refId + "&type=Edit");
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
        String pth, dirPhyPth;
        pth = saveFileAtWebSitePath + refId.ToString() + "-" + lblprocessSubType.Text + "/";
        dirPhyPth = Server.MapPath("~" + pth);

        if (!System.IO.Directory.Exists(dirPhyPth))
        {
            System.IO.Directory.CreateDirectory(dirPhyPth);
        }

        saveFileAtWebSitePath = pth;  //new path
        
        ////////////////////////////////////
        
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

            //add existing files to attach

            if (GridFiles.Rows.Count > 0)
            {
                CheckBox chk ;
                HyperLink lnk;

                foreach(GridViewRow rw in GridFiles.Rows)
                {
                    chk = rw.FindControl("chklstAttachedFiles") as CheckBox;
                    lnk = rw.FindControl("lnkFileLoc") as HyperLink;

                    string tmpstr;
                    int strt, ends;
                    tmpstr = lnk.NavigateUrl;
                    strt = tmpstr.LastIndexOf("~") + 1;
                    ends = tmpstr.Length - strt;
                    lnk.NavigateUrl = tmpstr.Substring(strt, ends);

                    if (chk.Checked == true)
                    {
                        if (fls.Trim() == "")
                            fls = Server.MapPath("~" + lnk.NavigateUrl);
                        else
                            fls += ";" + Server.MapPath("~" + lnk.NavigateUrl);
                    }
                }
            }

        }
         catch (Exception exps)
         {
                    lblMsg.Text = "Error !!! " + exps.Message;
                    //MsgBoxControl1.show(lblMsg.Text,"Error !!!");
         }
        return;
    }

    private void loadExistingAttachmentsForStatus()
    {
        sql = "select * from dbo.uploadTrack where fk_refId=" + refId + " order by srNo";
        Db.constr = myGlobal.getIntranetDBConnectionString();
        dt = Db.myGetDS(sql).Tables[0];
        GridFiles.DataSource = dt;
        GridFiles.DataBind();

        if (GridFiles.Rows.Count > 0)
        {
            CheckBox chk;
            HyperLink lnk;

            foreach (GridViewRow rw in GridFiles.Rows)
            {
                chk = rw.FindControl("chklstAttachedFiles") as CheckBox;
                lnk = rw.FindControl("lnkFileLoc") as HyperLink;

                string tmpstr;
                int strt, ends;

                tmpstr = chk.Text;
                strt = tmpstr.LastIndexOf("/") + 1;
                ends = tmpstr.Length - strt;

                chk.Text = tmpstr.Substring(strt, ends);
                lnk.NavigateUrl = "/download.aspx?file=~" + tmpstr; //sets path for download link;
                chk.Checked = false;
            }
        }

        if (GridFiles.Rows.Count == 0)  //if it is still empty after drd loop
        {
            lblNone.Visible = true;
        }
    }

   

    //NEW ADDITIONS BY JP(11 feb 2013//////////////////

    protected void gridDetails1_RowEditing(object sender, GridViewEditEventArgs e)
    {
        ClearTextBox();

        Label lblQty = (Label)gridDetails1.Rows[e.NewEditIndex].FindControl("lblQty");
        Label lblCurrPrice = (Label)gridDetails1.Rows[e.NewEditIndex].FindControl("lblCurrPrice");
        Label lblRebatePerUnit = (Label)gridDetails1.Rows[e.NewEditIndex].FindControl("lblRebatePerUnit");
        Label lblSellingPrice = (Label)gridDetails1.Rows[e.NewEditIndex].FindControl("lblSellingPrice");

        gridDetails1.EditIndex = e.NewEditIndex;
        BindGrid();

        GridViewRow myRow = gridDetails1.Rows[e.NewEditIndex];

        TextBox lblQtyEdit = myRow.FindControl("lblQtyEdit") as TextBox;
        TextBox lblCurrPriceEdit = myRow.FindControl("lblCurrPriceEdit") as TextBox;
        TextBox lblRebatePerUnitEdit = myRow.FindControl("lblRebatePerUnitEdit") as TextBox;
        TextBox lblSellingPriceEdit = myRow.FindControl("lblSellingPriceEdit") as TextBox;

        lblQtyEdit.Text = lblQty.Text;
        lblCurrPriceEdit.Text = lblCurrPrice.Text;
        lblRebatePerUnitEdit.Text = lblRebatePerUnit.Text;
        lblSellingPriceEdit.Text = lblSellingPrice.Text;
    }

    protected void gridDetails1_RowCancelingEdit(object sender, GridViewCancelEditEventArgs e)
    {
        gridDetails1.EditIndex = -1;
        BindGrid();
    }

    protected void gridDetails1_RowUpdating(object sender, GridViewUpdateEventArgs e)
    {
        try
        {
        GridViewRow myRow = gridDetails1.Rows[e.RowIndex];
        if (myRow != null)
        {
            TextBox lblQtyEdit = myRow.FindControl("lblQtyEdit") as TextBox;
            TextBox lblCurrPriceEdit = myRow.FindControl("lblCurrPriceEdit") as TextBox;
            TextBox lblRebatePerUnitEdit = myRow.FindControl("lblRebatePerUnitEdit") as TextBox;
            TextBox lblSellingPriceEdit = myRow.FindControl("lblSellingPriceEdit") as TextBox;
            //Label lblTotalCostAftrRebateEdit = (Label)myRow.FindControl("lblTotalCostAftrRebateEdit");
            Label lblAutoID = (Label)myRow.FindControl("lblAutoIDEdit");
            Label lblpoIdEdit = (Label)myRow.FindControl("lblpoIdEdit");

            if (lblCurrPriceEdit.Text == "" || lblQtyEdit.Text == "" || lblRebatePerUnitEdit.Text == "" || lblSellingPriceEdit.Text == "")
            {
                lblError.Text = "1 or more fields left blank, Fill all details to proceed";
                //MsgBoxControl1.show(lblError.Text, "Error !!!");
                return;
            }

            if (!Util.isValidDecimalNumber(lblCurrPriceEdit.Text))
            {
                lblError.Text = "Invalid value in Current Price field, supports only Numeric or Decimal values";
                //MsgBoxControl1.show(lblError.Text, "Error !!!");
                return;
            }

            if (!Util.isValidDecimalNumber(lblRebatePerUnitEdit.Text))
            {
                lblError.Text = "Invalid value in Rebate Per Unit field, supports only Numeric or Decimal values";
                //MsgBoxControl1.show(lblError.Text, "Error !!!");
                return;
            }

            if (!Util.isValidDecimalNumber(lblSellingPriceEdit.Text))
            {
                lblError.Text = "Invalid value in Selling Price field, supports only Numeric or Decimal values";
                //MsgBoxControl1.show(lblError.Text, "Error !!!");
                return;
            }

            if (!Util.isValidNumber(lblQtyEdit.Text))
            {
                lblError.Text = "Invalid value in Qty field, supports only Numeric values";
                //MsgBoxControl1.show(lblError.Text, "Error !!!");
                return;
            }

            double amountTotal, CostAfterRebate, totalSelling, margin, diff, TotalCostAfterRebate;
            //amountTotal=Convert.ToInt32(lblQtyEdit.Text) * Convert.ToInt32(lblCurrPriceEdit.Text);
            amountTotal = Convert.ToInt32(lblQtyEdit.Text) * Convert.ToDouble(lblCurrPriceEdit.Text);
            CostAfterRebate = Convert.ToDouble(lblCurrPriceEdit.Text) - Convert.ToDouble(lblRebatePerUnitEdit.Text);
            TotalCostAfterRebate = Convert.ToInt32(lblQtyEdit.Text) * Convert.ToDouble(CostAfterRebate);
            totalSelling = Convert.ToDouble(lblSellingPriceEdit.Text) * Convert.ToInt32(lblQtyEdit.Text);
            diff = totalSelling - TotalCostAfterRebate;
            margin = Math.Round((diff / TotalCostAfterRebate * 100), 3);

            sql = "update dbo.PurchaseOrderlines set qty=" + lblQtyEdit.Text + ", currPrice=" + lblCurrPriceEdit.Text + ", amountTotal=" + amountTotal + ", rebatePerUnit=" + lblRebatePerUnitEdit.Text + ", CostAfterRebate=" + CostAfterRebate + ",totalCostAfterRebate=" + TotalCostAfterRebate + ", sellingPrice=" + lblSellingPriceEdit.Text + ", totalSelling=" + totalSelling + ", margin=" + margin + " where poLineId=" + lblAutoID.Text + " ";
            Db.constr = myGlobal.getIntranetDBConnectionString();
            Db.myExecuteSQL(sql);
            //Session["fk_poId"] = lblpoIdEdit.Text; 
            UpdatePurchaseOrder();
            lblError.Text = "Data has been Updated successfully";
            //MsgBoxControl1.show(lblError.Text, "Success !!");
            gridDetails1.EditIndex = -1;
            BindGrid();
           }
        }
        catch (Exception e2)
        {
            lblError.Text = "Error! " + e2.Message;
        }
    }

    protected void gridDetails1_RowDeleting(object sender, GridViewDeleteEventArgs e)
    {
        try
        {
            GridViewRow myRow = gridDetails1.Rows[e.RowIndex];
            if (myRow != null)
            {
                Label lblAutoID = (Label)myRow.FindControl("lblAutoID");
                sql = "delete from dbo.PurchaseOrderlines where poLineId='" + lblAutoID.Text + "'";
                Db.constr = myGlobal.getIntranetDBConnectionString();
                Db.myExecuteSQL(sql);
                UpdatePurchaseOrder();

                lblError.Text = "Record has been deleted successfully";
                //MsgBoxControl1.show(lblError.Text, "Success !!!");
                BindGrid(); //refresh grid
            }
        }
        catch (Exception e2)
        {
            lblError.Text = "Error! " + e2.Message;
        }
    }

    protected void gridDetails1_RowDataBound(object sender, GridViewRowEventArgs e)
    {
    }

    protected void UpdatePurchaseOrder()
    {
        string amountTotal, totalCostAfterRebate, totalSelling, fk_poId;
        double margin, diff;
        fk_poId = lblOrderId.Text; //Session["fk_poId"].ToString();
        sql = "select sum(amountTotal) as 'amountTotal',sum(totalCostAfterRebate) as 'totalCostAfterRebate',sum(totalSelling) as 'totalSelling' from dbo.PurchaseOrderlines where fk_poId=" + fk_poId + " group by fk_poId";
        Db.constr = myGlobal.getIntranetDBConnectionString();
        dr = Db.myGetReader(sql);

        if (dr.HasRows)
        {
            double LineTotal, LineTotalRebate, LineTotalSelling;
            while (dr.Read())
            {
                amountTotal = dr["amountTotal"].ToString();
                totalCostAfterRebate = dr["totalCostAfterRebate"].ToString();
                totalSelling = dr["totalSelling"].ToString();
                diff = Convert.ToDouble(totalSelling) - Convert.ToDouble(totalCostAfterRebate);
                margin = Math.Round((diff / Convert.ToDouble(totalCostAfterRebate) * 100), 3);
                string qry = "update dbo.PurchaseOrders set total=" + amountTotal + ", totalCostAfterRebate=" + totalCostAfterRebate + ", totalSelling=" + totalSelling + ", margin=" + margin + " where poid=" + fk_poId + "";
                Db.constr = myGlobal.getIntranetDBConnectionString();
                Db.myExecuteSQL(qry);

                LineTotal = Convert.ToDouble(amountTotal.ToString());
                LineTotalRebate = Convert.ToDouble(totalCostAfterRebate.ToString());
                LineTotalSelling = Convert.ToDouble(totalSelling.ToString());

                lblLineTotal.Text = string.Format("{0:n}", LineTotal);
                lblLineTotalRebate.Text = string.Format("{0:n}", LineTotalRebate);
                lblLineTotalSelling.Text = string.Format("{0:n}", LineTotalSelling);
                lblLineMargin.Text = margin.ToString();
            }
        }
    }

    protected void LoadPart()
    {
        //Db.LoadDDLsWithCon(ddlPartNo, "select *,csimpleCode+' : '+Description_1 as part  from stkItem Where ItemGroup=(select BUGroupName from tej.[dbo].[TblVendorsBUMapping] where BUName='" + lblBU.Text + "') order by Part", "Part", "cSimpleCode", myGlobal.getConnectionStringForDB("TRI"));
        Db.LoadDDLsWithCon(ddlPartNo, "select *,Code+' : '+Description_1 as part  from stkItem Where ItemGroup=(select BUGroupName from tej.[dbo].[TblVendorsBUMapping] where BUName='" + lblBU.Text + "') order by Part", "Part", "Code", myGlobal.getConnectionStringForDB("TRI"));
        string desc, myString;
        desc = ddlPartNo.SelectedItem.Text;
        myString = desc.Split(':').Last();
        txtPartNo.Text = ddlPartNo.SelectedValue.ToString();
        txtDescription.Text = myString;

    }

    protected void ddlPartNo_SelectedIndexChanged(object sender, EventArgs e)
    {
        string desc, myString;
        desc = ddlPartNo.SelectedItem.Text;
        myString = desc.Split(':').Last();
        txtPartNo.Text = ddlPartNo.SelectedValue.ToString();
        txtDescription.Text = myString;
    }

    protected void btnInsert_Click(object sender, EventArgs e)
    {
        try
        {
            if (gridDetails1.Rows.Count > 0)
            {
                foreach (GridViewRow rw in gridDetails1.Rows)
                {
                    Label lblPartNo = rw.FindControl("lblPartNo") as Label;
                    if (lblPartNo.Text == txtPartNo.Text)
                    {
                        lblError1.Text = "Warning! an item with same part no is already present, Please select a different item";
                        return;
                    }
                }
            }

            if (txtPartNo.Text == "")
            {
                lblError1.Text = "Warning! Part number could not be left blank";
                return;
            }

            if (txtDescription.Text == "")
            {
                lblError1.Text = "Warning! Description Field could not be left blank";
                return;
            }

            if (!Util.isValidDecimalNumber(txtCurrentPrice.Text))
            {
                lblError1.Text = "Warning! Invalid value in Current Price field, supports only Numeric or Decimal values";
                return;
            }

            if (!Util.isValidNumber(txtQuantity.Text))
            {
                lblError1.Text = "Warning! Invalid value in Quantity field, supports only Numeric values";
                return;
            }

            if (!Util.isValidDecimalNumber(txtSellingPrice.Text))
            {
                lblError1.Text = "Warning! Invalid value in Selling Price field, supports only Numeric or Decimal values";
                return;
            }

            if (!Util.isValidDecimalNumber(txtRebatePerUnit.Text))
            {
                lblError1.Text = "Warning! Invalid value in Rebate Per Unit field, supports only Numeric or Decimal values";
                return;
            }
        
            string fk_poId = "", Region = "", CustName = "", amountTotal, totalCostAfterRebate, totalSelling, CostAfterRebate;
            double margin, diff;
            int LineNos = 0;

            fk_poId = lblOrderId.Text;
            Region = lbldbCode.Text;
            CustName = lblCustomer.Text;
        
            if (gridDetails1.Rows.Count > 0)
            {
                Label lblLineNo;
                foreach (GridViewRow rw in gridDetails1.Rows)
                {
                    lblLineNo = (Label)rw.FindControl("lblLineNo");
                   
                    if (Convert.ToInt32(lblLineNo.Text) > LineNos)
                     LineNos = Convert.ToInt32(lblLineNo.Text);
                }

                LineNos = LineNos + 1;
            }
            else
            {
                LineNos = 1;
            }

            amountTotal = (Convert.ToDouble(txtCurrentPrice.Text) * Convert.ToDouble(txtQuantity.Text)).ToString();
            CostAfterRebate = (Convert.ToDouble(txtCurrentPrice.Text) - Convert.ToDouble(txtRebatePerUnit.Text)).ToString();
            totalCostAfterRebate = (Convert.ToDouble(CostAfterRebate) * Convert.ToDouble(txtQuantity.Text)).ToString();
            totalSelling = (Convert.ToDouble(txtSellingPrice.Text) * Convert.ToDouble(txtQuantity.Text)).ToString();
            diff = Convert.ToDouble(totalSelling) - Convert.ToDouble(totalCostAfterRebate);
            margin = Math.Round((diff / Convert.ToDouble(totalCostAfterRebate) * 100), 3);

            sql = "insert into dbo.PurchaseOrderlines(fk_poId,LineNum,customerName,region,partNo,smallDescription,qty,currPrice,amountTotal,rebatePerUnit,CostAfterRebate,totalCostAfterRebate,sellingPrice,totalSelling,margin,orderType) values('" + fk_poId + "','" + LineNos.ToString() + "','" + CustName + "','" + Region + "','" + txtPartNo.Text + "','" + txtDescription.Text.Replace("'"," ") + "','" + txtQuantity.Text + "','" + txtCurrentPrice.Text + "','" + amountTotal + "','" + txtRebatePerUnit.Text + "','" + CostAfterRebate + "','" + totalCostAfterRebate + "','" + txtSellingPrice.Text + "','" + totalSelling + "','" + margin + "','" + ddlOrderType.SelectedItem.Text + "')";
            Db.constr = myGlobal.getIntranetDBConnectionString();
            Db.myExecuteSQL(sql);

            //Session["fk_poId"] = fk_poId;
            UpdatePurchaseOrder();

            lblError.Text = "Record Inserted Successfully";
            //MsgBoxControl1.show(lblError.Text, "Success !!!");
            BindGrid();
        
            ClearTextBox();
            lnkNewItem.Visible = true;
        }
        catch (Exception e2)
        {
            lblError.Text = "Error! " + e2.Message;
        }
    }

    private void ClearTextBox()
    {
        pnlNewRow.Visible = false;
        //lnkNewItem.Visible = true;
        txtQuantity.Text = "";
        txtCurrentPrice.Text = "";
        txtRebatePerUnit.Text = "";
        txtSellingPrice.Text = "";
        lblError1.Text = "";
    }
    protected void btnClose_Click(object sender, EventArgs e)
    {
        ClearTextBox();
        lnkNewItem.Visible = true;
    }

    protected void lnkNewItem_Click(object sender, EventArgs e)
    {
        try
        {
            gridDetails1.EditIndex = -1;
            BindGrid();

            lnkNewItem.Visible = false;
            pnlNewRow.Visible = true;
            LoadPart();
        }
        catch (Exception e2)
        {
            lblError.Text = "Error! " + e2.Message;
        }
    }

    private Boolean isChildItemPresent()
    {
        sql = "select count(*) as cnt from dbo.purchaseOrderLines where fk_poId=" + refId;
        Db.constr = myGlobal.getIntranetDBConnectionString();
        int ret = Db.myExecuteScalar(sql);
        if (ret > 0)
            return true;
        else
            return false;
    }

    public DataTable CustomGetAllUsers()
    {
        DataSet ds = new DataSet();

        DataTable dt = new DataTable();

        MembershipUserCollection muc;
        muc = Membership.GetAllUsers();

        dt.Columns.Add("UserName", Type.GetType("System.String"));
        dt.Columns.Add("Email", Type.GetType("System.String"));
        dt.Columns.Add("CreationDate", Type.GetType("System.DateTime"));

        /* Here is the list of columns returned of the Membership.GetAllUsers() method
         * UserName, Email, PasswordQuestion, Comment, IsApproved
         * IsLockedOut, LastLockoutDate, CreationDate, LastLoginDate
         * LastActivityDate, LastPasswordChangedDate, IsOnline, ProviderName
         */

        foreach (MembershipUser mu in muc)
        {
            DataRow dr;
            dr = dt.NewRow();
            dr["UserName"] = mu.UserName;
            dr["Email"] = mu.Email;
            dr["CreationDate"] = mu.CreationDate;
            dt.Rows.Add(dr);
        }
        return dt;
    }

    protected void LnkSaveDwnl_Click(object sender, EventArgs e)
    {
        //MsgBoxControl1.show("This feature is under construction, will be shortly available, sorry for inconvenience", "");
        //return;
        lblMsg.Text = "";
        lblError.Text = "";

        if (qrls.ToUpper() == "productManagerCordinator".ToUpper() && action.ToUpper() == "TASK")  //these are only verified if it is the said role , usually it will be the last stage
        {
            if (updateOrderInfo() == false)
            {
                lblMsg.Text += ", Kindly supply correct data and retry View/Download option.";
                lblError.Text = lblMsg.Text;
                return;
            }
        }

        String pth, flNm;
        pth = webFileUploadsBasePath + refId.ToString() + "-" + lblprocessSubType.Text + "/";
        flNm = "Order-" + refId.ToString() + ".html";
        
        try
        {
            //call save function here
            saveDataIntoFile(pth, flNm,"html");
            string getFile = "/download.aspx?file=~" + pth + flNm;
            Response.Redirect(getFile);
        }
        catch (Exception exp)
        {
            lblMsg.Text = "Error ! " + exp.Message;
        }
    }

    protected void LnkSaveDwnlCVS_Click(object sender, EventArgs e)
    {
        lblMsg.Text = "";
        lblError.Text = "";

        if (qrls.ToUpper() == "productManagerCordinator".ToUpper() && action.ToUpper() == "TASK")  //these are only verified if it is the said role , usually it will be the last stage
        {
            if (updateOrderInfo() == false)
            {
                lblMsg.Text += ", Kindly supply correct data and retry View/Download option.";
                lblError.Text = lblMsg.Text;
                return;
            }
        }

        String pth, flNm;
        pth = webFileUploadsBasePath + refId.ToString() + "-" + lblprocessSubType.Text + "/";
        flNm = "Order-" + refId.ToString() + ".csv";

        try
        {
            //call save function here
            saveDataIntoFile(pth, flNm, "csv");
            string getFile = "/download.aspx?file=~" + pth + flNm;
            Response.Redirect(getFile);
        }
        catch (Exception exp)
        {
            lblMsg.Text = "Error ! " + exp.Message;
        }
    }

    private Boolean saveDataIntoFile(string virtPth, string flNameWithExt, string fileType)
    {
        Boolean fFlg = true;

        //work here
        String dirPhyPth, flpth;
        dirPhyPth = Server.MapPath("~" + virtPth);

        if (!System.IO.Directory.Exists(dirPhyPth))
        {
            System.IO.Directory.CreateDirectory(dirPhyPth);
        }

        //delete file if exists already

        flpth = dirPhyPth + flNameWithExt;

        if (System.IO.File.Exists(flpth))
        {
            System.IO.File.Delete(flpth);
        }

        ///////////////// call save path here
        if (fileType == "html")
        {
            StreamWriter sWriter = File.CreateText(flpth);
            sWriter.WriteLine(getHtmlString(flNameWithExt));
            sWriter.Close();
        }

        if (fileType == "csv")
        {
            getCSVFile(flpth);
        }

        ///////////////

        return fFlg;
    }

    private Boolean getCSVFile(string PdirPhyPth)
    {
        try
        {
        
            //String pth, flNm;
            //pth = webFileUploadsBasePath + refId.ToString() + "-" + lblprocessSubType.Text + "/";
            //flNm = pth + "Order-" + refId.ToString() + ".csv";

            //String dirPhyPth;
            //dirPhyPth = Server.MapPath("~" + flNm);

            using (CsvFileWriter writer = new CsvFileWriter(PdirPhyPth))
            {
                CsvRow row;

                row = new CsvRow();
                row.Add("Order Type :");
                row.Add(lblprocessSubType.Text);
                row.Add("");
                row.Add("");
                row.Add("");
                row.Add("");
                row.Add("System PO NO :");
                row.Add(lblOrderId.Text);
                writer.WriteRow(row);

                row = new CsvRow();
                row.Add("Vendor [BU] :");
                row.Add(lblVendor.Text + " [" + lblBU.Text + "]");
                row.Add("");
                row.Add("");
                row.Add("");
                row.Add("");
                row.Add("EVO PO NO :");
                row.Add(txtEvoPONO.Text);
                writer.WriteRow(row);

                string pCustName = "NA";

                if (lblCustomer.Text.Trim() != "")
                {
                    pCustName = lblCustomer.Text.Trim() + " [" + lblCustAcct.Text.Trim() + "]";
                }

                row = new CsvRow();
                row.Add("Customer Reference :");
                row.Add(pCustName);
                row.Add("");
                row.Add("");
                row.Add("");
                row.Add("");
                row.Add("PO Date :");
                row.Add(txtPODate.Text);
                writer.WriteRow(row);

                row = new CsvRow();
                row.Add("FPO Reference :");
                row.Add(txtFpoRef.Text.Trim().ToUpper());
                row.Add("");
                row.Add("");
                row.Add("");
                row.Add("");
                row.Add("	REQ Delivery Date :");
                row.Add(txtReqDelDate.Text);
                writer.WriteRow(row);

                row = new CsvRow();
                row.Add("");
                row.Add("");
                row.Add("");
                row.Add("");
                row.Add("");
                row.Add("");
                row.Add("OPG Code :");
                row.Add(txtOPGCode.Text);
                writer.WriteRow(row);

                row = new CsvRow();
                row.Add("");
                row.Add("");
                row.Add("");
                row.Add("");
                row.Add("");
                row.Add("");
                row.Add("Ship To :");
                row.Add(txtCBNNo.Text);
                writer.WriteRow(row);

                row = new CsvRow();
                row.Add("");
                row.Add("");
                row.Add("");
                row.Add("");
                row.Add("");
                row.Add("");
                row.Add("CBN Name :");
                row.Add(ddlCBNName.SelectedItem.Text);
                writer.WriteRow(row);

                row = new CsvRow();
                row.Add("Comments :");
                row.Add(lblComments.Text);
                writer.WriteRow(row);

                //////////////////////////////////////
                row = new CsvRow();
                row.Add("");
                writer.WriteRow(row);

                row = new CsvRow();
                row.Add("Line No");
                row.Add("Region");
                row.Add("Part No");
                row.Add("Description");
                row.Add("Qty");
                row.Add("Curr Price");
                row.Add("Amount Total");
                row.Add("Rebate Per Unit");
                row.Add("Cost Aftr Rebate");
                row.Add("Total Cost After Rebate");
                row.Add("Selling Price");
                row.Add("Total Selling");
                row.Add("Margin%");
                writer.WriteRow(row);

                row = new CsvRow();   //empty line
                row.Add("");
                writer.WriteRow(row);

                foreach (GridViewRow rw in gridDetails1.Rows)
                {
                    if (rw.RowType == DataControlRowType.DataRow)
                    {
                        row = new CsvRow();
                        //pCustName = (rw.FindControl("lblCustName") as Label).Text;

                        row.Add((rw.FindControl("lblLineNo") as Label).Text);
                        row.Add((rw.FindControl("lblRegion") as Label).Text);
                        row.Add((rw.FindControl("lblPartNo") as Label).Text);
                        row.Add((rw.FindControl("lblDesc") as Label).Text);
                        row.Add((rw.FindControl("lblQty") as Label).Text);
                        row.Add((rw.FindControl("lblCurrPrice") as Label).Text);
                        row.Add((rw.FindControl("lblAmountTotal") as Label).Text);
                        row.Add((rw.FindControl("lblRebatePerUnit") as Label).Text);
                        row.Add((rw.FindControl("lblCostAftrRebate") as Label).Text);
                        row.Add((rw.FindControl("lblTotalCostAftrRebate") as Label).Text);
                        row.Add((rw.FindControl("lblSellingPrice") as Label).Text);
                        row.Add((rw.FindControl("lblTotalSelling") as Label).Text);
                        row.Add((rw.FindControl("lblMargin") as Label).Text);
                        writer.WriteRow(row);
                    }
                }

                row = new CsvRow();   //empty line
                row.Add("");
                writer.WriteRow(row);

                row = new CsvRow();
                row.Add("");
                row.Add("");
                row.Add("");
                row.Add("");
                row.Add("");
                row.Add("");
                row.Add("Amount $");
                row.Add("");
                row.Add("");
                row.Add("Cost After Rebate $");
                row.Add("");
                row.Add("Selling $");
                row.Add("Margin %");
                writer.WriteRow(row);

                row = new CsvRow();
                row.Add("");
                row.Add("");
                row.Add("");
                row.Add("");
                row.Add("");
                row.Add("Totals $");
                row.Add(lblLineTotal.Text);
                row.Add("");
                row.Add("");
                row.Add(lblLineTotalRebate.Text);
                row.Add("");
                row.Add(lblLineTotalSelling.Text);
                row.Add(lblLineMargin.Text);
                writer.WriteRow(row);

                row = new CsvRow();   //empty line
                row.Add("");
                writer.WriteRow(row);

                row = new CsvRow();
                row.Add("");
                row.Add("Product Manager");
                row.Add("");
                row.Add("");
                row.Add("Head Of Finance");

                writer.WriteRow(row);

                row = new CsvRow();
                row.Add("");
                row.Add(lblProdctManager.Text);
                row.Add("");
                row.Add("");
                row.Add(lblHeadOfOffice.Text);
                writer.WriteRow(row);

                return true;
            }
        }
        catch (Exception exp)
        {
            return false;
        }
    }
    
    private string getHtmlString(string flNameTitle)
    {
        string htmlString,LinesHtmlTbl,pCustName="NA";

        //inner table for grid
        LinesHtmlTbl = "<table border='1' style='Width:100%'>";

        //write header row

        LinesHtmlTbl += "<tr>";
        LinesHtmlTbl += "<td>Line No</td>";
        LinesHtmlTbl += "<td>Region</td>";
        LinesHtmlTbl += "<td style='Width:40%'>Part No</td>";
        LinesHtmlTbl += "<td>Qty</td>";
        LinesHtmlTbl += "<td>Curr Price</td>";
        LinesHtmlTbl += "<td>Amount Total</td>";
        LinesHtmlTbl += "<td>Rebate Per Unit</td>";
        LinesHtmlTbl += "<td>Cost Aftr Rebate</td>";
        LinesHtmlTbl += "<td>Total Cost After Rebate</td>";
        LinesHtmlTbl += "<td>Selling Price</td>";
        LinesHtmlTbl += "<td>Total Selling</td>";
        LinesHtmlTbl += "<td>Margin%</td>";
        LinesHtmlTbl += "</tr>";


        foreach (GridViewRow rw in gridDetails1.Rows)
        {
            if (rw.RowType == DataControlRowType.DataRow)
            {
                LinesHtmlTbl += "<tr>";
                LinesHtmlTbl += "<td>" + (rw.FindControl("lblLineNo") as Label).Text + "</td>";
                
                //stores the customer name
                //pCustName=(rw.FindControl("lblCustName") as Label).Text;

                LinesHtmlTbl += "<td>" + (rw.FindControl("lblRegion") as Label).Text + "</td>";
                LinesHtmlTbl += "<td>[" + (rw.FindControl("lblPartNo") as Label).Text + "] <br/>" + (rw.FindControl("lblDesc") as Label).Text + "</td>";
                LinesHtmlTbl += "<td>" + (rw.FindControl("lblQty") as Label).Text + "</td>";
                LinesHtmlTbl += "<td>" + (rw.FindControl("lblCurrPrice") as Label).Text + "</td>";
                LinesHtmlTbl += "<td>" + (rw.FindControl("lblAmountTotal") as Label).Text + "</td>";
                LinesHtmlTbl += "<td>" + (rw.FindControl("lblRebatePerUnit") as Label).Text + "</td>";
                LinesHtmlTbl += "<td>" + (rw.FindControl("lblCostAftrRebate") as Label).Text + "</td>";
                LinesHtmlTbl += "<td>" + (rw.FindControl("lblTotalCostAftrRebate") as Label).Text + "</td>";
                LinesHtmlTbl += "<td>" + (rw.FindControl("lblSellingPrice") as Label).Text + "</td>";
                LinesHtmlTbl += "<td>" + (rw.FindControl("lblTotalSelling") as Label).Text + "</td>";
                LinesHtmlTbl += "<td>" + (rw.FindControl("lblMargin") as Label).Text + "</td>";
                LinesHtmlTbl += "</tr>";
            }
        }

        LinesHtmlTbl += "</table>";  //closed inner table

       
        
        if (lblCustomer.Text.Trim() != "")
        {
            pCustName = lblCustomer.Text.Trim() + " [" + lblCustAcct.Text.Trim() + "]";
        }

        /////////////////////////////Start main table////////////

        htmlString = "<html<head><title>" + flNameTitle + "</title></head><body><div>";
        htmlString += "<table style='width:99%'>";
       
        htmlString += "<tr style='height:40px'>";
        htmlString += "<td style='width:57%;border:0px solid'><input type='button' value='Print this page' onclick='window.print()'></td>";
        htmlString += "<td style='width:5%'>&nbsp;</td>";
        htmlString += "<td style='width:38%;border:0px solid'>&nbsp;</td>";
        htmlString += "</tr>";

        htmlString += "<tr>";
        htmlString += "<td style='width:57%;border:1px solid'>Order Type : <b>" + lblprocessSubType.Text + "</b></td>";
        htmlString += "<td style='width:5%'>&nbsp;</td>";
        htmlString += "<td style='width:38%;border:1px solid'>System PO NO : <b>" + lblOrderId.Text + "</b></td>";
        htmlString += "</tr>";
        
        htmlString += "<tr>";
        htmlString += "<td style='border:1px solid'>Vendor [BU] : <b>" + lblVendor.Text + " [" + lblBU.Text + "]</b></td>";
        htmlString += "<td>&nbsp;</td>";
        htmlString += "<td  style='border:1px solid'>EVO PO NO : <b>" + txtEvoPONO.Text + "</b></td>";
        htmlString += "</tr>";

        

        htmlString += "<tr>";
        htmlString += "<td style='border:1px solid'>Customer Reference : <b>" + pCustName + "</b></td>";
        htmlString += "<td>&nbsp;</td>";
        htmlString += "<td style='border:1px solid'>PO Date : <b>" + txtPODate.Text + "</b></td>";
        htmlString += "</tr>";
        
        htmlString += "<tr>";
        //htmlString += "<td>&nbsp;</td>";
        htmlString += "<td style='border:1px solid'>FPO Reference : <b>" + txtFpoRef.Text.Trim().ToUpper() + "</b></td>";
        htmlString += "<td>&nbsp;</td>";
        htmlString += "<td style='border:1px solid'>REQ Delivery Date : <b>" + txtReqDelDate.Text + "</b></td>";
        htmlString += "</tr>";
        
        htmlString += "<tr>";
        htmlString += "<td>&nbsp;</td>";
        htmlString += "<td>&nbsp;</td>";
        htmlString += "<td style='border:1px solid'>OPG Code : <b>" + txtOPGCode.Text + "</b></td>";
        htmlString += "</tr>";
        
        htmlString += "<tr>";
        htmlString += "<td style='border:0px solid'>&nbsp;</td>";
        htmlString += "<td>&nbsp;</td>";
        htmlString += "<td style='border:1px solid'>Ship To : <b>" + txtCBNNo.Text + "</b></td>";
        htmlString += "</tr>";

        htmlString += "<tr>";
        htmlString += "<td style='border:0px solid'>&nbsp;</td>";
        htmlString += "<td>&nbsp;</td>";
        htmlString += "<td style='border:1px solid'>CBN Name : <b>" + ddlCBNName.SelectedItem.Text + "</b></td>";

        htmlString += "</tr>";
        htmlString += "<tr>";
        htmlString += "<td style='border:1px solid' colspan='3'>Comments : <b>" + lblComments.Text + "</b></td>";
        htmlString += "</tr>";

        htmlString += "<tr style='Height:30px'><td colspan='3'>&nbsp;</td></tr>";

        htmlString += "<tr><td colspan='3'>";

        //lines html string formed above
          htmlString += LinesHtmlTbl;

        htmlString += "</td></tr>";
        

        htmlString += "<tr>";
        htmlString += "<td colspan='3' align='Right'>";

        htmlString += "<table border='1' style='Width:60%'>";
        htmlString += "<tr>";
        htmlString += "<td>&nbsp;</td>";
        htmlString += "<td>Amount $</td>";
        htmlString += "<td>Cost After Rebate $</td>";
        htmlString += "<td>Selling $ </td>";
        htmlString += "<td>Margin %</td>";
        
        htmlString += "</tr>";

        htmlString += "<tr>";
        htmlString += "<td>Totals $</td>";
        htmlString += "<td>" + lblLineTotal.Text + "</td>";
        htmlString += "<td>" + lblLineTotalRebate.Text + "</td>";
        htmlString += "<td>" + lblLineTotalSelling.Text + "</td>";
        htmlString += "<td>" + lblLineMargin.Text + "</td>";
        htmlString += "</tr>";
        htmlString += "</table>";  //closed inner table
        
        htmlString += "</td>";
        htmlString += "</tr>";

        htmlString += "<tr><td colspan='3'>&nbsp;</td><tr>";
        htmlString += "<tr><td align='Center' style='border:1px solid'>Product Manager</td><td>&nbsp;</td><td align='Center' style='border:1px solid'>Head Of Finance</td></tr>";
        htmlString += "<tr><td align='Center' style='border:1px solid'><b>" + lblProdctManager.Text + "</b></td><td>&nbsp;</td><td align='Center' style='border:1px solid'><b>" + lblHeadOfOffice.Text + "</b></td></tr>";
        htmlString += "</table></div></body></html>";

        return htmlString;
    }

    private void ReadPdfFile(string pth)
    {

        WebClient client = new WebClient();
        Byte[] buffer = client.DownloadData(pth);

        if (buffer != null)
        {
            Response.ContentType = "application/pdf";
            Response.AddHeader("content-length", buffer.Length.ToString());
            Response.BinaryWrite(buffer);
        }

    }

    //protected void ddlCBNName_SelectedIndexChanged(object sender, EventArgs e)
    //{
    //    lblCBNName1.Text = ddlCBNName.SelectedItem.Text;
    //}
}

