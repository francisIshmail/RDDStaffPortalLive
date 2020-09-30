Imports System.Data.SqlClient
Imports System.Data

Partial Class Intranet_EVO_EditStockItemVB
    Inherits System.Web.UI.Page

    Public constr As String
    Public DBCode As String
    Public query As String
    Public prefix As String
    Public constrEVO As String
    Public constrOB1 As String
    Public con As SqlConnection
    Public conEVO As SqlConnection
    Public conOB1 As SqlConnection
    Public ping As String
    Dim whereclaues As String
    Dim partInTZ, partInTRI, partInKE, partInEPZ, partInUG As Integer
    Dim category, manufacture, BU, PL, model, part, dashCategory, group, desc1, desc2, desc3, avgCost, grvcost As String
    Dim active As Boolean

    Protected Sub Page_Load(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.Load
        btnUpdate.Attributes.Add("onClick", "return getConfirmation();")
        lblError.Text = ""
        lblSuccess.Text = ""
        constr = myGlobal.getConnectionStringForDB(ddlDB.SelectedValue.ToString())
        If (IsPostBack <> True) Then
            pnlItem.Enabled = False
            pnlEditItem.Enabled = False
            LoadCharacters()
        End If
    End Sub

    Protected Sub btnConnect_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles btnConnect.Click
        Try
            filldashboardcatagory()
            fillcomboCategory()
        Catch ex As Exception
            lblError.Text = "Warning : while loading dashboard category there was some issue, make sure you select the dashboard category from the list. Continue anyways"
        End Try

        Try
            LoadItem()
        Catch ex As Exception
            lblError.Text = "Can't load Web page" & vbCrLf & ex.Message
            'MsgBox("Can't load Web page" & vbCrLf & ex.Message)
        End Try
    End Sub

    Private Sub LoadCharacters()
        ddlCodeFilter.Items.Add("0")
        ddlCodeFilter.Items.Add("1")
        ddlCodeFilter.Items.Add("2")
        ddlCodeFilter.Items.Add("3")
        ddlCodeFilter.Items.Add("4")
        ddlCodeFilter.Items.Add("5")
        ddlCodeFilter.Items.Add("6")
        ddlCodeFilter.Items.Add("7")
        ddlCodeFilter.Items.Add("8")
        ddlCodeFilter.Items.Add("9")
        ddlCodeFilter.Items.Add("A")
        ddlCodeFilter.Items.Add("B")
        ddlCodeFilter.Items.Add("C")
        ddlCodeFilter.Items.Add("D")
        ddlCodeFilter.Items.Add("E")
        ddlCodeFilter.Items.Add("F")
        ddlCodeFilter.Items.Add("G")
        ddlCodeFilter.Items.Add("H")
        ddlCodeFilter.Items.Add("I")
        ddlCodeFilter.Items.Add("J")
        ddlCodeFilter.Items.Add("K")
        ddlCodeFilter.Items.Add("L")
        ddlCodeFilter.Items.Add("M")
        ddlCodeFilter.Items.Add("N")
        ddlCodeFilter.Items.Add("O")
        ddlCodeFilter.Items.Add("P")
        ddlCodeFilter.Items.Add("Q")
        ddlCodeFilter.Items.Add("R")
        ddlCodeFilter.Items.Add("S")
        ddlCodeFilter.Items.Add("T")
        ddlCodeFilter.Items.Add("U")
        ddlCodeFilter.Items.Add("V")
        ddlCodeFilter.Items.Add("W")
        ddlCodeFilter.Items.Add("X")
        ddlCodeFilter.Items.Add("Y")
        ddlCodeFilter.Items.Add("Z")
        ddlCodeFilter.Items.Add("Others")
    End Sub

    Protected Sub ddlSimpleCode_SelectedIndexChanged(ByVal sender As Object, ByVal e As System.EventArgs) Handles ddlSimpleCode.SelectedIndexChanged

        chkSeg2.Enabled = True
        chkSeg2.Checked = False
        chkSeg3.Enabled = False
        chkSeg3.Checked = False
        chkSeg4.Enabled = False
        chkSeg4.Checked = False
        chkSeg5.Enabled = False
        chkSeg5.Checked = False
        chkSeg6.Enabled = False
        chkSeg6.Checked = False
        chkNewModel.Enabled = False
        chkNewPL.Enabled = False
        cmbBUTRI.Enabled = False
        cmbModelTRI.Enabled = False
        cmbPLTRI.Enabled = False


        If (Not getItemExistence()) Then
            Exit Sub
        End If

        Try
            FillDetails()
        Catch ex As Exception
            lblError.Text = "Warning : Few Fields could not be set according to database values , may be for inconsistent data in the database"
        End Try

        Try
            setCombos()
        Catch ex As Exception
            lblError.Text = "Warning : Dropdowns could not be set according to database values , may be for inconsistent data in the database"
        End Try

    End Sub

    Private Sub LoadItem()
        whereclaues = ""

        If ddlCodeFilter.SelectedItem.Text = "Others" Then
            whereclaues = " WHERE lower(code) not like lower('0%') and lower(code) not like lower('1%') and lower(code) not like lower('2%') and lower(code) not like lower('3%') and lower(code) not like lower('4%') and lower(code) not like lower('5%') and lower(code) not like lower('6%') and lower(code) not like lower('7%') and lower(code) not like lower('8%') and lower(code) not like lower('9%') and lower(code) not like lower('A%') and lower(code) not like lower('B%') and lower(code) not like lower('C%') and lower(code) not like lower('D%') and lower(code) not like lower('E%') and lower(code) not like lower('F%') and lower(code) not like lower('G%') and lower(code) not like lower('H%') and lower(code) not like lower('I%') and lower(code) not like lower('J%') and lower(code) not like lower('K%') and lower(code) not like lower('L%') and lower(code) not like lower('M%') and lower(code) not like lower('N%') and lower(code) not like lower('O%') and lower(code) not like lower('P%') and lower(code) not like lower('Q%') and lower(code) not like lower('R%') and lower(code) not like lower('S%') and lower(code) not like lower('T%') and lower(code) not like lower('U%') and lower(code) not like lower('V%') and lower(code) not like lower('W%') and lower(code) not like lower('X%') and lower(code) not like lower('Y%') and lower(code) not like lower('Z%') "
        Else
            whereclaues = " WHERE lower(code) like lower('" + ddlCodeFilter.SelectedItem.Text + "%') "
        End If

        query = "Select stockLink,code,csimplecode,itemgroup,iInvSegValue1ID ,iInvSegValue2ID ,iInvSegValue3ID ,iInvSegValue4ID ,iInvSegValue5ID ,iInvSegValue6ID ,iInvSegValue7ID,ulIIdashboardCategory,description_1,description_2,description_3,cExtDescription,AveUCst ,LatUCst ,LowUCst ,HigUCst,StdUCst,fItemLastGRVCost,ItemActive,ucIICreatedBy,udIICreationDate from stkitem " + whereclaues + " order by code"
        'query = "select * from stkitem where code like '%galaxy%'"

        Db.constr = myGlobal.getConnectionStringForDB(ddlDB.SelectedValue.ToString())
        Session("TblItemsOfDB") = Db.myGetDS(query).Tables(0)

        Try
            ddlSimpleCode.DataSource = Session("TblItemsOfDB")
            ddlSimpleCode.DataTextField = "code"
            ddlSimpleCode.DataValueField = "code"
            ddlSimpleCode.DataBind()
            lblConMsg.Text = "Connected"
        Catch ex As Exception
            lblError.Text = "Error : Connection to database failed, kindly retry later"
            Exit Sub
        End Try


        'lblError.Visible = True


        If (ddlSimpleCode.Items.Count > 0) Then
            lblSimpleCodeCount.Text = "(" + ddlSimpleCode.Items.Count.ToString() + ") Items Found"
            If (Not getItemExistence()) Then
                Exit Sub
            End If

            Try
                FillDetails()
                btnConnect.Enabled = False
            Catch ex As Exception
                lblError.Text = "Warning : Few Fields could not be set according to database values , may be for inconsistent data in the database"
            End Try

            Try
                setCombos()
            Catch ex As Exception
                lblError.Text = "Warning : Dropdowns could not be set according to database values , may be for inconsistent data in the database"
            End Try

            pnlItem.Enabled = True
        Else
            lblSimpleCodeCount.Text = ""
            lblSimpleCodeCount.Text = "(0) Items Found"
        End If

    End Sub

    Protected Sub ddlDB_SelectedIndexChanged(ByVal sender As Object, ByVal e As System.EventArgs) Handles ddlDB.SelectedIndexChanged
        ResetAll()
    End Sub

    Protected Sub ddlCodeFilter_SelectedIndexChanged(ByVal sender As Object, ByVal e As System.EventArgs) Handles ddlCodeFilter.SelectedIndexChanged
        ResetAll()
    End Sub

    Public Function getItemExistenceOnUpdate() As Boolean 'work here
        Dim tstr As String
        lblError.Text = "Verifying Existence of new part no. in all dbs"

        Try
            Dim drd1 As SqlDataReader
            'tstr = "declare @tz int; declare @tri int; set @tz=0; set @tri=0; select top 1 @tz=stockLink  from [RedDotTanzania].[dbo].stkitem where (lower(Code)<>lower('" & lblCode.Text & "')) and ((lower(Code) like lower('" & lblUniqueDesc.Text & "%')) or (lower('" & lblUniqueDesc.Text & "') like lower(Code+'%'))) ; select top 1 @tri=stockLink from [Triangle].[dbo].stkitem where (lower(Code)<>lower('" & lblCode.Text & "')) and ((lower(Code) like lower('" & lblUniqueDesc.Text & "%')) or (lower('" & lblUniqueDesc.Text & "') like lower(Code+'%'))) ;select @tz as tz,@tri as tri ;"
            tstr = "declare @tz int; declare @tri int; set @tz=0; set @tri=0; select top 1 @tz=stockLink  from [RedDotTanzania].[dbo].stkitem where (lower(Code)<>lower('" & lblCode.Text & "')) and ((lower(Code)=lower('" & lblUniqueDesc.Text & "'))) ; select top 1 @tri=stockLink from [Triangle].[dbo].stkitem where (lower(Code)<>lower('" & lblCode.Text & "')) and ((lower(Code)=lower('" & lblUniqueDesc.Text & "'))) ;select @tz as tz,@tri as tri ;"
            Db.constr = myGlobal.getConnectionStringForDB("EVO")
            drd1 = Db.myGetReader(tstr)

            drd1.Read()
            partInTZ = drd1("tz")
            partInTRI = drd1("tri")
            drd1.Close()

            '---------------for OB1----------------

            'tstr = "declare @ke int; declare @epz int;declare @ug int;set @ke=0;set @epz=0;set @ug=0;select top 1 @ke=stockLink from [Red Dot Distribution Limited - Kenya].[dbo].stkitem where (lower(Code)<>lower('" & lblCode.Text & "')) and ((lower(Code) like lower('" & lblUniqueDesc.Text & "%')) or (lower('" & lblUniqueDesc.Text & "') like lower(Code+'%'))) ; select top 1 @epz=stockLink  from [RED DOT DISTRIBUTION EPZ LTD].[dbo].stkitem where (lower(Code)<>lower('" & lblCode.Text & "')) and ((lower(Code) like lower('" & lblUniqueDesc.Text & "%')) or (lower('" & lblUniqueDesc.Text & "') like lower(Code+'%'))) ;  select top 1 @ug=stockLink  from [UgandaKE].[dbo].stkitem where (lower(Code)<>lower('" & lblCode.Text & "')) and ((lower(Code) like lower('" & lblUniqueDesc.Text & "%')) or (lower('" & lblUniqueDesc.Text & "') like lower(Code+'%'))) ;  select @ke as ke,@epz as epz,@ug as ug ;"
            tstr = "declare @ke int; declare @epz int;declare @ug int;set @ke=0;set @epz=0;set @ug=0;select top 1 @ke=stockLink from [Red Dot Distribution Limited - Kenya].[dbo].stkitem where (lower(Code)<>lower('" & lblCode.Text & "')) and ((lower(Code)=lower('" & lblUniqueDesc.Text & "'))) ; select top 1 @epz=stockLink  from [RED DOT DISTRIBUTION EPZ LTD].[dbo].stkitem where (lower(Code)<>lower('" & lblCode.Text & "')) and ((lower(Code)=lower('" & lblUniqueDesc.Text & "'))) ;  select top 1 @ug=stockLink  from [UgandaKE].[dbo].stkitem where (lower(Code)<>lower('" & lblCode.Text & "')) and ((lower(Code)=lower('" & lblUniqueDesc.Text & "'))) ;  select @ke as ke,@epz as epz,@ug as ug ;"
            Db.constr = myGlobal.getConnectionStringForDB("OB1")
            drd1 = Db.myGetReader(tstr)

            drd1.Read()
            partInKE = drd1("ke")
            partInEPZ = drd1("epz")
            partInUG = drd1("ug")
            drd1.Close()
        Catch ex As Exception
            lblError.Text = "Error : Newly formed unique code could not be verified for it's existence in all the databases, " + ex.Message + " Please retry."
            getItemExistenceOnUpdate = True  'as per the condition here we have to return true to stop here
            Exit Function
        End Try

        Dim flg As Boolean
        Dim msg As String
        flg = False
        msg = "Error : Can't Update current Stock Item because exactly the same Part No. '" & lblUniqueDesc.Text & "' is already present in the databases : "

        If partInTZ > 0 And CheckStatusTZ.Checked Then
            msg = msg & " TZ ,"
            flg = True
        End If

        If partInTRI > 0 And CheckStatusDU.Checked Then
            msg = msg & " TRI ,"
            flg = True
        End If

        If partInKE > 0 And CheckStatusKE.Checked Then
            msg = msg & " KE ,"
            flg = True
        End If

        If partInEPZ > 0 And CheckStatusEPZ.Checked Then
            msg = msg & " EPZ ,"
            flg = True
        End If

        If partInUG > 0 And CheckStatusUG.Checked Then
            msg = msg & " UG ,"
            flg = True
        End If

        msg = msg & " Please retry giving a different value in Simple Code field."

        If flg = True Then
            'MsgBox(msg)
            lblError.Text = msg
        End If

        lblError.Text = "verified."
        getItemExistenceOnUpdate = flg
    End Function

    Public Sub getItemExistenceold()  'work here

        lblError.Text = "Verifying Existence of part no. in all dbs"
        Dim tstr As String

        Dim drd1 As SqlDataReader
        tstr = "declare @tz int; declare @tri int; set @tz=0; set @tri=0; select top 1 @tz=stockLink  from [RedDotTanzania].[dbo].stkitem where (lower(Code)=lower('" & ddlSimpleCode.SelectedItem.Text & "')); select top 1 @tri=stockLink from [Triangle].[dbo].stkitem where (lower(Code)=lower('" & ddlSimpleCode.SelectedItem.Text & "'));select @tz as tz,@tri as tri ;"
        Db.constr = myGlobal.getConnectionStringForDB("EVO")
        drd1 = Db.myGetReader(tstr)

        drd1.Read()
        partInTZ = drd1("tz")
        partInTRI = drd1("tri")
        drd1.Close()

        '---------------for OB1----------------

        tstr = "declare @ke int; declare @epz int;declare @ug int;set @ke=0;set @epz=0;set @ug=0;select top 1 @ke=stockLink from [Red Dot Distribution Limited - Kenya].[dbo].stkitem where (lower(Code)=('" & ddlSimpleCode.SelectedItem.Text & "')); select top 1 @epz=stockLink  from [RED DOT DISTRIBUTION EPZ LTD].[dbo].stkitem where (lower(Code)=lower('" & ddlSimpleCode.SelectedItem.Text & "'));select top 1 @ug=stockLink  from [UgandaKE].[dbo].stkitem where (lower(Code)=lower('" & ddlSimpleCode.SelectedItem.Text & "'))select @ke as ke,@epz as epz,@ug as ug ;"
        Db.constr = myGlobal.getConnectionStringForDB("OB1")
        drd1 = Db.myGetReader(tstr)

        drd1.Read()
        partInKE = drd1("ke")
        partInEPZ = drd1("epz")
        partInUG = drd1("ug")
        drd1.Close()


        If partInTZ > 0 Then 'And CheckCreateInTZ.Checked Then
            CheckStatusTZ.Checked = True
            CheckStatusTZ.ForeColor = Drawing.Color.Green
        Else
            CheckStatusTZ.Checked = False
            CheckStatusTZ.ForeColor = Drawing.Color.Red
        End If

        If partInTRI > 0 Then 'And CheckCreateInDU.Checked Then
            CheckStatusDU.Checked = True
            CheckStatusDU.ForeColor = Drawing.Color.Green
        Else
            CheckStatusDU.Checked = False
            CheckStatusDU.ForeColor = Drawing.Color.Red
        End If

        If partInKE > 0 Then 'And CheckCreateInKE.Checked Then
            CheckStatusKE.Checked = True
            CheckStatusKE.ForeColor = Drawing.Color.Green
        Else
            CheckStatusKE.Checked = False
            CheckStatusKE.ForeColor = Drawing.Color.Red
        End If

        If partInEPZ > 0 Then 'And CheckCreateInEPZ.Checked Then
            CheckStatusEPZ.Checked = True
            CheckStatusEPZ.ForeColor = Drawing.Color.Green
        Else
            CheckStatusEPZ.Checked = False
            CheckStatusEPZ.ForeColor = Drawing.Color.Red
        End If

        If partInUG > 0 Then 'And CheckCreateInUG.Checked Then
            CheckStatusUG.Checked = True
            CheckStatusUG.ForeColor = Drawing.Color.Green
        Else
            CheckStatusUG.Checked = False
            CheckStatusUG.ForeColor = Drawing.Color.Red
        End If

        pnlEditItem.Enabled = True

        pnlEditItem.Enabled = True

        lblError.Text = "verified."
    End Sub

    Public Function getItemExistence() As Boolean  'work here
        lblError.Text = "Verifying Existence of part no. in all dbs"
        Dim tstr As String

        Try
            Dim drd1 As SqlDataReader
            tstr = "declare @tz int; declare @tri int; set @tz=0; set @tri=0; select top 1 @tz=stockLink  from [RedDotTanzania].[dbo].stkitem where (lower(Code)=lower('" & ddlSimpleCode.SelectedItem.Text & "')); select top 1 @tri=stockLink from [Triangle].[dbo].stkitem where (lower(Code)=lower('" & ddlSimpleCode.SelectedItem.Text & "'));select @tz as tz,@tri as tri ;"
            Db.constr = myGlobal.getConnectionStringForDB("EVO")
            drd1 = Db.myGetReader(tstr)

            drd1.Read()
            partInTZ = drd1("tz")
            partInTRI = drd1("tri")
            drd1.Close()

            '---------------for OB1----------------

            tstr = "declare @ke int; declare @epz int;declare @ug int;set @ke=0;set @epz=0;set @ug=0;select top 1 @ke=stockLink from [Red Dot Distribution Limited - Kenya].[dbo].stkitem where (lower(Code)=('" & ddlSimpleCode.SelectedItem.Text & "')); select top 1 @epz=stockLink  from [RED DOT DISTRIBUTION EPZ LTD].[dbo].stkitem where (lower(Code)=lower('" & ddlSimpleCode.SelectedItem.Text & "'));select top 1 @ug=stockLink  from [UgandaKE].[dbo].stkitem where (lower(Code)=lower('" & ddlSimpleCode.SelectedItem.Text & "'))select @ke as ke,@epz as epz,@ug as ug ;"
            Db.constr = myGlobal.getConnectionStringForDB("OB1")
            drd1 = Db.myGetReader(tstr)

            drd1.Read()
            partInKE = drd1("ke")
            partInEPZ = drd1("epz")
            partInUG = drd1("ug")
            drd1.Close()
        Catch ex As Exception
            lblError.Text = "Error : part no. could not be verified in all databases, " + ex.Message + " Please retry."
            ddlSimpleCode.SelectedIndex = 0
            getItemExistence = False
            Exit Function
        End Try


        If partInTZ > 0 Then 'And CheckCreateInTZ.Checked Then
            CheckStatusTZ.Checked = True
            CheckStatusTZ.ForeColor = Drawing.Color.Green
        Else
            CheckStatusTZ.Checked = False
            CheckStatusTZ.ForeColor = Drawing.Color.Red
        End If

        If partInTRI > 0 Then 'And CheckCreateInDU.Checked Then
            CheckStatusDU.Checked = True
            CheckStatusDU.ForeColor = Drawing.Color.Green
        Else
            CheckStatusDU.Checked = False
            CheckStatusDU.ForeColor = Drawing.Color.Red
        End If

        If partInKE > 0 Then 'And CheckCreateInKE.Checked Then
            CheckStatusKE.Checked = True
            CheckStatusKE.ForeColor = Drawing.Color.Green
        Else
            CheckStatusKE.Checked = False
            CheckStatusKE.ForeColor = Drawing.Color.Red
        End If

        If partInEPZ > 0 Then 'And CheckCreateInEPZ.Checked Then
            CheckStatusEPZ.Checked = True
            CheckStatusEPZ.ForeColor = Drawing.Color.Green
        Else
            CheckStatusEPZ.Checked = False
            CheckStatusEPZ.ForeColor = Drawing.Color.Red
        End If

        If partInUG > 0 Then 'And CheckCreateInUG.Checked Then
            CheckStatusUG.Checked = True
            CheckStatusUG.ForeColor = Drawing.Color.Green
        Else
            CheckStatusUG.Checked = False
            CheckStatusUG.ForeColor = Drawing.Color.Red
        End If

        pnlEditItem.Enabled = True

        lblError.Text = "verified."

        getItemExistence = True
    End Function

    Private Sub ResetAll()
        lblColumnCount.Text = "0"
        lblConMsg.Text = "Not Connected"
        btnConnect.Enabled = True
        'lblError.Visible = False
        pnlItem.Enabled = False
        pnlEditItem.Enabled = False
        lblCode.Text = ""
        lblUniqueDesc.Text = ""
        txtSimpleCode.Text = ""
        lblSimpleCodeCount.Text = ""
        chkSeg2.Checked = False
        chkSeg3.Checked = False
        chkSeg4.Checked = False
        chkSeg5.Checked = False
        chkSeg6.Checked = False
        CheckStatusDU.Checked = False
        CheckStatusEPZ.Checked = False
        CheckStatusKE.Checked = False
        CheckStatusTZ.Checked = False
        CheckStatusUG.Checked = False
        CheckStatusDU.Enabled = False
        CheckStatusEPZ.Enabled = False
        CheckStatusKE.Enabled = False
        CheckStatusTZ.Enabled = False
        CheckStatusUG.Enabled = False

        CheckStatusTZ.ForeColor = Drawing.Color.Black
        CheckStatusDU.ForeColor = Drawing.Color.Black
        CheckStatusKE.ForeColor = Drawing.Color.Black
        CheckStatusEPZ.ForeColor = Drawing.Color.Black
        CheckStatusUG.ForeColor = Drawing.Color.Black

        ddlSimpleCode.Items.Clear()
        cmbBUTRI.Items.Clear()
        cmbCatTRI.Items.Clear()
        cmbManTRI.Items.Clear()
        cmbModelTRI.Items.Clear()
        cmbPartTRI.Items.Clear()
        cmbPLTRI.Items.Clear()
        cmbDashCategory.Items.Clear()
        cmbGroup.Items.Clear()
        txtDescription1.Text = ""
        txtDescription2.Text = ""
        txtDescription3.Text = ""
        txtavgCost.Text = ""
        txtgrvCost.Text = ""
        cmbIsActive.SelectedIndex = 0
        txtModelTRI.Text = ""
        txtPLTRI.Text = ""
        txtModelTRI.Visible = False
        txtPLTRI.Visible = False
        cmbModelTRI.Visible = True
        cmbPLTRI.Visible = True
        chkNewModel.Checked = False
        chkNewPL.Checked = False

    End Sub



    Private Sub FillDetails()
        Dim dtt As DataTable

        'lblError.Text = ddlSimpleCode.SelectedItem.Text + " ------ " + ddlSimpleCode.SelectedValue.ToString()

        'query = "Select stockLink,code,csimplecode,itemgroup,iInvSegValue1ID ,iInvSegValue2ID ,iInvSegValue3ID ,iInvSegValue4ID ,iInvSegValue5ID ,iInvSegValue6ID ,iInvSegValue7ID,ulIIdashboardCategory,description_1,description_2,description_3,cExtDescription,AveUCst ,LatUCst ,LowUCst ,HigUCst,StdUCst,fItemLastGRVCost,ItemActive,ucIICreatedBy,udIICreationDate from stkitem where code='" + ddlSimpleCode.SelectedItem.Text + "' order by code"
        'Db.constr = myGlobal.getConnectionStringForDB(ddlDB.SelectedValue.ToString())
        'dtt = Db.myGetDS(query).Tables(0)

        dtt = Session("TblItemsOfDB")

        For Each dr In dtt.Rows
            If dr("code") = ddlSimpleCode.SelectedItem.Text Then
                txtSimpleCode.Text = dr("csimplecode")
                lblCode.Text = dr("code")


                If Not IsDBNull(dr("iInvSegValue1ID")) Then
                    If dr("iInvSegValue1ID") <> 0 Then
                        category = dr("iInvSegValue1ID").ToString()

                    Else
                        category = "-1"
                    End If
                End If
                If Not IsDBNull(dr("iInvSegValue2ID")) Then
                    If dr("iInvSegValue2ID") <> 0 Then
                        manufacture = dr("iInvSegValue2ID").ToString()

                    Else
                        manufacture = "-1"
                    End If
                End If

                If Not IsDBNull(dr("iInvSegValue3ID")) Then
                    If dr("iInvSegValue3ID") <> 0 Then
                        BU = dr("iInvSegValue3ID").ToString()

                    Else
                        BU = "-1"
                    End If
                End If

                If Not IsDBNull(dr("iInvSegValue5ID")) Then
                    If dr("iInvSegValue5ID") <> 0 Then
                        PL = dr("iInvSegValue5ID").ToString()

                    Else
                        PL = "-1"
                    End If
                End If

                If Not IsDBNull(dr("iInvSegValue4ID")) Then
                    If dr("iInvSegValue4ID") <> 0 Then
                        model = dr("iInvSegValue4ID").ToString()

                    Else
                        model = "-1"
                    End If
                End If

                If Not IsDBNull(dr("iInvSegValue6ID")) Then
                    If dr("iInvSegValue6ID") <> 0 Then
                        part = dr("iInvSegValue6ID").ToString()

                    Else
                        part = "-1"
                    End If
                End If

                If Not IsDBNull(dr("ulIIdashboardCategory")) Then
                    dashCategory = dr("ulIIdashboardCategory").ToString()
                Else
                    dashCategory = "-1"
                End If

                If Not IsDBNull(dr("Description_1")) Then
                    desc1 = dr("Description_1").ToString()
                Else
                    desc1 = ""
                End If

                If Not IsDBNull(dr("Description_2")) Then
                    desc2 = dr("Description_2").ToString()
                Else
                    desc2 = ""
                End If

                If Not IsDBNull(dr("Description_3")) Then
                    desc3 = dr("Description_3").ToString()
                Else
                    desc3 = ""
                End If

                If Not IsDBNull(dr("AveUCst")) Then
                    avgCost = dr("AveUCst").ToString()
                Else
                    avgCost = ""
                End If

                If Not IsDBNull(dr("LatUCst")) Then
                    grvcost = dr("LatUCst").ToString()
                Else
                    grvcost = ""
                End If

                If Not IsDBNull(dr("ItemGroup")) Then
                    group = dr("ItemGroup").ToString()
                Else
                    group = "-1"
                End If

                If Not IsDBNull(dr("ItemActive")) Then
                    active = dr("ItemActive")
                Else
                    active = -1
                End If

                Exit For
            End If
        Next

        load_groups()
        txtDescription1.Text = desc1
        txtDescription2.Text = desc2
        txtDescription3.Text = desc3
        txtavgCost.Text = avgCost
        txtgrvCost.Text = grvcost
        If (active = "True") Then
            cmbIsActive.SelectedIndex = 0
        Else
            cmbIsActive.SelectedIndex = 1
        End If

    End Sub

    Private Sub setCombos()

        'Setting Dropdowns.......

        If (cmbDashCategory.Items.Count > 0) Then
            Dim tt As ListItem
            If dashCategory <> "-1" Then
                tt = cmbDashCategory.Items.FindByText(dashCategory)
                If Not tt Is Nothing Then
                    cmbDashCategory.ClearSelection()
                    cmbDashCategory.Items.FindByText(dashCategory).Selected = True
                End If
            End If
        End If

        If (cmbCatTRI.Items.Count > 0) Then
            If category <> "-1" Then
                cmbCatTRI.ClearSelection()
                cmbCatTRI.Items.FindByValue(category).Selected = True
                'cmbCatTRI.SelectedValue = category

                'manLoad()
            End If
        End If

        If (cmbManTRI.Items.Count > 0) Then
            If manufacture <> "-1" Then
                cmbManTRI.ClearSelection()
                cmbManTRI.Items.FindByValue(manufacture).Selected = True
                'cmbManTRI.SelectedValue = manufacture

                'buLoad()
                chkSeg2.Checked = True
                chkSeg2.Enabled = True
                cmbManTRI.Enabled = True

                chkSeg3.Enabled = True

            End If
        End If

        If (cmbBUTRI.Items.Count > 0) Then
            If BU <> "-1" Then
                cmbBUTRI.ClearSelection()
                cmbBUTRI.Items.FindByValue(BU).Selected = True
                'cmbBUTRI.SelectedValue = BU

                'plLoad()
                chkSeg3.Checked = True
                chkSeg3.Enabled = True
                cmbBUTRI.Enabled = True

                chkSeg4.Enabled = True

            End If
        End If

        If (cmbPLTRI.Items.Count > 0) Then
            If model <> "-1" Then
                cmbPLTRI.ClearSelection()
                cmbPLTRI.Items.FindByValue(model).Selected = True
                'cmbPLTRI.SelectedValue = model

                'modelLoad()
                chkSeg4.Checked = True
                chkSeg4.Enabled = True
                cmbPLTRI.Enabled = True
                chkNewPL.Enabled = True

                chkSeg5.Enabled = True

            End If
        End If

        If (cmbModelTRI.Items.Count > 0) Then
            If PL <> "-1" Then
                cmbModelTRI.ClearSelection()
                cmbModelTRI.Items.FindByValue(PL).Selected = True
                'cmbModelTRI.SelectedValue = PL

                'partLoad()
                chkSeg5.Checked = True
                chkSeg5.Enabled = True
                cmbModelTRI.Enabled = True
                chkNewModel.Enabled = True
            End If
        End If

        If (cmbPartTRI.Items.Count > 0) Then
            If part <> "-1" Then
                cmbPartTRI.ClearSelection()
                cmbPartTRI.Items.FindByValue(part).Selected = True
                'cmbPartTRI.SelectedValue = part

                chkSeg6.Checked = True
                chkSeg6.Enabled = True
                cmbPartTRI.Enabled = True
            End If
        End If

    End Sub

    Private Sub load_groups()
        query = "select * from dbo.GrpTbl"
        Db.LoadDDLsWithCon(cmbGroup, query, "StGroup", "idGrpTbl", constr)
        If (cmbGroup.Items.Count > 0) Then
            If group <> "" Then
                cmbGroup.Items.FindByText(group).Selected = True
            End If
        End If
    End Sub

    Public Sub filldashboardcatagory()
        query = "select * from tej.[dbo].tblDashboardCategory order by dashboardCategoryName"
        Db.LoadDDLsWithCon(cmbDashCategory, query, "dashboardCategoryName", "autoindex", myGlobal.getConnectionStringForDB("EVO"))
        If (cmbDashCategory.Items.Count > 0) Then
            lblColumnCount.Text = "(" + cmbDashCategory.Items.Count.ToString() + ")"
        End If
    End Sub

    Public Sub fillcomboCategory()
        Try
            lblError.Text = ""
            constr = myGlobal.getConnectionStringForDB(ddlDB.SelectedValue.ToString())
            query = "select idInvSegValue,cValue from dbo._etblInvSegValue where iInvSegGroupID=1 order by cValue"
            Db.LoadDDLsWithCon(cmbCatTRI, query, "cValue", "idInvSegValue", constr)
            cmbCatTRI.Enabled = True

            If (cmbCatTRI.Items.Count > 0) Then
                manLoad()
            Else
                cmbManTRI.Items.Clear()
                cmbBUTRI.Items.Clear()
                cmbPLTRI.Items.Clear()
                cmbModelTRI.Items.Clear()
                cmbPartTRI.Items.Clear()
            End If
        Catch ex As Exception
            lblError.Text = "Error :" + ex.Message
            cmbManTRI.Items.Clear()
            cmbBUTRI.Items.Clear()
            cmbPLTRI.Items.Clear()
            cmbModelTRI.Items.Clear()
            cmbPartTRI.Items.Clear()
        End Try
    End Sub

    Public Sub manLoad()
        Try
            'query = "select idInvSegValue,cValue from dbo._etblInvSegValue where iInvSegGroupID=2 and idInvSegValue in (select distinct(iInvSegValue2ID) from dbo.StkItem where iInvSegValue1ID='" & cmbCatTRI.SelectedValue & "') order by cValue"
            query = "select idInvSegValue,cValue from dbo._etblInvSegValue where iInvSegGroupID=2 order by cValue"
            Db.LoadDDLsWithCon(cmbManTRI, query, "cValue", "idInvSegValue", constr)
            If (cmbManTRI.Items.Count > 0) Then
                buLoad()
            Else
                cmbBUTRI.Items.Clear()
                cmbPLTRI.Items.Clear()
                cmbModelTRI.Items.Clear()
                cmbPartTRI.Items.Clear()
            End If
        Catch ex As Exception
            lblError.Text = "Error :" + ex.Message
            cmbBUTRI.Items.Clear()
            cmbPLTRI.Items.Clear()
            cmbModelTRI.Items.Clear()
            cmbPartTRI.Items.Clear()
        End Try
    End Sub

    Public Sub buLoad()
        Try
            'query = "select idInvSegValue,cValue from dbo._etblInvSegValue where iInvSegGroupID=3 and idInvSegValue in (select distinct(iInvSegValue3ID) from dbo.StkItem where iInvSegValue1ID='" & cmbCatTRI.SelectedValue & "' and iInvSegValue2ID='" & cmbManTRI.SelectedValue & "' ) order by cValue"
            query = "select idInvSegValue,cValue from dbo._etblInvSegValue where iInvSegGroupID=3 order by cValue"
            Db.LoadDDLsWithCon(cmbBUTRI, query, "cValue", "idInvSegValue", constr)
            If (cmbBUTRI.Items.Count > 0) Then
                plLoad()
            Else

                cmbPLTRI.Items.Clear()
                cmbModelTRI.Items.Clear()
                cmbPartTRI.Items.Clear()
            End If
        Catch ex As Exception
            lblError.Text = "Error :" + ex.Message
            cmbPLTRI.Items.Clear()
            cmbModelTRI.Items.Clear()
            cmbPartTRI.Items.Clear()
        End Try
    End Sub

    Public Sub plLoad()
        Try
            'query = "select idInvSegValue,cValue from dbo._etblInvSegValue where iInvSegGroupID=5 and idInvSegValue in (select distinct(iInvSegValue4ID) from dbo.StkItem where iInvSegValue1ID='" & cmbCatTRI.SelectedValue & "' and iInvSegValue2ID='" & cmbManTRI.SelectedValue & "' and iInvSegValue3ID='" & cmbBUTRI.SelectedValue & "' ) order by cValue"
            query = "select idInvSegValue,cValue from dbo._etblInvSegValue where iInvSegGroupID=5 order by cValue"
            Db.LoadDDLsWithCon(cmbPLTRI, query, "cValue", "idInvSegValue", constr)
            If (cmbPLTRI.Items.Count > 0) Then
                modelLoad()
            Else
                cmbModelTRI.Items.Clear()
                cmbPartTRI.Items.Clear()
            End If
        Catch ex As Exception
            lblError.Text = "Error :" + ex.Message
            cmbModelTRI.Items.Clear()
            cmbPartTRI.Items.Clear()
        End Try
    End Sub

    Public Sub modelLoad()
        Try
            'query = "select idInvSegValue,cValue from dbo._etblInvSegValue where iInvSegGroupID=4 and idInvSegValue in (select distinct(iInvSegValue5ID) from dbo.StkItem where iInvSegValue1ID='" & cmbCatTRI.SelectedValue & "' and iInvSegValue2ID='" & cmbManTRI.SelectedValue & "' and iInvSegValue3ID='" & cmbBUTRI.SelectedValue & "'and iInvSegValue4ID='" & cmbPLTRI.SelectedValue & " ') order by cValue"
            query = "select idInvSegValue,cValue from dbo._etblInvSegValue where iInvSegGroupID=4 order by cValue"
            Db.LoadDDLsWithCon(cmbModelTRI, query, "cValue", "idInvSegValue", constr)
            If (cmbModelTRI.Items.Count > 0) Then
                partLoad()
            Else
                cmbPartTRI.Items.Clear()
            End If
        Catch ex As Exception
            lblError.Text = "Error :" + ex.Message
            cmbPartTRI.Items.Clear()
        End Try
    End Sub

    Public Sub partLoad()
        Try
            'query = "select idInvSegValue,cValue from dbo._etblInvSegValue where idInvSegValue in (select distinct(iInvSegValue6ID) from dbo.StkItem where iInvSegValue1ID='" & cmbCatTRI.SelectedValue & "' and iInvSegValue2ID='" & cmbManTRI.SelectedValue & "' and iInvSegValue3ID='" & cmbBUTRI.SelectedValue & "'and iInvSegValue4ID='" & cmbPLTRI.SelectedValue & "' and iInvSegValue5ID='" & cmbModelTRI.SelectedValue & "'  ) order by cValue"
            query = "select idInvSegValue,cValue from dbo._etblInvSegValue where iInvSegGroupID=6 order by cValue"
            Db.LoadDDLsWithCon(cmbPartTRI, query, "cVAlue", "idInvSegValue", constr)
        Catch ex As Exception
            lblError.Text = "Error :" + ex.Message
        End Try
    End Sub

    Protected Sub chkSeg2_CheckedChanged(ByVal sender As Object, ByVal e As System.EventArgs) Handles chkSeg2.CheckedChanged
        If chkSeg2.Checked = True Then
            chkSeg3.Enabled = True
            'chkSeg4.Enabled = True
            'chkSeg5.Enabled = True
            'chkSeg6.Enabled = True
            cmbManTRI.Enabled = True
            'cmbBUTRI.Enabled = False
            'cmbPLTRI.Enabled = False
            'cmbModelTRI.Enabled = False
            cmbPartTRI.Enabled = False

        ElseIf chkSeg2.Checked = False Then

            chkSeg3.Enabled = False
            chkSeg4.Enabled = False
            chkSeg5.Enabled = False
            chkSeg6.Enabled = False
            cmbManTRI.Enabled = False
            cmbBUTRI.Enabled = False
            cmbPLTRI.Enabled = False
            cmbModelTRI.Enabled = False
            cmbPartTRI.Enabled = False
            chkSeg3.Checked = False
            chkSeg4.Checked = False
            chkSeg5.Checked = False
            chkSeg6.Checked = False

            chkNewModel.Enabled = False
            chkNewPL.Enabled = False

            chkNewModel.Checked = False
            chkNewPL.Checked = False
            Call handleModelFields(False)
            Call handlePLFields(False)
        End If
    End Sub

    Protected Sub chkSeg3_CheckedChanged(ByVal sender As Object, ByVal e As System.EventArgs) Handles chkSeg3.CheckedChanged
        If chkSeg3.Checked = True Then
            chkSeg4.Enabled = True
            'chkSeg5.Enabled = True
            'chkSeg6.Enabled = True
            cmbBUTRI.Enabled = True
            'cmbPLTRI.Enabled = False
            'cmbModelTRI.Enabled = False
            cmbPartTRI.Enabled = False
        ElseIf chkSeg3.Checked = False Then
            chkSeg4.Enabled = False
            chkSeg5.Enabled = False
            chkSeg6.Enabled = False
            cmbBUTRI.Enabled = False
            cmbPLTRI.Enabled = False
            cmbModelTRI.Enabled = False
            cmbPartTRI.Enabled = False
            chkSeg4.Checked = False
            chkSeg5.Checked = False
            chkSeg6.Checked = False

            chkNewModel.Enabled = False
            chkNewPL.Enabled = False

            chkNewModel.Checked = False
            chkNewPL.Checked = False
            Call handleModelFields(False)
            Call handlePLFields(False)
        End If
    End Sub

    Protected Sub chkSeg4_CheckedChanged(ByVal sender As Object, ByVal e As System.EventArgs) Handles chkSeg4.CheckedChanged
        If chkSeg4.Checked = True Then
            chkSeg5.Enabled = True
            chkSeg6.Enabled = True
            cmbPLTRI.Enabled = True
            txtPLTRI.Enabled = True
            cmbModelTRI.Enabled = False
            cmbPartTRI.Enabled = False

            chkNewPL.Enabled = True

        ElseIf chkSeg4.Checked = False Then
            chkSeg5.Enabled = False
            chkSeg6.Enabled = False
            cmbPLTRI.Enabled = False
            txtPLTRI.Enabled = False
            cmbModelTRI.Enabled = False
            cmbPartTRI.Enabled = False
            chkSeg5.Checked = False
            chkSeg6.Checked = False

            chkNewModel.Enabled = False
            chkNewPL.Enabled = False

            chkNewModel.Checked = False
            chkNewPL.Checked = False
            Call handleModelFields(False)
            Call handlePLFields(False)
        End If
    End Sub

    Protected Sub chkSeg5_CheckedChanged(ByVal sender As Object, ByVal e As System.EventArgs) Handles chkSeg5.CheckedChanged
        If chkSeg5.Checked = True Then
            chkSeg5.Enabled = True
            chkSeg6.Enabled = True
            cmbPartTRI.Enabled = False

            If (chkNewPL.Checked = True) Then
                cmbModelTRI.Enabled = False
                cmbModelTRI.Visible = False
                txtModelTRI.Visible = True
                txtModelTRI.Enabled = True
                chkNewModel.Enabled = False
                chkNewModel.Checked = True
            Else
                cmbModelTRI.Enabled = True
                cmbModelTRI.Visible = True
                txtModelTRI.Visible = False
                txtModelTRI.Enabled = True
                chkNewModel.Enabled = True
                chkNewModel.Checked = False
            End If

        ElseIf chkSeg5.Checked = False Then
            chkSeg6.Enabled = False
            cmbModelTRI.Enabled = False
            txtModelTRI.Enabled = False
            cmbPartTRI.Enabled = False
            chkSeg6.Checked = False

            chkNewModel.Enabled = False

            chkNewModel.Checked = False
            Call handleModelFields(False)
        End If
    End Sub

    Protected Sub chkSeg6_CheckedChanged(ByVal sender As Object, ByVal e As System.EventArgs) Handles chkSeg6.CheckedChanged
        If chkSeg6.Checked = True Then
            cmbPartTRI.Enabled = True
        ElseIf chkSeg6.Checked = False Then
            cmbPartTRI.Enabled = False
        End If
    End Sub

    Protected Sub chkNewModel_CheckedChanged(ByVal sender As Object, ByVal e As System.EventArgs) Handles chkNewModel.CheckedChanged
        Call handleModelFields(chkNewModel.Checked)
    End Sub

    Protected Sub chkNewPL_CheckedChanged(ByVal sender As Object, ByVal e As System.EventArgs) Handles chkNewPL.CheckedChanged
        Call handlePLFields(chkNewPL.Checked)
    End Sub

    Private Sub handleModelFields(ByVal flg As Boolean)
        If flg = True Then
            cmbModelTRI.Visible = False
            txtModelTRI.Visible = True
            txtModelTRI.Enabled = True
        Else
            cmbModelTRI.Visible = True
            txtModelTRI.Visible = False
            txtModelTRI.Enabled = False
        End If
    End Sub

    Private Sub handlePLFields(ByVal flg As Boolean)
        If flg = True Then
            cmbPLTRI.Visible = False
            txtPLTRI.Visible = True
            txtPLTRI.Enabled = True
            chkSeg5.Checked = False
            cmbModelTRI.Enabled = False
            txtModelTRI.Enabled = False
            chkNewModel.Enabled = False
            chkNewModel.Checked = False
        Else
            cmbPLTRI.Visible = True
            txtPLTRI.Visible = False
            txtPLTRI.Enabled = False
            If (chkSeg5.Checked = True) Then
                cmbModelTRI.Enabled = True
                cmbModelTRI.Visible = True
                txtModelTRI.Visible = False
                txtModelTRI.Enabled = False
                chkNewModel.Enabled = True
                chkNewModel.Checked = False
            End If
        End If
    End Sub

    Protected Sub btnUpdate_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles btnUpdate.Click
        Try
            lblError.Text = "Update begins.."

            If (chkNewPL.Checked = True And chkSeg4.Checked = True) Then
                If (txtPLTRI.Text = "") Then
                    lblError.Text = "Error!! Product Line Name Can't Be Left Empty, Please Enter Product Line value Or Select From Available list of Product Line"
                    'Message.Show(Me, lblError.Text)
                    Exit Sub
                End If
            End If

            If (chkNewModel.Checked = True And chkSeg5.Checked = True) Then
                If (txtModelTRI.Text = "") Then
                    lblError.Text = "Error!! Model Name Can't Be Left Empty, Please Enter Model Name Or Select From Available list of Models"
                    'Message.Show(Me, lblError.Text)
                    Exit Sub
                End If
            End If

            If txtSimpleCode.Text = "" Or cmbGroup.SelectedItem.Text = "" Then 'Or cmbPack.SelectedItem.Text = "" Or cmbLocation.SelectedItem.Text = "" Then
                lblError.Text = "One or more fields left blank!!! Either Simple Code or Group is left blank. Make sure these are not blank."
                'Message.Show(Me, lblError.Text)
                Exit Sub
            End If

            If cmbCatTRI.SelectedItem.Text = "" Then
                lblError.Text = "One or more fields left blank!!!  Error! Select Category from segments to proceed."
                'Message.Show(Me, lblError.Text)
                Exit Sub
            End If


            If Not Util.isValidDecimalNumber(txtavgCost.Text) Then
                lblError.Text = "Invalid Value! Field Average Cost, Please supply a valid numeric value."
                'Message.Show(Me, lblError.Text)
                Exit Sub
            End If

            If Convert.ToInt32(txtavgCost.Text <= 0) Then
                lblError.Text = "Invalid Value! Field Average Cost, Please supply a valid numeric value greater than Zero"
                'Message.Show(Me, lblError.Text)
                Exit Sub
            End If


            If Not Util.isValidDecimalNumber(txtgrvCost.Text) Then
                lblError.Text = "Invalid Value! Field GRV Cost , Please supply a valid numeric value."
                'Message.Show(Me, lblError.Text)
                Exit Sub
            End If

            If Convert.ToInt32(txtgrvCost.Text <= 0) Then
                lblError.Text = "Invalid Value! Field GRV Cost, Please supply a valid numeric value greater than Zero"
                'Message.Show(Me, lblError.Text)
                Exit Sub
            End If

            lblUniqueDesc.Text = txtSimpleCode.Text
            '---verify connections---------------------------

            Dim rsAddnewCmd As New SqlCommand

            constrEVO = myGlobal.getConnectionStringForDB("EVO")
            constrOB1 = myGlobal.getConnectionStringForDB("OB1")
            Dim conFlagEvo, conFlagOB1 As Boolean
            conFlagEvo = False
            conFlagOB1 = False
            Try
                conEVO = New SqlConnection()
                conEVO.ConnectionString = constrEVO
                conEVO.Open()
                conFlagEvo = True
            Catch ex As Exception
                conFlagEvo = False
            End Try

            Try
                conOB1 = New SqlConnection
                conOB1.ConnectionString = constrOB1
                conOB1.Open()
                conFlagOB1 = True
            Catch ex As Exception
                conFlagOB1 = False
            End Try

            If conFlagEvo = False Or conFlagOB1 = False Then
                If conEVO.State = 1 Then
                    conEVO.Close()
                End If
                If conOB1.State = 1 Then
                    conOB1.Close()
                End If
                lblError.Text = "Severs connection failed...Error! One of the server connection failed, So Part no. not be updated in any database , Retry little later"
                'Message.Show(Me, lblError.Text)
                Exit Sub
            End If

            '------------------------------

            lblSuccess.Text = "Please wait while AddOnce Stocks is creating your stock Item..."


            If chkSeg1.Checked = True And cmbCatTRI.Text <> "" Then
                lblUniqueDesc.Text = lblUniqueDesc.Text & "/" & cmbCatTRI.SelectedItem.Text
            End If
            If chkSeg2.Checked = True And cmbManTRI.Text <> "" Then
                lblUniqueDesc.Text = lblUniqueDesc.Text & "/" & cmbManTRI.SelectedItem.Text
            End If
            If chkSeg3.Checked = True And cmbBUTRI.Text <> "" Then
                lblUniqueDesc.Text = lblUniqueDesc.Text & "/" & cmbBUTRI.SelectedItem.Text
            End If
            If (chkNewPL.Checked = False) Then
                If chkSeg4.Checked = True And cmbPLTRI.Text <> "" Then
                    lblUniqueDesc.Text = lblUniqueDesc.Text & "/" & cmbPLTRI.SelectedItem.Text
                End If
            End If
            If (chkNewPL.Checked = True) Then
                If chkSeg4.Checked = True And txtPLTRI.Text <> "" Then
                    lblUniqueDesc.Text = lblUniqueDesc.Text & "/" & txtPLTRI.Text.ToUpper
                End If
            End If

            If (chkNewModel.Checked = False) Then
                If chkSeg5.Checked = True And cmbModelTRI.Text <> "" Then
                    lblUniqueDesc.Text = lblUniqueDesc.Text & "/" & cmbModelTRI.SelectedItem.Text
                End If
            End If
            If (chkNewModel.Checked = True) Then
                If chkSeg5.Checked = True And txtModelTRI.Text <> "" Then
                    lblUniqueDesc.Text = lblUniqueDesc.Text & "/" & txtModelTRI.Text.ToUpper
                End If
            End If
            If chkSeg6.Checked = True And cmbPartTRI.Text <> "" Then
                lblUniqueDesc.Text = lblUniqueDesc.Text & "/" & cmbPartTRI.SelectedItem.Text
            End If


            'check for the existence of stkitem in the database

            partInTZ = partInTRI = partInKE = partInEPZ = partInUG = 0

            If getItemExistenceOnUpdate() Then
                Exit Sub
            End If

            rsAddnewCmd.CommandType = Data.CommandType.StoredProcedure

            If chkSeg1.Checked = True And cmbCatTRI.Text <> "" Then
                rsAddnewCmd.Parameters.Add(New SqlParameter("@cvalue1", Data.SqlDbType.VarChar, 50)).Value = cmbCatTRI.SelectedItem.Text
            Else
                rsAddnewCmd.Parameters.Add(New SqlParameter("@cvalue1", Data.SqlDbType.VarChar, 50)).Value = ""
            End If

            If chkSeg2.Checked = True And cmbManTRI.Text <> "" Then
                rsAddnewCmd.Parameters.Add(New SqlParameter("@cvalue2", Data.SqlDbType.VarChar, 50)).Value = cmbManTRI.SelectedItem.Text
            Else
                rsAddnewCmd.Parameters.Add(New SqlParameter("@cvalue2", Data.SqlDbType.VarChar, 50)).Value = ""
            End If

            If chkSeg3.Checked = True And cmbBUTRI.Text <> "" Then
                rsAddnewCmd.Parameters.Add(New SqlParameter("@cvalue3", Data.SqlDbType.VarChar, 50)).Value = cmbBUTRI.SelectedItem.Text
            Else
                rsAddnewCmd.Parameters.Add(New SqlParameter("@cvalue3", Data.SqlDbType.VarChar, 50)).Value = ""
            End If

            If chkSeg4.Checked = True And cmbPLTRI.Text <> "" Then
                If (chkNewPL.Checked = False) Then
                    rsAddnewCmd.Parameters.Add(New SqlParameter("@cvalue4", Data.SqlDbType.VarChar, 50)).Value = cmbPLTRI.SelectedItem.Text
                Else
                    rsAddnewCmd.Parameters.Add(New SqlParameter("@cvalue4", Data.SqlDbType.VarChar, 50)).Value = txtPLTRI.Text.ToUpper
                End If
            Else
                rsAddnewCmd.Parameters.Add(New SqlParameter("@cvalue4", Data.SqlDbType.VarChar, 50)).Value = ""
            End If

            If chkSeg5.Checked = True And cmbModelTRI.Text <> "" Then
                If (chkNewModel.Checked = False) Then
                    rsAddnewCmd.Parameters.Add(New SqlParameter("@cvalue5", Data.SqlDbType.VarChar, 50)).Value = cmbModelTRI.SelectedItem.Text
                Else
                    rsAddnewCmd.Parameters.Add(New SqlParameter("@cvalue5", Data.SqlDbType.VarChar, 50)).Value = txtModelTRI.Text.ToUpper
                End If
            Else
                rsAddnewCmd.Parameters.Add(New SqlParameter("@cvalue5", Data.SqlDbType.VarChar, 50)).Value = ""
            End If

            If chkSeg6.Checked = True And cmbPartTRI.Text <> "" Then
                rsAddnewCmd.Parameters.Add(New SqlParameter("@cvalue6", Data.SqlDbType.VarChar, 50)).Value = cmbPartTRI.SelectedItem.Text
            Else
                rsAddnewCmd.Parameters.Add(New SqlParameter("@cvalue6", Data.SqlDbType.VarChar, 50)).Value = ""
            End If



            rsAddnewCmd.Parameters.Add(New SqlParameter("@OldCode", Data.SqlDbType.VarChar, 255)).Value = lblCode.Text

            rsAddnewCmd.Parameters.Add(New SqlParameter("@cSimpleCode", Data.SqlDbType.VarChar, 50)).Value = txtSimpleCode.Text

            rsAddnewCmd.Parameters.Add(New SqlParameter("@code", Data.SqlDbType.VarChar, 255)).Value = lblUniqueDesc.Text

            rsAddnewCmd.Parameters.Add(New SqlParameter("@cExtDescription", Data.SqlDbType.VarChar, 255)).Value = lblUniqueDesc.Text

            rsAddnewCmd.Parameters.Add(New SqlParameter("@ItemGroup", Data.SqlDbType.VarChar, 50)).Value = cmbGroup.SelectedItem.Text

            rsAddnewCmd.Parameters.Add(New SqlParameter("@Description_1", Data.SqlDbType.VarChar, 50)).Value = txtDescription1.Text

            rsAddnewCmd.Parameters.Add(New SqlParameter("@Description_2", Data.SqlDbType.VarChar, 50)).Value = txtDescription2.Text

            rsAddnewCmd.Parameters.Add(New SqlParameter("@Description_3", Data.SqlDbType.VarChar, 50)).Value = txtDescription3.Text

            rsAddnewCmd.Parameters.Add(New SqlParameter("@AveUCst", Data.SqlDbType.Float, 50)).Value = Convert.ToDouble(txtavgCost.Text)

            rsAddnewCmd.Parameters.Add(New SqlParameter("@LatUCst", Data.SqlDbType.Float, 50)).Value = Convert.ToDouble(txtgrvCost.Text)

            rsAddnewCmd.Parameters.Add(New SqlParameter("@LowUCst", Data.SqlDbType.Float, 50)).Value = Convert.ToDouble(txtavgCost.Text)

            rsAddnewCmd.Parameters.Add(New SqlParameter("@HigUCst", Data.SqlDbType.Float, 50)).Value = Convert.ToDouble(txtavgCost.Text)

            rsAddnewCmd.Parameters.Add(New SqlParameter("@StdUCst", Data.SqlDbType.Float, 50)).Value = Convert.ToDouble(txtavgCost.Text)

            rsAddnewCmd.Parameters.Add(New SqlParameter("@ItemActive", Data.SqlDbType.Bit, 50)).Value = Convert.ToBoolean(cmbIsActive.SelectedItem.Text)

            rsAddnewCmd.Parameters.Add(New SqlParameter("@fItemLastGRVCost", Data.SqlDbType.Float)).Value = Convert.ToDouble(txtgrvCost.Text)

            rsAddnewCmd.Parameters.Add(New SqlParameter("@ulIIdashboardCategory", Data.SqlDbType.VarChar, 100)).Value = cmbDashCategory.SelectedItem.Text

            If chkSeg1.Checked = True And cmbCatTRI.Text <> "" Then
                rsAddnewCmd.Parameters.Add("@iInvSegValue1ID", Data.SqlDbType.Int).Value = cmbCatTRI.SelectedValue
            Else
                rsAddnewCmd.Parameters.Add("@iInvSegValue1ID", Data.SqlDbType.Int).Value = 0
            End If

            If chkSeg2.Checked = True And cmbManTRI.Text <> "" Then
                rsAddnewCmd.Parameters.Add("@iInvSegValue2ID", Data.SqlDbType.Int).Value = cmbManTRI.SelectedValue
            Else
                rsAddnewCmd.Parameters.Add("@iInvSegValue2ID", Data.SqlDbType.Int).Value = 0
            End If

            If chkSeg3.Checked = True And cmbBUTRI.Text <> "" Then
                rsAddnewCmd.Parameters.Add("@iInvSegValue3ID", Data.SqlDbType.Int).Value = cmbBUTRI.SelectedValue
            Else
                rsAddnewCmd.Parameters.Add("@iInvSegValue3ID", Data.SqlDbType.Int).Value = 0
            End If

            If (chkNewPL.Checked = False) Then
                If chkSeg4.Checked = True And cmbPLTRI.Text <> "" Then
                    rsAddnewCmd.Parameters.Add("@iInvSegValue4ID", Data.SqlDbType.Int).Value = cmbPLTRI.SelectedValue
                Else
                    rsAddnewCmd.Parameters.Add("@iInvSegValue4ID", Data.SqlDbType.Int).Value = 0
                End If
            End If

            If (chkNewPL.Checked = True) Then
                If chkSeg4.Checked = True And txtPLTRI.Text <> "" Then
                    rsAddnewCmd.Parameters.Add("@iInvSegValue4ID", Data.SqlDbType.Int).Value = 0
                End If
            End If

            If (chkNewModel.Checked = False) Then
                If chkSeg5.Checked = True And cmbModelTRI.Text <> "" Then
                    rsAddnewCmd.Parameters.Add("@iInvSegValue5ID", Data.SqlDbType.Int).Value = cmbModelTRI.SelectedValue
                Else
                    rsAddnewCmd.Parameters.Add("@iInvSegValue5ID", Data.SqlDbType.Int).Value = 0
                End If
            End If

            If (chkNewModel.Checked = True) Then
                If chkSeg5.Checked = True And txtModelTRI.Text <> "" Then
                    rsAddnewCmd.Parameters.Add("@iInvSegValue5ID", Data.SqlDbType.Int).Value = 0
                End If
            End If

            If chkSeg6.Checked = True And cmbPartTRI.Text <> "" Then
                rsAddnewCmd.Parameters.Add("@iInvSegValue6ID", Data.SqlDbType.Int).Value = cmbPartTRI.SelectedValue
            Else
                rsAddnewCmd.Parameters.Add("@iInvSegValue6ID", Data.SqlDbType.Int).Value = 0
            End If

            rsAddnewCmd.Parameters.Add(New SqlParameter("@createdBy", Data.SqlDbType.VarChar, 100)).Value = lblVersionNo.Text

            If CheckStatusKE.Checked Then
                rsAddnewCmd.Parameters.Add("@createInKE", Data.SqlDbType.Int).Value = 1
            Else
                rsAddnewCmd.Parameters.Add("@createInKE", Data.SqlDbType.Int).Value = 0
            End If

            If CheckStatusDU.Checked Then
                rsAddnewCmd.Parameters.Add("@createInDU", Data.SqlDbType.Int).Value = 1
            Else
                rsAddnewCmd.Parameters.Add("@createInDU", Data.SqlDbType.Int).Value = 0
            End If

            If CheckStatusEPZ.Checked Then
                rsAddnewCmd.Parameters.Add("@createInEPZ", Data.SqlDbType.Int).Value = 1
            Else
                rsAddnewCmd.Parameters.Add("@createInEPZ", Data.SqlDbType.Int).Value = 0
            End If

            If CheckStatusTZ.Checked Then
                rsAddnewCmd.Parameters.Add("@createInTZ", Data.SqlDbType.Int).Value = 1
            Else
                rsAddnewCmd.Parameters.Add("@createInTZ", Data.SqlDbType.Int).Value = 0
            End If

            If CheckStatusUG.Checked Then
                rsAddnewCmd.Parameters.Add("@createInUG", Data.SqlDbType.Int).Value = 1
            Else
                rsAddnewCmd.Parameters.Add("@createInUG", Data.SqlDbType.Int).Value = 0
            End If

            rsAddnewCmd.Parameters.Add(New SqlParameter("@resultKE", Data.SqlDbType.Int)).Direction = Data.ParameterDirection.Output
            rsAddnewCmd.Parameters.Add(New SqlParameter("@resultDU", Data.SqlDbType.Int)).Direction = Data.ParameterDirection.Output
            rsAddnewCmd.Parameters.Add(New SqlParameter("@resultEPZ", Data.SqlDbType.Int)).Direction = Data.ParameterDirection.Output
            rsAddnewCmd.Parameters.Add(New SqlParameter("@resultTZ", Data.SqlDbType.Int)).Direction = Data.ParameterDirection.Output
            rsAddnewCmd.Parameters.Add(New SqlParameter("@resultUG", Data.SqlDbType.Int)).Direction = Data.ParameterDirection.Output


            Dim resKE As Integer
            Dim resDU As Integer
            Dim resEPZ As Integer
            Dim resTZ As Integer
            Dim resUG As Integer

            resKE = 0
            resDU = 0
            resEPZ = 0
            resTZ = 0
            resUG = 0


            '''''''''''''''''''''''''''''''''''''call again''OB1'''''''''

            rsAddnewCmd.Connection = conOB1
            rsAddnewCmd.CommandTimeout = 600
            rsAddnewCmd.CommandText = "tej.[dbo].[EditStkItemMain-EPZ-KE-UG]"
            rsAddnewCmd.ExecuteNonQuery()

            resKE = rsAddnewCmd.Parameters("@resultKE").Value
            resEPZ = rsAddnewCmd.Parameters("@resultEPZ").Value
            resUG = rsAddnewCmd.Parameters("@resultUG").Value

            If conOB1.State = 1 Then
                conOB1.Close()
            End If

            '''''''''''''''''''''''''''''''''''''call again''''''EVO'''''
            If resKE = 1 And resEPZ = 1 And resUG = 1 Then  'If ke,Epz got created then only go for TZ, DU

                rsAddnewCmd.Connection = conEVO
                rsAddnewCmd.CommandTimeout = 600
                rsAddnewCmd.CommandText = "tej.[dbo].[EditStkItemMain-TRI-TZ]"
                rsAddnewCmd.ExecuteNonQuery()


                resTZ = rsAddnewCmd.Parameters("@resultTZ").Value
                resDU = rsAddnewCmd.Parameters("@resultDU").Value

                If conEVO.State = 1 Then
                    conEVO.Close()
                End If

            End If


            '''''''''''''''''''''''''''''''''''''call again'''''''''''

            If resKE = 1 And resDU = 1 And resEPZ = 1 And resTZ = 1 And resUG = 1 Then
                lblSuccess.Text = "Stock Item: " & "'" & lblUniqueDesc.Text & "'" & " Updated successfully for all the warehouses in selected countries/databases "
                lblSuccess.ForeColor = Drawing.Color.Green
                'Message.Show(Me, lblSuccess.Text)
                ResetAll()
            Else
                lblSuccess.Text = "Stock Item: " & "'" & lblUniqueDesc.Text & "'" & " could not be Updated due to error occured in one of the countries/databases. Retry or Please consult database administrator."
                lblSuccess.ForeColor = Drawing.Color.Red
                'Message.Show(Me, lblSuccess.Text)
            End If

        Catch ex As Exception
            lblError.Text = "Error : " + ex.Message
        End Try
    End Sub

End Class
