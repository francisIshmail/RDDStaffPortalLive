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
            string usernm = User.Identity.Name;
            ViewData["nm"] = usernm;
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
            List<MarketingPlanLines> sbdata = new List<MarketingPlanLines>();
            if (subrow != "")
            {
                sbdata = jss.Deserialize<List<MarketingPlanLines>>(subrow);
            }
            string path = Server.MapPath("~/excelFileUpload/MarketingPlan/" + datum .RefNo+ "/");
            string strMappath = "~/excelFileUpload/" + "MarketingPlan/" + datum.RefNo + "/";
            DirectoryInfo d = new DirectoryInfo(path);//Assuming Test is your Folder
            List<MarketingPlanDoc> mdoc = new List<MarketingPlanDoc>();
            if (!Directory.Exists(strMappath))
            {
                FileInfo[] Files = d.GetFiles(); //Getting Text files
                string str = "";
                foreach (FileInfo file in Files)
                {
                    string fpath = strMappath.ToString().Replace("~", "") + "" + file.Name;
                    str += fpath.Replace(" ", "%20") + ",";


                }
                str = str.Substring(0, str.Length - 1);
                string[] fileList = str.Split(',');
               
                for (int j = 0; j < fileList.Length; j++)
                {
                    MarketingPlanDoc mdoclst = new MarketingPlanDoc();
                    mdoclst.docid = j + 1.ToString();
                    mdoclst.fpath = fileList[j].ToString();
                    mdoc.Add(mdoclst);
                }
            }
            
           
            try
            {
                
               result1 = MarketingDbOp.SaveMaketingPlan(datum, sbdata, usernm,mdoc);
               
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
            
            System.Web.Script.Serialization.JavaScriptSerializer objSerializer = new System.Web.Script.Serialization.JavaScriptSerializer();
           return Json(new { data = MarketingDbOp.GetFilList(Country, fund, BU, status, Fromdt, Todate) }, JsonRequestBehavior.AllowGet);

        }
        public JsonResult  ApprovePlanLines(string data)
        {
            List<ApproveLinedata> sbdata = new List<ApproveLinedata>();
            JavaScriptSerializer jss = new JavaScriptSerializer();
            string usernm = User.Identity.Name;
            //string subrow = Request.Form["mainrowtab"].ToString();
            try
            {
               
                    sbdata = jss.Deserialize<List<ApproveLinedata>>(data);
                    string rslt = MarketingDbOp.ApprovePlanLines(sbdata,  usernm);
                
                var result = new
                {
                    msg = "True",


                };
                return Json(result);
            }
            catch(Exception e)
            {
                var result = new
                {
                    msg = "False",


                };
                return Json(result);
            }
        }
        public JsonResult UpdatePlandtl(string planid, string status, string data)
        {
            List<MarketingPlanLines> sbdata = new List<MarketingPlanLines>();
            JavaScriptSerializer jss = new JavaScriptSerializer();
            string usernm = User.Identity.Name;
            string rslt;
            string result1;
            //string subrow = Request.Form["mainrowtab"].ToString();
            try
            {
                if(status!="Open")
                {
                    result1 = MarketingDbOp.updateplanstatus(planid,status);


                }
                sbdata = jss.Deserialize<List<MarketingPlanLines>>(data);
                 rslt = MarketingDbOp.AddupdatePlanLines(sbdata, usernm,planid);

                var result = new
                {
                    msg = "True",


                };
                return Json(result);
            }
            catch (Exception e)
            {
                var result = new
                {
                    msg = "False",


                };
                return Json(result);
            }
        }
          
        public JsonResult UpdateAmt(string RDDamt , string pid)
        {
            string usernm = User.Identity.Name;
            System.Web.Script.Serialization.JavaScriptSerializer objSerializer = new System.Web.Script.Serialization.JavaScriptSerializer();
            string result1 = string.Empty;
            string msg = "";
            try
            {

                msg = MarketingDbOp.updateamt(RDDamt, pid, usernm);
            }
            catch (Exception ex)
            {
                
                msg = "Error occured :" + ex.Message;
            }
            var result = new
            {
                rspmsg=msg,

            };
            return Json(result);
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
        public JsonResult GetPlanfiles(string id)
        {
            System.Web.Script.Serialization.JavaScriptSerializer objSerializer = new System.Web.Script.Serialization.JavaScriptSerializer();
           // List<MarketingPlanLines> data = new List<MarketingPlanLines>();
            string path = Server.MapPath("~/excelFileUpload/MarketingPlan/" + id+"/");
            string strMappath = "~/excelFileUpload/" + "MarketingPlan/" +id + "/";
            DirectoryInfo d = new DirectoryInfo(path);//Assuming Test is your Folder
            if (!Directory.Exists(strMappath))
            {
                FileInfo[] Files = d.GetFiles(); //Getting Text files
                string str = "";
                foreach (FileInfo file in Files)
                {
                    string fpath = strMappath.ToString().Replace("~", "") + "" + file.Name;


                    str += fpath.Replace(" ", "%20") + ",";


                }
                str = str.Substring(0, str.Length - 1);

                string[] fileList = str.Split(',');


                return Json(fileList, JsonRequestBehavior.AllowGet);
            }
            else
            {
                string[] fileList=new string[] { };
                return Json(fileList, JsonRequestBehavior.AllowGet);
            }
        }
      
        public JsonResult GetAllData()
        {

            System.Web.Script.Serialization.JavaScriptSerializer objSerializer = new System.Web.Script.Serialization.JavaScriptSerializer();
            List<Marketing_SearchData> data = MarketingDbOp.GetAllList();//w List<Marketing_SearchData>();
            return Json(new { data = MarketingDbOp.GetAllList() }, JsonRequestBehavior.AllowGet);

           

        }


        [HttpPost]
        public JsonResult UploadDoc(string Refno, string type)
        {
            string fname = "";
            string strMappath = "";
            string _imgname = string.Empty;
            string _imgname1 = string.Empty;
            if (Request.Files.Count > 0)
            {
                try
                {
                    string str = Refno;// + "_" + User.Identity.Name;
                    //strMappath = "~/excelFileUpload/" + "PV/" + User.Identity.Name + "/" + type + "/";
                    strMappath = "~/excelFileUpload/" + "MarketingPlan/" + str + "/";

                    if (!Directory.Exists(strMappath))
                    {
                        Directory.CreateDirectory(System.IO.Path.Combine(Server.MapPath(strMappath)));
                    }


                    //  Get all files from Request object  
                    System.Web.HttpFileCollectionBase files = Request.Files;
                    var _ext = "";
                    var fileName = "";
                    for (int i = 0; i < files.Count; i++)
                    {
                        HttpPostedFileBase file = files[i];
                        // Checking for Internet Explorer  
                        if (Request.Browser.Browser.ToUpper() == "IE" || Request.Browser.Browser.ToUpper() == "INTERNETEXPLORER" || Request.Browser.Browser.ToUpper() == "GOOGLE CHROME")
                        {
                            string[] testfiles = file.FileName.Split(new char[] { '\\' });
                            fname = testfiles[testfiles.Length - 1];
                        }
                        else
                        {

                            fname = file.FileName;
                            fileName = Path.GetFileNameWithoutExtension(file.FileName);
                            _ext = System.IO.Path.GetExtension(fname).ToUpper();
                            if ((_ext != ".JPG" && _ext != ".PNG" && _ext != ".GIF" && _ext != ".PDF") && type == "Header")
                            {
                                return Json("Error occurred. Error details: Only Image Or Pdf", JsonRequestBehavior.AllowGet);
                            }

                        }

                        // Get the complete folder path and store the file inside it.
                        _imgname1 = fileName + "" + System.DateTime.Now.ToString("ddMMyyyyHHMMss") + "" + _ext;
                        _imgname = System.IO.Path.Combine(Server.MapPath(strMappath), _imgname1);
                        file.SaveAs(_imgname);
                    }
                    // Returns message that successfully uploaded  
                    return Json(strMappath.ToString().Replace("~", "") + _imgname1, JsonRequestBehavior.AllowGet);
                }
                catch (Exception ex)
                {
                    return Json("Error occurred. Error details: " + ex.Message);
                }
            }
            else
            {
                return Json(strMappath.ToString().Replace("~", "") + _imgname1, JsonRequestBehavior.AllowGet);
            }

        }

    }
}