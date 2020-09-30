Imports OfficeOpenXml
Imports Microsoft.Office.Interop.Excel
Imports System.IO
Imports System.Net

Partial Class Intranet_sellOut
    Inherits System.Web.UI.Page

    Const unAssigned = "Unassigned"
    Const doubleQuote = """"

    Public nextRefresh As Date
    Public trapPivotFilterChange As Boolean

    ' Provide the connection string.
    Public strConn As String
    Public currDB As String
    Public currRegion As String
    Public exchangeRate As Object

    Public totgrv As Long
    'Public rsGRV As ADODB.Recordset

    Public currRowSupplier As Integer
    Public currRowDebtors As Integer
    Public currRowDailyReport As Integer
    Public currRowStock As Integer
    Public currRowAgedStock As Integer
    Public currRowSellIn As Integer
    Public currRowGP As Integer
    Public currRowSellOutRebate As Integer
    Public currCol As Integer
    Public fromDate As String
    Public toDate As String

    Public samsungWeekOffset As Integer
    Public itemCountSamsungTZ As Integer
    Public itemCountSamsungKE As Integer
    Public itemCountSamsungJA As Integer

    Public startParamRow As Integer
    Public refreshRate As Double
    Public exchangeDU As Double
    Public exchangeTZ As Double
    Public exchangeKE As Double
    Public exchangeEPZ As Double
    Public refreshing As Boolean
    Public refreshCount As Integer
    Public serverEVO As String
    Public userEVO As String
    Public passwordEVO As String
    Public serverOB1 As String
    Public userOB1 As String
    Public passwordOB1 As String
    Public invalidMapping As String
    Public currProgressRow As Integer
    Public currLogRow As Integer

    Dim AsOfDate As Date
    Dim TxDate As Date

    Dim currAge As Integer
    Dim prevAge As Integer

    ' Create a connection object.
    'Dim cnEVO As ADODB.Connection
    'Dim rsWH As ADODB.Recordset
    Public numWarehouses As Integer

    ' Provide the connection string.
    'Dim strConn As String
    'Dim currDB As String
    'Dim currRegion As String
    'Dim exchangeRate As Variant

    'Dim totgrv As Long
    'Dim rsGRV As ADODB.Recordset

    Public currSellInRow As Integer
    Public currSellInCol As Integer

    Public currPart As String
    Public prevPart As String
    Public currWH As Integer
    Public currAgedRow As Integer
    Public currAgedCol As Integer
    'Dim currCol As Integer
    Public errorStatus As String
    Public currSegments As String
    Public qtyOnHand As Long
    Public currQty As Long
    Public prevQty As Long
    Public prevXFRQty As Long

    Public currDescription As String
    Public currBU As String
    Public currBUDesc As String
    Public currCost As Object
    Public currValue As Object
    Public currTxType As Object
    Public currWHQty As Object
    Public currAutoIdx As Object
    Public currAuditId As Object

    Public objExcel As Object
    Public objWorkbook As Object

    Public rsGRV As ADODB.Recordset

    Public currentUser As String = myGlobal.loggedInUser
    Public getAppPath As String = Server.MapPath("~\DownloadsUploads\")
    Public templatePath As String = getAppPath & "ReportTemplates\SellOutTemplate.xlsx"
    Public fileSavePthName As String = "~\DownloadsUploads\ReportSaver\SellOutReport-" & currentUser & ".xlsx"
    Public filePath As String = getAppPath & "ReportSaver\SellOutReport-" & currentUser & ".xlsx"


    Public workbook As ExcelWorkbook
    Public worksheetSummary As ExcelWorksheet
    Public worksheetParameters As ExcelWorksheet
    Public worksheetMappings As ExcelWorksheet
    Public worksheetDailyReport As ExcelWorksheet
    Public worksheetEventLog As ExcelWorksheet
    Public worksheetAPC As ExcelWorksheet
    Public worksheetLogitech As ExcelWorksheet
    Public worksheetMicrosoft As ExcelWorksheet
    Public worksheetSamsungTZ As ExcelWorksheet
    Public worksheetSamsungKE As ExcelWorksheet
    Public worksheetSamsungETUGBRRW As ExcelWorksheet
    Public worksheetToshiba As ExcelWorksheet
    Public worksheetCustomer As ExcelWorksheet
    Public worksheetAgedStock As ExcelWorksheet
    Dim cntr As String

    Protected Sub btnGenerate_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles btnGenerate.Click
        lblErrors.Text = ""
        'check for the valid login and the permission
        Dim loggiRole As String
        loggiRole = "sellOutReporting"

        If myGlobal.isCurrentUserOnRole(loggiRole) = False Then
            Message.Show(Me, "Sorry! update permission denied to current user for this application")
            Exit Sub
        End If

        lblMsg.Text = ""
        Try

            If (txtFromDate.Text = String.Empty Or txtToDate.Text = String.Empty) Then
                'MsgBox("Please select From and To Dates", MsgBoxStyle.Information, "Message")
                Message.Show(Me, "Please select From and To Dates")
                Exit Sub
            ElseIf txtFromDate.Text > txtToDate.Text Then
                'MsgBox("To Date should be greater than From Date", MsgBoxStyle.Information, "Message")
                Message.Show(Me, "To Date should be greater than From Date")
                Exit Sub
            End If

            Dim template As New FileInfo(templatePath)
            If (template.Exists = False) Then
                'MsgBox("Template not found", MsgBoxStyle.Information, "Message")
                Message.Show(Me, "Template not found")
                Exit Sub
            End If

            lnkDwld.Visible = False

            Dim file As New FileInfo(filePath)
            If file.Exists Then
                file.Delete()
            End If

            Using xlPackage As New ExcelPackage(file, template)

                'cntr = "true"
                workbook = xlPackage.Workbook

                worksheetSummary = workbook.Worksheets("Summary")
                worksheetParameters = workbook.Worksheets("Parameters")
                worksheetMappings = workbook.Worksheets("Mappings")
                worksheetDailyReport = workbook.Worksheets("DAILY REPORT")
                worksheetEventLog = workbook.Worksheets("Eventlog")
                worksheetAPC = workbook.Worksheets("APC")
                worksheetLogitech = workbook.Worksheets("Logitech")
                worksheetMicrosoft = workbook.Worksheets("Microsoft")
                worksheetSamsungTZ = workbook.Worksheets("SamsungTZ")
                worksheetSamsungKE = workbook.Worksheets("SamsungKE")
                worksheetSamsungETUGBRRW = workbook.Worksheets("SamsungET-UG-BR-RW")
                worksheetToshiba = workbook.Worksheets("Toshiba")
                worksheetCustomer = workbook.Worksheets("Customer")

                Call refreshNow()
                xlPackage.Save()

            End Using

        Catch ex As Exception
            cntr = "false"
            'MsgBox("Error! :" & ex.Message, MsgBoxStyle.Information, "Message")
            lblErrors.Text = lblErrors.Text + "<br>Error! : " & ex.Message
            lnkDwld.Visible = False
        End Try



        If (cntr = "true") Then
            Dim fl As String
            fl = "SellOutReport-" & ddlSupplier.Text & "-" & currentUser & "-" & Now.ToString("dd-MMM-yyyy-hh-mm-ss") & ".xlsx"
            'Server.Transfer("~/downloadSellOut.aspx?file=" & fileSavePthName & "&setfileName=" & fl)
            Session("dwnldFileNamePath") = "~/downloadSellOut.aspx?file=" & fileSavePthName & "&setfileName=" & fl
            lnkDwld.Text = "Download Report " & ddlSupplier.Text
            lnkDwld.Visible = True
        End If

        'Public getAppPath As String = Server.MapPath("~\Intranet\Reporting\")
        'Public templatePath As String = getAppPath & "ReportTemplates\SellOutTemplate.xlsx"
        'Public fileName As String = "ReportSaver\SellOutReport-" & currentUser & ".xlsx"
        'Public filePath As String = getAppPath & "ReportSaver\SellOutReport-" & currentUser & ".xlsx"

        'put this code on link button

        'If (cntr = "true") Then
        '    Dim fl As String
        '    fl = "SellOutReport-" & ddlSupplier.Text & "-" & currentUser & "-" & Now.ToString("dd-MMM-yyyy-hh-mm-ss") & ".xlsx"
        '    Server.Transfer("~/downloadSellOut.aspx?file=" & fileName & "&setfileName=" & fl)
        'End If

    End Sub

    Protected Sub lnkDwld_Click(sender As Object, e As System.EventArgs) Handles lnkDwld.Click
        If Not Session("dwnldFileNamePath") Is Nothing Then
            Server.Transfer(Session("dwnldFileNamePath").ToString())
        End If

    End Sub

    Public Function checkNullValue(ByVal fld As Object) As Object
        Dim ret As Object
        If IsDBNull(fld) Then
            ret = ""
        Else
            ret = fld
        End If
        checkNullValue = ret
    End Function
    Public Function checkNullDivision(ByVal val As Object, ByVal exchange As Object) As Object
        Dim ret As Object
        If IsDBNull(val) Then
            ret = ""
        Else
            ret = val / exchange
        End If
        checkNullDivision = ret
    End Function
    Public Function checkSingleQuote(ByVal fld As String) As String
        Dim ret As String
        If IsDBNull(fld) Then
            ret = ""
        Else
            ret = fld.Replace("'", "")
        End If
        checkSingleQuote = ret
    End Function
    Public Sub refreshNow()
        

        cntr = "true"
        Call Init()
        Call showProgress("Starting manual refresh")
        worksheetParameters.Cell(8, 2).Value = Now
        worksheetParameters.Cell(9, 2).Value = 0
        refreshCount = refreshCount + 1
        'worksheetSummary.Cell(3, 5).Value = refreshCount
        Call RefreshDashboard_Click()
        'nextRefresh = Now + TimeValue(Round(worksheetParameters.Cell(76, 2) / 60) & ":" & (worksheetParameters.Cell(76, 2) - (Round(objWorkbook.worksheetParameters.Cell(76, 2) / 60) * 60)) & ":00")
    End Sub
    Public Sub init()
        Call clearProgress()

        '' ''Select Case "local"
        '' ''    Case "NX7300"
        '' ''        serverEVO = "NX7300\NX7300SQL"
        '' ''        userEVO = "sa"
        '' ''        passwordEVO = "p455w0rd"

        '' ''        serverOB1 = "NX7300\NX7300SQL"
        '' ''        userOB1 = "sa"
        '' ''        passwordOB1 = "p455w0rd"
        '' ''    Case "TZ"
        '' ''        serverEVO = "192.168.255.10"
        '' ''        userEVO = "dashboard"
        '' ''        passwordEVO = "p455w0rd"

        '' ''        serverOB1 = "192.168.255.4,3177"
        '' ''        userOB1 = "dashboard"
        '' ''        passwordOB1 = "p455w0rd"
        '' ''    Case "RDTZ"
        '' ''        serverEVO = "192.168.255.10"
        '' ''        userEVO = "dashboard"
        '' ''        passwordEVO = "p455w0rd"

        '' ''        'serverOB1 = "41.215.46.75,3177"
        '' ''        serverOB1 = "192.168.1.4,3177"
        '' ''        userOB1 = "dashboard"
        '' ''        passwordOB1 = "p455w0rd"
        '' ''    Case "RDDAPPS"
        '' ''        serverEVO = "192.168.255.10"
        '' ''        userEVO = "dashboard"
        '' ''        passwordEVO = "p455w0rd"

        '' ''        'serverOB1 = "41.215.46.75,3177"
        '' ''        serverOB1 = "192.168.1.4,3177"
        '' ''        userOB1 = "dashboard"
        '' ''        passwordOB1 = "p455w0rd"
        '' ''    Case "local"
        '' ''        serverEVO = "JESUS\GENESIS2005"
        '' ''        userEVO = "sa"
        '' ''        passwordEVO = "n3Wp455W0rD"

        '' ''        'serverOB1 = "41.215.46.75,3177"
        '' ''        serverOB1 = "JESUS\GENESIS2005"
        '' ''        userOB1 = "sa"
        '' ''        passwordOB1 = "n3Wp455W0rD"
        '' ''    Case Else
        '' ''        MsgBox("Invalid SQL server [" & serverEVO & "]", MsgBoxStyle.Information, "Message")
        '' ''        Exit Sub
        '' ''End Select

        startParamRow = 3
        exchangeDU = Convert.ToDouble(worksheetParameters.Cell(startParamRow, 2).Value)

        startParamRow = startParamRow + 1
        exchangeTZ = Convert.ToDouble(worksheetParameters.Cell(startParamRow, 2).Value)

        startParamRow = startParamRow + 1
        exchangeKE = Convert.ToDouble(worksheetParameters.Cell(startParamRow, 2).Value)

        startParamRow = startParamRow + 1
        exchangeEPZ = Convert.ToDouble(worksheetParameters.Cell(startParamRow, 2).Value)

        startParamRow = startParamRow + 1
        refreshRate = Convert.ToDouble(worksheetParameters.Cell(startParamRow, 2).Value)

        ''objWorkbook.Worksheets("Summary").Shapes("startStopRefreshButton").Characters.Text = "tej Autorefresh"

        refreshing = False
        refreshCount = 0
        worksheetParameters.Cell(9, 2).Value = 0
        'worksheetSummary.Cell(3, 2).Value = "INACTIVE"
        'worksheetSummary.Cell(3, 5).Value = refreshCount

    End Sub
    Public Sub startRefresh()
        If vbCancel = MsgBox("Previous data will be lost !!! Are you sure?", MsgBoxStyle.OkCancel, "Message") Then
            Exit Sub
        End If
        Call init()
        'objExcel.OnTime(Now + TimeValue("00:00:01"), "doRefresh")
    End Sub
    Sub showProgress(ByVal msg As String)
        Dim timestamp As DateTime
        Dim timestampStr As DateTime
        For i = 2 To 5
            'worksheetSummary.Cell(i, 7).Value = worksheetSummary.Cell(i + 1, 7).Value
        Next i
        timestamp = Now()
        timestampStr = Right("00" & Hour(timestamp), 2) & ":" & Right("00" & Minute(timestamp), 2) & ":" & Right("00" & Second(timestamp), 2)
        'worksheetSummary.Cell(6, 7).Value = Mid(timestampStr & ": " & msg, 1, 50)
        'txtStatus.Text &= Environment.NewLine & Mid(timestampStr & ": " & msg, 1, 50)
        currLogRow = currLogRow + 1
        worksheetEventLog.Cell(currLogRow, 1).Value = timestampStr
        worksheetEventLog.Cell(currLogRow, 3).Value = msg
        'RIGHT("00" & HOUR(timestamp),2)  & ":" & RIGHT("00" & MINUTE(timestamp),2) & ":" & RIGHT("00" & SECOND(timestamp),2)
    End Sub
    Sub clearProgress()
        'worksheetEventLog.Cell.ClearContents()
        currLogRow = 0
        For i = 2 To 6
            'worksheetSummary.Cell(i, 7).Value = ""
        Next i
    End Sub
    Public Sub stopRefresh()
        Call showProgress("Stopping autorefresh")
        If refreshing Then
            objExcel.OnTime(nextRefresh, "doRefresh", , False)
            refreshing = False
        Else
        End If
        'worksheetSummary.Cell(3, 2).Value = "INACTIVE"
    End Sub
    Public Sub doRefresh()
        Call showProgress("Doing autorefresh")
        worksheetParameters.Cell(8, 2).Value = Now
        refreshing = True
        'worksheetSummary.Cell(3, 2).Value = "ACTIVE"
        refreshCount = refreshCount + 1
        'worksheetSummary.Cell(3, 5).Value = refreshCount
        Call RefreshDashboard_Click()
        Call showProgress("Scheduling next refresh")
        nextRefresh = Now + TimeValue(Math.Round(worksheetParameters.Cell(6, 2).Value / 60) & ":" & (worksheetParameters.Cell(6, 2).Value - (Math.Round(worksheetParameters.Cell(6, 2).Value / 60) * 60)) & ":00")
        worksheetParameters.Cell(9, 2).Value = nextRefresh
        objExcel.OnTime(nextRefresh, "doRefresh")
    End Sub
    Sub initAPC()
        Call showProgress("Starting initAPC")
        currCol = 0
        currRowSupplier = 0
        With worksheetAPC

            '.Cells.ClearContents()

            currRowSupplier = currRowSupplier + 1

            'GS NUMBER   REPORTING PARTNER NAME  INVOICE DATE    PRODUCT SKU PRODUCT DESC    QTY SHIP TO NAME    BILL TO NAME    SHIP TO STREET1 SHIP TO STREET2 SHIP TO STREET3 SHIP TO CITY    SHIP TO STATE PROVINCE  SHIP TO POSTAL CODE BILL TO STREET1 BILL TO STREET2 BILL TO STREET3 BILL TO CITY    BILL TO STATE PROVINCE  BILL TO POSTAL CODE BILL TO COUNTRY SHIP TO COUNTRY REPORTING PARTNER COUNTRY   SERIAL NUMBER   BILL TO TAX NUMBER  PRODUCT CURRENCY    PRODUCT PRICE   Total

            currCol = currCol + 1
            .Cell(currRowSupplier, currCol).Value = "GS NUMBER"

            currCol = currCol + 1
            .Cell(currRowSupplier, currCol).Value = "REPORTING PARTNER NAME"

            currCol = currCol + 1
            .Cell(currRowSupplier, currCol).Value = "INVOICE DATE"

            currCol = currCol + 1
            .Cell(currRowSupplier, currCol).Value = "PRODUCT SKU"

            currCol = currCol + 1
            .Cell(currRowSupplier, currCol).Value = "PRODUCT DESC"

            currCol = currCol + 1
            .Cell(currRowSupplier, currCol).Value = "QTY"

            currCol = currCol + 1
            .Cell(currRowSupplier, currCol).Value = "SHIP TO NAME"

            currCol = currCol + 1
            .Cell(currRowSupplier, currCol).Value = "BILL TO NAME"

            currCol = currCol + 1
            .Cell(currRowSupplier, currCol).Value = "SHIP TO STREET1"

            currCol = currCol + 1
            .Cell(currRowSupplier, currCol).Value = "SHIP TO STREET2"

            currCol = currCol + 1
            .Cell(currRowSupplier, currCol).Value = "SHIP TO STREET3"

            currCol = currCol + 1
            .Cell(currRowSupplier, currCol).Value = "SHIP TO CITY"

            currCol = currCol + 1
            .Cell(currRowSupplier, currCol).Value = "SHIP TO STATE PROVINCE"

            currCol = currCol + 1
            .Cell(currRowSupplier, currCol).Value = "SHIP TO POSTAL CODE"

            currCol = currCol + 1
            .Cell(currRowSupplier, currCol).Value = "BILL TO STREET1"

            currCol = currCol + 1
            .Cell(currRowSupplier, currCol).Value = "BILL TO STREET2"

            currCol = currCol + 1
            .Cell(currRowSupplier, currCol).Value = "BILL TO STREET3"

            currCol = currCol + 1
            .Cell(currRowSupplier, currCol).Value = "BILL TO CITY"

            currCol = currCol + 1
            .Cell(currRowSupplier, currCol).Value = "BILL TO STATE PROVINCE"

            currCol = currCol + 1
            .Cell(currRowSupplier, currCol).Value = "BILL TO POSTAL CODE"

            currCol = currCol + 1
            .Cell(currRowSupplier, currCol).Value = "BILL TO COUNTRY"

            currCol = currCol + 1
            .Cell(currRowSupplier, currCol).Value = "SHIP TO COUNTRY"

            currCol = currCol + 1
            .Cell(currRowSupplier, currCol).Value = "REPORTING PARTNER COUNTRY"

            currCol = currCol + 1
            .Cell(currRowSupplier, currCol).Value = "SERIAL NUMBER"

            currCol = currCol + 1
            .Cell(currRowSupplier, currCol).Value = "BILL TO TAX NUMBER"

            currCol = currCol + 1
            .Cell(currRowSupplier, currCol).Value = "PRODUCT CURRENCY"

            currCol = currCol + 1
            .Cell(currRowSupplier, currCol).Value = "PRODUCT PRICE"

            currCol = currCol + 1
            .Cell(currRowSupplier, currCol).Value = "Total"

        End With
    End Sub
    Sub writeAPC(ByVal rs As ADODB.Recordset, ByVal exchange As Double)
        'Call showProgress("Starting writeAPC")
        If (rs("Id").Value <> "OInv") And (rs("Id").Value <> "Crn") Then
            Exit Sub
        End If

        currCol = 0
        With worksheetAPC

            currRowSupplier = currRowSupplier + 1

            'GS NUMBER   REPORTING PARTNER NAME  INVOICE DATE    PRODUCT SKU PRODUCT DESC    QTY SHIP TO NAME    BILL TO NAME    SHIP TO STREET1 SHIP TO STREET2 SHIP TO STREET3 SHIP TO CITY    SHIP TO STATE PROVINCE  SHIP TO POSTAL CODE BILL TO STREET1 BILL TO STREET2 BILL TO STREET3 BILL TO CITY    BILL TO STATE PROVINCE  BILL TO POSTAL CODE BILL TO COUNTRY SHIP TO COUNTRY REPORTING PARTNER COUNTRY   SERIAL NUMBER   BILL TO TAX NUMBER  PRODUCT CURRENCY    PRODUCT PRICE   Total

            currCol = currCol + 1
            .Cell(currRowSupplier, currCol).Value = "10487" '"GS NUMBER"

            currCol = currCol + 1
            .Cell(currRowSupplier, currCol).Value = "Red Dot Distribution Ltd" '"REPORTING PARTNER NAME"

            currCol = currCol + 1
            .Cell(currRowSupplier, currCol).Value = checkNullValue(rs("TxDate").Value) '"INVOICE DATE"

            currCol = currCol + 1
            .Cell(currRowSupplier, currCol).Value = checkNullValue(rs("ItemSimpleCode").Value) '"PRODUCT SKU"

            currCol = currCol + 1
            .Cell(currRowSupplier, currCol).Value = checkSingleQuote(rs("ItemDescription").Value) '"PRODUCT DESC"

            currCol = currCol + 1
            .Cell(currRowSupplier, currCol).Value = checkNullValue(rs("Quantity").Value) '"QTY"

            currCol = currCol + 1
            .Cell(currRowSupplier, currCol).Value = checkNullValue(rs("CustomerName").Value) '"SHIP TO NAME"

            currCol = currCol + 1
            .Cell(currRowSupplier, currCol).Value = checkNullValue(rs("CustomerName").Value) '"BILL TO NAME"

            currCol = currCol + 1
            .Cell(currRowSupplier, currCol).Formula = "=iferror(vlookup(""" & rs("CustomerAccount").Value & """" & ",vlookupCustomer,10,FALSE),"""")" '"SHIP TO STREET1"

            currCol = currCol + 1
            .Cell(currRowSupplier, currCol).Formula = "=$I" & currRowSupplier '"SHIP TO STREET2"

            currCol = currCol + 1
            .Cell(currRowSupplier, currCol).Formula = "=$I" & currRowSupplier '"SHIP TO STREET3"

            currCol = currCol + 1
            .Cell(currRowSupplier, currCol).Formula = "=$I" & currRowSupplier '"SHIP TO CITY"

            currCol = currCol + 1
            .Cell(currRowSupplier, currCol).Formula = "=$I" & currRowSupplier '"SHIP TO STATE PROVINCE"

            currCol = currCol + 1
            .Cell(currRowSupplier, currCol).Formula = "=$I" & currRowSupplier '"SHIP TO POSTAL CODE"

            currCol = currCol + 1
            .Cell(currRowSupplier, currCol).Formula = "=$I" & currRowSupplier '"BILL TO STREET1"

            currCol = currCol + 1
            .Cell(currRowSupplier, currCol).Formula = "=$I" & currRowSupplier 'BILL TO STREET2"

            currCol = currCol + 1
            .Cell(currRowSupplier, currCol).Formula = "=$I" & currRowSupplier '"BILL TO STREET3"

            currCol = currCol + 1
            .Cell(currRowSupplier, currCol).Formula = "=$I" & currRowSupplier '"BILL TO CITY"

            currCol = currCol + 1
            .Cell(currRowSupplier, currCol).Formula = "=$I" & currRowSupplier '"BILL TO STATE PROVINCE"

            currCol = currCol + 1
            .Cell(currRowSupplier, currCol).Formula = "=$I" & currRowSupplier '"BILL TO POSTAL CODE"

            currCol = currCol + 1
            .Cell(currRowSupplier, currCol).Formula = "=iferror(vlookup(""" & rs("CustomerAccount").Value & """" & ",vlookupCustomer,11,FALSE),"""")" '"BILL TO COUNTRY"

            currCol = currCol + 1
            .Cell(currRowSupplier, currCol).Formula = "=iferror(vlookup(""" & rs("CustomerAccount").Value & """" & ",vlookupCustomer,11,FALSE),"""")" '"SHIP TO COUNTRY"

            currCol = currCol + 1
            .Cell(currRowSupplier, currCol).Formula = "=iferror(vlookup(""" & rs("CustomerAccount").Value & """" & ",vlookupCustomer,11,FALSE),"""")" '"REPORTING PARTNER COUNTRY"

            currCol = currCol + 1
            .Cell(currRowSupplier, currCol).Value = "" '"SERIAL NUMBER"

            currCol = currCol + 1
            .Cell(currRowSupplier, currCol).Value = "" '"BILL TO TAX NUMBER"

            currCol = currCol + 1
            .Cell(currRowSupplier, currCol).Value = "USD" '"PRODUCT CURRENCY"

            currCol = currCol + 1
            If IsDBNull(rs("ActualValue").Value) = False Then
                .Cell(currRowSupplier, currCol).Value = rs("ActualValue").Value / exchange '"PRODUCT PRICE"
            End If

            currCol = currCol + 1
            If IsDBNull(rs("ActualValue").Value) = False And IsDBNull(rs("ActualQuantity").Value) = False Then
                .Cell(currRowSupplier, currCol).Value = rs("ActualValue").Value / (rs("ActualQuantity").Value * exchange) '"Total"
            End If
        End With
    End Sub

    Sub initLogitech()
        Call showProgress("Starting initLogitech")
        currCol = 0
        currRowSupplier = 0
        With worksheetLogitech

            '.Cells.ClearContents()

            currRowSupplier = currRowSupplier + 1

            'Date    Account Account Name    REGION  Item Code   DESCRIPTION  SALE EACH   SALE TOTAL      QTY

            currCol = currCol + 1
            .Cell(currRowSupplier, currCol).Value = "Date"

            currCol = currCol + 1
            .Cell(currRowSupplier, currCol).Value = "Account"

            currCol = currCol + 1
            .Cell(currRowSupplier, currCol).Value = "Account Name"

            currCol = currCol + 1
            .Cell(currRowSupplier, currCol).Value = "REGION"

            currCol = currCol + 1
            .Cell(currRowSupplier, currCol).Value = "Item Code"

            currCol = currCol + 1
            .Cell(currRowSupplier, currCol).Value = "DESCRIPTION"

            currCol = currCol + 1
            .Cell(currRowSupplier, currCol).Value = " SALE EACH "

            currCol = currCol + 1
            .Cell(currRowSupplier, currCol).Value = " SALE TOTAL "

            currCol = currCol + 1
            .Cell(currRowSupplier, currCol).Value = " QTY "


        End With

    End Sub
    Sub writeLogitech(ByVal rs As ADODB.Recordset, ByVal exchange As Double, ByVal region As String)
        'Call showProgress("Starting writeAPC")
        If (rs("Id").Value <> "OInv") And (rs("Id").Value <> "Crn") Then
            Exit Sub
        End If

        currCol = 0
        With worksheetLogitech

            currRowSupplier = currRowSupplier + 1

            'GS NUMBER   REPORTING PARTNER NAME  INVOICE DATE    PRODUCT SKU PRODUCT DESC    QTY SHIP TO NAME    BILL TO NAME    SHIP TO STREET1 SHIP TO STREET2 SHIP TO STREET3 SHIP TO CITY    SHIP TO STATE PROVINCE  SHIP TO POSTAL CODE BILL TO STREET1 BILL TO STREET2 BILL TO STREET3 BILL TO CITY    BILL TO STATE PROVINCE  BILL TO POSTAL CODE BILL TO COUNTRY SHIP TO COUNTRY REPORTING PARTNER COUNTRY   SERIAL NUMBER   BILL TO TAX NUMBER  PRODUCT CURRENCY    PRODUCT PRICE   Total

            currCol = currCol + 1
            .Cell(currRowSupplier, currCol).Value = checkNullValue(rs("TxDate").Value) '"Date"

            currCol = currCol + 1
            .Cell(currRowSupplier, currCol).Value = checkNullValue(rs("CustomerAccount").Value) '"Account"

            currCol = currCol + 1
            .Cell(currRowSupplier, currCol).Value = checkNullValue(rs("CustomerName").Value) '"Account Name"

            currCol = currCol + 1
            .Cell(currRowSupplier, currCol).Value = region '"REGION"

            currCol = currCol + 1
            .Cell(currRowSupplier, currCol).Value = checkNullValue(rs("ItemSimpleCode").Value) '"Item Code"

            currCol = currCol + 1
            .Cell(currRowSupplier, currCol).Value = checkSingleQuote(rs("ItemDescription").Value) '"DESCRIPTION"

            currCol = currCol + 1
            If IsDBNull(rs("ActualValue").Value) = False Then
                .Cell(currRowSupplier, currCol).Value = rs("ActualValue").Value / exchange '" SALE EACH "
            End If

            currCol = currCol + 1
            If IsDBNull(rs("ActualValue").Value) = False And IsDBNull(rs("Quantity").Value) = False Then
                .Cell(currRowSupplier, currCol).Value = (rs("ActualValue").Value * rs("Quantity").Value) / exchange  '" SALE TOTAL "
            End If

            currCol = currCol + 1
            .Cell(currRowSupplier, currCol).Value = checkNullValue(rs("Quantity").Value) '" QTY "

        End With
    End Sub
    Sub initMicrosoft()
        Call showProgress("Starting initMicrosoft")
        currCol = 0
        currRowSupplier = 0
        With worksheetMicrosoft

            '.Cells.ClearContents()

            currRowSupplier = currRowSupplier + 1

            'Invoice Number  Invoice Date    SB Name SB ID   SB Address Line 1   SB Address Line 2   SB Address Line 3   SB City SB State    SB Postal Code  SB Country Code MS Part Number  MS DESCRIPTION  Quantity Sold   Quantity Returned   Microsoft Agreement Number  Discount Price  Promotion Number

            currCol = currCol + 1
            .Cell(currRowSupplier, currCol).Value = "Invoice Number"

            currCol = currCol + 1
            .Cell(currRowSupplier, currCol).Value = "Invoice Date"

            currCol = currCol + 1
            .Cell(currRowSupplier, currCol).Value = "SB Name"

            currCol = currCol + 1
            .Cell(currRowSupplier, currCol).Value = "SB ID"

            currCol = currCol + 1
            .Cell(currRowSupplier, currCol).Value = "SB Address Line 1"

            currCol = currCol + 1
            .Cell(currRowSupplier, currCol).Value = "SB Address Line 2"

            currCol = currCol + 1
            .Cell(currRowSupplier, currCol).Value = "SB Address Line 3"

            currCol = currCol + 1
            .Cell(currRowSupplier, currCol).Value = "SB City"

            currCol = currCol + 1
            .Cell(currRowSupplier, currCol).Value = "SB State"

            currCol = currCol + 1
            .Cell(currRowSupplier, currCol).Value = "SB Postal Code"

            currCol = currCol + 1
            .Cell(currRowSupplier, currCol).Value = "SB Country Code"

            currCol = currCol + 1
            .Cell(currRowSupplier, currCol).Value = "MS Part Number"

            currCol = currCol + 1
            .Cell(currRowSupplier, currCol).Value = "MS DESCRIPTION"

            currCol = currCol + 1
            .Cell(currRowSupplier, currCol).Value = "Quantity Sold"

            currCol = currCol + 1
            .Cell(currRowSupplier, currCol).Value = "Quantity Returned"

            currCol = currCol + 1
            .Cell(currRowSupplier, currCol).Value = "Microsoft Agreement Number"

            currCol = currCol + 1
            .Cell(currRowSupplier, currCol).Value = "Discount Price"

            currCol = currCol + 1
            .Cell(currRowSupplier, currCol).Value = "Promotion Number"

        End With
    End Sub
    Sub writeMicrosoft(ByVal rs As ADODB.Recordset, ByVal exchange As Double)
        'Call showProgress("Starting writeAPC")
        If (rs("Id").Value <> "OInv") And (rs("Id").Value <> "Crn") Then
            Exit Sub
        End If
        currCol = 0
        With worksheetMicrosoft

            currRowSupplier = currRowSupplier + 1

            currCol = currCol + 1
            .Cell(currRowSupplier, currCol).Value = checkNullValue(rs("Reference").Value) '"Invoice Number"

            currCol = currCol + 1
            .Cell(currRowSupplier, currCol).Value = checkNullValue(rs("TxDate").Value) '"Invoice Date"

            currCol = currCol + 1
            .Cell(currRowSupplier, currCol).Value = checkNullValue(rs("CustomerName").Value) '"SB Name"

            currCol = currCol + 1
            .Cell(currRowSupplier, currCol).Value = checkNullValue(rs("CustomerAccount").Value) '"SB ID"

            currCol = currCol + 1
            .Cell(currRowSupplier, currCol).Value = 0.0# '"SB Address Line 1"

            currCol = currCol + 1
            .Cell(currRowSupplier, currCol).Formula = "=iferror(vlookup($D" & currRowSupplier & ",vlookupCustomer,4,FALSE),"""")" '"SB Address Line 2"

            currCol = currCol + 1
            .Cell(currRowSupplier, currCol).Formula = "=iferror(vlookup($D" & currRowSupplier & ",vlookupCustomer,5,FALSE),"""")" '"SB Address Line 3"

            currCol = currCol + 1
            .Cell(currRowSupplier, currCol).Formula = "=iferror(vlookup($D" & currRowSupplier & ",vlookupCustomer,6,FALSE),"""")" '"SB City"

            currCol = currCol + 1
            .Cell(currRowSupplier, currCol).Formula = "=iferror(vlookup($D" & currRowSupplier & ",vlookupCustomer,7,FALSE),"""")" '"SB State"

            currCol = currCol + 1
            .Cell(currRowSupplier, currCol).Formula = "=iferror(vlookup($D" & currRowSupplier & ",vlookupCustomer,8,FALSE),"""")" '"SB Postal Code"

            currCol = currCol + 1
            .Cell(currRowSupplier, currCol).Formula = "=iferror(vlookup($D" & currRowSupplier & ",vlookupCustomer,9,FALSE),"""")" '"SB Country Code"

            currCol = currCol + 1
            .Cell(currRowSupplier, currCol).Value = checkNullValue(rs("ItemSimpleCode").Value) '"MS Part Number"

            currCol = currCol + 1
            .Cell(currRowSupplier, currCol).Value = checkSingleQuote(rs("ItemDescription").Value) '"MS DESCRIPTION"

            If rs("Quantity").Value >= 0 Then
                currCol = currCol + 1
                .Cell(currRowSupplier, currCol).Value = checkNullValue(rs("Quantity").Value) '"Quantity Sold"

                currCol = currCol + 1
                .Cell(currRowSupplier, currCol).Value = 0 '"Quantity Returned"
            Else
                currCol = currCol + 1
                .Cell(currRowSupplier, currCol).Value = 0 '"Quantity Sold"

                currCol = currCol + 1
                .Cell(currRowSupplier, currCol).Value = checkNullValue(rs("Quantity").Value) '"Quantity Returned"
            End If

            currCol = currCol + 1
            .Cell(currRowSupplier, currCol).Value = "10897494" 'Microsoft Agreement Number"

            currCol = currCol + 1
            .Cell(currRowSupplier, currCol).Value = "" '"Discount Price"

            currCol = currCol + 1
            .Cell(currRowSupplier, currCol).Value = "" '"Promotion Number"

        End With
    End Sub
    Sub initSamsung()
        Dim rowbase As Integer
        Call showProgress("Starting initSamsung")
        currRowSupplier = 0
        With worksheetSamsungTZ
            For i = 1 To itemCountSamsungTZ
                rowbase = 4 + (i - 1) * 8
                .Cell(rowbase, samsungWeekOffset).Value = 0
                .Cell(rowbase + 1, samsungWeekOffset).Value = 0
                .Cell(rowbase + 3, samsungWeekOffset).Value = 0
                .Cell(rowbase + 4, samsungWeekOffset).Value = 0

            Next i
        End With

        With worksheetSamsungKE
            For i = 1 To itemCountSamsungKE
                rowbase = 4 + (i - 1) * 8
                .Cell(rowbase, samsungWeekOffset).Value = 0
                .Cell(rowbase + 1, samsungWeekOffset).Value = 0
                .Cell(rowbase + 3, samsungWeekOffset).Value = 0
                .Cell(rowbase + 4, samsungWeekOffset).Value = 0
            Next i
        End With

        With worksheetSamsungETUGBRRW
            For i = 1 To itemCountSamsungJA
                rowbase = 4 + (i - 1) * 8
                .Cell(rowbase, samsungWeekOffset).Value = 0
                .Cell(rowbase + 1, samsungWeekOffset).Value = 0
                .Cell(rowbase + 3, samsungWeekOffset).Value = 0
                .Cell(rowbase + 4, samsungWeekOffset).Value = 0

            Next i
        End With
    End Sub
    Sub writeSamsung(ByVal rs As ADODB.Recordset, ByVal exchange As Double, ByVal region As String, ByVal samsungWeekOffset As Integer)
        Dim suffix As String
        Dim maxItems, itemRowBase As Integer
        Dim wrksht As ExcelWorksheet
        'Call showProgress("Starting writeAPC")
        If (rs("Id").Value <> "OInv") And (rs("Id").Value <> "Crn") And (rs("Id").Value <> "OGrv") And (rs("Id").Value <> "Rts") Then
            Exit Sub
        End If

        Select Case region
            Case "TZ"
                suffix = region
                maxItems = itemCountSamsungTZ
                wrksht = worksheetSamsungTZ
            Case "KE"
                suffix = region
                maxItems = itemCountSamsungKE
                wrksht = worksheetSamsungKE
            Case Else
                suffix = "ET-UG-BR-RW"
                maxItems = itemCountSamsungJA
                wrksht = worksheetSamsungETUGBRRW
        End Select

        With wrksht
            For itemNum = 1 To maxItems
                itemRowBase = 4 + (itemNum - 1) * 8
                If .Cell(itemRowBase, 2).Value = rs("ItemSimpleCode").Value Then
                    Select Case rs("id").Value
                        Case "OInv"
                            .Cell(itemRowBase + 1, samsungWeekOffset).Value = .Cell(itemRowBase + 1, samsungWeekOffset).Value + rs("Cost").Value * rs("Quantity").Value
                            .Cell(itemRowBase + 4, samsungWeekOffset).Value = .Cell(itemRowBase + 4, samsungWeekOffset).Value + rs("Quantity").Value
                        Case "Crn"
                            .Cell(itemRowBase + 1, samsungWeekOffset).Value = .Cell(itemRowBase + 1, samsungWeekOffset).Value - rs("Cost").Value * rs("Quantity").Value
                            .Cell(itemRowBase + 4, samsungWeekOffset).Value = .Cell(itemRowBase + 4, samsungWeekOffset).Value - rs("Quantity").Value
                        Case "OGrv"
                            .Cell(itemRowBase, samsungWeekOffset).Value = .Cell(itemRowBase, samsungWeekOffset).Value + rs("Cost").Value * rs("Quantity").Value
                            .Cell(itemRowBase + 3, samsungWeekOffset).Value = .Cell(itemRowBase + 3, samsungWeekOffset).Value + rs("Quantity").Value
                        Case "Rts"
                            .Cell(itemRowBase, samsungWeekOffset).Value = .Cell(itemRowBase, samsungWeekOffset).Value - rs("Cost").Value * rs("Quantity").Value
                            .Cell(itemRowBase + 3, samsungWeekOffset).Value = .Cell(itemRowBase + 3, samsungWeekOffset).Value - rs("Quantity").Value
                    End Select
                    'MsgBox "Found : " & rs("ItemSimpleCode")

                    Exit Sub
                End If
            Next itemNum
        End With
        'MsgBox("Item Not found: [" & rs("ItemSimpleCode").Value + "]", MsgBoxStyle.Information, "Message")
        lblErrors.Text = lblErrors.Text + "<br>Error! : Item Not found: [" & rs("ItemSimpleCode").Value + "]"
    End Sub
    Sub initToshiba()
        Call showProgress("Starting initToshiba")
        currCol = 0
        currRowSupplier = 0
        With worksheetToshiba

            '.Cells.ClearContents()

            currRowSupplier = currRowSupplier + 1

            'VAT ResellerName    ResellerCountry ToshibaPartNumber   Qty WeekOfReference YearOfReference

            currCol = currCol + 1
            .Cell(currRowSupplier, currCol).Value = "VAT"

            currCol = currCol + 1
            .Cell(currRowSupplier, currCol).Value = "ResellerName"

            currCol = currCol + 1
            .Cell(currRowSupplier, currCol).Value = "ResellerCountry"

            currCol = currCol + 1
            .Cell(currRowSupplier, currCol).Value = "ToshibaPartNumber"

            currCol = currCol + 1
            .Cell(currRowSupplier, currCol).Value = "Qty"

            currCol = currCol + 1
            .Cell(currRowDailyReport, currCol).Value = "WeekOfReference"

            currCol = currCol + 1
            .Cell(currRowSupplier, currCol).Value = "YearOfReference"

        End With
    End Sub
    Sub writeToshiba(ByVal rs As ADODB.Recordset, ByVal exchange As Double)
        currCol = 0
        With worksheetToshiba

            currRowSupplier = currRowSupplier + 1

            'VAT ResellerName    ResellerCountry ToshibaPartNumber   Qty WeekOfReference YearOfReference

            currCol = currCol + 1
            .Cell(currRowSupplier, currCol).Value = checkNullValue(rs("CustomerAccount").Value) '"VAT"

            currCol = currCol + 1
            .Cell(currRowSupplier, currCol).Value = checkNullValue(rs("CustomerName").Value) '"ResellerName"

            currCol = currCol + 1
            .Cell(currRowSupplier, currCol).Formula = "=iferror(vlookup($A" & currRowSupplier & ",vlookupCustomer,11,FALSE),"""")" '"ResellerCountry"

            currCol = currCol + 1
            .Cell(currRowSupplier, currCol).Value = checkNullValue(rs("ItemSimpleCode").Value) '"ToshibaPartNumber"

            currCol = currCol + 1
            .Cell(currRowSupplier, currCol).Value = checkNullValue(rs("Quantity").Value) '"Qty"

            currCol = currCol + 1
            .Cell(currRowSupplier, currCol).Formula = "=WEEKNUM(""" & rs("TxDate").Value & """)" '"WeekOfReference"

            currCol = currCol + 1
            .Cell(currRowSupplier, currCol).Formula = "=YEAR(""" & rs("TxDate").Value & """)" '"YearOfReference"

        End With
    End Sub
    Sub initDailyReport()
        Call showProgress("Starting initDailyReport")
        currCol = 0
        currRowDailyReport = 0
        With worksheetDailyReport

            '.Cells.ClearContents()

            currRowDailyReport = currRowDailyReport + 1

            'Item Code   DESCRIPTION BIZ TYPE    Date    Tr Code Account Account Name    Document    Sup Inv No  RDD PO No
            'Group   GROUP DESCRIPTION   BU  PL  PL DESCRIPTION   SALE EACH   SALE TOTAL      QTY     Profit     Prof %   Unit Cost   TOTAL COST
            'DATA  FROM  PROJECT REGION  SALES REP   PM  MONTH   WEEK     REBATE      TOTAL COST AFT REBATE   GP USD     GP %

            currCol = currCol + 1
            .Cell(currRowDailyReport, currCol).Value = "Item Code"

            currCol = currCol + 1
            .Cell(currRowDailyReport, currCol).Value = "Description"

            currCol = currCol + 1
            .Cell(currRowDailyReport, currCol).Value = "BIZ TYPE"

            currCol = currCol + 1
            .Cell(currRowDailyReport, currCol).Value = "Date"

            currCol = currCol + 1
            .Cell(currRowDailyReport, currCol).Value = "Tr Code"

            currCol = currCol + 1
            .Cell(currRowDailyReport, currCol).Value = "Account"

            currCol = currCol + 1
            .Cell(currRowDailyReport, currCol).Value = "Account Name"

            currCol = currCol + 1
            .Cell(currRowDailyReport, currCol).Value = "Document"

            currCol = currCol + 1
            .Cell(currRowDailyReport, currCol).Value = "Sup Inv No"

            currCol = currCol + 1
            .Cell(currRowDailyReport, currCol).Value = "RDD PO No"

            currCol = currCol + 1
            .Cell(currRowDailyReport, currCol).Value = "Group"

            currCol = currCol + 1
            .Cell(currRowDailyReport, currCol).Value = "GROUP DESCRIPTION"

            currCol = currCol + 1
            .Cell(currRowDailyReport, currCol).Value = "BU"

            currCol = currCol + 1
            .Cell(currRowDailyReport, currCol).Value = "PL"

            currCol = currCol + 1
            .Cell(currRowDailyReport, currCol).Value = "PL DESCRIPTION"

            currCol = currCol + 1
            .Cell(currRowDailyReport, currCol).Value = "SALE EACH"

            currCol = currCol + 1
            .Cell(currRowDailyReport, currCol).Value = "SALE TOTAL"

            currCol = currCol + 1
            .Cell(currRowDailyReport, currCol).Value = "QTY"

            currCol = currCol + 1
            .Cell(currRowDailyReport, currCol).Value = "Profit"

            currCol = currCol + 1
            .Cell(currRowDailyReport, currCol).Value = "Prof %"

            currCol = currCol + 1
            .Cell(currRowDailyReport, currCol).Value = "Unit Cost"

            currCol = currCol + 1
            .Cell(currRowDailyReport, currCol).Value = "TOTAL COST"

            currCol = currCol + 1
            .Cell(currRowDailyReport, currCol).Value = "DATA  FROM"

            currCol = currCol + 1
            .Cell(currRowDailyReport, currCol).Value = "PROJECT"

            currCol = currCol + 1
            .Cell(currRowDailyReport, currCol).Value = "REGION"

            currCol = currCol + 1
            .Cell(currRowDailyReport, currCol).Value = "SALES REP"

            currCol = currCol + 1
            .Cell(currRowDailyReport, currCol).Value = "PM"

            currCol = currCol + 1
            .Cell(currRowDailyReport, currCol).Value = "MONTH"

            currCol = currCol + 1
            .Cell(currRowDailyReport, currCol).Value = "WEEK"

            currCol = currCol + 1
            .Cell(currRowDailyReport, currCol).Value = "REBATE"

            currCol = currCol + 1
            .Cell(currRowDailyReport, currCol).Value = "TOTAL COST AFT REBATE "

            currCol = currCol + 1
            .Cell(currRowDailyReport, currCol).Value = "GP USD"

            currCol = currCol + 1
            .Cell(currRowDailyReport, currCol).Value = "GP %"


        End With
    End Sub
    Sub getDailyReport(ByVal cn As ADODB.Connection, ByVal rs As ADODB.Recordset, ByVal sql As String, ByVal exchange As Double, ByVal region As String, ByVal supplier As String)
        Dim segs, BU, PL, currSegment As String
        Dim slashCount, prevPos As Integer
        Call showProgress("Starting getDailyReport")
        ' Assign the Connection object.
        rs.ActiveConnection = cn
        'Debug.Print(sql)
        ' Run the query
        rs.Open(sql)

        ' process the result set
        Do While Not rs.EOF
            'DoEvents

            Select Case supplier
                Case "APC"
                    Call writeAPC(rs, exchange)
                Case "Logitech"
                    Call writeLogitech(rs, exchange, region)
                Case "Microsoft"
                    Call writeMicrosoft(rs, exchange)
                Case "Samsung"
                    Call writeSamsung(rs, exchange, region, samsungWeekOffset)
                Case "Toshiba"
                    Call writeToshiba(rs, exchange)
                Case Else
                    Call criticalError("Invalid Supplier [" & supplier & "]")
                    Exit Sub
            End Select

            With worksheetDailyReport
                currCol = 0

                currRowDailyReport = currRowDailyReport + 1

                currCol = currCol + 1
                .Cell(currRowDailyReport, currCol).Value = checkNullValue(rs("ItemSimpleCode").Value)

                currCol = currCol + 1
                .Cell(currRowDailyReport, currCol).Value = checkSingleQuote(rs("ItemDescription").Value)

                currCol = currCol + 1
                .Cell(currRowDailyReport, currCol).Value = "RR"

                currCol = currCol + 1
                .Cell(currRowDailyReport, currCol).Value = checkNullValue(rs("TxDate").Value)

                currCol = currCol + 1
                .Cell(currRowDailyReport, currCol).Value = checkNullValue(rs("Id").Value)

                currCol = currCol + 1
                .Cell(currRowDailyReport, currCol).Value = checkNullValue(rs("CustomerAccount").Value)

                currCol = currCol + 1
                .Cell(currRowDailyReport, currCol).Value = checkNullValue(rs("CustomerName").Value)

                currCol = currCol + 1
                .Cell(currRowDailyReport, currCol).Value = checkNullValue(rs("Reference").Value)

                currCol = currCol + 1
                .Cell(currRowDailyReport, currCol).Value = checkNullValue(rs("Order_no").Value)

                currCol = currCol + 1
                .Cell(currRowDailyReport, currCol).Value = checkNullValue(rs("ExtOrderNum").Value)

                currCol = currCol + 1
                .Cell(currRowDailyReport, currCol).Value = checkNullValue(rs("ItemGroup").Value)

                currCol = currCol + 1
                .Cell(currRowDailyReport, currCol).Value = checkNullValue(rs("ItemGroupDescription").Value)

                segs = rs("ItemCode").Value
                slashCount = 0
                prevPos = 0
                BU = ""
                PL = ""
                For i = 1 To Len(segs)
                    If Mid(segs, i, 1) = "/" Then
                        currSegment = Mid(segs, prevPos + 1, i - prevPos - 1)
                        prevPos = i
                        slashCount = slashCount + 1
                        If slashCount = 4 Then
                            BU = currSegment
                        Else
                            If slashCount = 6 Then
                                PL = currSegment
                            End If
                        End If

                    Else
                    End If
                Next i
                currSegment = Mid(segs, prevPos + 1)
                'If PL = "" Then PL = currSegment

                currCol = currCol + 1
                .Cell(currRowDailyReport, currCol).Value = BU


                currCol = currCol + 1
                .Cell(currRowDailyReport, currCol).Value = PL

                currCol = currCol + 1
                .Cell(currRowDailyReport, currCol).Value = "PL DESCRIPTION"

                currCol = currCol + 1
                If IsDBNull(rs("ActualValue").Value) = False And IsDBNull(rs("ActualQuantity").Value) = False Then
                    If rs("ActualQuantity").Value <> 0 Then
                        .Cell(currRowDailyReport, currCol).Value = rs("ActualValue").Value / (rs("ActualQuantity").Value * exchange)
                    Else
                        'MsgBox("Row# " & currRowDailyReport & ": ActualQuantity = 0", MsgBoxStyle.Information, "Message")
                        lblErrors.Text = lblErrors.Text + "<br>Error! : Row# " & currRowDailyReport & ": ActualQuantity = 0"
                        .Cell(currRowDailyReport, currCol).Value = 0
                    End If
                End If
                currCol = currCol + 1
                .Cell(currRowDailyReport, currCol).Value = checkNullDivision(rs("ActualValue").Value, exchange)

                currCol = currCol + 1
                .Cell(currRowDailyReport, currCol).Value = checkNullValue(rs("ActualQuantity").Value)

                currCol = currCol + 1
                If IsDBNull(rs("Profit").Value) = False Then
                    .Cell(currRowDailyReport, currCol).Value = checkNullDivision(rs("Profit").Value, exchange)
                End If

                currCol = currCol + 1
                If IsDBNull(rs("ProfitPerc").Value) = False Then
                    .Cell(currRowDailyReport, currCol).Value = checkNullValue(rs("ProfitPerc").Value)
                End If

                currCol = currCol + 1
                .Cell(currRowDailyReport, currCol).Value = checkNullDivision(rs("Cost").Value, exchange)

                currCol = currCol + 1
                If IsDBNull(rs("Cost").Value) = False And IsDBNull(rs("Quantity").Value) = False Then
                    .Cell(currRowDailyReport, currCol).Value = (rs("Cost").Value * rs("Quantity").Value) / exchange
                End If

                currCol = currCol + 1
                .Cell(currRowDailyReport, currCol).Value = region

                currCol = currCol + 1
                .Cell(currRowDailyReport, currCol).Formula = "=IFERROR(VLOOKUP($W" & currRowDailyReport & " & ""."" & " & rs("Project").Value & ",vlookupProject,2,FALSE)," & invalidMapping & ")"

                currCol = currCol + 1
                .Cell(currRowDailyReport, currCol).Formula = "=IFERROR(VLOOKUP($W" & currRowDailyReport & " & ""."" & " & rs("Project").Value & ",vlookupProject,11,FALSE)," & invalidMapping & ")"

                currCol = currCol + 1
                .Cell(currRowDailyReport, currCol).Value = checkNullValue(rs("RepCode").Value)

                currCol = currCol + 1
                .Cell(currRowDailyReport, currCol).Formula = "=IFERROR(VLOOKUP($K" & currRowDailyReport & ",vlookupPM,2,FALSE)," & invalidMapping & ")"

                currCol = currCol + 1
                .Cell(currRowDailyReport, currCol).Formula = "=IF(MOD(MONTH($D" & currRowDailyReport & "),3)=0,3,MOD(MONTH($D" & currRowDailyReport & "),3))" '"=MONTH($D" & currRowDailyReport & ")"

                currCol = currCol + 1
                .Cell(currRowDailyReport, currCol).Formula = "=WEEKNUM($D" & currRowDailyReport & ")"

                currCol = currCol + 1
                .Cell(currRowDailyReport, currCol).Value = "0" 'rebate

                currCol = currCol + 1
                .Cell(currRowDailyReport, currCol).Formula = "=$U" & currRowDailyReport & "- $AD" & currRowDailyReport

                currCol = currCol + 1
                .Cell(currRowDailyReport, currCol).Formula = "=($AD" & currRowDailyReport & "* $R" & currRowDailyReport & ")+$S" & currRowDailyReport

                currCol = currCol + 1
                .Cell(currRowDailyReport, currCol).Formula = "=IFERROR(($AF" & currRowDailyReport & "/$V" & currRowDailyReport & "),0)"

                'Item Code   DESCRIPTION BIZ TYPE    Date    Tr Code Account Account Name    Document    Sup Inv No  RDD PO No
                'Group   GROUP DESCRIPTION   BU  PL  PL DESCRIPTION   SALE EACH   SALE TOTAL      QTY     Profit     Prof %   Unit Cost   TOTAL COST
                'DATA  FROM  PROJECT REGION  SALES REP   PM  MONTH   WEEK     REBATE      TOTAL COST AFT REBATE   GP USD     GP %

            End With
nextRec:


            rs.MoveNext()
        Loop
        rs.Close()
        Call showProgress("Finishing getSales")
    End Sub
    
    Public Sub RefreshDashboard_Click()
        Dim samsungStartYear, samsungStartWeek, samsungRemainingWeeks, samsungReportingYear, samsungReportingWeek As Integer
        Dim samsungReportingWeekLabel As String
        'Dim appExcel As Excel.Application
        'Dim NewBook As Excel.Workbook
        ' Create a connection object.

        'Call exportFile(worksheetParameters.Cell(17, 2))
        'Exit Sub
        'GoTo refreshAll
       

        Dim supplier As String



        ''''''Dim cnOB1 As ADODB.Connection


        Dim rs As ADODB.Recordset
        rs = New ADODB.Recordset

        Call showProgress("Starting refresh")

        trapPivotFilterChange = False
        'Call updateAllPivotFilters 'reset according to whatever is selected by user


        'invalidMapping = """n/a"""
        invalidMapping = doubleQuote + unAssigned + doubleQuote

        workbook.CalcMode = ExcelCalcMode.Manual

        'supplier = worksheetSummary.Cell(5, 3)
        'fromDate = worksheetParameters.Cell(29, 2).value
        'toDate = worksheetParameters.Cell(30, 2).value

        'supplier = "Toshiba"
        'fromDate = "2011-01-01"
        'toDate = "2011-02-01"

        supplier = ddlSupplier.SelectedItem.Text
        fromDate = txtFromDate.Text
        toDate = txtToDate.Text

        lblMsg.Text = "Report Generation begin at " + Now.ToString("hh:mm:ss tt")
        lblErrors.Text = ""

        worksheetSummary.Cell(3, 3).Value = supplier
        worksheetSummary.Cell(4, 3).Value = fromDate
        worksheetSummary.Cell(5, 3).Value = toDate
        worksheetSummary.Cell(6, 3).Value = Date.Now.Date
        worksheetParameters.Cell(29, 2).Value = fromDate
        worksheetParameters.Cell(30, 2).Value = toDate

        samsungStartYear = 2010
        samsungStartWeek = 40
        samsungRemainingWeeks = 52 - samsungStartWeek + 1
        samsungReportingYear = worksheetParameters.Cell(31, 2).value
        samsungReportingWeek = worksheetParameters.Cell(32, 2).value
        samsungWeekOffset = 6 + samsungRemainingWeeks + (((samsungReportingYear - samsungStartYear) - 1) * 52) + samsungReportingWeek - 1
        samsungReportingWeekLabel = worksheetSamsungTZ.Cell(3, samsungWeekOffset).Value

        If supplier = "Samsung" Then

            'determine # rows in each samsung worksheet
            Call findItemCountSamsung(itemCountSamsungTZ, "TZ")
            Call findItemCountSamsung(itemCountSamsungKE, "KE")
            Call findItemCountSamsung(itemCountSamsungJA, "ET-UG-BR-RW")
        End If

        Call initDailyReport()

        'init Supplier sheet
        Select Case supplier
            Case "APC"
                Call initAPC()
            Case "Logitech"
                Call initLogitech()
            Case "Microsoft"
                Call initMicrosoft()
            Case "Samsung"
                Call initSamsung()
            Case "Toshiba"
                Call initToshiba()
            Case Else
                Call criticalError("Invalid Supplier [" & supplier & "]")
                Exit Sub
        End Select


        Dim cnEVO As New ADODB.Connection
        'cnEVO = New ADODB.Connection
        cnEVO.ConnectionTimeout = 600
        strConn = "Provider=SQLOLEDB.1;" & myGlobal.getConnectionStringForDB("TZ")

        'Now open the connection.
        cnEVO.Open(strConn)

        Call showProgress("Starting DU data")
        cnEVO.CommandTimeout = 600
        Call getDailyReport(cnEVO, rs, "EXEC [tej].[dbo].[getSellOutFor-TRI-TZ] @SUPPLIER='" & supplier & "', @DB='TRI', @FROMDATE = N'" & fromDate & "', @TODATE = N'" & toDate & "'", exchangeDU, "TRI", supplier)

        Call showProgress("Starting TZ data")
        cnEVO.CommandTimeout = 600
        Call getDailyReport(cnEVO, rs, "EXEC [tej].[dbo].[getSellOutFor-TRI-TZ] @SUPPLIER='" & supplier & "', @DB='TZ', @FROMDATE = N'" & fromDate & "', @TODATE = N'" & toDate & "'", exchangeTZ, "TZ", supplier)

        cnEVO.Close()   'close EVO connection here
        cnEVO = Nothing

        ''--start for----KE  & EPZ --------------------------

        Dim cnOB1 As New ADODB.Connection
        'cnEVO = New ADODB.Connection
        cnOB1.ConnectionTimeout = 600
        strConn = "Provider=SQLOLEDB.1;" & myGlobal.getConnectionStringForDB("KE")

        'Now open the connection.
        cnOB1.Open(strConn)

        Call showProgress("Starting EPZ data")
        cnOB1.CommandTimeout = 600
        Call getDailyReport(cnOB1, rs, "EXEC [tej].[dbo].[getSellOutFor-EPZ-KE] @SUPPLIER='" & supplier & "', @DB='EPZ', @FROMDATE = N'" & fromDate & "', @TODATE = N'" & toDate & "'", exchangeEPZ, "EPZ", supplier)

skipEPZ:

        Call showProgress("Starting KE data")
        cnOB1.CommandTimeout = 600
        Call getDailyReport(cnOB1, rs, "EXEC [tej].[dbo].[getSellOutFor-EPZ-KE] @SUPPLIER='" & supplier & "', @DB='KE', @FROMDATE = N'" & fromDate & "', @TODATE = N'" & toDate & "'", exchangeKE, "KE", supplier)

        Call showProgress("Finishing KE data")

        rs = Nothing
        cnOB1.Close()
        cnOB1 = Nothing

refreshAll:

        workbook.CalcMode = ExcelCalcMode.Automatic


        lblMsg.Text = lblMsg.Text + " , Finished at " + Now.ToString("hh:mm:ss tt") + " , Report Done."
        GoTo Done
        '*** STOP EMAIL
        ''If worksheetParameters.Cell(2, 2) <> "NX7300" Then
        If worksheetParameters.Cell(18, 2).Value <> "" Then
            'Call sendEmail(worksheetParameters.Cell(17, 2), _
            'worksheetParameters.Cell(18, 2), "dashboard", "Attached is dashboard summary")
            '"munir@raha.com;tej@reddot.co.tz", "dashboard", "Attached is dashboard summary")
        Else
        End If
        ''End If
Done:
        Call showProgress("Finishing refresh")
    End Sub
    Sub findItemCountSamsung(ByRef numItems As Integer, ByRef region As String)
        Dim currRow As Integer
        Dim moreRows As Boolean
        Dim tempWrksheet As ExcelWorksheet

        Select Case region
            Case "TZ"
                tempWrksheet = worksheetSamsungTZ
            Case "KE"
                tempWrksheet = worksheetSamsungKE
            Case Else
                tempWrksheet = worksheetSamsungETUGBRRW
        End Select

        moreRows = True
        currRow = 4
        numItems = 0
        Do While (moreRows)
            If tempWrksheet.Cell(currRow, 3).Value = "K$" Then 'valid entry
                currRow = currRow + 8
                numItems = numItems + 1
            Else
                moreRows = False
            End If
        Loop

    End Sub

    ' ACCESS VBA MODULE: Send E-mail without Security Warning
    ' (c) 2005 Wayne Phillips (http://www.everythingaccess.com)
    ' Written 07/05/2005
    ' Last updated v1.3 - 11/11/2005
    '
    ' Please read the full tutorial & code here:
    ' http://www.everythingaccess.com/tutorials.asp?ID=Outlook-Send-E-mail-without-Security-Warning
    '
    ' Please leave the copyright notices in place - Thank you.

    'This is a test function - replace the e-mail addresses with your own before executing!!
    '(CC/BCC can be blank strings, attachments string is optional)

    Sub sendEmail(ByVal summaryFile As String, ByVal toAddr As String, ByVal subject As String, ByVal emailBody As String)
        'Dim blnSuccessful As Boolean

        'Call exportFile(summaryFile)

        'blnSuccessful = FnSafeSendEmail(toAddr, _
        '                                subject & " " & Date, _
        '                                emailBody, _
        '                                summaryFile)
        'If blnSuccessful Then
        '    MsgBox "E-mail message sent successfully!"
        'Else
        '    MsgBox "Failed to send e-mail!"
        'End If
    End Sub


    'This is the procedure that calls the exposed Outlook VBA function...
    Public Function FnSafeSendEmail(ByVal strTo As String, _
                        ByVal strSubject As String, _
                        ByVal strMessageBody As String, _
                        Optional ByVal strAttachmentPaths As String = "", _
                        Optional ByVal strCC As String = "", _
                        Optional ByVal strBCC As String = "") As Boolean

        Dim objOutlook As Object ' Note: Must be late-binding.
        Dim objNameSpace As Object
        Dim objExplorer As Object
        Dim blnSuccessful As Boolean
        Dim blnNewInstance As Boolean

        'Is an instance of Outlook already open that we can bind to?
        'On Error Resume Next
        On Error GoTo 0
        Err.Clear()
        objOutlook = GetObject("", "Outlook.Application")
        'On Error GoTo 0


        If Err.Number > 0 Then 'objOutlook Is Nothing Then
            Err.Clear()

            'Outlook isn't already running - create a new instance...
            objOutlook = CreateObject("Outlook.Application")
            If Err.Number > 0 Then
                'Debug.Print("Could not create Outlook object")
                FnSafeSendEmail = "unsuccessful"
                Exit Function
            End If

            blnNewInstance = True
            'We need to instantiate the Visual Basic environment... (messy)
            objNameSpace = objOutlook.GetNamespace("MAPI")
            objExplorer = objOutlook.Explorers.Add(objNameSpace.Folders(1), 0)
            objExplorer.CommandBars.FindControl(, 1695).Execute()

            objExplorer.Close()

            objNameSpace = Nothing
            objExplorer = Nothing

        End If

        blnSuccessful = objOutlook.FnSendMailSafe(strTo, strCC, strBCC, _
                                                    strSubject, strMessageBody, _
                                                    strAttachmentPaths)

        'If blnNewInstance = True Then objOutlook.Quit
        objOutlook = Nothing

        FnSafeSendEmail = blnSuccessful

    End Function
    Private Sub exportFile(ByVal summaryFile As String)
        'Dim strExcelFile As String
        'Dim strWorksheet As String
        ''Dim strDB As String
        'Dim strTable As String
        'Dim dashboardFile As String
        ''Dim objDB As Database

        ''Change Based on your needs, or use
        ''as parameters to the sub
        ''objWorkbook.Worksheets("Positions").Range("W2").value = .Range("A3").End(xlDown).Offset(0, 41).value

        ''strExcelFile = "C:\Documents and Settings\tej_2\Desktop\dashboard\summary.xlsx"

        ''If excel file already exists, you can delete it here
        ''If Dir(strExcelFile) <> "" Then Kill strExcelFile
        'dashboardFile = objWorkbook.Name
        'summaryFile = worksheetParameters.Cell(17, 2)
        ''Workbooks(dashboardFile).objWorkbook.Worksheets("Summary").Range("B160:U236").Copy
        'objWorkbook.Worksheets("Summary").Range("summaryExport").Copy()
        ''Selection.Copy
        ''Workbooks.Open (summaryFile)
        'Workbooks.Add()

        ''ActiveSheet.Cells.ClearContents
        ''Workbooks("summary.xlsx").worksheetSummary.Cell.Delete
        ''ActiveSheet.Paste
        'ActiveCell.PasteSpecial(Paste:=xlValues, operation:=xlPasteSpecialOperationNone)
        'ActiveCell.PasteSpecial(Paste:=xlFormats, operation:=xlPasteSpecialOperationNone)
        ''ActiveSheet.Paste
        ''ActiveSheet.PasteSpecial xlPasteValues
        ''Workbooks("summary.xlsx").objWorkbook.Worksheets("Summary").Paste
        ''ActiveWorkbook.SaveAs Filename:= _
        ''strExcelFile _
        '', FileFormat:=xlCSV, CreateBackup:=False
        'Application.DisplayAlerts = False

        'ActiveWorkbook.SaveAs(summaryFile, xlWorkbookNormal)
        'ActiveWorkbook.Close()

        ''Workbooks("summary.xlsx").objWorkbook.Worksheets("Summary").Delete
        ''Workbooks("summary.xlsx").Save
        ''Workbooks("summary.xlsx").Close
        'Application.DisplayAlerts = True
    End Sub
    Sub criticalError(ByVal errorMsg As String)
        'MsgBox("Error! : " & errorMsg, MsgBoxStyle.Information, "Message")
        lblErrors.Text = lblErrors.Text + "<br>Error! : " & errorMsg
    End Sub

    Protected Sub Page_Load(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.Load
        btnGenerate.Attributes.Add("onClick", "return getConfirmation();")
        lblMsg.Text = ""
    End Sub

    Protected Sub ddlSupplier_SelectedIndexChanged(sender As Object, e As System.EventArgs) Handles ddlSupplier.SelectedIndexChanged
        lblMsg.Text = ""
        lblErrors.Text = ""
        'lnkDwld.Text = "Download Report #"
        'dwnldFileNamePath = "#"
        lnkDwld.Visible = False

    End Sub

    
End Class
