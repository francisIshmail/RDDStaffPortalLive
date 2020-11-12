using RDDStaffPortal.DAL;
using RDDStaffPortal.DAL.DataModels;
using RDDStaffPortal.DAL.DataModels.MarketingPlan;
using RDDStaffPortal.DAL.MarketingPlan;
using RDDStaffPortal.DAL.Targets;
using System;
using System.Collections.Generic;
using System.Data;
using System.IO;
using System.Web;
using System.Web.Mvc;
using System.Web.Script.Serialization;
namespace RDDStaffPortal.Areas.Marketing.Controllers
{
    public class MarketingPlanController : Controller
    {
        // GET: Marketing/MarketingPlan
        CountryTargetDbOperations CountryDbOp = new CountryTargetDbOperations();
        MarketingPlanDbOperation MarketingDbOp = new MarketingPlanDbOperation();

        public ActionResult Index()
        {

            return View();
        }


        public JsonResult GetCountry()
        {
            System.Web.Script.Serialization.JavaScriptSerializer objSerializer = new System.Web.Script.Serialization.JavaScriptSerializer();
            //List<RDD_salesperson> BU = new List<RDD_salesperson>();
            List<RDD_CountryTarget> data = new List<RDD_CountryTarget>();
            data = CountryDbOp.GetCountryList();
            return Json(data, JsonRequestBehavior.AllowGet);

        }

        public JsonResult GetBU()
        {
            System.Web.Script.Serialization.JavaScriptSerializer objSerializer = new System.Web.Script.Serialization.JavaScriptSerializer();
            List<RDD_VenderBu> data = new List<RDD_VenderBu>();
            data = MarketingDbOp.GetBUList();
            return Json(data, JsonRequestBehavior.AllowGet);

        }
        public JsonResult Getrefno(string id)
        {
            DataSet DS = Db.myGetDS("select dbo.GetMarketingRefNo('" + id + "') as  refnoNo ");

            string nextrefNo = DS.Tables[0].Rows[0][0].ToString();

            return Json(nextrefNo, JsonRequestBehavior.AllowGet);

        }
        [HttpPost]
        public ActionResult SavePlan(string maintab, List<MarketingPlanLines> mainrowtab, HttpPostedFileBase files)//string fdata, List<MarketingPlanLines> subdata)
                                                                                                                   //  public JsonResult SavePlan(string model, string model1, HttpPostedFileBase files)//string fdata, List<MarketingPlanLines> subdata)
        {
            string usernm = User.Identity.Name;
            System.Web.Script.Serialization.JavaScriptSerializer objSerializer = new System.Web.Script.Serialization.JavaScriptSerializer();
            string result1 = string.Empty;
            string maindt = Request.Form["maintab"].ToString();
            string subrow = Request.Form["mainrowtab"].ToString();
            JavaScriptSerializer jss = new JavaScriptSerializer();
            MarketingPlanMaster datum = objSerializer.Deserialize<MarketingPlanMaster>(maindt);
            List<MarketingPlanLines> sbdata = jss.Deserialize<List<MarketingPlanLines>>(subrow);
            try
            {

                result1 = MarketingDbOp.SaveMaketingPlan(datum, sbdata, usernm);
                string path = Server.MapPath("~/excelFileUpload/MarketingPlan/" + datum.RefNo);
                HttpFileCollectionBase f = Request.Files;
                var httpContext = System.Web.HttpContext.Current;
                if (!Directory.Exists(path))
                {
                    Directory.CreateDirectory(path);
                }
                for (int i = 0; i < f.Count; i++)
                {
                    HttpPostedFileBase fil = f[i];
                    var fileName = fil.FileName;
                    var fpath = path + "/" + fileName;
                    fil.SaveAs(fpath);

                }

                TempData["Msg"] = "Record Saved Successfully";
                TempData["Val"] = result1;
                TempData["Originator"] = usernm;
            }
            catch (Exception ex)
            {
                // result = "Error occured :" + ex.Message;
                TempData["Msg"] = "Error occured :" + ex.Message;
            }
            //  return RedirectToAction("Index", "SalesPersonTarget", new { area = "Targets" });
            var result = new
            {
                msg = "True",
                user = usernm,
                id = result1,

            };
            return Json(result);// new { result =result, url = Url.Action("Index", "MarketingPlan") });// (result, JsonRequestBehavior.AllowGet);
                                //  return RedirectToAction("Index", "MarketingPlan", new { area = "Marketing" });
        }

        public JsonResult GetFilteredData(string Country, string fund, string BU, string status, string Fromdt, string Todate)
        {
            //RDD_GetFilterMarketingPlan
            System.Web.Script.Serialization.JavaScriptSerializer objSerializer = new System.Web.Script.Serialization.JavaScriptSerializer();
            // List<Marketing_SearchData> data = new List<Marketing_SearchData>();
            //  data = MarketingDbOp.GetFilList(Country, fund, BU, status, Fromdt, Todate);
            //  return Json(data, JsonRequestBehavior.AllowGet);
            // List<Marketing_SearchData> data = MarketingDbOp.GetAllList();//w List<Marketing_SearchData>();
            return Json(new { data = MarketingDbOp.GetFilList(Country, fund, BU, status, Fromdt, Todate) }, JsonRequestBehavior.AllowGet);

        }

        public JsonResult GetPlan(string id)
        {
            System.Web.Script.Serialization.JavaScriptSerializer objSerializer = new System.Web.Script.Serialization.JavaScriptSerializer();
            MarketingPlanMaster data = new MarketingPlanMaster();
            data = MarketingDbOp.GetMarketingplan(id);
            return Json(data, JsonRequestBehavior.AllowGet);

        }
        public JsonResult GetPlanLines(string id)
        {
            System.Web.Script.Serialization.JavaScriptSerializer objSerializer = new System.Web.Script.Serialization.JavaScriptSerializer();
            List<MarketingPlanLines> data = new List<MarketingPlanLines>();
            data = MarketingDbOp.GetMarketingplanLine(id);
            return Json(data, JsonRequestBehavior.AllowGet);

        }
        //public JsonResult getData()
        //{

        //    if (pageSize == -1)
        //    {
        //        draw = "2";
        //    }

        //    List<RDD_Menus> data = menuDbOp.GetMenuList1();
        //    // Total record count.
        //    int totalRecords = 0;
        //    if (data != null)
        //    {
        //        totalRecords = data.Count;
        //    }
        //    else
        //        totalRecords = 0;


        //    if (!string.IsNullOrEmpty(search) &&
        //                !string.IsNullOrWhiteSpace(search))
        //    {
        //        // Apply search
        //        data = data.Where(p => p.MenuId.ToString().ToLower().Contains(search.ToLower()) ||
        //                                p.MenuName.ToString().ToLower().Contains(search.ToLower()) ||
        //                                p.MenuCssClass.ToString().ToLower().Contains(search.ToLower()) ||
        //                                p.ModuleName.ToString().ToLower().Contains(search.ToLower()) ||
        //                                p.Levels.ToString().ToLower().Contains(search.ToLower()) ||
        //                                p.URL.ToString().ToLower().Contains(search.ToLower()) ||
        //                                p.DisplaySeq.ToString().ToLower().Contains(search.ToLower()) ||
        //                                 p.ObjType.ToString().ToLower().Contains(search.ToLower()) ||
        //                               p.IsDefault.ToString().ToLower().Contains(search.ToLower())

        //                              ).ToList();

        //    }


        //    int recFilter = data.Count;


        //    //  data = this.MenuColumnWithOrder(order, orderDir, data);

        //    if (pageSize != -1)
        //    {
        //        data = data.Skip(startRec).Take(pageSize).ToList();
        //    }
        //    else
        //    {
        //        data = data.ToList();
        //    }

        //    result = this.Json(new { draw = Convert.ToInt32(draw), recordsTotal = totalRecords, recordsFiltered = recFilter, data = data }, JsonRequestBehavior.AllowGet);

        //    return result;
        //}

        public JsonResult GetAllData()
        {

            System.Web.Script.Serialization.JavaScriptSerializer objSerializer = new System.Web.Script.Serialization.JavaScriptSerializer();
            List<Marketing_SearchData> data = MarketingDbOp.GetAllList();//w List<Marketing_SearchData>();
            return Json(new { data = MarketingDbOp.GetAllList() }, JsonRequestBehavior.AllowGet);

            //data = MarketingDbOp.GetAllList();
            // return Json(data, JsonRequestBehavior.AllowGet);
            //int startRec = Convert.ToInt32(Request.Form.GetValues("start")[0]);
            //int pageSize = Convert.ToInt32(Request.Form.GetValues("length")[0]);
            //JsonResult result = new JsonResult();
            //string draw = Request.Form.GetValues("draw")[0];

            //int totalRecords = 0;
            //if (data != null)
            //{
            //    totalRecords = data.Count;
            //}
            //else
            //    totalRecords = 0;



            //int recFilter = data.Count;


            ////  data = this.MenuColumnWithOrder(order, orderDir, data);


            //    //data = data.ToList();


            //result = this.Json(new { draw = Convert.ToInt32(draw), recordsTotal = totalRecords, recordsFiltered = recFilter, data = data }, JsonRequestBehavior.AllowGet);

            //return result;

        }
    }
}