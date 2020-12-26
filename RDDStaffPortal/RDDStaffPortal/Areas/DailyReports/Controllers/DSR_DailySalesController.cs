using ClosedXML.Excel;
using DocumentFormat.OpenXml.Drawing.Charts;
using Newtonsoft.Json;
using RDDStaffPortal.DAL;
using RDDStaffPortal.DAL.DailyReports;
using RDDStaffPortal.DAL.DataModels.DailyReports;
using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data;
using System.Data.OleDb;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Net;
using System.Net.Mail;
using System.Reflection;
using System.Web;
using System.Web.Mvc;
using System.Web.Routing;
using System.Web.UI;
using System.Web.UI.WebControls;
using DataTable = System.Data.DataTable;

namespace RDDStaffPortal.Areas.DailyReports.Controllers
{
    [Authorize]
    public class DSR_DailySalesController : Controller
    {
        // GET: DailyReports/DSR

        RDD_DailySalesReports_Db_Operation rDD_Daily = new RDD_DailySalesReports_Db_Operation();
        CommonFunction com = new CommonFunction();
        public ActionResult Index(bool? Status, string erromsg = null)
        {
            //HtmlHelper.ClientValidationEnabled = false;
            RDD_DailySalesReports rDD = new RDD_DailySalesReports();
            if (Status != null)
            {
                rDD.ErrorMsg = erromsg;//(Status == true ? "Import successfully" : "Error occur");
                ModelState.AddModelError("ErrorMsg", rDD.ErrorMsg);

            }

            DataSet ds = null;
            List<SelectListItem> CountryList = new List<SelectListItem>();
            List<SelectListItem> ModeOfCallList = new List<SelectListItem>();
            List<SelectListItem> CallStatusList = new List<SelectListItem>();
            List<SelectListItem> NextActionList = new List<SelectListItem>();
            try
            {
                ds = rDD_Daily.GetDrop(User.Identity.Name);
                CountryList.Add(new SelectListItem()
                {
                    Text = "Select",
                    Value = "0"
                });
                ModeOfCallList.Add(new SelectListItem()
                {
                    Text = "Select",
                    Value = "0"
                });
                CallStatusList.Add(new SelectListItem()
                {
                    Text = "Select",
                    Value = "0"
                });
                NextActionList.Add(new SelectListItem()
                {
                    Text = "Select",
                    Value = "0"
                });
                if (ds.Tables.Count > 0)
                {
                    DataTable dtModule;
                    DataRowCollection drc;
                    try
                    {
                        dtModule = ds.Tables[0];
                        drc = dtModule.Rows;
                        foreach (DataRow dr in drc)
                        {
                            CountryList.Add(new SelectListItem()
                            {
                                Text = dr["country"].ToString(),
                                Value = dr["country"].ToString()
                            }); ;
                        }
                    }
                    catch (Exception)
                    {
                        CountryList.Add(new SelectListItem()
                        {
                            Text = "Error",
                            Value = "-1"
                        });
                    }
                    try
                    {
                        dtModule = ds.Tables[1];
                        drc = dtModule.Rows;
                        foreach (DataRow dr in drc)
                        {
                            CallStatusList.Add(new SelectListItem()
                            {
                                Text = dr["CallStatus"].ToString(),
                                Value = dr["CallStatus"].ToString()
                            }); ;
                        }
                    }
                    catch (Exception)
                    {
                        CallStatusList.Add(new SelectListItem()
                        {
                            Text = "Error",
                            Value = "-1"
                        });
                    }
                    try
                    {
                        dtModule = ds.Tables[2];
                        drc = dtModule.Rows;
                        foreach (DataRow dr in drc)
                        {
                            ModeOfCallList.Add(new SelectListItem()
                            {
                                Text = dr["Modeofcall"].ToString(),
                                Value = dr["Modeofcall"].ToString()
                            }); ;
                        }
                    }
                    catch (Exception)
                    {

                        ModeOfCallList.Add(new SelectListItem()
                        {
                            Text = "Error",
                            Value = "-1"
                        });
                    }
                    try
                    {
                        dtModule = ds.Tables[3];
                        drc = dtModule.Rows;
                        foreach (DataRow dr in drc)
                        {
                            NextActionList.Add(new SelectListItem()
                            {
                                Text = dr["NextAction"].ToString(),
                                Value = dr["NextAction"].ToString()
                            }); ;
                        }
                    }
                    catch (Exception)
                    {
                        NextActionList.Add(new SelectListItem()
                        {
                            Text = "Error",
                            Value = "-1"
                        });
                    }
                }


                ds = rDD_Daily.GetDatas(System.DateTime.Now, User.Identity.Name);

                if (ds.Tables.Count > 0)
                {
                    DataTable dtModule;
                    DataRowCollection drc;
                    try
                    {

                        dtModule = ds.Tables[0];
                        drc = dtModule.Rows;
                        foreach (DataRow dr in drc)
                        {
                            rDD.ActionType = dr["freqOfRpt"].ToString();
                        }

                    }
                    catch (Exception)
                    {

                        rDD.ActionType = "";
                    }

                    try
                    {

                        dtModule = ds.Tables[1];
                        drc = dtModule.Rows;
                        foreach (DataRow dr in drc)
                        {

                            rDD.FromDate = Convert.ToDateTime(dr["FromDate"].ToString());

                            rDD.ToDate = Convert.ToDateTime(dr["ToDate"].ToString());
                        }

                    }
                    catch (Exception)
                    {

                        rDD.FromDate = System.DateTime.Now;
                        rDD.ToDate = System.DateTime.Now;
                    }


                }


            }
            catch (Exception)
            {
                CountryList.Add(new SelectListItem()
                {
                    Text = "Error",
                    Value = "-1"
                });
                ModeOfCallList.Add(new SelectListItem()
                {
                    Text = "Error",
                    Value = "-1"
                });
                CallStatusList.Add(new SelectListItem()
                {
                    Text = "Error",
                    Value = "-1"
                });
                NextActionList.Add(new SelectListItem()
                {
                    Text = "Error",
                    Value = "-1"
                });
                rDD.ActionType = "";
                rDD.FromDate = System.DateTime.Now;
                rDD.ToDate = System.DateTime.Now;
            }
            rDD.CountryList = CountryList;
            rDD.ModeOfCallList = ModeOfCallList;
            rDD.CallStatusList = CallStatusList;
            rDD.NextActionList = NextActionList;
            return View(rDD);
        }
        [Route("GetCompanyList")]
        public ActionResult GetCompnay(string CountryCode)
        {
            DataSet DS = rDD_Daily.FillCompnayName(User.Identity.Name, CountryCode);
            ContentResult ret = null;
            try
            {
                if (DS.Tables.Count > 0)
                {
                    ret = Content(JsonConvert.SerializeObject(DS), "application/json");
                }
            }
            catch (Exception)
            {
                ret = null;
            }
            return ret;

        }
        [Route("GetRDDPersonaDetails")]
        public ActionResult GetRDDPersonaDetails(string Compnay)
        {
            DataSet DS = rDD_Daily.GetRDD_PersonDet(Compnay);
            ContentResult ret = null;
            try
            {
                if (DS.Tables.Count > 0)
                {
                    ret = Content(JsonConvert.SerializeObject(DS), "application/json");
                }
            }
            catch (Exception)
            {
                ret = null;
            }
            return ret;

        }


        [Route("GetWeeklyReports")]
        public ActionResult GetWeeklyReports()
        {
            DataSet DS = rDD_Daily.GetWeeklyReports(User.Identity.Name);
            ContentResult ret = null;
            try
            {
                if (DS.Tables.Count > 0)
                {
                    ret = Content(JsonConvert.SerializeObject(DS), "application/json");
                }
            }
            catch (Exception)
            {
                ret = null;
            }
            return ret;

        }
        [Route("DailSalesReportCommentSave")]
        public ActionResult DailSalesReportCommentSave(List<RDD_DailySalesReportComment> rDD_DailySalesDet)
        {
            int i = 0;
            while (rDD_DailySalesDet.Count > i)
            {
                rDD_DailySalesDet[i].ReportReadBy = User.Identity.Name;
                rDD_DailySalesDet[i].ReportReadOn = DateTime.Now;
                rDD_DailySalesDet[i].PM_ReportReadBy = User.Identity.Name;
                rDD_DailySalesDet[i].PM_ReportReadOn = DateTime.Now;
                i++;
            }
            return Json(new { data = rDD_Daily.saveComment(rDD_DailySalesDet) }, JsonRequestBehavior.AllowGet);
        }

        [Route("FinalDailSalesReportSave")]
        public ActionResult FinalDailSalesReportSave(List<RDD_DailySalesReports> rDD_DailySalesDet)
        {
            int i = 0;
            while (rDD_DailySalesDet.Count > i)
            {
                rDD_DailySalesDet[i].LastUpdatedBy = User.Identity.Name;

                rDD_DailySalesDet[i].LastUpdatedOn = DateTime.Now;

                i++;
            }
            DataTable dt=ToDataTable(rDD_DailySalesDet);

            MemoryStream ms = DataToExcel(dt);
            return Json(new { data = rDD_Daily.FinalSave(rDD_DailySalesDet,ms) }, JsonRequestBehavior.AllowGet);
        }
        public static DataTable ToDataTable<T>(List<T> items)
        {
            DataTable dataTable = new DataTable(typeof(T).Name);

            //Get all the properties
            PropertyInfo[] Props = typeof(T).GetProperties(BindingFlags.Public | BindingFlags.Instance);
            foreach (PropertyInfo prop in Props)
            {
                //Defining type of data column gives proper data table 
                var type = (prop.PropertyType.IsGenericType && prop.PropertyType.GetGenericTypeDefinition() == typeof(Nullable<>) ? Nullable.GetUnderlyingType(prop.PropertyType) : prop.PropertyType);
                //Setting column names as Property names
                dataTable.Columns.Add(prop.Name, type);
            }
            foreach (T item in items)
            {
                var values = new object[Props.Length];
                for (int i = 0; i < Props.Length; i++)
                {
                    //inserting property values to datatable rows
                    values[i] = Props[i].GetValue(item, null);
                }
                dataTable.Rows.Add(values);
            }
            //put a breakpoint here and check datatable
            return dataTable;
        }
        [Route("DailSalesReportSave")]
        public ActionResult DailSalesReportSave(RDD_DailySalesReports rDD_DailySales)
        {
            if (!rDD_DailySales.EditFlag)
            {
                rDD_DailySales.CreatedBy = User.Identity.Name;
                rDD_DailySales.CreatedOn = DateTime.Now;
            }
            else
            {
                rDD_DailySales.LastUpdatedBy = User.Identity.Name;
                rDD_DailySales.LastUpdatedOn = DateTime.Now;
            }
            return Json(new { data = rDD_Daily.Save(rDD_DailySales) }, JsonRequestBehavior.AllowGet);
        }
        [Route("DailSalesReportDraftSave")]
        public ActionResult DailSalesReportDraftSave(RDD_DailySalesReports rDD_DailySales)
        {
            if (!rDD_DailySales.EditFlag)
            {
                rDD_DailySales.CreatedBy = User.Identity.Name;
                rDD_DailySales.CreatedOn = DateTime.Now;

            }
            else
            {
                rDD_DailySales.LastUpdatedBy = User.Identity.Name;
                rDD_DailySales.LastUpdatedOn = DateTime.Now;
            }
            rDD_DailySales.IsDraft = true;
            rDD_DailySales.DocDraftDate = DateTime.Now;
            return Json(new { data = rDD_Daily.Save(rDD_DailySales) }, JsonRequestBehavior.AllowGet);
        }

        public ActionResult GetRDD_DailySalesReports(DateTime fromdate, DateTime todate)

        {

            return PartialView(rDD_Daily.GetRDD_DailySalesReports(User.Identity.Name, fromdate, todate));
        }
        [Route("DeleteRDD_DailySales")]
        public ActionResult DeleteRDD_DailySales(int VisitId)
        {
            return Json(new { data = rDD_Daily.DeleteActivity(User.Identity.Name, VisitId) }, JsonRequestBehavior.AllowGet);
        }

        //[Route("ImportDailyReports")]
        [HttpPost]
        public ActionResult Index(HttpPostedFileBase file, DateTime fromdate, DateTime todate)
        {
            string erromsg = "";
            string filePath = string.Empty;
            // fromdate = fromdate.AddDays(-7);
            todate = todate.AddDays(1);

            DataTable dt = new DataTable();
            bool Status = false;
            try
            {
                if (file != null)
                {
                    string path = Server.MapPath("~/Uploads/");
                    if (!Directory.Exists(path))
                    {
                        Directory.CreateDirectory(path);
                    }

                    filePath = path + Path.GetFileName(file.FileName);
                    string extension = Path.GetExtension(file.FileName);
                    file.SaveAs(filePath);

                    string conString = string.Empty;

                    switch (extension)
                    {
                        case ".xls": //Excel 97-03.
                            conString = "Provider=Microsoft.Jet.OLEDB.4.0;Data Source=" + filePath + ";Extended Properties='Excel 8.0;HDR=YES'";
                            break;
                        case ".xlsx": //Excel 07 and above.
                            conString = "Provider=Microsoft.ACE.OLEDB.12.0;Data Source=" + filePath + ";Extended Properties='Excel 8.0;HDR=YES'";
                            break;
                    }
                    conString = string.Format(conString, filePath);

                    using (OleDbConnection connExcel = new OleDbConnection(conString))
                    {
                        using (OleDbCommand cmdExcel = new OleDbCommand())
                        {
                            using (OleDbDataAdapter odaExcel = new OleDbDataAdapter())
                            {
                                cmdExcel.Connection = connExcel;

                                //Get the name of First Sheet.
                                connExcel.Open();
                                DataTable dtExcelSchema;
                                dtExcelSchema = connExcel.GetOleDbSchemaTable(OleDbSchemaGuid.Tables, null);
                                string sheetName = dtExcelSchema.Rows[0]["TABLE_NAME"].ToString();
                                connExcel.Close();

                                //Read Data from First Sheet.
                                connExcel.Open();
                                cmdExcel.CommandText = "SELECT * From [" + sheetName + "]";
                                odaExcel.SelectCommand = cmdExcel;
                                odaExcel.Fill(dt);
                                connExcel.Close();
                            }
                        }
                    }

                    //
                    DataColumn newColumn3 = new DataColumn("CreatedOn", typeof(System.DateTime));
                    newColumn3.DefaultValue = System.DateTime.Now;
                    dt.Columns.Add(newColumn3);
                    DataColumn newColumn6 = new DataColumn("VisitDate", typeof(System.DateTime));
                    newColumn6.DefaultValue = fromdate;
                    dt.Columns.Add(newColumn6);
                    DataColumn newColumn2 = new DataColumn("CreatedBy", typeof(System.String));
                    newColumn2.DefaultValue = User.Identity.Name;
                    dt.Columns.Add(newColumn2);
                    DataColumn newColumn = new DataColumn("IsDraft", typeof(System.Int32));
                    newColumn.DefaultValue = 1;
                    dt.Columns.Add(newColumn);
                    DataColumn newColumn1 = new DataColumn("ToDate", typeof(System.DateTime));
                    newColumn1.DefaultValue = todate;
                    dt.Columns.Add(newColumn1);
                    DataColumn newColumn4 = new DataColumn("DocDraftDate", typeof(System.DateTime));
                    newColumn4.DefaultValue = DateTime.Now;
                    dt.Columns.Add(newColumn4);
                    //
                    DataSet ds = rDD_Daily.GetDrop(User.Identity.Name);
                    DataTable CountryDt = ds.Tables[0];
                    DataTable CallModeDt = ds.Tables[2];
                    DataTable CallTypeDt = ds.Tables[1];
                    DataTable NextActionDt = ds.Tables[3];
                    bool contains = false;
                    for (int i = dt.Rows.Count - 1; i >= 0; i--)
                    {
                        if (dt.Rows[i][1] == DBNull.Value)
                        {
                            dt.Rows[i].Delete();
                        }
                        else
                        {
                            contains = CountryDt.AsEnumerable().Any(row => dt.Rows[i][0].ToString() == row.Field<String>("Country"));
                            if (contains == false)
                            {
                                erromsg = "Invalid Field That Row " + i + 1 + " " + dt.Rows[i][0].ToString();
                                i = -1;
                            }
                            if (i != -1)
                            {
                                contains = CallModeDt.AsEnumerable().Any(row => dt.Rows[i][2].ToString() == row.Field<String>("Modeofcall"));
                                if (contains == false)
                                {
                                    erromsg = "Invalid Field That Row " + i + 1 + " " + dt.Rows[i][2].ToString();
                                    i = -1;
                                }
                            }

                            if (i != -1)
                            {
                                contains = CallTypeDt.AsEnumerable().Any(row => dt.Rows[i][3].ToString() == row.Field<String>("CallStatus"));
                                if (contains == false)
                                {
                                    erromsg = "Invalid Field That Row " + i + 1 + " " + dt.Rows[i][3].ToString();
                                    i = -1;
                                }
                            }


                            if (i != -1)
                            {
                                contains = NextActionDt.AsEnumerable().Any(row => dt.Rows[i][13].ToString() == row.Field<String>("NextAction"));
                                if (contains == false)
                                {
                                    erromsg = "Invalid Field That Row " + i + 1 + " " + dt.Rows[i][13].ToString();
                                    i = -1;
                                }
                            }

                            if (i != -1)
                            {
                                if (Convert.ToDateTime(dt.Rows[i][1]) >= fromdate && Convert.ToDateTime(dt.Rows[i][1]) <= todate)
                                {

                                }
                                else
                                {
                                    // dt.Rows[i].Delete();
                                    erromsg = "Invalid Field That Row " + i + 1 + " " + dt.Rows[i][1].ToString();
                                    contains = false;
                                    i = -1;
                                }
                            }

                        }
                    }
                    dt.AcceptChanges();
                    //var data = rDD_Daily.SaveExcel(dt);
                    if (contains == true)
                    {
                        Status = rDD_Daily.SaveExcel(dt);
                    }
                    else
                    {
                        Status = false;
                    }
                }
            }
            catch (Exception ex)
            {
                erromsg = ex.Message;
            }
            if (Status == true)
            {
                erromsg = "Import Successfully";
            }
            return RedirectToAction("Index", new RouteValueDictionary(new { controller = "DSR_DailySales", action = "Index", Status, erromsg }));
        }


        [Route("RDD_DSR_NewSeller")]
        public ActionResult NewResseller(RDD_DSR_NewResellerEntry rDD_DSR_New)
        {
            rDD_DSR_New.CreatedBy = User.Identity.Name;
            rDD_DSR_New.CreatedOn = System.DateTime.Now;
            return Json(new { data = rDD_Daily.NewResseller(rDD_DSR_New) }, JsonRequestBehavior.AllowGet);
        }
        [Route("GetRDD_DailySalesDateRange")]
        public ActionResult GetRDD_DailySalesDateRange(DateTime FromDate, DateTime ToDate)
        {
            DataSet DS = rDD_Daily.GetRDD_DailyReportDateRange(FromDate, ToDate, User.Identity.Name);
            ContentResult ret = null;
            try
            {
                if (DS.Tables.Count > 0)
                {
                    ret = Content(JsonConvert.SerializeObject(DS), "application/json");
                }
            }
            catch (Exception)
            {
                ret = null;
            }
            return ret;
        }

        public ActionResult DailySalesComments()
        {
            RDD_DailySalesReports rdd = new RDD_DailySalesReports();

            DateTime today = DateTime.Today;
            int daysUntilMonday = ((int)DayOfWeek.Monday - (int)today.DayOfWeek + 7) % 7;
            int daysUntilSaturday = ((int)DayOfWeek.Saturday - (int)today.DayOfWeek + 7) % 7;
            DateTime nextSaturday = today.AddDays(daysUntilSaturday);
            DateTime nextMonday = today.AddDays(daysUntilMonday);
            rdd.ToDate = nextSaturday;
            rdd.FromDate = nextMonday;
            return View(rdd);
        }


        public MemoryStream DataToExcel(DataTable dt)
        {
            //StreamWriter sw = new StreamWriter();
            System.IO.StringWriter tw = new System.IO.StringWriter();
            HtmlTextWriter hw = new HtmlTextWriter(tw);
            if (dt.Rows.Count > 0)
            {
                dt.Columns.Remove("VisitId");               
                dt.Columns.Remove("ToDate");
                dt.Columns.Remove("VisitType");
                dt.Columns.Remove("IsNewPartner");               
                dt.Columns.Remove("ForwardCallCCEmail");
                dt.Columns.Remove("Priority");
                dt.Columns.Remove("IsDraft");
                dt.Columns.Remove("NextReminderDate");
                dt.Columns.Remove("DocDraftDate");
                dt.Columns.Remove("CreatedBy");
                dt.Columns.Remove("CreatedOn");
                dt.Columns.Remove("LastUpdatedBy");
                dt.Columns.Remove("LastUpdatedOn");
                dt.Columns.Remove("IsActive");                
                dt.Columns.Remove("IsRead");
                dt.Columns.Remove("ReportReadBy");
                dt.Columns.Remove("ReportReadOn");
                dt.Columns.Remove("Comments");
                dt.Columns.Remove("ActualVisitDate");
                dt.Columns.Remove("IsRptSentToManager");               
                dt.Columns.Remove("EditFlag");
                dt.Columns.Remove("SaveFlag");
                dt.Columns.Remove("ActionType");
                dt.Columns.Remove("ErrorMsg");
                dt.Columns.Remove("FromDate");
                dt.Columns.Remove("ModeOfCallList");
                dt.Columns.Remove("NextActionList");
                //dt.Columns.Remove("ModeOfCallList");
                dt.Columns.Remove("CallStatusList");
                dt.Columns.Remove("CompanyList");
                dt.Columns.Remove("CountryList");
                dt.Columns.Add("DateStr");
                dt.Columns.Add("DateStr1");
                dt.Columns.Add("DateStr2",typeof(double));
                dt.Columns.Add("DateStr3",typeof(string));
                foreach (DataRow dr in dt.Rows)
                {
                    dr["DateStr"] = string.Format("{0:dd-MMM-yyyy}", dr["VisitDate"]);
                    dr["DateStr1"] = string.Format("{0:dd-MMM-yyyy}", dr["ReminderDate"]);
                    dr["DateStr2"] =  dr["ExpectedBusinessAmt"];
                    dr["DateStr3"] = dr["ContactNo"];
                }
                dt.Columns.Remove("VisitDate");
                dt.Columns.Remove("ReminderDate");
                dt.Columns.Remove("ExpectedBusinessAmt");
                dt.Columns.Remove("ContactNo");
                dt.Columns["DateStr"].ColumnName = "VisitDate";
                dt.Columns["DateStr1"].ColumnName = "ReminderDate";
                dt.Columns["DateStr2"].ColumnName = "Biz Amt";
                dt.Columns["DateStr3"].ColumnName = "ContactNo";
                dt.Columns["Country"].SetOrdinal(0);
                dt.Columns["VisitDate"].SetOrdinal(1);
                dt.Columns["ModeOfCall"].SetOrdinal(2);
                dt.Columns["CallStatus"].SetOrdinal(3);              
                dt.Columns["CardCode"].SetOrdinal(4);
                dt.Columns["Company"].SetOrdinal(5);
                dt.Columns["PersonMet"].SetOrdinal(6);
                dt.Columns["Email"].SetOrdinal(7);
                dt.Columns["Designation"].SetOrdinal(8);
                dt.Columns["ContactNo"].SetOrdinal(9);
                dt.Columns["BU"].SetOrdinal(10);
                dt.Columns["Discussion"].SetOrdinal(11);
                dt.Columns["Biz Amt"].SetOrdinal(12);
                dt.Columns["NextAction"].SetOrdinal(13);
                dt.Columns["Feedback"].SetOrdinal(14);
                dt.Columns["ForwardCallToEmail"].SetOrdinal(15);
                dt.Columns["ForwardRemark"].SetOrdinal(16);

                dt.Columns["ReminderDate"].SetOrdinal(17);
                dt.Columns["ReminderDesc"].SetOrdinal(18);

                dt.Columns["CallStatus"].ColumnName = "Call Type";
                dt.Columns["ModeOfCall"].ColumnName = "Call Mode";
                dt.Columns["Company"].ColumnName = "Customer";
                dt.Columns["PersonMet"].ColumnName = "Contact Person";

                dt.Columns["NextAction"].ColumnName = "Next Action";
                dt.Columns["ForwardCallToEmail"].ColumnName = "ForwardCallTo";
                dt.Columns["ForwardRemark"].ColumnName = "Forward Remark";
                dt.Columns["ReminderDate"].ColumnName = "Reminder Date";
                dt.Columns["ReminderDesc"].ColumnName = "Reminder Desc";

                dt.AcceptChanges();


                DataGrid dgGrid = new DataGrid();
                dgGrid.DataSource = dt;
                dgGrid.DataBind();
                dgGrid.HeaderStyle.Font.Bold = true;
                dgGrid.HeaderStyle.BackColor = Color.Yellow;
                //Get the HTML for the control.
                dgGrid.RenderControl(hw);
                //Write the HTML back to the browser.
                //Response.ContentType = application/vnd.ms-excel;
                Response.ClearContent();
                Response.Buffer = true;
                Response.ContentType = "application/vnd.ms-excel";
                Response.ContentEncoding = System.Text.Encoding.Default;
               
            }
            MemoryStream s = new MemoryStream();
            System.Text.Encoding Enc = System.Text.Encoding.Default;
            byte[] mBArray = Enc.GetBytes(tw.ToString());
            s = new MemoryStream(mBArray, false);

            return s;
        }
        

    }
   
}
