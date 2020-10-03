Imports System.Data.SqlClient
Partial Class Intranet_EVO_AddOnce1
    Inherits System.Web.UI.Page

    Public active As String
    Public WarehouseItem As String
    Public serviceItem As String
    Public serialnum As String
    Public strictSerial As String
    Public allowDup As String
    Public commissionable As String
    Public grpTRI As Integer
    Public BinTRI As Integer
    Public PackTRI As Integer
    Public seg1TRI As Long
    Public seg2TRI As Long
    Public seg3TRI As Long
    Public seg4TRI As Long
    Public seg5TRI As Long
    Public seg6TRI As Long
    Public seg1EPZ As Long
    Public seg2EPZ As Long
    Public seg3EPZ As Long
    Public seg4EPZ As Long
    Public seg5EPZ As Long
    Public seg6EPZ As Long
    Public seg1Tz As Long
    Public seg2Tz As Long
    Public seg3Tz As Long
    Public seg4Tz As Long
    Public seg5Tz As Long
    Public seg6Tz As Long
    Public seg1KE As Long
    Public seg2KE As Long
    Public seg3KE As Long
    Public seg4KE As Long
    Public seg5KE As Long
    Public seg6KE As Long
    Public plID1 As Integer
    Public plID2 As Integer
    Public plID3 As Integer

    Public plID1EPZ As Integer
    Public plID2EPZ As Integer
    Public plID3EPZ As Integer

    Public plID1TZ As Integer
    Public plID2TZ As Integer
    Public plID3TZ As Integer

    Public plID1KE As Integer
    Public plID2KE As Integer
    Public plID3KE As Integer

    Public stkLink As Integer
    Public stkLinkepz As Integer
    Public stkLinkTz As Integer
    Public stkLinkKE As Integer
    Public recon As String
    Public LocTz As String
    Public LocKE As String

    Public connection As ADODB.Connection
    Public connectionOB1 As ADODB.Connection
    Public connstrIND As String
    Public connstrTZ As String
    Public connstrEPZ As String
    Public connstrKE As String
    Public connstrLiveEVO, connstrLiveOB1 As String
    Public connstrTest As String

    Public XVal As String

    Dim rsdname As New ADODB.Recordset
    Dim prevXval As String
    Dim priceListStr As String

    Public prefix As String
    Public constr As String
    Public constrEVO As String
    Public constrOB1 As String
    Public con As SqlConnection
    Public conEVO As SqlConnection
    Public conOB1 As SqlConnection
    Public ping As String

    Dim query As String
    Dim partInTZ, partInTRI, partInKE, partInEPZ, partInUG As Integer

    Sub Main()


        'btnGenerate.Attributes.Add("onClick", "return getConfirmation();")

        ' RDTZ server connection

        'connecting to RDDAPPS  192.168.56.25 for tesing databases 
        connstrTest = "Provider=SQLOLEDB.1;Password=n3Wp455W0rD;Persist Security Info=True;User ID=sa;Initial Catalog=triangle;Data Source=192.168.56.25"

        'close upper open this for live connection
        connstrLiveEVO = "Provider=SQLOLEDB.1;Password=p455w0rd;Persist Security Info=True;User ID=tej;Initial Catalog=triangle;Data Source=192.168.56.40"
        connstrLiveOB1 = "Provider=SQLOLEDB.1;Password=r3Dd0Tk3Ny4L1m1T3d;Persist Security Info=True;User ID=sa;Initial Catalog=Red Dot Distribution Limited - Kenya;Data Source=192.168.1.4,3177"


        connstrIND = "Provider=SQLOLEDB.1;Password=sas;Persist Security Info=True;User ID=sa;Initial Catalog=triangle;Data Source=ETERNATEC1"

        'connstrTZ = "Provider=SQLOLEDB.1;Password=sas;Persist Security Info=True;User ID=sa;Initial Catalog=RedDotTanzania;Data Source=zayah"
        'connstrEPZ = "Provider=SQLOLEDB.1;Password=sas;Persist Security Info=True;User ID=sa;Initial Catalog=RED DOT DISTRIBUTION EPZ LTD;Data Source=zayah"
        'connstrKE = "Provider=SQLOLEDB.1;Password=sas;Persist Security Info=True;User ID=sa;Initial Catalog=Red Dot Distribution Limited - Kenya;Data Source=zayah"

        LocTz = "DAR"
        LocKE = "NBO"
        'frmMain.Show()
    End Sub

    Protected Sub Page_Load(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.Load
        cmdAdd.Attributes.Add("onClick", "return getConfirmation();")

        lblError.Text = ""
        If (IsPostBack <> True) Then
        End If

        constr = myGlobal.getConnectionStringForDB("EVO")

        lblmsgPriceListMatrix.Text = "Please supply Numeric values to Price List Matrix"
        MSFlexGrid1.Enabled = True

    End Sub

    Protected Sub cmdConnectDb_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles cmdConnectDb.Click

        'constr = myGlobal.getConnectionStringForDB("JA")

        ResetFieldsAll()
        load_groups()
        load_location()
        load_packcode()
        FillGrid()

        cmdConnectDb.Enabled = False
        lblMsg.Text = "Database Connected .... Now Fill in the Stock Item Form and click 'Add Stock Item' button"
        cmdAdd.Enabled = True

        Call filldashboardcatagory()

        Exit Sub

    End Sub


    Protected Sub cmdAdd_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles cmdAdd.Click
        Try
            lblInfo.Text = ""

            If Not CheckCreateInDU.Checked And Not CheckCreateInTZ.Checked And Not CheckCreateInKE.Checked And Not CheckCreateInEPZ.Checked And Not CheckCreateInUG.Checked Then
                lblInfo.Text = "Error ! Please select at least one database to create part no."
                'Message.Show(Me, lblInfo.Text)
                Exit Sub
            End If

            If chkReconcile.Checked = True Then
                recon = "Reconcile"
            Else
                recon = "Do_Not_Reconcile"
            End If
            If chkActive.Checked = True Then
                active = "True"
            Else
                active = "False"
            End If

            If chkCommission.Checked = True Then
                commissionable = "True"
            Else
                commissionable = "False"
            End If

            If chkStrictSerial.Checked = True Then
                strictSerial = "True"
            Else
                strictSerial = "False"
            End If

            If chkAllowDupSerial.Checked = True Then
                allowDup = "True"
            Else
                allowDup = "False"
            End If
            If chkSerial.Checked = True Then
                serialnum = "True"
            Else
                serialnum = "False"
            End If

            If chkService.Checked = True Then
                serviceItem = "True"
            Else
                serviceItem = "False"
            End If
            If chkWarehouse.Checked = True Then
                WarehouseItem = "True"
            Else
                WarehouseItem = "False"
            End If

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

            If txtSimpleCode.Text = "" Or cmbGroup.SelectedItem.Text = "" Or cmbPack.SelectedItem.Text = "" Or cmbLocation.SelectedItem.Text = "" Then
                lblError.Text = "One or more fields left blank!!! Either Simple Code/Group/Pack/ or Bin location is left blank. Make sure these 4 textboxes are not blank."
                'Message.Show(Me, lblError.Text)
                Exit Sub
            End If
            If cmbCatTRI.SelectedItem.Text = "" Then
                lblError.Text = "One or more fields left blank!!!  Error! Select Category from segments to proceed."
                'Message.Show(Me, lblError.Text)
                Exit Sub
            End If

            If Not validateCreatePriceListParameter() Then
                Exit Sub
            End If

            If Not Util.isValidDecimalNumber(txtAvgCost.Text) Then
                lblError.Text = "Invalid Value! Field Average Cost, Please supply a valid numeric value."
                'Message.Show(Me, lblError.Text)
                Exit Sub
            End If
            If Convert.ToInt32(txtAvgCost.Text <= 0) Then
                lblError.Text = "Invalid Value! Field Average Cost, Please supply a valid numeric value greater than Zero"
                'Message.Show(Me, lblError.Text)
                Exit Sub
            End If


            If Not Util.isValidDecimalNumber(txtGRVCost.Text) Then
                lblError.Text = "Invalid Value! Field GRV Cost , Please supply a valid numeric value."
                'Message.Show(Me, lblError.Text)
                Exit Sub
            End If
            If Convert.ToInt32(txtGRVCost.Text <= 0) Then
                lblError.Text = "Invalid Value! Field GRV Cost, Please supply a valid numeric value greater than Zero"
                'Message.Show(Me, lblError.Text)
                Exit Sub
            End If

            If Not Util.isValidNumber(txtReOrderLevel.Text) Then
                lblError.Text = "Invalid Value! Field ReOrder Level, Please supply a valid numeric value."
                'Message.Show(Me, lblError.Text)
                Exit Sub
            End If
            If Convert.ToInt32(txtReOrderLevel.Text < 0) Then
                lblError.Text = "Invalid Value! Field ReOrder Level, Please supply a valid numeric value greater than or Equal to Zero"
                'Message.Show(Me, lblError.Text)
                Exit Sub
            End If

            If Not Util.isValidNumber(txtReOrderQty.Text) Then
                lblError.Text = "Invalid Value! Field ReOrder Quantity, Please supply a valid numeric value."
                'Message.Show(Me, lblError.Text)
                Exit Sub
            End If
            If Convert.ToInt32(txtReOrderQty.Text < 0) Then
                lblError.Text = "Invalid Value! Field ReOrder Quantity, Please supply a valid numeric value greater than or Equal to Zero"
                'Message.Show(Me, lblError.Text)
                Exit Sub
            End If

            If Not Util.isValidNumber(txtMinLevel.Text) Then
                lblError.Text = "Invalid Value! Field Minimum level, Please supply a valid numeric value."
                'Message.Show(Me, lblError.Text)
                Exit Sub
            End If
            If Convert.ToInt32(txtMinLevel.Text < 0) Then
                lblError.Text = "Invalid Value! Field Minimum level, Please supply a valid numeric value greater than or Equal to Zero"
                'Message.Show(Me, lblError.Text)
                Exit Sub
            End If

            If Not Util.isValidNumber(txtMaxLevel.Text) Then
                lblError.Text = "Invalid Value! Field Maximum level, Please supply a valid numeric value."
                'Message.Show(Me, lblError.Text)
                Exit Sub
            End If
            If Convert.ToInt32(txtMaxLevel.Text < 0) Then
                lblError.Text = "Invalid Value! Field Maximum level, Please supply a valid numeric value greater than or Equal to Zero"
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
                lblError.Text = "Severs connection failed...Error! One of the server connection failed, So Part no. not created yet in any database , Retry little later"
                'Message.Show(Me, lblError.Text)
                Exit Sub
            End If

            '------------------------------

            lblInfo.Text = "Please wait while AddOnce Stocks is creating your stock Item..."



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

            'If 'Message.Show(Me, Do you want to proceed creating stock Item: '" & lblUniqueDesc.Text & "'", vbQuestion + vbYesNo, "Create confirmation...") = vbYes Then

            'End If

            'check for the existence of stkitem in the database

            partInTZ = partInTRI = partInKE = partInEPZ = partInUG = 0

            'If doesItemExist() = True Then
            '    Exit Sub
            'End If

            If getItemExistence() Then
                Exit Sub
            End If

            '''''Set rsAddnewCmd = New ADODB.Command
            'Set rsAddnewCmd.ActiveConnection = connection

            rsAddnewCmd.CommandType = Data.CommandType.StoredProcedure

            If chkSeg1.Checked = True Then
                rsAddnewCmd.Parameters.Add(New SqlParameter("@cvalue1", Data.SqlDbType.VarChar, 50)).Value = cmbCatTRI.SelectedItem.Text
            Else
                rsAddnewCmd.Parameters.Add(New SqlParameter("@cvalue1", Data.SqlDbType.VarChar, 50)).Value = ""
            End If

            If chkSeg2.Checked = True Then
                rsAddnewCmd.Parameters.Add(New SqlParameter("@cvalue2", Data.SqlDbType.VarChar, 50)).Value = cmbManTRI.SelectedItem.Text
            Else
                rsAddnewCmd.Parameters.Add(New SqlParameter("@cvalue2", Data.SqlDbType.VarChar, 50)).Value = ""
            End If

            If chkSeg3.Checked = True Then
                rsAddnewCmd.Parameters.Add(New SqlParameter("@cvalue3", Data.SqlDbType.VarChar, 50)).Value = cmbBUTRI.SelectedItem.Text
            Else
                rsAddnewCmd.Parameters.Add(New SqlParameter("@cvalue3", Data.SqlDbType.VarChar, 50)).Value = ""
            End If

            If chkSeg4.Checked = True Then
                If (chkNewPL.Checked = False) Then
                    rsAddnewCmd.Parameters.Add(New SqlParameter("@cvalue4", Data.SqlDbType.VarChar, 50)).Value = cmbPLTRI.SelectedItem.Text
                Else
                    rsAddnewCmd.Parameters.Add(New SqlParameter("@cvalue4", Data.SqlDbType.VarChar, 50)).Value = txtPLTRI.Text.ToUpper
                End If
            Else
                rsAddnewCmd.Parameters.Add(New SqlParameter("@cvalue4", Data.SqlDbType.VarChar, 50)).Value = ""
            End If

            If chkSeg5.Checked = True Then
                If (chkNewModel.Checked = False) Then
                    rsAddnewCmd.Parameters.Add(New SqlParameter("@cvalue5", Data.SqlDbType.VarChar, 50)).Value = cmbModelTRI.SelectedItem.Text
                Else
                    rsAddnewCmd.Parameters.Add(New SqlParameter("@cvalue5", Data.SqlDbType.VarChar, 50)).Value = txtModelTRI.Text.ToUpper
                End If
            Else
                rsAddnewCmd.Parameters.Add(New SqlParameter("@cvalue5", Data.SqlDbType.VarChar, 50)).Value = ""
            End If

            If chkSeg6.Checked = True Then
                rsAddnewCmd.Parameters.Add(New SqlParameter("@cvalue6", Data.SqlDbType.VarChar, 50)).Value = cmbPartTRI.SelectedItem.Text
            Else
                rsAddnewCmd.Parameters.Add(New SqlParameter("@cvalue6", Data.SqlDbType.VarChar, 50)).Value = ""
            End If

            rsAddnewCmd.Parameters.Add(New SqlParameter("@code", Data.SqlDbType.VarChar, 255)).Value = lblUniqueDesc.Text

            rsAddnewCmd.Parameters.Add(New SqlParameter("@Description_1", Data.SqlDbType.VarChar, 50)).Value = txtDesc1.Text

            rsAddnewCmd.Parameters.Add(New SqlParameter("@Description_2", Data.SqlDbType.VarChar, 50)).Value = txtDesc2.Text

            rsAddnewCmd.Parameters.Add(New SqlParameter("@Description_3", Data.SqlDbType.VarChar, 50)).Value = txtDesc3.Text

            rsAddnewCmd.Parameters.Add(New SqlParameter("@ItemGroup", Data.SqlDbType.VarChar, 50)).Value = cmbGroup.SelectedItem.Text

            rsAddnewCmd.Parameters.Add(New SqlParameter("@Pack", Data.SqlDbType.VarChar, 50)).Value = cmbPack.SelectedItem.Text

            rsAddnewCmd.Parameters.Add(New SqlParameter("@Re_Ord_Lvl", Data.SqlDbType.Float, 50)).Value = Convert.ToDouble(txtReOrderLevel.Text)

            rsAddnewCmd.Parameters.Add(New SqlParameter("@Re_Ord_Qty", Data.SqlDbType.Float, 50)).Value = Convert.ToDouble(txtReOrderQty.Text)

            rsAddnewCmd.Parameters.Add(New SqlParameter("@Min_Lvl", Data.SqlDbType.Float, 50)).Value = Convert.ToDouble(txtMinLevel.Text)

            rsAddnewCmd.Parameters.Add(New SqlParameter("@Max_Lvl", Data.SqlDbType.Float, 50)).Value = Convert.ToDouble(txtMaxLevel.Text)

            rsAddnewCmd.Parameters.Add(New SqlParameter("@AveUCst", Data.SqlDbType.Float, 50)).Value = Convert.ToDouble(txtAvgCost.Text)

            rsAddnewCmd.Parameters.Add(New SqlParameter("@LatUCst", Data.SqlDbType.Float, 50)).Value = Convert.ToDouble(txtGRVCost.Text)

            rsAddnewCmd.Parameters.Add(New SqlParameter("@LowUCst", Data.SqlDbType.Float, 50)).Value = Convert.ToDouble(txtAvgCost.Text)

            rsAddnewCmd.Parameters.Add(New SqlParameter("@HigUCst", Data.SqlDbType.Float, 50)).Value = Convert.ToDouble(txtAvgCost.Text)

            rsAddnewCmd.Parameters.Add(New SqlParameter("@StdUCst", Data.SqlDbType.Float, 50)).Value = Convert.ToDouble(txtAvgCost.Text)

            rsAddnewCmd.Parameters.Add(New SqlParameter("@ServiceItem", Data.SqlDbType.Bit, 50)).Value = Convert.ToBoolean(serviceItem)

            rsAddnewCmd.Parameters.Add(New SqlParameter("@ItemActive", Data.SqlDbType.Bit, 50)).Value = Convert.ToBoolean(active)

            rsAddnewCmd.Parameters.Add(New SqlParameter("@WhseItem", Data.SqlDbType.Bit, 50)).Value = Convert.ToBoolean(WarehouseItem)

            rsAddnewCmd.Parameters.Add(New SqlParameter("@SerialItem", Data.SqlDbType.Bit, 50)).Value = Convert.ToBoolean(serialnum)

            rsAddnewCmd.Parameters.Add(New SqlParameter("@DuplicateSN", Data.SqlDbType.Bit, 50)).Value = Convert.ToBoolean(allowDup)

            rsAddnewCmd.Parameters.Add(New SqlParameter("@StrictSN", Data.SqlDbType.Bit, 50)).Value = Convert.ToBoolean(strictSerial)

            rsAddnewCmd.Parameters.Add(New SqlParameter("@iBinLocationID", Data.SqlDbType.Int)).Value = BinTRI

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

            rsAddnewCmd.Parameters.Add(New SqlParameter("@cExtDescription", Data.SqlDbType.VarChar, 255)).Value = lblUniqueDesc.Text
            rsAddnewCmd.Parameters.Add(New SqlParameter("@cSimpleCode", Data.SqlDbType.VarChar, 20)).Value = txtSimpleCode.Text
            rsAddnewCmd.Parameters.Add(New SqlParameter("@bCommissionItem", Data.SqlDbType.Bit, 20)).Value = Convert.ToBoolean(commissionable)
            rsAddnewCmd.Parameters.Add(New SqlParameter("@bLotItem", Data.SqlDbType.Bit)).Value = Convert.ToBoolean(False)
            rsAddnewCmd.Parameters.Add(New SqlParameter("@fItemLastGRVCost", Data.SqlDbType.Float)).Value = Convert.ToDouble(txtGRVCost.Text)
            rsAddnewCmd.Parameters.Add(New SqlParameter("@ulIIReconcile", Data.SqlDbType.VarChar, 100)).Value = recon
            rsAddnewCmd.Parameters.Add(New SqlParameter("@ulIIdashboardCategory", Data.SqlDbType.VarChar, 100)).Value = cmbDashCategory.SelectedItem.Text

            ''''''''''''''' 'Below is for Adding Item in warehouse(WhseStk)'''''''''''''''''

            rsAddnewCmd.Parameters.Add(New SqlParameter("@WHStockGroup", Data.SqlDbType.VarChar, 20)).Value = cmbGroup.SelectedItem.Text
            rsAddnewCmd.Parameters.Add(New SqlParameter("@WHRe_Ord_Lvl", Data.SqlDbType.Float)).Value = Convert.ToDouble(txtReOrderLevel.Text)
            rsAddnewCmd.Parameters.Add(New SqlParameter("@WHRe_Ord_Qty", Data.SqlDbType.Float)).Value = Convert.ToDouble(txtReOrderQty.Text)
            rsAddnewCmd.Parameters.Add(New SqlParameter("@WHMin_Lvl", Data.SqlDbType.Float)).Value = Convert.ToDouble(txtMinLevel.Text)
            rsAddnewCmd.Parameters.Add(New SqlParameter("@WHMax_Lvl", Data.SqlDbType.Float)).Value = Convert.ToDouble(txtMaxLevel.Text)
            rsAddnewCmd.Parameters.Add(New SqlParameter("@WHUsePriceDefs", Data.SqlDbType.Bit)).Value = Convert.ToBoolean(False)
            rsAddnewCmd.Parameters.Add(New SqlParameter("@WHUseInfoDefs", Data.SqlDbType.Bit)).Value = Convert.ToBoolean(True)
            rsAddnewCmd.Parameters.Add(New SqlParameter("@WHUseOrderDefs", Data.SqlDbType.Bit)).Value = Convert.ToBoolean(True)
            rsAddnewCmd.Parameters.Add(New SqlParameter("@WHUseDefaultDefs", Data.SqlDbType.Bit)).Value = Convert.ToBoolean(True)
            rsAddnewCmd.Parameters.Add(New SqlParameter("@WHPackCode", Data.SqlDbType.VarChar, 5)).Value = cmbPack.SelectedItem.Text
            rsAddnewCmd.Parameters.Add(New SqlParameter("@WHUseSupplierDefs", Data.SqlDbType.Bit)).Value = Convert.ToBoolean(True)
            rsAddnewCmd.Parameters.Add(New SqlParameter("@fAverageCost", Data.SqlDbType.Float)).Value = Convert.ToDouble(txtAvgCost.Text)
            rsAddnewCmd.Parameters.Add(New SqlParameter("@fLatestCost", Data.SqlDbType.Float)).Value = Convert.ToDouble(txtGRVCost.Text)
            rsAddnewCmd.Parameters.Add(New SqlParameter("@fLowestCost", Data.SqlDbType.Float)).Value = Convert.ToDouble(txtAvgCost.Text)
            rsAddnewCmd.Parameters.Add(New SqlParameter("@fHighestCost", Data.SqlDbType.Float)).Value = Convert.ToDouble(txtAvgCost.Text)
            rsAddnewCmd.Parameters.Add(New SqlParameter("@fManualCost", Data.SqlDbType.Float)).Value = Convert.ToDouble(txtAvgCost.Text)
            rsAddnewCmd.Parameters.Add(New SqlParameter("@fWhseLastGRVCost", Data.SqlDbType.Float)).Value = Convert.ToDouble(txtGRVCost.Text)
            rsAddnewCmd.Parameters.Add(New SqlParameter("@bUseMarkup", Data.SqlDbType.Int)).Value = 1
            rsAddnewCmd.Parameters.Add(New SqlParameter("@priceListStr", Data.SqlDbType.VarChar, 255)).Value = priceListStr

            'new addition on 28-feb-2012 for pricelist option, created a parameter in 5 procedures for the same true/false 1/0
            If chkPriceList.Checked = True Then
                rsAddnewCmd.Parameters.Add(New SqlParameter("@usePriceListYesNo", Data.SqlDbType.Int)).Value = 1
            Else
                rsAddnewCmd.Parameters.Add(New SqlParameter("@usePriceListYesNo", Data.SqlDbType.Int)).Value = 0
            End If
            '''''''''''''

            rsAddnewCmd.Parameters.Add(New SqlParameter("@createdBy", Data.SqlDbType.VarChar, 100)).Value = lblVersionNo.Text

            If CheckCreateInKE.Checked Then
                rsAddnewCmd.Parameters.Add("@createInKE", Data.SqlDbType.Int).Value = 1
            Else
                rsAddnewCmd.Parameters.Add("@createInKE", Data.SqlDbType.Int).Value = 0
            End If

            If CheckCreateInDU.Checked Then
                rsAddnewCmd.Parameters.Add("@createInDU", Data.SqlDbType.Int).Value = 1
            Else
                rsAddnewCmd.Parameters.Add("@createInDU", Data.SqlDbType.Int).Value = 0
            End If

            If CheckCreateInEPZ.Checked Then
                rsAddnewCmd.Parameters.Add("@createInEPZ", Data.SqlDbType.Int).Value = 1
            Else
                rsAddnewCmd.Parameters.Add("@createInEPZ", Data.SqlDbType.Int).Value = 0
            End If

            If CheckCreateInTZ.Checked Then
                rsAddnewCmd.Parameters.Add("@createInTZ", Data.SqlDbType.Int).Value = 1
            Else
                rsAddnewCmd.Parameters.Add("@createInTZ", Data.SqlDbType.Int).Value = 0
            End If

            If CheckCreateInUG.Checked Then
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
            rsAddnewCmd.CommandText = "tej.[dbo].[AddStkItemMain-EPZ-KE-UG]"
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
                rsAddnewCmd.CommandText = "tej.[dbo].[AddStkItemMain-TRI-TZ]"
                rsAddnewCmd.ExecuteNonQuery()

                resTZ = rsAddnewCmd.Parameters("@resultTZ").Value
                resDU = rsAddnewCmd.Parameters("@resultDU").Value

                If conEVO.State = 1 Then
                    conEVO.Close()
                End If

            End If


            '''''''''''''''''''''''''''''''''''''call again'''''''''''

            If resKE = 1 And CheckCreateInKE.Checked Then
                lblResKE.Text = "Created"
                lblResKE.ForeColor = Drawing.Color.Green 'RGB(0, 166, 0)
            Else
                lblResKE.Text = "Not Created"
                lblResKE.ForeColor = Drawing.Color.Red 'RGB(255, 0, 0)
            End If

            If resDU = 1 And CheckCreateInDU.Checked Then
                lblResDU.Text = "Created"
                lblResDU.ForeColor = Drawing.Color.Green 'RGB(0, 166, 0)
            Else
                lblResDU.Text = "Not Created"
                lblResDU.ForeColor = Drawing.Color.Red 'RGB(255, 0, 0)
            End If

            If resEPZ = 1 And CheckCreateInEPZ.Checked Then
                lblResEPZ.Text = "Created"
                lblResEPZ.ForeColor = Drawing.Color.Green 'RGB(0, 166, 0)
            Else
                lblResEPZ.Text = "Not Created"
                lblResEPZ.ForeColor = Drawing.Color.Red 'RGB(255, 0, 0)
            End If

            If resTZ = 1 And CheckCreateInTZ.Checked Then
                lblResTZ.Text = "Created"
                lblResTZ.ForeColor = Drawing.Color.Green 'RGB(0, 166, 0)
            Else
                lblResTZ.Text = "Not Created"
                lblResTZ.ForeColor = Drawing.Color.Red 'RGB(255, 0, 0)
            End If

            If resUG = 1 And CheckCreateInUG.Checked Then
                lblResUG.Text = "Created"
                lblResUG.ForeColor = Drawing.Color.Green 'RGB(0, 166, 0)
            Else
                lblResUG.Text = "Not Created"
                lblResUG.ForeColor = Drawing.Color.Red 'RGB(255, 0, 0)
            End If



            If resKE = 1 And resDU = 1 And resEPZ = 1 And resTZ = 1 And resUG = 1 Then
                lblInfo.Text = "Stock Item: " & "'" & lblUniqueDesc.Text & "'" & " created successfully for all the warehouses in selected countries/databases. You can create new Stock Item or Exit."
                lblInfo.ForeColor = Drawing.Color.Green
                'Message.Show(Me, lblInfo.Text)
                cmdAdd.Enabled = False
                cmdConnectDb.Text = "Connect"
                cmdConnectDb.Enabled = True
                ResetFieldsAll()
                pnlItem.Enabled = False
                pnlGrouping.Enabled = False
                pnlItemType.Enabled = False
                pnlPricing.Enabled = False
                pnlsegments.Enabled = False
                pnlMatrix.Enabled = False
            Else
                lblInfo.Text = "Stock Item: " & "'" & lblUniqueDesc.Text & "'" & " could not be created due to error occured in one of the countries/databases. Retry or Please consult database administrator."
                lblInfo.ForeColor = Drawing.Color.Red
                'Message.Show(Me, lblInfo.Text)
            End If
        Catch ex As Exception
            lblError.Text = "Error : " + ex.Message
        End Try
    End Sub

    'Protected Sub cmdClear_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles cmdClear.Click
    '    ResetFieldsAll()
    '    pnlsegments.Enabled = False
    'End Sub

    Protected Sub chkActive_CheckedChanged(ByVal sender As Object, ByVal e As System.EventArgs) Handles chkActive.CheckedChanged
        If chkActive.Checked = True Then
            active = "True"
        Else
            active = "False"
        End If
    End Sub

    Protected Sub chkAllowDupSerial_CheckedChanged(ByVal sender As Object, ByVal e As System.EventArgs) Handles chkAllowDupSerial.CheckedChanged
        If chkAllowDupSerial.Checked = True Then
            allowDup = "True"
        Else
            allowDup = "False"
        End If
    End Sub

    Protected Sub chkCommission_CheckedChanged(ByVal sender As Object, ByVal e As System.EventArgs) Handles chkCommission.CheckedChanged
        If chkCommission.Checked = True Then
            commissionable = "True"
        Else
            commissionable = "False"
        End If
    End Sub

    Private Sub load_groups()

        query = "select * from dbo.GrpTbl"
        Db.LoadDDLsWithCon(cmbGroup, query, "StGroup", "idGrpTbl", constr)
    End Sub
    Private Sub load_location()

        query = "select * from dbo._btblBINLocation"
        Db.LoadDDLsWithCon(cmbLocation, query, "cBinLocationName", "idBinLocation", constr)
    End Sub

    Private Sub ResetFieldsAll()
        'CheckStatusDU.Checked = False
        'CheckStatusEPZ.Checked = False
        'CheckStatusTZ.Checked = False
        'CheckStatusKE.Checked = False
        'CheckStatusUG.Checked = False

        pnlItem.Enabled = True
        pnlGrouping.Enabled = True
        pnlItemType.Enabled = True
        pnlPricing.Enabled = True
        txtSimpleCode.Text = ""
        lblUniqueDesc.Text = ""
        lblUniqueDesc.Text = ""
        txtDesc1.Text = ""
        txtDesc2.Text = ""
        txtDesc3.Text = ""
        cmbGroup.ClearSelection()
        cmbLocation.ClearSelection()
        cmbPack.ClearSelection()

        txtReOrderLevel.Text = 0
        txtReOrderQty.Text = 0
        txtMinLevel.Text = 0
        txtMaxLevel.Text = 0
        chkActive.Checked = True

        chkSegment.Checked = False

        chkService.Checked = False
        chkWarehouse.Checked = True
        chkSerial.Checked = False
        chkAllowDupSerial.Checked = False
        chkStrictSerial.Checked = False
        chkCommission.Checked = False
        chkReconcile.Checked = False
        txtAvgCost.Text = 0
        txtGRVCost.Text = 0

        optAvg.Checked = True
        optLatest.Checked = False
        optManual.Checked = False

        cmbCatTRI.ClearSelection()
        cmbManTRI.ClearSelection()
        cmbBUTRI.ClearSelection()
        cmbPLTRI.ClearSelection()
        cmbModelTRI.ClearSelection()
        cmbPartTRI.ClearSelection()

        chkSeg1.Checked = True
        chkSeg2.Checked = False
        chkSeg3.Checked = False
        chkSeg4.Checked = False
        chkSeg5.Checked = False
        chkSeg6.Checked = False

        'chkPricelist1.Value = 0
        'chkPricelist2.Value = 0
        'chkPricelist3.Value = 0
        'txtMarkUp1.Text = 0
        'txtMarkUp2.Text = 0
        'txtMarkUp3.Text = 0
        'txtExcl1.Text = 0
        'txtExcl2.Text = 0
        'txtExcl3.Text = 0
        'txtIncl1.Text = 0
        'txtIncl2.Text = 0
        'txtIncl3.Text = 0

    End Sub
    Private Sub load_packcode()

        query = "select * from dbo.PckTbl"
        Db.LoadDDLsWithCon(cmbPack, query, "code", "idPckTbl", constr)
    End Sub
    Private Sub chkReconcile_Click()
        If chkReconcile.Checked = True Then
            recon = "Reconcile"
        Else
            recon = "Do_Not_Reconcile"
        End If
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

    Protected Sub chkSegment_CheckedChanged(ByVal sender As Object, ByVal e As System.EventArgs) Handles chkSegment.CheckedChanged
        If cmdConnectDb.Enabled = True Then
            'Message.Show(Me, "Please Connect to database first to proceed.")
            lblError.Text = "Please Connect to database first to proceed."
            chkSegment.Checked = False
            Exit Sub
        End If

        If chkSegment.Checked = True Then
            pnlsegments.Enabled = True
            Call fillcomboCategory()

        ElseIf chkSegment.Checked = False Then
            pnlsegments.Enabled = False

            cmbCatTRI.Enabled = False
        End If
    End Sub

    Protected Sub chkSerial_CheckedChanged(ByVal sender As Object, ByVal e As System.EventArgs) Handles chkSerial.CheckedChanged
        If chkSerial.Checked = True Then
            serialnum = "True"
        Else
            serialnum = "False"
        End If
    End Sub

    Protected Sub chkService_CheckedChanged(ByVal sender As Object, ByVal e As System.EventArgs) Handles chkService.CheckedChanged
        If chkService.Checked = True Then
            serviceItem = "True"
        Else
            serviceItem = "False"
        End If
    End Sub

    Protected Sub chkWarehouse_CheckedChanged(ByVal sender As Object, ByVal e As System.EventArgs) Handles chkWarehouse.CheckedChanged
        If chkWarehouse.Checked = True Then
            WarehouseItem = "True"
        Else
            WarehouseItem = "False"
        End If
    End Sub

    Protected Sub chkStrictSerial_CheckedChanged(ByVal sender As Object, ByVal e As System.EventArgs) Handles chkStrictSerial.CheckedChanged
        If chkStrictSerial.Checked = True Then
            strictSerial = "True"
        Else
            strictSerial = "False"
        End If
    End Sub

    Protected Sub cmbLocation_SelectedIndexChanged(ByVal sender As Object, ByVal e As System.EventArgs) Handles cmbLocation.SelectedIndexChanged
        'cmbBinID.ListIndex = cmbLocation.ListIndex
        'BinTRI = cmbBinID.Text
        BinTRI = cmbLocation.Text
    End Sub

    Public Function doesItemExist() As Boolean

        query = ("select * from dbo.stkitem where lower(Code)='" & LCase(lblUniqueDesc.Text) & "'")
        'constr = myGlobal.getConnectionStringForDB("JA")

        If (Db.myGetDS(query).Tables(0).Rows.Count > 0) Then
            'Message.Show(Me, "Warning ! Stock Item '" & lblUniqueDesc.Text & "' Already present in the database, can not create same item record ")
            lblError.Text = "Warning ! Stock Item '" & lblUniqueDesc.Text & "' Already present in the database, can not create same item record "
            doesItemExist = True
        Else
            doesItemExist = False
        End If
    End Function

    Public Function getItemExistence() As Boolean 'work here
        Dim tstr As String

        Dim drd1 As SqlDataReader
        Try
            tstr = "declare @tz int; declare @tri int; set @tz=0; set @tri=0; select top 1 @tz=stockLink  from [RedDotTanzania].[dbo].stkitem where (lower(Code) like lower('" & lblUniqueDesc.Text & "%')) or (lower('" & lblUniqueDesc.Text & "') like lower(Code+'%')) ; select top 1 @tri=stockLink from [Triangle].[dbo].stkitem where (lower(Code) like lower('" & lblUniqueDesc.Text & "%')) or (lower('" & lblUniqueDesc.Text & "') like lower(Code+'%')) ;select @tz as tz,@tri as tri ;"
            Db.constr = myGlobal.getConnectionStringForDB("EVO")
            drd1 = Db.myGetReader(tstr)

            drd1.Read()
            partInTZ = drd1("tz")
            partInTRI = drd1("tri")
            drd1.Close()

            '---------------for OB1----------------

            tstr = "declare @ke int; declare @epz int;declare @ug int;set @ke=0;set @epz=0;set @ug=0;select top 1 @ke=stockLink from [Red Dot Distribution Limited - Kenya].[dbo].stkitem where (lower(Code) like lower('" & lblUniqueDesc.Text & "%')) or (lower('" & lblUniqueDesc.Text & "') like lower(Code+'%')) ; select top 1 @epz=stockLink  from [RED DOT DISTRIBUTION EPZ LTD].[dbo].stkitem where (lower(Code) like lower('" & lblUniqueDesc.Text & "%')) or (lower('" & lblUniqueDesc.Text & "') like lower(Code+'%')) ;  select top 1 @ug=stockLink  from [UgandaKE].[dbo].stkitem where (lower(Code) like lower('" & lblUniqueDesc.Text & "%')) or (lower('" & lblUniqueDesc.Text & "') like lower(Code+'%')) ;  select @ke as ke,@epz as epz,@ug as ug ;"
            Db.constr = myGlobal.getConnectionStringForDB("OB1")
            drd1 = Db.myGetReader(tstr)

            drd1.Read()
            partInKE = drd1("ke")
            partInEPZ = drd1("epz")
            partInUG = drd1("ug")
            drd1.Close()

        Catch ex As Exception
            lblError.Text = "Error : part no. could not be verified in all databases, " + ex.Message + " Please retry."
            getItemExistence = True 'as per the condition here we have to return true to stop here
            Exit Function
        End Try

        Dim flg As Boolean
        Dim msg As String
        flg = False
        msg = "Error : Can't create Stock Item '" & lblUniqueDesc.Text & "' because prtially or exactly the same Part No. is already present in the databases : "

        If partInTZ > 0 And CheckCreateInTZ.Checked Then
            msg = msg & " TZ ,"
            flg = True
        End If

        If partInTRI > 0 And CheckCreateInDU.Checked Then
            msg = msg & " TRI ,"
            flg = True
        End If

        If partInKE > 0 And CheckCreateInKE.Checked Then
            msg = msg & " KE ,"
            flg = True
        End If

        If partInEPZ > 0 And CheckCreateInEPZ.Checked Then
            msg = msg & " EPZ ,"
            flg = True
        End If

        If partInUG > 0 And CheckCreateInUG.Checked Then
            msg = msg & " UG ,"
            flg = True
        End If

        msg = msg & " Please retry giving a different value in Simple Code field."

        If flg = True Then
            '  MsgBox(msg)
            lblInfo.Text = msg
        End If
        getItemExistence = flg

    End Function

    Public Sub fillcomboCategory()
        query = "select idInvSegValue,cValue from dbo._etblInvSegValue where iInvSegGroupID=1 order by cValue"
        Db.LoadDDLsWithCon(cmbCatTRI, query, "cValue", "idInvSegValue", constr)
        cmbCatTRI.Enabled = True

        If (cmbCatTRI.Items.Count > 0) Then
            cmbCatTRI.SelectedIndex = 0
            manLoad()
        Else
            cmbManTRI.Items.Clear()
            cmbBUTRI.Items.Clear()
            cmbPLTRI.Items.Clear()
            cmbModelTRI.Items.Clear()
            cmbPartTRI.Items.Clear()
        End If

    End Sub

    Protected Sub cmbCatTRI_SelectedIndexChanged(ByVal sender As Object, ByVal e As System.EventArgs) Handles cmbCatTRI.SelectedIndexChanged
        manLoad()
    End Sub

    Protected Sub cmbManTRI_SelectedIndexChanged(ByVal sender As Object, ByVal e As System.EventArgs) Handles cmbManTRI.SelectedIndexChanged
        buLoad()
    End Sub

    Protected Sub cmbBUTRI_SelectedIndexChanged(ByVal sender As Object, ByVal e As System.EventArgs) Handles cmbBUTRI.SelectedIndexChanged
        plLoad()
    End Sub

    Protected Sub cmbPLTRI_SelectedIndexChanged(ByVal sender As Object, ByVal e As System.EventArgs) Handles cmbPLTRI.SelectedIndexChanged
        modelLoad()
    End Sub

    Protected Sub cmbModelTRI_SelectedIndexChanged(ByVal sender As Object, ByVal e As System.EventArgs) Handles cmbModelTRI.SelectedIndexChanged
        partLoad()
    End Sub

    Public Sub manLoad()
        'query = "select idInvSegValue,cValue from dbo._etblInvSegValue where iInvSegGroupID=2 and idInvSegValue in (select distinct(iInvSegValue2ID) from dbo.StkItem where iInvSegValue1ID='" & cmbCatTRI.SelectedValue & "') order by cValue"
        query = "select idInvSegValue,cValue from dbo._etblInvSegValue where iInvSegGroupID=2 order by cValue"
        Db.LoadDDLsWithCon(cmbManTRI, query, "cValue", "idInvSegValue", constr)
        If (cmbManTRI.Items.Count > 0) Then
            'cmbManTRI.Items.Insert(0, "")
            cmbManTRI.SelectedIndex = 0
            buLoad()
        Else
            cmbBUTRI.Items.Clear()
            cmbPLTRI.Items.Clear()
            cmbModelTRI.Items.Clear()
            cmbPartTRI.Items.Clear()
        End If
    End Sub

    Public Sub buLoad()
        'query = "select idInvSegValue,cValue from dbo._etblInvSegValue where iInvSegGroupID=3 and idInvSegValue in (select distinct(iInvSegValue3ID) from dbo.StkItem where iInvSegValue1ID='" & cmbCatTRI.SelectedValue & "' and iInvSegValue2ID='" & cmbManTRI.SelectedValue & "' ) order by cValue"
        query = "select idInvSegValue,cValue from dbo._etblInvSegValue where iInvSegGroupID=3 order by cValue"
        Db.LoadDDLsWithCon(cmbBUTRI, query, "cValue", "idInvSegValue", constr)
        If (cmbBUTRI.Items.Count > 0) Then
            cmbBUTRI.SelectedIndex = 0
            plLoad()
        Else
            cmbPLTRI.Items.Clear()
            cmbModelTRI.Items.Clear()
            cmbPartTRI.Items.Clear()
        End If
    End Sub

    Public Sub plLoad()
        'query = "select idInvSegValue,cValue from dbo._etblInvSegValue where iInvSegGroupID=5 and idInvSegValue in (select distinct(iInvSegValue4ID) from dbo.StkItem where iInvSegValue1ID='" & cmbCatTRI.SelectedValue & "' and iInvSegValue2ID='" & cmbManTRI.SelectedValue & "' and iInvSegValue3ID='" & cmbBUTRI.SelectedValue & "' ) order by cValue"
        query = "select idInvSegValue,cValue from dbo._etblInvSegValue where iInvSegGroupID=5 order by cValue"
        Db.LoadDDLsWithCon(cmbPLTRI, query, "cValue", "idInvSegValue", constr)
        If (cmbPLTRI.Items.Count > 0) Then
            cmbPLTRI.SelectedIndex = 0
            modelLoad()
        Else
            cmbModelTRI.Items.Clear()
            cmbPartTRI.Items.Clear()
        End If
    End Sub

    Public Sub modelLoad()
        'query = "select idInvSegValue,cValue from dbo._etblInvSegValue where iInvSegGroupID=4 and idInvSegValue in (select distinct(iInvSegValue5ID) from dbo.StkItem where iInvSegValue1ID='" & cmbCatTRI.SelectedValue & "' and iInvSegValue2ID='" & cmbManTRI.SelectedValue & "' and iInvSegValue3ID='" & cmbBUTRI.SelectedValue & "'and iInvSegValue4ID='" & cmbPLTRI.SelectedValue & " ') order by cValue"
        query = "select idInvSegValue,cValue from dbo._etblInvSegValue where iInvSegGroupID=4 order by cValue"
        Db.LoadDDLsWithCon(cmbModelTRI, query, "cValue", "idInvSegValue", constr)
        If (cmbModelTRI.Items.Count > 0) Then
            cmbModelTRI.SelectedIndex = 0
            partLoad()
        Else
            cmbPartTRI.Items.Clear()
        End If
    End Sub

    Public Sub partLoad()
        query = "select idInvSegValue,cValue from dbo._etblInvSegValue where idInvSegValue in (select distinct(iInvSegValue6ID) from dbo.StkItem where iInvSegValue1ID='" & cmbCatTRI.SelectedValue & "' and iInvSegValue2ID='" & cmbManTRI.SelectedValue & "' and iInvSegValue3ID='" & cmbBUTRI.SelectedValue & "'and iInvSegValue4ID='" & cmbPLTRI.SelectedValue & "' and iInvSegValue5ID='" & cmbModelTRI.SelectedValue & "'  ) order by cValue"
        Db.LoadDDLsWithCon(cmbPartTRI, query, "cVAlue", "idInvSegValue", constr)
        If (cmbPartTRI.Items.Count > 0) Then
            cmbPartTRI.SelectedIndex = 0
        Else
            'cmbPartTRI.Items.Add("No Item")
            'cmbPartTRI.SelectedIndex = 0
        End If
    End Sub


    Public Sub FillGrid()
        query = "SELECT cName FROM _etblPriceListName"
        'constr = myGlobal.getConnectionStringForDB("JA")
        MSFlexGrid1.DataSource = Db.myGetDS(query)
        MSFlexGrid1.DataBind()
    End Sub


    Public Function validateCreatePriceListParameter() As Boolean
        Dim val As String
        Dim R As Integer
        Dim ret As Boolean
        ret = True
        val = ""
        For R = 0 To MSFlexGrid1.Rows.Count - 1
            Dim tmpLblPriceList As Label
            Dim tmptxtMarkUp As TextBox
            Dim tmptxtExclPrice As TextBox
            Dim tmptxtInclPrice As TextBox
            tmpLblPriceList = MSFlexGrid1.Rows(R).FindControl("lblPriceList")
            tmptxtMarkUp = MSFlexGrid1.Rows(R).FindControl("txtMarkUp")
            tmptxtExclPrice = MSFlexGrid1.Rows(R).FindControl("txtExclPrice")
            tmptxtInclPrice = MSFlexGrid1.Rows(R).FindControl("txtInclPrice")

            If (IsNumeric(tmptxtExclPrice.Text) And IsNumeric(tmptxtInclPrice.Text) And IsNumeric(tmptxtMarkUp.Text)) Then
                val = tmpLblPriceList.Text & "-" & tmptxtMarkUp.Text & "-" & tmptxtExclPrice.Text & "-" & tmptxtInclPrice.Text
                If R = 0 Then
                    priceListStr = val
                Else
                    priceListStr = priceListStr & ";" & val
                End If
                ret = True
            Else
                'Message.Show(Me, "Invalid Value! Please supply a valid numeric value in Price List Grid Row:" & R)
                lblError.Text = "Invalid Value! Please supply a valid numeric value in Price List Grid Row:" & R
                priceListStr = ""
                ret = False
                Exit For
            End If
        Next
        validateCreatePriceListParameter = ret
    End Function

    Protected Sub lnkNewDash_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles lnkNewDash.Click
        lblErrorMsg.Text = ""
        pnlGrouping.Enabled = False
        pnlItem.Enabled = False
        pnlItemType.Enabled = False
        pnlMatrix.Enabled = False
        pnlNewDashCategory.Visible = True
        pnlPricing.Enabled = False
        pnlsegments.Enabled = False
        'pnlStatusItem.Enabled = False
        pnlBtn1.Enabled = False

        filldashboardcatagory1()
    End Sub

    Public Sub filldashboardcatagory()
        query = "select * from tej.[dbo].tblDashboardCategory order by dashboardCategoryName"
        Db.LoadDDLsWithCon(cmbDashCategory, query, "dashboardCategoryName", "autoindex", constr)
        If (cmbDashCategory.Items.Count <> 0) Then
            lblColumnCount.Text = "(" + cmbDashCategory.Items.Count.ToString() + ")"
        End If

    End Sub

    Protected Sub imgBtnClose_Click(ByVal sender As Object, ByVal e As System.Web.UI.ImageClickEventArgs) Handles imgBtnClose.Click
        pnlGrouping.Enabled = True
        pnlItem.Enabled = True
        pnlItemType.Enabled = True
        pnlMatrix.Enabled = True
        pnlNewDashCategory.Visible = False
        pnlPricing.Enabled = True
        pnlsegments.Enabled = True
        'pnlStatusItem.Enabled = False
        pnlBtn1.Enabled = True
        filldashboardcatagory()
    End Sub

    Public Sub filldashboardcatagory1()
        query = "select * from tej.[dbo].tblDashboardCategory order by dashboardCategoryName"
        Db.LoadDDLsWithCon(ddlDashCategory, query, "dashboardCategoryName", "autoindex", constr)
        If (ddlDashCategory.Items.Count <> 0) Then
            lblCount.Text = "(" + ddlDashCategory.Items.Count.ToString() + ")"
        End If
    End Sub

    Protected Sub btnAdd_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles btnAdd.Click
        If (txtNewCategory.Text = "") Then
            lblErrorMsg.Text = "Error!! DashBoard Category Can't be Left Empty"
            Exit Sub
        ElseIf (IsNothing(ddlDashCategory.Items.FindByText(txtNewCategory.Text.ToUpper))) Then
            query = "insert into tej.[dbo].tblDashboardCategory values('" & txtNewCategory.Text.ToUpper & "')"
            Db.myExecuteSQL(query)
            lblErrorMsg.Text = "Success!! dashboard Category Inserted Successfully"
            filldashboardcatagory1()
            txtNewCategory.Text = ""
        Else
            lblErrorMsg.Text = "Error!! Can't Insert value, Same dashboard Category exists"
            Exit Sub
        End If

        
    End Sub

    Protected Sub btnDelete_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles btnDelete.Click
        query = "delete from tej.[dbo].tblDashboardCategory where autoindex=" & ddlDashCategory.SelectedValue & ""
        Db.myExecuteSQL(query)
        lblErrorMsg.Text = "Success!! Selected dashboard Category Deleted Successfully"
        filldashboardcatagory1()
    End Sub

    'Protected Sub rdoSelect_CheckedChanged(ByVal sender As Object, ByVal e As System.EventArgs) Handles rdoSelect.CheckedChanged
    '    If (cmbModelTRI.Visible = True) Then
    '        cmbModelTRI.Visible = False
    '        txtModelTRI.Visible = True
    '    Else
    '        cmbModelTRI.Visible = True
    '        txtModelTRI.Visible = False
    '    End If
    'End Sub

    'Protected Sub rdoNew_CheckedChanged(ByVal sender As Object, ByVal e As System.EventArgs) Handles rdoNew.CheckedChanged
    '    If (txtModelTRI.Visible = True) Then
    '        cmbModelTRI.Visible = True
    '        txtModelTRI.Visible = False
    '    Else
    '        cmbModelTRI.Visible = False
    '        txtModelTRI.Visible = True
    '    End If
    'End Sub

    Protected Sub chkNewModel_CheckedChanged(ByVal sender As Object, ByVal e As System.EventArgs) Handles chkNewModel.CheckedChanged
        Call handleModelFields(chkNewModel.Checked)
    End Sub

    Protected Sub chkNewPL_CheckedChanged(ByVal sender As Object, ByVal e As System.EventArgs) Handles chkNewPL.CheckedChanged
        Call handlePLFields(chkNewPL.Checked)
    End Sub

    'Private Sub handleModelFields(ByVal flg As Boolean)
    '    If flg = True Then
    '        cmbModelTRI.Visible = False
    '        txtModelTRI.Visible = True
    '        txtModelTRI.Enabled = True
    '    Else
    '        cmbModelTRI.Visible = True
    '        txtModelTRI.Visible = False
    '        txtModelTRI.Enabled = False
    '    End If
    'End Sub

    'Private Sub handlePLFields(ByVal flg As Boolean)
    '    If flg = True Then
    '        cmbPLTRI.Visible = False
    '        txtPLTRI.Visible = True
    '        txtPLTRI.Enabled = True
    '    Else
    '        cmbPLTRI.Visible = True
    '        txtPLTRI.Visible = False
    '        txtPLTRI.Enabled = False
    '    End If
    'End Sub

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
End Class