Imports System.Data.SqlClient
Imports System.Data
Partial Class Intranet_sapBase_sapAddOnceGLAccounts
    Inherits System.Web.UI.Page

    Dim createFlgAE, createFlgUG, createFlgTZ, createFlgKE As Integer
    Dim conFlg, flgStatus As Boolean
    Dim constr As String
    Public Shared oCompany1, oCompanyAE, oCompanyUG, oCompanyTZ, oCompanyKE As SAPbobsCOM.Company
    Public Shared oCmpSrv As SAPbobsCOM.CompanyService
    'Public Shared oIPS As SAPbobsCOM.InventoryPostingsService
    Public Shared oSBObob As SAPbobsCOM.SBObob
    Public Shared debugModule As String
    Dim dst As dataset


    Protected Sub Page_Load(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.Load
        cmdAdd.Attributes.Add("onClick", "return getConfirmation();")
        '''''cmdConnectDb.Attributes.Add("onClick", "return waitMsg();")

        lblError.Text = ""
        If (IsPostBack <> True) Then
        End If
    End Sub

    Protected Sub Page_Unload(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.Unload
        ''If (IsNothing(oCompanyAE) = False And oCompanyAE.Connected = True) Then
        ''    oCompanyAE.Disconnect()
        ''End If

        ''If (IsNothing(oCompanyUG) = False And oCompanyUG.Connected = True) Then
        ''    oCompanyUG.Disconnect()
        ''End If

        ''If (IsNothing(oCompanyTZ) = False And oCompanyTZ.Connected = True) Then
        ''    oCompanyTZ.Disconnect()
        ''End If

        ''If (IsNothing(oCompanyKE) = False And oCompanyKE.Connected = True) Then
        ''    oCompanyKE.Disconnect()
        ''End If

        'oCompanyAE = Nothing
        'oCompanyUG = Nothing
        'oCompanyTZ = Nothing
        'oCompanyKE = Nothing
        'GC.Collect()
    End Sub

    Private Sub ResetFieldsAll()

        pnlItem.Enabled = True

    End Sub

    Private Sub loadExistingData()
        'SAP UG Being the base database for existing data


        constr = myGlobal.getConnectionStringForSapDBs("AE")
        Db.LoadDDLsWithCon(ddl1, "select AcctCode as fldValue,(AcctName + ' [' + convert(varchar(2),Levels) +'] ' + ' [' + AcctCode +']') as ShowVal from dbo.OACt where postable='N' and levels<" + ddlLvl.SelectedValue.ToString() + " order by levels,AcctName", "ShowVal", "fldValue", constr)

        'ddl1.DataSource = Db.myGetDS("Exec [tejSap].dbo.[getUdfValuesForUdfFieldAlias] 'UG','BU'").Tables(0)
        'ddl1.DataTextField = "ShowVal"
        'ddl1.DataValueField = "ValFld"
        'ddl1.DataBind()

        lblLstCnt.Text = ddl1.Items.Count.ToString()

        DDLPostable.SelectedIndex = 0
        DDLAcctType.SelectedIndex = 0
    End Sub

    Protected Sub cmdConnectDb_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles cmdConnectDb.Click
        cmdConnectDb.Enabled = False

        chkAE.Checked = False
        chkUG.Checked = False
        chkTZ.Checked = False
        chkKE.Checked = False

        conFlg = True

        lblMsg.Text = "Please wait while connecting.............."
        connectSAPDB(oCompanyAE, myGlobal.getCredsForSapCompany("SAPCompanyConnectCredsAE"), "AE")
        connectSAPDB(oCompanyUG, myGlobal.getCredsForSapCompany("SAPCompanyConnectCredsUG"), "UG")
        connectSAPDB(oCompanyTZ, myGlobal.getCredsForSapCompany("SAPCompanyConnectCredsTZ"), "TZ")
        connectSAPDB(oCompanyKE, myGlobal.getCredsForSapCompany("SAPCompanyConnectCredsKE"), "KE")

        If conFlg = True Then
            If ddlLvl.SelectedIndex < 0 Then
                ddlLvl.SelectedIndex = 3
            End If

            Call loadExistingData()
            pnlItem.Enabled = True
            cmdConnectDb.Enabled = False
            lblMsg.Text = "SAP Companies are Connected .... Now Fill in the Form and click 'Add Item' at the bottom of the form"
            cmdAdd.Enabled = True
        Else
            '' //reload / reconect page
            cmdConnectDb.Enabled = True
            Message.Show(Page, "Issue : One of the companies failed to connect, Please try to reconnect, or contact system admin")

        End If

    End Sub

    Private Sub connectSAPDB(ByRef oCompany As SAPbobsCOM.Company, ByVal pCon As String, ByVal pDB As String)

        If conFlg = False Then
            Exit Sub
        End If

        Dim lRetCode As Integer
        Dim conElements() As String

        lblError.Text = ""

        constr = pCon

        If constr = "" Then
            lblError.Text = pDB + " : Connection information not found in settings. Please contact system administrator"
            Exit Sub
        End If

        conElements = constr.Split(";")

        ' Dim oCompany As SAPbobsCOM.Company
        oCompany = New SAPbobsCOM.Company
        Try
            oCompany.Server = conElements(0) '  "fresh"
            oCompany.DbUserName = conElements(1) '"sa"
            oCompany.DbPassword = conElements(2) ' "Qwert123!@#"
            oCompany.CompanyDB = conElements(3) ' "sapUG"

            oCompany.UserName = conElements(4) ' "manager"
            oCompany.Password = conElements(5) '"reddot321"
            oCompany.DbServerType = SAPbobsCOM.BoDataServerTypes.dst_MSSQL2008 'cmbDBType.SelectedIndex + 1
            oCompany.language = SAPbobsCOM.BoSuppLangs.ln_English

            '// Use Windows authentication for database server.
            '// True for NT server authentication,
            '// False for database server authentication.
            oCompany.UseTrusted = False
            oCompany.LicenseServer = conElements(6)  '    "fresh" & ":" & "30000"

            '// Connecting to a company DB
            lRetCode = oCompany.Connect
            oCmpSrv = oCompany.GetCompanyService
            oSBObob = oCompany.GetBusinessObject(SAPbobsCOM.BoObjectTypes.BoBridge)

            'pnlItem.Enabled = True

            'cmdConnectDb.Enabled = False
            'lblMsg.Text = "Database/Company Connected .... Now Fill in the Form and click 'Add Item' at the bottom of the form"
            'cmdAdd.Enabled = True

            If pDB.ToUpper() = "AE" Then
                chkAE.Checked = True
                ' oCompanyAE = oCompany
            ElseIf pDB.ToUpper() = "UG" Then
                chkUG.Checked = True
                ' oCompanyUG = oCompany
            ElseIf pDB.ToUpper() = "TZ" Then
                chkTZ.Checked = True
                ' oCompanyTZ = oCompany
            ElseIf pDB.ToUpper() = "KE" Then
                chkKE.Checked = True
                ' oCompanyKE = oCompany
            End If

        Catch ex As Exception
            conFlg = False
            lblMsg.Text = "Error ! " + pDB + " - " + ex.Message
        End Try
    End Sub


    Protected Sub cmdAdd_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles cmdAdd.Click
        Try
            lblError.Text = ""

            lblInfo.Text = ""

            If ddl1.SelectedIndex < 0 Then
                lblError.Text = "Error : Please select father account for the new account and retry."
                Exit Sub
            End If


            If ddlLvl.SelectedIndex < 0 Then
                lblError.Text = "Error : Please select account level from the droplist, and retry."
                Exit Sub
            End If


            If txtSimpleCode.Text = "" Or txtDesc1.Text = "" Then
                lblError.Text = "One or more fields left blank!!! Either GL Account Code or GL Account Description is left blank. Make sure these 2 textboxes are not blank."
                Exit Sub
            End If

            If txtSimpleCode.Text.IndexOf(" ") >= 0 Then
                lblError.Text = "Invalid Character occurs, Blank Space in field GL Account Code not supported."
                Exit Sub
            End If

            If txtSimpleCode.Text.IndexOf("'") >= 0 Then
                lblError.Text = "Invalid Character occurs ' in field GL Account Code , Char( ' ) not supported."
                Exit Sub
            End If

            If txtDesc1.Text.IndexOf("'") >= 0 Then
                lblError.Text = "Invalid Character occurs ' in field GL Account Desc , Char( ' ) not supported."
                Exit Sub
            End If

            If chkAEdo.Checked = False And chkUGdo.Checked = False And chkTZdo.Checked = False And chkKEdo.Checked = False Then
                lblError.Text = "Error !  Please select at least one database to proceed and retry"
                Exit Sub
            End If


            If DDLPostable.SelectedValue.ToString().ToUpper() = "SELECT" Then
                lblError.Text = "Error : Please select Title status for the new account and retry."
                Exit Sub
            End If

            If DDLAcctType.SelectedValue.ToString().ToUpper() = "SELECT" Then
                lblError.Text = "Error : Please select Account Type for the new account and retry."
                Exit Sub
            End If

            txtSimpleCode.Text = txtSimpleCode.Text.Trim()
            txtDesc1.Text = txtDesc1.Text.Trim()

            flgStatus = True
            createFlgAE = createFlgUG = createFlgTZ = createFlgKE = 0   ''//intialize to not done

            lblError.Text = "GL Account '" + txtDesc1.Text.Trim() + " / " + txtSimpleCode.Text.Trim() + " Created for ... "


            If chkAEdo.Checked = True Then
                Call workFor("AE")
            End If

            If chkUGdo.Checked = True Then
                Call workFor("UG")
            End If

            If chkTZdo.Checked = True Then
                Call workFor("TZ")
            End If

            If chkKEdo.Checked = True Then
                Call workFor("KE")
            End If

            myGlobal.updateSapAddonceLogTable("GL ACCOUNTS", txtSimpleCode.Text, txtDesc1.Text, createFlgAE, createFlgUG, createFlgTZ, createFlgKE, 0, myGlobal.loggedInUser())


            If flgStatus = True Then
                txtSimpleCode.Text = ""
                txtDesc1.Text = ""

                'Refresh list
                Call loadExistingData()
                lblError.Text = lblError.Text + " successfully"
            Else
                lblError.Text = lblError.Text + " Error occured.., Please retry or contact sysadmin"
            End If

            'Message.Show(Page, lblError.Text)

        Catch ex As Exception
            lblError.Text = "Error : " + ex.Message
        End Try
    End Sub

    Private Sub workFor(ByVal pdb As String)

        ' ''If pdb.ToUpper() = "AE" Then
        ' ''    sapCls.initGlobals(pdb)   'send country code
        ' ''    addGLAccount(oCompanyAE, Convert.ToInt32(ddlLvl.SelectedValue), txtSimpleCode.Text, txtDesc1.Text, ddl1.SelectedValue.ToString(), "tYES", "tNO", "tNO", "at_Other", sapCls.AllCurrencies, pdb)
        ' ''ElseIf pdb.ToUpper() = "UG" Then
        ' ''    addGLAccount(oCompanyUG, Convert.ToInt32(ddlLvl.SelectedValue), txtSimpleCode.Text, txtDesc1.Text, ddl1.SelectedValue.ToString(), "tYES", "tNO", "tNO", "at_Other", sapCls.AllCurrencies, pdb)
        ' ''ElseIf pdb.ToUpper() = "TZ" Then
        ' ''    addGLAccount(oCompanyTZ, Convert.ToInt32(ddlLvl.SelectedValue), txtSimpleCode.Text, txtDesc1.Text, ddl1.SelectedValue.ToString(), "tYES", "tNO", "tNO", "at_Other", sapCls.AllCurrencies, pdb)
        ' ''ElseIf pdb.ToUpper() = "KE" Then
        ' ''    addGLAccount(oCompanyKE, Convert.ToInt32(ddlLvl.SelectedValue), txtSimpleCode.Text, txtDesc1.Text, ddl1.SelectedValue.ToString(), "tYES", "tNO", "tNO", "at_Other", sapCls.AllCurrencies, pdb)
        ' ''End If

        If pdb.ToUpper() = "AE" Then
            sapCls.initGlobals(pdb)   'send country code
            addGLAccount(oCompanyAE, Convert.ToInt32(ddlLvl.SelectedValue), txtSimpleCode.Text, txtDesc1.Text, ddl1.SelectedValue.ToString(), DDLPostable.SelectedValue().ToString(), "tNO", "tNO", DDLAcctType.SelectedValue().ToString(), sapCls.AllCurrencies, pdb)
        ElseIf pdb.ToUpper() = "UG" Then
            addGLAccount(oCompanyUG, Convert.ToInt32(ddlLvl.SelectedValue), txtSimpleCode.Text, txtDesc1.Text, ddl1.SelectedValue.ToString(), DDLPostable.SelectedValue().ToString(), "tNO", "tNO", DDLAcctType.SelectedValue().ToString(), sapCls.AllCurrencies, pdb)
        ElseIf pdb.ToUpper() = "TZ" Then
            addGLAccount(oCompanyTZ, Convert.ToInt32(ddlLvl.SelectedValue), txtSimpleCode.Text, txtDesc1.Text, ddl1.SelectedValue.ToString(), DDLPostable.SelectedValue().ToString(), "tNO", "tNO", DDLAcctType.SelectedValue().ToString(), sapCls.AllCurrencies, pdb)
        ElseIf pdb.ToUpper() = "KE" Then
            addGLAccount(oCompanyKE, Convert.ToInt32(ddlLvl.SelectedValue), txtSimpleCode.Text, txtDesc1.Text, ddl1.SelectedValue.ToString(), DDLPostable.SelectedValue().ToString(), "tNO", "tNO", DDLAcctType.SelectedValue().ToString(), sapCls.AllCurrencies, pdb)
        End If

        If flgStatus = True Then
            lblError.Text = lblError.Text + pdb + " , ..."
        End If

    End Sub

    Private Sub addGLAccount(ByRef oCompany As SAPbobsCOM.Company, ByVal Levels As Integer, ByVal Code As String, ByVal Name As String, ByVal FatherAccountKey As String, ByVal ActiveAccount As String, ByVal CashAccount As String, ByVal LockManualTransaction As String, ByVal AccountType As String, ByVal Currency As String, ByVal dbCode As String)
        'Create the BusinessPartners object
        If flgStatus = False Then
            Exit Sub
        End If

        Dim oCOA As SAPbobsCOM.ChartOfAccounts
        Dim nErr As Long
        Dim errMsg As String

        Try
            errMsg = ""

            oCOA = oCompany.GetBusinessObject(SAPbobsCOM.BoObjectTypes.oChartOfAccounts)
            oCOA.Code = Code
            oCOA.Name = Name
            '''''' add code to set Title acct status , filed not identified
            oCOA.FatherAccountKey = FatherAccountKey
            oCOA.ActiveAccount = IIf(ActiveAccount = "tNO", SAPbobsCOM.BoYesNoEnum.tNO, SAPbobsCOM.BoYesNoEnum.tYES)
            oCOA.CashAccount = IIf(CashAccount = "tNO", SAPbobsCOM.BoYesNoEnum.tNO, SAPbobsCOM.BoYesNoEnum.tYES)
            oCOA.LockManualTransaction = IIf(LockManualTransaction = "tNO", SAPbobsCOM.BoYesNoEnum.tNO, SAPbobsCOM.BoYesNoEnum.tYES)
            oCOA.AccountType = IIf(AccountType = "at_Other", SAPbobsCOM.BoAccountTypes.at_Other, IIf(AccountType = "at_Expenses", SAPbobsCOM.BoAccountTypes.at_Expenses, SAPbobsCOM.BoAccountTypes.at_Revenues))


            ' ''''''''''''just for reference'''''''''''

            ''''''''''addGLAccount(ByVal Levels As Integer, ByVal Code As String, ByVal Name As String, ByVal FatherAccountKey As String,
            ''''''''' ByVal ActiveAccount As String, ByVal CashAccount As String, ByVal LockManualTransaction As String, ByVal AccountType As String, ByVal Currency As String)

            ' ''addGLAccount(4, "10-05-15-10-000", "APC                  ", "10-05-15-00-000", "tYES", "tNO", "tNO", "at_Other", AllCurrencies)
            ' ''addGLAccount(5, "10-05-30-40-010", "APC-MDF              ", "10-05-30-40-000", "tYES", "tNO", "tNO", "at_Other", AllCurrencies)
            ' ''addGLAccount(5, "10-05-30-40-025", "APC-Sellout Rebate   ", "10-05-30-40-000", "tYES", "tNO", "tNO", "at_Other", AllCurrencies)
            ' ''addGLAccount(5, "10-05-30-45-010", "APC-Claim            ", "10-05-30-45-000", "tYES", "tNO", "tNO", "at_Other", AllCurrencies)
            ' ''addGLAccount(3, "40-05-10-00-000", "Sales-APC            ", "40-05-00-00-000", "tYES", "tNO", "tNO", "at_Revenues", AllCurrencies)

            ' ''addGLAccount(3, "50-05-15-00-000", "Cost of Sales-APC    ", "50-05-00-00-000", "tNO", "tNO", "tNO", "at_Other", AllCurrencies)       '' TITLE ALL NO
            ' ''addGLAccount(4, "50-05-15-05-000", "Cost of Sales-APC    ", "50-05-15-00-000", "tNO", "tNO", "tNO", "at_Other", AllCurrencies)       '' TITLE ALL NO

            ' ''addGLAccount(5, "50-05-15-05-005", "Cost of Goods-APC    ", "50-05-15-05-000", "tYES", "tNO", "tNO", "at_Expenses", AllCurrencies)
            ' ''addGLAccount(5, "50-05-15-05-010", "Hub charges-APC      ", "50-05-15-05-000", "tYES", "tNO", "tNO", "at_Expenses", AllCurrencies)
            ' ''addGLAccount(5, "50-05-15-05-015", "MDF Sellout-APC      ", "50-05-15-05-000", "tYES", "tNO", "tNO", "at_Expenses", AllCurrencies)
            ' ''addGLAccount(5, "50-05-15-05-020", "Sellout Incentive-APC", "50-05-15-05-000", "tYES", "tNO", "tNO", "at_Expenses", AllCurrencies)

            ' ''addGLAccount(3, "70-10-10-00-000", "APC-MDF              ", "70-10-00-00-000", "tNO", "tNO", "tNO", "at_Other", AllCurrencies)       '' TITLE ALL NO

            ' ''addGLAccount(4, "70-10-10-05-000", "APC-MDF Expense      ", "70-10-10-00-000", "tYES", "tNO", "tNO", "at_Expenses", AllCurrencies)
            ' ''addGLAccount(4, "70-10-10-10-000", "APC-MDF Income       ", "70-10-10-00-000", "tYES", "tNO", "tNO", "at_Revenues", AllCurrencies)
            ' ''addGLAccount(3, "70-20-10-00-000", "APC-Target Rebate    ", "70-20-00-00-000", "tYES", "tNO", "tNO", "at_Revenues", AllCurrencies)

            ''''''''''''''''''



            If (Currency <> sapCls.AllCurrencies) Then oCOA.AcctCurrency = Currency

            If (0 <> oCOA.Add()) Then
                'Check Error
                Call oCompany.GetLastError(nErr, errMsg)
                If (0 <> nErr) Then
                    flgStatus = False
                    Message.Show(Page, dbCode + " !!  Failed to add GL Acoount:" + Str(nErr) + "," + errMsg)
                End If

            Else
                'lblError.Text += " success "

                If dbCode = "AE" Then
                    createFlgAE = 1
                ElseIf dbCode = "UG" Then
                    createFlgUG = 1
                ElseIf dbCode = "TZ" Then
                    createFlgTZ = 1
                ElseIf dbCode = "KE" Then
                    createFlgKE = 1
                End If

            End If

        Catch ex As Exception
            flgStatus = False
            Message.Show(Page, "Error :" + dbCode + " , " + ex.Message)
        End Try
    End Sub

    'Protected Sub Timer1_Tick(ByVal sender As Object, ByVal e As System.EventArgs) Handles Timer1.Tick
    '    'lblError.Text = "Grid Refreshed at: " + DateTime.Now.ToLongTimeString()
    '    lblError.Text = lblError.Text
    'End Sub

    Protected Sub ddlLvl_SelectedIndexChanged(ByVal sender As Object, ByVal e As System.EventArgs) Handles ddlLvl.SelectedIndexChanged
        loadExistingData()
    End Sub

   
End Class