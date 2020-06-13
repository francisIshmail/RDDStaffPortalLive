using Microsoft.Office.Interop.Excel;
using RDDStaffPortal.DAL.DataModels;
using RDDStaffPortal.DAL.InitialSetup;
using RDDStaffPortal.DAL.Report;
using System;
using System.Collections.Generic;
using System.Data;
using System.IO;
using System.Linq;
using System.Runtime.InteropServices;
using System.Runtime.Remoting.Messaging;
using System.Text;
using System.Web;
using System.Web.Mvc;
using System.Web.UI;
using System.Web.UI.WebControls;
using DataTable = System.Data.DataTable;
using ClosedXML.Excel;

namespace RDDStaffPortal.Areas.Reports.Controllers
{
    [Authorize]
    public class ReportsController : Controller
    {
        // GET: Reports/Reports


        RDD_Stock_SheetDBOperation _ReptOp = new RDD_Stock_SheetDBOperation();
        public ActionResult Index()
       {
            RDD_Stock_Sheet _RDD_Stock = new RDD_Stock_Sheet();
            
            List<Rdd_comonDrop> WhseOwnerList = new List<Rdd_comonDrop>(); 
            List<Rdd_comonDrop> countryList = new List<Rdd_comonDrop>();
            List<Rdd_comonDrop> WhseStatusList = new List<Rdd_comonDrop>();
            List<Rdd_comonDrop> warehousenameList = new List<Rdd_comonDrop>();
            List<Rdd_comonDrop> BUGroupList = new List<Rdd_comonDrop>();
            List<Rdd_comonDrop> BUList = new List<Rdd_comonDrop>();
            DataSet dsModules = _ReptOp.GetDrop();
            if (dsModules.Tables.Count > 0)
            {
                DataTable dtModule;
                DataRowCollection drc;
                dtModule = dsModules.Tables[0];
                 drc = dtModule.Rows;
                foreach (DataRow dr in drc)
                {
                    WhseOwnerList.Add(new Rdd_comonDrop()
                    {
                        Code = !string.IsNullOrWhiteSpace(dr["WhseOwner"].ToString()) ? dr["WhseOwner"].ToString() : "",
                    });
                }
                dtModule = dsModules.Tables[1];
                 drc = dtModule.Rows;
                foreach (DataRow dr in drc)
                {
                    countryList.Add(new Rdd_comonDrop()
                    {
                        Code = !string.IsNullOrWhiteSpace(dr["country"].ToString()) ? dr["country"].ToString() : "",
                    });
                }
                dtModule = dsModules.Tables[2];
                 drc = dtModule.Rows;
                foreach (DataRow dr in drc)
                {
                    WhseStatusList.Add(new Rdd_comonDrop()
                    {
                        Code = !string.IsNullOrWhiteSpace(dr["WhseStatus"].ToString()) ? dr["WhseStatus"].ToString() : "",
                    });
                }
                dtModule = dsModules.Tables[3];
                drc = dtModule.Rows;
                foreach (DataRow dr in drc)
                {
                    warehousenameList.Add(new Rdd_comonDrop()
                    {
                        Code = !string.IsNullOrWhiteSpace(dr["warehousename"].ToString()) ? dr["warehousename"].ToString() : "",
                    });
                }
                dtModule = dsModules.Tables[4];
                drc = dtModule.Rows;
                foreach (DataRow dr in drc)
                {
                    BUGroupList.Add(new Rdd_comonDrop()
                    {
                        Code = !string.IsNullOrWhiteSpace(dr["BUGroup"].ToString()) ? dr["BUGroup"].ToString() : "",
                    });
                }
                dtModule = dsModules.Tables[5];
                drc = dtModule.Rows;
                foreach (DataRow dr in drc)
                {
                    BUList.Add(new Rdd_comonDrop()
                    {
                        Code = !string.IsNullOrWhiteSpace(dr["BU"].ToString()) ? dr["BU"].ToString() : "",
                    });
                }
            }
            ViewBag.WhseOwnerList = new SelectList(WhseOwnerList, "Code", "Code");
            ViewBag.countryList = new SelectList(countryList, "Code", "Code");
            ViewBag.WhseStatusList = new SelectList(WhseStatusList, "Code", "Code");
            ViewBag.warehousenameList = new SelectList(warehousenameList, "Code", "Code");
            ViewBag.BUGroupList = new SelectList(BUGroupList, "Code", "Code");
            ViewBag.BUList = new SelectList(BUList, "Code", "Code");

            return View(_RDD_Stock);
        }

        public ActionResult Inventory()
        {
            RDD_Stock_Sheet _RDD_Stock = new RDD_Stock_Sheet();

            List<Rdd_comonDrop> WhseOwnerList = new List<Rdd_comonDrop>();
            List<Rdd_comonDrop> countryList = new List<Rdd_comonDrop>();
            List<Rdd_comonDrop> WhseStatusList = new List<Rdd_comonDrop>();
            List<Rdd_comonDrop> warehousenameList = new List<Rdd_comonDrop>();
            List<Rdd_comonDrop> BUGroupList = new List<Rdd_comonDrop>();
            List<Rdd_comonDrop> BUList = new List<Rdd_comonDrop>();
            DataSet dsModules = _ReptOp.GetDrop();
            if (dsModules.Tables.Count > 0)
            {
                DataTable dtModule;
                DataRowCollection drc;
                dtModule = dsModules.Tables[0];
                drc = dtModule.Rows;
                foreach (DataRow dr in drc)
                {
                    WhseOwnerList.Add(new Rdd_comonDrop()
                    {
                        Code = !string.IsNullOrWhiteSpace(dr["WhseOwner"].ToString()) ? dr["WhseOwner"].ToString() : "",
                    });
                }
                dtModule = dsModules.Tables[1];
                drc = dtModule.Rows;
                foreach (DataRow dr in drc)
                {
                    countryList.Add(new Rdd_comonDrop()
                    {
                        Code = !string.IsNullOrWhiteSpace(dr["country"].ToString()) ? dr["country"].ToString() : "",
                    });
                }
                dtModule = dsModules.Tables[2];
                drc = dtModule.Rows;
                foreach (DataRow dr in drc)
                {
                    WhseStatusList.Add(new Rdd_comonDrop()
                    {
                        Code = !string.IsNullOrWhiteSpace(dr["WhseStatus"].ToString()) ? dr["WhseStatus"].ToString() : "",
                    });
                }
                dtModule = dsModules.Tables[3];
                drc = dtModule.Rows;
                foreach (DataRow dr in drc)
                {
                    warehousenameList.Add(new Rdd_comonDrop()
                    {
                        Code = !string.IsNullOrWhiteSpace(dr["warehousename"].ToString()) ? dr["warehousename"].ToString() : "",
                    });
                }
                dtModule = dsModules.Tables[4];
                drc = dtModule.Rows;
                foreach (DataRow dr in drc)
                {
                    BUGroupList.Add(new Rdd_comonDrop()
                    {
                        Code = !string.IsNullOrWhiteSpace(dr["BUGroup"].ToString()) ? dr["BUGroup"].ToString() : "",
                    });
                }
                dtModule = dsModules.Tables[5];
                drc = dtModule.Rows;
                foreach (DataRow dr in drc)
                {
                    BUList.Add(new Rdd_comonDrop()
                    {
                        Code = !string.IsNullOrWhiteSpace(dr["BU"].ToString()) ? dr["BU"].ToString() : "",
                    });
                }
            }
            ViewBag.WhseOwnerList = new SelectList(WhseOwnerList, "Code", "Code");
            ViewBag.countryList = new SelectList(countryList, "Code", "Code");
            ViewBag.WhseStatusList = new SelectList(WhseStatusList, "Code", "Code");
            ViewBag.warehousenameList = new SelectList(warehousenameList, "Code", "Code");
            ViewBag.BUGroupList = new SelectList(BUGroupList, "Code", "Code");
            ViewBag.BUList = new SelectList(BUList, "Code", "Code");

            return View(_RDD_Stock);
        }

        public ActionResult BackLogSheet()
        {
            RDD_BackLog_Sheet _RDD_Back = new RDD_BackLog_Sheet();

            List<Rdd_comonDrop> mapped_BUList = new List<Rdd_comonDrop>();
           
            List<Rdd_comonDrop> BUGroupList = new List<Rdd_comonDrop>();
            List<Rdd_comonDrop> mapped_ProjectList = new List<Rdd_comonDrop>();
            DataSet dsModules = _ReptOp.GetDrop1();
            if (dsModules.Tables.Count > 0)
            {
                DataTable dtModule;
                DataRowCollection drc;
                dtModule = dsModules.Tables[0];
                drc = dtModule.Rows;
                foreach (DataRow dr in drc)
                {
                    mapped_BUList.Add(new Rdd_comonDrop()
                    {
                        Code = !string.IsNullOrWhiteSpace(dr["mapped_BU"].ToString()) ? dr["mapped_BU"].ToString() : "",
                    });
                }
                
                dtModule = dsModules.Tables[1];
                drc = dtModule.Rows;
                foreach (DataRow dr in drc)
                {
                    BUGroupList.Add(new Rdd_comonDrop()
                    {
                        Code = !string.IsNullOrWhiteSpace(dr["BUGroup"].ToString()) ? dr["BUGroup"].ToString() : "",
                    });
                }
                dtModule = dsModules.Tables[2];
                drc = dtModule.Rows;
                foreach (DataRow dr in drc)
                {
                    mapped_ProjectList.Add(new Rdd_comonDrop()
                    {
                        Code = !string.IsNullOrWhiteSpace(dr["mapped_Project"].ToString()) ? dr["mapped_Project"].ToString() : "",
                    });
                }
            }
                     
            ViewBag.mapped_ProjectList = new SelectList(mapped_ProjectList, "Code", "Code");
            ViewBag.BUGroupList = new SelectList(BUGroupList, "Code", "Code");
            ViewBag.mapped_BUList = new SelectList(mapped_BUList, "Code", "Code");

            return View(_RDD_Back);
        }



        public ActionResult BackLogSheetReport(int pagesize, int pageno, string psearch, string MappBU, string BUGroup, string MappProj)
        {
            return Json(new { data = _ReptOp.GetRDDBackLogList(User.Identity.Name, pagesize, pageno, psearch,MappBU,BUGroup,MappProj) }, JsonRequestBehavior.AllowGet);
        }

        public ActionResult StockSheetReport(int pagesize, int pageno, string psearch, string Country, string BUGroup, string BU, string Whsename, string WhseOwn, string WhseStatus)
        {
            return Json(new { data = _ReptOp.GetRDDCustMergList(User.Identity.Name, pagesize, pageno, psearch,Country,BUGroup,BU,Whsename,WhseOwn,WhseStatus) }, JsonRequestBehavior.AllowGet);
        }

        public ActionResult DownloadToExcel3()
        {
            DataTable dt = _ReptOp.Getdata3(User.Identity.Name);
            using (XLWorkbook wb = new XLWorkbook())
            {
                wb.Worksheets.Add(dt);
                using (MemoryStream stream = new MemoryStream())
                {
                    wb.SaveAs(stream);
                    return File(stream.ToArray(), "application/vnd.openxmlformats-officedocument.spreadsheetml.sheet", "BackLogSheet.xlsx");
                }
            }
            //var grdReport = new System.Web.UI.WebControls.GridView();
            //DataTable dt = _ReptOp.Getdata3(User.Identity.Name);
            //dt.Columns.Remove("RowNum");
            //dt.Columns.Remove("TotalCount");

            //grdReport.DataSource = dt;
            //grdReport.DataBind();
            //System.IO.StringWriter sw = new System.IO.StringWriter();
            //System.Web.UI.HtmlTextWriter htw = new System.Web.UI.HtmlTextWriter(sw);
            //grdReport.RenderControl(htw);
            //byte[] bindata = System.Text.Encoding.ASCII.GetBytes(sw.ToString());
            //return File(bindata, "application/ms-excel", "ReportFile.xls");
        }

        public ActionResult DownloadToExcel2()
        {
            DataTable dt = _ReptOp.Getdata1(User.Identity.Name);
            using (XLWorkbook wb = new XLWorkbook())
            {
                wb.Worksheets.Add(dt);
                using (MemoryStream stream = new MemoryStream())
                {
                    wb.SaveAs(stream);
                    return File(stream.ToArray(), "application/vnd.openxmlformats-officedocument.spreadsheetml.sheet", "InventorySheet.xlsx");
                }
            }
            //var grdReport = new System.Web.UI.WebControls.GridView();
            //grdReport.DataSource = _ReptOp.Getdata1(User.Identity.Name);
            //grdReport.DataBind();
            //System.IO.StringWriter sw = new System.IO.StringWriter();
            //System.Web.UI.HtmlTextWriter htw = new System.Web.UI.HtmlTextWriter(sw);
            //grdReport.RenderControl(htw);
            //byte[] bindata = System.Text.Encoding.ASCII.GetBytes(sw.ToString());
            //return File(bindata, "application/ms-excel", "ReportFile.xls");
        }
        public FileResult DownloadToExcel1()
        {
            DataTable dt = _ReptOp.Getdata(User.Identity.Name);
            using (XLWorkbook wb = new XLWorkbook())
            {
                wb.Worksheets.Add(dt);
                using (MemoryStream stream = new MemoryStream())
                {
                    wb.SaveAs(stream);
                    return File(stream.ToArray(), "application/vnd.openxmlformats-officedocument.spreadsheetml.sheet", "Stocksheet.xlsx");
                }
            }
            //var grdReport = new System.Web.UI.WebControls.GridView();
            //grdReport.DataSource = _ReptOp.Getdata(User.Identity.Name);
            //grdReport.DataBind();
            //System.IO.StringWriter sw = new System.IO.StringWriter();
            //System.Web.UI.HtmlTextWriter htw = new System.Web.UI.HtmlTextWriter(sw);
            //grdReport.RenderControl(htw);
            //byte[] bindata = System.Text.Encoding.ASCII.GetBytes(sw.ToString());
            //return File(bindata, "application/ms-excel", "ReportFile.xls");
        }
       // [HttpPost]

        [Route("DownloadExcelTes")]
        public FileResult DownloadExcelTes()
        {
           
            DataTable dt= _ReptOp.Getdata(User.Identity.Name);
            using (XLWorkbook wb = new XLWorkbook())
            {
                wb.Worksheets.Add(dt);
                using (MemoryStream stream = new MemoryStream())
                {
                    wb.SaveAs(stream);
                    return File(stream.ToArray(), "application/vnd.openxmlformats-officedocument.spreadsheetml.sheet", "Grid.xlsx");
                }
            }
            //var gv = new GridView();
            //gv.DataSource = _ReptOp.Getdata(User.Identity.Name);
            //gv.DataBind();
            //Response.ClearContent();
            //Response.Buffer = true;
            //Response.AddHeader("content-disposition", "attachment; filename=DemoExcel.xls");
            //Response.ContentType = "application/ms-excel";
            //Response.Charset = "";
            //StringWriter objStringWriter = new StringWriter();
            //HtmlTextWriter objHtmlTextWriter = new HtmlTextWriter(objStringWriter);
            //gv.RenderControl(objHtmlTextWriter);
            //Response.Output.Write(objStringWriter.ToString());
            //Response.Flush();
            //Response.End();
           // return View("Index");
        }
        [HttpPost]

        [Route("DownloadExcel")]
        public ActionResult DownloadExcel()
        {
            DataTable ds = _ReptOp.Getdata(User.Identity.Name);

            string ExcelFilePath = "C:\\Users\\dev3\\Documents";
            int ColumnsCount;

            if (ds == null || (ColumnsCount = ds.Columns.Count) == 0)
                throw new Exception("ExportToExcel: Null or empty input table!\n");

            // load excel, and create a new workbook
            Microsoft.Office.Interop.Excel.Application Excel = new Microsoft.Office.Interop.Excel.Application();
            Excel.Workbooks.Add();

            // single worksheet
            Microsoft.Office.Interop.Excel._Worksheet Worksheet = Excel.ActiveSheet;

            object[] Header = new object[ColumnsCount];

            // column headings               
            for (int i = 0; i < ColumnsCount; i++)
                Header[i] = ds.Columns[i].ColumnName;

            Microsoft.Office.Interop.Excel.Range HeaderRange = Worksheet.get_Range((Microsoft.Office.Interop.Excel.Range)(Worksheet.Cells[1, 1]), (Microsoft.Office.Interop.Excel.Range)(Worksheet.Cells[1, ColumnsCount]));
            HeaderRange.Value = Header;
            HeaderRange.Interior.Color = System.Drawing.ColorTranslator.ToOle(System.Drawing.Color.LightGray);
            HeaderRange.Font.Bold = true;

            // DataCells
            int RowsCount = ds.Rows.Count;
            object[,] Cells = new object[RowsCount, ColumnsCount];

            for (int j = 0; j < RowsCount; j++)
                for (int i = 0; i < ColumnsCount; i++)
                    Cells[j, i] = ds.Rows[j][i];

            Worksheet.get_Range((Microsoft.Office.Interop.Excel.Range)(Worksheet.Cells[2, 1]), (Microsoft.Office.Interop.Excel.Range)(Worksheet.Cells[RowsCount + 1, ColumnsCount])).Value = Cells;

            // check fielpath
            if (ExcelFilePath != null && ExcelFilePath != "")
            {
                try
                {
                    Worksheet.SaveAs(ExcelFilePath);
                    Excel.Quit();

                }
                catch (Exception ex)
                {
                    throw new Exception("ExportToExcel: Excel file could not be saved! Check filepath.\n"
                        + ex.Message);
                }
            }
            else    // no filepath is given
            {
                Excel.Visible = true;
            }
          
            return View(); 
       
          
        }
       

    }
}