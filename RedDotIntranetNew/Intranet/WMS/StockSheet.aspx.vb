Imports System
Imports System.Collections.Generic
Imports System.Linq
Imports System.Web
Imports System.Web.UI
Imports System.Web.UI.WebControls
Imports System.Data
Imports System.IO
Imports Microsoft.Office.Interop.Excel
Imports System.Reflection
Imports Microsoft.Office.Interop

Partial Class Intranet_WMS_StockSheet

    Inherits System.Web.UI.Page

    Dim Stock As New WMSClsStock()
    Protected Sub Page_Load(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.Load

        Page.Title = " Stock Sheet"
       
        Dim myWorkBook As Workbook
        Dim appExcel As Microsoft.Office.Interop.Excel.Application
        Dim NewBook As Workbook

        
        Dim rowCount As Long = 0
        Dim colCount As Long = 0

        Dim rRGB As Integer = 242
        Dim gRGB As Integer = 164
        Dim bRGB As Integer = 8

        Dim fso 'As Scripting.FileSystemObject
        Dim excelFile As String = Server.MapPath("~/Intranet/WMS/Exportfiles/StockSheet_" + DateTime.Now.ToString("yyyy-mm-dd") + ".xlsx")
        Dim docStagesFile As String = Server.MapPath("~/Intranet/WMS/Exportfiles/docStages_" + DateTime.Now.ToString("yyyy-mm-dd") + ".xlsx")

        If File.Exists(excelFile) Then
            File.Delete(excelFile)
        End If

        If File.Exists(docStagesFile) Then
            File.Delete(docStagesFile)
        End If


        appExcel = CreateObject("Excel.Application")
        appExcel.Visible = True
        'Do While appExcel.Workbooks.Count > 0
        '    Workbooks(1).Delete()
        'Loop
        NewBook = appExcel.Workbooks.Add
        appExcel.Worksheets(1).Name = "BOE"
        appExcel.Worksheets(2).Name = "DO"
       
        appExcel.Worksheets(1).Activate()


        rowCount = rowCount + 1
        colCount = colCount + 1

        appExcel.Worksheets(1).Cells(rowCount, colCount) = "creation"
        appExcel.Worksheets(1).Cells(rowCount, colCount).Interior.Color = RGB(rRGB, gRGB, bRGB)


        colCount = colCount + 1
        appExcel.Worksheets(1).Cells(rowCount, colCount) = "#days"
        appExcel.Worksheets(1).Cells(rowCount, colCount).Interior.Color = RGB(rRGB, gRGB, bRGB)

        colCount = colCount + 1
        appExcel.Worksheets(1).Cells(rowCount, colCount) = "confirmation"
        appExcel.Worksheets(1).Cells(rowCount, colCount).Interior.Color = RGB(rRGB, gRGB, bRGB)

        colCount = colCount + 1
        appExcel.Worksheets(1).Cells(rowCount, colCount) = "#days"
        appExcel.Worksheets(1).Cells(rowCount, colCount).Interior.Color = RGB(rRGB, gRGB, bRGB)

        colCount = colCount + 1
        appExcel.Worksheets(1).Cells(rowCount, colCount) = "unstuffingTally"
        appExcel.Worksheets(1).Cells(rowCount, colCount).Interior.Color = RGB(rRGB, gRGB, bRGB)

        colCount = colCount + 1
        appExcel.Worksheets(1).Cells(rowCount, colCount) = "#days"
        appExcel.Worksheets(1).Cells(rowCount, colCount).Interior.Color = RGB(rRGB, gRGB, bRGB)

        colCount = colCount + 1
        appExcel.Worksheets(1).Cells(rowCount, colCount) = "received"
        appExcel.Worksheets(1).Cells(rowCount, colCount).Interior.Color = RGB(rRGB, gRGB, bRGB)

        colCount = colCount + 1
        appExcel.Worksheets(1).Cells(rowCount, colCount) = "total #days"
        appExcel.Worksheets(1).Cells(rowCount, colCount).Interior.Color = RGB(rRGB, gRGB, bRGB)

        Dim dtdocStages As System.Data.DataTable = Stock.docStages()
       
        For Each row As DataRow In dtdocStages.Rows

            If row("docType") = "PRE" Then
                rowCount = rowCount + 1
                colCount = 0

                colCount = colCount + 1
                appExcel.Worksheets(1).Cells(rowCount, colCount) = row("creation")
                appExcel.Worksheets(1).Cells(rowCount, colCount).Interior.Color = 65535

                colCount = colCount + 1
                appExcel.Worksheets(1).Cells(rowCount, colCount) = "=IF(OR(A" & rowCount & "=0,C" & rowCount & "=0),""N/A"",C" & rowCount & "-A" & rowCount & ")"

                appExcel.Worksheets(1).Cells(rowCount, colCount).Interior.Color = 14136213

                colCount = colCount + 1
                appExcel.Worksheets(1).Cells(rowCount, colCount) = row("confirmation")
                appExcel.Worksheets(1).Cells(rowCount, colCount).Interior.Color = 65535

                colCount = colCount + 1
                appExcel.Worksheets(1).Cells(rowCount, colCount) = "=IF(OR(C" & rowCount & "=0,E" & rowCount & "=0),""N/A"",E" & rowCount & "-C" & rowCount & ")"
                appExcel.Worksheets(1).Cells(rowCount, colCount).Interior.Color = 14136213

                colCount = colCount + 1
                appExcel.Worksheets(1).Cells(rowCount, colCount) = row("unstuffingTally")
                appExcel.Worksheets(1).Cells(rowCount, colCount).Interior.Color = 65535

                colCount = colCount + 1
                appExcel.Worksheets(1).Cells(rowCount, colCount) = "=IF(OR(E" & rowCount & "=0,G" & rowCount & "=0),""N/A"",G" & rowCount & "-E" & rowCount & ")"
                appExcel.Worksheets(1).Cells(rowCount, colCount).Interior.Color = 14136213

                colCount = colCount + 1
                appExcel.Worksheets(1).Cells(rowCount, colCount) = row("Received")
                appExcel.Worksheets(1).Cells(rowCount, colCount).Interior.Color = 65535

                colCount = colCount + 1
                appExcel.Worksheets(1).Cells(rowCount, colCount) = "=IFERROR($B" & rowCount & "+$D" & rowCount & "+$F" & rowCount & ",""N/A"")"
                appExcel.Worksheets(1).Cells(rowCount, colCount).Interior.Color = 10147522


            End If

        Next








        appExcel.Worksheets(1).Columns("A").NumberFormat = "m/d/yyyy"
        appExcel.Worksheets(1).Columns("B").NumberFormat = "0.0"
        appExcel.Worksheets(1).Columns("C").NumberFormat = "m/d/yyyy"
        appExcel.Worksheets(1).Columns("D").NumberFormat = "0.0"
        appExcel.Worksheets(1).Columns("E").NumberFormat = "m/d/yyyy"
        appExcel.Worksheets(1).Columns("F").NumberFormat = "0.0"
        appExcel.Worksheets(1).Columns("G").NumberFormat = "m/d/yyyy"
        appExcel.Worksheets(1).Columns("H").NumberFormat = "0.0"
        'Columns("B").ColumnWidth = 35
        'Columns("D").AutoFit



        appExcel.Worksheets(2).Activate()

        rowCount = 0
        colCount = 0

        rowCount = rowCount + 1
        colCount = colCount + 1

        appExcel.Worksheets(2).Cells(rowCount, colCount) = "creation"
        appExcel.Worksheets(2).Cells(rowCount, colCount).Interior.Color = RGB(rRGB, gRGB, bRGB)

        colCount = colCount + 1
        appExcel.Worksheets(2).Cells(rowCount, colCount) = "#days"
        appExcel.Worksheets(2).Cells(rowCount, colCount).Interior.Color = RGB(rRGB, gRGB, bRGB)

        colCount = colCount + 1
        appExcel.Worksheets(2).Cells(rowCount, colCount) = "release"
        appExcel.Worksheets(2).Cells(rowCount, colCount).Interior.Color = RGB(rRGB, gRGB, bRGB)

        colCount = colCount + 1
        appExcel.Worksheets(2).Cells(rowCount, colCount) = "#days"
        appExcel.Worksheets(2).Cells(rowCount, colCount).Interior.Color = RGB(rRGB, gRGB, bRGB)

        colCount = colCount + 1
        appExcel.Worksheets(2).Cells(rowCount, colCount) = "pickTicket"
        appExcel.Worksheets(2).Cells(rowCount, colCount).Interior.Color = RGB(rRGB, gRGB, bRGB)

        colCount = colCount + 1
        appExcel.Worksheets(2).Cells(rowCount, colCount) = "#days"
        appExcel.Worksheets(2).Cells(rowCount, colCount).Interior.Color = RGB(rRGB, gRGB, bRGB)

        colCount = colCount + 1
        appExcel.Worksheets(2).Cells(rowCount, colCount) = "deliveryNote"
        appExcel.Worksheets(2).Cells(rowCount, colCount).Interior.Color = RGB(rRGB, gRGB, bRGB)

        colCount = colCount + 1
        appExcel.Worksheets(2).Cells(rowCount, colCount) = "#days"
        appExcel.Worksheets(2).Cells(rowCount, colCount).Interior.Color = RGB(rRGB, gRGB, bRGB)

        colCount = colCount + 1
        appExcel.Worksheets(2).Cells(rowCount, colCount) = "customerInvoice"
        appExcel.Worksheets(2).Cells(rowCount, colCount).Interior.Color = RGB(rRGB, gRGB, bRGB)

        colCount = colCount + 1
        appExcel.Worksheets(2).Cells(rowCount, colCount) = "#days"
        appExcel.Worksheets(2).Cells(rowCount, colCount).Interior.Color = RGB(rRGB, gRGB, bRGB)

        colCount = colCount + 1
        appExcel.Worksheets(2).Cells(rowCount, colCount) = "DOInvoice"
        appExcel.Worksheets(2).Cells(rowCount, colCount).Interior.Color = RGB(rRGB, gRGB, bRGB)

        colCount = colCount + 1
        appExcel.Worksheets(2).Cells(rowCount, colCount) = "#days"
        appExcel.Worksheets(2).Cells(rowCount, colCount).Interior.Color = RGB(rRGB, gRGB, bRGB)

        colCount = colCount + 1
        appExcel.Worksheets(2).Cells(rowCount, colCount) = "deliveryAdvice"
        appExcel.Worksheets(2).Cells(rowCount, colCount).Interior.Color = RGB(rRGB, gRGB, bRGB)

        colCount = colCount + 1
        appExcel.Worksheets(2).Cells(rowCount, colCount) = "#days"
        appExcel.Worksheets(2).Cells(rowCount, colCount).Interior.Color = RGB(rRGB, gRGB, bRGB)

        colCount = colCount + 1
        appExcel.Worksheets(2).Cells(rowCount, colCount) = "XFROwnership"
        appExcel.Worksheets(2).Cells(rowCount, colCount).Interior.Color = RGB(rRGB, gRGB, bRGB)

        colCount = colCount + 1
        appExcel.Worksheets(2).Cells(rowCount, colCount) = "total #days"
        appExcel.Worksheets(2).Cells(rowCount, colCount).Interior.Color = RGB(rRGB, gRGB, bRGB)


        For Each rows As DataRow In dtdocStages.Rows
            If rows("docType") = "DO" Then



                rowCount = rowCount + 1
                colCount = 0

                colCount = colCount + 1
                appExcel.Worksheets(2).Cells(rowCount, colCount) = rows("creation")
                appExcel.Worksheets(2).Cells(rowCount, colCount).Interior.Color = 65535

                colCount = colCount + 1
                appExcel.Worksheets(2).Cells(rowCount, colCount) = "=IF(OR(A" & rowCount & "=0,C" & rowCount & "=0),""N/A"",C" & rowCount & "-A" & rowCount & ")"
                appExcel.Worksheets(2).Cells(rowCount, colCount).Interior.Color = 14136213

                colCount = colCount + 1
                appExcel.Worksheets(2).Cells(rowCount, colCount) = rows("Release")
                appExcel.Worksheets(2).Cells(rowCount, colCount).Interior.Color = 65535

                colCount = colCount + 1
                appExcel.Worksheets(2).Cells(rowCount, colCount) = "=IF(OR(C" & rowCount & "=0,E" & rowCount & "=0),""N/A"",E" & rowCount & "-C" & rowCount & ")"
                appExcel.Worksheets(2).Cells(rowCount, colCount).Interior.Color = 14136213

                colCount = colCount + 1
                appExcel.Worksheets(2).Cells(rowCount, colCount) = rows("pickTicket")
                appExcel.Worksheets(2).Cells(rowCount, colCount).Interior.Color = 65535

                colCount = colCount + 1
                appExcel.Worksheets(2).Cells(rowCount, colCount) = "=IF(OR(E" & rowCount & "=0,G" & rowCount & "=0),""N/A"",G" & rowCount & "-E" & rowCount & ")"
                appExcel.Worksheets(2).Cells(rowCount, colCount).Interior.Color = 14136213

                colCount = colCount + 1
                appExcel.Worksheets(2).Cells(rowCount, colCount) = rows("deliveryNote")
                appExcel.Worksheets(2).Cells(rowCount, colCount).Interior.Color = 65535

                colCount = colCount + 1
                appExcel.Worksheets(2).Cells(rowCount, colCount) = "=IF(OR(G" & rowCount & "=0,I" & rowCount & "=0),""N/A"",I" & rowCount & "-G" & rowCount & ")"
                appExcel.Worksheets(2).Cells(rowCount, colCount).Interior.Color = 14136213

                colCount = colCount + 1
                appExcel.Worksheets(2).Cells(rowCount, colCount) = rows("customerInvoice")
                appExcel.Worksheets(2).Cells(rowCount, colCount).Interior.Color = 65535

                colCount = colCount + 1
                appExcel.Worksheets(2).Cells(rowCount, colCount) = "=IF(OR(I" & rowCount & "=0,K" & rowCount & "=0),""N/A"",K" & rowCount & "-I" & rowCount & ")"
                appExcel.Worksheets(2).Cells(rowCount, colCount).Interior.Color = 14136213

                colCount = colCount + 1
                appExcel.Worksheets(2).Cells(rowCount, colCount) = rows("DOInvoice")
                appExcel.Worksheets(2).Cells(rowCount, colCount).Interior.Color = 65535

                colCount = colCount + 1
                appExcel.Worksheets(2).Cells(rowCount, colCount) = "=IF(OR(K" & rowCount & "=0,M" & rowCount & "=0),""N/A"",M" & rowCount & "-K" & rowCount & ")"
                appExcel.Worksheets(2).Cells(rowCount, colCount).Interior.Color = 14136213

                colCount = colCount + 1
                appExcel.Worksheets(2).Cells(rowCount, colCount) = rows("deliveryAdvice")
                appExcel.Worksheets(2).Cells(rowCount, colCount).Interior.Color = 65535

                colCount = colCount + 1
                appExcel.Worksheets(2).Cells(rowCount, colCount) = "=IF(OR(M" & rowCount & "=0,O" & rowCount & "=0),""N/A"",O" & rowCount & "-M" & rowCount & ")"
                appExcel.Worksheets(2).Cells(rowCount, colCount).Interior.Color = 14136213

                colCount = colCount + 1
                appExcel.Worksheets(2).Cells(rowCount, colCount) = rows("XFROwnership")
                appExcel.Worksheets(2).Cells(rowCount, colCount).Interior.Color = 65535

                colCount = colCount + 1
                appExcel.Worksheets(2).Cells(rowCount, colCount) = "=IFERROR($B" & rowCount & "+$D" & rowCount & "+$F" & rowCount & ",""N/A"")"
                appExcel.Worksheets(2).Cells(rowCount, colCount).Interior.Color = 10147522




            End If
        Next
     


        appExcel.Worksheets(2).Columns("A").NumberFormat = "m/d/yyyy"
        appExcel.Worksheets(2).Columns("B").NumberFormat = "0.0"
        appExcel.Worksheets(2).Columns("C").NumberFormat = "m/d/yyyy"
        appExcel.Worksheets(2).Columns("D").NumberFormat = "0.0"
        appExcel.Worksheets(2).Columns("E").NumberFormat = "m/d/yyyy"
        appExcel.Worksheets(2).Columns("F").NumberFormat = "0.0"
        appExcel.Worksheets(2).Columns("G").NumberFormat = "m/d/yyyy"
        appExcel.Worksheets(2).Columns("H").NumberFormat = "0.0"
        appExcel.Worksheets(2).Columns("I").NumberFormat = "m/d/yyyy"
        appExcel.Worksheets(2).Columns("J").NumberFormat = "0.0"
        appExcel.Worksheets(2).Columns("K").NumberFormat = "m/d/yyyy"
        appExcel.Worksheets(2).Columns("L").NumberFormat = "0.0"
        appExcel.Worksheets(2).Columns("M").NumberFormat = "m/d/yyyy"
        appExcel.Worksheets(2).Columns("N").NumberFormat = "0.0"
        appExcel.Worksheets(2).Columns("O").NumberFormat = "m/d/yyyy"
        appExcel.Worksheets(2).Columns("P").NumberFormat = "0.0"



        NewBook.SaveAs(Filename:=docStagesFile)

        ' Call sendEmail(docStagesFile, getEmailAddresses("docStagesEmailList"))

        appExcel.Worksheets(1).Cells.Delete()
        appExcel.Worksheets(2).Cells.Delete()

        '======================================================================
        With NewBook
            .Title = "Stock Sheet"
            .Subject = "Sales"
        End With
        appExcel.Worksheets.Add()
        appExcel.Worksheets(1).Name = "Stock Sheet"
        'Worksheets.Add
        appExcel.Worksheets(2).Name = "Expired Reservations"
        'Worksheets.Add
        appExcel.Worksheets(3).Name = "Reserved Stock"


        '======================================================================
        appExcel.Worksheets(1).Activate() 'Stock Sheet


        rowCount = 0
        colCount = 0

        rRGB = 242
        gRGB = 164
        bRGB = 8

        rowCount = rowCount + 1
        colCount = colCount + 1
        appExcel.Worksheets(1).Cells(rowCount, colCount) = "Part#"
        appExcel.Worksheets(1).Cells(rowCount, colCount).Interior.Color = RGB(rRGB, gRGB, bRGB)

        colCount = colCount + 1
        appExcel.Worksheets(1).Cells(rowCount, colCount) = "Description"
        appExcel.Worksheets(1).Cells(rowCount, colCount).Interior.Color = RGB(rRGB, gRGB, bRGB)

        colCount = colCount + 1
        appExcel.Worksheets(1).Cells(rowCount, colCount) = "Warehouse"
        appExcel.Worksheets(1).Cells(rowCount, colCount).Interior.Color = RGB(rRGB, gRGB, bRGB)

        colCount = colCount + 1
        appExcel.Worksheets(1).Cells(rowCount, colCount) = "TotQty"
        appExcel.Worksheets(1).Cells(rowCount, colCount).Interior.Color = RGB(rRGB, gRGB, bRGB)

        colCount = colCount + 1
        appExcel.Worksheets(1).Cells(rowCount, colCount) = "Allocated"
        appExcel.Worksheets(1).Cells(rowCount, colCount).Interior.Color = RGB(rRGB, gRGB, bRGB)

        colCount = colCount + 1
        appExcel.Worksheets(1).Cells(rowCount, colCount) = "Reserved"
        appExcel.Worksheets(1).Cells(rowCount, colCount).Interior.Color = RGB(rRGB, gRGB, bRGB)

        colCount = colCount + 1
        appExcel.Worksheets(1).Cells(rowCount, colCount) = "Avail"
        appExcel.Worksheets(1).Cells(rowCount, colCount).Interior.Color = RGB(rRGB, gRGB, bRGB)

        colCount = colCount + 1
        appExcel.Worksheets(1).Cells(rowCount, colCount) = "CBM"
        appExcel.Worksheets(1).Cells(rowCount, colCount).Interior.Color = RGB(rRGB, gRGB, bRGB)

        colCount = colCount + 1
        appExcel.Worksheets(1).Cells(rowCount, colCount) = "qty10"
        appExcel.Worksheets(1).Cells(rowCount, colCount).Interior.Color = RGB(rRGB, gRGB, bRGB)

        colCount = colCount + 1
        appExcel.Worksheets(1).Cells(rowCount, colCount) = "val10"
        appExcel.Worksheets(1).Cells(rowCount, colCount).Interior.Color = RGB(rRGB, gRGB, bRGB)

        colCount = colCount + 1
        appExcel.Worksheets(1).Cells(rowCount, colCount) = "qty20"
        appExcel.Worksheets(1).Cells(rowCount, colCount).Interior.Color = RGB(rRGB, gRGB, bRGB)

        colCount = colCount + 1
        appExcel.Worksheets(1).Cells(rowCount, colCount) = "val20"
        appExcel.Worksheets(1).Cells(rowCount, colCount).Interior.Color = RGB(rRGB, gRGB, bRGB)

        colCount = colCount + 1
        appExcel.Worksheets(1).Cells(rowCount, colCount) = "qty30"
        appExcel.Worksheets(1).Cells(rowCount, colCount).Interior.Color = RGB(rRGB, gRGB, bRGB)

        colCount = colCount + 1
        appExcel.Worksheets(1).Cells(rowCount, colCount) = "val30"
        appExcel.Worksheets(1).Cells(rowCount, colCount).Interior.Color = RGB(rRGB, gRGB, bRGB)

        colCount = colCount + 1
        appExcel.Worksheets(1).Cells(rowCount, colCount) = "qty45"
        appExcel.Worksheets(1).Cells(rowCount, colCount).Interior.Color = RGB(rRGB, gRGB, bRGB)

        colCount = colCount + 1
        appExcel.Worksheets(1).Cells(rowCount, colCount) = "val45"
        appExcel.Worksheets(1).Cells(rowCount, colCount).Interior.Color = RGB(rRGB, gRGB, bRGB)

        colCount = colCount + 1
        appExcel.Worksheets(1).Cells(rowCount, colCount) = "qty60"
        appExcel.Worksheets(1).Cells(rowCount, colCount).Interior.Color = RGB(rRGB, gRGB, bRGB)

        colCount = colCount + 1
        appExcel.Worksheets(1).Cells(rowCount, colCount) = "val60"
        appExcel.Worksheets(1).Cells(rowCount, colCount).Interior.Color = RGB(rRGB, gRGB, bRGB)

        colCount = colCount + 1
        appExcel.Worksheets(1).Cells(rowCount, colCount) = "qty90"
        appExcel.Worksheets(1).Cells(rowCount, colCount).Interior.Color = RGB(rRGB, gRGB, bRGB)

        colCount = colCount + 1
        appExcel.Worksheets(1).Cells(rowCount, colCount) = "val90"
        appExcel.Worksheets(1).Cells(rowCount, colCount).Interior.Color = RGB(rRGB, gRGB, bRGB)

        colCount = colCount + 1
        appExcel.Worksheets(1).Cells(rowCount, colCount) = "qty120"
        appExcel.Worksheets(1).Cells(rowCount, colCount).Interior.Color = RGB(rRGB, gRGB, bRGB)

        colCount = colCount + 1
        appExcel.Worksheets(1).Cells(rowCount, colCount) = "val120"
        appExcel.Worksheets(1).Cells(rowCount, colCount).Interior.Color = RGB(rRGB, gRGB, bRGB)

        colCount = colCount + 1
        appExcel.Worksheets(1).Cells(rowCount, colCount) = "qty120plus"
        appExcel.Worksheets(1).Cells(rowCount, colCount).Interior.Color = RGB(rRGB, gRGB, bRGB)

        colCount = colCount + 1
        appExcel.Worksheets(1).Cells(rowCount, colCount) = "val120plus"
        appExcel.Worksheets(1).Cells(rowCount, colCount).Interior.Color = RGB(rRGB, gRGB, bRGB)

        Dim dtexcelStockSheet As System.Data.DataTable = Stock.excelStockSheet()
        For Each row As DataRow In dtexcelStockSheet.Rows
            rowCount = rowCount + 1
            colCount = 0

            colCount = colCount + 1
            appExcel.Worksheets(1).Cells(rowCount, colCount) = row("part")
            appExcel.Worksheets(1).Cells(rowCount, colCount).Interior.Color = RGB(150, 150, 150)

            colCount = colCount + 1
            appExcel.Worksheets(1).Cells(rowCount, colCount) = row("description")
            appExcel.Worksheets(1).Cells(rowCount, colCount).Interior.Color = RGB(190, 190, 190)

            colCount = colCount + 1
            appExcel.Worksheets(1).Cells(rowCount, colCount) = row("warehouse")
            appExcel.Worksheets(1).Cells(rowCount, colCount).Interior.Color = RGB(150, 150, 150)

            colCount = colCount + 1
            appExcel.Worksheets(1).Cells(rowCount, colCount) = row("TotQty")
            appExcel.Worksheets(1).Cells(rowCount, colCount).Interior.Color = 15853019

            colCount = colCount + 1
            appExcel.Worksheets(1).Cells(rowCount, colCount) = row("Allocated")
            appExcel.Worksheets(1).Cells(rowCount, colCount).Interior.Color = 14994616

            colCount = colCount + 1
            appExcel.Worksheets(1).Cells(rowCount, colCount) = row("reserved")
            appExcel.Worksheets(1).Cells(rowCount, colCount).Interior.Color = 14136213

            colCount = colCount + 1
            appExcel.Worksheets(1).Cells(rowCount, colCount) = row("Avail")
            appExcel.Worksheets(1).Cells(rowCount, colCount).Interior.Color = 65535

            colCount = colCount + 1
            appExcel.Worksheets(1).Cells(rowCount, colCount) = row("CBM")
            appExcel.Worksheets(1).Cells(rowCount, colCount).Interior.Color = 10147522

            colCount = colCount + 1
            appExcel.Worksheets(1).Cells(rowCount, colCount) = row("qty10")
            appExcel.Worksheets(1).Cells(rowCount, colCount).Interior.Color = 10147522

            colCount = colCount + 1
            appExcel.Worksheets(1).Cells(rowCount, colCount) = row("val10")
            appExcel.Worksheets(1).Cells(rowCount, colCount).Interior.Color = 10147522

            colCount = colCount + 1
            appExcel.Worksheets(1).Cells(rowCount, colCount) = row("qty20")
            appExcel.Worksheets(1).Cells(rowCount, colCount).Interior.Color = 10147522

            colCount = colCount + 1
            appExcel.Worksheets(1).Cells(rowCount, colCount) = row("val20")
            appExcel.Worksheets(1).Cells(rowCount, colCount).Interior.Color = 10147522

            colCount = colCount + 1
            appExcel.Worksheets(1).Cells(rowCount, colCount) = row("qty30")
            appExcel.Worksheets(1).Cells(rowCount, colCount).Interior.Color = 10147522

            colCount = colCount + 1
            appExcel.Worksheets(1).Cells(rowCount, colCount) = row("val30")
            appExcel.Worksheets(1).Cells(rowCount, colCount).Interior.Color = 10147522

            colCount = colCount + 1
            appExcel.Worksheets(1).Cells(rowCount, colCount) = row("qty45")
            appExcel.Worksheets(1).Cells(rowCount, colCount).Interior.Color = 10147522

            colCount = colCount + 1
            appExcel.Worksheets(1).Cells(rowCount, colCount) = row("val45")
            appExcel.Worksheets(1).Cells(rowCount, colCount).Interior.Color = 10147522

            colCount = colCount + 1
            appExcel.Worksheets(1).Cells(rowCount, colCount) = row("qty60")
            appExcel.Worksheets(1).Cells(rowCount, colCount).Interior.Color = 10147522

            colCount = colCount + 1
            appExcel.Worksheets(1).Cells(rowCount, colCount) = row("val60")
            appExcel.Worksheets(1).Cells(rowCount, colCount).Interior.Color = 10147522

            colCount = colCount + 1
            appExcel.Worksheets(1).Cells(rowCount, colCount) = row("qty90")
            appExcel.Worksheets(1).Cells(rowCount, colCount).Interior.Color = 10147522

            colCount = colCount + 1
            appExcel.Worksheets(1).Cells(rowCount, colCount) = row("val90")
            appExcel.Worksheets(1).Cells(rowCount, colCount).Interior.Color = 10147522

            colCount = colCount + 1
            appExcel.Worksheets(1).Cells(rowCount, colCount) = row("qty120")
            appExcel.Worksheets(1).Cells(rowCount, colCount).Interior.Color = 10147522

            colCount = colCount + 1
            appExcel.Worksheets(1).Cells(rowCount, colCount) = row("val120")
            appExcel.Worksheets(1).Cells(rowCount, colCount).Interior.Color = 10147522

            colCount = colCount + 1
            appExcel.Worksheets(1).Cells(rowCount, colCount) = row("qty120plus")
            appExcel.Worksheets(1).Cells(rowCount, colCount).Interior.Color = 10147522

            colCount = colCount + 1
            appExcel.Worksheets(1).Cells(rowCount, colCount) = row("val120plus")
            appExcel.Worksheets(1).Cells(rowCount, colCount).Interior.Color = 10147522

        Next
        
        appExcel.Worksheets(1).Columns("A").AutoFit()
        appExcel.Worksheets(1).Columns("B").ColumnWidth = 35 'Description
        appExcel.Worksheets(1).Columns("C").ColumnWidth = 10 'Warehouse
        appExcel.Worksheets(1).Columns("D").AutoFit()
        appExcel.Worksheets(1).Columns("E").AutoFit()
        appExcel.Worksheets(1).Columns("F").AutoFit()
        appExcel.Worksheets(1).Columns("G").AutoFit()
        appExcel.Worksheets(1).Columns("H").AutoFit()


        appExcel.Worksheets(1).Cells(2, 1).Select()
        'appExcel.Worksheets(1).FreezePanes = True


        '======================================================================
        appExcel.Worksheets(2).Activate() 'Expired Reservations







        rowCount = 0
        colCount = 0

        rRGB = 242
        gRGB = 164
        bRGB = 8

        rowCount = rowCount + 1
        colCount = colCount + 1
        appExcel.Worksheets(2).Cells(rowCount, colCount) = "customer"
        appExcel.Worksheets(2).Cells(rowCount, colCount).Interior.Color = RGB(rRGB, gRGB, bRGB)

        colCount = colCount + 1
        appExcel.Worksheets(2).Cells(rowCount, colCount) = "part#"
        appExcel.Worksheets(2).Cells(rowCount, colCount).Interior.Color = RGB(rRGB, gRGB, bRGB)

        colCount = colCount + 1
        appExcel.Worksheets(2).Cells(rowCount, colCount) = "qty"
        appExcel.Worksheets(2).Cells(rowCount, colCount).Interior.Color = RGB(rRGB, gRGB, bRGB)

        colCount = colCount + 1
        appExcel.Worksheets(2).Cells(rowCount, colCount) = "description"
        appExcel.Worksheets(2).Cells(rowCount, colCount).Interior.Color = RGB(rRGB, gRGB, bRGB)

        colCount = colCount + 1
        appExcel.Worksheets(2).Cells(rowCount, colCount) = "created"
        appExcel.Worksheets(2).Cells(rowCount, colCount).Interior.Color = RGB(rRGB, gRGB, bRGB)

        colCount = colCount + 1
        appExcel.Worksheets(2).Cells(rowCount, colCount) = "expired"
        appExcel.Worksheets(2).Cells(rowCount, colCount).Interior.Color = RGB(rRGB, gRGB, bRGB)

        colCount = colCount + 1
        appExcel.Worksheets(2).Cells(rowCount, colCount) = "reserve order#"
        appExcel.Worksheets(2).Cells(rowCount, colCount).Interior.Color = RGB(rRGB, gRGB, bRGB)

        colCount = colCount + 1
        appExcel.Worksheets(2).Cells(rowCount, colCount) = "DO#"
        appExcel.Worksheets(2).Cells(rowCount, colCount).Interior.Color = RGB(rRGB, gRGB, bRGB)

        colCount = colCount + 1
        appExcel.Worksheets(2).Cells(rowCount, colCount) = "notes"
        appExcel.Worksheets(2).Cells(rowCount, colCount).Interior.Color = RGB(rRGB, gRGB, bRGB)


        Dim dtDO As System.Data.DataTable = Stock.DoList()
        'rsDO = CurrentDb.OpenRecordset("deliveryOrder")
        'rsStock = CurrentDb.OpenRecordset("expireableReservations")
        For Each row As DataRow In dtDO.Rows
            'check if doid is in dtstock
            Dim dtStock As System.Data.DataTable = Stock.checkexperibleReservation(row("do_id"))
            Dim doid As String
            If dtStock.Rows.Count = 0 Then
                Continue For
                'If dtStock.Rows.Count = 0 Then
                '    'MsgBox("Impossible: DO# [" & Str(rsDO![do_id]) & "] not found")
                '    Exit Sub
            Else
                ' update the status 

                Stock.updateStockStatus(row("do_id"))

                rowCount = rowCount + 1
                colCount = 0

                colCount = colCount + 1
                appExcel.Worksheets(2).Cells(rowCount, colCount) = dtStock.Rows(0)("customer")
                appExcel.Worksheets(2).Cells(rowCount, colCount).Interior.Color = RGB(150, 150, 150)

                colCount = colCount + 1
                appExcel.Worksheets(2).Cells(rowCount, colCount) = dtStock.Rows(0)("part#")
                appExcel.Worksheets(2).Cells(rowCount, colCount).Interior.Color = RGB(150, 190, 150)

                colCount = colCount + 1
                appExcel.Worksheets(2).Cells(rowCount, colCount) = dtStock.Rows(0)("qty")
                appExcel.Worksheets(2).Cells(rowCount, colCount).Interior.Color = RGB(150, 150, 150)

                colCount = colCount + 1
                appExcel.Worksheets(2).Cells(rowCount, colCount) = dtStock.Rows(0)("description")
                appExcel.Worksheets(2).Cells(rowCount, colCount).Interior.Color = RGB(150, 190, 150)

                colCount = colCount + 1
                appExcel.Worksheets(2).Cells(rowCount, colCount) = CDate(dtStock.Rows(0)("created"))
                appExcel.Worksheets(2).Cells(rowCount, colCount).Interior.Color = RGB(190, 150, 190)

                colCount = colCount + 1
                appExcel.Worksheets(2).Cells(rowCount, colCount) = CDate(dtStock.Rows(0)("expiry"))
                appExcel.Worksheets(2).Cells(rowCount, colCount).Interior.Color = RGB(150, 190, 150)

                colCount = colCount + 1
                appExcel.Worksheets(2).Cells(rowCount, colCount) = dtStock.Rows(0)("reservation#")
                appExcel.Worksheets(2).Cells(rowCount, colCount).Interior.Color = 15853019

                colCount = colCount + 1
                appExcel.Worksheets(2).Cells(rowCount, colCount) = dtStock.Rows(0)("DO#")
                appExcel.Worksheets(2).Cells(rowCount, colCount).Interior.Color = 14994616

                colCount = colCount + 1
                appExcel.Worksheets(2).Cells(rowCount, colCount) = dtStock.Rows(0)("notes")
                appExcel.Worksheets(2).Cells(rowCount, colCount).Interior.Color = 14136213




            End If
        Next
       
       

       

        appExcel.Worksheets(2).Columns("A:I").AutoFit()

        appExcel.Worksheets(2).Cells(2, 1).Select()
        'appExcel.Worksheets(2).FreezePanes = True


        '======================================================================
        appExcel.Worksheets(3).Activate() 'Reserved Stock

        rowCount = 0
        colCount = 0

        rRGB = 242
        gRGB = 164
        bRGB = 8

        rowCount = rowCount + 1
        colCount = colCount + 1
        appExcel.Worksheets(3).Cells(rowCount, colCount) = "Warehouse"
        appExcel.Worksheets(3).Cells(rowCount, colCount).Interior.Color = RGB(rRGB, gRGB, bRGB)

        colCount = colCount + 1
        appExcel.Worksheets(3).Cells(rowCount, colCount) = "part#"
        appExcel.Worksheets(3).Cells(rowCount, colCount).Interior.Color = RGB(rRGB, gRGB, bRGB)

        colCount = colCount + 1
        appExcel.Worksheets(3).Cells(rowCount, colCount) = "description"
        appExcel.Worksheets(3).Cells(rowCount, colCount).Interior.Color = RGB(rRGB, gRGB, bRGB)

        colCount = colCount + 1
        appExcel.Worksheets(3).Cells(rowCount, colCount) = "customer"
        appExcel.Worksheets(3).Cells(rowCount, colCount).Interior.Color = RGB(rRGB, gRGB, bRGB)

        colCount = colCount + 1
        appExcel.Worksheets(3).Cells(rowCount, colCount) = "qty"
        appExcel.Worksheets(3).Cells(rowCount, colCount).Interior.Color = RGB(rRGB, gRGB, bRGB)

        colCount = colCount + 1
        appExcel.Worksheets(3).Cells(rowCount, colCount) = "status"
        appExcel.Worksheets(3).Cells(rowCount, colCount).Interior.Color = RGB(rRGB, gRGB, bRGB)

        colCount = colCount + 1
        appExcel.Worksheets(3).Cells(rowCount, colCount) = "expiry"
        appExcel.Worksheets(3).Cells(rowCount, colCount).Interior.Color = RGB(rRGB, gRGB, bRGB)

        colCount = colCount + 1
        appExcel.Worksheets(3).Cells(rowCount, colCount) = "order#"
        appExcel.Worksheets(3).Cells(rowCount, colCount).Interior.Color = RGB(rRGB, gRGB, bRGB)

        colCount = colCount + 1
        appExcel.Worksheets(3).Cells(rowCount, colCount) = "DO#"
        appExcel.Worksheets(3).Cells(rowCount, colCount).Interior.Color = RGB(rRGB, gRGB, bRGB)

        'rsStock = CurrentDb.OpenRecordset("reservedItems")

        Dim dtreserveitem As System.Data.DataTable = Stock.reserveitem()

        For Each row As DataRow In dtreserveitem.Rows

            rowCount = rowCount + 1
            colCount = 0

            colCount = colCount + 1
            appExcel.Worksheets(3).Cells(rowCount, colCount) = row("warehouse")
            appExcel.Worksheets(3).Cells(rowCount, colCount).Interior.Color = RGB(150, 150, 150)

            colCount = colCount + 1
            appExcel.Worksheets(3).Cells(rowCount, colCount) = row("part#")
            appExcel.Worksheets(3).Cells(rowCount, colCount).Interior.Color = RGB(190, 190, 190)

            colCount = colCount + 1
            appExcel.Worksheets(3).Cells(rowCount, colCount) = row("description")
            appExcel.Worksheets(3).Cells(rowCount, colCount).Interior.Color = RGB(150, 150, 150)

            colCount = colCount + 1
            appExcel.Worksheets(3).Cells(rowCount, colCount) = row("customer")
            appExcel.Worksheets(3).Cells(rowCount, colCount).Interior.Color = 15853019

            colCount = colCount + 1
            appExcel.Worksheets(3).Cells(rowCount, colCount) = row("qty")
            appExcel.Worksheets(3).Cells(rowCount, colCount).Interior.Color = 14994616

            colCount = colCount + 1
            appExcel.Worksheets(3).Cells(rowCount, colCount) = row("status")
            appExcel.Worksheets(3).Cells(rowCount, colCount).Interior.Color = 14136213

            colCount = colCount + 1
            If String.IsNullOrEmpty(row("expiry").ToString()) = False Then
                appExcel.Worksheets(3).Cells(rowCount, colCount) = CDate(row("expiry"))
            End If
            appExcel.Worksheets(3).Cells(rowCount, colCount).Interior.Color = 65535

            colCount = colCount + 1
            appExcel.Worksheets(3).Cells(rowCount, colCount) = row("order#")
            appExcel.Worksheets(3).Cells(rowCount, colCount).Interior.Color = 14136213

            colCount = colCount + 1
            appExcel.Worksheets(3).Cells(rowCount, colCount) = row("DO#")
            appExcel.Worksheets(3).Cells(rowCount, colCount).Interior.Color = 10147522

        Next
      


        appExcel.Worksheets(3).Columns("A").ColumnWidth = 10 'Warehouse
        appExcel.Worksheets(3).Columns("B").AutoFit()
        appExcel.Worksheets(3).Columns("C").ColumnWidth = 35 'Description
        appExcel.Worksheets(3).Columns("D").ColumnWidth = 12 'customer
        appExcel.Worksheets(3).Columns("E:I").AutoFit()

        appExcel.Worksheets(3).Cells(2, 1).Select()
        ' appExcel.Worksheets(3).FreezePanes = True

       

        ' NewBook.SaveAs(Filename:=excelFile)
        NewBook.Close()
        appExcel.Quit()
        '  Call sendEmail(excelFile, getEmailAddresses("stocksheetEmailList"))

    End Sub
End Class
