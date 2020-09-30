Imports System.Data.SqlClient
Imports System.Data
Partial Class Intranet_sapBase_sapAddOnceUDFs
    Inherits System.Web.UI.Page

    Dim createFlgAE, createFlgUG, createFlgTZ, createFlgKE As Integer
    Dim conFlg, flgStatus As Boolean
    Dim constr, getExistingDataFromDb As String
    Public Shared oCompany1, oCompanyAE, oCompanyUG, oCompanyTZ, oCompanyKE As SAPbobsCOM.Company
    Public Shared oCmpSrv As SAPbobsCOM.CompanyService
    'Public Shared oIPS As SAPbobsCOM.InventoryPostingsService
    Public Shared oSBObob As SAPbobsCOM.SBObob
    Public Shared debugModule As String
    Dim dst As DataSet
    Dim drd As SqlDataReader


    Protected Sub Page_Load(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.Load
        cmdAdd.Attributes.Add("onClick", "return getConfirmation();")
        '''''cmdConnectDb.Attributes.Add("onClick", "return waitMsg();")

        lblError.Text = ""

        getExistingDataFromDb = "AE"  ''SAPAE db''just intialized in begning, value is given at run time for the selected database

        If (IsPostBack <> True) Then
        End If
    End Sub

    Private Sub ResetFieldsAll()

        pnlItem.Enabled = True

    End Sub

    Private Sub loadExistingData()
        'SAP UG Being the base database for existing data


        constr = myGlobal.getConnectionStringForSapDBs(getExistingDataFromDb)

        Db.LoadDDLsWithCon(ddl1, "select distinct(AliasID) as fldValue FROM [dbo].[CUFD] order by AliasID", "fldValue", "fldValue", constr)

        'ddl1.DataSource = Db.myGetDS("Exec [tejSap].dbo.[getUdfValuesForUdfFieldAlias] 'UG','BU'").Tables(0)
        'ddl1.DataTextField = "ShowVal"
        'ddl1.DataValueField = "ValFld"
        'ddl1.DataBind()

        lblLstCnt.Text = ddl1.Items.Count.ToString()
    End Sub

    Protected Sub cmdConnectDb_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles cmdConnectDb.Click


        If (Not chkAE.Checked And Not chkUG.Checked And Not chkTZ.Checked And Not chkKE.Checked) Then
            lblError.Text = "Can't procedd as no database has been selected to connect to sap, Try again after selection."
            Exit Sub
        End If

            cmdConnectDb.Enabled = False

        chkAEdo.Checked = False
        chkUGdo.Checked = False
        chkTZdo.Checked = False
        chkKEdo.Checked = False

            conFlg = True

            lblMsg.Text = "Please wait while connecting.............."

        If chkAE.Checked Then
            connectSAPDB(oCompanyAE, myGlobal.getCredsForSapCompany("SAPCompanyConnectCredsAE"), "AE")
        ElseIf chkUG.Checked Then
            connectSAPDB(oCompanyUG, myGlobal.getCredsForSapCompany("SAPCompanyConnectCredsUG"), "UG")
        ElseIf chkTZ.Checked Then
            connectSAPDB(oCompanyTZ, myGlobal.getCredsForSapCompany("SAPCompanyConnectCredsTZ"), "TZ")
        ElseIf chkKE.Checked Then
            connectSAPDB(oCompanyKE, myGlobal.getCredsForSapCompany("SAPCompanyConnectCredsKE"), "KE")
        End If

        If conFlg = True Then
            Call loadExistingData()
            pnlItem.Enabled = True
            cmdConnectDb.Enabled = False
            lblMsg.Text = "Selected SAP Companies Connected .... Now Fill in the Form and click 'Add Item' at the bottom of the form"
            cmdAdd.Enabled = True

            chkAE.Enabled = False
            chkUG.Enabled = False
            chkTZ.Enabled = False
            chkKE.Enabled = False

        Else
            '' //reload / reconect page
            cmdConnectDb.Enabled = True
            Message.Show(Page, "Issue : failed to connect sap Company, Please try to reconnect, or contact system admin")

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

        getExistingDataFromDb = pDB    ''''''''''this is to pick existing data from

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
                chkAEdo.Checked = True
                ' oCompanyAE = oCompany
            ElseIf pDB.ToUpper() = "UG" Then
                chkUG.Checked = True
                chkUGdo.Checked = True
                ' oCompanyUG = oCompany
            ElseIf pDB.ToUpper() = "TZ" Then
                chkTZ.Checked = True
                chkTZdo.Checked = True
                ' oCompanyTZ = oCompany
            ElseIf pDB.ToUpper() = "KE" Then
                chkKE.Checked = True
                chkKEdo.Checked = True
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
                lblError.Text = "One or more fields left blank!!! Either UDF Value or UDF Description is left blank. Make sure these 2 textboxes are not blank."
                Exit Sub
            End If

            If txtSimpleCode.Text.IndexOf(" ") >= 0 Then
                lblError.Text = "Invalid Character occurs, Blank Space in field UDF Value not supported."
                Exit Sub
            End If

            If txtSimpleCode.Text.IndexOf("'") >= 0 Then
                lblError.Text = "Invalid Character occurs ' in field UDF Value , Char( ' ) not supported."
                Exit Sub
            End If

            If txtDesc1.Text.IndexOf("'") >= 0 Then
                lblError.Text = "Invalid Character occurs ' in field UDF Desc , Char( ' ) not supported."
                Exit Sub
            End If

            If txtTbl.Text.IndexOf(" ") >= 0 Then
                lblError.Text = "Invalid Character occurs, Blank Space in field 'TableID' not supported."
                Exit Sub
            End If

            If txtTbl.Text.IndexOf("'") >= 0 Then
                lblError.Text = "Invalid Character occurs ' in field 'TableID' , Char( ' ) not supported."
                Exit Sub
            End If

            If chkAEdo.Checked = False And chkUGdo.Checked = False And chkTZdo.Checked = False And chkKEdo.Checked = False Then
                lblError.Text = "Error !  Please select at least one database to proceed and retry"
                Exit Sub
            End If

            txtSimpleCode.Text = txtSimpleCode.Text.ToUpper()
            txtDesc1.Text = txtDesc1.Text.Trim()
            txtTbl.Text = txtTbl.Text.Trim()

            flgStatus = True
            createFlgAE = createFlgUG = createFlgTZ = createFlgKE = 0   ''//intialize to not done

            lblError.Text = "UDF Description/Value/TableID '" + txtDesc1.Text.Trim() + " / " + txtSimpleCode.Text.Trim() + " / " + txtTbl.Text.Trim() + " Created for ... "


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

            myGlobal.updateSapAddonceLogTable("UDF", txtSimpleCode.Text + " ; " + txtDesc1.Text, ddl1.SelectedItem.Text + " ; " + txtTbl.Text, createFlgAE, createFlgUG, createFlgTZ, createFlgKE, 0, myGlobal.loggedInUser())

            If flgStatus = True Then
                'Refresh list
                'Call loadExistingData()  'no need to refresh
                txtSimpleCode.Text = ""
                txtDesc1.Text = ""
                txtTbl.Text = ""
                lblError.Text = lblError.Text + " successfully"
            End If

            Message.Show(Page, lblError.Text)
        Catch ex As Exception
            lblError.Text = "Error : " + ex.Message
        End Try
    End Sub

    Private Sub workFor(ByVal pdb As String)

        Dim udfId As Integer

        udfId = -1

        constr = myGlobal.getConnectionStringForSapDBs(getExistingDataFromDb)
        drd = Db.myGetReader("select fieldid FROM [dbo].[CUFD] where TableID='" + txtTbl.Text.Trim() + "' and aliasid='" + ddl1.SelectedItem.Text + "' ")

        If (drd.HasRows) Then
            drd.Read()
            udfId = Convert.ToInt32(drd(0))
        Else
            lblError.Text = "Error !!!!! Seems Like UDF filed '" + ddl1.SelectedItem.Text + "' does not exist for table '" + txtTbl.Text.Trim() + "' , Please verify filed values on Form and retry or Contact system admin."
            Exit Sub
        End If

        drd.Close()

        If pdb.ToUpper() = "AE" Then
            addUDFValueNEW(oCompanyAE, txtTbl.Text.Trim(), udfId, txtSimpleCode.Text.Trim(), txtDesc1.Text.Trim(), pdb)

        ElseIf pdb.ToUpper() = "UG" Then
            addUDFValueNEW(oCompanyUG, txtTbl.Text.Trim(), udfId, txtSimpleCode.Text.Trim(), txtDesc1.Text.Trim(), pdb)

        ElseIf pdb.ToUpper() = "TZ" Then
            addUDFValueNEW(oCompanyTZ, txtTbl.Text.Trim(), udfId, txtSimpleCode.Text.Trim(), txtDesc1.Text.Trim(), pdb)

        ElseIf pdb.ToUpper() = "KE" Then
            addUDFValueNEW(oCompanyKE, txtTbl.Text.Trim(), udfId, txtSimpleCode.Text.Trim(), txtDesc1.Text.Trim(), pdb)
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
            System.Runtime.InteropServices.Marshal.ReleaseComObject(oUserFieldsMD)
            oUserFieldsMD = Nothing
            GC.Collect()
        Catch ex As Exception
            flgStatus = False
            Message.Show(Page, dbCode + " !! Failed to add UDF : " + ex.Message)
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