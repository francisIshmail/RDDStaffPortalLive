using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

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
using System.Net.Mime;
using System.Web.Script.Serialization;
using System.Data.OleDb;

namespace RDDStaffPortal.Areas.SAP.Controllers
{
    public class ItemMasterController : Controller
    {
        // GET: SAP/ItemMaster

        ItemMaster_DBOperation ItemMaster_DBOperation = new ItemMaster_DBOperation();
        public ActionResult Index()
        {
            return View();
        }

        public ActionResult Get_BindDDLList(string type,string value)
        {
            ContentResult retVal = null;
            DataSet DS;

            try
            {
                DS = ItemMaster_DBOperation.Get_BindDDLList(type,value);

                if (DS.Tables.Count > 0)
                    retVal = Content(JsonConvert.SerializeObject(DS), "application/json");
                else
                    retVal = null;

                return retVal;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public ActionResult Part_ToAddNew_Value(string insfor, string value, string descr, string type)
        {
            ContentResult retVal = null;
            DataSet DS;

            try
            {
                DS = ItemMaster_DBOperation.Part_ToAddNew_Value(insfor, value, descr,type );

                if (DS.Tables.Count > 0)
                    retVal = Content(JsonConvert.SerializeObject(DS), "application/json");
                else
                    retVal = null;

                return retVal;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public ActionResult Connet_To_SAP(string dbname)
        {
            ContentResult retVal = null;
            DataSet DS;
            try
            {
                DS = ItemMaster_DBOperation.Connet_To_SAPDB(dbname);

                if (DS.Tables.Count > 0)
                {
                    retVal = Content(JsonConvert.SerializeObject(DS), "application/json");
                }
                return retVal;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public ActionResult Add_ItemToSAPDB(string DBList,string itmCode,string itmDesc,int mfrId,int itmGrpId,string itmGrpCode,string itmBU,string itmProductCategory,string itmPL,string itmProductGrp,double Lenght,double Width,double Height,double Weight)
        {
            ContentResult retVal = null;
            DataSet DS;
            try
            {
                DS = ItemMaster_DBOperation.Add_ItemToSAPDB(DBList,itmCode,itmDesc,mfrId,itmGrpId,itmGrpCode,itmBU,itmProductCategory,itmPL,itmProductGrp,Lenght,Width,Height,Weight);

                if (DS.Tables.Count > 0)
                {
                    retVal = Content(JsonConvert.SerializeObject(DS), "application/json");
                }
                return retVal;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
    }
}