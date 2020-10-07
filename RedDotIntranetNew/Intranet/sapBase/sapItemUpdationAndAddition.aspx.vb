Imports System.Data.SqlClient
Imports System.Data
Partial Class Intranet_sapBase_sapItemUpdationAndAddition
    Inherits System.Web.UI.Page

    Dim createFlgAE, createFlgUG, createFlgTZ, createFlgKE, cntUpdated, cntNotUpdated As Integer
    Dim conFlg, flgStatus As Boolean
    Dim constr, updQry, pDBNow As String
    Public Shared oCompanyWork As SAPbobsCOM.Company
    Public Shared oCmpSrv As SAPbobsCOM.CompanyService
    'Public Shared oIPS As SAPbobsCOM.InventoryPostingsService
    Public Shared oSBObob As SAPbobsCOM.SBObob
    Public Shared debugModule As String
    Dim dst, dsItmList As DataSet
    Dim oUpdItem As SAPbobsCOM.Items


    Protected Sub Page_Load(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.Load
        cmdConnectDb.Attributes.Add("onClick", "return getConfirmation();")
        '''''cmdConnectDb.Attributes.Add("onClick", "return waitMsg();")

        lblError.Text = ""

        If (IsPostBack <> True) Then
        End If
    End Sub
    Private Sub UpdateStatusToItemListbl(ByVal pitmCod As String, ByVal pitmGrp As String, ByVal dbName As String, ByVal UpdateType As String)

        If dbName.IndexOf("-TEST", 0) >= 0 Then
            dbName = dbName.Replace("-TEST", "")
        End If

        If updQry = "" Then
            updQry = "update [dbo].[tblForItemCategroyUpdation] set Status" + dbName + "=1,updationComment=isnull(updationComment,'') + '" + dbName + " : " + UpdateType + " ; ' where ItmCode='" + pitmCod + "' and Grp='" + pitmGrp + "'"
        Else
            updQry = updQry + " ; update [dbo].[tblForItemCategroyUpdation] set Status" + dbName + "=1,updationComment=isnull(updationComment,'') + ' " + dbName + " : " + UpdateType + " ; ' where ItmCode='" + pitmCod + "' and Grp='" + pitmGrp + "'"
        End If

        If updQry.Length() > 10000 Then
            Db.constr = myGlobal.getConnectionStringForSapDBs("TejSap")
            Db.myExecuteSQL(updQry)
            updQry = ""
        End If
    End Sub
    Private Sub fetchItemListToUpdate()
        Db.constr = myGlobal.getConnectionStringForSapDBs("TejSap")
        dsItmList = Db.myGetDS("select * from  tejSap.[dbo].[updatableItemList]")  ' calling a view in trjsap
        'lblLstCnt.Text = ddl1.Items.Count.ToString()
    End Sub

    Protected Sub cmdConnectDb_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles cmdConnectDb.Click

        txtItemsUpdated.Text = ""
        txtItemsNotUpdated.Text = ""

        If txtTargetDb.Text.Trim() = "" Then
            lblMsg.Text = "Error ! Please supply a valid SAP database Name "
            Exit Sub
        End If

        cmdConnectDb.Enabled = False
        conFlg = True

        lblMsg.Text = "Please wait while connecting.............."

        If txtTargetDb.Text.ToUpper() = "SAPAE" Then
            connectSAPDB(oCompanyWork, myGlobal.getCredsForSapCompany("SAPCompanyConnectCredsAE"), "AE")
            pDBNow = "AE"
        ElseIf txtTargetDb.Text.ToUpper() = "SAPKE" Then
            connectSAPDB(oCompanyWork, myGlobal.getCredsForSapCompany("SAPCompanyConnectCredsKE"), "KE")
            pDBNow = "KE"
        ElseIf txtTargetDb.Text.ToUpper() = "SAPTZ" Then
            connectSAPDB(oCompanyWork, myGlobal.getCredsForSapCompany("SAPCompanyConnectCredsTZ"), "TZ")
            pDBNow = "TZ"
        ElseIf txtTargetDb.Text.ToUpper() = "SAPUG" Then
            connectSAPDB(oCompanyWork, myGlobal.getCredsForSapCompany("SAPCompanyConnectCredsUG"), "UG")
            pDBNow = "UG"

        ElseIf txtTargetDb.Text.ToUpper() = "SAPAE-TEST" Then
            connectSAPDB(oCompanyWork, myGlobal.getCredsForSapCompany("SAPCompanyConnectCredsSAPAE-TEST"), "SAPAE-TEST")
            pDBNow = "AE"
        ElseIf txtTargetDb.Text.ToUpper() = "SAPKE-TEST" Then
            connectSAPDB(oCompanyWork, myGlobal.getCredsForSapCompany("SAPCompanyConnectCredsSAPKE-TEST"), "SAPKE-TEST")
            pDBNow = "KE"
        ElseIf txtTargetDb.Text.ToUpper() = "SAPTZ-TEST" Then
            connectSAPDB(oCompanyWork, myGlobal.getCredsForSapCompany("SAPCompanyConnectCredsSAPTZ-TEST"), "SAPTZ-TEST")
            pDBNow = "TZ"
        ElseIf txtTargetDb.Text.ToUpper() = "SAPUG-TEST" Then
            connectSAPDB(oCompanyWork, myGlobal.getCredsForSapCompany("SAPCompanyConnectCredsSAPUG-TEST"), "SAPUG-TEST")
            pDBNow = "UG"
        Else
            lblMsg.Text = "Error ! Seems like you supplied invalid sap database name : '" + txtTargetDb.Text + "'"
            cmdConnectDb.Enabled = True
            Exit Sub
        End If

        If conFlg = True Then
            lblMsg.Text = "SAP Company on database " + txtTargetDb.Text.Trim() + " Connected. "

            cmdConnectDb.Enabled = False
            fetchItemListToUpdate()
            updQry = ""    'clear update query

            If dsItmList.Tables(0).Rows.Count > 0 Then
                cntUpdated = 0
                cntNotUpdated = 0

                lblMsg.Text = lblMsg.Text + "...Updation began..@" + DateAndTime.Now().ToString() + ".."
                'work here

                sapCls.initGlobals(pDBNow)   'send country code , once before the loop starts for this database update

                Dim itm As Integer
                For itm = 0 To dsItmList.Tables(0).Rows.Count - 1
                    UpdateItems(oCompanyWork, dsItmList.Tables(0).Rows(itm)("ItmCode").ToString(), dsItmList.Tables(0).Rows(itm)("ItmDesc").ToString(), Convert.ToInt32(dsItmList.Tables(0).Rows(itm)("MfrId")), Convert.ToInt32(dsItmList.Tables(0).Rows(itm)("BUId")), dsItmList.Tables(0).Rows(itm)("Grp").ToString(), dsItmList.Tables(0).Rows(itm)("ProductCategory").ToString(), dsItmList.Tables(0).Rows(itm)("PL").ToString(), dsItmList.Tables(0).Rows(itm)("ProductGroup").ToString(), txtTargetDb.Text.ToUpper())
                Next

                If updQry <> "" Then '''''execute the left ones soon as going out from loop
                    Db.constr = myGlobal.getConnectionStringForSapDBs("TejSap")
                    Db.myExecuteSQL(updQry)
                    updQry = ""
                End If

                lblMsg.Text = lblMsg.Text + "...Finished successfullt. @" + DateAndTime.Now().ToString()

            Else
                lblError.Text = "No, Items found in updatatable source to be updated into database : '" + txtTargetDb.Text + "'"
                lblMsg.Text = lblMsg.Text + "...Update canceled as no recs found in source table to be updated..@" + DateAndTime.Now().ToString()
                cmdConnectDb.Enabled = True
                Exit Sub
            End If

        Else
            '' //reload / reconect page
            cmdConnectDb.Enabled = True
            MsgBox("Issue : One of the companies failed to connect, Please try to reconnect, or contact system admin")
            Exit Sub
        End If

        cmdConnectDb.Enabled = True


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

            'If pDB.ToUpper() = "AE" Then
            '    chkAE.Checked = True
            '    ' oCompanyAE = oCompany
            'ElseIf pDB.ToUpper() = "UG" Then
            '    chkUG.Checked = True
            '    ' oCompanyUG = oCompany
            'ElseIf pDB.ToUpper() = "TZ" Then
            '    chkTZ.Checked = True
            '    ' oCompanyTZ = oCompany
            'ElseIf pDB.ToUpper() = "KE" Then
            '    chkKE.Checked = True
            '    ' oCompanyKE = oCompany
            'End If

        Catch ex As Exception
            conFlg = False
            'MsgBox(ex.Message)
            lblMsg.Text = "Error ! " + pDB + " - " + ex.Message
        End Try
    End Sub


    Private Sub UpdateItems(ByRef oCompany As SAPbobsCOM.Company, ByVal itmCode As String, ByVal itmDesc As String, ByVal mfrId As Integer, ByVal itmGrpId As Integer, ByVal itmGrpCode As String, ByVal itmPCat As String, ByVal itmPL As String, ByVal itmPGrp As String, ByVal dbCode As String)
        'Dim oItem As SAPbobsCOM.Items
        Dim nErr As Long
        Dim errMsg As String

        errMsg = ""

        Try

            oUpdItem = oCompany.GetBusinessObject(SAPbobsCOM.BoObjectTypes.oItems)

            If oUpdItem.GetByKey(itmCode) Then
                ''''oUpdItem.ItemCode = itmCode

                If CheckUpdateDesc.Checked = True And itmDesc <> "" Then
                    oUpdItem.ItemName = itmDesc
                End If

                oUpdItem.UserFields.Fields.Item("FirmCode").Value = mfrId ''.ToString()  '' this is group code
                oUpdItem.ItemsGroupCode = itmGrpId ''.ToString() 'sends group id
                oUpdItem.UserFields.Fields.Item("U_BU").Value = itmGrpCode  '' this is group code

                oUpdItem.UserFields.Fields.Item("U_Model").Value = "NA"

                oUpdItem.UserFields.Fields.Item("U_Category").Value = itmPCat
                oUpdItem.UserFields.Fields.Item("U_ProdLine").Value = itmPL
                oUpdItem.UserFields.Fields.Item("U_DashboardCategory").Value = itmPGrp
                oUpdItem.User_Text = "Updated : " & Now.Date.ToString("MM-dd-yyyy")
                oUpdItem.Update()  ''updates to db

                UpdateStatusToItemListbl(itmCode, itmGrpCode, dbCode, "Edited") ''''update temp item list table for the update
                txtItemsUpdated.Text = txtItemsUpdated.Text + itmCode + ";"
                cntUpdated = cntUpdated + 1
                lblUpdatedCnt.Text = cntUpdated.ToString()

            Else 'not found case

                '''''''''''''''''''''Add items those were not found-----------
                'addItemsMissing(oCompanyAE, txtSimpleCode.Text.Trim(), txtDesc1.Text.Trim(), Convert.ToInt32(ddlMfr.SelectedValue), Convert.ToInt32(ddl1.SelectedValue), lblGrp.Text, lblBU.Text, lblProductCategory.Text, lblPL.Text, lblProductGroup.Text, pdb)

                oUpdItem = oCompany.GetBusinessObject(SAPbobsCOM.BoObjectTypes.oItems)
                oUpdItem.ItemCode = itmCode
                oUpdItem.ItemName = itmDesc
                oUpdItem.ItemsGroupCode = itmGrpId 'sends group id
                oUpdItem.UserFields.Fields.Item("FirmCode").Value = mfrId  '' this is group code
                oUpdItem.UserFields.Fields.Item("U_BU").Value = itmGrpCode  '' this is group code
                oUpdItem.UserFields.Fields.Item("U_Model").Value = "NA"

                oUpdItem.UserFields.Fields.Item("U_Category").Value = itmPCat   ''this is product category
                oUpdItem.UserFields.Fields.Item("U_ProdLine").Value = itmPL                ''this is product Line
                oUpdItem.UserFields.Fields.Item("U_DashboardCategory").Value = itmPGrp ''this is product Group
                oUpdItem.User_Text = "Added : " & Now.Date.ToString("MM-dd-yyyy")

                oUpdItem.SalesVATGroup = sapCls.VATOUT
                oUpdItem.GLMethod = SAPbobsCOM.BoGLMethods.glm_ItemClass
                oUpdItem.DefaultWarehouse = sapCls.MASTER_WAREHOUSE

                If (0 <> oUpdItem.Add()) Then
                    'Check Error
                    Call oCompany.GetLastError(nErr, errMsg)
                    If (0 <> nErr) Then
                        If -10 <> nErr Then
                            'MsgBox("Erro : DB '" + dbCode + "' Failed to add item:[" & itmDesc & "]" & Str(nErr) & "," & errMsg)
                        End If
                    End If
                Else
                    UpdateStatusToItemListbl(itmCode, itmGrpCode, dbCode, "Added") ''''update temp item list table for the update
                    cntNotUpdated = cntNotUpdated + 1
                    txtItemsNotUpdated.Text = txtItemsNotUpdated.Text + itmCode + ";"
                    lblNotUpdatedCnt.Text = cntNotUpdated.ToString()
                End If
                '''''''''''''''''''''code Add items those were not found end-----------
            End If

        Catch ex As Exception
            flgStatus = False
            MsgBox("Failed to add item:[" & itmCode & "] " & ex.Message)
        End Try
    End Sub


    'Private Sub addItem(ByRef oCompany As SAPbobsCOM.Company, ByVal itmCode As String, ByVal itmDesc As String, ByVal mfrId As Integer, ByVal itmGrpId As Integer, ByVal itmGrpCode As String, ByVal itmBU As String, ByVal itmCatg As String, ByVal itmPL As String, ByVal itmModel As String, ByVal itmdashCatg As String, ByVal dbCode As String)

    'End Sub

     Private Sub addItemsMissing(ByRef oCompany As SAPbobsCOM.Company, ByVal itmCode As String, ByVal itmDesc As String, ByVal mfrId As Integer, ByVal itmGrpId As Integer, ByVal itmGrpCode As String, ByVal itmBU As String, ByVal itmProductCategory As String, ByVal itmPL As String, ByVal itmProductGrp As String, ByVal dbCode As String)

        If flgStatus = False Then
            Exit Sub
        End If

        debugModule = "addItems"
        Dim oItem As SAPbobsCOM.Items
        Dim nErr As Long
        Dim errMsg As String

        errMsg = ""

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

            oItem.SalesVATGroup = sapCls.VATOUT
            oItem.GLMethod = SAPbobsCOM.BoGLMethods.glm_ItemClass
            oItem.DefaultWarehouse = sapCls.MASTER_WAREHOUSE

            If (0 <> oItem.Add()) Then
                'Check Error
                Call oCompany.GetLastError(nErr, errMsg)
                If (0 <> nErr) Then
                    If -10 <> nErr Then
                        MsgBox("Erro : DB '" + dbCode + "' Failed to add item:[" & itmDesc & "]" & Str(nErr) & "," & errMsg)
                    End If
                End If
            Else
                'lblError.Text += " success "
            End If

        Catch ex As Exception
            flgStatus = False
            MsgBox("Failed to add item:[" & itmDesc & "] " & ex.Message)
        End Try
    End Sub
End Class