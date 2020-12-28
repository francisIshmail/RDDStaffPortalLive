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
                DataTable dtModule =  DS.Tables[0];
                DataRowCollection drc = dtModule.Rows;
                foreach (DataRow dr1 in drc)
                {
                    RDD_EmployeeRegistration EmpLst = new RDD_EmployeeRegistration();
                    ViewBag.pageCurrent = !string.IsNullOrWhiteSpace(dr1["TotalCount"].ToString()) ? Convert.ToInt32(dr1["TotalCount"].ToString()) : 0;
                    EmpLst.EmployeeId = !string.IsNullOrWhiteSpace(dr1["EmployeeId"].ToString()) ? Convert.ToInt32(dr1["EmployeeId"].ToString()) : 0;

                    EmpLst.FName = !string.IsNullOrWhiteSpace(dr1["FName"].ToString()) ? dr1["FName"].ToString() : "";
                    EmpLst.LName = !string.IsNullOrWhiteSpace(dr1["LName"].ToString()) ? dr1["LName"].ToString() : "";
                    EmpLst.Country = !string.IsNullOrWhiteSpace(dr1["Country"].ToString()) ? dr1["Country"].ToString() : "";
                    EmpLst.DesigName = !string.IsNullOrWhiteSpace(dr1["DesigName"].ToString()) ? dr1["DesigName"].ToString() : "";
                    EmpLst.Email = !string.IsNullOrWhiteSpace(dr1["Email"].ToString()) ? dr1["Email"].ToString() : "";
                    EmpLst.DeptName = !string.IsNullOrWhiteSpace(dr1["DeptName"].ToString()) ? dr1["DeptName"].ToString() : "";
                    EmpLst.Contact_No = !string.IsNullOrWhiteSpace(dr1["Contact_No"].ToString()) ? dr1["Contact_No"].ToString() : "";
                    EmpLst.Ext_no = !string.IsNullOrWhiteSpace(dr1["Ext_no"].ToString()) ? dr1["Ext_no"].ToString() : "";
                    EmpLst.About = !string.IsNullOrWhiteSpace(dr1["AboutUs"].ToString()) ? dr1["AboutUs"].ToString() : "";
                    // EmpLst.ProfileCompletedPercentage = Convert.ToInt32(DS.Tables[0].Rows[i]["ProfileCompletedPercentage"].ToString());
                    if (dr1["ProfileCompletedPercentage"] != null && !DBNull.Value.Equals(dr1["ProfileCompletedPercentage"]))
                    {
                        EmpLst.ProfileCompletedPercentage = Convert.ToInt32(dr1["ProfileCompletedPercentage"]);
                    }

                    if (dr1["ImagePath"] != null && dr1["ImagePath"].ToString().Length > 0)

                    {
                        EmpLst.ImagePath = (byte[])dr1["ImagePath"];
                    }
                    else
                    {

                        EmpLst.ImagePath = file;
                    }

                    DataRow[] dr = DS.Tables[0].Select("Country = '" + EmpLst.Country + "'");

                    List<CountryLists> Countrylst1 = new List<CountryLists>();
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
                //    for (int i = 0; i < DS.Tables[0].Rows.Count; i++)
                //{
                    
                //}




                
            }
            ViewBag.EmpDisplayList = EmpDisplayList;
            return View();
            }
        }



    }   