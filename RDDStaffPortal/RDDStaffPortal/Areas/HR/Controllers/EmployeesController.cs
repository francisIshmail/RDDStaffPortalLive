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

namespace RDDStaffPortal.Areas.HR.Controllers
{
    [Authorize]
    public class EmployeesController : Controller
    {
        EmployeeRegistrationDbOperation EmpDbOp = new EmployeeRegistrationDbOperation();

        Common cm = new Common();
        AccountService accountservice = new AccountService();


        public ActionResult GetCreateUserAcc(string username, string useremail, string ques, string ans, string role)
        {
            return Json(accountservice.CreateUserAccount(username, useremail, ques, ans , role), JsonRequestBehavior.AllowGet);

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
            if (EmployeeId != null)
            {
              
                DataSet ds1 = Db.myGetDS("exec RDD_CompareUser '" + EmployeeId + "'");
                string name = ds1.Tables[0].Rows[0]["LoginName"].ToString();
                
                if (username.ToLower() == name.ToLower())
                {
                    string com = "True";
                    ViewBag.comparename = com;
                }
            }
          
            DataSet ds = Db.myGetDS("EXEC RDD_GetUserType '" + username + "'");
            List<RDD_EmployeeRegistration> loginuserList = new List<RDD_EmployeeRegistration>();
            if (ds.Tables.Count > 0)
            {
                for (int i = 0; i < ds.Tables[0].Rows.Count; i++)
                {
                    RDD_EmployeeRegistration EmpLst = new RDD_EmployeeRegistration();
                    EmpLst.IsUserInRoleHeadOfFinance = ds.Tables[0].Rows[i]["IsFinanceUser"].ToString();
                    EmpLst.IsUserInRoleHR = ds.Tables[0].Rows[i]["IsHRUser"].ToString();


                    loginuserList.Add(EmpLst);
                }
            }
            ViewBag.loginuserList = loginuserList;


            Db.constr = System.Configuration.ConfigurationManager.ConnectionStrings["tejSAP"].ConnectionString;
            DataSet DS = Db.myGetDS("EXEC EmpReg_GetDDLDataToBind");
            List<RDD_EmployeeRegistration> DeptList = new List<RDD_EmployeeRegistration>();
            if (DS.Tables.Count > 0)
            {
                for (int i = 0; i < DS.Tables[0].Rows.Count; i++)
                {
                    RDD_EmployeeRegistration DeptLst = new RDD_EmployeeRegistration();
                    DeptLst.DeptId = Convert.ToInt32(DS.Tables[0].Rows[i]["DeptId"]);
                    DeptLst.DeptName = DS.Tables[0].Rows[i]["DeptName"].ToString();
                    DeptList.Add(DeptLst);
                }
            }

            List<RDD_EmployeeRegistration> DesigList = new List<RDD_EmployeeRegistration>();
            if (DS.Tables.Count > 0)
            {
                for (int i = 0; i < DS.Tables[1].Rows.Count; i++)
                {
                    RDD_EmployeeRegistration DesigLst = new RDD_EmployeeRegistration();
                    DesigLst.DesigId = Convert.ToInt32(DS.Tables[1].Rows[i]["DesigId"]);
                    DesigLst.DesigName = DS.Tables[1].Rows[i]["DesigName"].ToString();
                    DesigList.Add(DesigLst);
                }

            }

            List<RDD_EmployeeRegistration> StatusList = new List<RDD_EmployeeRegistration>();
            if (DS.Tables.Count > 0)
            {
                for (int i = 0; i < DS.Tables[2].Rows.Count; i++)
                {
                    RDD_EmployeeRegistration StatusLst = new RDD_EmployeeRegistration();
                    StatusLst.StatusId = Convert.ToInt32(DS.Tables[2].Rows[i]["StatusId"]);
                    StatusLst.StatusName = DS.Tables[2].Rows[i]["StatusName"].ToString();
                    StatusList.Add(StatusLst);
                }

            }


            DS = Db.myGetDS("EXEC RDD_GetManagerList '"+username+"'");
            List<RDD_EmployeeRegistration> ManagerList = new List<RDD_EmployeeRegistration>();
            if (DS.Tables.Count > 0)
            {
                for (int i = 0; i < DS.Tables[0].Rows.Count; i++)
                {
                    RDD_EmployeeRegistration MangLst = new RDD_EmployeeRegistration();
                    MangLst.ManagerId = Convert.ToInt32(DS.Tables[0].Rows[i]["EmployeeId"]);
                    MangLst.ManagerName = DS.Tables[0].Rows[i]["Empname"].ToString();
                    ManagerList.Add(MangLst);

                }
            }

            DS = Db.myGetDS("EXEC RDD_BU");
            List<RDD_EmployeeRegistration> BUList = new List<RDD_EmployeeRegistration>();
            if (DS.Tables.Count > 0)
            {
                for (int i = 0; i < DS.Tables[0].Rows.Count; i++)
                {
                    RDD_EmployeeRegistration BULst = new RDD_EmployeeRegistration();
                    BULst.u_bugroup = DS.Tables[0].Rows[i]["BUGroup"].ToString();

                    BUList.Add(BULst);
                }
            }

            DS = Db.myGetDS("EXEC RDD_GETCountryCode");
            List<RDD_EmployeeRegistration> CountryList = new List<RDD_EmployeeRegistration>();
            if (DS.Tables.Count > 0)
            {
                for (int i = 0; i < DS.Tables[0].Rows.Count; i++)
                {
                    RDD_EmployeeRegistration CouLst = new RDD_EmployeeRegistration();

                    CouLst.CountryCode = DS.Tables[0].Rows[i]["CountryCode"].ToString();
                    CouLst.Country = DS.Tables[0].Rows[i]["Country"].ToString();
                    CountryList.Add(CouLst);

                }
            }
           

            DS = Db.myGetDS("EXEC RDD_GETCountryCode");
            List<RDD_EmployeeRegistration> ddlCountry = new List<RDD_EmployeeRegistration>();
            if (DS.Tables.Count > 0)
            {
                for (int i = 0; i < DS.Tables[0].Rows.Count; i++)
                {
                    RDD_EmployeeRegistration CouLst = new RDD_EmployeeRegistration();
                    CouLst.CountryCodeName = DS.Tables[0].Rows[i]["CountryCode"].ToString();
                    CouLst.Country = DS.Tables[0].Rows[i]["Country"].ToString();
                    ddlCountry.Add(CouLst);

                }
            }

          
            DS = Db.myGetDS("select dbo.GetRDDEmpNo() as  EmpNo ");

             string NextEmpNo = DS.Tables[0].Rows[0]["EmpNO"].ToString();


            DS = Db.myGetDS("EXEC RDD_GETCurrency");
            List<RDD_EmployeeRegistration> CurrencyList = new List<RDD_EmployeeRegistration>();
            if (DS.Tables.Count > 0)
            {
                for (int i = 0; i < DS.Tables[0].Rows.Count; i++)
                {
                    RDD_EmployeeRegistration CurrencyLst = new RDD_EmployeeRegistration();
                    CurrencyLst.Currency = (DS.Tables[0].Rows[i]["Currency"]).ToString();

                    CurrencyList.Add(CurrencyLst);
                }
            }      

            ViewBag.DeptList = new SelectList(DeptList, "DeptId", "DeptName");
            ViewBag.DesigList = new SelectList(DesigList, "DesigId", "DesigName");
            ViewBag.StatusList = new SelectList(StatusList, "StatusId", "StatusName");
            ViewBag.BUList = new SelectList(BUList, "u_bugroup", "u_bugroup");

            ViewBag.CountryList = CountryList;
            ViewBag.ManagerList = new SelectList(ManagerList, "ManagerId", "Managername");
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
                return View(objemp);
            }
            else
            {
                RDD_EmployeeRegistration objemp = EmpDbOp.Edit(EmployeeId);
              //  Session["FILE"] = objemp.ImagePath;
                ViewBag.EmployeeNo = objemp.EmployeeNo;
                
                objemp.Editflag = true;

              
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

    }
}



