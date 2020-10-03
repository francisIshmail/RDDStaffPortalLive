using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Data;
using System.Collections;
public partial class Intranet_sapBase_SapTallyExportInvoices : System.Web.UI.Page
{
    SAPbobsCOM.Company oCompanyWork;
    SAPbobsCOM.CompanyService oCmpSrv;
    SAPbobsCOM.SBObob oSBObob;
    Boolean statusConnected,invoiceToBeExecuted;

    protected void Page_Load(object sender, EventArgs e)
    {
        Response.CacheControl = "private";
        Response.Expires = 0;
        Response.AddHeader("pragma", "no-cache");

        if (!IsPostBack)
        {
            if (myGlobal.loggedInUser().ToUpper() == "VISHAV")
            {
                btnLoadCustList.Enabled = true;
                btnLoadCustList.Visible = true;
            }
            else
            {
                btnLoadCustList.Enabled = false;
                btnLoadCustList.Visible = false;
            }

            loadDBList(ddlSourceDB, "SOURCEDB");
            loadDBList(ddlDestDB, "DESTINATIONDB");

            //txtFromDate.Text = DateTime.Now.Date.ToString("MM") + "-01-" + DateTime.Now.Date.ToString("yyyy");
            //txtToDate.Text = DateTime.Now.Date.AddDays(0).ToString("MM-dd-yyyy");
            txtExchangeRate.Text = "";
            txtSapUser.Text = "";
            txtSapPwd.Text = "";
            setBtnExportStatus(false);
        }
        lblMsg.Text = "";
        lblError.Text = "";
        lblCustAddedList.Text = "";
    }

    

    
    protected void BindGrid()
    {
        //string summarySQL = "Exec GetSAPTZInvoiceDetailsV2 '" + ddlSourceDB.SelectedItem.Text + "','" + ddlDestDB.SelectedItem.Text + "','" + txtFromDate.Text + "','" + txtToDate.Text + "'";
        string summarySQL = "Exec GetSAPTZInvoiceDetailsV4 '" + ddlSourceDB.SelectedItem.Text + "','" + ddlDestDB.SelectedItem.Text + "'";
            Db.constr = myGlobal.getAppSettingsDataForKey("tejSap");
            Grid1.DataSource = Db.myGetDS(summarySQL).Tables[0];
            Grid1.DataBind();

           if(Grid1.Rows.Count<=0)
            lblRowCnt.Text = "Rows : " + Grid1.Rows.Count.ToString() +  " ,  : No Invoice is to be Exported.";
           else
            lblRowCnt.Text = "Rows : " + Grid1.Rows.Count.ToString();

            setGridColsVisible(false);
    }

    private void loadDBList(DropDownList ddl,String srcORdest)
    {
        string pRet = "";
        string[] arr;

        if (srcORdest == "SOURCEDB")
            pRet = myGlobal.getAppSettingsDataForKey("TallyInvoiceExportSourceDbList");
        else
            pRet = myGlobal.getAppSettingsDataForKey("TallyInvoiceExportDestinationDbList");

        arr = pRet.Split(';');
        ddl.DataSource = arr;
        ddl.DataBind();
    }

    

    private Boolean GridHasExportableRows()
    {
        //this function gets true if grid has at least one exportable row
        Boolean pret = false;

        CheckBox chk;
        foreach (GridViewRow rw in Grid1.Rows)
        {
            if (rw.RowType == DataControlRowType.DataRow)
            {
                chk = (CheckBox)rw.FindControl("chkExport") as CheckBox;
                if (chk.Checked && chk.Enabled)
                {
                    pret = true;
                    break;
                }
            }
        }


        return pret;
    }
    protected void Grid1_RowDataBound(object sender, GridViewRowEventArgs e)
    {
        if (e.Row.RowType == DataControlRowType.DataRow)
        {
            if (Grid1.EditIndex < 0)
            {
                CheckBox tmpchk = (CheckBox)e.Row.FindControl("chkExport") as CheckBox;
                Label tmplbllblLineNum = (Label)e.Row.FindControl("lblLineNum") as Label;
                Label tmplblCustExistsInTly = (Label)e.Row.FindControl("lblCustExistsInTly") as Label;

                if (tmplbllblLineNum.Text == "0")
                {
                    tmpchk.Visible = true;
                    tmplblCustExistsInTly.Visible = true;
                    e.Row.BackColor = System.Drawing.Color.Gold;
                }
                else
                {
                    tmpchk.Visible = false;
                    tmplblCustExistsInTly.Visible = false;
                }

            }
        }
    }
    protected void btnExport_Click(object sender, EventArgs e)
    {
        string conSapCompanyString="";
        

        statusConnected = false; //intitalize false at every attemp of export and check from object to get latest status and update accordingly

        if (GridHasExportableRows()) // if there is at least one invoice selected then it enters if else not
        {
            
            if (ddlDestDB.SelectedItem.Text.Trim() == "")
            {
                lblMsg.Text = "Error ! Invoices Destination DB is not selected";
                return;
            }

            if (txtExchangeRate.Text.Trim() == "" || Util.isValidDecimalNumber(txtExchangeRate.Text.Trim()) == false)
            {
                lblMsg.Text = "Error ! Either the field Exchange RATE is empty or contains a Invalid Value, please supply a valid numeric value and retry";
                return;
            }

            if (txtExchangeRate.Text.Trim() == "0")
            {
                lblMsg.Text = "Error ! Exchange RATE can't be Zero, please supply a valid numeric value";
                return;
            }

            // ask for these if not connected
            if (oCompanyWork == null || oCompanyWork.Connected == false)
            {
                statusConnected = false;
                lblConnectStatus.Text = "Not Connected";

                if (txtSapUser.Text.Trim() == "")
                {
                    lblMsg.Text = "Error ! SAP USER ID field is empty, please supply a valid login id for '" + ddlDestDB.SelectedItem.Text + "'";
                    return;
                }

                if (txtSapPwd.Text.Trim() == "")
                {
                    lblMsg.Text = "Error ! SAP Password field is empty, please supply a valid login password for '" + ddlDestDB.SelectedItem.Text + "'";
                    return;
                }
            }
            else
            {
                statusConnected = true;
            }

            //main work here

            try
            {

                if(ddlDestDB.SelectedItem.Text.ToUpper()=="SAPTLY")
                    conSapCompanyString=myGlobal.getCredsForSapCompany("SAPCompanyConnectCredsTLY");
                else
                    conSapCompanyString=myGlobal.getCredsForSapCompany("SAPCompanyConnectCredsTLY-TEST");



                if (!statusConnected) //if false then only call to reconnect else goes to next code
                    connectSAPDB(ref oCompanyWork, conSapCompanyString, ddlDestDB.SelectedItem.Text);

                if (statusConnected)
                {
                    lblConnectStatus.Text = "Connected @" + DateTime.Now.ToString("hh:mm:ss") ;
                    
                    //step 1 is to create missing customers in tally requred for selected incoices.
                    CreateBPNotInSAPTLY(oCompanyWork);

                    ExportInvoices(oCompanyWork);


                    
                    lblMsg.Text = "Successfully Exported selected invoices to database '" + ddlDestDB.SelectedItem.Text + "'.";

                    //oCompanyWork = null;
                    //lblConnectStatus.Text += "....Disconected @" + DateTime.Now.ToString("hh:mm:ss");
                }
                else
                {
                    lblConnectStatus.Text = "Not Connected";
                    //do nothing as the message has already been printed for not connecting the database in Try block
                }
            }
            catch (Exception exp)
            {
                lblMsg.Text = "Error While Exporting invoices  to database '" + ddlDestDB.SelectedItem.Text + "' ," + exp.Message;
                return;
            }
            
            
        }
        else
        {
            lblMsg.Text = "Warning ! None of the Invoice is selected to be exported , in the Grid below, Please select at leat one invoice to export and retry";
        }

    }

    

    private void ExportInvoices(SAPbobsCOM.Company oCompany)
    {
        invoiceToBeExecuted = false;
        //string prevInvId, currInvId,wrkInvId;
        //prevInvId = "-1"; currInvId = "-1"; wrkInvId = "-1";
        string  trkIdNow = "";
        string invDocCurr;
        int maxInvLineNo, totalInvLines,wrkRow;

        DateTime invTaxdt;

        CheckBox mChk;
        Label mlblDocNum;

        CheckBox cChk;
        Label clblLineNum,clblTotalInvLines, clblDocNum, clblCCode, clblTaxDate, clblRemark, clblTrackNo, clblNumAtCart;
        Label clblDesc, clblGLAcct, clblAmtUSD, clblVatTaxCod;

        lblMsg.Text = "Successfully Created Invoices...";
       
        maxInvLineNo = 0;
        totalInvLines = 0;

        lblCustAddedList.Text = "";
        //foreach (GridViewRow rw in Grid1.Rows)
        for(wrkRow=0;wrkRow<Grid1.Rows.Count;wrkRow++)
        {
            GridViewRow rw = Grid1.Rows[wrkRow]; //get row no.

            if (rw.RowType == DataControlRowType.DataRow)
            {
                mChk = (CheckBox)rw.FindControl("chkExport") as CheckBox;
                mlblDocNum = (Label)rw.FindControl("lblDocNum") as Label;

                if (mChk.Checked && mChk.Enabled)  //if both are true, as we disable the chk after export of the invoice
                {
                    SAPbobsCOM.Documents oFZEARInvoice;
                    oFZEARInvoice = (SAPbobsCOM.Documents)oCompany.GetBusinessObject(SAPbobsCOM.BoObjectTypes.oInvoices);

                    invoiceToBeExecuted = true; //keep the status if the invoice is being created to be executed/exported , 0 case means it has just created a invoice

                   
                    //wrkInvId = mlblDocNum.Text;

                    ////child rows
                    for (int ch = rw.RowIndex; ch<Grid1.Rows.Count; ch++)
                    {
                        //create 3 dynamic string arrays
                        GridViewRow cRw = Grid1.Rows[ch];
                        clblLineNum = (Label)cRw.FindControl("lblLineNum") as Label;
                        clblTotalInvLines = (Label)cRw.FindControl("lblTotalInvLines") as Label;
                        
                        clblDocNum = (Label)cRw.FindControl("lblDocNum") as Label;

                        clblCCode = (Label)cRw.FindControl("lblCardCode") as Label;
                        clblNumAtCart = (Label)cRw.FindControl("lblCustRef") as Label;
                        invDocCurr = "USD"; //verified from the database TZ. suppose to be this by default

                        clblTaxDate = (Label)cRw.FindControl("lblTaxDate") as Label;
                        invTaxdt = Convert.ToDateTime(clblTaxDate.Text);
                       
                        //// slp code suplied 1 as static for finance
                        clblRemark = (Label)cRw.FindControl("lblRemarks") as Label;
                        clblTrackNo = (Label)cRw.FindControl("lblTrackID") as Label;
                        trkIdNow = clblTrackNo.Text; // to use it as method level scope we are storing this value to  a different variable for message purpose

                        clblDesc = (Label)cRw.FindControl("lblLineDescr") as Label;
                        clblGLAcct = (Label)cRw.FindControl("lblTallySalesAcct") as Label;
                        clblVatTaxCod = (Label)cRw.FindControl("lblVatGroup") as Label;
                        clblAmtUSD = (Label)cRw.FindControl("lblAmtUSD") as Label;

                        if (clblDocNum.Text == mlblDocNum.Text)
                        {
                            maxInvLineNo = Convert.ToInt32(clblLineNum.Text);
                            totalInvLines = Convert.ToInt32(clblTotalInvLines.Text);

                            if (clblLineNum.Text == "0")  //if starting line, so get val for max variables
                            {
                                oFZEARInvoice.DocObjectCode = SAPbobsCOM.BoObjectTypes.oInvoices; //13
                                oFZEARInvoice.DocType = SAPbobsCOM.BoDocumentTypes.dDocument_Service; //S

                                //oFZEARInvoice.DocRate = Math.Round(1/(Convert.ToDouble(txtExchangeRate.Text.Trim())),6);
                                oFZEARInvoice.DocRate = Convert.ToDouble(txtExchangeRate.Text.Trim());

                                //oFZEARInvoice.Series =68;    //defined new series range reflects in ONNM , NNM1  tables

                                oFZEARInvoice.HandWritten = SAPbobsCOM.BoYesNoEnum.tYES;  //this specifies if we are going to give any manual docnum, no series applicable , just that the no. should not duplicate existing nos.
                                oFZEARInvoice.DocNum = Convert.ToInt32(clblDocNum.Text);

                                oFZEARInvoice.CardCode = clblCCode.Text;
                                oFZEARInvoice.NumAtCard = clblNumAtCart.Text;
                                oFZEARInvoice.DocCurrency = invDocCurr;  // "USD"

                                oFZEARInvoice.TaxDate = invTaxdt; //document date
                                oFZEARInvoice.DocDate = invTaxdt; //posting date  dd-mm-yyyy format
                                oFZEARInvoice.DocDueDate = invTaxdt; //due date
                                

                                oFZEARInvoice.SalesPersonCode = 1; // 1 for finance
                                oFZEARInvoice.Comments = clblRemark.Text;
                                oFZEARInvoice.TrackingNumber = clblTrackNo.Text;


                                //oFZEARInvoice.Lines.Add();
                                oFZEARInvoice.Lines.ItemDescription = clblDesc.Text;
                                oFZEARInvoice.Lines.VatGroup = clblVatTaxCod.Text;
                                oFZEARInvoice.Lines.AccountCode = clblGLAcct.Text;
                                oFZEARInvoice.Lines.LineTotal = Convert.ToDouble(clblAmtUSD.Text);
                            }
                            else//line no >0
                            {
                                oFZEARInvoice.Lines.Add();
                                oFZEARInvoice.Lines.ItemDescription = clblDesc.Text;
                                oFZEARInvoice.Lines.VatGroup = clblVatTaxCod.Text;
                                oFZEARInvoice.Lines.AccountCode = clblGLAcct.Text;
                                oFZEARInvoice.Lines.LineTotal = Convert.ToDouble(clblAmtUSD.Text);
                                wrkRow = ch; //move main grid row to next ,till where the child loop has worked
                            }
                        }
                        else
                        {
                            try { 
                                         ////Creating invoice here

                                        if (maxInvLineNo == totalInvLines)
                                        {

                                            ////oCompany.StartTransaction();
                                            if (oFZEARInvoice.Add() != 0)
                                            {
                                                lblError.Text = "Error : Failed creating AR Invoice '" + trkIdNow + "' in destination database '" + ddlDestDB.SelectedItem.Text + "' " + oCompany.GetLastErrorDescription();
                                                //// oCompany.EndTransaction(SAPbobsCOM.BoWfTransOpt.wf_RollBack);
                                                invoiceToBeExecuted = false; //keep the status if the invoice is being created to be executed/exported , 0 case means it has just created a invoice
                                            }
                                            else
                                            {
                                                /////oCompany.EndTransaction(SAPbobsCOM.BoWfTransOpt.wf_Commit);
                                                invoiceToBeExecuted = false; //keep the status if the invoice is being created to be executed/exported , 0 case means it has just created a invoice
                                                lblMsg.Text += "  '" + trkIdNow + "'";
                                                mChk.Enabled = false;
                                                mChk.BackColor = System.Drawing.Color.Green;
                                            }

                                            
                                        }
                                        else
                                        {
                                            lblError.Text = "Warning :AR Invoice '" + trkIdNow + "' can't be exported as it seems it is partially fetched ,fetch Data again and retry" ;
                                        }
                                    }
                                catch (Exception ep)
                                {
                                    lblError.Text = ep.Message;
                                }

                            break; //current invoice is over
                        }

                        
                    }//sub loop for grid ends

                            //comming out of child loop check
                            if (invoiceToBeExecuted)  //is true, means there is something pending
                            {
                                try
                                {
                                    ////Creating invoice here
                                    if (maxInvLineNo == totalInvLines)
                                        {
                                            //////oCompany.StartTransaction();
                                            if (oFZEARInvoice.Add() != 0)
                                            {
                                                lblError.Text = "Error : Failed creating AR Invoice '" + trkIdNow + "' in destination database '" + ddlDestDB.SelectedItem.Text + "' " + oCompany.GetLastErrorDescription();
                                                ////oCompany.EndTransaction(SAPbobsCOM.BoWfTransOpt.wf_RollBack);
                                                invoiceToBeExecuted = false; //keep the status if the invoice is being created to be executed/exported , 0 case means it has just created a invoice
                                            }
                                            else
                                            {
                                                ////oCompany.EndTransaction(SAPbobsCOM.BoWfTransOpt.wf_Commit);
                                                invoiceToBeExecuted = false; //keep the status if the invoice is being created to be executed/exported , 0 case means it has just created a invoice
                                                lblMsg.Text += "  '" + trkIdNow + "'";
                                                mChk.Enabled = false;
                                                mChk.BackColor = System.Drawing.Color.Green;
                                            }
                                        }
                                    else
                                    {
                                        lblError.Text = "Warning :AR Invoice '" + trkIdNow + "' can't be exported as it seems it is partially fetched ,fetch Data again and retry";
                                    }
                                }
                                catch (Exception ep)
                                {
                                    lblError.Text = ep.Message;
                                }
                            }

                } //end of if (mChk.Checked)
            } //row type
        }//end of grid row loop
    }
    protected void btnLoadCustList_Click(object sender, EventArgs e)
    {
        lblMsg.Text = "";
        string conSapCompanyString = "";
        statusConnected = false; //intitalize false at every attemp of export and check from object to get latest status and update accordingly

        if (oCompanyWork == null || oCompanyWork.Connected == false)
        {
            statusConnected = false;
            lblConnectStatus.Text = "Not Connected";

            if (txtSapUser.Text.Trim() == "")
            {
                lblMsg.Text = "Error ! SAP USER ID field is empty, please supply a valid login id for '" + ddlDestDB.SelectedItem.Text + "'";
                return;
            }

            if (txtSapPwd.Text.Trim() == "")
            {
                lblMsg.Text = "Error ! SAP Password field is empty, please supply a valid login password for '" + ddlDestDB.SelectedItem.Text + "'";
                return;
            }
        }
        else
        {
            statusConnected = true;
        }


        try
        {

            if (ddlDestDB.SelectedItem.Text.ToUpper() == "SAPTLY")
                conSapCompanyString = myGlobal.getCredsForSapCompany("SAPCompanyConnectCredsTLY");
            else
                conSapCompanyString = myGlobal.getCredsForSapCompany("SAPCompanyConnectCredsTLY-TEST");



            if (!statusConnected) //if false then only call to reconnect else goes to next code
                connectSAPDB(ref oCompanyWork, conSapCompanyString, ddlDestDB.SelectedItem.Text);

            if (statusConnected)
            {
                lblConnectStatus.Text = "Connected @" + DateTime.Now.ToString("hh:mm:ss");

                ///////////////////////////
                //string summarySQL1 = "Exec [tejSap].[dbo].[getBPListToAdd] '" + ddlSourceDB.SelectedItem.Text + "'";
                string summarySQL1 = "Exec [tejSap].[dbo].[getBPListToAddCommon] '" + ddlSourceDB.SelectedItem.Text + "','" + ddlDestDB.SelectedItem.Text + "','C'";
                Db.constr = myGlobal.getAppSettingsDataForKey("tejSap");

                DataTable dtCust = Db.myGetDS(summarySQL1).Tables[0];

                string dbAcctForSAPTLYCustomers, dbccode, dbcname;
                Boolean retFlg = true;

                try
                {
                    if (dtCust.Rows.Count <= 0)
                    {
                        lblMsg.Text = "No New Customers Records found to be exported to Tally. Thanks.";
                        return;
                    }

                    for (int tr = 0; tr < dtCust.Rows.Count; tr++)
                    {
                        dbccode = dtCust.Rows[tr]["CardCode"].ToString();
                        dbcname = dtCust.Rows[tr]["CardName"].ToString();
                        dbAcctForSAPTLYCustomers = dtCust.Rows[tr]["DebPayAcct"].ToString();
                        //dbAcctForSAPTLYCustomers="10-05-030-00-00"; //static for sap tally

                        if (retFlg) //is true
                        {
                            retFlg = CreateBPWithDetails(oCompanyWork, dbccode, dbcname, dbAcctForSAPTLYCustomers,"C");
                            lblMsg.Text += " - " + dbccode;
                        }
                        else
                            break;
                    }
                }
                catch (Exception ep)
                {
                    return; //stop execution
                }


                lblMsg.Text = "Successfully Exported Customers to database '" + ddlDestDB.SelectedItem.Text + "'.";

            }
            else
            {
                lblConnectStatus.Text = "Not Connected";
                //do nothing as the message has already been printed for not connecting the database in Try block
            }
        }
        catch (Exception exp)
        {
            lblMsg.Text = "Error While Exporting Customers  to database '" + ddlDestDB.SelectedItem.Text + "' ," + exp.Message;
            return;
        }

        
    }

    private Boolean CreateBPWithDetails(SAPbobsCOM.Company oCompany,string pccode,string pcname,string pDebAcct,string cTyp)
    {
        int nErr = 0;
        string errMsg = "";
        SAPbobsCOM.BusinessPartners oBP;
        oBP = (SAPbobsCOM.BusinessPartners)oCompany.GetBusinessObject(SAPbobsCOM.BoObjectTypes.oBusinessPartners);

        if (cTyp == "C")
        {
            oBP.CardType = SAPbobsCOM.BoCardTypes.cCustomer;  //C   for customer
            oBP.GroupCode = 100;  //by default send static 100 for all customers group
        }
        else
        {
            oBP.CardType = SAPbobsCOM.BoCardTypes.cSupplier; //S for supplier
            oBP.GroupCode = 101;  //by default send static 101 for all suppliers group
        }

        oBP.CardCode = pccode;
        oBP.CardName = pcname;

        oBP.Currency = "##"; //all currencies
        oBP.PayTermsGrpCode = 1; //by default send static 1 for all customers payment group
        // oBP.CreditLimit = 0;   //nill by default

        oBP.DebitorAccount = pDebAcct;

        if (0 != oBP.Add())
        {
            //'Check Error
            oCompany.GetLastError(out nErr, out errMsg);
            if (0 != nErr)
                lblError.Text = "Failed to add BP: '" + pccode + ", " + nErr.ToString() + "," + errMsg;

            return false;
        }
        else
            return true;
    }

    private void CreateBPNotInSAPTLY(SAPbobsCOM.Company oCompany)
    {
        SAPbobsCOM.BusinessPartners oBP;
        int nErr = 0;
        string errMsg = "";
        CheckBox chk;
        Label lblLineNum, lblCCode, lblCName, lblExistSts, lblSundryDebtorsAcct;
        ArrayList arrCustAdded=new ArrayList ();
        lblCustAddedList.Text = "";
        foreach (GridViewRow rw in Grid1.Rows)
        {
            if (rw.RowType == DataControlRowType.DataRow)
            {
                chk = (CheckBox)rw.FindControl("chkExport") as CheckBox;
                lblLineNum = (Label)rw.FindControl("lblLineNum") as Label;
                lblCCode = (Label)rw.FindControl("lblCardCode") as Label;
                lblCName = (Label)rw.FindControl("lblCardNameTZ") as Label;
                lblExistSts = (Label)rw.FindControl("lblCustExistsInTly") as Label;
                lblSundryDebtorsAcct = (Label)rw.FindControl("lblTallySundryDebtorsAcct") as Label;

                //if (chk.Checked && lblExistSts.Text.ToUpper() == "NO")
                if (lblLineNum.Text == "0" && lblExistSts.Text.ToUpper() == "NO" && !arrCustAdded.Contains(lblCCode.Text))
                {
                    oBP = (SAPbobsCOM.BusinessPartners)oCompany.GetBusinessObject(SAPbobsCOM.BoObjectTypes.oBusinessPartners);

                    oBP.CardType = SAPbobsCOM.BoCardTypes.cCustomer;  // customer or supplier
                    oBP.GroupCode = 100;  //by default send static 100 for all customers group

                    oBP.CardCode = lblCCode.Text;
                    oBP.CardName = lblCName.Text;
                    
                    oBP.Currency = "##"; //all currencies
                    oBP.PayTermsGrpCode = 1; //by default send static 1 for all customers payment group
                   // oBP.CreditLimit = 0;   //nill by default

                    oBP.DebitorAccount = lblSundryDebtorsAcct.Text;

                    if (0 != oBP.Add())
                    {
                        //'Check Error
                        oCompany.GetLastError(out nErr, out errMsg);
                        if (0 != nErr)
                            lblError.Text = "Failed to add BP: '" + lblCCode.Text + ", " + nErr.ToString() + "," + errMsg;
                    }
                    else
                    {
                        lblExistSts.Text = "YES";
                        arrCustAdded.Add(lblCCode.Text);

                        if(lblCustAddedList.Text == "")
                            lblCustAddedList.Text = "Customers Added : " + lblCName.Text;
                        else
                            lblCustAddedList.Text += " ; " + lblCName.Text;
                    }

                }  // end of working if

            }
        } //end of grid loop
    }

    private Boolean connectSAPDB(ref SAPbobsCOM.Company oCompany, string pSapCredsString, string pDB)    
    {
        Boolean pRet=false;
        int lRetCode=0;

        string[] conElements;

        ////////////
        if (oCompany != null)
        {
            if (oCompany.Connected)
            {
                return true;
            }
        }
        //////////////


        if(pSapCredsString=="")
        {
            lblMsg.Text = pDB + " : Connection information not found in settings. Please contact system administrator";
            return false;
        }

        conElements = pSapCredsString.Split(';');

        oCompany = new SAPbobsCOM.Company();
        try
        {
            oCompany.Server = conElements[0]; //'  "fresh"
            oCompany.DbUserName = conElements[1]; // '"sa"
            oCompany.DbPassword = conElements[2]; // ' "Qwert123!@#"
            oCompany.CompanyDB = conElements[3]; // ' "sapUG"

            oCompany.UserName = txtSapUser.Text.Trim();    // ' "manager"
            oCompany.Password = txtSapPwd.Text.Trim(); // '"reddot321"
            oCompany.DbServerType = SAPbobsCOM.BoDataServerTypes.dst_MSSQL2008; //'cmbDBType.SelectedIndex + 1
            oCompany.language = SAPbobsCOM.BoSuppLangs.ln_English;

            
            oCompany.UseTrusted = false;
            oCompany.LicenseServer = conElements[6];  //'    "fresh" & ":" & "30000"

            lRetCode = oCompany.Connect();
            oCmpSrv = oCompany.GetCompanyService();
            oSBObob = (SAPbobsCOM.SBObob)oCompany.GetBusinessObject(SAPbobsCOM.BoObjectTypes.BoBridge);
            
            pRet = true;
        }
        catch(Exception ex)
        {
            pRet = false;
            lblMsg.Text = "Error ! " + pDB + " - " + ex.Message;
        }

        statusConnected = pRet;  //pRet assigns same status to page variable
       return pRet;
    }
    
    
   

    protected void btnGetInvList_Click(object sender, EventArgs e)
    {
        if (ddlSourceDB.SelectedItem.Text.Trim()=="")
        {
            lblMsg.Text = "Error ! Invoices Source DB is not selected.";
            return;
        }

        //if (txtFromDate.Text.Trim() == "" || Util.IsValidDate(txtFromDate.Text) == false)
        //{
        //    lblMsg.Text = "Error ! Either the field FROM DATE is empty or contains a Invalid Date, please supply a valid value in format MM-DD-YYYY";
        //    return;
        //}

        //if (txtToDate.Text.Trim() == "" || Util.IsValidDate(txtToDate.Text) == false)
        //{
        //    lblMsg.Text = "Error ! Either the field TO DATE is empty or contains a Invalid Date, please supply a valid value in format MM-DD-YYYY";
        //    return;
        //}


        BindGrid();

        if(Grid1.Rows.Count>0)
            setBtnExportStatus(true);
        else
            setBtnExportStatus(false);

    }
    private void setBtnExportStatus(Boolean pflg)
    {
        if (pflg)
        {
            btnExport.Enabled = true;
            btnExport.BackColor = System.Drawing.Color.Green;
            btnExport.ForeColor = System.Drawing.Color.Blue;
        }
        else
        {
            btnExport.Enabled = false;
            btnExport.BackColor = System.Drawing.Color.Silver;
            btnExport.ForeColor = System.Drawing.Color.Gray;
        }
    }

    private void setGridColsVisible(Boolean flg)
    {
        Grid1.Columns[11].Visible = flg;
        Grid1.Columns[15].Visible = flg;
        Grid1.Columns[16].Visible = flg;
        Grid1.Columns[17].Visible = flg;
        Grid1.Columns[18].Visible = flg;
        Grid1.Columns[19].Visible = flg;
        Grid1.Columns[20].Visible = flg;
        Grid1.Columns[21].Visible = flg;
        Grid1.Columns[22].Visible = flg;
        Grid1.Columns[23].Visible = flg;
        Grid1.Columns[24].Visible = flg;
    }
   
}