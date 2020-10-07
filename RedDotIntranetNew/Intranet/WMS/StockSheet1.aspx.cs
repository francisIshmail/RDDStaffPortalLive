using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Data;
using System.IO;
using Excel = Microsoft.Office.Interop.Excel;
using System.Reflection; 

public partial class Intranet_WMS_StockSheet : System.Web.UI.Page
{
  
    WMSClsStock Stock = new WMSClsStock();
    protected void Page_Load(object sender, EventArgs e)
    {
        Page.Title = " Stock Sheet";
        DataTable dt = Stock.stookSheet();


        Microsoft.Office.Interop.Excel.Application appExcel = default(Microsoft.Office.Interop.Excel.Application);
        Microsoft.Office.Interop.Excel.Workbook NewBook = default(Microsoft.Office.Interop.Excel.Workbook);
        Microsoft.Office.Interop.Excel.Workbook myWorkBook = default(Microsoft.Office.Interop.Excel.Workbook);
      

        string excelFile = Server.MapPath("~/Intranet/WMS/Exportfiles/StockSheet" + DateTime.Now.ToString("yyy-dd-mm") + ".xlsx");
        string docStagesFile = Server.MapPath("~/Intranet/WMS/Exportfiles/docStages" + DateTime.Now.ToString("yyy-dd-mm") + ".xlsx");

        int rRGB,gRGB,bRGB;

        long rowCount,colCount;

        if (File.Exists(excelFile))
            File.Delete(excelFile);
        if (File.Exists(docStagesFile))
            File.Delete(docStagesFile); 
        
        while (appExcel.Workbooks.Count  > 0)
        {
        

        }
        //Start Excel and get Application object.
        appExcel = new Microsoft.Office.Interop.Excel.Application();
        appExcel.Visible = true;

        //Get a new workbook.
        NewBook = (Microsoft.Office.Interop.Excel.Workbook)(appExcel.Workbooks.Add(Missing.Value));
     

 


    }
}