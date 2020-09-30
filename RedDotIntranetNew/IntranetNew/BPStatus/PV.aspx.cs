using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Data;
using System.IO;
using System.Web.Services;
using System.Data.SqlClient;

using iTextSharp.text;
using iTextSharp.text.html.simpleparser;
using iTextSharp.text.pdf;


public partial class IntranetNew_BPStatus_PV : System.Web.UI.Page
{
    protected void Page_Load(object sender, EventArgs e)
    {
        try
        {
            lblMsg.Text = "";
            // select CurrCode from SAPAE.dbo.OCRN -- Currencies
            if (!IsPostBack)
            {
                Bindddl();
                pnlPVApproval.Visible = false;
                string PVRole = string.Empty, PVCountry = string.Empty, DisplayName = string.Empty;
                Db.constr = myGlobal.getMembershipDBConnectionString();
                SqlDataReader rdr = Db.myGetReader(" select Isnull(PVRole,'Employee') PVRole,isnull(PVCountry,'') PVCountry, isnull(DisplayName,'') As DisplayName From dbo.aspnet_Users U where username='" + myGlobal.loggedInUser() + "'");
                if (rdr.HasRows)
                {
                    while (rdr.Read())
                    {
                        PVRole = rdr["PVRole"].ToString();
                        PVCountry = rdr["PVCountry"].ToString();
                        DisplayName = rdr["DisplayName"].ToString();
                    }
                }

                lblUserPVRole.Text = PVRole;
                lblUserPVCountry.Text = PVCountry;
                lblUserDisplayName.Text = DisplayName;
                
                // ADD MODE
                if (Request.QueryString["PVID"] == null) 
                {
                    InitializeSaveMode();
                    BtnExportToPDF.Visible = false;
                    //btnSave.Text = "Save";
                    //SetInitialRow();
                    //EnableDisableControls(PVRole, PVCountry,"Save",true);
                    //txtApprovedAmt.Enabled = false;

                    //if (lblUserPVRole.Text.Trim().ToUpper() == "CA")
                    //{
                    //    ddlDatabase.Enabled = true;
                    //    ddlVoucherType.Enabled = true;
                    //    ddlCountry.Enabled = true;
                    //    txtVendorEmployee.Enabled = true;
                    //}
                    //else
                    //{
                    //    txtVendorEmployee.Text = DisplayName;
                    //    txtBenificiary.Text = DisplayName;
                    //}
                    //try
                    //{
                    //    txtCreatedOn.Text = String.Format("{0:MM/dd/yyyy}", DateTime.Now.ToString() );
                    //}
                    //catch { }
                }
                else  // UPDATE MODE
                {
                    BtnExportToPDF.Visible = true;
                    if (Request.QueryString["PVID"] != null)
                        lblPVId.Text = Request.QueryString["PVID"].ToString();
                    BindData(Request.QueryString["PVID"], PVRole, PVCountry);
                }
            }
        }
        catch (Exception Ex)
        {
            lblMsg.Text = "Error occured in Page_Load : " + Ex.Message;
        }

    }

    private void BindData(string pvid, string pvrole, string pvcountry)
    {
        try
        {
            btnSave.Text = "Update";
            Db.constr = myGlobal.getAppSettingsDataForKey("tejSAP");
            //            DataSet DSPV = Db.myGetDS(@" select  PVId,Country,RefNo,DocStatus,VType,DBName,Currency,VendorCode,VendorEmployee,Benificiary, convert(numeric(19,2),RequestedAmt ) as RequestedAmt,
            //                                            convert(numeric(19,2),ApprovedAmt) As ApprovedAmt, BeingPayOf,PayRequestDate,PayMethod,PayRefNo,PayDate,FilePath,ClosedDate,CAappStatus,
            //                                            CAappRemarks,CAapprovedBy,CAapprovedOn,CMappStatus,CMappRemarks,CMapprovedBy,CMapprovedOn,CFOappStatus,CFOappRemarks,CFOapprovedBy,CFOapprovedOn,
            //                                            CreatedOn,CreatedBy,LastUpdatedOn,LastUpdatedBy,BankCode,BankName
            //                                         from PV
            //                                         where pvid=" + pvid + " ; select PVLineId,PVId,Date,Description,convert(numeric(19,2),amount) as Amount,Remarks,FilePath from PVLines where isnull(IsDeleted,0)=0 And  pvid=" + pvid);
            DataSet DSPV = Db.myGetDS(" EXEC PV_GetPVById  " + lblPVId.Text);
            if (DSPV.Tables.Count > 0)
            {
                DataTable dtPV = DSPV.Tables[0];
                if (dtPV.Rows.Count > 0)   /// START of PV Header
                {
                    if (dtPV.Rows[0]["PVId"] != null && !DBNull.Value.Equals(dtPV.Rows[0]["PVId"]))
                    {
                        lblPVId.Text = dtPV.Rows[0]["PVId"].ToString();
                    }
                    if (dtPV.Rows[0]["Country"] != null && !DBNull.Value.Equals(dtPV.Rows[0]["Country"]))
                    {
                        ddlCountry.SelectedItem.Text = dtPV.Rows[0]["Country"].ToString();
                        ddlCountry.SelectedItem.Value = dtPV.Rows[0]["Country"].ToString();
                    }
                    if (dtPV.Rows[0]["RefNo"] != null && !DBNull.Value.Equals(dtPV.Rows[0]["RefNo"]))
                    {
                        txtRefNo.Text = dtPV.Rows[0]["RefNo"].ToString();
                    }
                    if (dtPV.Rows[0]["DocStatus"] != null && !DBNull.Value.Equals(dtPV.Rows[0]["DocStatus"]))
                    {
                        ddlStatus.SelectedItem.Text = dtPV.Rows[0]["DocStatus"].ToString();
                        ddlStatus.SelectedItem.Value = dtPV.Rows[0]["DocStatus"].ToString();
                    }
                    if (dtPV.Rows[0]["VType"] != null && !DBNull.Value.Equals(dtPV.Rows[0]["VType"]))
                    {
                        ddlVoucherType.SelectedItem.Text = dtPV.Rows[0]["VType"].ToString();
                        ddlVoucherType.SelectedItem.Value = dtPV.Rows[0]["VType"].ToString();
                    }
                    if (dtPV.Rows[0]["DBName"] != null && !DBNull.Value.Equals(dtPV.Rows[0]["DBName"]))
                    {
                        ddlDatabase.SelectedItem.Text = dtPV.Rows[0]["DBName"].ToString();
                        ddlDatabase.SelectedItem.Value = dtPV.Rows[0]["DBName"].ToString();
                    }
                    if (dtPV.Rows[0]["Currency"] != null && !DBNull.Value.Equals(dtPV.Rows[0]["Currency"]))
                    {
                        ddlCurrency.SelectedItem.Text = dtPV.Rows[0]["Currency"].ToString();
                        ddlCurrency.SelectedItem.Value = dtPV.Rows[0]["Currency"].ToString();
                    }
                    if (dtPV.Rows[0]["VendorEmployee"] != null && !DBNull.Value.Equals(dtPV.Rows[0]["VendorEmployee"]))
                    {
                        txtVendorEmployee.Text = dtPV.Rows[0]["VendorEmployee"].ToString();
                    }
                    if (dtPV.Rows[0]["VendorCode"] != null && !DBNull.Value.Equals(dtPV.Rows[0]["VendorCode"]))
                    {
                        lblVendEmployeeCode.Text = dtPV.Rows[0]["VendorCode"].ToString();
                    }
                    if (dtPV.Rows[0]["Benificiary"] != null && !DBNull.Value.Equals(dtPV.Rows[0]["Benificiary"]))
                    {
                        txtBenificiary.Text = dtPV.Rows[0]["Benificiary"].ToString();
                    }
                    if (dtPV.Rows[0]["RequestedAmt"] != null && !DBNull.Value.Equals(dtPV.Rows[0]["RequestedAmt"]))
                    {
                        txtRequestedAmount.Text = dtPV.Rows[0]["RequestedAmt"].ToString();
                    }
                    if (dtPV.Rows[0]["ApprovedAmt"] != null && !DBNull.Value.Equals(dtPV.Rows[0]["ApprovedAmt"]))
                    {
                        txtApprovedAmt.Text = dtPV.Rows[0]["ApprovedAmt"].ToString();
                    }

                    if (dtPV.Rows[0]["PayMethod"] != null && !DBNull.Value.Equals(dtPV.Rows[0]["PayMethod"]))
                    {
                        try
                        {
                            if (!string.IsNullOrEmpty(dtPV.Rows[0]["PayMethod"].ToString()))
                            {
                                ddlPaymentMethod.SelectedItem.Text = dtPV.Rows[0]["PayMethod"].ToString();
                                ddlPaymentMethod.SelectedItem.Value = dtPV.Rows[0]["PayMethod"].ToString();
                            }
                        }
                        catch { }
                    }

                    if (dtPV.Rows[0]["PayRefNo"] != null && !DBNull.Value.Equals(dtPV.Rows[0]["PayRefNo"]))
                    {
                        txtChequeNo.Text = dtPV.Rows[0]["PayRefNo"].ToString();
                    }

                    if (dtPV.Rows[0]["PayDate"] != null && !DBNull.Value.Equals(dtPV.Rows[0]["PayDate"]))
                    {
                        txtChequeDate.Text = String.Format("{0:MM/dd/yyyy}", Convert.ToDateTime(dtPV.Rows[0]["PayDate"]));
                    }

                    if (dtPV.Rows[0]["BankCode"] != null && !DBNull.Value.Equals(dtPV.Rows[0]["BankCode"]))
                    {
                        lblBankCode.Text = dtPV.Rows[0]["BankCode"].ToString();
                    }
                    if (dtPV.Rows[0]["BankName"] != null && !DBNull.Value.Equals(dtPV.Rows[0]["BankName"]))
                    {
                        txtBankName.Text = dtPV.Rows[0]["BankName"].ToString();
                    }

                    if (dtPV.Rows[0]["BeingPayOf"] != null && !DBNull.Value.Equals(dtPV.Rows[0]["BeingPayOf"]))
                    {
                        txtBeingPayOf.Text = dtPV.Rows[0]["BeingPayOf"].ToString();
                    }
                    if (dtPV.Rows[0]["PayRequestDate"] != null && !DBNull.Value.Equals(dtPV.Rows[0]["PayRequestDate"]))
                    {
                        txtPaymentReqDate.Text = String.Format("{0:MM/dd/yyyy}", Convert.ToDateTime(dtPV.Rows[0]["PayRequestDate"]));
                    }
                    if (dtPV.Rows[0]["FilePath"] != null && !DBNull.Value.Equals(dtPV.Rows[0]["FilePath"]))
                    {
                        string FilePath = dtPV.Rows[0]["FilePath"].ToString();
                        if (!string.IsNullOrEmpty(FilePath))
                        {
                            ancChequeImage.HRef = FilePath;
                        }
                    }
                    if (dtPV.Rows[0]["CreatedOn"] != null && !DBNull.Value.Equals(dtPV.Rows[0]["CreatedOn"]))
                    {
                        txtCreatedOn.Text = String.Format("{0:MM/dd/yyyy}", Convert.ToDateTime(dtPV.Rows[0]["CreatedOn"]));
                    }
                    if (dtPV.Rows[0]["CreatedBy"] != null && !DBNull.Value.Equals(dtPV.Rows[0]["CreatedBy"]))
                    {
                        txtCreatedBy.Text = dtPV.Rows[0]["CreatedBy"].ToString();
                    }

                    if (dtPV.Rows[0]["CAappStatus"] != null && !DBNull.Value.Equals(dtPV.Rows[0]["CAappStatus"]))
                    {
                        ddlCAapproval.SelectedItem.Text = dtPV.Rows[0]["CAappStatus"].ToString();
                        ddlCAapproval.SelectedItem.Value = dtPV.Rows[0]["CAappStatus"].ToString();
                    }
                    if (dtPV.Rows[0]["CAappRemarks"] != null && !DBNull.Value.Equals(dtPV.Rows[0]["CAappRemarks"]))
                    {
                        txtCAapprovalRemarks.Text = dtPV.Rows[0]["CAappRemarks"].ToString();
                    }

                    if (dtPV.Rows[0]["CMappStatus"] != null && !DBNull.Value.Equals(dtPV.Rows[0]["CMappStatus"]))
                    {
                        ddlCMapproval.SelectedItem.Text = dtPV.Rows[0]["CMappStatus"].ToString();
                        ddlCMapproval.SelectedItem.Value = dtPV.Rows[0]["CMappStatus"].ToString();
                    }
                    if (dtPV.Rows[0]["CMappRemarks"] != null && !DBNull.Value.Equals(dtPV.Rows[0]["CMappRemarks"]))
                    {
                        txtCMapprovalRemarks.Text = dtPV.Rows[0]["CMappRemarks"].ToString();
                    }

                    if (dtPV.Rows[0]["CFOappStatus"] != null && !DBNull.Value.Equals(dtPV.Rows[0]["CFOappStatus"]))
                    {
                        ddlCFOapproval.SelectedItem.Text = dtPV.Rows[0]["CFOappStatus"].ToString();
                        ddlCFOapproval.SelectedItem.Value = dtPV.Rows[0]["CFOappStatus"].ToString();
                    }
                    if (dtPV.Rows[0]["CFOappRemarks"] != null && !DBNull.Value.Equals(dtPV.Rows[0]["CFOappRemarks"]))
                    {
                        txtCFOapprovalRemarks.Text = dtPV.Rows[0]["CFOappRemarks"].ToString();
                    }

                } /// END of PV Header

                /// To bind GridLines
                if (DSPV.Tables.Count > 1)
                {
                    gvPVLines.DataSource = DSPV.Tables[1];
                    gvPVLines.DataBind();
                }

                pnlPVApproval.Visible = true;

                // User can delete PV if it is created by him/her
                if (dtPV.Rows[0]["CreatedBy"] != null && !DBNull.Value.Equals(dtPV.Rows[0]["CreatedBy"]))
                {
                    if (myGlobal.loggedInUser() == dtPV.Rows[0]["CreatedBy"].ToString())
                    {
                        BtnDelete.Enabled = true;
                    }
                }

                ddlCAapproval.Enabled = false;
                txtCAapprovalRemarks.Enabled = false;
                ddlCMapproval.Enabled = false;
                txtCMapprovalRemarks.Enabled = false;
                ddlCFOapproval.Enabled = false;
                txtCFOapprovalRemarks.Enabled = false;

                ddlPaymentMethod.Enabled = false;
                txtChequeNo.Enabled = false;
                txtChequeDate.Enabled = false;
                fuploadCheckImage.Enabled = false;

                if (pvrole == "Employee")
                {
                    if (ddlCAapproval.SelectedItem.Text == "Approved" || ddlCAapproval.SelectedItem.Text == "Rejected-Closed"
                         || ddlCMapproval.SelectedItem.Text == "Approved" || ddlCMapproval.SelectedItem.Text == "Rejected-Closed"
                         || ddlCFOapproval.SelectedItem.Text == "Approved" || ddlCFOapproval.SelectedItem.Text == "Rejected-Closed")
                    {
                        EnableDisableControls(pvrole, pvcountry, "Update", false);
                        BtnDelete.Enabled = false;
                        BtnAddRow.Enabled = false;
                        btnSave.Enabled = false;
                        txtBankName.Enabled = false;
                    }
                    else
                    {
                        ddlCountry.Enabled = false;
                        ddlDatabase.Enabled = false;
                        ddlVoucherType.Enabled = false;
                        txtVendorEmployee.Enabled = false;
                        txtApprovedAmt.Enabled = false;
                        txtBankName.Enabled = false;
                        //EnableDisableControls(pvrole, pvcountry, "Update", true);
                    }
                    txtApprovedAmt.Enabled = false;
                }
                else if (pvrole == "CA")
                {
                    if (ddlCAapproval.SelectedItem.Text == "Approved" || ddlCAapproval.SelectedItem.Text == "Rejected-Closed")
                    {
                        EnableDisableControls(pvrole, pvcountry, "Update", false);
                        btnSave.Enabled = false;
                        BtnAddRow.Enabled = false;
                        BtnDelete.Enabled = false;
                        // if PV is approved by CA, CM & CFO and PV status is OPEN then keep it enable for CA so that CA can close it.
                        if (ddlCAapproval.SelectedItem.Text == "Approved" && ddlCMapproval.SelectedItem.Text == "Approved" && ddlCFOapproval.SelectedItem.Text == "Approved" && ddlStatus.SelectedItem.Text == "Open")
                        {
                            ddlStatus.Enabled = true;
                            btnSave.Enabled = true;
                        }
                        else
                        {
                            ddlStatus.Enabled = false;
                        }
                        txtApprovedAmt.Enabled = false;
                        txtBankName.Enabled = false;
                    }
                    else
                    {
                        ddlCAapproval.Enabled = true;
                        txtCAapprovalRemarks.Enabled = true;
                        ddlPaymentMethod.Enabled = true;
                        txtChequeNo.Enabled = true;
                        txtChequeDate.Enabled = true;
                        fuploadCheckImage.Enabled = true;
                        txtApprovedAmt.Enabled = true;
                        if (ddlVoucherType.SelectedItem.Text == "Internal")
                        {
                            ddlCountry.Enabled = false;
                            ddlVoucherType.Enabled = false;
                            ddlDatabase.Enabled = false;
                            txtVendorEmployee.Enabled = false;
                            //txtBenificiary.Text = false;
                        }
                        else if (ddlVoucherType.SelectedItem.Text == "Vendor")
                        {
                            ddlCountry.Enabled = false;
                        }

                        if (ddlPaymentMethod.SelectedItem.Text == "Cheque" || ddlPaymentMethod.SelectedItem.Text == "TT")
                        {
                            txtBankName.Enabled = true;
                        }
                        else
                        {
                            txtBankName.Enabled = false;
                        }

                        //EnableDisableControls(pvrole, pvcountry, "Update", true);
                    }

                    if (!string.IsNullOrEmpty(ancChequeImage.HRef))
                    {
                        ancChequeImage.Visible = true;
                    }

                }
                else if (pvrole == "CM")
                {
                    if (!string.IsNullOrEmpty(ancChequeImage.HRef))
                    {
                        ancChequeImage.Visible = true;
                    }
                    if (ddlCAapproval.SelectedItem.Text == "Approved") // If PV is approved by CA then only CM can approve it
                    {
                        ddlCMapproval.Enabled = true;
                        txtCMapprovalRemarks.Enabled = true;
                        BtnAddRow.Enabled = false;
                    }

                    if (ddlCMapproval.SelectedItem.Text == "Approved" || ddlCMapproval.SelectedItem.Text == "Rejected-Closed" || ddlCMapproval.SelectedItem.Text == "Rejected-Open") // If PV is approved by CA then only CM can approve it
                    {
                        btnSave.Enabled = false;
                        ddlCMapproval.Enabled = false;
                        txtCMapprovalRemarks.Enabled = false;
                        BtnAddRow.Enabled = false;
                    }
                    EnableDisableControls(pvrole, pvcountry, "Update", false);
                    txtBankName.Enabled = false;
                }
                else if (pvrole == "CFO")
                {
                    EnableDisableControls(pvrole, pvcountry, "Update", false);
                    // if CA approved PV then CFO can approve on behalf on CM and also can approve CFO approval.
                    if ((ddlCMapproval.SelectedItem.Text == "Pending" || ddlCMapproval.SelectedItem.Text == "--Select--") && ddlCAapproval.SelectedItem.Text == "Approved")
                    {
                        ddlCMapproval.Enabled = true;
                        txtCMapprovalRemarks.Enabled = true;
                        ddlCFOapproval.Enabled = true;
                        txtCFOapprovalRemarks.Enabled = true;
                        BtnAddRow.Enabled = false;
                    }
                    // if PV is approved by both CA & CM then CFO can approve PV so keep ddl enable
                    else if (ddlCMapproval.SelectedItem.Text == "Approved" && ddlCAapproval.SelectedItem.Text == "Approved")
                    {
                        ddlCFOapproval.Enabled = true;
                        txtCFOapprovalRemarks.Enabled = true;
                        BtnAddRow.Enabled = false;
                    }
                    else if (ddlCMapproval.SelectedItem.Text == "Rejected-Closed" || ddlCMapproval.SelectedItem.Text == "Rejected-Open" || ddlCAapproval.SelectedItem.Text == "Rejected-Closed" || ddlCMapproval.SelectedItem.Text == "Rejected-Closed")
                    {
                        BtnAddRow.Enabled = false;
                        btnSave.Enabled = false;
                    }

                    if (ddlCFOapproval.SelectedItem.Text == "Approved" || ddlCFOapproval.SelectedItem.Text == "Rejected-Closed" || ddlCFOapproval.SelectedItem.Text == "Rejected-Closed")
                    {
                        ddlCFOapproval.Enabled = false;
                        txtCFOapprovalRemarks.Enabled = false;
                        btnSave.Enabled = false;
                        BtnAddRow.Enabled = false;
                    }

                    txtApprovedAmt.Enabled = false;
                    if (!string.IsNullOrEmpty(ancChequeImage.HRef))
                    {
                        ancChequeImage.Visible = true;
                    }
                    txtBankName.Enabled = false;
                }
            }
        }
        catch (Exception ex)
        {
            lblMsg.Text = "Error occured in BindData() " + ex.Message;
        }
    }

    private void EnableDisableControls(string PVRole, string PVCountry , string Mode, bool IsEnabled )
    {
        try
        {
            if (Mode == "Save")
            {
                if (PVRole.Trim().ToUpper() == "EMPLOYEE" || PVRole.Trim().ToUpper() == "CM" || PVRole.Trim().ToUpper() == "CFO")
                {
                    txtApprovedAmt.Enabled = false;
                    ddlPaymentMethod.Enabled = false;
                    txtChequeDate.Enabled = false;
                    txtChequeNo.Enabled = false;
                    fuploadCheckImage.Enabled = false;
                    ddlVoucherType.SelectedItem.Text = "Internal";
                    ddlVoucherType.SelectedItem.Value = "Internal";
                    ddlVoucherType.Enabled = false;
                    ddlDatabase.Enabled = false;
                    txtVendorEmployee.Enabled = false;
                    if (PVRole.Trim().ToUpper() == "CFO")
                    {
                        ddlCountry.Enabled = true;
                    }
                    else
                    {
                        ddlCountry.Enabled = false;
                    }
                }
                else if (PVRole.Trim().ToUpper() == "CA")
                {
                    txtApprovedAmt.Enabled = true;
                    ddlPaymentMethod.Enabled = true;
                    txtChequeDate.Enabled = true;
                    txtChequeNo.Enabled = true;
                    fuploadCheckImage.Enabled = true;

                    ddlVoucherType.SelectedItem.Text = "Vendor";
                    ddlVoucherType.SelectedItem.Value = "Vendor";
                    ddlVoucherType.Enabled = true;
                }

                if (!string.IsNullOrEmpty(PVCountry) )
                {
                    ddlCountry.SelectedItem.Text = PVCountry;
                    ddlCountry.SelectedItem.Value = PVCountry;
                    ddlCountry_SelectedIndexChanged(null, null);
                }
            }
            else
            {
                ddlCountry.Enabled = IsEnabled;
                ddlDatabase.Enabled = IsEnabled;
                ddlVoucherType.Enabled = IsEnabled;
                txtVendorEmployee.Enabled = IsEnabled;
                
                ddlCurrency.Enabled = IsEnabled;
                txtBenificiary.Enabled = IsEnabled;
                txtPaymentReqDate.Enabled = IsEnabled;
                txtRequestedAmount.Enabled = IsEnabled;
                
                txtBeingPayOf.Enabled = IsEnabled;
                txtBankName.Enabled = IsEnabled;
                gvPVLines.Enabled = IsEnabled;

                //if (PVRole.ToUpper() == "EMPLOYEE")
                //{
                //    btnSave.Enabled = IsEnabled;
                //    BtnAddRow.Enabled = IsEnabled;
                //    ddlStatus.Enabled = false;
                //    txtApprovedAmt.Enabled = false;
                    
                //    ddlPaymentMethod.Enabled = false;
                //    fuploadCheckImage.Enabled = false;
                //    txtChequeNo.Enabled = false;
                //    txtChequeDate.Enabled = false;
                //}
                //else
                //{
                //    ddlStatus.Enabled = IsEnabled;
                //    txtApprovedAmt.Enabled = IsEnabled;
                //    ddlPaymentMethod.Enabled = IsEnabled;
                //    fuploadCheckImage.Enabled = IsEnabled;

                //    txtChequeNo.Enabled = IsEnabled;
                //    txtChequeDate.Enabled = IsEnabled;
                //}

            }

            ChangeLabelCaption();

        }
        catch (Exception ex)
        {
            lblMsg.Text = "Error occured in  EnableDisableControls: " + ex.Message;
        }
    }

    public void Bindddl()
    {
        try
        {
            lblMsg.Text = "";
            Db.constr = myGlobal.getAppSettingsDataForKey("tejSAP");
            //string sql = "select '--Select--' As Country union all select Country from tejSAP.dbo.rddcountrieslist ;  select '--Select--' As Currency union all select CurrCode from SAPAE.dbo.OCRN";
            string sql = " EXEC PV_GetCountriesAndCurrencies '" + myGlobal.loggedInUser() + "' ";
            DataSet DS = new DataSet();
            DS = Db.myGetDS(sql);
            if (DS.Tables.Count > 0)
            {
                ddlCountry.DataSource = DS.Tables[0];
                ddlCountry.DataTextField = "Country";
                ddlCountry.DataValueField = "Country";
                ddlCountry.DataBind();

                if (DS.Tables.Count > 1)
                {
                    ddlCurrency.DataSource = DS.Tables[1];
                    ddlCurrency.DataTextField = "Currency";
                    ddlCurrency.DataValueField = "Currency";
                    ddlCurrency.DataBind();
                }
            }

            ddlStatus.SelectedItem.Text = "Open";
            ddlStatus.SelectedItem.Value= "Open";
        }
        catch (Exception ex)
        {
            lblMsg.Text = "Error occured in Bindddl : " + ex.Message;
        }
    }

    private void SetInitialRow()
    {
        try
        {
            //Db.constr = myGlobal.getAppSettingsDataForKey("tejSAP");

            //txtrefno.Text = Db.myExecuteScalar2("Select dbo.GetMarketingRefNo('" + ddlCountry.SelectedItem.Value + "')");
            //string refno = txtrefno.Text;
            //lblMsg.Text = "";
            DataTable dt = new DataTable();
            DataRow dr = null;
            dt.Columns.Add(new DataColumn("PVLineId", typeof(int)));
            dt.Columns.Add(new DataColumn("Date", typeof(string)));
            dt.Columns.Add(new DataColumn("Description", typeof(string)));
            dt.Columns.Add(new DataColumn("Amount", typeof(string)));
            dt.Columns.Add(new DataColumn("Remarks", typeof(string)));
            dt.Columns.Add(new DataColumn("FilePath", typeof(string)));
            //dt.Columns.Add(new DataColumn("LineRefNo", typeof(string)));

            dr = dt.NewRow();
            dr["PVLineId"] = "0";
            dr["Date"] = string.Empty;
            dr["Description"] = string.Empty;
            dr["Amount"] = string.Empty;
            dr["Remarks"] = string.Empty;
            dr["FilePath"] = string.Empty;
            //dr["LineRefNo"] = string.Empty;

            dt.Rows.Add(dr);
            gvPVLines.DataSource = dt;
            gvPVLines.DataBind();

        }
        catch (Exception ex)
        {
            lblMsg.Text = "Error occured SetInitialRow() : " + ex.Message;
        }
    }
    /// <summary>
    /// This function is to ADD new row in gridview , If SrNo is 0 then Add Row Event, If SrNo is greater than 0 then Delete Row Event
    /// </summary>
    /// <param name="SrNo"></param>
    private void AddNewRowToGrid( int SrNo )
    {
        try
        {
           
            lblMsg.Text = "";
            DataTable dt = new DataTable();
            DataRow dr = null;
            dt.Columns.Add(new DataColumn("PVLineId", typeof(int)));
            dt.Columns.Add(new DataColumn("Date", typeof(string)));
            dt.Columns.Add(new DataColumn("Description", typeof(string)));
            dt.Columns.Add(new DataColumn("Amount", typeof(string)));
            dt.Columns.Add(new DataColumn("Remarks", typeof(string)));
            dt.Columns.Add(new DataColumn("FilePath", typeof(string)));
            //dt.Columns.Add(new DataColumn("LineRefNo", typeof(string)));

            bool AddEmpltyRow = true; int LineRefMaxCount = 0;

            for (int i = 0; i < gvPVLines.Rows.Count; i++)
            {
                GridViewRow row = gvPVLines.Rows[i];
                
                Label LoopSrNo= (Label)row.FindControl("lblsrno");
                Label lblPVLineId = (Label)row.FindControl("lblPVLineId");
                TextBox txtdate = (TextBox)row.FindControl("txtdate");
                TextBox txtDescription = (TextBox)row.FindControl("txtDescription");
                TextBox txtgvamt = (TextBox)row.FindControl("txtgvamt");
                TextBox txtRemarks = (TextBox)row.FindControl("txtRemarks");
                Label lblFilePath = (Label)row.FindControl("lblFilePath");
                //Label lblLineRefNo = (Label)row.FindControl("lblLineRefNo");

                FileUpload grvfupload = (FileUpload)row.FindControl("fUploadAddattachment");

                if (string.IsNullOrEmpty(txtDescription.Text) && string.IsNullOrEmpty(txtdate.Text) && string.IsNullOrEmpty(txtgvamt.Text) )
                {
                    AddEmpltyRow = false;
                    //if (dt.Rows.Count + 1 == gvPVLines.Rows.Count)  // COMMENTED TO DELETE
                    //{
                    //    dr = dt.NewRow();
                    //    dr["PVLineId"] = "0";
                    //    dr["Date"] = string.Empty;
                    //    dr["Description"] = string.Empty;
                    //    dr["Amount"] = string.Empty;
                    //    dr["Remarks"] = string.Empty;
                    //    dr["FilePath"] = string.Empty;
                    //    //dr["LineRefNo"] = txtRefNo.Text + "-" + (LineRefMaxCount + 1).ToString();
                    //    dt.Rows.Add(dr);
                    //    gvPVLines.DataSource = dt;
                    //    gvPVLines.DataBind();
                    //}
                }
                else
                {
                    if (SrNo != Convert.ToInt32(LoopSrNo.Text) )
                    {
                        dr = dt.NewRow();
                        dr["PVLineId"] = lblPVLineId.Text;
                        dr["Date"] = txtdate.Text;
                        dr["Description"] = txtDescription.Text;
                        dr["Amount"] = txtgvamt.Text;
                        dr["Remarks"] = txtRemarks.Text;

                        if (grvfupload.HasFile)
                        {

                           
                            string folder = "~/excelFileUpload/PV/" + txtRefNo.Text + "_" + myGlobal.loggedInUser() + "/";
                            string dirpath = Server.MapPath(folder);

                            if (!System.IO.Directory.Exists(dirpath))
                            {
                                System.IO.Directory.CreateDirectory(dirpath);
                            }
                            try
                            {
                                /// DELETE previously uploaded file..
                                if (!string.IsNullOrEmpty(lblFilePath.Text))
                                {
                                    string FilePathToDelete = Server.MapPath(lblFilePath.Text);
                                    if (!string.IsNullOrEmpty(FilePathToDelete))
                                    {
                                        if (File.Exists(FilePathToDelete))
                                        {
                                            File.Delete(FilePathToDelete);
                                        }
                                    }
                                }
                            }
                            catch { }

                            /// Save the file
                            string rowLevelFiles = dirpath + grvfupload.FileName; //Server.MapPath(dirpath + fuploadCheckImage.FileName);
                            grvfupload.SaveAs(rowLevelFiles);

                            //ancChequeImage.HRef = folder + fuploadCheckImage.FileName;
                            dr["FilePath"] = folder + grvfupload.FileName;
                        }
                        else
                        {
                            dr["FilePath"] = lblFilePath.Text;
                        }

                        //if (string.IsNullOrEmpty(lblLineRefNo.Text))
                        //{
                        //    dr["LineRefNo"] = txtRefNo.Text + "-1";
                        //    LineRefMaxCount = 1;
                        //}
                        //else
                        //{
                        //    dr["LineRefNo"] = lblLineRefNo.Text;
                        //    string[] maxRefNoarry = lblLineRefNo.Text.Split('-');
                        //    if (Convert.ToInt32(maxRefNoarry[1]) > LineRefMaxCount)
                        //    {
                        //        LineRefMaxCount = Convert.ToInt32(maxRefNoarry[1]);
                        //    }
                        //}
                        dt.Rows.Add(dr);
                    }
                }
            }
            if (AddEmpltyRow==true && SrNo==0)
            {
                dr = dt.NewRow();
                dr["PVLineId"] = "0";
                dr["Date"] = string.Empty;
                dr["Description"] = string.Empty;
                dr["Amount"] = string.Empty;
                dr["Remarks"] = string.Empty;
                dr["FilePath"] = string.Empty;
                //dr["LineRefNo"] = txtRefNo.Text+"-"+(LineRefMaxCount+1).ToString();
                dt.Rows.Add(dr);
                gvPVLines.DataSource = dt;
                gvPVLines.DataBind();
            }

            // this is in case if someone delete row then to bind remaining data
            if (gvPVLines.Rows.Count > 1 && SrNo>0 )
            {
                // this is if someone add one empty row and then delete existing row.. then we should add one empty row
                if (dt.Rows.Count + 2 == gvPVLines.Rows.Count)
                {
                    dr = dt.NewRow();
                    dr["PVLineId"] = "0";
                    dr["Date"] = string.Empty;
                    dr["Description"] = string.Empty;
                    dr["Amount"] = string.Empty;
                    dr["Remarks"] = string.Empty;
                    dr["FilePath"] = string.Empty;
                    //dr["LineRefNo"] = txtRefNo.Text + "-" + (LineRefMaxCount + 1).ToString();
                    dt.Rows.Add(dr);
                    //gvPVLines.DataSource = dt;
                    //gvPVLines.DataBind();
                }
                gvPVLines.DataSource = dt;
                gvPVLines.DataBind();
            }

            ChangeLabelCaption();

        }
        catch (Exception ex)
        {
            lblMsg.Text = "Error occured AddNewRowToGrid() : " + ex.Message;
        }
    }

    protected void BtnAddRow_Click(object sender, EventArgs e)
    {
        try
        {
            lblMsg.Text = "";
            if (fuploadCheckImage.HasFile) // This is to uplaod the check image before adding Row...
            {
                string exten = System.IO.Path.GetExtension(fuploadCheckImage.FileName);
                if (exten.ToLower() != ".pdf" && exten.ToLower() != ".jpeg" && exten.ToLower() != ".jpg" )
                {
                    lblMsg.Text = "Invalid file format for cheque/TT/PayRef image, Supported file formats are .pdf,.jpeg, .jpg ";
                    return;
                }

                string folder = "~/excelFileUpload/PV/" + txtRefNo.Text + "_" + myGlobal.loggedInUser() + "/";
                string dirpath = Server.MapPath( folder );
                
                if (!System.IO.Directory.Exists(dirpath))
                {
                    System.IO.Directory.CreateDirectory(dirpath);
                }

                try // code to delete previously updated file if new file is uploaded
                {
                    string OldChequelink = Server.MapPath(ancChequeImage.HRef);
                    if (!string.IsNullOrEmpty(OldChequelink))
                    {
                        if (File.Exists(OldChequelink))
                        {
                            File.Delete(OldChequelink);
                        }
                    }
                }
                catch { }
                
                string chequeFilePath = dirpath + fuploadCheckImage.FileName; //Server.MapPath(dirpath + fuploadCheckImage.FileName);
                fuploadCheckImage.SaveAs(chequeFilePath);

                ancChequeImage.HRef = folder + fuploadCheckImage.FileName;
                ancChequeImage.Visible = true;
            }
            ValidateRowLevelFilesFormatAndSize();
            AddNewRowToGrid(0);
        }
        catch (Exception ex)
        {
            lblMsg.Text = "Error occured BtnAddRow() : " + ex.Message;
        }
    }

    public void ValidateRowLevelFilesFormatAndSize()
    {

        for (int i = 0; i < gvPVLines.Rows.Count; i++)
        {
            GridViewRow row = gvPVLines.Rows[i];
            Label LoopSrNo = (Label)row.FindControl("lblsrno");
            FileUpload grvfupload = (FileUpload)row.FindControl("fUploadAddattachment");

            string exten = System.IO.Path.GetExtension(grvfupload.FileName);
            if (exten.ToLower() != ".pdf" && exten.ToLower() != ".doc" && exten.ToLower() != ".docx" && exten.ToLower() != ".jpeg" && exten.ToLower() != ".jpg" && exten.ToLower() != ".xlsx")
            {
                lblMsg.Text = "Invalid file format for supporting document for Row No " + LoopSrNo.Text + ", Supported file formats are .pdf,.jpeg, .jpg, .doc, .docx, .xlsx ";
                return;
            }

            const int ThreeMegaBytes = 3 * 1024 * 1024;
            if (grvfupload.PostedFile.ContentLength > ThreeMegaBytes)
            {
                lblMsg.Text = "File size is too large for RowNo " + LoopSrNo.Text + ", Maximun allowable size is 3 mb";
                return;
            }
        }
    }

    private void ChangeLabelCaption()
    {
        try
        {
            if (ddlVoucherType.SelectedItem.Text == "Internal")
            {
                lblVendorEmploye.InnerText = "Employee";
            }
            else if (ddlVoucherType.SelectedItem.Text == "Vendor")
            {
                lblVendorEmploye.InnerText = "Vendor";
            }
            else
            {
                lblVendorEmploye.InnerText = "Vendor/Employee";
            }

            if (ddlPaymentMethod.SelectedItem.Text == "Cheque")
            {
                lblChequeNo.InnerText="Cheque No";
                lblChequeDate.InnerText="Cheque Date";
                lblChequeImage.InnerText="Cheque Image";
                txtBankName.Enabled = true;
            }
            else if (ddlPaymentMethod.SelectedItem.Text == "TT")
            {
                lblChequeNo.InnerText="TT No";
                lblChequeDate.InnerText="TT Date";
                lblChequeImage.InnerText="TT Image";
                txtBankName.Enabled = true;
            }
            else
            {
                lblChequeNo.InnerText="Pay Ref No";
                lblChequeDate.InnerText="Pay Ref Date";
                lblChequeImage.InnerText="Pay Ref Image";
                txtBankName.Enabled = false;
            }

        }
        catch (Exception ex)
        {
            lblMsg.Text = "Error occured ChangeLabelCaption() : " + ex.Message;
        }
    }

    protected void BtnDelete_Click(object sender, EventArgs e)
    {
        try
        {
            lblMsg.Text = "";
            string SrNo = (((Label)(((Button)sender).NamingContainer as GridViewRow).FindControl("lblsrno")).Text);
            string PVLineId = (((Label)(((Button)sender).NamingContainer as GridViewRow).FindControl("lblPVLineId")).Text);
            string FilePathToDelete = (((Label)(((Button)sender).NamingContainer as GridViewRow).FindControl("lblFilePath")).Text);
            //string LineRefNo = (((Label)(((Button)sender).NamingContainer as GridViewRow).FindControl("lblLineRefNo")).Text);
            if (!string.IsNullOrEmpty(SrNo))
            {
                //if (Session["LineRefNo"] != null)  // LineRefNo session holds the URL of saved file at row level
                //{
                //    Session.Remove("LineRefNo");
                //}
                AddNewRowToGrid(Convert.ToInt32(SrNo));

                if (!string.IsNullOrEmpty(PVLineId) && PVLineId != "0")
                {
                    Db.constr = myGlobal.getAppSettingsDataForKey("tejSAP");
                    Db.myExecuteSQL(" Update dbo.PVLines set IsDeleted=1 Where PVLineId= "+PVLineId);

                    try // code to delete previously updated file if record is deleted..
                    {
                        FilePathToDelete = Server.MapPath(FilePathToDelete);
                        if (!string.IsNullOrEmpty(FilePathToDelete))
                        {
                            if (File.Exists(FilePathToDelete))
                            {
                                File.Delete(FilePathToDelete);
                            }
                        }
                    }
                    catch { }
                }
            }
        }
        catch (Exception ex)
        {
            lblMsg.Text = "Error occured BtnDelete() : " + ex.Message;
        }
    }

    [WebMethod]
    public static string[] GetVendors(string prefix, string dbname, string emporvend)
    {
        
            List<string> vendors = new List<string>();

            Db.constr = myGlobal.getAppSettingsDataForKey("tejSAP");

            int IsVendor = 1;
            if (emporvend == "Vendor")
            {
                IsVendor = 1;
            }
            else
            {
                IsVendor = 0;
            }

            string qry = " Exec PV_GetVendors_Employees '" + prefix + "','" + dbname + "'," + IsVendor;

            SqlDataReader rdr = Db.myGetReader(qry);
            while (rdr.Read())
            {
                vendors.Add(string.Format("{0}#{1}", rdr["CardName"], rdr["CardCode"]));
            }
            
        return vendors.ToArray();
        
    }

    [WebMethod]
    public static string[] GetBanks(string prefix, string dbname)
    {

        List<string> Banks = new List<string>();

        Db.constr = myGlobal.getAppSettingsDataForKey("tejSAP");

        string qry = " Exec PV_GetBankLists '" + prefix + "','" + dbname + "'";

        SqlDataReader rdr = Db.myGetReader(qry);
        while (rdr.Read())
        {
            Banks.Add(string.Format("{0}#{1}", rdr["AcctName"], rdr["AcctCode"]));
        }

        return Banks.ToArray();
    }

    //[WebMethod]
    //public static string getRefNo(string Country)
    //{
    //    string refNo = "";
    //    if (Country != "--Select--")
    //        refNo = Db.myExecuteScalar2("select dbo.GetPVRefNo('" + Country + "') As RefNo");
    //    return refNo;
    //}

    protected void btnSave_Click(object sender, EventArgs e)
    {
        try
        {

            if (ddlCountry.SelectedItem.Text == "--Select--")
            {
                lblMsg.Text = "Please select country";
                return;
            }

            ChangeLabelCaption();

            if (ddlVoucherType.SelectedItem.Text == "--Select--")
            {
                lblMsg.Text = "Please select voucher type";
                return;
            }
            else if (ddlVoucherType.SelectedItem.Text == "Internal")
            {
                if (string.IsNullOrEmpty(txtVendorEmployee.Text))
                {
                    lblMsg.Text = "Please enter employee";
                    return;
                }
            }
            else if (ddlVoucherType.SelectedItem.Text == "Vendor")
            {
                if (ddlDatabase.SelectedItem.Text == "--Select--")
                {
                    lblMsg.Text = "Please select database";
                    return;
                }

                if (string.IsNullOrEmpty(txtVendorEmployee.Text))
                {
                    lblMsg.Text = "Please enter Vendor";
                    return;
                }
            }

            if (ddlCurrency.SelectedItem.Text == "--Select--")
            {
                lblMsg.Text = "Please select currency";
                return;
            }

            if (string.IsNullOrEmpty(txtBenificiary.Text))
            {
                lblMsg.Text = "Please enter Benificiary";
                return;
            }

            if (string.IsNullOrEmpty(txtPaymentReqDate.Text))
            {
                lblMsg.Text = "Please enter payment request date";
                return;
            }

            if (string.IsNullOrEmpty(txtRequestedAmount.Text))
            {
                lblMsg.Text = "Please enter requested amount";
                return;
            }

            if (Convert.ToDecimal(txtRequestedAmount.Text)<=0)
            {
                lblMsg.Text = "Requested amount must be greater than zero";
                return;
            }

            if (string.IsNullOrEmpty(txtBeingPayOf.Text))
            {
                lblMsg.Text = "Please enter Being Payment Of";
                return;
            }

            if (gvPVLines.Rows.Count == 0)
            {
                lblMsg.Text = " Please enter atleast one row for payment voucher.";
                return;
            }

            decimal rowAmounttot = 0;
            foreach (GridViewRow row in gvPVLines.Rows)
            {
                Label LoopSrNo = (Label)row.FindControl("lblsrno");
                //Label lblPVLineId = (Label)row.FindControl("lblPVLineId");
                TextBox txtdate = (TextBox)row.FindControl("txtdate");
                TextBox txtDescription = (TextBox)row.FindControl("txtDescription");
                TextBox txtgvamt = (TextBox)row.FindControl("txtgvamt");
                TextBox txtRemarks = (TextBox)row.FindControl("txtRemarks");
                Label lblFilePath = (Label)row.FindControl("lblFilePath");
                //Label lblLineRefNo = (Label)row.FindControl("lblLineRefNo");
                FileUpload grvfupload = (FileUpload)row.FindControl("fUploadAddattachment");
                
                //if (gvPVLines.Rows.Count > 1)
                //{
                    if (string.IsNullOrEmpty(txtdate.Text) || string.IsNullOrEmpty(txtDescription.Text) || string.IsNullOrEmpty(txtgvamt.Text) || string.IsNullOrEmpty(txtRemarks.Text))
                    {
                        lblMsg.Text = "All fields (Date, Description, Amount, Remarks ) are mandatory for Row " + LoopSrNo.Text;
                        return;
                    }

                    if (!string.IsNullOrEmpty(txtgvamt.Text) && Convert.ToDecimal(txtgvamt.Text) <= 0)
                    {
                        lblMsg.Text = "Amount must be greater than zero for Row " + LoopSrNo.Text;
                        return;
                    }
                    else
                    {
                        rowAmounttot = rowAmounttot + Convert.ToDecimal(txtgvamt.Text);
                        if (rowAmounttot > Convert.ToDecimal(txtRequestedAmount.Text))
                        {
                            lblMsg.Text = "Total amount of all rows can not be greater than requested amount.";
                            return;
                        }
                    }

                    if (string.IsNullOrEmpty(lblFilePath.Text) && grvfupload.HasFile == false)
                    {
                        lblMsg.Text = "Please upload supporting document for Row " + LoopSrNo.Text;
                        return;
                    }
                    
                    if (grvfupload.HasFile)
                    {
                        string exten = System.IO.Path.GetExtension(grvfupload.FileName);
                        if (exten.ToLower() != ".pdf" && exten.ToLower() != ".doc" && exten.ToLower() != ".docx" && exten.ToLower() != ".jpeg" && exten.ToLower() != ".jpg" && exten.ToLower() != ".xlsx")
                        {
                            lblMsg.Text = "Invalid file format for supporting document for Row No " + LoopSrNo.Text + ", Supported file formats are .pdf,.jpeg, .jpg, .doc, .docx, .xlsx ";
                            return;
                        }

                        const int ThreeMegaBytes = 3 * 1024 * 1024;
                        if (grvfupload.PostedFile.ContentLength > ThreeMegaBytes)
                        {
                            lblMsg.Text = "File size is too large for RowNo " + LoopSrNo.Text + ", Maximun allowable size is 3 mb";
                            return;
                        }
                    }

                //}
                //else
                //{
                //}
            }

            string approvedAmt="0";
            if(!string.IsNullOrEmpty(txtApprovedAmt.Text) && Convert.ToDecimal(txtApprovedAmt.Text)>0 )
            {
                if (Convert.ToDecimal(txtApprovedAmt.Text) > Convert.ToDecimal(txtRequestedAmount.Text))
                {
                    lblMsg.Text = "Approved amount can not be greater than requested amount";
                    return;
                }
                approvedAmt=txtApprovedAmt.Text;
            }

            // if payment method is cheque or TT then bank name is mandatory.
            if (ddlPaymentMethod.SelectedItem.Text == "Cheque" || ddlPaymentMethod.SelectedItem.Text == "TT")
            {
                if (string.IsNullOrEmpty(txtBankName.Text))
                {
                    lblMsg.Text = "Please enter bank name";
                    return;
                }

                //if (btnSave.Text == "Save" && !string.IsNullOrEmpty(txtChequeDate.Text))
                //{
                //    int count = DateTime.Compare(Convert.ToDateTime(txtChequeDate.Text), DateTime.Now);
                //    if (count < 0)
                //    {
                //        lblMsg.Text = "Cheque Date can not be less than todays date";
                //        return;
                //    }
                //}

                if (fuploadCheckImage.HasFile)
                {
                    string exten = System.IO.Path.GetExtension(fuploadCheckImage.FileName);
                    if (exten.ToLower() != ".pdf" && exten.ToLower() != ".jpeg" && exten.ToLower() != ".jpg")
                    {
                        lblMsg.Text = "Invalid file format for cheque/TT/PayRef image, Supported file formats are .pdf,.jpeg, .jpg ";
                        return;
                    }
                }

            }

            #region " Start : Approval validation "

            #region " CA Approved /  Reject validation in update mode "

            // if appvedAmt is greater than Zero then CA approval status must be Approved And paymethod must be selected.
            if (lblUserPVRole.Text.Trim().ToUpper() == "CA")
            {
                if (Convert.ToDecimal(approvedAmt) > 0 || ddlCAapproval.SelectedItem.Text == "Approved")
                {
                    if (Convert.ToDecimal(approvedAmt) > 0 && ddlCAapproval.SelectedItem.Text != "Approved")
                    {
                        lblMsg.Text = "CA approval status must be Approved if approved amount is greater than 0";
                        return;
                    }

                    if (Convert.ToDecimal(approvedAmt) <= 0 && ddlCAapproval.SelectedItem.Text == "Approved")
                    {
                        lblMsg.Text = "Approved amount must be greater than zero if CA approval status is Approved";
                        return;
                    }

                    if (string.IsNullOrEmpty(txtCAapprovalRemarks.Text))
                    {
                        lblMsg.Text = "Please enter CA remarks";
                        return;
                    }

                    if (ddlPaymentMethod.SelectedItem.Text == "--Select--")
                    {
                        lblMsg.Text = "Please select payment Method";
                        return;
                    }

                    if (ddlPaymentMethod.SelectedItem.Text == "Cash" || ddlPaymentMethod.SelectedItem.Text == "Other")
                    {
                        if (string.IsNullOrEmpty(txtChequeNo.Text))
                        {
                            lblMsg.Text = "Please enter payment Ref No";
                            return;
                        }
                        if (string.IsNullOrEmpty(txtChequeDate.Text))
                        {
                            lblMsg.Text = "Please enter payment Ref Date";
                            return;
                        }
                    }

                    if (ddlPaymentMethod.SelectedItem.Text == "Cheque")
                    {
                        if (string.IsNullOrEmpty(txtChequeNo.Text))
                        {
                            lblMsg.Text = "Please enter cheque No";
                            return;
                        }
                        if (string.IsNullOrEmpty(txtChequeDate.Text))
                        {
                            lblMsg.Text = "Please enter cheque Date";
                            return;
                        }

                        if (fuploadCheckImage.HasFile == false && string.IsNullOrEmpty(ancChequeImage.HRef))
                        {
                            lblMsg.Text = "Please upload cheque Image";
                            return;
                        }
                    }

                    if (ddlPaymentMethod.SelectedItem.Text == "TT" && ddlStatus.SelectedItem.Text == "Paid - Closed")
                    {
                        if (string.IsNullOrEmpty(txtChequeNo.Text))
                        {
                            lblMsg.Text = "Please enter TT No";
                            return;
                        }
                        if (string.IsNullOrEmpty(txtChequeDate.Text))
                        {
                            lblMsg.Text = "Please enter TT Date";
                            return;
                        }
                        if (fuploadCheckImage.HasFile == false && string.IsNullOrEmpty(ancChequeImage.HRef))
                        {
                            lblMsg.Text = "Please upload TT Image";
                            return;
                        }
                    }
                }
                else if (ddlCAapproval.SelectedItem.Text == "Rejected-Open" || ddlCAapproval.SelectedItem.Text == "Rejected-Closed")
                {
                    if (btnSave.Text == "Save")  /// if CA is adding PV and trying to select Rejected-Open or Rejected-Closed then show error msg
                    {
                        lblMsg.Text = "CA approval status can not be "+ddlCAapproval.SelectedItem.Text +" while adding new PV." ;
                        return;
                    }
                    if (string.IsNullOrEmpty(txtCAapprovalRemarks.Text))
                    {
                        lblMsg.Text = "Please enter CA remarks";
                        return;
                    }
                    if (ddlStatus.SelectedItem.Text == "Paid - Closed")
                    {
                        lblMsg.Text = "PV status can not be Paid - Closed, if CA approval status is " + ddlCAapproval.SelectedItem.Text;
                        return;
                    }
                    if (Convert.ToDecimal(approvedAmt) != 0)
                    {
                        lblMsg.Text = "Approved amount must be zero if CA approval status is " + ddlCAapproval.SelectedItem.Text;
                        return;
                    }
                    if (ddlCAapproval.SelectedItem.Text == "Rejected-Open" && ddlStatus.SelectedItem.Text == "Rejected-Closed")
                    {
                        lblMsg.Text = "PV status must be Open if CA approval status " + ddlCAapproval.SelectedItem.Text;
                        return;
                    }
                }
                else if (ddlCAapproval.SelectedItem.Text == "Pending")
                {
                    if (Convert.ToDecimal(approvedAmt) != 0)
                    {
                        lblMsg.Text = "Approved amount must be zero if CA approval status is " + ddlCAapproval.SelectedItem.Text;
                        return;
                    }
                    if (ddlStatus.SelectedItem.Text != "Open")
                    {
                        lblMsg.Text = "PV status must be open if CA approval status is " + ddlCAapproval.SelectedItem.Text;
                        return;
                    }
                }
            }

            #endregion // CA approved / Rejected

            if (btnSave.Text == "Update")
            {
                if (lblUserPVRole.Text.Trim().ToUpper() == "CM") // CM approval validation
                {
                    if (ddlCMapproval.SelectedItem.Text != "Pending" && ddlCMapproval.SelectedItem.Text != "--Select--")
                    {
                        if (string.IsNullOrEmpty(txtCMapprovalRemarks.Text))
                        {
                            lblMsg.Text = "Please enter CM remarks";
                            return;
                        }
                    }

                    if (ddlCMapproval.SelectedItem.Text == "Pending" || ddlCMapproval.SelectedItem.Text == "--Select--")
                    {
                        lblMsg.Text = "Please select CM approval status";
                        return;
                    }
                }

                if (lblUserPVRole.Text.Trim().ToUpper() == "CFO") // CFO approval validation
                {
                    if (ddlCMapproval.SelectedItem.Text != "Approved" && ddlCFOapproval.SelectedItem.Text == "Approved")
                    {
                        lblMsg.Text = "CM approval must be approved, if CFO approval is approved";
                        return;
                    }
                    if (ddlCMapproval.SelectedItem.Text != "Pending" && ddlCMapproval.SelectedItem.Text != "--Select--")
                    {
                        if (string.IsNullOrEmpty(txtCMapprovalRemarks.Text))
                        {
                            lblMsg.Text = "Please enter CM remarks";
                            return;
                        }
                    }

                    if ((ddlCFOapproval.SelectedItem.Text == "Pending" || ddlCFOapproval.SelectedItem.Text == "--Select--") && ddlCMapproval.SelectedItem.Text == "Approved")
                    {
                        lblMsg.Text = "Please select CFO approval status";
                        return;
                    }

                    if (ddlCFOapproval.SelectedItem.Text != "Pending" && ddlCFOapproval.SelectedItem.Text != "--Select--")
                    {
                        if (string.IsNullOrEmpty(txtCFOapprovalRemarks.Text))
                        {
                            lblMsg.Text = "Please enter CFO remarks";
                            return;
                        }
                    }

                    if (ddlCMapproval.SelectedItem.Text == "Rejected-Open" && ddlCFOapproval.SelectedItem.Text == "Rejected-Closed")
                    {
                        lblMsg.Text = "CFO approval status can not be "+ ddlCFOapproval.SelectedItem.Text +" when CM approval status is Rejected - Open" ;
                        return;
                    }
                    if (ddlCMapproval.SelectedItem.Text == "Rejected-Closed" && ddlCFOapproval.SelectedItem.Text == "Rejected-Open")
                    {
                        lblMsg.Text = "CFO approval status can not be " + ddlCFOapproval.SelectedItem.Text + " when CM approval status is Rejected - Closed";
                        return;
                    }
                    
                }
            }

            #endregion "End : Approval validation"

            string PayMethod=string.Empty,PayRefNo=string.Empty,PayDate=string.Empty,FilePath=string.Empty;

            if (ddlPaymentMethod.SelectedItem.Text != "--Select--" && lblUserPVRole.Text.Trim().ToUpper() == "CA")  // Keep this open only for CA, b,coz CA uploads documents
            {

                if (ddlPaymentMethod.SelectedItem.Text == "Cheque" && (fuploadCheckImage.HasFile==false && string.IsNullOrEmpty(ancChequeImage.HRef) ))
                {
                    lblMsg.Text = "please upload cheque image";
                    return;
                }

                PayMethod = ddlPaymentMethod.SelectedItem.Text;
                PayRefNo = txtChequeNo.Text;
                PayDate = txtChequeDate.Text;

                FilePath = ancChequeImage.HRef;

                if (fuploadCheckImage.HasFile) // This is to uplaod the check image before adding Row...
                {
                    string folder = "~/excelFileUpload/PV/" + txtRefNo.Text + "_" + myGlobal.loggedInUser() + "/";
                    string dirpath = Server.MapPath(folder);

                    if (!System.IO.Directory.Exists(dirpath))
                    {
                        System.IO.Directory.CreateDirectory(dirpath);
                    }

                    try // code to delete previously updated file if new file is uploaded
                    {
                        string OldChequelink = Server.MapPath(ancChequeImage.HRef);
                        if (!string.IsNullOrEmpty(OldChequelink))
                        {
                            if (File.Exists(OldChequelink))
                            {
                                File.Delete(OldChequelink);
                            }
                        }
                    }
                    catch { }

                    string chequeFilePath = dirpath + fuploadCheckImage.FileName; //Server.MapPath(dirpath + fuploadCheckImage.FileName);
                    fuploadCheckImage.SaveAs(chequeFilePath);

                    FilePath = folder + fuploadCheckImage.FileName;
                }
            }

            if (btnSave.Text == "Save") // SAVE MODE
            {
                string sql = " Declare @PVId int;    Insert into PV(Country,RefNo,DocStatus,VType,DBName,Currency,VendorCode,VendorEmployee,Benificiary,RequestedAmt,ApprovedAmt,BeingPayOf,PayRequestDate,PayMethod,PayRefNo,PayDate,FilePath,CreatedOn,CreatedBy,CAappstatus,BankCode,BankName) ";
                sql = sql + " Values ('" + ddlCountry.SelectedItem.Text + "',dbo.GetPVRefNo('" + ddlCountry.SelectedItem.Text + "'),'" + ddlStatus.SelectedItem.Text + "','" + ddlVoucherType.SelectedItem.Text + "','" + ddlDatabase.SelectedItem.Text + "','" + ddlCurrency.SelectedItem.Text + "','"+lblVendEmployeeCode.Text+"','" + txtVendorEmployee.Text + "','" + txtBenificiary.Text + "'," + txtRequestedAmount.Text + "," + approvedAmt + ",'" + txtBeingPayOf.Text + "','" + txtPaymentReqDate.Text + "','" + PayMethod + "','" + PayRefNo + "','" + PayDate + "','" + FilePath + "',getdate(),'" + myGlobal.loggedInUser() + "','Pending','"+lblBankCode.Text+"','"+txtBankName.Text+"') ; ";
                sql = sql + "  set @PVId=  @@Identity ; ";

                foreach (GridViewRow row in gvPVLines.Rows)
                {
                    Label LoopSrNo = (Label)row.FindControl("lblsrno");
                    TextBox txtdate = (TextBox)row.FindControl("txtdate");
                    TextBox txtDescription = (TextBox)row.FindControl("txtDescription");
                    TextBox txtgvamt = (TextBox)row.FindControl("txtgvamt");
                    TextBox txtRemarks = (TextBox)row.FindControl("txtRemarks");
                    Label lblFilePath = (Label)row.FindControl("lblFilePath");
                    FileUpload grvfupload = (FileUpload)row.FindControl("fUploadAddattachment");

                    string rowLevelfilepath = "";
                    if (!string.IsNullOrEmpty(lblFilePath.Text))
                    {
                        rowLevelfilepath = lblFilePath.Text;
                    }
                    if (grvfupload.HasFile)
                    {
                        string folder = "~/excelFileUpload/PV/" + txtRefNo.Text + "_" + myGlobal.loggedInUser() + "/";
                        string dirpath = Server.MapPath(folder);

                        if (!System.IO.Directory.Exists(dirpath))
                        {
                            System.IO.Directory.CreateDirectory(dirpath);
                        }
                        try
                        {
                            /// DELETE previously uploaded file..
                            if (!string.IsNullOrEmpty(lblFilePath.Text))
                            {
                                string FilePathToDelete = Server.MapPath(lblFilePath.Text);
                                if (!string.IsNullOrEmpty(FilePathToDelete))
                                {
                                    if (File.Exists(FilePathToDelete))
                                    {
                                        File.Delete(FilePathToDelete);
                                    }
                                }
                            }
                        }
                        catch { }
                        /// Save the file
                        string rowLevelFiles = dirpath + grvfupload.FileName; //Server.MapPath(dirpath + fuploadCheckImage.FileName);
                        grvfupload.SaveAs(rowLevelFiles);
                        rowLevelfilepath = folder + grvfupload.FileName;
                    }

                    sql = sql + " ; insert into PVLines( PVId,[Date],[Description],Amount,Remarks,FilePath,CreatedOn,CreatedBy ) ";
                    sql = sql + " Values ( @PVId ,'" + txtdate.Text + "','" + txtDescription.Text + "','" + txtgvamt.Text + "','" + txtRemarks.Text + "','" + rowLevelfilepath + "',getdate(),'" + myGlobal.loggedInUser() + "' )  ";
                }

                sql = sql + " ; insert into PVApprovalLog(PVId,ActionBy,ActionOn,ActionStatus,Role,Remarks,LogDate) values ( @PVId ,'" + myGlobal.loggedInUser() + "',getdate(),'Pending','"+lblUserPVRole.Text.Trim().ToUpper() +"','NA',getdate()) ";

                if (lblUserPVRole.Text.Trim().ToUpper() == "CA")
                {
                    sql = sql + " ; Update PV Set CAappStatus='" + ddlCAapproval.SelectedItem.Text + "',CAappRemarks='" + txtCAapprovalRemarks.Text + "',CAapprovedBy='" + myGlobal.loggedInUser() + "',CAapprovedOn=getdate() where pvid= @PVId ";
                    sql = sql + " ; insert into PVApprovalLog(PVId,ActionBy,ActionOn,ActionStatus,Role,Remarks,LogDate) values ( @PVId ,'" + myGlobal.loggedInUser() + "',getdate(),'"+ddlCAapproval.SelectedItem.Text+"','" + lblUserPVRole.Text.Trim().ToUpper() + "','"+txtCAapprovalRemarks.Text+"',getdate()) ";
                }

                sql = sql + " ; EXEC PV_SendApprovalRequestEmail @PVId ,'" + lblUserPVRole.Text + "','" + myGlobal.loggedInUser() + "'";

                Db.constr = myGlobal.getAppSettingsDataForKey("tejSAP");
                Db.myExecuteSQL(sql);

                lblMsg.Text = "Payment voucher submitted successfully.";
                Page.ClientScript.RegisterStartupScript(this.GetType(), "alert", "<script> alert('Payment voucher submitted successfully.'); </script>");
                ClearControls();
            }
            else /// UPDATE MODE
            {

                string sql = "";

                if (lblUserPVRole.Text.Trim().ToUpper() == "CA" || lblUserPVRole.Text.Trim().ToUpper() == "EMPLOYEE")  // CA or EMPLOYEE is updating
                {
                    if (lblUserPVRole.Text.Trim().ToUpper() == "EMPLOYEE")  // Header Level update if Employee is updating record
                    {
                        sql = " Update PV set Currency='" + ddlCurrency.SelectedItem.Text + "',Benificiary='" + txtBenificiary.Text + "',RequestedAmt=" + txtRequestedAmount.Text + ",BeingPayOf='" + txtBeingPayOf.Text + "',PayRequestDate='" + txtPaymentReqDate.Text + "', CAappStatus= case when CAappStatus='Rejected-Open' then 'Pending' else CAappStatus end, LastUpdatedBy='" + myGlobal.loggedInUser() + "',LastUpdatedOn = getdate() Where PVId=" + lblPVId.Text;
                    }
                    else if (lblUserPVRole.Text.Trim().ToUpper() == "CA") // Header Level update if CA is updating record
                    {
                        /// if PV is approved by CA, CM, CFO then CA can close it.
                        if (ddlCAapproval.SelectedItem.Text == "Approved" && ddlCMapproval.SelectedItem.Text == "Approved" && ddlCFOapproval.SelectedItem.Text == "Approved")
                        {
                            sql = " Update PV  set DocStatus='"+ddlStatus.SelectedItem.Text+"' Where PVid= " + lblPVId.Text;
                        }
                        else
                        {
                            sql = " Update PV set Currency='" + ddlCurrency.SelectedItem.Text + "',VendorCode='" + lblVendEmployeeCode.Text + "',VendorEmployee='" + txtVendorEmployee.Text + "',Benificiary='" + txtBenificiary.Text + "',RequestedAmt=" + txtRequestedAmount.Text + ",ApprovedAmt=" + txtApprovedAmt.Text + ",BeingPayOf='" + txtBeingPayOf.Text + "',PayRequestDate='" + txtPaymentReqDate.Text + "',PayMethod='" + PayMethod + "',PayRefNo='" + PayRefNo + "',PayDate='" + PayDate + "',FilePath='" + FilePath + "', LastUpdatedBy='" + myGlobal.loggedInUser() + "',LastUpdatedOn = getdate(),BankCode='" + lblBankCode.Text + "',BankName='" + txtBankName.Text + "' ";
                            if (ddlCAapproval.SelectedItem.Text != "Pending" || ddlCAapproval.SelectedItem.Text != "--Select--")
                            {
                                sql = sql + " ,CAappStatus='" + ddlCAapproval.SelectedItem.Text + "', CAappRemarks='" + txtCAapprovalRemarks.Text + "',CAapprovedBy='" + myGlobal.loggedInUser() + "',CAapprovedOn=getdate() ";
                            }
                            if (ddlCAapproval.SelectedItem.Text == "Approved")
                            {
                                sql = sql + " ,CMappStatus='Pending', CMappRemarks=NULL,CMapprovedBy=NULL,CMapprovedOn=NULL ";
                            }
                            sql = sql + " Where PVId=" + lblPVId.Text;
                        }
                    }

                    //if (ddlCAapproval.SelectedItem.Text != "Approved" && ddlCAapproval.SelectedItem.Text != "Rejected - Closed")
                    //{
                        #region "Row Level update if Employee OR CA is updating Record"

                        foreach (GridViewRow row in gvPVLines.Rows)
                        {
                            Label LoopSrNo = (Label)row.FindControl("lblsrno");
                            Label lblPVLineId = (Label)row.FindControl("lblPVLineId");
                            TextBox txtdate = (TextBox)row.FindControl("txtdate");
                            TextBox txtDescription = (TextBox)row.FindControl("txtDescription");
                            TextBox txtgvamt = (TextBox)row.FindControl("txtgvamt");
                            TextBox txtRemarks = (TextBox)row.FindControl("txtRemarks");
                            Label lblFilePath = (Label)row.FindControl("lblFilePath");
                            FileUpload grvfupload = (FileUpload)row.FindControl("fUploadAddattachment");

                            string rowLevelfilepath = "";
                            if (!string.IsNullOrEmpty(lblFilePath.Text))
                            {
                                rowLevelfilepath = lblFilePath.Text;
                            }
                            if (grvfupload.HasFile)
                            {
                                string folder = "~/excelFileUpload/PV/" + txtRefNo.Text + "_" + myGlobal.loggedInUser() + "/";
                                string dirpath = Server.MapPath(folder);

                                if (!System.IO.Directory.Exists(dirpath))
                                {
                                    System.IO.Directory.CreateDirectory(dirpath);
                                }
                                try
                                {
                                    /// DELETE previously uploaded file..
                                    if (!string.IsNullOrEmpty(lblFilePath.Text))
                                    {
                                        string FilePathToDelete = Server.MapPath(lblFilePath.Text);
                                        if (!string.IsNullOrEmpty(FilePathToDelete))
                                        {
                                            if (File.Exists(FilePathToDelete))
                                            {
                                                File.Delete(FilePathToDelete);
                                            }
                                        }
                                    }
                                }
                                catch { }
                                /// Save the file
                                string rowLevelFiles = dirpath + grvfupload.FileName; //Server.MapPath(dirpath + fuploadCheckImage.FileName);
                                grvfupload.SaveAs(rowLevelFiles);
                                rowLevelfilepath = folder + grvfupload.FileName;
                            }

                            if (lblPVLineId.Text == "0")
                            {
                                sql = sql + " ;  insert into PVLines( PVId,[Date],[Description],Amount,Remarks,FilePath,CreatedOn,CreatedBy ) ";
                                sql = sql + " Values ( " + lblPVId.Text + ",'" + txtdate.Text + "','" + txtDescription.Text + "','" + txtgvamt.Text + "','" + txtRemarks.Text + "','" + rowLevelfilepath + "',getdate(),'" + myGlobal.loggedInUser() + "' )  ";
                            }
                            else
                            {
                                sql = sql + " ; Update PVLines Set [Date]='" + txtdate.Text + "',[Description]='" + txtDescription.Text + "',Amount='" + txtgvamt.Text + "',Remarks='" + txtRemarks.Text + "',FilePath='" + rowLevelfilepath + "',LastUpdatedOn=getdate(),LastUpdatedBy='" + myGlobal.loggedInUser() + "'  Where PVLineId=" + lblPVLineId.Text + " And  PVId=" + lblPVId.Text;
                            }
                        }
                        #endregion

                        if (lblUserPVRole.Text.Trim().ToUpper() == "CA" && ddlCAapproval.SelectedItem.Text != "Pending" && ddlCAapproval.SelectedItem.Text != "--Select--")
                        {
                            sql = sql + " ; insert into PVApprovalLog(PVId,ActionBy,ActionOn,ActionStatus,Role,Remarks,LogDate) values (" + lblPVId.Text + ",'" + myGlobal.loggedInUser() + "',getdate(),'" + ddlCAapproval.SelectedItem.Text + "','CA','" + txtCAapprovalRemarks.Text + "',getdate()) ";
                        }
                    //}
                
                }
                else if (lblUserPVRole.Text.Trim().ToUpper() == "CM" ) // CM is updating
                {
                    sql = "update PV   set CMappStatus='" + ddlCMapproval.SelectedItem.Text + "',CMappRemarks='" + txtCMapprovalRemarks.Text + "',CMapprovedBy='" + myGlobal.loggedInUser() + "',CMapprovedOn=getdate(), CAappStatus= case when '" + ddlCMapproval.SelectedItem.Text + "'='Rejected-Open' then 'Pending' else CAappStatus end, CAappRemarks=case when '" + ddlCMapproval.SelectedItem.Text + "'='Rejected-Open' then 'Rejected By CM' else CAappRemarks end, CAapprovedBy=case when '" + ddlCMapproval.SelectedItem.Text + "'='Rejected-Open' then '" + myGlobal.loggedInUser() + "' else CAapprovedBy end , CAapprovedOn= case when '" + ddlCMapproval.SelectedItem.Text + "'='Rejected-Open' then getdate() else CAapprovedOn end  , DocStatus=case when '" + ddlCMapproval.SelectedItem.Text + "'='Rejected-Closed' then 'Rejected-Closed' else DocStatus End ";
                    if (ddlCMapproval.SelectedItem.Text == "Approved")
                    {
                        sql = sql + " , CFOappStatus= case when isnull(CFOappStatus,'')<>'' then 'Pending' else CFOappStatus end, CFOappRemarks=NULL,CFOapprovedBy=NULL,CFOapprovedOn=NULL  ";
                    }
                    sql = sql + " Where PVid=" + lblPVId.Text ;
                    sql = sql + " ; insert into PVApprovalLog(PVId,ActionBy,ActionOn,ActionStatus,Role,Remarks,LogDate) values (" + lblPVId.Text + ",'" + myGlobal.loggedInUser() + "',getdate(),'" + ddlCMapproval.SelectedItem.Text + "','CM','" + txtCMapprovalRemarks.Text + "',getdate()) ";

                    //Db.myExecuteSQL(sql);
                    //lblMsg.Text = "Payment voucher " + ddlCMapproval.SelectedItem.Text + " successfully.";
                    //Page.ClientScript.RegisterStartupScript(this.GetType(), "alert", "<script> alert('Payment voucher " + ddlCMapproval.SelectedItem.Text + " successfully.'); </script>");
                }
                else if (lblUserPVRole.Text.Trim().ToUpper() == "CFO") // CFO is updating
                {
                   //sql = "update PV   set CFOappStatus='" + ddlCFOapproval.SelectedItem.Text + "',CFOappRemarks='" + txtCFOapprovalRemarks.Text + "',CFOapprovedBy='" + myGlobal.loggedInUser() + "',CFOapprovedOn=getdate() Where PVid=" + lblPVId.Text;
                   //sql = sql + " ; insert into PVApprovalLog(PVId,ActionBy,ActionOn,ActionStatus,Role,Remarks,LogDate) values (" + lblPVId.Text + ",'" + myGlobal.loggedInUser() + "',getdate(),'" + ddlCFOapproval.SelectedItem.Text + "','CFO','" + txtCFOapprovalRemarks.Text + "',getdate()) ";

                    sql = "  EXEC PV_CFOApproval  "+lblPVId.Text+ ",'"+ddlCMapproval.SelectedItem.Text+"','"+txtCMapprovalRemarks.Text+"','"+ddlCFOapproval.SelectedItem.Text+"','"+txtCFOapprovalRemarks.Text+"','"+myGlobal.loggedInUser()+"'";

                    //lblMsg.Text = "Payment voucher " + ddlCFOapproval.SelectedItem.Text + " successfully.";
                    //Page.ClientScript.RegisterStartupScript(this.GetType(), "alert", "<script> alert('Payment voucher " + ddlCMapproval.SelectedItem.Text + " successfully.'); </script>");
                }

                sql = sql + " ; EXEC PV_SendApprovalRequestEmail "+lblPVId.Text+" ,'" + lblUserPVRole.Text + "','" + myGlobal.loggedInUser() + "'";

                Db.myExecuteSQL(sql);

                #region "Send mail to signatories"

                /// if logged In user is CFO & PV is approved by CA,CM & CFO then send mail to signatories
                if (lblUserPVRole.Text.Trim().ToUpper() == "CFO" && ddlCAapproval.SelectedItem.Text=="Approved" && ddlCMapproval.SelectedItem.Text=="Approved" && ddlCFOapproval.SelectedItem.Text=="Approved" )
                {

                    SendMailToSignatories();
                }

                #endregion

                lblMsg.Text = "Payment voucher updated successfully.";
                Page.ClientScript.RegisterStartupScript(this.GetType(), "alert", "<script> alert('Payment voucher updated successfully.'); </script>");
                ClearControls();

            } // End of UPDATE MODE

        }
        catch (Exception ex)
        {
            lblMsg.Text = "Error occured btnSave_Click() : " + ex.Message;
        }
    }


    public bool SendMailToSignatories()
    {
        bool result = false;
        try
        {
            string signatoryEmail = string.Empty, CFOEmail = string.Empty;
            Db.constr = myGlobal.getAppSettingsDataForKey("tejSAP");
            DataSet DS = Db.myGetDS(" EXEC PV_GetSignatories " + lblPVId.Text);
            if (DS.Tables.Count > 0)
            {
                if (DS.Tables[0].Rows[0]["Signatories"] != null && !DBNull.Value.Equals(DS.Tables[0].Rows[0]["Signatories"]))
                {
                    signatoryEmail = DS.Tables[0].Rows[0]["Signatories"].ToString();
                }
                if (DS.Tables[0].Rows[0]["CFOEmail"] != null && !DBNull.Value.Equals(DS.Tables[0].Rows[0]["CFOEmail"]))
                {
                    CFOEmail = DS.Tables[0].Rows[0]["CFOEmail"].ToString();
                }
                // send mail
                if (!string.IsNullOrEmpty(signatoryEmail) && !string.IsNullOrEmpty(CFOEmail))
                {
                    string html = string.Empty;
                    if (DS.Tables[1].Rows[0][0] != null && !DBNull.Value.Equals(DS.Tables[1].Rows[0][0]))
                    {
                        html = DS.Tables[1].Rows[0][0].ToString();
                    }

                    #region : old code to send PV to signatory

//                    string html = " Dear " + signatoryEmail + ", <br/><br/>";
//                    html = html + " Payment voucher <b>" + txtRefNo.Text + "</b> has been approved by <b> " + "alfarid" + " </b> for <b>" + txtVendorEmployee.Text + " </b>.<br/>";
//                    if ((ddlPaymentMethod.SelectedItem.Text == "Cheque" || ddlPaymentMethod.SelectedItem.Text == "TT") && !string.IsNullOrEmpty(ancChequeImage.HRef))
//                    {
//                        html = html + "Please find attached the " + ddlPaymentMethod.SelectedItem.Text + " image.  <br/> ";
//                    }
//                    html = html + " Please see below details, <br/><br/>  ";
//                    html = html + @"  <table border='1' width='100%' align='center'> <thead> <tr> <th width='8%' align='center' style='background-color:#ce352c;color:White;' >  Country </th> 
//																								 <th width='8%' align='center' style='background-color:#ce352c;color:White;' >  Currency  </th> 
//																								 <th width='9%' align='center' style='background-color:#ce352c;color:White;'>Approved Amt</th>
//																								 <th width='8%' align='center' style='background-color:#ce352c;color:White;'>PayReq Date</th> 
//                                                                                                 <th width='8%' align='center' style='background-color:#ce352c;color:White;'>Pymnt Method</th>
//                                                                                                 <th width='15%' align='center' style='background-color:#ce352c;color:White;'>Bank</th>
//																								 <th width='18%' align='center' style='background-color:#ce352c;color:White;'>Benificiary</th>
//																								 <th width='18%' align='center' style='background-color:#ce352c;color:White;'>Being Pay Of</th>
//																								  </tr> </thead> <tbody>";

//                    html = html + " <tr><td align='center'> " + ddlCountry.SelectedItem.Text + "</td>";
//                    html = html + " <td align='center'> " + ddlCurrency.SelectedItem.Text + "</td>";
//                    html = html + " <td align='center'> " + txtApprovedAmt.Text + "</td>";
//                    html = html + " <td align='center'> " + txtPaymentReqDate.Text + "</td>";
//                    html = html + " <td align='center'> " + ddlPaymentMethod.SelectedItem.Text + "</td>";
//                    html = html + " <td align='center'> " + txtBankName.Text + "</td>";
//                    html = html + " <td align='center'> " + txtBenificiary.Text + "</td>";
//                    html = html + " <td align='center'> " + txtBeingPayOf.Text + "</td></tr>";
//                    html = html + " </tbody> </table> <br/><br/> ";
//                    html = html + " <b>CFO approval Remarks  </b> &nbsp; : " + txtCFOapprovalRemarks.Text;
//                    html = html + " <br/><br/> Best Regards, <br/> Red Dot Distribution";

                    #endregion

                    string subject = "PV " + txtRefNo.Text + " for '" + txtVendorEmployee.Text + "' is approved by CFO";
                    // pls uncomment below to send mail to signatories before deploying it in LIVE

                    #region "Code to send multiple Attachements"

                    string attachmentPath = string.Empty;
                    SqlDataReader rdr = Db.myGetReader(" select FilePath from PVLines Where PVId=" + lblPVId.Text + " And isnull(FilePath,'') <> '' ");
                    if (rdr.HasRows)
                    {
                        while (rdr.Read())
                        {
                            if (rdr["FilePath"] != null && !DBNull.Value.Equals(rdr["FilePath"]))
                            {
                                if (string.IsNullOrEmpty(attachmentPath))
                                {
                                    attachmentPath = Server.MapPath(rdr["FilePath"].ToString());
                                }
                                else
                                {
                                    attachmentPath = attachmentPath + "?" + Server.MapPath(rdr["FilePath"].ToString());
                                }
                            }
                        }
                    }

                    if (!string.IsNullOrEmpty(Server.MapPath(ancChequeImage.HRef)))
                    {
                        if (!string.IsNullOrEmpty(attachmentPath))
                        {
                            attachmentPath = attachmentPath + "?" + Server.MapPath(ancChequeImage.HRef);
                        }
                        else
                        {
                            attachmentPath = Server.MapPath(ancChequeImage.HRef);
                        }
                    }

                    #endregion

                    //signatoryEmail = "chetan@reddotdistribution.com; pramod@reddotdistribution.com";
                    //CFOEmail = "pramod@reddotdistribution.com";
                    try
                    {
                        //string sendmailresponse = Mail.SendSingleAttachPV("reddotstaff@reddotdistribution.com", signatoryEmail, CFOEmail , subject, html, true, Server.MapPath(ancChequeImage.HRef));
                        string sendmailresponse = Mail.SendSingleAttachPV("reddotstaff@reddotdistribution.com", signatoryEmail, CFOEmail, subject, html, true, attachmentPath);
                        //string sendmailresponse = Mail.SendSingleAttachPV("reddotstaff@reddotdistribution.com", "pramod@reddotdistribution.com" , "pramod@reddotdistribution.com", subject, html, true, Server.MapPath(ancChequeImage.HRef));
                        if (sendmailresponse == "Mail Sent Successfully")
                        {
                            result = true;
                        }
                        html = html.Replace('\'', '\"');
                        Db.constr = myGlobal.getAppSettingsDataForKey("tejSAP");
                        html = "";
                        Db.myExecuteSQL(" Insert into PVSendMailLog(PVId,SignatoriesEmail,CFOmail,html,SendMailResponse) Values (" + lblPVId.Text + ",'" + signatoryEmail + "','" + CFOEmail + "','" + html + "','" + sendmailresponse + "') ");
                    }
                    catch (Exception ex)
                    {
                        lblMsg.Text = "Error occured in send mail " + ex.Message;
                        html = html.Replace('\'', '\"');
                        html = "";
                        Db.constr = myGlobal.getAppSettingsDataForKey("tejSAP");
                        Db.myExecuteSQL(" Insert into PVSendMailLog(PVId,SignatoriesEmail,CFOmail,html,SendMailResponse) Values (" + lblPVId.Text + ",'" + signatoryEmail + "','" + CFOEmail + "','" + html + "',' Failed to send mail - " + ex.Message + "') ");
                    }
                }
            }
        }
        catch (Exception ex)
        {
            lblMsg.Text = "Error occured in send mail " + ex.Message;
            Db.constr = myGlobal.getAppSettingsDataForKey("tejSAP");
            Db.myExecuteSQL(" Insert into PVSendMailLog(PVId,SignatoriesEmail,CFOmail,html,SendMailResponse) Values (" + lblPVId.Text + ",'NA','NA','NA',' Failed to send mail - " + ex.Message + " ') ");
        }

        return result;

    }


    protected void gvPVLines_RowDeleting(object sender, GridViewDeleteEventArgs e)
    {

    }

    protected void ddlCountry_SelectedIndexChanged(object sender, EventArgs e)
    {
        try
        {
            Db.constr = myGlobal.getAppSettingsDataForKey("tejSAP");
            
            txtRefNo.Text = Db.myExecuteScalar2( " select dbo.GetPVRefNo('"+ddlCountry.SelectedItem.Text+"')");

        }
        catch (Exception ex)
        {
            lblMsg.Text = "Error occured on Country selection()  " + ex.Message;
        }
    }

    public void InitializeSaveMode()
    {
        btnSave.Text = "Save";
        SetInitialRow();
        EnableDisableControls(lblUserPVRole.Text, lblUserPVCountry.Text, "Save", true);
        txtApprovedAmt.Enabled = false;

        if (lblUserPVRole.Text.Trim().ToUpper() == "CA")
        {
            ddlDatabase.Enabled = true;
            ddlVoucherType.Enabled = true;
            ddlCountry.Enabled = true;
            txtVendorEmployee.Enabled = true;
            txtApprovedAmt.Enabled = true;
            pnlPVApproval.Visible = true;
            ddlCMapproval.Enabled = false;
            ddlCFOapproval.Enabled = false;
            txtCMapprovalRemarks.Enabled = false;
            txtCFOapprovalRemarks.Enabled = false;

            ddlCAapproval.SelectedItem.Text = "Approved";
            ddlCAapproval.Enabled = false;
            txtCAapprovalRemarks.Text = "pls approve";
        }
        else
        {
            txtVendorEmployee.Text = lblUserDisplayName.Text;
            txtBenificiary.Text = lblUserDisplayName.Text;
        }
        try
        {
            txtCreatedBy.Text = myGlobal.loggedInUser();
            txtCreatedOn.Text = String.Format("{0:MM/dd/yyyy}", DateTime.Now.ToString());
        }
        catch { }
    }

    private void ClearControls()
    {
        try
        {
            lblPVId.Text = "";
            txtVendorEmployee.Text = "";
            lblVendEmployeeCode.Text = "";
            txtRefNo.Text = "";
            txtPaymentReqDate.Text = "";
            txtRequestedAmount.Text = "";
            txtApprovedAmt.Text = "";
            txtChequeNo.Text = "";
            txtChequeDate.Text = "";
            txtBeingPayOf.Text = "";
            txtBenificiary.Text = "";
            txtCAapprovalRemarks.Text = "";
            txtCMapprovalRemarks.Text = "";
            txtCFOapprovalRemarks.Text = "";

            try
            {
                    ddlCountry.SelectedItem.Text = "--Select--";
                    ddlCountry.SelectedItem.Value = "--Select--";
                    ddlDatabase.SelectedItem.Text = "--Select--";
                    ddlDatabase.SelectedItem.Value = "--Select--";
                    ddlVoucherType.SelectedItem.Text = "--Select--";
                    ddlVoucherType.SelectedItem.Value = "--Select--";
                    ddlCurrency.SelectedItem.Text = "--Select--";
                    ddlCurrency.SelectedItem.Value = "--Select--";
                    ddlPaymentMethod.SelectedItem.Text = "--Select--";
                    ddlPaymentMethod.SelectedItem.Value = "--Select--";

                    ddlCAapproval.SelectedItem.Text = "--Select--";
                    ddlCAapproval.SelectedItem.Value = "--Select--";

                    ddlCMapproval.SelectedItem.Text = "--Select--";
                    ddlCMapproval.SelectedItem.Value = "--Select--";

                    ddlCFOapproval.SelectedItem.Text = "--Select--";
                    ddlCFOapproval.SelectedItem.Value = "--Select--";

                    ddlStatus.SelectedItem.Text = "Open";
                    ddlStatus.SelectedItem.Value = "Open";
            }
            catch (Exception ex)
            {
            }

            //SetInitialRow();

            InitializeSaveMode();

            if (lblUserPVRole.Text.Trim().ToUpper() == "EMPLOYEE" || lblUserPVRole.Text.Trim().ToUpper() == "CM" || lblUserPVRole.Text.Trim().ToUpper() == "CFO")
            {

                ddlCurrency.Enabled = true;
                txtRequestedAmount.Enabled = true;
                txtBeingPayOf.Enabled = true;
                txtBenificiary.Enabled = true;
                txtPaymentReqDate.Enabled = true;
                BtnAddRow.Enabled = true;
                pnlPVApproval.Visible = false;
                gvPVLines.Enabled = true;
                ddlCountry.Enabled = false;
            }
            
            BtnDelete.Enabled = false;

        }
        catch (Exception ex)
        {
            lblMsg.Text = "Error occured in ClearControls()  " + ex.Message;
        }
    }

    protected void BtnDelete_Click1(object sender, EventArgs e)
    {
        try
        {
            Db.constr = myGlobal.getAppSettingsDataForKey("tejSAP");
            Db.myExecuteSQL(" Update dbo.PV set IsDeleted=1,LastUpdatedOn=getdate(),LastUpdatedBy='"+myGlobal.loggedInUser()+"' Where PVid=" + lblPVId.Text);

            lblMsg.Text = "Payment voucher deleted successfully.";
            Page.ClientScript.RegisterStartupScript(this.GetType(), "alert", "<script> alert('Payment voucher deleted successfully.'); </script>");

            ClearControls();
        }
        catch (Exception ex)
        {
            lblMsg.Text = "Error occured in BtnDelete_Click1()  " + ex.Message;
        }
    }

    protected void BtnExportToPDF_Click(object sender, EventArgs e)
    {
        try
        {
            //Exec PV_GetDataToPrintPV 13
            dvExportPVToPDF.InnerHtml = "";
            string preparedBy = string.Empty, passedBy = string.Empty, ApprovedBy = string.Empty, AmtInWord=string.Empty;
                    Db.constr = myGlobal.getAppSettingsDataForKey("tejSAP");
                    DataSet DS =  Db.myGetDS(" Exec PV_GetDataToPrintPV "+ lblPVId.Text );
                    if (DS.Tables.Count > 0)
                    {
                        if (DS.Tables[0].Rows[0]["preparedBy"] != null && !DBNull.Value.Equals(DS.Tables[0].Rows[0]["preparedBy"]))
                        {
                            preparedBy = DS.Tables[0].Rows[0]["preparedBy"].ToString();
                        }
                        if (DS.Tables[0].Rows[0]["PassedBy"] != null && !DBNull.Value.Equals(DS.Tables[0].Rows[0]["PassedBy"]))
                        {
                            passedBy = DS.Tables[0].Rows[0]["PassedBy"].ToString();
                        }
                        if (DS.Tables[0].Rows[0]["ApprovedBy"] != null && !DBNull.Value.Equals(DS.Tables[0].Rows[0]["ApprovedBy"]))
                        {
                            ApprovedBy = DS.Tables[0].Rows[0]["ApprovedBy"].ToString();
                        }
                        if (DS.Tables[0].Rows[0]["AmtInWord"] != null && !DBNull.Value.Equals(DS.Tables[0].Rows[0]["AmtInWord"]))
                        {
                            AmtInWord = DS.Tables[0].Rows[0]["AmtInWord"].ToString();
                        }
                        
                        string html = @" <table width='100%' align='center' border='1'> <tr> <td> <table width='100%' align='center' border='0' >
                             <tr>
                                <td colspan='6'> &nbsp; </td>
                             </tr>
                             <tr>
                                <td colspan='6' align='center'> <span style='font-size:medium' > <b> <u> PAYMENT VOUCHER </u> </b> </span> </td>
                             </tr>
                             <tr>
                                <td colspan='4' width='70%' > &nbsp; </td>
                                <td width='15%'> <span style='font-size:10px;font-family:calibri;'>  PV No  </span> </td>
                                <td width='15%'> <span style='font-size:10px;font-family:calibri;'> <b>" + txtRefNo.Text + @" </b> </span> </td>
                             </tr>
                             <tr>
                                <td width='17%'> <span style='font-size:10px;font-family:calibri;'>  Paid to Mr./Ms.</span>   </td>
                                <td width='52%' colspan='3'> <span style='font-size:10px;font-family:calibri;'> <b><u> " + txtVendorEmployee.Text + @" </u></b> </span> </td>
                                <td width='15%'> <span style='font-size:10px;font-family:calibri;'>  Date </span> </td>
                                <td width='15%'> <span style='font-size:10px;font-family:calibri;'> <b> " + Convert.ToDateTime(txtCreatedOn.Text).ToString("dd/MM/yyyy") + @" </b> </span> </td>
                             </tr>
                             <tr>
                                <td width='17%'> <span style='font-size:10px;font-family:calibri;'>  Cash / Cheque No </span> </td>
                                <td width='18%'> <span style='font-size:10px;font-family:calibri;'> <b> " + txtChequeNo.Text + @" </b> </span> </td>
                                <td width='15%'> <span style='font-size:10px;font-family:calibri;'>  Bank Name </span> </td>
                                <td width='20%'> <span style='font-size:10px;font-family:calibri;'> <b> " + txtBankName.Text + @" </b> </span> </td>
                                <td width='15%'> <span style='font-size:10px;font-family:calibri;'> Payment Date </span> </td>
                                <td width='15%'> <span style='font-size:10px;font-family:calibri;'> <b> " + Convert.ToDateTime(txtChequeDate.Text).ToString("dd/MM/yyyy") + @" </b> </span> </td>
                             </tr>
                             <tr>
                                <td width='100%' colspan='6' >&nbsp;<br/></td>
                             </tr>
                             <tr>
                                <td width='100%' colspan='6' > 
		                            <table width='100%' align='left' border='1' >
			                             <tr>
    				                            <td width='8%' > &nbsp; <span style='font-size:10px;font-family:calibri;'>  <b> SrNo </b> </span>  </td>
				                            <td width='72%' align='center' >  &nbsp; <span style='font-size:10px;font-family:calibri;'>  <b>Particulars </b> </span> </td>
    				                            <td width='20%' align='right'> &nbsp; <span style='font-size:10px;font-family:calibri;'>  <b> Amount ( " + ddlCurrency.SelectedItem.Text + @" ) </b> </span> </td>
 			                            </tr> ";

                        decimal total = 0;
                        foreach (GridViewRow row in gvPVLines.Rows)
                        {
                            Label LoopSrNo = (Label)row.FindControl("lblsrno");
                            TextBox txtDescription = (TextBox)row.FindControl("txtDescription");
                            TextBox txtgvamt = (TextBox)row.FindControl("txtgvamt");
                            total = total + Convert.ToDecimal(txtgvamt.Text);
                            html = html + " <tr> <td width='8%' align='center' >  <span style='font-size:10px;font-family:calibri;'>  " + LoopSrNo.Text + " </span> </td>   ";
                            html = html + " <td width='72%'>  <span style='font-size:10px;font-family:calibri;'>  " + txtDescription.Text + " </span> </td>   ";
                            html = html + " <td width='20%' align='right' >  <span style='font-size:10px;font-family:calibri;'>  " +  string.Format("{0:#,0.00}", Convert.ToDecimal(txtgvamt.Text) ) + " </span> </td>  </tr>  ";
                        }

                        html = html + " <tr> <td width='8%' >  <span style='font-size:10px;font-family:calibri;'>  &nbsp; </span> </td>   <td width='72%' align='right'  >  <span style='font-size:10px;font-family:calibri;'> <b> TOTAL ( " + ddlCurrency.SelectedItem.Text + " ) </b> </span> </td>   ";
                        html = html + " <td width='20%' align='right' >  <span style='font-size:10px;font-family:calibri;'> <b> " + string.Format("{0:#,0.00}", total)  + " </b> </span> </td>  </tr>  ";
                        html = html + @"      </table>
                                </td>
                             </tr>
                            <tr>
                                <td width='17%' > <span style='font-size:10px;font-family:calibri;'>  Amount in Word </span> </td>
                                <td width='83%' colspan='5' > <span style='font-size:10px;font-family:calibri;'> <b>( "+ddlCurrency.SelectedItem.Text+" ) "+ AmtInWord +@" </b> </span> </td>
                            </tr>
                             <tr>
                                <td width='100%' colspan='6' > <span style='font-size:10px;font-family:calibri;'>   &nbsp;&nbsp; </span> </td>
                             </tr>
                             <tr>
                                <td width='17%' > <span style='font-size:10px;font-family:calibri;'>   Prepared by </span> </td>
                                <td width='18%' > <span style='font-size:10px;font-family:calibri;'> <b>"+ preparedBy +@"</b> </span> </td>
                                <td width='15%' > <span style='font-size:10px;font-family:calibri;'>  Passed By </span> </td>
                                <td width='20%' > <span style='font-size:10px;font-family:calibri;'> <b> "+ passedBy +@" </b> </span> </td>
                                <td width='15%'>  <span style='font-size:10px;font-family:calibri;'> Approved By </span> </td>
                                <td width='15%'>  <span style='font-size:10px;font-family:calibri;'> <b> "+ApprovedBy+@" </b> </span> </td>
                             </tr>
                             <tr>
                                <td width='17%' > <span style='font-size:10px;font-family:calibri;'>  Received by Name </span> </td>
                                <td width='53%' colspan='3' > <span style='font-size:10px;font-family:calibri;'> <b> _____________________________ </b> </span> </td>
                                <td width='15%'> <span style='font-size:10px;font-family:calibri;'> &nbsp; </span> </td>
                                <td width='15%'> <span style='font-size:10px;font-family:calibri;'> &nbsp; </span> </td>
                             </tr>
                            </table> </td> </tr>  </table>";

                        dvExportPVToPDF.InnerHtml = html;

                        Response.ContentType = "application/pdf";
                        Response.AddHeader("content-disposition", "attachment;filename=" + txtRefNo.Text + ".pdf");
                        Response.Cache.SetCacheability(HttpCacheability.NoCache);

                        StringWriter stringWriter = new StringWriter();
                        HtmlTextWriter htmlTextWriter = new HtmlTextWriter(stringWriter);
                        dvExportPVToPDF.RenderControl(htmlTextWriter);

                        StringReader stringReader = new StringReader(stringWriter.ToString());
                        Document Doc = new Document(PageSize.A4, 10f, 10f, 50f, 30f);
                        HTMLWorker htmlparser = new HTMLWorker(Doc);
                        PdfWriter.GetInstance(Doc, Response.OutputStream);
                        Doc.Open();
                        htmlparser.Parse(stringReader);
                        Doc.Close();
                        Response.Write(Doc);
                        Response.End();
                    }
                    else
                    {
                        lblMsg.Text = " Failed to get Prepared By, Approved By details.";
                    }

        }
        catch (Exception ex)
        {
            lblMsg.Text = "Error occured in Export To PDF  " + ex.Message;
        }
    }

    protected void txtgvamt_TextChanged(object sender, EventArgs e)
    {
        try
        {
            lblMsg.Text = "";
            double total = 0;
            foreach (GridViewRow gvr in gvPVLines.Rows)
            {
                TextBox txtgvamt = (TextBox)gvr.FindControl("txtgvamt");
                double sum;
                if (double.TryParse(txtgvamt.Text.Trim(), out sum))
                {
                    total += sum;
                }
            }

            double reqAmt = 0;
            double.TryParse(txtRequestedAmount.Text.Trim(), out reqAmt);
            if (total > reqAmt)
            {
                lblMsg.Text = "Total of all rows amount must be less than or equal to requested amount ";
                txtApprovedAmt.Text = "0";
            }
            else
            {
                txtApprovedAmt.Text = total.ToString();
            }
        }
        catch (Exception ex)
        {
            lblMsg.Text = " Error occured in approve amt calculation "+ ex.Message;
        }

    }

    [WebMethod]
    public static VendorAgeing GetAgeing( string  dbName, string cardcode )
    {
        VendorAgeing v = new VendorAgeing();

        Db.constr = myGlobal.getAppSettingsDataForKey("tejSAP");

        string qry = " Exec rddVendordue '" + cardcode + "','" + dbName + "'";

        DataSet DS= Db.myGetDS(qry);
        if(DS.Tables.Count>0)
        {
            if (DS.Tables[0].Rows.Count > 0)
            {
                if (DS.Tables[0].Rows[0]["Balance"] != null && !DBNull.Value.Equals(DS.Tables[0].Rows[0]["Balance"]))
                    v.Balance = Convert.ToDecimal(DS.Tables[0].Rows[0]["Balance"]);
                else
                    v.Balance = 0;
                if (DS.Tables[0].Rows[0]["0-30"] != null && !DBNull.Value.Equals(DS.Tables[0].Rows[0]["0-30"]))
                    v.zeroTothirty = Convert.ToDecimal(DS.Tables[0].Rows[0]["0-30"]);
                else
                    v.zeroTothirty = 0;
                if (DS.Tables[0].Rows[0]["31-45"] != null && !DBNull.Value.Equals(DS.Tables[0].Rows[0]["31-45"]))
                    v.thirtyoneTofourtyfive = Convert.ToDecimal(DS.Tables[0].Rows[0]["31-45"]);
                else
                    v.thirtyoneTofourtyfive = 0;
                if (DS.Tables[0].Rows[0]["46-60"] != null && !DBNull.Value.Equals(DS.Tables[0].Rows[0]["46-60"]))
                    v.fourtyfiveTosixty = Convert.ToDecimal(DS.Tables[0].Rows[0]["46-60"]);
                else
                    v.fourtyfiveTosixty = 0;
                if (DS.Tables[0].Rows[0]["61-90"] != null && !DBNull.Value.Equals(DS.Tables[0].Rows[0]["61-90"]))
                    v.sixtyoneToninty = Convert.ToDecimal(DS.Tables[0].Rows[0]["61-90"]);
                else
                    v.sixtyoneToninty = 0;
                if (DS.Tables[0].Rows[0]["91-120"] != null && !DBNull.Value.Equals(DS.Tables[0].Rows[0]["91-120"]))
                    v.nintyoneToonetwenty = Convert.ToDecimal(DS.Tables[0].Rows[0]["91-120"]);
                else
                    v.nintyoneToonetwenty = 0;
                if (DS.Tables[0].Rows[0]["121+"] != null && !DBNull.Value.Equals(DS.Tables[0].Rows[0]["121+"]))
                    v.onetwentyplus = Convert.ToDecimal(DS.Tables[0].Rows[0]["121+"]);
                else
                    v.onetwentyplus = 0;
            }
        }

        return v;
    }

    protected void BtnSendMailToSignatories_Click(object sender, EventArgs e)
    {
        string[] PVSendMailUsers =  myGlobal.getAppSettingsDataForKey("PVSendMailToSignatories").ToLower().Split(';');
       
            //if (lblUserPVRole.Text.Trim().ToUpper() == "CFO" && ddlCAapproval.SelectedItem.Text == "Approved" && ddlCMapproval.SelectedItem.Text == "Approved" && ddlCFOapproval.SelectedItem.Text == "Approved")
            if (PVSendMailUsers.Contains(myGlobal.loggedInUser().ToLower()) && ddlCAapproval.SelectedItem.Text == "Approved" && ddlCMapproval.SelectedItem.Text == "Approved" && ddlCFOapproval.SelectedItem.Text == "Approved" && ddlStatus.SelectedItem.Text.Trim()=="Open")
            {
                Db.constr = myGlobal.getAppSettingsDataForKey("tejSAP");
                int retval = Db.myExecuteScalar("select count(*) from PVSendMailLog Where PVID=" + lblPVId.Text + " And SendMailResponse='Mail Sent Successfully'");
                if (retval == 0)
                {
                    if (SendMailToSignatories())
                    {
                        lblMessage.Text = "PV sent to signatories.";
                        lblMsg.Text = "PV sent to signatories.";
                        Page.ClientScript.RegisterStartupScript(this.GetType(), "alert", "<script> alert('PV sent to signatories.'); </script>");
                    }
                }
                else
                {
                    lblMsg.Text = "PV is already sent to signatories";
                }
            }
            else
            {
                lblMsg.Text = "You are not authorized to send mail";
            }
        
    }
}



public class VendorAgeing
{
    public decimal Balance { get; set; }
    public decimal zeroTothirty { get; set; }
    public decimal thirtyoneTofourtyfive { get; set; }
    public decimal fourtyfiveTosixty { get; set; }
    public decimal sixtyoneToninty { get; set; }
    public decimal nintyoneToonetwenty { get; set; }
    public decimal onetwentyplus { get; set; }
}