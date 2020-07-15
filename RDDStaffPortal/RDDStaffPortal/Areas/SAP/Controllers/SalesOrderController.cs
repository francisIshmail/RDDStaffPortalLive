using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using RDDStaffPortal.Areas.HR.Models;
using RDDStaffPortal.DAL.HR;
using RDDStaffPortal.DAL.DataModels;
using RDDStaffPortal.DAL.DataModels.SAP;
using RDDStaffPortal.DAL.SAP;
using System.Data;
using RDDStaffPortal.DAL;
using System.IO;
using System.Web.Helpers;
using System.Net;
using System.Web.Routing;
using RDDStaffPortal.WebServices;
using Microsoft.Ajax.Utilities;

using System.Runtime.InteropServices;
using System.Runtime.Remoting.Messaging;
using System.Text;
using Newtonsoft.Json;

using System.Web.UI;
using System.Web.UI.WebControls;
using DataTable = System.Data.DataTable;
using ClosedXML.Excel;
using System.Data.SqlClient;

namespace RDDStaffPortal.Areas.SAP.Controllers
{
    public class SalesOrderController : Controller
    {
        // GET: SAP/SalesOrder

        SalesOrder_DBOperation SalesOrder_DBOperation = new SalesOrder_DBOperation();
        public ActionResult Index()
        {

            Db.constr = System.Configuration.ConfigurationManager.ConnectionStrings["tejSAP"].ConnectionString;

            DataSet DS = Db.myGetDS("EXEC RDD_SOR_Get_UserWise_DBList '1','1'");
            List<RDD_OSOR> ddlDBList = new List<RDD_OSOR>();
            if (DS.Tables.Count > 0)
            {
                for (int i = 0; i < DS.Tables[0].Rows.Count; i++)
                {
                    RDD_OSOR DBList = new RDD_OSOR();
                    DBList.DBCode = DS.Tables[0].Rows[i]["Code"].ToString();
                    DBList.DBName = DS.Tables[0].Rows[i]["Descr"].ToString();
                    ddlDBList.Add(DBList);

                }
            }
            ViewBag.ddlDBList = new SelectList(ddlDBList, "DBCode", "DBName");
            return View();
        }

        public ActionResult Get_BindDDLList(string dbname)
        {
            string retVal = string.Empty;
            DataSet DS;
           
            try
            {
                 DS= SalesOrder_DBOperation.Get_BindDDLList(dbname);                
                return Content(JsonConvert.SerializeObject(DS), "application/json");

            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public ActionResult GetCustomers(string prefix, string dbname, string field)
        {
            string retVal = string.Empty;
            SqlDataReader rdr;
            List<string> customers = new List<string>();

            try
            {
                rdr = SalesOrder_DBOperation.GetCustomers(prefix, dbname, field);

                while (rdr.Read())
                {
                    customers.Add(string.Format("{0}#{1}", rdr["CardName"], rdr["CardCode"]));
                }
                return  Content(JsonConvert.SerializeObject(customers.ToArray()), "application/json"); 

            }
            catch (Exception ex)
            {
                throw ex;
            }

        }

    }
}