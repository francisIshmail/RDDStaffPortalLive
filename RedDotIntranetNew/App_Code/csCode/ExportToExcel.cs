using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Data;
using System.Windows.Forms;
using Excel = Microsoft.Office.Interop.Excel;

/// <summary>
/// Summary description for ExportToExcel
/// </summary>
public class ExportToExcel
{
    public ExportToExcel()
    {

    }

    public string ExportDataToExcel(DataTable table, string FileName)
    {

        string success = "Failed";
        try
        {
            //Creae an Excel application instance
            Excel.Application excelApp = new Excel.Application();

            //Create an Excel workbook instance and open it from the predefined location
            //Excel.Workbook excelWorkBook = excelApp.Workbooks.Open(@"E:\Org.xlsx");
           Excel.Workbook excelWorkBook = excelApp.Workbooks.Open(FileName);
           // Excel.Workbook excelWorkBook = excelApp.Workbooks.Add(FileName);

            bool SheetExist = false;
            Excel.Worksheet excelWorkSheet = null;
            foreach (Excel.Worksheet sheet in excelWorkBook.Sheets)
            {
                // Check the name of the current sheet
                if (sheet.Name == "Daily Visit Report")
                {
                    excelWorkSheet = sheet;
                    sheet.Cells.ClearContents();
                    SheetExist = true;
                    break; // Exit the loop now
                }
            }

            if (SheetExist == false)
            {
                //Add a new worksheet to workbook with the Datatable name
                excelWorkSheet = (Excel.Worksheet)excelWorkBook.Sheets.Add();  // excelWorkBook.Sheets.Add();
                excelWorkSheet.Name = "Daily Visit Report";
            }

            for (int i = 1; i < table.Columns.Count + 1; i++)
            {
                excelWorkSheet.Cells[1, i] = table.Columns[i - 1].ColumnName;
            }
            for (int j = 0; j < table.Rows.Count; j++)
            {
                for (int k = 0; k < table.Columns.Count; k++)
                {
                    excelWorkSheet.Cells[j + 2, k + 1] = table.Rows[j].ItemArray[k].ToString();
                }
            }

            excelWorkBook.Save();
            excelWorkBook.Close();
            excelApp.Quit();

            success = "success";


        }
        catch (Exception ex)
        {
            success = "Failed to write in excel : "+ ex.Message;
        }

        return success;
    }

}
      