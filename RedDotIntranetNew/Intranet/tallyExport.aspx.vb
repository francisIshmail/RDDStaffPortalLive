Imports System.Xml
Imports System.Net
Imports System.IO
Imports System.Collections

Partial Class Intranet_tallyExport
    Inherits System.Web.UI.Page

    Dim ServerXMLHTTP As Object
    Dim xmlDoc As New Object
    Dim xmlResponse As New Object

    Dim rsRep As New ADODB.Recordset
    Dim rsTerms As New ADODB.Recordset
    Dim rsTermsDesc As New ADODB.Recordset
    Dim rsInvNum As New ADODB.Recordset
    Dim rsInvNumLines As New ADODB.Recordset
    Dim Account As New ADODB.Recordset
    Dim rsInvStockItem As New ADODB.Recordset
    Dim getLine As New ADODB.Recordset
    Dim getTallyClient As New ADODB.Recordset
    Dim getSerialNo As New ADODB.Recordset

    Dim exists As String
    Dim rplc As String
    Public addMaping As Boolean
    Public invFound As Boolean
    Public addMapping As Boolean
    Public strXML As String
    Public ledger As String
    Public DQ As String
    Public InvNm As String
    Public TrimInvNm As String
    Public AccID As Integer
    Public CustName As String
    Public InDate As Date
    Public InAmt As Double
    Public fInAmt As Double
    Public InAmtExcl As Double
    Public fInAmtExcl As Double
    Public ExRate As Double
    Public InvoiceDate As String
    Public fTaxValue As Double
    Public TaxValue As Double
    Public AutoIndex As Long
    Public LineCode As String
    Public LineQty As String
    Public LineDesc As String
    Public LinePriceExcl As Double
    Public LinePriceLocal As Double
    Public StockID As Integer
    Public LineDiscount As Integer
    Public company As String
    Public voucherID As String
    Public invNumber, invDate, invAmount, effDate, reference, narration, partyLedger, debitAcct, creditAcct, ledgerName As String
    Public ExtOrder As String
    Public SO As String
    Public Rep As String
    Public Terms As Integer
    Public TermDesc As String
    Public DiscExcl As Double
    Public srlNo As String
    Public idLine As Long
    Public LineVAT As Double
    Public fileName As String
    Public line1 As String
    Public line2 As String
    Public line3 As String
    Public tallyClientTable As String
    Public tallyCustName As String
    Public strConn As String
    Dim responsstr As String
    Dim msg As String
    Dim clientIPAddress As String
    Dim url As String

    Public con As New ADODB.Connection
    Dim httpWebRequest As HttpWebRequest
    Dim streamWriter As StreamWriter
    Dim webResponse As WebResponse
    Dim responseStream As Stream
    Dim streamReader As StreamReader
    Dim variablesList As New ArrayList
    Dim newVarList As New ArrayList

    'Dim rddCompanyName As String = "Red Dot Distribution Ltd-2010/11"
    'Dim ccCompanyName As String = "Computer Centre (T) Ltd - 2010/11"
    Dim getCompanyName As New ADODB.Recordset


    Protected Sub Page_Load(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.Load
        If IsPostBack = False Then

            Try

                radioRDD.Checked = True
            setConnections()
            getCompanyName.Open("select companyNameEVO,companyNameTally from [tej].[dbo].[EVO_Tally_CompanyName]", con, ADODB.CursorTypeEnum.adOpenStatic)

            With getCompanyName
                Session("sessRDDName") = getCompanyName.Fields("companyNameTally").Value.ToString()
                radioRDD.Text = getCompanyName.Fields("companyNameEVO").Value.ToString()
                .MoveNext()
                Session("sessCCName") = getCompanyName.Fields("companyNameTally").Value.ToString()
                radioCC.Text = getCompanyName.Fields("companyNameEVO").Value.ToString()
            End With

                getCompanyName.Close()

            Catch ex As Exception
                Message.Show(Me, "Error ! " & ex.Message)
                If con.State = 1 Then
                    con.Close()
                End If

                Exit Sub
            End Try

            If con.State = 1 Then
                con.Close()
            End If

            exportButton.Visible = False
            txtCo.Enabled = False
            custEvo.Enabled = False
            custTally.Enabled = False
            custEvo.Visible = False
            custTally.Visible = False
            lblEvoCust.Visible = False
            lblTallyCust.Visible = False

            Label1.Visible = False
            txtHtml.Visible = False
            txtCo.Text = Session("sessRDDName").ToString()
            lblMsg.Text = ""
        End If
    End Sub

    Protected Sub radioRDD_CheckedChanged(ByVal sender As Object, ByVal e As System.EventArgs) Handles radioRDD.CheckedChanged
        txtCo.Text = Session("sessRDDName").ToString()
    End Sub

    Protected Sub radioCC_CheckedChanged(ByVal sender As Object, ByVal e As System.EventArgs) Handles radioCC.CheckedChanged
        txtCo.Text = Session("sessCCName").ToString()
    End Sub
    Public Sub setConnections()
        If radioRDD.Checked = True Then
            strConn = "Provider=SQLOLEDB.1;" & myGlobal.getConnectionStringForDB("TZ")
        Else
            strConn = "Provider=SQLOLEDB.1;" & myGlobal.getConnectionStringForDB("CC")
        End If

        If con.State = 0 Then
            con.Open(strConn)
        End If
    End Sub
    Protected Sub findButton_Click1(ByVal sender As Object, ByVal e As System.EventArgs) Handles findButton.Click
        'check for the valid login and the permission
        Dim loggiRole As String
        loggiRole = "TallyInvoiceExporter"

        If myGlobal.isCurrentUserOnRole(loggiRole) = False Then
            Message.Show(Me, "Sorry! update permission denied to current user for this application")
            Exit Sub
        End If

        Try
            setConnections()

            exportButton.Visible = False
            custEvo.Enabled = False
            custTally.Enabled = False
            custEvo.Visible = False
            custTally.Visible = False
            lblEvoCust.Visible = False
            lblTallyCust.Visible = False
            radioCC.Enabled = False
            radioRDD.Enabled = False

            If radioRDD.Checked = True Then
                fetchInv()
            Else
                fetchInvCC()
            End If

            txtHtml.Value = ""
            Label1.Visible = False
            txtHtml.Visible = False

        Catch ex As Exception
            Message.Show(Me, ex.Message)
            radioCC.Enabled = True
            radioRDD.Enabled = True
            exportButton.Visible = False
        End Try
    End Sub
    Public Function fetchInv() As Boolean

        fetchInv = False
        rsInvNum.Open("SELECT * FROM dbo.InvNum WHERE InvNumber = '" & txtInvNum.Text & "'", con, ADODB.CursorTypeEnum.adOpenStatic)

        If rsInvNum.RecordCount <> 1 Then
            Message.Show(Me, "Invoice not found")
            rsInvNum.Close()
            fetchInv = False
            radioCC.Enabled = True
            radioRDD.Enabled = True
            con.Close()
            Exit Function
        End If

        With rsInvNum
            AutoIndex = rsInvNum("AutoIndex").Value
            InvNm = rsInvNum("invNumber").Value
            AccID = rsInvNum("AccountID").Value
            InDate = rsInvNum("invDate").Value
            ExRate = rsInvNum("fExchangeRate").Value
            InAmt = rsInvNum("InvTotIncl").Value
            fInAmt = rsInvNum("fInvTotInclForeign").Value 'BalDue
            InAmtExcl = rsInvNum("InvTotExcl").Value
            fInAmtExcl = rsInvNum("fInvTotExclDExForeign").Value 'SubTotal excluding Disc
            TaxValue = rsInvNum("InvTotTax").Value
            fTaxValue = rsInvNum("fInvTotTaxForeign").Value 'Vat
            ExtOrder = rsInvNum("ExtOrderNum").Value 'LPO
            SO = rsInvNum("OrderNum").Value
            Rep = rsInvNum("DocRepID").Value
            Terms = rsInvNum("iInvSettlementTermsID").Value
            DiscExcl = rsInvNum("fInvDiscAmntExForeign").Value
        End With
        rsInvNum.Close()

        InvoiceDate = Format(InDate, "yyyyMMdd")

        Account.Open("SELECT * FROM dbo.Client WHERE DCLink = " & AccID & "", con, ADODB.CursorTypeEnum.adOpenStatic)

        If Account.RecordCount <> 1 Then
            'MsgBox("Customer for current Invoice Number not found in database")
            lblMsg.Text = "Customer for current Invoice Number not found in database"
            fetchInv = False
            radioCC.Enabled = True
            radioRDD.Enabled = True
            con.Close()
            Exit Function
        Else
            With Account
                CustName = Account("Name").Value
            End With
        End If

findMapping:

        getTallyClient.Open("SELECT CustNameTally FROM [tej].[dbo].[EvoTallyClients] WHERE CustNameEvo = '" & CustName & "'", con, ADODB.CursorTypeEnum.adOpenStatic)
        custEvo.Text = ""
        custTally.Text = ""
        addMapping = False
        Select Case getTallyClient.RecordCount
            Case 0 'no rec
                'MsgBox("Customer: [" & CustName & "] - not found in MAPPING file - WILL BE ADDED")
                'lblMsg.Text = "Customer: [" & CustName & "] - not found in MAPPING file - WILL BE ADDED"
                getTallyClient.Close()
                getTallyClient.Open("INSERT INTO [tej].[dbo].[EvoTallyClients] VALUES('" & CustName & "',NULL)", con, ADODB.CursorTypeEnum.adOpenStatic)

                GoTo findMapping
            Case 1
                custEvo.Text = CustName
                lblEvoCust.Visible = True
                custEvo.Visible = True
                If IsDBNull(getTallyClient("CustNameTally").Value) Then

                    lblMsg.Text = "Please enter the Tally name for the displayed EVO Customer Then click EXPORT button." _
                    & vbCrLf & vbCrLf & "**** MAKE SURE YOU HAVE ALREADY CREATED THIS CUSTOMER IN TALLY" _
                    & vbCrLf & vbCrLf & "**** YOU MUST ENTER THE NAME EXACTLY AS IN TALLY"

                    custTally.Enabled = True
                    custTally.Visible = True
                    lblTallyCust.Visible = True
                    exportButton.Enabled = True
                    exportButton.Visible = True

                    addMapping = True
                    fetchInv = True
                Else
                    custEvo.Text = CustName
                    CustName = getTallyClient("CustNameTally").Value
                    custTally.Text = CustName
                    custTally.Visible = True
                    lblTallyCust.Visible = True
                    exportButton.Enabled = True
                    exportButton.Visible = True
                    fetchInv = True
                End If
            Case Else
                'MsgBox("Customer: [" & CustName & "] - multiple entries found in MAPPING file.")
                Message.Show(Me, "Customer: [" & CustName & "] - multiple entries found in MAPPING file")
                fetchInv = False
                radioRDD.Enabled = True
                radioCC.Enabled = True
        End Select


        Account.Close()
        getTallyClient.Close()

        If Not fetchInv Then Exit Function

        rsRep.Open("SELECT * from dbo.SalesRep WHERE idSalesRep = '" & Rep & "'", con, ADODB.CursorTypeEnum.adOpenStatic)
        If rsRep.RecordCount = 0 Then
            GoTo getTerms
        Else
            With rsRep
                Rep = rsRep("Name").Value
            End With
        End If

getTerms:
        rsRep.Close()

        rsTermsDesc.Open("SELECT * from dbo._etblSettlementTerms WHERE idSettlementTerms = '" & Terms & "'", con, ADODB.CursorTypeEnum.adOpenStatic)
        If rsTermsDesc.RecordCount = 0 Then
            GoTo buildXML
        Else
            With rsTermsDesc
                TermDesc = rsTermsDesc("cSettlementDescription").Value
            End With
        End If

buildXML:
        rsTermsDesc.Close()
        fetchInv = True

        variablesList.Add(AutoIndex)
        variablesList.Add(InvNm)
        variablesList.Add(ExRate)
        variablesList.Add(InAmt)
        variablesList.Add(fInAmt)
        variablesList.Add(fInAmtExcl)
        variablesList.Add(TaxValue)
        variablesList.Add(fTaxValue)
        variablesList.Add(ExtOrder)
        variablesList.Add(SO)
        variablesList.Add(InvoiceDate)
        variablesList.Add(TermDesc)
        variablesList.Add(Rep)
        variablesList.Add(DiscExcl)
        variablesList.Add(CustName)
        variablesList.Add(addMapping)
        Session("sessVarList") = variablesList
    End Function
    Public Function fetchInvCC() As Boolean

        fetchInvCC = False
        rsInvNum.Open("SELECT * FROM dbo.InvNum WHERE InvNumber = '" & txtInvNum.Text & "'", con, ADODB.CursorTypeEnum.adOpenStatic)

        If rsInvNum.RecordCount <> 1 Then
            Message.Show(Me, "Invoice not found")
            rsInvNum.Close()
            fetchInvCC = False
            radioCC.Enabled = True
            radioRDD.Enabled = True
            con.Close()
            Exit Function
        End If

        With rsInvNum
            AutoIndex = rsInvNum("AutoIndex").Value
            InvNm = rsInvNum("invNumber").Value
            AccID = rsInvNum("AccountID").Value
            InDate = rsInvNum("invDate").Value
            ExRate = rsInvNum("fExchangeRate").Value
            InAmt = rsInvNum("InvTotIncl").Value
            fInAmt = rsInvNum("fInvTotInclForeign").Value 'BalDue
            InAmtExcl = rsInvNum("InvTotExcl").Value
            fInAmtExcl = rsInvNum("fInvTotExclDExForeign").Value 'SubTotal excluding Disc
            TaxValue = rsInvNum("InvTotTax").Value
            fTaxValue = rsInvNum("fInvTotTaxForeign").Value 'Vat
            ExtOrder = rsInvNum("ExtOrderNum").Value 'LPO
            SO = rsInvNum("OrderNum").Value
            Rep = rsInvNum("DocRepID").Value
            Terms = rsInvNum("iInvSettlementTermsID").Value
            DiscExcl = rsInvNum("fInvDiscAmntExForeign").Value
        End With
        rsInvNum.Close()

        InvoiceDate = Format(InDate, "yyyyMMdd")

        Account.Open("SELECT * FROM dbo.Client WHERE DCLink = " & AccID & "", con, ADODB.CursorTypeEnum.adOpenStatic)

        If Account.RecordCount <> 1 Then
            'MsgBox("Customer for current Invoice Number not found in database")
            Message.Show(Me, "Customer for current Invoice Number not found in database")
            fetchInvCC = False
            radioCC.Enabled = True
            radioRDD.Enabled = True
            con.Close()
            Exit Function
        Else
            With Account
                CustName = Account("Name").Value
            End With
        End If

        If ExRate = 0 Or ExRate = 1 Then
            tallyCustName = "CustNameTallyLOCAL"
        Else
            tallyCustName = "CustNameTallyUSD"
        End If

findMapping:

        getTallyClient.Open("SELECT " & tallyCustName & " FROM [tej].[dbo].[EvoTallyClientsCC11] WHERE CustNameEvo = '" & CustName & "'", con, ADODB.CursorTypeEnum.adOpenStatic)
        custEvo.Text = ""
        custTally.Text = ""
        addMapping = False
        Select Case getTallyClient.RecordCount
            Case 0 'no rec
                'MsgBox("Customer: [" & CustName & "] - not found in MAPPING file - WILL BE ADDED")
                'Message.Show(Me, "Customer: [" & CustName & "] - not found in MAPPING file - WILL BE ADDED")
                getTallyClient.Close()
                getTallyClient.Open("INSERT INTO [tej].[dbo].[EvoTallyClientsCC11] VALUES('" & CustName & "',NULL,NULL)", con, ADODB.CursorTypeEnum.adOpenStatic)

                GoTo findMapping
            Case 1
                custEvo.Text = CustName
                lblEvoCust.Visible = True
                custEvo.Visible = True
                If IsDBNull(getTallyClient("" & tallyCustName & "").Value) Then

                    lblMsg.Text = "Please enter the Tally name for the displayed EVO Customer Then click EXPORT button." _
                    & vbCrLf & vbCrLf & "**** MAKE SURE YOU HAVE ALREADY CREATED THIS CUSTOMER IN TALLY" _
                    & vbCrLf & vbCrLf & "**** YOU MUST ENTER THE NAME EXACTLY AS IN TALLY"

                    custTally.Enabled = True
                    custTally.Visible = True
                    lblTallyCust.Visible = True
                    exportButton.Enabled = True
                    exportButton.Visible = True
                    addMapping = True
                    fetchInvCC = True
                Else
                    custEvo.Text = CustName
                    CustName = getTallyClient("" & tallyCustName & "").Value
                    custTally.Text = CustName
                    custTally.Visible = True
                    lblTallyCust.Visible = True
                    exportButton.Enabled = True
                    exportButton.Visible = True
                    fetchInvCC = True
                End If
            Case Else
                'MsgBox("Customer: [" & CustName & "] - multiple entries found in MAPPING file.")
                Message.Show(Me, "Customer: [" & CustName & "] - multiple entries found in MAPPING file")
                fetchInvCC = False
        End Select

        Account.Close()
        getTallyClient.Close()

        If Not fetchInvCC Then Exit Function

        rsRep.Open("SELECT * from dbo.SalesRep WHERE idSalesRep = '" & Rep & "'", con, ADODB.CursorTypeEnum.adOpenStatic)
        If rsRep.RecordCount = 0 Then
            GoTo getTerms
        Else
            With rsRep
                Rep = rsRep("Name").Value
            End With
        End If

getTerms:
        rsRep.Close()

        'TermDesc = ""
        rsTermsDesc.Open("SELECT * from dbo._etblSettlementTerms WHERE idSettlementTerms = '" & Terms & "'", con, ADODB.CursorTypeEnum.adOpenStatic)
        If rsTermsDesc.RecordCount = 0 Then
            GoTo buildXML
        Else
            With rsTermsDesc
                TermDesc = rsTermsDesc("cSettlementDescription").Value
            End With
        End If

buildXML:
        'If "" = TermDesc Then TermDesc = " "
        rsTermsDesc.Close()
        fetchInvCC = True

        variablesList.Add(AutoIndex)
        variablesList.Add(InvNm)
        variablesList.Add(ExRate)
        variablesList.Add(InAmt)
        variablesList.Add(fInAmt)
        variablesList.Add(fInAmtExcl)
        variablesList.Add(TaxValue)
        variablesList.Add(fTaxValue)
        variablesList.Add(ExtOrder)
        variablesList.Add(SO)
        variablesList.Add(InvoiceDate)
        variablesList.Add(TermDesc)
        variablesList.Add(Rep)
        variablesList.Add(DiscExcl)
        variablesList.Add(CustName)
        variablesList.Add(addMapping)
        variablesList.Add(tallyCustName)
        Session("sessVarList") = variablesList
    End Function

    Public Sub fetchInvLinesCC()
        Dim existsDesc As String
        Dim existsCode As String

        With rsInvNumLines
            idLine = rsInvNumLines("idInvoiceLines").Value
            StockID = rsInvNumLines("iStockCodeID").Value
            LineQty = rsInvNumLines("fQuantity").Value
            'LinePriceExcl = rsInvNumLines("fQtyProcessedLineTotExclForeign").Value
            'LinePriceLocal = rsInvNumLines("fQtyProcessedLineTotExcl").Value
            LinePriceExcl = rsInvNumLines("fQtyLastProcessLineTotExclForeign").Value
            LinePriceLocal = rsInvNumLines("fQtyLastProcessLineTotExcl").Value
            LineDiscount = rsInvNumLines("fLineDiscount").Value
            'srlNo = !cSerialNumber
            LineVAT = rsInvNumLines("fQuantityLineTaxAmountForeign").Value
        End With

        rsInvStockItem.Open("SELECT * FROM dbo.StkItem WHERE StockLink = '" & StockID & "'", con, ADODB.CursorTypeEnum.adOpenStatic)
        If rsInvStockItem.RecordCount = 0 Then
            GoTo mvnx
        Else
            With rsInvStockItem
                LineCode = rsInvStockItem("cSimpleCode").Value
                LineDesc = rsInvStockItem("Description_1").Value
            End With


            existsCode = InStr(LineCode, "&") <> 0

            If existsCode = "True" Then
                rplc = Replace$(LineCode, "&", "&amp;")
                LineCode = rplc
            End If

            existsDesc = InStr(LineDesc, "&") <> 0

            If existsDesc = "True" Then
                rplc = Replace$(LineDesc, "&", "&amp;")
                LineDesc = rplc
            End If

            rsInvStockItem.Close()
            rsInvNumLines.MoveNext()
mvnx:
        End If
    End Sub
    Protected Sub exportButton_Click1(ByVal sender As Object, ByVal e As System.EventArgs) Handles exportButton.Click
        Try
            setConnections()

            findButton.Enabled = False

            If Trim(custTally.Text) = "" Then
                lblMsg.Text = "Please enter the Tally name for the displayed EVO Customer Then click EXPORT button." _
                & vbCrLf & vbCrLf & "**** MAKE SURE YOU HAVE ALREADY CREATED THIS CUSTOMER IN TALLY" _
                & vbCrLf & vbCrLf & "**** YOU MUST ENTER THE NAME EXACTLY AS IN TALLY"
                custTally.Enabled = True
                findButton.Enabled = True
                Exit Sub
            End If

            Label1.Visible = True
            txtHtml.Visible = True

            If radioRDD.Checked = True Then
                Call createXML()
            Else
                Call createXMLCC()
            End If

            'strXML = "<ENVELOPE><HEADER><TALLYREQUEST>Import Data</TALLYREQUEST></HEADER><BODY><IMPORTDATA><REQUESTDESC><REPORTNAME>Vouchers</REPORTNAME><STATICVARIABLES><SVCURRENTCOMPANY>Computer Centre (T) Ltd - 2010/11</SVCURRENTCOMPANY></STATICVARIABLES></REQUESTDESC><REQUESTDATA><TALLYMESSAGE xmlns:UDF=""TallyUDF""><VOUCHER VCHTYPE=""Sales"" ACTION=""Create""><ISOPTIONAL>No</ISOPTIONAL><USEFORGAINLOSS>No</USEFORGAINLOSS><USEFORCOMPOUND>No</USEFORCOMPOUND><FORJOBCOSTING>No</FORJOBCOSTING><POSCARDLEDGER/><POSCASHLEDGER/><POSGIFTLEDGER/><POSCHEQUELEDGER/><NARRATION>main narration</NARRATION><VOUCHERTYPENAME>Sales</VOUCHERTYPENAME><VOUCHERNUMBER>32008743</VOUCHERNUMBER><DATE>20110502</DATE><EFFECTIVEDATE>20110502</EFFECTIVEDATE><ISCANCELLED>No</ISCANCELLED><USETRACKINGNUMBER>No</USETRACKINGNUMBER>" & _
            '             "<ISPOSTDATED>No</ISPOSTDATED><ISINVOICE>No</ISINVOICE><DIFFACTUALQTY>No</DIFFACTUALQTY><REFERENCE>&lt;SO0147&gt;&lt;SCI-06-00000525&gt;</REFERENCE><PARTYLEDGERNAME>SCI Tanzania</PARTYLEDGERNAME><NARRATION>&lt;Anthony Fernandes&gt;&lt;&gt;&lt;0&gt;&lt;0&gt;</NARRATION><ENTEREDBY>Evolution</ENTEREDBY><ASPAYSLIP>No</ASPAYSLIP><ALLLEDGERENTRIES.LIST><NARRATION></NARRATION><REMOVEZEROENTRIES>No</REMOVEZEROENTRIES><ISDEEMEDPOSITIVE>Yes</ISDEEMEDPOSITIVE><LEDGERFROMITEM>No</LEDGERFROMITEM><LEDGERNAME>SCI Tanzania</LEDGERNAME><AMOUNT>-u0 @ Tshs 1/u = -Tshs 456</AMOUNT></ALLLEDGERENTRIES.LIST><ALLLEDGERENTRIES.LIST><NARRATION>1@0&lt;0&gt;&lt;SUA1000I&gt;&lt;APC Smart-UPS 1000VA USB &amp; Serial 230V&gt;&lt;0&gt;</NARRATION><UDF:SUNITEMSERIALNO.LIST TYPE=""STRING"" ISLIST=""YES"" DESC=""`SUNItemSerialNo`""><UDF:SUNITEMSERIALNO DESC=""`SUNItemSerialNo`"" >AS0611130222</UDF:SUNITEMSERIALNO></UDF:SUNITEMSERIALNO.LIST><REMOVEZEROENTRIES>No</REMOVEZEROENTRIES><ISDEEMEDPOSITIVE>No</ISDEEMEDPOSITIVE><LEDGERFROMITEM>No</LEDGERFROMITEM><LEDGERNAME>Sales</LEDGERNAME><AMOUNT>u0 @ Tshs 1/u = Tshs 380</AMOUNT></ALLLEDGERENTRIES.LIST><ALLLEDGERENTRIES.LIST><LEDGERNAME>Vat Sales</LEDGERNAME><GSTCLASS/><ISDEEMEDPOSITIVE>No</ISDEEMEDPOSITIVE><LEDGERFROMITEM>No</LEDGERFROMITEM><REMOVEZEROENTRIES>No</REMOVEZEROENTRIES><ISPARTYLEDGER>No</ISPARTYLEDGER><AMOUNT>u0 @ Tshs 1/u = Tshs 76</AMOUNT></ALLLEDGERENTRIES.LIST></VOUCHER></TALLYMESSAGE></REQUESTDATA></IMPORTDATA></BODY></ENVELOPE>"

            clientIPAddress = System.Web.HttpContext.Current.Request.UserHostAddress
            url = "http://" & clientIPAddress & ":9000"

            httpWebRequest = DirectCast(WebRequest.Create(url), HttpWebRequest)
            httpWebRequest.Method = "POST"
            httpWebRequest.ContentLength = strXML.Length
            httpWebRequest.ContentType = "application/x-www-form-urlencoded"

            streamWriter = New StreamWriter(httpWebRequest.GetRequestStream())
            streamWriter.Write(strXML)
            streamWriter.Close()

            webResponse = httpWebRequest.GetResponse()
            responseStream = webResponse.GetResponseStream()
            streamReader = New StreamReader(responseStream)
            msg = streamReader.ReadToEnd()
            streamReader.Close()
            responseStream.Close()
            webResponse.Close()

            txtHtml.Value = msg
            Message.Show(Me, msg)

            con.Close()
            tallyClientTable = ""
            tallyCustName = ""
            con = Nothing
            exportButton.Visible = False
            custTally.Text = ""
            custEvo.Text = ""
            custTally.Enabled = False
            custEvo.Enabled = False
            custTally.Visible = False
            custEvo.Visible = False
            lblEvoCust.Visible = False
            lblTallyCust.Visible = False
            radioRDD.Enabled = True
            radioCC.Enabled = True
            findButton.Enabled = True
            'Debug.Print("Response String " + responsstr)
        Catch ex As WebException
            If ex.Response IsNot Nothing Then
                '' can use ex.Response.Status, .StatusDescription
                responseStream = ex.Response.GetResponseStream()
                streamReader = New StreamReader(responseStream)
                msg = streamReader.ReadToEnd()
                txtHtml.Value = msg
            Else
                txtHtml.Value = ex.Message
                Message.Show(Me, ex.Message)
            End If
            radioRDD.Enabled = True
            radioCC.Enabled = True
            findButton.Enabled = True
            exportButton.Visible = False
        End Try
    End Sub
    Public Sub createXML()
        newVarList = Session("sessVarList")
        CustName = newVarList.Item(14)
        addMapping = newVarList.Item(15)
        If addMapping Then
            getTallyClient.Open("UPDATE [tej].[dbo].[EvoTallyClients] SET CustNameTally='" & Trim(custTally.Text) & "' WHERE CustNameEVO='" & custEvo.Text & "'", con, ADODB.CursorTypeEnum.adOpenStatic)
            addMapping = False
            lblMsg.Text = ""
            CustName = Trim(custTally.Text) 'change recently added
        End If
        custTally.Enabled = False
        Call setXML8() 'Tally header

        getLine.Open("SELECT * FROM dbo._btblInvoiceLines WHERE iInvoiceID = '" & AutoIndex & "'", con, ADODB.CursorTypeEnum.adOpenStatic)
        rsInvNumLines.Open("SELECT * FROM dbo._btblInvoiceLines WHERE iInvoiceID = '" & AutoIndex & "'", con, ADODB.CursorTypeEnum.adOpenStatic)

        Do Until getLine.EOF = True
            Call fetchInvLines()
            Call SetXML8_Lines()
            getLine.MoveNext()
        Loop

        Call SetXML8_Vat()

        Call setXML_End()

        'Debug.Print(strXML + vbCrLf)
        getLine.Close()
        rsInvNumLines.Close()
    End Sub
    Public Sub createXMLCC()
        newVarList = Session("sessVarList")
        CustName = newVarList.Item(14)
        addMapping = newVarList.Item(15)
        tallyCustName = newVarList.Item(16)
        If addMapping Then
            getTallyClient.Open("UPDATE [tej].[dbo].[EvoTallyClientsCC11] SET " & tallyCustName & "='" & Trim(custTally.Text) & "' WHERE CustNameEVO='" & custEvo.Text & "'", con, ADODB.CursorTypeEnum.adOpenStatic)
            addMapping = False
            lblMsg.Text = ""
            CustName = Trim(custTally.Text) 'change recently added
        End If
        custTally.Enabled = False
        Call setXML8CC() 'Tally header

        getLine.Open("SELECT * FROM dbo._btblInvoiceLines WHERE iInvoiceID = '" & AutoIndex & "'", con, ADODB.CursorTypeEnum.adOpenStatic)
        rsInvNumLines.Open("SELECT * FROM dbo._btblInvoiceLines WHERE iInvoiceID = '" & AutoIndex & "'", con, ADODB.CursorTypeEnum.adOpenStatic)


        Do Until getLine.EOF = True
            Call fetchInvLinesCC()
            Call SetXML8_LinesCC()
            getLine.MoveNext()
        Loop
        Call SetXML8_VatCC()

        Call setXML_EndCC()

        'Debug.Print(strXML + vbCrLf)
        getLine.Close()
        rsInvNumLines.Close()
    End Sub
    Private Sub setXML8()

        AutoIndex = newVarList.Item(0)
        InvNm = newVarList.Item(1)
        ExRate = newVarList.Item(2)
        InAmt = newVarList.Item(3)
        fInAmt = newVarList.Item(4)
        fInAmtExcl = newVarList.Item(5)
        TaxValue = newVarList.Item(6)
        fTaxValue = newVarList.Item(7)
        ExtOrder = newVarList.Item(8)
        SO = newVarList.Item(9)
        InvoiceDate = newVarList.Item(10)
        TermDesc = newVarList.Item(11)
        Rep = newVarList.Item(12)
        DiscExcl = newVarList.Item(13)

        DQ = Chr(34)
        company = txtCo.Text.Trim
        'CustName = custTally.Text

        ExtOrder = Replace(ExtOrder, "&", "&amp;")

        exists = InStr(CustName, "&") <> 0

        If exists = "True" Then
            rplc = Replace$(CustName, "&", "&amp;")
            CustName = rplc
        End If

        invNumber = InvNm

        invDate = InvoiceDate
        'invDate = "20110502"
        invAmount = InAmt
        effDate = invDate
        'effDate = "20110502"
        'reference = "EVO invoice# " & invNumber
        reference = "&lt;" & SO & "&gt;" & "&lt;" & ExtOrder & "&gt;" 'SO and LPO
        narration = "&lt;" & Rep & "&gt;" & "&lt;" & TermDesc & "&gt;" & "&lt;" & DiscExcl & "&gt;" & "&lt;" & fInAmtExcl & "&gt;"
        partyLedger = CustName
        ledgerName = partyLedger
        debitAcct = partyLedger
        creditAcct = "Sales"
        strXML = ""
        strXML = strXML & _
        "<ENVELOPE>" & vbCrLf
        strXML = strXML & _
            "<HEADER>" & vbCrLf
        strXML = strXML & _
                "<TALLYREQUEST>Import Data</TALLYREQUEST>" & vbCrLf
        strXML = strXML & _
            "</HEADER>" & vbCrLf
        strXML = strXML & _
            "<BODY>" & vbCrLf
        strXML = strXML & _
                "<IMPORTDATA>" & vbCrLf
        strXML = strXML & _
                    "<REQUESTDESC>" & vbCrLf
        strXML = strXML & _
                        "<REPORTNAME>Vouchers</REPORTNAME>" & vbCrLf
        strXML = strXML & _
                        "<STATICVARIABLES>" & vbCrLf
        strXML = strXML & _
                            "<SVCURRENTCOMPANY>" & company & "</SVCURRENTCOMPANY>" & vbCrLf
        strXML = strXML & _
                        "</STATICVARIABLES>" & vbCrLf
        strXML = strXML & _
                    "</REQUESTDESC>" & vbCrLf
        strXML = strXML & _
                    "<REQUESTDATA>" & vbCrLf
        strXML = strXML & _
                        "<TALLYMESSAGE xmlns:UDF=" & DQ & "TallyUDF" & DQ & ">" & vbCrLf
        strXML = strXML & _
                            "<VOUCHER VCHTYPE=" & DQ & "Sales" & DQ & " ACTION=" & DQ & "Create" & DQ & ">" & vbCrLf
        strXML = strXML & _
                                "<ISOPTIONAL>No</ISOPTIONAL>" & vbCrLf
        strXML = strXML & _
                                "<USEFORGAINLOSS>No</USEFORGAINLOSS>" & vbCrLf
        strXML = strXML & _
                                "<USEFORCOMPOUND>No</USEFORCOMPOUND>" & vbCrLf
        strXML = strXML & _
                                "<FORJOBCOSTING>No</FORJOBCOSTING>" & vbCrLf
        strXML = strXML & _
                                "<POSCARDLEDGER/>" & vbCrLf
        strXML = strXML & _
                                "<POSCASHLEDGER/>" & vbCrLf
        strXML = strXML & _
                                "<POSGIFTLEDGER/>" & vbCrLf
        strXML = strXML & _
                                "<POSCHEQUELEDGER/>" & vbCrLf
        strXML = strXML & _
                                "<NARRATION>main narration</NARRATION>" & vbCrLf
        strXML = strXML & _
                                "<VOUCHERTYPENAME>Sales</VOUCHERTYPENAME>" & vbCrLf
        strXML = strXML & _
                                "<VOUCHERNUMBER>" & invNumber & "</VOUCHERNUMBER>" & vbCrLf
        strXML = strXML & _
                                "<DATE>" & invDate & "</DATE>" & vbCrLf
        strXML = strXML & _
                                "<EFFECTIVEDATE>" & effDate & "</EFFECTIVEDATE>" & vbCrLf
        strXML = strXML & _
                                "<ISCANCELLED>No</ISCANCELLED>" & vbCrLf
        strXML = strXML & _
                                "<USETRACKINGNUMBER>No</USETRACKINGNUMBER>" & vbCrLf
        strXML = strXML & _
                                "<ISPOSTDATED>No</ISPOSTDATED>" & vbCrLf
        strXML = strXML & _
                                "<ISINVOICE>No</ISINVOICE>" & vbCrLf
        strXML = strXML & _
                                "<DIFFACTUALQTY>No</DIFFACTUALQTY>" & vbCrLf
        strXML = strXML & _
                                "<REFERENCE>" & reference & "</REFERENCE>" & vbCrLf
        strXML = strXML & _
                                "<PARTYLEDGERNAME>" & partyLedger & "</PARTYLEDGERNAME>" & vbCrLf
        strXML = strXML & _
                                "<NARRATION>" & narration & "</NARRATION>" & vbCrLf
        strXML = strXML & _
                                "<ENTEREDBY>Evolution</ENTEREDBY>" & vbCrLf
        strXML = strXML & _
                                "<ASPAYSLIP>No</ASPAYSLIP>" & vbCrLf
        strXML = strXML & _
                                "<ALLLEDGERENTRIES.LIST>" & vbCrLf
        strXML = strXML & _
                                    "<NARRATION></NARRATION>" & vbCrLf
        strXML = strXML & _
                                    "<REMOVEZEROENTRIES>No</REMOVEZEROENTRIES>" & vbCrLf
        strXML = strXML & _
                                    "<ISDEEMEDPOSITIVE>Yes</ISDEEMEDPOSITIVE>" & vbCrLf
        strXML = strXML & _
                                    "<LEDGERFROMITEM>No</LEDGERFROMITEM>" & vbCrLf
        strXML = strXML & _
                                    "<LEDGERNAME>" & debitAcct & "</LEDGERNAME>" & vbCrLf
        strXML = strXML & _
                                    "<AMOUNT>-u" & fInAmt & " @ Tshs " & ExRate & "/u" & " = -Tshs " & invAmount & "</AMOUNT>" & vbCrLf
        strXML = strXML & _
                                "</ALLLEDGERENTRIES.LIST>" & vbCrLf


        'goes to Lines at setXML8 LINES

    End Sub
    Public Sub getSerialsforLine()
        'change here
        getSerialNo = New ADODB.Recordset

        'getSerialNo.Open "select * from dbo._btblInvoiceLineSN where iSerialInvoiceID=" & AutoIndex, con, adOpenStatic
        getSerialNo.Open("select * from dbo._btblInvoiceLineSN where iSerialInvoiceID=" & AutoIndex & " and iSerialInvoiceLineID= " & idLine, con, ADODB.CursorTypeEnum.adOpenStatic)

        If getSerialNo.RecordCount > 0 Then


            strXML = strXML & _
                                   "<UDF:SUNITEMSERIALNO.LIST TYPE=" & DQ & "STRING" & DQ & " ISLIST=" & DQ & "YES" & DQ & " DESC=" & DQ & "`SUNItemSerialNo`" & DQ & ">" & vbCrLf


            'getSerialNo
            Do Until getSerialNo.EOF = True


                srlNo = getSerialNo("cSerialNumber").Value

                strXML = strXML & _
                                     "<UDF:SUNITEMSERIALNO DESC=" & DQ & "`SUNItemSerialNo`"" >" & srlNo & "</UDF:SUNITEMSERIALNO>" & vbCrLf
                getSerialNo.MoveNext()
            Loop
            'End With

            getSerialNo.Close()

            strXML = strXML & _
                                    "</UDF:SUNITEMSERIALNO.LIST>" & vbCrLf

            'change till here
        End If
    End Sub
    Public Sub SetXML8_Lines()
        strXML = strXML & _
                                "<ALLLEDGERENTRIES.LIST>" & vbCrLf
        strXML = strXML & _
                                     "<NARRATION>" & LineQty & "@" & LinePriceExcl & "&lt;" & LineDiscount & "&gt;" & "&lt;" & LineCode & "&gt;" & "&lt;" & LineDesc & "&gt;" & "&lt;" & LineVAT & "&gt;" & "</NARRATION>" & vbCrLf
        'strXML = strXML & _
        '                           "<NARRATION>" & LineQty & "@" & LinePriceExcl & "/" & LineDiscount & "/" & LineCode & "/" & LineDesc & "</NARRATION>" & vbCrLf


        Call getSerialsforLine()

        strXML = strXML & _
                                    "<REMOVEZEROENTRIES>No</REMOVEZEROENTRIES>" & vbCrLf
        strXML = strXML & _
                                    "<ISDEEMEDPOSITIVE>No</ISDEEMEDPOSITIVE>" & vbCrLf
        strXML = strXML & _
                                    "<LEDGERFROMITEM>No</LEDGERFROMITEM>" & vbCrLf
        strXML = strXML & _
                                    "<LEDGERNAME>" & creditAcct & "</LEDGERNAME>" & vbCrLf
        strXML = strXML & _
                                    "<AMOUNT>u" & LinePriceExcl & " @ Tshs " & ExRate & "/u" & " = Tshs " & LinePriceLocal & "</AMOUNT>" & vbCrLf
        'strXML = strXML & _
        '                            "<AMOUNT>u" & fInAmtExcl & " @ Tshs " & ExRate & "/u" & " = Tshs " & InAmtExcl & "</AMOUNT>" & vbCrLf
        strXML = strXML & _
                                "</ALLLEDGERENTRIES.LIST>" & vbCrLf
    End Sub
    Public Sub SetXML8_Vat()

        strXML = strXML & _
                                "<ALLLEDGERENTRIES.LIST>" & vbCrLf
        strXML = strXML & _
                                    "<LEDGERNAME>Vat Sales</LEDGERNAME>" & vbCrLf
        strXML = strXML & _
                                    "<GSTCLASS/>" & vbCrLf
        strXML = strXML & _
                                    "<ISDEEMEDPOSITIVE>No</ISDEEMEDPOSITIVE>" & vbCrLf
        strXML = strXML & _
                                    "<LEDGERFROMITEM>No</LEDGERFROMITEM>" & vbCrLf
        strXML = strXML & _
                                    "<REMOVEZEROENTRIES>No</REMOVEZEROENTRIES>" & vbCrLf
        strXML = strXML & _
                                    "<ISPARTYLEDGER>No</ISPARTYLEDGER>" & vbCrLf
        strXML = strXML & _
                                    "<AMOUNT>u" & fTaxValue & " @ Tshs " & ExRate & "/u" & " = Tshs " & TaxValue & "</AMOUNT>" & vbCrLf
        strXML = strXML & _
                                "</ALLLEDGERENTRIES.LIST>" & vbCrLf
    End Sub

    Public Sub setXML_End()
        strXML = strXML & _
                            "</VOUCHER>" & vbCrLf
        strXML = strXML & _
                        "</TALLYMESSAGE>" & vbCrLf
        strXML = strXML & _
                    "</REQUESTDATA>" & vbCrLf
        strXML = strXML & _
                "</IMPORTDATA>" & vbCrLf
        strXML = strXML & _
            "</BODY>" & vbCrLf
        strXML = strXML & _
        "</ENVELOPE>" + vbCrLf

    End Sub
    Public Sub fetchInvLines()
        Dim existsDesc As String
        Dim existsCode As String

        With rsInvNumLines
            idLine = rsInvNumLines("idInvoiceLines").Value
            StockID = rsInvNumLines("iStockCodeID").Value
            LineQty = rsInvNumLines("fQuantity").Value
            'LinePriceExcl = rsInvNumLines("fQtyProcessedLineTotExclForeign").Value
            'LinePriceLocal = rsInvNumLines("fQtyProcessedLineTotExcl").Value
            LinePriceExcl = rsInvNumLines("fQtyLastProcessLineTotExclForeign").Value
            LinePriceLocal = rsInvNumLines("fQtyLastProcessLineTotExcl").Value
            LineDiscount = rsInvNumLines("fLineDiscount").Value
            'srlNo = !cSerialNumber
            LineVAT = rsInvNumLines("fQuantityLineTaxAmountForeign").Value
        End With

        rsInvStockItem.Open("SELECT * FROM dbo.StkItem WHERE StockLink = '" & StockID & "'", con, ADODB.CursorTypeEnum.adOpenStatic)
        If rsInvStockItem.RecordCount = 0 Then
            GoTo mvnx
        Else
            With rsInvStockItem
                LineCode = rsInvStockItem("cSimpleCode").Value
                LineDesc = rsInvStockItem("Description_1").Value
            End With


            existsCode = InStr(LineCode, "&") <> 0

            If existsCode = "True" Then
                rplc = Replace$(LineCode, "&", "&amp;")
                LineCode = rplc
            End If

            existsDesc = InStr(LineDesc, "&") <> 0

            If existsDesc = "True" Then
                rplc = Replace$(LineDesc, "&", "&amp;")
                LineDesc = rplc
            End If

            rsInvStockItem.Close()
            rsInvNumLines.MoveNext()
mvnx:
        End If
    End Sub
    Private Sub setXML8CC()
        AutoIndex = newVarList.Item(0)
        InvNm = newVarList.Item(1)
        ExRate = newVarList.Item(2)
        InAmt = newVarList.Item(3)
        fInAmt = newVarList.Item(4)
        fInAmtExcl = newVarList.Item(5)
        TaxValue = newVarList.Item(6)
        fTaxValue = newVarList.Item(7)
        ExtOrder = newVarList.Item(8)
        SO = newVarList.Item(9)
        InvoiceDate = newVarList.Item(10)
        TermDesc = newVarList.Item(11)
        Rep = newVarList.Item(12)
        DiscExcl = newVarList.Item(13)

        DQ = Chr(34)
        company = txtCo.Text
        'CustName = custTally.Text

        exists = InStr(CustName, "&") <> 0

        If exists = "True" Then
            rplc = Replace$(CustName, "&", "&amp;")
            CustName = rplc
        End If

        invNumber = InvNm

        invDate = InvoiceDate
        'invDate = "20110502"
        invAmount = InAmt
        effDate = invDate
        'effDate = "20110502"
        'reference = "EVO invoice# " & invNumber
        reference = "&lt;" & SO & "&gt;" & "&lt;" & ExtOrder & "&gt;" 'SO and LPO
        narration = "&lt;" & Rep & "&gt;" & "&lt;" & TermDesc & "&gt;" & "&lt;" & DiscExcl & "&gt;" & "&lt;" & fInAmtExcl & "&gt;"
        partyLedger = CustName
        ledgerName = partyLedger
        debitAcct = partyLedger
        creditAcct = "Sales"
        strXML = ""
        strXML = strXML & _
        "<ENVELOPE>" & vbCrLf
        strXML = strXML & _
            "<HEADER>" & vbCrLf
        strXML = strXML & _
                "<TALLYREQUEST>Import Data</TALLYREQUEST>" & vbCrLf
        strXML = strXML & _
            "</HEADER>" & vbCrLf
        strXML = strXML & _
            "<BODY>" & vbCrLf
        strXML = strXML & _
                "<IMPORTDATA>" & vbCrLf
        strXML = strXML & _
                    "<REQUESTDESC>" & vbCrLf
        strXML = strXML & _
                        "<REPORTNAME>Vouchers</REPORTNAME>" & vbCrLf
        strXML = strXML & _
                        "<STATICVARIABLES>" & vbCrLf
        strXML = strXML & _
                            "<SVCURRENTCOMPANY>" & company & "</SVCURRENTCOMPANY>" & vbCrLf
        strXML = strXML & _
                        "</STATICVARIABLES>" & vbCrLf
        strXML = strXML & _
                    "</REQUESTDESC>" & vbCrLf
        strXML = strXML & _
                    "<REQUESTDATA>" & vbCrLf
        strXML = strXML & _
                        "<TALLYMESSAGE xmlns:UDF=" & DQ & "TallyUDF" & DQ & ">" & vbCrLf
        strXML = strXML & _
                            "<VOUCHER VCHTYPE=" & DQ & "Sales" & DQ & " ACTION=" & DQ & "Create" & DQ & ">" & vbCrLf
        strXML = strXML & _
                                "<ISOPTIONAL>No</ISOPTIONAL>" & vbCrLf
        strXML = strXML & _
                                "<USEFORGAINLOSS>No</USEFORGAINLOSS>" & vbCrLf
        strXML = strXML & _
                                "<USEFORCOMPOUND>No</USEFORCOMPOUND>" & vbCrLf
        strXML = strXML & _
                                "<FORJOBCOSTING>No</FORJOBCOSTING>" & vbCrLf
        strXML = strXML & _
                                "<POSCARDLEDGER/>" & vbCrLf
        strXML = strXML & _
                                "<POSCASHLEDGER/>" & vbCrLf
        strXML = strXML & _
                                "<POSGIFTLEDGER/>" & vbCrLf
        strXML = strXML & _
                                "<POSCHEQUELEDGER/>" & vbCrLf
        strXML = strXML & _
                                "<NARRATION>main narration</NARRATION>" & vbCrLf
        strXML = strXML & _
                                "<VOUCHERTYPENAME>Sales</VOUCHERTYPENAME>" & vbCrLf
        strXML = strXML & _
                                "<VOUCHERNUMBER>" & invNumber & "</VOUCHERNUMBER>" & vbCrLf
        strXML = strXML & _
                                "<DATE>" & invDate & "</DATE>" & vbCrLf
        strXML = strXML & _
                                "<EFFECTIVEDATE>" & effDate & "</EFFECTIVEDATE>" & vbCrLf
        strXML = strXML & _
                                "<ISCANCELLED>No</ISCANCELLED>" & vbCrLf
        strXML = strXML & _
                                "<USETRACKINGNUMBER>No</USETRACKINGNUMBER>" & vbCrLf
        strXML = strXML & _
                                "<ISPOSTDATED>No</ISPOSTDATED>" & vbCrLf
        strXML = strXML & _
                                "<ISINVOICE>No</ISINVOICE>" & vbCrLf
        strXML = strXML & _
                                "<DIFFACTUALQTY>No</DIFFACTUALQTY>" & vbCrLf
        strXML = strXML & _
                                "<REFERENCE>" & reference & "</REFERENCE>" & vbCrLf
        strXML = strXML & _
                                "<PARTYLEDGERNAME>" & partyLedger & "</PARTYLEDGERNAME>" & vbCrLf
        strXML = strXML & _
                                "<NARRATION>" & narration & "</NARRATION>" & vbCrLf
        strXML = strXML & _
                                "<ENTEREDBY>Evolution</ENTEREDBY>" & vbCrLf
        strXML = strXML & _
                                "<ASPAYSLIP>No</ASPAYSLIP>" & vbCrLf
        strXML = strXML & _
                                "<ALLLEDGERENTRIES.LIST>" & vbCrLf
        strXML = strXML & _
                                    "<NARRATION></NARRATION>" & vbCrLf
        strXML = strXML & _
                                    "<REMOVEZEROENTRIES>No</REMOVEZEROENTRIES>" & vbCrLf
        strXML = strXML & _
                                    "<ISDEEMEDPOSITIVE>Yes</ISDEEMEDPOSITIVE>" & vbCrLf
        strXML = strXML & _
                                    "<LEDGERFROMITEM>No</LEDGERFROMITEM>" & vbCrLf
        strXML = strXML & _
                                    "<LEDGERNAME>" & debitAcct & "</LEDGERNAME>" & vbCrLf
        strXML = strXML & _
                                    "<AMOUNT>-u" & fInAmt & " @ Tshs " & ExRate & "/u" & " = -Tshs " & invAmount & "</AMOUNT>" & vbCrLf
        strXML = strXML & _
                                "</ALLLEDGERENTRIES.LIST>" & vbCrLf


        'goes to Lines at setXML8 LINES

    End Sub

    Public Sub getSerialsforLineCC()
        'change here
        getSerialNo = New ADODB.Recordset

        'getSerialNo.Open "select * from dbo._btblInvoiceLineSN where iSerialInvoiceID=" & AutoIndex, con, adOpenStatic
        getSerialNo.Open("select * from dbo._btblInvoiceLineSN where iSerialInvoiceID=" & AutoIndex & " and iSerialInvoiceLineID= " & idLine, con, ADODB.CursorTypeEnum.adOpenStatic)

        If getSerialNo.RecordCount > 0 Then


            strXML = strXML & _
                                   "<UDF:SUNITEMSERIALNO.LIST TYPE=" & DQ & "STRING" & DQ & " ISLIST=" & DQ & "YES" & DQ & " DESC=" & DQ & "`SUNItemSerialNo`" & DQ & ">" & vbCrLf


            'getSerialNo
            Do Until getSerialNo.EOF = True


                srlNo = getSerialNo("cSerialNumber").Value

                strXML = strXML & _
                                     "<UDF:SUNITEMSERIALNO DESC=" & DQ & "`SUNItemSerialNo`"" >" & srlNo & "</UDF:SUNITEMSERIALNO>" & vbCrLf
                getSerialNo.MoveNext()
            Loop
            'End With

            getSerialNo.Close()

            strXML = strXML & _
                                    "</UDF:SUNITEMSERIALNO.LIST>" & vbCrLf

            'change till here
        End If
    End Sub
    Public Sub SetXML8_LinesCC()
        strXML = strXML & _
                                "<ALLLEDGERENTRIES.LIST>" & vbCrLf
        strXML = strXML & _
                                     "<NARRATION>" & LineQty & "@" & LinePriceExcl & "&lt;" & LineDiscount & "&gt;" & "&lt;" & LineCode & "&gt;" & "&lt;" & LineDesc & "&gt;" & "&lt;" & LineVAT & "&gt;" & "</NARRATION>" & vbCrLf
        'strXML = strXML & _
        '                           "<NARRATION>" & LineQty & "@" & LinePriceExcl & "/" & LineDiscount & "/" & LineCode & "/" & LineDesc & "</NARRATION>" & vbCrLf

        Call getSerialsforLineCC()

        strXML = strXML & _
                                    "<REMOVEZEROENTRIES>No</REMOVEZEROENTRIES>" & vbCrLf
        strXML = strXML & _
                                    "<ISDEEMEDPOSITIVE>No</ISDEEMEDPOSITIVE>" & vbCrLf
        strXML = strXML & _
                                    "<LEDGERFROMITEM>No</LEDGERFROMITEM>" & vbCrLf
        strXML = strXML & _
                                    "<LEDGERNAME>" & creditAcct & "</LEDGERNAME>" & vbCrLf
        strXML = strXML & _
                                    "<AMOUNT>u" & LinePriceExcl & " @ Tshs " & ExRate & "/u" & " = Tshs " & LinePriceLocal & "</AMOUNT>" & vbCrLf
        'strXML = strXML & _
        '                            "<AMOUNT>u" & fInAmtExcl & " @ Tshs " & ExRate & "/u" & " = Tshs " & InAmtExcl & "</AMOUNT>" & vbCrLf
        strXML = strXML & _
                                "</ALLLEDGERENTRIES.LIST>" & vbCrLf
    End Sub
    Public Sub SetXML8_VatCC()

        strXML = strXML & _
                                "<ALLLEDGERENTRIES.LIST>" & vbCrLf
        strXML = strXML & _
                                    "<LEDGERNAME>Vat Sales</LEDGERNAME>" & vbCrLf
        strXML = strXML & _
                                    "<GSTCLASS/>" & vbCrLf
        strXML = strXML & _
                                    "<ISDEEMEDPOSITIVE>No</ISDEEMEDPOSITIVE>" & vbCrLf
        strXML = strXML & _
                                    "<LEDGERFROMITEM>No</LEDGERFROMITEM>" & vbCrLf
        strXML = strXML & _
                                    "<REMOVEZEROENTRIES>No</REMOVEZEROENTRIES>" & vbCrLf
        strXML = strXML & _
                                    "<ISPARTYLEDGER>No</ISPARTYLEDGER>" & vbCrLf
        strXML = strXML & _
                                    "<AMOUNT>u" & fTaxValue & " @ Tshs " & ExRate & "/u" & " = Tshs " & TaxValue & "</AMOUNT>" & vbCrLf
        strXML = strXML & _
                                "</ALLLEDGERENTRIES.LIST>" & vbCrLf
    End Sub

    Public Sub setXML_EndCC()
        strXML = strXML & _
                            "</VOUCHER>" & vbCrLf
        strXML = strXML & _
                        "</TALLYMESSAGE>" & vbCrLf
        strXML = strXML & _
                    "</REQUESTDATA>" & vbCrLf
        strXML = strXML & _
                "</IMPORTDATA>" & vbCrLf
        strXML = strXML & _
            "</BODY>" & vbCrLf
        strXML = strXML & _
        "</ENVELOPE>" + vbCrLf

    End Sub

End Class
