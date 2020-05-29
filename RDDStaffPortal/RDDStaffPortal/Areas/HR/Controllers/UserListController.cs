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

namespace RDDStaffPortal.Areas.HR.Controllers
{
    [Authorize]

    public class UserListController : Controller
    {
        // GET: HR/UserList
        public ActionResult Index()
        {

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
            DataSet DS = Db.myGetDS("EXEC RDD_DisplayEmployeeList");
            List<RDD_EmployeeRegistration> EmpDisplayList = new List<RDD_EmployeeRegistration>();
            if (DS.Tables.Count > 0)
            {
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


                //for (int i = 0; i < DS.Tables[1].Rows.Count; i++)

                //{
                //    RDD_EmployeeRegistration EmpLst1 = new RDD_EmployeeRegistration();
                //    DataTable Ds = DS.Tables[1];

                //    if (DS.Tables[1].Rows[i]["ImagePath"] != null && !DBNull.Value.Equals(DS.Tables[1].Rows[i]["ImagePath"]))
                //    {
                //        EmpLst1.ImagePath = (byte[])DS.Tables[1].Rows[i]["ImagePath"];
                //    }

                //}



                ViewBag.EmpDisplayList = EmpDisplayList;
            }
                return View();
            }
        }



    }   