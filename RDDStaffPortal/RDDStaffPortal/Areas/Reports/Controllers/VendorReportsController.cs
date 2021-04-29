using ClosedXML.Excel;
using ClosedXML.Extensions;
using DocumentFormat.OpenXml.Drawing.Charts;
using DocumentFormat.OpenXml.Office2010.PowerPoint;
using DocumentFormat.OpenXml.Presentation;
using RDDStaffPortal.DAL.DataModels;
using RDDStaffPortal.DAL.Report;
using System;
using System.Collections.Generic;
using System.Data;
using System.Drawing.Text;
using System.IO;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace RDDStaffPortal.Areas.Reports.Controllers
{
    public class VendorReportsController : Controller
    {
        VendorReportDBOperation VenRepDBOp = new VendorReportDBOperation();
        // GET: Reports/VendorReports
        public ActionResult Index()
        {
            DataSet dsModules = VenRepDBOp.DropDownFill();

            List<Rdd_comonDrop> BUList = new List<Rdd_comonDrop>();
            List<Rdd_comonDrop> CountryList = new List<Rdd_comonDrop>();
            if (dsModules.Tables.Count > 0)
            {
                System.Data.DataTable dtModule;
                DataRowCollection drc;
                dtModule = dsModules.Tables[0];
                drc = dtModule.Rows;
                foreach (DataRow dr in drc)
                {
                    BUList.Add(new Rdd_comonDrop()
                    {
                        Code = !string.IsNullOrWhiteSpace(dr["BU"].ToString()) ? dr["BU"].ToString() : "",
                    });
                }
                dtModule = dsModules.Tables[1];
                drc = dtModule.Rows;
                foreach (DataRow dr in drc)
                {
                    CountryList.Add(new Rdd_comonDrop()
                    {
                        Code = !string.IsNullOrWhiteSpace(dr["CountryCode"].ToString()) ? dr["CountryCode"].ToString() : "",
                        CodeName= !string.IsNullOrWhiteSpace(dr["Country"].ToString()) ? dr["Country"].ToString() : "",
                    });
                }

            }
            ViewBag.BUList = new SelectList(BUList, "Code", "Code");
            ViewBag.CountryList= new SelectList(CountryList, "Code", "CodeName");
            return View();
        }

        [Route("CannonReport")]
        public ActionResult GetCanonReport(string BU, DateTime FromDate, DateTime Todate,string  country_code)//string BU,DateTime FromDate,DateTime Todate)
        {
            DataSet ds; 

            ds = VenRepDBOp.GetCannonReport(BU,FromDate,Todate,country_code);
            var style = XLWorkbook.DefaultStyle;
            style.Font.FontColor = XLColor.Black;
            style.Border.DiagonalBorder = XLBorderStyleValues.Thick;
            style.Border.DiagonalBorderColor = XLColor.Black;
            style.Font.FontColor = XLColor.Black;
            style.Border.OutsideBorder = XLBorderStyleValues.Thick;
            var dataSet = ds;
            
            if (ds.Tables[0].Rows.Count == 0)
            {
                return View();
            }
            var wb_1 = new XLWorkbook();
            var wb_2 = new XLWorkbook();
            var wb_3 = new XLWorkbook();
            if (BU == "CANON")
            {
                ds.Tables[0].TableName = "SALES";
                ds.Tables[1].TableName = "INVENTORY";

                // Add all DataTables in the DataSet as a worksheets
                wb_1.Worksheets.Add(dataSet);
                var ws_1 = wb_1.Worksheet(1);
                ws_1.Style.Fill.BackgroundColor = XLColor.White;
                ws_1.AutoFilter.Clear();

                var ws_2 = wb_1.Worksheet(2);
                ws_2.Style.Fill.BackgroundColor = XLColor.White;
                var totalRows = ws_1.RowsUsed().Count();
                var totalRows1 = ws_2.RowsUsed().Count();

                ws_1.Range("A1:H1").Style.Fill.SetBackgroundColor(XLColor.Orange);
                ws_1.Range("A1:H1").Style.Font.Bold.ToString();
                ws_2.Range("A1:D1").Style.Fill.SetBackgroundColor(XLColor.Orange);
                ws_2.Range("A1:D1").Style.Font.Bold.ToString();

                ws_1.Range("A1:H" + (totalRows + 1) + "").Style.Border.InsideBorder = XLBorderStyleValues.Thin;
                ws_1.Range("A1:H" + (totalRows + 1) + "").Style.Border.InsideBorderColor = XLColor.Black;


                ws_1.Range("A" + (totalRows + 1) + ":H" + (totalRows + 1) + "").Style.Border.OutsideBorder = XLBorderStyleValues.Thin;
                ws_1.Range("A" + (totalRows + 1) + ":H" + (totalRows + 1) + "").Style.Border.OutsideBorderColor = XLColor.Black;


                ws_2.Range("A" + (totalRows1 + 1) + ":D" + (totalRows1 + 1) + "").Style.Border.OutsideBorder = XLBorderStyleValues.Thin;
                ws_2.Range("A" + (totalRows1 + 1) + ":D" + (totalRows1 + 1) + "").Style.Border.OutsideBorderColor = XLColor.Black;



                ws_2.Range("A1:D" + (totalRows1 + 1) + "").Style.Border.InsideBorder = XLBorderStyleValues.Thin;
                ws_2.Range("A1:D" + (totalRows1 + 1) + "").Style.Border.InsideBorderColor = XLColor.Black;

                return wb_1.Deliver("" + BU + " REPORT.xlsx", "application/vnd.openxmlformats-officedocument.spreadsheetml.sheet");
            }
            else if (BU == "LOGITECH")
            {
                ds.Tables[0].TableName = "SALES";
                ds.Tables[1].TableName = "INVENTORY";

                // Add all DataTables in the DataSet as a worksheets
                wb_1.Worksheets.Add(dataSet.Tables[0]);
                wb_2.Worksheets.Add(dataSet.Tables[1]);
                var ws_1 = wb_1.Worksheet(1);
                ws_1.Style.Fill.BackgroundColor = XLColor.White;
                ws_1.AutoFilter.Clear();

                var ws_2 = wb_2.Worksheet(1);
                ws_2.Style.Fill.BackgroundColor = XLColor.White;
                var totalRows = ws_1.RowsUsed().Count();
                var totalRows1 = ws_2.RowsUsed().Count();
                int i = 2;
                ws_1.Style.Font.FontName = "Arial";
                ws_1.Style.Font.FontSize = 8;
                ws_2.Style.Font.FontName = "Calibri";
                ws_2.Style.Font.FontSize = 11;
                // var TotalSum = ds.Tables[0].Select().Sum(w => (int)w["QTY"]);
                while (totalRows >= i)
                {
                    ws_1.Cell(i, 1).DataType = XLDataType.DateTime;
                    ws_1.Cell(i, 1).Style.DateFormat.Format = "dd MMM yyyy";
                    ws_1.Cell(i, 7).DataType = XLDataType.Number;
                    ws_1.Cell(i, 7).Style.NumberFormat.Format = "$ " + "#,##0.00";
                    ws_1.Cell(i, 8).DataType = XLDataType.Number;
                    ws_1.Cell(i, 8).Style.NumberFormat.Format = "$ " + "#,##0.00";
                    ws_1.Cell(i, 9).DataType = XLDataType.Number;
                    ws_1.Cell(i, 9).Style.NumberFormat.Format = "#,##0.00";





                    i++;
                }


                ws_1.Cell(totalRows + 1, 9).Value = ws_1.Evaluate("SUM(I2:I" + totalRows + ")");
                ws_1.Cell(totalRows + 1, 3).Value = "Grand Total";
                ws_1.Cell(totalRows + 1, 3).Style.Font.Bold = true;
                ws_1.Cell(totalRows + 1, 9).Style.Font.Bold = true;
                ws_1.Cell(totalRows + 1, 9).Style.Font.FontColor = XLColor.Red;
                ws_1.Cell(totalRows + 1, 9).Style.Fill.BackgroundColor = XLColor.Yellow;
                ws_1.Cell(totalRows + 1, 9).DataType = XLDataType.Number;
                ws_1.Cell(totalRows + 1, 9).Style.NumberFormat.Format = "#,##0.00";



                ws_1.Range("A1:I1").Style.Fill.SetBackgroundColor(XLColor.Orange);
                ws_1.Range("A1:I1").Style.Font.Bold.ToString();
                ws_2.Range("A1:C1").Style.Fill.SetBackgroundColor(XLColor.Orange);
                ws_2.Range("A1:C1").Style.Font.Bold.ToString();

                ws_1.Range("A1:I" + (totalRows + 1) + "").Style.Border.InsideBorder = XLBorderStyleValues.Thin;
                ws_1.Range("A1:I" + (totalRows + 1) + "").Style.Border.InsideBorderColor = XLColor.Black;

                ws_1.Range("A" + (totalRows + 1) + ":I" + (totalRows + 1) + "").Style.Border.OutsideBorder = XLBorderStyleValues.Thin;
                ws_1.Range("A" + (totalRows + 1) + ":I" + (totalRows + 1) + "").Style.Border.OutsideBorderColor = XLColor.Black;


                ws_2.Range("A" + (totalRows1 + 1) + ":C" + (totalRows1 + 1) + "").Style.Border.OutsideBorder = XLBorderStyleValues.Thin;
                ws_2.Range("A" + (totalRows1 + 1) + ":C" + (totalRows1 + 1) + "").Style.Border.OutsideBorderColor = XLColor.Black;




                ws_2.Range("A1:C" + (totalRows1 + 1) + "").Style.Border.InsideBorder = XLBorderStyleValues.Thin;
                ws_2.Range("A1:C" + (totalRows1 + 1) + "").Style.Border.InsideBorderColor = XLColor.Black;
                string strMappath = "~/VendorReportExcel/" + User.Identity.Name + "/";
                if (!Directory.Exists(strMappath))
                {
                    Directory.CreateDirectory(System.IO.Path.Combine(Server.MapPath(strMappath)));
                }
                var file1 = Path.Combine(Server.MapPath(strMappath), BU + " Sales.xlsx");
                wb_1.SaveAs(file1);
                var file2 = Path.Combine(Server.MapPath(strMappath), BU + " Inventory.xlsx");
                wb_2.SaveAs(file2);
                var FilesName = "/VendorReportExcel/" + User.Identity.Name + "/" + BU + " Sales.xlsx$/VendorReportExcel/"+User.Identity.Name+"/" + BU + " Inventory.xlsx";
                return Json(new { data = FilesName }, JsonRequestBehavior.AllowGet);
            }
            else if (BU == "HIKVISION")
            {
                ds.Tables[0].TableName = "SALES";
                ds.Tables[1].TableName = "INVENTORY";

                // Add all DataTables in the DataSet as a worksheets
                wb_1.Worksheets.Add(dataSet);

                var ws_1 = wb_1.Worksheet(1);



                ws_1.AutoFilter.Clear();
                ws_1.Style.Fill.BackgroundColor = XLColor.White;

                var ws_2 = wb_1.Worksheet(2);
                ws_2.Style.Fill.BackgroundColor = XLColor.White;
                var totalRows = ws_1.RowsUsed().Count();
                var totalRows1 = ws_2.RowsUsed().Count();
                int i = 2;
                while (totalRows >= i)
                {

                    ws_1.Cell(i, 9).DataType = XLDataType.Number;
                    ws_1.Cell(i, 9).Style.NumberFormat.Format = "$ #,##0.00";





                    i++;
                }

                //A = 255, R = 0, G = 0, B = 0


                ws_1.Range("A1:I1").Style.Fill.SetBackgroundColor(XLColor.Yellow);
                ws_1.Range("A1:I1").Style.Font.Bold.ToString();
                ws_2.Range("A1:R1").Style.Fill.SetBackgroundColor(XLColor.Yellow);
                ws_2.Range("A1:R1").Style.Font.FontColor = XLColor.Bistre;


                ws_1.Range("A1:I1").Style.Font.FontColor = XLColor.Bistre;//FromArgb(255, 0, 0, 0);

                ws_2.Range("A1:R1").Style.Font.Bold.ToString();

                ws_1.Range("A1:I" + (totalRows + 1) + "").Style.Border.InsideBorder = XLBorderStyleValues.Thin;
                ws_1.Range("A1:I" + (totalRows + 1) + "").Style.Border.InsideBorderColor = XLColor.Black;


                ws_1.Range("A" + (totalRows + 1) + ":I" + (totalRows + 1) + "").Style.Border.OutsideBorder = XLBorderStyleValues.Thin;
                ws_1.Range("A" + (totalRows + 1) + ":I" + (totalRows + 1) + "").Style.Border.OutsideBorderColor = XLColor.Black;


                ws_2.Range("A" + (totalRows1 + 1) + ":R" + (totalRows1 + 1) + "").Style.Border.OutsideBorder = XLBorderStyleValues.Thin;
                ws_2.Range("A" + (totalRows1 + 1) + ":R" + (totalRows1 + 1) + "").Style.Border.OutsideBorderColor = XLColor.Black;



                ws_2.Range("A1:R" + (totalRows1 + 1) + "").Style.Border.InsideBorder = XLBorderStyleValues.Thin;
                ws_2.Range("A1:R" + (totalRows1 + 1) + "").Style.Border.InsideBorderColor = XLColor.Black;

                return wb_1.Deliver("" + BU + " REPORT.xlsx", "application/vnd.openxmlformats-officedocument.spreadsheetml.sheet");


            }
            else if (BU == "APC")
            {
                ds.Tables[0].TableName = "10487_POS_" + FromDate.ToString("MMyyyy") + "";
                ds.Tables[1].TableName = "10487_INV_" + FromDate.ToString("MMyyyy") + "";

                // Add all DataTables in the DataSet as a worksheets
                wb_1.Worksheets.Add(dataSet.Tables[0]);
                wb_2.Worksheets.Add(dataSet.Tables[1]);
                var ws_1 = wb_1.Worksheet(1);
                ws_1.Style.Fill.BackgroundColor = XLColor.White;
                ws_1.Style.Alignment.Horizontal = XLAlignmentHorizontalValues.Left;
                ws_1.Style.Alignment.WrapText = true;

                ws_1.AutoFilter.Clear();

                var ws_2 = wb_2.Worksheet(1);
                ws_2.Style.Fill.BackgroundColor = XLColor.White;
                ws_2.Style.Alignment.Horizontal = XLAlignmentHorizontalValues.Center;
                ws_2.AutoFilter.Clear();
                ws_2.SetAutoFilter(false);
                ws_1.AutoFilter.Clear();
                ws_1.SetAutoFilter(false);
                var totalRows = ws_1.RowsUsed().Count();
                var totalRows1 = ws_2.RowsUsed().Count();
                int i = 2;
                ws_1.Style.Font.FontName = "Helv";
                ws_1.Style.Font.FontSize = 10;
                ws_2.Style.Font.FontName = "Arial";
                ws_2.Style.Font.FontSize = 10;
                // var TotalSum = ds.Tables[0].Select().Sum(w => (int)w["QTY"]);
                while (totalRows1 >= i)
                {

                    //ws_2.Cell(i,7).Style.Alignment.
                    ws_2.Cell(i, 7).DataType = XLDataType.Number;
                    ws_2.Cell(i, 7).Style.NumberFormat.Format = "$ " + "#,##0.00";





                    i++;
                }


                //ws_1.Cell(totalRows + 1, 9).Value = ws_1.Evaluate("SUM(I2:I" + totalRows + ")");
                //ws_1.Cell(totalRows + 1, 3).Value = "Grand Total";
                //ws_1.Cell(totalRows + 1, 3).Style.Font.Bold = true;
                //ws_1.Cell(totalRows + 1, 9).Style.Font.Bold = true;
                //ws_1.Cell(totalRows + 1, 9).Style.Font.FontColor = XLColor.Red;
                //ws_1.Cell(totalRows + 1, 9).Style.Fill.BackgroundColor = XLColor.White;
                //ws_1.Cell(totalRows + 1, 9).DataType = XLDataType.Number;
                //ws_1.Cell(totalRows + 1, 9).Style.NumberFormat.Format = "#,##0.00";



                ws_1.Range("A1:AP1").Style.Fill.SetBackgroundColor(XLColor.White);
                //ws_1.Range("A1:AP1").Style.Font.Bold.ToString();
                ws_2.Range("A1:I1").Style.Fill.SetBackgroundColor(XLColor.White);
                // ws_2.Range("A1:I1").Style.Font.Bold.ToString();
                ws_1.Range("A1:AP1").Style.Font.FontColor = XLColor.Bistre;


                ws_2.Range("A1:I1").Style.Font.FontColor = XLColor.Bistre;

                ws_1.Range("A1:AP" + (totalRows + 1) + "").Style.Border.InsideBorder = XLBorderStyleValues.Thin;
                ws_1.Range("A1:AP" + (totalRows + 1) + "").Style.Border.InsideBorderColor = XLColor.Black;

                ws_1.Range("A" + (totalRows + 1) + ":AP" + (totalRows + 1) + "").Style.Border.OutsideBorder = XLBorderStyleValues.Thin;
                ws_1.Range("A" + (totalRows + 1) + ":AQ" + (totalRows + 1) + "").Style.Border.OutsideBorderColor = XLColor.Black;


                ws_2.Range("A" + (totalRows1 + 1) + ":I" + (totalRows1 + 1) + "").Style.Border.OutsideBorder = XLBorderStyleValues.Thin;
                ws_2.Range("A" + (totalRows1 + 1) + ":J" + (totalRows1 + 1) + "").Style.Border.OutsideBorderColor = XLColor.Black;




                ws_2.Range("A1:I" + (totalRows1 + 1) + "").Style.Border.InsideBorder = XLBorderStyleValues.Thin;
                ws_2.Range("A1:I" + (totalRows1 + 1) + "").Style.Border.InsideBorderColor = XLColor.Black;
                string strMappath = "~/VendorReportExcel/" + User.Identity.Name + "/";
                if (!Directory.Exists(strMappath))
                {
                    Directory.CreateDirectory(System.IO.Path.Combine(Server.MapPath(strMappath)));
                }
                var file1 = Path.Combine(Server.MapPath(strMappath), BU + " - 10487_POS_" + FromDate.ToString("MMyyyy") + ".xlsx");
                wb_1.SaveAs(file1);
                var file2 = Path.Combine(Server.MapPath(strMappath), BU + " - 10487_INV_" + FromDate.ToString("MMyyyy") + ".xlsx");
                wb_2.SaveAs(file2);
                var FilesName = "/VendorReportExcel/" + User.Identity.Name + "/" + BU + " - 10487_POS_" + FromDate.ToString("MMyyyy") + ".xlsx$/VendorReportExcel/" + User.Identity.Name + "/"  + BU + " - 10487_INV_" + FromDate.ToString("MMyyyy") + ".xlsx";
                return Json(new { data = FilesName }, JsonRequestBehavior.AllowGet);


            }
            else if (BU == "EPSON")
            {
                ds.Tables[0].TableName = BU+" - SAL" +ds.Tables[0].Rows[0][0]+ FromDate.ToString("ddMMyy") +FromDate.ToString("MMM").Substring(0,1)+ "";
                ds.Tables[1].TableName = BU+" - CUS"  + ds.Tables[1].Rows[0][0] + FromDate.ToString("ddMMyy") +FromDate.ToString("MMM").Substring(0, 1) + "";
                ds.Tables[2].TableName = BU + " - STO" + ds.Tables[2].Rows[0][0] + FromDate.ToString("ddMMyy") + FromDate.ToString("MMM").Substring(0, 1) + "";

                // Add all DataTables in the DataSet as a worksheets
                wb_1.Worksheets.Add(dataSet.Tables[0]);
                wb_2.Worksheets.Add(dataSet.Tables[1]);
                wb_3.Worksheets.Add(dataSet.Tables[2]);
                var ws_1 = wb_1.Worksheet(1);
                ws_1.Style.Fill.BackgroundColor = XLColor.White;
                ws_1.Style.Alignment.Horizontal = XLAlignmentHorizontalValues.Left;
                ws_1.Style.Alignment.WrapText = true;
                ws_1.Row(1).Hide();
              // ws_1.Row(1).InsertRowsAbove(1);

                ws_1.AutoFilter.Clear();

                var ws_2 = wb_2.Worksheet(1);
                ws_2.Style.Fill.BackgroundColor = XLColor.White;
                ws_2.Style.Alignment.Horizontal = XLAlignmentHorizontalValues.Left;
                ws_2.AutoFilter.Clear();
                ws_2.SetAutoFilter(false);
                ws_2.Row(1).Hide();
                ///ws_2.Row(1).Delete();




                var ws_3 = wb_3.Worksheet(1);
                ws_3.Style.Fill.BackgroundColor = XLColor.White;
                ws_3.Style.Alignment.Horizontal = XLAlignmentHorizontalValues.Left;
                ws_3.AutoFilter.Clear();
                ws_3.SetAutoFilter(false);
                ws_3.Row(1).Hide();
                //ws_3.Row(1).Delete();

                ws_1.AutoFilter.Clear();
                ws_1.SetAutoFilter(false);
                var totalRows = ws_1.RowsUsed().Count();
                var totalRows1 = ws_2.RowsUsed().Count();

                var totalRows2 = ws_3.RowsUsed().Count();
                int i = 2;
                ws_1.Style.Font.FontName = "Calibri";
                ws_1.Style.Font.FontSize = 11;
                ws_2.Style.Font.FontName = "Arial";
                ws_2.Style.Font.FontSize = 8;
                ws_3.Style.Font.FontName = "Arial";
                ws_3.Style.Font.FontSize = 8;
                // var TotalSum = ds.Tables[0].Select().Sum(w => (int)w["QTY"]);
                //while (totalRows1 >= i)
                //{
                //    ws_2.Cell(i, 3).Value=FromDate.ToString("MMM").Substring(0, 1);
                //    i++;
                //}
                //i = 2;
                //while (totalRows2 >= i)
                //{
                //    ws_3.Cell(i, 3).Value = FromDate.ToString("MMM").Substring(0, 1);
                //    i++;
                //}


                //ws_1.Cell(totalRows + 1, 9).Value = ws_1.Evaluate("SUM(I2:I" + totalRows + ")");
                //ws_1.Cell(totalRows + 1, 3).Value = "Grand Total";
                //ws_1.Cell(totalRows + 1, 3).Style.Font.Bold = true;
                //ws_1.Cell(totalRows + 1, 9).Style.Font.Bold = true;
                //ws_1.Cell(totalRows + 1, 9).Style.Font.FontColor = XLColor.Red;
                //ws_1.Cell(totalRows + 1, 9).Style.Fill.BackgroundColor = XLColor.White;
                //ws_1.Cell(totalRows + 1, 9).DataType = XLDataType.Number;
                //ws_1.Cell(totalRows + 1, 9).Style.NumberFormat.Format = "#,##0.00";



                //ws_1.Range("A1:AP1").Style.Fill.SetBackgroundColor(XLColor.White);
                ////ws_1.Range("A1:AP1").Style.Font.Bold.ToString();
                //ws_2.Range("A1:I1").Style.Fill.SetBackgroundColor(XLColor.White);
                //// ws_2.Range("A1:I1").Style.Font.Bold.ToString();
                //ws_1.Range("A1:AP1").Style.Font.FontColor = XLColor.Bistre;


                //ws_2.Range("A1:I1").Style.Font.FontColor = XLColor.Bistre;

                //ws_1.Range("A1:AP" + (totalRows + 1) + "").Style.Border.InsideBorder = XLBorderStyleValues.Thin;
                //ws_1.Range("A1:AP" + (totalRows + 1) + "").Style.Border.InsideBorderColor = XLColor.Black;

                //ws_1.Range("A" + (totalRows + 1) + ":AP" + (totalRows + 1) + "").Style.Border.OutsideBorder = XLBorderStyleValues.Thin;
                //ws_1.Range("A" + (totalRows + 1) + ":AQ" + (totalRows + 1) + "").Style.Border.OutsideBorderColor = XLColor.Black;


                //ws_2.Range("A" + (totalRows1 + 1) + ":I" + (totalRows1 + 1) + "").Style.Border.OutsideBorder = XLBorderStyleValues.Thin;
                //ws_2.Range("A" + (totalRows1 + 1) + ":J" + (totalRows1 + 1) + "").Style.Border.OutsideBorderColor = XLColor.Black;




                //ws_2.Range("A1:I" + (totalRows1 + 1) + "").Style.Border.InsideBorder = XLBorderStyleValues.Thin;
                //ws_2.Range("A1:I" + (totalRows1 + 1) + "").Style.Border.InsideBorderColor = XLColor.Black;
                string strMappath = "~/VendorReportExcel/" + User.Identity.Name + "/";
                if (!Directory.Exists(strMappath))
                {
                    Directory.CreateDirectory(System.IO.Path.Combine(Server.MapPath(strMappath)));
                }
                var file1 = Path.Combine(Server.MapPath(strMappath),  ds.Tables[0].TableName + FromDate.ToString("ddMMyy") + FromDate.ToString("MMM").Substring(0,1) + ".xlsx");
               // wb_1.SaveAs(file1, FileFormat:= xlText);
                wb_1.SaveAs(file1);
                var file2 = Path.Combine(Server.MapPath(strMappath),  ds.Tables[1].TableName + FromDate.ToString("ddMMyy") + FromDate.ToString("MMM").Substring(0, 1) + ".xlsx");
                wb_2.SaveAs(file2);
                var file3 = Path.Combine(Server.MapPath(strMappath), ds.Tables[2].TableName + FromDate.ToString("ddMMyy") + FromDate.ToString("MMM").Substring(0, 1) + ".xlsx");
                wb_3.SaveAs(file3);
                var FilesName = "/VendorReportExcel/" + User.Identity.Name + "/" +  ds.Tables[0].TableName + FromDate.ToString("ddMMyy") + FromDate.ToString("MMM").Substring(0, 1) + ".xlsx$/VendorReportExcel/" + User.Identity.Name + "/"  + ds.Tables[1].TableName + FromDate.ToString("ddMMyy") + FromDate.ToString("MMM").Substring(0, 1) + ".xlsx$/VendorReportExcel/" + User.Identity.Name + "/" + ds.Tables[2].TableName + FromDate.ToString("ddMMyy") + FromDate.ToString("MMM").Substring(0, 1) + ".xlsx";
                return Json(new { data = FilesName }, JsonRequestBehavior.AllowGet);


            }


            return View();

            
        }

     



    }
}