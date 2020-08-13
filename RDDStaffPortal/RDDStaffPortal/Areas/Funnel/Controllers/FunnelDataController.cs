using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using RDDStaffPortal.DAL.InitialSetup;
using RDDStaffPortal.DAL.DataModels;
using Microsoft.Ajax.Utilities;
using System.Data;

using RDDStaffPortal.DAL;
using RDDStaffPortal.Areas.Funnel.Models;
using DocumentFormat.OpenXml.Bibliography;
using DocumentFormat.OpenXml.Presentation;
using ClosedXML.Excel;
using System.IO;

namespace RDDStaffPortal.Areas.Funnel.Controllers
{
    [Authorize]
    public class FunnelDataController : Controller
    {
        RDD_FunnelDataDbOperation RddFunnel = new RDD_FunnelDataDbOperation();
        // GET: Funnel/FunnelData
        //public ActionResult GetPieChart()
        //{
          

        //    JsonResult result = new JsonResult();
        //    List<Pichart_Funnel> data1 = new List<Pichart_Funnel>();
          
        //    data1 = RddFunnel.GetPieChartData(User.Identity.Name);

        //    result = this.Json(new { data = data1 }, JsonRequestBehavior.AllowGet);

        //    return result;
           

        //}

        //public ActionResult GetLineChart()
        //{ 

        //    JsonResult result = new JsonResult();
        //    List<Linechart_Funnel> data1 = new List<Linechart_Funnel>();

        //    data1 = RddFunnel.GetLineChartData(User.Identity.Name);

        //    result = this.Json(new { data = data1 }, JsonRequestBehavior.AllowGet);

        //    return result;


       // }

        public ActionResult GetCharts(DateTime fromdate,DateTime todate)
        { 
            JsonResult result = new JsonResult();
            RDD_Funnel_Chart data1 = new RDD_Funnel_Chart();
            data1 = RddFunnel.GetChartDetails(User.Identity.Name,fromdate,todate);
            result = this.Json(new { data = data1 }, JsonRequestBehavior.AllowGet);
            return result;
            
        }

        //public ActionResult Getlinechart()
        //{
        //    JsonResult result = new JsonResult();

        //    List<Sales_BU> lst = new List<Sales_BU>();
        //    //'Active', 'Dormant', 'Hard Block', 'Soft Block', 'Block List'


        //    lst = _DashDbOp.GetSalesSummery(User.Identity.Name);

        //    List<string> list = new List<string> { "Jan", "Feb", "Mar", "Apr", "May", "Jun", "Jul", "Aug", "Sep", "Oct", "Nov", "Dec" };


        //    List<BarChart_Dash> data = new List<BarChart_Dash>();
        //    //'Active', 'Dormant', 'Hard Block', 'Soft Block', 'Block List'
        //    List<decimal> numbers = lst.AsEnumerable()
        //                   .Select(r => r.Points1)
        //                   .ToList();

        //    data.Add(new BarChart_Dash
        //    {
        //        lbls = list,
        //        points = numbers

        //    });
        //    numbers = lst.AsEnumerable()
        //                  .Select(r => r.Points2)
        //                  .ToList();

        //    data.Add(new BarChart_Dash
        //    {

        //        points = numbers

        //    });
        //    numbers = lst.AsEnumerable()
        //                  .Select(r => r.Points3)
        //                  .ToList();

        //    data.Add(new BarChart_Dash
        //    {

        //        points = numbers

        //    });

        //    result = this.Json(new { data = data }, JsonRequestBehavior.AllowGet);

        //    return result;

        //}
        public ActionResult Index()
        {

            string username = User.Identity.Name;
            //DataSet DS = Db.myGetDS("select dbo.GetQuoteID(Country) as QuoteID");
            //string QuoteID = DS.Tables[0].Rows[0]["QuoteID"].ToString();




           
            //Db.constr = System.Configuration.ConfigurationManager.ConnectionStrings["tejSAP"].ConnectionString;
            //DataSet ds = Db.myGetDS("EXEC RDD_FunnelGetPieCrtData '" + username + "'");
            //List<RDD_FunnelData> loginuserList = new List<RDD_FunnelData>();
            //if (ds.Tables.Count > 0)
            //{
            //    for (int i = 0; i < ds.Tables[0].Rows.Count; i++)
            //    {
            //        RDD_FunnelData EmpLst = new RDD_FunnelData();
            //        EmpLst.TotalCost = Convert.ToInt32(ds.Tables[0].Rows[i]["cost"]);
                   
            //        loginuserList.Add(EmpLst);
            //    }
            //}
            //ViewBag.loginuserList = loginuserList;





            ViewBag.CreatedBy = username;
            //ViewBag.QuoteID = QuoteID;

            //ViewBag.Country = RddFunnel.GetCountryList(username).ToList();
            //// ViewBag.BU = RddFunnel.GetBUList().ToList();
            //ViewBag.DealStatus = RddFunnel.GetStatusList().ToList();

            DataSet dsModules = RddFunnel.GetDrop1(username);

            List<Rdd_comonDrop> MonthList = new List<Rdd_comonDrop>();
            List<Rdd_comonDrop> YearList = new List<Rdd_comonDrop>();
            List<Rdd_comonDrop> CountryList = new List<Rdd_comonDrop>();
            List<Rdd_comonDrop> BUList = new List<Rdd_comonDrop>();
            List<Rdd_comonDrop> StatusList = new List<Rdd_comonDrop>();
            List<Rdd_comonDrop> ClosingMonthList = new List<Rdd_comonDrop>();
            List<Rdd_comonDrop> ClosingYearList = new List<Rdd_comonDrop>();


            if (dsModules.Tables.Count > 0)
            {
                DataTable dtModule;
                DataRowCollection drc;
                dtModule = dsModules.Tables[0];
                drc = dtModule.Rows;
                foreach (DataRow dr in drc)
                {
                    CountryList.Add(new Rdd_comonDrop()
                    {
                        Code = !string.IsNullOrWhiteSpace(dr["country"].ToString()) ? dr["country"].ToString() : "",
                    });
                }

                dtModule = dsModules.Tables[1];
                drc = dtModule.Rows;
                foreach (DataRow dr in drc)
                {
                    BUList.Add(new Rdd_comonDrop()
                    {
                        Code = !string.IsNullOrWhiteSpace(dr["BU"].ToString()) ? dr["BU"].ToString() : "",
                    });
                }
                dtModule = dsModules.Tables[2];
                drc = dtModule.Rows;
                foreach (DataRow dr in drc)
                {
                    StatusList.Add(new Rdd_comonDrop()
                    {
                        Code = !string.IsNullOrWhiteSpace(dr["dealStatus"].ToString()) ? dr["dealStatus"].ToString() : "",
                    });
                }


                dtModule = dsModules.Tables[3];
                drc = dtModule.Rows;
                foreach (DataRow dr in drc)
                {
                    MonthList.Add(new Rdd_comonDrop()
                    {
                        Code = !string.IsNullOrWhiteSpace(dr["quoteMonthMMM"].ToString()) ? dr["quoteMonthMMM"].ToString() : "",
                    });
                }
                dtModule = dsModules.Tables[4];
                drc = dtModule.Rows;
                foreach (DataRow dr in drc)
                {
                    YearList.Add(new Rdd_comonDrop()
                    {
                        Code = !string.IsNullOrWhiteSpace(dr["quoteYear"].ToString()) ? dr["quoteYear"].ToString() : "",
                    });
                }

                dtModule = dsModules.Tables[5];
                drc = dtModule.Rows;
                foreach (DataRow dr in drc)
                {
                   ClosingMonthList.Add(new Rdd_comonDrop()
                    {
                        Code = !string.IsNullOrWhiteSpace(dr["expclosingMonthMMM"].ToString()) ? dr["expclosingMonthMMM"].ToString() : "",
                    });
                }
                dtModule = dsModules.Tables[6];
                drc = dtModule.Rows;
                foreach (DataRow dr in drc)
                {
                    ClosingYearList.Add(new Rdd_comonDrop()
                    {
                        Code = !string.IsNullOrWhiteSpace(dr["expclosingYear"].ToString()) ? dr["expclosingYear"].ToString() : "",
                    });
                }



            }

            ViewBag.CountryList = new SelectList(CountryList, "Code", "Code");
            ViewBag.BUList = new SelectList(BUList, "Code", "Code");
            ViewBag.StatusList = new SelectList(StatusList, "Code", "Code");
            ViewBag.MonthList = new SelectList(MonthList, "Code", "Code");
            ViewBag.YearList = new SelectList(YearList, "Code", "Code");
            ViewBag.ClosingMonthList = new SelectList(ClosingMonthList, "Code", "Code");
            ViewBag.ClosingYearList = new SelectList(ClosingYearList, "Code", "Code");


            return View();
        }

        public ActionResult FillCustomer(string Country)
        {


            return Json(RddFunnel.FillCustomer(Country), JsonRequestBehavior.AllowGet);

        }
        public ActionResult FillBU(string Country)
        {
            string username = User.Identity.Name;

            return Json(RddFunnel.FillBU(Country, username), JsonRequestBehavior.AllowGet);

        }

        public ActionResult SaveFunlData(FunnelData FunnelDataa)
        {
            string result = string.Empty;
            try
            {
                RDD_FunnelData rdd_data = new RDD_FunnelData();

              
                rdd_data.ChangeCount = FunnelDataa.ChangeCount;

                rdd_data.fid = FunnelDataa.fid;
                rdd_data.Country = FunnelDataa.Country;
                rdd_data.BUName = FunnelDataa.BUName;
                rdd_data.bdm = FunnelDataa.bdm;
                rdd_data.CardName = FunnelDataa.CardName;
                rdd_data.CardCode = FunnelDataa.CardCode;
                rdd_data.quoteID = FunnelDataa.quoteID;
                // rdd_data.CardName
                // rdd_data.quoid
                rdd_data.enduser = FunnelDataa.enduser;
                rdd_data.goodsDescr = FunnelDataa.goodsDescr;
                rdd_data.remarks = FunnelDataa.remarks;
                rdd_data.Remarks2 = FunnelDataa.Remarks2;
                rdd_data.Remarks3 = FunnelDataa.Remarks3;
                rdd_data.CreatedBy = User.Identity.Name;
                rdd_data.Createddte = FunnelDataa.Createddte;
                rdd_data.UpdatedBy = User.Identity.Name;
                rdd_data.Updateddte = FunnelDataa.Updateddte;
                rdd_data.NextReminderDt = FunnelDataa.NextReminderDt;
                rdd_data.quoteDate = FunnelDataa.quoteDate;
                rdd_data.Cost = FunnelDataa.Cost;
                rdd_data.Landed = FunnelDataa.Landed;
                rdd_data.MarginUSD = FunnelDataa.MarginUSD;
                rdd_data.value = FunnelDataa.value;
                rdd_data.DealStatus = FunnelDataa.DealStatus;
                rdd_data.expClosingDt = FunnelDataa.expClosingDt;
                rdd_data.orderBookedDate = FunnelDataa.orderBookedDate;
                rdd_data.InvoiceDt = FunnelDataa.InvoiceDt;
                result = RddFunnel.Save(rdd_data);
            }

            catch (Exception ex)
            {
                result = "Error occured :" + ex.Message;
            }
            return Json(result, JsonRequestBehavior.AllowGet);
        }
        public ActionResult SaveResellerData(FunnelData ResellerDataa )
        {
            string result = string.Empty;
            string createdby = User.Identity.Name;
            try
            {
                RDD_FunnelData rdd_data = new RDD_FunnelData();

                rdd_data.Country = ResellerDataa.Country;
                rdd_data.resellername = ResellerDataa.resellername;

                result = RddFunnel.Savereseller(rdd_data,createdby);
            }

            catch (Exception ex)
            {
              
                result = "Error occured :" + ex.Message;
            }
            return Json(result, JsonRequestBehavior.AllowGet);
        }
        public ActionResult getQuoiteId(string Country)
        {
            return Json(RddFunnel.getQuoiteId(Country), JsonRequestBehavior.AllowGet);

        }

        public ActionResult DownloadToExcel3()
        {
            DataTable dt = RddFunnel.Getdata1(User.Identity.Name);
            using (XLWorkbook wb = new XLWorkbook())
            {
                wb.Worksheets.Add(dt);
                using (MemoryStream stream = new MemoryStream())
                {
                    wb.SaveAs(stream);
                    return File(stream.ToArray(), "application/vnd.openxmlformats-officedocument.spreadsheetml.sheet", "FunnelSheet.xlsx");
                }
            }

        }

        public ActionResult getFunnelDataList(int pagesize, int pageno, string psearch,string pcountry,string pBU,string pQMonth,string pQYear,string pCloseMonth,string pcloseYear,string pstatus)
        {
            string username = User.Identity.Name;

            var jsonResult = Json(RddFunnel.getFunnelData(pagesize, pageno, username, psearch,pcountry,pBU, pQMonth,pQYear,pCloseMonth,pcloseYear,pstatus), JsonRequestBehavior.AllowGet);
            jsonResult.MaxJsonLength = int.MaxValue;
            return jsonResult;

        }

        public ActionResult GetDataById(int fid)
        {

            return Json(RddFunnel.getdatabyId(fid), JsonRequestBehavior.AllowGet);

        }

        public ActionResult DeleteItemm(int fid)
        {
            string result = string.Empty;
            try
            {

                result = RddFunnel.Deletedata(fid);

            }
            catch (Exception ex)
            {
                result = "Error occured :" + ex.Message;
            }
            // return Json(result, JsonRequestBehavior.AllowGet);
            return Json(new { DeleteFlag = result }, JsonRequestBehavior.AllowGet);
            // return Json(RddFunnel.Deletedata(fid), JsonRequestBehavior.AllowGet);

        }


    


    }
}
