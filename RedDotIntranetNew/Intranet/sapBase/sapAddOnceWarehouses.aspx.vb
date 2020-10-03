Imports System.Data.SqlClient
Imports System.Data
Imports System.Web.UI.HtmlControls
Partial Class Intranet_sapBase_sapAddOnceWarehouses
    Inherits System.Web.UI.Page

    Dim createFlgAE, createFlgUG, createFlgTZ, createFlgKE As Integer
    Dim conFlg, flgStatus As Boolean
    Dim constr As String
    Public Shared oCompany1, oCompanyAE, oCompanyUG, oCompanyTZ, oCompanyKE As SAPbobsCOM.Company
    Public Shared oCmpSrv As SAPbobsCOM.CompanyService
    'Public Shared oIPS As SAPbobsCOM.InventoryPostingsService
    Public Shared oSBObob As SAPbobsCOM.SBObob
    Public Shared debugModule As String
    Dim dst As DataSet
    ' Dim MsgBoxControl1 As MsgBoxControl


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
        Db.LoadDDLsWithCon(ddl1, "select whsCode as fldValue,(whsName +' [' + whsCode +']') as ShowVal from dbo.OWHS order by whsName", "ShowVal", "fldValue", constr)

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
            lblMsg.Text = "Error ! " + pDB + " - " + ex.Message
        End Try
    End Sub


    Protected Sub cmdAdd_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles cmdAdd.Click
        Try
            lblError.Text = ""

            lblInfo.Text = ""

            If txtSimpleCode.Text = "" Or txtDesc1.Text = "" Then
                lblError.Text = "One or more fields left blank!!! Either Warehouse Code or Warehouse Description is left blank. Make sure these 2 textboxes are not blank."
                Exit Sub
            End If

            If txtSimpleCode.Text.IndexOf(" ") >= 0 Then
                lblError.Text = "Invalid Character occurs, Blank Space in field Warehouse Code not supported."
                Exit Sub
            End If

            If txtSimpleCode.Text.IndexOf("'") >= 0 Then
                lblError.Text = "Invalid Character occurs ' in field Warehouse Code , Char( ' ) not supported."
                Exit Sub
            End If

            If txtDesc1.Text.IndexOf("'") >= 0 Then
                lblError.Text = "Invalid Character occurs ' in field Warehouse Desc , Char( ' ) not supported."
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

            lblError.Text = "Warehouse '" + txtDesc1.Text.Trim() + " / " + txtSimpleCode.Text.Trim() + " Created for ... "


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

            myGlobal.updateSapAddonceLogTable("WAREHOUSE", txtSimpleCode.Text, txtDesc1.Text, createFlgAE, createFlgUG, createFlgTZ, createFlgKE, 0, myGlobal.loggedInUser())

            If flgStatus = True Then
                txtSimpleCode.Text = ""
                txtDesc1.Text = ""

                'Refresh list
                Call loadExistingData()
                lblError.Text = lblError.Text + " successfully"
            End If

            Message.Show(Page, lblError.Text)

        Catch ex As Exception
            lblError.Text = "Error : " + ex.Message
        End Try
    End Sub

    Private Sub workFor(ByVal pdb As String)

        If pdb.ToUpper() = "AE" Then
            addWarehouseNEW(oCompanyAE, txtSimpleCode.Text, txtDesc1.Text, pdb)
        ElseIf pdb.ToUpper() = "UG" Then
            addWarehouseNEW(oCompanyUG, txtSimpleCode.Text, txtDesc1.Text, pdb)
        ElseIf pdb.ToUpper() = "TZ" Then
            addWarehouseNEW(oCompanyTZ, txtSimpleCode.Text, txtDesc1.Text, pdb)
        ElseIf pdb.ToUpper() = "KE" Then
            addWarehouseNEW(oCompanyKE, txtSimpleCode.Text, txtDesc1.Text, pdb)
        End If

        If flgStatus = True Then
            lblError.Text = lblError.Text + pdb + " , ..."
        End If

    End Sub


    Private Sub addWarehouseNEW(ByRef oCompany As SAPbobsCOM.Company, ByVal WHCode As String, ByVal WhNm As String, ByVal dbCode As String)
        'U_whseCode	U_whseOwner	U_whsestatus	U_orderStatusType	U_functionType
        If flgStatus = False Then
            Exit Sub
        End If

        Dim oWarehouse As SAPbobsCOM.Warehouses
        Dim nErr As Long
        Dim errMsg As String
        Try
            oWarehouse = oCompany.GetBusinessObject(SAPbobsCOM.BoObjectTypes.oWarehouses)
            oWarehouse.WarehouseCode = WHCode
            oWarehouse.WarehouseName = WhNm
            'oWarehouse.UserFields.Fields.Item("U_whseCode").Value = U_whseCode
            'oWarehouse.UserFields.Fields.Item("U_whseOwner").Value = U_whseOwner
            'oWarehouse.UserFields.Fields.Item("U_whsestatus").Value = U_whsestatus
            'oWarehouse.UserFields.Fields.Item("U_orderStatusType").Value = U_orderStatusType
            'oWarehouse.UserFields.Fields.Item("U_functionType").Value = U_functionType

            If (0 <> oWarehouse.Add()) Then
                'Check Error
                Call oCompany.GetLastError(nErr, errMsg)
                If (0 <> nErr) Then
                    flgStatus = False
                    Message.Show(Page, dbCode + " !! Failed to add Warehouse:" + Str(nErr) + "," + errMsg)
                End If

            Else
                'Dim objCode As String
                'MsgBox("Succeeded add a business parnter, new objcode=" + oCOA.CardCode)
                'oCOA.SaveXML("c:\temp\BP" + oCOA.CardCode + ".xml")

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



End Class