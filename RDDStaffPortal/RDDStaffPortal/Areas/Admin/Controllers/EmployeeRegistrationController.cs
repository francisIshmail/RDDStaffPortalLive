using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using RDDStaffPortal.Areas.Admin.Models;
using RDDStaffPortal.DAL.InitialSetup;
using RDDStaffPortal.DAL.DataModels;
using System.Data;
using RDDStaffPortal.DAL;

namespace RDDStaffPortal.Areas.Admin.Controllers
{
    public class EmployeeRegistrationController : Controller
    {
        EmployeeRegistrationDbOperation EmpDbOp = new EmployeeRegistrationDbOperation();
        // GET: Admin/EmployeeRegistration
        public ActionResult Index()
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

            DS = Db.myGetDS("EXEC RDD_BU");
            List<RDD_EmployeeRegistration> BUList = new List<RDD_EmployeeRegistration>();
            if (DS.Tables.Count > 0)
            {
                for (int i = 0; i < DS.Tables[0].Rows.Count; i++)
                {
                    RDD_EmployeeRegistration BULst = new RDD_EmployeeRegistration();
                    BULst.u_bugroup=DS.Tables[0].Rows[i]["BUGroup"].ToString();

                    BUList.Add(BULst);
                }
            }

            DS = Db.myGetDS("select CountryCode from rddCountriesList");
            List<RDD_EmployeeRegistration> CountryList = new List<RDD_EmployeeRegistration>();
            if (DS.Tables.Count > 0)
            {
                for (int i = 0; i < DS.Tables[0].Rows.Count; i++)
                {
                    RDD_EmployeeRegistration CouLst = new RDD_EmployeeRegistration();
                    CouLst.CountryCode = DS.Tables[0].Rows[i]["CountryCode"].ToString();
                    CountryList.Add(CouLst);

                }
            }

                    ViewBag.DeptList = new SelectList(DeptList, "DeptId", "DeptName");
            ViewBag.DesigList = new SelectList(DesigList, "DesigId", "DesigName");
            ViewBag.BUList = new SelectList(BUList, "u_bugroup", "u_bugroup");

            ViewBag.CountryList = CountryList;
            return View();

        }
        public ActionResult GetBuList(string bugroup)
        {
           // Db.constr = System.Configuration.ConfigurationManager.ConnectionStrings["tejSAP"].ConnectionString;
           //DataSet DS = Db.myGetDS("select * from SAPAE.dbo.OITB  where u_bugroup=''");
           // List<RDD_EmployeeRegistration> CountryList = new List<RDD_EmployeeRegistration>();
           // if (DS.Tables.Count > 0)
           // {
           //     for (int i = 0; i < DS.Tables[0].Rows.Count; i++)
           //     {
           //         RDD_EmployeeRegistration CouLst = new RDD_EmployeeRegistration();
           //         CouLst.CountryCode = DS.Tables[0].Rows[i]["CountryCode"].ToString();
           //         CountryList.Add(CouLst);

           //     }
           // }

            return View();

        }






        public JsonResult AddEmpReg(EmployeeRegistration Emp)
        {
            string result = string.Empty;
            try
            {
                RDD_EmployeeRegistration rdd_empreg = new RDD_EmployeeRegistration();

                rdd_empreg.EmployeeId = Emp.EmployeeId;
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


        
 