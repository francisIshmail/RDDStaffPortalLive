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

namespace RDDStaffPortal.Areas.HR.Controllers
{
    public class EmployeeListController : Controller
    {
        // GET: HR/EmployeeList
        public ActionResult Index()
        {
            Db.constr = System.Configuration.ConfigurationManager.ConnectionStrings["tejSAP"].ConnectionString;
            DataSet DS = Db.myGetDS("EXEC RDD_DisplayEmployeeList");
            List <RDD_EmployeeRegistration> EmpDisplayList = new List<RDD_EmployeeRegistration>();
            if (DS.Tables.Count > 0)
            {
                for (int i = 0; i < DS.Tables[0].Rows.Count; i++)
                {
                    RDD_EmployeeRegistration EmpLst = new RDD_EmployeeRegistration();
                    EmpLst.EmployeeId= Convert.ToInt32(DS.Tables[0].Rows[i]["EmployeeId"]);
                    EmpLst.FName = DS.Tables[0].Rows[i]["FName"].ToString();
                    EmpLst.LName = DS.Tables[0].Rows[i]["LName"].ToString();
                    EmpLst.Country = DS.Tables[0].Rows[i]["Country"].ToString();
                    EmpLst.DesigName= DS.Tables[0].Rows[i]["DesigName"].ToString();
                    EmpLst.Email = DS.Tables[0].Rows[i]["Email"].ToString();
                    EmpLst.Contact_No = DS.Tables[0].Rows[i]["Contact_No"].ToString();
                    EmpLst.EmployeeNo= DS.Tables[0].Rows[i]["EmployeeNo"].ToString();
                    EmpLst.Ext_no = DS.Tables[0].Rows[i]["Ext_no"].ToString();

                    EmpDisplayList.Add(EmpLst);
                }
            }
            ViewBag.EmpDisplayList = EmpDisplayList;
            return View();
        }


        //public ActionResult Edit()
        //{
        //Redirect.ToAction()
        
        //}
    }

}