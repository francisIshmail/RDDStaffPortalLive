Imports System.Data.SqlClient
Imports System.Data
Partial Class Intranet_sapBase_sapAddOnceItems
    Inherits System.Web.UI.Page

    Dim createFlgAE, createFlgUG, createFlgTZ, createFlgKE, createFlgZM, createFlgMA, createFlgTRI As Integer
    Dim conFlg, flgStatus As Boolean
    Dim constr As String
    Public Shared oCompany1, oCompanyAE, oCompanyUG, oCompanyTZ, oCompanyKE, oCompanyZM, oCompanyMA, oCompanyTRI As SAPbobsCOM.Company
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

    'Private Sub loadCategory()
    '    ddlCat.Items.Clear()
    '    lblLstCntCat.Text = "0"
    '    constr = myGlobal.getConnectionStringForSapDBs("TEJSAP")
    '    Db.LoadDDLsWithCon(ddlCat, "select cid,category from tejSap.dbo.tblCategory order by category", "category", "cid", constr)
    '    lblLstCntCat.Text = ddlCat.Items.Count.ToString()
    'End Sub
    Private Sub loadMfr()
        ddlMfr.Items.Clear()
        lblLstCntMfr.Text = "0"
        constr = myGlobal.getConnectionStringForSapDBs("TRI") 'SAPAE
        Db.LoadDDLsWithCon(ddlMfr, "select  FirmCode,FirmName from  sapAE.dbo.omrc order by FirmName", "FirmName", "FirmCode", constr)
        lblLstCntMfr.Text = ddlMfr.Items.Count.ToString()
        'ddlMfr.SelectedIndex = 1

        If ddlMfr.Items.Count > 0 Then
            radNewProductCategory.Enabled = True
            radNewProductGroup.Enabled = True
            radNewPL.Enabled = True
        Else
            radNewProductCategory.Enabled = False
            radNewProductGroup.Enabled = False
            radNewPL.Enabled = False
        End If

    End Sub

    Private Sub loadBu()
        If ddlMfr.SelectedIndex >= 0 Then
            ddl1.Items.Clear()
            lblLstCnt.Text = "0"
            constr = myGlobal.getConnectionStringForSapDBs("TEJSAP")
            '''Db.LoadDDLsWithCon(ddl1, "select G.fk_idGrpTbl as fldValue ,(G.descrBUForAddOnceSap+' [' + G.groupBU +']') as ShowVal,G.MfrCodeSapAE from tejSap.dbo.Mapping_BUs G where region='TRI' and G.MfrCodeSapAE=" + ddlMfr.SelectedValue.ToString() + " order by G.descrBUForAddOnceSap", "ShowVal", "fldValue", constr)
            Db.LoadDDLsWithCon(ddl1, "select * from tejSap.[dbo].[getBUListForAddonceView]  where MfrCodeSapAE=" + ddlMfr.SelectedValue.ToString() + " order by BU", "ShowVal", "fldValue", constr)
            lblLstCnt.Text = ddl1.Items.Count.ToString()
            Call splitBu()

            If ddl1.Items.Count > 0 Then
                radNewProductCategory.Enabled = True
                radNewProductGroup.Enabled = True
                radNewPL.Enabled = True
            Else
                radNewProductCategory.Enabled = False
                radNewProductGroup.Enabled = False
                radNewPL.Enabled = False
            End If
        End If
    End Sub
    'Private Sub loadMdl()
    '    If ddl1.SelectedIndex >= 0 Then
    '        ddlMdl.Items.Clear()
    '        ddlPL.Items.Clear()
    '        lblLstCntMdl.Text = "0"
    '        constr = myGlobal.getConnectionStringForSapDBs("TEJSAP")
    '        Db.LoadDDLsWithCon(ddlMdl, "select mid,model from  tejSap.dbo.tblModel where fk_buID=" + ddl1.SelectedValue.ToString() + " order by model", "model", "mid", constr)
    '        lblLstCntMdl.Text = ddlMdl.Items.Count.ToString()

    '        If ddlMdl.Items.Count > 0 Then
    '            radNewPL.Enabled = True
    '        Else
    '            radNewPL.Enabled = False
    '        End If
    '    End If
    'End Sub
    Private Sub loadProductCategory()
        If ddl1.SelectedIndex >= 0 Then
            ddlProductCategory.Items.Clear()
            ddlPL.Items.Clear()
            lblLstCntProductCategory.Text = "0"
            constr = myGlobal.getConnectionStringForSapDBs("TEJSAP")
            Db.LoadDDLsWithCon(ddlProductCategory, "select pCatId,prodCategory from  tejSap.dbo.tblSapProductCategory where fk_buID=" + ddl1.SelectedValue.ToString() + " order by prodCategory", "prodCategory", "pCatId", constr)
            lblLstCntProductCategory.Text = ddlProductCategory.Items.Count.ToString()

            If ddlProductCategory.Items.Count > 0 Then
                radNewPL.Enabled = True
            Else
                radNewPL.Enabled = False
            End If
        End If
    End Sub

    Private Sub loadPL()
        If ddlProductCategory.SelectedIndex >= 0 Then
            ddlPL.Items.Clear()
            lblLstCntPL.Text = "0"
            constr = myGlobal.getConnectionStringForSapDBs("TEJSAP")
            Db.LoadDDLsWithCon(ddlPL, "select plId,plCode + ' [' + descrip + ']' as PLTxt,fk_pCatId from  tejSap.dbo.tblSapProductLine   where fk_pCatId=" + ddlProductCategory.SelectedValue.ToString() + " order by plCode", "PLTxt", "plId", constr)
            'Db.LoadDDLsWithCon(ddlPL, "select plId,plCode as PLTxt,fk_mid from  tejSap.dbo.tblProductLine   where fk_mid=" + ddlMdl.SelectedValue.ToString() + " order by plCode", "PLTxt", "plId", constr)
            lblLstCntPL.Text = ddlPL.Items.Count.ToString()
            splitPL()
        End If
    End Sub

    Private Sub loadProductGroup()
        If ddl1.SelectedIndex >= 0 Then
            ddlProductGroup.Items.Clear()
            lblLstCntProductGroup.Text = "0"
            constr = myGlobal.getConnectionStringForSapDBs("TEJSAP")
            Db.LoadDDLsWithCon(ddlProductGroup, "select pGrpId,prodGroup from  tejSap.dbo.tblSapProductGroup where fk_buID=" + ddl1.SelectedValue.ToString() + " order by prodGroup", "prodGroup", "pGrpId", constr)
            lblLstCntProductGroup.Text = ddlProductGroup.Items.Count.ToString()
        End If
    End Sub

    'Private Sub loadDashCategory()
    '    ddlDcat.Items.Clear()
    '    lblLstCntDcat.Text = "0"
    '    constr = myGlobal.getConnectionStringForSapDBs("TEJSAP")
    '    Db.LoadDDLsWithCon(ddlDcat, "select dcId,dCategory from tejSap.dbo.tblDCategory where dCategory is not null and dCategory<>'null' order by dCategory", "dCategory", "dcId", constr)
    '    lblLstCntDcat.Text = ddlDcat.Items.Count.ToString()
    'End Sub

    Private Sub loadExistingData()
        'SAP AE Being the base database for existing data
        ''''loadCategory()
        loadMfr()
        loadBu()
        ''''loadMdl()
        loadProductCategory()
        loadPL()
        loadProductGroup()
        ''''loadDashCategory()
    End Sub

    

    ' ''Protected Sub cmdConnectDb_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles cmdConnectDb.Click

    ' ''    cmdConnectDb.Enabled = False

    ' ''    chkAE.Checked = False
    ' ''    chkUG.Checked = False
    ' ''    chkTZ.Checked = False
    ' ''    chkKE.Checked = False

    ' ''    conFlg = True

    ' ''    lblMsg.Text = "Please wait while connecting.............."
    ' ''    ''connectSAPDB(oCompanyAE, myGlobal.getCredsForSapCompany("SAPCompanyConnectCredsAE"), "AE")
    ' ''    ''connectSAPDB(oCompanyUG, myGlobal.getCredsForSapCompany("SAPCompanyConnectCredsUG"), "UG")
    ' ''    ''connectSAPDB(oCompanyTZ, myGlobal.getCredsForSapCompany("SAPCompanyConnectCredsTZ"), "TZ")
    ' ''    ''connectSAPDB(oCompanyKE, myGlobal.getCredsForSapCompany("SAPCompanyConnectCredsKE"), "KE")

    ' ''    'If conFlg = True Then
    ' ''    '    Call loadExistingData()
    ' ''    'pnlItem.Enabled = True
    ' ''    '    cmdConnectDb.Enabled = False
    ' ''    '    lblMsg.Text = "SAP Companies are Connected .... Now Fill in the Form and click 'Add Item' at the bottom of the form"
    ' ''    '    cmdAdd.Enabled = True
    ' ''    'Else
    ' ''    '    '' //reload / reconect page
    ' ''    '    cmdConnectDb.Enabled = True
    ' ''    '    MsgBox("Issue : One of the companies failed to connect, Please try to reconnect, or contact system admin")

    ' ''    'End If

    ' ''    ''remove temport , open above code
    ' ''    pnlItem.Enabled = True
    ' ''    loadExistingData()

    ' ''End Sub

    Protected Sub cmdConnectDb_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles cmdConnectDb.Click

        cmdConnectDb.Enabled = False

        chkAE.Checked = False
        chkUG.Checked = False
        chkTZ.Checked = False
        chkKE.Checked = False
        chkZM.Checked = False
        chkMA.Checked = False
        chkTRI.Checked = False

        conFlg = True

        lblMsg.Text = "Please wait while connecting.............."

        connectSAPDB(oCompanyAE, myGlobal.getCredsForSapCompany("SAPCompanyConnectCredsAE"), "AE")
        connectSAPDB(oCompanyUG, myGlobal.getCredsForSapCompany("SAPCompanyConnectCredsUG"), "UG")
        connectSAPDB(oCompanyTZ, myGlobal.getCredsForSapCompany("SAPCompanyConnectCredsTZ"), "TZ")
        connectSAPDB(oCompanyKE, myGlobal.getCredsForSapCompany("SAPCompanyConnectCredsKE"), "KE")
        connectSAPDB(oCompanyZM, myGlobal.getCredsForSapCompany("SAPCompanyConnectCredsZM"), "ZM")
        connectSAPDB(oCompanyMA, myGlobal.getCredsForSapCompany("SAPCompanyConnectCredsMA"), "MA")
        connectSAPDB(oCompanyTRI, myGlobal.getCredsForSapCompany("SAPCompanyConnectCredsTRI"), "TRI")

        If conFlg = True Then
            Call loadExistingData()
            pnlItem.Enabled = True
            cmdConnectDb.Enabled = False
            lblMsg.Text = "SAP Companies are Connected .... Now Fill in the Form and click 'Add Item' at the bottom of the form"
            cmdAdd.Enabled = True
        Else
            '' //reload / reconect page
            cmdConnectDb.Enabled = True
            lblMsg.Text = lblMsg.Text + " Failed to connect to SAP."
            ''MsgBox("Issue : One of the companies failed to connect, Please try to reconnect, or contact system admin")

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

            Dim ErrMessage As String
            ErrMessage = ""
            If (lRetCode <> 0) Then
                oCompany.GetLastError(lRetCode, ErrMessage)
                lblMsg.Text = ErrMessage
            End If

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
            ElseIf pDB.ToUpper() = "ZM" Then
                chkZM.Checked = True
                ' oCompanyKE = oCompany
            ElseIf pDB.ToUpper() = "MA" Then
                chkMA.Checked = True
                ' oCompanyKE = oCompany
            ElseIf pDB.ToUpper() = "TRI" Then
                chkTRI.Checked = True
                ' oCompanyKE = oCompany
            End If

        Catch ex As Exception
            conFlg = False
            'MsgBox(ex.Message)
            lblMsg.Text = "Error ! " + pDB + " - " + ex.Message
        End Try
    End Sub


    Protected Sub cmdAdd_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles cmdAdd.Click
        Try
            lblError.Text = ""

            lblInfo.Text = ""

            If txtSimpleCode.Text = "" Or txtDesc1.Text = "" Then
                lblError.Text = "One or more fields left blank!!! Either Item Code or Item Description is left blank. Make sure these 2 textboxes are not blank."
                Exit Sub
            End If

            'space is allowed

            'If txtSimpleCode.Text.IndexOf(" ") >= 0 Then
            '    lblError.Text = "Invalid Character occurs, Blank Space in field Item Code not supported."
            '    Exit Sub
            'End If

            If txtSimpleCode.Text.IndexOf("'") >= 0 Then
                lblError.Text = "Invalid Character occurs ' in field Item Code , Char( ' ) not supported."
                Exit Sub
            End If

            If txtDesc1.Text.IndexOf("'") >= 0 Then
                lblError.Text = "Invalid Character occurs ' in field Item Desc , Char( ' ) not supported."
                Exit Sub
            End If


            If chkAEdo.Checked = False And chkUGdo.Checked = False And chkTZdo.Checked = False And chkKEdo.Checked = False And chkZMdo.Checked = False And chkMAdo.Checked = False Then
                lblError.Text = "Error !  Please select at least one database to proceed and retry"
                Exit Sub
            End If


            '''''''''''''''''''''''new code

            ''''''''''''''''category field'''''''''''''''''''''''''
           
            'lblCat.Text = ddlCat.SelectedItem.Text.Trim()

            'If lblCat.Text.Trim() = "" Then
            '    lblError.Text = "Erro ! Field Category left blank!!!. Make sure either you select from the list or Enter new value in text field provided for it, and retry"
            '    Exit Sub
            'End If

            'If lblCat.Text.IndexOf("'") >= 0 Then
            '    lblError.Text = "Invalid Character occurs ' in field Category , Char( ' ) not supported."
            '    Exit Sub
            'End If
            '''''''''''''''''''''MF field'''''''''''''''''''''''''''
            lblMfr.Text = ddlMfr.SelectedItem.Text.Trim()
            If lblMfr.Text.Trim() = "" Then
                lblError.Text = "Erro ! Field Manufacturer left blank!!!.Make sure you select from the list, and retry"
                Exit Sub
            End If

            If lblMfr.Text.IndexOf("'") >= 0 Then
                lblError.Text = "Invalid Character occurs ' in field Manufacturer , Char( ' ) not supported."
                Exit Sub
            End If
            '''''''''''''''''''''BU field'''''''''''''''''''''''''''
            'bu code taken directly at final stage

            '''''''''''''''''''''Model field'''''''''''''''''''''''''''

            'lblMdl.Text = ddlMdl.SelectedItem.Text.Trim()

            'If lblMdl.Text.Trim() = "" Then
            '    lblError.Text = "Erro ! Field Model left blank!!!. Make sure either you select from the list or Enter new value in text field provided for it, and retry"
            '    Exit Sub
            'End If

            'If lblMdl.Text.IndexOf("'") >= 0 Then
            '    lblError.Text = "Invalid Character occurs ' in field Model , Char( ' ) not supported."
            '    Exit Sub
            'End If

            '''''''''''''''''''''Product Category field'''''''''''''''''''''''''''

            lblProductCategory.Text = ddlProductCategory.SelectedItem.Text.Trim()

            If lblProductCategory.Text.Trim() = "" Then
                lblError.Text = "Erro ! Field  Product Category left blank!!!. Make sure either you select from the list or Enter new value in text field provided for it, and retry"
                Exit Sub
            End If

            If lblProductCategory.Text.IndexOf("'") >= 0 Then
                lblError.Text = "Invalid Character occurs ' in field  Product Category, Char( ' ) not supported."
                Exit Sub
            End If

            '''''''''''''''''''''PL field'''''''''''''''''''''''''''

            'lblPL.Text = ddlPL.SelectedItem.Text.Trim()

            If lblPL.Text.Trim() = "" Then
                lblError.Text = "Erro ! Field Product Line left blank!!!. Make sure either you select from the list or Enter new value in text field provided for it, and retry"
                Exit Sub
            End If

            If lblPL.Text.IndexOf("'") >= 0 Then
                lblError.Text = "Invalid Character occurs ' in field Product Line , Char( ' ) not supported."
                Exit Sub
            End If

            '''''''''''''''''''''Product Group field'''''''''''''''''''''''''''

            lblProductGroup.Text = ddlProductGroup.SelectedItem.Text.Trim()

            If lblProductGroup.Text.Trim() = "" Then
                lblError.Text = "Erro ! Field  Product Group left blank!!!. Make sure either you select from the list or Enter new value in text field provided for it, and retry"
                Exit Sub
            End If

            If lblProductGroup.Text.IndexOf("'") >= 0 Then
                lblError.Text = "Invalid Character occurs ' in field  Product Group, Char( ' ) not supported."
                Exit Sub
            End If

            If txtLength.Text.Trim() = "" Then
                lblError.Text = "Error ! Please enter Length, and retry"
                Exit Sub
            End If

            If Convert.ToDouble(txtLength.Text.Trim()) <= 0 Then
                lblError.Text = "Error ! Please enter Length greater than zero, and retry"
                Exit Sub
            End If

            If txtWidth.Text.Trim() = "" Then
                lblError.Text = "Error ! Please enter Width, and retry"
                Exit Sub
            End If

            If Convert.ToDouble(txtWidth.Text.Trim()) <= 0 Then
                lblError.Text = "Error ! Please enter width greater than zero, and retry"
                Exit Sub
            End If

            If txtHeight.Text.Trim() = "" Then
                lblError.Text = "Error ! Please enter Height, and retry"
                Exit Sub
            End If

            If Convert.ToDouble(txtHeight.Text.Trim()) <= 0 Then
                lblError.Text = "Error ! Please enter height greater than zero, and retry"
                Exit Sub
            End If

            ''''''''''''''''Dashboard category''field''''''''''''''''''''''''''
            'lblDcat.Text = ddlDcat.SelectedItem.Text.Trim()

            'If lblDcat.Text.Trim() = "" Then
            '    lblError.Text = "Erro ! Field Dashboard Category left blank!!!. Make sure either you select from the list or Enter new value in text field provided for it, and retry"
            '    Exit Sub
            'End If

            'If lblDcat.Text.IndexOf("'") >= 0 Then
            '    lblError.Text = "Invalid Character occurs ' in field Dashboard Category , Char( ' ) not supported."
            '    Exit Sub
            'End If

            

            ''''''''''''''''''''''''''''new code ends''''''''''''''''''''''''''''''''''''

            txtSimpleCode.Text = txtSimpleCode.Text.Trim()
            txtDesc1.Text = txtDesc1.Text.Trim()

            flgStatus = True
            createFlgAE = createFlgUG = createFlgTZ = createFlgKE = createFlgZM = createFlgMA = createFlgTRI = 0   ''//intialize to not done


            lblError.Text = "Stock Item '" + txtSimpleCode.Text.Trim() + " / " + txtDesc1.Text.Trim() + " Created for ... "


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

            If chkZMdo.Checked = True Then
                Call workFor("ZM")
            End If

            If chkMAdo.Checked = True Then
                Call workFor("MA")
            End If

            If chkTRIdo.Checked = True Then
                Call workFor("TRI")
            End If

            myGlobal.updateSapAddonceLogTable("PartNo", txtSimpleCode.Text, txtDesc1.Text, createFlgAE, createFlgUG, createFlgTZ, createFlgKE, createFlgZM, myGlobal.loggedInUser())

            If flgStatus = True Then
                txtSimpleCode.Text = ""
                txtDesc1.Text = ""

                lblProductCategory.Text = ""
                lblProductGroup.Text = ""
                lblPL.Text = ""
                lblPLDesc.Text = ""
                'lblDcat.Text = ""
                txtLength.Text = ""
                txtWidth.Text = ""
                txtHeight.Text = ""
                txtWeight.Text = ""


                '''''Refresh list
                Call loadExistingData()
                lblError.Text = lblError.Text + " successfully"
            End If

            ''Message.Show(Page, lblError.Text)

        Catch ex As Exception
            lblError.Text = "Error : " + ex.Message
        End Try
    End Sub

    Private Sub workFor(ByVal pdb As String)

        If pdb.ToUpper() = "AE" Then
            sapCls.initGlobals(pdb)   'send country code
            addItem(oCompanyAE, txtSimpleCode.Text.Trim(), txtDesc1.Text.Trim(), Convert.ToInt32(ddlMfr.SelectedValue), Convert.ToInt32(ddl1.SelectedValue), lblGrp.Text, lblBU.Text, lblProductCategory.Text, lblPL.Text, lblProductGroup.Text, pdb)
        ElseIf pdb.ToUpper() = "UG" Then
            sapCls.initGlobals(pdb)   'send country code
            addItem(oCompanyUG, txtSimpleCode.Text.Trim(), txtDesc1.Text.Trim(), Convert.ToInt32(ddlMfr.SelectedValue), Convert.ToInt32(ddl1.SelectedValue), lblGrp.Text, lblBU.Text, lblProductCategory.Text, lblPL.Text, lblProductGroup.Text, pdb)
        ElseIf pdb.ToUpper() = "TZ" Then
            sapCls.initGlobals(pdb)   'send country code
            addItem(oCompanyTZ, txtSimpleCode.Text.Trim(), txtDesc1.Text.Trim(), Convert.ToInt32(ddlMfr.SelectedValue), Convert.ToInt32(ddl1.SelectedValue), lblGrp.Text, lblBU.Text, lblProductCategory.Text, lblPL.Text, lblProductGroup.Text, pdb)
        ElseIf pdb.ToUpper() = "KE" Then
            sapCls.initGlobals(pdb)   'send country code
            addItem(oCompanyKE, txtSimpleCode.Text.Trim(), txtDesc1.Text.Trim(), Convert.ToInt32(ddlMfr.SelectedValue), Convert.ToInt32(ddl1.SelectedValue), lblGrp.Text, lblBU.Text, lblProductCategory.Text, lblPL.Text, lblProductGroup.Text, pdb)
        ElseIf pdb.ToUpper() = "ZM" Then
            sapCls.initGlobals(pdb)   'send country code
            addItem(oCompanyZM, txtSimpleCode.Text.Trim(), txtDesc1.Text.Trim(), Convert.ToInt32(ddlMfr.SelectedValue), Convert.ToInt32(ddl1.SelectedValue), lblGrp.Text, lblBU.Text, lblProductCategory.Text, lblPL.Text, lblProductGroup.Text, pdb)
        ElseIf pdb.ToUpper() = "MA" Then
            sapCls.initGlobals(pdb)   'send country code
            addItem(oCompanyMA, txtSimpleCode.Text.Trim(), txtDesc1.Text.Trim(), Convert.ToInt32(ddlMfr.SelectedValue), Convert.ToInt32(ddl1.SelectedValue), lblGrp.Text, lblBU.Text, lblProductCategory.Text, lblPL.Text, lblProductGroup.Text, pdb)
        ElseIf pdb.ToUpper() = "TRI" Then
            sapCls.initGlobals(pdb)   'send country code
            addItem(oCompanyTRI, txtSimpleCode.Text.Trim(), txtDesc1.Text.Trim(), Convert.ToInt32(ddlMfr.SelectedValue), Convert.ToInt32(ddl1.SelectedValue), lblGrp.Text, lblBU.Text, lblProductCategory.Text, lblPL.Text, lblProductGroup.Text, pdb)
        End If


        If flgStatus = True Then
            lblError.Text = lblError.Text + pdb + " , ..."
        End If

    End Sub

    Private Sub addItem(ByRef oCompany As SAPbobsCOM.Company, ByVal itmCode As String, ByVal itmDesc As String, ByVal mfrId As Integer, ByVal itmGrpId As Integer, ByVal itmGrpCode As String, ByVal itmBU As String, ByVal itmProductCategory As String, ByVal itmPL As String, ByVal itmProductGrp As String, ByVal dbCode As String)

        If flgStatus = False Then
            Exit Sub
        End If

        debugModule = "addItems"
        Dim oItem As SAPbobsCOM.Items
        Dim nErr As Long
        Dim errMsg As String

        errMsg = ""

        'addUDFValueNEW("OITM", 0, "Category", "Category")  ''replace values for specific udf field as per id
        'addUDFValueNEW("OITM", 1, "ProdLine", "ProdLine")
        'addUDFValueNEW("OITM", 2, "DashboardCategory", "DashboardCategory")
        'addUDFValueNEW("OITM", 3, "Model", "Model")
        'addUDFValueNEW("OITM", 4, "BU", "BU")

        '''''------------Add this just first time to test

        'addUDFValueNEW("OITM", 0, "Hardware", "Hardware")   'Category 
        'addUDFValueNEW("OITM", 0, "Software", "Software")   'Category

        'addUDFValueNEW("OITM", 1, "Dell-Mouse", "Dell-Mouse")           ' ProdLine
        'addUDFValueNEW("OITM", 1, "Dell-Monitor", "Dell-Monitor")       ' ProdLine

        'addUDFValueNEW("OITM", 2, "Dell Monitors", "Dell Monitors")     'DashboardCategory

        'addUDFValueNEW("OITM", 3, "USB-Mouse", "USB-Mouse")             'Models
        'addUDFValueNEW("OITM", 3, "LED-21", "LED-21")                   'Models
        '''''''''''-----------------

        Try
            oItem = oCompany.GetBusinessObject(SAPbobsCOM.BoObjectTypes.oItems)
            oItem.ItemCode = itmCode
            oItem.ItemName = itmDesc
            oItem.ItemsGroupCode = itmGrpId.ToString() 'sends group id

            oItem.UserFields.Fields.Item("FirmCode").Value = mfrId.ToString()  '' this is group code
            oItem.UserFields.Fields.Item("U_BU").Value = itmGrpCode  '' this is group code
            oItem.UserFields.Fields.Item("U_Model").Value = "NA"

            oItem.UserFields.Fields.Item("U_Category").Value = itmProductCategory   ''this is product category
            oItem.UserFields.Fields.Item("U_ProdLine").Value = itmPL                ''this is product Line
            oItem.UserFields.Fields.Item("U_DashboardCategory").Value = itmProductGrp ''this is product Group

            Dim Lenght, Width, Height, Weight As Double
            Lenght = Convert.ToDouble(txtLength.Text) / 100
            Width = Convert.ToDouble(txtWidth.Text) / 100
            Height = Convert.ToDouble(txtHeight.Text) / 100
            ''Volume = Lenght * Width * Height

            oItem.PurchaseUnitLength = Convert.ToDouble(txtLength.Text) / 100 ''length
            oItem.PurchaseUnitWidth = Convert.ToDouble(txtWidth.Text) / 100  ''Width
            oItem.PurchaseUnitHeight = Convert.ToDouble(txtHeight.Text) / 100  ''Height

            '' oItem.PurchaseUnitVolume = Volume
            Weight = 0
            If txtWeight.Text.Trim() <> "" Then
                Weight = txtWeight.Text
            End If

            oItem.PurchaseUnitWeight = (Weight * 1000) ''Weight

            'oItem.SalesVATGroup = sapCls.VATOUT
            oItem.GLMethod = SAPbobsCOM.BoGLMethods.glm_ItemClass
            oItem.DefaultWarehouse = sapCls.MASTER_WAREHOUSE

            If (0 <> oItem.Add()) Then
                'Check Error
                Call oCompany.GetLastError(nErr, errMsg)
                If (0 <> nErr) Then
                    If -10 <> nErr Then
                        flgStatus = False
                        lblError.Text = lblError.Text + " Erro : DB '" + dbCode + "' Failed to add item:[" & txtSimpleCode.Text & "]" & Str(nErr) & "," & errMsg
                        'MsgBox("Erro : DB '" + dbCode + "' Failed to add item:[" & txtSimpleCode.Text & "]" & Str(nErr) & "," & errMsg)
                    End If
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
                ElseIf dbCode = "ZM" Then
                    createFlgZM = 1
                ElseIf dbCode = "MA" Then
                    createFlgMA = 1
                ElseIf dbCode = "TRI" Then
                    createFlgTRI = 1
                End If
            End If

        Catch ex As Exception
            flgStatus = False
            lblError.Text = lblError.Text + " Failed to add item:[" & txtSimpleCode.Text & "] " & ex.Message
            'MsgBox("Failed to add item:[" & txtSimpleCode.Text & "] " & ex.Message)
        End Try
    End Sub

    Private Sub splitBu()
        If ddl1.SelectedIndex >= 0 Then
            Dim pstr As String
            Dim i, j As Integer
            'pstr = "DELL CONSUMER [DCOM]"   'testing ok
            pstr = ddl1.SelectedItem.Text
            i = pstr.IndexOf("[")
            j = pstr.IndexOf("]") - 1
            lblBU.Text = pstr.Substring(0, i - 1).Trim()
            lblGrp.Text = pstr.Substring(i + 1, j - i).Trim()
        End If
    End Sub
    Private Sub splitPL()
        If ddlPL.SelectedIndex >= 0 Then
            Dim pstr As String
            Dim i, j As Integer
            'pstr = "DELL CONSUMER [DCOM]"   'testing ok
            pstr = ddlPL.SelectedItem.Text
            i = pstr.IndexOf("[")
            j = pstr.IndexOf("]") - 1
            lblPL.Text = pstr.Substring(0, i - 1).Trim()
            lblPLDesc.Text = pstr.Substring(i + 1, j - i).Trim()
        End If
    End Sub

    Protected Sub radNewProductCategory_CheckedChanged(ByVal sender As Object, ByVal e As System.EventArgs) Handles radNewProductCategory.CheckedChanged
        callNewProc(radNewProductCategory.Checked, "PCAT")
    End Sub

    Protected Sub radNewProductGroup_CheckedChanged(ByVal sender As Object, ByVal e As System.EventArgs) Handles radNewProductGroup.CheckedChanged
        callNewProc(radNewProductGroup.Checked, "PGRP")
    End Sub
    'Protected Sub radNewMdl_CheckedChanged(ByVal sender As Object, ByVal e As System.EventArgs) Handles radNewMdl.CheckedChanged
    '    callNewProc(radNewMdl.Checked, "MODEL")
    'End Sub

    Protected Sub radNewPL_CheckedChanged(ByVal sender As Object, ByVal e As System.EventArgs) Handles radNewPL.CheckedChanged
        callNewProc(radNewPL.Checked, "PL")
    End Sub

    'Protected Sub radNewDcat_CheckedChanged(ByVal sender As Object, ByVal e As System.EventArgs) Handles radNewDcat.CheckedChanged
    '    callNewProc(radNewDcat.Checked, "DCAT")
    'End Sub

    Private Sub callNewProc(ByVal tf As Boolean, ByVal wrk As String)
        lblError.Text = ""

        If tf = True Then
            PanelNewCreation.Visible = True
            txtNewCode.Text = ""
            txtnewDesc.Text = ""
            lblAddNewFor.Text = wrk
        Else
            PanelNewCreation.Visible = False
        End If

    End Sub

    Protected Sub btnSaveNew_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles btnSaveNew.Click
        lblError.Text = ""

        If txtNewCode.Text.Trim() = "" Then
            lblError.Text = "Erro ! New Value Field left blank!!! provided value for it, and retry"
            Exit Sub
        End If

        If txtNewCode.Text.IndexOf("'") >= 0 Then
            lblError.Text = "Invalid Character occurs ' in New Value field , Char( ' ) not supported."
            Exit Sub
        End If

        If txtnewDesc.Text.Trim() = "" Then
            lblError.Text = "Erro ! New Description Field left blank!!! provided value for it, and retry"
            Exit Sub
        End If

        If txtnewDesc.Text.IndexOf("'") >= 0 Then
            lblError.Text = "Invalid Character occurs ' in New Description field , Char( ' ) not supported."
            Exit Sub
        End If

        Dim tmpstr As String
        tmpstr = ""



        If lblAddNewFor.Text = "PCAT" Then
            tmpstr = "insert into tejSap.dbo.tblSapProductCategory(prodCategory,descrip,fk_buId) values('" + txtNewCode.Text.Trim() + "','" + txtnewDesc.Text.Trim() + "'," + ddl1.SelectedValue.ToString() + ")"
            Db.constr = myGlobal.getConnectionStringForSapDBs("TEJSAP")
            Db.myExecuteSQL(tmpstr)
            loadProductCategory()
        ElseIf lblAddNewFor.Text = "PGRP" Then
            tmpstr = "insert into tejSap.dbo.tblSapProductGroup(prodGroup,descrip,fk_buId) values('" + txtNewCode.Text.Trim() + "','" + txtnewDesc.Text.Trim() + "'," + ddl1.SelectedValue.ToString() + ")"
            Db.constr = myGlobal.getConnectionStringForSapDBs("TEJSAP")
            Db.myExecuteSQL(tmpstr)
            loadProductGroup()
        ElseIf lblAddNewFor.Text = "PL" Then
            tmpstr = "insert into tblSapProductLine(plCode,descrip,fk_pCatId) values('" + txtNewCode.Text.Trim() + "','" + txtnewDesc.Text.Trim() + "'," + ddlProductCategory.SelectedValue.ToString() + ")"
            Db.constr = myGlobal.getConnectionStringForSapDBs("TEJSAP")
            Db.myExecuteSQL(tmpstr)
            loadPL()
        Else
            tmpstr = ""
        End If

        'If lblAddNewFor.Text = "MODEL" Then
        '    tmpstr = "insert into tejSap.dbo.tblModel(model,descrip,fk_buId) values('" + txtNewCode.Text.Trim() + "','" + txtnewDesc.Text.Trim() + "'," + ddl1.SelectedValue.ToString() + ")"
        '    Db.constr = myGlobal.getConnectionStringForSapDBs("TEJSAP")
        '    Db.myExecuteSQL(tmpstr)
        '    loadMdl()
        'ElseIf lblAddNewFor.Text = "PL" Then
        '    tmpstr = "insert into tblProductLine(plCode,descrip,fk_mid) values('" + txtNewCode.Text.Trim() + "','" + txtnewDesc.Text.Trim() + "'," + ddlMdl.SelectedValue.ToString() + ")"
        '    Db.constr = myGlobal.getConnectionStringForSapDBs("TEJSAP")
        '    Db.myExecuteSQL(tmpstr)
        '    loadPL()
        'ElseIf lblAddNewFor.Text = "DCAT" Then
        '    tmpstr = "insert into tejSap.dbo.tblDCategory(dCategory,descrip) values('" + txtNewCode.Text.Trim() + "','" + txtnewDesc.Text.Trim() + "')"
        '    Db.constr = myGlobal.getConnectionStringForSapDBs("TEJSAP")
        '    Db.myExecuteSQL(tmpstr)
        '    loadDashCategory()
        'Else
        '    tmpstr = ""
        'End If

        lblError.Text = lblAddNewFor.Text + " updated to database as per selected filters."
        PanelNewCreation.Visible = False
        uncheck()
    End Sub

    Protected Sub btnCancel_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles btnCancel.Click
        PanelNewCreation.Visible = False
        uncheck()
    End Sub

    Private Sub uncheck()
        radNewProductCategory.Checked = False
        radNewPL.Checked = False
        radNewProductGroup.Checked = False
    End Sub

    Protected Sub ddlMfr_SelectedIndexChanged(ByVal sender As Object, ByVal e As System.EventArgs) Handles ddlMfr.SelectedIndexChanged
        loadBu()
        loadProductCategory()
        loadProductGroup()

        loadPL() 'based on poduct category

    End Sub
    Protected Sub ddl1_SelectedIndexChanged(ByVal sender As Object, ByVal e As System.EventArgs) Handles ddl1.SelectedIndexChanged
        'HP IPG [H003]
        Call splitBu()
        loadProductCategory()
        loadProductGroup()

        loadPL() 'based on poduct category
    End Sub

    'Protected Sub ddlMdl_SelectedIndexChanged(ByVal sender As Object, ByVal e As System.EventArgs) Handles ddlMdl.SelectedIndexChanged
    '    loadPL()
    'End Sub
    Protected Sub ddlProductCategory_SelectedIndexChanged(ByVal sender As Object, ByVal e As System.EventArgs) Handles ddlProductCategory.SelectedIndexChanged
        loadPL()
    End Sub
    Protected Sub ddlPL_SelectedIndexChanged(ByVal sender As Object, ByVal e As System.EventArgs) Handles ddlPL.SelectedIndexChanged
        Call splitPL()
    End Sub

   
End Class