using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using RDDStaffPortal.Areas.HR.Models;
using RDDStaffPortal.DAL.HR;
using RDDStaffPortal.DAL.DataModels;
using System.Data;
using RDDStaffPortal.DAL;
using System.IO;
using System.Web.Helpers;
using System.Net;
using System.Web.Routing;
using RDDStaffPortal.WebServices;
using DocumentFormat.OpenXml.Drawing.Charts;
using DataTable = System.Data.DataTable;
using Antlr.Runtime;
using Newtonsoft.Json;

namespace RDDStaffPortal.Areas.HR.Controllers
{
    [Authorize]
    public class EmployeesController : Controller
    {
        EmployeeRegistrationDbOperation EmpDbOp = new EmployeeRegistrationDbOperation();

        Common cm = new Common();
        AccountService accountservice = new AccountService();

       // public ActionResult ToUpdateAcc(string email, string Fname,string Lname)
       // { 
       //    // return Json() 
                
       //}


        public ActionResult GetCreateUserAcc(string username, string useremail, string ques, string ans, string role,string fname,string lname)
        {
         
            string result = string.Empty;

            var k = accountservice.CreateUserAccount(username, useremail, ques, ans, role);
            if (k.Success == true)
            {

                var t = EmpDbOp.Update(useremail, fname, lname);
                if (t == false)
                {
                    k.Message = "Error";
                }
               // result = rdd_empreg.Update();
            }

            return Json(new  { Message=k.Message }, JsonRequestBehavior.AllowGet);

        }

        public ActionResult gethistory(int empid,string tblname)
        {

            return Json(cm.GetChangeLog(empid,tblname),JsonRequestBehavior.AllowGet);
        }

        // GET: Admin/EmployeeRegistration
        //public ActionResult Create()
        //{
        //    return View();
        //}
        //[Route("Create")]
        //[HttpPost]
        //public ActionResult Create(Employees model)
        //{
        //    HttpPostedFileBase file = Request.Files["ImageData"];
        //    ContentRepository service = new ContentRepository();
        //    int i = service.UploadImageInDataBase(file, model);
        //    if (i == 1)
        //    {
        //        return RedirectToAction("Index");
        //    }
        //    return View(model);
        //}
        //private readonly DBContext db = new DBContext();
        //public int UploadImageInDataBase(HttpPostedFileBase file, ContentViewModel contentViewModel)
        //{
        //    contentViewModel.Image = ConvertToBytes(file);
        //    var Content = new Content
        //    {
        //        Title = contentViewModel.Title,
        //        Description = contentViewModel.Description,
        //        Contents = contentViewModel.Contents,
        //        Image = contentViewModel.Image
        //    };
        //    db.Contents.Add(Content);
        //    int i = db.SaveChanges();
        //    if (i == 1)
        //    {
        //        return 1;
        //    }
        //    else
        //    {
        //        return 0;
        //    }
        //}
        //public byte[] ConvertToBytes(HttpPostedFileBase image)
        //{
        //    byte[] imageBytes = null;
        //    BinaryReader reader = new BinaryReader(image.InputStream);
        //    imageBytes = reader.ReadBytes((int)image.ContentLength);
        //}
        public ActionResult Index(int? EmployeeId)

        {
            Db.constr = System.Configuration.ConfigurationManager.ConnectionStrings["tejSAP"].ConnectionString;
         
            string username = User.Identity.Name;
            DataSet ds;
            if (EmployeeId != null)
            {

                 ds = EmpDbOp.GetDrop2(username, EmployeeId);
                // DataSet ds1 = Db.myGetDS("exec RDD_UpdateEmployeeLogin");
             //   DataSet ds1 = Db.myGetDS("exec RDD_CompareUser '" + EmployeeId + "'");
                string name = ds.Tables[0].Rows[0]["LoginName"].ToString();
                
                if (username.ToLower() == name.ToLower())
                {
                    string com = "True";
                    ViewBag.comparename = com;
                }
            }
            else
            {
                 ds = EmpDbOp.GetDrop1(username);

            }
          
            // ds = Db.myGetDS("EXEC RDD_GetUserType '" + username + "'");
            List<RDD_EmployeeRegistration> loginuserList = new List<RDD_EmployeeRegistration>();
            if (ds.Tables.Count > 0)
            {
                for (int i = 0; i < ds.Tables[1].Rows.Count; i++)
                {
                    RDD_EmployeeRegistration EmpLst = new RDD_EmployeeRegistration();
                    EmpLst.IsUserInRoleHeadOfFinance = ds.Tables[1].Rows[i]["IsFinanceUser"].ToString();
                    EmpLst.IsUserInRoleHR = ds.Tables[1].Rows[i]["IsHRUser"].ToString();


                    loginuserList.Add(EmpLst);
                }
            }
            ViewBag.loginuserList = loginuserList;
            var loginuser = loginuserList;

           // Db.constr = System.Configuration.ConfigurationManager.ConnectionStrings["tejSAP"].ConnectionString;
           // DataSet DS = Db.myGetDS("EXEC EmpReg_GetDDLDataToBind");
            List<RDD_EmployeeRegistration> DeptList = new List<RDD_EmployeeRegistration>();
            if (ds.Tables.Count > 0)
            {
                for (int i = 0; i < ds.Tables[2].Rows.Count; i++)
                {
                    RDD_EmployeeRegistration DeptLst = new RDD_EmployeeRegistration();
                    DeptLst.DeptId = Convert.ToInt32(ds.Tables[2].Rows[i]["DeptId"]);
                    DeptLst.DeptName = ds.Tables[2].Rows[i]["DeptName"].ToString();
                    DeptList.Add(DeptLst);
                }
            }

            List<RDD_EmployeeRegistration> DesigList = new List<RDD_EmployeeRegistration>();
            if (ds.Tables.Count > 0)
            {
                for (int i = 0; i < ds.Tables[3].Rows.Count; i++)
                {
                    RDD_EmployeeRegistration DesigLst = new RDD_EmployeeRegistration();
                    DesigLst.DesigId = Convert.ToInt32(ds.Tables[3].Rows[i]["DesigId"]);
                    DesigLst.DesigName = ds.Tables[3].Rows[i]["DesigName"].ToString();
                    DesigList.Add(DesigLst);
                }

            }

            List<RDD_EmployeeRegistration> StatusList = new List<RDD_EmployeeRegistration>();
            if (ds.Tables.Count > 0)
            {
                for (int i = 0; i < ds.Tables[4].Rows.Count; i++)
                {
                    RDD_EmployeeRegistration StatusLst = new RDD_EmployeeRegistration();
                    StatusLst.StatusId = Convert.ToInt32(ds.Tables[4].Rows[i]["StatusId"]);
                    StatusLst.StatusName = ds.Tables[4].Rows[i]["StatusName"].ToString();
                    StatusList.Add(StatusLst);
                }

            }

            List<RDD_EmployeeRegistration> JobBandList = new List<RDD_EmployeeRegistration>();
            if (ds.Tables.Count > 0)
            {
                for (int i = 0; i < ds.Tables[5].Rows.Count; i++)
                {
                    RDD_EmployeeRegistration JobbandLst = new RDD_EmployeeRegistration();
                    JobbandLst.JobBandId = Convert.ToInt32(ds.Tables[5].Rows[i]["JobBandId"]);
                    JobbandLst.JobBandName = ds.Tables[5].Rows[i]["JobBandName"].ToString();
                    JobBandList.Add(JobbandLst);
                }

            }

            List<RDD_EmployeeRegistration> JobGradeList = new List<RDD_EmployeeRegistration>();
            if (ds.Tables.Count > 0)
            {
                for (int i = 0; i < ds.Tables[6].Rows.Count; i++)
                {
                    RDD_EmployeeRegistration jobgradeLst = new RDD_EmployeeRegistration();
                    jobgradeLst.JobGradeId = Convert.ToInt32(ds.Tables[6].Rows[i]["JobGradeId"]);
                    jobgradeLst.JobGradeName = ds.Tables[6].Rows[i]["JobGradeName"].ToString();
                    JobGradeList.Add(jobgradeLst);
                }

            }



          //  ds = Db.myGetDS("EXEC RDD_GetManagerList '"+username+"'");
            List<RDD_EmployeeRegistration> ManagerList = new List<RDD_EmployeeRegistration>();
            if (ds.Tables.Count > 0)
            {
                for (int i = 0; i < ds.Tables[7].Rows.Count; i++)
                {
                    RDD_EmployeeRegistration MangLst = new RDD_EmployeeRegistration();
                    MangLst.ManagerId = Convert.ToInt32(ds.Tables[7].Rows[i]["EmployeeId"]);
                    MangLst.ManagerName = ds.Tables[7].Rows[i]["Empname"].ToString();
                    ManagerList.Add(MangLst);

                }
            }

            //DS = Db.myGetDS("EXEC RDD_GetManagerList '" + username + "'");
            List<RDD_EmployeeRegistration> ManagerListL2 = new List<RDD_EmployeeRegistration>();
            if (ds.Tables.Count > 0)
            {
                for (int i = 0; i < ds.Tables[7].Rows.Count; i++)
                {
                    RDD_EmployeeRegistration MangLstl2 = new RDD_EmployeeRegistration();
                    MangLstl2.ManagerIdL2 = Convert.ToInt32(ds.Tables[7].Rows[i]["EmployeeId"]);
                    MangLstl2.ManagerName = ds.Tables[7].Rows[i]["Empname"].ToString();
                    ManagerListL2.Add(MangLstl2);

                }
            }




           // DS = Db.myGetDS("EXEC RDD_BU");
            List<RDD_EmployeeRegistration> BUList = new List<RDD_EmployeeRegistration>();
            if (ds.Tables.Count > 0)
            {
                for (int i = 0; i < ds.Tables[8].Rows.Count; i++)
                {
                    RDD_EmployeeRegistration BULst = new RDD_EmployeeRegistration();
                    BULst.u_bugroup = ds.Tables[8].Rows[i]["BUGroup"].ToString();

                    BUList.Add(BULst);
                }
            }

          //  ds = Db.myGetDS("EXEC RDD_GETCountryCode");
            List<RDD_EmployeeRegistration> CountryList = new List<RDD_EmployeeRegistration>();
            if (ds.Tables.Count > 0)
            {
                for (int i = 0; i < ds.Tables[9].Rows.Count; i++)
                {
                    RDD_EmployeeRegistration CouLst = new RDD_EmployeeRegistration();

                    CouLst.CountryCode = ds.Tables[9].Rows[i]["CountryCode"].ToString();
                    CouLst.Country = ds.Tables[9].Rows[i]["Country"].ToString();
                    CountryList.Add(CouLst);

                }
            }
           

           // DS = Db.myGetDS("EXEC RDD_GETCountryCode");
            List<RDD_EmployeeRegistration> ddlCountry = new List<RDD_EmployeeRegistration>();
            if (ds.Tables.Count > 0)
            {
                for (int i = 0; i < ds.Tables[9].Rows.Count; i++)
                {
                    RDD_EmployeeRegistration CouLst = new RDD_EmployeeRegistration();
                    CouLst.CountryCodeName = ds.Tables[9].Rows[i]["CountryCode"].ToString();
                    CouLst.Country = ds.Tables[9].Rows[i]["Country"].ToString();
                    ddlCountry.Add(CouLst);

                }
            }

          
          //  ds = Db.myGetDS("select dbo.GetRDDEmpNo() as  EmpNo ");

             string NextEmpNo =ds.Tables[10].Rows[0]["EmpNO"].ToString();


           // DS = Db.myGetDS("EXEC RDD_GETCurrency");
            List<RDD_EmployeeRegistration> CurrencyList = new List<RDD_EmployeeRegistration>();
            if (ds.Tables.Count > 0)
            {
                for (int i = 0; i < ds.Tables[11].Rows.Count; i++)
                {
                    RDD_EmployeeRegistration CurrencyLst = new RDD_EmployeeRegistration();
                    CurrencyLst.Currency = (ds.Tables[11].Rows[i]["Currency"]).ToString();

                    CurrencyList.Add(CurrencyLst);
                }
            }
            ViewBag.JobBandList = new SelectList(JobBandList, "JobBandId", "JobBandName");
            ViewBag.JobGradeList = new SelectList(JobGradeList, "JobGradeId", "JobGradeName");

            ViewBag.DeptList = new SelectList(DeptList, "DeptId", "DeptName");
            ViewBag.DesigList = new SelectList(DesigList, "DesigId", "DesigName");
            ViewBag.StatusList = new SelectList(StatusList, "StatusId", "StatusName");
            ViewBag.BUList = new SelectList(BUList, "u_bugroup", "u_bugroup");

            ViewBag.CountryList = CountryList;
            ViewBag.ManagerList = new SelectList(ManagerList, "ManagerId", "Managername");
            ViewBag.ManagerListL2 = new SelectList(ManagerListL2, "ManagerIdL2", "Managername");

            
            ViewBag.ddlCountry = new SelectList(ddlCountry, "CountryCodeName", "Country");
            
            ViewBag.CurrencyList = new SelectList(CurrencyList, "Currency", "Currency");

            // if (EmployeeId == null)
            if (EmployeeId == null )
            {
                RDD_EmployeeRegistration objemp = new RDD_EmployeeRegistration();
                ViewBag.EmployeeNo = NextEmpNo;
                objemp.Editflag = false;

                objemp.ImagePath1 = Server.MapPath("/Images/TempLogo/defaultimg.jpg");
                objemp.LogoType = ".jpg";
                byte[] file;
                using (var stream = new FileStream(objemp.ImagePath1, FileMode.Open, FileAccess.Read))
                {
                    using (var reader1 = new BinaryReader(stream))
                    {
                        file = reader1.ReadBytes((int)stream.Length);
                    }
                }
                objemp.ImagePath = file;
                
                ds = EmpDbOp.GetEmployeeConfigure(User.Identity.Name, "II");
                if (ds.Tables.Count > 0)
                {
                    for (int i = 0; i < ds.Tables[0].Rows.Count; i++)
                    {
                        var columName = (ds.Tables[0].Rows[i]["ColumName"]).ToString();
                        ViewData.Add(columName, true);

                    }
                }
                objemp.DOB = DateTime.Now;

                
                return View(objemp);
            }
            else
            {
                RDD_EmployeeRegistration objemp = EmpDbOp.Edit(EmployeeId);
              //  Session["FILE"] = objemp.ImagePath;
                ViewBag.EmployeeNo = objemp.EmployeeNo;
                
                objemp.Editflag = true;

                ds = EmpDbOp.GetEmployeeConfigure(User.Identity.Name, "II");
                if (ds.Tables.Count > 0)
                {
                    for (int i = 0; i < ds.Tables[0].Rows.Count; i++)
                    {
                        var columName = (ds.Tables[0].Rows[i]["ColumName"]).ToString();
                        ViewData.Add(columName, true);
                    }
                }
                return View(objemp);
            }


           // return View();


        }
       [Route("GetCountrywthFlag")]
        public ActionResult GetCountrywthFlag()
        {

           DataSet DS = Db.myGetDS("EXEC RDD_GetNationalityList");
            List<Rdd_comonDrop> CitizenshipList = new List<Rdd_comonDrop>();
            if (DS.Tables.Count > 0)
            {
                for (int i = 0; i < DS.Tables[0].Rows.Count; i++)
                {
                    Rdd_comonDrop CitizenshipLst = new Rdd_comonDrop();

                    CitizenshipLst.CodeName = (DS.Tables[0].Rows[i]["Citizenship"]).ToString();
                    CitizenshipLst.Code= (DS.Tables[0].Rows[i]["Code"]).ToString();

                    CitizenshipList.Add(CitizenshipLst);
                }
            }
            return Json(CitizenshipList, JsonRequestBehavior.AllowGet);
           // ViewBag.CitizenshipList = new SelectList(CitizenshipList, "CodeName", "Code");
           // return View();
                 
        }
        public ActionResult GetBuList(string bugroup)
        {
            Db.constr = System.Configuration.ConfigurationManager.ConnectionStrings["tejSAP"].ConnectionString;
            // DataSet DS = Db.myGetDS("select ItmsGrpNam from SAPAE.dbo.OITB  where u_bugroup='" + bugroup+"'");
            DataSet DS = Db.myGetDS("exec RDD_GetBUList " + bugroup + "");
            List<RDD_EmployeeRegistration> BUItmList = new List<RDD_EmployeeRegistration>();
            if (DS.Tables.Count > 0)
            {
                for (int i = 0; i < DS.Tables[0].Rows.Count; i++)
                {
                    RDD_EmployeeRegistration BUItmLst = new RDD_EmployeeRegistration();
                    BUItmLst.ItmsGrpNam = DS.Tables[0].Rows[i]["ItmsGrpNam"].ToString();
                    BUItmList.Add(BUItmLst);

                }
            }
            //ViewBag.BUItmList = BUItmList;
            return Json(BUItmList, JsonRequestBehavior.AllowGet);

        }

        public ActionResult ViewProfile()
        {
            //Response.Redirect("/Employee/EmployeeId")
            //;
            return RedirectToAction("Index", new RouteValueDictionary(new { controller = "Employees", action = "Index", EmployeeId = EmpDbOp.GetEmployeeIdByLoginName(User.Identity.Name) }));
        }

        [HttpPost]
        public JsonResult AddEmpReg(Employees EmpData, List<RDD_EmployeeRegistration> EmpInfoProEdu, IEnumerable<HttpPostedFileBase> files, List<DocumentList> EmpDatas)
        {
                    string result = string.Empty;
            try
            {
               
                RDD_EmployeeRegistration rdd_empreg = new RDD_EmployeeRegistration();             
                
              //  rdd_empreg.ImagePath = EmpData.ImagePath;
                rdd_empreg.JobBandId = EmpData.JobBandId;
                rdd_empreg.JobGradeId = EmpData.JobGradeId;
                rdd_empreg.ManagerIdL2 = EmpData.ManagerIdL2;

                rdd_empreg.EmployeeId = EmpData.EmployeeId;
                rdd_empreg.About = EmpData.About;
                rdd_empreg.CountryCodeName = EmpData.CountryCodeName;
                rdd_empreg.ManagerId = EmpData.ManagerId;
              //  rdd_empreg.ManagerName = EmpData.ManagerName;
                rdd_empreg.EmployeeNo = EmpData.EmployeeNo;
                rdd_empreg.FName = EmpData.FName;
                rdd_empreg.LName = EmpData.LName;
                rdd_empreg.Email = EmpData.Email;
                rdd_empreg.Gender = EmpData.Gender;
                rdd_empreg.Current_Address = EmpData.Current_Address;
                rdd_empreg.Permanent_Address = EmpData.Permanent_Address;
                rdd_empreg.Contact_No = EmpData.Contact_No;
                rdd_empreg.Ext_no = EmpData.Ext_no;
                rdd_empreg.IM_Id = EmpData.IM_Id;
                rdd_empreg.Marital_Status = EmpData.Marital_Status;
                rdd_empreg.DOB = EmpData.DOB;

                rdd_empreg.Citizenship = EmpData.Citizenship;
                rdd_empreg.DesigId = EmpData.DesigId;
                rdd_empreg.DeptId = EmpData.DeptId;
                rdd_empreg.Emergency_Contact = EmpData.Emergency_Contact;
                rdd_empreg.passport_no = EmpData.passport_no;

                rdd_empreg.StatusId = EmpData.StatusId;
                rdd_empreg.type_of_employement = EmpData.type_of_employement;
                rdd_empreg.Joining_Date = EmpData.Joining_Date;
                rdd_empreg.No_child = EmpData.No_child;
                rdd_empreg.National_id = EmpData.National_id;
                rdd_empreg.Contract_Start_date = EmpData.Contract_Start_date;
                rdd_empreg.Note = EmpData.Note;
                rdd_empreg.IsActive = EmpData.IsActive;
                rdd_empreg.ImagePath1 = EmpData.ImagePath1;

                rdd_empreg.CreatedBy = User.Identity.Name;

                rdd_empreg.FId = EmpData.FId;

                rdd_empreg.Currency = EmpData.Currency;
                rdd_empreg.Salary = EmpData.Salary;
                rdd_empreg.Salary_Start_Date = EmpData.Salary_Start_Date;
                rdd_empreg.Remark = EmpData.Remark;
                rdd_empreg.Account_No = EmpData.Account_No;
                rdd_empreg.Bank_Name = EmpData.Bank_Name;
                rdd_empreg.Branch_Name = EmpData.Branch_Name;
                rdd_empreg.Bank_Code = EmpData.Bank_Code;
                rdd_empreg.Tax_no = EmpData.Tax_no;
                rdd_empreg.Insurance_no = EmpData.Insurance_no;
                rdd_empreg.other_ref_no = EmpData.other_ref_no;


                rdd_empreg.CId = EmpData.CId;
                rdd_empreg.CountryCode = EmpData.CountryCode;


              
                if(rdd_empreg.ImagePath1=="" || rdd_empreg.ImagePath1==null)
                {
                    rdd_empreg.imgbool =false;
                }
                else
                {
                    rdd_empreg.imgbool =true;
                }

                string TempPath = (string)Session["FILE"];

                if (TempPath != null && TempPath != "")
                {
                    rdd_empreg.ImagePath1 = TempPath;
                    rdd_empreg.LogoType = (string)Session["type"];
                }
                else
                {
                   
                  
                    rdd_empreg.ImagePath1 = Server.MapPath("/Images/TempLogo/defaultimg.jpg");
                    rdd_empreg.LogoType = ".jpg";
                   
                }
               


                result = EmpDbOp.Save(rdd_empreg, EmpInfoProEdu, EmpDatas);

              //  string[] UserName = EmpData.Email.Split('@');

             //  var response =   accountservice.CreateUserAccount(UserName[0], EmpData.Email, "  What is your favorite website ?", "www.reddotdistribution.com", "webReports");
              // result = response.Message;



                Session["FILE"] = null;

            }
            catch (Exception ex)
            {
                result = "Error occured :" + ex.Message;
            }
            return Json(result, JsonRequestBehavior.AllowGet);
        }


        
        public JsonResult DeleteRecord(int EId)
        {
            string result = string.Empty;
            try
            {
                result = EmpDbOp.Delete(EId);
            }
            catch (Exception ex)
            {
                result = "Error occured :" + ex.Message;
            }
            return Json(new { DeleteFlag= result }, JsonRequestBehavior.AllowGet);
        }

        public JsonResult DeleteAttachRecord(int DId)
        {
            string result = string.Empty;
            try
            {
                result = EmpDbOp.DeleteAttc(DId);
            }
            catch (Exception ex)
            {
                result = "Error occured :" + ex.Message;
            }
            return Json(new { DeleteFlag = result }, JsonRequestBehavior.AllowGet);
        }

        public JsonResult Uploadfile()
        {
            string _imgname = string.Empty;
            if (System.Web.HttpContext.Current.Request.Files.AllKeys.Any())
            {
                var pic = System.Web.HttpContext.Current.Request.Files["MyImages"];
                if (pic.ContentLength > 0)
                {
                    var _ext = Path.GetExtension(pic.FileName);
                    if (_ext.ToLower() != ".jpeg" && _ext.ToLower() != ".jpg" && _ext.ToLower() != ".png" && _ext.ToLower() != ".bmp")
                        return Json("InvalidError", JsonRequestBehavior.AllowGet);
                    var fileName = Path.GetFileName(pic.FileName);


                  // _imgname = DateTime.Now.ToString("ddMMyyyyhhmmss");
                    var _comPath = Server.MapPath("/Images/TempLogo/Temp") +"user_" + _imgname + "Abc" + _ext;
                    _imgname = "Tempuser_" +  "Abc" + _ext;

                    ViewBag.Msg = _comPath;
                    var path = _comPath;

                    string[] files = System.IO.Directory.GetFiles(Server.MapPath("/Images/TempLogo"), "Temp" + "Abc" + ".*");
                    foreach (string f in files)
                    {
                        System.IO.File.Delete(f);
                    }

                    Session["FILE"] = _comPath;
                    Session["type"] = _ext;
                    // Saving Image in Original Mode
                    pic.SaveAs(path);

                    // resizing image
                    MemoryStream ms = new MemoryStream();
                    WebImage img = new WebImage(_comPath);

                    if (img.Width > 200)
                        img.Resize(200, 200);
                    img.Save(_comPath);
                    // end resize
                }
            }

            return Json(_imgname, JsonRequestBehavior.AllowGet);
        }

        public JsonResult GetEmpNxtNo()
        {
           DataSet DS = Db.myGetDS("select dbo.GetRDDEmpNo() as  EmpNo ");

            string NextEmpNo = DS.Tables[0].Rows[0]["EmpNO"].ToString();

            return Json(NextEmpNo, JsonRequestBehavior.AllowGet);
            
        }



        public FileResult Download(string parentPartId)
        {
            
                byte[] fileBytes = null;
                string FileVirtualPath = null;
                if (parentPartId != null)
                {
                   
                    FileVirtualPath = "~/Uploads/" + parentPartId;


                }
                return File(FileVirtualPath, "application/force-download", Path.GetFileName(FileVirtualPath));
               
           
           }
        [HttpPost]
        public JsonResult UploadDoc()
        {
            string fname = "";           
            string _imgname = string.Empty;
            if (Request.Files.Count > 0)
            {
                try
                {
                    //  Get all files from Request object  
                    System.Web.HttpFileCollectionBase files = Request.Files;
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

                            var _ext = Path.GetExtension(fname);
                           
                        }
                        // Get the complete folder path and store the file inside it.  
                        _imgname = Path.Combine(Server.MapPath("/Uploads/"), fname);
                        file.SaveAs(_imgname);
                    }
                    // Returns message that successfully uploaded  
                    return Json(fname, JsonRequestBehavior.AllowGet);
                }
                catch (Exception ex)
                {
                    return Json("Error occurred. Error details: " + ex.Message);
                }
            }
            else
            {
                return Json(fname, JsonRequestBehavior.AllowGet);
            }
           
        }


        public ActionResult Fillmanagerl2(int managerL1Id)
        {
            Db.constr = System.Configuration.ConfigurationManager.ConnectionStrings["tejSAP"].ConnectionString;
            DataSet DS = Db.myGetDS("exec RDD_GetManagerL2 '" + managerL1Id+"'");
            List <RDD_EmployeeRegistration> ManagerListL2 = new List<RDD_EmployeeRegistration>();
            if (DS.Tables.Count > 0)
            {
                for (int i = 0; i < DS.Tables[0].Rows.Count; i++)
                {
                    RDD_EmployeeRegistration MangLstl2 = new RDD_EmployeeRegistration();
                    MangLstl2.ManagerIdL2 = Convert.ToInt32(DS.Tables[0].Rows[i]["EmployeeId"]);
                    MangLstl2.ManagerName = DS.Tables[0].Rows[i]["Empname"].ToString();
                    ManagerListL2.Add(MangLstl2);

                }
            }

            return Json(ManagerListL2, JsonRequestBehavior.AllowGet);
        }



        //public ActionResult Fillmanagerl1(int managerL1)
        //{
        //    Db.constr = System.Configuration.ConfigurationManager.ConnectionStrings["tejSAP"].ConnectionString;
        //    DataSet DS = Db.myGetDS("exec RDD_GetManagerL2 '" + managerL1 + "'");
        //    List<RDD_EmployeeRegistration> ManagerListL2 = new List<RDD_EmployeeRegistration>();
        //    if (DS.Tables.Count > 0)
        //    {
        //        for (int i = 0; i < DS.Tables[0].Rows.Count; i++)
        //        {
        //            RDD_EmployeeRegistration MangLstl2 = new RDD_EmployeeRegistration();
        //            MangLstl2.ManagerIdL2 = Convert.ToInt32(DS.Tables[0].Rows[i]["EmployeeId"]);
        //            MangLstl2.ManagerName = DS.Tables[0].Rows[i]["Empname"].ToString();
        //            ManagerListL2.Add(MangLstl2);

        //        }
        //    }

        //    return Json(ManagerListL2, JsonRequestBehavior.AllowGet);
        //}

      //  [ChildActionOnly]
        public ActionResult GetEmployeeConfig()
        {
            List<Employee_ConfigureList> list =new List<Employee_ConfigureList>();
            DataSet ds = EmpDbOp.GetEmployeeConfigure(User.Identity.Name, "I");
            if (ds.Tables.Count > 0)
            {
               
                for (int i = 0; i < ds.Tables[0].Rows.Count; i++) {
                    Employee_ConfigureList Emp = new Employee_ConfigureList();
                    Emp.ColumnName=ds.Tables[0].Rows[i]["Column"].ToString();
                    Emp.Description = ds.Tables[0].Rows[i]["Description"].ToString();
                    Emp.Types = ds.Tables[0].Rows[i]["Types"].ToString();

                    list.Add(Emp);
                }
               
            }
            return PartialView(list);
        }
        [Route("GetEmpConfigList")]
        public ActionResult GetEmpConfigList(string UserRole)
        {
            DataSet ds = EmpDbOp.GetEmployeeConfigure(UserRole, "II");
            return Content(JsonConvert.SerializeObject(ds), "application/json");
        }


        public ActionResult EmployeeConfig()
        {
            return View();
        }
        [Route("SaveEmpConfig")]
        public ActionResult saveEmpConfig(Employee_Configure EmpConfig)
        {
            return Json(EmpDbOp.EmployeeConfigure(EmpConfig),JsonRequestBehavior.AllowGet);

        }


    }
}



