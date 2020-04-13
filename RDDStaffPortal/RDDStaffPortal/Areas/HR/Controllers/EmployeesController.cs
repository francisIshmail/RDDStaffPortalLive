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



namespace RDDStaffPortal.Areas.HR.Controllers
{
    public class EmployeesController : Controller
    {
        EmployeeRegistrationDbOperation EmpDbOp = new EmployeeRegistrationDbOperation();
        // GET: Admin/EmployeeRegistration
        //public ActionResult Update(int EmployeeId)
        //{




        //    return View();

        //}


        //public ActionResult Edit(int EmployeeId)
        //{
        //    string result = string.Empty;
        //    try
        //    {
        //        RDD_EmployeeRegistration objemp = EmpDbOp.Edit(EmployeeId);


        //    }
        //    catch (Exception ex)
        //    {
        //        result = "Error occured :" + ex.Message;
        //    }

        //    return View("Index");
        //    //  return Json(result, JsonRequestBehavior.AllowGet);
        //}
        public ActionResult Index(int? EmployeeId)

        {

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


            DS = Db.myGetDS("EXEC RDD_GetManagerList");
            List<RDD_EmployeeRegistration> ManagerList = new List<RDD_EmployeeRegistration>();
            if (DS.Tables.Count > 0)
            {
                for (int i = 0; i < DS.Tables[0].Rows.Count; i++)
                {
                    RDD_EmployeeRegistration MangLst = new RDD_EmployeeRegistration();
                    MangLst.ManagerId = Convert.ToInt32(DS.Tables[0].Rows[i]["ManagerId"]);
                    MangLst.ManagerName = DS.Tables[0].Rows[i]["ManagerName"].ToString();
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
            List<RDD_EmployeeRegistration> MaxId = new List<RDD_EmployeeRegistration>();
            if (DS.Tables.Count > 0)
            {
                for (int i = 0; i < DS.Tables[0].Rows.Count; i++)
                {
                    RDD_EmployeeRegistration EmpNO = new RDD_EmployeeRegistration();
                    EmpNO.EmployeeNo = DS.Tables[0].Rows[i]["EmpNO"].ToString();

                    MaxId.Add(EmpNO);

                }
            }

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
            ViewBag.MaxId = MaxId;
            ViewBag.CurrencyList = new SelectList(CurrencyList, "Currency", "Currency"); 

            if (EmployeeId == null)
            {
                RDD_EmployeeRegistration objemp = new RDD_EmployeeRegistration();
                objemp.Editflag = false;
                return View(objemp);
            }
            else
            {
                RDD_EmployeeRegistration objemp = EmpDbOp.Edit(EmployeeId);
                objemp.Editflag = true;
                return View(objemp);
            }


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



        public JsonResult AddEmpReg(Employees Emp)
        {
            //if(Emp.my_file != null)
            //{
            //    string filename = Path.GetFileNameWithoutExtension(Emp.my_file.FileName);
            //    string extension = Path.GetExtension(Emp.my_file.FileName);
            //    filename = filename + DateTime.Now.ToString("yymmssff") + extension;
            //    Emp.ImagePath = filename;
            //    Emp.my_file.SaveAs(Path.Combine(Server.MapPath("~/HR/Images"), filename));
               
            //}
           
          
            string result = string.Empty;
            try
            {
                RDD_EmployeeRegistration rdd_empreg = new RDD_EmployeeRegistration();
                rdd_empreg.ImagePath = Emp.ImagePath;
                

                rdd_empreg.EmployeeId = Emp.EmployeeId;
                rdd_empreg.CountryCodeName = Emp.CountryCodeName;
                rdd_empreg.ManagerName = Emp.ManagerName;
                rdd_empreg.EmployeeNo = Emp.EmployeeNo;
                rdd_empreg.FName = Emp.FName;
                rdd_empreg.LName = Emp.LName;
                rdd_empreg.Email = Emp.Email;
                rdd_empreg.Gender = Emp.Gender;
                rdd_empreg.Current_Address = Emp.Current_Address;
                rdd_empreg.Permanent_Address = Emp.Permanent_Address;
                rdd_empreg.Contact_No = Emp.Contact_No;
                rdd_empreg.Ext_no = Emp.Ext_no;
                rdd_empreg.IM_Id = Emp.IM_Id;
                rdd_empreg.Marital_Status = Emp.Marital_Status;
                rdd_empreg.DOB = Emp.DOB;

                rdd_empreg.Citizenship = Emp.Citizenship;
                rdd_empreg.DesigId = Emp.DesigId;
                rdd_empreg.DeptId = Emp.DeptId;
                rdd_empreg.Emergency_Contact = Emp.Emergency_Contact;
                rdd_empreg.passport_no = Emp.passport_no;

                rdd_empreg.StatusId = Emp.StatusId;
                rdd_empreg.type_of_employement = Emp.type_of_employement;
                rdd_empreg.Joining_Date = Emp.Joining_Date;
                rdd_empreg.No_child = Emp.No_child;
                rdd_empreg.National_id = Emp.National_id;
                rdd_empreg.Contract_Start_date = Emp.Contract_Start_date;
                rdd_empreg.Note = Emp.Note;



                rdd_empreg.CreatedBy = User.Identity.Name;

                rdd_empreg.FId = Emp.FId;

                rdd_empreg.Currency = Emp.Currency;
                rdd_empreg.Salary = Emp.Salary;
                rdd_empreg.Salary_Start_Date = Emp.Salary_Start_Date;
                rdd_empreg.Remark = Emp.Remark;
                rdd_empreg.Account_No = Emp.Account_No;
                rdd_empreg.Bank_Name = Emp.Bank_Name;
                rdd_empreg.Branch_Name = Emp.Branch_Name;
                rdd_empreg.Bank_Code = Emp.Bank_Code;
                rdd_empreg.Tax_no = Emp.Tax_no;
                rdd_empreg.Insurance_no = Emp.Insurance_no;
                rdd_empreg.other_ref_no = Emp.other_ref_no;


                rdd_empreg.CId = Emp.CId;
                rdd_empreg.CountryCode = Emp.CountryCode;
                //rdd_empreg.ItmsGrpNam = Emp.ItmsGrpNam;

                result = EmpDbOp.Save(rdd_empreg);

            }
            catch (Exception ex)
            {
                result = "Error occured :" + ex.Message;
            }
            return Json(result, JsonRequestBehavior.AllowGet);
        }
      
    }
}



