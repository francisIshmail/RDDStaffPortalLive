Imports System.Data.SqlClient
Imports System.Data
Partial Class Intranet_sapBase_sapAddOnceGroup
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

    Private Sub ResetFieldsAll()

        pnlItem.Enabled = True

    End Sub

    Private Sub loadExistingData()
        'SAP UG Being the base database for existing data


        constr = myGlobal.getConnectionStringForSapDBs("UG")
        Db.LoadDDLsWithCon(ddl1, "select itmsGrpCod as fldValue ,(ItmsGrpNam+' [' + U_BU +']') as ShowVal from dbo.OITB order by ItmsGrpNam", "ShowVal", "fldValue", constr)

        'ddl1.DataSource = Db.myGetDS("Exec [tejSap].dbo.[getUdfValuesForUdfFieldAlias] 'UG','BU'").Tables(0)
        'ddl1.DataTextField = "ShowVal"
        'ddl1.DataValueField = "ValFld"
        'ddl1.DataBind()

        lblLstCnt.Text = ddl1.Items.Count.ToString()
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
            lblMsg.Text = "Error ! " + lRetCode.ToString() + " , " + pDB + " - " + ex.Message
        End Try
    End Sub
 

    Protected Sub cmdAdd_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles cmdAdd.Click
        Try
            lblError.Text = ""

            lblInfo.Text = ""

            If txtSimpleCode.Text = "" Or txtDesc1.Text = "" Then
                lblError.Text = "One or more fields left blank!!! Either Group Code or Group Description is left blank. Make sure these 2 textboxes are not blank."
                Exit Sub
            End If

            If txtSimpleCode.Text.IndexOf(" ") >= 0 Then
                lblError.Text = "Invalid Character occurs, Blank Space in field Group Code not supported."
                Exit Sub
            End If

            If txtSimpleCode.Text.IndexOf("'") >= 0 Then
                lblError.Text = "Invalid Character occurs ' in field Group Code , Char( ' ) not supported."
                Exit Sub
            End If

            If txtDesc1.Text.IndexOf("'") >= 0 Then
                lblError.Text = "Invalid Character occurs ' in field Group Desc , Char( ' ) not supported."
                Exit Sub
            End If

            If chkAEdo.Checked = False And chkUGdo.Checked = False And chkTZdo.Checked = False And chkKEdo.Checked = False Then
                lblError.Text = "Error !  Please select at least one database to proceed and retry"
                Exit Sub
            End If

            txtSimpleCode.Text = txtSimpleCode.Text.Trim()
            txtDesc1.Text = txtDesc1.Text.Trim()

            flgStatus = True
            createFlgAE = createFlgUG = createFlgTZ = createFlgKE = 0   ''//intialize to not done

            lblError.Text = "BU/Group '" + txtDesc1.Text.Trim() + " / " + txtSimpleCode.Text.Trim() + " Created for ... "


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

            myGlobal.updateSapAddonceLogTable("BU", txtSimpleCode.Text, txtDesc1.Text, createFlgAE, createFlgUG, createFlgTZ, createFlgKE, 0, myGlobal.loggedInUser())

            If flgStatus = True Then
                'Refresh list
                Call loadExistingData()
                txtSimpleCode.Text = ""
                txtDesc1.Text = ""
                lblError.Text = lblError.Text + " successfully"
            End If

            Message.Show(Page, lblError.Text)
        Catch ex As Exception
            lblError.Text = "Error : " + ex.Message
        End Try
    End Sub

    Private Sub workFor(ByVal pdb As String)

        If pdb.ToUpper() = "AE" Then
            addUDFValueNEW(oCompanyAE, "OITB", 0, txtSimpleCode.Text.Trim(), txtDesc1.Text.Trim(), pdb)
            addItemGroup(oCompanyAE, txtDesc1.Text.Trim(), "100200101000000", "500101600000000", "200100200300000", "400100700000000", "500102502000000", "500102504000000", "500102504000000", "100200101000000", "400100700000000", "500101600000000", "500102502000000", "600400300000000", "100200101000000", "100200101000000", "", txtSimpleCode.Text.Trim(), pdb)

        ElseIf pdb.ToUpper() = "UG" Then
            addUDFValueNEW(oCompanyUG, "OITB", 0, txtSimpleCode.Text.Trim(), txtDesc1.Text.Trim(), pdb)
            addItemGroup(oCompanyUG, txtDesc1.Text.Trim(), "100200101000000", "500101600000000", "200100200300000", "400100700000000", "500102502000000", "500102504000000", "500102504000000", "100200101000000", "400100700000000", "500101600000000", "500102502000000", "600400300000000", "100200101000000", "100200101000000", "", txtSimpleCode.Text.Trim(), pdb)

        ElseIf pdb.ToUpper() = "TZ" Then
            addUDFValueNEW(oCompanyTZ, "OITB", 0, txtSimpleCode.Text.Trim(), txtDesc1.Text.Trim(), pdb)
            addItemGroup(oCompanyTZ, txtDesc1.Text.Trim(), "100200101000000", "500101600000000", "200100200300000", "400100700000000", "500102502000000", "500102504000000", "500102504000000", "100200101000000", "400100700000000", "500101600000000", "500102502000000", "600400300000000", "100200101000000", "100200101000000", "", txtSimpleCode.Text.Trim(), pdb)

        ElseIf pdb.ToUpper() = "KE" Then
            addUDFValueNEW(oCompanyKE, "OITB", 0, txtSimpleCode.Text.Trim(), txtDesc1.Text.Trim(), pdb)
            addItemGroup(oCompanyKE, txtDesc1.Text.Trim(), "100200101000000", "500101600000000", "200100200300000", "400100700000000", "500102502000000", "500102504000000", "500102504000000", "100200101000000", "400100700000000", "500101600000000", "500102502000000", "600400300000000", "100200101000000", "100200101000000", "", txtSimpleCode.Text.Trim(), pdb)

        End If

        If flgStatus = True Then
            lblError.Text = lblError.Text + pdb + " , ..."
        End If

    End Sub
   
    Private Sub addUDFValueNEW(ByRef oCompany As SAPbobsCOM.Company, ByVal TableName As String, ByVal FieldNum As Integer, ByVal Value As String, ByVal Description As String, ByVal dbCode As String)

        If flgStatus = False Then
            Exit Sub
        End If

        debugModule = "addUDFValueNEW"
        Dim nErr As Long
        Dim errMsg As String
        Dim oUserFieldsMD

        'System.Runtime.InteropServices.Marshal.ReleaseComObject(oUserFieldsMD)
        'oUserFieldsMD = Nothing
        GC.Collect()
        oUserFieldsMD = oCompany.GetBusinessObject(SAPbobsCOM.BoObjectTypes.oUserFields)
        Try
            If Not oUserFieldsMD.GetByKey(TableName, FieldNum) Then Exit Sub

            If oUserFieldsMD.ValidValues.Value <> "" Then
                oUserFieldsMD.ValidValues.SetCurrentLine(oUserFieldsMD.ValidValues.Count - 1)
                oUserFieldsMD.ValidValues.Add()
            End If
            'oUserFieldsMD.ValidValues.SetCurrentLine(0)
            oUserFieldsMD.ValidValues.Value = Value
            oUserFieldsMD.ValidValues.Description = Description
            If (0 <> oUserFieldsMD.Update()) Then
                'Check Error
                Call oCompany.GetLastError(nErr, errMsg)
                If (0 <> nErr) Then
                    Message.Show(Page, dbCode + " !! Failed to add UDF value:" + Str(nErr) + "," + errMsg)
                End If
            Else
            End If
            System.Runtime.InteropServices.Marshal.ReleaseComObject(oUserFieldsMD)
            oUserFieldsMD = Nothing
            GC.Collect()
        Catch ex As Exception
            flgStatus = False
            Message.Show(Page, dbCode + " !! Failed to add Item Group to UDF : " + ex.Message)
        End Try

    End Sub

    Private Sub addItemGroup(ByRef oCompany As SAPbobsCOM.Company, ByVal Name As String, ByVal InventoryAccount As String, ByVal CostAccount As String, ByVal TransfersAccount As String, ByVal RevenuesAccount As String, ByVal VarianceAccount As String, ByVal DecreasingAccount As String, ByVal IncreasingAccount As String, ByVal ReturningAccount As String, ByVal ForeignRevenuesAccount As String, ByVal ForeignExpensesAccount As String, ByVal PriceDifferencesAccount As String, ByVal ExchangeRateDifferencesAccount As String, ByVal DecreaseGLAccount As String, ByVal IncreaseGLAccount As String, ByVal NegativeInventoryAdjustmentAccount As String, ByVal U_GrCode As String, ByVal dbCode As String)

        If flgStatus = False Then
            Exit Sub
        End If

        debugModule = "addItemGroup"

        Dim oItemGroup As SAPbobsCOM.ItemGroups
        Dim nErr As Long
        Dim errMsg As String
        errMsg = ""

        oItemGroup = oCompany.GetBusinessObject(SAPbobsCOM.BoObjectTypes.oItemGroups)
        oItemGroup.GroupName = Name
        'oItemGroup.InventoryAccount = InventoryAccount
        'oItemGroup.CostAccount = CostAccount
        'oItemGroup.TransfersAccount = TransfersAccount
        'oItemGroup.RevenuesAccount = RevenuesAccount
        'oItemGroup.VarianceAccount = VarianceAccount
        'oItemGroup.DecreasingAccount = DecreasingAccount
        'oItemGroup.IncreasingAccount = IncreasingAccount
        'oItemGroup.ReturningAccount = ReturningAccount
        'oItemGroup.ForeignRevenuesAccount = ForeignRevenuesAccount
        'oItemGroup.ForeignExpensesAccount = ForeignExpensesAccount
        'oItemGroup.DecreaseGLAccount = DecreaseGLAccount
        'oItemGroup.IncreaseGLAccount = IncreaseGLAccount
        'oItemGroup.PriceDifferencesAccount = PriceDifferencesAccount
        oItemGroup.UserFields.Fields.Item("U_BU").Value = U_GrCode

        If (0 <> oItemGroup.Add()) Then
            'Check Error
            Call oCompany.GetLastError(nErr, errMsg)
            If (0 <> nErr) Then
                flgStatus = False
                Message.Show(Page, dbCode + " !! Failed to add Item Group:" + Str(nErr) + "," + errMsg)
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
    End Sub
End Class