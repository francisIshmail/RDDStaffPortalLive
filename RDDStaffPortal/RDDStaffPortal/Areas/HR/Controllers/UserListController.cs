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
using System.Diagnostics;
using RDDStaffPortal.WebServices;
using System.Data.SqlClient;

namespace RDDStaffPortal.Areas.HR.Controllers
{
    [Authorize]

    public class UserListController : Controller
    {
        AccountService accountservice = new AccountService();
        Common cm = new Common();
        CommonFunction Com = new CommonFunction();
        // GET: HR/UserList
        //public ActionResult gethistory(int empid, string tblname)
        //{

        //    return Json(cm.GetChangeLog(empid, tblname), JsonRequestBehavior.AllowGet);
        //}
        public ActionResult Index(string currentFilter, string searchString , int? page)
        {

            if (searchString == null)
            {
                currentFilter = "";
            }
            else
            {
                currentFilter = searchString;
                
            }
            ViewBag.txtSearch = searchString;
            ViewBag.numSize = 10;
            string loginuser = User.Identity.Name;
            Db.constr = System.Configuration.ConfigurationManager.ConnectionStrings["tejSAP"].ConnectionString;
            //DataSet ds = Db.myGetDS("EXEC RDD_GetUserType '"+loginuser+"'");
            //List<RDD_EmployeeRegistration> loginuserList = new List<RDD_EmployeeRegistration>();
            //if (ds.Tables.Count > 0)
            //{
            //    for (int i = 0; i < ds.Tables[0].Rows.Count; i++)
            //    {
            //        RDD_EmployeeRegistration EmpLst = new RDD_EmployeeRegistration();
            //        EmpLst.IsUserInRoleHeadOfFinance = ds.Tables[0].Rows[i]["IsFinanceUser"].ToString();
            //        EmpLst.IsUserInRoleHR = ds.Tables[0].Rows[i]["IsHRUser"].ToString();                   
            //        loginuserList.Add(EmpLst);
            //    }
            //}

            DataSet ds = Db.myGetDS("EXEC RDD_EMPLOYEES_DISBALE 'III','" + User.Identity.Name+"'");
            if (ds.Tables.Count > 0)
            {
                for (int i = 0; i < ds.Tables[0].Rows.Count; i++)
                {
                    var columName = (ds.Tables[0].Rows[i]["ColumName"]).ToString();
                    ViewData.Add(columName, true);

                }
            }

            //ViewBag.loginuserList = loginuserList;
            

            string Temppath = Server.MapPath("/Images/TempLogo/defaultimg.jpg");
            // objemp.LogoType = ".jpg";
            byte[] file;
            using (var stream = new FileStream(Temppath, FileMode.Open, FileAccess.Read))
            {
                using (var reader = new BinaryReader(stream))
                {
                    file = reader.ReadBytes((int)stream.Length);
                }
            }

            Db.constr = System.Configuration.ConfigurationManager.ConnectionStrings["tejSAP"].ConnectionString;
            if (page > 0)
            {
                page = page;
            }
            else
            {
                page = 1;
            }
            // DataSet DS = Db.myGetDS("EXEC RDD_DisplayEmployeeList "+ currentFilter);
            SqlParameter[] Para = {

                    new SqlParameter("@p_search",currentFilter),
                     new SqlParameter("@p_pagesize",10),
                     new SqlParameter("@p_pageno",page),
                     new SqlParameter("@p_SortColumn","EmployeeId"),
                     new SqlParameter("@p_SortOrder","ASC")


                };
            DataSet DS = Com.ExecuteDataSet("RDD_DisplayEmployeeListtest",CommandType.StoredProcedure,Para);
            List<RDD_EmployeeRegistration> EmpDisplayList = new List<RDD_EmployeeRegistration>();
            if (DS.Tables.Count > 0)
            {
                ViewBag.pageCurrent= !string.IsNullOrWhiteSpace(DS.Tables[0].Rows[0]["TotalCount"].ToString()) ? Convert.ToInt32(DS.Tables[0].Rows[0]["TotalCount"].ToString()) : 0;
                for (int i = 0; i < DS.Tables[0].Rows.Count; i++)
                {
                    RDD_EmployeeRegistration EmpLst = new RDD_EmployeeRegistration();
                    EmpLst.EmployeeId = !string.IsNullOrWhiteSpace(DS.Tables[0].Rows[i]["EmployeeId"].ToString()) ? Convert.ToInt32(DS.Tables[0].Rows[i]["EmployeeId"].ToString()) : 0;

                    EmpLst.FName = !string.IsNullOrWhiteSpace(DS.Tables[0].Rows[i]["FName"].ToString()) ? DS.Tables[0].Rows[i]["FName"].ToString() : "";
                    EmpLst.LName = !string.IsNullOrWhiteSpace(DS.Tables[0].Rows[i]["LName"].ToString()) ? DS.Tables[0].Rows[i]["LName"].ToString() : "";
                    EmpLst.Country = !string.IsNullOrWhiteSpace(DS.Tables[0].Rows[i]["Country"].ToString()) ? DS.Tables[0].Rows[i]["Country"].ToString() : "";
                    EmpLst.DesigName = !string.IsNullOrWhiteSpace(DS.Tables[0].Rows[i]["DesigName"].ToString()) ? DS.Tables[0].Rows[i]["DesigName"].ToString() : "";
                    EmpLst.Email = !string.IsNullOrWhiteSpace(DS.Tables[0].Rows[i]["Email"].ToString()) ? DS.Tables[0].Rows[i]["Email"].ToString() : "";
                    EmpLst.DeptName = !string.IsNullOrWhiteSpace(DS.Tables[0].Rows[i]["DeptName"].ToString()) ? DS.Tables[0].Rows[i]["DeptName"].ToString() : "";
                    EmpLst.Contact_No = !string.IsNullOrWhiteSpace(DS.Tables[0].Rows[i]["Contact_No"].ToString()) ? DS.Tables[0].Rows[i]["Contact_No"].ToString() : "";
                    EmpLst.Ext_no = !string.IsNullOrWhiteSpace(DS.Tables[0].Rows[i]["Ext_no"].ToString()) ? DS.Tables[0].Rows[i]["Ext_no"].ToString() : "";
                    EmpLst.About = !string.IsNullOrWhiteSpace(DS.Tables[0].Rows[i]["AboutUs"].ToString()) ? DS.Tables[0].Rows[i]["AboutUs"].ToString() : "";
                   // EmpLst.ProfileCompletedPercentage = Convert.ToInt32(DS.Tables[0].Rows[i]["ProfileCompletedPercentage"].ToString());
                    if (DS.Tables[0].Rows[i]["ProfileCompletedPercentage"] != null && !DBNull.Value.Equals(DS.Tables[0].Rows[i]["ProfileCompletedPercentage"]))
                    {
                        EmpLst.ProfileCompletedPercentage = Convert.ToInt32(DS.Tables[0].Rows[i]["ProfileCompletedPercentage"]);
                    }

                    if (DS.Tables[0].Rows[i]["ImagePath"] != null && DS.Tables[0].Rows[i]["ImagePath"].ToString().Length > 0)

                    {
                        EmpLst.ImagePath = (byte[])DS.Tables[0].Rows[i]["ImagePath"];
                    }
                    else
                    {

                        EmpLst.ImagePath = file;
                    }

                    DataRow[] dr = DS.Tables[0].Select("Country = '" + EmpLst.Country+"'");

                    List <CountryLists> Countrylst1 = new List<CountryLists>();
                    for (int j = 0; j < dr.Length; j++)
                    {
                        Countrylst1.Add(new CountryLists
                        {
                            Empid = Convert.ToInt32(dr[j]["EmployeeId"].ToString()),
                            Imagepath = (byte[])dr[j]["ImagePath"]
                        }
                      );

                    }

                    EmpLst.CountryLists = Countrylst1;

                        string base64String = Convert.ToBase64String(EmpLst.ImagePath);
                    EmpLst.ImagePath1 = base64String;

                    EmpDisplayList.Add(EmpLst); 
                }




                
            }
            ViewBag.EmpDisplayList = EmpDisplayList;
            return View();
            }
        }



    }   